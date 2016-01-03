using System;
using System.Collections.Generic;

namespace LegendsViewer.Legends
{
    public class EntityReputation
    {
        public Entity Entity { get; set; }
        public int UnsolvedMurders { get; set; }
        public int FirstSuspectedAgelessYear { get; set; }
        public string FirstSuspectedAglessSeason { get; set; }

        public EntityReputation(List<Property> properties, World world)
        {
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "entity_id": Entity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "unsolved_murders": UnsolvedMurders = Convert.ToInt32(property.Value); break;
                    case "first_ageless_year": FirstSuspectedAgelessYear = Convert.ToInt32(property.Value); break;
                    case "first_ageless_season_count": FirstSuspectedAglessSeason = Formatting.TimeCountToSeason(Convert.ToInt32(property.Value)); break;
                    default:
                        break;
                }
            }
        }
    }
}
