using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls
{
    class EraPrinter : HTMLPrinter
    {
        Era Era;

        public EraPrinter(Era era)
        {
            Era = era;
        }

        public override string GetTitle()
        {
            if (Era.Name != "") return Era.Name;
            else return "(" + Era.StartYear + " - " + Era.EndYear + ")";
        }

        public override string Print()
        {
            HTML = new StringBuilder();

            HTML.AppendLine("<b>");
            if (Era.Name != "")
                HTML.Append(Era.Name + ". ");
            HTML.Append("(" + Era.StartYear + " - " + Era.EndYear + ")</b></br></br>");



            if (Era.Wars.Count > 0)
            {
                int warCount = 1;
                HTML.AppendLine("<b>Wars</b></br>");
                HTML.AppendLine("<table>");
                foreach (War war in Era.Wars)
                {
                    HTML.AppendLine("<tr>");
                    HTML.AppendLine("<td width=\"20\" align=\"right\">" + warCount + ".</td><td width=\"10\"></td><td>");
                    if (war.StartYear < Era.StartYear) HTML.Append("<font color=\"Blue\">" + war.StartYear + "</font> - ");
                    else HTML.Append(war.StartYear + " - ");
                    if (war.EndYear == -1) HTML.Append("<font color=\"Blue\">Present</font>");
                    else if (war.EndYear > Era.EndYear) HTML.Append("<font color=\"Blue\">" + war.EndYear + "</font>");
                    else HTML.Append(war.EndYear);
                    HTML.Append("</td><td>" + war.ToLink() + "</td><td align=\"right\">" + war.Attacker.PrintEntity() + "</td><td>against</td><td align=\"right\">" + war.Defender.PrintEntity() + "</td>");

                    int attackerVictories = 0, defenderVictories = 0, attackerConquerings = 0, defenderConquerings = 0, attackerKills, defenderKills;
                    attackerVictories = war.AttackerVictories.OfType<Battle>().Count();
                    defenderVictories = war.DefenderVictories.OfType<Battle>().Count();
                    attackerConquerings = war.AttackerVictories.OfType<SiteConquered>().Count(conquering => conquering.ConquerType != SiteConqueredType.Pillaging);
                    defenderConquerings = war.DefenderVictories.OfType<SiteConquered>().Count(conquering => conquering.ConquerType != SiteConqueredType.Pillaging);
                    attackerKills = war.Collections.OfType<Battle>().Where(battle => war.Attacker == battle.Attacker).Sum(battle => battle.DefenderDeathCount) + war.Collections.OfType<Battle>().Where(battle => war.Attacker == battle.Defender).Sum(battle => battle.AttackerDeathCount);
                    defenderKills = war.Collections.OfType<Battle>().Where(battle => war.Defender == battle.Attacker).Sum(battle => battle.DefenderDeathCount) + war.Collections.OfType<Battle>().Where(battle => war.Defender == battle.Defender).Sum(battle => battle.AttackerDeathCount);

                    HTML.AppendLine("<td>(A/D)</td>");
                    HTML.AppendLine("<td>Battles:</td><td align=right>" + attackerVictories + "</td><td>/</td><td>" + defenderVictories + "</td>");
                    HTML.AppendLine("<td>Sites:</td><td align=right>" + attackerConquerings + "</td><td>/</td><td>" + defenderConquerings + "</td>");
                    HTML.AppendLine("<td>Kills:</td><td align=right>" + attackerKills + "</td><td>/</td><td>" + defenderKills + "</td>");
                    HTML.AppendLine("</tr>");

                    HTML.AppendLine("</tr>");
                    warCount++;

                }
                HTML.AppendLine("</table></br>");
            }


            HTML.AppendLine("<b>Event Log</b> " + MakeLink(Font("[Chart]", "Maroon"), LinkOption.LoadChart) + LineBreak);
            //List<WorldEvent> eraEvents = World.Events.Where(eraEvent => eraEvent.Year >= StartYear && eraEvent.Year <= EndYear).ToList();
            foreach (WorldEvent eraEvent in Era.Events)
                if (!Era.Filters.Contains(eraEvent.Type))
                    HTML.AppendLine(eraEvent.Print() + "</br></br>");

            return HTML.ToString();
        }

    }
}
