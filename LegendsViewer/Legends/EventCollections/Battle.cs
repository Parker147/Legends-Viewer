using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using LegendsViewer.Controls;

namespace LegendsViewer.Legends
{
    public enum BattleOutcome
    {
        [Description("Attacker Won")]
        AttackerWon,
        [Description("Defender Won")]
        DefenderWon,
        Unknown
    }
    public class Battle : EventCollection
    {
        public string Name { get; set; }
        public BattleOutcome Outcome { get; set; }
        public Location Coordinates { get; set; }
        public WorldRegion Region { get; set; }
        public UndergroundRegion UndergroundRegion { get; set; }
        public Site Site { get; set; }
        public SiteConquered Conquering { get; set; }
        public Entity Attacker { get; set; }
        public Entity Defender { get; set; }
        public Entity Victor { get; set; }
        public List<Squad> Attackers { get; set; }
        public List<Squad> Defenders { get; set; }
        public List<HistoricalFigure> NotableAttackers { get; set; }
        public List<HistoricalFigure> NotableDefenders { get; set; }
        public List<HistoricalFigure> NonCombatants { get; set; }
        public List<Squad> AttackerSquads { get; set; }
        public List<Squad> DefenderSquads { get; set; }
        public int AttackerCount { get { return NotableAttackers.Count + AttackerSquads.Sum(squad => squad.Numbers); } set { } }
        public int DefenderCount { get { return NotableDefenders.Count + DefenderSquads.Sum(squad => squad.Numbers); } set { } }
        public int AttackersRemainingCount { get { return Attackers.Sum(squad => squad.Numbers - squad.Deaths); } set { } }
        public int DefendersRemainingCount { get { return Defenders.Sum(squad => squad.Numbers - squad.Deaths); } set { } }
        public int DeathCount { get { return AttackerDeathCount + DefenderDeathCount; } set { } }
        public List<string> Deaths { get; set; }
        public List<HistoricalFigure> NotableDeaths { get { return NotableAttackers.Where(attacker => GetSubEvents().OfType<HFDied>().Count(death => death.HistoricalFigure == attacker) > 0).Concat(NotableDefenders.Where(defender => GetSubEvents().OfType<HFDied>().Count(death => death.HistoricalFigure == defender) > 0)).ToList(); } set { } }
        public int AttackerDeathCount { get; set; }
        public int DefenderDeathCount { get; set; }
        public double AttackersToDefenders
        {
            get
            {
                if (AttackerCount == 0 && DefenderCount == 0) return 0;
                if (DefenderCount == 0) return double.MaxValue;
                return Math.Round(AttackerCount / Convert.ToDouble(DefenderCount), 2);
            }
            set { }
        }
        public double AttackersToDefendersRemaining
        {
            get
            {
                if (AttackersRemainingCount == 0 && DefendersRemainingCount == 0) return 0;
                if (DefendersRemainingCount == 0) return double.MaxValue;
                return Math.Round(AttackersRemainingCount / Convert.ToDouble(DefendersRemainingCount), 2);
            }
            set { }
        }
        public double AttackerToDefenderKills
        {
            get
            {
                if (AttackerDeathCount == 0 && DefenderDeathCount == 0) return 0;
                if (AttackerDeathCount == 0) return double.MaxValue;
                return Math.Round(DefenderDeathCount / Convert.ToDouble(AttackerDeathCount), 2);
            }
            set { }
        }

        public List<string> AttackersAsList
        {
            get;
            set;
        }

        public List<string> DefendersAsList
        {
            get;
            set;
        }

