namespace BiddersList
{
    partial class SelectionDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectionDetails));
            this.lstSelected = new System.Windows.Forms.ListBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.bttnRemove = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.bttnCancel = new System.Windows.Forms.Button();
            this.bttnClearAll = new System.Windows.Forms.Button();
            this.lstData = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // lstSelected
            // 
            this.lstSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstSelected.FormattingEnabled = true;
            this.lstSelected.ItemHeight = 15;
            this.lstSelected.Location = new System.Drawing.Point(275, 31);
            this.lstSelected.Name = "lstSelected";
            this.lstSelected.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSelected.Size = new System.Drawing.Size(190, 244);
            this.lstSelected.TabIndex = 1;
            this.lstSelected.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstSelected_KeyDown);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(53, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(86, 15);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Vendor Type";
            // 
            // bttnRemove
            // 
            this.bttnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnRemove.Location = new System.Drawing.Point(205, 134);
            this.bttnRemove.Name = "bttnRemove";
            this.bttnRemove.Size = new System.Drawing.Size(63, 23);
            this.bttnRemove.TabIndex = 4;
            this.bttnRemove.Text = "<<";
            this.bttnRemove.UseVisualStyleBackColor = true;
            this.bttnRemove.Click += new System.EventHandler(this.bttnRemove_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(331, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Selected";
            // 
            // bttnCancel
            // 
            this.bttnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnCancel.Location = new System.Drawing.Point(402, 281);
            this.bttnCancel.Name = "bttnCancel";
            this.bttnCancel.Size = new System.Drawing.Size(63, 23);
            this.bttnCancel.TabIndex = 7;
            this.bttnCancel.Text = "&Close";
            this.bttnCancel.UseVisualStyleBackColor = true;
            this.bttnCancel.Click += new System.EventHandler(this.bttnClose_Click);
            // 
            // bttnClearAll
            // 
            this.bttnClearAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnClearAll.Location = new System.Drawing.Point(310, 281);
            this.bttnClearAll.Name = "bttnClearAll";
            this.bttnClearAll.Size = new System.Drawing.Size(84, 23);
            this.bttnClearAll.TabIndex = 8;
            this.bttnClearAll.Text = "Clear &All";
            this.bttnClearAll.UseVisualStyleBackColor = true;
            this.bttnClearAll.Click += new System.EventHandler(this.bttnClearAll_Click);
            // 
            // lstData
            // 
            this.lstData.CheckOnClick = true;
            this.lstData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstData.FormattingEnabled = true;
            this.lstData.Location = new System.Drawing.Point(9, 29);
            this.lstData.Name = "lstData";
            this.lstData.Size = new System.Drawing.Size(190, 244);
            this.lstData.TabIndex = 10;
            this.lstData.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // SelectionDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 307);
            this.Controls.Add(this.lstData);
            this.Controls.Add(this.bttnClearAll);
            this.Controls.Add(this.bttnCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bttnRemove);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lstSelected);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectionDetails";
            this.ShowInTaskbar = false;
            this.Text = "Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstSelected;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button bttnRemove;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bttnCancel;
        private System.Windows.Forms.Button bttnClearAll;
        private System.Windows.Forms.CheckedListBox lstData;
    }
}