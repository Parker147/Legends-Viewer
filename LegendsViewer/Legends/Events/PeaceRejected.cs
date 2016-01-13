using System.Collections.Generic;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class PeaceRejected : PeaceEfforts
    {
        public PeaceRejected(List<Property> properties, World world)
            : base(properties, world)
        {
            Decision = "rejected";
        }
    }
}