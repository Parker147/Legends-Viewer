using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Docuverse.Identicon;
using LegendsViewer.Controls.HTML.Utilities;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.EventCollections;
using LegendsViewer.Legends.Events;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends
{
    public class Entity : WorldObject
    {
        public string Name { get; set; }
        public Entity Parent { get; set; }
        public bool IsCiv { get; set; }
        public string Race { get; set; }
        public List<HistoricalFigure> Worshipped { get; set; }
        public List<string> LeaderTypes { get; set; }
        public List<List<HistoricalFigure>> Leaders { get; set; }
        public List<HistoricalFigure> AllLeaders { get { return Leaders.SelectMany(leaders => leaders).ToList(); } set { } }
        public List<Population> Populations { get; set; }
        public List<string> PopulationsAsList
        {
            get
            {
                List<string> populations = new List<string>();
                foreach (Population population in Populations)
                    for (int i = 0; i < population.Count; i++)
                        populations.Add(population.Race);
                return populations;
            }
            set { }
        }
        public List<Entity> Groups { get; set; }
        public List<OwnerPeriod> SiteHistory { get; set; }
        public List<Site> CurrentSites { get { return SiteHistory.Where(site => site.EndYear == -1).Select(site => site.Site).ToList(); } set { } }
        public List<Site> LostSites { get { return SiteHistory.Where(site => site.EndYear >= 0).Select(site => site.Site).ToList(); } set { } }
        public List<Site> Sites { get { return SiteHistory.Select(site => site.Site).ToList(); } set { } }

        public EntityType Type { get; set; } // legends_plus.xml
        public List<EntitySiteLink> SiteLinks { get; set; } // legends_plus.xml
        public List<EntityEntityLink> EntityLinks { get; set; } // legends_plus.xml
        public List<EntityPosition> EntityPositions { get; set; } // legends_plus.xml
        public List<EntityPositionAssignment> EntityPositionAssignments { get; set; } // legends_plus.xml
        public List<Location> Claims { get; set; } // legends_plus.xml

        public List<War> Wars { get; set; }
        public List<War> WarsAttacking { get { return Wars.Where(war => war.Attacker == this).ToList(); } set { } }
        public List<War> WarsDefending { get { return Wars.Where(war => war.Defender == this).ToList(); } set { } }
        public int WarVictories { get { return WarsAttacking.Sum(war => war.AttackerBattleVictories.Count) + WarsDefending.Sum(war => war.DefenderBattleVictories.Count); } set { } }
        public int WarLosses { get { return WarsAttacking.Sum(war => war.DefenderBattleVictories.Count) + WarsDefending.Sum(war => war.AttackerBattleVictories.Count); } set { } }
        public int WarKills { get { return WarsAttacking.Sum(war => war.DefenderDeathCount) + WarsDefending.Sum(war => war.AttackerDeathCount); } set { } }
        public int WarDeaths { get { return WarsAttacking.Sum(war => war.AttackerDeathCount) + WarsDefending.Sum(war => war.DefenderDeathCount); } set { } }
        public double WarKillDeathRatio
        {
            get
            {
                if (WarDeaths == 0 && WarKills == 0) return 0;
                if (WarDeaths == 0) return double.MaxValue;
                return Math.Round(WarKills / Convert.ToDouble(WarDeaths), 2);
            }
            set { }
        }
        public double WarVictoryRatio
        {
            get
            {
                if (WarVictories == 0 && WarLosses == 0) return 0;
                if (WarLosses == 0) return double.MaxValue;
                return Math.Round(WarVictories / Convert.ToDouble(WarLosses), 2);
            }
            set { }
        }

        public string IdenticonString { get; set; }
        public string SmallIdenticonString { get; set; }
        public int IdenticonCode { get; set; }
        public Color IdenticonColor { get; set; }
        public Color LineColor { get; set; }
        public Bitmap Identicon { get; set; }

        private string _icon;
        public string Icon
        {
            get
            {
                if (string.IsNullOrEmpty(_icon))
                {
                    string coloredIcon;
                    if (IsCiv)
                    {
                        coloredIcon = PrintIdenticon() + " ";
                    }
                    else if (World.MainRaces.ContainsKey(Race))
                    {
                        Color civilizedPopColor = World.MainRaces.FirstOrDefault(r => r.Key == Race).Value;
                        coloredIcon = "<span class=\"fa-stack fa-lg\" style=\"font-size:smaller;\">";
                        coloredIcon += "<i class=\"fa fa-square fa-stack-2x\"></i>";
                        coloredIcon += "<i class=\"fa fa-group fa-stack-1x\" style=\"color:" + ColorTranslator.ToHtml(civilizedPopColor) + ";\"></i>";
                        coloredIcon += "</span>";
                    }
                    else
                    {
                        coloredIcon = "<span class=\"fa-stack fa-lg\" style=\"font-size:smaller;\">";
                        coloredIcon += "<i class=\"fa fa-square fa-stack-2x\"></i>";
                        coloredIcon += "<i class=\"fa fa-group fa-stack-1x fa-inverse\"></i>";
                        coloredIcon += "</span>";
                    }
                    _icon = coloredIcon;
                }
                return _icon;
            }
            set { _icon = value; }
        }

        public static List<string> Filters;

        public override List<WorldEvent> FilteredEvents
        {
            get { return Events.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }
        public Entity(World world)
        {
            ID = -1; Name = "INVALID ENTITY"; Race = "Unknown";
            Parent = null;
            Worshipped = new List<HistoricalFigure>();
            LeaderTypes = new List<string>();
            Leaders = new List<List<HistoricalFigure>>();
            Groups = new List<Entity>();
            SiteHistory = new List<OwnerPeriod>();
            Wars = new List<War>();
            Populations = new List<Population>();
            EntityPositions = new List<EntityPosition>();
            EntityPositionAssignments = new List<EntityPositionAssignment>();
            Claims = new List<Location>();
        }
        public Entity(List<Property> properties, World world)
            : base(properties, world)
        {
            Name = "";
            Race = "Unknown";
            Type = EntityType.Unknown;
            Parent = null;
            Worshipped = new List<HistoricalFigure>();
            LeaderTypes = new List<string>();
            Leaders = new List<List<HistoricalFigure>>();
            Groups = new List<Entity>();
            SiteHistory = new List<OwnerPeriod>();
            SiteLinks = new List<EntitySiteLink>();
            EntityLinks = new List<EntityEntityLink>();
            Wars = new List<War>();
            Populations = new List<Population>();
            EntityPositions = new List<EntityPosition>();
            EntityPositionAssignments = new List<EntityPositionAssignment>();
            Claims = new List<Location>();

            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "name": Name = Formatting.InitCaps(property.Value); break;
                    case "race":
                        Race = Formatting.MakePopulationPlural(Formatting.FormatRace(property.Value));
                        break;
                    case "type":
                        switch (property.Value)
                        {
                            case "civilization":
                                Type = EntityType.Civilization;
                                break;
                            case "religion":
                                Type = EntityType.Religion;
                                break;
                            case "sitegovernment":
                                Type = EntityType.SiteGovernment;
                                break;
                            case "nomadicgroup":
                                Type = EntityType.NomadicGroup;
                                break;
                            case "outcast":
                                Type = EntityType.Outcast;
                                break;
                            case "migratinggroup":
                                Type = EntityType.MigratingGroup;
                                break;
                            case "performancetroupe":
                                Type = EntityType.PerformanceTroupe;
                                break;
                            default:
                                Type = EntityType.Unknown;
                                world.ParsingErrors.Report("Unknown Entity Type: " + property.Value);
                                break;
                        }
                        break;
                    case "child":
                        property.Known = true;
                        break;
                    case "site_link":
                        SiteLinks.Add(new EntitySiteLink(property.SubProperties, world));
                        break;
                    case "entity_link":
                        property.Known = true;
                        foreach (Property subProperty in property.SubProperties)
                        {
                            subProperty.Known = true;
                        }
                        world.AddEntityEntityLink(this, property);
                        break;
                    case "worship_id":
                        property.Known = true;
                        break;
                    case "claims":
                        string[] coordinateStrings = property.Value.Split(new char[] { '|' },
                            StringSplitOptions.RemoveEmptyEntries);
                        foreach (var coordinateString in coordinateStrings)
                        {
                            string[] xYCoordinates = coordinateString.Split(',');
                            int x = Convert.ToInt32(xYCoordinates[0]);
                            int y = Convert.ToInt32(xYCoordinates[1]);
                            Claims.Add(new Location(x, y));
                        }
                        break;
                    case "entity_position": EntityPositions.Add(new EntityPosition(property.SubProperties, world)); break;
                    case "entity_position_assignment": EntityPositionAssignments.Add(new EntityPositionAssignment(property.SubProperties, world)); break;
                    case "histfig_id":
                        property.Known = true; // historical figure == last known entitymember?
                        break;
                }
            }
        }
        public override string ToString() { return this.Name; }



        public bool EqualsOrParentEquals(Entity entity)
        {
            return this == entity || Parent == entity;
        }

        public string PrintEntity(bool link = true, DwarfObject pov = null)
        {
            string entityString = ToLink(link, pov);
            if (Parent != null)
            {
                entityString += " of " + Parent.ToLink(link, pov);
            }
            return entityString;
        }

        //TODO: Check and possibly move logic
        public void AddOwnedSite(OwnerPeriod newSite)
        {
            if (newSite.StartCause == "UNKNOWN" && SiteHistory.All(s => s.Site != newSite.Site))
                SiteHistory.Insert(0, newSite);
            else
                SiteHistory.Add(newSite);

            if (newSite.Owner != this)
                Groups.Add((Entity)newSite.Owner);
            if (Parent != null && Parent != null)
            {
                Parent.AddOwnedSite(newSite);
                Race = Parent.Race;
            }
        }

        public void AddPopulations(List<Population> populations)
        {
            foreach (Population population in populations)
            {
                Population popMatch = this.Populations.FirstOrDefault(pop => pop.Race == population.Race);
                if (popMatch != null)
                    popMatch.Count += population.Count;
                else
                    this.Populations.Add(new Population(population.Race, population.Count));
            }
            this.Populations = this.Populations.OrderByDescending(pop => pop.Count).ToList();

        }

        public string PrintIdenticon(bool fullSize = false)
        {
            if (IsCiv)
            {
                string printIdenticon = "<img src=\"data:image/gif;base64,";
                if (fullSize) printIdenticon += IdenticonString;
                else printIdenticon += SmallIdenticonString;
                printIdenticon += "\" align=absmiddle />";
                return printIdenticon;
            }
            return "";
        }

        public Bitmap GetIdenticon(int size)
        {
            IdenticonRenderer identiconRenderer = new IdenticonRenderer();
            return identiconRenderer.Render(IdenticonCode, size, IdenticonColor);
        }

        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            if (link)
            {
                string title;
                if (IsCiv)
                {
                    title = "Civilization of " + Race;
                }
                else
                {
                    title = "Group of " + Race;
                }
                if (Parent != null)
                {
                    title += ", of " + Parent.Name;
                }
                if (pov != this)
                {
                    return Icon + "<a href = \"entity#" + ID + "\" title=\"" + title + "\">" + Name + "</a>";
                }
                return Icon + "<a title=\"" + title + "\">" + HTMLStyleUtil.CurrentDwarfObject(Name) + "</a>";
            }
            return Name;
        }
    }
}
