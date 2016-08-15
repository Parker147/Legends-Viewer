using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using LegendsViewer.Legends;
using LegendsViewer.Legends.EventCollections;
using LegendsViewer.Legends.Events;

namespace LegendsViewer
{
    public class ChartPanel : Panel
    {
        Chart DwarfChart;
        DwarfObject FocusObject;
        World World;
        List<Series> Series = new List<Series>();
        public Boolean OtherChart;
        ToolStripMenuItem TimelineMenu;
        string AliveHFRace = "";
        public List<ChartOption> SeriesOptions = new List<ChartOption>();
        public ChartPanel(World world, DwarfObject focusObject, List<ChartOption> options = null)
        {
            Dock = DockStyle.Fill;

            FocusObject = focusObject as DwarfObject;
            World = world;
            DwarfChart = new Chart();
            DwarfChart.Dock = DockStyle.Fill;
            Controls.Add(DwarfChart);
            this.Refresh();

            DwarfChart.BackColor = Color.Wheat;
            DwarfChart.BackGradientStyle = GradientStyle.TopBottom;
            DwarfChart.ChartAreas.Add(new ChartArea());
            DwarfChart.ChartAreas.Last().AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            DwarfChart.ChartAreas.Last().BackColor = Color.FromArgb(235, 235, 235);
            DwarfChart.Legends.Add(new Legend());
            DwarfChart.Legends.Last().LegendStyle = LegendStyle.Row;
            DwarfChart.Legends.Last().Position.Width = 100;
            DwarfChart.Legends.Last().Position.Y = 100;
            DwarfChart.Legends.Last().Position.Height = 4;
            DwarfChart.Titles.Add(new Title());
            DwarfChart.Titles.Last().Position.Auto = false;
            DwarfChart.Titles.Last().Position.X = 50;
            DwarfChart.Titles.Last().Position.Y = 1.5f;
            if (options != null) SeriesOptions = options;
            else if (FocusObject.GetType() == typeof(Battle))
                SeriesOptions.Add(ChartOption.OtherBattleRemaining);
            else
            {
                SeriesOptions.Add(ChartOption.TimelineEvents);
                SeriesOptions.Add(ChartOption.TimelineEventsFiltered);
            }
            SetupMenu();
            //GenerateSeries();
        }

        public void RefreshFiltered()
        {
            if (SeriesOptions.Contains(ChartOption.TimelineEventsFiltered))
            {
                Series.RemoveAt(SeriesOptions.IndexOf(ChartOption.TimelineEventsFiltered));
                SeriesOptions.Remove(ChartOption.TimelineEventsFiltered);
                SeriesOptions.Add(ChartOption.TimelineEventsFiltered);
                GenerateSeries();
            }
        }

        /*protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            RefreshAllSeries();
        }*/

        public void RefreshAllSeries()
        {
            SeriesOptions = SeriesOptions.Distinct().ToList();
            Series.Clear();
            DwarfChart.Series.Clear();
            GenerateSeries();
        }

        public void GenerateSeries()
        {
            for (int i = 0; i < TimelineMenu.DropDownItems.Count; i++)
            {
                ChartMenuItem item = (TimelineMenu.DropDownItems[i] as ChartMenuItem);
                if (SeriesOptions.Contains(item.Option)) item.Checked = true;
                else item.Checked = false;
            }

            int newSeries = SeriesOptions.Count - Series.Count;
            if (newSeries < 0) //Some Error prevention, sometimes Series don't get remade properly during Refresh and will have too man, so force it to remake the series, obsolete?
            {
                Series.Clear();
                newSeries = SeriesOptions.Count;
            }
            if (newSeries > 0)
            {
                DwarfChart.Series.Clear();
                int seriesCount = SeriesOptions.Count;
                for (int i = SeriesOptions.Count - newSeries; i < SeriesOptions.Count; i++)
                {
                    List<Series> addSeries = GenerateSeries(SeriesOptions[i]);
                    foreach (Series series in addSeries)
                    {
                        Series.Add(series);
                        if (SeriesOptions.Count(option => option == SeriesOptions[i]) != addSeries.Count)
                            SeriesOptions.Insert(i, SeriesOptions[i++]);

                    }
                }

                if (SeriesOptions.Contains(ChartOption.TimelineEventsFiltered))
                {
                    int i = SeriesOptions.IndexOf(ChartOption.TimelineEventsFiltered);
                    SeriesOptions.Remove(ChartOption.TimelineEventsFiltered);
                    SeriesOptions.Insert(0, ChartOption.TimelineEventsFiltered);
                    Series temp = Series[i];
                    Series.RemoveAt(i);
                    Series.Insert(0, temp);
                }
                if (SeriesOptions.Contains(ChartOption.TimelineEvents))
                {
                    int i = SeriesOptions.IndexOf(ChartOption.TimelineEvents);
                    SeriesOptions.Remove(ChartOption.TimelineEvents);
                    SeriesOptions.Insert(0, ChartOption.TimelineEvents);
                    Series temp = Series[i];
                    Series.RemoveAt(i);
                    Series.Insert(0, temp);
                }

                foreach (Series series in Series) DwarfChart.Series.Add(series);
            }

            if (DwarfChart.Series.Count == 0) return;
            if (!SeriesOptions.Contains(ChartOption.WorldHFRemaining) && !SeriesOptions.Contains(ChartOption.OtherBattleRemaining))
            {
                DwarfChart.ChartAreas.First().AxisX.Minimum = DwarfChart.Series.SelectMany(series => series.Points).Select(point => point.XValue).Min();
                DwarfChart.ChartAreas.First().AxisX.Maximum = DwarfChart.Series.SelectMany(series => series.Points).Select(point => point.XValue).Max();
            }
            else
            {
                DwarfChart.ChartAreas.First().AxisX.Minimum = DwarfChart.Series.SelectMany(series => series.Points).Select(point => point.XValue).Min() - 1;
                DwarfChart.ChartAreas.First().AxisX.Maximum = DwarfChart.Series.SelectMany(series => series.Points).Select(point => point.XValue).Max() + 1;
            }

            double maxYAxis = DwarfChart.Series.SelectMany(series => series.Points).Select(point => point.YValues[0]).Max();
            int minYAxis = 20;
            if (maxYAxis < minYAxis)
                DwarfChart.ChartAreas.First().AxisY.Maximum = minYAxis;
            else
                DwarfChart.ChartAreas.First().AxisY.Maximum = maxYAxis * 1.05;
        }

