using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaseServer;

namespace DataEditor
{
    public partial class frmSchemeOpen : Form
    {
        private string scheme = String.Empty;
        public frmSchemeOpen(string screenname)
        {
            InitializeComponent();
            foreach (KeyValuePair<string, string> item in Data.GetSchemesList())
            {
                ListViewItem lvi = lvSchemes.Items.Add(item.Key);
                lvi.SubItems.Add(item.Value);
                if (lvi.Text == screenname)
                {
                    lvi.Selected = true;
                    lvSchemes.FocusedItem = lvi;
                }
            }
            scheme = screenname;
        }

        public string SelectedScheme()
        {
            return scheme;
        }

        private void lvSchemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = true;
            if (lvSchemes.FocusedItem != null)
                scheme = lvSchemes.FocusedItem.Text;
        }

        private void lvSchemes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
