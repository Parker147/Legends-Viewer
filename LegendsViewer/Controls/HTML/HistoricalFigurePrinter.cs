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
            PrintTitle();
            PrintMiscInfo();
            PrintPositions();
            PrintRelatedHistoricalFigures();
            PrintRelatedEntities();
            PrintReputations();
            PrintRelatedSites();
            PrintSkills();
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
            HTML.AppendLine("<h1>" + HistoricalFigure.Name + "</h1>");
            string title = string.Empty;
            if (HistoricalFigure.Deity)
            {
                title = "Is a deity";
                if (HistoricalFigure.WorshippedBy != null) 
                    title += " that occurs in the myths of " + HistoricalFigure.WorshippedBy.ToLink() + ". ";
                else 
                    title += ". ";
                title += HistoricalFigure.ToLink(false, HistoricalFigure) + " is most often depicted as a " + HistoricalFigure.GetRaceTitleString() + ". ";
            }
            else if (HistoricalFigure.Force)
            {
                title = "Is a force said to permeate nature. ";
                if (HistoricalFigure.WorshippedBy != null)
                    title += "Worshipped by " + HistoricalFigure.WorshippedBy.ToLink();
            }
            else
            {
                if (HistoricalFigure.DeathYear >= 0) title += "Was a " + HistoricalFigure.GetRaceTitleString();
                else title += "Is a " + HistoricalFigure.GetRaceTitleString();
                title += " born in " + HistoricalFigure.BirthYear;

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
            HTML.AppendLine("<b>" + title + "</b></br>");
        }


        private void PrintMiscInfo()
        {
            //if (HistoricalFigure.CurrentIdentity != null)
            //{
            //    HTML.AppendLine(Bold("Current Identity: ") + HistoricalFigure.CurrentIdentity.ToLink() + LineBreak);
            //}
            //if (HistoricalFigure.UsedIdentity != null)
            //{
            //    HTML.AppendLine(Bold("Used Identity: ") + HistoricalFigure.UsedIdentity.ToLink() + LineBreak);
            //}
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
            if (HistoricalFigure.ActiveInteractions.Count > 0)
            {
                string interactions = "";
                foreach (string interaction in HistoricalFigure.ActiveInteractions)
                {
                    if (HistoricalFigure.ActiveInteractions.Last() == interaction && HistoricalFigure.ActiveInteractions.Count > 1)
                        interactions += " and ";
                    else if (interactions.Length > 0) interactions += ", ";
                    interactions += interaction;
                }
                HTML.AppendLine(Bold("Active Interactions: ") + interactions + LineBreak);
            }
            if (HistoricalFigure.InteractionKnowledge.Count > 0)
            {
                string interactions = "";
                foreach (string interaction in HistoricalFigure.InteractionKnowledge)
                {
                    if (HistoricalFigure.InteractionKnowledge.Last() == interaction && HistoricalFigure.InteractionKnowledge.Count > 1)
                        interactions += " and ";
                    else if (interactions.Length > 0) interactions += ", ";
                    interactions += interaction;
                }
                HTML.AppendLine(Bold("Interaction Knowledge: ") + interactions + LineBreak);
            }
            if (HistoricalFigure.HoldingArtifacts.Count > 0)
            {
                string artifacts = "";
                foreach(Artifact artifact in HistoricalFigure.HoldingArtifacts)
                {
                    if (HistoricalFigure.HoldingArtifacts.Last() == artifact && HistoricalFigure.HoldingArtifacts.Count > 1)
                        artifacts += " and ";
                    else if (artifacts.Length > 0) 
                        artifacts += ", ";
                    artifacts += artifact.ToLink();
                }
                HTML.AppendLine(Bold("Holding Artifacts: ") + artifacts + LineBreak);
            }
            if (HistoricalFigure.Animated)
                HTML.AppendLine(Bold("Animated as: ") + HistoricalFigure.AnimatedType + LineBreak);
            if (HistoricalFigure.JourneyPets.Count > 0)
            {
                string pets = "";
                foreach (string pet in HistoricalFigure.JourneyPets)
                {
                    if (HistoricalFigure.JourneyPets.Last() == pet && HistoricalFigure.JourneyPets.Count > 1) pets += " and ";
                    else if (pets.Length > 0) pets += ", ";
                    pets += pet;
                }
                HTML.AppendLine(Bold("Journey Pets: ") + pets + LineBreak);
            }
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
                    string relationString = hf + ", " + relation.Type.GetDescription();
                    if (relation.Type == HistoricalFigureLinkType.Deity)
                        relationString += " (" + relation.Strength + "%)";
                    HTML.AppendLine(ListItem + relationString);
                }
                EndList(ListType.Unordered);
            }
        }

        private void PrintRelatedEntities()
        {
            if (HistoricalFigure.RelatedEntities.Count > 0)
            {
                HTML.AppendLine(Bold("Related Entities") + LineBreak);
                StartList(ListType.Unordered);
                foreach (EntityLink link in HistoricalFigure.RelatedEntities)
                {
                    string linkString = link.Entity.PrintEntity() + " (" + link.Type.GetDescription();
                    if (link.Strength > 0)
                        linkString += " " + link.Strength + "%";
                    if (link.StartYear > -1)
                    {
                        linkString += " " + link.PositionID + ", " + link.StartYear + "-";
                        if (link.EndYear > -1)
                            linkString += link.EndYear;
                        else
                            linkString += "Present";
                    }
                    linkString += ")";
                    HTML.AppendLine(ListItem + linkString);
                }
                EndList(ListType.Unordered);
            }
        }

        private void PrintRelatedSites()
        {
            if (HistoricalFigure.RelatedSites.Count > 0)
            {
                HTML.AppendLine(Bold("Related Sites") + LineBreak);
                StartList(ListType.Unordered);
                foreach (SiteLink link in HistoricalFigure.RelatedSites)
                {
                    string linkString = link.Site.ToLink() + ", " + link.Type.GetDescription() + " (" + (link.Type == SiteLinkType.Occupation ? link.OccupationID : link.SubID) + ") ";
                    if (link.Entity != null)
                        linkString += "" + link.Entity.ToLink();
                    HTML.AppendLine(ListItem + linkString);
                }
                EndList(ListType.Unordered);
            }
        }

        private void PrintReputations()
        {
            if (HistoricalFigure.Reputations.Count > 0)
            {
                HTML.AppendLine(Bold("Entity Reputations") + LineBreak);
                StartList(ListType.Unordered);
                foreach(EntityReputation reputation in HistoricalFigure.Reputations)
                {
                    HTML.AppendLine(ListItem + reputation.Entity.PrintEntity() + ": ");
                    StartList(ListType.Unordered);
                    if (reputation.UnsolvedMurders > 0)
                        HTML.AppendLine(ListItem + "Unsolved Murders: " + reputation.UnsolvedMurders);
                    if (reputation.FirstSuspectedAgelessYear > 0)
                        HTML.AppendLine(ListItem + "First Suspected Ageless Year: " + reputation.FirstSuspectedAgelessYear + ", " + reputation.FirstSuspectedAglessSeason);
                    EndList(ListType.Unordered);
                }
                EndList(ListType.Unordered);
            }
        }

        private void PrintSkills()
        {
            if (HistoricalFigure.Skills.Count > 0)
            {
                HTML.AppendLine(Bold("Skills") + LineBreak);
                StartList(ListType.Unordered);
                foreach (Skill skill in HistoricalFigure.Skills)
                {
                    HTML.AppendLine(ListItem + skill.Name + " - " + skill.Rank + " (" + skill.Points + ")");
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
                    if (battle.ParentCollection != null)
                    {
                        battleTable.AddData("as part of");
                        battleTable.AddData(battle.ParentCollection.ToLink());
                    }
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
            if (HistoricalFigure.NotableKills.Count > 0)
            {
                HTML.AppendLine(Bold("Kills") + " " + MakeLink("[Load]", LinkOption.LoadHFKills));
                StartList(ListType.Ordered);
                foreach (HFDied kill in HistoricalFigure.NotableKills)
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
