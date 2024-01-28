using ApplicationDataUtilities;
using System;
using System.IO;
using System.Windows.Forms;

namespace PalworldServerManager
{
    public partial class SettingsForm : Form
    {
        public string steamInstallPath = "";
        public string defaultServerInstallPath = "";
        public string steamCmdInstallPath = "";

        public SettingsForm(UserSettings settings)
        {
            InitializeComponent();

            steamInstallPath = settings.userSettingsDict["steamInstallDir"];
            steamCmdInstallPath = settings.userSettingsDict["steamCmdInstallDir"];
            defaultServerInstallPath = settings.userSettingsDict["defaultServerDir"];

            steamText.Text = steamInstallPath;
            steamCmdTxt.Text = steamCmdInstallPath;
            installText.Text = defaultServerInstallPath;
        }

        private bool ValidateSettings(out string err)
        {
            if(steamInstallPath == "")
            {
                err = "Error: Steam install path cannot be empty!";
                return false;
            }
            else if(!Directory.Exists(steamInstallPath + ProgramConstants.DEFAULT_PAL_SERVER_DIR_NAME))
            {
                err = string.Format("Error: Could not find default PalServer folder in {0}, please select the correct Steam \"common\" folder.\nHint: it should end with \\steamapps\\common", steamInstallPath);
                return false;
            }

            if(steamCmdInstallPath == "")
            {
                err = "Error: SteamCmd install path cannot be empty!";
                return false;
            }

            if (defaultServerInstallPath == "")
            {
                err = "Error: Default install path cannot be empty!";
                return false;
            }

            err = "";
            return true;
        }

        private void steamInstallBtn_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                steamInstallPath = folderBrowserDialog1.SelectedPath;
                steamText.Text = steamInstallPath;
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

        private DialogResult ShowSteamCmdWarningIfNeeded()
        {
            string warningMsg = "WARNING: This application requires the usage of SteamCMD for server install & updates, a tool by Valve that is a command-line version of the Steam Client.\n" +
            "This application will auto-install SteamCMD to the specified directory if it is not already installed there.\n" +
            "Press Yes to continue, or Cancel to abort.";

            return MessageBox.Show(warningMsg, "WARNING", MessageBoxButtons.OKCancel);
        }

        private void completeBtn_Click(object sender, EventArgs e)
        {
            string validationErr = "";
            if (ValidateSettings(out validationErr))
            {
                DialogResult result = ShowSteamCmdWarningIfNeeded();
                if (result == DialogResult.OK) 
                {
                    Close();
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    Close();
                    DialogResult = DialogResult.Abort;
                }
            }
            else
            {
                errorText.Text = validationErr;
            }
        }

        private void steamCmdBtn_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                steamCmdInstallPath = folderBrowserDialog1.SelectedPath;
                steamCmdTxt.Text = steamCmdInstallPath;
            }
        }
    }
}
