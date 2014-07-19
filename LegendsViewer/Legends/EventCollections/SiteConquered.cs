using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Controls;

namespace LegendsViewer.Legends
{
    public enum SiteConqueredType : int
    {
        Pillaging,
        Destruction,
        Conquest,
        Unknown
    };

    public class SiteConquered : EventCollection
    {
        public int Ordinal { get; set; }
        public SiteConqueredType ConquerType { get; set; }
        public Site Site { get; set; }
        public Entity Attacker { get; set; }
        public Entity Defender { get; set; }
        public Battle Battle { get; set; }
        public List<HistoricalFigure> Deaths { get { return GetSubEvents().OfType<HFDied>().Select(death => death.HistoricalFigure).ToList(); } set { } }
        public static List<string> Filters;
        public override List<WorldEvent> FilteredEvents
        {
            get { return AllEvents.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }
        public SiteConquered()
            : base()
        {
            Initialize();
        }

        public SiteConquered(List<Property> properties, World world)
            : base(properties, world)
        {
            Initialize();
            foreach (Property property in properties)
                switch (property.Name)
                {
                    case "ordinal": Ordinal = Convert.ToInt32(property.Value); break;
                    case "war_eventcol": ParentCollection = world.GetEventCollection(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "attacking_enid": Attacker = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "defending_enid": Defender = world.GetEntity(Convert.ToInt32(property.Value)); break;
                }

            

            if (Collection.OfType<PlunderedSite>().Count() > 0) ConquerType = SiteConqueredType.Pillaging;
            else if (Collection.OfType<DestroyedSite>().Count() > 0) ConquerType = SiteConqueredType.Destruction;
            else if (Collection.OfType<NewSiteLeader>().Count() > 0 || Collection.OfType<SiteTakenOver>().Count() > 0) ConquerType = SiteConqueredType.Conquest;
            else ConquerType = SiteConqueredType.Unknown;

            if (ConquerType == SiteConqueredType.Pillaging) Notable = false;

            Site.Warfare.Add(this);
            if (ParentCollection != null)
            {
                (ParentCollection as War).DeathCount += Collection.OfType<HFDied>().Count();

                if (Attacker == (ParentCollection as War).Attacker) 
                    (ParentCollection as War).AttackerVictories.Add(this);
                else 
                    (ParentCollection as War).DefenderVictories.Add(this);
            }

        }

        private void Initialize()
        {
            Ordinal = 1;
        }

        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            if (link)
            {
                string linkedString = "";
                if (pov != this)
                {
                    linkedString = "<a href = \"collection#" + this.ID + "\"><font color=\"800000\">" + "The " + this.GetOrdinal(Ordinal) + ConquerType + " of " + Site.ToLink(false) + "</font></a>";
                    if (pov != Battle) linkedString += " as a result of " + Battle.ToLink();
                }
                else
                    linkedString = "<font color=\"Blue\">" + "The " + this.GetOrdinal(Ordinal) + ConquerType + " of " + Site.ToLink(false) + "</font>";

                return linkedString;
            }
            else
                return "The " + this.GetOrdinal(Ordinal) + ConquerType + " of " + Site.ToLink(false);
        }
        public override string ToString()
        {
            return ToLink(false);
        }

    }
}
