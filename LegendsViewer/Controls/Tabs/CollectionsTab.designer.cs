namespace LegendsViewer.Controls.Tabs
{
    partial class CollectionsTab
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
            this.tcCollections = new System.Windows.Forms.TabControl();
            this.tpEras = new System.Windows.Forms.TabPage();
            this.tcEras = new System.Windows.Forms.TabControl();
            this.tpEraSearch = new System.Windows.Forms.TabPage();
            this.listEraSearch = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.btnEraShow = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numStart = new System.Windows.Forms.NumericUpDown();
            this.numEraEnd = new System.Windows.Forms.NumericUpDown();
            this.tpEraEvents = new System.Windows.Forms.TabPage();
            this.tpWorldConstructions = new System.Windows.Forms.TabPage();
            this.tcWorldConstructions = new System.Windows.Forms.TabControl();
            this.tpWorldConstructionSearch = new System.Windows.Forms.TabPage();
            this.lblWorldConstructionResult = new System.Windows.Forms.Label();
            this.listWorldConstructionsSearch = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnWorldConstructionsSearch = new System.Windows.Forms.Button();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbConstructionType = new System.Windows.Forms.ComboBox();
            this.groupBox24 = new System.Windows.Forms.GroupBox();
            this.radWorldConstructionsSortFiltered = new System.Windows.Forms.RadioButton();
            this.radWorldConstructionsSortNone = new System.Windows.Forms.RadioButton();
            this.radWorldConstructionsSortEvents = new System.Windows.Forms.RadioButton();
            this.txtWorldConstructionsSearch = new System.Windows.Forms.TextBox();
            this.tpWorldConstructionEvents = new System.Windows.Forms.TabPage();
            this.tpStructures = new System.Windows.Forms.TabPage();
            this.tcStructures = new System.Windows.Forms.TabControl();
            this.tpStructureSearch = new System.Windows.Forms.TabPage();
            this.lblStructureResults = new System.Windows.Forms.Label();
            this.listStructureSearch = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnStructuresSearch = new System.Windows.Forms.Button();
            this.groupBox25 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbStructureType = new System.Windows.Forms.ComboBox();
            this.groupBox26 = new System.Windows.Forms.GroupBox();
            this.radStructuresSortFiltered = new System.Windows.Forms.RadioButton();
            this.radStructuresSortNone = new System.Windows.Forms.RadioButton();
            this.radStructuresSortEvents = new System.Windows.Forms.RadioButton();
            this.txtStructuresSearch = new System.Windows.Forms.TextBox();
            this.tpStructureEvents = new System.Windows.Forms.TabPage();
            this.tcCollections.SuspendLayout();
            this.tpEras.SuspendLayout();
            this.tcEras.SuspendLayout();
            this.tpEraSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listEraSearch)).BeginInit();
            this.groupBox16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEraEnd)).BeginInit();
            this.tpWorldConstructions.SuspendLayout();
            this.tcWorldConstructions.SuspendLayout();
            this.tpWorldConstructionSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listWorldConstructionsSearch)).BeginInit();
            this.groupBox23.SuspendLayout();
            this.groupBox24.SuspendLayout();
            this.tpStructures.SuspendLayout();
            this.tcStructures.SuspendLayout();
            this.tpStructureSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listStructureSearch)).BeginInit();
            this.groupBox25.SuspendLayout();
            this.groupBox26.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcCollections
            // 
            this.tcCollections.Controls.Add(this.tpEras);
            this.tcCollections.Controls.Add(this.tpWorldConstructions);
            this.tcCollections.Controls.Add(this.tpStructures);
            this.tcCollections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcCollections.Location = new System.Drawing.Point(0, 0);
            this.tcCollections.Multiline = true;
            this.tcCollections.Name = "tcCollections";
            this.tcCollections.SelectedIndex = 0;
            this.tcCollections.Size = new System.Drawing.Size(269, 518);
            this.tcCollections.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tcCollections.TabIndex = 1;
            // 
            // tpEras
            // 
            this.tpEras.Controls.Add(this.tcEras);
            this.tpEras.Location = new System.Drawing.Point(4, 22);
            this.tpEras.Name = "tpEras";
            this.tpEras.Size = new System.Drawing.Size(261, 492);
            this.tpEras.TabIndex = 0;
            this.tpEras.Text = "Eras";
            this.tpEras.UseVisualStyleBackColor = true;
            // 
            // tcEras
            // 
            this.tcEras.Controls.Add(this.tpEraSearch);
            this.tcEras.Controls.Add(this.tpEraEvents);
            this.tcEras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcEras.Location = new System.Drawing.Point(0, 0);
            this.tcEras.Name = "tcEras";
            this.tcEras.SelectedIndex = 0;
            this.tcEras.Size = new System.Drawing.Size(261, 492);
            this.tcEras.TabIndex = 3;
            // 
            // tpEraSearch
            // 
            this.tpEraSearch.Controls.Add(this.listEraSearch);
            this.tpEraSearch.Controls.Add(this.groupBox16);
            this.tpEraSearch.Location = new System.Drawing.Point(4, 22);
            this.tpEraSearch.Name = "tpEraSearch";
            this.tpEraSearch.Size = new System.Drawing.Size(253, 466);
            this.tpEraSearch.TabIndex = 0;
            this.tpEraSearch.Text = "Search";
            this.tpEraSearch.UseVisualStyleBackColor = true;
            // 
            // listEraSearch
            // 
            this.listEraSearch.AllColumns.Add(this.olvColumn7);
            this.listEraSearch.AllColumns.Add(this.olvColumn8);
            this.listEraSearch.AlternateRowBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listEraSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listEraSearch.CellEditUseWholeCell = false;
            this.listEraSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn7,
            this.olvColumn8});
            this.listEraSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.listEraSearch.FullRowSelect = true;
            this.listEraSearch.GridLines = true;
            this.listEraSearch.HeaderWordWrap = true;
            this.listEraSearch.Location = new System.Drawing.Point(3, 3);
            this.listEraSearch.Name = "listEraSearch";
            this.listEraSearch.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.listEraSearch.ShowCommandMenuOnRightClick = true;
            this.listEraSearch.ShowImagesOnSubItems = true;
            this.listEraSearch.ShowItemCountOnGroups = true;
            this.listEraSearch.Size = new System.Drawing.Size(247, 358);
            this.listEraSearch.TabIndex = 60;
            this.listEraSearch.UseAlternatingBackColors = true;
            this.listEraSearch.UseCompatibleStateImageBehavior = false;
            this.listEraSearch.UseFiltering = true;
            this.listEraSearch.UseHotItem = true;
            this.listEraSearch.UseHyperlinks = true;
            this.listEraSearch.View = System.Windows.Forms.View.Details;
            this.listEraSearch.SelectedIndexChanged += new System.EventHandler(this.listEras_SelectedIndexChanged);
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "Name";
            this.olvColumn7.IsEditable = false;
            this.olvColumn7.MinimumWidth = 50;
            this.olvColumn7.Text = "Name";
            this.olvColumn7.UseInitialLetterForGroup = true;
            this.olvColumn7.Width = 155;
            // 
            // olvColumn8
            // 
            this.olvColumn8.AspectName = "EventCount";
            this.olvColumn8.IsEditable = false;
            this.olvColumn8.Text = "Events";
            this.olvColumn8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn8.Width = 70;
            // 
            // groupBox16
            // 
            this.groupBox16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox16.Controls.Add(this.btnEraShow);
            this.groupBox16.Controls.Add(this.label5);
            this.groupBox16.Controls.Add(this.label4);
            this.groupBox16.Controls.Add(this.label3);
            this.groupBox16.Controls.Add(this.numStart);
            this.groupBox16.Controls.Add(this.numEraEnd);
            this.groupBox16.Location = new System.Drawing.Point(3, 378);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(250, 85);
            this.groupBox16.TabIndex = 45;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Filter / Sort";
            // 
            // btnEraShow
            // 
            this.btnEraShow.Location = new System.Drawing.Point(85, 58);
            this.btnEraShow.Name = "btnEraShow";
            this.btnEraShow.Size = new System.Drawing.Size(75, 23);
            this.btnEraShow.TabIndex = 21;
            this.btnEraShow.Text = "Show";
            this.btnEraShow.UseVisualStyleBackColor = true;
            this.btnEraShow.Click += new System.EventHandler(this.btnEraShow_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(120, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(134, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "End";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Beginning";
            // 
            // numStart
            // 
            this.numStart.Location = new System.Drawing.Point(34, 32);
            this.numStart.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numStart.Name = "numStart";
            this.numStart.Size = new System.Drawing.Size(80, 20);
            this.numStart.TabIndex = 19;
            // 
            // numEraEnd
            // 
            this.numEraEnd.Location = new System.Drawing.Point(136, 32);
            this.numEraEnd.Name = "numEraEnd";
            this.numEraEnd.Size = new System.Drawing.Size(80, 20);
            this.numEraEnd.TabIndex = 20;
            // 
            // tpEraEvents
            // 
            this.tpEraEvents.AutoScroll = true;
            this.tpEraEvents.Location = new System.Drawing.Point(4, 22);
            this.tpEraEvents.Name = "tpEraEvents";
            this.tpEraEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tpEraEvents.Size = new System.Drawing.Size(253, 448);
            this.tpEraEvents.TabIndex = 1;
            this.tpEraEvents.Text = "Events";
            this.tpEraEvents.UseVisualStyleBackColor = true;
            // 
            // tpWorldConstructions
            // 
            this.tpWorldConstructions.Controls.Add(this.tcWorldConstructions);
            this.tpWorldConstructions.Location = new System.Drawing.Point(4, 22);
            this.tpWorldConstructions.Name = "tpWorldConstructions";
            this.tpWorldConstructions.Size = new System.Drawing.Size(261, 492);
            this.tpWorldConstructions.TabIndex = 3;
            this.tpWorldConstructions.Text = "Constructions";
            this.tpWorldConstructions.UseVisualStyleBackColor = true;
            // 
            // tcWorldConstructions
            // 
            this.tcWorldConstructions.Controls.Add(this.tpWorldConstructionSearch);
            this.tcWorldConstructions.Controls.Add(this.tpWorldConstructionEvents);
            this.tcWorldConstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcWorldConstructions.Location = new System.Drawing.Point(0, 0);
            this.tcWorldConstructions.Name = "tcWorldConstructions";
            this.tcWorldConstructions.SelectedIndex = 0;
            this.tcWorldConstructions.Size = new System.Drawing.Size(261, 492);
            this.tcWorldConstructions.TabIndex = 4;
            // 
            // tpWorldConstructionSearch
            // 
            this.tpWorldConstructionSearch.Controls.Add(this.lblWorldConstructionResult);
            this.tpWorldConstructionSearch.Controls.Add(this.listWorldConstructionsSearch);
            this.tpWorldConstructionSearch.Controls.Add(this.btnWorldConstructionsSearch);
            this.tpWorldConstructionSearch.Controls.Add(this.groupBox23);
            this.tpWorldConstructionSearch.Controls.Add(this.txtWorldConstructionsSearch);
            this.tpWorldConstructionSearch.Location = new System.Drawing.Point(4, 22);
            this.tpWorldConstructionSearch.Name = "tpWorldConstructionSearch";
            this.tpWorldConstructionSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tpWorldConstructionSearch.Size = new System.Drawing.Size(253, 466);
            this.tpWorldConstructionSearch.TabIndex = 0;
            this.tpWorldConstructionSearch.Text = "Search";
            this.tpWorldConstructionSearch.UseVisualStyleBackColor = true;
            // 
            // lblWorldConstructionResult
            // 
            this.lblWorldConstructionResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWorldConstructionResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorldConstructionResult.Location = new System.Drawing.Point(155, 287);
            this.lblWorldConstructionResult.Name = "lblWorldConstructionResult";
            this.lblWorldConstructionResult.Size = new System.Drawing.Size(95, 10);
            this.lblWorldConstructionResult.TabIndex = 59;
            this.lblWorldConstructionResult.Text = "0 / 0";
            this.lblWorldConstructionResult.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.hint.SetToolTip(this.lblWorldConstructionResult, "Results Shown");
            // 
            // listWorldConstructionsSearch
            // 
            this.listWorldConstructionsSearch.AllColumns.Add(this.olvColumn5);
            this.listWorldConstructionsSearch.AllColumns.Add(this.olvColumn6);
            this.listWorldConstructionsSearch.AlternateRowBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listWorldConstructionsSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listWorldConstructionsSearch.CellEditUseWholeCell = false;
            this.listWorldConstructionsSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn5,
            this.olvColumn6});
            this.listWorldConstructionsSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.listWorldConstructionsSearch.FullRowSelect = true;
            this.listWorldConstructionsSearch.GridLines = true;
            this.listWorldConstructionsSearch.HeaderWordWrap = true;
            this.listWorldConstructionsSearch.Location = new System.Drawing.Point(3, 31);
            this.listWorldConstructionsSearch.Name = "listWorldConstructionsSearch";
            this.listWorldConstructionsSearch.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.listWorldConstructionsSearch.ShowCommandMenuOnRightClick = true;
            this.listWorldConstructionsSearch.ShowImagesOnSubItems = true;
            this.listWorldConstructionsSearch.ShowItemCountOnGroups = true;
            this.listWorldConstructionsSearch.Size = new System.Drawing.Size(247, 251);
            this.listWorldConstructionsSearch.TabIndex = 58;
            this.listWorldConstructionsSearch.UseAlternatingBackColors = true;
            this.listWorldConstructionsSearch.UseCompatibleStateImageBehavior = false;
            this.listWorldConstructionsSearch.UseFiltering = true;
            this.listWorldConstructionsSearch.UseHotItem = true;
            this.listWorldConstructionsSearch.UseHyperlinks = true;
            this.listWorldConstructionsSearch.View = System.Windows.Forms.View.Details;
            this.listWorldConstructionsSearch.SelectedIndexChanged += new System.EventHandler(this.listWorldConstructionsSearch_SelectedIndexChanged);
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "Name";
            this.olvColumn5.IsEditable = false;
            this.olvColumn5.MinimumWidth = 50;
            this.olvColumn5.Text = "Name";
            this.olvColumn5.UseInitialLetterForGroup = true;
            this.olvColumn5.Width = 155;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "TypeAsString";
            this.olvColumn6.IsEditable = false;
            this.olvColumn6.Text = "Type";
            this.olvColumn6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn6.Width = 70;
            // 
            // btnWorldConstructionsSearch
            // 
            this.btnWorldConstructionsSearch.Location = new System.Drawing.Point(3, 3);
            this.btnWorldConstructionsSearch.Name = "btnWorldConstructionsSearch";
            this.btnWorldConstructionsSearch.Size = new System.Drawing.Size(75, 23);
            this.btnWorldConstructionsSearch.TabIndex = 46;
            this.btnWorldConstructionsSearch.Text = "Search";
            this.btnWorldConstructionsSearch.UseVisualStyleBackColor = true;
            this.btnWorldConstructionsSearch.Click += new System.EventHandler(this.searchWorldConstructionList);
            // 
            // groupBox23
            // 
            this.groupBox23.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox23.Controls.Add(this.label7);
            this.groupBox23.Controls.Add(this.cmbConstructionType);
            this.groupBox23.Controls.Add(this.groupBox24);
            this.groupBox23.Location = new System.Drawing.Point(3, 296);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(247, 164);
            this.groupBox23.TabIndex = 45;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "Filter / Sort";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Type";
            // 
            // cmbConstructionType
            // 
            this.cmbConstructionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConstructionType.FormattingEnabled = true;
            this.cmbConstructionType.Location = new System.Drawing.Point(6, 35);
            this.cmbConstructionType.Name = "cmbConstructionType";
            this.cmbConstructionType.Size = new System.Drawing.Size(121, 21);
            this.cmbConstructionType.TabIndex = 18;
            this.cmbConstructionType.SelectedIndexChanged += new System.EventHandler(this.searchWorldConstructionList);
            // 
            // groupBox24
            // 
            this.groupBox24.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox24.Controls.Add(this.radWorldConstructionsSortFiltered);
            this.groupBox24.Controls.Add(this.radWorldConstructionsSortNone);
            this.groupBox24.Controls.Add(this.radWorldConstructionsSortEvents);
            this.groupBox24.Location = new System.Drawing.Point(133, 19);
            this.groupBox24.Name = "groupBox24";
            this.groupBox24.Size = new System.Drawing.Size(108, 126);
            this.groupBox24.TabIndex = 15;
            this.groupBox24.TabStop = false;
            this.groupBox24.Text = "Sort By";
            // 
            // radWorldConstructionsSortFiltered
            // 
            this.radWorldConstructionsSortFiltered.AutoSize = true;
            this.radWorldConstructionsSortFiltered.Location = new System.Drawing.Point(6, 42);
            this.radWorldConstructionsSortFiltered.Name = "radWorldConstructionsSortFiltered";
            this.radWorldConstructionsSortFiltered.Size = new System.Drawing.Size(95, 17);
            this.radWorldConstructionsSortFiltered.TabIndex = 16;
            this.radWorldConstructionsSortFiltered.TabStop = true;
            this.radWorldConstructionsSortFiltered.Text = "Filtered Events";
            this.radWorldConstructionsSortFiltered.UseVisualStyleBackColor = true;
            this.radWorldConstructionsSortFiltered.CheckedChanged += new System.EventHandler(this.searchWorldConstructionList);
            // 
            // radWorldConstructionsSortNone
            // 
            this.radWorldConstructionsSortNone.AutoSize = true;
            this.radWorldConstructionsSortNone.Checked = true;
            this.radWorldConstructionsSortNone.Location = new System.Drawing.Point(6, 65);
            this.radWorldConstructionsSortNone.Name = "radWorldConstructionsSortNone";
            this.radWorldConstructionsSortNone.Size = new System.Drawing.Size(51, 17);
            this.radWorldConstructionsSortNone.TabIndex = 14;
            this.radWorldConstructionsSortNone.TabStop = true;
            this.radWorldConstructionsSortNone.Text = "None";
            this.radWorldConstructionsSortNone.UseVisualStyleBackColor = true;
            this.radWorldConstructionsSortNone.CheckedChanged += new System.EventHandler(this.searchWorldConstructionList);
            // 
            // radWorldConstructionsSortEvents
            // 
            this.radWorldConstructionsSortEvents.AutoSize = true;
            this.radWorldConstructionsSortEvents.Location = new System.Drawing.Point(6, 19);
            this.radWorldConstructionsSortEvents.Name = "radWorldConstructionsSortEvents";
            this.radWorldConstructionsSortEvents.Size = new System.Drawing.Size(58, 17);
            this.radWorldConstructionsSortEvents.TabIndex = 13;
            this.radWorldConstructionsSortEvents.Text = "Events";
            this.radWorldConstructionsSortEvents.UseVisualStyleBackColor = true;
            this.radWorldConstructionsSortEvents.CheckedChanged += new System.EventHandler(this.searchWorldConstructionList);
            // 
            // txtWorldConstructionsSearch
            // 
            this.txtWorldConstructionsSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWorldConstructionsSearch.Location = new System.Drawing.Point(81, 5);
            this.txtWorldConstructionsSearch.Name = "txtWorldConstructionsSearch";
            this.txtWorldConstructionsSearch.Size = new System.Drawing.Size(169, 20);
            this.txtWorldConstructionsSearch.TabIndex = 44;
            this.txtWorldConstructionsSearch.TextChanged += new System.EventHandler(this.searchWorldConstructionList);
            // 
            // tpWorldConstructionEvents
            // 
            this.tpWorldConstructionEvents.Location = new System.Drawing.Point(4, 22);
            this.tpWorldConstructionEvents.Name = "tpWorldConstructionEvents";
            this.tpWorldConstructionEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tpWorldConstructionEvents.Size = new System.Drawing.Size(253, 448);
            this.tpWorldConstructionEvents.TabIndex = 1;
            this.tpWorldConstructionEvents.Text = "Events";
            this.tpWorldConstructionEvents.UseVisualStyleBackColor = true;
            // 
            // tpStructures
            // 
            this.tpStructures.Controls.Add(this.tcStructures);
            this.tpStructures.Location = new System.Drawing.Point(4, 22);
            this.tpStructures.Name = "tpStructures";
            this.tpStructures.Size = new System.Drawing.Size(261, 492);
            this.tpStructures.TabIndex = 4;
            this.tpStructures.Text = "Structures";
            this.tpStructures.UseVisualStyleBackColor = true;
            // 
            // tcStructures
            // 
            this.tcStructures.Controls.Add(this.tpStructureSearch);
            this.tcStructures.Controls.Add(this.tpStructureEvents);
            this.tcStructures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcStructures.Location = new System.Drawing.Point(0, 0);
            this.tcStructures.Name = "tcStructures";
            this.tcStructures.SelectedIndex = 0;
            this.tcStructures.Size = new System.Drawing.Size(261, 492);
            this.tcStructures.TabIndex = 4;
            // 
            // tpStructureSearch
            // 
            this.tpStructureSearch.Controls.Add(this.lblStructureResults);
            this.tpStructureSearch.Controls.Add(this.listStructureSearch);
            this.tpStructureSearch.Controls.Add(this.btnStructuresSearch);
            this.tpStructureSearch.Controls.Add(this.groupBox25);
            this.tpStructureSearch.Controls.Add(this.txtStructuresSearch);
            this.tpStructureSearch.Location = new System.Drawing.Point(4, 22);
            this.tpStructureSearch.Name = "tpStructureSearch";
            this.tpStructureSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tpStructureSearch.Size = new System.Drawing.Size(253, 466);
            this.tpStructureSearch.TabIndex = 0;
            this.tpStructureSearch.Text = "Search";
            this.tpStructureSearch.UseVisualStyleBackColor = true;
            // 
            // lblStructureResults
            // 
            this.lblStructureResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStructureResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStructureResults.Location = new System.Drawing.Point(155, 287);
            this.lblStructureResults.Name = "lblStructureResults";
            this.lblStructureResults.Size = new System.Drawing.Size(95, 10);
            this.lblStructureResults.TabIndex = 57;
            this.lblStructureResults.Text = "0 / 0";
            this.lblStructureResults.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.hint.SetToolTip(this.lblStructureResults, "Results Shown");
            // 
            // listStructureSearch
            // 
            this.listStructureSearch.AllColumns.Add(this.olvColumn3);
            this.listStructureSearch.AllColumns.Add(this.olvColumn4);
            this.listStructureSearch.AlternateRowBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listStructureSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listStructureSearch.CellEditUseWholeCell = false;
            this.listStructureSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn3,
            this.olvColumn4});
            this.listStructureSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.listStructureSearch.FullRowSelect = true;
            this.listStructureSearch.GridLines = true;
            this.listStructureSearch.HeaderWordWrap = true;
            this.listStructureSearch.Location = new System.Drawing.Point(3, 31);
            this.listStructureSearch.Name = "listStructureSearch";
            this.listStructureSearch.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.listStructureSearch.ShowCommandMenuOnRightClick = true;
            this.listStructureSearch.ShowImagesOnSubItems = true;
            this.listStructureSearch.ShowItemCountOnGroups = true;
            this.listStructureSearch.Size = new System.Drawing.Size(247, 251);
            this.listStructureSearch.TabIndex = 56;
            this.listStructureSearch.UseAlternatingBackColors = true;
            this.listStructureSearch.UseCompatibleStateImageBehavior = false;
            this.listStructureSearch.UseFiltering = true;
            this.listStructureSearch.UseHotItem = true;
            this.listStructureSearch.UseHyperlinks = true;
            this.listStructureSearch.View = System.Windows.Forms.View.Details;
            this.listStructureSearch.SelectedIndexChanged += new System.EventHandler(this.listStructuresSearch_SelectedIndexChanged);
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Name";
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.MinimumWidth = 50;
            this.olvColumn3.Text = "Name";
            this.olvColumn3.UseInitialLetterForGroup = true;
            this.olvColumn3.Width = 155;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "TypeAsString";
            this.olvColumn4.IsEditable = false;
            this.olvColumn4.Text = "Type";
            this.olvColumn4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn4.Width = 70;
            // 
            // btnStructuresSearch
            // 
            this.btnStructuresSearch.Location = new System.Drawing.Point(3, 3);
            this.btnStructuresSearch.Name = "btnStructuresSearch";
            this.btnStructuresSearch.Size = new System.Drawing.Size(75, 23);
            this.btnStructuresSearch.TabIndex = 46;
            this.btnStructuresSearch.Text = "Search";
            this.btnStructuresSearch.UseVisualStyleBackColor = true;
            this.btnStructuresSearch.Click += new System.EventHandler(this.searchStructureList);
            // 
            // groupBox25
            // 
            this.groupBox25.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox25.Controls.Add(this.label6);
            this.groupBox25.Controls.Add(this.cmbStructureType);
            this.groupBox25.Controls.Add(this.groupBox26);
            this.groupBox25.Location = new System.Drawing.Point(3, 296);
            this.groupBox25.Name = "groupBox25";
            this.groupBox25.Size = new System.Drawing.Size(247, 164);
            this.groupBox25.TabIndex = 45;
            this.groupBox25.TabStop = false;
            this.groupBox25.Text = "Filter / Sort";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Type";
            // 
            // cmbStructureType
            // 
            this.cmbStructureType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStructureType.FormattingEnabled = true;
            this.cmbStructureType.Location = new System.Drawing.Point(6, 35);
            this.cmbStructureType.Name = "cmbStructureType";
            this.cmbStructureType.Size = new System.Drawing.Size(121, 21);
            this.cmbStructureType.TabIndex = 16;
            this.cmbStructureType.SelectedIndexChanged += new System.EventHandler(this.searchStructureList);
            // 
            // groupBox26
            // 
            this.groupBox26.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox26.Controls.Add(this.radStructuresSortFiltered);
            this.groupBox26.Controls.Add(this.radStructuresSortNone);
            this.groupBox26.Controls.Add(this.radStructuresSortEvents);
            this.groupBox26.Location = new System.Drawing.Point(133, 19);
            this.groupBox26.Name = "groupBox26";
            this.groupBox26.Size = new System.Drawing.Size(108, 126);
            this.groupBox26.TabIndex = 15;
            this.groupBox26.TabStop = false;
            this.groupBox26.Text = "Sort By";
            // 
            // radStructuresSortFiltered
            // 
            this.radStructuresSortFiltered.AutoSize = true;
            this.radStructuresSortFiltered.Location = new System.Drawing.Point(6, 42);
            this.radStructuresSortFiltered.Name = "radStructuresSortFiltered";
            this.radStructuresSortFiltered.Size = new System.Drawing.Size(95, 17);
            this.radStructuresSortFiltered.TabIndex = 16;
            this.radStructuresSortFiltered.TabStop = true;
            this.radStructuresSortFiltered.Text = "Filtered Events";
            this.radStructuresSortFiltered.UseVisualStyleBackColor = true;
            this.radStructuresSortFiltered.CheckedChanged += new System.EventHandler(this.searchStructureList);
            // 
            // radStructuresSortNone
            // 
            this.radStructuresSortNone.AutoSize = true;
            this.radStructuresSortNone.Checked = true;
            this.radStructuresSortNone.Location = new System.Drawing.Point(6, 65);
            this.radStructuresSortNone.Name = "radStructuresSortNone";
            this.radStructuresSortNone.Size = new System.Drawing.Size(51, 17);
            this.radStructuresSortNone.TabIndex = 14;
            this.radStructuresSortNone.TabStop = true;
            this.radStructuresSortNone.Text = "None";
            this.radStructuresSortNone.UseVisualStyleBackColor = true;
            this.radStructuresSortNone.CheckedChanged += new System.EventHandler(this.searchStructureList);
            // 
            // radStructuresSortEvents
            // 
            this.radStructuresSortEvents.AutoSize = true;
            this.radStructuresSortEvents.Location = new System.Drawing.Point(6, 19);
            this.radStructuresSortEvents.Name = "radStructuresSortEvents";
            this.radStructuresSortEvents.Size = new System.Drawing.Size(58, 17);
            this.radStructuresSortEvents.TabIndex = 13;
            this.radStructuresSortEvents.Text = "Events";
            this.radStructuresSortEvents.UseVisualStyleBackColor = true;
            this.radStructuresSortEvents.CheckedChanged += new System.EventHandler(this.searchStructureList);
            // 
            // txtStructuresSearch
            // 
            this.txtStructuresSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStructuresSearch.Location = new System.Drawing.Point(81, 5);
            this.txtStructuresSearch.Name = "txtStructuresSearch";
            this.txtStructuresSearch.Size = new System.Drawing.Size(169, 20);
            this.txtStructuresSearch.TabIndex = 44;
            this.txtStructuresSearch.TextChanged += new System.EventHandler(this.searchStructureList);
            // 
            // tpStructureEvents
            // 
            this.tpStructureEvents.Location = new System.Drawing.Point(4, 22);
            this.tpStructureEvents.Name = "tpStructureEvents";
            this.tpStructureEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tpStructureEvents.Size = new System.Drawing.Size(253, 448);
            this.tpStructureEvents.TabIndex = 1;
            this.tpStructureEvents.Text = "Events";
            this.tpStructureEvents.UseVisualStyleBackColor = true;
            // 
            // CollectionsTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcCollections);
            this.Name = "CollectionsTab";
            this.tcCollections.ResumeLayout(false);
            this.tpEras.ResumeLayout(false);
            this.tcEras.ResumeLayout(false);
            this.tpEraSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listEraSearch)).EndInit();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEraEnd)).EndInit();
            this.tpWorldConstructions.ResumeLayout(false);
            this.tcWorldConstructions.ResumeLayout(false);
            this.tpWorldConstructionSearch.ResumeLayout(false);
            this.tpWorldConstructionSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listWorldConstructionsSearch)).EndInit();
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.groupBox24.ResumeLayout(false);
            this.groupBox24.PerformLayout();
            this.tpStructures.ResumeLayout(false);
            this.tcStructures.ResumeLayout(false);
            this.tpStructureSearch.ResumeLayout(false);
            this.tpStructureSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listStructureSearch)).EndInit();
            this.groupBox25.ResumeLayout(false);
            this.groupBox25.PerformLayout();
            this.groupBox26.ResumeLayout(false);
            this.groupBox26.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcCollections;
        private System.Windows.Forms.TabPage tpEras;
        private System.Windows.Forms.TabControl tcEras;
        private System.Windows.Forms.TabPage tpEraSearch;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.Button btnEraShow;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numStart;
        private System.Windows.Forms.NumericUpDown numEraEnd;
        private System.Windows.Forms.TabPage tpEraEvents;
        private System.Windows.Forms.TabPage tpWorldConstructions;
        private System.Windows.Forms.TabControl tcWorldConstructions;
        private System.Windows.Forms.TabPage tpWorldConstructionSearch;
        private System.Windows.Forms.Button btnWorldConstructionsSearch;
        private System.Windows.Forms.GroupBox groupBox23;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbConstructionType;
        private System.Windows.Forms.GroupBox groupBox24;
        private System.Windows.Forms.RadioButton radWorldConstructionsSortFiltered;
        private System.Windows.Forms.RadioButton radWorldConstructionsSortNone;
        private System.Windows.Forms.RadioButton radWorldConstructionsSortEvents;
        private System.Windows.Forms.TextBox txtWorldConstructionsSearch;
        private System.Windows.Forms.TabPage tpWorldConstructionEvents;
        private System.Windows.Forms.TabPage tpStructures;
        private System.Windows.Forms.TabControl tcStructures;
        private System.Windows.Forms.TabPage tpStructureSearch;
        private System.Windows.Forms.Button btnStructuresSearch;
        private System.Windows.Forms.GroupBox groupBox25;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbStructureType;
        private System.Windows.Forms.GroupBox groupBox26;
        private System.Windows.Forms.RadioButton radStructuresSortFiltered;
        private System.Windows.Forms.RadioButton radStructuresSortNone;
        private System.Windows.Forms.RadioButton radStructuresSortEvents;
        private System.Windows.Forms.TextBox txtStructuresSearch;
        private System.Windows.Forms.TabPage tpStructureEvents;
        private System.Windows.Forms.Label lblStructureResults;
        private BrightIdeasSoftware.ObjectListView listStructureSearch;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private System.Windows.Forms.Label lblWorldConstructionResult;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.ObjectListView listWorldConstructionsSearch;
        private BrightIdeasSoftware.ObjectListView listEraSearch;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private BrightIdeasSoftware.OLVColumn olvColumn8;
    }
}
