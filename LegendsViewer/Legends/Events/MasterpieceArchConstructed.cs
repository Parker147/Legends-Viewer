using System.Collections.Generic;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class MasterpieceArchConstructed : MasterpieceArch
    {
        public MasterpieceArchConstructed(List<Property> properties, World world)
            : base(properties, world)
        {
            Process = "constructed";
        }
    }
}