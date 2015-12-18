using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LegendsViewer.Controls.Query;
using LegendsViewer.Legends;
using LegendsViewer.Controls;
using SevenZip;


namespace LegendsViewer
{
   
    public partial class frmLegendsViewer : Form
    {
        private World world;
        private FileLoader FileLoader;
        private HistoricalFigureList hfSearch;
        private SitesList siteSearch;
        private RegionsList regionSearch;
        private UndergroundRegionsList uRegionSearch;
        private EntitiesList entitySearch;
        private WarsList warSearch;
        private BattlesList battleSearch;
        private ArtifactList artifactSearch;
        private ConqueringsList conqueringsSearch;
        private BeastAttackList beastAttackSearch;

        string version = "1.14";
        private TabPage[] EventTabs;
        Type[] EventTabTypes = new Type[]{typeof(HistoricalFigure), typeof(Site), typeof(Region),
                                            typeof(UndergroundRegion), typeof(Entity), typeof(War),
                                            typeof(Battle), typeof(SiteConquered), typeof(Era), typeof(BeastAttack),
                                            typeof(Artifact)};                         
        private List<List<String>> TabEvents;
        DwarfTabControl Browser;
        private bool DontRefreshBrowserPages = true;
        private string CommandFile;

        public frmLegendsViewer(string file = "")
        {
            InitializeComponent();
            FileLoader = new LegendsViewer.FileLoader(this, btnXML, txtXML, btnHistory, txtHistory, btnSitePops, txtSitePops, btnMap, txtMap, lblStatus, txtLog);
            EventTabs = new TabPage[] { tpHFEvents, tpSiteEvents, tpRegionEvents, tpURegionEvents, tpCivEvents, tpWarEvents, tpBattlesEvents, tpConqueringsEvents, tpEraEvents, tpBeastAttackEvents, tpArtifactsEvents };
            tcWorld.Height = ClientSize.Height;
            btnBack.Location = new Point(tcWorld.Right + 3, 3);
            btnForward.Location = new Point(btnBack.Right + 3, 3);
            Browser = new DwarfTabControl(world);
            Browser.Location = new Point(tcWorld.Right, btnBack.Bottom + 3);
            Browser.Size = new Size(ClientSize.Width - Browser.Left , ClientSize.Height - Browser.Top);
            Browser.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right);
            Controls.Add(Browser);
            foreach (TabPage tp in tcWorld.TabPages)
                foreach (TabControl tabControl in tp.Controls.OfType<TabControl>())
                    HideTabControlBorder(tabControl);
            if (file != "")
                CommandFile = file;

            hint.SetToolTip(chkFilterWarfare, "Unnotable Battle = Attackers outnumber defenders 10 to 1 and win and suffer < 10% losses. \nUnnotable Conquering = All Pillagings.");
        }

