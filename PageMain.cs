using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using ExcelInterop = Microsoft.Office.Interop.Excel;

using SysconCommon.Algebras.DataTables;
using SysconCommon.Common;
using SysconCommon.Common.Environment;

namespace BiddersList
{
    /// <summary>
    /// The main page of BidderList application
    /// </summary>
    public partial class PageMain : UserControl
    {
        public event EventHandler SMBDirChanged;

        IList<SysconBidderListDataModel> _bidderListData = null;
        private BindingSource _bindingSrc = null;

        /// <summary>
        /// 
        /// </summary>
        public PageMain()
        {
            InitializeComponent();

            cboSavedSearches.DisplayMember = "Name";
            cboSavedSearches.ValueMember = null;

            ddListVndType.SelectedIndexChanged += new EventHandler(ddListVndType_SelectedIndexChanged);
            ddLstRegion.SelectedIndexChanged += new EventHandler(ddLstRegion_SelectedIndexChanged);
            ddLstCostCodeDiv.SelectedIndexChanged += new EventHandler(ddLstCostCodeDiv_SelectedIndexChanged);
        }

        #region Properties
        
        public MainForm MainForm
        {
            get { return this.ParentForm as MainForm; }
        }
        
        public void ShowHideRegistrationButton(bool isVisible)
        {
            bttnRegister.Visible = isVisible;
        }

        public void LoadData(IList<SysconBidderListDataModel> bidListData)
        {
            _bidderListData = bidListData;

            txtDataDir.Text = this.MainForm.MbApi.smartGetSMBDir();

            _bindingSrc = new BindingSource();
            grdVendor.DataBindings.Clear();           
            grdVendor.DataSource = null;

            //Restrict to only these 5 columns
            grdVendor.AutoGenerateColumns = false;
            grdVendor.ColumnCount = 5;
            grdVendor.DataSource = _bindingSrc;

            grdVendor.Columns[0].DataPropertyName = "vndNme";
            grdVendor.Columns[1].DataPropertyName = "Addrs1";
            grdVendor.Columns[2].DataPropertyName = "cstcde";
            grdVendor.Columns[3].DataPropertyName = "State_";
            grdVendor.Columns[4].DataPropertyName = "Selctd";

            foreach (SysconBidderListDataModel model in bidListData)
            {
                _bindingSrc.Add(model);
            }

            ListBoxData[] vndTypeData = null;
            ListBoxData[] regionData = null;

            using (var con = SysconCommon.Common.Environment.Connections.GetOLEDBConnection())
            {
                //Fill Vendor type data
                string sqlStr = "SELECT distinct ap.vndtyp, PADC(NVL(vt.TypNme, '--NO DESC LISTED--'),25,' ') as Description FROM actpay ap LEFT JOIN VndTyp vt ON ap.vndtyp = vt.RecNum ORDER BY ap.vndtyp";
                DataTable dt = con.GetDataTable("VendorType", sqlStr);
                vndTypeData = dt.Rows.Select(p => new ListBoxData() { Name = (p[0].ToString().Trim() + " - " + p[1].ToString().Trim()), Value = p[0].ToString() }).ToArray();
                ddListVndType.SetDataSource("Vendor Type", vndTypeData);

                //Fill Region
                sqlStr = "select distinct region from SysConBidderList WHERE ! EMPTY(Region)";
                dt = con.GetDataTable("VendorType", sqlStr);
                regionData = dt.Rows.Select(p => new ListBoxData() { Name = p[0].ToString().Trim(), Value = p[0].ToString().Trim() }).ToArray();
                ddLstRegion.SetDataSource("Region", regionData);

                //Fill Cost Division
                sqlStr = "SELECT RecNum, NVL(DivNme, PADR('-- No Description --', 30, ' ')) as DivNme  FROM cstdiv";
                dt = con.GetDataTable("CostDiv", sqlStr);

                List<ListBoxData> costDivisionData = new List<ListBoxData>();
                costDivisionData.Add(new ListBoxData() { Name = "0 -- No Description", Value = "0" });
                costDivisionData.AddRange(dt.Rows.Select(p => new ListBoxData() { Name = (p[0].ToString().Trim() + " - " + p[1].ToString().Trim()), Value = p[0].ToString() }));
                ddLstCostCodeDiv.SetDataSource("Division", costDivisionData);
            }

            //Fill saved searches combo
            this.LoadSavedSearchData();
        }
        
        #endregion

        private void PageMain_Load(object sender, EventArgs e)
        {

        }

