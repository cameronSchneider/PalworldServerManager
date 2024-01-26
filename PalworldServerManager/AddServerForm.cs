using ApplicationDataUtilities;
using System;
using System.IO;
using System.Windows.Forms;

namespace PalworldServerManager
{
    public partial class AddServerForm : Form
    {
        public string newServerPath = "";
        public string newServerArgs = "";
        public string newServerPort = "";
        public string newServerName = "";

        private string defaultInstallDir = "";

        public class AddServerFormOptions
        {
            public string defaultDir = "";
            public bool isEditMenu = false;
            public KnownServer editData;
        }

        public AddServerForm(AddServerFormOptions options)
        {
            InitializeComponent();

            defaultInstallDir = options.defaultDir;

            if(defaultInstallDir != "")
            {
                newServerPath = defaultInstallDir;
                folderBrowserDialog1.SelectedPath = defaultInstallDir;
                serverPathTxt.Text = defaultInstallDir;
            }

            if(options.isEditMenu) 
            {
                SetupEditMenu(options.editData);
            }
        }

        private bool ValidateSettings(out string err)
        {
            if (newServerPath == "")
            {
                err = "Error: Select an new path to install this server on!";
                return false;
            }
            else if (File.Exists(newServerPath + ProgramConstants.SERVER_EXE_NAME))
            {
                err = string.Format("Error: New server path {0} already contains PalServer.exe, select a path without an existing installation.", newServerPath);
                return false;
            }

            if (newServerName == "")
            {
                err = "Error: Name cannot be empty!";
                return false;
            }

            if (newServerPort == "0" || newServerPort == "")
            {
                err = "Error: Server port cannot be 0.";
                return false;
            }

            if(MainForm.GetInstance().DoesServerNameExist(newServerName)) 
            {
                err = string.Format("Error: Server name {0} is already in use, please enter another.", newServerName);
                return false;
            }

            err = "";
            return true;
        }

        private void SetupEditMenu(KnownServer dataToUse)
        {
            this.Text = "Edit Server Information";

            newServerPath = dataToUse.ServerPath;
            newServerName = dataToUse.ServerName;
            newServerPort = dataToUse.ServerPort;
            newServerArgs = dataToUse.ServerLaunchArgs;

            serverPathTxt.Text = dataToUse.ServerPath;
            nameInput.Text = dataToUse.ServerName;
            portSelectTxt.Text = dataToUse.ServerPort;
            argsInput.Text = dataToUse.ServerLaunchArgs;

            folderBrowserDialog1.SelectedPath = dataToUse.ServerPath;
        }

        private void choosePathBtn_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                serverPathTxt.Text = folderBrowserDialog1.SelectedPath;
                newServerPath = folderBrowserDialog1.SelectedPath;
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

        private void argsInput_TextChanged(object sender, EventArgs e)
        {
            newServerArgs = argsInput.Text;
        }

        private void nameInput_TextChanged(object sender, EventArgs e)
        {
            newServerName = nameInput.Text;
        }

        private void portSelectTxt_ValueChanged(object sender, EventArgs e)
        {
            newServerPort = portSelectTxt.Text;
        }
    }
}
