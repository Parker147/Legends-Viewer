using LegendsViewer.Controls.HTML.Utilities;
using System.Collections.Generic;
using System.Linq;
using LegendsViewer.Legends.Events;
using LegendsViewer.Legends.Parser;
using System;

namespace LegendsViewer.Legends
{
    public class WrittenContent : WorldObject
    {
        public string Name { get; set; } // legends_plus.xml
        public int PageStart { get; set; } // legends_plus.xml
        public int PageEnd { get; set; } // legends_plus.xml
        public string Type { get; set; } // legends_plus.xml
        public HistoricalFigure Author { get; set; } // legends_plus.xml
        public List<string> Styles { get; set; } // legends_plus.xml
        public List<Reference> References { get; set; } // legends_plus.xml

        public string Icon = "<i class=\"fa fa-fw fa-book\"></i> ";

        public static List<string> Filters;
        public override List<WorldEvent> FilteredEvents
        {
            get { return Events.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }

        public WrittenContent(List<Property> properties, World world)
            : base(properties, world)
        {
            Name = "Untitled";
            Styles = new List<string>();
            References = new List<Reference>();

            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "title": Name = Formatting.InitCaps(property.Value); break;
                    case "page_start": PageStart = Convert.ToInt32(property.Value); break;
                    case "page_end": PageEnd = Convert.ToInt32(property.Value); break;
                    case "reference": References.Add(new Reference(property.SubProperties, world)); break;
                    case "type": Type = property.Value; break;
                    case "author": Author = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "style": Styles.Add(property.Value); break;
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            if (link)
            {
                string type = null;
                int typeId;
                if (!int.TryParse(Type, out typeId))
                {
                    type = Type;
                }
                string title = "Written Content";
                title += string.IsNullOrWhiteSpace(type) ? "" : ", " + type;
                title += "&#13";
                title += "Events: " + Events.Count;

                string linkedString = "";
                if (pov != this)
                {
                    linkedString = Icon + "<a href = \"writtencontent#" + ID + "\" title=\"" + title + "\">" + Name + "</a>";
                }
                else
                {
                    linkedString = Icon + "<a title=\"" + title + "\">" + HTMLStyleUtil.CurrentDwarfObject(Name) + "</a>";
                }
                return linkedString;
            }
            else
            {
                return Name;
            }
        }
    }
}
