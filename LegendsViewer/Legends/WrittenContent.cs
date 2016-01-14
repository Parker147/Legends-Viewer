using LegendsViewer.Controls.HTML.Utilities;
using System.Collections.Generic;
using System.Linq;
using LegendsViewer.Legends.Events;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends
{
    public class WrittenContent : WorldObject
    {
        public string Name { get; set; } // legends_plus.xml
        public static List<string> Filters;
        public override List<WorldEvent> FilteredEvents
        {
            get { return Events.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }

        public WrittenContent(List<Property> properties, World world)
            : base(properties, world)
        {
            Name = "Untitled";
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "title": Name = Formatting.InitCaps(property.Value); break;
                }
            }
        }

        public override string ToString() { return Name; }

        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            if (link)
            {
                string linkedString = "";
                if (pov != this)
                {
                    string title = "Written Content";
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