        private List<Series> GenerateSeries(ChartOption option, string command = "")
        {
            List<Series> series = new List<Series>();
            switch (option) //Series Setup
            {
                case ChartOption.TimelineEvents: series.Add(new Series("Events"));
                    series.First().Color = Color.FromArgb(65, 140, 240); break;
                case ChartOption.TimelineEventsFiltered: series.Add(new Series("Events (Filtered)"));
                    series.First().Color = Color.FromArgb(252, 180, 64); break;
                case ChartOption.TimelineActiveSites: series.Add(new Series("Sites"));
                    series.First().Color = Color.FromArgb(145, 70, 170); break;
                case ChartOption.TimelineActiveSitesByRace:
                    List<string> races = World.Entities.Where(entity => entity.IsCiv).GroupBy(entity => entity.Race).Select(entity => entity.Key).ToList();
                    foreach (string race in races)
                    {
                        Series raceSeries = new Series(race);
                        raceSeries.Color = World.Entities.First(entity => entity.IsCiv && entity.Race == race).LineColor;
                        raceSeries.BorderWidth = 3;
                        raceSeries.IsVisibleInLegend = false;
                        series.Add(raceSeries);
                    }
                    break;
                case ChartOption.TimelineActiveWars: series.Add(new Series("Wars"));
                    series.First().Color = Color.FromArgb(202, 107, 75); break;
                case ChartOption.TimelineAliveHFs:
                    series.Add(new Series("Historical Figures"));
                    series.First().Color = Color.FromArgb(224, 64, 10); break;
                case ChartOption.TimeLineAliveHFSpecific:
                    series.Add(new Series(AliveHFRace));
                    series.First().Color = Color.FromArgb(224, 64, 10); break;
                case ChartOption.TimelineBattles: series.Add(new Series("Battles"));
                    series.First().Color = Color.FromArgb(26, 59, 105); break;
                case ChartOption.TimelineBeastAttacks: series.Add(new Series("Beast Attacks"));
                    series.First().Color = Color.FromArgb(105, 170, 60); break;
                case ChartOption.TimelineBattleDeaths: series.Add(new Series("Battle Deaths"));
                    series.First().Color = Color.FromArgb(130, 160, 210); break;
                case ChartOption.WorldHFAlive: series.Add(new Series("Historical Figures - Alive")); break;
                case ChartOption.WorldHFRemaining:
                    series.Add(new Series("Historical Figures - Totals"));
                    series.Add(new Series("Historical Figures - Remaining"));
                    break;
                //case ChartOption.WorldHFDead: series.Insert(new Series("Historical Figures - Dead");break;
                case ChartOption.WorldHFRaces: series.Add(new Series("Historical Figures")); break;
                case ChartOption.WorldOutdoorPopulations: series.Add(new Series("Outdoor Populations (Not Including Unnumbered)")); break;
                case ChartOption.WorldRegionTypes: series.Add(new Series("Regions")); break;
                case ChartOption.WorldSitePopulations: series.Add(new Series("Site Populations")); break;
                case ChartOption.WorldDeaths: series.Add(new Series("Deaths")); break;
                case ChartOption.WorldSiteTypes: series.Add(new Series("Sites")); break;
                case ChartOption.WorldUndergroundPopulations: series.Add(new Series("Underground Populations (Not Including Unnumbered)")); break;
                case ChartOption.OtherEventTypes: series.Add(new Series("Event Types")); break;
                case ChartOption.OtherEntityPopulations: series.Add(new Series("Entity Populations")); break;
                case ChartOption.OtherKillsByRace: series.Add(new Series("Kills by Race")); break;
                case ChartOption.OtherDeaths: series.Add(new Series("Deaths")); break;
                case ChartOption.OtherSitePopulations: series.Add(new Series("Site Populations")); break;
                case ChartOption.OtherWarLosses: series.Add(new Series("War Losses")); break;
                case ChartOption.OtherBattleRemaining:
                    series.Add(new Series("Forces - Total"));
                    series.Add(new Series("Forces - Remaining"));
                    break;
            }

            switch (option) //Chart Setup
            {
                case ChartOption.TimelineEvents:
                case ChartOption.TimelineEventsFiltered:
                case ChartOption.TimelineActiveSites:
                case ChartOption.TimelineActiveSitesByRace:
                case ChartOption.TimelineActiveWars:
                case ChartOption.TimelineAliveHFs:
                case ChartOption.TimeLineAliveHFSpecific:
                case ChartOption.TimelineBattles:
                case ChartOption.TimelineBeastAttacks:
                case ChartOption.TimelineBattleDeaths:
                    foreach (Series setup in series)
                    {
                        if (option == ChartOption.TimelineEvents || option == ChartOption.TimelineEventsFiltered)
                            setup.ChartType = SeriesChartType.Area;
                        else
                            setup.ChartType = SeriesChartType.Line;
                        //setup.XValueType = ChartValueType.Int32;
                    }

                    DwarfChart.Legends.Last().LegendStyle = LegendStyle.Row;
                    DwarfChart.Legends.Last().Position.Auto = false;
                    DwarfChart.Legends.Last().Position.Width = 100;
                    DwarfChart.Legends.Last().Position.Y = 100;
                    DwarfChart.Legends.Last().Position.Height = 4;
                    DwarfChart.Titles.Last().Text = "Timeline";
                    DwarfChart.ChartAreas.Last().Area3DStyle.Enable3D = false;
                    DwarfChart.ChartAreas.First().AxisX.MajorGrid.Enabled = true;
                    DwarfChart.ChartAreas.First().AxisX.Interval = 0;
                    DwarfChart.ChartAreas.First().AxisX.LabelStyle.Angle = 0;
                    DwarfChart.Legends.Last().Enabled = true;
                    break;
                case ChartOption.WorldHFAlive:
                //case ChartOption.WorldHFDead:
                case ChartOption.WorldHFRaces:
                case ChartOption.WorldOutdoorPopulations:
                case ChartOption.WorldRegionTypes:
                case ChartOption.WorldSitePopulations:
                case ChartOption.WorldDeaths:
                case ChartOption.WorldSiteTypes:
                case ChartOption.WorldUndergroundPopulations:
                case ChartOption.OtherEventTypes:
                case ChartOption.OtherEntityPopulations:
                case ChartOption.OtherKillsByRace:
                //case ChartOption.OtherEntityWarDeaths:
                //case ChartOption.OtherEntityWarKills:
                //case ChartOption.OtherEntityWarLosses:
                case ChartOption.OtherDeaths:
                case ChartOption.OtherSitePopulations:
                case ChartOption.OtherWarLosses:
                    series.First().ChartType = SeriesChartType.Pie;
                    if (option == ChartOption.OtherWarLosses)
                        series.First().CustomProperties = "PieLabelStyle=Outside,PieStartAngle=270";
                    else
                        series.First().CustomProperties = "CollectedThreshold=0.75, PieLabelStyle=Outside,PieStartAngle=270";
                    series.First().IsValueShownAsLabel = true;
                    series.First().Label = "#LEGENDTEXT\n#VAL (#PERCENT)";
                    DwarfChart.ChartAreas.Last().Area3DStyle.Enable3D = true;
                    DwarfChart.ChartAreas.First().AxisX.MajorGrid.Enabled = true;
                    DwarfChart.Legends.Last().Enabled = false;
                    DwarfChart.Titles.Last().Text = series.First().Name;
                    break;
                //DwarfChart.LegendsViewer.Last().LegendStyle = LegendStyle.Column;
                //DwarfChart.LegendsViewer.Last().Position.Auto = true; break;
                case ChartOption.WorldHFRemaining:
                case ChartOption.OtherBattleRemaining:
                    foreach (Series setup in series)
                    {
                        setup.ChartType = SeriesChartType.Column;
                        setup.IsValueShownAsLabel = true;
                        setup.CustomProperties = "DrawSideBySide=False";
                    }
                    //series.Last().CustomProperties += ",LabelStyle=Bottom";
                    DwarfChart.ChartAreas.Last().Area3DStyle.Enable3D = false;
                    DwarfChart.ChartAreas.Last().AxisX.IntervalOffset = 0;
                    DwarfChart.ChartAreas.First().AxisX.MajorGrid.Enabled = false;
                    DwarfChart.ChartAreas.First().AxisX.Interval = 1;
                    DwarfChart.ChartAreas.First().AxisX.LabelStyle.Angle = -30;
                    DwarfChart.Legends.Last().Enabled = false;
                    DwarfChart.Titles.Last().Text = series.Last().Name;
                    break;
            }

            switch (option) //Generate Series
            {
                case ChartOption.TimelineActiveSites:
                case ChartOption.TimelineActiveSitesByRace:
                case ChartOption.TimelineActiveWars:
                case ChartOption.TimelineAliveHFs:
                case ChartOption.TimeLineAliveHFSpecific:
                case ChartOption.TimelineBattles:
                case ChartOption.TimelineBeastAttacks:
                case ChartOption.TimelineEvents:
                case ChartOption.TimelineEventsFiltered:
                case ChartOption.TimelineBattleDeaths:
                    int startYear = 0, endYear = 0;
                    List<WorldEvent> eventsList = null;
                    List<BeastAttack> beastAttacks;
                    List<Battle> battles;
                    List<War> wars;
                    List<HistoricalFigure> aliveHFs = null;
                    List<HistoricalFigure> hfs = null;


                    if (FocusObject is EventCollection)
                        eventsList = (FocusObject as EventCollection).GetSubEvents();
                    else
                        eventsList = (FocusObject as WorldObject).Events;

                    if (FocusObject.GetType() == typeof(Entity)) wars = (FocusObject as Entity).Wars;
                    else if (FocusObject.GetType() == typeof(War)) wars = new List<War>() { FocusObject as War };
                    else wars = World.Wars;

                    if (FocusObject.GetType() == typeof(HistoricalFigure)) battles = (FocusObject as HistoricalFigure).Battles;
                    else if (FocusObject.GetType() == typeof(Site)) battles = (FocusObject as Site).Warfare.OfType<Battle>().ToList();
                    else if (FocusObject.GetType() == typeof(Entity)) battles = (FocusObject as Entity).Wars.SelectMany(war => war.Collections.OfType<Battle>()).ToList();
                    else if (FocusObject.GetType() == typeof(War)) battles = (FocusObject as War).Collections.OfType<Battle>().ToList();
                    else if (FocusObject.GetType() == typeof(WorldRegion)) battles = (FocusObject as WorldRegion).Battles;
                    else battles = World.Battles;

                    if (FocusObject.GetType() == typeof(HistoricalFigure)) beastAttacks = (FocusObject as HistoricalFigure).BeastAttacks;
                    else if (FocusObject.GetType() == typeof(Site)) beastAttacks = (FocusObject as Site).BeastAttacks;
                    else beastAttacks = World.BeastAttacks;

                    eventsList = eventsList.OrderBy(events => events.Year).ToList();
                    if (eventsList.Count > 0)
                    {
                        startYear = eventsList.First().Year;
                        endYear = eventsList.Last().Year;
                    }

                    int hfIndex = 0;
                    if (option == ChartOption.TimelineAliveHFs || option == ChartOption.TimeLineAliveHFSpecific)
                    {
                        if (option == ChartOption.TimelineAliveHFs)
                            hfs = World.HistoricalFigures.OrderBy(hf => hf.BirthYear).ToList();
                        //aliveHFs = World.HistoricalFigures.Where(hf => hf.BirthYear <= startYear).ToList();
                        else
                            hfs = World.HistoricalFigures.Where(hf => hf.Race == AliveHFRace).OrderBy(hf => hf.BirthYear).ToList(); //====================================================
                        aliveHFs = hfs.Where(hf => hf.BirthYear <= startYear).ToList();
                        //hfs = World.HistoricalFigures.OrderBy(hf => hf.BirthYear).ToList();
                        HistoricalFigure firstHFIndex = hfs.FirstOrDefault(hf => hf.BirthYear > startYear);
                        if (firstHFIndex == null) 
                            hfIndex = 0;
                        else
                            hfIndex = hfs.IndexOf(firstHFIndex);
                    }

                    int offset = 0;
                    if (startYear == -1)
                        offset = 1;
                    else
                    {
                        // offset = startYear % 10;
                        // if (offset < 5) offset = 0 - offset;
                    }
                    DwarfChart.ChartAreas.Last().AxisX.IntervalOffset = offset;

                    //uses an event index so the loop doesn't go through every event for each year, only the eventsList for that year
                    int eventIndex = 0;
                    if (eventsList.Count > 0)
                        eventIndex = eventsList.IndexOf(eventsList.First(ev => ev.Year == startYear));
                    int beastAttackStartYear = 0;
                    int beastAttackIndex = 0;
                    if (beastAttacks != null && beastAttacks.Count(ba => ba.StartYear >= startYear) > 0)
                    {
                        if (beastAttacks.FindIndex(ba => ba.StartYear == startYear) >= 0) beastAttackStartYear = startYear;
                        else beastAttackStartYear = beastAttacks.First(ba => ba.StartYear > startYear).StartYear;
                        beastAttackIndex = beastAttacks.IndexOf(beastAttacks.First(ba => ba.StartYear == beastAttackStartYear));
                    }
                    int battleStartYear = 0;
                    int battleIndex = 0;
                    if (battles != null && battles.Count > 0)
                    {
                        if (battles.FindIndex(battle => battle.StartYear == startYear) >= 0) battleStartYear = startYear;
                        else battleStartYear = battles.First(battle => battle.StartYear > startYear).StartYear;
                        battleIndex = battles.IndexOf(battles.First(battle => battle.StartYear == battleStartYear));
                    }
                    for (int year = startYear; year <= endYear; year++)
                    {
                        int count = 0;
                        switch (option)
                        {
                            case ChartOption.TimelineEvents:
                            case ChartOption.TimelineEventsFiltered:
                                while (eventIndex < eventsList.Count && eventsList[eventIndex].Year == year)
                                {
                                    if (option == ChartOption.TimelineEvents) count++;
                                    else if (!(FocusObject.GetType().GetField("Filters").GetValue(null) as List<string>).Contains(eventsList[eventIndex].Type))
                                        count++;
                                    eventIndex++;
                                }
                                break;
                            case ChartOption.TimelineActiveSites:
                                if (FocusObject.GetType() == typeof(Era))
                                    count = World.Entities.Where(entity => entity.IsCiv).Sum(entity => entity.SiteHistory.Count(site => year >= site.StartYear && (year <= site.EndYear || site.EndYear == -1)));
                                else
                                    count = (FocusObject as Entity).SiteHistory.Count(site => year >= site.StartYear && (year <= site.EndYear || site.EndYear == -1));
                                break;
                            case ChartOption.TimelineActiveSitesByRace:
                                foreach (Series race in series)
                                {
                                    count = World.Entities.Where(entity => entity.IsCiv && entity.Race == race.Name).Sum(entity => entity.SiteHistory.Count(site => year >= site.StartYear && (year <= site.EndYear || site.EndYear == -1)));
                                    race.Points.AddXY(year, count);
                                }
                                break;
                            case ChartOption.TimelineActiveWars: count = wars.Count(war => year >= war.StartYear && (year <= war.EndYear || war.EndYear == -1)); break;
                            case ChartOption.TimelineAliveHFs:
                            case ChartOption.TimeLineAliveHFSpecific:
                                aliveHFs.RemoveAll(hf => hf.DeathYear <= year && hf.DeathYear != -1); //Removes Dead HFs
                                while (hfIndex < hfs.Count && hfs[hfIndex].BirthYear == year) //Adds HFs born in current year;
                                {
                                    aliveHFs.Add(hfs[hfIndex]);
                                    hfIndex++;
                                }
                                count = aliveHFs.Count;
                                break;
                            //count = World.HistoricalFigures.Count(hf => year >= hf.BirthYear && (year <= hf.DeathYear || hf.DeathYear == -1)); break;
                            case ChartOption.TimelineBattles:
                            case ChartOption.TimelineBattleDeaths:
                                //count = battles.Count(battle => battle.StartYear == year);
                                while (battleIndex < battles.Count && battles[battleIndex].StartYear == year)
                                {
                                    switch (option)
                                    {
                                        case ChartOption.TimelineBattles: count++; break;
                                        case ChartOption.TimelineBattleDeaths:
                                            Battle yearBattle = battles[battleIndex];
                                            count += yearBattle.AttackerDeathCount + yearBattle.DefenderDeathCount;
                                            break;
                                    }
                                    battleIndex++;
                                }
                                break;
                            case ChartOption.TimelineBeastAttacks:
                                while (beastAttackIndex < beastAttacks.Count && beastAttacks[beastAttackIndex].StartYear == year)
                                {
                                    count++;
                                    beastAttackIndex++;
                                }
                                break;

                            //case ChartOption.TimelineBattleDeaths: count = battles.Where(battle => battle.StartYear == year).Sum(battle => battle.AttackerDeathCount + battle.DefenderDeathCount); break;
                        }
                        if (series.Count == 1) series.First().Points.AddXY(year, count);
                    }
                    int maxPoints = Convert.ToInt32((DwarfChart.ClientRectangle.Width * 0.9)) / 3;
                    if (series.First().Points.Count > maxPoints)
                    {
                        List<double> averagedPoints = new List<double>();
                        int averageMaxCount = series.First().Points.Count / maxPoints;
                        int averageCount = 0;
                        double sum = 0;
                        for (int i = 0; i < series.First().Points.Count; i++)
                        {
                            sum += series.First().Points[i].YValues[0];
                            averageCount++;
                            if (averageCount == averageMaxCount)
                            {
                                averagedPoints.Add(sum / averageCount);
                                sum = 0;
                                averageCount = 0;
                            }
                        }
                        if (averageCount > 0) averagedPoints.Add(sum / averageCount);
                        double yearXPoints = Convert.ToDouble(endYear) / averagedPoints.Count;
                        series.First().Points.Clear();
                        for (int i = 0; i < averagedPoints.Count; i++)
                            series.First().Points.AddXY(i * yearXPoints, averagedPoints[i]);
                    }
                    break;
                case ChartOption.WorldHFAlive:
                    World.HistoricalFigures.Where(hf => hf.DeathYear == -1).GroupBy(hf => hf.Race).Select(hf => new { Race = hf.Key, Count = hf.Count() }).OrderByDescending(hf => hf.Count).ToList().ForEach(hf => { series.First().Points.AddY(hf.Count); series.First().Points.Last().LegendText = hf.Race; }); break;
                case ChartOption.WorldHFRemaining:
                    var hfTotals = World.HistoricalFigures.GroupBy(hf => hf.Race).Select(hf => new { Race = hf.Key, Count = hf.Count() }).OrderByDescending(hf => hf.Count).ToList();
                    var hfKilled = World.Events.OfType<HFDied>().GroupBy(death => death.HistoricalFigure.Race).Select(hf => new { Race = hf.Key, Count = hf.Count() }).OrderBy(hf => hfTotals.IndexOf(hfTotals.First(hf1 => hf1.Race == hf.Race))).ToList();
                    //= World.HistoricalFigures.Where(hf => hf.DeathYear == -1).GroupBy(hf => hf.Race).Select(hf => new { Race = hf.Key, Count = hf.Count() }).OrderBy(hf => hfTotals.IndexOf(hfTotals.First(hfT => hfT.Race == hf.Race))).ToList();
                    int otherLimit = Convert.ToInt32(hfTotals.Sum(hf => hf.Count) * 0.005);
                    var otherRaces = hfTotals.Where(hf => hf.Count < otherLimit).Select(hf => hf.Race).ToList();
                    int otherTotal = hfTotals.Where(hf => hf.Count < otherLimit).Sum(hf => hf.Count);
                    int otherKilled = hfKilled.Where(hf => otherRaces.Contains(hf.Race)).Sum(hf => hf.Count);
                    hfTotals.RemoveAll(hf => hf.Count < otherLimit);
                    for (int i = 0; i < hfTotals.Count; i++)
                    {
                        series.First().Points.AddXY(i, hfTotals[i].Count);
                        if (hfKilled.Count(hf => hf.Race == hfTotals[i].Race) > 0)
                            series.Last().Points.AddXY(i, hfTotals[i].Count - hfKilled.First(hf => hf.Race == hfTotals[i].Race).Count);
                        else
                            series.Last().Points.AddXY(i, hfTotals[i].Count);
                        series.Last().Points.Last().AxisLabel = Formatting.MakePopulationPlural(hfTotals[i].Race);
                    }
                    series.First().Points.AddXY(hfTotals.Count(hf => hf.Count >= otherLimit), otherTotal);
                    series.Last().Points.AddXY(hfTotals.Count(hf => hf.Count >= otherLimit), otherTotal - otherKilled);
                    series.Last().Points.Last().AxisLabel = "Other";
                    break;
                //case ChartOption.WorldHFDead:
                //    World.HistoricalFigures.Where(hf => hf.DeathYear > 0).GroupBy(hf => hf.Race).Select(hf => new { Race = hf.Key, Count = hf.Count() }).OrderByDescending(hf => hf.Count).ToList().ForEach(hf => { series.First().Points.AddY(hf.Count); series.First().Points.Last().LegendText = hf.Race; }); break;
                case ChartOption.WorldHFRaces:
                    World.HistoricalFigures.GroupBy(hf => hf.Race).Select(hf => new { Race = hf.Key, Count = hf.Count() }).OrderByDescending(hf => hf.Count).ToList().ForEach(hf => { series.First().Points.AddY(hf.Count); series.First().Points.Last().LegendText = hf.Race; }); break;
                case ChartOption.WorldRegionTypes:
                    World.Regions.GroupBy(region => region.Type).Select(region => new { Type = region.Key, Count = region.Count() }).OrderByDescending(region => region.Count).ToList().ForEach(region => { series.First().Points.AddY(region.Count); series.First().Points.Last().LegendText = region.Type; }); break;
                case ChartOption.WorldSitePopulations:
                    World.SitePopulations.GroupBy(pop => pop.Race).Select(pop => new { Type = pop.Key, Count = pop.Sum(population => population.Count) }).OrderByDescending(pop => pop.Count).ToList().ForEach(pop => { series.First().Points.AddY(pop.Count); series.First().Points.Last().LegendText = pop.Type; }); break;
                case ChartOption.WorldDeaths:
                case ChartOption.OtherDeaths:
                    List<HFDied> hfDeaths = new List<HFDied>();
                    List<Battle.Squad> squads = new List<Battle.Squad>();
                    List<string> deathRaces = new List<string>();
                    if (option == ChartOption.WorldDeaths)
                    {
                        hfDeaths = World.Events.OfType<HFDied>().ToList();
                        //hfDeaths = World.Battles.SelectMany(battle => battle.GetSubEvents().OfType<HFDied>()).ToList();
                        squads = World.Battles.SelectMany(battle => battle.AttackerSquads.Concat(battle.DefenderSquads)).ToList();
                    }
                    else if (FocusObject.GetType() == typeof(Site))
                    {
                        hfDeaths = (FocusObject as Site).Events.OfType<HFDied>().ToList();
                        squads = (FocusObject as Site).Warfare.OfType<Battle>().SelectMany(battle => battle.AttackerSquads.Concat(battle.DefenderSquads)).ToList();
                    }
                    else if (FocusObject.GetType() == typeof(Region))
                    {
                        hfDeaths = (FocusObject as WorldRegion).Events.OfType<HFDied>().ToList();
                        squads = (FocusObject as WorldRegion).Battles.SelectMany(battle => battle.AttackerSquads.Concat(battle.DefenderSquads)).ToList();
                    }
                    else if (FocusObject.GetType() == typeof(Era))
                    {
                        Era era = (FocusObject as Era);
                        hfDeaths = era.Events.OfType<HFDied>().ToList();
                        //hfDeaths = World.Battles.Where(battle => battle.StartYear >= era.StartYear && battle.StartYear <= era.EndYear).SelectMany(battle => battle.GetSubEvents().OfType<HFDied>()).ToList();
                        squads = World.Battles.Where(battle => battle.StartYear >= era.StartYear && battle.StartYear <= era.EndYear).SelectMany(battle => battle.AttackerSquads.Concat(battle.DefenderSquads)).ToList();
                    }


                    hfDeaths.Select(death => death.HistoricalFigure.Race).ToList().ForEach(death => deathRaces.Add(Formatting.MakePopulationPlural(death)));
                    foreach (Battle.Squad squad in squads)
                    {
                        string plural = Formatting.MakePopulationPlural(squad.Race);
                        for (int i = 0; i < squad.Deaths; i++) deathRaces.Add(plural);
                    }
                    deathRaces.GroupBy(race => race).Select(race => new { Type = race.Key, Count = race.Count() }).OrderByDescending(race => race.Count).ToList().ForEach(race => { series.First().Points.AddY(race.Count); series.First().Points.Last().LegendText = race.Type; });
                    break;
                case ChartOption.WorldSiteTypes:
                    World.Sites.GroupBy(region => region.Type).Select(site => new { Type = site.Key, Count = site.Count() }).OrderByDescending(site => site.Count).ToList().ForEach(site => { series.First().Points.AddY(site.Count); series.First().Points.Last().LegendText = site.Type; }); break;
                case ChartOption.WorldOutdoorPopulations:
                    World.OutdoorPopulations.Where(pop => pop.Count != int.MaxValue).ToList().ForEach(pop => { series.First().Points.AddY(pop.Count); series.First().Points.Last().LegendText = pop.Race; }); break;
                case ChartOption.WorldUndergroundPopulations:
                    World.UndergroundPopulations.Where(pop => pop.Count != int.MaxValue).ToList().ForEach(pop => { series.First().Points.AddY(pop.Count); series.First().Points.Last().LegendText = pop.Race; }); break;
                case ChartOption.OtherEventTypes:
                    if (FocusObject is EventCollection)
                        (FocusObject as EventCollection).GetSubEvents().GroupBy(events => events.Type).Select(events => new { Type = events.Key, Count = events.Count() }).OrderByDescending(events => events.Count).ToList().ForEach(events => { series.First().Points.AddY(events.Count); series.First().Points.Last().LegendText = AppHelpers.EventInfo.Single(eventInfo => eventInfo[0] == events.Type)[1]; });
                    else
                        (FocusObject as WorldObject).Events.GroupBy(events => events.Type).Select(events => new { Type = events.Key, Count = events.Count() }).OrderByDescending(events => events.Count).ToList().ForEach(events => { series.First().Points.AddY(events.Count); series.First().Points.Last().LegendText = AppHelpers.EventInfo.Single(eventInfo => eventInfo[0] == events.Type)[1]; }); break;
                case ChartOption.OtherKillsByRace:
                    (FocusObject as HistoricalFigure).NotableKills.GroupBy(death => death.HistoricalFigure.Race).Select(death => new { Race = death.Key, Count = death.Count() }).OrderByDescending(death => death.Count).ToList().ForEach(death => { series.First().Points.AddY(death.Count); series.First().Points.Last().LegendText = death.Race; }); break;
                case ChartOption.OtherEntityPopulations:
                    (FocusObject as Entity).Populations.OrderByDescending(pop => pop.Count).ToList().ForEach(pop => { series.First().Points.AddY(pop.Count); series.First().Points.Last().LegendText = pop.Race; }); break;
                //case ChartOption.OtherDeaths:
                //    (FocusObject as WorldRegion).Events.OfType<HFDied>().GroupBy(death => death.HistoricalFigure.Race).Select(death => new { Race = death.Key, Count = death.Count() }).OrderByDescending(death => death.Count).ToList().ForEach(death => { series.First().Points.AddY(death.Count); series.First().Points.Last().LegendText = death.Race; }); break;
                //case ChartOption.OtherSiteDeaths:
                //    (FocusObject as Site).Events.OfType<HFDied>().GroupBy(death => death.HistoricalFigure.Race).Select(death => new { Race = death.Key, Count = death.Count() }).OrderByDescending(death => death.Count).ToList().ForEach(death => { series.First().Points.AddY(death.Count); series.First().Points.Last().LegendText = death.Race; }); break;
                case ChartOption.OtherSitePopulations:
                    (FocusObject as Site).Populations.OrderByDescending(pop => pop.Count).ToList().ForEach(pop => { series.First().Points.AddY(pop.Count); series.First().Points.Last().LegendText = pop.Race; }); break;
                case ChartOption.OtherWarLosses:
                    List<War> warsList = new List<War>();
                    if (FocusObject.GetType() == typeof(War)) warsList.Add(FocusObject as War);
                    else if (FocusObject.GetType() == typeof(Entity))
                        foreach (War addWar in (FocusObject as Entity).Wars) warsList.Add(addWar);
                    List<Entity> entities = warsList.SelectMany(war => new List<Entity> { war.Attacker, war.Defender }).ToList();
                    entities = entities.GroupBy(entity => entity).Select(entity => entity.Key).ToList();
                    entities.RemoveAll(entity => entity.Parent != null && entities.Contains(entity.Parent));
                    foreach (Entity entity in entities)
                    {
                        List<Battle> battles1 = warsList.SelectMany(wars1 => wars1.Collections.OfType<Battle>()).ToList();
                        List<HFDied> hfDeathsList = new List<HFDied>();
                        List<Battle.Squad> squadsList = new List<Battle.Squad>();
                        List<string> deathRacesList = new List<string>();
                        hfDeathsList = battles1.Where(battle => battle.Attacker == entity || battle.Attacker.Parent == entity).SelectMany(battle => battle.GetSubEvents().OfType<HFDied>().Where(death => battle.NotableAttackers.Contains(death.HistoricalFigure))).ToList();
                        hfDeathsList = hfDeathsList.Concat(battles1.Where(battle => battle.Defender == entity || battle.Defender.Parent == entity).SelectMany(battle => battle.GetSubEvents().OfType<HFDied>().Where(death => battle.NotableDefenders.Contains(death.HistoricalFigure))).ToList()).ToList();
                        squadsList = battles1.Where(battle => battle.Attacker == entity || battle.Attacker.Parent == entity).SelectMany(battle => battle.AttackerSquads).ToList();
                        squadsList = squadsList.Concat(battles1.Where(battle => battle.Defender == entity || battle.Defender.Parent == entity).SelectMany(battle => battle.DefenderSquads).ToList()).ToList();
                        hfDeathsList.Select(death => death.HistoricalFigure.Race).ToList().ForEach(death => deathRacesList.Add(Formatting.MakePopulationPlural(death)));

                        //squadsList.GroupBy(squad => squad.Race).Select(squad => new { Race = squad.Key, Count = squad.Sum(race => race.Deaths) });
                        foreach (Battle.Squad squad in squadsList)
                        {
                            string plural = Formatting.MakePopulationPlural(squad.Race);
                            for (int i = 0; i < squad.Deaths; i++) deathRacesList.Add(plural);
                        }

                        var deathsList = deathRacesList.GroupBy(race => race).Select(race => new { Type = race.Key, Count = race.Count() }).OrderByDescending(race => race.Count).ToList();
                        int deathOtherLimit = Convert.ToInt32(deathsList.Sum(death => death.Count) * 0.02);
                        deathsList.Where(death => death.Count >= deathOtherLimit).ToList().ForEach(race => { series.First().Points.AddY(race.Count); series.First().Points.Last().LegendText = race.Type; series.First().Points.Last().Color = entity.LineColor; series.First().Points.Last().BorderColor = Color.Gray; series.First().Points.Last().LabelBackColor = Color.FromArgb(127, entity.LineColor); });
                        int deathOtherCount = deathsList.Where(death => death.Count < deathOtherLimit).Sum(death => death.Count);
                        if (deathOtherCount > 0)
                        {
                            series.First().Points.AddY(deathOtherCount);
                            series.First().Points.Last().LegendText = "Other";
                            series.First().Points.Last().Color = entity.LineColor;
                            series.First().Points.Last().BorderColor = Color.Gray;
                            series.First().Points.Last().LabelBackColor = Color.FromArgb(127, entity.LineColor);
                        }
                    }
                    break;
                case ChartOption.OtherBattleRemaining:
                    Battle battle1 = (FocusObject as Battle);
                    List<string> attackers = battle1.NotableAttackers.Select(hf => Formatting.MakePopulationPlural(hf.Race)).ToList();
                    List<string> attackersKilled = battle1.NotableAttackers.Where(hf => battle1.GetSubEvents().OfType<HFDied>().Count(death => death.HistoricalFigure == hf) > 0).Select(hf => Formatting.MakePopulationPlural(hf.Race)).ToList();
                    foreach (Battle.Squad squad in battle1.AttackerSquads)
                    {
                        string plural = Formatting.MakePopulationPlural(squad.Race);
                        for (int i = 0; i < squad.Numbers; i++) attackers.Add(plural);
                        for (int i = 0; i < squad.Deaths; i++) attackersKilled.Add(plural);
                    }
                    var attackerTotals = attackers.GroupBy(attacker => attacker).Select(result => new { Type = result.Key, Count = result.Count() }).OrderByDescending(attacker => attacker.Count).ToList();
                    var attackerTotalsKilled = attackersKilled.GroupBy(attacker => attacker).Select(result => new { Type = result.Key, Count = result.Count() }).OrderBy(attacker => attackerTotals.IndexOf(attackerTotals.First(race => race.Type == attacker.Type))).ToList();

                    for (int i = 0; i < attackerTotals.Count; i++)
                    {
                        series.First().Points.AddXY(i, attackerTotals[i].Count);
                        if (attackerTotalsKilled.Count(race => race.Type == attackerTotals[i].Type) > 0)
                            series.Last().Points.AddXY(i, attackerTotals[i].Count - attackerTotalsKilled.First(race => race.Type == attackerTotals[i].Type).Count);
                        else
                            series.Last().Points.AddXY(i, attackerTotals[i].Count);
                        series.First().Color = Color.LightGray;
                        series.Last().Points.Last().Color = battle1.Attacker.LineColor;
                        series.Last().Points.Last().AxisLabel = attackerTotals[i].Type;
                        //series.Last().Points.Last().LabelBackColor = Color.FromArgb(127, battle1.Attacker.LineColor);
                    }

                    series.First().Points.AddXY(attackerTotals.Count, 0);
                    series.First().Points.Last().IsEmpty = true;
                    series.First().Points.Last().Label = "";
                    series.Last().Points.AddXY(attackerTotals.Count, 0);
                    series.Last().Points.Last().IsEmpty = true;
                    series.Last().Points.Last().Label = "";
                    series.Last().Points.Last().AxisLabel = "VS.";

                    List<string> defenders = battle1.NotableDefenders.Select(hf => Formatting.MakePopulationPlural(hf.Race)).ToList();
                    List<string> defendersKilled = battle1.NotableDefenders.Where(hf => battle1.GetSubEvents().OfType<HFDied>().Count(death => death.HistoricalFigure == hf) > 0).Select(hf => Formatting.MakePopulationPlural(hf.Race)).ToList();
                    foreach (Battle.Squad squad in battle1.DefenderSquads)
                    {
                        string plural = Formatting.MakePopulationPlural(squad.Race);
                        for (int i = 0; i < squad.Numbers; i++) defenders.Add(plural);
                        for (int i = 0; i < squad.Deaths; i++) defendersKilled.Add(plural);
                    }
                    var defenderTotals = defenders.GroupBy(defender => defender).Select(result => new { Type = result.Key, Count = result.Count() }).OrderByDescending(defender => defender.Count).ToList();
                    var defenderTotalsKilled = defendersKilled.GroupBy(defender => defender).Select(result => new { Type = result.Key, Count = result.Count() }).OrderBy(defender => defenderTotals.IndexOf(defenderTotals.First(race => race.Type == defender.Type))).ToList();

                    for (int i = 0; i < defenderTotals.Count; i++)
                    {
                        series.First().Points.AddXY(i + attackerTotals.Count + 1, defenderTotals[i].Count);
                        if (defenderTotalsKilled.Count(race => race.Type == defenderTotals[i].Type) > 0)
                            series.Last().Points.AddXY(i + attackerTotals.Count + 1, defenderTotals[i].Count - defenderTotalsKilled.First(race => race.Type == defenderTotals[i].Type).Count);
                        else
                            series.Last().Points.AddXY(i + attackerTotals.Count + 1, defenderTotals[i].Count);
                        series.First().Color = Color.LightGray;
                        series.Last().Points.Last().Color = battle1.Defender.LineColor;
                        series.Last().Points.Last().AxisLabel = defenderTotals[i].Type;
                        //series.Last().Points.Last().LabelBackColor = Color.FromArgb(127, battle1.Defender.LineColor);
                    }
                    break;

            }

            return series;
        }

