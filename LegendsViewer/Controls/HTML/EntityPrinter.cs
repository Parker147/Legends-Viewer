using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Legends;
using System.Drawing;
using LegendsViewer.Controls.Map;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.EventCollections;

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
            PrintCurrentLeadership();
            PrintWorships();
            PrintWars();
            PrintSiteHistory();
            PrintPopulations(Entity.Populations);
            PrintEventLog(Entity.Events, Entity.Filters, Entity);
            return HTML.ToString();
        }

        private void PrintTitle()
        {
            string title = Entity.ToLink(false);
            if (Entity.IsCiv)
            {
                title += " is a civilization";
            }
            else
            {
                title += " is a ";
                switch (Entity.Type)
                {
                    case EntityType.Civilization:
                        title += "civilization";
                        break;
                    case EntityType.NomadicGroup:
                        title += "nomadic group";
                        break;
                    case EntityType.MigratingGroup:
                        title += "migrating group";
                        break;
                    case EntityType.Outcast:
                        title += "collection of outcasts";
                        break;
                    case EntityType.Religion:
                        title += "religious group";
                        break;
                    case EntityType.SiteGovernment:
                        title += "site government";
                        break;
                    case EntityType.PerformanceTroupe:
                        title += "performance troupe";
                        break;
                    default:
                        title += "group";
                        break;
                }
            }
            if (!string.IsNullOrWhiteSpace(Entity.Race) && Entity.Race != "Unknown")
            {
                title += " of ";
                title += Entity.Race.ToLower();
            }
            if (Entity.Parent != null) title += " of " + Entity.Parent.ToLink(true, Entity);
            title += ".";
            HTML.AppendLine("<h1>" + title + "</h1></br>");

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
                HTML.AppendLine(mapTable.GetTable() + "</br>");
                maps[0].Dispose();
                maps[1].Dispose();
            }
        }

        private void PrintLeaders()
        {
            if (Entity.Leaders != null && Entity.Leaders.Count > 0)
            {
                HTML.AppendLine(Bold("Leaderhistory") + " " + MakeLink("[Load]", LinkOption.LoadEntityLeaders) + LineBreak);
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

        private void PrintCurrentLeadership()
        {
            if (Entity.EntityPositionAssignments.Any() && Entity.EntityPositionAssignments.Where(epa => epa.HistoricalFigure != null).Any())
            {
                HTML.AppendLine("<b>Current Leadership</b><br />");
                HTML.AppendLine("<ul>");
                foreach (EntityPositionAssignment assignment in Entity.EntityPositionAssignments)
                {
                    EntityPosition position = Entity.EntityPositions.FirstOrDefault(pos => pos.ID == assignment.PositionID);
                    if (position != null && assignment.HistoricalFigure != null)
                    {
                        string positionName;
                        if (assignment.HistoricalFigure.Caste == "Female" && !string.IsNullOrEmpty(position.NameFemale))
                        {
                            positionName = position.NameFemale;
                        }
                        else if (assignment.HistoricalFigure.Caste == "Male" && !string.IsNullOrEmpty(position.NameMale))
                        {
                            positionName = position.NameMale;
                        }
                        else
                        {
                            positionName = position.Name;
                        }

                        HTML.AppendLine("<li>" + assignment.HistoricalFigure.ToLink() + ", " + positionName + "</li>");

                        if (!string.IsNullOrEmpty(position.Spouse))
                        {
                            HistoricalFigureLink spouseLink = assignment.HistoricalFigure.RelatedHistoricalFigures.FirstOrDefault(hfLink => hfLink.Type == HistoricalFigureLinkType.Spouse);
                            if (spouseLink != null)
                            {
                                HistoricalFigure spouse = spouseLink.HistoricalFigure;
                                if (spouse != null)
                                {
                                    string spousePositionName;
                                    if (spouse.Caste == "Female" && !string.IsNullOrEmpty(position.SpouseFemale))
                                    {
                                        spousePositionName = position.SpouseFemale;
                                    }
                                    else if (spouse.Caste == "Male" && !string.IsNullOrEmpty(position.SpouseMale))
                                    {
                                        spousePositionName = position.SpouseMale;
                                    }
                                    else
                                    {
                                        spousePositionName = position.Spouse;
                                    }

                                    HTML.AppendLine("<li>" + spouse.ToLink() + ", " + spousePositionName + "</li>");
                                }
                            }
                        }
                    }
                }
                HTML.AppendLine("</ul>");
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
                        siteTable.AddData(ownedSite.EndYear.ToString(), 0, TableDataAlign.Right);
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
    }
}
