using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LegendsViewer.Legends.Events;

namespace LegendsViewer.Legends.Parser
{
    class SitesAndPopulationsParser
    {
        StreamReader SitesAndPops;
        string CurrentLine;
        World World;
        Site Site;
        Entity Owner;
        Entity Parent;

        public SitesAndPopulationsParser(World world, string sitesAndPopsFile)
        {
            World = world;
            SitesAndPops = new StreamReader(sitesAndPopsFile, Encoding.GetEncoding("windows-1252"));
        }

        public void Parse()
        {
            ReadLine();
            while (CurrentLine != "")
            {
                SkipWorldPopulations();
                ReadSite();
                ReadSiteOwner();
                ReadParentCiv();
                CheckSiteOwnership();
                ReadOfficials();
                ReadPopulations();
            }
            ReadOutdoorPopulations();
            ReadUndergroundPopulations();
            SitesAndPops.Close();
        }

        private void ReadLine()
        {
            CurrentLine = SitesAndPops.ReadLine();
        }

        private bool InSites()
        {
            return !SitesAndPops.EndOfStream && CurrentLine != "Outdoor Animal Populations (Including Undead)";
        }

        private bool SiteStart()
        {
            return !SitesAndPops.EndOfStream && !CurrentLine.StartsWith("\t");
        }

        private void SkipWorldPopulations()
        {
            if (CurrentLine == "Civilized World Population")
            {
                ReadLine();
                CurrentLine = SitesAndPops.ReadLine();
                while (CurrentLine != "" && !SitesAndPops.EndOfStream)
                {
                    if (CurrentLine == "") { CurrentLine = SitesAndPops.ReadLine(); continue; }
                    int count;
                    string population = Formatting.InitCaps(CurrentLine.Substring(CurrentLine.IndexOf(" ") + 1));
                    string countString = CurrentLine.Substring(1, CurrentLine.IndexOf(" ") - 1);
                    if (countString == "Unnumbered") count = Int32.MaxValue;
                    else count = Convert.ToInt32(countString);
                    World.CivilizedPopulations.Add(new Population(population, count));
                    CurrentLine = SitesAndPops.ReadLine();
                }
                World.CivilizedPopulations = World.CivilizedPopulations.OrderByDescending(population => population.Count).ToList();
                while (CurrentLine != "Sites")
                {
                    CurrentLine = SitesAndPops.ReadLine();
                }
            }
            if (CurrentLine == "Sites")
            {
                ReadLine();
                ReadLine();
            }
        }

        private void ReadSite()
        {
            string siteID = CurrentLine.Substring(0, CurrentLine.IndexOf(":"));
            Site = World.GetSite(Convert.ToInt32(siteID));
            if (Site != null)
            {
                string untranslatedName = Formatting.InitCaps(Formatting.ReplaceNonAscii(CurrentLine.Substring(CurrentLine.IndexOf(' ') + 1, CurrentLine.IndexOf(',') - CurrentLine.IndexOf(' ') - 1)));
                if (!string.IsNullOrWhiteSpace(Site.Name))
                {
                    Site.UntranslatedName = untranslatedName;
                }
                else
                {
                    // string name = Formatting.InitCaps(Formatting.ReplaceNonAscii(CurrentLine.Substring(CurrentLine.IndexOf('\"') + 1, CurrentLine.LastIndexOf('\"') - CurrentLine.IndexOf('\"') - 1)));
                    // TODO v0.43.XX has invalid information for important locations and advmode camps
                }
            }
            Owner = null;
            ReadLine();
        }

