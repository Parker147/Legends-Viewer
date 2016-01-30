using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BrightIdeasSoftware;
using LegendsViewer.Legends;
using WFC;

namespace LegendsViewer.Controls.Tabs
{
    public partial class HistoricalFiguresTab : BaseSearchTab
    {
        private HistoricalFigureList hfSearch;

        public HistoricalFiguresTab()
        {
            InitializeComponent();
        }


        internal override void InitializeTab()
        {
            EventTabs = new TabPage[] { tpHFEvents };
            EventTabTypes = new Type[] { typeof(HistoricalFigure) };
            lnkMaxResults.Text = HistoricalFigureList.MaxResults.ToString();
            listHFSearch.ShowGroups = false;

            listHFSearch.AllColumns.Add(new OLVColumn { AspectName = "Caste", IsVisible = false, Text = "Caste", TextAlign = HorizontalAlignment.Left });
            listHFSearch.AllColumns.Add(new OLVColumn { AspectName = "PreviousRace", IsVisible = false, Text = "Previous Race", TextAlign = HorizontalAlignment.Left });
            listHFSearch.AllColumns.Add(new OLVColumn { AspectName = "Alive", IsVisible = false, Text = "Alive", TextAlign = HorizontalAlignment.Center, CheckBoxes = true });
            listHFSearch.AllColumns.Add(new OLVColumn { AspectName = "Skeleton", IsVisible = false, Text = "Skeleton", TextAlign = HorizontalAlignment.Center, CheckBoxes = true });
            listHFSearch.AllColumns.Add(new OLVColumn { AspectName = "Force", IsVisible = false, Text = "Force", TextAlign = HorizontalAlignment.Center, CheckBoxes = true });
            listHFSearch.AllColumns.Add(new OLVColumn { AspectName = "Zombie", IsVisible = false, Text = "Zombie", TextAlign = HorizontalAlignment.Center, CheckBoxes = true });
            listHFSearch.AllColumns.Add(new OLVColumn { AspectName = "Ghost", IsVisible = false, Text = "Ghost", TextAlign = HorizontalAlignment.Center, CheckBoxes = true });
            listHFSearch.AllColumns.Add(new OLVColumn { AspectName = "Animated", IsVisible = false, Text = "Animated", TextAlign = HorizontalAlignment.Center, CheckBoxes = true });
            listHFSearch.AllColumns.Add(new OLVColumn { AspectName = "Adventurer", IsVisible = false, Text = "Adventurer", TextAlign = HorizontalAlignment.Center, CheckBoxes = true });
            listHFSearch.AllColumns.Add(new OLVColumn
            {
                Text = "Kills", TextAlign = HorizontalAlignment.Right, IsVisible = false,
                AspectGetter = rowObject => ((HistoricalFigure)rowObject).NotableKills.Count
            });
            listHFSearch.AllColumns.Add(new OLVColumn
            {
                Text = "Events", TextAlign = HorizontalAlignment.Right, IsVisible = false,
                AspectGetter = rowObject => ((HistoricalFigure)rowObject).Events.Count
            });

        }

        internal override void AfterLoad(World world)
        {
            base.AfterLoad(world);
            hfSearch = new HistoricalFigureList(World);

            var races = from hf in World.HistoricalFigures
                        orderby hf.Race
                        group hf by hf.Race into race
                        select race;
            var castes = from hf in World.HistoricalFigures
                         orderby hf.Caste
                         group hf by hf.Caste into caste
                         select caste;
            var types = from hf in World.HistoricalFigures
                        orderby hf.AssociatedType
                        group hf by hf.AssociatedType into type
                        select type;

            cmbRace.Items.Add("All"); cmbRace.SelectedIndex = 0;
            foreach (var race in races)
                cmbRace.Items.Add(race.Key);
            cmbCaste.Items.Add("All"); cmbCaste.SelectedIndex = 0;
            foreach (var caste in castes)
                cmbCaste.Items.Add(caste.Key);
            cmbType.Items.Add("All"); cmbType.SelectedIndex = 0;
            foreach (var type in types)
                cmbType.Items.Add(type.Key);

            TabEvents.Clear();

            var historicalFigureEvents = from eventType in World.HistoricalFigures.SelectMany(hf => hf.Events)
                                         group eventType by eventType.Type into type
                                         select type.Key;
            TabEvents.Add(historicalFigureEvents.ToList());
        }

