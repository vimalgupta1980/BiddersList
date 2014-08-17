using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace BiddersList
{
    /// <summary>
    /// Selection details form
    /// </summary>
    public partial class SelectionDetails : Form
    {
        private ListBox _ownerListBox = null;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="title"></param>
        /// <param name="fieldName"></param>
        /// <param name="source"></param>
        public SelectionDetails(string title, string fieldName, ListBox owner)
        {
            InitializeComponent();

            this.Text = title;
            lblTitle.Text = fieldName;
            _ownerListBox = owner;

            lstSelected.DisplayMember = "Name";
            lstSelected.ValueMember = null;

            lstData.DataSource = owner.DataSource;            
            lstData.DisplayMember = "Name";
            lstData.ValueMember = null;

            foreach (object item in _ownerListBox.SelectedIndices)
            {
                int idx = (int)item;
                lstData.SetItemChecked(idx, true);
            }

            this.StartPosition = FormStartPosition.WindowsDefaultLocation;
        }

        #region Control Event Handlers

        private void bttnRemove_Click(object sender, EventArgs e)
        {
            while (lstSelected.SelectedItems.Count > 0)
            {
                ListBoxData item = (ListBoxData)lstSelected.SelectedItems[0];
                lstData.SelectedItems.Remove(item);
                lstData.SetItemCheckedEx(item.Name, false);
            }
        }

        private void bttnClearAll_Click(object sender, EventArgs e)
        {
            for(int i=0; i< lstData.Items.Count; i++)
            {
                lstData.SetItemChecked(i, false);
            }
        }

        private void bttnClose_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;   
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue == CheckState.Unchecked)
            {
                lstSelected.Items.Add(lstData.Items[e.Index]);
                _ownerListBox.SelectedIndices.Add(e.Index);
            }
            else
            {
                lstSelected.Items.Remove(lstData.Items[e.Index]);
                _ownerListBox.SelectedIndices.Remove(e.Index);
            }
        }

        private void lstSelected_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                while (lstSelected.SelectedItems.Count > 0)
                {
                    ListBoxData item = (ListBoxData)lstSelected.SelectedItems[0];
                    lstData.SelectedItems.Remove(item);
                    lstData.SetItemCheckedEx(item.Name, false);
                }
            }
        }

        #endregion

    }

    /// <summary>
    /// Extension methods for the CheckedListBox
    /// </summary>
    public static class CheckedListBoxExtension
    {
        public static void SetItemCheckedEx(this CheckedListBox owner, string item, bool selected)
        {
            int index = GetItemIndex(owner, item);

            if (index < 0)
                return;

            owner.SetItemChecked(index, selected);
        }

        private static int GetItemIndex(CheckedListBox owner, string item)
        {
            int index = 0;

            foreach (ListBoxData lbData in owner.Items)
            {
                if (item == lbData.Name)
                {
                    return index;
                }
                index++;
            }

            return -1;
        }
    }
}
