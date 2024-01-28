using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PalworldServerManager
{
    public partial class ProgressBarForm : Form
    {
        public ProgressBarForm(string title)
        {
            InitializeComponent();
            this.Text = title;
        }

        public void SetProgressSafe(int progress) 
        {
            if(progressBar1.InvokeRequired) 
            {
                Action safeWrite = delegate { SetProgressSafe(progress); };
                progressBar1.Invoke(safeWrite);
            }
            else
            {
                progressBar1.Value = progress;
            }
        }

        public void SetDescriptionTextSafe(string description) 
        {
            if (descriptionTxt.InvokeRequired)
            {
                Action safeWrite = delegate { SetDescriptionTextSafe(description); };
                descriptionTxt.Invoke(safeWrite);
            }
            else
            {
                descriptionTxt.Text = description;
            }
        }

        public void Center()
        {
            CenterToParent();
        }
    }
}