        private void ReadSiteOwner()
        {
            if (CurrentLine.Contains("Owner:"))
            {
                string entityName = CurrentLine.Substring(CurrentLine.IndexOf(":") + 2, CurrentLine.IndexOf(",") - CurrentLine.IndexOf(":") - 2);
                try
                {
                    Owner = World.GetEntity(entityName);
                }
                catch (Exception ex)
                {
                    Owner = World.EntitiesByName.FirstOrDefault(e => e.Name.Equals(entityName, StringComparison.OrdinalIgnoreCase) && e.Sites.Contains(Site));
                    if (Owner == null)
                    {
                        World.ParsingErrors.Report(ex.Message + ", Site Owner of " + Site.Name);
                    }
                }
                if (Owner != null)
                {
                    Owner.Race = Formatting.InitCaps(CurrentLine.Substring(CurrentLine.IndexOf(",") + 2, CurrentLine.Length - CurrentLine.IndexOf(",") - 2));
                    if (string.IsNullOrWhiteSpace(Owner.Race))
                    {
                        Owner.Race = "Unknown";
                    }
                }
                ReadLine();
            }
        }

        private void ReadParentCiv()
        {
            if (CurrentLine.Contains("Parent Civ:"))
            {
                string civName = CurrentLine.Substring(CurrentLine.IndexOf(":") + 2, CurrentLine.IndexOf(",") - CurrentLine.IndexOf(":") - 2);
                try
                {
                    Parent = World.GetEntity(civName);
                    Parent.Race = Formatting.InitCaps(CurrentLine.Substring(CurrentLine.IndexOf(",") + 2, CurrentLine.Length - CurrentLine.IndexOf(",") - 2));
                    if (string.IsNullOrWhiteSpace(Parent.Race))
                    {
                        Parent.Race = "Unknown";
                    }
                    if (Owner != null)
                    {
                        var current = Owner;
                        while (current.Parent != null)
                        {
                            current = current.Parent;
                        }
                        if (current != Parent)
                        {
                            current.Parent = Parent;
                        }
                        if (!Parent.Groups.Contains(Owner))
                        {
                            Parent.Groups.Add(Owner);
                        }
                    }
                }
                catch (Exception e)
                {
                    if (Owner != null)
                        World.ParsingErrors.Report(e.Message + ", Parent Civ of " + Owner.Name + ", Site Owner of " + Site.Name);
                    else
                        World.ParsingErrors.Report(e.Message + ", Parent Civ of Site Owner of " + Site.Name);
                }

                ReadLine();
            }
        }

        private void ReadOfficials()
        {
            if (CurrentLine.Contains(":")) //Read in Officials, law-givers, mayors etc, ex: law-giver: Oled Sugarynestled, human
            {
                while (!SitesAndPops.EndOfStream && CurrentLine.Contains(":") && !SiteStart())
                {
                    HistoricalFigure siteOfficial = null;
                    string officialName = Formatting.ReplaceNonAscii(CurrentLine.Substring(CurrentLine.IndexOf(":") + 2, CurrentLine.IndexOf(",") - CurrentLine.IndexOf(":") - 2));
                    try
                    {
                        siteOfficial = World.GetHistoricalFigure(officialName);
                    }
                    catch (Exception e)
                    {
                        World.ParsingErrors.Report(e.Message + ", Official of " + Site.Name);
                        ReadLine();
                        continue;
                    }
                    string siteOfficialPosition = CurrentLine.Substring(1, CurrentLine.IndexOf(":") - 1);
                    Site.Officials.Add(new Site.Official(siteOfficial, siteOfficialPosition));
                    ReadLine();
                }
            }
        }

        public void ReadPopulations()
        {
            List<Population> populations = new List<Population>();
            while (!SiteStart() && CurrentLine != "")
            {
                string race = Formatting.InitCaps(CurrentLine.Substring(CurrentLine.IndexOf(' ') + 1));
                race = Formatting.MakePopulationPlural(race);
                int count = Convert.ToInt32(CurrentLine.Substring(1, CurrentLine.IndexOf(' ') - 1));
                populations.Add(new Population(race, count));
                ReadLine();
            }

            Site.Populations = populations.OrderByDescending(pop => pop.Count).ToList();
            if (Owner != null)
            {
                Owner.AddPopulations(populations);
                if (Owner.Parent != null)
                    Owner.Parent.AddPopulations(populations);
            }
            World.SitePopulations.AddRange(populations);
        }

