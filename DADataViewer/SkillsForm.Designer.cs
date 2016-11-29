namespace DADataViewer
{
    partial class SkillsForm
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.skillTileLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.skillTilesPanel = new System.Windows.Forms.DoubleBufferedPanel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.skillTileLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 448);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(608, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // skillTileLabel
            // 
            this.skillTileLabel.Name = "skillTileLabel";
            this.skillTileLabel.Size = new System.Drawing.Size(88, 17);
            this.skillTileLabel.Text = "No tile selected";
            // 
            // skillTilesPanel
            // 
            this.skillTilesPanel.BackColor = System.Drawing.Color.Teal;
            this.skillTilesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skillTilesPanel.Location = new System.Drawing.Point(0, 0);
            this.skillTilesPanel.Name = "skillTilesPanel";
            this.skillTilesPanel.Size = new System.Drawing.Size(608, 448);
            this.skillTilesPanel.TabIndex = 1;
            this.skillTilesPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.skillTilesPanel_Paint);
            this.skillTilesPanel.MouseLeave += new System.EventHandler(this.skillTilesPanel_MouseLeave);
            this.skillTilesPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.skillTilesPanel_MouseMove);
            // 
            // SkillsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 470);
            this.Controls.Add(this.skillTilesPanel);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SkillsForm";
            this.ShowIcon = false;
            this.Text = "Skills";
            this.Load += new System.EventHandler(this.SkillsForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DoubleBufferedPanel skillTilesPanel;
        private System.Windows.Forms.ToolStripStatusLabel skillTileLabel;
    }
}