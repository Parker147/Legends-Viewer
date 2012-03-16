using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls
{
    public class HistoricalFigureHTMLPrinter : HTMLPrinter
    {
        World World;
        HistoricalFigure HistoricalFigure;

        public HistoricalFigureHTMLPrinter(HistoricalFigure hf, World world)
        {
            World = world;
            HistoricalFigure = hf;
        }

        public override string Print()
        {
            HTML = new StringBuilder();
            PrintStyle();
            PrintTitle();
            PrintMiscInfo();
            PrintPositions();
            PrintRelatedHistoricalFigures();
            PrintBattles();
            PrintKills();
            PrintBeastAttacks();
            PrintEvents();
            return HTML.ToString();
        }

        public override string GetTitle()
        {
            if (HistoricalFigure.Name.IndexOf(" ") > 0)
                return HistoricalFigure.Name.Substring(0, HistoricalFigure.Name.IndexOf(" ") + 1);
            else
                return HistoricalFigure.Name;
        }

        private void PrintTitle()
        {
            string title = String.Empty;
            if (HistoricalFigure.Deity)
            {
                title = HistoricalFigure.Name + " is a deity";
                if (HistoricalFigure.WorshippedBy != null) 
                    title += " that occurs in the myths of " + HistoricalFigure.WorshippedBy.ToLink() + ". ";
                else 
                    title += ". ";
                title += HistoricalFigure.ToLink(false, HistoricalFigure) + " is most often depicted as a " + HistoricalFigure.RaceString() + ". ";
            }
            else if (HistoricalFigure.Force)
            {
                title = HistoricalFigure.Name + " is a force said to permeate nature. ";
                if (HistoricalFigure.WorshippedBy != null)
                    title += "Worshipped by " + HistoricalFigure.WorshippedBy.ToLink();
            }
            else
            {
                title = HistoricalFigure.Name;
                if (HistoricalFigure.DeathYear >= 0) title += " was a " + HistoricalFigure.RaceString();
                else title += " is a " + HistoricalFigure.RaceString();
                title += " born in " + HistoricalFigure.BirthYear;
                //else title += " " + CasteNoun(Caste) + " was the first of " + CasteNoun(Caste, true) + " kind";
                if (HistoricalFigure.DeathYear > 0)
                {
                    HFDied death = HistoricalFigure.Events.OfType<HFDied>().First(hfDeath => hfDeath.HistoricalFigure == HistoricalFigure);
                    title += " and died in " + HistoricalFigure.DeathYear + " (" + death.Cause.GetDescription() + ")";
                    if (death.Slayer != null) title += " by " + death.Slayer.ToLink();
                    else if (death.SlayerRace != "UNKNOWN") title += " by a " + death.SlayerRace.ToLower();
                    if (death.PrintParentCollection().Length > 0)
                        title += ", " + death.PrintParentCollection().Replace("In ", "in ");
                }
                if (!title.EndsWith(". "))
                    title += ". ";
                title += LineBreak + "Caste: " + HistoricalFigure.Caste + LineBreak + "Type: " + HistoricalFigure.AssociatedType;
            }
            HTML.AppendLine(Bold(title) + LineBreak);
        }


        private void PrintMiscInfo()
        {
            if (HistoricalFigure.Spheres.Count > 0)
            {
                string spheres = "";
                foreach (string sphere in HistoricalFigure.Spheres)
                {
                    if (HistoricalFigure.Spheres.Last() == sphere && HistoricalFigure.Spheres.Count > 1) spheres += " and ";
                    else if (spheres.Length > 0) spheres += ", ";
                    spheres += sphere;
                }
                HTML.Append(Bold("Associated Spheres: ") + spheres + LineBreak);
            }
            if (HistoricalFigure.Goal != "")
                HTML.AppendLine(Bold("Goal: ") + HistoricalFigure.Goal + LineBreak);
            if (HistoricalFigure.ActiveInteraction != "")
                HTML.AppendLine(Bold("Active Interaction: ") + HistoricalFigure.ActiveInteraction + LineBreak);
            if (HistoricalFigure.InteractionKnowledge != "")
                HTML.AppendLine(Bold("Interaction Knowledge: ") + HistoricalFigure.InteractionKnowledge + LineBreak);
            if (HistoricalFigure.Animated)
                HTML.AppendLine(Bold("Animated as: ") + HistoricalFigure.AnimatedType + LineBreak);
            if (HistoricalFigure.JourneyPet != "")
                HTML.AppendLine(Bold("Journey Pet: ") + HistoricalFigure.JourneyPet + LineBreak);
            HTML.AppendLine(LineBreak);
        }

        private void PrintPositions()
        {
            if (HistoricalFigure.Positions.Count > 0)
            {
                HTML.AppendLine(Bold("Positions") + LineBreak);
                StartList(ListType.Ordered);
                foreach (HistoricalFigure.Position position in HistoricalFigure.Positions)
                {
                    HTML.AppendLine(ListItem + position.Title + " of " + position.Entity.PrintEntity() + " (" + position.Began + " - ");
                    if (position.Ended == -1) HTML.Append("Present)");
                    else HTML.Append(position.Ended + ")");
                }
                EndList(ListType.Ordered);
            }
        }

        private void PrintRelatedHistoricalFigures()
        {
            if (HistoricalFigure.RelatedHistoricalFigures.Count > 0)
            {
                HTML.AppendLine(Bold("Related Historical Figures") + LineBreak);
                StartList(ListType.Unordered);
                foreach (HistoricalFigureLink relation in HistoricalFigure.RelatedHistoricalFigures)
                {
                    string hf = "UNKNOWN";
                    if (relation.HistoricalFigure != null)
                        hf = relation.HistoricalFigure.ToLink();
                    string relationString = hf + ", " + relation.Type;
                    if (relation.Type == HistoricalFigureLinkType.Deity)
                        relationString += " (" + relation.Strength + "%)";
                    HTML.AppendLine(ListItem + relationString);
                }
                EndList(ListType.Unordered);
            }
        }

        private void PrintBattles()
        {
            Battle unnotableDeathBattle = null; //Temporarily make the battle that the HF died in Notable so it shows up.
            if (HistoricalFigure.Battles.Count > 0 && HistoricalFigure.Battles.Last().Collection.OfType<HFDied>().Count(death => death.HistoricalFigure == HistoricalFigure) == 1 && !HistoricalFigure.Battles.Last().Notable)
            {
                unnotableDeathBattle = HistoricalFigure.Battles.Last();
                unnotableDeathBattle.Notable = true;
            }

            if (HistoricalFigure.Battles.Count(battle => !World.FilterBattles || battle.Notable) > 0)
            {
                HTML.AppendLine(Bold("Battles") + MakeLink("[Load]", LinkOption.LoadHFBattles));
                if (World.FilterBattles)
                    HTML.Append(" (Notable)");
                HTML.Append(LineBreak);
                TableMaker battleTable = new TableMaker(true);
                foreach (Battle battle in HistoricalFigure.Battles.Where(battle => (!World.FilterBattles || battle.Notable) || battle.Collection.OfType<HFDied>().Count(death => death.HistoricalFigure == HistoricalFigure) > 0))
                {
                    battleTable.StartRow();
                    battleTable.AddData(battle.StartYear.ToString());
                    battleTable.AddData(battle.ToLink());
                    battleTable.AddData("as part of");
                    battleTable.AddData(battle.ParentCollection.ToLink());
                    string involvement = "";
                    if (battle.NotableAttackers.Count > 0 && battle.NotableAttackers.Contains(HistoricalFigure))
                        if (battle.Collection.OfType<FieldBattle>().Where(fieldBattle => fieldBattle.AttackerGeneral == HistoricalFigure).Count() > 0 ||
                            battle.Collection.OfType<AttackedSite>().Where(attack => attack.AttackerGeneral == HistoricalFigure).Count() > 0)
                            involvement += "Led the attack";
                        else
                            involvement += "Fought in the attack";
                    else if (battle.NotableDefenders.Count > 0 && battle.NotableDefenders.Contains(HistoricalFigure))
                        if (battle.Collection.OfType<FieldBattle>().Where(fieldBattle => fieldBattle.DefenderGeneral == HistoricalFigure).Count() > 0 ||
                            battle.Collection.OfType<AttackedSite>().Where(attack => attack.DefenderGeneral == HistoricalFigure).Count() > 0)
                            involvement += "Led the defense";
                        else
                            involvement += "Aided in the defense";
                    else
                        involvement += "A non combatant";

                    if (battle.GetSubEvents().OfType<HFDied>().Where(death => death.HistoricalFigure == HistoricalFigure).Count() > 0)
                        involvement += " and died";
                    battleTable.AddData(involvement);
                    if (battle.NotableAttackers.Contains(HistoricalFigure))
                    {
                        battleTable.AddData("against");
                        battleTable.AddData(battle.Defender.PrintEntity(), 0, TableDataAlign.Right);
                        if (battle.Victor == battle.Attacker)
                            battleTable.AddData("and won");
                        else
                            battleTable.AddData("and lost");
                    }
                    else if (battle.NotableDefenders.Contains(HistoricalFigure))
                    {
                        battleTable.AddData("against");
                        battleTable.AddData(battle.Attacker.PrintEntity(), 0, TableDataAlign.Right);
                        if (battle.Victor == battle.Defender)
                            battleTable.AddData("and won");
                        else
                            battleTable.AddData("and lost");
                    }

                    battleTable.AddData("Deaths: " + (battle.AttackerDeathCount + battle.DefenderDeathCount) + ")");

                    battleTable.EndRow();
                }
                HTML.AppendLine(battleTable.GetTable() + LineBreak);
            }

            if (World.FilterBattles && HistoricalFigure.Battles.Count(battle => !battle.Notable) > 0)
                HTML.AppendLine(Bold("Battles") + " (Unnotable): " + HistoricalFigure.Battles.Where(battle => !battle.Notable).Count() + LineBreak + LineBreak);

            if (unnotableDeathBattle != null)
                unnotableDeathBattle.Notable = false;
        }

        private void PrintKills()
        {
            if (HistoricalFigure.Kills.Count > 0)
            {
                HTML.AppendLine(Bold("Kills") + " " + MakeLink("[Load]", LinkOption.LoadHFKills));
                StartList(ListType.Ordered);
                foreach (HFDied kill in HistoricalFigure.Kills)
                    HTML.AppendLine(ListItem + kill.HistoricalFigure.ToLink() + ", in " + kill.Year + " (" + kill.Cause.GetDescription() + ")" + LineBreak);
                EndList(ListType.Ordered);
            }
        }

        private void PrintBeastAttacks()
        {
            if (HistoricalFigure.BeastAttacks != null && HistoricalFigure.BeastAttacks.Count > 0)
            {
                HTML.AppendLine(Bold("Beast Attacks"));
                StartList(ListType.Ordered);
                foreach (BeastAttack attack in HistoricalFigure.BeastAttacks)
                {
                    HTML.AppendLine(ListItem + attack.StartYear + ", " + MakeLink(attack.GetOrdinal(attack.Ordinal) + "rampage in ", attack) + attack.Site.ToLink());
                    if (attack.GetSubEvents().OfType<HFDied>().Count() > 0)
                        HTML.Append(" (Kills: " + attack.GetSubEvents().OfType<HFDied>().Count() + ")");
                }
                EndList(ListType.Ordered);
                HTML.AppendLine(LineBreak);
            }
        }

        private void PrintEvents()
        {
            HTML.AppendLine(Bold("Event Log") + " " + MakeLink(Font("[Chart]", "Maroon"), LinkOption.LoadChart) + LineBreak);
            foreach (var e in HistoricalFigure.Events)
                if (!HistoricalFigure.Filters.Contains(e.Type))
                    HTML.AppendLine(e.Print(true, HistoricalFigure) + LineBreak + LineBreak);
        }
    }
}
