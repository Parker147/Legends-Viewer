﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using LegendsViewer.Legends.Events;

namespace LegendsViewer.Legends.Parser
{
    public class SitesAndPopulationsParser : IDisposable
    {
        private readonly BackgroundWorker _worker;
        private readonly World _world;
        private readonly StreamReader _sitesAndPops;

        private string _currentLine;
        private Site _site;
        private Entity _owner;

        public SitesAndPopulationsParser(BackgroundWorker worker, World world, string sitesAndPopsFile)
        {
            _worker = worker;
            _world = world;
            _sitesAndPops = new StreamReader(sitesAndPopsFile, Encoding.GetEncoding("windows-1252"));
            _worker.ReportProgress(0, "\nParsing Sites and Populations...");
        }

        public void Parse()
        {
            ReadLine();
            ReadWorldPopulations();
            _worker.ReportProgress(0, "... Sites");
            while (_currentLine != "" && InSites())
            {
                ReadSite();
                ReadSiteOwner();
                ReadParentCiv();
                CheckSiteOwnership();
                ReadOfficials();
                ReadPopulations();
            }
            ReadOutdoorPopulations();
            ReadUndergroundPopulations();
            _sitesAndPops.Close();
        }

        private void ReadLine()
        {
            _currentLine = _sitesAndPops.ReadLine();
        }

        private bool InSites()
        {
            return !_sitesAndPops.EndOfStream && _currentLine != "Outdoor Animal Populations (Including Undead)";
        }

        private bool SiteStart()
        {
            return !_sitesAndPops.EndOfStream && !_currentLine.StartsWith("\t");
        }

        private void ReadWorldPopulations()
        {
            if (_currentLine == "Civilized World Population")
            {
                _worker.ReportProgress(0, "... World Populations");
                ReadLine();
                _currentLine = _sitesAndPops.ReadLine();
                while (!string.IsNullOrEmpty(_currentLine) && !_sitesAndPops.EndOfStream)
                {
                    if (string.IsNullOrEmpty(_currentLine))
                    {
                        _currentLine = _sitesAndPops.ReadLine();
                        continue;
                    }
                    string population = Formatting.InitCaps(_currentLine.Substring(_currentLine.IndexOf(" ", StringComparison.Ordinal) + 1));
                    string countString = _currentLine.Substring(1, _currentLine.IndexOf(" ", StringComparison.Ordinal) - 1);
                    var count = countString == "Unnumbered" ? int.MaxValue : Convert.ToInt32(countString);

                    _world.CivilizedPopulations.Add(new Population(population, count));
                    _currentLine = _sitesAndPops.ReadLine();
                }
                _world.CivilizedPopulations = _world.CivilizedPopulations.OrderByDescending(population => population.Count).ToList();
                while (_currentLine != "Sites")
                {
                    _currentLine = _sitesAndPops.ReadLine();
                }
            }
            if (_currentLine == "Sites")
            {
                ReadLine();
                ReadLine();
            }
        }

        private void ReadSite()
        {
            string siteId = _currentLine.Substring(0, _currentLine.IndexOf(":", StringComparison.Ordinal));
            _site = _world.GetSite(Convert.ToInt32(siteId));
            if (_site != null)
            {
                string untranslatedName = Formatting.InitCaps(Formatting.ReplaceNonAscii(_currentLine.Substring(_currentLine.IndexOf(' ') + 1, _currentLine.IndexOf(',') - _currentLine.IndexOf(' ') - 1)));
                if (!string.IsNullOrWhiteSpace(_site.Name))
                {
                    _site.UntranslatedName = untranslatedName;
                }
            }
            _owner = null;
            ReadLine();
        }

        private void ReadSiteOwner()
        {
            if (_currentLine.Contains("Owner:"))
            {
                string entityName = _currentLine.Substring(_currentLine.IndexOf(":", StringComparison.Ordinal) + 2,
                    _currentLine.IndexOf(",", StringComparison.Ordinal) - _currentLine.IndexOf(":", StringComparison.Ordinal) - 2);
                var entities = _world.Entities
                    .Where(entity => string.Compare(entity.Name, entityName, StringComparison.OrdinalIgnoreCase) == 0).ToList();
                if (entities.Count == 1)
                {
                    _owner = entities.First();
                }
                else if (entities.Count == 0)
                {
                    _world.ParsingErrors.Report($"Couldn\'t Find Entity: {entityName},\nSite Owner of {_site.Name}");
                }
                else
                {
                    var siteOwners = _site.OwnerHistory.Select(entry => entry.Owner).ToList();
                    if (siteOwners.Any())
                    {
                        foreach (var entity in entities)
                        {
                            if (siteOwners.Contains(entity))
                            {
                                _owner = entity;
                            }
                        }
                    }
#if DEBUG
                    if (_owner == null)
                    {
                        _world.ParsingErrors.Report($"Ambiguous ({entities.Count}) Site Ownership:\n{entityName}, Site Owner of {_site.Name}");
                    }
#endif
                }
                if (_owner != null)
                {
                    _owner.Race = Formatting.InitCaps(_currentLine.Substring(_currentLine.IndexOf(",", StringComparison.Ordinal) + 2,
                        _currentLine.Length - _currentLine.IndexOf(",", StringComparison.Ordinal) - 2));
                    if (string.IsNullOrWhiteSpace(_owner.Race))
                    {
                        _owner.Race = "Unknown";
                    }
                    if (!_owner.Sites.Contains(_site))
                    {
                        _owner.Sites.Add(_site);
                    }
                    if (!_owner.CurrentSites.Contains(_site))
                    {
                        _owner.CurrentSites.Add(_site);
                    }
                }
                ReadLine();
            }
        }

