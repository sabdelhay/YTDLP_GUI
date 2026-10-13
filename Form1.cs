using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTDLP_GUI.Helpers;

namespace YTDLP_GUI
{
    public partial class Form1 : Form
    {
        private readonly Queue<string> downloadQueue = new Queue<string>();
        private bool isDownloading = false;

        public Form1()
        {
            InitializeComponent();
            this.Load += async (s, e) => await Form1_Load(s, e);

        }

        private async Task Form1_Load(object sender, EventArgs e)
{
    InitUiLogic();

    try
    {
        await DependencyManager.EnsureDependenciesAsync(txtLog);
        txtLog.AppendText("Dependencies ready.\r\n");
    }
    catch (Exception ex)
    {
        MessageBox.Show("Failed to install dependencies:\r\n" + ex.Message);
    }
}

        private void InitUiLogic()
        {
            cmbFormat.Items.Clear();
            cmbFormat.Items.Add("Best Quality (MP4)");
            cmbFormat.Items.Add("1080p (MP4)");
            cmbFormat.Items.Add("720p (MP4)");
            cmbFormat.Items.Add("4K (MP4)");
            cmbFormat.Items.Add("Audio Only (MP3)");
            cmbFormat.Items.Add("Audio Only (M4A)");
            cmbFormat.SelectedIndex = 0;

            txtLog.Text = "Ready.\r\n";
        }

        private void btnAddToQueue_Click(object sender, EventArgs e)
        {
            var url = txtUrl.Text.Trim();
            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Please enter a URL.");
                return;
            }

            downloadQueue.Enqueue(url);
            lstQueue.Items.Add(url);
            txtLog.AppendText($"Added to queue: {url}\r\n");

            LoadThumbnail(url);
        }

        private async void LoadThumbnail(string url)
        {
            try
            {
                string videoId = ExtractVideoId(url);
                if (string.IsNullOrEmpty(videoId))
                    return;

                string thumbUrl = $"https://img.youtube.com/vi/{videoId}/hqdefault.jpg";

                using var http = new HttpClient();
                var data = await http.GetByteArrayAsync(thumbUrl);
                using var ms = new MemoryStream(data);
                picThumbnail.Image = System.Drawing.Image.FromStream(ms);
            }
            catch
            {
                // ignore thumbnail errors
            }
        }

        private string ExtractVideoId(string url)
        {
            var match = Regex.Match(url, @"v=([a-zA-Z0-9_\-]+)");
            if (match.Success)
                return match.Groups[1].Value;

            match = Regex.Match(url, @"youtu\.be/([a-zA-Z0-9_\-]+)");
            if (match.Success)
                return match.Groups[1].Value;

            return string.Empty;
        }

