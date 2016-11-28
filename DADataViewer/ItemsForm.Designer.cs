namespace DADataViewer
{
    partial class ItemsForm
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
            this.itemTilesPanel = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.itemFilesListBox = new System.Windows.Forms.ToolStripComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.itemTileLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // itemTilesPanel
            // 
            this.itemTilesPanel.BackColor = System.Drawing.Color.Teal;
            this.itemTilesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemTilesPanel.Location = new System.Drawing.Point(0, 25);
            this.itemTilesPanel.Name = "itemTilesPanel";
            this.itemTilesPanel.Size = new System.Drawing.Size(608, 448);
            this.itemTilesPanel.TabIndex = 0;
            this.itemTilesPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.itemTilesPanel_Paint);
            this.itemTilesPanel.MouseLeave += new System.EventHandler(this.itemTilesPanel_MouseLeave);
            this.itemTilesPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.itemTilesPanel_MouseMove);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemFilesListBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(608, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // itemFilesListBox
            // 
            this.itemFilesListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.itemFilesListBox.Name = "itemFilesListBox";
            this.itemFilesListBox.Size = new System.Drawing.Size(121, 25);
            this.itemFilesListBox.SelectedIndexChanged += new System.EventHandler(this.itemFilesListBox_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemTileLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 473);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(608, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // itemTileLabel
            // 
            this.itemTileLabel.Name = "itemTileLabel";
            this.itemTileLabel.Size = new System.Drawing.Size(88, 17);
            this.itemTileLabel.Text = "No tile selected";
            // 
            // ItemsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 495);
            this.Controls.Add(this.itemTilesPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ItemsForm";
            this.ShowIcon = false;
            this.Text = "Items";
            this.Load += new System.EventHandler(this.ItemsForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel itemTilesPanel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel itemTileLabel;
        private System.Windows.Forms.ToolStripComboBox itemFilesListBox;
    }
}