        private void ReadParentCiv()
        {
            if (_currentLine.Contains("Parent Civ:"))
            {
                Entity parent = null;
                string civName = _currentLine.Substring(_currentLine.IndexOf(":", StringComparison.Ordinal) + 2,
                    _currentLine.IndexOf(",", StringComparison.Ordinal) - _currentLine.IndexOf(":", StringComparison.Ordinal) - 2);
                var entities = _world.Entities
                    .Where(entity => string.Compare(entity.Name, civName, StringComparison.OrdinalIgnoreCase) == 0).ToList();
                if (entities.Count == 1)
                {
                    parent = entities.First();
                }
                else if (entities.Count == 0)
                {
                    _world.ParsingErrors.Report($"Couldn\'t Find Entity:\n{civName}, Parent Civ of {_owner.Name}");
                }
#if DEBUG
                else
                {
                    _world.ParsingErrors.Report($"Ambiguous Parent Entity Name:\n{civName}, Parent Civ of {_owner.Name}");
                }
#endif
                if (parent != null)
                {
                    parent.Race = Formatting.InitCaps(_currentLine.Substring(_currentLine.IndexOf(",", StringComparison.Ordinal) + 2,
                        _currentLine.Length - _currentLine.IndexOf(",", StringComparison.Ordinal) - 2));
                    if (string.IsNullOrWhiteSpace(parent.Race))
                    {
                        parent.Race = "Unknown";
                    }
                    if (_owner != null)
                    {
                        var current = _owner;
                        while (!current.IsCiv && current.Parent != null)
                        {
                            current = current.Parent;
                        }
                        if (!current.IsCiv && current.Parent == null && current != parent)
                        {
                            current.Parent = parent;
                        }
                        if (!parent.Groups.Contains(_owner))
                        {
                            parent.Groups.Add(_owner);
                        }
                    }
                }

                ReadLine();
            }
        }

        private void ReadOfficials()
        {
            if (_currentLine.Contains(":")) //Read in Officials, law-givers, mayors etc, ex: law-giver: Oled Sugarynestled, human
            {
                while (!_sitesAndPops.EndOfStream && _currentLine.Contains(":") && !SiteStart())
                {
                    string officialName = Formatting.ReplaceNonAscii(_currentLine.Substring(_currentLine.IndexOf(":", StringComparison.Ordinal) + 2,
                        _currentLine.IndexOf(",", StringComparison.Ordinal) - _currentLine.IndexOf(":", StringComparison.Ordinal) - 2));
                    var officials = _world.HistoricalFigures.Where(hf =>
                            string.Compare(hf.Name, officialName.Replace("'", "`"), StringComparison.OrdinalIgnoreCase) == 0).ToList();
                    if (officials.Count == 1)
                    {
                        var siteOfficial = officials.First();
                        string siteOfficialPosition = _currentLine.Substring(1, _currentLine.IndexOf(":", StringComparison.Ordinal) - 1);
                        _site.Officials.Add(new Site.Official(siteOfficial, siteOfficialPosition));
                    }
                    else if (officials.Count == 0)
                    {
                        _world.ParsingErrors.Report($"Couldn\'t Find Official:\n{officialName}, Official of {_site.Name}");
                    }
#if DEBUG
                    else
                    {
                        _world.ParsingErrors.Report($"Ambiguous ({officials.Count}) Official Name:\n{officialName}, Official of {_site.Name}");
                    }
#endif
                    ReadLine();
                }
            }
        }

        public void ReadPopulations()
        {
            List<Population> populations = new List<Population>();
            while (!SiteStart() && _currentLine != "")
            {
                string race = Formatting.InitCaps(_currentLine.Substring(_currentLine.IndexOf(' ') + 1));
                race = Formatting.MakePopulationPlural(race);
                int count = Convert.ToInt32(_currentLine.Substring(1, _currentLine.IndexOf(' ') - 1));
                populations.Add(new Population(race, count));
                ReadLine();
            }

            _site.Populations = populations.OrderByDescending(pop => pop.Count).ToList();
            _owner?.AddPopulations(populations);
            _world.SitePopulations.AddRange(populations);
        }

