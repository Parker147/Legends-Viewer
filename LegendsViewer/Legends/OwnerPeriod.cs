using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsViewer.Legends
{
    //TODO: Move site/owner add period/site lines to site creation/destruction events
    //TODO: Make Enums for start/end cause, search "new ownerperiod"
    public class OwnerPeriod
    {
        public Site Site;
        public DwarfObject Owner;
        public DwarfObject Ender;
        public int StartYear, EndYear;
        public string StartCause, EndCause;
        public OwnerPeriod(Site site, DwarfObject newowner, int year, string cause)
        {
            Site = site; Owner = newowner; StartYear = year; StartCause = cause; EndYear = -1;
            if (Owner != null && Owner is Entity)
                ((Entity) Owner).AddOwnedSite(this);
            Site.OwnerHistory.Add(this);
        }
    }
}