        public static List<string> Filters;
        public override List<WorldEvent> FilteredEvents
        {
            get { return AllEvents.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }

        public Battle(List<Property> properties, World world)
            : base(properties, world)
        {

            Initialize();

            List<string> attackerSquadRace, defenderSquadRace;
            List<int> attackerSquadEntityPopulation, attackerSquadNumbers, attackerSquadDeaths, attackerSquadSite,
                         defenderSquadEntityPopulation, defenderSquadNumbers, defenderSquadDeaths, defenderSquadSite;
            NotableAttackers = new List<HistoricalFigure>(); NotableDefenders = new List<HistoricalFigure>();
            AttackerSquads = new List<Squad>(); DefenderSquads = new List<Squad>();
            attackerSquadRace = new List<string>(); attackerSquadEntityPopulation = new List<int>(); attackerSquadNumbers = new List<int>(); attackerSquadDeaths = new List<int>();
            attackerSquadSite = new List<int>();
            defenderSquadRace = new List<string>(); defenderSquadEntityPopulation = new List<int>(); defenderSquadNumbers = new List<int>(); defenderSquadDeaths = new List<int>();
            defenderSquadSite = new List<int>();
            Attackers = new List<Squad>();
            Defenders = new List<Squad>();
            NonCombatants = new List<HistoricalFigure>();
            foreach (Property property in properties)
                switch (property.Name)
                {
                    case "outcome": switch (property.Value)
                        {
                            case "attacker won": Outcome = BattleOutcome.AttackerWon; break;
                            case "defender won": Outcome = BattleOutcome.DefenderWon; break;
                            default: Outcome = BattleOutcome.Unknown; world.ParsingErrors.Report("Unknown Battle Outcome: " + property.Value); break;
                        } break;
                    case "name": Name = Formatting.InitCaps(property.Value); break;
                    case "coords": Coordinates = Formatting.ConvertToLocation(property.Value); break;
                    case "war_eventcol": ParentCollection = world.GetEventCollection(Convert.ToInt32(property.Value)); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "attacking_hfid": NotableAttackers.Add(world.GetHistoricalFigure(Convert.ToInt32(property.Value))); break;
                    case "defending_hfid": NotableDefenders.Add(world.GetHistoricalFigure(Convert.ToInt32(property.Value))); break;
                    case "attacking_squad_race": attackerSquadRace.Add(Formatting.FormatRace(property.Value)); break;
                    case "attacking_squad_entity_pop": attackerSquadEntityPopulation.Add(Convert.ToInt32(property.Value)); break;
                    case "attacking_squad_number": attackerSquadNumbers.Add(Convert.ToInt32(property.Value)); break;
                    case "attacking_squad_deaths": attackerSquadDeaths.Add(Convert.ToInt32(property.Value)); break;
                    case "attacking_squad_site": attackerSquadSite.Add(Convert.ToInt32(property.Value)); break;
                    case "defending_squad_race": defenderSquadRace.Add(Formatting.FormatRace(property.Value)); break;
                    case "defending_squad_entity_pop": defenderSquadEntityPopulation.Add(Convert.ToInt32(property.Value)); break;
                    case "defending_squad_number": defenderSquadNumbers.Add(Convert.ToInt32(property.Value)); break;
                    case "defending_squad_deaths": defenderSquadDeaths.Add(Convert.ToInt32(property.Value)); break;
                    case "defending_squad_site": defenderSquadSite.Add(Convert.ToInt32(property.Value)); break;
                    case "noncom_hfid": NonCombatants.Add(world.GetHistoricalFigure(Convert.ToInt32(property.Value))); break;
                }

            if (Collection.OfType<AttackedSite>().Any())
            {
                Attacker = Collection.OfType<AttackedSite>().First().Attacker;
                Defender = Collection.OfType<AttackedSite>().First().Defender;
            }
            else if (Collection.OfType<FieldBattle>().Any())
            {
                Attacker = Collection.OfType<FieldBattle>().First().Attacker;
                Defender = Collection.OfType<FieldBattle>().First().Defender;
            }

            foreach (HistoricalFigure attacker in NotableAttackers) attacker.Battles.Add(this);
            foreach (HistoricalFigure defender in NotableDefenders) defender.Battles.Add(this);
            foreach (HistoricalFigure nonCombatant in NonCombatants) nonCombatant.Battles.Add(this);

            for (int i = 0; i < attackerSquadRace.Count; i++)
                AttackerSquads.Add(new Squad(attackerSquadRace[i], attackerSquadNumbers[i], attackerSquadDeaths[i], attackerSquadSite[i], attackerSquadEntityPopulation[i]));
            for (int i = 0; i < defenderSquadRace.Count; i++)
                DefenderSquads.Add(new Squad(defenderSquadRace[i], defenderSquadNumbers[i], defenderSquadDeaths[i], defenderSquadSite[i], defenderSquadEntityPopulation[i]));

            var groupedAttackerSquads = from squad in AttackerSquads
                                        group squad by squad.Race into squadRace
                                        select new { Race = squadRace.Key, Count = squadRace.Sum(squad => squad.Numbers), Deaths = squadRace.Sum(squad => squad.Deaths) };
            foreach (var squad in groupedAttackerSquads)
                Attackers.Add(new Squad(squad.Race, squad.Count + NotableAttackers.Count(attacker => attacker.Race == squad.Race), squad.Deaths + Collection.OfType<HFDied>().Count(death => death.HistoricalFigure.Race == squad.Race && NotableAttackers.Contains(death.HistoricalFigure)), -1, -1));
            foreach (var attacker in NotableAttackers.Where(hf => Attackers.Count(squad => squad.Race == hf.Race) == 0).GroupBy(hf => hf.Race).Select(race => new { Race = race.Key, Count = race.Count() }))
            {
                Attackers.Add(new Squad(attacker.Race, attacker.Count, Collection.OfType<HFDied>().Count(death => NotableAttackers.Contains(death.HistoricalFigure) && death.HistoricalFigure.Race == attacker.Race), -1, -1));
            }
            AttackersAsList = new List<string>();
            foreach (Squad squad in Attackers)
                for (int i = 0; i < squad.Numbers; i++)
                    AttackersAsList.Add(squad.Race);



            var groupedDefenderSquads = from squad in DefenderSquads
                                        group squad by squad.Race into squadRace
                                        select new { Race = squadRace.Key, Count = squadRace.Sum(squad => squad.Numbers), Deaths = squadRace.Sum(squad => squad.Deaths) };
            foreach (var squad in groupedDefenderSquads)
                Defenders.Add(new Squad(squad.Race, squad.Count + NotableDefenders.Count(defender => defender.Race == squad.Race), squad.Deaths + Collection.OfType<HFDied>().Count(death => death.HistoricalFigure.Race == squad.Race && NotableDefenders.Contains(death.HistoricalFigure)), -1, -1));
            foreach (var defender in NotableDefenders.Where(hf => Defenders.Count(squad => squad.Race == hf.Race) == 0).GroupBy(hf => hf.Race).Select(race => new { Race = race.Key, Count = race.Count() }))
            {
                Defenders.Add(new Squad(defender.Race, defender.Count, Collection.OfType<HFDied>().Count(death => NotableDefenders.Contains(death.HistoricalFigure) && death.HistoricalFigure.Race == defender.Race), -1, -1));
            }
            DefendersAsList = new List<string>();
            foreach (Squad squad in Defenders)
                for (int i = 0; i < squad.Numbers; i++)
                    DefendersAsList.Add(squad.Race);

            Deaths = new List<string>();
            foreach (Squad squad in Attackers.Concat(Defenders))
                for (int i = 0; i < squad.Deaths; i++)
                    Deaths.Add(squad.Race);

            AttackerDeathCount = Attackers.Sum(attacker => attacker.Deaths);
            DefenderDeathCount = Defenders.Sum(defender => defender.Deaths);

            if (Outcome == BattleOutcome.AttackerWon) Victor = Attacker;
            else if (Outcome == BattleOutcome.DefenderWon) Victor = Defender;

            War parentWar = ParentCollection as War;
            if (parentWar != null)
            {
                if (parentWar.Attacker == Attacker)
                {
                    parentWar.AttackerDeathCount += AttackerDeathCount;
                    parentWar.DefenderDeathCount += DefenderDeathCount;
                }
                else
                {
                    parentWar.AttackerDeathCount += DefenderDeathCount;
                    parentWar.DefenderDeathCount += AttackerDeathCount;
                }
                parentWar.DeathCount += attackerSquadDeaths.Sum() + defenderSquadDeaths.Sum() + Collection.OfType<HFDied>().Count();

                if (Attacker == parentWar.Attacker && Victor == Attacker) parentWar.AttackerVictories.Add(this);
                else parentWar.DefenderVictories.Add(this);
            }

            if (Site != null) Site.Warfare.Add(this);
            if (Region != null) Region.Battles.Add(this);
            if (UndergroundRegion != null) UndergroundRegion.Battles.Add(this);

            if ((attackerSquadDeaths.Sum() + defenderSquadDeaths.Sum() + Collection.OfType<HFDied>().Count()) == 0)
                Notable = false;
            if ((attackerSquadNumbers.Sum() + NotableAttackers.Count) > ((defenderSquadNumbers.Sum() + NotableDefenders.Count) * 10) //NotableDefenders outnumbered 10 to 1
                && Victor == Attacker
                && AttackerDeathCount < ((NotableAttackers.Count + attackerSquadNumbers.Sum()) * 0.1)) //NotableAttackers lossses < 10%
                Notable = false;
        }

        private void Initialize()
        {
            Name = "";
            Outcome = BattleOutcome.Unknown;
            Coordinates = new Location(0, 0);
            AttackerDeathCount = 0;
            DefenderDeathCount = 0;
        }

        public class Squad
        {
            public string Race { get; set; }
            public int Numbers { get; set; }
            public int Deaths { get; set; }
            public int Site { get; set; }
            public int Population { get; set; }
            public Squad(string race, int numbers, int deaths, int site, int population)
            {
                Race = String.Intern(race);
                Numbers = numbers;
                Deaths = deaths;
                Site = site;
                Population = population;
            }
        }

        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            if (link)
            {
                string linkedString = "";
                if (pov != this)
                {
                    string title = Attacker.PrintEntity(false) + " (Attacker)";
                    if (Victor == Attacker) title += "(V)";
                    title += "&#13";
                    title += "Kills: " + DefenderDeathCount;
                    title += "&#13";
                    title += Defender != null ? Defender.PrintEntity(false) : "UNKNOWN";
                    title += " (Defender)";
                    if (Victor == Defender) title += "(V)";
                    title += "&#13";
                    title += "Kills: " + AttackerDeathCount;

                    linkedString = "<a href = \"collection#" + this.ID + "\" title=\"" + title + "\"><font color=\"#6E5007\">" + Name + "</font></a>";
                }
                else
                    linkedString = "<font color=\"Blue\">" + Name + "</font>";
                return linkedString;
            }
            else
                return Name;
        }
        public override string ToString()
        {
            return Name;
        }

    }
}
