using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LegendsViewer.Legends;
using LegendsViewer.Controls;
using System.Diagnostics;
using LegendsViewer.Controls.HTML.Utilities;
using LegendsViewer.Controls.Tabs;
using LegendsViewer.Legends.EventCollections;

namespace LegendsViewer
{

    public partial class frmLegendsViewer : Form
    {
        string version = "n/a";
        internal DwarfTabControl Browser;
        internal bool DontRefreshBrowserPages = true;
        private string CommandFile;

        internal World World { get; private set; }

        internal FileLoader FileLoader { get; }

        private LVCoordinator Coordinator { get; set; }

        public frmLegendsViewer(string file = "")
        {
            InitializeComponent();

            // Start local http server
            LocalFileProvider.Run();

            Coordinator = new LVCoordinator(this);

            FileLoader = summaryTab1.CreateLoader();
            FileLoader.AfterLoad += (sender, args) => this.AfterLoad(args.Arg);

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            version = fvi.FileVersion;

            Text = "Legends Viewer";
            lblVersion.Text = "v" + version;
            lblVersion.Left = scWorld.Panel2.ClientSize.Width - lblVersion.Width - 3;
            tcWorld.Height = scWorld.Panel2.ClientSize.Height;

            Browser = new DwarfTabControl(World);
            Browser.Location = new Point(0, btnBack.Bottom + 3);
            Browser.Size = new Size(scWorld.Panel2.ClientSize.Width - Browser.Left, scWorld.Panel2.ClientSize.Height - Browser.Top);
            Browser.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right);
            scWorld.Panel2.Controls.Add(Browser);
            foreach (TabPage tp in tcWorld.TabPages)
                foreach (TabControl tabControl in tp.Controls.OfType<TabControl>())
                    HideTabControlBorder(tabControl);
            if (file != "")
                CommandFile = file;

            foreach (var v in tcWorld.TabPages.OfType<TabPage>().SelectMany(x => x.Controls.OfType<BaseSearchTab>()))
                v.Coordinator = Coordinator;

            BrowserUtil.SetBrowserEmulationMode();
            Browser.Navigate(ControlOption.ReadMe);
        }

        private void frmLegendsViewer_Shown(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CommandFile))
                FileLoader.AttemptLoadFrom(CommandFile);
        }

        private void HideTabControlBorder(TabControl tc)
        {
            Size tcSize = tc.Size;
            tc.Dock = DockStyle.None;
            tc.Size = tcSize;
            tc.Location = new Point(0, 0);
            tc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
            tc.Left -= 3;
            tc.Top -= 2;
            tc.Width += 6;
            tc.Height += 4;
            foreach (TabPage tp in tc.TabPages)
                foreach (var tabControl in tp.Controls.OfType<TabControl>())
                    HideTabControlBorder(tabControl);
        }

        private void AfterLoad(World loadedWorld)
        {
            if (!FileLoader.Working && World != null)
            {
                World.Dispose();
                foreach (Entity entity in World.Entities)
                {
                    entity.Identicon?.Dispose();
                }
                World = null;
            }

            World = loadedWorld;

            ResetForm();
            Application.DoEvents();

            Browser.World = World;
            Text += " - " + World.Name;
            Browser.Navigate(ControlOption.HTML, World);

            foreach (var v in tcWorld.TabPages.OfType<TabPage>().SelectMany(x => x.Controls.OfType<BaseSearchTab>()))
            {
                v.AfterLoad(loadedWorld);
            }

            dlgOpen.FileName = "";

            foreach (var v in tcWorld.TabPages.OfType<TabPage>().SelectMany(x => x.Controls.OfType<BaseSearchTab>()))
            {
                v.GenerateEventFilterCheckBoxes();
            }

            FileLoader.Working = false;
        }

        private void ResetForm()
        {
            Text = "Legends Viewer";
            foreach (var v in tcWorld.TabPages.OfType<TabPage>().SelectMany(x => x.Controls.OfType<BaseSearchTab>()))
            {
                v.ResetTab();
                v.ResetEvents();
            }
            Browser?.Reset();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Browser?.Back();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            Browser?.Forward();
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                Browser?.Navigate(ControlOption.HTML, World);
            }
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                Browser?.Navigate(ControlOption.Map);
            }
        }

        private void btnChart_Click(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                Browser?.Navigate(ControlOption.Chart, new Era(-1, World.Events.Last().Year, World));
            }
        }

        private void frmLegendsViewer_ResizeEnd(object sender, EventArgs e)
        {
            foreach (var chart in Browser.TabPages.OfType<DwarfTabPage>().Select(x => x.Current).OfType<ChartControl>())
                chart.DwarfChart.RefreshAllSeries();
        }

        public void ChangeBattleBaseList(List<Battle> battles, string mapBattles)
        {
            var tab = tpWarfare.Controls.OfType<WarfareTab>().FirstOrDefault();
            tab?.ChangeBattleBaseList(battles, mapBattles);

            tcWorld.SelectedTab = tpWarfare;
        }

        private void open_ReadMe(object sender, EventArgs e)
        {
            Browser.Navigate(ControlOption.ReadMe);
        }

        private void frmLegendsViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            World?.Dispose();
            LocalFileProvider.Stop();
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            (sender as TabControl)?.SelectedTab?.Controls.OfType<BaseSearchTab>().FirstOrDefault()?.DoSearch();
        }
    }
}
