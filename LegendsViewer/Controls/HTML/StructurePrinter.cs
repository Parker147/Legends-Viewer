
using System.Text;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls
{
    public class StructurePrinter : HTMLPrinter
    {
        Structure Structure;
        World World;

        public StructurePrinter(Structure structure, World world)
        {
            Structure = structure;
            World = world;
        }

        public override string Print()
        {
            HTML = new StringBuilder();

            HTML.AppendLine("<h1>" + Structure.Name + "</h1>");
            HTML.AppendLine("<b>" + Structure.Type.GetDescription() + " in " + Structure.Site.ToLink() + "</b><br/><br/>");

            PrintEventLog(Structure.Events, Structure.Filters, Structure);
            return HTML.ToString();
        }

        public override string GetTitle()
        {
            return Structure.Name;
        }
    }
}
