using System.Text;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls
{
    public class WorldConstructionPrinter : HTMLPrinter
    {
        WorldContruction WorldContruction;

        public WorldConstructionPrinter(WorldContruction worldContruction)
        {
            WorldContruction = worldContruction;
        }

        public override string Print()
        {
            HTML = new StringBuilder();
            HTML.AppendLine("<h1>" + WorldContruction.Name + "</h1><br />");

            PrintEventLog(WorldContruction.Events, WorldContruction.Filters, WorldContruction);
            return HTML.ToString();
        }

        public override string GetTitle()
        {
            return WorldContruction.Name;
        }
    }
}
