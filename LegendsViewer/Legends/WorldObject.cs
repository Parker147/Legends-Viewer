using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsViewer.Legends
{
    public abstract class WorldObject : DwarfObject
    {
        public List<WorldEvent> Events { get; set; }
        public int ID { get; set; }
        protected World World;
        protected WorldObject(List<Property> properties, World world)
        {
            World = world;
            ID = -1;
            Events = new List<WorldEvent>();
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "id": ID = Convert.ToInt32(property.Value); property.Known = true; break;
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
