using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class EntityCreated : WorldEvent
    {
        public Entity Entity;
        public Site Site;
        public EntityCreated(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach (Property property in properties)
                switch (property.Name)
                {
                    case "entity_id": Entity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;

                    //Unhandled Events
                    case "structure_id": property.Known = true; break;
                }
            Entity.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime();
            eventString += Entity.ToLink(link, pov) + " formed in ";
            eventString += (Site != null ? Site.ToLink(link, pov) : "UNKNOWN SITE") + ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
}