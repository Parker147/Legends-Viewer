using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Legends;
using System.Drawing;
using LegendsViewer.Controls.Map;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.EventCollections;
using System.Net;
using System;

namespace LegendsViewer.Controls
{
    class EntityPrinter : HTMLPrinter
    {
        private readonly Entity Entity;
        private readonly World World;
        private List<Entity> _allEnemies;

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

            HTML.AppendLine("<script type=\"text/javascript\" src=\"" + LocalFileProvider.LocalPrefix + "WebContent/scripts/Chart.bundle.min.js\"></script>");

            LoadCustomScripts();

            PrintTitle();
            PrintEntityLinks();
            PrintOriginStructure();
            PrintWorships();
            PrintLeaders();
            PrintCurrentLeadership();
            PrintWars();
            PrintWarfareInfo();
            PrintSiteHistory();
            PrintPopulations(Entity.Populations);
            PrintEventLog(Entity.Events, Entity.Filters, Entity);
            return HTML.ToString();
        }

        private void PrintOriginStructure()
        {
            if (Entity.OriginStructure == null)
            {
                return;
            }
            HTML.AppendLine(Bold("Originated in") + LineBreak);
            StartList(ListType.Unordered);
            HTML.AppendLine(ListItem + Entity.OriginStructure.ToLink(true, Entity) + " (" + Entity.OriginStructure.Site.ToLink(true, Entity) + ")");
            EndList(ListType.Unordered);
        }

        private void PrintChildEntites(Entity entity)
        {
            if (entity.EntityLinks.Count(entityLink => entityLink.Type.Equals(EntityEntityLinkType.Child)) == 0)
            {
                return;
            }
            StartList(ListType.Unordered);
            foreach (EntityEntityLink childLink in entity.EntityLinks.Where(entityLink => entityLink.Type.Equals(EntityEntityLinkType.Child)))
            {
                HTML.AppendLine(ListItem + childLink.Target.ToLink(true, entity) + " (" + childLink.Target.Type.GetDescription() + ")");
                PrintChildEntites(childLink.Target);
            }
            EndList(ListType.Unordered);
        }

        private void PrintEntityLinks()
        {
            if (Entity.EntityLinks.Count == 0)
            {
                return;
            }
            HTML.AppendLine(Bold("Related Entities") + LineBreak);
            StartList(ListType.Unordered);
            foreach (EntityEntityLink parentLink in Entity.EntityLinks.Where(entityLink => entityLink.Type.Equals(EntityEntityLinkType.Parent)))
            {
                HTML.AppendLine(ListItem + parentLink.Target.ToLink(true, Entity) + " (" + parentLink.Target.Type.GetDescription() + ")");
            }
            foreach (EntityEntityLink childLink in Entity.EntityLinks.Where(entityLink => entityLink.Type.Equals(EntityEntityLinkType.Child)))
            {
                HTML.AppendLine(ListItem + childLink.Target.ToLink(true, Entity) + " (" + childLink.Target.Type.GetDescription() + ")");
                PrintChildEntites(childLink.Target);
            }
            EndList(ListType.Unordered);
        }

        private void LoadCustomScripts()
        {
            HTML.AppendLine("<script>");
            HTML.AppendLine("window.onload = function(){");

            if (Entity.Wars.Any())
            {
                PopulateWarOverview();
            }

            HTML.AppendLine("}");
            HTML.AppendLine("</script>");
        }

