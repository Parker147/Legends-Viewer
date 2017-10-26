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
            this.tcCollections.SuspendLayout();
            this.tpEras.SuspendLayout();
            this.tcEras.SuspendLayout();
            this.tpEraSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listEraSearch)).BeginInit();
            this.groupBox16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEraEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // tcCollections
            // 
            this.tcCollections.Controls.Add(this.tpEras);
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
            this.tpEraEvents.Size = new System.Drawing.Size(253, 466);
            this.tpEraEvents.TabIndex = 1;
            this.tpEraEvents.Text = "Events";
            this.tpEraEvents.UseVisualStyleBackColor = true;
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
        private BrightIdeasSoftware.ObjectListView listEraSearch;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private BrightIdeasSoftware.OLVColumn olvColumn8;
    }
}
