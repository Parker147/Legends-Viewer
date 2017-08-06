using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class ArtifactCreated : WorldEvent
    {
        public int UnitID;
        public Artifact Artifact;
        public bool RecievedName;
        public HistoricalFigure HistoricalFigure;
        public Site Site;
        public ArtifactCreated(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach (Property property in properties)
                switch (property.Name)
                {
                    case "unit_id": UnitID = Convert.ToInt32(property.Value); break;
                    case "artifact_id": Artifact = world.GetArtifact(Convert.ToInt32(property.Value)); break;
                    case "hist_figure_id": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "name_only": RecievedName = true; property.Known = true; break;
                    case "hfid": if (HistoricalFigure == null) { HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); } else property.Known = true; break;
                    case "site": if (Site == null) { Site = world.GetSite(Convert.ToInt32(property.Value)); } else property.Known = true; break;
                }
            if (Artifact != null && HistoricalFigure != null)
            {
                Artifact.Creator = HistoricalFigure;
                //HistoricalFigure.CreatedArtifacts.Add(Artifact);
            }
            Artifact.AddEvent(this);
            HistoricalFigure.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime() + Artifact.ToLink(link, pov);
            if (RecievedName)
                eventString += " recieved its name";
            else
                eventString += " was created";
            if (Site != null)
                eventString += " in " + Site.ToLink(link, pov);
            if (RecievedName)
                eventString += " from ";
            else
                eventString += " by ";
            eventString += HistoricalFigure != null ? HistoricalFigure.ToLink(link, pov) : "UNKNOWN HISTORICAL FIGURE";
            eventString += PrintParentCollection(link, pov);
            eventString += ".";
            return eventString;
        }
    }
}