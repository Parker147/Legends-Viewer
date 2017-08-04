using System.Collections.Generic;
using System.Linq;
using LegendsViewer.Legends.Events;
using LegendsViewer.Legends.Parser;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Controls.HTML.Utilities;
using System;
using LegendsViewer.Controls;

namespace LegendsViewer.Legends
{
    public class Structure : WorldObject
    {
        public string Name { get; set; } // legends_plus.xml
        public string AltName { get; set; } // legends_plus.xml
        public StructureType Type { get; set; } // legends_plus.xml
        public List<int> InhabitantIDs { get; set; } // legends_plus.xml
        public List<HistoricalFigure> Inhabitants { get; set; } // legends_plus.xml
        public int DeityID { get; set; } // legends_plus.xml
        public HistoricalFigure Deity { get; set; } // legends_plus.xml
        public int ReligionID { get; set; } // legends_plus.xml
        public Entity Religion { get; set; } // legends_plus.xml
        public DungeonType DungeonType { get; set; } // legends_plus.xml
        public string TypeAsString { get { return Type.GetDescription(); } set { } }
        public string Icon { get; set; }

        public Site Site { get; set; }

        public int GlobalID { get; set; }

        public static List<string> Filters;
        public override List<WorldEvent> FilteredEvents
        {
            get { return Events.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }

        public Structure(List<Property> properties, World world, Site site)
            : base(properties, world)
        {
            Name = "UNKNOWN STRUCTURE";
            InhabitantIDs = new List<int>();
            DeityID = -1;
            ReligionID = -1;

            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "name": Name = Formatting.InitCaps(property.Value); break;
                    case "name2": AltName = Formatting.InitCaps(property.Value); break;
                    case "inhabitant": InhabitantIDs.Add(Convert.ToInt32(property.Value)); break;
                    case "deity": DeityID = Convert.ToInt32(property.Value); break;
                    case "religion": ReligionID = Convert.ToInt32(property.Value); break;
                    case "dungeon_type":
                        switch (property.Value)
                        {
                            case "0": DungeonType = DungeonType.Dungeon; break;
                            case "1": DungeonType = DungeonType.Sewers; break;
                            case "2": DungeonType = DungeonType.Catacombs; break;
                            default:
                                property.Known = false;
                                break;
                        }
                        break;
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
                                property.Known = false;
                                break;
                        }
                        break;
                }
            }
            string icon = "";
            switch (Type)
            {
                case StructureType.MeadHall:
                    icon = "<i class=\"fa fa-fw fa-beer\"></i>";
                    break;
                case StructureType.Market:
                    icon = "<i class=\"fa fa-fw fa-balance-scale\"></i>";
                    break;
                case StructureType.Keep:
                    icon = "<i class=\"fa fa-fw fa-fort-awesome\"></i>";
                    break;
                case StructureType.Temple:
                    icon = "<i class=\"fa fa-fw fa-university\"></i>";
                    break;
                case StructureType.Dungeon:
                    icon = "<i class=\"fa fa-fw fa-magnet fa-flip-vertical\"></i>";
                    break;
                case StructureType.InnTavern:
                    icon = "<i class=\"fa fa-fw fa-cutlery\"></i>";
                    break;
                case StructureType.Tomb:
                    icon = "<i class=\"fa fa-fw fa-youtube-play fa-rotate-270\"></i>";
                    break;
                case StructureType.UnderworldSpire:
                    icon = "<i class=\"fa fa-fw fa-indent fa-rotate-270\"></i>";
                    break;
                case StructureType.Library:
                    icon = "<i class=\"fa fa-fw fa-graduation-cap\"></i>";
                    break;
                default:
                    icon = "";
                    break;
            }
            Icon = icon;
            Site = site;

            GlobalID = world.Structures.Count;
            world.Structures.Add(this);
        }

        public void Resolve(World world)
        {
            Inhabitants = new List<HistoricalFigure>();
            if (InhabitantIDs.Any())
            {
                foreach (int InhabitantID in InhabitantIDs)
                {
                    Inhabitants.Add(world.GetHistoricalFigure(InhabitantID));
                }
            }
            if (DeityID != -1)
            {
                Deity = world.GetHistoricalFigure(DeityID);
            }
            if (ReligionID != -1)
            {
                Religion = world.GetEntity(ReligionID);
            }
            if (Deity != null && Religion != null)
            {
                if (!Religion.Worshipped.Contains(Deity))
                {
                    Religion.Worshipped.Add(Deity);
                    Deity.DedicatedStructures.Add(this);
                }
            }
        }


        public override string ToString() { return Name; }

        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            if (link)
            {
                string title = "";
                if (DungeonType != DungeonType.Unknown)
                {
                    title += DungeonType.GetDescription();
                }
                else
                {
                    title += Type.GetDescription();
                }
                title += "&#13";
                title += "Events: " + Events.Count;

                string linkedString = "";
                if (pov != this)
                {
                    linkedString = Icon + "<a href = \"structure#" + GlobalID + "\" title=\"" + title + "\">" + Name + "</a>";
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
