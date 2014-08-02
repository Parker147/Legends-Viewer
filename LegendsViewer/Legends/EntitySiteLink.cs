using System.Collections.Generic;

namespace LegendsViewer.Legends
{
    public class EntitySiteLink : DwarfObject
    {
        public EntitySiteLink(List<Property> properties, World world)
        {
            Type = EntitySiteLinkType.Unknown;
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "type":
                        switch (property.Value)
                        {
                            default: 
                                //All unknown for the time being.
                                //world.ParsingErrors.Report("Unknown Entity Site Link Type: " + property.Value);
                                break;
                        }
                        break;
                    case "site":
                        Site = world.GetSite(property.ValueAsInt());
                        break;
                    case "strength":
                        Strength = property.ValueAsInt();
                        break;
                }
            }
        }

        public EntitySiteLinkType Type { get; set; }
        public Site Site { get; set; }
        public int Strength { get; set; }
    }
}