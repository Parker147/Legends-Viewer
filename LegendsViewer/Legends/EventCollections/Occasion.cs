using System;
using System.Collections.Generic;
using System.Linq;

namespace LegendsViewer.Legends.EventCollections
{
    public class Occasion : EventCollection
    {
        public Entity Civ;
        public string Ordinal;

        public List<string> Filters;
        public override List<WorldEvent> FilteredEvents
        {
            get { return AllEvents.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }
        public Occasion(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach (Property property in properties)
                switch (property.Name)
                {
                    case "civ_id": Civ = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "ordinal": Ordinal = String.Intern(property.Value); break;
                    case "occasion_id":
                        // TODO
                        break;
                }
        }
        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            return "an occasion";
        }
    }
}
