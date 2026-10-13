using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTDLP_GUI.Helpers
{
    internal static class DependencyManager
    {
        private static readonly string BaseDir =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FoodYtDlp");

        private static readonly string YtDlpDir = Path.Combine(BaseDir, "yt-dlp");
        private static readonly string FfmpegDir = Path.Combine(BaseDir, "ffmpeg");

        public static async Task EnsureDependenciesAsync(TextBox log)
        {
            Directory.CreateDirectory(BaseDir);
            await UpdateYtDlpIfOld(log);
            await UpdateFfmpegIfOld(log);
        }

        private static async Task UpdateYtDlpIfOld(TextBox log)
        {
            Directory.CreateDirectory(YtDlpDir);
            string exePath = Path.Combine(YtDlpDir, "yt-dlp.exe");

            if (File.Exists(exePath) && new FileInfo(exePath).LastWriteTime > DateTime.Now.AddDays(-7))
            {
                log.AppendText("yt-dlp is up to date.\r\n");
                return;
            }

            log.AppendText("Installing/updating yt-dlp...\r\n");

            string url = "https://github.com/yt-dlp/yt-dlp/releases/latest/download/yt-dlp.exe";

            using var http = new HttpClient();
            var data = await http.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(exePath, data);

            log.AppendText("yt-dlp installed/updated.\r\n");
        }

        private static async Task UpdateFfmpegIfOld(TextBox log)
        {
            Directory.CreateDirectory(FfmpegDir);

            var existing = Directory.GetFiles(FfmpegDir, "ffmpeg.exe", SearchOption.AllDirectories);
            if (existing.Length > 0 && new FileInfo(existing[0]).LastWriteTime > DateTime.Now.AddDays(-30))
            {
                log.AppendText("FFmpeg is up to date.\r\n");
                return;
            }

            log.AppendText("Installing/updating FFmpeg...\r\n");

            string zipPath = Path.Combine(FfmpegDir, "ffmpeg.zip");

            using (var http = new HttpClient())
            {
                var data = await http.GetByteArrayAsync("https://www.gyan.dev/ffmpeg/builds/ffmpeg-release-essentials.zip");
                await File.WriteAllBytesAsync(zipPath, data);
            }

            ZipFile.ExtractToDirectory(zipPath, FfmpegDir, true);
            File.Delete(zipPath);

            var ffmpegFiles = Directory.GetFiles(FfmpegDir, "ffmpeg.exe", SearchOption.AllDirectories);
            if (ffmpegFiles.Length == 0)
                throw new Exception("FFmpeg not found after extraction.");

            string ffmpegExe = ffmpegFiles[0];
            File.WriteAllText(Path.Combine(FfmpegDir, "ffmpeg_path.txt"), ffmpegExe);

            log.AppendText("FFmpeg installed/updated.\r\n");
        }

        public static string GetYtDlpPath()
        {
            string exePath = Path.Combine(YtDlpDir, "yt-dlp.exe");
            if (!File.Exists(exePath))
                throw new Exception("yt-dlp not found. Run EnsureDependenciesAsync first.");
            return exePath;
        }

        public static string GetFfmpegPath()
        {
            string pathFile = Path.Combine(FfmpegDir, "ffmpeg_path.txt");
            if (File.Exists(pathFile))
                return File.ReadAllText(pathFile);

            var ffmpegFiles = Directory.GetFiles(FfmpegDir, "ffmpeg.exe", SearchOption.AllDirectories);
            if (ffmpegFiles.Length == 0)
                throw new Exception("FFmpeg not found.");
            return ffmpegFiles[0];
        }
    }
}
