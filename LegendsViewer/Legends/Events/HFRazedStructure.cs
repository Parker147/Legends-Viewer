using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class HFRazedStructure : WorldEvent
    {
        public int StructureID;
        public HistoricalFigure HistoricalFigure;
        public Site Site;

        public HFRazedStructure(List<Property> properties, World world) : base(properties, world)
        {
            foreach (Property property in properties)
                switch (property.Name)
                {
                    case "structure_id":
                        StructureID = Convert.ToInt32(property.Value);
                        break;
                    case "hist_fig_id":
                        HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value));
                        break;
                    case "site_id":
                        Site = world.GetSite(Convert.ToInt32(property.Value));
                        break;
                }
            HistoricalFigure.AddEvent(this);
            Site.AddEvent(this);
        }

        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + HistoricalFigure.ToLink(link, pov) + " razed a (" + StructureID + ") in " + Site.ToLink(link, pov) + ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
}