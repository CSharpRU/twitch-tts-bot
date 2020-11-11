namespace RDPG_Twitch_TTS_Bot
{
    partial class MainForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.volumeTrackBar = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ttsHistoryListBox = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.skipAllButton = new System.Windows.Forms.Button();
            this.skipButton = new System.Windows.Forms.Button();
            this.voiceComboBox = new System.Windows.Forms.ComboBox();
            this.testTtsButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.volumeTrackBar)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.volumeTrackBar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(725, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(75, 450);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Volume";
            // 
            // volumeTrackBar
            // 
            this.volumeTrackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.volumeTrackBar.Location = new System.Drawing.Point(3, 16);
            this.volumeTrackBar.Maximum = 100;
            this.volumeTrackBar.Name = "volumeTrackBar";
            this.volumeTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.volumeTrackBar.Size = new System.Drawing.Size(69, 431);
            this.volumeTrackBar.TabIndex = 0;
            this.volumeTrackBar.Value = 100;
            this.volumeTrackBar.Scroll += new System.EventHandler(this.volumeTrackBar_Scroll);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ttsHistoryListBox);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.MinimumSize = new System.Drawing.Size(725, 450);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(725, 450);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "TTS";
            // 
            // ttsHistoryListBox
            // 
            this.ttsHistoryListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ttsHistoryListBox.FormattingEnabled = true;
            this.ttsHistoryListBox.Location = new System.Drawing.Point(3, 16);
            this.ttsHistoryListBox.Name = "ttsHistoryListBox";
            this.ttsHistoryListBox.Size = new System.Drawing.Size(719, 390);
            this.ttsHistoryListBox.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.skipAllButton);
            this.groupBox3.Controls.Add(this.skipButton);
            this.groupBox3.Controls.Add(this.voiceComboBox);
            this.groupBox3.Controls.Add(this.testTtsButton);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(3, 406);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(719, 41);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Control";
            // 
            // skipAllButton
            // 
            this.skipAllButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.skipAllButton.Location = new System.Drawing.Point(476, 12);
            this.skipAllButton.Name = "skipAllButton";
            this.skipAllButton.Size = new System.Drawing.Size(75, 23);
            this.skipAllButton.TabIndex = 3;
            this.skipAllButton.Text = "Skip All";
            this.skipAllButton.UseVisualStyleBackColor = true;
            this.skipAllButton.Click += new System.EventHandler(this.skipAllButton_Click);
            // 
            // skipButton
            // 
            this.skipButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.skipButton.Location = new System.Drawing.Point(557, 12);
            this.skipButton.Name = "skipButton";
            this.skipButton.Size = new System.Drawing.Size(75, 23);
            this.skipButton.TabIndex = 2;
            this.skipButton.Text = "Skip";
            this.skipButton.UseVisualStyleBackColor = true;
            this.skipButton.Click += new System.EventHandler(this.skipButton_Click);
            // 
            // voiceComboBox
            // 
            this.voiceComboBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.voiceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.voiceComboBox.FormattingEnabled = true;
            this.voiceComboBox.Location = new System.Drawing.Point(6, 14);
            this.voiceComboBox.Name = "voiceComboBox";
            this.voiceComboBox.Size = new System.Drawing.Size(236, 21);
            this.voiceComboBox.TabIndex = 1;
            this.voiceComboBox.SelectionChangeCommitted += new System.EventHandler(this.voiceComboBox_SelectionChangeCommitted);
            // 
            // testTtsButton
            // 
            this.testTtsButton.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.testTtsButton.Location = new System.Drawing.Point(638, 12);
            this.testTtsButton.Name = "testTtsButton";
            this.testTtsButton.Size = new System.Drawing.Size(75, 23);
            this.testTtsButton.TabIndex = 0;
            this.testTtsButton.Text = "Test";
            this.testTtsButton.UseVisualStyleBackColor = true;
            this.testTtsButton.Click += new System.EventHandler(this.testTtsButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "RuDevPlaysGames Twitch TTS Bot";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.volumeTrackBar)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button skipAllButton;

        private System.Windows.Forms.Button skipButton;

        private System.Windows.Forms.ComboBox voiceComboBox;

        private System.Windows.Forms.ListBox ttsHistoryListBox;

        private System.Windows.Forms.GroupBox groupBox3;

        private System.Windows.Forms.Button testTtsButton;

        private System.Windows.Forms.GroupBox groupBox2;

        private System.Windows.Forms.TrackBar volumeTrackBar;

        private System.Windows.Forms.GroupBox groupBox1;

        #endregion
    }
}