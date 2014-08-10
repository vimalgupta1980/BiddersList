using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using SysconCommon;
using SysconCommon.Algebras.DataTables;
using SysconCommon.Common.Environment;

namespace BiddersList
{

    public partial class MainForm : Form
    {
        private SysconCommon.COMMethods mbapi = new SysconCommon.COMMethods();
        bool loaded = false;

        //private PageMain pageMain;
        private PageDE pageDE;
        private SysconBidderListDataModel _bidderListDataModel = new SysconBidderListDataModel();

        public MainForm()
        {
            InitializeComponent();

            pageDE = new PageDE();
            this.pageDE.Location = new System.Drawing.Point(0, 0);
            this.pageDE.Name = "pageDE";
            this.pageDE.Size = new System.Drawing.Size(780, 480);
            this.pageDE.TabIndex = 0;
            this.pageDE.Dock = DockStyle.Fill;
            pageDE.Visible = false;

            this.panelPageContainer.Controls.Add(pageDE);

            this.pageMain.SMBDirChanged += new EventHandler(PageMain_SMBDirChanged);
        }
       

        public COMMethods MbApi
        {
            get { return mbapi; }
        }

        void PageMain_SMBDirChanged(object sender, EventArgs e)
        {
            if (_biddersDS != null)
            {
                _biddersDS.Tables.Clear();
                _biddersDS.Dispose();
                _biddersDS = null;
            }

            CompanySetup();
        }

        private void pageMain_Load(object sender, EventArgs e)
        {
            //Fill cboSavedSearches
            //SELECT DISTINCT VndNme FROM SysConSavedList WHERE ! EMPTY(NVL(VndNme,'')) INTO CURSOR curSave
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // resets it everytime it is run so that the user can't just change to a product they already have a license for
            Env.SetConfigVar("product_id", 322504);

            var product_id = Env.GetConfigVar("product_id", 0, false);
            var product_version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            bool require_login = false;

            if (!loaded)
            {
                require_login = true;
                loaded = true;
                this.Text += " (version " + product_version + ")";
            }

            try
            {
                var license = SysconCommon.Protection.ProtectionInfo.GetLicense(product_id, product_version);
                if (license.IsTrial)
                {
                    if (!license.IsValid())
                    {
                        SetupInvalid();
                    }
                    else
                    {
                        var l = license as SysconCommon.Protection.TrialLicense;
                        SetupTrial(l.DaysLeft);
                    }
                }
                else
                {
                    SetupFull();
                }
            }
            catch
            {
                SetupInvalid();
            }

            if (require_login)
            {
                mbapi.smartGetSMBDir();

                if (mbapi.RequireSMBLogin() == null)
                    this.Close();
            }

            CompanySetup();
        }

        DataSet _biddersDS = null;
        private void CompanySetup()
        {
            string dataDir = mbapi.smartGetSMBDir();
            using (var con = SysconCommon.Common.Environment.Connections.GetOLEDBConnection())
            {
                try
                {
                    if (!File.Exists(Path.Combine(dataDir, "SysconBidderList.dbf")))
                    {
                        //Create table
                        string createQuery = "CREATE TABLE SysconBidderList FREE (RecNum INT NULL, ID INT AUTOINC UNIQUE, VndTyp N(3,0), VndNme C(40), Contct C(30) NULL, "
                                            + "Addrs1 C(30) NULL, Addrs2 C(30) NULL, CtyNme C(25) NULL, State_ C(2) NULL, ZipCde C(10) NULL, PhnNum C(14) NULL, "
                                            + "FaxNum C(14) NULL, E_Mail C(75) NULL, Region C(30) NULL, CstCde N(15,0) NULL, CstDiv N(10, 0), Selctd L NULL)";
                        con.ExecuteNonQuery(createQuery);

                        //Create Indexes
                        con.ExecuteNonQuery("EXECSCRIPT([SET EXCLUSIVE ON])");
                        con.ExecuteNonQuery("EXECSCRIPT([USE SysconBidderList])");
                        con.ExecuteNonQuery("EXECSCRIPT([INDEX ON ID TAG ID CANDIDATE])");
                        con.ExecuteNonQuery("EXECSCRIPT([INDEX ON RecNum TAG RecNum])");
                        con.ExecuteNonQuery("EXECSCRIPT([INDEX ON VndNme TAG VndNme])");
                        con.ExecuteNonQuery("EXECSCRIPT([INDEX ON CstCde TAG CstCde])");
                        con.ExecuteNonQuery("EXECSCRIPT([INDEX ON VndTyp TAG VndTyp])");
                        con.ExecuteNonQuery("EXECSCRIPT([INDEX ON Region TAG Region])");
                        con.ExecuteNonQuery("EXECSCRIPT([INDEX ON CstDiv TAG CstDiv])");

                        con.ExecuteNonQuery("UPDATE SysConBidderList SET Selctd = .F.");

                        //Fill data
                        con.ExecuteNonQuery("INSERT INTO SysconbidderList "
                                                + "(RecNum,VndNme, VndTyp, Addrs1, Addrs2, CtyNme, State_, "
                                                + "zipcde, PhnNum, FaxNum, E_Mail, Region, CstCde, CstDiv, Selctd) "
                                                + "SELECT ap.RecNum, ap.VndNme, ap.VndTyp, ap.Addrs1, "
                                                + "ap.Addrs2, ap.CtyNme, ap.state_, ap.zipcde, ap.PhnNum, "
                                                + "ap.faxnum, ap.E_Mail, ap.State_, ap.CdeDft,cc.divnum, .F. "
                                                + "FROM ActPay	ap left join SysconBidderList sbl ON ap.RecNum = sbl.RecNum "
                                                + "left join CstCde cc ON ap.cdedft = cc.recnum "
                                                + "WHERE ISNULL(sbl.RecNum)");
                    }

                    if (!File.Exists(Path.Combine(dataDir, "SysConSavedList.dbf")))
                    {
                        //Create table SysconSavedList
                        con.ExecuteNonQuery("CREATE TABLE SysConSavedList FREE (id int  NOT NULL, VndNme C(40) NOT NULL)");
                        con.ExecuteNonQuery("EXECSCRIPT([USE SysConSavedList])");
                        con.ExecuteNonQuery("EXECSCRIPT([INDEX ON UPPER(VndNme) TAG BLName])");
                        con.ExecuteNonQuery("EXECSCRIPT([INDEX ON ID TAG ID])");
                    }
                }
                finally
                {
                    con.ExecuteNonQuery("EXECSCRIPT([SET EXCLUSIVE OFF])");
                }

                _biddersDS = new DataSet();

                //Fill the SysconBidderList table and add to dataset
                DataTable bidListTable = con.GetDataTable("SysconBidderList", "SELECT * from SysconBidderList order by vndnme");
                bidListTable.TableName = "SysconBidderList";
                _biddersDS.Tables.Add(bidListTable);
                
                //Fill the SysConSavedList table and add to dataset
                DataTable savedListTable = con.GetDataTable("SysConSavedList", "SELECT * from SysConSavedList");
                savedListTable.TableName = "SysConSavedList";
                _biddersDS.Tables.Add(savedListTable);
            }

            DataTable dt = _biddersDS.Tables["SysconBidderList"];
            IList<SysconBidderListDataModel> bidListData = this.ToList(dt);

            this.pageMain.LoadData(bidListData);
            this.pageDE.LoadData(bidListData);
        }

