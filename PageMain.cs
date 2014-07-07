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

            lstCostCode.DisplayMember = "Name";
            lstCostCode.ValueMember = null;

            cboSavedSearches.DisplayMember = "Name";
            cboSavedSearches.ValueMember = null;
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
            ListBoxData[] costCodeData = null;
            //ListBoxData[] savedSearchData = null;

            using (var con = SysconCommon.Common.Environment.Connections.GetOLEDBConnection())
            {
                //Fill Vendor type data
                string sqlStr = "SELECT distinct ap.vndtyp, PADC(NVL(vt.TypNme, '--NO DESC LISTED--'),25,' ') as Description FROM actpay ap LEFT JOIN VndTyp vt ON ap.vndtyp = vt.RecNum ORDER BY ap.vndtyp";
                DataTable dt = con.GetDataTable("VendorType", sqlStr);
                vndTypeData = dt.Rows.Select(p => new ListBoxData() { Name = (p[0].ToString().Trim() + " - " + p[1].ToString().Trim()), Value = p[0].ToString() }).ToArray();
                lstVendorType.DataBindings.Clear();
                lstVendorType.DataSource = vndTypeData;
                lstVendorType.SelectedItems.Clear();

                //Fill Region
                sqlStr = "select distinct region from SysConBidderList WHERE ! EMPTY(Region)";
                dt = con.GetDataTable("VendorType", sqlStr);
                regionData = dt.Rows.Select(p => new ListBoxData() { Name = p[0].ToString().Trim(), Value = p[0].ToString().Trim() }).ToArray();
                lstRegion.DataBindings.Clear();
                lstRegion.DataSource = regionData;
                lstRegion.SelectedItems.Clear();

                //Fill Cost Code
                sqlStr = "SELECT DISTINCT TRANSFORM(sbl.CstCde, '999999999') AS CstCde, NVL(cc.CdeNme, PADR('-- No Description --', 30, ' ')) as Desc FROM SysconBidderList sbl LEFT JOIN CstCde cc ON sbl.CstCde = cc.RecNum ORDER BY 1";
                dt = con.GetDataTable("CostCode", sqlStr);
                costCodeData = dt.Rows.Select(p => new ListBoxData() { Name = (p[0].ToString().Trim() + " - " + p[1].ToString().Trim()), Value = p[0].ToString() }).ToArray();
                lstCostCode.DataBindings.Clear();
                lstCostCode.DataSource = costCodeData;
                lstCostCode.SelectedItems.Clear();

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
            //Add to saved searches
            //Check whether any row is selected

            var selectedBidderList = _bidderListData.Where(b => b.Selctd == true).ToList();
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
                        if (MessageBox.Show(this.MainForm, "This name already exists. Overwrite?", "Warning - Name already exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
            lstVendorType.Height = 30;
        }

        private void lstVendorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            IList<ListBoxData> selectedEntries = lstVendorType.SelectedItems.OfType<ListBoxData>().ToList();
            ApplyFilter(FilterCriteria.VendorType);
        }

        private void lstRegion_MouseEnter(object sender, EventArgs e)
        {
            lstRegion.Height = 30 + 10 * 30;
            lstRegion.BringToFront();
        }

        private void lstRegion_MouseLeave(object sender, EventArgs e)
        {
            lstRegion.Height = 30;
            lstRegion.SendToBack();
        }

        private void lstRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            IList<ListBoxData> selectedEntries = lstRegion.SelectedItems.OfType<ListBoxData>().ToList();
            ApplyFilter(FilterCriteria.Region);
        }

        private void lstCostCode_MouseEnter(object sender, EventArgs e)
        {
            lstCostCode.Height = 30 + 12 * 30;
            lstCostCode.BringToFront();
        }

        private void lstCostCode_MouseLeave(object sender, EventArgs e)
        {
            lstCostCode.Height = 30;
        }

        private void lstCostCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            IList<ListBoxData> selectedEntries = lstCostCode.SelectedItems.OfType<ListBoxData>().ToList();
            ApplyFilter(FilterCriteria.CostCode);
        }

        private void cboSavedSearches_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBoxData selected = cboSavedSearches.SelectedItem as ListBoxData;
            string searchName = selected.Value.Trim();

            IList<SysconBidderListDataModel> filteredData = _bidderListData;
            bool filtered = false;

            using (var con = SysconCommon.Common.Environment.Connections.GetOLEDBConnection())
            {
                DataTable dt = con.GetDataTable("", "SELECT id FROM SysConSavedList WHERE VndNme = \"{0}\"", searchName);
                IEnumerable<int> ids = from cc in dt.Rows select (int)cc[0];

                if (searchName == "-NONE-")
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

        private void ApplyFilter(FilterCriteria criteria)
        {
            IList<ListBoxData> vndTypeEntries = lstVendorType.SelectedItems.OfType<ListBoxData>().ToList();
            IEnumerable<string> vndTypes = from v in vndTypeEntries select v.Value.Trim();

            IList<ListBoxData> regionTypeEntries = lstRegion.SelectedItems.OfType<ListBoxData>().ToList();
            IEnumerable<string> regions = from r in regionTypeEntries select r.Value.Trim();

            IList<ListBoxData> costCodeEntries = lstCostCode.SelectedItems.OfType<ListBoxData>().ToList();
            IEnumerable<string> costCodes = from cc in costCodeEntries select cc.Value.Trim();

            IList<SysconBidderListDataModel> filteredData = _bidderListData;

            //switch (criteria)
            //{
            //    case FilterCriteria.VendorType:
            //        if (lstVendorType.SelectedItems.Count > 0)
            //            filteredData = _bidderListData.Where(b => vndTypes.ToList().Contains(b.VndTyp.ToString())).ToList();
            //        break;
            //    case FilterCriteria.Region:
            //        if (lstRegion.SelectedItems.Count > 0)
            //            filteredData = _bidderListData.Where(b => regions.ToList().Contains(b.State_)).ToList();
            //        break;
            //    case FilterCriteria.CostCode:
            //        if (lstCostCode.SelectedItems.Count > 0)
            //            filteredData = _bidderListData.Where(b => costCodes.ToList().Contains(b.CstCde.ToString())).ToList();
            //        break;
            //}

            if (lstVendorType.SelectedItems.Count > 0)
                filteredData = _bidderListData.Where(b => vndTypes.ToList().Contains(b.VndTyp.ToString())).ToList();

            if (lstRegion.SelectedItems.Count > 0)
                filteredData = filteredData.Where(b => regions.ToList().Contains(b.State_)).ToList();

            if (lstCostCode.SelectedItems.Count > 0)
                filteredData = filteredData.Where(b => costCodes.ToList().Contains(b.CstCde.ToString())).ToList();

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

                //write rows to excel file
                for (int i = 0; i < selectedBidList.Count; i++)
                {
                    SysconBidderListDataModel tempBidderData = selectedBidList[i];
                    string[] rowData = { tempBidderData.VndNme.Trim(), tempBidderData.Contct.Trim(), tempBidderData.Addrs1.Trim(), 
                                         tempBidderData.Addrs2.Trim(), tempBidderData.CtyNme.Trim(), tempBidderData.State_.Trim(),
                                         tempBidderData.ZipCde.Trim(), tempBidderData.PhnNum.Trim(), tempBidderData.FaxNum.Trim(), tempBidderData.E_Mail.Trim()
                                       };

                    range = myWorkSheet.get_Range(string.Format("A{0}:J{1}", i + 2, i + 2));
                    range.Value2 = rowData;
                }

                successFlag = true;

                //AutoFit column data
                for (int i = 1; i <= 10; i++)
                {
                    range = myWorkSheet.get_Range(string.Format("{0}{1}", (char)(64 + i), 1));
                    range.EntireColumn.AutoFit();
                }
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
            ListBoxData[] savedSearchData = null;
            using (var con = SysconCommon.Common.Environment.Connections.GetOLEDBConnection())
            {
                //Fill saved searches combo
                string sqlStr = "SELECT DISTINCT VndNme FROM SysConSavedList WHERE ! EMPTY(NVL(VndNme,''))";
                DataTable dt = con.GetDataTable("SavedSearches", sqlStr);
                savedSearchData = dt.Rows.Select(p => new ListBoxData() { Name = p[0].ToString().Trim(), Value = p[0].ToString() }).ToArray();
                cboSavedSearches.DataBindings.Clear();
                cboSavedSearches.DataSource = savedSearchData;
            }
        }
    }
    
}
