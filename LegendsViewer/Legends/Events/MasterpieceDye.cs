using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class MasterpieceDye : WorldEvent
    {
        private int SkillAtTime { get; set; }
        public HistoricalFigure Maker { get; set; }
        public Entity MakerEntity { get; set; }
        public Site Site { get; set; }
        public string ItemType { get; set; }
        public string ItemSubType { get; set; }
        public string Material { get; set; }
        public int MaterialType { get; set; }
        public int MaterialIndex { get; set; }
        public string DyeMaterial { get; set; }
        public int DyeMaterialType { get; set; }
        public int DyeMaterialIndex { get; set; }

        public MasterpieceDye(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "skill_at_time": SkillAtTime = Convert.ToInt32(property.Value); break;
                    case "hfid": Maker = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "entity_id": MakerEntity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "maker": if (Maker == null) { Maker = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); } else { property.Known = true; } break;
                    case "maker_entity": if (MakerEntity == null) { MakerEntity = world.GetEntity(Convert.ToInt32(property.Value)); } else { property.Known = true; } break;
                    case "site": if (Site == null) { Site = world.GetSite(Convert.ToInt32(property.Value)); } else { property.Known = true; } break;
                    case "item_type": ItemType = property.Value.Replace("_", " "); break;
                    case "item_subtype": ItemSubType = property.Value.Replace("_", " "); break;
                    case "mat": Material = property.Value.Replace("_", " "); break;
                    case "mat_type": MaterialType = Convert.ToInt32(property.Value); break;
                    case "mat_index": MaterialIndex = Convert.ToInt32(property.Value); break;
                    case "dye_mat": DyeMaterial = property.Value.Replace("_", " "); break;
                    case "dye_mat_type": DyeMaterialType = Convert.ToInt32(property.Value); break;
                    case "dye_mat_index": DyeMaterialIndex = Convert.ToInt32(property.Value); break;
                }
            }
            Maker.AddEvent(this);
            MakerEntity.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime();
            eventString += Maker != null ? Maker.ToLink(link, pov) : "UNKNOWN HISTORICAL FIGURE";
            eventString += " masterfully dyed a ";
            eventString += !string.IsNullOrWhiteSpace(Material) ? Material + " " : "";
            if (!string.IsNullOrWhiteSpace(ItemSubType) && ItemSubType != "-1")
            {
                eventString += ItemSubType;
            }
            else
            {
                eventString += !string.IsNullOrWhiteSpace(ItemType) ? ItemType : "UNKNOWN ITEM";
            }
            eventString += " with ";
            eventString += !string.IsNullOrWhiteSpace(DyeMaterial) ? DyeMaterial : "UNKNOWN DYE";
            eventString += " for ";
            eventString += MakerEntity != null ? MakerEntity.ToLink(link, pov) : "UNKNOWN ENTITY";
            eventString += " in ";
            eventString += Site != null ? Site.ToLink(link, pov) : "UNKNOWN SITE";
            eventString += PrintParentCollection(link, pov);
            eventString += ".";
            return eventString;
        }
    }
}