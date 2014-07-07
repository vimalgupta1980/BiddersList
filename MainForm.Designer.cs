namespace BiddersList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelPage = new BiddersList.RoundedPanel();
            this.lblSysconTag = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMid = new BiddersList.RoundedPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtCmpName = new System.Windows.Forms.Label();
            this.panelPageContainer = new System.Windows.Forms.Panel();
            this.pageMain = new BiddersList.PageMain();
            this.panelPage.SuspendLayout();
            this.panelMid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelPageContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPage
            // 
            this.panelPage.AntiAlias = true;
            this.panelPage.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panelPage.BorderColor = System.Drawing.Color.Black;
            this.panelPage.BorderWidth = 2;
            this.panelPage.Controls.Add(this.lblSysconTag);
            this.panelPage.Controls.Add(this.lblTitle);
            this.panelPage.Controls.Add(this.panelMid);
            this.panelPage.Controls.Add(this.txtCmpName);
            this.panelPage.Controls.Add(this.panelPageContainer);
            this.panelPage.Fill = false;
            this.panelPage.FillColor = System.Drawing.Color.Transparent;
            this.panelPage.Location = new System.Drawing.Point(11, 9);
            this.panelPage.Name = "panelPage";
            this.panelPage.Radius = 10;
            this.panelPage.Size = new System.Drawing.Size(806, 592);
            this.panelPage.TabIndex = 1;
            // 
            // lblSysconTag
            // 
            this.lblSysconTag.AutoSize = true;
            this.lblSysconTag.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSysconTag.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblSysconTag.Location = new System.Drawing.Point(657, 567);
            this.lblSysconTag.Name = "lblSysconTag";
            this.lblSysconTag.Size = new System.Drawing.Size(118, 13);
            this.lblSysconTag.TabIndex = 4;
            this.lblSysconTag.Text = "© 2014 Syscon, Inc.";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTitle.Location = new System.Drawing.Point(22, 567);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(295, 13);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Reports Plus Custom Master Builder Enhancements";
            // 
            // panelMid
            // 
            this.panelMid.AntiAlias = true;
            this.panelMid.BackColor = System.Drawing.Color.White;
            this.panelMid.BorderColor = System.Drawing.Color.Black;
            this.panelMid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMid.BorderWidth = 3;
            this.panelMid.Controls.Add(this.label1);
            this.panelMid.Controls.Add(this.pictureBox1);
            this.panelMid.Fill = true;
            this.panelMid.FillColor = System.Drawing.Color.White;
            this.panelMid.Location = new System.Drawing.Point(13, 25);
            this.panelMid.Name = "panelMid";
            this.panelMid.Radius = 12;
            this.panelMid.Size = new System.Drawing.Size(780, 39);
            this.panelMid.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(247, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(275, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "ReportPlus™ Bidder\'s List";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pictureBox1.Image = global::BiddersList.Properties.Resources.sysconlogo;
            this.pictureBox1.Location = new System.Drawing.Point(7, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(73, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // txtCmpName
            // 
            this.txtCmpName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCmpName.AutoSize = true;
            this.txtCmpName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCmpName.Location = new System.Drawing.Point(332, 2);
            this.txtCmpName.Name = "txtCmpName";
            this.txtCmpName.Size = new System.Drawing.Size(0, 14);
            this.txtCmpName.TabIndex = 1;
            // 
            // panelPageContainer
            // 
            this.panelPageContainer.BackColor = System.Drawing.Color.White;
            this.panelPageContainer.Controls.Add(this.pageMain);
            this.panelPageContainer.Location = new System.Drawing.Point(13, 71);
            this.panelPageContainer.Name = "panelPageContainer";
            this.panelPageContainer.Size = new System.Drawing.Size(780, 480);
            this.panelPageContainer.TabIndex = 1;
            // 
            // pageMain
            // 
            this.pageMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageMain.Location = new System.Drawing.Point(0, 0);
            this.pageMain.Name = "pageMain";
            this.pageMain.Size = new System.Drawing.Size(780, 480);
            this.pageMain.TabIndex = 0;
            this.pageMain.Load += new System.EventHandler(this.pageMain_Load);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BiddersList.Properties.Resources.wizstone;
            this.ClientSize = new System.Drawing.Size(828, 606);
            this.Controls.Add(this.panelPage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BidderList";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelPage.ResumeLayout(false);
            this.panelPage.PerformLayout();
            this.panelMid.ResumeLayout(false);
            this.panelMid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelPageContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private RoundedPanel panelPage;
        private System.Windows.Forms.Label txtCmpName;
        private System.Windows.Forms.Panel panelPageContainer;
        private RoundedPanel panelMid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblSysconTag;
        private System.Windows.Forms.Label lblTitle;
        private PageMain pageMain;
    }
}

