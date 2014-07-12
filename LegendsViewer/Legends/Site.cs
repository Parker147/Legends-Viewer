using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Controls;

namespace LegendsViewer.Legends
{
    public class Site : WorldObject
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string UntranslatedName { get; set; }
        public Location Coordinates { get; set; }
        public bool Structures { get; set; }
        public List<EventCollection> Warfare { get; set; }
        public List<Battle> Battles { get { return Warfare.OfType<Battle>().ToList(); } set { } }
        public List<SiteConquered> Conquerings { get { return Warfare.OfType<SiteConquered>().ToList(); } set { } }
        public List<OwnerPeriod> OwnerHistory { get; set; }
        public static List<string> Filters;
        public DwarfObject CurrentOwner
        {
            get
            {
                if (OwnerHistory.Count(site => site.EndYear == -1) > 0)
                    return OwnerHistory.First(site => site.EndYear == -1).Owner;
                else
                    return null;
            }
            set { }
        }
        public List<DwarfObject> PreviousOwners { get { return OwnerHistory.Where(site => site.EndYear >= 0).Select(site => site.Owner).ToList(); } set { } }
        public List<Site> Connections { get; set; }
        public List<Population> Populations { get; set; }
        public List<string> PopulationsAsList
        {
            get
            {
                List<string> populations = new List<string>();
                foreach (Population population in Populations)
                    for (int i = 0; i < population.Count; i++)
                        populations.Add(population.Race);
                return populations;
            }
            set { }
        }
        public List<Official> Officials { get; set; }
        public List<string> Deaths
        {
            get
            {
                List<string> deaths = new List<string>();
                deaths.AddRange(NotableDeaths.Select(death => death.Race));
                //List<Battle.Squad> squads = Battles.SelectMany(battle => battle.AttackerSquads.Concat(battle.DefenderSquads)).ToList();
                foreach (Battle.Squad squad in Battles.SelectMany(battle => battle.AttackerSquads.Concat(battle.DefenderSquads)).ToList())
                    for (int i = 0; i < squad.Deaths; i++)
                        deaths.Add(squad.Race);
                return deaths;
            }
            set { }
        }
        public List<HistoricalFigure> NotableDeaths { get { return Events.OfType<HFDied>().Select(death => death.HistoricalFigure).ToList(); } set { } }
        public List<BeastAttack> BeastAttacks { get; set; }
        public override List<WorldEvent> FilteredEvents
        {
            get { return Events.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }
        public class Official
        {
            public HistoricalFigure HistoricalFigure;
            public string Position;
            public Official(HistoricalFigure historicalFigure, string position)
            { HistoricalFigure = historicalFigure; Position = position; }
        }
        public Site()
        {
            ID = -1;
            Type = "INVALID";
            Name = "INVALID SITE";
            UntranslatedName = "";
            Warfare = new List<EventCollection>();
            OwnerHistory = new List<OwnerPeriod>();
            Connections = new List<Site>();
            Populations = new List<Population>();
            Officials = new List<Official>();
            BeastAttacks = new List<BeastAttack>();
        }

        public Site(List<Property> properties, World world)
            : base(properties, world)
        {
            Type = Name = UntranslatedName = "";
            Warfare = new List<EventCollection>();
            OwnerHistory = new List<OwnerPeriod>();
            Connections = new List<Site>();
            Populations = new List<Population>();
            Officials = new List<Official>();
            BeastAttacks = new List<BeastAttack>();
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "type": Type = Formatting.InitCaps(property.Value); break;
                    case "name": Name = Formatting.InitCaps(property.Value); break;
                    case "coords": Coordinates = Formatting.ConvertToLocation(property.Value); break;
                    case "structures": Structures = true; property.Known = true; break;
                }
        }

        public void AddConnection(Site connection)
        {
            if (!Connections.Contains(connection)) Connections.Add(connection);
        }

        public override string ToString() { return this.Name; }

        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            if (link)
            {
                if (pov != this)
                {
                    string title = Type + "&#13Events: " + Events.Count;
                    return "<a href = \"site#" + this.ID + "\" title=\"" + title + "\">" + this.Name + "</a>";
                }
                else
                    return "<font color=\"Blue\">" + this.Name + "</font>";
            }
            else
                return this.Name;
        }

    }
}