        private void bttnDir_Click(object sender, EventArgs e)
        {
            this.MainForm.MbApi.smartSelectSMBDirByGUI();
            var usr = this.MainForm.MbApi.RequireSMBLogin();
            if (usr != null)
            {
                txtDataDir.Text = this.MainForm.MbApi.smartGetSMBDir();
                if (SMBDirChanged != null)
                {
                    SMBDirChanged(this, EventArgs.Empty);
                }
            }
        }

        private void bttnEdit_Click(object sender, EventArgs e)
        {
            this.MainForm.SwitchPage(BidderListPages.DataEntryPage); 
        }

        private void bttnSave_Click(object sender, EventArgs e)
        {
            var selectedBidderList = _bidderListData.Where(b => b.Selctd == true).ToList();

            using (var con = SysconCommon.Common.Environment.Connections.GetOLEDBConnection())
            {
                DataTable dt = con.GetDataTable("SavedSearches", "Select * from SysconSavedList");
                if (dt != null && dt.Rows.Count == 0)
                {
                    //If this is the first time ask to create the master list
                    if (MessageBox.Show(this.MainForm, "You have not created a master search list.\nDo you want to create one now?", 
                                        "Create master search list?",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (selectedBidderList.Count() == 0)
                        {
                            MessageBox.Show(this.MainForm, "No Records Marked for Saving \n Mark at least One record", "Nothing to Save",
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            foreach (SysconBidderListDataModel sb in selectedBidderList)
                            {
                                con.ExecuteNonQuery("INSERT INTO SysConSavedList (VndNme, ID) VALUES (\"{0}\", {1})", "MASTER", sb.Id);
                                sb.Selctd = false;
                            }
                        }
                    }
                    return;
                }
            }

            //Add to saved searches
            //Check whether any row is selected
            if (selectedBidderList.Count() == 0)
            {
                MessageBox.Show(this.MainForm, "No Records Marked for Saving \n Mark at least One record", "Nothing to Save",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            InputDialog inputDlg = new InputDialog();
            DialogResult result = inputDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                using (var con = SysconCommon.Common.Environment.Connections.GetOLEDBConnection())
                {
                    //If name already exists then ask for 
                    string saveName = inputDlg.InputString;
                    int recCount = con.GetScalar<int>("SELECT COUNT(*) from SysconSavedList WHERE UPPER(VndNme)=\"{0}\"", saveName.ToUpper());

                    if (recCount > 0)
                    {
                        string message = (saveName.ToUpper() == "MASTER") ? "Do you want to override the Master list?" : "This name already exists. Overwrite?";
                        if (MessageBox.Show(this.MainForm, message, "Override saved list?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            con.ExecuteNonQuery("DELETE FROM SysConSavedList WHERE UPPER(VndNme) = \"{0}\"", saveName.ToUpper());

                            foreach (SysconBidderListDataModel sb in selectedBidderList)
                            {
                                con.ExecuteNonQuery("INSERT INTO SysConSavedList (VndNme, ID) VALUES (\"{0}\", {1})", saveName.ToUpper(), sb.Id);
                                sb.Selctd = false;
                            }
                        }
                    }
                    else
                    {
                        foreach (SysconBidderListDataModel sb in selectedBidderList)
                        {
                            con.ExecuteNonQuery("INSERT INTO SysConSavedList (VndNme, ID) VALUES (\"{0}\", {1})", saveName.ToUpper(), sb.Id);
                            sb.Selctd = false;
                        }
                    }
                }
            }

            grdVendor.Refresh();

            this.LoadSavedSearchData();
        }

        private void bttnExport_Click(object sender, EventArgs e)
        {
            var selected = _bidderListData.Where(b => b.Selctd == true).ToList();
            if (selected.Count() == 0)
            {
                MessageBox.Show(this.MainForm, "No Records Marked for Export \n Mark at least One record", "Nothing to Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Title = "Bidder List Export Filename";
            saveDlg.Filter = "*.xls | Excel Files";
            saveDlg.OverwritePrompt = true;
            saveDlg.FileName = "BidderList";
            saveDlg.DefaultExt = "xls";

            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                ExportRowsToExcel(selected, saveDlg.FileName);
            }
        }

        private void bttnExit_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }


        void ddListVndType_SelectedIndexChanged(object sender, EventArgs e)
        {
            IList<ListBoxData> selectedEntries = ddListVndType.SelectedItems.OfType<ListBoxData>().ToList();
            ApplyFilter();
        }

        void ddLstRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            IList<ListBoxData> selectedEntries = ddLstRegion.SelectedItems.OfType<ListBoxData>().ToList();
            ApplyFilter();
        }

        void ddLstCostCodeDiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            IList<ListBoxData> selectedEntries = ddLstCostCodeDiv.SelectedItems.OfType<ListBoxData>().ToList();
            ApplyFilter();
        }

        private void cboSavedSearches_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBoxData selected = cboSavedSearches.SelectedItem as ListBoxData;
            string searchName = selected.Name.Trim();

            IList<SysconBidderListDataModel> filteredData = _bidderListData;
            bool filtered = false;

            using (var con = SysconCommon.Common.Environment.Connections.GetOLEDBConnection())
            {
                DataTable dt = con.GetDataTable("", "SELECT id FROM SysConSavedList WHERE VndNme = \"{0}\"", searchName);
                IEnumerable<int> ids = from cc in dt.Rows select (int)cc[0];

                if (searchName.ToUpper() == "-NONE-")
                {
                    //No filtering required
                    filtered = false;
                }
                else
                {
                    //filteredData = (searchName == "-NONE-") ? _bidderListData : _bidderListData.Where(b => ids.ToList().Contains(b.Id)).ToList();
                    filteredData = _bidderListData.Where(b => ids.ToList().Contains(b.Id)).ToList();
                    filtered = true;
                }
            }

            if (filteredData != null)
            {
                _bindingSrc.Clear();
                foreach (SysconBidderListDataModel sb in filteredData)
                {
                    sb.Selctd = filtered;
                    _bindingSrc.Add(sb);
                }
            }
        }

