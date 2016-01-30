using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BrightIdeasSoftware;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls
{
    /// <summary>
    /// This class holds all the common bits of code that are used across multiple tabs.
    /// It also provides the tabs with access to form level elements, like status bar texts.
    /// </summary>
    public class LVCoordinator
    {
        public LVCoordinator(frmLegendsViewer form)
        {
            Form = form;
        }

        internal frmLegendsViewer Form { get; private set; }

        internal DwarfTabControl Browser => Form?.Browser;
        internal World World => Form?.World;
        internal FileLoader FileLoader => Form?.FileLoader;

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Legends Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ChangeHotItemStyle(ObjectListView olv, ComboBox cb)
        {
            olv.UseTranslucentHotItem = false;
            olv.UseHotItem = true;
            olv.UseExplorerTheme = false;

            switch (cb.SelectedIndex)
            {
                case 0:
                    olv.UseHotItem = false;
                    break;
                case 1:
                    HotItemStyle hotItemStyle = new HotItemStyle();
                    hotItemStyle.ForeColor = Color.AliceBlue;
                    hotItemStyle.BackColor = Color.FromArgb(255, 64, 64, 64);
                    olv.HotItemStyle = hotItemStyle;
                    break;
                case 2:
                    RowBorderDecoration rbd = new RowBorderDecoration();
                    rbd.BorderPen = new Pen(Color.SeaGreen, 2);
                    rbd.FillBrush = null;
                    rbd.CornerRounding = 4.0f;
                    HotItemStyle hotItemStyle2 = new HotItemStyle();
                    hotItemStyle2.Decoration = rbd;
                    olv.HotItemStyle = hotItemStyle2;
                    break;
                case 3:
                    olv.UseTranslucentHotItem = true;
                    break;
                case 4:
                    HotItemStyle hotItemStyle3 = new HotItemStyle();
                    hotItemStyle3.Decoration = new LightBoxDecoration();
                    olv.HotItemStyle = hotItemStyle3;
                    break;
                case 5:
                    olv.FullRowSelect = true;
                    olv.UseHotItem = false;
                    olv.UseExplorerTheme = true;
                    break;
            }
            olv.Invalidate();
        }

        public void HandleHotItemChanged(object sender, HotItemChangedEventArgs e)
        {
        }

        public void HandleSelectionChanged(ObjectListView listView)
        {

        }
    }
}
