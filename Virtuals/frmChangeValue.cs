using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Points.Virtuals
{
    public partial class frmChangeValue : Form
    {
        public frmChangeValue()
        {
            InitializeComponent();
            
        }

        private string val = "0";

        public string Value
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
                lbValue.Text = val;
                tbValue.TextChanged -= tbValue_TextChanged;
                tbValue.Text = val;
                tbValue.TextChanged += tbValue_TextChanged;
            }
        }

        private void tbValue_TextChanged(object sender, EventArgs e)
        {
            val = tbValue.Text;
        }

        private void frmChangeValue_Shown(object sender, EventArgs e)
        {
            tbValue.SelectAll();
            tbValue.Focus();
        }

    }
}
