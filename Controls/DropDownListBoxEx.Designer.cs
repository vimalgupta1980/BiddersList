namespace BiddersList
{
    partial class DropDownListBoxEx
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.bttnPanel = new System.Windows.Forms.Panel();
            this.bttnDetails = new System.Windows.Forms.Button();
            this.listPanel = new System.Windows.Forms.Panel();
            this.dropDownListBox1 = new BiddersList.DropDownListBox();
            this.panel1.SuspendLayout();
            this.bttnPanel.SuspendLayout();
            this.listPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bttnPanel);
            this.panel1.Controls.Add(this.listPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 26);
            this.panel1.TabIndex = 2;
            // 
            // bttnPanel
            // 
            this.bttnPanel.Controls.Add(this.bttnDetails);
            this.bttnPanel.Location = new System.Drawing.Point(313, 1);
            this.bttnPanel.Name = "bttnPanel";
            this.bttnPanel.Size = new System.Drawing.Size(32, 20);
            this.bttnPanel.TabIndex = 2;
            // 
            // bttnDetails
            // 
            this.bttnDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bttnDetails.Location = new System.Drawing.Point(0, 0);
            this.bttnDetails.Name = "bttnDetails";
            this.bttnDetails.Size = new System.Drawing.Size(32, 20);
            this.bttnDetails.TabIndex = 2;
            this.bttnDetails.Text = "...";
            this.bttnDetails.UseVisualStyleBackColor = true;
            this.bttnDetails.Click += new System.EventHandler(this.bttnDetails_Click);
            // 
            // listPanel
            // 
            this.listPanel.Controls.Add(this.dropDownListBox1);
            this.listPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.listPanel.Location = new System.Drawing.Point(0, 0);
            this.listPanel.Name = "listPanel";
            this.listPanel.Size = new System.Drawing.Size(311, 26);
            this.listPanel.TabIndex = 1;
            // 
            // dropDownListBox1
            // 
            this.dropDownListBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dropDownListBox1.FormattingEnabled = true;
            this.dropDownListBox1.ItemHeight = 15;
            this.dropDownListBox1.Location = new System.Drawing.Point(2, 1);
            this.dropDownListBox1.Name = "dropDownListBox1";
            this.dropDownListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.dropDownListBox1.ShowScrollbar = false;
            this.dropDownListBox1.Size = new System.Drawing.Size(305, 19);
            this.dropDownListBox1.TabIndex = 0;
            this.dropDownListBox1.SelectedIndexChanged += new System.EventHandler(this.dropDownListBox1_SelectedIndexChanged);
            this.dropDownListBox1.MouseEnter += new System.EventHandler(this.dropDownListBox1_MouseEnter);
            this.dropDownListBox1.MouseLeave += new System.EventHandler(this.dropDownListBox1_MouseLeave);
            // 
            // DropDownListBoxEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "DropDownListBoxEx";
            this.Size = new System.Drawing.Size(350, 26);
            this.MouseEnter += new System.EventHandler(this.DropDownListBoxEx_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.DropDownListBoxEx_MouseLeave);
            this.panel1.ResumeLayout(false);
            this.bttnPanel.ResumeLayout(false);
            this.listPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel bttnPanel;
        private System.Windows.Forms.Button bttnDetails;
        private System.Windows.Forms.Panel listPanel;
        private DropDownListBox dropDownListBox1;

    }
}
