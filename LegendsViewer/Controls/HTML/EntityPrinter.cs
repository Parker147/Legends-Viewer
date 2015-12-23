using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Legends;
using System.Drawing;

namespace LegendsViewer.Controls
{
    class EntityPrinter : HTMLPrinter
    {
        Entity Entity;
        World World;

        public EntityPrinter(Entity entity, World world)
        {
            Entity = entity;
            World = world;
        }

        public override string GetTitle()
        {
            return Entity.Name;
        }

        public override string Print()
        {
            HTML = new StringBuilder();
            PrintTitle();
            PrintLeaders();
            PrintWorships();
            PrintWars();
            PrintSiteHistory();
            PrintPopulations();
            PrintEvents();
            return HTML.ToString();
        }

        private void PrintTitle()
        {
            string title = Entity.ToLink(false);
            if (Entity.IsCiv) title += " is a civilization of ";
            else title += " is a group of ";
            title += Entity.Race.ToLower();
            if (Entity.Parent != null) title += " of " + Entity.Parent.ToLink(true, Entity);
            HTML.AppendLine("<h1>"+title+"</h1></br>");

            if (Entity.IsCiv)
                HTML.AppendLine(Entity.PrintIdenticon(true) + LineBreak + LineBreak);

            if (Entity.SiteHistory.Count > 0)
            {
                if (Entity.SiteHistory.Count(sitePeriod => sitePeriod.EndYear == -1) == 0)
                    HTML.AppendLine(Font("Last Known Sites. Year: " + (Entity.SiteHistory.Max(sitePeriod => sitePeriod.EndYear) - 1), "red"));
                List<Bitmap> maps = MapPanel.CreateBitmaps(World, Entity);
                TableMaker mapTable = new TableMaker();
                mapTable.StartRow();
                mapTable.AddData(MakeLink(BitmapToHTML(maps[0]), LinkOption.LoadMap));
                mapTable.AddData(MakeLink(BitmapToHTML(maps[1]), LinkOption.LoadMap));
                mapTable.EndRow();
                HTML.AppendLine(mapTable.GetTable()+"</br>");
                maps[0].Dispose();
                maps[1].Dispose();
            }
        }

        private void PrintLeaders()
        {
            if (Entity.Leaders != null && Entity.Leaders.Count > 0)
            {
                HTML.AppendLine(Bold("Leaders") + " " + MakeLink("[Load]", LinkOption.LoadEntityLeaders) + LineBreak);
                foreach (string leaderType in Entity.LeaderTypes)
                {
                    HTML.AppendLine(leaderType + "s" + LineBreak);
                    TableMaker leaderTable = new TableMaker(true);
                    foreach (HistoricalFigure leader in Entity.Leaders[Entity.LeaderTypes.IndexOf(leaderType)])
                    {
                        leaderTable.StartRow();
                        leaderTable.AddData(leader.Positions.Last(position => position.Title == leaderType).Began.ToString(), 0, TableDataAlign.Right);
                        leaderTable.AddData(leader.ToLink());
                        leaderTable.EndRow();
                    }
                    HTML.AppendLine(leaderTable.GetTable() + LineBreak);
                }
            }
        }

        private void PrintWorships()
        {
            if (Entity.Worshipped != null && Entity.Worshipped.Count > 0)
            {
                HTML.AppendLine(Bold("Worships") + LineBreak);
                StartList(ListType.Unordered);
                foreach (HistoricalFigure worship in Entity.Worshipped)
                {
                    string associations = "";
                    foreach (string association in worship.Spheres)
                    {
                        if (associations.Length > 0) associations += ", ";
                        associations += association;
                    }
                    HTML.AppendLine(ListItem + worship.ToLink() + " (" + associations + ")");
                }
                EndList(ListType.Unordered);
            }
        }

