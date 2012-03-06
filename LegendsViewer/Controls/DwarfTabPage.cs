using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LegendsViewer
{
    public class DwarfTabPage : System.Windows.Forms.TabPage
    {
        private Stack<PageControl> BackHistory = new Stack<PageControl>(), ForwardHistory = new Stack<PageControl>();
        public PageControl Current;

        public DwarfTabPage(PageControl pageControl)
        {
            Current = pageControl;
            LoadPageControl();
        }

        public void NewPageControl(PageControl pageControl)
        {
            ForwardHistory.Clear();
            if (Current != null)
            {
                Current.Dispose();
                BackHistory.Push(Current);
            }
            Current = pageControl;
            LoadPageControl();
        }

        private void LoadPageControl()
        {
            Controls.Clear();
            Text = Current.Title;
            Control newControl = Current.GetControl();
            Controls.Add(newControl);
            if (newControl.GetType() == typeof(ChartPanel))
            {
                this.Width = Current.TabControl.SelectedTab.Width;
                this.Refresh();
                (newControl as ChartPanel).RefreshAllSeries();
            }
        }

        public void Back()
        {
            if (BackHistory.Count > 0)
            {
                Current.Dispose();
                ForwardHistory.Push(Current);
                Current = BackHistory.Pop();
                LoadPageControl();
            }
        }

        public void Forward()
        {
            if (ForwardHistory.Count > 0)
            {
                Current.Dispose();
                BackHistory.Push(Current);
                Current = ForwardHistory.Pop();
                LoadPageControl();
            }
        }

        public void Close()
        {
            Current.Dispose();
            Controls.Clear();
        }
    }
}
