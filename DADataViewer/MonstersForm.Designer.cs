namespace DADataViewer
{
    partial class MonstersForm
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
            this.monsterFilesListBox = new System.Windows.Forms.ToolStripComboBox();
            this.monsterTilePanel = new System.Windows.Forms.DoubleBufferedPanel();
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.monsterTileLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.monsterFilesListBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(640, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // monsterFilesListBox
            // 
            this.monsterFilesListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monsterFilesListBox.Name = "monsterFilesListBox";
            this.monsterFilesListBox.Size = new System.Drawing.Size(121, 25);
            this.monsterFilesListBox.SelectedIndexChanged += new System.EventHandler(this.monsterFilesListBox_SelectedIndexChanged);
            // 
            // monsterTilePanel
            // 
            this.monsterTilePanel.BackColor = System.Drawing.Color.Teal;
            this.monsterTilePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monsterTilePanel.Location = new System.Drawing.Point(0, 25);
            this.monsterTilePanel.Name = "monsterTilePanel";
            this.monsterTilePanel.Size = new System.Drawing.Size(640, 353);
            this.monsterTilePanel.TabIndex = 1;
            this.monsterTilePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.monsterTilePanel_Paint);
            // 
            // animationTimer
            // 
            this.animationTimer.Interval = 200;
            this.animationTimer.Tick += new System.EventHandler(this.animationTimer_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.monsterTileLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 378);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(640, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // monsterTileLabel
            // 
            this.monsterTileLabel.Name = "monsterTileLabel";
            this.monsterTileLabel.Size = new System.Drawing.Size(88, 17);
            this.monsterTileLabel.Text = "No tile selected";
            // 
            // MonstersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 400);
            this.Controls.Add(this.monsterTilePanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.Name = "MonstersForm";
            this.ShowIcon = false;
            this.Text = "Monsters";
            this.Load += new System.EventHandler(this.MonstersForm_Load);
            this.VisibleChanged += new System.EventHandler(this.MonstersForm_VisibleChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox monsterFilesListBox;
        private System.Windows.Forms.DoubleBufferedPanel monsterTilePanel;
        private System.Windows.Forms.Timer animationTimer;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel monsterTileLabel;
    }
}