        internal void SwitchPage(BidderListPages page)
        {
            if (page == BidderListPages.MainPage)
            {
                pageMain.Visible = true;
                pageDE.Visible = false;
            }

            if (page == BidderListPages.DataEntryPage)
            {
                pageMain.Visible = false;
                pageDE.Visible = true;
            }
        }

        private IList<SysconBidderListDataModel> ToList(DataTable self)
        {
            IList<SysconBidderListDataModel> data = new List<SysconBidderListDataModel>();
            foreach (DataRow row in self.Rows)
            {
                var rv = new SysconBidderListDataModel();
                var fields = typeof(SysconBidderListDataModel).GetFields();

                var props = typeof(SysconBidderListDataModel).GetProperties();

                foreach (DataColumn dc in self.Columns)
                {
                    var field = fields.Where(f => f.Name.ToUpper() == dc.ColumnName.ToUpper()).FirstOrDefault();
                    if (field == null)
                    {
                        var prop = props.Where(p => p.Name.ToUpper() == dc.ColumnName.ToUpper()).FirstOrDefault();
                        if (prop != null)
                        {
                            if (prop.PropertyType != typeof(string) && row[dc].ToString().Trim() == "")
                            {
                                // do nothing
                            }
                            else
                            {
                                prop.SetValue(rv, Convert.ChangeType(row[dc], prop.PropertyType), null);
                            }
                        }

                        continue;
                    }

                    if (field.FieldType != typeof(string) && row[dc].ToString().Trim() == "")
                    {
                        // do nothing
                    }
                    else
                    {
                        field.SetValue(rv, Convert.ChangeType(row[dc], field.FieldType));
                    }
                }

                data.Add(rv);
            }

            return data;
        }


        #region Licencing

        private void SetupTrial(int daysLeft)
        {
            var msg = string.Format("You have {0} days left to evaluate this software \n Do you want to activate it?", daysLeft);
            //this.demoLabel.Text = msg;
            if (MessageBox.Show(msg, "License info", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                //Show activation dialog
                TryActivation();
            }
            {
                pageMain.Enabled = true;
                this.Text = this.Text + string.Format(" - Trial ({0} days remaining)", daysLeft);
            }
        }

        private void SetupInvalid()
        {
            pageMain.Enabled = false;
            //this.demoLabel.Text = "Your License has expired or is invalid";
            if (MessageBox.Show("Your License has expired or is invalid", "License info", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                //Show activation dialog
                TryActivation();
            }
            else
            {
                this.Text = this.Text + "License expired";
            }
        }

        private void SetupFull()
        {
            pageMain.Enabled = true;
            //this.demoLabel.Text = "";
        }

        private void TryActivation()
        {
            var product_id = Env.GetConfigVar("product_id", 0, false);
            var product_version = Env.GetConfigVar("product_version", "1.0.0.0", false);

            var frm = new SysconCommon.Protection.ProtectionPlusOnlineActivationForm(product_id, product_version);
            frm.ShowDialog();
            this.OnLoad(null);
        }

        #endregion
    }  

}
