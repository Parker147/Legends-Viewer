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
        private WorldConstructionsList worldConstructionSearch;
        private StructuresList structureSearch;

        public CollectionsTab()
        {
            InitializeComponent();
        }


        internal override void InitializeTab()
        {
            EventTabs = new TabPage[] { tpEraEvents, tpStructureEvents, tpWorldConstructionEvents };
            EventTabTypes = new Type[] { typeof(Artifact), typeof(Era), typeof(Structure), typeof(WorldConstruction), typeof(WrittenContent) };

            listStructureSearch.ShowGroups = false;

            listWorldConstructionsSearch.ShowGroups = false;

            listEraSearch.ShowGroups = false;
        }

        internal override void AfterLoad(World world)
        {
            base.AfterLoad(world);

            worldConstructionSearch = new WorldConstructionsList(World);
            structureSearch = new StructuresList(World);

            var structures = from structure in World.Structures
                             orderby structure.Type.GetDescription()
                             group structure by structure.Type.GetDescription() into structuretype
                             select structuretype;
            var worldconstructions = from construction in World.WorldConstructions
                                     orderby construction.Type.GetDescription()
                                     group construction by construction.Type.GetDescription() into constructiontype
                                     select constructiontype;

            listEraSearch.SetObjects(World.Eras.ToArray());

            cmbStructureType.Items.Add("All"); cmbStructureType.SelectedIndex = 0;
            foreach (var structure in structures)
                cmbStructureType.Items.Add(structure.Key);
            cmbConstructionType.Items.Add("All"); cmbConstructionType.SelectedIndex = 0;
            foreach (var construction in worldconstructions)
                cmbConstructionType.Items.Add(construction.Key);

            numStart.Maximum = numEraEnd.Value = numEraEnd.Maximum = World.Events.Last().Year;

            Coordinator.Form.DontRefreshBrowserPages = true;
            foreach (CheckBox eraCheck in tpEraEvents.Controls.OfType<CheckBox>())
                eraCheck.Checked = false;
            Coordinator.Form.DontRefreshBrowserPages = false;


            var worldConstructionEvents = from eventType in World.WorldConstructions.SelectMany(element => element.Events)
                                          group eventType by eventType.Type into type
                                          select type.Key;

            var structureEvents = from eventType in World.Structures.SelectMany(element => element.Events)
                                  group eventType by eventType.Type into type
                                  select type.Key;

            var eventTypes = from eventType in World.Events
                             group eventType by eventType.Type into type
                             select type.Key;

            TabEvents.Clear();
            TabEvents.Add(eventTypes.ToList());
            TabEvents.Add(structureEvents.ToList());
            TabEvents.Add(worldConstructionEvents.ToList());
        }

        internal override void DoSearch()
        {
            searchStructureList(null, null);
            searchWorldConstructionList(null, null);
            base.DoSearch();
        }

        internal override void ResetTab()
        {


            Coordinator.Form.DontRefreshBrowserPages = true;
            foreach (CheckBox eraCheck in tpEraEvents.Controls.OfType<CheckBox>())
                eraCheck.Checked = false;
            Coordinator.Form.DontRefreshBrowserPages = false;

            txtWorldConstructionsSearch.Clear();
            cmbConstructionType.Items.Clear();
            listWorldConstructionsSearch.Items.Clear();
            radWorldConstructionsSortNone.Checked = true;

            txtStructuresSearch.Clear();
            cmbStructureType.Items.Clear();
            listStructureSearch.Items.Clear();
            radStructuresSortNone.Checked = true;

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

        private void searchWorldConstructionList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                worldConstructionSearch.Name = txtWorldConstructionsSearch.Text;
                worldConstructionSearch.Type = cmbConstructionType.SelectedItem.ToString();
                worldConstructionSearch.sortEvents = radWorldConstructionsSortEvents.Checked;
                worldConstructionSearch.sortFiltered = radWorldConstructionsSortFiltered.Checked;
                IEnumerable<WorldConstruction> list = worldConstructionSearch.GetList();
                var results = list.ToArray();
                listWorldConstructionsSearch.SetObjects(results);
                UpdateCounts(lblWorldConstructionResult, results.Length, worldConstructionSearch.BaseList.Count);
            }
        }

        private void searchStructureList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                structureSearch.Name = txtStructuresSearch.Text;
                structureSearch.Type = cmbStructureType.SelectedItem.ToString();
                structureSearch.sortEvents = radStructuresSortEvents.Checked;
                structureSearch.sortFiltered = radStructuresSortFiltered.Checked;
                IEnumerable<Structure> list = structureSearch.GetList();
                var results = list.ToArray();
                listStructureSearch.SetObjects(list.ToArray());
                UpdateCounts(lblStructureResults, results.Length, structureSearch.BaseList.Count);
            }
        }

        private void listEras_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSearch_SelectedIndexChanged(sender, e);
        }

        private void listWorldConstructionsSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSearch_SelectedIndexChanged(sender, e);
        }

        private void listStructuresSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSearch_SelectedIndexChanged(sender, e);
        }
    }
}
