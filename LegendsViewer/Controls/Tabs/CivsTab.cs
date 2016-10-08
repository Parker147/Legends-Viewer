using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using LegendsViewer.Legends;
using BrightIdeasSoftware;

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

            listCivSearch.AllColumns.Add(new OLVColumn { AspectName = "Race", IsVisible = false, Text = "Race", TextAlign = HorizontalAlignment.Left });
            listCivSearch.AllColumns.Add(new OLVColumn
            {
                Text = "Events",
                TextAlign = HorizontalAlignment.Right,
                IsVisible = false,
                AspectGetter = rowObject => ((Entity)rowObject).Events.Count
            });
            listCivSearch.AllColumns.Add(new OLVColumn
            {
                Text = "Sites",
                TextAlign = HorizontalAlignment.Right,
                IsVisible = false,
                AspectGetter = rowObject => ((Entity)rowObject).CurrentSites.Count
            });
            listCivSearch.ShowGroups = false;
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
            var entites = from entity in world.Entities
                          orderby entity.Type.GetDescription()
                          group entity by entity.Type.GetDescription() into entityType
                          select entityType;

            cmbCivRace.Items.Add("All"); cmbCivRace.SelectedIndex = 0;
            foreach (var civRace in civRaces)
                cmbCivRace.Items.Add(civRace.Key);
            cmbEntityPopulation.Items.Add("All"); cmbEntityPopulation.SelectedIndex = 0;
            foreach (var civPopulation in civPopulationTypes)
                cmbEntityPopulation.Items.Add(civPopulation.Key);
            cmbEntityType.Items.Add("All"); cmbEntityType.SelectedIndex = 0;
            foreach (var entity in entites)
                cmbEntityType.Items.Add(entity.Key);

            var entityEvents = from eventType in World.Entities.SelectMany(hf => hf.Events)
                               group eventType by eventType.Type into type
                               select type.Key;
            TabEvents.Clear();
            TabEvents.Add(entityEvents.ToList());
        }

        internal override void DoSearch()
        {
            searchEntityList(null, null);
            base.DoSearch();
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
                    entitySearch.Type = cmbEntityType.SelectedItem.ToString();
                    entitySearch.race = cmbCivRace.SelectedItem.ToString();
                    entitySearch.civs = chkCiv.Checked;
                    entitySearch.PopulationType = cmbEntityPopulation.SelectedItem.ToString();
                    entitySearch.sortSites = radCivSites.Checked;
                    entitySearch.sortEvents = radEntitySortEvents.Checked;
                    entitySearch.sortFiltered = radCivSortFiltered.Checked;
                    entitySearch.sortWars = radCivSortWars.Checked;
                    entitySearch.SortPopulation = radEntitySortPopulation.Checked;

                    IEnumerable<Entity> list = entitySearch.getList();
                    var results = list.ToArray();
                    listCivSearch.SetObjects(results);
                    //listCivSearch.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    UpdateCounts(results.Length, entitySearch.BaseList.Count);
                }
            }
        }

        private void listCivSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSearch_SelectedIndexChanged(sender, e);
        }

        private void UpdateCounts(int shown, int total)
        {
            lblShownResults.Text = $"{shown} / {total}";
        }
    }
}
