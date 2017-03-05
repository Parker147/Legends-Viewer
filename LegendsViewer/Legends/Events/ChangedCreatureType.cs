using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class ChangedCreatureType : WorldEvent
    {
        public HistoricalFigure Changee, Changer;
        public string OldRace, OldCaste, NewRace, NewCaste;
        public ChangedCreatureType(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach (Property property in properties)
                switch (property.Name)
                {
                    case "old_race": OldRace = Formatting.FormatRace(property.Value); break;
                    case "old_caste": OldCaste = property.Value; break;
                    case "new_race": NewRace = Formatting.FormatRace(property.Value); break;
                    case "new_caste": NewCaste = property.Value; break;
                    case "changee_hfid": Changee = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "changer_hfid": Changer = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "changee": if (Changee == null) { Changee = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); } else property.Known = true; break;
                    case "changer": if (Changer == null) { Changer = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); } else property.Known = true; break;
                }
            Changee.PreviousRace = OldRace;
            Changee.AddEvent(this);
            Changer.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + Changer.ToLink(link, pov) + " changed " + Changee.ToLink(link, pov) + " from a " + OldRace + " into a " + NewRace;
            eventString += PrintParentCollection(link, pov);
            eventString += ".";
            return eventString;
        }
    }
}