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
