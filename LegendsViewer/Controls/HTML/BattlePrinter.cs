using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Legends;
using LegendsViewer.Legends.EventCollections;
using LegendsViewer.Legends.Events;

namespace LegendsViewer.Controls
{
    class BattlePrinter : HTMLPrinter
    {
        Battle Battle;
        World World;

        public BattlePrinter(Battle battle, World world)
        {
            Battle = battle;
            World = world;
        }

        public override string GetTitle()
        {
            return Battle.Name;
        }

        public override string Print()
        {
            HTML = new StringBuilder();

            HTML.AppendLine("<h1>" + GetTitle() + "</h1></br>");

            string battleDescription = Battle.GetYearTime() + Battle.ToLink(false);
            if (Battle.ParentCollection != null)
            {
                battleDescription += " occured as part of " + Battle.ParentCollection.ToLink();
                battleDescription += " waged by " + (Battle.ParentCollection as War).Attacker.PrintEntity();
                battleDescription += " on " + (Battle.ParentCollection as War).Defender.PrintEntity();
            }
            if (Battle.Site != null)
            {
                battleDescription += " at " + Battle.Site.ToLink();
            }
            if (Battle.Region != null)
            {
                battleDescription += " in " + Battle.Region.ToLink();
            }
            HTML.AppendLine(battleDescription+".</br>");

            if (Battle.Conquering != null)
                HTML.AppendLine("<b>Outcome:</b> " + Battle.Conquering.ToLink(true, Battle) + "</br>");
            HTML.AppendLine("</br>");

            List<System.Drawing.Bitmap> maps = MapPanel.CreateBitmaps(World, Battle);
            //HTML.AppendLine("<table border=\"0\" width=\"" + (maps[0].Width + maps[1].Width + 10) + "\">");
            HTML.AppendLine("<table>");
            HTML.AppendLine("<tr>");
            HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[0]), LinkOption.LoadMap) + "</td>");
            HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[1]), LinkOption.LoadMap) + "</td>");
            HTML.AppendLine("</tr></table></br>");

            HTML.AppendLine("<center>" + MakeLink(Font("[Chart]", "Maroon"), LinkOption.LoadChart) + "</center>" + LineBreak);

            HTML.AppendLine("<table>");
            HTML.AppendLine("<tr><td valign=\"top\">");
            if (Battle.Victor == Battle.Attacker) HTML.AppendLine("<center><b><u>Victor</u></b></center></br>");
            else HTML.AppendLine("</br></br>");
            HTML.AppendLine("<b>" + Battle.Attacker.PrintEntity() + " (Attacker) " + (Battle.NotableAttackers.Count + Battle.AttackerSquads.Sum(squad => squad.Numbers)) + " Members, " + Battle.AttackerDeathCount + " Losses</b> " + MakeLink("[Load]", LinkOption.LoadBattleAttackers) + LineBreak);
            HTML.AppendLine("<ul>");
            var squadRaces = from squad in Battle.AttackerSquads
                             group squad by squad.Race into squads
                             select new { Race = squads.Key, Numbers = squads.Sum(squad => squad.Numbers), Deaths = squads.Sum(squad => squad.Deaths) };
            squadRaces = squadRaces.OrderByDescending(squad => squad.Numbers);

            foreach (var squadRace in squadRaces)
            {
                HTML.AppendLine("<li>" + squadRace.Numbers + " " + Formatting.MakePopulationPlural(squadRace.Race));
                HTML.Append(", " + squadRace.Deaths + " Losses</br>");
            }
            foreach (HistoricalFigure attacker in Battle.NotableAttackers)
            {
                HTML.AppendLine("<li>" + attacker.ToLink());
                if (Battle.Collection.OfType<FieldBattle>().Any(fieldBattle => fieldBattle.AttackerGeneral == attacker) ||
                    Battle.Collection.OfType<AttackedSite>().Any(attack => attack.AttackerGeneral == attacker))
                    HTML.Append(" <b>(Led the Attack)</b> ");

                if (Battle.GetSubEvents().OfType<HFDied>().Any(death => death.HistoricalFigure == attacker))
                    HTML.Append(" (Died) ");
            }
            HTML.AppendLine("</ul>");
            HTML.AppendLine("</td><td width=\"20\"></td><td valign=\"top\">");


            if (Battle.Victor == Battle.Defender) HTML.AppendLine("<center><b><u>Victor</u></b></center></br>");
            else HTML.AppendLine("</br></br>");
            HTML.AppendLine("<b>" + Battle.Defender.PrintEntity() + " (Defender) " + (Battle.NotableDefenders.Count + Battle.DefenderSquads.Sum(squad => squad.Numbers)) + " Members, " + Battle.DefenderDeathCount + " Losses</b> " + MakeLink("[Load]", LinkOption.LoadBattleDefenders) + LineBreak);
            HTML.AppendLine("<ul>");
            squadRaces = from squad in Battle.DefenderSquads
                         group squad by squad.Race into squads
                         select new { Race = squads.Key, Numbers = squads.Sum(squad => squad.Numbers), Deaths = squads.Sum(squad => squad.Deaths) };
            squadRaces = squadRaces.OrderByDescending(squad => squad.Numbers);

            foreach (var squadRace in squadRaces)
            {
                HTML.AppendLine("<li>" + squadRace.Numbers + " " + Formatting.MakePopulationPlural(squadRace.Race));
                HTML.Append(", " + squadRace.Deaths + " Losses</br>");
            }
            foreach (HistoricalFigure defender in Battle.NotableDefenders)
            {
                HTML.AppendLine("<li>" + defender.ToLink());
                if (Battle.Collection.OfType<FieldBattle>().Any(fieldBattle => fieldBattle.DefenderGeneral == defender) ||
                    Battle.Collection.OfType<AttackedSite>().Any(attack => attack.DefenderGeneral == defender))
                    HTML.Append(" <b>(Led the Defense)</b> ");

                if (Battle.GetSubEvents().OfType<HFDied>().Any(death => death.HistoricalFigure == defender))
                    HTML.Append(" (Died) ");
            }
            HTML.AppendLine("</ul>");
            HTML.AppendLine("</td></tr></table></br>");

            if (Battle.NonCombatants.Count > 0)
            {
                HTML.AppendLine("<b>Non Combatants</b></br>");
                HTML.AppendLine("<ol>");
                foreach (HistoricalFigure nonCombatant in Battle.NonCombatants)
                {
                    HTML.AppendLine("<li>" + nonCombatant.ToLink());
                    if (Battle.Collection.OfType<HFDied>().Any(death => death.HistoricalFigure == nonCombatant))
                        HTML.Append(" (Died) ");
                }
                HTML.AppendLine("</ol>");
            }

            PrintEventLog(Battle.GetSubEvents(), Battle.Filters, Battle);

            return HTML.ToString();
        }
    }
}
