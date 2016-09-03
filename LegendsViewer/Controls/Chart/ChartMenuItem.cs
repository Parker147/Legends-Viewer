using System;
using System.Windows.Forms;

namespace LegendsViewer
{
    public class ChartMenuItem : ToolStripMenuItem
    {
        private ChartOption chartOption;

        public ChartOption Option
        {
            get
            {
                return chartOption;
            }
            set
            {
                chartOption= value;
                switch (chartOption)
                {
                    case ChartOption.TimelineEvents: Text = "Events"; break;
                    case ChartOption.TimelineEventsFiltered: Text = "Events (Filtered)"; break;
                    case ChartOption.TimelineBattleDeaths: Text = "Battle Deaths"; break;
                    case ChartOption.TimelineActiveSites: Text = "Active Sites"; break;
                    case ChartOption.TimelineActiveSitesByRace: Text = "Active Sites by Race"; break;
                    case ChartOption.TimelineAliveHFs: Text = "Alive Historical Figures"; break;
                    case ChartOption.TimeLineAliveHFSpecific: Text = "Alive Historical Figures..."; break;
                    case ChartOption.TimelineActiveWars: Text = "Active Wars"; break;
                    case ChartOption.TimelineBattles: Text = "Battles"; break;
                    case ChartOption.TimelineBeastAttacks: Text = "Beast Attacks"; break;
                    case ChartOption.OtherEventTypes: Text = "Event Types"; break;
                    case ChartOption.OtherKillsByRace: Text = "Kills"; break;
                    case ChartOption.OtherEntityPopulations: Text = "Site Populations"; break;
                    case ChartOption.OtherDeaths: Text = "Deaths"; break; ;
                    case ChartOption.OtherSitePopulations: Text = "Populations"; break;
                    case ChartOption.OtherWarLosses: Text = "War Losses"; break;
                    case ChartOption.OtherBattleRemaining: Text = "Remaining Forces"; break;
                    case ChartOption.WorldHFRaces: Text = "Historical Figures"; break;
                    case ChartOption.WorldHFAlive: Text = "Historical Figures - Alive"; break;
                    case ChartOption.WorldHFRemaining: Text = "Historical Figures - Remaining"; break;
                    case ChartOption.WorldSitePopulations: Text = "Site Populations"; break;
                    case ChartOption.WorldDeaths: Text = "Deaths"; break;
                    case ChartOption.WorldSiteTypes: Text = "Sites"; break;
                    case ChartOption.WorldRegionTypes: Text = "Regions"; break;
                    case ChartOption.WorldOutdoorPopulations: Text = "Outdoor Populations"; break;
                    case ChartOption.WorldUndergroundPopulations: Text = "Underground Populations"; break;
                }

            }
        }

        ChartPanel Chart;
        public ChartMenuItem(ChartPanel chart)
        {
            Chart = chart;
        }

        protected override void OnClick(EventArgs e)
        {
            Chart.MenuClick(this, Option);
        }
    }
}