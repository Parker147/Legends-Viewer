using System.Collections.Generic;
using LegendsViewer.Legends.Parser;
using System;

namespace LegendsViewer.Legends
{
    public class RelationshipProfileHF
    {
        public int MeetCount { get; set; }
        public int LastMeetYear { get; set; }
        public int LastMeetSeconds72 { get; set; }
        public int HistoricalFigureID { get; set; }
        public int KnownIdentityID { get; set; } // TODO find the purpose of this property
        public List<Reputation> Reputations { get; set; }

        public RelationshipProfileHF(List<Property> properties)
        {
            Reputations = new List<Reputation>();
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "meet_count": MeetCount = Convert.ToInt32(property.Value); break;
                    case "last_meet_year": LastMeetYear = Convert.ToInt32(property.Value); break;
                    case "last_meet_seconds72": LastMeetSeconds72 = Convert.ToInt32(property.Value); break;
                    case "hf_id": HistoricalFigureID = Convert.ToInt32(property.Value); break;
                    case "known_identity_id": KnownIdentityID = Convert.ToInt32(property.Value); break;
                    default:
                        Reputations.Add(new Reputation(property));
                        break;
                }
            }
        }
    }
}
