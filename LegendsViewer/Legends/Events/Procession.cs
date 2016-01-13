using System.Collections.Generic;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class Procession : OccasionEvent
    {
        public Procession(List<Property> properties, World world) : base(properties, world)
        {
            OccasionType = OccasionType.Procession;
        }
    }
}