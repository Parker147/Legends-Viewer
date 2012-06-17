using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsViewer.Legends
{
    public class Artifact : WorldObject
    {
        public string Name { get; set; }
        public string Item { get; set; }
        public static List<string> Filters;
        public override List<WorldEvent> FilteredEvents
        {
            get { return Events.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }

        public Artifact(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "name": Name = Formatting.InitCaps(property.Value); break;
                    case "item": Item = Formatting.InitCaps(property.Value); break;
                  
                }
        }

        public Artifact() { Name = "INVALID ARTIFACT"; Item = "INVALID ITEM"; }

        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            if (link)
            {
                string linkedString = "";
                if (pov != this)
                {
                    string title = "Events: " + this.Events.Count;

                    linkedString = "<a href = \"artifact#" + this.ID + "\" title=\"" + title + "\">" + Name + "</a>";
                }
                else
                    linkedString = "<font color=\"Blue\">" + Name + "</font>";
                return linkedString;
            }
            else
                return Name;
        }
    }
}