        public void CheckSiteOwnership()
        {
            //Check if a site was gained without a proper event from the xml
            if (_owner != null)
            {
                if (_site.OwnerHistory.Count == 0)
                {
                    _site.OwnerHistory.Add(new OwnerPeriod(_site, _owner, -1, "founded"));
                }
                else if (_site.OwnerHistory.Last().Owner != _owner)
                {
                    bool found = false;
                    if (_site.OwnerHistory.Last().Owner is HistoricalFigure lastKnownOwner)
                    {
                        if (lastKnownOwner.DeathYear != -1)
                        {
                            _site.OwnerHistory.Add(new OwnerPeriod(_site, _owner, lastKnownOwner.DeathYear, "after death of last owner (" + lastKnownOwner.DeathCause + ") took over"));
                            found = true;
                        }
                        else
                        {
                            if (_site.Type == "Vault")
                            {
                                if (_owner is Entity entity)
                                {
                                    _site.OwnerHistory.Add(new OwnerPeriod(_site, entity, -1, "moved into"));
                                    found = true;
                                }
                            }
                        }
                    }
                    else if (_site.CurrentOwner == null)
                    {
                        _site.OwnerHistory.Add(new OwnerPeriod(_site, _owner, _site.OwnerHistory.Last().EndYear, "slowly repopulated after the site was " + _site.OwnerHistory.Last().EndCause));
                        found = true;
                    }
                    if (!found)
                    {
                        ChangeHfState lastSettledEvent = _site.Events.OfType<ChangeHfState>().LastOrDefault();
                        if (lastSettledEvent != null)
                        {
                            _site.OwnerHistory.Add(new OwnerPeriod(_site, _owner, lastSettledEvent.Year, "settled in"));
                        }
                        else
                        {
                            _world.ParsingErrors.Report("Site ownership conflict: " + _site.Name + ". Actually owned by " + _owner.ToLink(false) + " instead of " + _site.OwnerHistory.Last().Owner.ToLink(false));
                        }
                    }
                }
            }
            //check for loss of period ownership, since some some loss of ownership eventsList are missing
            if (_owner == null && _site.OwnerHistory.Count > 0 && _site.OwnerHistory.Last().EndYear == -1)
            {
                _site.OwnerHistory.Last().EndYear = _world.Events.Last().Year - 1;
                _site.OwnerHistory.Last().EndCause = "abandoned";
            }
        }

        private void ReadOutdoorPopulations()
        {
            _worker.ReportProgress(0, "... Outdoor Populations");
            ReadLine();
            ReadLine();
            while (!string.IsNullOrEmpty(_currentLine) && !_sitesAndPops.EndOfStream)
            {
                if (string.IsNullOrEmpty(_currentLine))
                {
                    _currentLine = _sitesAndPops.ReadLine();
                    continue;
                }

                var population = Formatting.InitCaps(_currentLine.Substring(_currentLine.IndexOf(" ", StringComparison.Ordinal) + 1));
                var countString = _currentLine.Substring(1, _currentLine.IndexOf(" ", StringComparison.Ordinal) - 1);
                var count = countString == "Unnumbered" ? Int32.MaxValue : Convert.ToInt32(countString);

                _world.OutdoorPopulations.Add(new Population(population, count));
                _currentLine = _sitesAndPops.ReadLine();
            }
            _world.OutdoorPopulations = _world.OutdoorPopulations.OrderByDescending(population => population.Count).ToList();
        }

        private void ReadUndergroundPopulations()
        {
            _worker.ReportProgress(0, "... Underground Populations");
            ReadLine();
            ReadLine();
            while (!string.IsNullOrEmpty(_currentLine) && !_sitesAndPops.EndOfStream)
            {
                if (string.IsNullOrEmpty(_currentLine))
                {
                    _currentLine = _sitesAndPops.ReadLine(); continue;
                }

                var population = Formatting.InitCaps(_currentLine.Substring(_currentLine.IndexOf(" ", StringComparison.Ordinal) + 1));
                var countString = _currentLine.Substring(1, _currentLine.IndexOf(" ", StringComparison.Ordinal) - 1);
                var count = countString == "Unnumbered" ? Int32.MaxValue : Convert.ToInt32(countString);

                _world.UndergroundPopulations.Add(new Population(population, count));
                _currentLine = _sitesAndPops.ReadLine();
            }
            _world.UndergroundPopulations = _world.UndergroundPopulations.OrderByDescending(population => population.Count).ToList();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sitesAndPops.Dispose();
            }
        }

    }
}
