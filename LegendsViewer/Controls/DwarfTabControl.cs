using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using LegendsViewer;
using LegendsViewer.Legends;
using LegendsViewer.Controls;
using LegendsViewer.Controls.Map;
using LegendsViewer.Controls.Query;

namespace LegendsViewer
{
    public class DwarfTabControl : TabControlImproved //System.Windows.Forms.TabControl
    {
        public World World;
        public bool NewTab = false;
        public DwarfTabControl(World world) : base() { World = world; }

        public void Navigate(ControlOption control, object navigateObject = null)
        {
            PageControl newControl = null;
            switch(control)
            {
                case ControlOption.HTML:
                    if (navigateObject != null)
                        newControl = new HTMLControl(navigateObject, this, World); 
                    break;
                case ControlOption.Chart:
                    newControl = new ChartControl(World, navigateObject as DwarfObject, this); break;
                case ControlOption.Map:
                    newControl = new MapControl(World, navigateObject, this); break;
                case ControlOption.Search:
                    newControl = new SearchControl(this); break;
            }

            if (newControl != null)
            {
                if (MakeNewTab())
                {
                    TabPages.Add(new DwarfTabPage(newControl));
                }
                else
                {
                    (SelectedTab as DwarfTabPage).NewPageControl(newControl);
                }
            }

        }

        private bool MakeNewTab()
        {
            return TabPages.Count == 0 || Control.ModifierKeys == Keys.Control || NewTab;
        }

        public void Back() 
        {
            if (SelectedTab != null)
                (SelectedTab as DwarfTabPage).Back();
        }

        public void Forward() 
        { 
            if (SelectedTab != null) 
                (SelectedTab as DwarfTabPage).Forward(); 
        }

        public override void CloseTab(int index = -1)
        {
            if (SelectedIndex == -1) return;
            if (index == -1) index = SelectedIndex;
            if (TabPages.Count > 0)
            {
                (TabPages[index] as DwarfTabPage).Close();
                int newSelectedIndex = SelectedIndex;
                if (index <= SelectedIndex) newSelectedIndex = SelectedIndex - 1;
                TabPage temp = TabPages[index];
                TabPages.Remove(TabPages[index]);
                temp.Dispose();
                if (TabPages.Count > 0 && newSelectedIndex == -1) SelectedIndex = 0;
                else SelectedIndex = newSelectedIndex;
            }
            GC.Collect();
        }
        public void RefreshAll(Type refreshType)
        {
            foreach (DwarfTabPage page in TabPages.OfType<DwarfTabPage>())
                //if (page.BackHistory.Count > 0)
                    if (page.Current.GetType() == typeof(HTMLControl)
                        && (page.Current as HTMLControl).HTMLObject.GetType() == refreshType)
                        page.Current.Refresh();
                    else if (page.Current.GetType() == typeof(ChartControl) && (page.Current as ChartControl).FocusObject.GetType() == refreshType)
                        page.Current.Refresh();
        }

