namespace LegendsViewer.Controls.Tabs
{
    partial class GeographyTab
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tcRegionsSub = new System.Windows.Forms.TabControl();
            this.tpOverworld = new System.Windows.Forms.TabPage();
            this.tcRegions = new System.Windows.Forms.TabControl();
            this.tpRegionSearch = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.radRegionSortDeaths = new System.Windows.Forms.RadioButton();
            this.radRegionSortBattles = new System.Windows.Forms.RadioButton();
            this.radRegionSortFiltered = new System.Windows.Forms.RadioButton();
            this.radRegionNone = new System.Windows.Forms.RadioButton();
            this.radRegionSortEvents = new System.Windows.Forms.RadioButton();
            this.label27 = new System.Windows.Forms.Label();
            this.cmbRegionType = new System.Windows.Forms.ComboBox();
            this.txtRegionSearch = new System.Windows.Forms.TextBox();
            this.listRegionSearch = new System.Windows.Forms.ListBox();
            this.btnRegionSearch = new System.Windows.Forms.Button();
            this.tpRegionEvents = new System.Windows.Forms.TabPage();
            this.tpUnderground = new System.Windows.Forms.TabPage();
            this.tcURegions = new System.Windows.Forms.TabControl();
            this.tpURegionSearch = new System.Windows.Forms.TabPage();
            this.btnURegionSearch = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.radURegionSortFiltered = new System.Windows.Forms.RadioButton();
            this.radURegionNone = new System.Windows.Forms.RadioButton();
            this.radURegionSortEvents = new System.Windows.Forms.RadioButton();
            this.label28 = new System.Windows.Forms.Label();
            this.cmbURegionType = new System.Windows.Forms.ComboBox();
            this.listURegionSearch = new System.Windows.Forms.ListBox();
            this.tpURegionEvents = new System.Windows.Forms.TabPage();
            this.tpLandmasses = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnLandmassSearch = new System.Windows.Forms.Button();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.groupBox28 = new System.Windows.Forms.GroupBox();
            this.radLandmassFiltered = new System.Windows.Forms.RadioButton();
            this.radLandmassNone = new System.Windows.Forms.RadioButton();
            this.radLandmassEvents = new System.Windows.Forms.RadioButton();
            this.txtLandmassSearch = new System.Windows.Forms.TextBox();
            this.listLandmassSearch = new System.Windows.Forms.ListBox();
            this.tpLandmassEvents = new System.Windows.Forms.TabPage();
            this.tbMountainPeaks = new System.Windows.Forms.TabPage();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnMountainPeakSearch = new System.Windows.Forms.Button();
            this.groupBox29 = new System.Windows.Forms.GroupBox();
            this.groupBox30 = new System.Windows.Forms.GroupBox();
            this.radMountainPeakFiltered = new System.Windows.Forms.RadioButton();
            this.radMountainPeakNone = new System.Windows.Forms.RadioButton();
            this.radMountainPeakEvents = new System.Windows.Forms.RadioButton();
            this.txtMountainPeakSearch = new System.Windows.Forms.TextBox();
            this.listMountainPeakSearch = new System.Windows.Forms.ListBox();
            this.tpMountainPeakEvents = new System.Windows.Forms.TabPage();
            this.tcRegionsSub.SuspendLayout();
            this.tpOverworld.SuspendLayout();
            this.tcRegions.SuspendLayout();
            this.tpRegionSearch.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tpUnderground.SuspendLayout();
            this.tcURegions.SuspendLayout();
            this.tpURegionSearch.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.tpLandmasses.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox27.SuspendLayout();
            this.groupBox28.SuspendLayout();
            this.tbMountainPeaks.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox29.SuspendLayout();
            this.groupBox30.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcRegionsSub
            // 
            this.tcRegionsSub.Controls.Add(this.tpOverworld);
            this.tcRegionsSub.Controls.Add(this.tpUnderground);
            this.tcRegionsSub.Controls.Add(this.tpLandmasses);
            this.tcRegionsSub.Controls.Add(this.tbMountainPeaks);
            this.tcRegionsSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcRegionsSub.Location = new System.Drawing.Point(0, 0);
            this.tcRegionsSub.Multiline = true;
            this.tcRegionsSub.Name = "tcRegionsSub";
            this.tcRegionsSub.SelectedIndex = 0;
            this.tcRegionsSub.Size = new System.Drawing.Size(269, 518);
            this.tcRegionsSub.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tcRegionsSub.TabIndex = 2;
            // 
            // tpOverworld
            // 
            this.tpOverworld.Controls.Add(this.tcRegions);
            this.tpOverworld.Location = new System.Drawing.Point(4, 22);
            this.tpOverworld.Name = "tpOverworld";
            this.tpOverworld.Padding = new System.Windows.Forms.Padding(3);
            this.tpOverworld.Size = new System.Drawing.Size(261, 492);
            this.tpOverworld.TabIndex = 0;
            this.tpOverworld.Text = "Regions";
            this.tpOverworld.UseVisualStyleBackColor = true;
            // 
            // tcRegions
            // 
            this.tcRegions.Controls.Add(this.tpRegionSearch);
            this.tcRegions.Controls.Add(this.tpRegionEvents);
            this.tcRegions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcRegions.Location = new System.Drawing.Point(3, 3);
            this.tcRegions.Name = "tcRegions";
            this.tcRegions.SelectedIndex = 0;
            this.tcRegions.Size = new System.Drawing.Size(255, 486);
            this.tcRegions.TabIndex = 1;
            // 
            // tpRegionSearch
            // 
            this.tpRegionSearch.Controls.Add(this.groupBox2);
            this.tpRegionSearch.Controls.Add(this.txtRegionSearch);
            this.tpRegionSearch.Controls.Add(this.listRegionSearch);
            this.tpRegionSearch.Controls.Add(this.btnRegionSearch);
            this.tpRegionSearch.Location = new System.Drawing.Point(4, 22);
            this.tpRegionSearch.Name = "tpRegionSearch";
            this.tpRegionSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tpRegionSearch.Size = new System.Drawing.Size(247, 460);
            this.tpRegionSearch.TabIndex = 0;
            this.tpRegionSearch.Text = "Search";
            this.tpRegionSearch.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.groupBox7);
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Controls.Add(this.cmbRegionType);
            this.groupBox2.Location = new System.Drawing.Point(3, 270);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(240, 163);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter / Sort";
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.radRegionSortDeaths);
            this.groupBox7.Controls.Add(this.radRegionSortBattles);
            this.groupBox7.Controls.Add(this.radRegionSortFiltered);
            this.groupBox7.Controls.Add(this.radRegionNone);
            this.groupBox7.Controls.Add(this.radRegionSortEvents);
            this.groupBox7.Location = new System.Drawing.Point(133, 19);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(99, 131);
            this.groupBox7.TabIndex = 13;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Sort By";
            // 
            // radRegionSortDeaths
            // 
            this.radRegionSortDeaths.AutoSize = true;
            this.radRegionSortDeaths.Location = new System.Drawing.Point(6, 88);
            this.radRegionSortDeaths.Name = "radRegionSortDeaths";
            this.radRegionSortDeaths.Size = new System.Drawing.Size(59, 17);
            this.radRegionSortDeaths.TabIndex = 16;
            this.radRegionSortDeaths.TabStop = true;
            this.radRegionSortDeaths.Text = "Deaths";
            this.radRegionSortDeaths.UseVisualStyleBackColor = true;
            this.radRegionSortDeaths.CheckedChanged += new System.EventHandler(this.searchRegionList);
            // 
            // radRegionSortBattles
            // 
            this.radRegionSortBattles.AutoSize = true;
            this.radRegionSortBattles.Location = new System.Drawing.Point(6, 65);
            this.radRegionSortBattles.Name = "radRegionSortBattles";
            this.radRegionSortBattles.Size = new System.Drawing.Size(57, 17);
            this.radRegionSortBattles.TabIndex = 15;
            this.radRegionSortBattles.TabStop = true;
            this.radRegionSortBattles.Text = "Battles";
            this.radRegionSortBattles.UseVisualStyleBackColor = true;
            this.radRegionSortBattles.CheckedChanged += new System.EventHandler(this.searchRegionList);
            // 
            // radRegionSortFiltered
            // 
            this.radRegionSortFiltered.AutoSize = true;
            this.radRegionSortFiltered.Location = new System.Drawing.Point(6, 42);
            this.radRegionSortFiltered.Name = "radRegionSortFiltered";
            this.radRegionSortFiltered.Size = new System.Drawing.Size(95, 17);
            this.radRegionSortFiltered.TabIndex = 14;
            this.radRegionSortFiltered.TabStop = true;
            this.radRegionSortFiltered.Text = "Filtered Events";
            this.radRegionSortFiltered.UseVisualStyleBackColor = true;
            this.radRegionSortFiltered.CheckedChanged += new System.EventHandler(this.searchRegionList);
            // 
            // radRegionNone
            // 
            this.radRegionNone.AutoSize = true;
            this.radRegionNone.Checked = true;
            this.radRegionNone.Location = new System.Drawing.Point(6, 111);
            this.radRegionNone.Name = "radRegionNone";
            this.radRegionNone.Size = new System.Drawing.Size(51, 17);
            this.radRegionNone.TabIndex = 13;
            this.radRegionNone.TabStop = true;
            this.radRegionNone.Text = "None";
            this.radRegionNone.UseVisualStyleBackColor = true;
            this.radRegionNone.CheckedChanged += new System.EventHandler(this.searchRegionList);
            // 
            // radRegionSortEvents
            // 
            this.radRegionSortEvents.AutoSize = true;
            this.radRegionSortEvents.Location = new System.Drawing.Point(6, 19);
            this.radRegionSortEvents.Name = "radRegionSortEvents";
            this.radRegionSortEvents.Size = new System.Drawing.Size(58, 17);
            this.radRegionSortEvents.TabIndex = 12;
            this.radRegionSortEvents.Text = "Events";
            this.radRegionSortEvents.UseVisualStyleBackColor = true;
            this.radRegionSortEvents.CheckedChanged += new System.EventHandler(this.searchRegionList);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 16);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(31, 13);
            this.label27.TabIndex = 12;
            this.label27.Text = "Type";
            // 
            // cmbRegionType
            // 
            this.cmbRegionType.FormattingEnabled = true;
            this.cmbRegionType.Location = new System.Drawing.Point(6, 32);
            this.cmbRegionType.Name = "cmbRegionType";
            this.cmbRegionType.Size = new System.Drawing.Size(121, 21);
            this.cmbRegionType.TabIndex = 0;
            this.cmbRegionType.SelectedIndexChanged += new System.EventHandler(this.searchRegionList);
            // 
            // txtRegionSearch
            // 
            this.txtRegionSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegionSearch.Location = new System.Drawing.Point(81, 5);
            this.txtRegionSearch.Name = "txtRegionSearch";
            this.txtRegionSearch.Size = new System.Drawing.Size(163, 20);
            this.txtRegionSearch.TabIndex = 33;
            this.txtRegionSearch.TextChanged += new System.EventHandler(this.searchRegionList);
            // 
            // listRegionSearch
            // 
            this.listRegionSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listRegionSearch.FormattingEnabled = true;
            this.listRegionSearch.Location = new System.Drawing.Point(3, 31);
            this.listRegionSearch.Name = "listRegionSearch";
            this.listRegionSearch.Size = new System.Drawing.Size(242, 225);
            this.listRegionSearch.TabIndex = 32;
            this.listRegionSearch.SelectedIndexChanged += new System.EventHandler(this.listRegionSearch_SelectedIndexChanged);
            // 
            // btnRegionSearch
            // 
            this.btnRegionSearch.Location = new System.Drawing.Point(3, 3);
            this.btnRegionSearch.Name = "btnRegionSearch";
            this.btnRegionSearch.Size = new System.Drawing.Size(75, 23);
            this.btnRegionSearch.TabIndex = 31;
            this.btnRegionSearch.Text = "Search";
            this.btnRegionSearch.UseVisualStyleBackColor = true;
            this.btnRegionSearch.Click += new System.EventHandler(this.searchRegionList);
            // 
            // tpRegionEvents
            // 
            this.tpRegionEvents.AutoScroll = true;
            this.tpRegionEvents.Location = new System.Drawing.Point(4, 22);
            this.tpRegionEvents.Name = "tpRegionEvents";
            this.tpRegionEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tpRegionEvents.Size = new System.Drawing.Size(247, 460);
            this.tpRegionEvents.TabIndex = 1;
            this.tpRegionEvents.Text = "Events";
            this.tpRegionEvents.UseVisualStyleBackColor = true;
            // 
            // tpUnderground
            // 
            this.tpUnderground.Controls.Add(this.tcURegions);
            this.tpUnderground.Location = new System.Drawing.Point(4, 22);
            this.tpUnderground.Name = "tpUnderground";
            this.tpUnderground.Padding = new System.Windows.Forms.Padding(3);
            this.tpUnderground.Size = new System.Drawing.Size(261, 492);
            this.tpUnderground.TabIndex = 1;
            this.tpUnderground.Text = "Underground";
            this.tpUnderground.UseVisualStyleBackColor = true;
            // 
            // tcURegions
            // 
            this.tcURegions.Controls.Add(this.tpURegionSearch);
            this.tcURegions.Controls.Add(this.tpURegionEvents);
            this.tcURegions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcURegions.Location = new System.Drawing.Point(3, 3);
            this.tcURegions.Name = "tcURegions";
            this.tcURegions.SelectedIndex = 0;
            this.tcURegions.Size = new System.Drawing.Size(255, 486);
            this.tcURegions.TabIndex = 1;
            // 
            // tpURegionSearch
            // 
            this.tpURegionSearch.Controls.Add(this.btnURegionSearch);
            this.tpURegionSearch.Controls.Add(this.groupBox3);
            this.tpURegionSearch.Controls.Add(this.listURegionSearch);
            this.tpURegionSearch.Location = new System.Drawing.Point(4, 22);
            this.tpURegionSearch.Name = "tpURegionSearch";
            this.tpURegionSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tpURegionSearch.Size = new System.Drawing.Size(247, 460);
            this.tpURegionSearch.TabIndex = 0;
            this.tpURegionSearch.Text = "Search";
            this.tpURegionSearch.UseVisualStyleBackColor = true;
            // 
            // btnURegionSearch
            // 
            this.btnURegionSearch.Location = new System.Drawing.Point(3, 3);
            this.btnURegionSearch.Name = "btnURegionSearch";
            this.btnURegionSearch.Size = new System.Drawing.Size(75, 23);
            this.btnURegionSearch.TabIndex = 34;
            this.btnURegionSearch.Text = "Search";
            this.btnURegionSearch.UseVisualStyleBackColor = true;
            this.btnURegionSearch.Click += new System.EventHandler(this.searchURegionList);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.groupBox8);
            this.groupBox3.Controls.Add(this.label28);
            this.groupBox3.Controls.Add(this.cmbURegionType);
            this.groupBox3.Location = new System.Drawing.Point(3, 271);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(240, 162);
            this.groupBox3.TabIndex = 33;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filter / Sort";
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox8.Controls.Add(this.radURegionSortFiltered);
            this.groupBox8.Controls.Add(this.radURegionNone);
            this.groupBox8.Controls.Add(this.radURegionSortEvents);
            this.groupBox8.Location = new System.Drawing.Point(133, 19);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(99, 92);
            this.groupBox8.TabIndex = 13;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Sort By";
            // 
            // radURegionSortFiltered
            // 
            this.radURegionSortFiltered.AutoSize = true;
            this.radURegionSortFiltered.Location = new System.Drawing.Point(6, 42);
            this.radURegionSortFiltered.Name = "radURegionSortFiltered";
            this.radURegionSortFiltered.Size = new System.Drawing.Size(95, 17);
            this.radURegionSortFiltered.TabIndex = 14;
            this.radURegionSortFiltered.TabStop = true;
            this.radURegionSortFiltered.Text = "Filtered Events";
            this.radURegionSortFiltered.UseVisualStyleBackColor = true;
            this.radURegionSortFiltered.CheckedChanged += new System.EventHandler(this.searchURegionList);
            // 
            // radURegionNone
            // 
            this.radURegionNone.AutoSize = true;
            this.radURegionNone.Checked = true;
            this.radURegionNone.Location = new System.Drawing.Point(6, 65);
            this.radURegionNone.Name = "radURegionNone";
            this.radURegionNone.Size = new System.Drawing.Size(51, 17);
            this.radURegionNone.TabIndex = 13;
            this.radURegionNone.TabStop = true;
            this.radURegionNone.Text = "None";
            this.radURegionNone.UseVisualStyleBackColor = true;
            this.radURegionNone.CheckedChanged += new System.EventHandler(this.searchURegionList);
            // 
            // radURegionSortEvents
            // 
            this.radURegionSortEvents.AutoSize = true;
            this.radURegionSortEvents.Location = new System.Drawing.Point(6, 19);
            this.radURegionSortEvents.Name = "radURegionSortEvents";
            this.radURegionSortEvents.Size = new System.Drawing.Size(58, 17);
            this.radURegionSortEvents.TabIndex = 12;
            this.radURegionSortEvents.Text = "Events";
            this.radURegionSortEvents.UseVisualStyleBackColor = true;
            this.radURegionSortEvents.CheckedChanged += new System.EventHandler(this.searchURegionList);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(6, 16);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(31, 13);
            this.label28.TabIndex = 12;
            this.label28.Text = "Type";
            // 
            // cmbURegionType
            // 
            this.cmbURegionType.FormattingEnabled = true;
            this.cmbURegionType.Location = new System.Drawing.Point(6, 32);
            this.cmbURegionType.Name = "cmbURegionType";
            this.cmbURegionType.Size = new System.Drawing.Size(121, 21);
            this.cmbURegionType.TabIndex = 0;
            this.cmbURegionType.SelectedIndexChanged += new System.EventHandler(this.searchURegionList);
            // 
            // listURegionSearch
            // 
            this.listURegionSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listURegionSearch.FormattingEnabled = true;
            this.listURegionSearch.Location = new System.Drawing.Point(3, 31);
            this.listURegionSearch.Name = "listURegionSearch";
            this.listURegionSearch.Size = new System.Drawing.Size(242, 225);
            this.listURegionSearch.TabIndex = 32;
            this.listURegionSearch.SelectedIndexChanged += new System.EventHandler(this.listURegionSearch_SelectedIndexChanged);
            // 
            // tpURegionEvents
            // 
            this.tpURegionEvents.AutoScroll = true;
            this.tpURegionEvents.Location = new System.Drawing.Point(4, 22);
            this.tpURegionEvents.Name = "tpURegionEvents";
            this.tpURegionEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tpURegionEvents.Size = new System.Drawing.Size(247, 460);
            this.tpURegionEvents.TabIndex = 1;
            this.tpURegionEvents.Text = "Events";
            this.tpURegionEvents.UseVisualStyleBackColor = true;
            // 
            // tpLandmasses
            // 
            this.tpLandmasses.Controls.Add(this.tabControl2);
            this.tpLandmasses.Location = new System.Drawing.Point(4, 22);
            this.tpLandmasses.Name = "tpLandmasses";
            this.tpLandmasses.Size = new System.Drawing.Size(261, 492);
            this.tpLandmasses.TabIndex = 2;
            this.tpLandmasses.Text = "Landmasses";
            this.tpLandmasses.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tpLandmassEvents);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(261, 492);
            this.tabControl2.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnLandmassSearch);
            this.tabPage1.Controls.Add(this.groupBox27);
            this.tabPage1.Controls.Add(this.txtLandmassSearch);
            this.tabPage1.Controls.Add(this.listLandmassSearch);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(253, 466);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Search";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnLandmassSearch
            // 
            this.btnLandmassSearch.Location = new System.Drawing.Point(3, 3);
            this.btnLandmassSearch.Name = "btnLandmassSearch";
            this.btnLandmassSearch.Size = new System.Drawing.Size(75, 23);
            this.btnLandmassSearch.TabIndex = 46;
            this.btnLandmassSearch.Text = "Search";
            this.btnLandmassSearch.UseVisualStyleBackColor = true;
            this.btnLandmassSearch.Click += new System.EventHandler(this.searchLandmassList);
            // 
            // groupBox27
            // 
            this.groupBox27.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox27.Controls.Add(this.groupBox28);
            this.groupBox27.Location = new System.Drawing.Point(3, 296);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Size = new System.Drawing.Size(247, 164);
            this.groupBox27.TabIndex = 45;
            this.groupBox27.TabStop = false;
            this.groupBox27.Text = "Filter / Sort";
            // 
            // groupBox28
            // 
            this.groupBox28.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox28.Controls.Add(this.radLandmassFiltered);
            this.groupBox28.Controls.Add(this.radLandmassNone);
            this.groupBox28.Controls.Add(this.radLandmassEvents);
            this.groupBox28.Location = new System.Drawing.Point(133, 19);
            this.groupBox28.Name = "groupBox28";
            this.groupBox28.Size = new System.Drawing.Size(108, 126);
            this.groupBox28.TabIndex = 15;
            this.groupBox28.TabStop = false;
            this.groupBox28.Text = "Sort By";
            // 
            // radLandmassFiltered
            // 
            this.radLandmassFiltered.AutoSize = true;
            this.radLandmassFiltered.Location = new System.Drawing.Point(6, 42);
            this.radLandmassFiltered.Name = "radLandmassFiltered";
            this.radLandmassFiltered.Size = new System.Drawing.Size(95, 17);
            this.radLandmassFiltered.TabIndex = 16;
            this.radLandmassFiltered.TabStop = true;
            this.radLandmassFiltered.Text = "Filtered Events";
            this.radLandmassFiltered.UseVisualStyleBackColor = true;
            this.radLandmassFiltered.CheckedChanged += new System.EventHandler(this.searchLandmassList);
            // 
            // radLandmassNone
            // 
            this.radLandmassNone.AutoSize = true;
            this.radLandmassNone.Checked = true;
            this.radLandmassNone.Location = new System.Drawing.Point(6, 65);
            this.radLandmassNone.Name = "radLandmassNone";
            this.radLandmassNone.Size = new System.Drawing.Size(51, 17);
            this.radLandmassNone.TabIndex = 14;
            this.radLandmassNone.TabStop = true;
            this.radLandmassNone.Text = "None";
            this.radLandmassNone.UseVisualStyleBackColor = true;
            this.radLandmassNone.CheckedChanged += new System.EventHandler(this.searchLandmassList);
            // 
            // radLandmassEvents
            // 
            this.radLandmassEvents.AutoSize = true;
            this.radLandmassEvents.Location = new System.Drawing.Point(6, 19);
            this.radLandmassEvents.Name = "radLandmassEvents";
            this.radLandmassEvents.Size = new System.Drawing.Size(58, 17);
            this.radLandmassEvents.TabIndex = 13;
            this.radLandmassEvents.Text = "Events";
            this.radLandmassEvents.UseVisualStyleBackColor = true;
            this.radLandmassEvents.CheckedChanged += new System.EventHandler(this.searchLandmassList);
            // 
            // txtLandmassSearch
            // 
            this.txtLandmassSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLandmassSearch.Location = new System.Drawing.Point(81, 5);
            this.txtLandmassSearch.Name = "txtLandmassSearch";
            this.txtLandmassSearch.Size = new System.Drawing.Size(169, 20);
            this.txtLandmassSearch.TabIndex = 44;
            this.txtLandmassSearch.TextChanged += new System.EventHandler(this.searchLandmassList);
            // 
            // listLandmassSearch
            // 
            this.listLandmassSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listLandmassSearch.FormattingEnabled = true;
            this.listLandmassSearch.Location = new System.Drawing.Point(3, 31);
            this.listLandmassSearch.Name = "listLandmassSearch";
            this.listLandmassSearch.Size = new System.Drawing.Size(247, 264);
            this.listLandmassSearch.TabIndex = 43;
            this.listLandmassSearch.SelectedIndexChanged += new System.EventHandler(this.listLandmassSearch_SelectedIndexChanged);
            // 
            // tpLandmassEvents
            // 
            this.tpLandmassEvents.Location = new System.Drawing.Point(4, 22);
            this.tpLandmassEvents.Name = "tpLandmassEvents";
            this.tpLandmassEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tpLandmassEvents.Size = new System.Drawing.Size(253, 466);
            this.tpLandmassEvents.TabIndex = 1;
            this.tpLandmassEvents.Text = "Events";
            this.tpLandmassEvents.UseVisualStyleBackColor = true;
            // 
            // tbMountainPeaks
            // 
            this.tbMountainPeaks.Controls.Add(this.tabControl3);
            this.tbMountainPeaks.Location = new System.Drawing.Point(4, 22);
            this.tbMountainPeaks.Name = "tbMountainPeaks";
            this.tbMountainPeaks.Size = new System.Drawing.Size(261, 492);
            this.tbMountainPeaks.TabIndex = 3;
            this.tbMountainPeaks.Text = "Mountains";
            this.tbMountainPeaks.UseVisualStyleBackColor = true;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage3);
            this.tabControl3.Controls.Add(this.tpMountainPeakEvents);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Location = new System.Drawing.Point(0, 0);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(261, 492);
            this.tabControl3.TabIndex = 6;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnMountainPeakSearch);
            this.tabPage3.Controls.Add(this.groupBox29);
            this.tabPage3.Controls.Add(this.txtMountainPeakSearch);
            this.tabPage3.Controls.Add(this.listMountainPeakSearch);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(253, 466);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Search";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnMountainPeakSearch
            // 
            this.btnMountainPeakSearch.Location = new System.Drawing.Point(3, 3);
            this.btnMountainPeakSearch.Name = "btnMountainPeakSearch";
            this.btnMountainPeakSearch.Size = new System.Drawing.Size(75, 23);
            this.btnMountainPeakSearch.TabIndex = 46;
            this.btnMountainPeakSearch.Text = "Search";
            this.btnMountainPeakSearch.UseVisualStyleBackColor = true;
            this.btnMountainPeakSearch.Click += new System.EventHandler(this.searchMountainPeakList);
            // 
            // groupBox29
            // 
            this.groupBox29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox29.Controls.Add(this.groupBox30);
            this.groupBox29.Location = new System.Drawing.Point(3, 296);
            this.groupBox29.Name = "groupBox29";
            this.groupBox29.Size = new System.Drawing.Size(247, 164);
            this.groupBox29.TabIndex = 45;
            this.groupBox29.TabStop = false;
            this.groupBox29.Text = "Filter / Sort";
            // 
            // groupBox30
            // 
            this.groupBox30.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox30.Controls.Add(this.radMountainPeakFiltered);
            this.groupBox30.Controls.Add(this.radMountainPeakNone);
            this.groupBox30.Controls.Add(this.radMountainPeakEvents);
            this.groupBox30.Location = new System.Drawing.Point(133, 19);
            this.groupBox30.Name = "groupBox30";
            this.groupBox30.Size = new System.Drawing.Size(108, 126);
            this.groupBox30.TabIndex = 15;
            this.groupBox30.TabStop = false;
            this.groupBox30.Text = "Sort By";
            // 
            // radMountainPeakFiltered
            // 
            this.radMountainPeakFiltered.AutoSize = true;
            this.radMountainPeakFiltered.Location = new System.Drawing.Point(6, 42);
            this.radMountainPeakFiltered.Name = "radMountainPeakFiltered";
            this.radMountainPeakFiltered.Size = new System.Drawing.Size(95, 17);
            this.radMountainPeakFiltered.TabIndex = 16;
            this.radMountainPeakFiltered.TabStop = true;
            this.radMountainPeakFiltered.Text = "Filtered Events";
            this.radMountainPeakFiltered.UseVisualStyleBackColor = true;
            this.radMountainPeakFiltered.CheckedChanged += new System.EventHandler(this.searchMountainPeakList);
            // 
            // radMountainPeakNone
            // 
            this.radMountainPeakNone.AutoSize = true;
            this.radMountainPeakNone.Checked = true;
            this.radMountainPeakNone.Location = new System.Drawing.Point(6, 65);
            this.radMountainPeakNone.Name = "radMountainPeakNone";
            this.radMountainPeakNone.Size = new System.Drawing.Size(51, 17);
            this.radMountainPeakNone.TabIndex = 14;
            this.radMountainPeakNone.TabStop = true;
            this.radMountainPeakNone.Text = "None";
            this.radMountainPeakNone.UseVisualStyleBackColor = true;
            this.radMountainPeakNone.CheckedChanged += new System.EventHandler(this.searchMountainPeakList);
            // 
            // radMountainPeakEvents
            // 
            this.radMountainPeakEvents.AutoSize = true;
            this.radMountainPeakEvents.Location = new System.Drawing.Point(6, 19);
            this.radMountainPeakEvents.Name = "radMountainPeakEvents";
            this.radMountainPeakEvents.Size = new System.Drawing.Size(58, 17);
            this.radMountainPeakEvents.TabIndex = 13;
            this.radMountainPeakEvents.Text = "Events";
            this.radMountainPeakEvents.UseVisualStyleBackColor = true;
            this.radMountainPeakEvents.CheckedChanged += new System.EventHandler(this.searchMountainPeakList);
            // 
            // txtMountainPeakSearch
            // 
            this.txtMountainPeakSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMountainPeakSearch.Location = new System.Drawing.Point(81, 5);
            this.txtMountainPeakSearch.Name = "txtMountainPeakSearch";
            this.txtMountainPeakSearch.Size = new System.Drawing.Size(169, 20);
            this.txtMountainPeakSearch.TabIndex = 44;
            this.txtMountainPeakSearch.TextChanged += new System.EventHandler(this.searchMountainPeakList);
            // 
            // listMountainPeakSearch
            // 
            this.listMountainPeakSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listMountainPeakSearch.FormattingEnabled = true;
            this.listMountainPeakSearch.Location = new System.Drawing.Point(3, 31);
            this.listMountainPeakSearch.Name = "listMountainPeakSearch";
            this.listMountainPeakSearch.Size = new System.Drawing.Size(247, 251);
            this.listMountainPeakSearch.TabIndex = 43;
            this.listMountainPeakSearch.SelectedIndexChanged += new System.EventHandler(this.listMountainPeakSearch_SelectedIndexChanged);
            // 
            // tpMountainPeakEvents
            // 
            this.tpMountainPeakEvents.Location = new System.Drawing.Point(4, 22);
            this.tpMountainPeakEvents.Name = "tpMountainPeakEvents";
            this.tpMountainPeakEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tpMountainPeakEvents.Size = new System.Drawing.Size(253, 466);
            this.tpMountainPeakEvents.TabIndex = 1;
            this.tpMountainPeakEvents.Text = "Events";
            this.tpMountainPeakEvents.UseVisualStyleBackColor = true;
            // 
            // GeographyTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcRegionsSub);
            this.Name = "GeographyTab";
            this.tcRegionsSub.ResumeLayout(false);
            this.tpOverworld.ResumeLayout(false);
            this.tcRegions.ResumeLayout(false);
            this.tpRegionSearch.ResumeLayout(false);
            this.tpRegionSearch.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tpUnderground.ResumeLayout(false);
            this.tcURegions.ResumeLayout(false);
            this.tpURegionSearch.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.tpLandmasses.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox27.ResumeLayout(false);
            this.groupBox28.ResumeLayout(false);
            this.groupBox28.PerformLayout();
            this.tbMountainPeaks.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox29.ResumeLayout(false);
            this.groupBox30.ResumeLayout(false);
            this.groupBox30.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcRegionsSub;
        private System.Windows.Forms.TabPage tpOverworld;
        private System.Windows.Forms.TabControl tcRegions;
        private System.Windows.Forms.TabPage tpRegionSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton radRegionSortDeaths;
        private System.Windows.Forms.RadioButton radRegionSortBattles;
        private System.Windows.Forms.RadioButton radRegionSortFiltered;
        private System.Windows.Forms.RadioButton radRegionNone;
        private System.Windows.Forms.RadioButton radRegionSortEvents;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox cmbRegionType;
        private System.Windows.Forms.TextBox txtRegionSearch;
        private System.Windows.Forms.ListBox listRegionSearch;
        private System.Windows.Forms.Button btnRegionSearch;
        private System.Windows.Forms.TabPage tpRegionEvents;
        private System.Windows.Forms.TabPage tpUnderground;
        private System.Windows.Forms.TabControl tcURegions;
        private System.Windows.Forms.TabPage tpURegionSearch;
        private System.Windows.Forms.Button btnURegionSearch;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton radURegionSortFiltered;
        private System.Windows.Forms.RadioButton radURegionNone;
        private System.Windows.Forms.RadioButton radURegionSortEvents;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox cmbURegionType;
        private System.Windows.Forms.ListBox listURegionSearch;
        private System.Windows.Forms.TabPage tpURegionEvents;
        private System.Windows.Forms.TabPage tpLandmasses;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnLandmassSearch;
        private System.Windows.Forms.GroupBox groupBox27;
        private System.Windows.Forms.GroupBox groupBox28;
        private System.Windows.Forms.RadioButton radLandmassFiltered;
        private System.Windows.Forms.RadioButton radLandmassNone;
        private System.Windows.Forms.RadioButton radLandmassEvents;
        private System.Windows.Forms.TextBox txtLandmassSearch;
        private System.Windows.Forms.ListBox listLandmassSearch;
        private System.Windows.Forms.TabPage tpLandmassEvents;
        private System.Windows.Forms.TabPage tbMountainPeaks;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnMountainPeakSearch;
        private System.Windows.Forms.GroupBox groupBox29;
        private System.Windows.Forms.GroupBox groupBox30;
        private System.Windows.Forms.RadioButton radMountainPeakFiltered;
        private System.Windows.Forms.RadioButton radMountainPeakNone;
        private System.Windows.Forms.RadioButton radMountainPeakEvents;
        private System.Windows.Forms.TextBox txtMountainPeakSearch;
        private System.Windows.Forms.ListBox listMountainPeakSearch;
        private System.Windows.Forms.TabPage tpMountainPeakEvents;
    }
}
