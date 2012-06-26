using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

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

        public HistoricalFigureLink(HistoricalFigure historicalFigureTarget, HistoricalFigureLinkType type, int strength = 0)
        {
            HistoricalFigure = historicalFigureTarget;
            Type = type;
            Strength = strength;
        }
    }

    public enum HistoricalFigureLinkType
    {
        Apprentice,
        Child,
        Deity,
        Father,
        Lover,
        Master,
        Mother,
        Spouse,
        Prisoner, //Not found in XML, used by AddHFHFLink event
        [Description("Ex-Spouse")]
        ExSpouse,
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
        [Description("Former Member")]
        FormerMember,
        Position,
        [Description("Former Position")]
        FormerPosition,
        Prisoner,
        [Description("Former Prisoner")]
        FormerPrisoner,
        [Description("Former Slave")]
        FormerSlave,
        Slave,
        [Description("Respected for heroic acts")]
        Hero,
        Unknown
    }

    public class SiteLink
    {
        public SiteLinkType Type { get; set; }
        public Site Site { get; set; }
        public int SubID { get; set; }
        public Entity Entity { get; set; }
        public SiteLink(List<Property> properties, World world)
        {
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "link_type":
                        switch(property.Value)
                        {
                            case "lair": Type = SiteLinkType.Lair; break;
                            case "home site building": Type = SiteLinkType.HomeSiteBuilding; break;
                            case "home site underground": Type = SiteLinkType.HomeSiteUnderground; break;
                            case "home structure": Type = SiteLinkType.HomeStructure; break;
                            case "seat of power": Type = SiteLinkType.SeatOfPower; break;
                            default: 
                                Type = SiteLinkType.Unknown;
                                world.ParsingErrors.Report("Unknown Site Link Type: " + property.Value);
                                break;
                        }
                        break;
                    case "site_id":
                        Site = world.GetSite(Convert.ToInt32(property.Value)); 
                        break;
                    case "sub_id":
                        SubID = Convert.ToInt32(property.Value);
                        break;
                    case "entity_id":
                        Entity = world.GetEntity(Convert.ToInt32(property.Value));
                        break;
                }
            }
        }
    }

    public enum SiteLinkType
    {
        Lair,
        [Description("Home - Site Building")]
        HomeSiteBuilding,
        [Description("Home - Site Underground")]
        HomeSiteUnderground,
        [Description("Home - Structure")]
        HomeStructure,
        [Description("Seat of Power")]
        SeatOfPower,
        Unknown
    }
}
