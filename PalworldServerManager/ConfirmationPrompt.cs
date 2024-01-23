using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PalworldServerManager
{
    public partial class ConfirmationPrompt : Form
    {
        public ConfirmationPrompt(string promptText = null)
        {
            InitializeComponent();

            if(promptText != null) 
            {
                promptTextBox.Text = promptText;
            }
        }
    }
}