        private void PrintWars()
        {
            if (Entity.Wars.Count(war => !World.FilterBattles || war.Notable) > 0)
            {
                HTML.AppendLine(Bold("Wars") + " " + MakeLink("[Load]", LinkOption.LoadEntityWars) + LineBreak);
                TableMaker warTable = new TableMaker(true);
                foreach (War war in Entity.Wars.Where(war => !World.FilterBattles || war.Notable))
                {
                    warTable.StartRow();
                    string endString;
                    if (war.EndYear == -1) endString = "Present";
                    else endString = war.EndYear.ToString();

                    warTable.AddData(war.StartYear + " - " + endString);
                    warTable.AddData(war.ToLink());

                    if (war.Attacker == Entity)
                    {
                        warTable.AddData("waged against");
                        warTable.AddData(war.Defender.PrintEntity(), 0);
                        warTable.AddData("");
                    }
                    else if (war.Attacker.Parent == Entity)
                    {
                        warTable.AddData("waged against");
                        warTable.AddData(war.Defender.PrintEntity(), 0);
                        warTable.AddData("by " + war.Attacker.ToLink());
                    }
                    else if (war.Defender == Entity)
                    {
                        warTable.AddData("defended against");
                        warTable.AddData(war.Attacker.PrintEntity(), 0);
                        warTable.AddData("");
                    }
                    else if (war.Defender.Parent == Entity)
                    {
                        warTable.AddData("defended against");
                        warTable.AddData(war.Attacker.PrintEntity(), 0);
                        warTable.AddData("by " + war.Defender.ToLink());
                    }

                    int battleVictories = 0, battleLossses = 0, sitesDestroyed = 0, sitesLost = 0, kills, losses;
                    if (war.Attacker == Entity || war.Attacker.Parent == Entity)
                    {
                        battleVictories = war.AttackerVictories.OfType<Battle>().Count();
                        battleLossses = war.DefenderVictories.OfType<Battle>().Count();
                        sitesDestroyed = war.AttackerVictories.OfType<SiteConquered>().Count(conquering => conquering.ConquerType != SiteConqueredType.Pillaging);
                        sitesLost = war.DefenderVictories.OfType<SiteConquered>().Count(conquering => conquering.ConquerType != SiteConqueredType.Pillaging);
                    }
                    else if (war.Defender == Entity || war.Defender.Parent == Entity)
                    {
                        battleVictories = war.DefenderVictories.OfType<Battle>().Count();
                        battleLossses = war.AttackerVictories.OfType<Battle>().Count();
                        sitesDestroyed = war.DefenderVictories.OfType<SiteConquered>().Count(conquering => conquering.ConquerType != SiteConqueredType.Pillaging);
                        sitesLost = war.AttackerVictories.OfType<SiteConquered>().Count(conquering => conquering.ConquerType != SiteConqueredType.Pillaging);
                    }

                    kills = war.Collections.OfType<Battle>().Where(battle => battle.Attacker == Entity || battle.Attacker.Parent == Entity).Sum(battle => battle.DefenderDeathCount) + war.Collections.OfType<Battle>().Where(battle => battle.Defender == Entity || battle.Defender.Parent == Entity).Sum(battle => battle.AttackerDeathCount);
                    losses = war.Collections.OfType<Battle>().Where(battle => battle.Attacker == Entity || battle.Attacker.Parent == Entity).Sum(battle => battle.AttackerDeathCount) + war.Collections.OfType<Battle>().Where(battle => battle.Defender == Entity || battle.Defender.Parent == Entity).Sum(battle => battle.DefenderDeathCount);

                    warTable.AddData("(V/L)");
                    warTable.AddData("Battles:");
                    warTable.AddData(battleVictories.ToString(), 0, TableDataAlign.Right);
                    warTable.AddData("/");
                    warTable.AddData(battleLossses.ToString());
                    warTable.AddData("Sites:");
                    warTable.AddData(sitesDestroyed.ToString(), 0, TableDataAlign.Right);
                    warTable.AddData("/");
                    warTable.AddData(sitesLost.ToString());
                    warTable.AddData("Deaths:");
                    warTable.AddData(kills.ToString(), 0, TableDataAlign.Right);
                    warTable.AddData("/");
                    warTable.AddData(losses.ToString());
                    warTable.EndRow();
                }
                HTML.AppendLine(warTable.GetTable() + LineBreak);

            }
        }

