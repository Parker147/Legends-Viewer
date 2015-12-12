using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls
{
    class WarPrinter : HTMLPrinter
    {
        War War;
        World World;

        public WarPrinter(War war, World world)
        {
            War = war;
            World = world;
        }

        public override string GetTitle()
        {
            return War.Name;
        }

        public override string Print()
        {
            HTML = new StringBuilder();

            HTML.AppendLine("<b>" + War.Name + " was waged by " + War.Attacker.PrintEntity() + " on " + War.Defender.PrintEntity() + "</b><br/>");
            HTML.AppendLine("Started " + War.GetYearTime().ToLower() + "and ");
            if (War.EndYear == -1)
                HTML.AppendLine("is still ongoing.");
            else
                HTML.AppendLine("ended " + War.GetYearTime(false).ToLower().Substring(0, War.GetYearTime(false).Length - 1) + ". ");
            HTML.AppendLine("</br></br>");

            List<System.Drawing.Bitmap> maps = MapPanel.CreateBitmaps(World, War);
            HTML.AppendLine("<table border=\"0\" width=\"" + (maps[0].Width + maps[1].Width + 10) + "\">");
            HTML.AppendLine("<tr>");
            HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[0]), LinkOption.LoadMap) + "</td>");
            HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[1]), LinkOption.LoadMap) + "</td>");
            HTML.AppendLine("</tr></table></br>");

            War.Collections.OfType<Battle>().Sum(battle => battle.Collection.OfType<HFDied>().Where(death => battle.NotableAttackers.Contains(death.HistoricalFigure)).Count());
            HTML.AppendLine("<b>" + War.Attacker.PrintEntity() + " (Attacker)</b>");
            HTML.AppendLine("<ul>");
            HTML.AppendLine("<li>Kills: " + (War.Collections.OfType<Battle>().Where(battle => battle.Attacker == War.Attacker).Sum(battle => battle.DefenderDeathCount) + War.Collections.OfType<Battle>().Where(battle => battle.Defender == War.Attacker).Sum(battle => battle.AttackerDeathCount)) + "</br>");
            HTML.AppendLine("<li>Battle Victories: " + War.AttackerVictories.OfType<Battle>().Count());
            HTML.AppendLine("<li>Conquerings: " + War.AttackerVictories.OfType<SiteConquered>().Count());
            HTML.AppendLine(" (" + War.AttackerVictories.OfType<SiteConquered>().Where(conquering => conquering.ConquerType == SiteConqueredType.Pillaging).Count() + " Pillagings, ");
            HTML.AppendLine(War.AttackerVictories.OfType<SiteConquered>().Where(conquering => conquering.ConquerType == SiteConqueredType.Destruction).Count() + " Destructions, ");
            HTML.AppendLine(War.AttackerVictories.OfType<SiteConquered>().Where(conquering => conquering.ConquerType == SiteConqueredType.Conquest).Count() + " Conquests)");
            HTML.AppendLine("</ul>");

            HTML.AppendLine("<b>" + War.Defender.PrintEntity() + " (Defender)</b>");
            HTML.AppendLine("<ul>");
            HTML.AppendLine("<li>Kills: " + (War.Collections.OfType<Battle>().Where(battle => battle.Attacker == War.Defender).Sum(battle => battle.DefenderDeathCount) + War.Collections.OfType<Battle>().Where(battle => battle.Defender == War.Defender).Sum(battle => battle.AttackerDeathCount)) + "</br>");
            HTML.AppendLine("<li>Battle Victories: " + War.DefenderVictories.OfType<Battle>().Count());
            HTML.AppendLine("<li>Conquerings: " + War.DefenderVictories.OfType<SiteConquered>().Count());
            HTML.AppendLine(" (" + War.DefenderVictories.OfType<SiteConquered>().Where(conquering => conquering.ConquerType == SiteConqueredType.Pillaging).Count() + " Pillagings, ");
            HTML.AppendLine(War.DefenderVictories.OfType<SiteConquered>().Where(conquering => conquering.ConquerType == SiteConqueredType.Destruction).Count() + " Destructions, ");
            HTML.AppendLine(War.DefenderVictories.OfType<SiteConquered>().Where(conquering => conquering.ConquerType == SiteConqueredType.Conquest).Count() + " Conquests)");
            HTML.AppendLine("</ul>");

            if (War.Collections.Count(battle => !World.FilterBattles || battle.Notable) > 0)
            {
                int warfareCount = 1;
                HTML.AppendLine("<b>Warfare</b> " + MakeLink("[Load]", LinkOption.LoadWarBattles));
                if (World.FilterBattles) HTML.Append(" (Notable)");
                HTML.Append("</br>");
                HTML.AppendLine("<table>");
                HTML.AppendLine("<tr><td colspan=6></td>");
                HTML.AppendLine("<td align=right>" + StringToImageHTML(War.Attacker.SmallIdenticonString) + "</td>");
                HTML.AppendLine("<td>/</td>");
                HTML.AppendLine("<td align=left>" + StringToImageHTML(War.Defender.SmallIdenticonString) + "</td>");
                HTML.AppendLine("</tr>");
                foreach (EventCollection warfare in War.Collections.Where(battle => !World.FilterBattles || battle.Notable))
                {
                    HTML.AppendLine("<tr>");
                    HTML.AppendLine("<td width=\"20\" align=\"right\">" + warfareCount + ".<td width=\"10\"></td><td>" + warfare.StartYear + "</td>");
                    string warfareString = warfare.ToLink();
                    if (warfareString.Contains(" as a result of"))
                        warfareString = warfareString.Insert(warfareString.IndexOf(" as a result of"), "</br>");
                    HTML.Append("<td>" + warfareString + "</td>");
                    if (warfare.GetType() == typeof(Battle))
                    {
                        Battle battle = warfare as Battle;
                        HTML.Append("<td>Victor: </td><td align=\"right\">");
                        if (battle.Victor == War.Attacker) HTML.Append(battle.Attacker.PrintEntity());
                        else HTML.Append(battle.Defender.PrintEntity());

                        if (battle.Attacker == War.Attacker) HTML.AppendLine("<td align=right>" + battle.DefenderDeathCount + "</td>");
                        else HTML.AppendLine("<td align=right>" + battle.AttackerDeathCount + "</td>");

                        HTML.AppendLine("<td>/</td>");

                        if (battle.Defender == War.Attacker) HTML.AppendLine("<td align=left>" + battle.DefenderDeathCount + "</td>");
                        else HTML.AppendLine("<td align=left>" + battle.AttackerDeathCount + "</td>");

                        HTML.AppendLine("</td>");
                    }
                    if (warfare.GetType() == typeof(SiteConquered))
                        HTML.AppendLine("<td>Victor: </td><td align=right>" + (warfare as SiteConquered).Attacker.PrintEntity() + "</td>");

                    HTML.AppendLine("</tr>");
                    warfareCount++;
                }
                HTML.AppendLine("</table></br>");
            }

            if (World.FilterBattles && War.Collections.Count(battle => !battle.Notable) > 0)
            {
                HTML.AppendLine("<b>Warfare</b> (Unnotable)</br>");
                HTML.AppendLine("<ul>");
                HTML.AppendLine("<li>Battles: " + War.Collections.OfType<Battle>().Where(battle => !battle.Notable).Count());
                HTML.AppendLine("<li>Pillagings: " + War.Collections.OfType<SiteConquered>().Where(conquering => conquering.ConquerType == SiteConqueredType.Pillaging).Count());
                HTML.AppendLine("</ul>");
            }

            HTML.AppendLine("<b>Event Log</b> " + MakeLink(Font("[Chart]", "Maroon"), LinkOption.LoadChart) + LineBreak);
            foreach (WorldEvent printEvent in War.GetSubEvents())
                if (!War.Filters.Contains(printEvent.Type))
                    HTML.AppendLine(printEvent.Print(true, War) + "<br/><br/>");
            return HTML.ToString();
        }
    }
}
