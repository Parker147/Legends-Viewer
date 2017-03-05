using System;
using System.Collections.Generic;
using System.Linq;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class RemoveHFSiteLink : WorldEvent
    {
        public int StructureID { get; set; }
        public Structure Structure { get; set; } // TODO
        public Entity Civ { get; set; }
        public HistoricalFigure HistoricalFigure { get; set; }
        public Site Site { get; set; }
        public SiteLinkType LinkType { get; set; }

        public RemoveHFSiteLink(List<Property> properties, World world) : base(properties, world)
        {
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "structure": StructureID = Convert.ToInt32(property.Value); break;
                    case "civ": Civ = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "histfig": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "link_type":
                        switch (property.Value.Replace("_", " "))
                        {
                            case "lair": LinkType = SiteLinkType.Lair; break;
                            case "hangout": LinkType = SiteLinkType.Hangout; break;
                            case "home site building": LinkType = SiteLinkType.HomeSiteBuilding; break;
                            case "home site underground": LinkType = SiteLinkType.HomeSiteUnderground; break;
                            case "home structure": LinkType = SiteLinkType.HomeStructure; break;
                            case "seat of power": LinkType = SiteLinkType.SeatOfPower; break;
                            case "occupation": LinkType = SiteLinkType.Occupation; break;
                            case "home site realization building": LinkType = SiteLinkType.HomeSiteRealizationBuilding; break;
                            default:
                                LinkType = SiteLinkType.Unknown;
                                world.ParsingErrors.Report("Unknown Site Link Type: " + property.Value.Replace("_", " "));
                                break;
                        }
                        break;
                    case "site": if (Site == null) { Site = world.GetSite(Convert.ToInt32(property.Value)); } else property.Known = true; break;
                }
            }
            if (Site != null)
            {
                Structure = Site.Structures.FirstOrDefault(structure => structure.ID == StructureID);
            }
            HistoricalFigure.AddEvent(this);
            Civ.AddEvent(this);
            Site.AddEvent(this);
            Structure.AddEvent(this);
        }

        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime();
            if (HistoricalFigure != null)
            {
                eventString += HistoricalFigure.ToLink(link, pov);
            }
            else
            {
                eventString += "UNKNOWN HISTORICAL FIGURE";
            }
            switch (LinkType)
            {
                case SiteLinkType.HomeSiteRealizationBuilding:
                    eventString += " moved out of ";
                    break;
                case SiteLinkType.Hangout:
                    eventString += " stopped ruling from ";
                    break;
                case SiteLinkType.SeatOfPower:
                    eventString += " stopped working from ";
                    break;
                default:
                    eventString += " UNKNOWN LINKTYPE (" + LinkType + ") ";
                    break;
            }
            if (Structure != null)
            {
                eventString += Structure.ToLink(link, pov);
            }
            else
            {
                eventString += "UNKNOWN STRUCTURE";
            }
            if (Civ != null)
            {
                eventString += " of " + Civ.ToLink(link, pov);
            }
            if (Site != null)
            {
                eventString += " in " + Site.ToLink(link, pov);
            }
            eventString += PrintParentCollection(link, pov);
            eventString += ".";
            return eventString;
        }
    }
}