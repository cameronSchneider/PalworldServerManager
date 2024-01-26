namespace PalworldServerManager
{
    partial class MainForm
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.startServerBtn = new System.Windows.Forms.Button();
            this.stopServerBtn = new System.Windows.Forms.Button();
            this.addServerBtn = new System.Windows.Forms.Button();
            this.removeServerBtn = new System.Windows.Forms.Button();
            this.editServerBtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSettingsOption = new System.Windows.Forms.ToolStripMenuItem();
            this.importExistingServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.versionToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.versionTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.serverContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStart = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStop = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDefaultSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editGameConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.serverContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All file" +
    "s (*.*)|*.*";
            this.openFileDialog.Title = "Select a Picture File";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 37);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.77778F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.22222F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1045, 568);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.startServerBtn, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.stopServerBtn, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.addServerBtn, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.removeServerBtn, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.editServerBtn, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(150, 435);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // startServerBtn
            // 
            this.startServerBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startServerBtn.Location = new System.Drawing.Point(3, 3);
            this.startServerBtn.Name = "startServerBtn";
            this.startServerBtn.Size = new System.Drawing.Size(144, 81);
            this.startServerBtn.TabIndex = 0;
            this.startServerBtn.Text = "Start Selected Server";
            this.startServerBtn.UseVisualStyleBackColor = true;
            this.startServerBtn.Click += new System.EventHandler(this.startServerBtn_Click);
            // 
            // stopServerBtn
            // 
            this.stopServerBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stopServerBtn.Location = new System.Drawing.Point(3, 90);
            this.stopServerBtn.Name = "stopServerBtn";
            this.stopServerBtn.Size = new System.Drawing.Size(144, 81);
            this.stopServerBtn.TabIndex = 1;
            this.stopServerBtn.Text = "Stop Selected Server";
            this.stopServerBtn.UseVisualStyleBackColor = true;
            this.stopServerBtn.Click += new System.EventHandler(this.stopServerBtn_Click);
            // 
            // addServerBtn
            // 
            this.addServerBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addServerBtn.Location = new System.Drawing.Point(3, 177);
            this.addServerBtn.Name = "addServerBtn";
            this.addServerBtn.Size = new System.Drawing.Size(144, 81);
            this.addServerBtn.TabIndex = 2;
            this.addServerBtn.Text = "Add Server";
            this.addServerBtn.UseVisualStyleBackColor = true;
            this.addServerBtn.Click += new System.EventHandler(this.addServerBtn_Click);
            // 
            // removeServerBtn
            // 
            this.removeServerBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.removeServerBtn.Location = new System.Drawing.Point(3, 351);
            this.removeServerBtn.Name = "removeServerBtn";
            this.removeServerBtn.Size = new System.Drawing.Size(144, 81);
            this.removeServerBtn.TabIndex = 3;
            this.removeServerBtn.Text = "Remove Selected Server";
            this.removeServerBtn.UseVisualStyleBackColor = true;
            this.removeServerBtn.Click += new System.EventHandler(this.removeServerBtn_Click);
            // 
            // editServerBtn
            // 
            this.editServerBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editServerBtn.Location = new System.Drawing.Point(3, 264);
            this.editServerBtn.Name = "editServerBtn";
            this.editServerBtn.Size = new System.Drawing.Size(144, 81);
            this.editServerBtn.TabIndex = 4;
            this.editServerBtn.Text = "Edit Selected Server";
            this.editServerBtn.UseVisualStyleBackColor = true;
            this.editServerBtn.Click += new System.EventHandler(this.editServerBtn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(159, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(883, 435);
            this.dataGridView1.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.660377F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.33963F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1051, 608);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1051, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editSettingsOption,
            this.importExistingServerToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // editSettingsOption
            // 
            this.editSettingsOption.Name = "editSettingsOption";
            this.editSettingsOption.Size = new System.Drawing.Size(189, 22);
            this.editSettingsOption.Text = "Edit Settings";
            this.editSettingsOption.Click += new System.EventHandler(this.editSettingsOption_Click);
            // 
            // importExistingServerToolStripMenuItem
            // 
            this.importExistingServerToolStripMenuItem.Name = "importExistingServerToolStripMenuItem";
            this.importExistingServerToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.importExistingServerToolStripMenuItem.Text = "Import Existing Server";
            this.importExistingServerToolStripMenuItem.Click += new System.EventHandler(this.importExistingServerToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStrip,
            this.versionToolStrip});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // helpToolStrip
            // 
            this.helpToolStrip.Name = "helpToolStrip";
            this.helpToolStrip.Size = new System.Drawing.Size(112, 22);
            this.helpToolStrip.Text = "Help";
            this.helpToolStrip.Click += new System.EventHandler(this.helpToolStrip_Click);
            // 
            // versionToolStrip
            // 
            this.versionToolStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.versionTextBox});
            this.versionToolStrip.Name = "versionToolStrip";
            this.versionToolStrip.Size = new System.Drawing.Size(112, 22);
            this.versionToolStrip.Text = "Version";
            this.versionToolStrip.Click += new System.EventHandler(this.versionToolStrip_Click);
            // 
            // versionTextBox
            // 
            this.versionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.versionTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.versionTextBox.Name = "versionTextBox";
            this.versionTextBox.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.versionTextBox.ReadOnly = true;
            this.versionTextBox.Size = new System.Drawing.Size(200, 16);
            // 
            // serverContextMenu
            // 
            this.serverContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuStart,
            this.contextMenuStop,
            this.contextMenuEdit,
            this.contextMenuRemove,
            this.openFileLocationToolStripMenuItem,
            this.openConfigToolStripMenuItem,
            this.openDefaultSettingsToolStripMenuItem,
            this.editGameConfigToolStripMenuItem});
            this.serverContextMenu.Name = "contextMenuStrip1";
            this.serverContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.serverContextMenu.Size = new System.Drawing.Size(184, 180);
            // 
            // contextMenuStart
            // 
            this.contextMenuStart.Name = "contextMenuStart";
            this.contextMenuStart.Size = new System.Drawing.Size(183, 22);
            this.contextMenuStart.Text = "Start";
            this.contextMenuStart.Click += new System.EventHandler(this.contextMenuStart_Click);
            // 
            // contextMenuStop
            // 
            this.contextMenuStop.Name = "contextMenuStop";
            this.contextMenuStop.Size = new System.Drawing.Size(183, 22);
            this.contextMenuStop.Text = "Stop";
            this.contextMenuStop.Click += new System.EventHandler(this.contextMenuStop_Click);
            // 
            // contextMenuEdit
            // 
            this.contextMenuEdit.Name = "contextMenuEdit";
            this.contextMenuEdit.Size = new System.Drawing.Size(183, 22);
            this.contextMenuEdit.Text = "Edit Server Settings";
            this.contextMenuEdit.Click += new System.EventHandler(this.contextMenuEdit_Click);
            // 
            // contextMenuRemove
            // 
            this.contextMenuRemove.Name = "contextMenuRemove";
            this.contextMenuRemove.Size = new System.Drawing.Size(183, 22);
            this.contextMenuRemove.Text = "Remove";
            this.contextMenuRemove.Click += new System.EventHandler(this.contextMenuRemove_Click);
            // 
            // openFileLocationToolStripMenuItem
            // 
            this.openFileLocationToolStripMenuItem.Name = "openFileLocationToolStripMenuItem";
            this.openFileLocationToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.openFileLocationToolStripMenuItem.Text = "Open File Location";
            this.openFileLocationToolStripMenuItem.Click += new System.EventHandler(this.openFileLocationToolStripMenuItem_Click);
            // 
            // openConfigToolStripMenuItem
            // 
            this.openConfigToolStripMenuItem.Name = "openConfigToolStripMenuItem";
            this.openConfigToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.openConfigToolStripMenuItem.Text = "Open Config";
            this.openConfigToolStripMenuItem.Click += new System.EventHandler(this.openConfigToolStripMenuItem_Click);
            // 
            // openDefaultSettingsToolStripMenuItem
            // 
            this.openDefaultSettingsToolStripMenuItem.Name = "openDefaultSettingsToolStripMenuItem";
            this.openDefaultSettingsToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.openDefaultSettingsToolStripMenuItem.Text = "Open Default Config";
            this.openDefaultSettingsToolStripMenuItem.Click += new System.EventHandler(this.openDefaultSettingsToolStripMenuItem_Click);
            // 
            // editGameConfigToolStripMenuItem
            // 
            this.editGameConfigToolStripMenuItem.Name = "editGameConfigToolStripMenuItem";
            this.editGameConfigToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.editGameConfigToolStripMenuItem.Text = "Edit Game Config";
            this.editGameConfigToolStripMenuItem.Click += new System.EventHandler(this.editGameConfigToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 608);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Name = "MainForm";
            this.Text = "Palworld Server Manager";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.serverContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button startServerBtn;
        private System.Windows.Forms.Button stopServerBtn;
        private System.Windows.Forms.Button addServerBtn;
        private System.Windows.Forms.Button removeServerBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSettingsOption;
        private System.Windows.Forms.ToolStripMenuItem importExistingServerToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip serverContextMenu;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStart;
        private System.Windows.Forms.ToolStripMenuItem contextMenuStop;
        private System.Windows.Forms.ToolStripMenuItem contextMenuEdit;
        private System.Windows.Forms.ToolStripMenuItem contextMenuRemove;
        private System.Windows.Forms.Button editServerBtn;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStrip;
        private System.Windows.Forms.ToolStripMenuItem versionToolStrip;
        private System.Windows.Forms.ToolStripTextBox versionTextBox;
        private System.Windows.Forms.ToolStripMenuItem openFileLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDefaultSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editGameConfigToolStripMenuItem;
    }
}

