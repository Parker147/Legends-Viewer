using System.Collections.Generic;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class Performance : OccasionEvent
    {
        public Performance(List<Property> properties, World world) : base(properties, world)
        {
            OccasionType = OccasionType.Performance;
        }
    }
}