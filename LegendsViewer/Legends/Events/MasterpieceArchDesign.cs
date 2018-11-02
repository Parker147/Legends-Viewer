using System.Collections.Generic;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class MasterpieceArchDesign : MasterpieceArch
    {
        public MasterpieceArchDesign(List<Property> properties, World world)
            : base(properties, world)
        {
            Process = "designed";
        }
    }
}