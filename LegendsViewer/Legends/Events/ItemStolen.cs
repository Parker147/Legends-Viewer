using System;
using System.Collections.Generic;
using System.Linq;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class ItemStolen : WorldEvent
    {
        public int StructureID { get; set; }
        public Structure Structure { get; set; }
        public int Item { get; set; }
        public string ItemType { get; set; }
        public string ItemSubType { get; set; }
        public string Material { get; set; }
        public int MaterialType { get; set; }
        public int MaterialIndex { get; set; }
        public HistoricalFigure Thief { get; set; }
        public Entity Entity { get; set; }
        public Site Site { get; set; }
        public Site ReturnSite { get; set; }

        public ItemStolen(List<Property> properties, World world)
            : base(properties, world)
        {
            ItemType = "UNKNOWN ITEM";
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "histfig": Thief = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "entity": Entity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "item": Item = Convert.ToInt32(property.Value); break;
                    case "item_type": ItemType = property.Value.Replace("_", " "); break;
                    case "item_subtype": ItemSubType = property.Value; break;
                    case "mat": Material = property.Value; break;
                    case "mattype": MaterialType = Convert.ToInt32(property.Value); break;
                    case "matindex": MaterialIndex = Convert.ToInt32(property.Value); break;
                    case "site": if (Site == null) { Site = world.GetSite(Convert.ToInt32(property.Value)); } else property.Known = true; break;
                    case "structure": StructureID = Convert.ToInt32(property.Value); break;
                }
            }
            if (Site != null)
            {
                Structure = Site.Structures.FirstOrDefault(structure => structure.ID == StructureID);
            }
            Thief.AddEvent(this);
            Site.AddEvent(this);
            Entity.AddEvent(this);
            Structure.AddEvent(this);
        }
        public override string Print(bool path = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime();
            eventString += " a ";
            if (!string.IsNullOrWhiteSpace(Material))
            {
                eventString += Material + " ";
            }
            eventString += ItemType;
            eventString += " was stolen ";
            if (Structure != null)
            {
                eventString += "from ";
                eventString += Structure.ToLink(path, pov);
                eventString += " ";
            }
            eventString += "in ";
            if (Site != null)
            {
                eventString += Site.ToLink(path, pov);
            }
            else
            {
                eventString += "UNKNOWN SITE";
            }
            eventString += " by ";
            if (Thief != null)
            {
                eventString += Thief.ToLink(path, pov);
            }
            else
            {
                eventString += "an unknown creature";
            }

            if (ReturnSite != null)
            {
                eventString += " and brought to " + ReturnSite.ToLink();
            }

            eventString += ". ";
            eventString += PrintParentCollection(path, pov);
            return eventString;
        }
    }
}