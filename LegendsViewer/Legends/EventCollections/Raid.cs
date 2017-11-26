using System;
using System.Collections.Generic;
using System.Linq;
using LegendsViewer.Legends.Events;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.EventCollections
{
    public class Raid : EventCollection
    {
        public int Ordinal { get; set; }
        public Location Coordinates { get; set; }
        public WorldRegion Region { get; set; }
        public UndergroundRegion UndergroundRegion { get; set; }
        public Site Site { get; set; }
        public EventCollection ParentEventCol { get; set; }

        public static List<string> Filters;
        public override List<WorldEvent> FilteredEvents
        {
            get { return AllEvents.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }
        public Raid(List<Property> properties, World world)
            : base(properties, world)
        {
            Initialize();

            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "ordinal": Ordinal = Convert.ToInt32(property.Value); break;
                    case "coords": Coordinates = Formatting.ConvertToLocation(property.Value); break;
                    case "parent_eventcol": ParentEventCol = world.GetEventCollection(Convert.ToInt32(property.Value)); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            }
        }

        private void Initialize()
        {
            Ordinal = 1;
            Coordinates = new Location(0, 0);
        }

        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            return "a raid";
        }

        public override string ToString()
        {
            return ToLink(false);
        }
    }
}
