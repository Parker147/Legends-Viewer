using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class BodyAbused : WorldEvent
    {
        // TODO
        public string ItemType { get; set; } // legends_plus.xml
        public string ItemSubType { get; set; } // legends_plus.xml
        public string Material { get; set; } // legends_plus.xml
        public int PileTypeID { get; set; } // legends_plus.xml
        public int MaterialTypeID { get; set; } // legends_plus.xml
        public int MaterialIndex { get; set; } // legends_plus.xml
        public int AbuseTypeID { get; set; } // legends_plus.xml

        public Entity Abuser { get; set; } // legends_plus.xml
        public HistoricalFigure Body { get; set; } // legends_plus.xml
        public HistoricalFigure HistoricalFigure { get; set; } // legends_plus.xml
        public Site Site { get; set; }
        public WorldRegion Region { get; set; }
        public UndergroundRegion UndergroundRegion { get; set; }
        public Location Coordinates { get; set; }
        public BodyAbused(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach (Property property in properties)
                switch (property.Name)
                {
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "coords": Coordinates = Formatting.ConvertToLocation(property.Value); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                    case "site": if (Site == null) { Site = world.GetSite(Convert.ToInt32(property.Value)); } else property.Known = true; break;
                    case "civ": Abuser = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "bodies": Body = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "histfig": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "props_item_type": ItemType = property.Value; break;
                    case "props_item_subtype": ItemSubType = property.Value; break;
                    case "props_item_mat": Material = property.Value; break;
                    case "abuse_type": AbuseTypeID = Convert.ToInt32(property.Value); break;
                    case "props_pile_type": PileTypeID = Convert.ToInt32(property.Value); break;
                    case "props_item_mat_type": MaterialTypeID = Convert.ToInt32(property.Value); break;
                    case "props_item_mat_index": MaterialIndex = Convert.ToInt32(property.Value); break;
                }
            Site.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
            Body.AddEvent(this);
            HistoricalFigure.AddEvent(this);
            Abuser.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime();
            if (HistoricalFigure != null)
            {
                eventString += "the body of ";
                if (Body != null)
                {
                    eventString += Body.ToLink(link, pov);
                }
                else
                {
                    eventString += "UNKNOWN HISTORICAL FIGURE";
                }
                eventString += " was animated by ";
                eventString += HistoricalFigure.ToLink(link, pov);
            }
            else
            {
                if (Body != null)
                {
                    eventString += Body.ToLink(link, pov);
                }
                else
                {
                    eventString += "UNKNOWN HISTORICAL FIGURE";
                }
                eventString += "'s body was abused by ";
                if (Abuser != null)
                {
                    eventString += Abuser.ToLink(link, pov);
                }
                else
                {
                    eventString += "UNKNOWN ENTITY";
                }
            }
            if (Site != null)
            {
                eventString += " in " + Site.ToLink(link, pov);
            }
            eventString += ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
}