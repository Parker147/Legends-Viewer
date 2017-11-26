using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class ArtifactGiven : WorldEvent
    {
        public Artifact Artifact { get; set; }
        public HistoricalFigure HistoricalFigureGiver { get; set; }
        public HistoricalFigure HistoricalFigureReceiver { get; set; }
        public Entity EntityGiver { get; set; }
        public Entity EntityReceiver { get; set; }

        public ArtifactGiven(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "artifact_id": Artifact = world.GetArtifact(Convert.ToInt32(property.Value)); break;
                    case "giver_hist_figure_id": HistoricalFigureGiver = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "receiver_hist_figure_id": HistoricalFigureReceiver = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "giver_entity_id": EntityGiver = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "receiver_entity_id": EntityReceiver = world.GetEntity(Convert.ToInt32(property.Value)); break;
                }
            }

            Artifact.AddEvent(this);
            HistoricalFigureGiver.AddEvent(this);
            HistoricalFigureReceiver.AddEvent(this);
            EntityGiver.AddEvent(this);
            EntityReceiver.AddEvent(this);
        }

        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime();
            eventString += Artifact.ToLink(link, pov);
            eventString += " was offered to ";
            eventString += HistoricalFigureReceiver.ToLink(link, pov);
            if (EntityReceiver != null)
            {
                eventString += " of ";
                eventString += EntityReceiver.ToLink(link, pov);
            }
            eventString += " by ";
            eventString += HistoricalFigureGiver.ToLink(link, pov);
            if (EntityGiver != null)
            {
                eventString += " of ";
                eventString += EntityGiver.ToLink(link, pov);
            }
            eventString += PrintParentCollection(link, pov);
            eventString += ".";
            return eventString;
        }
    }
}
