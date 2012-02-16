using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsViewer.Legends
{
    public class Artifact : WorldObject
    {
        public string Name, Item;
        public List<string> Filters;
        public override List<WorldEvent> FilteredEvents
        {
            get { return Events.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }
        public Artifact(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "name": Name = property.Value; break;
                    case "item": Item = property.Value; break;
                  
                }
        }
        public Artifact() { Name = "INVALID ARTIFACT"; Item = "INVALID ITEM"; }
    }
}