        /// <summary>
        /// Filter the Grid data
        /// </summary>
        private void ApplyFilter()
        {
            IList<ListBoxData> vndTypeEntries = ddListVndType.SelectedItems.OfType<ListBoxData>().ToList();
            IEnumerable<string> vndTypes = from v in vndTypeEntries select v.Value.Trim();

            IList<ListBoxData> regionTypeEntries = ddLstRegion.SelectedItems.OfType<ListBoxData>().ToList();
            IEnumerable<string> regions = from r in regionTypeEntries select r.Value.Trim();

            IList<ListBoxData> costCodeEntries = ddLstCostCodeDiv.SelectedItems.OfType<ListBoxData>().ToList();
            IEnumerable<string> costCodes = from cc in costCodeEntries select cc.Value.Trim();

            IList<SysconBidderListDataModel> filteredData = _bidderListData;

            if (ddListVndType.SelectedItems.Count > 0)
                filteredData = _bidderListData.Where(b => vndTypes.ToList().Contains(b.VndTyp.ToString())).ToList();

            if (ddLstRegion.SelectedItems.Count > 0)
                filteredData = filteredData.Where(b => regions.ToList().Contains(b.State_)).ToList();

            if (ddLstCostCodeDiv.SelectedItems.Count > 0)
                filteredData = filteredData.Where(b => costCodes.ToList().Contains(b.CstDiv.ToString())).ToList();

            if (filteredData != null)
            {
                _bindingSrc.Clear();
                foreach (SysconBidderListDataModel sb in filteredData)
                {
                    _bindingSrc.Add(sb);
                }
            }
        }


