using System.Collections.Generic;
using System.Windows.Forms;
using LegendsViewer.Controls.Chart;

namespace LegendsViewer.Controls
{
    public class DwarfTabPage : TabPage
    {
        private readonly Stack<PageControl> _backHistory = new Stack<PageControl>();
        private readonly Stack<PageControl> _forwardHistory = new Stack<PageControl>();

        public PageControl Current { get; private set; }

        public DwarfTabPage(PageControl pageControl)
        {
            Current = pageControl;
            LoadPageControl();
        }

        public void NewPageControl(PageControl pageControl)
        {
            foreach (var control in _forwardHistory)
            {
                control.Dispose();
            }
            _forwardHistory.Clear();

            if (Current != null)
            {
                _backHistory.Push(Current);
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
            if (_backHistory.Count > 0)
            {
                _forwardHistory.Push(Current);
                Current = _backHistory.Pop();
                LoadPageControl();
            }
        }

        public void Forward()
        {
            if (_forwardHistory.Count > 0)
            {
                _backHistory.Push(Current);
                Current = _forwardHistory.Pop();
                LoadPageControl();
            }
        }

        protected override void Dispose(bool disposing)
        {
            foreach (var forward in _forwardHistory)
            {
                forward.Dispose();
            }
            foreach (var back in _backHistory)
            {
                back.Dispose();
            }
            Current.Dispose();
            base.Dispose(disposing);
        }
    }
}
