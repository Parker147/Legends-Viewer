using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Legends;
using System.Drawing;
using LegendsViewer.Controls.HTML.Utilities;
using System.Windows.Forms;
using LegendsViewer.Legends.EventCollections;
using LegendsViewer.Legends.Events;

namespace LegendsViewer.Controls
{
    class WorldStatsPrinter : HTMLPrinter
    {
        World World;

        public WorldStatsPrinter(World world)
        {
            World = world;
        }

        public override string GetTitle()
        {
            return "Stats";
        }

        public override string Print()
        {
            HTML = new StringBuilder();

            HTML.AppendLine("<script>" + ChartJS + "</script>");
            HTML.AppendLine("<script>");
            HTML.AppendLine("var data = [");

            foreach (Population civilizedPop in World.CivilizedPopulations)
            {
                Color civilizedPopColor;
                if (World.MainRaces.ContainsKey(civilizedPop.Race))
                {
                    civilizedPopColor = World.MainRaces.First(r => r.Key == civilizedPop.Race).Value;
                }
                else
                {
                    civilizedPopColor = Color.Gray;
                }
                Color highlightPopColor = HTMLStyleUtil.ChangeColorBrightness(civilizedPopColor, 0.1f);
                Color darkenedPopColor = HTMLStyleUtil.ChangeColorBrightness(civilizedPopColor, -0.1f);
                HTML.AppendLine("{ value: " + civilizedPop.Count + ", color: \"" + ColorTranslator.ToHtml(darkenedPopColor) + "\", highlight: \"" + ColorTranslator.ToHtml(highlightPopColor) + "\", label: \"" + civilizedPop.Race + "\" }, ");
            }

            HTML.AppendLine("]");
            HTML.AppendLine("window.onload = function(){");
            HTML.AppendLine("var ctx = document.getElementById(\"canvas\").getContext(\"2d\");");
            HTML.AppendLine("window.myChart = new Chart(ctx).Doughnut(data, {animationEasing : \"easeOutCubic\",percentageInnerCutout : 60, legendTemplate : '<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>'});");
            HTML.AppendLine("var legenddiv = document.getElementById('canvas_legend');");
            HTML.AppendLine("legenddiv.innerHTML = window.myChart.generateLegend();");
            HTML.AppendLine("}");
            HTML.AppendLine("</script>");

            HTML.AppendLine("<h1>" + World.Name + "</h1></br>");

            HTML.AppendLine("<table>");
            HTML.AppendLine("<tr>");
            HTML.AppendLine("<td style=\"vertical-align: top; \">");
            int mapSideLength = 300;
            double resizePercent;
            Size mapSize;
            if (World.Map.Width > World.Map.Height) resizePercent = mapSideLength / Convert.ToDouble(World.Map.Width);
            else resizePercent = mapSideLength / Convert.ToDouble(World.Map.Height);
            mapSize = new Size(Convert.ToInt32(World.Map.Width * resizePercent), Convert.ToInt32(World.Map.Height * resizePercent));
            using (Bitmap resizedMap = new Bitmap(mapSize.Width, mapSize.Height))
            {
                using (Graphics resize = Graphics.FromImage(resizedMap))
                {
                    resize.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    resize.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    resize.DrawImage(World.Map, new Rectangle(0, 0, mapSize.Width, mapSize.Height), new Rectangle(0, 0, World.Map.Width, World.Map.Height), GraphicsUnit.Pixel);
                }
                HTML.AppendLine(MakeLink(BitmapToHTML(resizedMap), LinkOption.LoadMap) + "</br>");
            }
            HTML.AppendLine("</td>");

            HTML.AppendLine("<td style=\"vertical-align: middle; \">");
            HTML.AppendLine("<canvas id=\"canvas\" height=\"200\" width=\"200\"></canvas>");
            HTML.AppendLine("<div id=\"canvas_legend\" class=\"chart-legend\"></div>");
            HTML.AppendLine("</td>");

            HTML.AppendLine("<td style=\"vertical-align: top; \">");
            HTML.AppendLine("</td>");
            HTML.AppendLine("</tr>");

            var civsByRace = from civ in World.Entities.Where(entity => entity.IsCiv)
                             orderby civ.Race
                             select civ;

            var currentCivs = civsByRace.Where(civ => civ.Populations.Any(pop => pop.IsMainRace && pop.Count > 0)).ToList();
            var fallenCivs = civsByRace.Where(civ => !civ.Populations.Any(pop => pop.IsMainRace && pop.Count > 0)).ToList();

            HTML.AppendLine("<tr style=\"background-color: #ffffff;\">");
            if (currentCivs.Any())
            {
                HTML.AppendLine("<td style=\"vertical-align: top; \">");
                HTML.AppendLine("<h1>Current Civilizations: " + currentCivs.Count() + "</h1>");
                HTML.AppendLine("<ul>");
                foreach (var civRace in currentCivs.Select(cc => cc.Race).Distinct())
                {
                    HTML.AppendLine("<li>" + civRace + ": " + currentCivs.Count(cc => cc.Race == civRace) + "</li>");
                    HTML.AppendLine("<ul>");
                    foreach (var civ in currentCivs.Where(civ => civ.Race == civRace))
                    {
                        var intelligentPop = civ.Populations.Where(pop => pop.IsMainRace).ToList();
                        var intelligentPopCount = intelligentPop.Sum(cp => cp.Count);
                        var civPop = intelligentPop.FirstOrDefault(pop => pop.Race == civ.Race);
                        var civPopCount = civPop != null ? civPop.Count : 0;
                        HTML.AppendLine("<li>" + civ.ToLink()
                            + " [" + civPopCount + " +" + (intelligentPopCount - civPopCount) + " " + HTMLStyleUtil.SYMBOL_POPULATION
                            + ", " + civ.CurrentSites.Count + " " + HTMLStyleUtil.SYMBOL_SITE + "]</li>");
                    }
                    HTML.AppendLine("</ul>");
                }
                HTML.AppendLine("</ul>");
                HTML.AppendLine("</td>");
            }

            if (fallenCivs.Any())
            {
                HTML.AppendLine("<td style=\"vertical-align: top; \">");
                HTML.AppendLine("<h1>Fallen Civilizations: " + fallenCivs.Count() + "</h1>");
                HTML.AppendLine("<ul>");
                foreach (var civRace in fallenCivs.Select(fc => fc.Race).Distinct())
                {
                    HTML.AppendLine("<li>" + civRace + ": " + fallenCivs.Count(fc => fc.Race == civRace) + "</li>");
                    HTML.AppendLine("<ul>");
                    foreach (var civ in fallenCivs.Where(civ => civ.Race == civRace))
                    {
                        HTML.AppendLine("<li>" + civ.ToLink() + " " + HTMLStyleUtil.SYMBOL_DEAD + "</li>");
                    }
                    HTML.AppendLine("</ul>");
                }
                HTML.AppendLine("</ul>");
                HTML.AppendLine("</td>");
            }
            HTML.AppendLine("</tr>");
            HTML.AppendLine("</table>");

            HTML.AppendLine("</br>");
            HTML.AppendLine("<h1>Eras</h1>");
            HTML.AppendLine("<ol>");
            foreach (Era era in World.Eras)
                HTML.AppendLine("<li>" + era.Name + " (" + (era.StartYear < 0 ? 0 : era.StartYear) + " - " + era.EndYear + ")</li>");
            HTML.AppendLine("</ol>");
            HTML.AppendLine("</br>");

            HTML.AppendLine("<h1>Entities: " + World.Entities.Count(entity => !entity.IsCiv) + "</h1>");
            HTML.AppendLine("<ol>");
            var entityRaces = from entity in World.Entities.Where(entity => !entity.IsCiv)
                              orderby entity.Race
                              group entity by entity.Race into entityRace
                              select new { Type = entityRace.Key, Count = entityRace.Count() };
            entityRaces = entityRaces.OrderByDescending(entity => entity.Count);
            foreach (var entityRace in entityRaces)
                HTML.AppendLine("<li>" + entityRace.Type + ": " + entityRace.Count + "</li>");
            HTML.AppendLine("</ol>");
            HTML.AppendLine("</br>");

            HTML.AppendLine("<h1>Wars: " + World.EventCollections.OfType<War>().Count() + "</h1>");
            HTML.AppendLine("<ul>");
            HTML.AppendLine("<li>Battles: " + World.EventCollections.OfType<Battle>().Count() + "</li>");
            HTML.AppendLine("<ul>");
            HTML.AppendLine("<li>Notable: " + World.EventCollections.OfType<Battle>().Count(battle => battle.Notable) + "</li>");
            HTML.AppendLine("<li>Unnotable: " + World.EventCollections.OfType<Battle>().Count(battle => !battle.Notable) + "</li>");
            HTML.AppendLine("</ul>");
            HTML.AppendLine("</ul>");
            HTML.AppendLine("<ul>");
            HTML.AppendLine("<li>Conquerings: " + World.EventCollections.OfType<SiteConquered>().Count() + "</li>");
            var conquerings = from conquering in World.EventCollections.OfType<SiteConquered>()
                              group conquering by conquering.ConquerType into conquerType
                              select new { Type = conquerType.Key, Count = conquerType.Count() };
            conquerings = conquerings.OrderByDescending(conquering => conquering.Count);
            HTML.AppendLine("<ul>");
            foreach (var conquering in conquerings)
                HTML.AppendLine("<li>" + conquering.Type + "s: " + conquering.Count + "</li>");
            HTML.AppendLine("</ul>");
            HTML.AppendLine("</ul>");
            HTML.AppendLine("<ul>");
            HTML.AppendLine("<li>Deaths</li>");
            HTML.AppendLine("<ul>");
            HTML.AppendLine("<li>Historical Figures: " + World.EventCollections.OfType<Battle>().Sum(battle => battle.Collection.OfType<HFDied>().Count()) + "</li>");
            HTML.AppendLine("<li>Populations: " + World.EventCollections.OfType<Battle>().Sum(battle => battle.AttackerSquads.Sum(squad => squad.Deaths) + battle.DefenderSquads.Sum(squad => squad.Deaths)) + "</li>");
            HTML.AppendLine("</ul>");
            HTML.AppendLine("</ul>");
            HTML.AppendLine("</br>");

            HTML.AppendLine("<h1>Regions: " + World.Regions.Count + "</h1>");
            var regions = from region in World.Regions
                          group region by region.Type into regionType
                          select new { Type = regionType.Key, Count = regionType.Count() };
            regions = regions.OrderByDescending(region => region.Count);
            HTML.AppendLine("<ol>");
            foreach (var region in regions)
                HTML.AppendLine("<li>" + region.Type + ": " + region.Count + "</li>");
            HTML.AppendLine("</ol>");
            HTML.AppendLine("</br>");

            HTML.AppendLine("<h1>Underground Regions: " + World.UndergroundRegions.Count + "</h1>");
            var undergroundRegions = from undergroundRegion in World.UndergroundRegions
                                     group undergroundRegion by undergroundRegion.Type into type
                                     select new { Type = type.Key, Count = type.Count() };
            undergroundRegions = undergroundRegions.OrderByDescending(undergroundRegion => undergroundRegion.Count);
            HTML.AppendLine("<ol>");
            foreach (var undergroundRegion in undergroundRegions)
                HTML.AppendLine("<li>" + undergroundRegion.Type + ": " + undergroundRegion.Count + "</li>");
            HTML.AppendLine("</ol>");
            HTML.AppendLine("</br>");

            HTML.AppendLine("<h1>Sites: " + World.Sites.Count + "</h1>");
            var sites = from site in World.Sites
                        group site by site.Type into siteType
                        select new { Type = siteType.Key, Count = siteType.Count() };
            sites = sites.OrderByDescending(site => site.Count);
            HTML.AppendLine("<ol>");
            foreach (var site in sites)
                HTML.AppendLine("<li>" + site.Type + ": " + site.Count + "</li>");
            HTML.AppendLine("</ol>");
            HTML.AppendLine("</br>");

            HTML.AppendLine("<h1>Historical Figures: " + World.HistoricalFigures.Count + "</h1>");
            var hfRaces = from hf in World.HistoricalFigures
                          group hf by hf.Race into hfRace
                          select new { Race = hfRace.Key, Counts = hfRace.Count() };
            hfRaces = hfRaces.OrderByDescending(hf => hf.Counts);
            HTML.AppendLine("<ol>");
            foreach (var hfRace in hfRaces)
                HTML.AppendLine("<li>" + hfRace.Race + ": " + hfRace.Counts + "</li>");
            HTML.AppendLine("</ol>");
            HTML.AppendLine("<h2><b>Alive: " + World.HistoricalFigures.Count(hf => hf.DeathYear == -1) + "</b></h2>");
            var aliveHFs = from hf in World.HistoricalFigures.Where(hf => hf.DeathYear == -1)
                           group hf by hf.Race into hfRace
                           select new { Type = hfRace.Key, Count = hfRace.Count() };
            aliveHFs = aliveHFs.OrderByDescending(hf => hf.Count);
            HTML.AppendLine("<ol>");
            foreach (var aliveHF in aliveHFs)
                HTML.AppendLine("<li>" + aliveHF.Type + ": " + aliveHF.Count + "</li>");
            HTML.AppendLine("</ol>");
            HTML.AppendLine("</br>");

            HTML.AppendLine("<h1>Site Populations: " + World.SitePopulations.Sum(population => population.Count) + "</h1>");
            var populations = from population in World.SitePopulations
                              group population by population.Race into popType
                              select new { Type = popType.Key, Count = popType.Sum(population => population.Count) };
            populations = populations.OrderByDescending(population => population.Count);
            HTML.AppendLine("<ol>");
            foreach (var population in populations)
                HTML.AppendLine("<li>" + population.Type + ": " + population.Count + "</li>");
            HTML.AppendLine("</ol>");
            HTML.AppendLine("</br>");

            List<string> deaths = new List<string>();
            World.Battles.SelectMany(battle => battle.GetSubEvents().OfType<HFDied>()).Select(death => death.HistoricalFigure.Race).ToList().ForEach(death => deaths.Add(death));
            var popDeaths = World.Battles.SelectMany(battle => battle.AttackerSquads.Concat(battle.DefenderSquads)).GroupBy(squad => squad.Race).Select(squad => new { Type = squad.Key, Count = squad.Sum(population => population.Deaths) });
            foreach (var pop in popDeaths)
                for (int i = 0; i < pop.Count; i++) deaths.Add(pop.Type);
            var deathsGrouped = deaths.GroupBy(race => race).Select(race => new { Type = race.Key, Count = race.Count() }).OrderByDescending(race => race.Count);
            HTML.AppendLine("<h1><b>Battle Deaths by Race: " + deaths.Count + "</b></h2>");
            HTML.AppendLine("<ol>");
            foreach (var race in deathsGrouped)
                HTML.AppendLine("<li>" + race.Type + ": " + race.Count + "</li>");
            HTML.AppendLine("</ol>");
            HTML.AppendLine("</br>");

            HTML.AppendLine("<h1>Events: " + World.Events.Count + "</h1>");
            var events = from dEvent in World.Events
                         group dEvent by dEvent.Type into eventType
                         select new { Type = eventType.Key, Count = eventType.Count() };
            events = events.OrderByDescending(dEvent => dEvent.Count);
            HTML.AppendLine("<ul>");
            foreach (var dwarfEvent in events)
            {
                HTML.AppendLine("<li>" + AppHelpers.EventInfo[Array.IndexOf(AppHelpers.EventInfo, AppHelpers.EventInfo.Single(eventInfo => eventInfo[0] == dwarfEvent.Type))][1] + ": " + dwarfEvent.Count + "</li>");
                if (dwarfEvent.Type == "hf died")
                {
                    HTML.AppendLine("<ul>");
                    HTML.AppendLine("<li>As part of Collection</li>");
                    var deathCollections = from death in World.Events.OfType<HFDied>().Where(death => death.ParentCollection != null)
                                           group death by death.ParentCollection.Type into collectionType
                                           select new { Type = collectionType.Key, Count = collectionType.Count() };
                    deathCollections = deathCollections.OrderByDescending(collectionType => collectionType.Count);
                    HTML.AppendLine("<ul>");
                    foreach (var deathCollection in deathCollections)
                        HTML.AppendLine("<li>" + Formatting.InitCaps(deathCollection.Type) + ": " + deathCollection.Count + "</li>");
                    HTML.AppendLine("<li>None: " + World.Events.OfType<HFDied>().Count(death => death.ParentCollection == null) + "</li>");
                    HTML.AppendLine("</ul>");
                    HTML.AppendLine("</ul>");
                    HTML.AppendLine("<ul>");
                    HTML.AppendLine("<li>Deaths by Race</li>");
                    HTML.AppendLine("<ol>");
                    var hfDeaths = from death in World.Events.OfType<HFDied>()
                                   group death by death.HistoricalFigure.Race into deathRace
                                   select new { Type = deathRace.Key, Count = deathRace.Count() };
                    hfDeaths = hfDeaths.OrderByDescending(death => death.Count);
                    foreach (var hfdeath in hfDeaths)
                        HTML.AppendLine("<li>" + hfdeath.Type + ": " + hfdeath.Count + "</li>");
                    HTML.AppendLine("</ol>");
                    HTML.AppendLine("</ul>");
                    HTML.AppendLine("<ul>");
                    HTML.AppendLine("<li>Deaths by Cause</li>");
                    HTML.AppendLine("<ul>");
                    var deathCauses = from death in World.Events.OfType<HFDied>()
                                      group death by death.Cause into deathCause
                                      select new { Cause = deathCause.Key, Count = deathCause.Count() };
                    deathCauses = deathCauses.OrderByDescending(death => death.Count);
                    foreach (var deathCause in deathCauses)
                        HTML.AppendLine("<li>" + deathCause.Cause.GetDescription() + ": " + deathCause.Count + "</li>");
                    HTML.AppendLine("</ul>");
                    HTML.AppendLine("</ul>");
                }
                if (dwarfEvent.Type == "hf simple battle event")
                {
                    var fightTypes = from fight in World.Events.OfType<HFSimpleBattleEvent>()
                                     group fight by fight.SubType into fightType
                                     select new { Type = fightType.Key, Count = fightType.Count() };
                    fightTypes = fightTypes.OrderByDescending(fightType => fightType.Count);
                    HTML.AppendLine("<ul>");
                    HTML.AppendLine("<li>Fight SubTypes</li>");
                    HTML.AppendLine("<ul>");
                    foreach (var fightType in fightTypes)
                        HTML.AppendLine("<li>" + fightType.Type.GetDescription() + ": " + fightType.Count + "</li>");
                    HTML.AppendLine("</ul>");
                    HTML.AppendLine("</ul>");
                }
                if (dwarfEvent.Type == "change hf state")
                {
                    var states = from state in World.Events.OfType<ChangeHFState>()
                                 group state by state.State into stateType
                                 select new { Type = stateType.Key, Count = stateType.Count() };
                    states = states.OrderByDescending(state => state.Count);
                    HTML.AppendLine("<ul>");
                    HTML.AppendLine("<li>States</li>");
                    HTML.AppendLine("<ul>");
                    foreach (var state in states)
                        HTML.AppendLine("<li>" + state.Type.GetDescription() + ": " + state.Count + "</li>");
                    HTML.AppendLine("</ul>");
                    HTML.AppendLine("</ul>");
                }
            }
            HTML.AppendLine("</ul>");
            HTML.AppendLine("</br>");

            HTML.AppendLine("<h1>Event Collections: " + World.EventCollections.Count + "</h1>");
            var collectionTypes = from collection in World.EventCollections
                                  group collection by collection.Type into collectionType
                                  select new { Type = collectionType.Key, Count = collectionType.Count() };
            collectionTypes = collectionTypes.OrderByDescending(collection => collection.Count);
            foreach (var collectionType in collectionTypes)
            {
                HTML.AppendLine("<h2>" + Formatting.InitCaps(collectionType.Type) + ": " + collectionType.Count + "</h2>");
                HTML.AppendLine("<ul>");
                var subCollections = from subCollection in World.EventCollections.Where(collection => collection.Type == collectionType.Type).SelectMany(collection => collection.Collections)
                                     group subCollection by subCollection.Type into subCollectionType
                                     select new { Type = subCollectionType.Key, Count = subCollectionType.Count() };
                subCollections = subCollections.OrderByDescending(collection => collection.Count);
                if (subCollections.Any())
                {
                    HTML.AppendLine("<li>Sub Collections");
                    HTML.AppendLine("<ul>");
                    foreach (var subCollection in subCollections)
                        HTML.AppendLine("<li>" + Formatting.InitCaps(subCollection.Type) + ": " + subCollection.Count + "</li>");
                    HTML.AppendLine("</ul>");
                }

                HTML.AppendLine("<li>Events");
                var eventTypes = from dwarfEvent in World.EventCollections.Where(collection => collection.Type == collectionType.Type).SelectMany(collection => collection.Collection)
                                 group dwarfEvent by dwarfEvent.Type into eventType
                                 select new { Type = eventType.Key, Count = eventType.Count() };
                eventTypes = eventTypes.OrderByDescending(eventType => eventType.Count);
                HTML.AppendLine("<ul>");
                foreach (var eventType in eventTypes)
                    HTML.AppendLine("<li>" + AppHelpers.EventInfo[Array.IndexOf(AppHelpers.EventInfo, AppHelpers.EventInfo.Single(eventInfo => eventInfo[0] == eventType.Type))][1] + ": " + eventType.Count + "</li>");
                HTML.AppendLine("</ul>");

                HTML.AppendLine("</ul>");
            }
            HTML.AppendLine("</br>");

            if (World.CivilizedPopulations.Any())
            {
                HTML.AppendLine("<h1>Civilized Populations</h1>");
                HTML.AppendLine("<ol>");
                foreach (Population population in World.CivilizedPopulations)
                    if (population.Count == Int32.MaxValue) HTML.AppendLine("<li>" + population.Race + ": Unnumbered" + "</li>");
                    else HTML.AppendLine("<li>" + population.Race + ": " + population.Count + "</li>");
                HTML.AppendLine("</ol>");
                HTML.AppendLine("</br>");
            }
            if (World.OutdoorPopulations.Any())
            {
                HTML.AppendLine("<h1>Outdoor Populations</h1>");
                HTML.AppendLine("<ol>");
                foreach (Population population in World.OutdoorPopulations)
                    if (population.Count == Int32.MaxValue) HTML.AppendLine("<li>" + population.Race + ": Unnumbered" + "</li>");
                    else HTML.AppendLine("<li>" + population.Race + ": " + population.Count + "</li>");
                HTML.AppendLine("</ol>");
                HTML.AppendLine("</br>");

            }
            if (World.UndergroundPopulations.Any())
            {
                HTML.AppendLine("<h1>Underground Populations</h1>");
                HTML.AppendLine("<ol>");
                foreach (Population population in World.UndergroundPopulations)
                    if (population.Count == Int32.MaxValue) HTML.AppendLine("<li>" + population.Race + ": Unnumbered" + "</li>");
                    else HTML.AppendLine("<li>" + population.Race + ": " + population.Count + "</li>");
                HTML.AppendLine("</ol>");
                HTML.AppendLine("</br>");
            }

            return HTML.ToString();
        }
    }
}
