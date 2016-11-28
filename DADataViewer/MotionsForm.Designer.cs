namespace DADataViewer
{
    partial class MotionsForm
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
            this.motionTilesPanel = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.motionTilesListBox = new System.Windows.Forms.ToolStripComboBox();
            this.genderListBox = new System.Windows.Forms.ToolStripComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.motionTileLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // motionTilesPanel
            // 
            this.motionTilesPanel.BackColor = System.Drawing.Color.Teal;
            this.motionTilesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.motionTilesPanel.Location = new System.Drawing.Point(0, 25);
            this.motionTilesPanel.Name = "motionTilesPanel";
            this.motionTilesPanel.Size = new System.Drawing.Size(284, 214);
            this.motionTilesPanel.TabIndex = 0;
            this.motionTilesPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.motionTilesPanel_Paint);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.motionTilesListBox,
            this.genderListBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(284, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // motionTilesListBox
            // 
            this.motionTilesListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.motionTilesListBox.Name = "motionTilesListBox";
            this.motionTilesListBox.Size = new System.Drawing.Size(121, 25);
            // 
            // genderListBox
            // 
            this.genderListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.genderListBox.Items.AddRange(new object[] {
            "Man",
            "Woman"});
            this.genderListBox.Name = "genderListBox";
            this.genderListBox.Size = new System.Drawing.Size(121, 25);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.motionTileLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 239);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(284, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // motionTileLabel
            // 
            this.motionTileLabel.Name = "motionTileLabel";
            this.motionTileLabel.Size = new System.Drawing.Size(88, 17);
            this.motionTileLabel.Text = "No tile selected";
            // 
            // animationTimer
            // 
            this.animationTimer.Interval = 200;
            this.animationTimer.Tick += new System.EventHandler(this.animationTimer_Tick);
            // 
            // MotionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.motionTilesPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MotionsForm";
            this.ShowIcon = false;
            this.Text = "Motions";
            this.Load += new System.EventHandler(this.MotionsForm_Load);
            this.VisibleChanged += new System.EventHandler(this.MotionsForm_VisibleChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel motionTilesPanel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox motionTilesListBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel motionTileLabel;
        private System.Windows.Forms.ToolStripComboBox genderListBox;
        private System.Windows.Forms.Timer animationTimer;
    }
}