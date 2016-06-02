using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Events;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends
{
    public abstract class WorldObject : DwarfObject
    {
        public List<WorldEvent> Events { get; set; }
        public int EventCount { get { return Events.Count; } set { } }
        public int ID { get; set; }
        protected WorldObject(List<Property> properties, World world)
        {
            ID = -1;
            Events = new List<WorldEvent>();
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "id": ID = Convert.ToInt32(property.Value); break;
                    default: break;
                }
        }
        public WorldObject() { 
            ID = -1; 
            Events = new List<WorldEvent>(); 
        }
        

        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            return "";
        }
        public abstract List<WorldEvent> FilteredEvents { get; }

    }
}
