using System;
using System.Collections.Generic;
using System.Linq;
using LegendsViewer.Controls;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.Interfaces;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class OccasionEvent : WorldEvent
    {
        public Entity Civ { get; set; }
        public Site Site { get; set; }
        public WorldRegion Region { get; set; }
        public UndergroundRegion UndergroundRegion { get; set; }
        public int OccasionId { get; set; }
        public int ScheduleId { get; set; }
        public OccasionType OccasionType { get; set; }
        public EntityOccasion EntityOccasion { get; set; }
        public Schedule Schedule { get; set; }

        public OccasionEvent(List<Property> properties, World world) : base(properties, world)
        {
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "civ_id":
                        Civ = world.GetEntity(Convert.ToInt32(property.Value));
                        break;
                    case "site_id":
                        Site = world.GetSite(Convert.ToInt32(property.Value));
                        break;
                    case "subregion_id":
                        Region = world.GetRegion(Convert.ToInt32(property.Value));
                        break;
                    case "feature_layer_id":
                        UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value));
                        break;
                    case "occasion_id":
                        OccasionId = Convert.ToInt32(property.Value);
                        break;
                    case "schedule_id":
                        ScheduleId = Convert.ToInt32(property.Value);
                        break;
                }
            }

            if (Civ != null && Civ.Occassions.Any())
            {
                EntityOccasion = Civ.Occassions.ElementAt(OccasionId);
                if (EntityOccasion != null)
                {
                    Schedule = EntityOccasion.Schedules.ElementAt(ScheduleId);
                    
                    // DEBUG

                    //if (Schedule.Reference != -1 && Schedule.Type == ScheduleType.Storytelling)
                    //{
                    //    WorldEvent worldEvent = World.GetEvent(Schedule.Reference) as WorldEvent;
                    //    if (!(worldEvent is IFeatured))
                    //    {
                    //        world.ParsingErrors.Report("Unknown Occasion Feature - worldEvent.Type: " + worldEvent.Type);
                    //    }
                    //}
                }
            }

            Civ.AddEvent(this);
            Site.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
        }

        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime();
            eventString += Civ != null ? Civ.ToLink(link, pov) : "UNKNOWN CIV";
            eventString += " held a ";
            if (Schedule != null)
            {
                if (!string.IsNullOrWhiteSpace(Schedule.ItemType) || !string.IsNullOrWhiteSpace(Schedule.ItemSubType))
                {
                    eventString += !string.IsNullOrWhiteSpace(Schedule.ItemSubType) ? Schedule.ItemSubType : Schedule.ItemType;
                    eventString += " ";
                }
            }
            eventString += Schedule != null ? Schedule.Type.GetDescription().ToLower() :OccasionType.ToString().ToLower();
            if (Schedule != null)
            {
                switch (Schedule.Type)
                {
                    case ScheduleType.PoetryRecital:
                        if (Schedule.Reference != -1)
                        {
                            PoeticForm form = World.GetPoeticForm(Schedule.Reference);
                            eventString += " of ";
                            eventString += form != null ? form.ToLink(link, pov) : "UNKNOWN POETRICFORM";
                        }
                        break;
                    case ScheduleType.MusicalPerformance:
                        if (Schedule.Reference != -1)
                        {
                            MusicalForm form = World.GetMusicalForm(Schedule.Reference);
                            eventString += " of ";
                            eventString += form != null ? form.ToLink(link, pov) : "UNKNOWN MUSICALFORM";
                        }
                        break;
                    case ScheduleType.DancePerformance:
                        if (Schedule.Reference != -1)
                        {
                            DanceForm form = World.GetDanceForm(Schedule.Reference);
                            eventString += " of ";
                            eventString += form != null ? form.ToLink(link, pov) : "UNKNOWN DANCEFORM";
                        }
                        break;
                    case ScheduleType.Storytelling:
                        if (Schedule.Reference != -1)
                        {
                            WorldEvent worldEvent = World.GetEvent(Schedule.Reference);
                            if (worldEvent is IFeatured)
                            {
                                eventString += " of ";
                                eventString += worldEvent != null ? ((IFeatured)worldEvent).PrintFeature() : "UNKNOWN EVENT";
                            }
                        }
                        break;
                }
            }
            eventString += " in ";
            eventString += Site != null ? Site.ToLink(link, pov) : "UNKNOWN SITE";
            eventString += " as part of ";
            eventString += EntityOccasion != null ? EntityOccasion.ToLink(link, pov) : "UNKNOWN OCCASION";
            eventString += ".";
            if (Schedule != null)
            {
                switch (Schedule.Type)
                {
                    case ScheduleType.Procession:
                        Structure startStructure = Site.Structures.FirstOrDefault(s => s.Id == Schedule.Reference);
                        Structure endStructure = Site.Structures.FirstOrDefault(s => s.Id == Schedule.Reference2);
                        if (startStructure != null || endStructure != null)
                        {
                            eventString += " It started at ";
                            eventString += startStructure != null ? startStructure.ToLink(link, pov) : "UNKNOWN STRUCTURE";
                            eventString += " and ended at ";
                            eventString += endStructure != null ? endStructure.ToLink(link, pov) : "UNKNOWN STRUCTURE";
                            eventString += ".";
                        }
                        break;
                }
            }
            return eventString;
        }
    }
}