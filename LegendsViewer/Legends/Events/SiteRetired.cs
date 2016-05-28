using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Parser;
using System.Linq;

namespace LegendsViewer.Legends.Events
{
    public class SiteRetired : WorldEvent
    {
        public Site Site { get; set; }
        public Entity Civ { get; set; }
        public Entity SiteCiv { get; set; }
        public string First { get; set; }
        public SiteRetired(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "civ_id": Civ = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_civ_id": SiteCiv = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "first": First = property.Value; break;
                }
            }
            Site.OwnerHistory.Last().EndYear = this.Year;
            Site.OwnerHistory.Last().EndCause = "retired";
            if (SiteCiv != null)
            {
                SiteCiv.SiteHistory.Last(s => s.Site == Site).EndYear = this.Year;
                SiteCiv.SiteHistory.Last(s => s.Site == Site).EndCause = "retired";
            }
            Civ.SiteHistory.Last(s => s.Site == Site).EndYear = this.Year;
            Civ.SiteHistory.Last(s => s.Site == Site).EndCause = "retired";

            Site.AddEvent(this);
            Civ.AddEvent(this);
            SiteCiv.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime();
            eventString += SiteCiv != null ? SiteCiv.ToLink(link, pov) : "UNKNOWN ENTITY";
            eventString += " of ";
            eventString += Civ != null ? Civ.ToLink(link, pov) : "UNKNOWN CIV";
            eventString += " at the settlement of ";
            eventString += Site != null ? Site.ToLink(link, pov) : "UNKNOWN SITE";
            eventString += " regained their senses after an initial period of questionable judgment.";
            return eventString;
        }
    }
}