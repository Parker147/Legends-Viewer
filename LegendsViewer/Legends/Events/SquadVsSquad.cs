using System;
using System.Collections.Generic;
using System.Linq;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class SquadVsSquad : WorldEvent
    {
        public HistoricalFigure AttackerHistoricalFigure { get; set; }
        public int AttackerSquadId { get; set; }
        public int DefenderSquadId { get; set; }
        public int DefenderRaceId { get; set; }
        public int DefenderNumber { get; set; }
        public int DefenderSlain { get; set; }
        public Site Site { get; set; }
        public int StructureId { get; set; }
        public Structure Structure { get; set; }
        public WorldRegion Region { get; set; }
        public UndergroundRegion UndergroundRegion { get; set; }
        public HistoricalFigure AttackerLeader { get; set; }
        public int AttackerLeadershipRoll { get; set; }

        public SquadVsSquad(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "a_hfid": AttackerHistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "a_squad_id": AttackerSquadId = Convert.ToInt32(property.Value); break;
                    case "d_squad_id": DefenderSquadId = Convert.ToInt32(property.Value); break;
                    case "d_race": DefenderRaceId = Convert.ToInt32(property.Value); break;
                    case "d_interaction":
                        if (property.Value != "-1")
                        {
                            property.Known = false;
                        }
                        break;
                    case "d_effect":
                        if (property.Value != "-1")
                        {
                            property.Known = false;
                        }
                        break;
                    case "d_number": DefenderNumber = Convert.ToInt32(property.Value); break;
                    case "d_slain": DefenderSlain = Convert.ToInt32(property.Value); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "structure_id": StructureId = Convert.ToInt32(property.Value); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                    case "a_leader_hfid": AttackerLeader = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "a_leadership_roll": AttackerLeadershipRoll = Convert.ToInt32(property.Value); break;
                }
            }
            if (Site != null)
            {
                Structure = Site.Structures.FirstOrDefault(structure => structure.Id == StructureId);
            }
            AttackerHistoricalFigure.AddEvent(this);
            Site.AddEvent(this);
            Structure.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
            AttackerLeader.AddEvent(this);
        }

        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime();
            eventString += AttackerHistoricalFigure?.ToLink(link, pov) ?? "an unknown creature";
            if (AttackerLeader != null)
            {
                eventString += " as part of a squad";
                if (AttackerLeadershipRoll <= 25)
                {
                    eventString += " poorly";
                }
                else if (AttackerLeadershipRoll >= 100)
                {
                    eventString += " ably";
                }
                eventString += " led by ";
                eventString += AttackerLeader.ToLink(link, pov);
                eventString += ",";
            }
            eventString += " clashed with ";
            eventString += Formatting.IntegerToWords(DefenderNumber);
            eventString += " ";
            eventString += DefenderRaceId;
            if (Site != null)
            {
                eventString += " in " + Site.ToLink(link, pov);
            }
            if (DefenderNumber == DefenderSlain)
            {
                eventString += ", slaying them";
            }
            else if (DefenderSlain > 0)
            {
                eventString += ", slaying " + Formatting.IntegerToWords(DefenderSlain);
            }
            eventString += PrintParentCollection(link, pov);
            eventString += ".";
            return eventString;
        }
    }
}