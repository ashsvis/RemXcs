using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace RemXcs
{
    public partial class frmPrintPreview : Form
    {
        public frmPrintPreview()
        {
            InitializeComponent();
        }

        private void frmPrintPreview_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            printPreview.Focus();
            PrinterSettings ps = printDoc.PrinterSettings;
            ps.DefaultPageSettings.Landscape = true;
            printDoc.PrinterSettings = ps;
            printPreview.AutoZoom = true;
        }

        private void tsbPrintDialog_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog(this) == DialogResult.OK)
            {
                printDoc.Print();
            }
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            printDoc.Print();
            Close();
        }

        private void tsbZoomOut_Click(object sender, EventArgs e)
        {
            double[] zooms = new double[] { 0.25, 0.5, 0.75, 1, 1.5, 2 };
            Array.Reverse(zooms);
            foreach (double zoom in zooms)
            {
                if (printPreview.Zoom > zoom)
                {
                    printPreview.Zoom = zoom;
                    break;
                }
            }
        }

        private void tssbZoom_ButtonClick(object sender, EventArgs e)
        {
            double[] zooms = new double[] { 0.25, 0.5, 0.75, 1, 1.5, 2 };
            foreach (double zoom in zooms)
            {
                if (printPreview.Zoom < zoom)
                {
                    printPreview.Zoom = zoom;
                    break;
                }
            }
        }

        private void tsmZoomAuto_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            if (tsmi.Tag != null)
            {
                double k;
                if (double.TryParse(tsmi.Tag.ToString(), out k))
                {
                    if (k > 0)
                        printPreview.Zoom = k / 100.0;
                    else
                        printPreview.AutoZoom = true;
                }
            }
        }

    }
}
