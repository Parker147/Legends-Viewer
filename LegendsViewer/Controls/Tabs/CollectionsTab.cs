using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BrightIdeasSoftware;
using LegendsViewer.Legends;

namespace LegendsViewer.Controls.Tabs
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class CollectionsTab : BaseSearchTab
    {
        public CollectionsTab()
        {
            InitializeComponent();
        }


        internal override void InitializeTab()
        {
            EventTabs = new TabPage[] { tpEraEvents };
            EventTabTypes = new Type[] { typeof(Era) };

            listEraSearch.ShowGroups = false;
        }

        internal override void AfterLoad(World world)
        {
            base.AfterLoad(world);


            listEraSearch.SetObjects(World.Eras.ToArray());

            numStart.Maximum = numEraEnd.Value = numEraEnd.Maximum = World.Events.Last().Year;

            Coordinator.Form.DontRefreshBrowserPages = true;
            foreach (CheckBox eraCheck in tpEraEvents.Controls.OfType<CheckBox>())
                eraCheck.Checked = false;
            Coordinator.Form.DontRefreshBrowserPages = false;

            var eventTypes = from eventType in World.Events
                             group eventType by eventType.Type into type
                             select type.Key;

            TabEvents.Clear();
            TabEvents.Add(eventTypes.ToList());
        }

        internal override void DoSearch()
        {
            base.DoSearch();
        }

        internal override void ResetTab()
        {
            Coordinator.Form.DontRefreshBrowserPages = true;
            foreach (CheckBox eraCheck in tpEraEvents.Controls.OfType<CheckBox>())
                eraCheck.Checked = false;
            Coordinator.Form.DontRefreshBrowserPages = false;

            listEraSearch.Items.Clear();
            numStart.Value = -1;
            numEraEnd.Value = 0;
        }

        private void UpdateCounts(Label label, int shown, int total)
        {
            label.Text = $"{shown} / {total}";
        }

        private void btnEraShow_Click(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                Browser.Navigate(ControlOption.HTML, new Era(Convert.ToInt32(numStart.Value), Convert.ToInt32(numEraEnd.Value), World));
            }
        }

        private void listEras_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSearch_SelectedIndexChanged(sender, e);
        }
    }
}
