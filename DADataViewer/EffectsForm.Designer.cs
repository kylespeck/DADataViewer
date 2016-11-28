namespace DADataViewer
{
    partial class EffectsForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.effectFilesListBox = new System.Windows.Forms.ToolStripComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.effectNumberLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.effectPanel = new System.Windows.Forms.Panel();
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.effectFilesListBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(284, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // effectFilesListBox
            // 
            this.effectFilesListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.effectFilesListBox.Name = "effectFilesListBox";
            this.effectFilesListBox.Size = new System.Drawing.Size(121, 25);
            this.effectFilesListBox.SelectedIndexChanged += new System.EventHandler(this.effectFilesListBox_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.effectNumberLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 239);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(284, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // effectNumberLabel
            // 
            this.effectNumberLabel.Name = "effectNumberLabel";
            this.effectNumberLabel.Size = new System.Drawing.Size(102, 17);
            this.effectNumberLabel.Text = "No effect selected";
            // 
            // effectPanel
            // 
            this.effectPanel.BackColor = System.Drawing.Color.Teal;
            this.effectPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.effectPanel.Location = new System.Drawing.Point(0, 25);
            this.effectPanel.Name = "effectPanel";
            this.effectPanel.Size = new System.Drawing.Size(284, 214);
            this.effectPanel.TabIndex = 2;
            this.effectPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.effectPanel_Paint);
            // 
            // animationTimer
            // 
            this.animationTimer.Tick += new System.EventHandler(this.animationTimer_Tick);
            // 
            // EffectsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 400);
            this.Controls.Add(this.effectPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.Name = "EffectsForm";
            this.ShowIcon = false;
            this.Text = "Effects";
            this.Load += new System.EventHandler(this.EffectsForm_Load);
            this.VisibleChanged += new System.EventHandler(this.EffectsForm_VisibleChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox effectFilesListBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel effectPanel;
        private System.Windows.Forms.ToolStripStatusLabel effectNumberLabel;
        private System.Windows.Forms.Timer animationTimer;
    }
}