using System.Text;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls
{
    public class WorldConstructionPrinter : HTMLPrinter
    {
        WorldConstruction WorldContruction;

        public WorldConstructionPrinter(WorldConstruction worldContruction)
        {
            WorldContruction = worldContruction;
        }

        public override string Print()
        {
            HTML = new StringBuilder();
            HTML.AppendLine("<h1>" + WorldContruction.Name + "</h1><br />");

            PrintEventLog(WorldContruction.Events, WorldConstruction.Filters, WorldContruction);
            return HTML.ToString();
        }

        public override string GetTitle()
        {
            return WorldContruction.Name;
        }
    }
}
