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
    public partial class GeographyTab : BaseSearchTab
    {
        private LandmassesList landmassSearch;
        private MountainPeaksList mountainPeaksSearch;
        private RegionsList regionSearch;
        private UndergroundRegionsList uRegionSearch;

        public GeographyTab()
        {
            InitializeComponent();

        }


        internal override void InitializeTab()
        {
            EventTabs = new TabPage[] { tpRegionEvents, tpURegionEvents, tpLandmassEvents, tpMountainPeakEvents };
            EventTabTypes = new Type[] { typeof(Region), typeof(UndergroundRegion), typeof(Landmass), typeof(MountainPeak) };

            listRegionSearch.AllColumns.Add(new OLVColumn
            {
                Text = "Deaths",
                TextAlign = HorizontalAlignment.Right,
                IsVisible = false,
                AspectGetter = item => ((WorldRegion)item).Deaths.Count
            });
            listRegionSearch.AllColumns.Add(new OLVColumn
            {
                Text = "Events",
                TextAlign = HorizontalAlignment.Right,
                IsVisible = false,
                AspectGetter = rowObject => ((WorldRegion)rowObject).Events.Count
            });
            listRegionSearch.ShowGroups = false;

            listURegionSearch.ShowGroups = false;

            listLandmassesSearch.ShowGroups = false;

            listMountainPeakSearch.ShowGroups = false;
        }

        internal override void AfterLoad(World world)
        {
            base.AfterLoad(world);
            regionSearch = new RegionsList(World);
            uRegionSearch = new UndergroundRegionsList(World);
            landmassSearch = new LandmassesList(World);
            mountainPeaksSearch = new MountainPeaksList(World);

            var regions = from region in World.Regions
                          orderby region.Type
                          group region by region.Type into regiontype
                          select regiontype;
            var uregions = from uregion in World.UndergroundRegions
                           orderby uregion.Type
                           group uregion by uregion.Type into uregiontype
                           select uregiontype;

            cmbRegionType.Items.Add("All"); cmbRegionType.SelectedIndex = 0;
            foreach (var region in regions)
                cmbRegionType.Items.Add(region.Key);
            cmbURegionType.Items.Add("All"); cmbURegionType.SelectedIndex = 0;
            foreach (var uregion in uregions)
                cmbURegionType.Items.Add(uregion.Key);


            var regionEvents = from eventType in World.Regions.SelectMany(region => region.Events)
                               group eventType by eventType.Type into type
                               select type.Key;
            var undergroundRegionEvents = from eventType in World.UndergroundRegions.SelectMany(uRegion => uRegion.Events)
                                          group eventType by eventType.Type into type
                                          select type.Key;
            var landmassEvents = from eventType in World.Landmasses.SelectMany(element => element.Events)
                                 group eventType by eventType.Type into type
                                 select type.Key;

            var mountainPeakEvents = from eventType in World.MountainPeaks.SelectMany(element => element.Events)
                                     group eventType by eventType.Type into type
                                     select type.Key;

            TabEvents.Clear();
            TabEvents.Add(regionEvents.ToList());
            TabEvents.Add(undergroundRegionEvents.ToList());
            TabEvents.Add(landmassEvents.ToList());
            TabEvents.Add(mountainPeakEvents.ToList());

        }

        internal override void ResetTab()
        {

            txtRegionSearch.Clear();
            listRegionSearch.Items.Clear();
            cmbRegionType.Items.Clear();
            radRegionNone.Checked = true;

            listURegionSearch.Items.Clear();
            cmbURegionType.Items.Clear();
            radURegionNone.Checked = true;
        }



        private void searchRegionList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                regionSearch.name = txtRegionSearch.Text;
                regionSearch.type = cmbRegionType.SelectedItem.ToString();
                regionSearch.sortEvents = radRegionSortEvents.Checked;
                regionSearch.sortFiltered = radRegionSortFiltered.Checked;
                regionSearch.sortBattles = radRegionSortBattles.Checked;
                regionSearch.SortDeaths = radRegionSortDeaths.Checked;
                IEnumerable<WorldRegion> list = regionSearch.getList();
                var results = list.ToArray();
                listRegionSearch.SetObjects(results);
                //listRegionSearch.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                UpdateCounts(lblShownResults, results.Length, regionSearch.BaseList.Count);
            }
        }

        private void UpdateCounts(Label label, int shown, int total)
        {
            label.Text = $"{shown} / {total}";
        }

        private void searchURegionList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                uRegionSearch.type = cmbURegionType.SelectedItem.ToString();
                uRegionSearch.sortEvents = radURegionSortEvents.Checked;
                uRegionSearch.sortFiltered = radURegionSortFiltered.Checked;
                IEnumerable<UndergroundRegion> list = uRegionSearch.getList();
                var results = list.ToArray();
                listURegionSearch.SetObjects(results);
                UpdateCounts(lblURegionResults, results.Length, uRegionSearch.BaseList.Count);
            }
        }


        private void searchLandmassList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                landmassSearch.Name = txtLandmassSearch.Text;
                landmassSearch.sortEvents = radLandmassEvents.Checked;
                landmassSearch.sortFiltered = radLandmassFiltered.Checked;
                IEnumerable<Landmass> list = landmassSearch.getList();
                var results = list.ToArray();
                listLandmassesSearch.SetObjects(results);
                UpdateCounts(lblLandmassResults, results.Length, landmassSearch.BaseList.Count);
            }
        }

        private void searchMountainPeakList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                mountainPeaksSearch.Name = txtMountainPeakSearch.Text;
                mountainPeaksSearch.sortEvents = radMountainPeakEvents.Checked;
                mountainPeaksSearch.sortFiltered = radMountainPeakFiltered.Checked;
                IEnumerable<MountainPeak> list = mountainPeaksSearch.getList();
                var results = list.ToArray();
                listMountainPeakSearch.SetObjects(results);
                UpdateCounts(lblMountainPeakResults, results.Length, mountainPeaksSearch.BaseList.Count);
            }
        }

        private void listRegionSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSearch_SelectedIndexChanged(sender, e);
        }

        private void listURegionSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSearch_SelectedIndexChanged(sender, e);
        }

        private void listLandmassSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSearch_SelectedIndexChanged(sender, e);
        }

        private void listMountainPeakSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSearch_SelectedIndexChanged(sender, e);
        }
    }
}
