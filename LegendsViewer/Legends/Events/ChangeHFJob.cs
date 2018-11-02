using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class ChangeHfJob : WorldEvent
    {
        public HistoricalFigure HistoricalFigure;
        public Site Site;
        public WorldRegion Region;
        public UndergroundRegion UndergroundRegion;
        public string NewJob { get; set; }
        public string OldJob { get; set; }
        public ChangeHfJob(List<Property> properties, World world)
            : base(properties, world)
        {
            NewJob = "UNKNOWN JOB";
            OldJob = "UNKNOWN JOB";
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "hfid": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                    case "new_job": NewJob = string.Intern(property.Value.Replace("_", " ")); break;
                    case "old_job": OldJob = string.Intern(property.Value.Replace("_", " ")); break;
                    case "site": if (Site == null) { Site = world.GetSite(Convert.ToInt32(property.Value)); } else { property.Known = true; } break;
                }
            }

            HistoricalFigure.AddEvent(this);
            Site.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime() + HistoricalFigure.ToLink(link, pov);
            if (OldJob != "standard" && NewJob != "standard")
            {
                eventString += " gave up being a " + OldJob + " to become a " + NewJob;
            }
            else if (NewJob != "standard")
            {
                eventString += " became a " + NewJob;
            }
            else if (OldJob != "standard")
            {
                eventString += " stopped being a " + OldJob;
            }
            else
            {
                eventString += " became a peasant";
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