        private void PopulateWarOverview()
        {
            var allBattles = new List<Battle>();
            foreach (var war in Entity.Wars)
            {
                allBattles.AddRange(war.Battles);
            }

            _allEnemies = allBattles
                .Select(x => x.Attacker).Concat(allBattles.Select(x => x.Defender))
                .Distinct()
                .OrderBy(entity => entity.Race)
                .ToList();
            var entityLabels = string.Join(",", _allEnemies.Where(x => x.Name != Entity.Name).Select(x => $"'{x.Name} - {x.Race}'"));
            var battleVictorData = string.Join(",", _allEnemies.Where(x => x.Name != Entity.Name).Select(x => $"{allBattles.Count(y => y.Victor == Entity && (y.Attacker.Name == x.Name || y.Defender.Name == x.Name))}"));
            var battleLoserData = string.Join(",", _allEnemies.Where(x => x.Name != Entity.Name).Select(x => $"{allBattles.Count(y => y.Victor != Entity && (y.Attacker.Name == x.Name || y.Defender.Name == x.Name))}"));

            var victorColor = "255, 206, 86";
            var loserColor = "153, 102, 255";

            var battleVictorEntity =
                "{ " +
                    "label: \"As Victor\", " +
                    "data: [" + battleVictorData + "], " +
                    "backgroundColor: 'rgba(" + victorColor + ", 0.25)', " +
                    "hoverBackgroundColor: 'rgba(" + victorColor + ", 0.5)', " +
                    "borderWidth: 2, " +
                    "borderColor: 'rgba(" + victorColor + ", 0.8)', " +
                    "hoverBorderColor: 'rgba(" + victorColor + ", 1)' " +
                "}";
            var battleLoserEntity =
                "{ " +
                    "label: \"As Loser\", " +
                    "data: [" + battleLoserData + "], " +
                    "backgroundColor: 'rgba(" + loserColor + ", 0.25)', " +
                    "hoverBackgroundColor: 'rgba(" + loserColor + ", 0.5)', " +
                    "borderWidth: 2, " +
                    "borderColor: 'rgba(" + loserColor + ", 0.8)', " +
                    "hoverBorderColor: 'rgba(" + loserColor + ", 1)' " +
                "}";


            HTML.AppendLine("var warsByEntityChart = new Chart(document.getElementById('chart-countbyEntity').getContext('2d'), { type: 'horizontalBar', ");
            HTML.AppendLine("data: {");
            HTML.AppendLine("labels: [" + entityLabels + "], ");
            HTML.AppendLine("datasets:[" + battleVictorEntity + "," + battleLoserEntity + "],");
            HTML.AppendLine("},");
            HTML.AppendLine("options:{");
            HTML.AppendLine("maxBarThickness: 20,");
            HTML.AppendLine("legend:{");
            HTML.AppendLine("position:'top',");
            HTML.AppendLine("labels: { boxWidth: 12 }");
            HTML.AppendLine("}");
            HTML.AppendLine("}");
            HTML.AppendLine("});");
        }

        private void PrintWarfareInfo()
        {
            if (!Entity.Wars.Any())
            {
                return;
            }
            HTML.AppendLine("<div class=\"container-fluid\">");

            HTML.AppendLine("<div class=\"row\">");

            PrintWarfareGraph();
            PrintWarfareChart();

            HTML.AppendLine("</div>");

            HTML.AppendLine("</div>");
            HTML.AppendLine("</br>");
        }

        private void PrintWarfareChart()
        {
            if (_allEnemies.Count > 5)
            {
                HTML.AppendLine("<div class=\"col-md-12\">");
            }
            else
            {
                HTML.AppendLine("<div class=\"col-md-6 col-sm-12\">");
            }
            HTML.AppendLine(Bold("Battles against other Entities - Victory/Defeat Chart") + LineBreak);
            HTML.AppendLine("<canvas id=\"chart-countbyEntity\" class=\"bar-chart\" width=\"600\" height=\"300\"></canvas>");
            HTML.AppendLine("</div>");
        }

