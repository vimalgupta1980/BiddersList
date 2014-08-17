using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SysconCommon.Common;
using SysconCommon.Common.Environment;
using SysconCommon.Algebras.DataTables;
using SysconCommon.Foxpro;

namespace BiddersList
{
    public partial class PageDE : UserControl
    {
        private BindingSource _BindingSrc = null;
        private SysconBidderListDataModel _currentRow = null;

        /// <summary>
        /// 
        /// </summary>
        public PageDE()
        {
            InitializeComponent();
        }

        public MainForm MainForm
        {
            get { return this.ParentForm as MainForm; }
        }

        public SysconBidderListDataModel CurrentRow
        {
            get
            {
                return _currentRow;
            }
            private set
            {
                _currentRow = value;
                BindTextBoxes();
            }
        }


        public void LoadData(IList<SysconBidderListDataModel> bidListData)
        {
            _BindingSrc = new BindingSource();
            dgVendors.DataBindings.Clear();
            dgVendors.DataSource = null;

            //Restrict to only 2 columns
            dgVendors.AutoGenerateColumns = false;
            dgVendors.ColumnCount = 2;
            dgVendors.DataSource = _BindingSrc;            

            dgVendors.Columns[0].DataPropertyName = "RecNum";
            dgVendors.Columns[1].DataPropertyName = "VndNme";

            SortableBindingList<SysconBidderListDataModel> bindingList = new SortableBindingList<SysconBidderListDataModel>(bidListData);
            _BindingSrc.DataSource = bindingList;
            dgVendors.DataSource = _BindingSrc;

            radioBttnVndName.Checked = true;

            BindTextBoxes();
        }

        private void ResetTextBoxBindings()
        {
            txtVndName.DataBindings.Clear();
            txtVndType.DataBindings.Clear();
            txtContact.DataBindings.Clear();
            txtAddr1.DataBindings.Clear();
            txtAddr2.DataBindings.Clear();
            txtCity.DataBindings.Clear();
            txtState.DataBindings.Clear();
            txtZipCde.DataBindings.Clear();
            txtPhone.DataBindings.Clear();
            txtFax.DataBindings.Clear();
            txtEMail.DataBindings.Clear();
            txtCostCode.DataBindings.Clear();
            txtDivision.DataBindings.Clear();
            txtRegion.DataBindings.Clear();
        }

        private void BindTextBoxes()
        {
            ResetTextBoxBindings();

            txtVndName.DataBindings.Add(new Binding("Text", CurrentRow, "VndNme"));
            txtVndType.DataBindings.Add(new Binding("Text", CurrentRow, "VndTyp"));
            txtContact.DataBindings.Add(new Binding("Text", CurrentRow, "Contct"));
            txtAddr1.DataBindings.Add(new Binding("Text", CurrentRow, "Addrs1"));
            txtAddr2.DataBindings.Add(new Binding("Text", CurrentRow, "Addrs2"));
            txtCity.DataBindings.Add(new Binding("Text", CurrentRow, "CtyNme"));
            txtState.DataBindings.Add(new Binding("Text", CurrentRow, "State_"));
            txtZipCde.DataBindings.Add(new Binding("Text", CurrentRow, "ZipCde"));
            txtPhone.DataBindings.Add(new Binding("Text", CurrentRow, "PhnNum"));
            txtFax.DataBindings.Add(new Binding("Text", CurrentRow, "FaxNum"));
            txtEMail.DataBindings.Add(new Binding("Text", CurrentRow, "E_Mail"));
            txtCostCode.DataBindings.Add(new Binding("Text", CurrentRow, "CstCde"));
            txtDivision.DataBindings.Add(new Binding("Text", CurrentRow, "CstDiv"));
            txtRegion.DataBindings.Add(new Binding("Text", CurrentRow, "Region"));
        }


        private void chkEnableToolTips_Click(object sender, EventArgs e)
        {
            //this.ParentForm
        }

        bool _addingNew = false;
        private void bttnNew_Click(object sender, EventArgs e)
        {
            _addingNew = !_addingNew;

            bttnNew.Text = (_addingNew) ? "Cancel" : "&New";
            if (_addingNew)
            {
                //
                bttnDelete.Enabled = false;
                bttnSave.Enabled = true;
                dgVendors.Enabled = false;

                //txtVndName.ReadOnly = false;
                //txtVndType.ReadOnly = false;
            }
            else
            {
                bttnNew.Text = "&New";
                bttnDelete.Enabled = true;
                bttnSave.Enabled = false;
                dgVendors.Enabled = true;

                //txtVndName.ReadOnly = true;
                //txtVndType.ReadOnly = true;
            }
        }

        private void bttnDelete_Click(object sender, EventArgs e)
        {
            using (var con = SysconCommon.Common.Environment.Connections.GetOLEDBConnection())
            {
                SortableBindingList<SysconBidderListDataModel> bindingList = _BindingSrc.DataSource as SortableBindingList<SysconBidderListDataModel>;
                bindingList.Remove(bindingList.First(t=> t.RecNum == CurrentRow.RecNum));

                con.ExecuteNonQuery("DELETE from SysconBidderList where Id = {0}", CurrentRow.Id);

                CurrentRow = bindingList[0];
                dgVendors.Refresh();
            }
        }

