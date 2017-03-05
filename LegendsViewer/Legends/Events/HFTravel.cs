using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Parser;
using LegendsViewer.Legends.EventCollections;

namespace LegendsViewer.Legends.Events
{
    public class HFTravel : WorldEvent
    {
        public Location Coordinates;
        public bool Escaped, Returned;
        public HistoricalFigure HistoricalFigure;
        public Site Site;
        public WorldRegion Region;
        public UndergroundRegion UndergroundRegion;
        public HFTravel(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach (Property property in properties)
                switch (property.Name)
                {
                    case "coords": Coordinates = Formatting.ConvertToLocation(property.Value); break;
                    case "escape": Escaped = true; property.Known = true; break;
                    case "return": Returned = true; property.Known = true; break;
                    case "group_hfid": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                }
            HistoricalFigure.AddEvent(this);
            Site.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + HistoricalFigure.ToLink(link, pov);
            if (Escaped) return this.GetYearTime() + HistoricalFigure.ToLink(link, pov) + " escaped from the " + UndergroundRegion.ToLink(link, pov);
            else if (Returned) eventString += " returned to ";
            else eventString += " made a journey to ";

            if (UndergroundRegion != null) eventString += UndergroundRegion.ToLink(link, pov);
            else if (Site != null) eventString += Site.ToLink(link, pov);
            else if (Region != null) eventString += Region.ToLink(link, pov);
            if (!(ParentCollection is Journey))
            {
                eventString += PrintParentCollection(link, pov);
            }
            eventString += ".";
            return eventString;
        }
    }
}