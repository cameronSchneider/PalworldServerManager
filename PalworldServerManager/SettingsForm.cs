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
    public partial class SettingsForm : Form
    {
        public string steamInstallPath = "";
        public string defaultServerInstallPath = "";

        public SettingsForm(UserSettings settings)
        {
            InitializeComponent();

            steamInstallPath = settings.userSettingsDict["steamInstallDir"];
            defaultServerInstallPath = settings.userSettingsDict["defaultServerDir"];

            steamText.Text = steamInstallPath;
            installText.Text = defaultServerInstallPath;
        }

        private bool ValidateSettings(out string err)
        {
            if(steamInstallPath == "")
            {
                err = "Error: Steam install path cannot be empty!";
                return false;
            }
            else if(!Directory.Exists(steamInstallPath + MainForm.DEFAULT_PAL_SERVER_DIR_NAME))
            {
                err = string.Format("Error: Could not find default PalServer folder in {0}, please select the correct Steam \"common\" folder.\nHint: it should end with \\steamapps\\common", steamInstallPath);
                return false;
            }

            if(defaultServerInstallPath == "")
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
        private void completeBtn_Click(object sender, EventArgs e)
        {
            string validationErr = "";
            if (ValidateSettings(out validationErr))
            {
                Close();
                DialogResult = DialogResult.OK;
            }
            else
            {
                errorText.Text = validationErr;
            }
        }
    }
}
