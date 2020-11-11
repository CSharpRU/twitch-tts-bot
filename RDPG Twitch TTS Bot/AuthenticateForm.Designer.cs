using System.ComponentModel;
#pragma warning disable 618

namespace RDPG_Twitch_TTS_Bot
{
    partial class AuthenticateForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.twitchWebBrowser = new CefSharp.WinForms.ChromiumWebBrowser();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // twitchWebBrowser
            // 
            this.twitchWebBrowser.ActivateBrowserOnCreation = false;
            this.twitchWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.twitchWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.twitchWebBrowser.Name = "twitchWebBrowser";
            this.twitchWebBrowser.Size = new System.Drawing.Size(800, 450);
            this.twitchWebBrowser.TabIndex = 0;
            // 
            // urlTextBox
            // 
            this.urlTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.urlTextBox.Location = new System.Drawing.Point(0, 0);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.ReadOnly = true;
            this.urlTextBox.Size = new System.Drawing.Size(800, 20);
            this.urlTextBox.TabIndex = 1;
            // 
            // AuthenticateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.urlTextBox);
            this.Controls.Add(this.twitchWebBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AuthenticateForm";
            this.ShowIcon = false;
            this.Text = "Authentication";
            this.Load += new System.EventHandler(this.AuthenticateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox urlTextBox;

        private CefSharp.WinForms.ChromiumWebBrowser twitchWebBrowser;

        #endregion
    }
}