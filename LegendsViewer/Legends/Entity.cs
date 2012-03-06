using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Docuverse.Identicon;
using LegendsViewer.Controls;

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
        }
        public Entity(List<Property> properties, World world)
            : base(properties, world)
        {
            Name = "";
            Race = "Unknown";
            Parent = null;
            Worshipped = new List<HistoricalFigure>();
            LeaderTypes = new List<string>();
            Leaders = new List<List<HistoricalFigure>>();
            Groups = new List<Entity>();
            SiteHistory = new List<OwnerPeriod>();
            Wars = new List<War>();
            Populations = new List<Population>();
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "name": Name = Formatting.InitCaps(property.Value); break;
                }
        }
        public override string ToString() { return this.Name; }
        
        

        public bool EqualsOrParentEquals(Entity entity)
        {
            return this == entity || this.Parent == entity;
        }

        public string PrintEntity(bool link = true, DwarfObject pov = null)
        {
            string entityString = this.ToLink(link, pov);
            if (this.Parent != null) entityString += " of " + Parent.ToLink(link, pov);
            return entityString;
        }
        

        
        //TODO: Check and possibly move logic
        public void AddOwnedSite(OwnerPeriod newSite)
        {
            if (newSite.StartCause == "UNKNOWN" && SiteHistory.Where(s => s.Site == newSite.Site).Count() == 0)
                SiteHistory.Insert(0, newSite);
            else
                this.SiteHistory.Add(newSite);

            if (newSite.Owner != this)
                this.Groups.Add(newSite.Owner);
            if (this.Parent != null && this.Parent != null)
            {
                Parent.AddOwnedSite(newSite);
                this.Race = Parent.Race;
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
            else return "";

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
                if (pov != this)
                {
                    string title = "";
                    if (IsCiv) title = "Civilization of " + Race;
                    else title = "Group of " + Race;
                    if (Parent != null) title += ", of " + Parent.Name;
                    string entityLink = "<a href = \"entity#" + this.ID + "\" title=\"" + title + "\">" + this.Name + "</a>";
                    if (IsCiv) return entityLink + " " + PrintIdenticon() + " ";
                    else return entityLink;
                }
                else
                    return "<font color=\"Blue\">" + this.Name + "</font>";
            }
            else
                return this.Name;
        }

    }
}
