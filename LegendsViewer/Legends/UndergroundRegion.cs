using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsViewer.Legends
{
    public class UndergroundRegion : WorldObject
    {
        public int Depth { get; set; }
        public string Type { get; set; }
        public List<Battle> Battles { get; set; }
        public static List<string> Filters;
        public override List<WorldEvent> FilteredEvents
        {
            get { return Events.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }
        public UndergroundRegion() { Type = "INVALID UNDERGROUND REGION"; Depth = 0; Battles = new List<Battle>(); }
        public UndergroundRegion(List<Property> properties, World world)
            : base(properties, world)
        {
            Depth = 0;
            Type = "";
            Battles = new List<Battle>();
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "depth": Depth = Convert.ToInt32(property.Value); break;
                    case "type": Type = Formatting.InitCaps(property.Value); break;
                }
        }
        public override string ToString() { return this.Type; }
        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            string name;
            if (this.Type == "Cavern") name = "the depths of the world";
            else if (this.Type == "Underworld") name = "the Underworld";
            else name = "an underground region (" + this.Type + ")";

            if (link)
            {
                if (pov != this)
                    return "<a href = \"uregion#" + this.ID + "\">" + name + "</a>";
                else
                    return "<font color=\"Blue\">" + name + "</font>";
            }
            else
                return name;
        }
        
    }
}
