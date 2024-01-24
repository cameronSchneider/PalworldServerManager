namespace PalworldServerManager
{
    partial class AddServerForm
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
            this.nameInput = new System.Windows.Forms.TextBox();
            this.argsInput = new System.Windows.Forms.TextBox();
            this.choosePathBtn = new System.Windows.Forms.Button();
            this.completeBtn = new System.Windows.Forms.Button();
            this.portSelectTxt = new System.Windows.Forms.NumericUpDown();
            this.serverPathTxt = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.serverPathTip = new System.Windows.Forms.ToolTip(this.components);
            this.errorText = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portSelectTxt)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.61905F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.38095F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.nameInput, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.argsInput, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.choosePathBtn, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.completeBtn, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.portSelectTxt, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.serverPathTxt, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.errorText, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.16129F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.16129F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.16129F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.16129F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.35484F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(420, 307);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // nameInput
            // 
            this.nameInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nameInput.Location = new System.Drawing.Point(119, 202);
            this.nameInput.MaxLength = 18;
            this.nameInput.Name = "nameInput";
            this.nameInput.Size = new System.Drawing.Size(298, 23);
            this.nameInput.TabIndex = 7;
            this.nameInput.TextChanged += new System.EventHandler(this.nameInput_TextChanged);
            // 
            // argsInput
            // 
            this.argsInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.argsInput.Location = new System.Drawing.Point(119, 141);
            this.argsInput.Name = "argsInput";
            this.argsInput.Size = new System.Drawing.Size(298, 23);
            this.argsInput.TabIndex = 5;
            this.argsInput.TextChanged += new System.EventHandler(this.argsInput_TextChanged);
            // 
            // choosePathBtn
            // 
            this.choosePathBtn.AutoSize = true;
            this.choosePathBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.choosePathBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.choosePathBtn.Location = new System.Drawing.Point(10, 10);
            this.choosePathBtn.Margin = new System.Windows.Forms.Padding(10);
            this.choosePathBtn.Name = "choosePathBtn";
            this.choosePathBtn.Size = new System.Drawing.Size(96, 41);
            this.choosePathBtn.TabIndex = 0;
            this.choosePathBtn.Text = "Choose Server Path";
            this.serverPathTip.SetToolTip(this.choosePathBtn, "If you don\'t want this server in the default location, choose a new one.");
            this.choosePathBtn.UseVisualStyleBackColor = true;
            this.choosePathBtn.Click += new System.EventHandler(this.choosePathBtn_Click);
            // 
            // completeBtn
            // 
            this.completeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.completeBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.completeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.completeBtn.Location = new System.Drawing.Point(3, 247);
            this.completeBtn.Name = "completeBtn";
            this.completeBtn.Size = new System.Drawing.Size(110, 57);
            this.completeBtn.TabIndex = 8;
            this.completeBtn.Text = "Done";
            this.completeBtn.UseVisualStyleBackColor = false;
            this.completeBtn.Click += new System.EventHandler(this.completeBtn_Click);
            // 
            // portSelectTxt
            // 
            this.portSelectTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.portSelectTxt.Location = new System.Drawing.Point(119, 80);
            this.portSelectTxt.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portSelectTxt.Name = "portSelectTxt";
            this.portSelectTxt.Size = new System.Drawing.Size(298, 23);
            this.portSelectTxt.TabIndex = 9;
            this.portSelectTxt.ValueChanged += new System.EventHandler(this.portSelectTxt_ValueChanged);
            // 
            // serverPathTxt
            // 
            this.serverPathTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.serverPathTxt.AutoSize = true;
            this.serverPathTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serverPathTxt.Location = new System.Drawing.Point(119, 21);
            this.serverPathTxt.Name = "serverPathTxt";
            this.serverPathTxt.Size = new System.Drawing.Size(298, 19);
            this.serverPathTxt.TabIndex = 10;
            // 
            // errorText
            // 
            this.errorText.AutoSize = true;
            this.errorText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorText.Location = new System.Drawing.Point(119, 244);
            this.errorText.Name = "errorText";
            this.errorText.Size = new System.Drawing.Size(298, 63);
            this.errorText.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Enter Server Port";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Enter Cmd Args";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Enter Server Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AddServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 307);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AddServerForm";
            this.Text = "Add New Server";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portSelectTxt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button choosePathBtn;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox nameInput;
        private System.Windows.Forms.TextBox argsInput;
        private System.Windows.Forms.Button completeBtn;
        private System.Windows.Forms.NumericUpDown portSelectTxt;
        private System.Windows.Forms.ToolTip serverPathTip;
        private System.Windows.Forms.Label serverPathTxt;
        private System.Windows.Forms.Label errorText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}