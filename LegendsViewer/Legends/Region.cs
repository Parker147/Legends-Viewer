using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LegendsViewer.Legends
{
    public class WorldRegion : WorldObject
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<string> Deaths
        {
            get
            {
                List<string> deaths = new List<string>();
                deaths.AddRange(NotableDeaths.Select(death => death.Race));
                foreach (Battle.Squad squad in Battles.SelectMany(battle => battle.AttackerSquads.Concat(battle.DefenderSquads)))
                    for (int i = 0; i < squad.Deaths; i++)
                        deaths.Add(squad.Race);
                return deaths;
            }
            set { }
        }
        public List<HistoricalFigure> NotableDeaths { get { return Events.OfType<HFDied>().Select(death => death.HistoricalFigure).ToList(); } set { } }
        public List<Battle> Battles { get; set; }
        public List<Location> Coordinates { get; set; }
        public static List<string> Filters;
        public override List<WorldEvent> FilteredEvents
        {
            get { return Events.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }
        public WorldRegion()
        {
            Name = "INVALID REGION"; Type = "INVALID";
            Battles = new List<Battle>();
        }
        public WorldRegion(List<Property> properties, World world)
            : base(properties, world)
        {
            Battles = new List<Battle>();
            Coordinates = new List<Location>();
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "name": Name = Formatting.InitCaps(property.Value); break;
                    case "type": Type = String.Intern(property.Value); break;
                    case "coords":
                        string[] coordinateStrings = property.Value.Split(new char[] {'|'},
                            StringSplitOptions.RemoveEmptyEntries);
                        foreach(var coordinateString in coordinateStrings)
                        {
                            string[] xYCoordinates = coordinateString.Split(',');
                            int x = Convert.ToInt32(xYCoordinates[0]);
                            int y = Convert.ToInt32(xYCoordinates[1]);
                            Coordinates.Add(new Location(x, y));
                        }
                    break;
                }
        }
        public override string ToString() { return this.Name; }
        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            if (link)
            {
                if (pov != this)
                {
                    string title = Type + " | Events: " + Events.Count;
                    return "<a href = \"region#" + this.ID + "\" title=\"" + title + "\">" + this.Name + "</a>";
                }
                else
                    return "<font color=\"Blue\">" + this.Name + "</font>";
            }
            else
                return this.Name;
        }
    }
}
