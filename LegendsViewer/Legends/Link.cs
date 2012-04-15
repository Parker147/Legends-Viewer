using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsViewer.Legends
{
    public class HistoricalFigureLink
    {
        public HistoricalFigure HistoricalFigure { get; set; }
        public int Strength { get; set; }
        public HistoricalFigureLinkType Type { get; set; }

        public HistoricalFigureLink(List<Property> properties, World world)
        {
            Strength = 0;
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "hfid":
                        int id = Convert.ToInt32(property.Value);
                        HistoricalFigure = world.GetHistoricalFigure(id);
                        break;
                    case "link_strength": Strength = Convert.ToInt32(property.Value); break;
                    case "link_type":
                        HistoricalFigureLinkType linkType = HistoricalFigureLinkType.Unknown;
                        if (!Enum.TryParse(Formatting.InitCaps(property.Value), out linkType))
                        {
                            Type = HistoricalFigureLinkType.Unknown;
                            world.ParsingErrors.Report("Unknown HF Link Type: " + property.Value);
                        }
                        else
                            Type = linkType;                              
                        break;
                }
            }

            //XML states that deity links, that should be 100, are 0.
            if (Type == HistoricalFigureLinkType.Deity && Strength == 0)
                Strength = 100;
        }
    }

    public enum HistoricalFigureLinkType
    {
        Apprentice,
        Child,
        Deity,
        Father,
        Master,
        Mother,
        Spouse,
        Unknown
    }

    public class EntityLink
    {
        public EntityLinkType Type { get; set; }
        public Entity Entity { get; set; }
        public int Strength { get; set; }
        public int PositionID { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }

        public EntityLink(List<Property> properties, World world)
        {
            Strength = 0;
            StartYear = -1;
            EndYear = -1;
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "entity_id":
                        int id = Convert.ToInt32(property.Value);
                        Entity = world.GetEntity(id);
                        break;
                    case "position_profile_id": PositionID = Convert.ToInt32(property.Value); break;
                    case "start_year": 
                        StartYear = Convert.ToInt32(property.Value);
                        Type = EntityLinkType.Position;
                        break;
                    case "end_year": 
                        EndYear = Convert.ToInt32(property.Value);
                        Type = EntityLinkType.FormerPosition;
                        break;
                    case "link_strength": Strength = Convert.ToInt32(property.Value); break;
                    case "link_type":
                        EntityLinkType linkType = EntityLinkType.Unknown;
                        if (!Enum.TryParse(Formatting.InitCaps(property.Value), out linkType))
                        {
                            switch (property.Value)
                            {
                                case "former member": Type = EntityLinkType.FormerMember; break;
                                case "former prisoner": Type = EntityLinkType.FormerPrisoner; break;
                                case "former slave": Type = EntityLinkType.FormerSlave; break;
                                default:
                                    Type = EntityLinkType.Unknown;
                                    world.ParsingErrors.Report("Unknown Entity Link Type: " + property.Value);
                                    break;
                            }
                        }
                        else
                            Type = linkType;
                        break;
                }
            }
        }
    }

    public enum EntityLinkType
    {
        Criminal,
        Enemy,
        Member,
        FormerMember,
        Position,
        FormerPosition,
        Prisoner,
        FormerPrisoner,
        FormerSlave,
        Unknown
    }

    class SiteLink
    {
    }
}
