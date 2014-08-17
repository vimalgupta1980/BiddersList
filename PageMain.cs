﻿using System;
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

            lstVendorType.DisplayMember = "Name";
            lstVendorType.ValueMember = null;

            lstRegion.DisplayMember = "Name";
            lstRegion.ValueMember = null;

            lstCostCodeDiv.DisplayMember = "Name";
            lstCostCodeDiv.ValueMember = null;

            cboSavedSearches.DisplayMember = "Name";
            cboSavedSearches.ValueMember = null;

            ddListVndType.SelectedIndexChanged += new EventHandler(ddListVndType_SelectedIndexChanged);
        }
        

        public MainForm MainForm
        {
            get { return this.ParentForm as MainForm; }
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
            //ListBoxData[] costCodeData = null;
            //ListBoxData[] savedSearchData = null;

            using (var con = SysconCommon.Common.Environment.Connections.GetOLEDBConnection())
            {
                //Fill Vendor type data
                string sqlStr = "SELECT distinct ap.vndtyp, PADC(NVL(vt.TypNme, '--NO DESC LISTED--'),25,' ') as Description FROM actpay ap LEFT JOIN VndTyp vt ON ap.vndtyp = vt.RecNum ORDER BY ap.vndtyp";
                DataTable dt = con.GetDataTable("VendorType", sqlStr);
                vndTypeData = dt.Rows.Select(p => new ListBoxData() { Name = (p[0].ToString().Trim() + " - " + p[1].ToString().Trim()), Value = p[0].ToString() }).ToArray();
                //lstVendorType.DataBindings.Clear();
                //lstVendorType.DataSource = vndTypeData;
                //lstVendorType.SelectedItems.Clear();

                ddListVndType.SetDataSource("Vendor Type", vndTypeData);

                //Fill Region
                sqlStr = "select distinct region from SysConBidderList WHERE ! EMPTY(Region)";
                dt = con.GetDataTable("VendorType", sqlStr);
                regionData = dt.Rows.Select(p => new ListBoxData() { Name = p[0].ToString().Trim(), Value = p[0].ToString().Trim() }).ToArray();
                lstRegion.DataBindings.Clear();
                lstRegion.DataSource = regionData;
                lstRegion.SelectedItems.Clear();

                ////Fill Cost Code
                //sqlStr = "SELECT DISTINCT TRANSFORM(sbl.CstCde, '999999999') AS CstCde, NVL(cc.CdeNme, PADR('-- No Description --', 30, ' ')) as Desc FROM SysconBidderList sbl LEFT JOIN CstCde cc ON sbl.CstCde = cc.RecNum ORDER BY 1";
                //dt = con.GetDataTable("CostCode", sqlStr);
                //costCodeData = dt.Rows.Select(p => new ListBoxData() { Name = (p[0].ToString().Trim() + " - " + p[1].ToString().Trim()), Value = p[0].ToString() }).ToArray();
                //lstCostCodeDiv.DataBindings.Clear();
                //lstCostCodeDiv.DataSource = costCodeData;
                //lstCostCodeDiv.SelectedItems.Clear(); 

                //Fill Cost Division
                sqlStr = "SELECT RecNum, NVL(DivNme, PADR('-- No Description --', 30, ' ')) as DivNme  FROM cstdiv";
                dt = con.GetDataTable("CostDiv", sqlStr);

                List<ListBoxData> costDivisionData = new List<ListBoxData>();
                costDivisionData.Add(new ListBoxData() { Name = "0 -- No Description", Value = "0" });
                costDivisionData.AddRange(dt.Rows.Select(p => new ListBoxData() { Name = (p[0].ToString().Trim() + " - " + p[1].ToString().Trim()), Value = p[0].ToString() }));
                
                lstCostCodeDiv.DataBindings.Clear();
                lstCostCodeDiv.DataSource = costDivisionData;
                lstCostCodeDiv.SelectedItems.Clear();

                //Fill saved searches combo
                //sqlStr = "SELECT DISTINCT VndNme FROM SysConSavedList WHERE ! EMPTY(NVL(VndNme,''))";
                //dt = con.GetDataTable("SavedSearches", sqlStr);
                //savedSearchData = dt.Rows.Select(p => new ListBoxData() { Name = p[0].ToString().Trim(), Value = p[0].ToString() }).ToArray();
                //cboSavedSearches.DataBindings.Clear();
                //cboSavedSearches.DataSource = savedSearchData;
            }

            this.LoadSavedSearchData();
        }

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

        private void lstVendorType_MouseEnter(object sender, EventArgs e)
        {
            lstVendorType.Height = 30 + 12 * 30;
            lstVendorType.BringToFront();
        }

        private void lstVendorType_MouseLeave(object sender, EventArgs e)
        {
            //lstVendorType.Height = 30;
            if (!new Rectangle(new Point(0, 0), lstVendorType.Size).Contains(lstVendorType.PointToClient(Control.MousePosition)))
            {
                lstVendorType.Height = 30;
            }
        }

        private void lstVendorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            IList<ListBoxData> selectedEntries = lstVendorType.SelectedItems.OfType<ListBoxData>().ToList();
            ApplyFilter();
        }

        void ddListVndType_SelectedIndexChanged(object sender, EventArgs e)
        {
            IList<ListBoxData> selectedEntries = ddListVndType.SelectedItems.OfType<ListBoxData>().ToList();
            ApplyFilter();
        }

        private void lstRegion_MouseEnter(object sender, EventArgs e)
        {
            lstRegion.Height = 30 + 10 * 30;
            lstRegion.BringToFront();
            lstRegion.ShowScrollbar = true;
        }

        private void lstRegion_MouseLeave(object sender, EventArgs e)
        {
            //lstRegion.Height = 30;
            //lstRegion.SendToBack();

            if (!new Rectangle(new Point(0, 0), lstRegion.Size).Contains(lstRegion.PointToClient(Control.MousePosition)))
            {
                lstRegion.Height = 30;
                lstRegion.SendToBack();
                lstRegion.ShowScrollbar = false;
            }
        }

        private void lstRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            IList<ListBoxData> selectedEntries = lstRegion.SelectedItems.OfType<ListBoxData>().ToList();
            ApplyFilter();
        }

        private void lstCostCodeDiv_MouseEnter(object sender, EventArgs e)
        {
            lstCostCodeDiv.Height = 30 + 12 * 25;
            lstCostCodeDiv.BringToFront();
            lstCostCodeDiv.ShowScrollbar = true;
        }

        private void lstCostCodeDiv_MouseLeave(object sender, EventArgs e)
        {
            lstCostCodeDiv.Height = 30;
            lstCostCodeDiv.SendToBack();
            lstCostCodeDiv.ShowScrollbar = false;
        }

        private void lstCostCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            IList<ListBoxData> selectedEntries = lstCostCodeDiv.SelectedItems.OfType<ListBoxData>().ToList();
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

        private void ApplyFilter()
        {
            //IList<ListBoxData> vndTypeEntries = lstVendorType.SelectedItems.OfType<ListBoxData>().ToList();

            IList<ListBoxData> vndTypeEntries = ddListVndType.SelectedItems.OfType<ListBoxData>().ToList();
            IEnumerable<string> vndTypes = from v in vndTypeEntries select v.Value.Trim();

            IList<ListBoxData> regionTypeEntries = lstRegion.SelectedItems.OfType<ListBoxData>().ToList();
            IEnumerable<string> regions = from r in regionTypeEntries select r.Value.Trim();

            IList<ListBoxData> costCodeEntries = lstCostCodeDiv.SelectedItems.OfType<ListBoxData>().ToList();
            IEnumerable<string> costCodes = from cc in costCodeEntries select cc.Value.Trim();

            IList<SysconBidderListDataModel> filteredData = _bidderListData;

            if (ddListVndType.SelectedItems.Count > 0)
                filteredData = _bidderListData.Where(b => vndTypes.ToList().Contains(b.VndTyp.ToString())).ToList();

            if (lstRegion.SelectedItems.Count > 0)
                filteredData = filteredData.Where(b => regions.ToList().Contains(b.State_)).ToList();

            if (lstCostCodeDiv.SelectedItems.Count > 0)
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

            string[] columnHeaders = { "Vendor Name", "Contact", "Address1", "Address2", "City", "State", "Zip", "Phone", "Fax", "EMail" };
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

                ExcelInterop.Range range = myWorkSheet.get_Range("A1:J1");
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
                            foreach (DataRow dr in dt.Rows)
                            {
                                string jobTitle = (string)dr["jobttl"];
                                if (!string.IsNullOrEmpty(jobTitle) && jobTitle.ToUpper().Contains("ESTIMATOR"))
                                {
                                    tempBidderData.Contct = (string)dr["cntnme"];
                                    tempBidderData.PhnNum = (string)dr["PhnNum"];
                                    tempBidderData.E_Mail = (string)dr["E_Mail"];
                                    tempBidderData.FaxNum = (string)dr["FaxNum"];
                                }
                                else
                                {
                                    tempBidderData.Contct = "NO ESTIMATOR";
                                    tempBidderData.PhnNum = (string)dr["PhnNum"];
                                    tempBidderData.E_Mail = (string)dr["E_Mail"];
                                    tempBidderData.FaxNum = (string)dr["FaxNum"];
                                }
                                string[] rowData = { tempBidderData.VndNme.Trim(), tempBidderData.Contct.Trim(), tempBidderData.Addrs1.Trim(), 
                                         tempBidderData.Addrs2.Trim(), tempBidderData.CtyNme.Trim(), tempBidderData.State_.Trim(),
                                         tempBidderData.ZipCde.Trim(), tempBidderData.PhnNum.Trim(), tempBidderData.FaxNum.Trim(), tempBidderData.E_Mail.Trim()
                                       };

                                range = myWorkSheet.get_Range(string.Format("A{0}:J{1}", count + 2, count + 2));
                                range.Value2 = rowData;

                                count++;
                            }
                        }
                        else
                        {
                            tempBidderData.Contct = "NO ESTIMATOR";

                            string[] rowData1 = { tempBidderData.VndNme.Trim(), tempBidderData.Contct.Trim(), tempBidderData.Addrs1.Trim(), 
                                         tempBidderData.Addrs2.Trim(), tempBidderData.CtyNme.Trim(), tempBidderData.State_.Trim(),
                                         tempBidderData.ZipCde.Trim(), tempBidderData.PhnNum.Trim(), tempBidderData.FaxNum.Trim(), tempBidderData.E_Mail.Trim()
                                       };

                            range = myWorkSheet.get_Range(string.Format("A{0}:J{1}", count + 2, count + 2));
                            range.Value2 = rowData1;

                            count++;
                        }
                    }
                }

                //AutoFit column data
                for (int i = 1; i <= 10; i++)
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

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (_bidderListData == null || _bidderListData.Count == 0)
                return;

            foreach (SysconBidderListDataModel sbl in _bidderListData)
            {
                sbl.Selctd = chkAll.Checked;
            }
            grdVendor.Refresh();
        }

    }
    
}
