using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LegendsViewer
{
    public abstract class PageControl
    {
        public DwarfTabControl TabControl;
        public string Title;
        public abstract Control GetControl();
        public abstract void Dispose();
        public abstract void Refresh();
    }
}
