namespace BiddersList
{
    partial class PageMain
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDataDir = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboSavedSearches = new System.Windows.Forms.ComboBox();
            this.grdVendor = new System.Windows.Forms.DataGridView();
            this.clmVendor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCostCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRegion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.bttnEdit = new System.Windows.Forms.Button();
            this.bttnExport = new System.Windows.Forms.Button();
            this.bttnSave = new System.Windows.Forms.Button();
            this.bttnExit = new System.Windows.Forms.Button();
            this.bttnDir = new System.Windows.Forms.Button();
            this.lstVendorType = new System.Windows.Forms.ListBox();
            this.lstRegion = new System.Windows.Forms.ListBox();
            this.lstCostCode = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdVendor)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "MB Directory";
            // 
            // txtDataDir
            // 
            this.txtDataDir.Location = new System.Drawing.Point(109, 15);
            this.txtDataDir.Name = "txtDataDir";
            this.txtDataDir.ReadOnly = true;
            this.txtDataDir.Size = new System.Drawing.Size(606, 20);
            this.txtDataDir.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(13, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Vendor Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(13, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Region";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(13, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "Cost Code";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(447, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Saved Searches";
            // 
            // cboSavedSearches
            // 
            this.cboSavedSearches.FormattingEnabled = true;
            this.cboSavedSearches.Location = new System.Drawing.Point(450, 113);
            this.cboSavedSearches.Name = "cboSavedSearches";
            this.cboSavedSearches.Size = new System.Drawing.Size(301, 21);
            this.cboSavedSearches.TabIndex = 11;
            this.cboSavedSearches.SelectedIndexChanged += new System.EventHandler(this.cboSavedSearches_SelectedIndexChanged);
            // 
            // grdVendor
            // 
            this.grdVendor.AllowUserToAddRows = false;
            this.grdVendor.AllowUserToDeleteRows = false;
            this.grdVendor.AllowUserToResizeRows = false;
            this.grdVendor.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdVendor.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdVendor.ColumnHeadersHeight = 28;
            this.grdVendor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grdVendor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmVendor,
            this.clmAddr,
            this.clmCostCode,
            this.clmRegion,
            this.clmSelect});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdVendor.DefaultCellStyle = dataGridViewCellStyle7;
            this.grdVendor.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdVendor.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.grdVendor.Location = new System.Drawing.Point(16, 151);
            this.grdVendor.MultiSelect = false;
            this.grdVendor.Name = "grdVendor";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdVendor.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.grdVendor.RowHeadersVisible = false;
            this.grdVendor.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.grdVendor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdVendor.Size = new System.Drawing.Size(735, 290);
            this.grdVendor.TabIndex = 12;
            // 
            // clmVendor
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clmVendor.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmVendor.HeaderText = "Vendor";
            this.clmVendor.Name = "clmVendor";
            this.clmVendor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmVendor.Width = 200;
            // 
            // clmAddr
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F);
            this.clmAddr.DefaultCellStyle = dataGridViewCellStyle3;
            this.clmAddr.HeaderText = "Address";
            this.clmAddr.Name = "clmAddr";
            this.clmAddr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmAddr.Width = 200;
            // 
            // clmCostCode
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F);
            this.clmCostCode.DefaultCellStyle = dataGridViewCellStyle4;
            this.clmCostCode.HeaderText = "Cost Code";
            this.clmCostCode.Name = "clmCostCode";
            this.clmCostCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmCostCode.Width = 120;
            // 
            // clmRegion
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F);
            this.clmRegion.DefaultCellStyle = dataGridViewCellStyle5;
            this.clmRegion.HeaderText = "Region";
            this.clmRegion.Name = "clmRegion";
            this.clmRegion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clmRegion.Width = 140;
            // 
            // clmSelect
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9F);
            dataGridViewCellStyle6.NullValue = false;
            this.clmSelect.DefaultCellStyle = dataGridViewCellStyle6;
            this.clmSelect.HeaderText = "Select";
            this.clmSelect.Name = "clmSelect";
            this.clmSelect.Width = 70;
            // 
            // bttnEdit
            // 
            this.bttnEdit.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnEdit.Location = new System.Drawing.Point(363, 450);
            this.bttnEdit.Name = "bttnEdit";
            this.bttnEdit.Size = new System.Drawing.Size(84, 27);
            this.bttnEdit.TabIndex = 13;
            this.bttnEdit.Text = "E&dit";
            this.bttnEdit.UseVisualStyleBackColor = true;
            this.bttnEdit.Click += new System.EventHandler(this.bttnEdit_Click);
            // 
            // bttnExport
            // 
            this.bttnExport.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnExport.Location = new System.Drawing.Point(567, 450);
            this.bttnExport.Name = "bttnExport";
            this.bttnExport.Size = new System.Drawing.Size(84, 27);
            this.bttnExport.TabIndex = 14;
            this.bttnExport.Text = "&Export";
            this.bttnExport.UseVisualStyleBackColor = true;
            this.bttnExport.Click += new System.EventHandler(this.bttnExport_Click);
            // 
            // bttnSave
            // 
            this.bttnSave.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnSave.Location = new System.Drawing.Point(463, 450);
            this.bttnSave.Name = "bttnSave";
            this.bttnSave.Size = new System.Drawing.Size(84, 27);
            this.bttnSave.TabIndex = 15;
            this.bttnSave.Text = "&Save";
            this.bttnSave.UseVisualStyleBackColor = true;
            this.bttnSave.Click += new System.EventHandler(this.bttnSave_Click);
            // 
            // bttnExit
            // 
            this.bttnExit.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnExit.Location = new System.Drawing.Point(667, 450);
            this.bttnExit.Name = "bttnExit";
            this.bttnExit.Size = new System.Drawing.Size(84, 27);
            this.bttnExit.TabIndex = 16;
            this.bttnExit.Text = "E&xit";
            this.bttnExit.UseVisualStyleBackColor = true;
            this.bttnExit.Click += new System.EventHandler(this.bttnExit_Click);
            // 
            // bttnDir
            // 
            this.bttnDir.BackgroundImage = global::BiddersList.Properties.Resources.MB7;
            this.bttnDir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bttnDir.Location = new System.Drawing.Point(732, 15);
            this.bttnDir.Name = "bttnDir";
            this.bttnDir.Size = new System.Drawing.Size(22, 22);
            this.bttnDir.TabIndex = 17;
            this.bttnDir.UseVisualStyleBackColor = true;
            this.bttnDir.Click += new System.EventHandler(this.bttnDir_Click);
            // 
            // lstVendorType
            // 
            this.lstVendorType.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstVendorType.FormattingEnabled = true;
            this.lstVendorType.ItemHeight = 15;
            this.lstVendorType.Location = new System.Drawing.Point(109, 45);
            this.lstVendorType.Name = "lstVendorType";
            this.lstVendorType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstVendorType.Size = new System.Drawing.Size(223, 19);
            this.lstVendorType.TabIndex = 19;
            this.lstVendorType.SelectedIndexChanged += new System.EventHandler(this.lstVendorType_SelectedIndexChanged);
            this.lstVendorType.MouseEnter += new System.EventHandler(this.lstVendorType_MouseEnter);
            this.lstVendorType.MouseLeave += new System.EventHandler(this.lstVendorType_MouseLeave);
            // 
            // lstRegion
            // 
            this.lstRegion.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstRegion.FormattingEnabled = true;
            this.lstRegion.ItemHeight = 15;
            this.lstRegion.Location = new System.Drawing.Point(109, 77);
            this.lstRegion.Name = "lstRegion";
            this.lstRegion.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstRegion.Size = new System.Drawing.Size(223, 19);
            this.lstRegion.TabIndex = 20;
            this.lstRegion.SelectedIndexChanged += new System.EventHandler(this.lstRegion_SelectedIndexChanged);
            this.lstRegion.MouseEnter += new System.EventHandler(this.lstRegion_MouseEnter);
            this.lstRegion.MouseLeave += new System.EventHandler(this.lstRegion_MouseLeave);
            // 
            // lstCostCode
            // 
            this.lstCostCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCostCode.FormattingEnabled = true;
            this.lstCostCode.ItemHeight = 15;
            this.lstCostCode.Location = new System.Drawing.Point(109, 113);
            this.lstCostCode.Name = "lstCostCode";
            this.lstCostCode.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstCostCode.Size = new System.Drawing.Size(306, 19);
            this.lstCostCode.TabIndex = 21;
            this.lstCostCode.SelectedIndexChanged += new System.EventHandler(this.lstCostCode_SelectedIndexChanged);
            this.lstCostCode.MouseEnter += new System.EventHandler(this.lstCostCode_MouseEnter);
            this.lstCostCode.MouseLeave += new System.EventHandler(this.lstCostCode_MouseLeave);
            // 
            // PageMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstCostCode);
            this.Controls.Add(this.lstRegion);
            this.Controls.Add(this.lstVendorType);
            this.Controls.Add(this.bttnDir);
            this.Controls.Add(this.bttnExit);
            this.Controls.Add(this.bttnSave);
            this.Controls.Add(this.bttnExport);
            this.Controls.Add(this.bttnEdit);
            this.Controls.Add(this.grdVendor);
            this.Controls.Add(this.cboSavedSearches);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDataDir);
            this.Controls.Add(this.label1);
            this.Name = "PageMain";
            this.Size = new System.Drawing.Size(780, 480);
            this.Load += new System.EventHandler(this.PageMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdVendor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDataDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboSavedSearches;
        private System.Windows.Forms.Button bttnEdit;
        private System.Windows.Forms.Button bttnExport;
        private System.Windows.Forms.Button bttnSave;
        private System.Windows.Forms.Button bttnExit;
        private System.Windows.Forms.Button bttnDir;
        private System.Windows.Forms.DataGridView grdVendor;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmVendor;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCostCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRegion;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmSelect;
        private System.Windows.Forms.ListBox lstVendorType;
        private System.Windows.Forms.ListBox lstRegion;
        private System.Windows.Forms.ListBox lstCostCode;
    }
}
