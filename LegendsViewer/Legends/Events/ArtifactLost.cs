using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class ArtifactLost : WorldEvent
    {
        public Artifact Artifact { get; set; }
        public Site Site { get; set; }
        public ArtifactLost(List<Property> properties, World world) : base(properties, world)
        {
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "artifact_id": Artifact = world.GetArtifact(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            }

            Artifact.AddEvent(this);
            Site.AddEvent(this);
        }

        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime();
            eventString += Artifact != null ? Artifact.ToLink(link, pov) : "UNKNOWN ARTIFACT";
            eventString += " was lost in ";
            eventString += Site != null ? Site.ToLink(link, pov) : "an unknown site";
            eventString += PrintParentCollection(link, pov);
            eventString += ".";
            return eventString;
        }
    }
}