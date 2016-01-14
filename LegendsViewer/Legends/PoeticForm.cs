using System.Collections.Generic;
using LegendsViewer.Legends.Parser;
using LegendsViewer.Controls.HTML.Utilities;

namespace LegendsViewer.Legends
{
    public class PoeticForm : ArtForm
    {
        public PoeticForm(List<Property> properties, World world)
            : base(properties, world)
        {
        }

        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            if (link)
            {
                string linkedString = "";
                if (pov != this)
                {
                    string title = "Poetic Form";
                    title += "&#13";
                    title += "Events: " + Events.Count;

                    linkedString = "<a title=\"" + title + "\">" + Name + "</a>";
                }
                else
                    linkedString = HTMLStyleUtil.CurrentDwarfObject(Name);
                return linkedString;
            }
            else
                return Name;
        }
    }
}