        private void PrintSiteHistory()
        {
            if (Entity.SiteHistory.Count > 0)
            {
                HTML.AppendLine(Bold("Site History") + " " + MakeLink("[Load]", LinkOption.LoadEntitySites) + LineBreak);
                TableMaker siteTable = new TableMaker(true);
                foreach (OwnerPeriod ownedSite in Entity.SiteHistory.OrderBy(sh => sh.StartYear))
                {
                    siteTable.StartRow();
                    siteTable.AddData(ownedSite.Owner.ToLink(true, Entity));
                    siteTable.AddData(ownedSite.StartCause);
                    siteTable.AddData(ownedSite.Site.ToLink());
                    siteTable.AddData(ownedSite.StartYear.ToString(), 0, TableDataAlign.Right);
                    if (ownedSite.EndYear >= 0)
                    {
                        siteTable.AddData(ownedSite.EndCause);
                        siteTable.AddData(ownedSite.EndYear.ToString(), 0 , TableDataAlign.Right);
                    }
                    if (ownedSite.Ender != null)
                    {
                        if (ownedSite.Ender is Entity)
                        {
                            siteTable.AddData(" by " + ((Entity)ownedSite.Ender).PrintEntity());
                        }
                        else
                        {
                            siteTable.AddData(" by " + ownedSite.Ender.ToLink(true, Entity));
                        }
                    }
                    siteTable.EndRow();
                }
                HTML.AppendLine(siteTable.GetTable() + LineBreak);
            }
        }

        private void PrintPopulations()
        {
            if (Entity.Populations.Count > 0)
            {
                var mainRacePops = Entity.Populations.Where(pop => pop.IsMainRace);
                var outcastsPops = Entity.Populations.Where(pop => pop.IsOutcasts);
                var prisonersPops = Entity.Populations.Where(pop => pop.IsPrisoners);
                var slavesPops = Entity.Populations.Where(pop => pop.IsSlaves);
                var animalPeoplePops = Entity.Populations.Where(pop => pop.IsAnimalPeople);
                var otherRacePops = Entity.Populations.Where(pop => !pop.IsMainRace && !pop.IsOutcasts && !pop.IsPrisoners && !pop.IsSlaves && !pop.IsAnimalPeople);
                if (mainRacePops.Any())
                {
                    HTML.AppendLine("<b>Civilized Populations</b></br>");
                    HTML.AppendLine("<ul>");
                    foreach (Population population in mainRacePops)
                        HTML.AppendLine("<li>" + population.Count + " " + population.Race);
                    HTML.AppendLine("</ul>");
                }
                if (animalPeoplePops.Any())
                {
                    HTML.AppendLine("<b>Animal People Populations</b></br>");
                    HTML.AppendLine("<ul>");
                    foreach (Population population in animalPeoplePops)
                        HTML.AppendLine("<li>" + population.Count + " " + population.Race);
                    HTML.AppendLine("</ul>");
                }
                if (outcastsPops.Any())
                {
                    HTML.AppendLine("<b>Outcasts</b></br>");
                    HTML.AppendLine("<ul>");
                    foreach (Population population in outcastsPops)
                        HTML.AppendLine("<li>" + population.Count + " " + population.Race);
                    HTML.AppendLine("</ul>");
                }
                if (prisonersPops.Any())
                {
                    HTML.AppendLine("<b>Prisoners</b></br>");
                    HTML.AppendLine("<ul>");
                    foreach (Population population in prisonersPops)
                        HTML.AppendLine("<li>" + population.Count + " " + population.Race);
                    HTML.AppendLine("</ul>");
                }
                if (slavesPops.Any())
                {
                    HTML.AppendLine("<b>Slaves</b></br>");
                    HTML.AppendLine("<ul>");
                    foreach (Population population in slavesPops)
                        HTML.AppendLine("<li>" + population.Count + " " + population.Race);
                    HTML.AppendLine("</ul>");
                }
                if (otherRacePops.Any())
                {
                    HTML.AppendLine("<b>Other Populations</b></br>");
                    HTML.AppendLine("<ul>");
                    foreach (Population population in otherRacePops)
                        HTML.AppendLine("<li>" + population.Count + " " + population.Race);
                    HTML.AppendLine("</ul>");
                }
            }
        }

        private void PrintEvents()
        {
            HTML.AppendLine(Bold("Event Log") + " " + MakeLink(Font("[Chart]", "Maroon"), LinkOption.LoadChart) + LineBreak);
            foreach (var e in Entity.Events)
            {
                if (!Entity.Filters.Contains(e.Type))
                    HTML.AppendLine(e.Print(true, Entity) + LineBreak + LineBreak);
            }
        }

        
    }
}
