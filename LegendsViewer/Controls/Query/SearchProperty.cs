using System;
using System.Collections.Generic;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls.Query
{
    public class SearchProperty
    {
        public string Name;
        public string Description;
        public bool IsList = false;
        public bool IsSelectable = false;
        public Type Type;
        public List<SearchProperty> SubProperties = new List<SearchProperty>();
        public SearchProperty(string name, Type type)
            : this(name, name, type)
        {
        }

        public SearchProperty(string name, string description, Type type, bool selectable = false)
        {
            Name = name;
            Description = description;
            Type = type;
            if (Type.IsGenericType && Type.GetGenericTypeDefinition() == typeof(List<>))
            {
                IsList = true;

                //SubProperties = SearchObject.GetProperties(type.GetGenericArguments()[0]);
            }
            IsSelectable = selectable;
        }

        public override string ToString()
        {
            return Description;
        }

        public void SetSubProperties()
        {
            SubProperties = GetProperties(this.Type, true);
        }

        public static List<SearchProperty> GetProperties(Type searchType, bool noSubProperties = false)
        {
            Type nonGenericSearchType;
            if (searchType.IsGenericType && searchType != typeof(List<int>) && searchType != typeof(List<string>))
                nonGenericSearchType = searchType.GetGenericArguments()[0];
            else
                nonGenericSearchType = searchType;
            List<SearchProperty> SearchProperties = new List<SearchProperty>();
            if (nonGenericSearchType == typeof(HistoricalFigure))
            {
                SearchProperties = new List<SearchProperty>(){
			        new SearchProperty("Name", typeof(string)),
			        new SearchProperty("Race", typeof(string)),
			        new SearchProperty("AssociatedType", "Associated Type", typeof(string)),
			        new SearchProperty("Caste", typeof(string)),
                    new SearchProperty("CurrentState", "Current State", typeof(HFState)),
                    new SearchProperty("States", "All States", typeof(List<HistoricalFigure.State>), false),
                    new SearchProperty("Age", typeof(int)),
                    new SearchProperty("Appeared", typeof(int)),
                    new SearchProperty("BirthYear", "Birth Year", typeof(int)),
                    new SearchProperty("DeathYear", "Death Year", typeof(int)),
                    new SearchProperty("DeathCause", "Death Cause", typeof(DeathCause)),
                    new SearchProperty("DeathCollectionType", "Died in", typeof(string)),
                    new SearchProperty("Alive", "Is Alive", typeof(bool)),
                    new SearchProperty("Deity", "Is Deity", typeof(bool)),
                    new SearchProperty("Force", "Is Force", typeof(bool)),
                    new SearchProperty("Skeleton", "Is Skeleton", typeof(bool)),
                    new SearchProperty("Zombie", "Is Zombie", typeof(bool)),
                    new SearchProperty("Ghost", "Is Ghost", typeof(bool)),
                    new SearchProperty("Positions", "Positions", typeof(List<HistoricalFigure.Position>), false),
                    new SearchProperty("HFKills", "Kills", typeof(List<HistoricalFigure>), true),
                    new SearchProperty("Abductions", typeof(List<HistoricalFigure>)),
                    new SearchProperty("Abducted", typeof(int)),
                    new SearchProperty("Battles", "Battles", typeof(List<Battle>), true),
                    new SearchProperty("BattlesAttacking", "Battles (Attacking)", typeof(List<Battle>), true),
                    new SearchProperty("BattlesDefending", "Battles (Defending)", typeof(List<Battle>), true),
                    new SearchProperty("BattlesNonCombatant", "Battles (Non-Combatant)", typeof(List<Battle>), true),
                    new SearchProperty("BeastAttacks", "Beast Attacks", typeof(List<BeastAttack>), true),
                    new SearchProperty("Spheres", "Associated Spheres", typeof(List<string>), false)
                    //new SearchProperty("Beast", "Is Beast", typeof(bool))
                };
            }
            else if (nonGenericSearchType == typeof(Entity))
            {
                SearchProperties = new List<SearchProperty>() {
                    new SearchProperty("Name", typeof(string)),
                    new SearchProperty("Race", typeof(string)),
                    new SearchProperty("IsCiv", "Is Civilization", typeof(bool)),
                    new SearchProperty("Sites", "All Sites", typeof(List<Site>), true),
                    new SearchProperty("CurrentSites", "Current Sites", typeof(List<Site>), true),
                    new SearchProperty("LostSites", "Lost Sites", typeof(List<Site>), true),
                    new SearchProperty("Groups", "Groups", typeof(List<Entity>), true),
                    new SearchProperty("AllLeaders", "Leaders", typeof(List<HistoricalFigure>), true),
                    new SearchProperty("Worshipped", "Worshipped", typeof(List<HistoricalFigure>), true),
                    new SearchProperty("Wars", "Wars", typeof(List<War>), true),
                    new SearchProperty("WarsAttacking", "Wars (Attacking)", typeof(List<War>), true),
                    new SearchProperty("WarsDefending", "Wars (Defending)", typeof(List<War>), true),
                    new SearchProperty("WarKillDeathRatio", "Kills : Deaths", typeof(double)),
                    new SearchProperty("PopulationsAsList", "Populations", typeof(List<string>), false)
                };
            }
            else if (nonGenericSearchType == typeof(Site))
            {
                SearchProperties = new List<SearchProperty>(){
                    new SearchProperty("Name", typeof(string)),
                    new SearchProperty("Type", typeof(string)),
                    new SearchProperty("UntranslatedName", "Untranslated Name", typeof(string)),
                    new SearchProperty("Deaths", "Deaths", typeof(List<string>), false),
                    new SearchProperty("NotableDeaths", "Notable Deaths", typeof(List<HistoricalFigure>), true),
                    new SearchProperty("Battles", "Battles", typeof(List<Battle>), true),
                    new SearchProperty("Conquerings", "Conquerings", typeof(List<SiteConquered>), true),
                    new SearchProperty("CurrentOwner", "Current Owner", typeof(Entity)),
                    new SearchProperty("PreviousOwners", "Previous Owners", typeof(List<Entity>), true),
                    //new SearchProperty("Connections", typeof(List<Site>)),
                    new SearchProperty("PopulationsAsList", "Populations", typeof(List<string>), false),
                    new SearchProperty("BeastAttacks", "Beast Attacks", typeof(List<BeastAttack>), true)
                };
            }
            else if (nonGenericSearchType == typeof(WorldRegion))
            {
                SearchProperties = new List<SearchProperty>(){
                    new SearchProperty("Name", typeof(string)),
                    new SearchProperty("Type", typeof(string)),
                    new SearchProperty("Battles", "Battles", typeof(List<Battle>), true),
                    new SearchProperty("Deaths", "Deaths", typeof(List<string>), false),
                    new SearchProperty("NotableDeaths", "Notable Deaths", typeof(List<HistoricalFigure>), true)
                };
            }
            else if (nonGenericSearchType == typeof(UndergroundRegion))
            {
                SearchProperties = new List<SearchProperty>(){
                    new SearchProperty("Type", typeof(string)),
                    new SearchProperty("Depth", typeof(int))
                };
            }
            else if (nonGenericSearchType == typeof(War))
            {
                SearchProperties = new List<SearchProperty>(){
                    new SearchProperty("Name", typeof(string)),
                    new SearchProperty("StartYear", "Start Year", typeof(int)),
                    new SearchProperty("EndYear", "End Year", typeof(int)),
                    new SearchProperty("Length", typeof(int)),
                    new SearchProperty("Attacker", typeof(Entity)),
                    new SearchProperty("Defender", typeof(Entity)),
                    new SearchProperty("Battles", "Battles", typeof(List<Battle>), true),
                    new SearchProperty("SitesLost", "Sites Lost", typeof(List<Site>), true),
                    new SearchProperty("Deaths", "Deaths", typeof(List<string>), false),
                    new SearchProperty("AttackerBattleVictories", "Attacker Victories", typeof(List<Battle>), true),
                    new SearchProperty("DefenderBattleVictories", "Defender Victories", typeof(List<Battle>), true),
                    new SearchProperty("AttackerConquerings", "Attacker Conquerings", typeof(List<SiteConquered>), true),
                    new SearchProperty("DefenderConquerings", "Defender Conquerings", typeof(List<SiteConquered>), true),
                    new SearchProperty("AttackerSitesLost", "Attacker Sites Lost", typeof(List<Site>), true),
                    new SearchProperty("DefenderSitesLost", "Defender Sites Lost", typeof(List<Site>), true),
                    new SearchProperty("AttackerToDefenderVictories", "Attacker : Defender (Victories)", typeof(double)),
                    new SearchProperty("AttackerToDefenderKills", "Attacker : Defender (Kills)", typeof(double))
                    //new SearchProperty("ErrorBattles", typeof(List<Battle>))
                };
            }
            else if (nonGenericSearchType == typeof(Battle))
            {
                SearchProperties = new List<SearchProperty>(){
                    new SearchProperty("Name", typeof(string)),
                    new SearchProperty("Site", typeof(Site)),
                    new SearchProperty("Region", typeof(WorldRegion)),
                    new SearchProperty("StartYear", "Year", typeof(int)),
                    new SearchProperty("Attacker", typeof(Entity)),
                    new SearchProperty("Defender", typeof(Entity)),
                    new SearchProperty("AttackersAsList", "Attackers", typeof(List<string>), false),
                    new SearchProperty("DefendersAsList", "Defenders", typeof(List<string>), false),
                    new SearchProperty("NotableAttackers", "Notable Attackers", typeof(List<HistoricalFigure>), true),
                    new SearchProperty("NotableDefenders", "Notable Defenders", typeof(List<HistoricalFigure>), true),
                    new SearchProperty("AttackersToDefenders", "Attackers : Defenders", typeof(double)),
                    new SearchProperty("AttackersToDefendersRemaining", "Attackers : Defenders (Remaining)", typeof(double)),
                    new SearchProperty("Deaths", "Deaths", typeof(List<string>), false),
                    new SearchProperty("NotableDeaths", "Notable Deaths", typeof(List<HistoricalFigure>), true),
                    new SearchProperty("NonCombatants", "Non-Combatants", typeof(List<HistoricalFigure>), true),
                    new SearchProperty("Victor", typeof(Entity)),
                    new SearchProperty("Outcome", typeof(BattleOutcome)),
                    new SearchProperty("Conquering", typeof(SiteConquered))                    
                };
            }
            else if (nonGenericSearchType == typeof(SiteConquered))
            {
                SearchProperties = new List<SearchProperty>(){
                    new SearchProperty("Ordinal", typeof(int)),
                    new SearchProperty("ConquerType", "Conquered By", typeof(SiteConqueredType)),
                    new SearchProperty("Site", typeof(Site)),
                    new SearchProperty("StartYear", "Year", typeof(int)),
                    new SearchProperty("Attacker", typeof(Entity)),
                    new SearchProperty("Defender", typeof(Entity)),
                    new SearchProperty("Battle", typeof(Battle)),
                    new SearchProperty("Deaths", "Deaths", typeof(List<HistoricalFigure>), true)
                };

            }
            else if (nonGenericSearchType == typeof(BeastAttack))
            {
                SearchProperties = new List<SearchProperty>(){
                    new SearchProperty("Ordinal", typeof(int)),
                    new SearchProperty("Site", "Site", typeof(Site), true),
                    new SearchProperty("Defender", typeof(Entity)),
                    new SearchProperty("Beast", typeof(HistoricalFigure)),
                    new SearchProperty("StartYear", "Year", typeof(int)),
                    new SearchProperty("Deaths", "Deaths", typeof(List<HistoricalFigure>), true)
                };
            }
            else if (nonGenericSearchType == typeof(HFDied))
            {
                SearchProperties = new List<SearchProperty>()
                {
                    new SearchProperty("Slayer", typeof(HistoricalFigure)),
                    new SearchProperty("HistoricalFigure", "Historical Figure", typeof(HistoricalFigure)),
                    new SearchProperty("Cause", typeof(DeathCause)),
                    new SearchProperty("Site", typeof(Site)),
                    new SearchProperty("Region", typeof(WorldRegion))
                };
            }
            else if (nonGenericSearchType == typeof(HFAbducted))
            {
                SearchProperties = new List<SearchProperty>()
                {
                    new SearchProperty("Snatcher", typeof(HistoricalFigure)),
                    new SearchProperty("Target", typeof(HistoricalFigure)),
                    new SearchProperty("Site", typeof(Site))
                };
            }
            else if (nonGenericSearchType == typeof(Battle.Squad))
            {
                SearchProperties = new List<SearchProperty>()
                {
                    new SearchProperty("Race", typeof(string)),
                    new SearchProperty("Numbers", typeof(int)),
                    new SearchProperty("Deaths", typeof(int))
                };
            }
            else if (nonGenericSearchType == typeof(Population))
            {
                SearchProperties = new List<SearchProperty>()
                {
                    new SearchProperty("Race", typeof(string)),
                    new SearchProperty("Count", typeof(int))
                };
            }
            else if (nonGenericSearchType == typeof(HistoricalFigure.Position))
            {
                SearchProperties = new List<SearchProperty>()
                {
                    new SearchProperty("Entity", typeof(Entity)),
                    new SearchProperty("Title", typeof(string)),
                    new SearchProperty("Began", typeof(int)),
                    new SearchProperty("Ended", typeof(int)),
                    new SearchProperty("Length", typeof(int))
                };
            }
            else if (nonGenericSearchType == typeof(HistoricalFigure.State))
            {
                SearchProperties = new List<SearchProperty>()
                {
                    new SearchProperty("HFState", "State", typeof(HFState)),
                    new SearchProperty("StartYear", "Start Year", typeof(int)),
                    new SearchProperty("EndYear", "End Year", typeof(int))
                };
            }
            else if (searchType == typeof(List<int>))
                SearchProperties = new List<SearchProperty>() { new SearchProperty("Value", typeof(int)) };
            else if (searchType == typeof(List<string>))
                SearchProperties = new List<SearchProperty>() { new SearchProperty("Value", typeof(string)) };

            if (nonGenericSearchType.BaseType == typeof(WorldObject))
                SearchProperties.Add(new SearchProperty("Events", "Events", typeof(List<WorldEvent>), false));
            if (nonGenericSearchType.BaseType == typeof(EventCollection))
                SearchProperties.Add(new SearchProperty("AllEvents", "Events", typeof(List<WorldEvent>), false));
            if (nonGenericSearchType.BaseType == typeof(WorldObject) || nonGenericSearchType.BaseType == typeof(EventCollection))
                SearchProperties.Add(new SearchProperty("FilteredEvents", "Events (Filtered)", typeof(List<WorldEvent>), false));
            if (nonGenericSearchType.BaseType == typeof(WorldEvent))
                SearchProperties.Add(new SearchProperty("Year", typeof(int)));

            foreach (SearchProperty property in SearchProperties)
            {
                if (!noSubProperties)
                //(!searchType.IsGenericType || (!noSubProperties && !property.Type.IsGenericType)))// ||  (!noSubProperties && property.Type.IsGenericType && searchType.IsGenericType && searchType.GetGenericArguments()[0] != property.Type.GetGenericArguments()[0])))
                {
                    property.SetSubProperties();
                }
            }

            return SearchProperties;
        }

        public static List<QueryComparer> GetComparers(Type type)
        {
            List<QueryComparer> comparers = new List<QueryComparer>();
            if (type == null) return comparers;
            if (type == typeof(string))
                comparers = new List<QueryComparer>() { QueryComparer.Equals, QueryComparer.Contains, QueryComparer.StartsWith, QueryComparer.EndsWith, QueryComparer.NotEqual, QueryComparer.NotContains, QueryComparer.NotStartsWith, QueryComparer.NotEndsWith };
            else if (type == typeof(int) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>)) || type == typeof(double))
                comparers = new List<QueryComparer>() { QueryComparer.GreaterThan, QueryComparer.LessThan, QueryComparer.Equals };
            else if (type == typeof(bool) || type == typeof(DeathCause) || type == typeof(SiteConqueredType)
                      || type == typeof(BattleOutcome) || type == typeof(HFState))
                comparers = new List<QueryComparer>() { QueryComparer.Equals, QueryComparer.NotEqual };
            return comparers;
        }

        public static string ComparerToString(QueryComparer comparer)
        {
            switch (comparer)
            {
                case QueryComparer.Contains: return "Contains";
                case QueryComparer.EndsWith: return "Ends With";
                case QueryComparer.Equals: return "Equals";
                case QueryComparer.NotEqual: return "Doesn't Equal";
                case QueryComparer.GreaterThan: return "Greater Than";
                case QueryComparer.Is: return "Is";
                case QueryComparer.LessThan: return "Less Than";
                case QueryComparer.StartsWith: return "Starts With";
                case QueryComparer.NotStartsWith: return "Doesn't Start With";
                case QueryComparer.NotEndsWith: return "Doesn't End With";
                case QueryComparer.NotContains: return "Doesn't Contain";
                default: return comparer.ToString();
            }
        }

        public static QueryComparer StringToComparer(string comparer)
        {
            switch (comparer)
            {
                case "Contains": return QueryComparer.Contains;
                case "Ends With": return QueryComparer.EndsWith;
                case "Equals": return QueryComparer.Equals;
                case "Greater Than": return QueryComparer.GreaterThan;
                case "Is": return QueryComparer.Is;
                case "Less Than": return QueryComparer.LessThan;
                case "Starts With": return QueryComparer.StartsWith;
                case "Doesn't Equal": return QueryComparer.NotEqual;
                default: return QueryComparer.All;
            }
        }
    }
}
