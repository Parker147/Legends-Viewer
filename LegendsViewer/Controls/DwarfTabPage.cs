using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LegendsViewer.Controls;

namespace LegendsViewer
{
    public class DwarfTabPage : System.Windows.Forms.TabPage
    {
        private readonly Stack<PageControl> BackHistory = new Stack<PageControl>();
        private readonly Stack<PageControl> ForwardHistory = new Stack<PageControl>();
        public PageControl Current;

        public DwarfTabPage(PageControl pageControl)
        {
            Current = pageControl;
            LoadPageControl();
        }

        public void NewPageControl(PageControl pageControl)
        {
            while (ForwardHistory.Any())
            {
                var temp = ForwardHistory.Pop();
                (temp as HTMLControl)?.DisposePrinter();
                temp.Dispose();
            }
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
            if (newControl.GetType() == typeof(ChartPanel) && Current.TabControl.SelectedTab != null)
            {
                Width = Current.TabControl.SelectedTab.Width;
                Refresh();
                (newControl as ChartPanel)?.RefreshAllSeries();
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

        protected override void Dispose(bool disposing)
        {
            foreach (var forward in ForwardHistory)
            {
                (forward as HTMLControl)?.DisposePrinter();
                forward.Dispose();
            }
            foreach (var back in BackHistory)
            {
                (back as HTMLControl)?.DisposePrinter();
                back.Dispose();
            }
            (Current as HTMLControl)?.DisposePrinter();
            Current.Dispose();
            base.Dispose(disposing);
        }
    }
}
