namespace LegendsViewer.Controls.Tabs
{
    partial class ArtAndCraftsTab
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
            this.SuspendLayout();
            // 
            // tcArtAndCrafts
            // 
            this.tcArtAndCrafts.Controls.Add(this.tpArtifacts);
            this.tcArtAndCrafts.Controls.Add(this.tpWrittenContent);
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
            this.tpArtifacts.Location = new System.Drawing.Point(4, 22);
            this.tpArtifacts.Name = "tpArtifacts";
            this.tpArtifacts.Size = new System.Drawing.Size(261, 492);
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
            this.tcArtifacts.Size = new System.Drawing.Size(261, 492);
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
            this.tpArtifactsSearch.Size = new System.Drawing.Size(253, 466);
            this.tpArtifactsSearch.TabIndex = 0;
            this.tpArtifactsSearch.Text = "Search";
            this.tpArtifactsSearch.UseVisualStyleBackColor = true;
            // 
            // lblArtifactResults
            // 
            this.lblArtifactResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblArtifactResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArtifactResults.Location = new System.Drawing.Point(155, 318);
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
            this.listArtifactSearch.Size = new System.Drawing.Size(247, 285);
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
            this.btnArtifactListReset.Location = new System.Drawing.Point(3, 323);
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
            this.lblArtifactList.Location = new System.Drawing.Point(58, 327);
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
            this.groupBox19.Location = new System.Drawing.Point(3, 349);
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
            this.cbmArtMatFilter.SelectedIndexChanged += new System.EventHandler(this.searchArtifactList);
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
            this.cbmArtTypeFilter.SelectedIndexChanged += new System.EventHandler(this.searchArtifactList);
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
            this.radArtifactSortFiltered.CheckedChanged += new System.EventHandler(this.searchArtifactList);
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
            this.radArtifactSortNone.CheckedChanged += new System.EventHandler(this.searchArtifactList);
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
            this.radArtifactSortEvents.CheckedChanged += new System.EventHandler(this.searchArtifactList);
            // 
            // txtArtifactSearch
            // 
            this.txtArtifactSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtArtifactSearch.Location = new System.Drawing.Point(81, 5);
            this.txtArtifactSearch.Name = "txtArtifactSearch";
            this.txtArtifactSearch.Size = new System.Drawing.Size(169, 20);
            this.txtArtifactSearch.TabIndex = 44;
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
            this.tpWrittenContent.Location = new System.Drawing.Point(4, 22);
            this.tpWrittenContent.Name = "tpWrittenContent";
            this.tpWrittenContent.Size = new System.Drawing.Size(261, 492);
            this.tpWrittenContent.TabIndex = 2;
            this.tpWrittenContent.Text = "Books";
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
            this.tcWrittenContent.Size = new System.Drawing.Size(261, 492);
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
            this.tpWrittenContentSearch.Size = new System.Drawing.Size(253, 466);
            this.tpWrittenContentSearch.TabIndex = 0;
            this.tpWrittenContentSearch.Text = "Search";
            this.tpWrittenContentSearch.UseVisualStyleBackColor = true;
            // 
            // lblWrittenContentResults
            // 
            this.lblWrittenContentResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWrittenContentResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWrittenContentResults.Location = new System.Drawing.Point(155, 283);
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
            this.listWrittenContentSearch.Size = new System.Drawing.Size(247, 251);
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
            // 
            // groupBox21
            // 
            this.groupBox21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox21.Controls.Add(this.label8);
            this.groupBox21.Controls.Add(this.cmbWrittenContentType);
            this.groupBox21.Controls.Add(this.groupBox22);
            this.groupBox21.Location = new System.Drawing.Point(3, 296);
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
            this.cmbWrittenContentType.SelectedIndexChanged += new System.EventHandler(this.searchWrittenContentList);
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
            this.radWrittenContentSortFiltered.CheckedChanged += new System.EventHandler(this.searchWrittenContentList);
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
            this.radWrittenContentSortNone.CheckedChanged += new System.EventHandler(this.searchWrittenContentList);
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
            this.radWrittenContentSortEvents.CheckedChanged += new System.EventHandler(this.searchWrittenContentList);
            // 
            // txtWrittenContentSearch
            // 
            this.txtWrittenContentSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWrittenContentSearch.Location = new System.Drawing.Point(81, 5);
            this.txtWrittenContentSearch.Name = "txtWrittenContentSearch";
            this.txtWrittenContentSearch.Size = new System.Drawing.Size(169, 20);
            this.txtWrittenContentSearch.TabIndex = 44;
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcArtAndCrafts;
        private System.Windows.Forms.TabPage tpArtifacts;
        private System.Windows.Forms.TabControl tcArtifacts;
        private System.Windows.Forms.TabPage tpArtifactsSearch;
        private System.Windows.Forms.Label lblArtifactResults;
        private BrightIdeasSoftware.ObjectListView listArtifactSearch;
        private BrightIdeasSoftware.OLVColumn olvName;
        private BrightIdeasSoftware.OLVColumn olvType;
        private System.Windows.Forms.Button btnArtifactListReset;
        private System.Windows.Forms.Label lblArtifactList;
        private System.Windows.Forms.Button btnArtifactSearch;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.Label lblArtMatFilter;
        private System.Windows.Forms.ComboBox cbmArtMatFilter;
        private System.Windows.Forms.Label lblArtTypeFilter;
        private System.Windows.Forms.ComboBox cbmArtTypeFilter;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.RadioButton radArtifactSortFiltered;
        private System.Windows.Forms.RadioButton radArtifactSortNone;
        private System.Windows.Forms.RadioButton radArtifactSortEvents;
        private System.Windows.Forms.TextBox txtArtifactSearch;
        private System.Windows.Forms.TabPage tpArtifactsEvents;
        private System.Windows.Forms.TabPage tpWrittenContent;
        private System.Windows.Forms.TabControl tcWrittenContent;
        private System.Windows.Forms.TabPage tpWrittenContentSearch;
        private System.Windows.Forms.Label lblWrittenContentResults;
        private BrightIdeasSoftware.ObjectListView listWrittenContentSearch;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private System.Windows.Forms.Button btnWrittenContentSearch;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbWrittenContentType;
        private System.Windows.Forms.GroupBox groupBox22;
        private System.Windows.Forms.RadioButton radWrittenContentSortFiltered;
        private System.Windows.Forms.RadioButton radWrittenContentSortNone;
        private System.Windows.Forms.RadioButton radWrittenContentSortEvents;
        private System.Windows.Forms.TextBox txtWrittenContentSearch;
        private System.Windows.Forms.TabPage tpWrittenContentEvents;
    }
}
