using System;
using LegendsViewer.Controls.HTML.Utilities;
using System.Collections.Generic;
using System.Linq;
using LegendsViewer.Legends.Events;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends
{
    public class Artifact : WorldObject
    {
        public static string Icon = "<i class=\"fa fa-fw fa-diamond\"></i>";

        public string Name { get; set; }
        public string Item { get; set; }
        public HistoricalFigure Creator { get; set; }
        public string Type { get; set; } // legends_plus.xml
        public string SubType { get; set; } // legends_plus.xml
        public string Description { get; set; } // legends_plus.xml
        public string Material { get; set; } // legends_plus.xml
        public int PageCount { get; set; } // legends_plus.xml

        public List<int> WrittenContents { get; set; }

        public static List<string> Filters;
        public override List<WorldEvent> FilteredEvents
        {
            get { return Events.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }

        public Artifact(List<Property> properties, World world)
            : base(properties, world)
        {
            Name = "Untitled";
            Type = "Artifact";
            WrittenContents = new List<int>();

            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "name": Name = Formatting.InitCaps(property.Value); break;
                    case "item": Item = Formatting.InitCaps(property.Value); break;
                    case "item_type": Type = property.Value; break;
                    case "item_subtype": SubType = property.Value; break;
                    case "item_description": Description = Formatting.InitCaps(property.Value); break;
                    case "mat": Material = property.Value; break;
                    case "page_count": PageCount = Convert.ToInt32(property.Value); break;
                    case "writing": if (!WrittenContents.Contains(Convert.ToInt32(property.Value))) WrittenContents.Add(Convert.ToInt32(property.Value)); break;
                }
            }
        }

        public override string ToString() { return Name; }

        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            if (link)
            {
                string title = "Artifact" + (!string.IsNullOrEmpty(Type) ? ", " + Type : "");
                title += "&#13";
                title += "Events: " + Events.Count;
                if (pov != this)
                {
                    return Icon + "<a href = \"artifact#" + ID + "\" title=\"" + title + "\">" + Name + "</a>";
                }
                return Icon + "<a title=\"" + title + "\">" + HTMLStyleUtil.CurrentDwarfObject(Name) + "</a>";
            }
            return Name;
        }
    }
}
