using System.Collections.Generic;
using System.Linq;
using LegendsViewer.Legends.Events;
using LegendsViewer.Legends.Parser;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Controls.HTML.Utilities;

namespace LegendsViewer.Legends
{
    public class Structure : WorldObject
    {
        public string Name { get; set; } // legends_plus.xml
        public string AltName { get; set; } // legends_plus.xml
        public StructureType Type { get; set; } // legends_plus.xml
        public static List<string> Filters;
        public string Icon { get; set; }
        public override List<WorldEvent> FilteredEvents
        {
            get { return Events.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }

        public Structure(List<Property> properties, World world)
            : base(properties, world)
        {
            Name = "UNKNOWN STRUCTURE";
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "name": Name = Formatting.InitCaps(property.Value); break;
                    case "name2": AltName = Formatting.InitCaps(property.Value); break;
                    case "type":
                        switch (property.Value)
                        {
                            case "mead_hall": Type = StructureType.MeadHall; break;
                            case "market": Type = StructureType.Market; break;
                            case "keep": Type = StructureType.Keep; break;
                            case "temple": Type = StructureType.Temple; break;
                            case "dungeon": Type = StructureType.Dungeon; break;
                            case "tomb": Type = StructureType.Tomb; break;
                            case "inn_tavern": Type = StructureType.InnTavern; break;
                            case "underworld_spire": Type = StructureType.UnderworldSpire; break;
                            case "library": Type = StructureType.Library; break;
                            default:
                                world.ParsingErrors.Report("Unknown Structure StructureType: " + property.Value);
                                break;
                        }
                        break;
                }
            }
            string icon = "";
            switch (Type)
            {
                case StructureType.MeadHall:
                    icon = "<i class=\"fa fa-fw fa-beer\"></i> ";
                    break;
                case StructureType.Market:
                    icon = "<i class=\"fa fa-fw fa-balance-scale\"></i> ";
                    break;
                case StructureType.Keep:
                    icon = "<i class=\"fa fa-fw fa-fort-awesome\"></i> ";
                    break;
                case StructureType.Temple:
                    icon = "<i class=\"fa fa-fw fa-university\"></i> ";
                    break;
                case StructureType.Dungeon:
                    icon = "<i class=\"fa fa-fw fa-magnet fa-flip-vertical\"></i> ";
                    break;
                case StructureType.InnTavern:
                    icon = "<i class=\"fa fa-fw fa-cutlery\"></i> ";
                    break;
                case StructureType.Tomb:
                    icon = "<i class=\"fa fa-fw fa-youtube-play fa-rotate-270\"></i> ";
                    break;
                case StructureType.UnderworldSpire:
                    icon = "<i class=\"fa fa-fw fa-indent fa-rotate-270\"></i> ";
                    break;
                case StructureType.Library:
                    icon = "<i class=\"fa fa-fw fa-graduation-cap\"></i> ";
                    break;
                default:
                    icon = "";
                    break;
            }
            Icon = icon;
        }

        public override string ToString() { return Name; }

        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            if (link)
            {
                string title = Type.GetDescription();
                title += "&#13";
                title += "Events: " + Events.Count;

                string linkedString = "";
                if (pov != this)
                {
                    linkedString = Icon + "<a title=\"" + title + "\">" + Name + "</a>";
                }
                else
                {
                    linkedString = Icon + "<a title=\"" + title + "\">" + HTMLStyleUtil.CurrentDwarfObject(Name) + "</a>";
                }
                return linkedString;
            }
            else
            {
                return Icon + Name;
            }
        }
    }
}
