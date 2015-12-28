using LegendsViewer.Controls.HTML.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace LegendsViewer.Legends
{
    public class Structure : WorldObject
    {
        public string Name { get; set; } // legends_plus.xml
        public string AltName { get; set; } // legends_plus.xml
        public string Type { get; set; } // legends_plus.xml
        public static List<string> Filters;
        public override List<WorldEvent> FilteredEvents
        {
            get { return Events.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }

        public Structure(List<Property> properties, World world)
            : base(properties, world)
        {
            Name = "UNKNOWN STRUCTURE";
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "name": Name = Formatting.InitCaps(property.Value); break;
                    case "name2": AltName = Formatting.InitCaps(property.Value); break;
                    case "type": Type = Formatting.InitCaps(property.Value); break;
                }
            }
        }

        public override string ToString() { return Name; }

        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            //if (link)
            //{
            //    string linkedString = "";
            //    if (pov != this)
            //    {
            //        string title = "Events: " + this.Events.Count;

            //        linkedString = "<a href = \"structure#" + this.ID + "\" title=\"" + title + "\">" + Name + "</a>";
            //    }
            //    else
            //        linkedString = HTMLStyleUtil.CurrentDwarfObject(Name);
            //    return linkedString;
            //}
            //else
                return Name;
        }
    }
}
