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
        private string newServerPath = "";
        private string newServerArgs = "";
        private string newServerPort = "";
        private string newServerName = "";

        public KnownServerRow newServerRecord = null;

        public AddServerForm()
        {
            InitializeComponent();
        }

        private void completeBtn_Click(object sender, EventArgs e)
        {
            newServerRecord = new KnownServerRow();
            newServerRecord.ServerPath = newServerPath;
            newServerRecord.ServerPort = newServerPort;
            newServerRecord.ServerName = newServerName;
            newServerRecord.ServerLaunchArgs = newServerArgs;

            ((MainForm)Owner).AddServerFinished(newServerRecord);

            this.Close();
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