        private void bttnSave_Click(object sender, EventArgs e)
        {
            using (var con = SysconCommon.Common.Environment.Connections.GetOLEDBConnection())
            {
                DataTable dt = con.GetDataTable("SysconBidderList", "SELECT * from SysconBidderList WHERE recnum={0}", CurrentRow.RecNum);
                dt.Columns.Remove("Id");

                DataRow dr = dt.NewRow();                
                try
                {
                    dr["RecNum"]    = CurrentRow.RecNum;
                    dr["VndNme"]    = CurrentRow.VndNme;
                    dr["VndTyp"]    = CurrentRow.VndTyp;
                    dr["Contct"]    = CurrentRow.Contct;
                    dr["Addrs1"]    = CurrentRow.Addrs1;
                    dr["Addrs2"]    = CurrentRow.Addrs2;
                    dr["CtyNme"]    = CurrentRow.CtyNme;
                    dr["State_"]    = CurrentRow.State_;
                    dr["ZipCde"]    = CurrentRow.ZipCde;
                    dr["PhnNum"]    = CurrentRow.PhnNum;
                    dr["FaxNum"]    = CurrentRow.FaxNum;
                    dr["E_Mail"]    = CurrentRow.E_Mail;
                    dr["CstCde"]    = CurrentRow.CstCde;
                    dr["CstDiv"]    = CurrentRow.CstDiv;
                    dr["Region"]    = CurrentRow.Region;
                    dr["Selctd"]    = false;

                    //if add new then add the record
                    if (_addingNew)
                    {
                        string insertCommand = dr.FoxproInsertString("SysconBidderList");
                        con.ExecuteNonQuery(insertCommand);
                    }
                    else
                    {
                        //update the changed record
                        string updateCommand = "UPDATE SysconBidderList SET Contct=\"{0}\", Addrs1=\"{1}\", Addrs2=\"{2}\", CtyNme=\"{3}\", "
                                                + "State_=\"{4}\", ZipCde=\"{5}\", PhnNum=\"{6}\",FaxNum=\"{7}\",E_Mail=\"{8}\", "
                                                + "CstCde={9}, CstDiv={10}, Region=\"{11}\", Selctd={12} WHERE Id={13}";
                        con.ExecuteNonQuery(updateCommand, CurrentRow.Contct, CurrentRow.Addrs1, CurrentRow.Addrs2, CurrentRow.CtyNme, CurrentRow.State_,
                                                           CurrentRow.ZipCde, CurrentRow.PhnNum, CurrentRow.FaxNum, CurrentRow.E_Mail, CurrentRow.CstCde,
                                                           CurrentRow.CstDiv, CurrentRow.Region, 0, CurrentRow.Id);
                    }                    
                }
                finally
                {
                    _addingNew = false;
                    bttnNew.Enabled = true;
                    bttnNew.Text = "&New";
                    bttnDelete.Enabled = true;
                    bttnSave.Enabled = false;
                    dgVendors.Enabled = true;

                    txtVndName.Enabled = false;
                    txtVndType.Enabled = false;
                }
            }
        }

        private void bttnExit_Click(object sender, EventArgs e)
        {
            //TODO: If the data has changed then we need to save it.

            bttnNew.Enabled = true;
            bttnDelete.Enabled = true;
            bttnSave.Enabled = false;
            dgVendors.Enabled = true;
            dgVendors.BackgroundColor = SystemColors.ControlLightLight;

            this.MainForm.SwitchPage(BidderListPages.MainPage);            
        }

        private void dgVendors_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            CurrentRow = dgVendors.CurrentRow.DataBoundItem as SysconBidderListDataModel;
        }

        private void radioBttnRecNum_CheckedChanged(object sender, EventArgs e)
        {
            //Sort
            RadioButton rbRecNum = sender as RadioButton;

            if (rbRecNum.Checked)
            {
                dgVendors.Sort(dgVendors.Columns["clmRecNum"], ListSortDirection.Ascending);
            }
            else
            {
                dgVendors.Sort(dgVendors.Columns["clmVndName"], ListSortDirection.Ascending);
            }
        }

        private void txtContact_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                //.cmdNew    .Enabled = .T.
                //.cmdDelete .Enabled = .F.
                //.cmdSave   .Enabled = .T.
                //.lstVendor .Enabled = .F.

                //bttnNew.Enabled = true;
                //bttnDelete.Enabled = false;
                //bttnSave.Enabled = true;
                //dgVendors.BackgroundColor = Color.DarkGray;
                //dgVendors.Enabled = false;
            }
        }

        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            bttnNew.Enabled = true;
            bttnDelete.Enabled = false;
            bttnSave.Enabled = true;
            dgVendors.BackgroundColor = Color.DarkGray;
            dgVendors.Enabled = false;
        }
    }
}
