namespace PalworldServerManager
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.errorText = new System.Windows.Forms.Label();
            this.installText = new System.Windows.Forms.Label();
            this.steamInstallBtn = new System.Windows.Forms.Button();
            this.defaultDirBtn = new System.Windows.Forms.Button();
            this.completeBtn = new System.Windows.Forms.Button();
            this.steamText = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.steamToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.defaultToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.steamCmdBtn = new System.Windows.Forms.Button();
            this.steamCmdTxt = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.97495F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.02505F));
            this.tableLayoutPanel1.Controls.Add(this.steamCmdTxt, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.errorText, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.installText, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.steamInstallBtn, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.defaultDirBtn, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.completeBtn, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.steamText, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.steamCmdBtn, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(583, 396);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // errorText
            // 
            this.errorText.AutoSize = true;
            this.errorText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.errorText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorText.Location = new System.Drawing.Point(160, 297);
            this.errorText.Name = "errorText";
            this.errorText.Size = new System.Drawing.Size(420, 99);
            this.errorText.TabIndex = 5;
            // 
            // installText
            // 
            this.installText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.installText.AutoSize = true;
            this.installText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.installText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installText.Location = new System.Drawing.Point(160, 238);
            this.installText.Name = "installText";
            this.installText.Size = new System.Drawing.Size(420, 19);
            this.installText.TabIndex = 4;
            // 
            // steamInstallBtn
            // 
            this.steamInstallBtn.AutoSize = true;
            this.steamInstallBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.steamInstallBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.steamInstallBtn.Location = new System.Drawing.Point(20, 20);
            this.steamInstallBtn.Margin = new System.Windows.Forms.Padding(20);
            this.steamInstallBtn.Name = "steamInstallBtn";
            this.steamInstallBtn.Size = new System.Drawing.Size(117, 59);
            this.steamInstallBtn.TabIndex = 0;
            this.steamInstallBtn.Text = "Select Steam Install Path";
            this.steamToolTip.SetToolTip(this.steamInstallBtn, "This is where a blank install of a Dedicated Server should be.");
            this.steamInstallBtn.UseVisualStyleBackColor = true;
            this.steamInstallBtn.Click += new System.EventHandler(this.steamInstallBtn_Click);
            // 
            // defaultDirBtn
            // 
            this.defaultDirBtn.AutoSize = true;
            this.defaultDirBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.defaultDirBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defaultDirBtn.Location = new System.Drawing.Point(20, 218);
            this.defaultDirBtn.Margin = new System.Windows.Forms.Padding(20);
            this.defaultDirBtn.Name = "defaultDirBtn";
            this.defaultDirBtn.Size = new System.Drawing.Size(117, 59);
            this.defaultDirBtn.TabIndex = 1;
            this.defaultDirBtn.Text = "Select Default Server Install Path";
            this.defaultToolTip.SetToolTip(this.defaultDirBtn, "This is where your new server will be set up.");
            this.defaultDirBtn.UseVisualStyleBackColor = true;
            this.defaultDirBtn.Click += new System.EventHandler(this.defaultDirBtn_Click);
            // 
            // completeBtn
            // 
            this.completeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.completeBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.completeBtn.Location = new System.Drawing.Point(3, 300);
            this.completeBtn.Name = "completeBtn";
            this.completeBtn.Size = new System.Drawing.Size(151, 93);
            this.completeBtn.TabIndex = 2;
            this.completeBtn.Text = "Done";
            this.completeBtn.UseVisualStyleBackColor = false;
            this.completeBtn.Click += new System.EventHandler(this.completeBtn_Click);
            // 
            // steamText
            // 
            this.steamText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.steamText.AutoSize = true;
            this.steamText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.steamText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamText.Location = new System.Drawing.Point(160, 40);
            this.steamText.Name = "steamText";
            this.steamText.Size = new System.Drawing.Size(420, 19);
            this.steamText.TabIndex = 3;
            // 
            // steamCmdBtn
            // 
            this.steamCmdBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.steamCmdBtn.Location = new System.Drawing.Point(20, 119);
            this.steamCmdBtn.Margin = new System.Windows.Forms.Padding(20);
            this.steamCmdBtn.Name = "steamCmdBtn";
            this.steamCmdBtn.Size = new System.Drawing.Size(117, 59);
            this.steamCmdBtn.TabIndex = 6;
            this.steamCmdBtn.Text = "Select SteamCmd Install Path";
            this.steamCmdBtn.UseVisualStyleBackColor = true;
            this.steamCmdBtn.Click += new System.EventHandler(this.steamCmdBtn_Click);
            // 
            // steamCmdTxt
            // 
            this.steamCmdTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.steamCmdTxt.AutoSize = true;
            this.steamCmdTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.steamCmdTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steamCmdTxt.Location = new System.Drawing.Point(160, 139);
            this.steamCmdTxt.Name = "steamCmdTxt";
            this.steamCmdTxt.Size = new System.Drawing.Size(420, 19);
            this.steamCmdTxt.TabIndex = 7;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 396);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SettingsForm";
            this.Text = "Edit Settings";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button steamInstallBtn;
        private System.Windows.Forms.Button defaultDirBtn;
        private System.Windows.Forms.Button completeBtn;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolTip steamToolTip;
        private System.Windows.Forms.ToolTip defaultToolTip;
        private System.Windows.Forms.Label errorText;
        private System.Windows.Forms.Label installText;
        private System.Windows.Forms.Label steamText;
        private System.Windows.Forms.Label steamCmdTxt;
        private System.Windows.Forms.Button steamCmdBtn;
    }
}