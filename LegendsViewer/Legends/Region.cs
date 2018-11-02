﻿using System;
using System.Collections.Generic;
using System.Linq;
using LegendsViewer.Controls.HTML.Utilities;
using LegendsViewer.Legends.EventCollections;
using LegendsViewer.Legends.Events;
using LegendsViewer.Legends.Interfaces;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends
{
    public class WorldRegion : WorldObject, IHasCoordinates
    {
        public string Icon = "<i class=\"fa fa-fw fa-map-o\"></i>";

        public string Name { get; set; }
        public string Type { get; set; }
        public List<string> Deaths
        {
            get
            {
                List<string> deaths = new List<string>();
                deaths.AddRange(NotableDeaths.Select(death => death.Race));
                foreach (Battle.Squad squad in Battles.SelectMany(battle => battle.AttackerSquads.Concat(battle.DefenderSquads)))
                {
                    for (int i = 0; i < squad.Deaths; i++)
                    {
                        deaths.Add(squad.Race);
                    }
                }

                return deaths;
            }
            set { }
        }
        public List<HistoricalFigure> NotableDeaths { get { return Events.OfType<HfDied>().Select(death => death.HistoricalFigure).ToList(); } set { } }
        public List<Battle> Battles { get; set; }
        public List<Location> Coordinates { get; set; } // legends_plus.xml
        public int SquareTiles
        {
            get
            {
                return Coordinates.Count;
            }
        }

        public List<Site> Sites { get; set; } // legends_plus.xml
        public List<MountainPeak> MountainPeaks { get; set; } // legends_plus.xml

        public static List<string> Filters;
        public override List<WorldEvent> FilteredEvents
        {
            get { return Events.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }

        public WorldRegion(List<Property> properties, World world)
            : base(properties, world)
        {
            Name = "UNKNOWN REGION";
            Type = "INVALID";
            Battles = new List<Battle>();
            Coordinates = new List<Location>();
            Sites = new List<Site>();
            MountainPeaks = new List<MountainPeak>();
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "name": Name = Formatting.InitCaps(property.Value); break;
                    case "type": Type = string.Intern(property.Value); break;
                    case "coords":
                        string[] coordinateStrings = property.Value.Split(new[] { '|' },
                            StringSplitOptions.RemoveEmptyEntries);
                        foreach (var coordinateString in coordinateStrings)
                        {
                            string[] xYCoordinates = coordinateString.Split(',');
                            int x = Convert.ToInt32(xYCoordinates[0]);
                            int y = Convert.ToInt32(xYCoordinates[1]);
                            Coordinates.Add(new Location(x, y));
                        }
                        break;
                }
            }
        }
        public override string ToString() { return Name; }
        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            if (link)
            {
                string title = Type;
                title += "&#13";
                title += "Events: " + Events.Count;

                if (pov != this)
                {
                    return Icon + "<a href = \"region#" + Id + "\" title=\"" + title + "\">" + Name + "</a>";
                }
                return Icon + "<a title=\"" + title + "\">" + HtmlStyleUtil.CurrentDwarfObject(Name) + "</a>";
            }
            return Name;
        }
    }
}
