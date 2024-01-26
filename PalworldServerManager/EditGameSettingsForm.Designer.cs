namespace PalworldServerManager
{
    partial class EditGameSettingsForm
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.settingDescTxt = new System.Windows.Forms.Label();
            this.cmopleteBtn = new System.Windows.Forms.Button();
            this.settingsDataGrid = new System.Windows.Forms.DataGridView();
            label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.settingsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(48, 5);
            label1.Margin = new System.Windows.Forms.Padding(5);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(63, 13);
            label1.TabIndex = 0;
            label1.Text = "Description:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.settingsDataGrid, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.11539F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.88461F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 520);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.75827F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.24173F));
            this.tableLayoutPanel2.Controls.Add(label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.settingDescTxt, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmopleteBtn, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 378);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(786, 139);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // settingDescTxt
            // 
            this.settingDescTxt.AutoSize = true;
            this.settingDescTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingDescTxt.Location = new System.Drawing.Point(121, 5);
            this.settingDescTxt.Margin = new System.Windows.Forms.Padding(5);
            this.settingDescTxt.Name = "settingDescTxt";
            this.settingDescTxt.Size = new System.Drawing.Size(660, 59);
            this.settingDescTxt.TabIndex = 1;
            this.settingDescTxt.Text = "Description text";
            // 
            // cmopleteBtn
            // 
            this.cmopleteBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmopleteBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmopleteBtn.Location = new System.Drawing.Point(10, 79);
            this.cmopleteBtn.Margin = new System.Windows.Forms.Padding(10);
            this.cmopleteBtn.Name = "cmopleteBtn";
            this.cmopleteBtn.Size = new System.Drawing.Size(96, 50);
            this.cmopleteBtn.TabIndex = 2;
            this.cmopleteBtn.Text = "Done";
            this.cmopleteBtn.UseVisualStyleBackColor = false;
            this.cmopleteBtn.Click += new System.EventHandler(this.completeBtn_Click);
            // 
            // settingsDataGrid
            // 
            this.settingsDataGrid.AllowUserToAddRows = false;
            this.settingsDataGrid.AllowUserToDeleteRows = false;
            this.settingsDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.settingsDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.settingsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.NullValue = null;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.settingsDataGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.settingsDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsDataGrid.Location = new System.Drawing.Point(3, 3);
            this.settingsDataGrid.MultiSelect = false;
            this.settingsDataGrid.Name = "settingsDataGrid";
            dataGridViewCellStyle2.Format = "N6";
            dataGridViewCellStyle2.NullValue = null;
            this.settingsDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.settingsDataGrid.Size = new System.Drawing.Size(786, 369);
            this.settingsDataGrid.TabIndex = 1;
            // 
            // EditGameSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 520);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "EditGameSettingsForm";
            this.Text = "Edit Game Settings";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.settingsDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label settingDescTxt;
        private System.Windows.Forms.DataGridView settingsDataGrid;
        private System.Windows.Forms.Button cmopleteBtn;
    }
}