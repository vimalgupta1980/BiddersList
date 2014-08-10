using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BiddersList
{
    public partial class InputDialog : Form
    {
        public InputDialog()
        {
            InitializeComponent();
        }

        public string InputString
        {
            get;
            set;
        }

        private void InputDialog_Load(object sender, EventArgs e)
        {
            this.InputString = string.Empty;
            txtInput.Focus();
        }

        private void bttnOk_Click(object sender, EventArgs e)
        {
            this.InputString = txtInput.Text.Trim();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void bttnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }        

        private void InputDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            //DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        
    }
}
