using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class ArtifactPossessed : WorldEvent
    {
        public Artifact Artifact { get; set; }
        public int UnitId { get; set; }
        public HistoricalFigure HistoricalFigure { get; set; }
        public Site Site { get; set; }
        public WorldRegion Region { get; set; }
        public UndergroundRegion UndergroundRegion { get; set; }

        public ArtifactPossessed(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "artifact_id": Artifact = world.GetArtifact(Convert.ToInt32(property.Value)); break;
                    case "unit_id": UnitId = Convert.ToInt32(property.Value); break;
                    case "hist_figure_id": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                }
            }

            Artifact.AddEvent(this);
            HistoricalFigure.AddEvent(this);
            Site.AddEvent(this);
        }

        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime() + Artifact.ToLink(link, pov) + " was claimed";
            if (Site != null)
            {
                eventString += " in " + Site.ToLink(link, pov);
            }
            else if (Region != null)
            {
                eventString += Region.ToLink(link, pov);
            }
            else if (UndergroundRegion != null)
            {
                eventString += UndergroundRegion.ToLink(link, pov);
            }

            eventString += " by " + HistoricalFigure.ToLink(link, pov);
            eventString += PrintParentCollection(link, pov);
            eventString += ".";
            return eventString;
        }
    }
}