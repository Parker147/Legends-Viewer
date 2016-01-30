namespace LegendsViewer.Controls.Tabs
{
    partial class CivsTab
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
            this.tcCivs = new System.Windows.Forms.TabControl();
            this.tpCivSearch = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.cmbEntityPopulation = new System.Windows.Forms.ComboBox();
            this.radEntitySortPopulation = new System.Windows.Forms.RadioButton();
            this.radCivSortWars = new System.Windows.Forms.RadioButton();
            this.radCivSortFiltered = new System.Windows.Forms.RadioButton();
            this.radCivSites = new System.Windows.Forms.RadioButton();
            this.radEntityNone = new System.Windows.Forms.RadioButton();
            this.radEntitySortEvents = new System.Windows.Forms.RadioButton();
            this.chkCiv = new System.Windows.Forms.CheckBox();
            this.cmbCivRace = new System.Windows.Forms.ComboBox();
            this.txtCivSearch = new System.Windows.Forms.TextBox();
            this.listCivSearch = new System.Windows.Forms.ListBox();
            this.btnCivSearch = new System.Windows.Forms.Button();
            this.tpCivEvents = new System.Windows.Forms.TabPage();
            this.tcCivs.SuspendLayout();
            this.tpCivSearch.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcCivs
            // 
            this.tcCivs.Controls.Add(this.tpCivSearch);
            this.tcCivs.Controls.Add(this.tpCivEvents);
            this.tcCivs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcCivs.Location = new System.Drawing.Point(0, 0);
            this.tcCivs.Name = "tcCivs";
            this.tcCivs.SelectedIndex = 0;
            this.tcCivs.Size = new System.Drawing.Size(269, 518);
            this.tcCivs.TabIndex = 1;
            // 
            // tpCivSearch
            // 
            this.tpCivSearch.Controls.Add(this.groupBox4);
            this.tpCivSearch.Controls.Add(this.txtCivSearch);
            this.tpCivSearch.Controls.Add(this.listCivSearch);
            this.tpCivSearch.Controls.Add(this.btnCivSearch);
            this.tpCivSearch.Location = new System.Drawing.Point(4, 22);
            this.tpCivSearch.Name = "tpCivSearch";
            this.tpCivSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tpCivSearch.Size = new System.Drawing.Size(261, 492);
            this.tpCivSearch.TabIndex = 0;
            this.tpCivSearch.Text = "Search";
            this.tpCivSearch.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.groupBox9);
            this.groupBox4.Controls.Add(this.chkCiv);
            this.groupBox4.Controls.Add(this.cmbCivRace);
            this.groupBox4.Location = new System.Drawing.Point(3, 293);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(254, 175);
            this.groupBox4.TabIndex = 37;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Filter / Sort";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Race";
            // 
            // groupBox9
            // 
            this.groupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox9.Controls.Add(this.cmbEntityPopulation);
            this.groupBox9.Controls.Add(this.radEntitySortPopulation);
            this.groupBox9.Controls.Add(this.radCivSortWars);
            this.groupBox9.Controls.Add(this.radCivSortFiltered);
            this.groupBox9.Controls.Add(this.radCivSites);
            this.groupBox9.Controls.Add(this.radEntityNone);
            this.groupBox9.Controls.Add(this.radEntitySortEvents);
            this.groupBox9.Location = new System.Drawing.Point(6, 62);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(240, 107);
            this.groupBox9.TabIndex = 15;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Sort By";
            // 
            // cmbEntityPopulation
            // 
            this.cmbEntityPopulation.FormattingEnabled = true;
            this.cmbEntityPopulation.Location = new System.Drawing.Point(113, 38);
            this.cmbEntityPopulation.Name = "cmbEntityPopulation";
            this.cmbEntityPopulation.Size = new System.Drawing.Size(121, 21);
            this.cmbEntityPopulation.TabIndex = 19;
            this.cmbEntityPopulation.SelectedIndexChanged += new System.EventHandler(this.searchEntityList);
            // 
            // radEntitySortPopulation
            // 
            this.radEntitySortPopulation.AutoSize = true;
            this.radEntitySortPopulation.Location = new System.Drawing.Point(114, 19);
            this.radEntitySortPopulation.Name = "radEntitySortPopulation";
            this.radEntitySortPopulation.Size = new System.Drawing.Size(75, 17);
            this.radEntitySortPopulation.TabIndex = 18;
            this.radEntitySortPopulation.TabStop = true;
            this.radEntitySortPopulation.Text = "Population";
            this.radEntitySortPopulation.UseVisualStyleBackColor = true;
            this.radEntitySortPopulation.CheckedChanged += new System.EventHandler(this.searchEntityList);
            // 
            // radCivSortWars
            // 
            this.radCivSortWars.AutoSize = true;
            this.radCivSortWars.Location = new System.Drawing.Point(6, 88);
            this.radCivSortWars.Name = "radCivSortWars";
            this.radCivSortWars.Size = new System.Drawing.Size(50, 17);
            this.radCivSortWars.TabIndex = 17;
            this.radCivSortWars.TabStop = true;
            this.radCivSortWars.Text = "Wars";
            this.radCivSortWars.UseVisualStyleBackColor = true;
            this.radCivSortWars.CheckedChanged += new System.EventHandler(this.searchEntityList);
            // 
            // radCivSortFiltered
            // 
            this.radCivSortFiltered.AutoSize = true;
            this.radCivSortFiltered.Location = new System.Drawing.Point(6, 42);
            this.radCivSortFiltered.Name = "radCivSortFiltered";
            this.radCivSortFiltered.Size = new System.Drawing.Size(95, 17);
            this.radCivSortFiltered.TabIndex = 16;
            this.radCivSortFiltered.TabStop = true;
            this.radCivSortFiltered.Text = "Filtered Events";
            this.radCivSortFiltered.UseVisualStyleBackColor = true;
            this.radCivSortFiltered.CheckedChanged += new System.EventHandler(this.searchEntityList);
            // 
            // radCivSites
            // 
            this.radCivSites.AutoSize = true;
            this.radCivSites.Location = new System.Drawing.Point(6, 65);
            this.radCivSites.Name = "radCivSites";
            this.radCivSites.Size = new System.Drawing.Size(48, 17);
            this.radCivSites.TabIndex = 15;
            this.radCivSites.TabStop = true;
            this.radCivSites.Text = "Sites";
            this.radCivSites.UseVisualStyleBackColor = true;
            this.radCivSites.CheckedChanged += new System.EventHandler(this.searchEntityList);
            // 
            // radEntityNone
            // 
            this.radEntityNone.AutoSize = true;
            this.radEntityNone.Checked = true;
            this.radEntityNone.Location = new System.Drawing.Point(114, 65);
            this.radEntityNone.Name = "radEntityNone";
            this.radEntityNone.Size = new System.Drawing.Size(51, 17);
            this.radEntityNone.TabIndex = 14;
            this.radEntityNone.TabStop = true;
            this.radEntityNone.Text = "None";
            this.radEntityNone.UseVisualStyleBackColor = true;
            this.radEntityNone.CheckedChanged += new System.EventHandler(this.searchEntityList);
            // 
            // radEntitySortEvents
            // 
            this.radEntitySortEvents.AutoSize = true;
            this.radEntitySortEvents.Location = new System.Drawing.Point(6, 19);
            this.radEntitySortEvents.Name = "radEntitySortEvents";
            this.radEntitySortEvents.Size = new System.Drawing.Size(58, 17);
            this.radEntitySortEvents.TabIndex = 13;
            this.radEntitySortEvents.Text = "Events";
            this.radEntitySortEvents.UseVisualStyleBackColor = true;
            this.radEntitySortEvents.CheckedChanged += new System.EventHandler(this.searchEntityList);
            // 
            // chkCiv
            // 
            this.chkCiv.AutoSize = true;
            this.chkCiv.Location = new System.Drawing.Point(147, 34);
            this.chkCiv.Name = "chkCiv";
            this.chkCiv.Size = new System.Drawing.Size(80, 17);
            this.chkCiv.TabIndex = 14;
            this.chkCiv.Text = "Civilizations";
            this.chkCiv.UseVisualStyleBackColor = true;
            this.chkCiv.CheckedChanged += new System.EventHandler(this.searchEntityList);
            // 
            // cmbCivRace
            // 
            this.cmbCivRace.FormattingEnabled = true;
            this.cmbCivRace.Location = new System.Drawing.Point(6, 32);
            this.cmbCivRace.Name = "cmbCivRace";
            this.cmbCivRace.Size = new System.Drawing.Size(121, 21);
            this.cmbCivRace.TabIndex = 13;
            this.cmbCivRace.SelectedIndexChanged += new System.EventHandler(this.searchEntityList);
            // 
            // txtCivSearch
            // 
            this.txtCivSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCivSearch.Location = new System.Drawing.Point(81, 5);
            this.txtCivSearch.Name = "txtCivSearch";
            this.txtCivSearch.Size = new System.Drawing.Size(177, 20);
            this.txtCivSearch.TabIndex = 36;
            this.txtCivSearch.TextChanged += new System.EventHandler(this.searchEntityList);
            // 
            // listCivSearch
            // 
            this.listCivSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listCivSearch.FormattingEnabled = true;
            this.listCivSearch.Location = new System.Drawing.Point(3, 31);
            this.listCivSearch.Name = "listCivSearch";
            this.listCivSearch.Size = new System.Drawing.Size(256, 251);
            this.listCivSearch.TabIndex = 35;
            this.listCivSearch.SelectedIndexChanged += new System.EventHandler(this.listCivSearch_SelectedIndexChanged);
            // 
            // btnCivSearch
            // 
            this.btnCivSearch.Location = new System.Drawing.Point(3, 3);
            this.btnCivSearch.Name = "btnCivSearch";
            this.btnCivSearch.Size = new System.Drawing.Size(75, 23);
            this.btnCivSearch.TabIndex = 34;
            this.btnCivSearch.Text = "Search";
            this.btnCivSearch.UseVisualStyleBackColor = true;
            this.btnCivSearch.Click += new System.EventHandler(this.searchEntityList);
            // 
            // tpCivEvents
            // 
            this.tpCivEvents.AutoScroll = true;
            this.tpCivEvents.Location = new System.Drawing.Point(4, 22);
            this.tpCivEvents.Name = "tpCivEvents";
            this.tpCivEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tpCivEvents.Size = new System.Drawing.Size(261, 492);
            this.tpCivEvents.TabIndex = 1;
            this.tpCivEvents.Text = "Events";
            this.tpCivEvents.UseVisualStyleBackColor = true;
            // 
            // CivsTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcCivs);
            this.Name = "CivsTab";
            this.tcCivs.ResumeLayout(false);
            this.tpCivSearch.ResumeLayout(false);
            this.tpCivSearch.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcCivs;
        private System.Windows.Forms.TabPage tpCivSearch;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.ComboBox cmbEntityPopulation;
        private System.Windows.Forms.RadioButton radEntitySortPopulation;
        private System.Windows.Forms.RadioButton radCivSortWars;
        private System.Windows.Forms.RadioButton radCivSortFiltered;
        private System.Windows.Forms.RadioButton radCivSites;
        private System.Windows.Forms.RadioButton radEntityNone;
        private System.Windows.Forms.RadioButton radEntitySortEvents;
        private System.Windows.Forms.CheckBox chkCiv;
        private System.Windows.Forms.ComboBox cmbCivRace;
        private System.Windows.Forms.TextBox txtCivSearch;
        private System.Windows.Forms.ListBox listCivSearch;
        private System.Windows.Forms.Button btnCivSearch;
        private System.Windows.Forms.TabPage tpCivEvents;
    }
}