        public void CheckSiteOwnership()
        {
            //Check if a site was gained without a proper event from the xml
            if (Owner != null)
            {
                if (Site.OwnerHistory.Count == 0)
                {
                    new OwnerPeriod(Site, Owner, 1, "founded");
                }
                else if (Site.OwnerHistory.Last().Owner != Owner)
                {
                    bool found = false;
                    HistoricalFigure lastKnownOwner = Site.OwnerHistory.Last().Owner as HistoricalFigure;
                    if (lastKnownOwner != null)
                    {
                        if (lastKnownOwner.DeathYear != -1)
                        {
                            new OwnerPeriod(Site, Owner, lastKnownOwner.DeathYear, "after death of last owner (" + lastKnownOwner.DeathCause + ") took over");
                            found = true;
                        }
                        else if (Site.Type == "Vault" && Owner is Entity)
                        {
                            Site.OwnerHistory.Last().Owner = Owner;
                            found = true;
                        }
                    }
                    else if (Site.CurrentOwner == null)
                    {
                        new OwnerPeriod(Site, Owner, Site.OwnerHistory.Last().EndYear, "slowly repopulated after the site was " + Site.OwnerHistory.Last().EndCause);
                        found = true;
                    }
                    if(!found)
                    {
                        ChangeHFState lastSettledEvent = Site.Events.OfType<ChangeHFState>().LastOrDefault();
                        if (lastSettledEvent != null)
                        {
                            new OwnerPeriod(Site, Owner, lastSettledEvent.Year, "settled in");
                        }
                        else
                        {
                            World.ParsingErrors.Report("Site ownership conflict: " + Site.Name + ". Actually owned by " + Owner.ToLink(false) + " instead of " + Site.OwnerHistory.Last().Owner.ToLink(false));
                        }
                    }
                }
            }
            //check for loss of period ownership, since some some loss of ownership eventsList are missing
            if (Owner == null && Site.OwnerHistory.Count > 0 && Site.OwnerHistory.Last().EndYear == -1)
            {
                Site.OwnerHistory.Last().EndYear = World.Events.Last().Year - 1;
                Site.OwnerHistory.Last().EndCause = "abandoned";
            }
        }

        private void ReadOutdoorPopulations()
        {
            ReadLine();
            ReadLine();
            while (CurrentLine != "" && !SitesAndPops.EndOfStream)
            {
                if (CurrentLine == "") { CurrentLine = SitesAndPops.ReadLine(); continue; }
                string countString, population;
                int count;
                population = Formatting.InitCaps(CurrentLine.Substring(CurrentLine.IndexOf(" ") + 1));
                countString = CurrentLine.Substring(1, CurrentLine.IndexOf(" ") - 1);
                if (countString == "Unnumbered") count = Int32.MaxValue;
                else count = Convert.ToInt32(countString);
                World.OutdoorPopulations.Add(new Population(population, count));
                CurrentLine = SitesAndPops.ReadLine();
            }
            World.OutdoorPopulations = World.OutdoorPopulations.OrderByDescending(population => population.Count).ToList();
        }

        private void ReadUndergroundPopulations()
        {
            ReadLine();
            ReadLine();
            while (!SitesAndPops.EndOfStream)
            {
                if (CurrentLine == "") { CurrentLine = SitesAndPops.ReadLine(); continue; }
                string countString, population;
                int count;
                population = Formatting.InitCaps(CurrentLine.Substring(CurrentLine.IndexOf(" ") + 1));
                countString = CurrentLine.Substring(1, CurrentLine.IndexOf(" ") - 1);
                if (countString == "Unnumbered") count = Int32.MaxValue;
                else count = Convert.ToInt32(countString);
                World.UndergroundPopulations.Add(new Population(population, count));
                CurrentLine = SitesAndPops.ReadLine();
            }
            World.UndergroundPopulations = World.UndergroundPopulations.OrderByDescending(population => population.Count).ToList();
        }


    }
}
