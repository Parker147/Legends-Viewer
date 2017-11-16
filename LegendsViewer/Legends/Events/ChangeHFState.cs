using System;
using System.Collections.Generic;
using System.Linq;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class ChangeHfState : WorldEvent
    {
        public HistoricalFigure HistoricalFigure { get; set; }
        public Site Site { get; set; }
        public WorldRegion Region { get; set; }
        public UndergroundRegion UndergroundRegion { get; set; }
        public Location Coordinates { get; set; }
        public HfState State { get; set; }
        public int SubState { get; set; }

        public ChangeHfState(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "state":
                        switch (property.Value)
                        {
                            case "settled": State = HfState.Settled; break;
                            case "wandering": State = HfState.Wandering; break;
                            case "scouting": State = HfState.Scouting; break;
                            case "snatcher": State = HfState.Snatcher; break;
                            case "refugee": State = HfState.Refugee; break;
                            case "thief": State = HfState.Thief; break;
                            case "hunting": State = HfState.Hunting; break;
                            case "visiting": State = HfState.Visiting; break;
                            default: State = HfState.Unknown; property.Known = false; break;
                        }
                        break;
                    case "substate": SubState = Convert.ToInt32(property.Value); break;
                    case "coords": Coordinates = Formatting.ConvertToLocation(property.Value); break;
                    case "hfid": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                    case "site": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            }
            if (HistoricalFigure != null)
            {
                HistoricalFigure.AddEvent(this);
                HistoricalFigure.States.Add(new HistoricalFigure.State(State, Year));
                HistoricalFigure.State lastState = HistoricalFigure.States.LastOrDefault();
                if (lastState != null)
                {
                    lastState.EndYear = Year;
                }

                HistoricalFigure.CurrentState = State;
            }
            Site.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
        }

        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime() + HistoricalFigure.ToLink(link, pov);
            if (State == HfState.Visiting)
            {
                eventString += " visited ";
            }
            else if(State == HfState.Settled)
            {
                switch (SubState)
                {
                    case 45:
                        eventString += " fled to ";
                        break;
                    case 46:
                    case 47:
                        eventString += " moved to study in ";
                        break;
                    default:
                        eventString += " settled in ";
                        break;
                }
            }
            else if (State == HfState.Wandering)
            {
                eventString += " began wandering ";
            }
            else if (State == HfState.Refugee || State == HfState.Snatcher || State == HfState.Thief)
            {
                eventString += " became a " + State.ToString().ToLower() + " in ";
            }
            else if (State == HfState.Scouting)
            {
                eventString += " began scouting the area around ";
            }
            else if (State == HfState.Hunting)
            {
                eventString += " began hunting great beasts in ";
            }
            else 
            {
                eventString += " changed state in ";
            }
            if (Site != null)
            {
                eventString += Site.ToLink(link, pov);
            }
            else if (Region != null)
            {
                eventString += Region.ToLink(link, pov);
            }
            else if (UndergroundRegion != null)
            {
                eventString += UndergroundRegion.ToLink(link, pov);
            }
            else
            {
                eventString += "the wilds";
            }

            eventString += PrintParentCollection(link, pov);
            eventString += ".";
            return eventString;
        }
    }
}