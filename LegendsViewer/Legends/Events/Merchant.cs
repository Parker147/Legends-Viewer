using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class Merchant : WorldEvent
    {
        public Entity Source { get; set; }
        public Entity TraderEntity { get; set; }
        public Entity DepotEntity { get; set; }
        public Entity Destination { get; set; }
        public Site Site { get; set; }

        public Merchant(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "source":
                    case "trader_entity_id":
                        if (Source == null)
                        {
                            Source = world.GetEntity(Convert.ToInt32(property.Value));
                        }
                        else
                        {
                            property.Known = true;
                        }
                        break;
                    case "destination":
                    case "depot_entity_id":
                        if (Destination == null)
                        {
                            Destination = world.GetEntity(Convert.ToInt32(property.Value));
                        }
                        else
                        {
                            property.Known = true;
                        }
                        break;
                    case "site":
                    case "site_id":
                        if (Site == null)
                        {
                            Site = world.GetSite(Convert.ToInt32(property.Value));
                        }
                        else
                        {
                            property.Known = true;
                        }
                        break;
                }
            }
            Source.AddEvent(this);
            Destination.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime();
            eventString += "merchants from ";
            eventString += Source != null ? Source.ToLink(link, pov) : "UNKNOWN CIV";
            eventString += " visited ";
            eventString += Destination != null ? Destination.ToLink(link, pov) : "UNKNOWN ENTITY";
            eventString += " at ";
            eventString += Site != null ? Site.ToLink(link, pov) : "UNKNOWN SITE";
            eventString += ".";
            return eventString;
        }
    }
}