using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsViewer.Legends
{
    //TODO: Move site/owner add history/site lines to site creation/destruction events
    //TODO: Make Enums for start/end cause, search "new ownerperiod"
    public class OwnerPeriod
    {
        public Site Site;
        public Entity Owner, Ender;
        public int StartYear, EndYear;
        public string StartCause, EndCause;
        public OwnerPeriod(Site site, Entity newowner, int year, string cause)
        {
            Site = site; Owner = newowner; StartYear = year; StartCause = cause; EndYear = -1;
            if (Owner != null)
                Owner.AddOwnedSite(this);
            Site.OwnerHistory.Add(this);
        }
    }
}
