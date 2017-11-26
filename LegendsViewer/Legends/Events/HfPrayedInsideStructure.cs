using System;
using System.Collections.Generic;
using System.Linq;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class HfPrayedInsideStructure : WorldEvent
    {
        public HistoricalFigure HistoricalFigure { get; set; }
        public Site Site { get; set; }
        public int StructureId { get; set; }
        public Structure Structure { get; set; }


        public HfPrayedInsideStructure(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "hist_fig_id":
                        HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value));
                        break;
                    case "site_id":
                        Site = world.GetSite(Convert.ToInt32(property.Value));
                        break;
                    case "structure_id":
                        StructureId = Convert.ToInt32(property.Value);
                        break;
                }
            }

            if (Site != null)
            {
                Structure = Site.Structures.FirstOrDefault(structure => structure.LocalId == StructureId);
            }
            HistoricalFigure.AddEvent(this);
            Site.AddEvent(this);
            Structure.AddEvent(this);
        }

        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime();
            eventString += HistoricalFigure.ToLink(link, pov);
            eventString += " prayed";
            if (Structure != null)
            {
                eventString += " inside ";
                eventString += Structure.ToLink(link, pov);
            }
            if (Site != null)
            {
                eventString += " in ";
                eventString += Site.ToLink(link, pov);
            }
            eventString += PrintParentCollection(link, pov);
            eventString += ".";
            return eventString;
        }
    }
}
