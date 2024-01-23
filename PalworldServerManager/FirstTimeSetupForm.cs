using ApplicationDataUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PalworldServerManager
{
    public partial class FirstTimeSetupForm : Form
    {
        public string steamInstallPath = "";
        public string defaultServerInstallPath = "";

        private static string DEFAULT_PAL_SERVER_DIR_NAME = "\\PalServer";

        public FirstTimeSetupForm(UserSettings settings)
        {
            InitializeComponent();

            steamInstallPath = settings.userSettingsDict["steamInstallDir"];
            defaultServerInstallPath = settings.userSettingsDict["defaultServerDir"];

            steamText.Text = steamInstallPath;
            installText.Text = defaultServerInstallPath;
        }

        private bool ValidateSteamInstallHasPalServerFolder()
        {
            return Directory.Exists(steamInstallPath + DEFAULT_PAL_SERVER_DIR_NAME);
        }

        private void steamInstallBtn_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                steamInstallPath = folderBrowserDialog1.SelectedPath;
                steamText.Text = steamInstallPath;

                if(!ValidateSteamInstallHasPalServerFolder())
                {
                    errorText.Text = "Error: Ensure the stock PalWorld Dedicated Server is installed via Steam before using this tool!";
                    steamInstallPath = "";
                    steamText.Text = "";
                    completeBtn.Enabled = false;
                }
            }

        }

        private void defaultDirBtn_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                defaultServerInstallPath = folderBrowserDialog1.SelectedPath;
                installText.Text = defaultServerInstallPath;
            }

        }
    }
}
