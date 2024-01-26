using System;
using System.IO;
using System.Windows.Forms;

namespace PalworldServerManager
{
    public partial class ImportServerForm : Form
    {
        public string newServerPath = "";
        public string newServerArgs = "";
        public string newServerPort = "";
        public string newServerName = "";
        public string existingServerPath = "";

        public ImportServerForm(string defaultInstallDir)
        {
            InitializeComponent();

            if(defaultInstallDir != "")
            {
                newServerPath = defaultInstallDir;
                newPathTxt.Text = newServerPath;
            }
        }

        private bool ValidateSettings(out string err)
        {
            if (existingServerPath == "")
            {
                err = "Error: Select an existing path to a server installation!";
                return false;
            }
            else if (!File.Exists(existingServerPath + ProgramConstants.SERVER_EXE_NAME))
            {
                err = string.Format("Error: Existing path {0} does not contain PalServer.exe. Please select a valid PalServer installation.", existingServerPath);
                return false;
            }

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

            if(newServerPort == "0" || newServerPort == "")
            {
                err = "Error: Server port cannot be 0.";
                return false;
            }

            if (MainForm.GetInstance().DoesServerNameExist(newServerName))
            {
                err = string.Format("Error: Server name {0} is already in use, please enter another.", newServerName);
                return false;
            }

            err = "";
            return true;
        }

        private void existingPathBtn_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                existingServerPath = folderBrowserDialog1.SelectedPath;
                existingPathTxt.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void newServerPathBtn_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                newPathTxt.Text = folderBrowserDialog1.SelectedPath;
                newServerPath = folderBrowserDialog1.SelectedPath;
            }
        }

        private void completeBtn_Click(object sender, EventArgs e)
        {
            string promptText = string.Format("Are you sure you want to import {0}? \nThis will migrate the existing server folder to the new location, and delete the old folder.", newServerName);

            ConfirmationPrompt confirmPrompt = new ConfirmationPrompt(promptText);
            confirmPrompt.Text = "Confirm Import Server";

            DialogResult result = confirmPrompt.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string validationErr = "";
                if(ValidateSettings(out validationErr))
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

        private void portSelectTxt_ValueChanged(object sender, EventArgs e)
        {
            newServerPort = portSelectTxt.Text;
        }

        private void argsInput_TextChanged(object sender, EventArgs e)
        {
            newServerArgs = argsInput.Text;
        }

        private void nameInput_TextChanged(object sender, EventArgs e)
        {
            newServerName = nameInput.Text;
        }
    }
}