        public void Reset()
        {
            while (TabPages.Count > 0)
                CloseTab();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg > 512 && m.Msg <= 528)
                switch (m.WParam.ToInt64())
                {
                    case 66059:
                    case (int)MouseButtons.XButton1: Back(); break;
                    case 131595:
                    case (int)MouseButtons.XButton2: Forward(); break;
                }
            base.WndProc(ref m);
        }

    }

    public class TabControlImproved : System.Windows.Forms.TabControl
    {
        private int _hotTabIndex = -1;

        public TabControlImproved()
            : base()
        {
            AllowDrop = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        #region Properties

        private Point DragStartPosition = Point.Empty;
        private int CloseButtonHeight
        {
            get { return FontHeight; }
        }

        private int HotTabIndex
        {
            get { return _hotTabIndex; }
            set
            {
                if (_hotTabIndex != value)
                {
                    _hotTabIndex = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region Overridden Methods

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.OnFontChanged(EventArgs.Empty);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            IntPtr hFont = this.Font.ToHfont();
            SendMessage(this.Handle, WM_SETFONT, hFont, new IntPtr(-1));
            SendMessage(this.Handle, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);
            this.UpdateStyles();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            DragStartPosition = new Point(e.X, e.Y);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            TCHITTESTINFO HTI = new TCHITTESTINFO(e.X, e.Y);
            HotTabIndex = SendMessage(this.Handle, TCM_HITTEST, IntPtr.Zero, ref HTI);

            if (e.Button != MouseButtons.Left) return;

            Rectangle r = new Rectangle(DragStartPosition, Size.Empty);
            r.Inflate(SystemInformation.DragSize);

            TabPage tp = HoverTab();

            if (tp != null)
            {
                if (!r.Contains(e.X, e.Y))
                    this.DoDragDrop(tp, DragDropEffects.All);
            }
            DragStartPosition = Point.Empty;
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);

            TabPage hover_Tab = HoverTab();
            if (hover_Tab == null)
                e.Effect = DragDropEffects.None;
            else
            {
                if (e.Data.GetDataPresent(typeof(DwarfTabPage)))
                {
                    e.Effect = DragDropEffects.Move;
                    TabPage drag_tab = (TabPage)e.Data.GetData(typeof(DwarfTabPage));

                    if (hover_Tab == drag_tab) return;

                    Rectangle TabRect = this.GetTabRect(this.TabPages.IndexOf(hover_Tab));
                    TabRect.Inflate(-3, -3);
                    if (TabRect.Contains(this.PointToClient(new Point(e.X, e.Y))))
                    {
                        SwapTabPages(drag_tab, hover_Tab);
                        this.SelectedTab = drag_tab;
                    }
                }
            }
        }

        private TabPage HoverTab()
        {
            for (int index = 0; index <= this.TabCount - 1; index++)
            {
                if (this.GetTabRect(index).Contains(this.PointToClient(System.Windows.Forms.Cursor.Position)))
                    return this.TabPages[index];
            }
            return null;
        }


        private void SwapTabPages(TabPage tp1, TabPage tp2)
        {
            int Index1 = this.TabPages.IndexOf(tp1);
            int Index2 = this.TabPages.IndexOf(tp2);
            this.TabPages[Index1] = tp2;
            this.TabPages[Index2] = tp1;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            HotTabIndex = -1;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            for (int id = 0; id < this.TabCount; id++)
                DrawTabBackground(pevent.Graphics, id);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            for (int id = 0; id < this.TabCount; id++)
                DrawTabContent(e.Graphics, id);
        }

        public virtual void CloseTab(int index)
        {
            TabPages.RemoveAt(index);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == TCM_SETPADDING)
                m.LParam = MAKELPARAM(this.Padding.X + CloseButtonHeight / 2, this.Padding.Y);

            if (m.Msg == WM_MOUSEDOWN && !this.DesignMode && HotTabIndex >= 0)
            {
                Point pt = this.PointToClient(System.Windows.Forms.Cursor.Position);
                Rectangle closeRect = GetCloseButtonRect(HotTabIndex);
                if (closeRect.Contains(pt))
                {
                    CloseTab(HotTabIndex);
                    //TabPages.RemoveAt(HotTabIndex);
                    m.Msg = WM_NULL;
                }
            }
            base.WndProc(ref m);
        }

        #endregion

        #region Private Methods

        private IntPtr MAKELPARAM(int lo, int hi)
        {
            return new IntPtr((hi << 16) | (lo & 0xFFFF));
        }

        private void DrawTabBackground(Graphics graphics, int id)
        {
            Rectangle rc = GetTabRect(id);
            if (id == SelectedIndex)
                graphics.FillRectangle(Brushes.DarkGray, rc);
            else if (id == HotTabIndex)
            {
                rc.Width--;
                rc.Height--;
                graphics.DrawRectangle(Pens.DarkGray, rc);
            }
            else
                graphics.DrawLine(Pens.DarkGray, rc.Left, rc.Bottom, rc.Right, rc.Bottom);
        }

        private void DrawTabContent(Graphics graphics, int id)
        {
            bool selectedOrHot = id == this.SelectedIndex || id == this.HotTabIndex;
            bool vertical = this.Alignment >= TabAlignment.Left;

            Image tabImage = null;

            if (this.ImageList != null)
            {
                TabPage page = this.TabPages[id];
                if (page.ImageIndex > -1 && page.ImageIndex < this.ImageList.Images.Count)
                    tabImage = this.ImageList.Images[page.ImageIndex];

                if (page.ImageKey.Length > 0 && this.ImageList.Images.ContainsKey(page.ImageKey))
                    tabImage = this.ImageList.Images[page.ImageKey];
            }

            Rectangle tabRect = GetTabRect(id);
            Rectangle contentRect = vertical ? new Rectangle(0, 0, tabRect.Height, tabRect.Width) : new Rectangle(Point.Empty, tabRect.Size);
            Rectangle textrect = contentRect;
            textrect.Width -= FontHeight;

            if (tabImage != null)
            {
                textrect.Width -= tabImage.Width;
                textrect.X += tabImage.Width;
            }

            Color frColor = id == SelectedIndex ? Color.White : this.ForeColor;
            Color bkColor = id == SelectedIndex ? Color.DarkGray : this.BackColor;

            using (Bitmap bm = new Bitmap(contentRect.Width, contentRect.Height))
            {
                using (Graphics bmGraphics = Graphics.FromImage(bm))
                {
                    TextRenderer.DrawText(bmGraphics, this.TabPages[id].Text, this.Font, textrect, frColor, bkColor);
                    if (selectedOrHot)
                    {
                        Rectangle closeRect = new Rectangle(contentRect.Right - CloseButtonHeight, 0, CloseButtonHeight, CloseButtonHeight);
                        closeRect.Offset(-2, (contentRect.Height - closeRect.Height) / 2);
                        DrawCloseButton(bmGraphics, closeRect);
                    }

                    if (tabImage != null)
                    {
                        Rectangle imageRect = new Rectangle(Padding.X, 0, tabImage.Width, tabImage.Height);
                        imageRect.Offset(0, (contentRect.Height - imageRect.Height) / 2);
                        bmGraphics.DrawImage(tabImage, imageRect);
                    }
                }

                if (vertical)
                {
                    if (this.Alignment == TabAlignment.Left)
                        bm.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    else
                        bm.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
                graphics.DrawImage(bm, tabRect);

            }
        }

        private void DrawCloseButton(Graphics graphics, Rectangle bounds)
        {
            graphics.FillRectangle(Brushes.Red, bounds);
            using (Font closeFont = new Font("Arial", Font.Size, FontStyle.Bold))
                TextRenderer.DrawText(graphics, "X", closeFont, bounds, Color.White, Color.Red, TextFormatFlags.HorizontalCenter | TextFormatFlags.NoPadding | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);

        }

        private Rectangle GetCloseButtonRect(int id)
        {
            Rectangle tabRect = GetTabRect(id);
            Rectangle closeRect = new Rectangle(tabRect.Left, tabRect.Top, CloseButtonHeight, CloseButtonHeight);

            switch (Alignment)
            {
                case TabAlignment.Left:
                    closeRect.Offset((tabRect.Width - closeRect.Width) / 2, 0);
                    break;
                case TabAlignment.Right:
                    closeRect.Offset((tabRect.Width - closeRect.Width) / 2, tabRect.Height - closeRect.Height);
                    break;
                default:
                    closeRect.Offset(tabRect.Width - closeRect.Width, (tabRect.Height - closeRect.Height) / 2);
                    break;
            }

            return closeRect;
        }

        #endregion

        #region Interop

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wParam, ref TCHITTESTINFO lParam);

        [StructLayout(LayoutKind.Sequential)]
        private struct TCHITTESTINFO
        {
            public Point pt;
            public TCHITTESTFLAGS flags;
            public TCHITTESTINFO(int x, int y)
            {
                pt = new Point(x, y);
                flags = TCHITTESTFLAGS.TCHT_NOWHERE;
            }
        }

        [Flags()]
        private enum TCHITTESTFLAGS
        {
            TCHT_NOWHERE = 1,
            TCHT_ONITEMICON = 2,
            TCHT_ONITEMLABEL = 4,
            TCHT_ONITEM = TCHT_ONITEMICON | TCHT_ONITEMLABEL
        }

        private const int WM_NULL = 0x0;
        private const int WM_SETFONT = 0x30;
        private const int WM_FONTCHANGE = 0x1D;
        private const int WM_MOUSEDOWN = 0x201;

        private const int TCM_FIRST = 0x1300;
        private const int TCM_HITTEST = TCM_FIRST + 13;
        private const int TCM_SETPADDING = TCM_FIRST + 43;

        #endregion

    }




}
