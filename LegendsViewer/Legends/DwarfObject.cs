using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsViewer.Legends
{
    public abstract class DwarfObject
    {
        public virtual string ToLink(bool link = true, DwarfObject pov = null)
        {
            return "";
        }
        public virtual string Print(bool link = true)
        {
            return "";
        }
    }
}
