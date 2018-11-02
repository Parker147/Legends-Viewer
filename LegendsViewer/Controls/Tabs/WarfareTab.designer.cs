﻿using System.ComponentModel;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace LegendsViewer.Controls.Tabs
{
    partial class WarfareTab
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.tcWarfare = new System.Windows.Forms.TabControl();
            this.tpWars = new System.Windows.Forms.TabPage();
            this.tcWars = new System.Windows.Forms.TabControl();
            this.tpWarSearch = new System.Windows.Forms.TabPage();
            this.lblWarResults = new System.Windows.Forms.Label();
            this.listWarSearch = new BrightIdeasSoftware.ObjectListView();
            this.olvName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvDeaths = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnWarListReset = new System.Windows.Forms.Button();
            this.lblWarList = new System.Windows.Forms.Label();
            this.btnWarSearch = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.chkFilterWarfare = new System.Windows.Forms.CheckBox();
            this.chkWarOngoing = new System.Windows.Forms.CheckBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.radWarsSortConquerings = new System.Windows.Forms.RadioButton();
            this.radWarSortWarfare = new System.Windows.Forms.RadioButton();
            this.radWarDeaths = new System.Windows.Forms.RadioButton();
            this.radWarLength = new System.Windows.Forms.RadioButton();
            this.radWarSortFiltered = new System.Windows.Forms.RadioButton();
            this.radWarSortNone = new System.Windows.Forms.RadioButton();
            this.radWarSortEvents = new System.Windows.Forms.RadioButton();
            this.txtWarSearch = new System.Windows.Forms.TextBox();
            this.tpWarEvents = new System.Windows.Forms.TabPage();
            this.tpBattles = new System.Windows.Forms.TabPage();
            this.tcBattles = new System.Windows.Forms.TabControl();
            this.tpBattlesSearch = new System.Windows.Forms.TabPage();
            this.lblBattleResults = new System.Windows.Forms.Label();
            this.listBattleSearch = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnBattleListReset = new System.Windows.Forms.Button();
            this.lblBattleList = new System.Windows.Forms.Label();
            this.btnBattleSearch = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.radBattleSortDeaths = new System.Windows.Forms.RadioButton();
            this.radBattleSortFiltered = new System.Windows.Forms.RadioButton();
            this.radBattleSortNone = new System.Windows.Forms.RadioButton();
            this.radBattleSortEvents = new System.Windows.Forms.RadioButton();
            this.txtBattleSearch = new System.Windows.Forms.TextBox();
            this.tpBattlesEvents = new System.Windows.Forms.TabPage();
            this.tpConquerins = new System.Windows.Forms.TabPage();
            this.tcConquerings = new System.Windows.Forms.TabControl();
            this.tpConqueringsSearch = new System.Windows.Forms.TabPage();
            this.lblConqueringResult = new System.Windows.Forms.Label();
            this.listConqueringSearch = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnConqueringSearch = new System.Windows.Forms.Button();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbConqueringType = new System.Windows.Forms.ComboBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.radConqueringSortSite = new System.Windows.Forms.RadioButton();
            this.radConqueringSortFiltered = new System.Windows.Forms.RadioButton();
            this.radConqueringSortNone = new System.Windows.Forms.RadioButton();
            this.radConqueringSortEvents = new System.Windows.Forms.RadioButton();
            this.txtConqueringSearch = new System.Windows.Forms.TextBox();
            this.tpConqueringsEvents = new System.Windows.Forms.TabPage();
            this.tpBeastAttacks = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpBeastAttackSearch = new System.Windows.Forms.TabPage();
            this.lblBeastAttackResults = new System.Windows.Forms.Label();
            this.listBeastAttackSearch = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnBeastAttacksSearch = new System.Windows.Forms.Button();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.radBeastAttacksDeaths = new System.Windows.Forms.RadioButton();
            this.radBeastAttacksFiltered = new System.Windows.Forms.RadioButton();
            this.radBeastAttacksNone = new System.Windows.Forms.RadioButton();
            this.radBeastAttacksEvents = new System.Windows.Forms.RadioButton();
            this.txtBeastAttacksSearch = new System.Windows.Forms.TextBox();
            this.tpBeastAttackEvents = new System.Windows.Forms.TabPage();
            this.tpRaids = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tpRaidsSearch = new System.Windows.Forms.TabPage();
            this.lblRaidResults = new System.Windows.Forms.Label();
            this.listRaidSearch = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnRaidListReset = new System.Windows.Forms.Button();
            this.lblRaidList = new System.Windows.Forms.Label();
            this.btnRaidSearch = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radRaidSortStolen = new System.Windows.Forms.RadioButton();
            this.radRaidSortFiltered = new System.Windows.Forms.RadioButton();
            this.radRaidSortNone = new System.Windows.Forms.RadioButton();
            this.radRaidSortEvents = new System.Windows.Forms.RadioButton();
            this.txtRaidSearch = new System.Windows.Forms.TextBox();
            this.tpRaidsEvents = new System.Windows.Forms.TabPage();
            this.tcWarfare.SuspendLayout();
            this.tpWars.SuspendLayout();
            this.tcWars.SuspendLayout();
            this.tpWarSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listWarSearch)).BeginInit();
            this.groupBox10.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.tpBattles.SuspendLayout();
            this.tcBattles.SuspendLayout();
            this.tpBattlesSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBattleSearch)).BeginInit();
            this.groupBox12.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.tpConquerins.SuspendLayout();
            this.tcConquerings.SuspendLayout();
            this.tpConqueringsSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listConqueringSearch)).BeginInit();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.tpBeastAttacks.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpBeastAttackSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBeastAttackSearch)).BeginInit();
            this.groupBox18.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.tpRaids.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tpRaidsSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listRaidSearch)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcWarfare
            // 
            this.tcWarfare.Controls.Add(this.tpWars);
            this.tcWarfare.Controls.Add(this.tpBattles);
            this.tcWarfare.Controls.Add(this.tpConquerins);
            this.tcWarfare.Controls.Add(this.tpBeastAttacks);
            this.tcWarfare.Controls.Add(this.tpRaids);
            this.tcWarfare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcWarfare.ItemSize = new System.Drawing.Size(48, 18);
            this.tcWarfare.Location = new System.Drawing.Point(0, 0);
            this.tcWarfare.Multiline = true;
            this.tcWarfare.Name = "tcWarfare";
            this.tcWarfare.SelectedIndex = 0;
            this.tcWarfare.Size = new System.Drawing.Size(269, 518);
            this.tcWarfare.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tcWarfare.TabIndex = 1;
            // 
            // tpWars
            // 
            this.tpWars.Controls.Add(this.tcWars);
            this.tpWars.Location = new System.Drawing.Point(4, 22);
            this.tpWars.Name = "tpWars";
            this.tpWars.Size = new System.Drawing.Size(261, 492);
            this.tpWars.TabIndex = 0;
            this.tpWars.Text = "Wars";
            this.tpWars.UseVisualStyleBackColor = true;
            // 
            // tcWars
            // 
            this.tcWars.Controls.Add(this.tpWarSearch);
            this.tcWars.Controls.Add(this.tpWarEvents);
            this.tcWars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcWars.Location = new System.Drawing.Point(0, 0);
            this.tcWars.Name = "tcWars";
            this.tcWars.SelectedIndex = 0;
            this.tcWars.Size = new System.Drawing.Size(261, 492);
            this.tcWars.TabIndex = 0;
            // 
            // tpWarSearch
            // 
            this.tpWarSearch.Controls.Add(this.lblWarResults);
            this.tpWarSearch.Controls.Add(this.listWarSearch);
            this.tpWarSearch.Controls.Add(this.btnWarListReset);
            this.tpWarSearch.Controls.Add(this.lblWarList);
            this.tpWarSearch.Controls.Add(this.btnWarSearch);
            this.tpWarSearch.Controls.Add(this.groupBox10);
            this.tpWarSearch.Controls.Add(this.txtWarSearch);
            this.tpWarSearch.Location = new System.Drawing.Point(4, 22);
            this.tpWarSearch.Name = "tpWarSearch";
            this.tpWarSearch.Size = new System.Drawing.Size(253, 466);
            this.tpWarSearch.TabIndex = 0;
            this.tpWarSearch.Text = "Search";
            this.tpWarSearch.UseVisualStyleBackColor = true;
            // 
            // lblWarResults
            // 
            this.lblWarResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWarResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarResults.Location = new System.Drawing.Point(163, 270);
            this.lblWarResults.Name = "lblWarResults";
            this.lblWarResults.Size = new System.Drawing.Size(87, 11);
            this.lblWarResults.TabIndex = 56;
            this.lblWarResults.Text = "0 / 0";
            this.lblWarResults.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.hint.SetToolTip(this.lblWarResults, "Results Shown");
            // 
            // listWarSearch
            // 
            this.listWarSearch.AllColumns.Add(this.olvName);
            this.listWarSearch.AllColumns.Add(this.olvDeaths);
            this.listWarSearch.AlternateRowBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listWarSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listWarSearch.CellEditUseWholeCell = false;
            this.listWarSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvName,
            this.olvDeaths});
            this.listWarSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.listWarSearch.FullRowSelect = true;
            this.listWarSearch.GridLines = true;
            this.listWarSearch.HeaderWordWrap = true;
            this.listWarSearch.Location = new System.Drawing.Point(3, 31);
            this.listWarSearch.Name = "listWarSearch";
            this.listWarSearch.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.listWarSearch.ShowCommandMenuOnRightClick = true;
            this.listWarSearch.ShowImagesOnSubItems = true;
            this.listWarSearch.ShowItemCountOnGroups = true;
            this.listWarSearch.Size = new System.Drawing.Size(247, 236);
            this.listWarSearch.TabIndex = 55;
            this.listWarSearch.UseAlternatingBackColors = true;
            this.listWarSearch.UseCompatibleStateImageBehavior = false;
            this.listWarSearch.UseFiltering = true;
            this.listWarSearch.UseHotItem = true;
            this.listWarSearch.UseHyperlinks = true;
            this.listWarSearch.View = System.Windows.Forms.View.Details;
            this.listWarSearch.SelectedIndexChanged += new System.EventHandler(this.listWarSearch_SelectedIndexChanged);
            // 
            // olvName
            // 
            this.olvName.AspectName = "Name";
            this.olvName.IsEditable = false;
            this.olvName.MinimumWidth = 50;
            this.olvName.Text = "Name";
            this.olvName.UseInitialLetterForGroup = true;
            this.olvName.Width = 175;
            // 
            // olvDeaths
            // 
            this.olvDeaths.AspectName = "DeathCount";
            this.olvDeaths.IsEditable = false;
            this.olvDeaths.Text = "Deaths";
            this.olvDeaths.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvDeaths.Width = 50;
            // 
            // btnWarListReset
            // 
            this.btnWarListReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnWarListReset.Location = new System.Drawing.Point(0, 273);
            this.btnWarListReset.Name = "btnWarListReset";
            this.btnWarListReset.Size = new System.Drawing.Size(50, 20);
            this.btnWarListReset.TabIndex = 48;
            this.btnWarListReset.Text = "Reset";
            this.btnWarListReset.UseVisualStyleBackColor = true;
            this.btnWarListReset.Click += new System.EventHandler(this.ResetWarBaseList);
            // 
            // lblWarList
            // 
            this.lblWarList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblWarList.Location = new System.Drawing.Point(56, 277);
            this.lblWarList.Name = "lblWarList";
            this.lblWarList.Size = new System.Drawing.Size(189, 19);
            this.lblWarList.TabIndex = 47;
            this.lblWarList.Text = "All";
            // 
            // btnWarSearch
            // 
            this.btnWarSearch.Location = new System.Drawing.Point(3, 3);
            this.btnWarSearch.Name = "btnWarSearch";
            this.btnWarSearch.Size = new System.Drawing.Size(75, 23);
            this.btnWarSearch.TabIndex = 46;
            this.btnWarSearch.Text = "Search";
            this.btnWarSearch.UseVisualStyleBackColor = true;
            this.btnWarSearch.Click += new System.EventHandler(this.SearchWarList);
            // 
            // groupBox10
            // 
            this.groupBox10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox10.Controls.Add(this.chkFilterWarfare);
            this.groupBox10.Controls.Add(this.chkWarOngoing);
            this.groupBox10.Controls.Add(this.groupBox11);
            this.groupBox10.Location = new System.Drawing.Point(3, 299);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(247, 164);
            this.groupBox10.TabIndex = 45;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Filter / Sort";
            // 
            // chkFilterWarfare
            // 
            this.chkFilterWarfare.AutoSize = true;
            this.chkFilterWarfare.Checked = true;
            this.chkFilterWarfare.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFilterWarfare.Location = new System.Drawing.Point(89, 141);
            this.chkFilterWarfare.Name = "chkFilterWarfare";
            this.chkFilterWarfare.Size = new System.Drawing.Size(141, 17);
            this.chkFilterWarfare.TabIndex = 17;
            this.chkFilterWarfare.Text = "Filter Unnotable Warfare";
            this.chkFilterWarfare.UseVisualStyleBackColor = true;
            this.chkFilterWarfare.CheckedChanged += new System.EventHandler(this.chkFilterWarfare_CheckedChanged);
            // 
            // chkWarOngoing
            // 
            this.chkWarOngoing.AutoSize = true;
            this.chkWarOngoing.Location = new System.Drawing.Point(9, 140);
            this.chkWarOngoing.Name = "chkWarOngoing";
            this.chkWarOngoing.Size = new System.Drawing.Size(66, 17);
            this.chkWarOngoing.TabIndex = 16;
            this.chkWarOngoing.Text = "Ongoing";
            this.chkWarOngoing.UseVisualStyleBackColor = true;
            this.chkWarOngoing.CheckedChanged += new System.EventHandler(this.SearchWarList);
            // 
            // groupBox11
            // 
            this.groupBox11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox11.Controls.Add(this.radWarsSortConquerings);
            this.groupBox11.Controls.Add(this.radWarSortWarfare);
            this.groupBox11.Controls.Add(this.radWarDeaths);
            this.groupBox11.Controls.Add(this.radWarLength);
            this.groupBox11.Controls.Add(this.radWarSortFiltered);
            this.groupBox11.Controls.Add(this.radWarSortNone);
            this.groupBox11.Controls.Add(this.radWarSortEvents);
            this.groupBox11.Location = new System.Drawing.Point(6, 19);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(235, 115);
            this.groupBox11.TabIndex = 15;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Sort By";
            // 
            // radWarsSortConquerings
            // 
            this.radWarsSortConquerings.AutoSize = true;
            this.radWarsSortConquerings.Location = new System.Drawing.Point(119, 42);
            this.radWarsSortConquerings.Name = "radWarsSortConquerings";
            this.radWarsSortConquerings.Size = new System.Drawing.Size(84, 17);
            this.radWarsSortConquerings.TabIndex = 20;
            this.radWarsSortConquerings.TabStop = true;
            this.radWarsSortConquerings.Text = "Conquerings";
            this.radWarsSortConquerings.UseVisualStyleBackColor = true;
            this.radWarsSortConquerings.CheckedChanged += new System.EventHandler(this.SearchWarList);
            // 
            // radWarSortWarfare
            // 
            this.radWarSortWarfare.AutoSize = true;
            this.radWarSortWarfare.Location = new System.Drawing.Point(119, 19);
            this.radWarSortWarfare.Name = "radWarSortWarfare";
            this.radWarSortWarfare.Size = new System.Drawing.Size(63, 17);
            this.radWarSortWarfare.TabIndex = 19;
            this.radWarSortWarfare.TabStop = true;
            this.radWarSortWarfare.Text = "Warfare";
            this.radWarSortWarfare.UseVisualStyleBackColor = true;
            this.radWarSortWarfare.CheckedChanged += new System.EventHandler(this.SearchWarList);
            // 
            // radWarDeaths
            // 
            this.radWarDeaths.AutoSize = true;
            this.radWarDeaths.Location = new System.Drawing.Point(6, 88);
            this.radWarDeaths.Name = "radWarDeaths";
            this.radWarDeaths.Size = new System.Drawing.Size(59, 17);
            this.radWarDeaths.TabIndex = 18;
            this.radWarDeaths.TabStop = true;
            this.radWarDeaths.Text = "Deaths";
            this.radWarDeaths.UseVisualStyleBackColor = true;
            this.radWarDeaths.CheckedChanged += new System.EventHandler(this.SearchWarList);
            // 
            // radWarLength
            // 
            this.radWarLength.AutoSize = true;
            this.radWarLength.Location = new System.Drawing.Point(6, 65);
            this.radWarLength.Name = "radWarLength";
            this.radWarLength.Size = new System.Drawing.Size(58, 17);
            this.radWarLength.TabIndex = 17;
            this.radWarLength.TabStop = true;
            this.radWarLength.Text = "Length";
            this.radWarLength.UseVisualStyleBackColor = true;
            this.radWarLength.CheckedChanged += new System.EventHandler(this.SearchWarList);
            // 
            // radWarSortFiltered
            // 
            this.radWarSortFiltered.AutoSize = true;
            this.radWarSortFiltered.Location = new System.Drawing.Point(6, 42);
            this.radWarSortFiltered.Name = "radWarSortFiltered";
            this.radWarSortFiltered.Size = new System.Drawing.Size(95, 17);
            this.radWarSortFiltered.TabIndex = 16;
            this.radWarSortFiltered.TabStop = true;
            this.radWarSortFiltered.Text = "Filtered Events";
            this.radWarSortFiltered.UseVisualStyleBackColor = true;
            this.radWarSortFiltered.CheckedChanged += new System.EventHandler(this.SearchWarList);
            // 
            // radWarSortNone
            // 
            this.radWarSortNone.AutoSize = true;
            this.radWarSortNone.Checked = true;
            this.radWarSortNone.Location = new System.Drawing.Point(119, 65);
            this.radWarSortNone.Name = "radWarSortNone";
            this.radWarSortNone.Size = new System.Drawing.Size(51, 17);
            this.radWarSortNone.TabIndex = 14;
            this.radWarSortNone.TabStop = true;
            this.radWarSortNone.Text = "None";
            this.radWarSortNone.UseVisualStyleBackColor = true;
            this.radWarSortNone.CheckedChanged += new System.EventHandler(this.SearchWarList);
            // 
            // radWarSortEvents
            // 
            this.radWarSortEvents.AutoSize = true;
            this.radWarSortEvents.Location = new System.Drawing.Point(6, 19);
            this.radWarSortEvents.Name = "radWarSortEvents";
            this.radWarSortEvents.Size = new System.Drawing.Size(58, 17);
            this.radWarSortEvents.TabIndex = 13;
            this.radWarSortEvents.Text = "Events";
            this.radWarSortEvents.UseVisualStyleBackColor = true;
            this.radWarSortEvents.CheckedChanged += new System.EventHandler(this.SearchWarList);
            // 
            // txtWarSearch
            // 
            this.txtWarSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWarSearch.Location = new System.Drawing.Point(81, 5);
            this.txtWarSearch.Name = "txtWarSearch";
            this.txtWarSearch.Size = new System.Drawing.Size(170, 20);
            this.txtWarSearch.TabIndex = 44;
            this.txtWarSearch.TextChanged += new System.EventHandler(this.SearchWarList);
            // 
            // tpWarEvents
            // 
            this.tpWarEvents.Location = new System.Drawing.Point(4, 22);
            this.tpWarEvents.Name = "tpWarEvents";
            this.tpWarEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tpWarEvents.Size = new System.Drawing.Size(253, 466);
            this.tpWarEvents.TabIndex = 1;
            this.tpWarEvents.Text = "Events";
            this.tpWarEvents.UseVisualStyleBackColor = true;
            // 
            // tpBattles
            // 
            this.tpBattles.Controls.Add(this.tcBattles);
            this.tpBattles.Location = new System.Drawing.Point(4, 22);
            this.tpBattles.Name = "tpBattles";
            this.tpBattles.Size = new System.Drawing.Size(261, 492);
            this.tpBattles.TabIndex = 1;
            this.tpBattles.Text = "Battles";
            this.tpBattles.UseVisualStyleBackColor = true;
            // 
            // tcBattles
            // 
            this.tcBattles.Controls.Add(this.tpBattlesSearch);
            this.tcBattles.Controls.Add(this.tpBattlesEvents);
            this.tcBattles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcBattles.Location = new System.Drawing.Point(0, 0);
            this.tcBattles.Name = "tcBattles";
            this.tcBattles.SelectedIndex = 0;
            this.tcBattles.Size = new System.Drawing.Size(261, 492);
            this.tcBattles.TabIndex = 1;
            // 
            // tpBattlesSearch
            // 
            this.tpBattlesSearch.Controls.Add(this.lblBattleResults);
            this.tpBattlesSearch.Controls.Add(this.listBattleSearch);
            this.tpBattlesSearch.Controls.Add(this.btnBattleListReset);
            this.tpBattlesSearch.Controls.Add(this.lblBattleList);
            this.tpBattlesSearch.Controls.Add(this.btnBattleSearch);
            this.tpBattlesSearch.Controls.Add(this.groupBox12);
            this.tpBattlesSearch.Controls.Add(this.txtBattleSearch);
            this.tpBattlesSearch.Location = new System.Drawing.Point(4, 22);
            this.tpBattlesSearch.Name = "tpBattlesSearch";
            this.tpBattlesSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tpBattlesSearch.Size = new System.Drawing.Size(253, 466);
            this.tpBattlesSearch.TabIndex = 0;
            this.tpBattlesSearch.Text = "Search";
            this.tpBattlesSearch.UseVisualStyleBackColor = true;
            // 
            // lblBattleResults
            // 
            this.lblBattleResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBattleResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBattleResults.Location = new System.Drawing.Point(163, 270);
            this.lblBattleResults.Name = "lblBattleResults";
            this.lblBattleResults.Size = new System.Drawing.Size(87, 11);
            this.lblBattleResults.TabIndex = 58;
            this.lblBattleResults.Text = "0 / 0";
            this.lblBattleResults.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.hint.SetToolTip(this.lblBattleResults, "Results Shown");
            // 
            // listBattleSearch
            // 
            this.listBattleSearch.AllColumns.Add(this.olvColumn1);
            this.listBattleSearch.AllColumns.Add(this.olvColumn2);
            this.listBattleSearch.AlternateRowBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listBattleSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBattleSearch.CellEditUseWholeCell = false;
            this.listBattleSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2});
            this.listBattleSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBattleSearch.FullRowSelect = true;
            this.listBattleSearch.GridLines = true;
            this.listBattleSearch.HeaderWordWrap = true;
            this.listBattleSearch.Location = new System.Drawing.Point(3, 31);
            this.listBattleSearch.Name = "listBattleSearch";
            this.listBattleSearch.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.listBattleSearch.ShowCommandMenuOnRightClick = true;
            this.listBattleSearch.ShowImagesOnSubItems = true;
            this.listBattleSearch.ShowItemCountOnGroups = true;
            this.listBattleSearch.Size = new System.Drawing.Size(247, 236);
            this.listBattleSearch.TabIndex = 57;
            this.listBattleSearch.UseAlternatingBackColors = true;
            this.listBattleSearch.UseCompatibleStateImageBehavior = false;
            this.listBattleSearch.UseFiltering = true;
            this.listBattleSearch.UseHotItem = true;
            this.listBattleSearch.UseHyperlinks = true;
            this.listBattleSearch.View = System.Windows.Forms.View.Details;
            this.listBattleSearch.SelectedIndexChanged += new System.EventHandler(this.listBattleSearch_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.MinimumWidth = 50;
            this.olvColumn1.Text = "Name";
            this.olvColumn1.UseInitialLetterForGroup = true;
            this.olvColumn1.Width = 175;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "DeathCount";
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Text = "Deaths";
            this.olvColumn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn2.Width = 50;
            // 
            // btnBattleListReset
            // 
            this.btnBattleListReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBattleListReset.Location = new System.Drawing.Point(3, 273);
            this.btnBattleListReset.Name = "btnBattleListReset";
            this.btnBattleListReset.Size = new System.Drawing.Size(50, 20);
            this.btnBattleListReset.TabIndex = 48;
            this.btnBattleListReset.Text = "Reset";
            this.btnBattleListReset.UseVisualStyleBackColor = true;
            this.btnBattleListReset.Click += new System.EventHandler(this.ResetBattleBaseList);
            // 
            // lblBattleList
            // 
            this.lblBattleList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBattleList.Location = new System.Drawing.Point(57, 277);
            this.lblBattleList.Name = "lblBattleList";
            this.lblBattleList.Size = new System.Drawing.Size(189, 20);
            this.lblBattleList.TabIndex = 47;
            this.lblBattleList.Text = "All";
            // 
            // btnBattleSearch
            // 
            this.btnBattleSearch.Location = new System.Drawing.Point(3, 3);
            this.btnBattleSearch.Name = "btnBattleSearch";
            this.btnBattleSearch.Size = new System.Drawing.Size(75, 23);
            this.btnBattleSearch.TabIndex = 46;
            this.btnBattleSearch.Text = "Search";
            this.btnBattleSearch.UseVisualStyleBackColor = true;
            this.btnBattleSearch.Click += new System.EventHandler(this.SearchBattleList);
            // 
            // groupBox12
            // 
            this.groupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox12.Controls.Add(this.groupBox13);
            this.groupBox12.Location = new System.Drawing.Point(3, 299);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(247, 164);
            this.groupBox12.TabIndex = 45;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Filter / Sort";
            // 
            // groupBox13
            // 
            this.groupBox13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox13.Controls.Add(this.radBattleSortDeaths);
            this.groupBox13.Controls.Add(this.radBattleSortFiltered);
            this.groupBox13.Controls.Add(this.radBattleSortNone);
            this.groupBox13.Controls.Add(this.radBattleSortEvents);
            this.groupBox13.Location = new System.Drawing.Point(133, 19);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(108, 126);
            this.groupBox13.TabIndex = 15;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Sort By";
            // 
            // radBattleSortDeaths
            // 
            this.radBattleSortDeaths.AutoSize = true;
            this.radBattleSortDeaths.Location = new System.Drawing.Point(6, 63);
            this.radBattleSortDeaths.Name = "radBattleSortDeaths";
            this.radBattleSortDeaths.Size = new System.Drawing.Size(59, 17);
            this.radBattleSortDeaths.TabIndex = 17;
            this.radBattleSortDeaths.TabStop = true;
            this.radBattleSortDeaths.Text = "Deaths";
            this.radBattleSortDeaths.UseVisualStyleBackColor = true;
            this.radBattleSortDeaths.CheckedChanged += new System.EventHandler(this.SearchBattleList);
            // 
            // radBattleSortFiltered
            // 
            this.radBattleSortFiltered.AutoSize = true;
            this.radBattleSortFiltered.Location = new System.Drawing.Point(6, 42);
            this.radBattleSortFiltered.Name = "radBattleSortFiltered";
            this.radBattleSortFiltered.Size = new System.Drawing.Size(95, 17);
            this.radBattleSortFiltered.TabIndex = 16;
            this.radBattleSortFiltered.TabStop = true;
            this.radBattleSortFiltered.Text = "Filtered Events";
            this.radBattleSortFiltered.UseVisualStyleBackColor = true;
            this.radBattleSortFiltered.CheckedChanged += new System.EventHandler(this.SearchBattleList);
            // 
            // radBattleSortNone
            // 
            this.radBattleSortNone.AutoSize = true;
            this.radBattleSortNone.Checked = true;
            this.radBattleSortNone.Location = new System.Drawing.Point(6, 86);
            this.radBattleSortNone.Name = "radBattleSortNone";
            this.radBattleSortNone.Size = new System.Drawing.Size(51, 17);
            this.radBattleSortNone.TabIndex = 14;
            this.radBattleSortNone.TabStop = true;
            this.radBattleSortNone.Text = "None";
            this.radBattleSortNone.UseVisualStyleBackColor = true;
            this.radBattleSortNone.CheckedChanged += new System.EventHandler(this.SearchBattleList);
            // 
            // radBattleSortEvents
            // 
            this.radBattleSortEvents.AutoSize = true;
            this.radBattleSortEvents.Location = new System.Drawing.Point(6, 19);
            this.radBattleSortEvents.Name = "radBattleSortEvents";
            this.radBattleSortEvents.Size = new System.Drawing.Size(58, 17);
            this.radBattleSortEvents.TabIndex = 13;
            this.radBattleSortEvents.Text = "Events";
            this.radBattleSortEvents.UseVisualStyleBackColor = true;
            this.radBattleSortEvents.CheckedChanged += new System.EventHandler(this.SearchBattleList);
            // 
            // txtBattleSearch
            // 
            this.txtBattleSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBattleSearch.Location = new System.Drawing.Point(81, 5);
            this.txtBattleSearch.Name = "txtBattleSearch";
            this.txtBattleSearch.Size = new System.Drawing.Size(169, 20);
            this.txtBattleSearch.TabIndex = 44;
            this.txtBattleSearch.TextChanged += new System.EventHandler(this.SearchBattleList);
            // 
            // tpBattlesEvents
            // 
            this.tpBattlesEvents.Location = new System.Drawing.Point(4, 22);
            this.tpBattlesEvents.Name = "tpBattlesEvents";
            this.tpBattlesEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tpBattlesEvents.Size = new System.Drawing.Size(253, 466);
            this.tpBattlesEvents.TabIndex = 1;
            this.tpBattlesEvents.Text = "Events";
            this.tpBattlesEvents.UseVisualStyleBackColor = true;
            // 
            // tpConquerins
            // 
            this.tpConquerins.Controls.Add(this.tcConquerings);
            this.tpConquerins.Location = new System.Drawing.Point(4, 22);
            this.tpConquerins.Name = "tpConquerins";
            this.tpConquerins.Size = new System.Drawing.Size(261, 492);
            this.tpConquerins.TabIndex = 2;
            this.tpConquerins.Text = "Conquerings";
            this.tpConquerins.UseVisualStyleBackColor = true;
            // 
            // tcConquerings
            // 
            this.tcConquerings.Controls.Add(this.tpConqueringsSearch);
            this.tcConquerings.Controls.Add(this.tpConqueringsEvents);
            this.tcConquerings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcConquerings.Location = new System.Drawing.Point(0, 0);
            this.tcConquerings.Name = "tcConquerings";
            this.tcConquerings.SelectedIndex = 0;
            this.tcConquerings.Size = new System.Drawing.Size(261, 492);
            this.tcConquerings.TabIndex = 1;
            // 
            // tpConqueringsSearch
            // 
            this.tpConqueringsSearch.Controls.Add(this.lblConqueringResult);
            this.tpConqueringsSearch.Controls.Add(this.listConqueringSearch);
            this.tpConqueringsSearch.Controls.Add(this.btnConqueringSearch);
            this.tpConqueringsSearch.Controls.Add(this.groupBox14);
            this.tpConqueringsSearch.Controls.Add(this.txtConqueringSearch);
            this.tpConqueringsSearch.Location = new System.Drawing.Point(4, 22);
            this.tpConqueringsSearch.Name = "tpConqueringsSearch";
            this.tpConqueringsSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tpConqueringsSearch.Size = new System.Drawing.Size(253, 466);
            this.tpConqueringsSearch.TabIndex = 0;
            this.tpConqueringsSearch.Text = "Search";
            this.tpConqueringsSearch.UseVisualStyleBackColor = true;
            // 
            // lblConqueringResult
            // 
            this.lblConqueringResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConqueringResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConqueringResult.Location = new System.Drawing.Point(163, 285);
            this.lblConqueringResult.Name = "lblConqueringResult";
            this.lblConqueringResult.Size = new System.Drawing.Size(87, 11);
            this.lblConqueringResult.TabIndex = 60;
            this.lblConqueringResult.Text = "0 / 0";
            this.lblConqueringResult.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.hint.SetToolTip(this.lblConqueringResult, "Results Shown");
            // 
            // listConqueringSearch
            // 
            this.listConqueringSearch.AllColumns.Add(this.olvColumn3);
            this.listConqueringSearch.AllColumns.Add(this.olvColumn6);
            this.listConqueringSearch.AlternateRowBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listConqueringSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listConqueringSearch.CellEditUseWholeCell = false;
            this.listConqueringSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn3,
            this.olvColumn6});
            this.listConqueringSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.listConqueringSearch.FullRowSelect = true;
            this.listConqueringSearch.GridLines = true;
            this.listConqueringSearch.HeaderWordWrap = true;
            this.listConqueringSearch.Location = new System.Drawing.Point(3, 32);
            this.listConqueringSearch.Name = "listConqueringSearch";
            this.listConqueringSearch.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.listConqueringSearch.ShowCommandMenuOnRightClick = true;
            this.listConqueringSearch.ShowImagesOnSubItems = true;
            this.listConqueringSearch.ShowItemCountOnGroups = true;
            this.listConqueringSearch.Size = new System.Drawing.Size(247, 250);
            this.listConqueringSearch.TabIndex = 59;
            this.listConqueringSearch.UseAlternatingBackColors = true;
            this.listConqueringSearch.UseCompatibleStateImageBehavior = false;
            this.listConqueringSearch.UseFiltering = true;
            this.listConqueringSearch.UseHotItem = true;
            this.listConqueringSearch.UseHyperlinks = true;
            this.listConqueringSearch.View = System.Windows.Forms.View.Details;
            this.listConqueringSearch.SelectedIndexChanged += new System.EventHandler(this.listConqueringSearch_SelectedIndexChanged);
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Name";
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.MinimumWidth = 50;
            this.olvColumn3.Text = "Name";
            this.olvColumn3.UseInitialLetterForGroup = true;
            this.olvColumn3.Width = 175;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "DeathCount";
            this.olvColumn6.Text = "Deaths";
            this.olvColumn6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn6.Width = 50;
            // 
            // btnConqueringSearch
            // 
            this.btnConqueringSearch.Location = new System.Drawing.Point(3, 3);
            this.btnConqueringSearch.Name = "btnConqueringSearch";
            this.btnConqueringSearch.Size = new System.Drawing.Size(75, 23);
            this.btnConqueringSearch.TabIndex = 46;
            this.btnConqueringSearch.Text = "Search";
            this.btnConqueringSearch.UseVisualStyleBackColor = true;
            this.btnConqueringSearch.Click += new System.EventHandler(this.SearchConqueringList);
            // 
            // groupBox14
            // 
            this.groupBox14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox14.Controls.Add(this.label2);
            this.groupBox14.Controls.Add(this.cmbConqueringType);
            this.groupBox14.Controls.Add(this.groupBox15);
            this.groupBox14.Location = new System.Drawing.Point(0, 299);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(252, 164);
            this.groupBox14.TabIndex = 45;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Filter / Sort";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Type";
            // 
            // cmbConqueringType
            // 
            this.cmbConqueringType.FormattingEnabled = true;
            this.cmbConqueringType.Location = new System.Drawing.Point(6, 32);
            this.cmbConqueringType.Name = "cmbConqueringType";
            this.cmbConqueringType.Size = new System.Drawing.Size(121, 21);
            this.cmbConqueringType.TabIndex = 16;
            this.cmbConqueringType.SelectedIndexChanged += new System.EventHandler(this.SearchConqueringList);
            // 
            // groupBox15
            // 
            this.groupBox15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox15.Controls.Add(this.radConqueringSortSite);
            this.groupBox15.Controls.Add(this.radConqueringSortFiltered);
            this.groupBox15.Controls.Add(this.radConqueringSortNone);
            this.groupBox15.Controls.Add(this.radConqueringSortEvents);
            this.groupBox15.Location = new System.Drawing.Point(133, 19);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(113, 116);
            this.groupBox15.TabIndex = 15;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Sort By";
            // 
            // radConqueringSortSite
            // 
            this.radConqueringSortSite.AutoSize = true;
            this.radConqueringSortSite.Location = new System.Drawing.Point(6, 65);
            this.radConqueringSortSite.Name = "radConqueringSortSite";
            this.radConqueringSortSite.Size = new System.Drawing.Size(43, 17);
            this.radConqueringSortSite.TabIndex = 17;
            this.radConqueringSortSite.TabStop = true;
            this.radConqueringSortSite.Text = "Site";
            this.radConqueringSortSite.UseVisualStyleBackColor = true;
            this.radConqueringSortSite.CheckedChanged += new System.EventHandler(this.SearchConqueringList);
            // 
            // radConqueringSortFiltered
            // 
            this.radConqueringSortFiltered.AutoSize = true;
            this.radConqueringSortFiltered.Location = new System.Drawing.Point(6, 42);
            this.radConqueringSortFiltered.Name = "radConqueringSortFiltered";
            this.radConqueringSortFiltered.Size = new System.Drawing.Size(95, 17);
            this.radConqueringSortFiltered.TabIndex = 16;
            this.radConqueringSortFiltered.TabStop = true;
            this.radConqueringSortFiltered.Text = "Filtered Events";
            this.radConqueringSortFiltered.UseVisualStyleBackColor = true;
            this.radConqueringSortFiltered.CheckedChanged += new System.EventHandler(this.SearchConqueringList);
            // 
            // radConqueringSortNone
            // 
            this.radConqueringSortNone.AutoSize = true;
            this.radConqueringSortNone.Checked = true;
            this.radConqueringSortNone.Location = new System.Drawing.Point(6, 88);
            this.radConqueringSortNone.Name = "radConqueringSortNone";
            this.radConqueringSortNone.Size = new System.Drawing.Size(51, 17);
            this.radConqueringSortNone.TabIndex = 14;
            this.radConqueringSortNone.TabStop = true;
            this.radConqueringSortNone.Text = "None";
            this.radConqueringSortNone.UseVisualStyleBackColor = true;
            this.radConqueringSortNone.CheckedChanged += new System.EventHandler(this.SearchConqueringList);
            // 
            // radConqueringSortEvents
            // 
            this.radConqueringSortEvents.AutoSize = true;
            this.radConqueringSortEvents.Location = new System.Drawing.Point(6, 19);
            this.radConqueringSortEvents.Name = "radConqueringSortEvents";
            this.radConqueringSortEvents.Size = new System.Drawing.Size(58, 17);
            this.radConqueringSortEvents.TabIndex = 13;
            this.radConqueringSortEvents.Text = "Events";
            this.radConqueringSortEvents.UseVisualStyleBackColor = true;
            this.radConqueringSortEvents.CheckedChanged += new System.EventHandler(this.SearchConqueringList);
            // 
            // txtConqueringSearch
            // 
            this.txtConqueringSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConqueringSearch.Location = new System.Drawing.Point(81, 5);
            this.txtConqueringSearch.Name = "txtConqueringSearch";
            this.txtConqueringSearch.Size = new System.Drawing.Size(169, 20);
            this.txtConqueringSearch.TabIndex = 44;
            this.txtConqueringSearch.TextChanged += new System.EventHandler(this.SearchConqueringList);
            // 
            // tpConqueringsEvents
            // 
            this.tpConqueringsEvents.Location = new System.Drawing.Point(4, 22);
            this.tpConqueringsEvents.Name = "tpConqueringsEvents";
            this.tpConqueringsEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tpConqueringsEvents.Size = new System.Drawing.Size(253, 466);
            this.tpConqueringsEvents.TabIndex = 1;
            this.tpConqueringsEvents.Text = "Events";
            this.tpConqueringsEvents.UseVisualStyleBackColor = true;
            // 
            // tpBeastAttacks
            // 
            this.tpBeastAttacks.Controls.Add(this.tabControl1);
            this.tpBeastAttacks.Location = new System.Drawing.Point(4, 22);
            this.tpBeastAttacks.Name = "tpBeastAttacks";
            this.tpBeastAttacks.Size = new System.Drawing.Size(261, 492);
            this.tpBeastAttacks.TabIndex = 4;
            this.tpBeastAttacks.Text = "Rampages";
            this.tpBeastAttacks.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpBeastAttackSearch);
            this.tabControl1.Controls.Add(this.tpBeastAttackEvents);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(261, 492);
            this.tabControl1.TabIndex = 0;
            // 
            // tpBeastAttackSearch
            // 
            this.tpBeastAttackSearch.Controls.Add(this.lblBeastAttackResults);
            this.tpBeastAttackSearch.Controls.Add(this.listBeastAttackSearch);
            this.tpBeastAttackSearch.Controls.Add(this.btnBeastAttacksSearch);
            this.tpBeastAttackSearch.Controls.Add(this.groupBox18);
            this.tpBeastAttackSearch.Controls.Add(this.txtBeastAttacksSearch);
            this.tpBeastAttackSearch.Location = new System.Drawing.Point(4, 22);
            this.tpBeastAttackSearch.Name = "tpBeastAttackSearch";
            this.tpBeastAttackSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tpBeastAttackSearch.Size = new System.Drawing.Size(253, 466);
            this.tpBeastAttackSearch.TabIndex = 0;
            this.tpBeastAttackSearch.Text = "Search";
            this.tpBeastAttackSearch.UseVisualStyleBackColor = true;
            // 
            // lblBeastAttackResults
            // 
            this.lblBeastAttackResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBeastAttackResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBeastAttackResults.Location = new System.Drawing.Point(163, 285);
            this.lblBeastAttackResults.Name = "lblBeastAttackResults";
            this.lblBeastAttackResults.Size = new System.Drawing.Size(87, 11);
            this.lblBeastAttackResults.TabIndex = 58;
            this.lblBeastAttackResults.Text = "0 / 0";
            this.lblBeastAttackResults.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.hint.SetToolTip(this.lblBeastAttackResults, "Results Shown");
            // 
            // listBeastAttackSearch
            // 
            this.listBeastAttackSearch.AllColumns.Add(this.olvColumn4);
            this.listBeastAttackSearch.AllColumns.Add(this.olvColumn5);
            this.listBeastAttackSearch.AlternateRowBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listBeastAttackSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBeastAttackSearch.CellEditUseWholeCell = false;
            this.listBeastAttackSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn4,
            this.olvColumn5});
            this.listBeastAttackSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBeastAttackSearch.FullRowSelect = true;
            this.listBeastAttackSearch.GridLines = true;
            this.listBeastAttackSearch.HeaderWordWrap = true;
            this.listBeastAttackSearch.Location = new System.Drawing.Point(3, 31);
            this.listBeastAttackSearch.Name = "listBeastAttackSearch";
            this.listBeastAttackSearch.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.listBeastAttackSearch.ShowCommandMenuOnRightClick = true;
            this.listBeastAttackSearch.ShowImagesOnSubItems = true;
            this.listBeastAttackSearch.ShowItemCountOnGroups = true;
            this.listBeastAttackSearch.Size = new System.Drawing.Size(247, 251);
            this.listBeastAttackSearch.TabIndex = 57;
            this.listBeastAttackSearch.UseAlternatingBackColors = true;
            this.listBeastAttackSearch.UseCompatibleStateImageBehavior = false;
            this.listBeastAttackSearch.UseFiltering = true;
            this.listBeastAttackSearch.UseHotItem = true;
            this.listBeastAttackSearch.UseHyperlinks = true;
            this.listBeastAttackSearch.View = System.Windows.Forms.View.Details;
            this.listBeastAttackSearch.SelectedIndexChanged += new System.EventHandler(this.listBeastAttacks_SelectedIndexChanged);
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "Name";
            this.olvColumn4.IsEditable = false;
            this.olvColumn4.MinimumWidth = 50;
            this.olvColumn4.Text = "Name";
            this.olvColumn4.UseInitialLetterForGroup = true;
            this.olvColumn4.Width = 175;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "DeathCount";
            this.olvColumn5.IsEditable = false;
            this.olvColumn5.Text = "Deaths";
            this.olvColumn5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn5.Width = 50;
            // 
            // btnBeastAttacksSearch
            // 
            this.btnBeastAttacksSearch.Location = new System.Drawing.Point(3, 3);
            this.btnBeastAttacksSearch.Name = "btnBeastAttacksSearch";
            this.btnBeastAttacksSearch.Size = new System.Drawing.Size(75, 23);
            this.btnBeastAttacksSearch.TabIndex = 50;
            this.btnBeastAttacksSearch.Text = "Search";
            this.btnBeastAttacksSearch.UseVisualStyleBackColor = true;
            this.btnBeastAttacksSearch.Click += new System.EventHandler(this.SearchBeastAttackList);
            // 
            // groupBox18
            // 
            this.groupBox18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox18.Controls.Add(this.groupBox17);
            this.groupBox18.Location = new System.Drawing.Point(3, 299);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(247, 164);
            this.groupBox18.TabIndex = 49;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Filter / Sort";
            // 
            // groupBox17
            // 
            this.groupBox17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox17.Controls.Add(this.radBeastAttacksDeaths);
            this.groupBox17.Controls.Add(this.radBeastAttacksFiltered);
            this.groupBox17.Controls.Add(this.radBeastAttacksNone);
            this.groupBox17.Controls.Add(this.radBeastAttacksEvents);
            this.groupBox17.Location = new System.Drawing.Point(133, 19);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(108, 126);
            this.groupBox17.TabIndex = 15;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Sort By";
            // 
            // radBeastAttacksDeaths
            // 
            this.radBeastAttacksDeaths.AutoSize = true;
            this.radBeastAttacksDeaths.Location = new System.Drawing.Point(6, 63);
            this.radBeastAttacksDeaths.Name = "radBeastAttacksDeaths";
            this.radBeastAttacksDeaths.Size = new System.Drawing.Size(59, 17);
            this.radBeastAttacksDeaths.TabIndex = 17;
            this.radBeastAttacksDeaths.TabStop = true;
            this.radBeastAttacksDeaths.Text = "Deaths";
            this.radBeastAttacksDeaths.UseVisualStyleBackColor = true;
            this.radBeastAttacksDeaths.CheckedChanged += new System.EventHandler(this.SearchBeastAttackList);
            // 
            // radBeastAttacksFiltered
            // 
            this.radBeastAttacksFiltered.AutoSize = true;
            this.radBeastAttacksFiltered.Location = new System.Drawing.Point(6, 42);
            this.radBeastAttacksFiltered.Name = "radBeastAttacksFiltered";
            this.radBeastAttacksFiltered.Size = new System.Drawing.Size(95, 17);
            this.radBeastAttacksFiltered.TabIndex = 16;
            this.radBeastAttacksFiltered.TabStop = true;
            this.radBeastAttacksFiltered.Text = "Filtered Events";
            this.radBeastAttacksFiltered.UseVisualStyleBackColor = true;
            this.radBeastAttacksFiltered.CheckedChanged += new System.EventHandler(this.SearchBeastAttackList);
            // 
            // radBeastAttacksNone
            // 
            this.radBeastAttacksNone.AutoSize = true;
            this.radBeastAttacksNone.Checked = true;
            this.radBeastAttacksNone.Location = new System.Drawing.Point(6, 86);
            this.radBeastAttacksNone.Name = "radBeastAttacksNone";
            this.radBeastAttacksNone.Size = new System.Drawing.Size(51, 17);
            this.radBeastAttacksNone.TabIndex = 14;
            this.radBeastAttacksNone.TabStop = true;
            this.radBeastAttacksNone.Text = "None";
            this.radBeastAttacksNone.UseVisualStyleBackColor = true;
            this.radBeastAttacksNone.CheckedChanged += new System.EventHandler(this.SearchBeastAttackList);
            // 
            // radBeastAttacksEvents
            // 
            this.radBeastAttacksEvents.AutoSize = true;
            this.radBeastAttacksEvents.Location = new System.Drawing.Point(6, 19);
            this.radBeastAttacksEvents.Name = "radBeastAttacksEvents";
            this.radBeastAttacksEvents.Size = new System.Drawing.Size(58, 17);
            this.radBeastAttacksEvents.TabIndex = 13;
            this.radBeastAttacksEvents.Text = "Events";
            this.radBeastAttacksEvents.UseVisualStyleBackColor = true;
            this.radBeastAttacksEvents.CheckedChanged += new System.EventHandler(this.SearchBeastAttackList);
            // 
            // txtBeastAttacksSearch
            // 
            this.txtBeastAttacksSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBeastAttacksSearch.Location = new System.Drawing.Point(81, 5);
            this.txtBeastAttacksSearch.Name = "txtBeastAttacksSearch";
            this.txtBeastAttacksSearch.Size = new System.Drawing.Size(169, 20);
            this.txtBeastAttacksSearch.TabIndex = 48;
            this.txtBeastAttacksSearch.TextChanged += new System.EventHandler(this.SearchBeastAttackList);
            // 
            // tpBeastAttackEvents
            // 
            this.tpBeastAttackEvents.Location = new System.Drawing.Point(4, 22);
            this.tpBeastAttackEvents.Name = "tpBeastAttackEvents";
            this.tpBeastAttackEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tpBeastAttackEvents.Size = new System.Drawing.Size(253, 466);
            this.tpBeastAttackEvents.TabIndex = 1;
            this.tpBeastAttackEvents.Text = "Events";
            this.tpBeastAttackEvents.UseVisualStyleBackColor = true;
            // 
            // tpRaids
            // 
            this.tpRaids.Controls.Add(this.tabControl2);
            this.tpRaids.Location = new System.Drawing.Point(4, 22);
            this.tpRaids.Name = "tpRaids";
            this.tpRaids.Size = new System.Drawing.Size(261, 492);
            this.tpRaids.TabIndex = 5;
            this.tpRaids.Text = "Raids";
            this.tpRaids.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tpRaidsSearch);
            this.tabControl2.Controls.Add(this.tpRaidsEvents);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(261, 492);
            this.tabControl2.TabIndex = 2;
            // 
            // tpRaidsSearch
            // 
            this.tpRaidsSearch.Controls.Add(this.lblRaidResults);
            this.tpRaidsSearch.Controls.Add(this.listRaidSearch);
            this.tpRaidsSearch.Controls.Add(this.btnRaidListReset);
            this.tpRaidsSearch.Controls.Add(this.lblRaidList);
            this.tpRaidsSearch.Controls.Add(this.btnRaidSearch);
            this.tpRaidsSearch.Controls.Add(this.groupBox1);
            this.tpRaidsSearch.Controls.Add(this.txtRaidSearch);
            this.tpRaidsSearch.Location = new System.Drawing.Point(4, 22);
            this.tpRaidsSearch.Name = "tpRaidsSearch";
            this.tpRaidsSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tpRaidsSearch.Size = new System.Drawing.Size(253, 466);
            this.tpRaidsSearch.TabIndex = 0;
            this.tpRaidsSearch.Text = "Search";
            this.tpRaidsSearch.UseVisualStyleBackColor = true;
            // 
            // lblRaidResults
            // 
            this.lblRaidResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRaidResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRaidResults.Location = new System.Drawing.Point(163, 270);
            this.lblRaidResults.Name = "lblRaidResults";
            this.lblRaidResults.Size = new System.Drawing.Size(87, 11);
            this.lblRaidResults.TabIndex = 58;
            this.lblRaidResults.Text = "0 / 0";
            this.lblRaidResults.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.hint.SetToolTip(this.lblRaidResults, "Results Shown");
            // 
            // listRaidSearch
            // 
            this.listRaidSearch.AllColumns.Add(this.olvColumn7);
            this.listRaidSearch.AllColumns.Add(this.olvColumn8);
            this.listRaidSearch.AlternateRowBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listRaidSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listRaidSearch.CellEditUseWholeCell = false;
            this.listRaidSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn7,
            this.olvColumn8});
            this.listRaidSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.listRaidSearch.FullRowSelect = true;
            this.listRaidSearch.GridLines = true;
            this.listRaidSearch.HeaderWordWrap = true;
            this.listRaidSearch.Location = new System.Drawing.Point(3, 31);
            this.listRaidSearch.Name = "listRaidSearch";
            this.listRaidSearch.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.listRaidSearch.ShowCommandMenuOnRightClick = true;
            this.listRaidSearch.ShowImagesOnSubItems = true;
            this.listRaidSearch.ShowItemCountOnGroups = true;
            this.listRaidSearch.Size = new System.Drawing.Size(247, 236);
            this.listRaidSearch.TabIndex = 57;
            this.listRaidSearch.UseAlternatingBackColors = true;
            this.listRaidSearch.UseCompatibleStateImageBehavior = false;
            this.listRaidSearch.UseFiltering = true;
            this.listRaidSearch.UseHotItem = true;
            this.listRaidSearch.UseHyperlinks = true;
            this.listRaidSearch.View = System.Windows.Forms.View.Details;
            this.listRaidSearch.SelectedIndexChanged += new System.EventHandler(this.listRaids_SelectedIndexChanged);
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "Name";
            this.olvColumn7.IsEditable = false;
            this.olvColumn7.MinimumWidth = 50;
            this.olvColumn7.Text = "Name";
            this.olvColumn7.UseInitialLetterForGroup = true;
            this.olvColumn7.Width = 175;
            // 
            // olvColumn8
            // 
            this.olvColumn8.AspectName = "ItemsStolenCount";
            this.olvColumn8.IsEditable = false;
            this.olvColumn8.Text = "Stolen";
            this.olvColumn8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn8.Width = 50;
            // 
            // btnRaidListReset
            // 
            this.btnRaidListReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRaidListReset.Location = new System.Drawing.Point(3, 273);
            this.btnRaidListReset.Name = "btnRaidListReset";
            this.btnRaidListReset.Size = new System.Drawing.Size(50, 20);
            this.btnRaidListReset.TabIndex = 48;
            this.btnRaidListReset.Text = "Reset";
            this.btnRaidListReset.UseVisualStyleBackColor = true;
            this.btnRaidListReset.Click += new System.EventHandler(this.ResetRaidBaseList);
            // 
            // lblRaidList
            // 
            this.lblRaidList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRaidList.Location = new System.Drawing.Point(57, 277);
            this.lblRaidList.Name = "lblRaidList";
            this.lblRaidList.Size = new System.Drawing.Size(189, 20);
            this.lblRaidList.TabIndex = 47;
            this.lblRaidList.Text = "All";
            // 
            // btnRaidSearch
            // 
            this.btnRaidSearch.Location = new System.Drawing.Point(3, 3);
            this.btnRaidSearch.Name = "btnRaidSearch";
            this.btnRaidSearch.Size = new System.Drawing.Size(75, 23);
            this.btnRaidSearch.TabIndex = 46;
            this.btnRaidSearch.Text = "Search";
            this.btnRaidSearch.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(3, 299);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 164);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter / Sort";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.radRaidSortStolen);
            this.groupBox2.Controls.Add(this.radRaidSortFiltered);
            this.groupBox2.Controls.Add(this.radRaidSortNone);
            this.groupBox2.Controls.Add(this.radRaidSortEvents);
            this.groupBox2.Location = new System.Drawing.Point(133, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(108, 126);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sort By";
            // 
            // radRaidSortStolen
            // 
            this.radRaidSortStolen.AutoSize = true;
            this.radRaidSortStolen.Location = new System.Drawing.Point(6, 63);
            this.radRaidSortStolen.Name = "radRaidSortStolen";
            this.radRaidSortStolen.Size = new System.Drawing.Size(83, 17);
            this.radRaidSortStolen.TabIndex = 17;
            this.radRaidSortStolen.TabStop = true;
            this.radRaidSortStolen.Text = "Items Stolen";
            this.radRaidSortStolen.UseVisualStyleBackColor = true;
            this.radRaidSortStolen.CheckedChanged += new System.EventHandler(this.SearchRaidsList);
            // 
            // radRaidSortFiltered
            // 
            this.radRaidSortFiltered.AutoSize = true;
            this.radRaidSortFiltered.Location = new System.Drawing.Point(6, 42);
            this.radRaidSortFiltered.Name = "radRaidSortFiltered";
            this.radRaidSortFiltered.Size = new System.Drawing.Size(95, 17);
            this.radRaidSortFiltered.TabIndex = 16;
            this.radRaidSortFiltered.TabStop = true;
            this.radRaidSortFiltered.Text = "Filtered Events";
            this.radRaidSortFiltered.UseVisualStyleBackColor = true;
            this.radRaidSortFiltered.CheckedChanged += new System.EventHandler(this.SearchRaidsList);
            // 
            // radRaidSortNone
            // 
            this.radRaidSortNone.AutoSize = true;
            this.radRaidSortNone.Checked = true;
            this.radRaidSortNone.Location = new System.Drawing.Point(6, 86);
            this.radRaidSortNone.Name = "radRaidSortNone";
            this.radRaidSortNone.Size = new System.Drawing.Size(51, 17);
            this.radRaidSortNone.TabIndex = 14;
            this.radRaidSortNone.TabStop = true;
            this.radRaidSortNone.Text = "None";
            this.radRaidSortNone.UseVisualStyleBackColor = true;
            this.radRaidSortNone.CheckedChanged += new System.EventHandler(this.SearchRaidsList);
            // 
            // radRaidSortEvents
            // 
            this.radRaidSortEvents.AutoSize = true;
            this.radRaidSortEvents.Location = new System.Drawing.Point(6, 19);
            this.radRaidSortEvents.Name = "radRaidSortEvents";
            this.radRaidSortEvents.Size = new System.Drawing.Size(58, 17);
            this.radRaidSortEvents.TabIndex = 13;
            this.radRaidSortEvents.Text = "Events";
            this.radRaidSortEvents.UseVisualStyleBackColor = true;
            this.radRaidSortEvents.CheckedChanged += new System.EventHandler(this.SearchRaidsList);
            // 
            // txtRaidSearch
            // 
            this.txtRaidSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRaidSearch.Location = new System.Drawing.Point(81, 5);
            this.txtRaidSearch.Name = "txtRaidSearch";
            this.txtRaidSearch.Size = new System.Drawing.Size(169, 20);
            this.txtRaidSearch.TabIndex = 44;
            this.txtRaidSearch.TextChanged += new System.EventHandler(this.SearchRaidsList);
            // 
            // tpRaidsEvents
            // 
            this.tpRaidsEvents.Location = new System.Drawing.Point(4, 22);
            this.tpRaidsEvents.Name = "tpRaidsEvents";
            this.tpRaidsEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tpRaidsEvents.Size = new System.Drawing.Size(253, 466);
            this.tpRaidsEvents.TabIndex = 1;
            this.tpRaidsEvents.Text = "Events";
            this.tpRaidsEvents.UseVisualStyleBackColor = true;
            // 
            // WarfareTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcWarfare);
            this.Name = "WarfareTab";
            this.tcWarfare.ResumeLayout(false);
            this.tpWars.ResumeLayout(false);
            this.tcWars.ResumeLayout(false);
            this.tpWarSearch.ResumeLayout(false);
            this.tpWarSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listWarSearch)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.tpBattles.ResumeLayout(false);
            this.tcBattles.ResumeLayout(false);
            this.tpBattlesSearch.ResumeLayout(false);
            this.tpBattlesSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBattleSearch)).EndInit();
            this.groupBox12.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.tpConquerins.ResumeLayout(false);
            this.tcConquerings.ResumeLayout(false);
            this.tpConqueringsSearch.ResumeLayout(false);
            this.tpConqueringsSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listConqueringSearch)).EndInit();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.tpBeastAttacks.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpBeastAttackSearch.ResumeLayout(false);
            this.tpBeastAttackSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBeastAttackSearch)).EndInit();
            this.groupBox18.ResumeLayout(false);
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.tpRaids.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tpRaidsSearch.ResumeLayout(false);
            this.tpRaidsSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listRaidSearch)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tcWarfare;
        private TabPage tpWars;
        private TabControl tcWars;
        private TabPage tpWarSearch;
        private Button btnWarListReset;
        private Label lblWarList;
        private Button btnWarSearch;
        private GroupBox groupBox10;
        private CheckBox chkFilterWarfare;
        private CheckBox chkWarOngoing;
        private GroupBox groupBox11;
        private RadioButton radWarsSortConquerings;
        private RadioButton radWarSortWarfare;
        private RadioButton radWarDeaths;
        private RadioButton radWarLength;
        private RadioButton radWarSortFiltered;
        private RadioButton radWarSortNone;
        private RadioButton radWarSortEvents;
        private TextBox txtWarSearch;
        private TabPage tpWarEvents;
        private TabPage tpBattles;
        private TabControl tcBattles;
        private TabPage tpBattlesSearch;
        private Button btnBattleListReset;
        private Label lblBattleList;
        private Button btnBattleSearch;
        private GroupBox groupBox12;
        private GroupBox groupBox13;
        private RadioButton radBattleSortDeaths;
        private RadioButton radBattleSortFiltered;
        private RadioButton radBattleSortNone;
        private RadioButton radBattleSortEvents;
        private TextBox txtBattleSearch;
        private TabPage tpBattlesEvents;
        private TabPage tpConquerins;
        private TabControl tcConquerings;
        private TabPage tpConqueringsSearch;
        private Button btnConqueringSearch;
        private GroupBox groupBox14;
        private Label label2;
        private ComboBox cmbConqueringType;
        private GroupBox groupBox15;
        private RadioButton radConqueringSortSite;
        private RadioButton radConqueringSortFiltered;
        private RadioButton radConqueringSortNone;
        private RadioButton radConqueringSortEvents;
        private TextBox txtConqueringSearch;
        private TabPage tpConqueringsEvents;
        private TabPage tpBeastAttacks;
        private TabControl tabControl1;
        private TabPage tpBeastAttackSearch;
        private Button btnBeastAttacksSearch;
        private GroupBox groupBox18;
        private GroupBox groupBox17;
        private RadioButton radBeastAttacksDeaths;
        private RadioButton radBeastAttacksFiltered;
        private RadioButton radBeastAttacksNone;
        private RadioButton radBeastAttacksEvents;
        private TextBox txtBeastAttacksSearch;
        private TabPage tpBeastAttackEvents;
        private Label lblWarResults;
        private OLVColumn olvName;
        private OLVColumn olvDeaths;
        private ObjectListView listWarSearch;
        private Label lblBattleResults;
        private OLVColumn olvColumn1;
        private OLVColumn olvColumn2;
        private ObjectListView listBattleSearch;
        private Label lblConqueringResult;
        private OLVColumn olvColumn3;
        private ObjectListView listConqueringSearch;
        private Label lblBeastAttackResults;
        private ObjectListView listBeastAttackSearch;
        private OLVColumn olvColumn5;
        private OLVColumn olvColumn4;
        private OLVColumn olvColumn6;
        private TabPage tpRaids;
        private TabControl tabControl2;
        private TabPage tpRaidsSearch;
        private Label lblRaidResults;
        private ObjectListView listRaidSearch;
        private OLVColumn olvColumn7;
        private OLVColumn olvColumn8;
        private Button btnRaidListReset;
        private Label lblRaidList;
        private Button btnRaidSearch;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private RadioButton radRaidSortStolen;
        private RadioButton radRaidSortFiltered;
        private RadioButton radRaidSortNone;
        private RadioButton radRaidSortEvents;
        private TextBox txtRaidSearch;
        private TabPage tpRaidsEvents;
    }
}
