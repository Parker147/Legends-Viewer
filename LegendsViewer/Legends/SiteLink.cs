using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends
{
    public class SiteLink
    {
        public SiteLinkType Type { get; set; }
        public Site Site { get; set; }
        public int SubId { get; set; }
        public int OccupationId { get; set; }
        public Entity Entity { get; set; }
        public SiteLink(List<Property> properties, World world)
        {
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "link_type":
                        switch(property.Value)
                        {
                            case "lair": Type = SiteLinkType.Lair; break;
                            case "hangout": Type = SiteLinkType.Hangout; break;
                            case "home site building": Type = SiteLinkType.HomeSiteBuilding; break;
                            case "home site underground": Type = SiteLinkType.HomeSiteUnderground; break;
                            case "home structure": Type = SiteLinkType.HomeStructure; break;
                            case "seat of power": Type = SiteLinkType.SeatOfPower; break;
                            case "occupation": Type = SiteLinkType.Occupation; break;
                            default: 
                                Type = SiteLinkType.Unknown;
                                world.ParsingErrors.Report("Unknown Site Link Type: " + property.Value);
                                break;
                        }
                        break;
                    case "site_id":
                        Site = world.GetSite(Convert.ToInt32(property.Value)); 
                        break;
                    case "sub_id":
                        SubId = Convert.ToInt32(property.Value);
                        break;
                    case "entity_id":
                        Entity = world.GetEntity(Convert.ToInt32(property.Value));
                        break;
                    case "occupation_id":
                        OccupationId = Convert.ToInt32(property.Value);
                        break;
                }
            }
        }
    }
}
