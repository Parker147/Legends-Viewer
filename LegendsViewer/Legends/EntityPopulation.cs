using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace LegendsViewer.Legends
{
    public class EntityPopulation : WorldObject
    {
        public string Race { get; set; }
        public int Count { get; set; }
        public Entity Entity { get; set; }
    
        public static List<string> Filters;
        public override List<WorldEvent> FilteredEvents
        {
            get { return Events.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }
        public EntityPopulation(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "race":
                        var raceCount = property.Value.Split(':');
                        Race = raceCount[0];
                        Count = Convert.ToInt32(raceCount[1]);
                        break;
                    case "civ_id":
                        Entity = world.GetEntity(property.ValueAsInt());
                        break;
                }
            }
        }
    }
}
