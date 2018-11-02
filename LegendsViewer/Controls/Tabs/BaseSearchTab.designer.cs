﻿using System.ComponentModel;
using System.Windows.Forms;

namespace LegendsViewer.Controls.Tabs
{
    partial class BaseSearchTab
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
            this.components = new System.ComponentModel.Container();
            this.hint = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // LVQueryBaseTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "LVQueryBaseTab";
            this.Size = new System.Drawing.Size(269, 518);
            this.ResumeLayout(false);

        }

        #endregion

        protected internal ToolTip hint;
    }
}