        private void RemoveSeries(ChartOption option)
        {
            while (SeriesOptions.Count(option1 => option1 == option) > 0)
            {
                int i = SeriesOptions.IndexOf(option);
                SeriesOptions.Remove(option);
                Series.RemoveAt(i);
                DwarfChart.Series.RemoveAt(i);
            }
        }

        private void SetupMenu()
        {
            MenuStrip menu = new MenuStrip();
            ToolStripMenuItem timeline = new ToolStripMenuItem();
            TimelineMenu = timeline;
            ToolStripMenuItem other = new ToolStripMenuItem();
            ToolStripMenuItem world = new ToolStripMenuItem();

            timeline.Text = "Timeline";
            other.Text = "Other";
            world.Text = "World";


            if (((FocusObject is WorldObject) && (FocusObject as WorldObject).Events.Count > 0)
                || (FocusObject.GetType() == typeof(War) && (FocusObject as EventCollection).GetSubEvents().Count > 0))
            {
                timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineEvents});
                timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineEventsFiltered});
                other.DropDownItems.Add(new ChartMenuItem(this) { Option = ChartOption.OtherEventTypes});
            }

            if (FocusObject.GetType() == typeof(Era))
            {
                timeline.DropDownItems.Add(new ChartMenuItem(this) { Option = ChartOption.TimelineAliveHFs});
                timeline.DropDownItems.Add(new ChartMenuItem(this) { Option = ChartOption.TimeLineAliveHFSpecific});
                timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineActiveSites});
                timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineActiveSitesByRace});
                timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineActiveWars});
                timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineBattles});
                timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineBattleDeaths});
                timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineBeastAttacks});
                other.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.OtherDeaths});
            }
            else if (FocusObject.GetType() == typeof(HistoricalFigure))
            {
                HistoricalFigure hf = (FocusObject as HistoricalFigure);
                if (hf.NotableKills.Count > 0) other.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.OtherKillsByRace});
                if (hf.Battles.Count > 0) timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineBattles});
                if (hf.BeastAttacks != null) timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineBeastAttacks});
            }
            else if (FocusObject.GetType() == typeof(Entity))
            {
                Entity entity = (FocusObject as Entity);
                if (entity.SiteHistory.Count > 0) timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineActiveSites});
                if (entity.Populations.Count > 0) other.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.OtherEntityPopulations});
                if (entity.Wars.Count > 0)
                {
                    timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineActiveWars});
                    timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineBattles});
                    timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineBattleDeaths});
                    other.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.OtherWarLosses});
                }
            }
            else if (FocusObject.GetType() == typeof(Site))
            {
                Site site = (FocusObject as Site);
                if (site.BeastAttacks.Count > 0) timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineBeastAttacks});
                if (site.Warfare.OfType<Battle>().Any())
                {
                    timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineBattles});
                    timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineBattleDeaths});
                }
                if (site.Populations.Count > 0) other.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.OtherSitePopulations});
                if (site.Events.OfType<HFDied>().Any() || site.Warfare.Count > 0) other.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.OtherDeaths});
            }
            else if (FocusObject.GetType() == typeof(LegendsViewer.Legends.WorldRegion))
            {
                LegendsViewer.Legends.WorldRegion region = (FocusObject as LegendsViewer.Legends.WorldRegion);
                if (region.Events.OfType<HFDied>().Any()) other.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.OtherDeaths});
                if (region.Battles.Count > 0)
                {
                    timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineBattles});
                    timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineBattleDeaths});
                }
            }
            else if (FocusObject.GetType() == typeof(War))
            {
                timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineBattles});
                timeline.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.TimelineBattleDeaths});
                other.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.OtherWarLosses});
            }
            else if (FocusObject.GetType() == typeof(Battle))
                other.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.OtherBattleRemaining});

            world.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.WorldHFRaces});
            world.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.WorldHFAlive});
            world.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.WorldHFRemaining});
            //World.DropDownItems.Insert(new ChartMenuItem(this) {Option = ChartOption.WorldHFDead});
            world.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.WorldRegionTypes});
            world.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.WorldSitePopulations});
            world.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.WorldSiteTypes});
            world.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.WorldDeaths});
            world.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.WorldOutdoorPopulations});
            world.DropDownItems.Add(new ChartMenuItem(this) {Option = ChartOption.WorldUndergroundPopulations});


            menu.Items.Add(timeline);
            menu.Items.Add(other);
            menu.Items.Add(world);
            Controls.Add(menu);
        }

        public void MenuClick(ChartMenuItem sender, ChartOption option)
        {
            switch (option)
            {
                case ChartOption.TimelineActiveSites:
                case ChartOption.TimelineActiveSitesByRace:
                case ChartOption.TimelineActiveWars:
                case ChartOption.TimelineAliveHFs:
                case ChartOption.TimeLineAliveHFSpecific:
                case ChartOption.TimelineBattles:
                case ChartOption.TimelineBeastAttacks:
                case ChartOption.TimelineEvents:
                case ChartOption.TimelineEventsFiltered:
                case ChartOption.TimelineBattleDeaths:
                    if (option == ChartOption.TimeLineAliveHFSpecific && !SeriesOptions.Contains(ChartOption.TimeLineAliveHFSpecific))
                    {
                        dlgHF selectHFRace = new dlgHF(World);
                        selectHFRace.ShowDialog();
                        if (selectHFRace.SelectedRace == "") return;
                        AliveHFRace = selectHFRace.SelectedRace;
                    }
                    if (OtherChart) while (SeriesOptions.Count > 0) RemoveSeries(SeriesOptions[0]);
                    if (SeriesOptions.Contains(option)) RemoveSeries(option);
                    else SeriesOptions.Add(option);
                    //sender.Checked = !sender.Checked;
                    OtherChart = false;
                    break;
                default:
                    while (SeriesOptions.Count > 0) RemoveSeries(SeriesOptions[0]);
                    SeriesOptions.Add(option);
                    OtherChart = true;
                    break;
            }
            GenerateSeries();
        }
    }
}