        private void PrintWarfareGraph()
        {
            if (!Entity.Wars.Any())
            {
                return;
            }

            List<string> nodes = new List<string>();
            Dictionary<string, int> edges = new Dictionary<string, int>();

            foreach (var war in Entity.Wars)
            {
                foreach (var battle in war.Battles)
                {
                    string attacker = CreateNode(battle.Attacker);
                    if (!nodes.Contains(attacker))
                    {
                        nodes.Add(attacker);
                    }
                    string defender = CreateNode(battle.Defender);
                    if (!nodes.Contains(defender))
                    {
                        nodes.Add(defender);
                    }
                    string faveColor = ColorTranslator.ToHtml(battle.Attacker.IdenticonColor);
                    if (string.IsNullOrEmpty(faveColor) && battle.Attacker.Parent != null)
                    {
                        faveColor = ColorTranslator.ToHtml(battle.Attacker.Parent.IdenticonColor);
                    }
                    string edge = "{ data: { source: '" + battle.Attacker.ID + "', target: '" + battle.Defender.ID + "', faveColor: '" + faveColor + "', width: WIDTH, label: LABEL } },";
                    if (edges.ContainsKey(edge))
                    {
                        edges[edge]++;
                    }
                    else
                    {
                        edges[edge] = 1;
                    }
                }
            }
            if (_allEnemies.Count > 5)
            {
                HTML.AppendLine("<div class=\"col-md-12\">");
            }
            else
            {
                HTML.AppendLine("<div class=\"col-md-6 col-sm-12\">");
            }
            HTML.AppendLine(Bold("Battles against other Entities - Sum of battles - Graph") + LineBreak);
            HTML.AppendLine("<div id=\"warfaregraph\" class=\"legends_graph\"></div>");
            HTML.AppendLine("</div>");


            HTML.AppendLine("<script type=\"text/javascript\" src=\"" + LocalFileProvider.LocalPrefix + "WebContent/scripts/cytoscape.min.js\"></script>");
            HTML.AppendLine("<script>");
            HTML.AppendLine("window.warfaregraph_nodes = [");
            foreach (var node in nodes)
            {
                HTML.AppendLine(node);
            }
            HTML.AppendLine("]");
            HTML.AppendLine("window.warfaregraph_edges = [");
            foreach (var edge in edges)
            {
                HTML.AppendLine(edge.Key.Replace("WIDTH", edge.Value > 15 ? "15" : edge.Value.ToString()).Replace("LABEL", edge.Value.ToString()));
            }
            HTML.AppendLine("]");
            HTML.AppendLine("</script>");
            HTML.AppendLine("<script type=\"text/javascript\" src=\"" + LocalFileProvider.LocalPrefix + "WebContent/scripts/warfaregraph.js\"></script>");
        }

        private string CreateNode(Entity entity)
        {
            string classes = entity.Equals(Entity) ? " current" : "";
            string faveColor = ColorTranslator.ToHtml(entity.IdenticonColor);
            if (string.IsNullOrEmpty(faveColor) && entity.Parent != null)
            {
                faveColor = ColorTranslator.ToHtml(entity.Parent.IdenticonColor);
            }
            string title = "";
            if (!string.IsNullOrEmpty(entity.Race))
            {
                title += entity.Race;
                title += "\\n--------------------\\n";
            }
            title += entity.Name;
            if (entity.Type != EntityType.Unknown)
            {
                title += "\\n--------------------\\n";
                title += entity.Type.GetDescription();
            }

            if (entity.IsCiv)
            {
                classes += " civilization";
            }

            string node = "{ data: { ";
            node += "id: '" + entity.ID + "', ";
            node += "name: '" + WebUtility.HtmlEncode(title) + "', ";
            node += "href: 'entity#" + entity.ID + "', ";
            node += "faveColor: '" + faveColor + "', ";
            node += "icon: 'url(data:image/png;base64," + entity.SmallIdenticonString + ")' ";
            node += "}, classes: '" + classes + "' },";
            return node;
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
                        string positionName = position.GetTitleByCaste(assignment.HistoricalFigure.Caste);

                        HTML.AppendLine("<li>" + assignment.HistoricalFigure.ToLink() + ", " + positionName + "</li>");

                        if (!string.IsNullOrEmpty(position.Spouse))
                        {
                            HistoricalFigureLink spouseLink = assignment.HistoricalFigure.RelatedHistoricalFigures.FirstOrDefault(hfLink => hfLink.Type == HistoricalFigureLinkType.Spouse);
                            if (spouseLink != null)
                            {
                                HistoricalFigure spouse = spouseLink.HistoricalFigure;
                                if (spouse != null)
                                {
                                    string spousePositionName = position.GetTitleByCaste(spouse.Caste, true);
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
