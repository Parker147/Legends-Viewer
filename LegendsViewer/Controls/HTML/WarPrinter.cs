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

            HTML.AppendLine("<h1>" + GetTitle() + "</h1></br>");
            HTML.AppendLine("Started " + War.GetYearTime().ToLower() + "and ");
            if (War.EndYear == -1)
                HTML.AppendLine("is still ongoing.");
            else
                HTML.AppendLine("ended " + War.GetYearTime(false).ToLower().Substring(0, War.GetYearTime(false).Length - 1) + ". ");
            HTML.AppendLine(War.Name + " was waged by " + War.Attacker.PrintEntity() + " on " + War.Defender.PrintEntity() + ".<br/>");
            HTML.AppendLine("</br></br>");

            List<System.Drawing.Bitmap> maps = MapPanel.CreateBitmaps(World, War);

            HTML.AppendLine("<table>");
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
                HTML.AppendLine("<tr>");
                HTML.AppendLine("<th align=right>#</th>");
                HTML.AppendLine("<th align=right>Year</th>");
                HTML.AppendLine("<th>Battle</th>");
                HTML.AppendLine("<th>Victor</th>");
                HTML.AppendLine("<th align=right>" + StringToImageHTML(War.Attacker.SmallIdenticonString) + "</th>");
                HTML.AppendLine("<th>/</th>");
                HTML.AppendLine("<th align=left>" + StringToImageHTML(War.Defender.SmallIdenticonString) + "</th>");
                HTML.AppendLine("</tr>");
                foreach (EventCollection warfare in War.Collections.Where(battle => !World.FilterBattles || battle.Notable))
                {
                    HTML.AppendLine("<tr>");
                    HTML.AppendLine("<td align=right>" + warfareCount + ".</td>");
                    HTML.AppendLine("<td align=right>" + warfare.StartYear + "</td>");
                    string warfareString = warfare.ToLink();
                    if (warfareString.Contains(" as a result of"))
                        warfareString = warfareString.Insert(warfareString.IndexOf(" as a result of"), "</br>");
                    HTML.Append("<td>" + warfareString + "</td>");
                    if (warfare.GetType() == typeof(Battle))
                    {
                        Battle battle = warfare as Battle;
                        HTML.AppendLine("<td>"+ (battle.Victor == War.Attacker? battle.Attacker.PrintEntity(): battle.Defender.PrintEntity())+ "</td>");
                        HTML.AppendLine("<td align=right>" + (battle.Attacker == War.Attacker ? battle.DefenderDeathCount : battle.AttackerDeathCount) + "</td>");
                        HTML.AppendLine("<td>/</td>");
                        HTML.AppendLine("<td align=left>" + (battle.Defender == War.Attacker ? battle.DefenderDeathCount : battle.AttackerDeathCount) + "</td>");
                    }
                    else if (warfare.GetType() == typeof(SiteConquered))
                    {
                        HTML.AppendLine("<td align=right>" + (warfare as SiteConquered).Attacker.PrintEntity() + "</td>");
                    }

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

            PrintEventLog(War.GetSubEvents(), War.Filters, War);

            return HTML.ToString();
        }
    }
}
