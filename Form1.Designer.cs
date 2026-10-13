using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace YTDLP_GUI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private Button btnSettings;
        private Panel panelMain;
        private TabControl tabControl;
        private TabPage tabDownloader;
        private TabPage tabBrowser;

        private TextBox txtUrl;
        private Button btnAddToQueue;
        private Button btnDownloadNow;
        private ListBox lstQueue;
        private PictureBox picThumbnail;
        private ComboBox cmbFormat;
        private CheckBox chkDarkMode;
        private TextBox txtLog;
        private ProgressBar progressBar;
        private Panel bannerPanel;
        private WebView2 webBanner;

        private WebView2 youtubeBrowser;
        private Button btnSendToDownloader;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.btnSettings = new Button();
            this.panelMain = new Panel();
            this.tabControl = new TabControl();
            this.tabDownloader = new TabPage();
            this.tabBrowser = new TabPage();

            this.txtUrl = new TextBox();
            this.btnAddToQueue = new Button();
            this.btnDownloadNow = new Button();
            this.lstQueue = new ListBox();
            this.picThumbnail = new PictureBox();
            this.cmbFormat = new ComboBox();
            this.chkDarkMode = new CheckBox();
            this.txtLog = new TextBox();
            this.progressBar = new ProgressBar();
            this.bannerPanel = new Panel();
            this.webBanner = new WebView2();

            this.youtubeBrowser = new WebView2();
            this.btnSendToDownloader = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.picThumbnail)).BeginInit();
            this.bannerPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabDownloader.SuspendLayout();
            this.tabBrowser.SuspendLayout();
            this.SuspendLayout();

            // FORM
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Text = "Food yt-dlp YouTube Downloader";

            // SETTINGS BUTTON
            this.btnSettings.Location = new System.Drawing.Point(10, 10);
            this.btnSettings.Size = new System.Drawing.Size(140, 40);
            this.btnSettings.Text = "Settings";
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);

            // MAIN PANEL
            this.panelMain.Location = new System.Drawing.Point(0, 60);
            this.panelMain.Size = new System.Drawing.Size(1000, 640);
            this.panelMain.Controls.Add(this.tabControl);

            // TAB CONTROL
            this.tabControl.Dock = DockStyle.Fill;
            this.tabControl.Controls.Add(this.tabDownloader);
            this.tabControl.Controls.Add(this.tabBrowser);

            // DOWNLOADER TAB
            this.tabDownloader.Text = "Downloader";

            this.txtUrl.Location = new System.Drawing.Point(20, 20);
            this.txtUrl.Size = new System.Drawing.Size(700, 30);

            this.btnAddToQueue.Location = new System.Drawing.Point(740, 20);
            this.btnAddToQueue.Size = new System.Drawing.Size(220, 30);
            this.btnAddToQueue.Text = "Add to queue";
            this.btnAddToQueue.Click += new System.EventHandler(this.btnAddToQueue_Click);

            this.cmbFormat.Location = new System.Drawing.Point(20, 60);
            this.cmbFormat.Size = new System.Drawing.Size(200, 30);

            this.chkDarkMode.Location = new System.Drawing.Point(240, 60);
            this.chkDarkMode.Size = new System.Drawing.Size(150, 30);
            this.chkDarkMode.Text = "Dark mode";
            this.chkDarkMode.CheckedChanged += new System.EventHandler(this.chkDarkMode_CheckedChanged);

            this.btnDownloadNow.Location = new System.Drawing.Point(740, 60);
            this.btnDownloadNow.Size = new System.Drawing.Size(220, 30);
            this.btnDownloadNow.Text = "Download queue";
            this.btnDownloadNow.Click += new System.EventHandler(this.btnDownloadNow_Click);

            this.lstQueue.Location = new System.Drawing.Point(20, 110);
            this.lstQueue.Size = new System.Drawing.Size(350, 250);

            this.picThumbnail.Location = new System.Drawing.Point(390, 110);
            this.picThumbnail.Size = new System.Drawing.Size(350, 250);
            this.picThumbnail.SizeMode = PictureBoxSizeMode.Zoom;

            this.txtLog.Location = new System.Drawing.Point(20, 380);
            this.txtLog.Size = new System.Drawing.Size(720, 200);
            this.txtLog.Multiline = true;
            this.txtLog.ScrollBars = ScrollBars.Vertical;

            this.progressBar.Location = new System.Drawing.Point(20, 590);
            this.progressBar.Size = new System.Drawing.Size(720, 25);

            this.bannerPanel.Location = new System.Drawing.Point(760, 380);
            this.bannerPanel.Size = new System.Drawing.Size(200, 235);
            this.bannerPanel.BackColor = System.Drawing.Color.LightGray;
            this.bannerPanel.Controls.Add(this.webBanner);

            this.tabDownloader.Controls.Add(this.txtUrl);
            this.tabDownloader.Controls.Add(this.btnAddToQueue);
            this.tabDownloader.Controls.Add(this.cmbFormat);
            this.tabDownloader.Controls.Add(this.chkDarkMode);
            this.tabDownloader.Controls.Add(this.btnDownloadNow);
            this.tabDownloader.Controls.Add(this.lstQueue);
            this.tabDownloader.Controls.Add(this.picThumbnail);
            this.tabDownloader.Controls.Add(this.txtLog);
            this.tabDownloader.Controls.Add(this.progressBar);
            this.tabDownloader.Controls.Add(this.bannerPanel);

            // BROWSER TAB
            this.tabBrowser.Text = "Browser";
            this.youtubeBrowser.Dock = DockStyle.Fill;
            this.tabBrowser.Controls.Add(this.youtubeBrowser);

            this.btnSendToDownloader.Text = "Use current URL";
            this.btnSendToDownloader.Dock = DockStyle.Bottom;
            this.btnSendToDownloader.Height = 40;
            this.btnSendToDownloader.Click += new System.EventHandler(this.btnSendToDownloader_Click);
            this.tabBrowser.Controls.Add(this.btnSendToDownloader);

            // ADD CONTROLS TO FORM
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.panelMain);

            ((System.ComponentModel.ISupportInitialize)(this.picThumbnail)).EndInit();
            this.bannerPanel.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabDownloader.ResumeLayout(false);
            this.tabDownloader.PerformLayout();
            this.tabBrowser.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
