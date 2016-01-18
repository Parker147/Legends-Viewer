using LegendsViewer.Legends.Parser;
using System;
using System.Collections.Generic;

namespace LegendsViewer.Legends
{
    public class EntityPositionAssignment
    {
        public int ID { get; set; }
        public HistoricalFigure HistoricalFigure { get; set; }
        public int PositionID { get; set; }
        public int SquadID { get; set; }

        public EntityPositionAssignment(List<Property> properties, World world)
        {
            ID = -1;
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "id": ID = Convert.ToInt32(property.Value); break;
                    case "histfig": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "position_id": PositionID = Convert.ToInt32(property.Value); break;
                    case "squad_id": SquadID = Convert.ToInt32(property.Value); break;
                }
            }

        }
    }
}
