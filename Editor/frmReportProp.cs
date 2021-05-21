using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace DataEditor
{
    public partial class frmReportProp : Form
    {
        public frmReportProp(string printername, string descriptor)
        {
            InitializeComponent();
            this.PrinterName = printername;
            this.Descriptor = descriptor;
            tbDescriptor.Text = descriptor;
        }

        public string PrinterName { get; set; }
        public string Descriptor { get; set; }

        PrintDocument prtdoc = null;
        private void frmReportProp_Load(object sender, EventArgs e)
        {
            prtdoc = new PrintDocument();
            string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName;
            lvPrinters.Clear();
            foreach (string strPrinter in PrinterSettings.InstalledPrinters)
            {
                ListViewItem item = lvPrinters.Items.Add(strPrinter);
                item.ImageIndex = 0;
                if (PrinterName.Length == 0 && item.Text == strDefaultPrinter ||
                    PrinterName.Length > 0 && item.Text == PrinterName)
                {
                    item.Selected = true;
                    lvPrinters.FocusedItem = item;
                }
            }
        }

        private void lvPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPrinters.FocusedItem != null)
                this.PrinterName = lvPrinters.FocusedItem.Text;
        }

        private void tbDescriptor_TextChanged(object sender, EventArgs e)
        {
            this.Descriptor = tbDescriptor.Text;
        }

    }
}
