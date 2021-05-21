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
    public partial class frmReportOpen : Form
    {
        private string report = String.Empty;

        public frmReportOpen(string reportname)
        {
            InitializeComponent();
            foreach (KeyValuePair<string, string> item in Data.GetReportsList())
            {
                ListViewItem lvi = lvReports.Items.Add(item.Key);
                lvi.SubItems.Add(item.Value);
                if (lvi.Text == reportname)
                {
                    lvi.Selected = true;
                    lvReports.FocusedItem = lvi;
                }
            }
        }

        public string SelectedReport()
        {
            return report;
        }

        private void lvReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = true;
            report = (lvReports.FocusedItem != null) ?
                lvReports.FocusedItem.Text : String.Empty;
        }

        private void lvReports_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
