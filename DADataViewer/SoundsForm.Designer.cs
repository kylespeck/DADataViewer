namespace DADataViewer
{
    partial class SoundsForm
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
            this.soundFilesListBox = new System.Windows.Forms.ComboBox();
            this.playButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // soundFilesListBox
            // 
            this.soundFilesListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.soundFilesListBox.FormattingEnabled = true;
            this.soundFilesListBox.Location = new System.Drawing.Point(12, 12);
            this.soundFilesListBox.Name = "soundFilesListBox";
            this.soundFilesListBox.Size = new System.Drawing.Size(121, 21);
            this.soundFilesListBox.TabIndex = 0;
            // 
            // playButton
            // 
            this.playButton.Image = global::DADataViewer.Properties.Resources.Symbols_Play_16xMD;
            this.playButton.Location = new System.Drawing.Point(139, 10);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(23, 23);
            this.playButton.TabIndex = 1;
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // SoundsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(174, 45);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.soundFilesListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SoundsForm";
            this.ShowIcon = false;
            this.Text = "Sounds";
            this.Load += new System.EventHandler(this.SoundsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox soundFilesListBox;
        private System.Windows.Forms.Button playButton;
    }
}