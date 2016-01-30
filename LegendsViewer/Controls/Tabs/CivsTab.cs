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
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof (IDesigner))]
    public partial class CivsTab : BaseSearchTab
    {
        private EntitiesList entitySearch;
        public CivsTab()
        {
            InitializeComponent();

        }


        internal override void InitializeTab()
        {
            EventTabs = new TabPage[] { tpCivEvents};
            EventTabTypes = new Type[] { typeof(Entity) };

        }

        internal override void AfterLoad(World world)
        {
            base.AfterLoad(world);
            entitySearch = new EntitiesList(World);

            var civRaces = from civ in World.Entities.Where(entity => !string.IsNullOrWhiteSpace(entity.Name))
                           orderby civ.Race
                           group civ by civ.Race into civRace
                           select civRace;
            var populationTypes = from population in World.SitePopulations
                                  orderby population.Race
                                  group population by population.Race into type
                                  select type;
            var civPopulationTypes = from civPopulation in populationTypes
                                     where World.Entities.Count(entity => entity.Populations.Count(population => population.Race == civPopulation.Key) > 0) > 0
                                     select civPopulation;

            cmbCivRace.Items.Add("All"); cmbCivRace.SelectedIndex = 0;
            foreach (var civRace in civRaces)
                cmbCivRace.Items.Add(civRace.Key);
            cmbEntityPopulation.Items.Add("All"); cmbEntityPopulation.SelectedIndex = 0;
            foreach (var civPopulation in civPopulationTypes)
                cmbEntityPopulation.Items.Add(civPopulation.Key);

            var entityEvents = from eventType in World.Entities.SelectMany(hf => hf.Events)
                               group eventType by eventType.Type into type
                               select type.Key;
            TabEvents.Clear();
            TabEvents.Add(entityEvents.ToList());
        }

        internal override void ResetTab()
        {

            txtCivSearch.Clear();
            listCivSearch.Items.Clear();
            cmbCivRace.Items.Clear();
            cmbEntityPopulation.Items.Clear();
            chkCiv.Checked = false;
            radEntityNone.Checked = true;

        }

        private void searchEntityList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                if (sender == cmbEntityPopulation && !radEntitySortPopulation.Checked) radEntitySortPopulation.Checked = true;
                else
                {
                    entitySearch.name = txtCivSearch.Text;
                    entitySearch.race = cmbCivRace.SelectedItem.ToString();
                    entitySearch.civs = chkCiv.Checked;
                    entitySearch.PopulationType = cmbEntityPopulation.SelectedItem.ToString();
                    entitySearch.sortSites = radCivSites.Checked;
                    entitySearch.sortEvents = radEntitySortEvents.Checked;
                    entitySearch.sortFiltered = radCivSortFiltered.Checked;
                    entitySearch.sortWars = radCivSortWars.Checked;
                    entitySearch.SortPopulation = radEntitySortPopulation.Checked;
                    IEnumerable<Entity> list = entitySearch.getList();
                    listCivSearch.Items.Clear();
                    listCivSearch.Items.AddRange(list.ToArray());
                }
            }
        }

        public void resetCivBaseList(object sender, EventArgs e)
        {
            //if (!FileLoader.Working && World != null)
            //{
            //    lblHFList.Text = "All";
            //    lblHFList.ForeColor = Control.DefaultForeColor;
            //    lblHFList.Font = new Font(lblHFList.Font.FontFamily, lblHFList.Font.Size, FontStyle.Regular);
            //    hfSearch.BaseList = World.HistoricalFigures;
            //    searchHFList(null, null);
            //}
        }

        private void listCivSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSearch_SelectedIndexChanged(sender, e);
        }
    }
}
