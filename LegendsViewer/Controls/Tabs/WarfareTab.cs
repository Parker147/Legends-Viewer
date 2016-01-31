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
using LegendsViewer.Legends.EventCollections;

namespace LegendsViewer.Controls.Tabs
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof (IDesigner))]
    public partial class WarfareTab : BaseSearchTab
    {
        private WarsList warSearch;
        private BattlesList battleSearch;
        private ConqueringsList conqueringsSearch;
        private BeastAttackList beastAttackSearch;

        public WarfareTab()
        {
            InitializeComponent();
        }


        internal override void InitializeTab()
        {
            hint.SetToolTip(chkFilterWarfare, "Unnotable Battle = Attackers outnumber defenders 10 to 1 and win and suffer < 10% losses. \nUnnotable Conquering = All Pillagings.");

            EventTabs = new TabPage[] {tpWarEvents, tpBattlesEvents, tpConqueringsEvents, tpBeastAttackEvents};
            EventTabTypes = new Type[]{typeof(War), typeof(Battle), typeof(SiteConquered), typeof(BeastAttack)};

            listWarSearch.ShowGroups = false;

            listBattleSearch.ShowGroups = false;

            listConqueringSearch.ShowGroups = false;

            listBeastAttackSearch.ShowGroups = false;
        }

        internal override void ResetEvents() { }

        internal override void AfterLoad(World world)
        {
            base.AfterLoad(world);
            warSearch = new WarsList(World);
            battleSearch = new BattlesList(World);
            conqueringsSearch = new ConqueringsList(World);
            beastAttackSearch = new BeastAttackList(World);

            var conquerTypes = from conquer in World.EventCollections.OfType<SiteConquered>()
                               orderby conquer.ConquerType
                               group conquer by conquer.ConquerType into conquers
                               select conquers;

            cmbConqueringType.Items.Add("All"); cmbConqueringType.SelectedIndex = 0;
            foreach (var conquerType in conquerTypes)
                cmbConqueringType.Items.Add(conquerType.Key);


            var warEvents = from eventType in World.EventCollections.OfType<War>().SelectMany(war => war.GetSubEvents())
                            group eventType by eventType.Type into type
                            select type.Key;
            var battleEvents = from eventType in World.EventCollections.OfType<Battle>().SelectMany(battle => battle.GetSubEvents())
                               group eventType by eventType.Type into type
                               select type.Key;
            var conqueringEvents = from eventType in World.EventCollections.OfType<SiteConquered>().SelectMany(conquering => conquering.GetSubEvents())
                                   group eventType by eventType.Type into type
                                   select type.Key;
            var beastAttackEvents = from eventType in World.EventCollections.OfType<BeastAttack>().SelectMany(beastAttack => beastAttack.GetSubEvents())
                                    group eventType by eventType.Type into type
                                    select type.Key;

            TabEvents.Clear();
            TabEvents.Add(warEvents.ToList());
            TabEvents.Add(battleEvents.ToList());
            TabEvents.Add(conqueringEvents.ToList());
            TabEvents.Add(beastAttackEvents.ToList());
        }

        internal override void ResetTab()
        {

            lblBattleList.Text = lblWarList.Text = "All";
            lblWarList.ForeColor = DefaultForeColor;
            lblWarList.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Regular);

            txtWarSearch.Clear();
            listWarSearch.Items.Clear();
            chkWarOngoing.Checked = false;
            radWarSortNone.Checked = true;
            chkFilterWarfare.Checked = true;

            txtBattleSearch.Clear();
            listBattleSearch.Items.Clear();
            radBattleSortNone.Checked = true;

            txtConqueringSearch.Clear();
            listConqueringSearch.Items.Clear();
            cmbConqueringType.Items.Clear();
            radConqueringSortNone.Checked = true;
        }

        private void chkFilterWarfare_CheckedChanged(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
                World.FilterBattles = chkFilterWarfare.Checked;
        }

        private void searchWarList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                warSearch.Name = txtWarSearch.Text;
                warSearch.SortEvents = radWarSortEvents.Checked;
                warSearch.SortFiltered = radWarSortFiltered.Checked;
                warSearch.SortLength = radWarLength.Checked;
                warSearch.SortDeaths = radWarDeaths.Checked;
                warSearch.Ongoing = chkWarOngoing.Checked;
                warSearch.SortWarfare = radWarSortWarfare.Checked;
                warSearch.SortConquerings = radWarsSortConquerings.Checked;
                IEnumerable<War> list = warSearch.GetList();
                var results = list.ToArray();
                listWarSearch.SetObjects(results);
                UpdateCounts(lblWarResults, results.Length, warSearch.BaseList.Count);
            }
        }

        private void UpdateCounts(Label label, int shown, int total)
        {
            label.Text = $"{shown} / {total}";
        }

        public void ChangeWarBaseList(List<War> list, string listName)
        {
            FileLoader.Working = true;
            lblWarList.Text = listName;
            lblWarList.ForeColor = Color.Blue;
            lblWarList.Font = new Font(lblWarList.Font.FontFamily, lblWarList.Font.Size, FontStyle.Bold);
            warSearch.BaseList = list;
            txtWarSearch.Clear();
            chkWarOngoing.Checked = false;
            radWarSortNone.Checked = true;
            //tcWorld.SelectedTab = tpWarfare;
            tcWarfare.SelectedTab = tpWars;
            tcWars.SelectedTab = tpWarSearch;
            searchWarList(null, null);
            FileLoader.Working = false;
        }

        public void ResetWarBaseList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                lblWarList.Text = "All";
                lblWarList.ForeColor = Control.DefaultForeColor;
                lblWarList.Font = new Font(lblWarList.Font.FontFamily, lblWarList.Font.Size, FontStyle.Regular);
                warSearch.BaseList = World.EventCollections.OfType<War>().ToList();
                searchWarList(null, null);
            }
        }

        private void searchBattleList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                battleSearch.Name = txtBattleSearch.Text;
                battleSearch.SortEvents = radBattleSortEvents.Checked;
                battleSearch.SortFiltered = radBattleSortFiltered.Checked;
                battleSearch.SortDeaths = radBattleSortDeaths.Checked;
                IEnumerable<Battle> list = battleSearch.GetList();
                var results = list.ToArray();
                listBattleSearch.SetObjects(results);
                UpdateCounts(lblBattleResults, results.Length, battleSearch.BaseList.Count);
            }
        }

        public void ChangeBattleBaseList(List<Battle> list, string listName)
        {
            FileLoader.Working = true;
            lblBattleList.Text = listName;
            lblBattleList.ForeColor = Color.Blue;
            lblBattleList.Font = new Font(lblBattleList.Font.FontFamily, lblBattleList.Font.Size, FontStyle.Bold);
            battleSearch.BaseList = list;
            txtBattleSearch.Clear();

            radBattleSortNone.Checked = true;
            //tcWorld.SelectedTab = tpWarfare;
            tcWarfare.SelectedTab = tpBattles;
            tcBattles.SelectedTab = tpBattlesSearch;
            searchBattleList(null, null);
            FileLoader.Working = false;
        }

        public void ResetBattleBaseList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                lblBattleList.Text = "All";
                lblBattleList.ForeColor = Control.DefaultForeColor;
                lblBattleList.Font = new Font(lblBattleList.Font.FontFamily, lblBattleList.Font.Size, FontStyle.Regular);
                battleSearch.BaseList = World.EventCollections.OfType<Battle>().ToList();
                searchBattleList(null, null);
            }
        }

        private void searchConqueringList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                conqueringsSearch.Name = txtConqueringSearch.Text;
                conqueringsSearch.SortEvents = radConqueringSortEvents.Checked;
                conqueringsSearch.SortFiltered = radConqueringSortFiltered.Checked;
                conqueringsSearch.SortSite = radConqueringSortSite.Checked;
                conqueringsSearch.Type = cmbConqueringType.SelectedItem.ToString();
                IEnumerable<SiteConquered> list = conqueringsSearch.GetList();
                var results = list.ToArray();
                listConqueringSearch.SetObjects(results);
                UpdateCounts(lblConqueringResult, results.Length, conqueringsSearch.BaseList.Count);
            }
        }

        private void searchbeastAttackList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && World != null)
            {
                beastAttackSearch.Name = txtBeastAttacksSearch.Text;
                beastAttackSearch.SortEvents = radBeastAttacksEvents.Checked;
                beastAttackSearch.SortFiltered = radBeastAttacksFiltered.Checked;
                beastAttackSearch.SortDeaths = radBeastAttacksDeaths.Checked;
                IEnumerable<BeastAttack> list = beastAttackSearch.GetList();
                var results = list.ToArray();
                listBeastAttackSearch.SetObjects(results);
                UpdateCounts(lblBeastAttackResults, results.Length, beastAttackSearch.BaseList.Count);
            }
        }

        private void listWarSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSearch_SelectedIndexChanged(sender, e);
        }

        private void listBattleSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSearch_SelectedIndexChanged(sender, e);
        }

        private void listConqueringSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSearch_SelectedIndexChanged(sender, e);
        }

        private void listBeastAttacks_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSearch_SelectedIndexChanged(sender, e);
        }
    }
}
