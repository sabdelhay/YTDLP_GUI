using System;
using System.IO;
using System.Windows.Forms;

namespace YTDLP_GUI
{
    public partial class SettingsPage : UserControl
    {
        public SettingsPage()
        {
            InitializeComponent();

            txtOutputFolder.Text = Properties.Settings.Default.DefaultOutputFolder;
            chkAutoUpdate.Checked = Properties.Settings.Default.AutoUpdateDependencies;
            chkDarkMode.Checked = Properties.Settings.Default.DarkMode;
        }
         private void btnBack_Click(object sender, EventArgs e)
        {
            // Find the parent Form1
            Form1 parent = this.FindForm() as Form1;
            if (parent != null)
            {
                parent.ShowMainTabs();
            }
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
                txtOutputFolder.Text = fbd.SelectedPath;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtOutputFolder.Text) &&
                !Directory.Exists(txtOutputFolder.Text))
            {
                MessageBox.Show("Folder does not exist.");
                return;
            }

            Properties.Settings.Default.DefaultOutputFolder = txtOutputFolder.Text;
            Properties.Settings.Default.AutoUpdateDependencies = chkAutoUpdate.Checked;
            Properties.Settings.Default.DarkMode = chkDarkMode.Checked;
            Properties.Settings.Default.Save();

            MessageBox.Show("Settings saved.");
        }

    }
}