        private void frmLegendsViewer_Shown(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(CommandFile))
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
                foreach (TabControl tabControl in tp.Controls.OfType<TabControl>())
                    HideTabControlBorder(tabControl);
        }

        private void GenerateEventFilterCheckBoxes()
        {
            Array.Sort(AppHelpers.EventInfo, delegate(string[] a, string[] b)
            {
                return String.Compare(a[1], b[1]);
            });
            for (int eventTab = 0; eventTab < EventTabs.Count(); eventTab++)
            {
                int count = 0;
                TabEvents[eventTab].Sort((a, b) => AppHelpers.EventInfo[Array.IndexOf(AppHelpers.EventInfo, AppHelpers.EventInfo.Single(eventInfo => eventInfo[0] == a))][1].CompareTo(AppHelpers.EventInfo[Array.IndexOf(AppHelpers.EventInfo, AppHelpers.EventInfo.Single(eventInfo => eventInfo[0] == b))][1]));
                foreach (string eventType in TabEvents[eventTab])
                {

                    CheckBox eventCheck = new CheckBox();
                    EventTabs[eventTab].Controls.Add(eventCheck);
                    string[] eventInfo = AppHelpers.EventInfo.Where(a => a[0] == eventType).Single();
                    eventCheck.Text = eventInfo[1];
                    eventCheck.Checked = true;
                    eventCheck.CheckedChanged += OnEventFilterCheck;
                    hint.SetToolTip(eventCheck, eventInfo[2]);
                    eventCheck.Location = new Point(10, 23 * count);
                    eventCheck.Width = 235;
                    count++;
                }
                Button btnAll = new Button();
                btnAll.Text = "Select All";
                btnAll.Location = new Point(10, 23 * count);
                btnAll.Click += SelectAllEventCheckBoxes;
                EventTabs[eventTab].Controls.Add(btnAll);
                Button btnNone = new Button();
                btnNone.Text = "Deselect All";
                btnNone.Location = new Point(90, 23 * count);
                btnNone.Click += SelectAllEventCheckBoxes;
                EventTabs[eventTab].Controls.Add(btnNone);
            }
        }

        private void RemoveEventFilterCheckBoxes()
        {
            foreach (TabPage eventTab in EventTabs)
                eventTab.Controls.Clear();
        }

        
        private void OnEventFilterCheck(object sender, EventArgs e)
        {
            CheckBox eventCheck = (sender as CheckBox);
            if (!FileLoader.Working && world != null)
            {
                string[] eventInfo = AppHelpers.EventInfo.Where(a => a[1] == eventCheck.Text).Single();
                int eventPageIndex = Array.IndexOf(EventTabs, eventCheck.Parent);
                List<string> eventFilter = EventTabTypes[eventPageIndex].GetField("Filters").GetValue(null) as List<string>;
                if (eventCheck.Checked)
                    eventFilter.Remove(eventInfo[0]);
                else
                    eventFilter.Add(eventInfo[0]);
                if(!DontRefreshBrowserPages)
                    Browser.RefreshAll(EventTabTypes[eventPageIndex]);
            }
            else
            {
                eventCheck.CheckedChanged -= OnEventFilterCheck;
                eventCheck.Checked = true;
                eventCheck.CheckedChanged += OnEventFilterCheck;
            }
                
        }

        private void SelectAllEventCheckBoxes(object sender, EventArgs e)
        {
            Button selectButton = (sender as Button);
            DontRefreshBrowserPages = true;
            foreach(CheckBox checkEvent in selectButton.Parent.Controls.OfType<CheckBox>())
            {
                if (selectButton.Text == "Select All")
                    checkEvent.Checked = true;
                if (selectButton.Text == "Deselect All")
                    checkEvent.Checked = false;
            }
            Browser.RefreshAll(EventTabTypes[Array.IndexOf(EventTabs, selectButton.Parent)]);
            DontRefreshBrowserPages = false;
                    
        }

        private void listSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Browser.LoadPageControl(((sender as ListBox).SelectedItem as DwarfObject));
            Browser.Navigate(ControlOption.HTML, (sender as ListBox).SelectedItem);
        }

        private void searchHFList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && world != null)
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
                IEnumerable<HistoricalFigure> list = hfSearch.GetList();
                listHFSearch.Items.Clear();
                listHFSearch.Items.AddRange(list.ToArray());
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
            tcWorld.SelectedTab = tpHF;
            tcHF.SelectedTab = tpHFSearch;
            searchHFList(null, null);
            FileLoader.Working = false;
        }

        public void ResetHFBaseList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && world != null)
            {
                lblHFList.Text = "All";
                lblHFList.ForeColor = Control.DefaultForeColor;
                lblHFList.Font = new Font(lblHFList.Font.FontFamily, lblHFList.Font.Size, FontStyle.Regular);
                hfSearch.BaseList = world.HistoricalFigures;
                searchHFList(null, null);
            }
        }
		
		private void searchSiteList(object sender, EventArgs e)
		{
            if (!FileLoader.Working && world != null)
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
                    listSiteSearch.Items.Clear();
                    listSiteSearch.Items.AddRange(list.ToArray());
                }
            }
		}

        public void ChangeSiteBaseList(List<Site> list, string listName)
        {
            FileLoader.Working = true;
            lblSiteList.Text = listName;
            lblSiteList.ForeColor = Color.Blue;
            lblSiteList.Font = new Font(lblSiteList.Font.FontFamily, lblSiteList.Font.Size, FontStyle.Bold);
            siteSearch.BaseList = list;
            txtSiteSearch.Clear();
            cmbSiteType.SelectedIndex = 0;
            cmbSitePopulation.SelectedIndex = 0;
            radSiteNone.Checked = true;
            tcWorld.SelectedTab = tpSites;
            tcSites.SelectedTab = tpSiteSearch;
            searchSiteList(null, null);
            FileLoader.Working = false;
        }

        public void ResetSiteBaseList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && world != null)
            {
                lblSiteList.Text = "All";
                lblSiteList.ForeColor = Control.DefaultForeColor;
                lblSiteList.Font = new Font(lblSiteList.Font.FontFamily, lblSiteList.Font.Size, FontStyle.Regular);
                siteSearch.BaseList = world.Sites;
                searchSiteList(null, null);
            }
        }

		private void searchRegionList(object sender, EventArgs e)
		{
            if (!FileLoader.Working && world != null)
            {
                regionSearch.name = txtRegionSearch.Text;
                regionSearch.type = cmbRegionType.SelectedItem.ToString();
                regionSearch.sortEvents = radRegionSortEvents.Checked;
                regionSearch.sortFiltered = radRegionSortFiltered.Checked;
                regionSearch.sortBattles = radRegionSortBattles.Checked;
                regionSearch.SortDeaths = radRegionSortDeaths.Checked;
                IEnumerable<WorldRegion> list = regionSearch.getList();
                listRegionSearch.Items.Clear();
                listRegionSearch.Items.AddRange(list.ToArray());
            }
		}

		private void searchURegionList(object sender, EventArgs e)
		{
            if (!FileLoader.Working && world != null)
            {
                uRegionSearch.type = cmbURegionType.SelectedItem.ToString();
                uRegionSearch.sortEvents = radURegionSortEvents.Checked;
                uRegionSearch.sortFiltered = radURegionSortFiltered.Checked;
                IEnumerable<UndergroundRegion> list = uRegionSearch.getList();
                listURegionSearch.Items.Clear();
                listURegionSearch.Items.AddRange(list.ToArray());
            }
		}

		private void searchEntityList(object sender, EventArgs e)
		{
            if (!FileLoader.Working && world != null)
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

        private void searchWarList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && world != null)
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
                listWarSearch.Items.Clear();
                listWarSearch.Items.AddRange(list.ToArray());
            }
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
            tcWorld.SelectedTab = tpWarfare;
            tcWarfare.SelectedTab = tpWars;
            tcWars.SelectedTab = tpWarSearch;
            searchHFList(null, null);
            FileLoader.Working = false;
        }

        public void ResetWarBaseList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && world != null)
            {
                lblWarList.Text = "All";
                lblWarList.ForeColor = Control.DefaultForeColor;
                lblWarList.Font = new Font(lblWarList.Font.FontFamily, lblWarList.Font.Size, FontStyle.Regular);
                warSearch.BaseList = world.EventCollections.OfType<War>().ToList();
                searchWarList(null, null);
            }
        }

        private void searchBattleList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && world != null)
            {
                battleSearch.Name = txtBattleSearch.Text;
                battleSearch.SortEvents = radBattleSortEvents.Checked;
                battleSearch.SortFiltered = radBattleSortFiltered.Checked;
                battleSearch.SortDeaths = radBattleSortDeaths.Checked;
                IEnumerable<Battle> list = battleSearch.GetList();
                listBattleSearch.Items.Clear();
                listBattleSearch.Items.AddRange(list.ToArray());
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
            tcWorld.SelectedTab = tpWarfare;
            tcWarfare.SelectedTab = tpBattles;
            tcBattles.SelectedTab = tpBattlesSearch;
            searchBattleList(null, null);
            FileLoader.Working = false;
        }

        public void ResetBattleBaseList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && world != null)
            {
                lblBattleList.Text = "All";
                lblBattleList.ForeColor = Control.DefaultForeColor;
                lblBattleList.Font = new Font(lblBattleList.Font.FontFamily, lblBattleList.Font.Size, FontStyle.Regular);
                battleSearch.BaseList = world.EventCollections.OfType<Battle>().ToList();
                searchBattleList(null, null);
            }
        }

        private void searchConqueringList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && world != null)
            {
                conqueringsSearch.Name = txtConqueringSearch.Text;
                conqueringsSearch.SortEvents = radConqueringSortEvents.Checked;
                conqueringsSearch.SortFiltered = radConqueringSortFiltered.Checked;
                conqueringsSearch.SortSite = radConqueringSortSite.Checked;
                conqueringsSearch.Type = cmbConqueringType.SelectedItem.ToString();
                IEnumerable<SiteConquered> list = conqueringsSearch.GetList();
                listConqueringSearch.Items.Clear();
                listConqueringSearch.Items.AddRange(list.ToArray());
            }
        }

        private void searchbeastAttackList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && world != null)
            {
                beastAttackSearch.Name = txtBeastAttacksSearch.Text;
                beastAttackSearch.SortEvents = radBeastAttacksEvents.Checked;
                beastAttackSearch.SortFiltered = radBeastAttacksFiltered.Checked;
                beastAttackSearch.SortDeaths = radBeastAttacksDeaths.Checked;
                IEnumerable<BeastAttack> list = beastAttackSearch.GetList();
                listBeastAttacks.Items.Clear();
                listBeastAttacks.Items.AddRange(list.ToArray());
            }
        }

        private void btnEraShow_Click(object sender, EventArgs e)
        {
            if (!FileLoader.Working && world != null)
            {
                Browser.Navigate(ControlOption.HTML, new Era(Convert.ToInt32(numStart.Value), Convert.ToInt32(numEraEnd.Value), world));
            }
        }


        public void AfterLoad(World loadedWorld)
        {
            if (!FileLoader.Working && world != null)
            {
                world.Map.Dispose();
                world.MiniMap.Dispose();
                world.PageMiniMap.Dispose();
                foreach (Entity entity in world.Entities)
                    if (entity.Identicon != null) entity.Identicon.Dispose();
                world = null;
            }

            world = loadedWorld;

            lblStatus.Text = "Setup...";
            lblStatus.ForeColor = Color.Blue;
            ResetForm();
            Application.DoEvents();

            Browser.World = world;
            this.Text += " " + world.Name;
            Browser.Navigate(ControlOption.HTML, world);

            txtLog.AppendText(world.Log.ToString());
            hfSearch = new HistoricalFigureList(world);
            siteSearch = new SitesList(world);
            regionSearch = new RegionsList(world);
            uRegionSearch = new UndergroundRegionsList(world);
            entitySearch = new EntitiesList(world);
            warSearch = new WarsList(world);
            battleSearch = new BattlesList(world);
            conqueringsSearch = new ConqueringsList(world);
            beastAttackSearch = new BeastAttackList(world);
            artifactSearch = new ArtifactList(world);

            dlgOpen.FileName = "";

            var races = from hf in world.HistoricalFigures
                        orderby hf.Race
                        group hf by hf.Race into race
                        select race;
            var castes = from hf in world.HistoricalFigures
                         orderby hf.Caste
                         group hf by hf.Caste into caste
                         select caste;
            var types = from hf in world.HistoricalFigures
                        orderby hf.AssociatedType
                        group hf by hf.AssociatedType into type
                        select type;
            var sites = from site in world.Sites
                        orderby site.Type
                        group site by site.Type into sitetype
                        select sitetype;
            var regions = from region in world.Regions
                          orderby region.Type
                          group region by region.Type into regiontype
                          select regiontype;
            var uregions = from uregion in world.UndergroundRegions
                           orderby uregion.Type
                           group uregion by uregion.Type into uregiontype
                           select uregiontype;
            var civRaces = from civ in world.Entities
                           orderby civ.Race
                           group civ by civ.Race into civRace
                           select civRace;
            var conquerTypes = from conquer in world.EventCollections.OfType<SiteConquered>()
                               orderby conquer.ConquerType
                               group conquer by conquer.ConquerType into conquers
                               select conquers;
            var populationTypes = from population in world.SitePopulations
                                  orderby population.Race
                                  group population by population.Race into type
                                  select type;
            var civPopulationTypes = from civPopulation in populationTypes
                                     where world.Entities.Count(entity => entity.Populations.Count(population => population.Race == civPopulation.Key) > 0) > 0
                                     select civPopulation;
            var historicalFigureEvents = from eventType in world.HistoricalFigures.SelectMany(hf => hf.Events)
                                         group eventType by eventType.Type into type
                                         select type.Key;
            var siteEvents = from eventType in world.Sites.SelectMany(site => site.Events)
                                         group eventType by eventType.Type into type
                                         select type.Key;
            var regionEvents = from eventType in world.Regions.SelectMany(region => region.Events)
                                         group eventType by eventType.Type into type
                                         select type.Key;
            var undergroundRegionEvents = from eventType in world.UndergroundRegions.SelectMany(uRegion => uRegion.Events)
                                         group eventType by eventType.Type into type
                                         select type.Key;
            var entityEvents = from eventType in world.Entities.SelectMany(hf => hf.Events)
                                         group eventType by eventType.Type into type
                                         select type.Key;
            var warEvents = from eventType in world.EventCollections.OfType<War>().SelectMany(war => war.GetSubEvents())
                                         group eventType by eventType.Type into type
                                         select type.Key;
            var battleEvents = from eventType in world.EventCollections.OfType<Battle>().SelectMany(battle => battle.GetSubEvents())
                                         group eventType by eventType.Type into type
                                         select type.Key;
            var conqueringEvents = from eventType in world.EventCollections.OfType<SiteConquered>().SelectMany(conquering => conquering.GetSubEvents())
                                         group eventType by eventType.Type into type
                                         select type.Key;
            var beastAttackEvents = from eventType in world.EventCollections.OfType<BeastAttack>().SelectMany(beastAttack => beastAttack.GetSubEvents())
                                   group eventType by eventType.Type into type
                                   select type.Key;

            var artifactEvents = from eventType in world.Artifacts.SelectMany(artifact => artifact.Events)
                                 group eventType by eventType.Type into type
                                 select type.Key;

            var eventTypes = from eventType in world.Events
                             group eventType by eventType.Type into type
                             select type.Key;

            TabEvents = new List<List<string>>();
            foreach (Type eventTabType in EventTabTypes)
            {
                if (eventTabType == typeof(HistoricalFigure))
                    TabEvents.Add(historicalFigureEvents.ToList());
                else if (eventTabType == typeof(Site))
                    TabEvents.Add(siteEvents.ToList());
                else if (eventTabType == typeof(Region))
                    TabEvents.Add(regionEvents.ToList());
                else if (eventTabType == typeof(UndergroundRegion))
                    TabEvents.Add(undergroundRegionEvents.ToList());
                else if (eventTabType == typeof(Entity))
                    TabEvents.Add(entityEvents.ToList());
                else if (eventTabType == typeof(War))
                    TabEvents.Add(warEvents.ToList());
                else if (eventTabType == typeof(Battle))
                    TabEvents.Add(battleEvents.ToList());
                else if (eventTabType == typeof(SiteConquered))
                    TabEvents.Add(conqueringEvents.ToList());
                else if (eventTabType == typeof(Era))
                    TabEvents.Add(eventTypes.ToList());
                else if (eventTabType == typeof(Artifact))
                    TabEvents.Add(artifactEvents.ToList());
                else if (eventTabType == typeof(BeastAttack))
                    TabEvents.Add(beastAttackEvents.ToList());
            }
            GenerateEventFilterCheckBoxes();

            foreach (Era era in world.Eras)
                listEras.Items.Add(era);


            cmbRace.Items.Add("All"); cmbRace.SelectedIndex = 0;
            foreach (var race in races)
                cmbRace.Items.Add(race.Key);
            cmbCaste.Items.Add("All"); cmbCaste.SelectedIndex = 0;
            foreach (var caste in castes)
                cmbCaste.Items.Add(caste.Key);
            cmbType.Items.Add("All"); cmbType.SelectedIndex = 0;
            foreach (var type in types)
                cmbType.Items.Add(type.Key);
            cmbSiteType.Items.Add("All"); cmbSiteType.SelectedIndex = 0;
            foreach (var site in sites)
                cmbSiteType.Items.Add(site.Key);
            cmbRegionType.Items.Add("All"); cmbRegionType.SelectedIndex = 0;
            foreach (var region in regions)
                cmbRegionType.Items.Add(region.Key);
            cmbURegionType.Items.Add("All"); cmbURegionType.SelectedIndex = 0;
            foreach (var uregion in uregions)
                cmbURegionType.Items.Add(uregion.Key);
            cmbCivRace.Items.Add("All"); cmbCivRace.SelectedIndex = 0;
            foreach (var civRace in civRaces)
                cmbCivRace.Items.Add(civRace.Key);
            cmbConqueringType.Items.Add("All"); cmbConqueringType.SelectedIndex = 0;
            foreach (var conquerType in conquerTypes)
                cmbConqueringType.Items.Add(conquerType.Key);
            cmbSitePopulation.Items.Add("All"); cmbSitePopulation.SelectedIndex = 0;
            foreach (var populationType in populationTypes)
                cmbSitePopulation.Items.Add(populationType.Key);
            cmbEntityPopulation.Items.Add("All"); cmbEntityPopulation.SelectedIndex = 0;
            foreach(var civPopulation in civPopulationTypes)
                cmbEntityPopulation.Items.Add(civPopulation.Key);


            numStart.Maximum = numEraEnd.Value = numEraEnd.Maximum = world.Events.Last().Year;
            

            DontRefreshBrowserPages = true;
            foreach (CheckBox eraCheck in tpEraEvents.Controls.OfType<CheckBox>())
                eraCheck.Checked = false;
            DontRefreshBrowserPages = false;
            lblStatus.Text = "Done!";
            lblStatus.ForeColor = Color.Green;
            FileLoader.Working = false;
        }

        public void ResetForm()
        {
            txtXML.Text = "Legends XML / Archive";
            txtHistory.Text = "World History Text";
            txtSitePops.Text = "Sites and Populations Text";
            txtMap.Text = "Map Image";
            this.Text = "Legends Viewer " + version;
            if (world != null) this.Text += " " + world.Name;

            this.Text = "Legends Viewer " + version;

            lblBattleList.Text = lblHFList.Text = lblSiteList.Text = lblWarList.Text = "All";
            lblHFList.ForeColor = lblHFList.ForeColor = lblSiteList.ForeColor = lblWarList.ForeColor = Control.DefaultForeColor;
            lblHFList.Font = lblHFList.Font = lblSiteList.Font = lblWarList.Font = new Font(lblHFList.Font.FontFamily, lblHFList.Font.Size, FontStyle.Regular);

            txtLog.Clear();
            if (Browser != null) Browser.Reset();

            RemoveEventFilterCheckBoxes();
            if (TabEvents != null)
            TabEvents.Clear();

            txtHFSearch.Clear();
            listHFSearch.Items.Clear();
            chkAlive.Checked = chkDeity.Checked = chkGhost.Checked = chkVampire.Checked = chkWerebeast.Checked = chkForce.Checked = chkHFLeader.Checked = false;
            cmbRace.Items.Clear();
            cmbCaste.Items.Clear();
            cmbType.Items.Clear();
            radHFNone.Checked = true;

            txtSiteSearch.Clear();
            listSiteSearch.Items.Clear();
            cmbSiteType.Items.Clear();
            cmbSitePopulation.Items.Clear();
            radSiteNone.Checked = true;

            txtRegionSearch.Clear();
            listRegionSearch.Items.Clear();
            cmbRegionType.Items.Clear();
            radRegionNone.Checked = true;

            listURegionSearch.Items.Clear();
            cmbURegionType.Items.Clear();
            radURegionNone.Checked = true;

            txtCivSearch.Clear();
            listCivSearch.Items.Clear();
            cmbCivRace.Items.Clear();
            cmbEntityPopulation.Items.Clear();
            chkCiv.Checked = false;
            radEntityNone.Checked = true;

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

            txtArtifactSearch.Clear();
            listArtifactSearch.Items.Clear();
            radArtifactSortNone.Checked = true;

            listEras.Items.Clear();
            numStart.Value = -1;
            numEraEnd.Value = 0;
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            if (Browser != null)
                Browser.Back();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (Browser != null)
                Browser.Forward();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            if(Browser != null)
                Browser.CloseTab();
        }

        private void chkFilterWarfare_CheckedChanged(object sender, EventArgs e)
        {
            if (!FileLoader.Working && world != null)
                world.FilterBattles = chkFilterWarfare.Checked;
        }

        private void btnShowMap_Click(object sender, EventArgs e)
        {
            if (!FileLoader.Working && world != null)
            {
                Browser.Navigate(ControlOption.Map);
                ((Browser.SelectedTab as DwarfTabPage).Current.GetControl() as MapPanel).ToggleCivs();
            }
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            if (!FileLoader.Working && world != null)
                Browser.Navigate(ControlOption.HTML, world);
        }

        private void btnChart_Click(object sender, EventArgs e)
        {
            if (!FileLoader.Working && world != null)
            {
                Browser.Navigate(ControlOption.Chart, new Era(-1, world.Events.Last().Year, world));
            }
        }

        private void frmLegendsViewer_ResizeEnd(object sender, EventArgs e)
        {
            foreach (DwarfTabPage dwarfPage in Browser.TabPages.OfType<DwarfTabPage>())
                if (dwarfPage.Current.GetType() == typeof(ChartControl))
                    (dwarfPage.Current as ChartControl).DwarfChart.RefreshAllSeries();
        }

        private void btnAdvancedSearch_Click(object sender, EventArgs e)
        {
            if (!FileLoader.Working && world != null)
                Browser.Navigate(ControlOption.Search);
        }

        private void txtLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
                if (txtLog.SelectedText != "")
                    Clipboard.SetText(txtLog.SelectedText);
        }

        private void searchArtifactList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && world != null)
            {
                artifactSearch.Name = txtArtifactSearch.Text;
                artifactSearch.SortEvents = radArtifactSortEvents.Checked;
                artifactSearch.SortFiltered = radArtifactSortFiltered.Checked;
                IEnumerable<Artifact> list = artifactSearch.GetList();
                listArtifactSearch.Items.Clear();
                listArtifactSearch.Items.AddRange(list.ToArray());
            }
        }

        private void ResetArtifactBaseList(object sender, EventArgs e)
        {
            if (!FileLoader.Working && world != null)
            {
                lblArtifactList.Text = "All";
                lblArtifactList.ForeColor = Control.DefaultForeColor;
                lblArtifactList.Font = new Font(lblBattleList.Font.FontFamily, lblBattleList.Font.Size, FontStyle.Regular);
                artifactSearch.BaseList = world.Artifacts;
                searchArtifactList(null, null);
            }
        }
    }
}