        internal override void ResetTab()
        {
            txtHFSearch.Clear();
            listHFSearch.SetObjects(new object[0]);
            chkAlive.Checked = chkDeity.Checked = chkGhost.Checked = chkVampire.Checked = chkWerebeast.Checked = chkForce.Checked = chkHFLeader.Checked = false;
            cmbRace.Items.Clear();
            cmbCaste.Items.Clear();
            cmbType.Items.Clear();
            radHFNone.Checked = true;

            lblHFList.Text = "All";
            lblHFList.ForeColor = lblHFList.ForeColor = DefaultForeColor;
            lblHFList.Font = lblHFList.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Regular);

        }

        private void searchHFList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                if (txtHFSearch.Text.Length > 1)
                    hfSearch.name = txtHFSearch.Text;
                else
                    hfSearch.name = "";
                hfSearch.race = cmbRace.SelectedItem.ToString();
                hfSearch.caste = cmbCaste.SelectedItem.ToString();
                hfSearch.type = cmbType.SelectedItem.ToString();
                hfSearch.deity = chkDeity.Checked;
                hfSearch.force = chkForce.Checked;
                hfSearch.vampire = chkVampire.Checked;
                hfSearch.werebeast = chkWerebeast.Checked;
                hfSearch.ghost = chkGhost.Checked;
                hfSearch.alive = chkAlive.Checked;
                hfSearch.Leader = chkHFLeader.Checked;
                hfSearch.sortKills = radSortKills.Checked;
                hfSearch.sortEvents = radHFSortEvents.Checked;
                hfSearch.sortFiltered = radHFSortFiltered.Checked;
                hfSearch.sortBattles = radHFSortBattles.Checked;
                hfSearch.sortMiscKills = radSortMiscKills.Checked;

                IEnumerable<HistoricalFigure> list = hfSearch.GetList();
                var results = list.ToArray();
                listHFSearch.SetObjects(results);
                //listHFSearch.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                UpdateCounts(results.Length, hfSearch.BaseList.Count);
            }
        }

        public void ChangeHFBaseList(List<HistoricalFigure> list, string listName)
        {
            FileLoader.Working = true;
            lblHFList.Text = listName;
            lblHFList.ForeColor = Color.Blue;
            lblHFList.Font = new Font(lblHFList.Font.FontFamily, lblHFList.Font.Size, FontStyle.Bold);
            hfSearch.BaseList = list;
            txtHFSearch.Clear();
            chkAlive.Checked = chkDeity.Checked = chkForce.Checked = chkGhost.Checked = chkVampire.Checked = chkWerebeast.Checked = chkHFLeader.Checked = false;
            cmbRace.SelectedIndex = 0;
            cmbCaste.SelectedIndex = 0;
            cmbType.SelectedIndex = 0;
            radHFNone.Checked = true;
            //tcWorld.SelectedTab = tpHF;
            tcHF.SelectedTab = tpHFSearch;
            searchHFList(null, null);
            FileLoader.Working = false;
        }

        public void ResetHFBaseList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                lblHFList.Text = "All";
                lblHFList.ForeColor = Control.DefaultForeColor;
                lblHFList.Font = new Font(lblHFList.Font.FontFamily, lblHFList.Font.Size, FontStyle.Regular);
                hfSearch.BaseList = World.HistoricalFigures;
                searchHFList(null, null);
            }
        }

        private void listHFSearch_SelectedIndexChanged(object sender, EventArgs e)
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

            string value = HistoricalFigureList.MaxResults.ToString();
            if (InputBox.Show("Max Results:", "Enter maximum search results. (0 for All)", ref value, validation) == DialogResult.OK)
            {
                HistoricalFigureList.MaxResults = int.Parse(value);
                lnkMaxResults.Text = HistoricalFigureList.MaxResults.ToString();
                lnkMaxResults.Left = listPanel.Right - lnkMaxResults.Width - 3;
                lblShownResults.Left = lnkMaxResults.Left - lblShownResults.Width - 3;
                listSearch_SelectedIndexChanged(this, EventArgs.Empty);
            }
        }

        private void UpdateCounts(int shown, int total)
        {
            lblShownResults.Text = $"{shown} / {total}";
        }
    }
}
