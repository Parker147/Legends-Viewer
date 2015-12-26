using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls
{
    class SitePrinter : HTMLPrinter
    {
        Site Site;
        World World;

        public SitePrinter(Site site, World world)
        {
            Site = site;
            World = world;
        }

        public override string GetTitle()
        {
            return Site.Name;
        }

        public override string Print()
        {
            HTML = new StringBuilder();

            HTML.AppendLine("<h1>" + Site.UntranslatedName + ", \"" + Site.Name + "\"</h1>");
            HTML.AppendLine("<b>" + Site.ToLink(false) + " is a " + Site.Type + "</b><br /><br />");

            List<System.Drawing.Bitmap> maps = MapPanel.CreateBitmaps(World, Site);
            //HTML.AppendLine("<table border=\"0\" width=\"" + (maps[0].Width + maps[1].Width + 10) + "\">");
            HTML.AppendLine("<table>");
            HTML.AppendLine("<tr>");
            HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[0]), LinkOption.LoadMap) + "</td>");
            HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[1]), LinkOption.LoadMap) + "</td>");
            HTML.AppendLine("</tr></table></br>");

            if (Site.Warfare.Count(battle => !World.FilterBattles || battle.Notable) > 0)
            {
                int warfareCount = 1;
                HTML.AppendLine("<b>Warfare</b> " + MakeLink("[Load]", LinkOption.LoadSiteBattles));
                if (World.FilterBattles) HTML.Append(" (Notable)");
                HTML.Append("<table border=\"0\">");
                foreach (EventCollection warfare in Site.Warfare.Where(battle => !World.FilterBattles || battle.Notable))
                {
                    HTML.AppendLine("<tr>");
                    HTML.AppendLine("<td width=\"20\"  align=\"right\">" + warfareCount + ".</td><td width=\"10\"></td>");
                    HTML.AppendLine("<td>" + warfare.StartYear + "</td>");
                    string warfareString = warfare.ToLink();
                    if (warfareString.Contains(" as a result of"))
                        warfareString = warfareString.Insert(warfareString.IndexOf(" as a result of"), "</br>");
                    HTML.AppendLine("<td>" + warfareString + "</td>");
                    HTML.AppendLine("<td>as part of</td>");
                    HTML.AppendLine("<td>" + ((warfare.ParentCollection == null) ? "UNKNOWN" :warfare.ParentCollection.ToLink()) + "</td>");
                    HTML.AppendLine("<td>by ");
                    if (warfare.GetType() == typeof(Battle))
                    {
                        Battle battle = warfare as Battle;
                        HTML.Append(battle.Attacker.PrintEntity() + "</td>");
                        if (battle.Victor == battle.Attacker) HTML.AppendLine("<td>(V)</td>");
                        else HTML.AppendLine("<td></td>");
                        HTML.AppendLine("<td>(Deaths: " + (battle.AttackerDeathCount + battle.DefenderDeathCount) + ")</td>");
                    }
                    if (warfare.GetType() == typeof(SiteConquered)) HTML.Append((warfare as SiteConquered).Attacker.PrintEntity() + "</td>");
                    HTML.AppendLine("</tr>");
                    warfareCount++;
                }
                HTML.AppendLine("</table></br>");
            }

            if (World.FilterBattles && Site.Warfare.Count(battle => !battle.Notable) > 0)
            {
                HTML.AppendLine("<b>Warfare</b> (Unnotable)</br>");
                HTML.AppendLine("<ul>");
                HTML.AppendLine("<li>Battles: " + Site.Warfare.OfType<Battle>().Where(battle => !battle.Notable).Count());
                HTML.AppendLine("<li>Pillagings: " + Site.Warfare.OfType<SiteConquered>().Where(conquering => conquering.ConquerType == SiteConqueredType.Pillaging).Count());
                HTML.AppendLine("</ul>");
            }

            if (Site.OwnerHistory.Count > 0)
            {
                HTML.AppendLine("<b>Owner History</b><br />");
                HTML.AppendLine("<ol>");
                foreach (OwnerPeriod owner in Site.OwnerHistory)
                {
                    string ownerString = "UNKNOWN ENTITY";
                    if (owner.Owner != null)
                    {
                        if (owner.Owner is Entity)
                        {
                            ownerString = ((Entity)owner.Owner).PrintEntity();
                        }
                        else
                        {
                            ownerString = owner.Owner.ToLink(true, Site);
                        }
                    }
                    HTML.AppendLine("<li>" + ownerString + ", " + owner.StartCause + " " + Site.ToLink(true, Site) + " in " + owner.StartYear);
                    if (owner.EndYear >= 0)
                        HTML.Append(" and <font color=\"Red\">" + owner.EndCause + "</font> in " + owner.EndYear);
                    if (owner.Ender != null)
                    {
                        if (owner.Ender is Entity)
                        {
                            HTML.Append(" by " + ((Entity)owner.Ender).PrintEntity());
                        }
                        else
                        {
                            HTML.Append(" by " + owner.Ender.ToLink(true, Site));
                        }
                    }
                }
                HTML.AppendLine("</ol>");
            }

            if (Site.Officials.Count > 0)
            {
                HTML.AppendLine("<b>Officials</b></br>");
                HTML.AppendLine("<ol>");
                foreach (Site.Official official in Site.Officials)
                    HTML.AppendLine("<li>" + official.HistoricalFigure.ToLink() + ", " + official.Position);
                HTML.AppendLine("</ol>");
            }

            if (Site.Connections.Count > 0)
            {
                HTML.AppendLine("<b>Connections</b></br>");
                HTML.AppendLine("<ol>");
                foreach (Site connection in Site.Connections)
                    HTML.AppendLine("<li>" + connection.ToLink());
                HTML.AppendLine("</ol>");
            }

            if (Site.Populations.Count > 0)
            {
                var mainRacePops = Site.Populations.Where(pop => pop.IsMainRace);
                var outcastsPops = Site.Populations.Where(pop => pop.IsOutcasts);
                var prisonersPops = Site.Populations.Where(pop => pop.IsPrisoners);
                var slavesPops = Site.Populations.Where(pop => pop.IsSlaves);
                var animalPeoplePops = Site.Populations.Where(pop => pop.IsAnimalPeople);
                var otherRacePops = Site.Populations.Where(pop => !pop.IsMainRace && !pop.IsOutcasts && !pop.IsPrisoners && !pop.IsSlaves && !pop.IsAnimalPeople);
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

            if (Site.BeastAttacks != null && Site.BeastAttacks.Count > 0)
            {
                HTML.AppendLine("<b>Beast Attacks</b>");
                HTML.AppendLine("<ol>");
                foreach (BeastAttack attack in Site.BeastAttacks)
                {
                    HTML.AppendLine("<li>" + attack.StartYear + ", " + attack.ToLink(true, Site));
                    if (attack.GetSubEvents().OfType<HFDied>().Any()) HTML.Append(" (Deaths: " + attack.GetSubEvents().OfType<HFDied>().Count() + ")");
                }
                HTML.AppendLine("</ol>");
            }

            if (Site.Events.OfType<HFDied>().Any() || Site.Warfare.OfType<Battle>().Any())
            {
                HTML.AppendLine("<b>Deaths</b> " + MakeLink("[Load]", LinkOption.LoadSiteDeaths) + LineBreak);
                HTML.AppendLine("<ol>");
                foreach (HFDied death in Site.Events.OfType<HFDied>())
                    HTML.AppendLine("<li>" + death.HistoricalFigure.ToLink() + ", in " + death.Year + " (" + death.Cause + ")");
                HTML.AppendLine("<li>Population in Battle: " + Site.Warfare.OfType<Battle>().Sum(battle => battle.AttackerSquads.Sum(squad => squad.Deaths) + battle.DefenderSquads.Sum(squad => squad.Deaths)));
                HTML.AppendLine("</ol>");
            }


            HTML.AppendLine("<b>Event Log</b> " + MakeLink(Font("[Chart]", "Maroon"), LinkOption.LoadChart) + LineBreak);
            foreach (var e in Site.Events)
                if (!Site.Filters.Contains(e.Type))
                    HTML.AppendLine(e.Print(true, Site) + "<br /><br />");

            return HTML.ToString();
        }
    }
}
