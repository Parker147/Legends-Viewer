using LegendsViewer.Legends;
using System.Text;

namespace LegendsViewer.Controls.HTML
{
    public class ArtFormPrinter : HTMLPrinter
    {
        ArtForm Artform;
        World World;

        public ArtFormPrinter(ArtForm artform, World world)
        {
            Artform = artform;
            World = world;
        }

        public override string GetTitle()
        {
            return Artform.Name;
        }

        public override string Print()
        {
            HTML = new StringBuilder();

            HTML.AppendLine("<h1>" + Artform.Name + ", " + Artform.FormType + " Form</h1><br />");

            PrintEventLog(Artform.Events, ArtForm.Filters, Artform);

            return HTML.ToString();
        }
    }
}
