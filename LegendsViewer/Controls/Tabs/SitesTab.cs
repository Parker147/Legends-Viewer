using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BrightIdeasSoftware;
using LegendsViewer.Legends;
using WFC;

namespace LegendsViewer.Controls.Tabs
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class SitesTab : BaseSearchTab
    {
        private SitesList siteSearch;

        public SitesTab()
        {
            InitializeComponent();
        }

        internal override void InitializeTab()
        {
            lnkMaxResults.Text = WorldObjectList.MaxResults.ToString();
            MaxResultsLabels.Add(lnkMaxResults);

            EventTabs = new TabPage[] { tpSiteEvents };
            EventTabTypes = new Type[] { typeof(Site) };

            listSiteSearch.ShowGroups = false;
            listSiteSearch.AllColumns.Add(new OLVColumn
            {
                Text = "Structures",
                TextAlign = HorizontalAlignment.Right,
                IsVisible = false,
                AspectGetter = item => ((Site)item).Structures.Count
            });
            listSiteSearch.AllColumns.Add(new OLVColumn
            {
                Text = "Warfare",
                TextAlign = HorizontalAlignment.Right,
                IsVisible = false,
                AspectGetter = item => ((Site)item).Warfare.Count
            });
            listSiteSearch.AllColumns.Add(new OLVColumn
            {
                Text = "Battles",
                TextAlign = HorizontalAlignment.Right,
                IsVisible = false,
                AspectGetter = item => ((Site)item).Battles.Count
            });
            listSiteSearch.AllColumns.Add(new OLVColumn
            {
                Text = "Conquerings",
                TextAlign = HorizontalAlignment.Right,
                IsVisible = false,
                AspectGetter = item => ((Site)item).Conquerings.Count
            });
            listSiteSearch.AllColumns.Add(new OLVColumn
            {
                Text = "Current Owner",
                TextAlign = HorizontalAlignment.Right,
                IsVisible = false,
                AspectGetter = item => ((Site)item).CurrentOwner?.Print(false)
            });
            listSiteSearch.AllColumns.Add(new OLVColumn
            {
                Text = "Deaths",
                TextAlign = HorizontalAlignment.Right,
                IsVisible = false,
                AspectGetter = item => ((Site)item).Deaths.Count
            });
            listSiteSearch.AllColumns.Add(new OLVColumn
            {
                Text = "Beast Attacks",
                TextAlign = HorizontalAlignment.Right,
                IsVisible = false,
                AspectGetter = item => ((Site)item).BeastAttacks.Count
            });
            listSiteSearch.AllColumns.Add(new OLVColumn
            {
                Text = "Events",
                TextAlign = HorizontalAlignment.Right,
                IsVisible = false,
                AspectGetter = rowObject => ((Site)rowObject).Events.Count
            });
        }

        internal override void AfterLoad(World world)
        {
            base.AfterLoad(world);
            siteSearch = new SitesList(World);

            var sites = from site in World.Sites
                        where !string.IsNullOrWhiteSpace(site.Name)
                        orderby site.Type
                        group site by site.Type into sitetype
                        select sitetype;

            var populationTypes = from population in World.SitePopulations
                                  orderby population.Race
                                  group population by population.Race into type
                                  select type;

            cmbSiteType.Items.Add("All");
            cmbSiteType.SelectedIndex = 0;

            foreach (var site in sites)
                cmbSiteType.Items.Add(site.Key);

            cmbSitePopulation.Items.Add("All"); cmbSitePopulation.SelectedIndex = 0;
            foreach (var populationType in populationTypes)
                cmbSitePopulation.Items.Add(populationType.Key);

            var siteEvents = from eventType in World.Sites.SelectMany(site => site.Events)
                             group eventType by eventType.Type into type
                             select type.Key;

            TabEvents.Clear();
            TabEvents.Add(siteEvents.ToList());
        }

        internal override void ResetTab()
        {
            txtSiteSearch.Clear();
            listSiteSearch.Items.Clear();
            cmbSiteType.Items.Clear();
            cmbSitePopulation.Items.Clear();
            radSiteNone.Checked = true;
        }

        private void searchSiteList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                if (sender == cmbSitePopulation && !radSiteSortPopulation.Checked) radSiteSortPopulation.Checked = true;
                else
                {
                    siteSearch.name = txtSiteSearch.Text;
                    siteSearch.type = cmbSiteType.SelectedItem.ToString();
                    siteSearch.PopulationType = cmbSitePopulation.SelectedItem.ToString();
                    siteSearch.sortOwners = radSiteOwners.Checked;
                    siteSearch.sortEvents = radSiteSortEvents.Checked;
                    siteSearch.sortFiltered = radSiteSortFiltered.Checked;
                    siteSearch.sortWarfare = radSiteSortWarfare.Checked;
                    siteSearch.SortPopulation = radSiteSortPopulation.Checked;
                    siteSearch.SortConnections = radSortConnections.Checked;
                    siteSearch.SortDeaths = radSiteSortDeaths.Checked;
                    siteSearch.SortBeastAttacks = radSiteBeastAttacks.Checked;
                    IEnumerable<Site> list = siteSearch.getList();
                    var results = list.ToArray();
                    listSiteSearch.SetObjects(results);
                    UpdateCounts(results.Length, siteSearch.BaseList.Count);
                }
            }
        }

        private void UpdateCounts(int shown, int total)
        {
            lblShownResults.Text = $"{shown} / {total}";
        }

        public void ResetSiteBaseList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                txtSiteSearch.Clear();
                cmbSiteType.SelectedIndex = 0;
                cmbSitePopulation.SelectedIndex = 0;
                radSiteNone.Checked = true;
            }
        }

        private void lnkMaxResults_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // InputBox with value validation - first define validation delegate, which
            // returns empty string for valid values and error message for invalid values
            InputBoxValidation validation = delegate (string val)
            {
                if (val == "") return "Value cannot be empty.";
                if (!(new Regex(@"^[0-9]+$")).IsMatch(val)) return "Value is not valid.";
                return "";
            };

            string value = WorldObjectList.MaxResults.ToString();
            if (InputBox.Show("Max Results:", "Enter maximum search results. (0 for All)", ref value, validation) == DialogResult.OK)
            {
                WorldObjectList.MaxResults = int.Parse(value);
                foreach (LinkLabel lnkLabel in MaxResultsLabels)
                {
                    lnkLabel.Text = WorldObjectList.MaxResults.ToString();
                    lnkLabel.Left = lnkLabel.Parent.Right - lnkLabel.Width - 3;
                }
                lblShownResults.Left = lnkMaxResults.Left - lblShownResults.Width - 3;
                listSearch_SelectedIndexChanged(this, EventArgs.Empty);
                searchSiteList(null, null);
            }
        }

        private void listSiteSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSearch_SelectedIndexChanged(sender, e);
        }

        private void filterPanel_OnPanelExpand(object sender, EventArgs e)
        {
            var panel = sender as RichPanel;
            if (panel != null)
            {
                foreach (var control in panel.Controls.OfType<Control>())
                    control.Visible = panel.Expanded;
            }
        }
    }
}
