using System.Collections.Generic;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class PeaceAccepted : PeaceEfforts
    {
        public PeaceAccepted(List<Property> properties, World world)
            : base(properties, world)
        {
            Decision = "accepted";
        }
    }
}