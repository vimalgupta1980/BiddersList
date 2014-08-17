using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BiddersList
{
    public partial class DropDownListBoxEx : UserControl
    {
        #region Members
        public event EventHandler SelectedIndexChanged;

        int _originalListBoxHeight = 0;

        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        public DropDownListBoxEx()
        {
            InitializeComponent();

            dropDownListBox1.SelectedIndices.Clear();
            _originalListBoxHeight = dropDownListBox1.Height;
        }

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public CheckedListBox.SelectedObjectCollection SelectedItems
        {
            get { return dropDownListBox1.SelectedItems; }
        }

        /// <summary>
        /// Title for this dialog
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// Set the DataSource
        /// </summary>
        /// <param name="dataSource"></param>
        public void SetDataSource(string title, object dataSource)
        {
            this.Title = title;

            dropDownListBox1.DisplayMember = "Name";
            dropDownListBox1.ValueMember = null;

            dropDownListBox1.DataSource = dataSource;
            dropDownListBox1.SelectedIndex = -1;
        }

        /// <summary>
        /// Clear the data bindings
        /// </summary>
        public void ClearDataBindings()
        {
            dropDownListBox1.DataBindings.Clear();
        }

        private void bttnDetails_Click(object sender, EventArgs e)
        {
            SelectionDetails detailsForm = new SelectionDetails(this.Title, this.Title, dropDownListBox1);

            detailsForm.ShowDialog();
        }

        private void dropDownListBox1_MouseEnter(object sender, EventArgs e)
        {
            DropDownListBoxEx_MouseEnter(sender, e);
        }

        private void dropDownListBox1_MouseLeave(object sender, EventArgs e)
        {
            dropDownListBox1.Height = _originalListBoxHeight;
            dropDownListBox1.ShowScrollbar = false;

            this.Height = 26;
        }

        private void DropDownListBoxEx_MouseEnter(object sender, EventArgs e)
        {
            //this.Height = 33 + 2 * 27;
            //panel1.Height = this.Height;
            dropDownListBox1.ShowScrollbar = true;
            dropDownListBox1.Height = 30 + 5 * 27;
            dropDownListBox1.BringToFront();
            dropDownListBox1.Focus();

            this.Height = dropDownListBox1.Height + 8;
            //panel1.Height = this.Height -5;
            //panel1.BringToFront();
        }

        private void DropDownListBoxEx_MouseLeave(object sender, EventArgs e)
        {
            dropDownListBox1_MouseLeave(dropDownListBox1, e);
        }

        private void dropDownListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(sender, e);
            }
        }

    }
}
