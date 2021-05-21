using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaseServer;
using IniFiles.Net;

namespace RemXcs
{
    public partial class frmReportPreview : Form
    {
        PrintReport printReport;
        DateTime lastdate;
        string internalname;

        public frmReportPreview(string name)
        {
            InitializeComponent();
            internalname = name;
            printReport = new PrintReport(printDoc);
            LoadReportProps();
        }

        private void LoadReportProps()
        {
            printReport.LoadReport(internalname, Properties.Settings.Default.ReportsConfig);
            tsbPrintAuto.Checked = printReport.PrintAuto;
            tstbPrintDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
            tstbPrintTime.Text = printReport.PrintTime.ToString("HH:mm");
            int periodindex = printReport.PrintPeriod;
            if (periodindex < tscbPrintPeriod.Items.Count)
            {
                tscbPrintPeriod.SelectedIndexChanged -= tscbPrintPeriod_SelectedIndexChanged;
                tscbPrintPeriod.SelectedIndex = periodindex;
                tscbPrintPeriod.SelectedIndexChanged += tscbPrintPeriod_SelectedIndexChanged;
            }
        }

        public void LoadReport(string name)
        {
            internalname = name;
            lastdate = printReport.PrintDate;
            tstbPrintDate.Text = lastdate.ToString("dd.MM.yyyy");
            LoadReportProps();
            printPreview.InvalidatePreview();
        }

        private void frmReportPreview_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            printPreview.Focus();
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

        private void printDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            printReport.PaintPage(e.Graphics);
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
        }

        private void tstbPrintDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DateTime date;
                if (DateTime.TryParse(tstbPrintDate.Text, out date))
                {
                    lastdate = printReport.PrintDate;
                    printReport.PrintDate = date;
                    printPreview.InvalidatePreview();
                }
                else
                    tstbPrintDate.Text = lastdate.ToString("dd.MM.yyyy");
                tstbPrintDate.SelectAll();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                tstbPrintDate.Text = lastdate.ToString("dd.MM.yyyy");
                tstbPrintDate.SelectAll();
            }
        }

        private void tsbPrintAuto_Click(object sender, EventArgs e)
        {
            printReport.PrintAuto = tsbPrintAuto.Checked;
            //Data.SetReportProps(printReport.ReportName, printReport.PrintAuto,
            //    printReport.PrintTime, printReport.PrintPeriod);
            saveCustomReportProps();
        }

        private void tsbPrintAuto_CheckedChanged(object sender, EventArgs e)
        {
            tsbPrintAuto.Image = (tsbPrintAuto.Checked) ?
                Properties.Resources.on : Properties.Resources.off;
            tstbPrintTime.Enabled = tsbPrintAuto.Checked;
            tslPrintPeriod.Enabled = tsbPrintAuto.Checked;
            tscbPrintPeriod.Enabled = tsbPrintAuto.Checked;
        }

        private void tstbPrintTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DateTime time;
                if (DateTime.TryParse(tstbPrintTime.Text, out time))
                {
                    printReport.PrintTime = time;
                    //Data.SetReportProps(printReport.ReportName, printReport.PrintAuto,
                    //    printReport.PrintTime, printReport.PrintPeriod);
                    saveCustomReportProps();
                }
                else
                    tstbPrintTime.Text = printReport.PrintTime.ToString("HH:mm");
                tstbPrintTime.SelectAll();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                tstbPrintTime.Text = printReport.PrintTime.ToString("HH:mm");
                tstbPrintTime.SelectAll();
            }
        }

        private void tscbPrintPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            printReport.PrintPeriod = tscbPrintPeriod.SelectedIndex;
            saveCustomReportProps();
            //Data.SetReportProps(printReport.ReportName, printReport.PrintAuto,
            //    printReport.PrintTime, printReport.PrintPeriod);
        }

        private void saveCustomReportProps()
        {
            //Data.SetReportProps(printReport.ReportName, printReport.PrintAuto,
            //    printReport.PrintTime, printReport.PrintPeriod);
            MemIniFile mif = new MemIniFile(String.Empty);
            mif.FromString(Properties.Settings.Default.ReportsConfig);
            mif.WriteString(internalname, "ReportName", printReport.ReportName);
            mif.WriteBool(internalname, "PrintAuto", printReport.PrintAuto);
            mif.WriteDate(internalname, "PrintTime", printReport.PrintTime);
            mif.WriteInteger(internalname, "PrintPeriod", printReport.PrintPeriod);
            //mif.UpdateFile();
            Properties.Settings settings = Properties.Settings.Default;
            settings.ReportsConfig = mif.ToString();
            settings.Save();
        }
    }
}
