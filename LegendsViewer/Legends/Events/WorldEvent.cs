using System;
using System.Collections.Generic;
using LegendsViewer.Legends.EventCollections;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class WorldEvent : IComparable<WorldEvent>
    {
        public int ID { get; set; }
        public int Year { get; set; }
        public int Seconds72 { get; set; }
        public string Type { get; set; }
        public EventCollection ParentCollection { get; set; }
        public World World { get; set; }

        public WorldEvent() { ID = -1; Year = -1; Seconds72 = -1; Type = "INVALID"; }

        public WorldEvent(List<Property> properties, World world)
        {
            World = world;
            foreach (Property property in properties)
                switch (property.Name)
                {
                    case "id": ID = Convert.ToInt32(property.Value); property.Known = true; break;
                    case "year": Year = Convert.ToInt32(property.Value); property.Known = true; break;
                    case "seconds72": Seconds72 = Convert.ToInt32(property.Value); property.Known = true; break;
                    case "type": Type = string.Intern(property.Value); property.Known = true; break;
                }
        }

        public virtual string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime() + Type;
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }

        public virtual string GetYearTime()
        {
            if (this.Year == -1) return "In a time before time, ";
            string yearTime = "In " + this.Year + ", ";
            if (this.Seconds72 == -1)
                return yearTime;

            int month = this.Seconds72 % 100800;
            if (month <= 33600) yearTime += "early ";
            else if (month <= 67200) yearTime += "mid";
            else if (month <= 100800) yearTime += "late ";

            int season = this.Seconds72 % 403200;
            if (season < 100800) yearTime += "spring, ";
            else if (season < 201600) yearTime += "summer, ";
            else if (season < 302400) yearTime += "autumn, ";
            else if (season < 403200) yearTime += "winter, ";

            int monthIndex = this.Seconds72 / (28 * 1200);
            string[] monthNames = { "Granite", "Slate", "Felsite", "Hematite", "Malachite", "Galena", "Limestone", "Sandstone", "Timber", "Moonstone", "Opal", "Obsidian" };
            string monthName = monthNames[monthIndex];
            int dayIndex = 1 + (this.Seconds72 % (28 * 1200)) / 1200;

            return yearTime + " (" + Formatting.AddOrdinal(dayIndex) + " of " + monthName + ") ";
        }

        public string PrintParentCollection(bool link = true, DwarfObject pov = null)
        {
            EventCollection parent = ParentCollection;
            string collectionString = "";
            while (parent != null)
            {
                if (collectionString.Length > 0) collectionString += " as part of ";
                collectionString += parent.ToLink(link, pov);
                parent = parent.ParentCollection;
            }

            if (collectionString.Length > 0)
                return "In " + collectionString + ". ";
            else
                return collectionString;
        }

        public int Compare(WorldEvent worldEvent)
        {
            return this.ID.CompareTo(worldEvent.ID);
        }

        public int CompareTo(object obj)
        {
            return this.ID.CompareTo(obj);
        }

        public int CompareTo(WorldEvent other)
        {
            return this.ID.CompareTo(other.ID);
        }
    }
}