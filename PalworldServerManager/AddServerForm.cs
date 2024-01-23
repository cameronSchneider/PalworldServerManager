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
    public partial class AddServerForm : Form
    {
        public string newServerPath = "";
        public string newServerArgs = "";
        public string newServerPort = "";
        public string newServerName = "";

        private string defaultInstallDir = "";

        public AddServerForm(string defaultDir, bool isImporting = false)
        {
            InitializeComponent();

            defaultInstallDir = defaultDir;

            if(defaultInstallDir != "")
            {
                newServerPath = defaultInstallDir;
                folderBrowserDialog1.SelectedPath = defaultInstallDir;
                serverPathTxt.Text = defaultInstallDir;
            }

            if(isImporting) 
            {
                this.Text = "Import Existing Information";
            }
        }

        private void choosePathBtn_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                serverPathTxt.Text = folderBrowserDialog1.SelectedPath;
                newServerPath = folderBrowserDialog1.SelectedPath;
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
