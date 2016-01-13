using System;
using System.Collections.Generic;
using System.Linq;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.Events;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.EventCollections
{
    public class War : EventCollection
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public int DeathCount { get; set; }
        public List<string> Deaths
        {
            get
            {
                List<string> deaths = new List<string>();

                foreach (Battle battle in Battles)
                {
                    deaths.AddRange(battle.Deaths);
                }
                return deaths;
            }
            set { }
        }
        public int AttackerDeathCount { get; set; }
        public int DefenderDeathCount { get; set; }
        public Entity Attacker { get; set; }
        public Entity Defender { get; set; }
        public List<Battle> Battles { get { return Collections.OfType<Battle>().ToList(); } set { } }
        public List<SiteConquered> Conquerings { get { return Collections.OfType<SiteConquered>().ToList(); } set { } }
        public List<Site> SitesLost { get { return Conquerings.Where(conquering => conquering.ConquerType == SiteConqueredType.Conquest || conquering.ConquerType == SiteConqueredType.Destruction).Select(conquering => conquering.Site).ToList(); } set { } }
        public List<Site> AttackerSitesLost { get { return DefenderConquerings.Where(conquering => conquering.ConquerType == SiteConqueredType.Conquest || conquering.ConquerType == SiteConqueredType.Destruction).Select(conquering => conquering.Site).ToList(); } set { } }
        public List<Site> DefenderSitesLost { get { return AttackerConquerings.Where(conquering => conquering.ConquerType == SiteConqueredType.Conquest || conquering.ConquerType == SiteConqueredType.Destruction).Select(conquering => conquering.Site).ToList(); } set { } }
        public List<EventCollection> AttackerVictories { get; set; }
        public List<EventCollection> DefenderVictories { get; set; }
        public List<Battle> AttackerBattleVictories { get { return Collections.OfType<Battle>().Where(battle => battle.Victor.EqualsOrParentEquals(Attacker)).ToList(); } set { } }
        public List<Battle> DefenderBattleVictories { get { return Collections.OfType<Battle>().Where(battle => battle.Victor.EqualsOrParentEquals(Defender)).ToList(); } set { } }
        public List<Battle> ErrorBattles { get { return Collections.OfType<Battle>().Where(battle => (!battle.Attacker.EqualsOrParentEquals(this.Attacker) && !battle.Attacker.EqualsOrParentEquals(this.Defender)) || (!battle.Defender.EqualsOrParentEquals(this.Defender) && !battle.Defender.EqualsOrParentEquals(this.Attacker))).ToList(); } set { } }
        public List<SiteConquered> AttackerConquerings { get { return Collections.OfType<SiteConquered>().Where(conquering => conquering.Attacker.EqualsOrParentEquals(Attacker)).ToList(); } set { } }
        public List<SiteConquered> DefenderConquerings { get { return Collections.OfType<SiteConquered>().Where(conquering => conquering.Attacker.EqualsOrParentEquals(Defender)).ToList(); } set { } }
        public double AttackerToDefenderKills
        {
            get
            {
                if (AttackerDeathCount == 0 && DefenderDeathCount == 0) return 0;
                if (AttackerDeathCount == 0) return double.MaxValue;
                return Math.Round(DefenderDeathCount / Convert.ToDouble(AttackerDeathCount), 2);
            }
            set { }
        }
        public double AttackerToDefenderVictories
        {
            get
            {
                if (DefenderBattleVictories.Count == 0 && AttackerBattleVictories.Count == 0) return 0;
                if (DefenderBattleVictories.Count == 0) return double.MaxValue;
                return Math.Round(AttackerBattleVictories.Count / Convert.ToDouble(DefenderBattleVictories.Count), 2);
            }
            set { }
        }
        public static List<string> Filters;
        public override List<WorldEvent> FilteredEvents
        {
            get { return AllEvents.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }

        public War()
            : base()
        {
            Initialize();
        }

        public War(List<Property> properties, World world)
            : base(properties, world)
        {
            Initialize();
            foreach (Property property in properties)
                switch (property.Name)
                {
                    case "name": Name = Formatting.InitCaps(property.Value); break;
                    case "aggressor_ent_id": Attacker = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "defender_ent_id": Defender = world.GetEntity(Convert.ToInt32(property.Value)); break;
                }
            Defender.Wars.Add(this);
            if (Defender.Parent != null)
                Defender.Parent.Wars.Add(this);
            Attacker.Wars.Add(this);
            if (Attacker.Parent != null)
                Attacker.Parent.Wars.Add(this);
            if (EndYear >= 0)
                Length = EndYear - StartYear;
            else if (world.Events.Count > 0)
                Length = world.Events.Last().Year - StartYear;
        }

        private void Initialize()
        {
            Name = "";
            Length = 0;
            DeathCount = 0;
            AttackerDeathCount = 0;
            DefenderDeathCount = 0;
            Attacker = null;
            Defender = null;
            AttackerVictories = new List<EventCollection>();
            DefenderVictories = new List<EventCollection>();
        }

        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            if (link)
            {
                string linkedString = "";
                if (pov != this)
                {
                    string title = Attacker.PrintEntity(false) + " (Attacker)&#13" + Defender.PrintEntity(false) + " (Defender)&#13Deaths: " + DeathCount + " | (" + StartYear + " - ";
                    if (EndYear == -1) title += "Present)";
                    else title += EndYear + ")";
                    linkedString = "<a href = \"collection#" + this.ID + "\" title=\"" + title + "\"><font color=\"#6E5007\">" + Name + "</font></a>";
                }
                else
                    linkedString = "<font color=\"Blue\">" + Name + "</font>";

                return linkedString;
            }
            else
                return Name;
        }
        public override string ToString()
        {
            return Name;
        }

    }
}
