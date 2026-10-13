namespace YTDLP_GUI
{
    partial class SettingsPage
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.CheckBox chkAutoUpdate;
        private System.Windows.Forms.CheckBox chkDarkMode;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblOutputFolder;
        private System.Windows.Forms.Button btnBack;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
{
    this.btnBack = new System.Windows.Forms.Button();
    this.lblOutputFolder = new System.Windows.Forms.Label();
    this.txtOutputFolder = new System.Windows.Forms.TextBox();
    this.btnBrowse = new System.Windows.Forms.Button();
    this.chkAutoUpdate = new System.Windows.Forms.CheckBox();
    this.chkDarkMode = new System.Windows.Forms.CheckBox();
    this.btnSave = new System.Windows.Forms.Button();

    this.SuspendLayout();

    // Back button
    this.btnBack.Location = new System.Drawing.Point(10, 10);
    this.btnBack.Size = new System.Drawing.Size(120, 35);
    this.btnBack.Text = "Back";
    this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
    this.Controls.Add(this.btnBack);

    // Default output folder label
    this.lblOutputFolder.Location = new System.Drawing.Point(10, 60);
    this.lblOutputFolder.Size = new System.Drawing.Size(200, 25);
    this.lblOutputFolder.Text = "Default output folder:";
    this.Controls.Add(this.lblOutputFolder);

    // Output folder textbox
    this.txtOutputFolder.Location = new System.Drawing.Point(10, 90);
    this.txtOutputFolder.Size = new System.Drawing.Size(250, 30);
    this.Controls.Add(this.txtOutputFolder);

    // Browse button
    this.btnBrowse.Location = new System.Drawing.Point(270, 90);
    this.btnBrowse.Size = new System.Drawing.Size(100, 30);
    this.btnBrowse.Text = "Browse";
    this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
    this.Controls.Add(this.btnBrowse);

    // Auto-update checkbox
    this.chkAutoUpdate.Location = new System.Drawing.Point(10, 140);
    this.chkAutoUpdate.Size = new System.Drawing.Size(250, 30);
    this.chkAutoUpdate.Text = "Auto-update yt-dlp & FFmpeg";
    this.Controls.Add(this.chkAutoUpdate);

    // Dark mode checkbox
    this.chkDarkMode.Location = new System.Drawing.Point(10, 180);
    this.chkDarkMode.Size = new System.Drawing.Size(150, 30);
    this.chkDarkMode.Text = "Dark mode";
    this.Controls.Add(this.chkDarkMode);

    // Save button
    this.btnSave.Location = new System.Drawing.Point(10, 230);
    this.btnSave.Size = new System.Drawing.Size(120, 35);
    this.btnSave.Text = "Save";
    this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
    this.Controls.Add(this.btnSave);

    // SettingsPage size
    this.Size = new System.Drawing.Size(400, 300);

    this.ResumeLayout(false);
    this.PerformLayout();
}

    }
}
