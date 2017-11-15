using System.ComponentModel;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace LegendsViewer.Controls.Tabs
{
    partial class ArtAndCraftsTab
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
            this.tcArtAndCrafts = new System.Windows.Forms.TabControl();
            this.tpArtifacts = new System.Windows.Forms.TabPage();
            this.tcArtifacts = new System.Windows.Forms.TabControl();
            this.tpArtifactsSearch = new System.Windows.Forms.TabPage();
            this.lblArtifactResults = new System.Windows.Forms.Label();
            this.listArtifactSearch = new BrightIdeasSoftware.ObjectListView();
            this.olvName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnArtifactListReset = new System.Windows.Forms.Button();
            this.lblArtifactList = new System.Windows.Forms.Label();
            this.btnArtifactSearch = new System.Windows.Forms.Button();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.lblArtMatFilter = new System.Windows.Forms.Label();
            this.cbmArtMatFilter = new System.Windows.Forms.ComboBox();
            this.lblArtTypeFilter = new System.Windows.Forms.Label();
            this.cbmArtTypeFilter = new System.Windows.Forms.ComboBox();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.radArtifactSortFiltered = new System.Windows.Forms.RadioButton();
            this.radArtifactSortNone = new System.Windows.Forms.RadioButton();
            this.radArtifactSortEvents = new System.Windows.Forms.RadioButton();
            this.txtArtifactSearch = new System.Windows.Forms.TextBox();
            this.tpArtifactsEvents = new System.Windows.Forms.TabPage();
            this.tpWrittenContent = new System.Windows.Forms.TabPage();
            this.tcWrittenContent = new System.Windows.Forms.TabControl();
            this.tpWrittenContentSearch = new System.Windows.Forms.TabPage();
            this.lblWrittenContentResults = new System.Windows.Forms.Label();
            this.listWrittenContentSearch = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnWrittenContentSearch = new System.Windows.Forms.Button();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbWrittenContentType = new System.Windows.Forms.ComboBox();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.radWrittenContentSortFiltered = new System.Windows.Forms.RadioButton();
            this.radWrittenContentSortNone = new System.Windows.Forms.RadioButton();
            this.radWrittenContentSortEvents = new System.Windows.Forms.RadioButton();
            this.txtWrittenContentSearch = new System.Windows.Forms.TextBox();
            this.tpWrittenContentEvents = new System.Windows.Forms.TabPage();
            this.tpDanceForms = new System.Windows.Forms.TabPage();
            this.tbMusicalForms = new System.Windows.Forms.TabPage();
            this.tpPoeticForms = new System.Windows.Forms.TabPage();
            this.tcDanceForms = new System.Windows.Forms.TabControl();
            this.tpDanceFormsSearch = new System.Windows.Forms.TabPage();
            this.lblDanceFormsResults = new System.Windows.Forms.Label();
            this.listDanceFormsSearch = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnDanceFormsSearch = new System.Windows.Forms.Button();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.groupBox28 = new System.Windows.Forms.GroupBox();
            this.radDanceFormsFiltered = new System.Windows.Forms.RadioButton();
            this.radDanceFormsNone = new System.Windows.Forms.RadioButton();
            this.radDanceFormsEvents = new System.Windows.Forms.RadioButton();
            this.txtDanceFormsSearch = new System.Windows.Forms.TextBox();
            this.tpDanceFormsEvents = new System.Windows.Forms.TabPage();
            this.tcMusicalForms = new System.Windows.Forms.TabControl();
            this.tpMusicalFormsSearch = new System.Windows.Forms.TabPage();
            this.lblMusicalFormsResults = new System.Windows.Forms.Label();
            this.listMusicalFormsSearch = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnMusicalFormsSearch = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radMusicalFormsFiltered = new System.Windows.Forms.RadioButton();
            this.radMusicalFormsNone = new System.Windows.Forms.RadioButton();
            this.radMusicalFormsEvents = new System.Windows.Forms.RadioButton();
            this.txtMusicalFormsSearch = new System.Windows.Forms.TextBox();
            this.tpMusicalFormsEvents = new System.Windows.Forms.TabPage();
            this.tcPoeticForms = new System.Windows.Forms.TabControl();
            this.tpPoeticFormsSearch = new System.Windows.Forms.TabPage();
            this.lblPoeticFormsResults = new System.Windows.Forms.Label();
            this.listPoeticFormsSearch = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnPoeticFormsSearch = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radPoeticFormsFiltered = new System.Windows.Forms.RadioButton();
            this.radPoeticFormsNone = new System.Windows.Forms.RadioButton();
            this.radPoeticFormsEvents = new System.Windows.Forms.RadioButton();
            this.txtPoeticFormsSearch = new System.Windows.Forms.TextBox();
            this.tpPoeticFormsEvents = new System.Windows.Forms.TabPage();
            this.tcArtAndCrafts.SuspendLayout();
            this.tpArtifacts.SuspendLayout();
            this.tcArtifacts.SuspendLayout();
            this.tpArtifactsSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listArtifactSearch)).BeginInit();
            this.groupBox19.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.tpWrittenContent.SuspendLayout();
            this.tcWrittenContent.SuspendLayout();
            this.tpWrittenContentSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listWrittenContentSearch)).BeginInit();
            this.groupBox21.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.tpDanceForms.SuspendLayout();
            this.tbMusicalForms.SuspendLayout();
            this.tpPoeticForms.SuspendLayout();
            this.tcDanceForms.SuspendLayout();
            this.tpDanceFormsSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listDanceFormsSearch)).BeginInit();
            this.groupBox27.SuspendLayout();
            this.groupBox28.SuspendLayout();
            this.tcMusicalForms.SuspendLayout();
            this.tpMusicalFormsSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listMusicalFormsSearch)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tcPoeticForms.SuspendLayout();
            this.tpPoeticFormsSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listPoeticFormsSearch)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcArtAndCrafts
            // 
            this.tcArtAndCrafts.Controls.Add(this.tpArtifacts);
            this.tcArtAndCrafts.Controls.Add(this.tpWrittenContent);
            this.tcArtAndCrafts.Controls.Add(this.tpDanceForms);
            this.tcArtAndCrafts.Controls.Add(this.tbMusicalForms);
            this.tcArtAndCrafts.Controls.Add(this.tpPoeticForms);
            this.tcArtAndCrafts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcArtAndCrafts.ItemSize = new System.Drawing.Size(86, 18);
            this.tcArtAndCrafts.Location = new System.Drawing.Point(0, 0);
            this.tcArtAndCrafts.Multiline = true;
            this.tcArtAndCrafts.Name = "tcArtAndCrafts";
            this.tcArtAndCrafts.SelectedIndex = 0;
            this.tcArtAndCrafts.Size = new System.Drawing.Size(269, 518);
            this.tcArtAndCrafts.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tcArtAndCrafts.TabIndex = 2;
            // 
            // tpArtifacts
            // 
            this.tpArtifacts.Controls.Add(this.tcArtifacts);
            this.tpArtifacts.Location = new System.Drawing.Point(4, 40);
            this.tpArtifacts.Name = "tpArtifacts";
            this.tpArtifacts.Size = new System.Drawing.Size(261, 474);
            this.tpArtifacts.TabIndex = 1;
            this.tpArtifacts.Text = "Artifacts";
            this.tpArtifacts.UseVisualStyleBackColor = true;
            // 
            // tcArtifacts
            // 
            this.tcArtifacts.Controls.Add(this.tpArtifactsSearch);
            this.tcArtifacts.Controls.Add(this.tpArtifactsEvents);
            this.tcArtifacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcArtifacts.Location = new System.Drawing.Point(0, 0);
            this.tcArtifacts.Name = "tcArtifacts";
            this.tcArtifacts.SelectedIndex = 0;
            this.tcArtifacts.Size = new System.Drawing.Size(261, 474);
            this.tcArtifacts.TabIndex = 3;
            // 
            // tpArtifactsSearch
            // 
            this.tpArtifactsSearch.Controls.Add(this.lblArtifactResults);
            this.tpArtifactsSearch.Controls.Add(this.listArtifactSearch);
            this.tpArtifactsSearch.Controls.Add(this.btnArtifactListReset);
            this.tpArtifactsSearch.Controls.Add(this.lblArtifactList);
            this.tpArtifactsSearch.Controls.Add(this.btnArtifactSearch);
            this.tpArtifactsSearch.Controls.Add(this.groupBox19);
            this.tpArtifactsSearch.Controls.Add(this.txtArtifactSearch);
            this.tpArtifactsSearch.Location = new System.Drawing.Point(4, 22);
            this.tpArtifactsSearch.Name = "tpArtifactsSearch";
            this.tpArtifactsSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tpArtifactsSearch.Size = new System.Drawing.Size(253, 448);
            this.tpArtifactsSearch.TabIndex = 0;
            this.tpArtifactsSearch.Text = "Search";
            this.tpArtifactsSearch.UseVisualStyleBackColor = true;
            // 
            // lblArtifactResults
            // 
            this.lblArtifactResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblArtifactResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArtifactResults.Location = new System.Drawing.Point(155, 300);
            this.lblArtifactResults.Name = "lblArtifactResults";
            this.lblArtifactResults.Size = new System.Drawing.Size(95, 10);
            this.lblArtifactResults.TabIndex = 53;
            this.lblArtifactResults.Text = "0 / 0";
            this.lblArtifactResults.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // listArtifactSearch
            // 
            this.listArtifactSearch.AllColumns.Add(this.olvName);
            this.listArtifactSearch.AllColumns.Add(this.olvType);
            this.listArtifactSearch.AlternateRowBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listArtifactSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listArtifactSearch.CellEditUseWholeCell = false;
            this.listArtifactSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvName,
            this.olvType});
            this.listArtifactSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.listArtifactSearch.FullRowSelect = true;
            this.listArtifactSearch.GridLines = true;
            this.listArtifactSearch.HeaderWordWrap = true;
            this.listArtifactSearch.Location = new System.Drawing.Point(3, 30);
            this.listArtifactSearch.Name = "listArtifactSearch";
            this.listArtifactSearch.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.listArtifactSearch.ShowCommandMenuOnRightClick = true;
            this.listArtifactSearch.ShowImagesOnSubItems = true;
            this.listArtifactSearch.ShowItemCountOnGroups = true;
            this.listArtifactSearch.Size = new System.Drawing.Size(247, 267);
            this.listArtifactSearch.TabIndex = 49;
            this.listArtifactSearch.UseAlternatingBackColors = true;
            this.listArtifactSearch.UseCompatibleStateImageBehavior = false;
            this.listArtifactSearch.UseFiltering = true;
            this.listArtifactSearch.UseHotItem = true;
            this.listArtifactSearch.UseHyperlinks = true;
            this.listArtifactSearch.View = System.Windows.Forms.View.Details;
            this.listArtifactSearch.SelectedIndexChanged += new System.EventHandler(this.listArtifactSearch_SelectedIndexChanged);
            // 
            // olvName
            // 
            this.olvName.AspectName = "Name";
            this.olvName.IsEditable = false;
            this.olvName.MinimumWidth = 50;
            this.olvName.Text = "Name";
            this.olvName.UseInitialLetterForGroup = true;
            this.olvName.Width = 155;
            // 
            // olvType
            // 
            this.olvType.AspectName = "Type";
            this.olvType.IsEditable = false;
            this.olvType.Text = "Type";
            this.olvType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvType.Width = 70;
            // 
            // btnArtifactListReset
            // 
            this.btnArtifactListReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnArtifactListReset.Location = new System.Drawing.Point(3, 305);
            this.btnArtifactListReset.Name = "btnArtifactListReset";
            this.btnArtifactListReset.Size = new System.Drawing.Size(50, 20);
            this.btnArtifactListReset.TabIndex = 48;
            this.btnArtifactListReset.Text = "Reset";
            this.btnArtifactListReset.UseVisualStyleBackColor = true;
            this.btnArtifactListReset.Click += new System.EventHandler(this.ResetArtifactBaseList);
            // 
            // lblArtifactList
            // 
            this.lblArtifactList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblArtifactList.Location = new System.Drawing.Point(58, 309);
            this.lblArtifactList.Name = "lblArtifactList";
            this.lblArtifactList.Size = new System.Drawing.Size(189, 20);
            this.lblArtifactList.TabIndex = 47;
            this.lblArtifactList.Text = "All";
            // 
            // btnArtifactSearch
            // 
            this.btnArtifactSearch.Location = new System.Drawing.Point(3, 3);
            this.btnArtifactSearch.Name = "btnArtifactSearch";
            this.btnArtifactSearch.Size = new System.Drawing.Size(75, 23);
            this.btnArtifactSearch.TabIndex = 46;
            this.btnArtifactSearch.Text = "Search";
            this.btnArtifactSearch.UseVisualStyleBackColor = true;
            this.btnArtifactSearch.Click += new System.EventHandler(this.SearchArtifactList);
            // 
            // groupBox19
            // 
            this.groupBox19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox19.Controls.Add(this.lblArtMatFilter);
            this.groupBox19.Controls.Add(this.cbmArtMatFilter);
            this.groupBox19.Controls.Add(this.lblArtTypeFilter);
            this.groupBox19.Controls.Add(this.cbmArtTypeFilter);
            this.groupBox19.Controls.Add(this.groupBox20);
            this.groupBox19.Location = new System.Drawing.Point(3, 331);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(247, 112);
            this.groupBox19.TabIndex = 45;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Filter / Sort";
            // 
            // lblArtMatFilter
            // 
            this.lblArtMatFilter.AutoSize = true;
            this.lblArtMatFilter.Location = new System.Drawing.Point(3, 63);
            this.lblArtMatFilter.Name = "lblArtMatFilter";
            this.lblArtMatFilter.Size = new System.Drawing.Size(44, 13);
            this.lblArtMatFilter.TabIndex = 21;
            this.lblArtMatFilter.Text = "Material";
            // 
            // cbmArtMatFilter
            // 
            this.cbmArtMatFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbmArtMatFilter.FormattingEnabled = true;
            this.cbmArtMatFilter.Location = new System.Drawing.Point(3, 79);
            this.cbmArtMatFilter.Name = "cbmArtMatFilter";
            this.cbmArtMatFilter.Size = new System.Drawing.Size(116, 21);
            this.cbmArtMatFilter.TabIndex = 20;
            this.cbmArtMatFilter.SelectedIndexChanged += new System.EventHandler(this.SearchArtifactList);
            // 
            // lblArtTypeFilter
            // 
            this.lblArtTypeFilter.AutoSize = true;
            this.lblArtTypeFilter.Location = new System.Drawing.Point(3, 17);
            this.lblArtTypeFilter.Name = "lblArtTypeFilter";
            this.lblArtTypeFilter.Size = new System.Drawing.Size(31, 13);
            this.lblArtTypeFilter.TabIndex = 19;
            this.lblArtTypeFilter.Text = "Type";
            // 
            // cbmArtTypeFilter
            // 
            this.cbmArtTypeFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbmArtTypeFilter.FormattingEnabled = true;
            this.cbmArtTypeFilter.Location = new System.Drawing.Point(3, 33);
            this.cbmArtTypeFilter.Name = "cbmArtTypeFilter";
            this.cbmArtTypeFilter.Size = new System.Drawing.Size(116, 21);
            this.cbmArtTypeFilter.TabIndex = 18;
            this.cbmArtTypeFilter.SelectedIndexChanged += new System.EventHandler(this.SearchArtifactList);
            // 
            // groupBox20
            // 
            this.groupBox20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox20.Controls.Add(this.radArtifactSortFiltered);
            this.groupBox20.Controls.Add(this.radArtifactSortNone);
            this.groupBox20.Controls.Add(this.radArtifactSortEvents);
            this.groupBox20.Location = new System.Drawing.Point(125, 14);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(116, 86);
            this.groupBox20.TabIndex = 15;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "Sort By";
            // 
            // radArtifactSortFiltered
            // 
            this.radArtifactSortFiltered.AutoSize = true;
            this.radArtifactSortFiltered.Location = new System.Drawing.Point(6, 42);
            this.radArtifactSortFiltered.Name = "radArtifactSortFiltered";
            this.radArtifactSortFiltered.Size = new System.Drawing.Size(95, 17);
            this.radArtifactSortFiltered.TabIndex = 16;
            this.radArtifactSortFiltered.TabStop = true;
            this.radArtifactSortFiltered.Text = "Filtered Events";
            this.radArtifactSortFiltered.UseVisualStyleBackColor = true;
            this.radArtifactSortFiltered.CheckedChanged += new System.EventHandler(this.SearchArtifactList);
            // 
            // radArtifactSortNone
            // 
            this.radArtifactSortNone.AutoSize = true;
            this.radArtifactSortNone.Checked = true;
            this.radArtifactSortNone.Location = new System.Drawing.Point(6, 65);
            this.radArtifactSortNone.Name = "radArtifactSortNone";
            this.radArtifactSortNone.Size = new System.Drawing.Size(51, 17);
            this.radArtifactSortNone.TabIndex = 14;
            this.radArtifactSortNone.TabStop = true;
            this.radArtifactSortNone.Text = "None";
            this.radArtifactSortNone.UseVisualStyleBackColor = true;
            this.radArtifactSortNone.CheckedChanged += new System.EventHandler(this.SearchArtifactList);
            // 
            // radArtifactSortEvents
            // 
            this.radArtifactSortEvents.AutoSize = true;
            this.radArtifactSortEvents.Location = new System.Drawing.Point(6, 19);
            this.radArtifactSortEvents.Name = "radArtifactSortEvents";
            this.radArtifactSortEvents.Size = new System.Drawing.Size(58, 17);
            this.radArtifactSortEvents.TabIndex = 13;
            this.radArtifactSortEvents.Text = "Events";
            this.radArtifactSortEvents.UseVisualStyleBackColor = true;
            this.radArtifactSortEvents.CheckedChanged += new System.EventHandler(this.SearchArtifactList);
            // 
            // txtArtifactSearch
            // 
            this.txtArtifactSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtArtifactSearch.Location = new System.Drawing.Point(81, 5);
            this.txtArtifactSearch.Name = "txtArtifactSearch";
            this.txtArtifactSearch.Size = new System.Drawing.Size(169, 20);
            this.txtArtifactSearch.TabIndex = 44;
            this.txtArtifactSearch.TextChanged += new System.EventHandler(this.SearchArtifactList);
            // 
            // tpArtifactsEvents
            // 
            this.tpArtifactsEvents.Location = new System.Drawing.Point(4, 22);
            this.tpArtifactsEvents.Name = "tpArtifactsEvents";
            this.tpArtifactsEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tpArtifactsEvents.Size = new System.Drawing.Size(253, 466);
            this.tpArtifactsEvents.TabIndex = 1;
            this.tpArtifactsEvents.Text = "Events";
            this.tpArtifactsEvents.UseVisualStyleBackColor = true;
            // 
            // tpWrittenContent
            // 
            this.tpWrittenContent.Controls.Add(this.tcWrittenContent);
            this.tpWrittenContent.Location = new System.Drawing.Point(4, 40);
            this.tpWrittenContent.Name = "tpWrittenContent";
            this.tpWrittenContent.Size = new System.Drawing.Size(261, 474);
            this.tpWrittenContent.TabIndex = 2;
            this.tpWrittenContent.Text = "Written Content";
            this.tpWrittenContent.UseVisualStyleBackColor = true;
            // 
            // tcWrittenContent
            // 
            this.tcWrittenContent.Controls.Add(this.tpWrittenContentSearch);
            this.tcWrittenContent.Controls.Add(this.tpWrittenContentEvents);
            this.tcWrittenContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcWrittenContent.Location = new System.Drawing.Point(0, 0);
            this.tcWrittenContent.Name = "tcWrittenContent";
            this.tcWrittenContent.SelectedIndex = 0;
            this.tcWrittenContent.Size = new System.Drawing.Size(261, 474);
            this.tcWrittenContent.TabIndex = 4;
            // 
            // tpWrittenContentSearch
            // 
            this.tpWrittenContentSearch.Controls.Add(this.lblWrittenContentResults);
            this.tpWrittenContentSearch.Controls.Add(this.listWrittenContentSearch);
            this.tpWrittenContentSearch.Controls.Add(this.btnWrittenContentSearch);
            this.tpWrittenContentSearch.Controls.Add(this.groupBox21);
            this.tpWrittenContentSearch.Controls.Add(this.txtWrittenContentSearch);
            this.tpWrittenContentSearch.Location = new System.Drawing.Point(4, 22);
            this.tpWrittenContentSearch.Name = "tpWrittenContentSearch";
            this.tpWrittenContentSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tpWrittenContentSearch.Size = new System.Drawing.Size(253, 448);
            this.tpWrittenContentSearch.TabIndex = 0;
            this.tpWrittenContentSearch.Text = "Search";
            this.tpWrittenContentSearch.UseVisualStyleBackColor = true;
            // 
            // lblWrittenContentResults
            // 
            this.lblWrittenContentResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWrittenContentResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWrittenContentResults.Location = new System.Drawing.Point(155, 265);
            this.lblWrittenContentResults.Name = "lblWrittenContentResults";
            this.lblWrittenContentResults.Size = new System.Drawing.Size(95, 10);
            this.lblWrittenContentResults.TabIndex = 55;
            this.lblWrittenContentResults.Text = "0 / 0";
            this.lblWrittenContentResults.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // listWrittenContentSearch
            // 
            this.listWrittenContentSearch.AllColumns.Add(this.olvColumn1);
            this.listWrittenContentSearch.AllColumns.Add(this.olvColumn2);
            this.listWrittenContentSearch.AlternateRowBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listWrittenContentSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listWrittenContentSearch.CellEditUseWholeCell = false;
            this.listWrittenContentSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2});
            this.listWrittenContentSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.listWrittenContentSearch.FullRowSelect = true;
            this.listWrittenContentSearch.GridLines = true;
            this.listWrittenContentSearch.HeaderWordWrap = true;
            this.listWrittenContentSearch.Location = new System.Drawing.Point(3, 27);
            this.listWrittenContentSearch.Name = "listWrittenContentSearch";
            this.listWrittenContentSearch.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.listWrittenContentSearch.ShowCommandMenuOnRightClick = true;
            this.listWrittenContentSearch.ShowImagesOnSubItems = true;
            this.listWrittenContentSearch.ShowItemCountOnGroups = true;
            this.listWrittenContentSearch.Size = new System.Drawing.Size(247, 233);
            this.listWrittenContentSearch.TabIndex = 54;
            this.listWrittenContentSearch.UseAlternatingBackColors = true;
            this.listWrittenContentSearch.UseCompatibleStateImageBehavior = false;
            this.listWrittenContentSearch.UseFiltering = true;
            this.listWrittenContentSearch.UseHotItem = true;
            this.listWrittenContentSearch.UseHyperlinks = true;
            this.listWrittenContentSearch.View = System.Windows.Forms.View.Details;
            this.listWrittenContentSearch.SelectedIndexChanged += new System.EventHandler(this.listWrittenContentSearch_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.MinimumWidth = 50;
            this.olvColumn1.Text = "Name";
            this.olvColumn1.UseInitialLetterForGroup = true;
            this.olvColumn1.Width = 155;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Type";
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Text = "Type";
            this.olvColumn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn2.Width = 70;
            // 
            // btnWrittenContentSearch
            // 
            this.btnWrittenContentSearch.Location = new System.Drawing.Point(3, 3);
            this.btnWrittenContentSearch.Name = "btnWrittenContentSearch";
            this.btnWrittenContentSearch.Size = new System.Drawing.Size(75, 23);
            this.btnWrittenContentSearch.TabIndex = 46;
            this.btnWrittenContentSearch.Text = "Search";
            this.btnWrittenContentSearch.UseVisualStyleBackColor = true;
            this.btnWrittenContentSearch.Click += new System.EventHandler(this.SearchWrittenContentList);
            // 
            // groupBox21
            // 
            this.groupBox21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox21.Controls.Add(this.label8);
            this.groupBox21.Controls.Add(this.cmbWrittenContentType);
            this.groupBox21.Controls.Add(this.groupBox22);
            this.groupBox21.Location = new System.Drawing.Point(3, 278);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(247, 164);
            this.groupBox21.TabIndex = 45;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Filter / Sort";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Type";
            // 
            // cmbWrittenContentType
            // 
            this.cmbWrittenContentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWrittenContentType.FormattingEnabled = true;
            this.cmbWrittenContentType.Location = new System.Drawing.Point(6, 35);
            this.cmbWrittenContentType.Name = "cmbWrittenContentType";
            this.cmbWrittenContentType.Size = new System.Drawing.Size(121, 21);
            this.cmbWrittenContentType.TabIndex = 18;
            this.cmbWrittenContentType.SelectedIndexChanged += new System.EventHandler(this.SearchWrittenContentList);
            // 
            // groupBox22
            // 
            this.groupBox22.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox22.Controls.Add(this.radWrittenContentSortFiltered);
            this.groupBox22.Controls.Add(this.radWrittenContentSortNone);
            this.groupBox22.Controls.Add(this.radWrittenContentSortEvents);
            this.groupBox22.Location = new System.Drawing.Point(133, 19);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(108, 126);
            this.groupBox22.TabIndex = 15;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "Sort By";
            // 
            // radWrittenContentSortFiltered
            // 
            this.radWrittenContentSortFiltered.AutoSize = true;
            this.radWrittenContentSortFiltered.Location = new System.Drawing.Point(6, 42);
            this.radWrittenContentSortFiltered.Name = "radWrittenContentSortFiltered";
            this.radWrittenContentSortFiltered.Size = new System.Drawing.Size(95, 17);
            this.radWrittenContentSortFiltered.TabIndex = 16;
            this.radWrittenContentSortFiltered.TabStop = true;
            this.radWrittenContentSortFiltered.Text = "Filtered Events";
            this.radWrittenContentSortFiltered.UseVisualStyleBackColor = true;
            this.radWrittenContentSortFiltered.CheckedChanged += new System.EventHandler(this.SearchWrittenContentList);
            // 
            // radWrittenContentSortNone
            // 
            this.radWrittenContentSortNone.AutoSize = true;
            this.radWrittenContentSortNone.Checked = true;
            this.radWrittenContentSortNone.Location = new System.Drawing.Point(6, 65);
            this.radWrittenContentSortNone.Name = "radWrittenContentSortNone";
            this.radWrittenContentSortNone.Size = new System.Drawing.Size(51, 17);
            this.radWrittenContentSortNone.TabIndex = 14;
            this.radWrittenContentSortNone.TabStop = true;
            this.radWrittenContentSortNone.Text = "None";
            this.radWrittenContentSortNone.UseVisualStyleBackColor = true;
            this.radWrittenContentSortNone.CheckedChanged += new System.EventHandler(this.SearchWrittenContentList);
            // 
            // radWrittenContentSortEvents
            // 
            this.radWrittenContentSortEvents.AutoSize = true;
            this.radWrittenContentSortEvents.Location = new System.Drawing.Point(6, 19);
            this.radWrittenContentSortEvents.Name = "radWrittenContentSortEvents";
            this.radWrittenContentSortEvents.Size = new System.Drawing.Size(58, 17);
            this.radWrittenContentSortEvents.TabIndex = 13;
            this.radWrittenContentSortEvents.Text = "Events";
            this.radWrittenContentSortEvents.UseVisualStyleBackColor = true;
            this.radWrittenContentSortEvents.CheckedChanged += new System.EventHandler(this.SearchWrittenContentList);
            // 
            // txtWrittenContentSearch
            // 
            this.txtWrittenContentSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWrittenContentSearch.Location = new System.Drawing.Point(81, 5);
            this.txtWrittenContentSearch.Name = "txtWrittenContentSearch";
            this.txtWrittenContentSearch.Size = new System.Drawing.Size(169, 20);
            this.txtWrittenContentSearch.TabIndex = 44;
            this.txtWrittenContentSearch.TextChanged += new System.EventHandler(this.SearchWrittenContentList);
            // 
            // tpWrittenContentEvents
            // 
            this.tpWrittenContentEvents.Location = new System.Drawing.Point(4, 22);
            this.tpWrittenContentEvents.Name = "tpWrittenContentEvents";
            this.tpWrittenContentEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tpWrittenContentEvents.Size = new System.Drawing.Size(253, 466);
            this.tpWrittenContentEvents.TabIndex = 1;
            this.tpWrittenContentEvents.Text = "Events";
            this.tpWrittenContentEvents.UseVisualStyleBackColor = true;
            // 
            // tpDanceForms
            // 
            this.tpDanceForms.Controls.Add(this.tcDanceForms);
            this.tpDanceForms.Location = new System.Drawing.Point(4, 40);
            this.tpDanceForms.Name = "tpDanceForms";
            this.tpDanceForms.Size = new System.Drawing.Size(261, 474);
            this.tpDanceForms.TabIndex = 3;
            this.tpDanceForms.Text = "Dance Forms";
            this.tpDanceForms.UseVisualStyleBackColor = true;
            // 
            // tbMusicalForms
            // 
            this.tbMusicalForms.Controls.Add(this.tcMusicalForms);
            this.tbMusicalForms.Location = new System.Drawing.Point(4, 40);
            this.tbMusicalForms.Name = "tbMusicalForms";
            this.tbMusicalForms.Size = new System.Drawing.Size(261, 474);
            this.tbMusicalForms.TabIndex = 4;
            this.tbMusicalForms.Text = "Musical Forms";
            this.tbMusicalForms.UseVisualStyleBackColor = true;
            // 
            // tpPoeticForms
            // 
            this.tpPoeticForms.Controls.Add(this.tcPoeticForms);
            this.tpPoeticForms.Location = new System.Drawing.Point(4, 40);
            this.tpPoeticForms.Name = "tpPoeticForms";
            this.tpPoeticForms.Size = new System.Drawing.Size(261, 474);
            this.tpPoeticForms.TabIndex = 5;
            this.tpPoeticForms.Text = "Poetic Forms";
            this.tpPoeticForms.UseVisualStyleBackColor = true;
            // 
            // tcDanceForms
            // 
            this.tcDanceForms.Controls.Add(this.tpDanceFormsSearch);
            this.tcDanceForms.Controls.Add(this.tpDanceFormsEvents);
            this.tcDanceForms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDanceForms.Location = new System.Drawing.Point(0, 0);
            this.tcDanceForms.Name = "tcDanceForms";
            this.tcDanceForms.SelectedIndex = 0;
            this.tcDanceForms.Size = new System.Drawing.Size(261, 474);
            this.tcDanceForms.TabIndex = 6;
            // 
            // tpDanceFormsSearch
            // 
            this.tpDanceFormsSearch.Controls.Add(this.lblDanceFormsResults);
            this.tpDanceFormsSearch.Controls.Add(this.listDanceFormsSearch);
            this.tpDanceFormsSearch.Controls.Add(this.btnDanceFormsSearch);
            this.tpDanceFormsSearch.Controls.Add(this.groupBox27);
            this.tpDanceFormsSearch.Controls.Add(this.txtDanceFormsSearch);
            this.tpDanceFormsSearch.Location = new System.Drawing.Point(4, 22);
            this.tpDanceFormsSearch.Name = "tpDanceFormsSearch";
            this.tpDanceFormsSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tpDanceFormsSearch.Size = new System.Drawing.Size(253, 448);
            this.tpDanceFormsSearch.TabIndex = 0;
            this.tpDanceFormsSearch.Text = "Search";
            this.tpDanceFormsSearch.UseVisualStyleBackColor = true;
            // 
            // lblDanceFormsResults
            // 
            this.lblDanceFormsResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDanceFormsResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDanceFormsResults.Location = new System.Drawing.Point(167, 261);
            this.lblDanceFormsResults.Name = "lblDanceFormsResults";
            this.lblDanceFormsResults.Size = new System.Drawing.Size(80, 11);
            this.lblDanceFormsResults.TabIndex = 56;
            this.lblDanceFormsResults.Text = "0 / 0";
            this.lblDanceFormsResults.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.hint.SetToolTip(this.lblDanceFormsResults, "Results Shown");
            // 
            // listDanceFormsSearch
            // 
            this.listDanceFormsSearch.AllColumns.Add(this.olvColumn5);
            this.listDanceFormsSearch.AlternateRowBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listDanceFormsSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listDanceFormsSearch.CellEditUseWholeCell = false;
            this.listDanceFormsSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn5});
            this.listDanceFormsSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.listDanceFormsSearch.FullRowSelect = true;
            this.listDanceFormsSearch.GridLines = true;
            this.listDanceFormsSearch.HeaderWordWrap = true;
            this.listDanceFormsSearch.Location = new System.Drawing.Point(3, 31);
            this.listDanceFormsSearch.Name = "listDanceFormsSearch";
            this.listDanceFormsSearch.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.listDanceFormsSearch.ShowCommandMenuOnRightClick = true;
            this.listDanceFormsSearch.ShowImagesOnSubItems = true;
            this.listDanceFormsSearch.ShowItemCountOnGroups = true;
            this.listDanceFormsSearch.Size = new System.Drawing.Size(247, 227);
            this.listDanceFormsSearch.TabIndex = 55;
            this.listDanceFormsSearch.UseAlternatingBackColors = true;
            this.listDanceFormsSearch.UseCompatibleStateImageBehavior = false;
            this.listDanceFormsSearch.UseFiltering = true;
            this.listDanceFormsSearch.UseHotItem = true;
            this.listDanceFormsSearch.UseHyperlinks = true;
            this.listDanceFormsSearch.View = System.Windows.Forms.View.Details;
            this.listDanceFormsSearch.SelectedIndexChanged += new System.EventHandler(this.listDanceFormsSearch_SelectedIndexChanged);
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "Name";
            this.olvColumn5.IsEditable = false;
            this.olvColumn5.MinimumWidth = 50;
            this.olvColumn5.Text = "Name";
            this.olvColumn5.UseInitialLetterForGroup = true;
            this.olvColumn5.Width = 220;
            // 
            // btnDanceFormsSearch
            // 
            this.btnDanceFormsSearch.Location = new System.Drawing.Point(3, 3);
            this.btnDanceFormsSearch.Name = "btnDanceFormsSearch";
            this.btnDanceFormsSearch.Size = new System.Drawing.Size(75, 23);
            this.btnDanceFormsSearch.TabIndex = 46;
            this.btnDanceFormsSearch.Text = "Search";
            this.btnDanceFormsSearch.UseVisualStyleBackColor = true;
            this.btnDanceFormsSearch.Click += new System.EventHandler(this.SearchDanceFormsList);
            // 
            // groupBox27
            // 
            this.groupBox27.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox27.Controls.Add(this.groupBox28);
            this.groupBox27.Location = new System.Drawing.Point(3, 278);
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
            this.groupBox28.Controls.Add(this.radDanceFormsFiltered);
            this.groupBox28.Controls.Add(this.radDanceFormsNone);
            this.groupBox28.Controls.Add(this.radDanceFormsEvents);
            this.groupBox28.Location = new System.Drawing.Point(133, 19);
            this.groupBox28.Name = "groupBox28";
            this.groupBox28.Size = new System.Drawing.Size(108, 126);
            this.groupBox28.TabIndex = 15;
            this.groupBox28.TabStop = false;
            this.groupBox28.Text = "Sort By";
            // 
            // radDanceFormsFiltered
            // 
            this.radDanceFormsFiltered.AutoSize = true;
            this.radDanceFormsFiltered.Location = new System.Drawing.Point(6, 42);
            this.radDanceFormsFiltered.Name = "radDanceFormsFiltered";
            this.radDanceFormsFiltered.Size = new System.Drawing.Size(95, 17);
            this.radDanceFormsFiltered.TabIndex = 16;
            this.radDanceFormsFiltered.TabStop = true;
            this.radDanceFormsFiltered.Text = "Filtered Events";
            this.radDanceFormsFiltered.UseVisualStyleBackColor = true;
            this.radDanceFormsFiltered.CheckedChanged += new System.EventHandler(this.SearchDanceFormsList);
            // 
            // radDanceFormsNone
            // 
            this.radDanceFormsNone.AutoSize = true;
            this.radDanceFormsNone.Checked = true;
            this.radDanceFormsNone.Location = new System.Drawing.Point(6, 65);
            this.radDanceFormsNone.Name = "radDanceFormsNone";
            this.radDanceFormsNone.Size = new System.Drawing.Size(51, 17);
            this.radDanceFormsNone.TabIndex = 14;
            this.radDanceFormsNone.TabStop = true;
            this.radDanceFormsNone.Text = "None";
            this.radDanceFormsNone.UseVisualStyleBackColor = true;
            this.radDanceFormsNone.CheckedChanged += new System.EventHandler(this.SearchDanceFormsList);
            // 
            // radDanceFormsEvents
            // 
            this.radDanceFormsEvents.AutoSize = true;
            this.radDanceFormsEvents.Location = new System.Drawing.Point(6, 19);
            this.radDanceFormsEvents.Name = "radDanceFormsEvents";
            this.radDanceFormsEvents.Size = new System.Drawing.Size(58, 17);
            this.radDanceFormsEvents.TabIndex = 13;
            this.radDanceFormsEvents.Text = "Events";
            this.radDanceFormsEvents.UseVisualStyleBackColor = true;
            this.radDanceFormsEvents.CheckedChanged += new System.EventHandler(this.SearchDanceFormsList);
            // 
            // txtDanceFormsSearch
            // 
            this.txtDanceFormsSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDanceFormsSearch.Location = new System.Drawing.Point(81, 5);
            this.txtDanceFormsSearch.Name = "txtDanceFormsSearch";
            this.txtDanceFormsSearch.Size = new System.Drawing.Size(169, 20);
            this.txtDanceFormsSearch.TabIndex = 44;
            this.txtDanceFormsSearch.TextChanged += new System.EventHandler(this.SearchDanceFormsList);
            // 
            // tpDanceFormsEvents
            // 
            this.tpDanceFormsEvents.Location = new System.Drawing.Point(4, 22);
            this.tpDanceFormsEvents.Name = "tpDanceFormsEvents";
            this.tpDanceFormsEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tpDanceFormsEvents.Size = new System.Drawing.Size(253, 448);
            this.tpDanceFormsEvents.TabIndex = 1;
            this.tpDanceFormsEvents.Text = "Events";
            this.tpDanceFormsEvents.UseVisualStyleBackColor = true;
            // 
            // tcMusicalForms
            // 
            this.tcMusicalForms.Controls.Add(this.tpMusicalFormsSearch);
            this.tcMusicalForms.Controls.Add(this.tpMusicalFormsEvents);
            this.tcMusicalForms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMusicalForms.Location = new System.Drawing.Point(0, 0);
            this.tcMusicalForms.Name = "tcMusicalForms";
            this.tcMusicalForms.SelectedIndex = 0;
            this.tcMusicalForms.Size = new System.Drawing.Size(261, 474);
            this.tcMusicalForms.TabIndex = 6;
            // 
            // tpMusicalFormsSearch
            // 
            this.tpMusicalFormsSearch.Controls.Add(this.lblMusicalFormsResults);
            this.tpMusicalFormsSearch.Controls.Add(this.listMusicalFormsSearch);
            this.tpMusicalFormsSearch.Controls.Add(this.btnMusicalFormsSearch);
            this.tpMusicalFormsSearch.Controls.Add(this.groupBox1);
            this.tpMusicalFormsSearch.Controls.Add(this.txtMusicalFormsSearch);
            this.tpMusicalFormsSearch.Location = new System.Drawing.Point(4, 22);
            this.tpMusicalFormsSearch.Name = "tpMusicalFormsSearch";
            this.tpMusicalFormsSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tpMusicalFormsSearch.Size = new System.Drawing.Size(253, 448);
            this.tpMusicalFormsSearch.TabIndex = 0;
            this.tpMusicalFormsSearch.Text = "Search";
            this.tpMusicalFormsSearch.UseVisualStyleBackColor = true;
            // 
            // lblMusicalFormsResults
            // 
            this.lblMusicalFormsResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMusicalFormsResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMusicalFormsResults.Location = new System.Drawing.Point(167, 261);
            this.lblMusicalFormsResults.Name = "lblMusicalFormsResults";
            this.lblMusicalFormsResults.Size = new System.Drawing.Size(80, 11);
            this.lblMusicalFormsResults.TabIndex = 56;
            this.lblMusicalFormsResults.Text = "0 / 0";
            this.lblMusicalFormsResults.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.hint.SetToolTip(this.lblMusicalFormsResults, "Results Shown");
            // 
            // listMusicalFormsSearch
            // 
            this.listMusicalFormsSearch.AllColumns.Add(this.olvColumn3);
            this.listMusicalFormsSearch.AlternateRowBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listMusicalFormsSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listMusicalFormsSearch.CellEditUseWholeCell = false;
            this.listMusicalFormsSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn3});
            this.listMusicalFormsSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.listMusicalFormsSearch.FullRowSelect = true;
            this.listMusicalFormsSearch.GridLines = true;
            this.listMusicalFormsSearch.HeaderWordWrap = true;
            this.listMusicalFormsSearch.Location = new System.Drawing.Point(3, 31);
            this.listMusicalFormsSearch.Name = "listMusicalFormsSearch";
            this.listMusicalFormsSearch.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.listMusicalFormsSearch.ShowCommandMenuOnRightClick = true;
            this.listMusicalFormsSearch.ShowImagesOnSubItems = true;
            this.listMusicalFormsSearch.ShowItemCountOnGroups = true;
            this.listMusicalFormsSearch.Size = new System.Drawing.Size(247, 227);
            this.listMusicalFormsSearch.TabIndex = 55;
            this.listMusicalFormsSearch.UseAlternatingBackColors = true;
            this.listMusicalFormsSearch.UseCompatibleStateImageBehavior = false;
            this.listMusicalFormsSearch.UseFiltering = true;
            this.listMusicalFormsSearch.UseHotItem = true;
            this.listMusicalFormsSearch.UseHyperlinks = true;
            this.listMusicalFormsSearch.View = System.Windows.Forms.View.Details;
            this.listMusicalFormsSearch.SelectedIndexChanged += new System.EventHandler(this.listMusicalFormsSearch_SelectedIndexChanged);
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Name";
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.MinimumWidth = 50;
            this.olvColumn3.Text = "Name";
            this.olvColumn3.UseInitialLetterForGroup = true;
            this.olvColumn3.Width = 220;
            // 
            // btnMusicalFormsSearch
            // 
            this.btnMusicalFormsSearch.Location = new System.Drawing.Point(3, 3);
            this.btnMusicalFormsSearch.Name = "btnMusicalFormsSearch";
            this.btnMusicalFormsSearch.Size = new System.Drawing.Size(75, 23);
            this.btnMusicalFormsSearch.TabIndex = 46;
            this.btnMusicalFormsSearch.Text = "Search";
            this.btnMusicalFormsSearch.UseVisualStyleBackColor = true;
            this.btnMusicalFormsSearch.Click += new System.EventHandler(this.SearchMusicalFormsList);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(3, 278);
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
            this.groupBox2.Controls.Add(this.radMusicalFormsFiltered);
            this.groupBox2.Controls.Add(this.radMusicalFormsNone);
            this.groupBox2.Controls.Add(this.radMusicalFormsEvents);
            this.groupBox2.Location = new System.Drawing.Point(133, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(108, 126);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sort By";
            // 
            // radMusicalFormsFiltered
            // 
            this.radMusicalFormsFiltered.AutoSize = true;
            this.radMusicalFormsFiltered.Location = new System.Drawing.Point(6, 42);
            this.radMusicalFormsFiltered.Name = "radMusicalFormsFiltered";
            this.radMusicalFormsFiltered.Size = new System.Drawing.Size(95, 17);
            this.radMusicalFormsFiltered.TabIndex = 16;
            this.radMusicalFormsFiltered.TabStop = true;
            this.radMusicalFormsFiltered.Text = "Filtered Events";
            this.radMusicalFormsFiltered.UseVisualStyleBackColor = true;
            this.radMusicalFormsFiltered.CheckedChanged += new System.EventHandler(this.SearchMusicalFormsList);
            // 
            // radMusicalFormsNone
            // 
            this.radMusicalFormsNone.AutoSize = true;
            this.radMusicalFormsNone.Checked = true;
            this.radMusicalFormsNone.Location = new System.Drawing.Point(6, 65);
            this.radMusicalFormsNone.Name = "radMusicalFormsNone";
            this.radMusicalFormsNone.Size = new System.Drawing.Size(51, 17);
            this.radMusicalFormsNone.TabIndex = 14;
            this.radMusicalFormsNone.TabStop = true;
            this.radMusicalFormsNone.Text = "None";
            this.radMusicalFormsNone.UseVisualStyleBackColor = true;
            this.radMusicalFormsNone.CheckedChanged += new System.EventHandler(this.SearchMusicalFormsList);
            // 
            // radMusicalFormsEvents
            // 
            this.radMusicalFormsEvents.AutoSize = true;
            this.radMusicalFormsEvents.Location = new System.Drawing.Point(6, 19);
            this.radMusicalFormsEvents.Name = "radMusicalFormsEvents";
            this.radMusicalFormsEvents.Size = new System.Drawing.Size(58, 17);
            this.radMusicalFormsEvents.TabIndex = 13;
            this.radMusicalFormsEvents.Text = "Events";
            this.radMusicalFormsEvents.UseVisualStyleBackColor = true;
            this.radMusicalFormsEvents.CheckedChanged += new System.EventHandler(this.SearchMusicalFormsList);
            // 
            // txtMusicalFormsSearch
            // 
            this.txtMusicalFormsSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMusicalFormsSearch.Location = new System.Drawing.Point(81, 5);
            this.txtMusicalFormsSearch.Name = "txtMusicalFormsSearch";
            this.txtMusicalFormsSearch.Size = new System.Drawing.Size(169, 20);
            this.txtMusicalFormsSearch.TabIndex = 44;
            this.txtMusicalFormsSearch.TextChanged += new System.EventHandler(this.SearchMusicalFormsList);
            // 
            // tpMusicalFormsEvents
            // 
            this.tpMusicalFormsEvents.Location = new System.Drawing.Point(4, 22);
            this.tpMusicalFormsEvents.Name = "tpMusicalFormsEvents";
            this.tpMusicalFormsEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tpMusicalFormsEvents.Size = new System.Drawing.Size(253, 448);
            this.tpMusicalFormsEvents.TabIndex = 1;
            this.tpMusicalFormsEvents.Text = "Events";
            this.tpMusicalFormsEvents.UseVisualStyleBackColor = true;
            // 
            // tcPoeticForms
            // 
            this.tcPoeticForms.Controls.Add(this.tpPoeticFormsSearch);
            this.tcPoeticForms.Controls.Add(this.tpPoeticFormsEvents);
            this.tcPoeticForms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcPoeticForms.Location = new System.Drawing.Point(0, 0);
            this.tcPoeticForms.Name = "tcPoeticForms";
            this.tcPoeticForms.SelectedIndex = 0;
            this.tcPoeticForms.Size = new System.Drawing.Size(261, 474);
            this.tcPoeticForms.TabIndex = 6;
            // 
            // tpPoeticFormsSearch
            // 
            this.tpPoeticFormsSearch.Controls.Add(this.lblPoeticFormsResults);
            this.tpPoeticFormsSearch.Controls.Add(this.listPoeticFormsSearch);
            this.tpPoeticFormsSearch.Controls.Add(this.btnPoeticFormsSearch);
            this.tpPoeticFormsSearch.Controls.Add(this.groupBox3);
            this.tpPoeticFormsSearch.Controls.Add(this.txtPoeticFormsSearch);
            this.tpPoeticFormsSearch.Location = new System.Drawing.Point(4, 22);
            this.tpPoeticFormsSearch.Name = "tpPoeticFormsSearch";
            this.tpPoeticFormsSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tpPoeticFormsSearch.Size = new System.Drawing.Size(253, 448);
            this.tpPoeticFormsSearch.TabIndex = 0;
            this.tpPoeticFormsSearch.Text = "Search";
            this.tpPoeticFormsSearch.UseVisualStyleBackColor = true;
            // 
            // lblPoeticFormsResults
            // 
            this.lblPoeticFormsResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPoeticFormsResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoeticFormsResults.Location = new System.Drawing.Point(167, 261);
            this.lblPoeticFormsResults.Name = "lblPoeticFormsResults";
            this.lblPoeticFormsResults.Size = new System.Drawing.Size(80, 11);
            this.lblPoeticFormsResults.TabIndex = 56;
            this.lblPoeticFormsResults.Text = "0 / 0";
            this.lblPoeticFormsResults.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.hint.SetToolTip(this.lblPoeticFormsResults, "Results Shown");
            // 
            // listPoeticFormsSearch
            // 
            this.listPoeticFormsSearch.AllColumns.Add(this.olvColumn4);
            this.listPoeticFormsSearch.AlternateRowBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listPoeticFormsSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listPoeticFormsSearch.CellEditUseWholeCell = false;
            this.listPoeticFormsSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn4});
            this.listPoeticFormsSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.listPoeticFormsSearch.FullRowSelect = true;
            this.listPoeticFormsSearch.GridLines = true;
            this.listPoeticFormsSearch.HeaderWordWrap = true;
            this.listPoeticFormsSearch.Location = new System.Drawing.Point(3, 31);
            this.listPoeticFormsSearch.Name = "listPoeticFormsSearch";
            this.listPoeticFormsSearch.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.listPoeticFormsSearch.ShowCommandMenuOnRightClick = true;
            this.listPoeticFormsSearch.ShowImagesOnSubItems = true;
            this.listPoeticFormsSearch.ShowItemCountOnGroups = true;
            this.listPoeticFormsSearch.Size = new System.Drawing.Size(247, 227);
            this.listPoeticFormsSearch.TabIndex = 55;
            this.listPoeticFormsSearch.UseAlternatingBackColors = true;
            this.listPoeticFormsSearch.UseCompatibleStateImageBehavior = false;
            this.listPoeticFormsSearch.UseFiltering = true;
            this.listPoeticFormsSearch.UseHotItem = true;
            this.listPoeticFormsSearch.UseHyperlinks = true;
            this.listPoeticFormsSearch.View = System.Windows.Forms.View.Details;
            this.listPoeticFormsSearch.SelectedIndexChanged += new System.EventHandler(this.listPoeticFormsSearch_SelectedIndexChanged);
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "Name";
            this.olvColumn4.IsEditable = false;
            this.olvColumn4.MinimumWidth = 50;
            this.olvColumn4.Text = "Name";
            this.olvColumn4.UseInitialLetterForGroup = true;
            this.olvColumn4.Width = 220;
            // 
            // btnPoeticFormsSearch
            // 
            this.btnPoeticFormsSearch.Location = new System.Drawing.Point(3, 3);
            this.btnPoeticFormsSearch.Name = "btnPoeticFormsSearch";
            this.btnPoeticFormsSearch.Size = new System.Drawing.Size(75, 23);
            this.btnPoeticFormsSearch.TabIndex = 46;
            this.btnPoeticFormsSearch.Text = "Search";
            this.btnPoeticFormsSearch.UseVisualStyleBackColor = true;
            this.btnPoeticFormsSearch.Click += new System.EventHandler(this.SearchPoeticFormsList);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Location = new System.Drawing.Point(3, 278);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(247, 164);
            this.groupBox3.TabIndex = 45;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filter / Sort";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.radPoeticFormsFiltered);
            this.groupBox4.Controls.Add(this.radPoeticFormsNone);
            this.groupBox4.Controls.Add(this.radPoeticFormsEvents);
            this.groupBox4.Location = new System.Drawing.Point(133, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(108, 126);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Sort By";
            // 
            // radPoeticFormsFiltered
            // 
            this.radPoeticFormsFiltered.AutoSize = true;
            this.radPoeticFormsFiltered.Location = new System.Drawing.Point(6, 42);
            this.radPoeticFormsFiltered.Name = "radPoeticFormsFiltered";
            this.radPoeticFormsFiltered.Size = new System.Drawing.Size(95, 17);
            this.radPoeticFormsFiltered.TabIndex = 16;
            this.radPoeticFormsFiltered.TabStop = true;
            this.radPoeticFormsFiltered.Text = "Filtered Events";
            this.radPoeticFormsFiltered.UseVisualStyleBackColor = true;
            this.radPoeticFormsFiltered.CheckedChanged += new System.EventHandler(this.SearchPoeticFormsList);
            // 
            // radPoeticFormsNone
            // 
            this.radPoeticFormsNone.AutoSize = true;
            this.radPoeticFormsNone.Checked = true;
            this.radPoeticFormsNone.Location = new System.Drawing.Point(6, 65);
            this.radPoeticFormsNone.Name = "radPoeticFormsNone";
            this.radPoeticFormsNone.Size = new System.Drawing.Size(51, 17);
            this.radPoeticFormsNone.TabIndex = 14;
            this.radPoeticFormsNone.TabStop = true;
            this.radPoeticFormsNone.Text = "None";
            this.radPoeticFormsNone.UseVisualStyleBackColor = true;
            this.radPoeticFormsNone.CheckedChanged += new System.EventHandler(this.SearchPoeticFormsList);
            // 
            // radPoeticFormsEvents
            // 
            this.radPoeticFormsEvents.AutoSize = true;
            this.radPoeticFormsEvents.Location = new System.Drawing.Point(6, 19);
            this.radPoeticFormsEvents.Name = "radPoeticFormsEvents";
            this.radPoeticFormsEvents.Size = new System.Drawing.Size(58, 17);
            this.radPoeticFormsEvents.TabIndex = 13;
            this.radPoeticFormsEvents.Text = "Events";
            this.radPoeticFormsEvents.UseVisualStyleBackColor = true;
            this.radPoeticFormsEvents.CheckedChanged += new System.EventHandler(this.SearchPoeticFormsList);
            // 
            // txtPoeticFormsSearch
            // 
            this.txtPoeticFormsSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPoeticFormsSearch.Location = new System.Drawing.Point(81, 5);
            this.txtPoeticFormsSearch.Name = "txtPoeticFormsSearch";
            this.txtPoeticFormsSearch.Size = new System.Drawing.Size(169, 20);
            this.txtPoeticFormsSearch.TabIndex = 44;
            this.txtPoeticFormsSearch.TextChanged += new System.EventHandler(this.SearchPoeticFormsList);
            // 
            // tpPoeticFormsEvents
            // 
            this.tpPoeticFormsEvents.Location = new System.Drawing.Point(4, 22);
            this.tpPoeticFormsEvents.Name = "tpPoeticFormsEvents";
            this.tpPoeticFormsEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tpPoeticFormsEvents.Size = new System.Drawing.Size(253, 448);
            this.tpPoeticFormsEvents.TabIndex = 1;
            this.tpPoeticFormsEvents.Text = "Events";
            this.tpPoeticFormsEvents.UseVisualStyleBackColor = true;
            // 
            // ArtAndCraftsTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcArtAndCrafts);
            this.Name = "ArtAndCraftsTab";
            this.tcArtAndCrafts.ResumeLayout(false);
            this.tpArtifacts.ResumeLayout(false);
            this.tcArtifacts.ResumeLayout(false);
            this.tpArtifactsSearch.ResumeLayout(false);
            this.tpArtifactsSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listArtifactSearch)).EndInit();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            this.tpWrittenContent.ResumeLayout(false);
            this.tcWrittenContent.ResumeLayout(false);
            this.tpWrittenContentSearch.ResumeLayout(false);
            this.tpWrittenContentSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listWrittenContentSearch)).EndInit();
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            this.tpDanceForms.ResumeLayout(false);
            this.tbMusicalForms.ResumeLayout(false);
            this.tpPoeticForms.ResumeLayout(false);
            this.tcDanceForms.ResumeLayout(false);
            this.tpDanceFormsSearch.ResumeLayout(false);
            this.tpDanceFormsSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listDanceFormsSearch)).EndInit();
            this.groupBox27.ResumeLayout(false);
            this.groupBox28.ResumeLayout(false);
            this.groupBox28.PerformLayout();
            this.tcMusicalForms.ResumeLayout(false);
            this.tpMusicalFormsSearch.ResumeLayout(false);
            this.tpMusicalFormsSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listMusicalFormsSearch)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tcPoeticForms.ResumeLayout(false);
            this.tpPoeticFormsSearch.ResumeLayout(false);
            this.tpPoeticFormsSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listPoeticFormsSearch)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tcArtAndCrafts;
        private TabPage tpArtifacts;
        private TabControl tcArtifacts;
        private TabPage tpArtifactsSearch;
        private Label lblArtifactResults;
        private ObjectListView listArtifactSearch;
        private OLVColumn olvName;
        private OLVColumn olvType;
        private Button btnArtifactListReset;
        private Label lblArtifactList;
        private Button btnArtifactSearch;
        private GroupBox groupBox19;
        private Label lblArtMatFilter;
        private ComboBox cbmArtMatFilter;
        private Label lblArtTypeFilter;
        private ComboBox cbmArtTypeFilter;
        private GroupBox groupBox20;
        private RadioButton radArtifactSortFiltered;
        private RadioButton radArtifactSortNone;
        private RadioButton radArtifactSortEvents;
        private TextBox txtArtifactSearch;
        private TabPage tpArtifactsEvents;
        private TabPage tpWrittenContent;
        private TabControl tcWrittenContent;
        private TabPage tpWrittenContentSearch;
        private Label lblWrittenContentResults;
        private ObjectListView listWrittenContentSearch;
        private OLVColumn olvColumn1;
        private OLVColumn olvColumn2;
        private Button btnWrittenContentSearch;
        private GroupBox groupBox21;
        private Label label8;
        private ComboBox cmbWrittenContentType;
        private GroupBox groupBox22;
        private RadioButton radWrittenContentSortFiltered;
        private RadioButton radWrittenContentSortNone;
        private RadioButton radWrittenContentSortEvents;
        private TextBox txtWrittenContentSearch;
        private TabPage tpWrittenContentEvents;
        private TabPage tpDanceForms;
        private TabPage tbMusicalForms;
        private TabPage tpPoeticForms;
        private TabControl tcDanceForms;
        private TabPage tpDanceFormsSearch;
        private Label lblDanceFormsResults;
        private ObjectListView listDanceFormsSearch;
        private OLVColumn olvColumn5;
        private Button btnDanceFormsSearch;
        private GroupBox groupBox27;
        private GroupBox groupBox28;
        private RadioButton radDanceFormsFiltered;
        private RadioButton radDanceFormsNone;
        private RadioButton radDanceFormsEvents;
        private TextBox txtDanceFormsSearch;
        private TabPage tpDanceFormsEvents;
        private TabControl tcMusicalForms;
        private TabPage tpMusicalFormsSearch;
        private Label lblMusicalFormsResults;
        private ObjectListView listMusicalFormsSearch;
        private OLVColumn olvColumn3;
        private Button btnMusicalFormsSearch;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private RadioButton radMusicalFormsFiltered;
        private RadioButton radMusicalFormsNone;
        private RadioButton radMusicalFormsEvents;
        private TextBox txtMusicalFormsSearch;
        private TabPage tpMusicalFormsEvents;
        private TabControl tcPoeticForms;
        private TabPage tpPoeticFormsSearch;
        private Label lblPoeticFormsResults;
        private ObjectListView listPoeticFormsSearch;
        private OLVColumn olvColumn4;
        private Button btnPoeticFormsSearch;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private RadioButton radPoeticFormsFiltered;
        private RadioButton radPoeticFormsNone;
        private RadioButton radPoeticFormsEvents;
        private TextBox txtPoeticFormsSearch;
        private TabPage tpPoeticFormsEvents;
    }
}
