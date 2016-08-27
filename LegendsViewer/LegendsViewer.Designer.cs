namespace LegendsViewer
{
    partial class frmLegendsViewer
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.hint = new System.Windows.Forms.ToolTip(this.components);
            this.lblVersion = new System.Windows.Forms.Label();
            this.scWorld = new System.Windows.Forms.SplitContainer();
            this.tcWorld = new System.Windows.Forms.TabControl();
            this.tpSummary = new System.Windows.Forms.TabPage();
            this.tpHF = new System.Windows.Forms.TabPage();
            this.tpCivs = new System.Windows.Forms.TabPage();
            this.tpSites = new System.Windows.Forms.TabPage();
            this.tpRegions = new System.Windows.Forms.TabPage();
            this.tpWarfare = new System.Windows.Forms.TabPage();
            this.tpCollections = new System.Windows.Forms.TabPage();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.summaryTab1 = new LegendsViewer.Controls.Tabs.SummaryTab();
            this.historicalFiguresTab1 = new LegendsViewer.Controls.Tabs.HistoricalFiguresTab();
            this.civsTab1 = new LegendsViewer.Controls.Tabs.CivsTab();
            this.sitesTab1 = new LegendsViewer.Controls.Tabs.SitesTab();
            this.geographyTab1 = new LegendsViewer.Controls.Tabs.GeographyTab();
            this.warfareTab1 = new LegendsViewer.Controls.Tabs.WarfareTab();
            this.collectionsTab1 = new LegendsViewer.Controls.Tabs.CollectionsTab();
            ((System.ComponentModel.ISupportInitialize)(this.scWorld)).BeginInit();
            this.scWorld.Panel1.SuspendLayout();
            this.scWorld.Panel2.SuspendLayout();
            this.scWorld.SuspendLayout();
            this.tcWorld.SuspendLayout();
            this.tpSummary.SuspendLayout();
            this.tpHF.SuspendLayout();
            this.tpCivs.SuspendLayout();
            this.tpSites.SuspendLayout();
            this.tpRegions.SuspendLayout();
            this.tpWarfare.SuspendLayout();
            this.tpCollections.SuspendLayout();
            this.SuspendLayout();
            // 
            // dlgOpen
            // 
            this.dlgOpen.Filter = "Dwarf Files | *.xml; *.txt; *.zip";
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblVersion.Location = new System.Drawing.Point(930, 9);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(48, 13);
            this.lblVersion.TabIndex = 34;
            this.lblVersion.Text = "v1.00.00";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblVersion.Click += new System.EventHandler(this.open_ReadMe);
            // 
            // scWorld
            // 
            this.scWorld.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scWorld.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scWorld.Location = new System.Drawing.Point(0, 0);
            this.scWorld.Name = "scWorld";
            // 
            // scWorld.Panel1
            // 
            this.scWorld.Panel1.Controls.Add(this.tcWorld);
            this.scWorld.Panel1MinSize = 279;
            // 
            // scWorld.Panel2
            // 
            this.scWorld.Panel2.Controls.Add(this.btnClose);
            this.scWorld.Panel2.Controls.Add(this.lblVersion);
            this.scWorld.Panel2.Controls.Add(this.btnBack);
            this.scWorld.Panel2.Controls.Add(this.btnForward);
            this.scWorld.Size = new System.Drawing.Size(1264, 681);
            this.scWorld.SplitterDistance = 279;
            this.scWorld.TabIndex = 35;
            // 
            // tcWorld
            // 
            this.tcWorld.Controls.Add(this.tpSummary);
            this.tcWorld.Controls.Add(this.tpHF);
            this.tcWorld.Controls.Add(this.tpCivs);
            this.tcWorld.Controls.Add(this.tpSites);
            this.tcWorld.Controls.Add(this.tpRegions);
            this.tcWorld.Controls.Add(this.tpWarfare);
            this.tcWorld.Controls.Add(this.tpCollections);
            this.tcWorld.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcWorld.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tcWorld.Location = new System.Drawing.Point(0, 0);
            this.tcWorld.Multiline = true;
            this.tcWorld.Name = "tcWorld";
            this.tcWorld.SelectedIndex = 0;
            this.tcWorld.Size = new System.Drawing.Size(279, 681);
            this.tcWorld.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tcWorld.TabIndex = 2;
            // 
            // tpSummary
            // 
            this.tpSummary.Controls.Add(this.summaryTab1);
            this.tpSummary.Location = new System.Drawing.Point(4, 40);
            this.tpSummary.Name = "tpSummary";
            this.tpSummary.Size = new System.Drawing.Size(271, 637);
            this.tpSummary.TabIndex = 0;
            this.tpSummary.Text = "Summary";
            this.tpSummary.UseVisualStyleBackColor = true;
            // 
            // tpHF
            // 
            this.tpHF.Controls.Add(this.historicalFiguresTab1);
            this.tpHF.Location = new System.Drawing.Point(4, 40);
            this.tpHF.Margin = new System.Windows.Forms.Padding(0);
            this.tpHF.Name = "tpHF";
            this.tpHF.Size = new System.Drawing.Size(271, 637);
            this.tpHF.TabIndex = 1;
            this.tpHF.Text = "Historical Figures";
            this.tpHF.UseVisualStyleBackColor = true;
            // 
            // tpCivs
            // 
            this.tpCivs.Controls.Add(this.civsTab1);
            this.tpCivs.Location = new System.Drawing.Point(4, 40);
            this.tpCivs.Name = "tpCivs";
            this.tpCivs.Size = new System.Drawing.Size(271, 637);
            this.tpCivs.TabIndex = 5;
            this.tpCivs.Text = "Civs and entities";
            this.tpCivs.UseVisualStyleBackColor = true;
            // 
            // tpSites
            // 
            this.tpSites.Controls.Add(this.sitesTab1);
            this.tpSites.Location = new System.Drawing.Point(4, 40);
            this.tpSites.Name = "tpSites";
            this.tpSites.Size = new System.Drawing.Size(271, 637);
            this.tpSites.TabIndex = 2;
            this.tpSites.Text = "Sites";
            this.tpSites.UseVisualStyleBackColor = true;
            // 
            // tpRegions
            // 
            this.tpRegions.Controls.Add(this.geographyTab1);
            this.tpRegions.Location = new System.Drawing.Point(4, 40);
            this.tpRegions.Name = "tpRegions";
            this.tpRegions.Size = new System.Drawing.Size(271, 637);
            this.tpRegions.TabIndex = 3;
            this.tpRegions.Text = "Geography";
            this.tpRegions.UseVisualStyleBackColor = true;
            // 
            // tpWarfare
            // 
            this.tpWarfare.Controls.Add(this.warfareTab1);
            this.tpWarfare.Location = new System.Drawing.Point(4, 40);
            this.tpWarfare.Name = "tpWarfare";
            this.tpWarfare.Size = new System.Drawing.Size(271, 637);
            this.tpWarfare.TabIndex = 6;
            this.tpWarfare.Text = "Warfare";
            this.tpWarfare.UseVisualStyleBackColor = true;
            // 
            // tpCollections
            // 
            this.tpCollections.Controls.Add(this.collectionsTab1);
            this.tpCollections.Location = new System.Drawing.Point(4, 40);
            this.tpCollections.Name = "tpCollections";
            this.tpCollections.Size = new System.Drawing.Size(271, 637);
            this.tpCollections.TabIndex = 7;
            this.tpCollections.Text = "Collections";
            this.tpCollections.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(225, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 33;
            this.btnClose.Text = "Close Tab";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Visible = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(6, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 30;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(87, 3);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(75, 23);
            this.btnForward.TabIndex = 31;
            this.btnForward.Text = "Forward";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // summaryTab1
            // 
            this.summaryTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryTab1.Location = new System.Drawing.Point(0, 0);
            this.summaryTab1.Name = "summaryTab1";
            this.summaryTab1.Size = new System.Drawing.Size(271, 637);
            this.summaryTab1.TabIndex = 0;
            // 
            // historicalFiguresTab1
            // 
            this.historicalFiguresTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historicalFiguresTab1.Location = new System.Drawing.Point(0, 0);
            this.historicalFiguresTab1.Name = "historicalFiguresTab1";
            this.historicalFiguresTab1.Size = new System.Drawing.Size(271, 655);
            this.historicalFiguresTab1.TabIndex = 0;
            // 
            // civsTab1
            // 
            this.civsTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.civsTab1.Location = new System.Drawing.Point(0, 0);
            this.civsTab1.Name = "civsTab1";
            this.civsTab1.Size = new System.Drawing.Size(271, 655);
            this.civsTab1.TabIndex = 0;
            // 
            // sitesTab1
            // 
            this.sitesTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sitesTab1.Location = new System.Drawing.Point(0, 0);
            this.sitesTab1.Name = "sitesTab1";
            this.sitesTab1.Size = new System.Drawing.Size(271, 637);
            this.sitesTab1.TabIndex = 0;
            // 
            // geographyTab1
            // 
            this.geographyTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.geographyTab1.Location = new System.Drawing.Point(0, 0);
            this.geographyTab1.Name = "geographyTab1";
            this.geographyTab1.Size = new System.Drawing.Size(271, 637);
            this.geographyTab1.TabIndex = 0;
            // 
            // warfareTab1
            // 
            this.warfareTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.warfareTab1.Location = new System.Drawing.Point(0, 0);
            this.warfareTab1.Name = "warfareTab1";
            this.warfareTab1.Size = new System.Drawing.Size(271, 637);
            this.warfareTab1.TabIndex = 0;
            // 
            // collectionsTab1
            // 
            this.collectionsTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.collectionsTab1.Location = new System.Drawing.Point(0, 0);
            this.collectionsTab1.Name = "collectionsTab1";
            this.collectionsTab1.Size = new System.Drawing.Size(271, 637);
            this.collectionsTab1.TabIndex = 0;
            // 
            // frmLegendsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.scWorld);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmLegendsViewer";
            this.Text = "Legends Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLegendsViewer_FormClosed);
            this.Shown += new System.EventHandler(this.frmLegendsViewer_Shown);
            this.ResizeEnd += new System.EventHandler(this.frmLegendsViewer_ResizeEnd);
            this.scWorld.Panel1.ResumeLayout(false);
            this.scWorld.Panel2.ResumeLayout(false);
            this.scWorld.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scWorld)).EndInit();
            this.scWorld.ResumeLayout(false);
            this.tcWorld.ResumeLayout(false);
            this.tpSummary.ResumeLayout(false);
            this.tpHF.ResumeLayout(false);
            this.tpCivs.ResumeLayout(false);
            this.tpSites.ResumeLayout(false);
            this.tpRegions.ResumeLayout(false);
            this.tpWarfare.ResumeLayout(false);
            this.tpCollections.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcWorld;
        private System.Windows.Forms.TabPage tpSites;
        private System.Windows.Forms.TabPage tpRegions;
        private System.Windows.Forms.TabPage tpCivs;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.TabPage tpHF;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolTip hint;
        private System.Windows.Forms.TabPage tpWarfare;
        private System.Windows.Forms.TabPage tpCollections;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.SplitContainer scWorld;
        private System.Windows.Forms.TabPage tpSummary;
        private Controls.Tabs.SummaryTab summaryTab1;
        private Controls.Tabs.HistoricalFiguresTab historicalFiguresTab1;
        private Controls.Tabs.CivsTab civsTab1;
        private Controls.Tabs.SitesTab sitesTab1;
        private Controls.Tabs.GeographyTab geographyTab1;
        private Controls.Tabs.WarfareTab warfareTab1;
        private Controls.Tabs.CollectionsTab collectionsTab1;
    }
}