        private void ExportRowsToExcel(IList<SysconBidderListDataModel> selectedBidList, string excelFile)
        {
            ExcelInterop.Application myExcelApp = null;
            ExcelInterop.Workbook myWorkbook = null;
            ExcelInterop.Worksheet myWorkSheet = null;
            object missingValue = System.Reflection.Missing.Value;

            string[] columnHeaders = { "Vendor Name", "Phone", "Address1", "Address2", "City", "State", "Zip", "Contact", "Phone", "Fax", "EMail" };
            int cols = selectedBidList.Count;
            bool successFlag = false;
            try
            {
                myExcelApp = new ExcelInterop.Application();
                if (myExcelApp == null)
                {
                    MessageBox.Show(this.MainForm, "Could not create Excel application object", "Error");
                    return;
                }

                myWorkbook = myExcelApp.Workbooks.Add();
                myWorkSheet = (ExcelInterop.Worksheet)myWorkbook.Worksheets.get_Item("Sheet1");

                ExcelInterop.Range range = myWorkSheet.get_Range("A1:K1");
                object[,] workingValues = new object[columnHeaders.Length, 1];
                range.Value2 = columnHeaders;
                range.Font.Bold = true;

                using (var con = SysconCommon.Common.Environment.Connections.GetOLEDBConnection())
                {
                    int count = 0;
                    //write rows to excel file
                    for (int i = 0; i < selectedBidList.Count; i++)
                    {
                        SysconBidderListDataModel tempBidderData = selectedBidList[i];
                        
                        //Get the details from the vendor contacts table
                        DataTable dt = con.GetDataTable("Contacts", "Select cntnme, jobttl, PhnNum, E_Mail, FaxNum from vndcnt where recnum={0}", tempBidderData.RecNum);
                        if (dt.Rows.Count > 0)
                        {
                            string[] rowData = null;

                            foreach (DataRow dr in dt.Rows)
                            {
                                string jobTitle = (string)dr["jobttl"];
                                if (!string.IsNullOrEmpty(jobTitle) && jobTitle.ToUpper().Contains("ESTIMATOR"))
                                {
                                    tempBidderData.Contct = (string)dr["cntnme"];

                                    rowData = new string[]{  tempBidderData.VndNme.Trim(), tempBidderData.PhnNum.Trim(), tempBidderData.Addrs1.Trim(), 
                                                             tempBidderData.Addrs2.Trim(), tempBidderData.CtyNme.Trim(), tempBidderData.State_.Trim(),
                                                             tempBidderData.ZipCde.Trim(), tempBidderData.Contct.Trim(), (string)dr["PhnNum"], 
                                                             (string)dr["FaxNum"], (string)dr["E_Mail"]
                                                           };
                                }
                                else
                                {
                                    tempBidderData.Contct = "NO ESTIMATOR";
                                    //tempBidderData.PhnNum = (string)dr["PhnNum"];
                                    //tempBidderData.E_Mail = (string)dr["E_Mail"];
                                    //tempBidderData.FaxNum = (string)dr["FaxNum"];

                                    rowData = new string[]{  tempBidderData.VndNme.Trim(), tempBidderData.PhnNum.Trim(), tempBidderData.Addrs1.Trim(), 
                                                             tempBidderData.Addrs2.Trim(), tempBidderData.CtyNme.Trim(), tempBidderData.State_.Trim(),
                                                             tempBidderData.ZipCde.Trim(), tempBidderData.Contct.Trim(), string.Empty, 
                                                             string.Empty, string.Empty
                                                           };
                                }                                

                                range = myWorkSheet.get_Range(string.Format("A{0}:K{1}", count + 2, count + 2));
                                range.Value2 = rowData;

                                count++;
                            }
                        }
                        else
                        {
                            tempBidderData.Contct = "NO ESTIMATOR";

                            string[] rowData1 = {   tempBidderData.VndNme.Trim(), tempBidderData.PhnNum.Trim(), tempBidderData.Addrs1.Trim(), 
                                                    tempBidderData.Addrs2.Trim(), tempBidderData.CtyNme.Trim(), tempBidderData.State_.Trim(),
                                                    tempBidderData.ZipCde.Trim(), tempBidderData.Contct.Trim(), string.Empty, 
                                                    string.Empty, string.Empty
                                                };

                            range = myWorkSheet.get_Range(string.Format("A{0}:K{1}", count + 2, count + 2));
                            range.Value2 = rowData1;

                            count++;
                        }
                    }
                }

                //AutoFit column data
                for (int i = 1; i <= 11; i++)
                {
                    range = myWorkSheet.get_Range(string.Format("{0}{1}", (char)(64 + i), 1));
                    range.EntireColumn.AutoFit();
                }

                successFlag = true;
            }
            finally
            {
                //Save and close result work book
                if (myWorkbook != null)
                {
                    myWorkbook.SaveAs(excelFile, ExcelInterop.XlFileFormat.xlWorkbookNormal);

                    myWorkbook.Close(false, missingValue, missingValue);
                    Marshal.ReleaseComObject(myWorkbook);
                    myWorkbook = null;
                }

                if (myExcelApp != null)
                {
                    myExcelApp.Quit();
                    Marshal.ReleaseComObject(myExcelApp);
                    myExcelApp = null;
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();

                string msg = successFlag ? "Selected data successfully exported to excel" : "There was some problem in exporting selected data to excel";
                MessageBox.Show(this.MainForm, msg);
            }
        }

        private void LoadSavedSearchData()
        {
            List<ListBoxData> savedSearchData = new List<ListBoxData>();
            savedSearchData.Add(new ListBoxData() { Name = "-None-", Value = "0" });

            using (var con = SysconCommon.Common.Environment.Connections.GetOLEDBConnection())
            {                
                //Fill saved searches combo
                string sqlStr = "SELECT DISTINCT VndNme FROM SysConSavedList WHERE ! EMPTY(NVL(VndNme,''))";
                DataTable dt = con.GetDataTable("SavedSearches", sqlStr);
                savedSearchData.AddRange(dt.Rows.Select(p => new ListBoxData() { Name = p[0].ToString().Trim(), Value = p[0].ToString() }));
                cboSavedSearches.DataBindings.Clear();
                cboSavedSearches.DataSource = savedSearchData;
            }
        }

        private void bttnRegister_Click(object sender, EventArgs e)
        {
            this.MainForm.TryActivation();
        }

    }
    
}
