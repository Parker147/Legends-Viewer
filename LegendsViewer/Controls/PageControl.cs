using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LegendsViewer
{
    public abstract class PageControl : IDisposable
    {
        public DwarfTabControl TabControl;
        public string Title;
        public abstract Control GetControl();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public abstract void Refresh();

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