        private void btnDownloadNow_Click(object sender, EventArgs e)
        {
            if (downloadQueue.Count == 0)
            {
                MessageBox.Show("Queue is empty.");
                return;
            }

            if (isDownloading)
            {
                MessageBox.Show("Already downloading.");
                return;
            }

            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;

                string outputFolder = fbd.SelectedPath;
                isDownloading = true;
                DownloadNextInQueue(outputFolder);
            }
        }

        private void DownloadNextInQueue(string outputFolder)
        {
            if (downloadQueue.Count == 0)
            {
                txtLog.AppendText("\r\nAll downloads completed.\r\n");
                isDownloading = false;
                progressBar.Value = 100;
                return;
            }

            string url = downloadQueue.Dequeue();
            lstQueue.Items.RemoveAt(0);

            txtLog.AppendText($"Starting download: {url}\r\n");
            progressBar.Value = 0;

            string formatArg = cmbFormat.SelectedIndex switch
                {
                    0 => "-f \"bv*[ext=mp4]+ba[ext=m4a]/bv*+ba\" --merge-output-format mp4", // Best MP4 with audio
                    1 => "-f \"bv*[height=1080]+ba[ext=m4a]/bv*+ba\" --merge-output-format mp4",
                    2 => "-f \"bv*[height=720]+ba[ext=m4a]/bv*+ba\" --merge-output-format mp4",
                    3 => "-f \"bv*[height>=2160]+ba[ext=m4a]/bv*+ba\" --merge-output-format mp4",
                    4 => "--extract-audio --audio-format mp3",
                    5 => "-f \"ba[ext=m4a]\"",
                    _ => "-f \"bv*[ext=mp4]+ba[ext=m4a]/bv*+ba\" --merge-output-format mp4"
                };


            switch (cmbFormat.SelectedIndex)
            {
                case 0: // Best Quality
                    formatArg = "-f bestvideo+bestaudio --merge-output-format mp4";
                    break;

                case 1: // 1080p
                    formatArg = "-f \"bestvideo[height=1080]+bestaudio\" --merge-output-format mp4";
                    break;

                case 2: // 720p
                    formatArg = "-f \"bestvideo[height=720]+bestaudio\" --merge-output-format mp4";
                    break;

                case 3: // 4K
                    formatArg = "-f \"bestvideo[height>=2160]+bestaudio\" --merge-output-format mp4";
                    break;

                case 4: // MP3
                    formatArg = "--extract-audio --audio-format mp3";
                    break;

                case 5: // M4A
                    formatArg = "-f bestaudio[ext=m4a]";
                    break;
            }

            string ytDlpExe = DependencyManager.GetYtDlpPath();
            string ffmpegExe = DependencyManager.GetFfmpegPath();

            var psi = new ProcessStartInfo
            {
                FileName = ytDlpExe,
                Arguments =
                    $"{formatArg} " +
                    $"--ffmpeg-location \"{ffmpegExe}\" " +
                    $"\"{url}\" " +
                    $"-o \"{outputFolder}\\%(title)s.%(ext)s\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            var p = new Process { StartInfo = psi };

            p.OutputDataReceived += (s, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    txtLog.Invoke(new Action(() =>
                    {
                        txtLog.AppendText(e.Data + "\r\n");
                        UpdateProgressFromOutput(e.Data);
                    }));
                }
            };

            p.ErrorDataReceived += (s, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    txtLog.Invoke(new Action(() =>
                    {
                        txtLog.AppendText("ERROR: " + e.Data + "\r\n");
                    }));
                }
            };

            p.EnableRaisingEvents = true;
            p.Exited += (s, e) =>
            {
                txtLog.Invoke(new Action(() =>
                {
                    txtLog.AppendText("\r\nDownload finished.\r\n");
                    progressBar.Value = 100;
                }));

                this.Invoke(new Action(() => DownloadNextInQueue(outputFolder)));
            };

            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
        }

        private void UpdateProgressFromOutput(string line)
        {
            var match = Regex.Match(line, @"(\d+(\.\d+)?)%");
            if (match.Success)
            {
                if (double.TryParse(match.Groups[1].Value, out double percent))
                {
                    int value = (int)Math.Max(0, Math.Min(100, percent));
                    progressBar.Value = value;
                }
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            var settings = new SettingsPage();
            settings.Dock = DockStyle.Fill;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(settings);
        }

        public void ShowMainTabs()
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(tabControl);
        }

        private void chkDarkMode_CheckedChanged(object sender, EventArgs e)
        {
            // simple theme toggle
            var dark = chkDarkMode.Checked;
            var back = dark ? System.Drawing.Color.FromArgb(30, 30, 30) : System.Drawing.SystemColors.Control;
            var fore = dark ? System.Drawing.Color.White : System.Drawing.Color.Black;

            this.BackColor = back;
            foreach (Control c in this.Controls)
            {
                c.BackColor = back;
                c.ForeColor = fore;
            }
        }

        private void btnSendToDownloader_Click(object sender, EventArgs e)
        {
            try
            {
                var url = youtubeBrowser.Source?.ToString();
                if (!string.IsNullOrWhiteSpace(url))
                {
                    txtUrl.Text = url;
                    tabControl.SelectedTab = tabDownloader;
                }
            }
            catch
            {
                // ignore
            }
        }
    }
}
