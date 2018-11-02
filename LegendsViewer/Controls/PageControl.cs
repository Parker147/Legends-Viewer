using System;
using System.Windows.Forms;

namespace LegendsViewer.Controls
{
    public abstract class PageControl : IDisposable
    {
        public DwarfTabControl TabControl;
        public string Title;

        internal bool Disposed;

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
