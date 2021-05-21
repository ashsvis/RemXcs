using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Text;
using BaseServer;

namespace RemXcs
{
    public partial class frmLogs : Form
    {
        Form PanelHost;
        string viewLog = String.Empty;
        string LogSQL = String.Empty;
        string LogDateFormat = String.Empty;
        private int viewPos = 0;
        private int viewCount = 34;
        IUpdatePanel updater;

        public frmLogs(Form panelhost, string log)
        {
            InitializeComponent();
            PanelHost = panelhost;
            updater = panelhost as IUpdatePanel;
            viewLog = log;
        }

        private void frmLogs_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void frmLogs_Shown(object sender, EventArgs e)
        {
            int sum = 0;
            ListView lv = lvLogView;
            switch (viewLog)
            {
                case "AlarmLog":
                    LogSQL = "select `snaptime`,`station`,`name`,`param`,`value`," +
                         "`setpoint`,`message`,`descriptor` from `alarmlog`" +
                         " order by `snaptime` desc limit {0},{1}";
                    LogDateFormat = "dd.MM.yy ddd HH:mm:ss.fff";
                    sum = 0;
                    for (int i = 0; i < 7; i++) sum += lv.Columns[i].Width;
                    lv.Columns[7].Width = lv.ClientSize.Width - sum;
                    break;
                case "SwitchLog":
                    LogSQL = "select `snaptime`,`station`,`name`,`param`,`oldvalue`," +
                        "`newvalue`,`descriptor` from `switchlog`" +
                        " order by `snaptime` desc limit {0},{1}";
                    LogDateFormat = "dd.MM.yy ddd HH:mm:ss.fff";
                    lv.Columns[7].Width += lv.Columns[6].Width;
                    lv.Columns.RemoveAt(6);
                    lv.Columns[4].Text = "Было";
                    lv.Columns[5].Text = "Стало";
                    sum = 0;
                    for (int i = 0; i < 6; i++) sum += lv.Columns[i].Width;
                    lv.Columns[6].Width = lv.ClientSize.Width - sum;
                    break;
                case "ChangeLog":
                    LogSQL = "select `snaptime`,`station`,`name`,`param`,`oldvalue`," +
                        "`newvalue`,`autor`,`descriptor` from `changelog`" +
                        " order by `snaptime` desc limit {0},{1}";
                    LogDateFormat = "dd.MM.yy ddd HH:mm:ss.fff";
                    lv.Columns[4].Text = "Было";
                    lv.Columns[5].Text = "Стало";
                    lv.Columns[6].Text = "Кто сделал";
                    lv.Columns[6].Width = lv.Columns[5].Width;
                    sum = 0;
                    for (int i = 0; i < 7; i++) sum += lv.Columns[i].Width;
                    lv.Columns[7].Width = lv.ClientSize.Width - sum;
                    break;
                case "SystemLog":
                    LogSQL = "select `snaptime`,`station`,`name`," +
                        "`descriptor` from `systemlog`" +
                        " order by `snaptime` desc limit {0},{1}";
                    LogDateFormat = "dd.MM.yy ddd HH:mm:ss";
                    sum = 0;
                    lv.Columns.RemoveAt(4);
                    lv.Columns.RemoveAt(4);
                    lv.Columns.RemoveAt(4);
                    lv.Columns.RemoveAt(4);
                    for (int i = 0; i < 3; i++) sum += lv.Columns[i].Width;
                    lv.Columns[3].Width = lv.ClientSize.Width - sum;
                    break;
            }
            viewCount = calcRowsCount(lvLogView);
            lvLogView.SetDoubleBuffered(true);
            LoadLog(viewPos, viewCount);
            tsbBackward.Enabled = LoadLog(viewPos + viewCount, -1) > 0 ? true : false;
        }

        private int calcRowsCount(ListView lv)
        {   // Автоматический подсчет количества строк, которые умещаются в ListView без прокрутки
            int i = 0;
            ListViewItem item;
            lv.Items.Clear();
            do
            {
                item = new ListViewItem(i++.ToString());
                lv.Items.Add(item);
                item.Selected = true;
                lv.EnsureVisible(item.Index);
            } while (lv.TopItem.Text == "0");
            lv.Items.Clear();
            return i-1;
        }

        List<ListViewItem> reportrows = new List<ListViewItem>();
        public int LoadLog(int pos, int count, bool print = false)
        {
            int result = 0;
            if (pos < 0) return result;
            if (String.IsNullOrWhiteSpace(LogSQL)) return result;
            string SQL = String.Format(LogSQL, pos, Math.Abs(count));
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, false)) // чтение удалённо
            {
                if (mySQL.Connected)
                {
                    try
                    {
                        ArrayList results = mySQL.GetLogRecords(SQL, LogDateFormat);
                        if (count > 0)
                        {
                            reportrows.Clear();
                            int row = 0;
                            foreach (string[] rec in results)
                            {
                                ListViewItem item = new ListViewItem(rec[0]);
                                if (row % 2 != 0) item.BackColor = Color.FromArgb(240, 240, 240);
                                for (int i = 1; i < rec.Length; i++) item.SubItems.Add(rec[i]);
                                reportrows.Add(item);
                                row++;
                            }
                            if (!print)
                            {
                                lvLogView.Items.Clear();
                                lvLogView.Items.AddRange(reportrows.ToArray());
                            }
                        }
                        result = results.Count;
                    }
                    catch (Exception e)
                    {
                        IUpdatePanel up = PanelHost as IUpdatePanel;
                        if (up != null) up.UpdateStatusMessage(e.Message);
                    }
                }
                else
                {
                    IUpdatePanel up = PanelHost as IUpdatePanel;
                    if (up != null) up.UpdateStatusMessage("База данных '" +
                        mySQL.CurrentDatabase + "' сервера недоступна!");
                }
            }
            return result;
        }

        enum GoalRecord
        {
            FirstRecord,
            LastRecord  
        }

        private void GotoRecord(GoalRecord goal)
        {
            if (lvLogView.Items.Count > 0)
            {
                ListViewItem item;
                if (goal == GoalRecord.FirstRecord)
                    item = lvLogView.Items[0];
                else
                    item = lvLogView.Items[lvLogView.Items.Count - 1];
                item.Selected = true;
                lvLogView.FocusedItem = item;
                item.EnsureVisible();
            }
        }

        private void tsbBackward_Click(object sender, EventArgs e)
        {
            viewPos += viewCount;
            LoadLog(viewPos, viewCount);
            GotoRecord(GoalRecord.FirstRecord);
            tsbBackward.Enabled = LoadLog(viewPos + viewCount, -1) > 0  ? true : false;
            tsbForward.Enabled = true;
            tsbEnd.Enabled = true;
        }

        private void tsbForward_Click(object sender, EventArgs e)
        {
            if (viewPos - viewCount >= 0)
            {
                viewPos -= viewCount;
                LoadLog(viewPos < 0 ? 0 : viewPos, viewCount);
                GotoRecord(GoalRecord.LastRecord);
                tsbForward.Enabled = LoadLog(viewPos - viewCount, -1) > 0 ? true : false;
                tsbEnd.Enabled = tsbForward.Enabled;
                tsbBackward.Enabled = true;
            }
            else
                tsbForward.Enabled = false;
        }

        private void SelectEndRecords()
        {
            viewPos = 0;
            LoadLog(viewPos, viewCount);
            GotoRecord(GoalRecord.LastRecord);
            tsbBackward.Enabled = LoadLog(viewPos + viewCount, -1) > 0 ? true : false;
            tsbForward.Enabled = false;
            tsbEnd.Enabled = false;
        }

        private void tsbEnd_Click(object sender, EventArgs e)
        {
            SelectEndRecords();
        }

        private void tsbAnchor_Click(object sender, EventArgs e)
        {
            SelectEndRecords();
        }

        private void frmLogs_Activated(object sender, EventArgs e)
        {
            if (!tsbEnd.Enabled) SelectEndRecords();
        }

        frmPrintPreview form = null;
        private void tsbPreview_Click(object sender, EventArgs e)
        {
            string Caption = "Предварительный просмотр";
            updater.CloseAndRemove(Caption);
            form = new frmPrintPreview();
            updater.ChildForms().Add(form);
            form.printDoc.QueryPageSettings += printDoc_QueryPageSettings;
            form.printDoc.PrintPage += printDoc_PrintPage;
            form.Text = Caption;
            form.MdiParent = PanelHost;
            form.Show();
        }

        private void printDoc_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
            e.PageSettings.Landscape = true;
        }

        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            RectangleF prnrect = e.PageBounds;
            prnrect.Inflate(new Size(-30, -50));
            string sysinfo = "RemX - Автоматизированная система контроля и управления";
            string printdate = "Отпечатано: " + DateTime.Now.ToString();
            string printname = "Журнал: " + this.Text;
            string grdesc = "Без фильтра";
            SizeF strSize;
            PointF strPoint;
            using (Font printfont = new Font("Arial", 8, FontStyle.Italic))
            {
                strSize = e.Graphics.MeasureString(sysinfo, printfont);
                strPoint = prnrect.Location;
                strPoint.Y -= strSize.Height;
                e.Graphics.DrawString(sysinfo, printfont, Brushes.Black, strPoint);
                //------------------
                strSize = e.Graphics.MeasureString(printdate, printfont);
                strPoint = prnrect.Location;
                strPoint.Y += prnrect.Height;
                e.Graphics.DrawString(printdate, printfont, Brushes.Black, strPoint);
                //------------------
                strSize = e.Graphics.MeasureString(grdesc, printfont);
                strPoint = prnrect.Location;
                strPoint.X += prnrect.Width - strSize.Width;
                strPoint.Y += prnrect.Height;
                e.Graphics.DrawString(grdesc, printfont, Brushes.Black, strPoint);
                //------------------
                strSize = e.Graphics.MeasureString(printname, printfont);
                strPoint = prnrect.Location;
                strPoint.X += prnrect.Width - strSize.Width;
                strPoint.Y -= strSize.Height;
                e.Graphics.DrawString(printname, printfont, Brushes.Black, strPoint);
            }
            string tabletext = "Xy";
            int prnviewCount = 0;
            Single[] chWidth = new float[lvLogView.Columns.Count];
            using (Font printfont = new Font("Arial", 8, FontStyle.Regular))
            {
                //Single strHeight = prnrect.Height / (viewCount + 1);
                Single blankHeight = 0;
                Single strHeight = e.Graphics.MeasureString(tabletext, printfont).Height + blankHeight;
                prnviewCount = int.Parse(Math.Truncate(prnrect.Height / strHeight).ToString()) - 1;
                LoadLog(viewPos, prnviewCount, true);
                strPoint.Y = prnrect.Y;
                // Печать заголовка таблицы
                strPoint.X = prnrect.X;
                using (Font headerfont = new Font("Arial Narrow", 8, FontStyle.Bold))
                {
                    foreach (ColumnHeader ch in lvLogView.Columns)
                    {
                        tabletext = ch.Text;
                        Single blankWidth = 10;
                        Single strWidth = e.Graphics.MeasureString(tabletext, headerfont).Width + blankWidth;
                        chWidth[ch.Index] = strWidth;
                        foreach (ListViewItem lvi in reportrows)
                        {
                            strWidth = e.Graphics.MeasureString(
                                lvi.SubItems[ch.Index].Text, printfont).Width + blankWidth;
                            if (strWidth > prnrect.X + prnrect.Width - strPoint.X)
                                strWidth = prnrect.X + prnrect.Width - strPoint.X;
                            if (chWidth[ch.Index] < strWidth) chWidth[ch.Index] = strWidth;
                        }
                        e.Graphics.DrawString(tabletext, headerfont, Brushes.Black, strPoint);
                        strPoint.X += chWidth[ch.Index];
                    }
                }
                strPoint.Y += strHeight;
                int row = 0;
                // Печать строк таблицы
                foreach (ListViewItem lvi in reportrows)
                {
                    if (row > prnviewCount - 1) break;
                    strPoint.X = prnrect.X;
                    if (row % 2 != 0)
                        using (SolidBrush brush = new SolidBrush(Color.FromArgb(240, 240, 240))) 
                            e.Graphics.FillRectangle(brush,
                                strPoint.X, strPoint.Y, prnrect.Width, strHeight);
                    row++;
                    foreach (ColumnHeader ch in lvLogView.Columns)
                    {
                        tabletext = lvi.SubItems[ch.Index].Text;
                        strSize = e.Graphics.MeasureString(tabletext, printfont);
                        RectangleF rect = new RectangleF(strPoint,
                            new SizeF(chWidth[ch.Index], strHeight));
                        e.Graphics.DrawString(tabletext, printfont, Brushes.Black, rect);
                        strPoint.X += chWidth[ch.Index];
                    }
                    strPoint.Y += strHeight;
                }
                reportrows.Clear();
            }
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog(this) == DialogResult.OK)
            {
                printDoc.Print();
            }
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            saveCSVFileDialog.FileName = this.Text + ".csv";
            if (saveCSVFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                    List<string> lines = new List<string>(10001);
                    List<string> columns = new List<string>(11);
                    foreach (ColumnHeader ch in lvLogView.Columns) columns.Add(ch.Text);
                    lines.Add(String.Join(";", columns.ToArray()));
                    LoadLog(0, 10000, true);
                    foreach (ListViewItem lvi in reportrows)
                    {
                        columns.Clear();
                        foreach (ColumnHeader ch in lvLogView.Columns) columns.Add(lvi.SubItems[ch.Index].Text);
                        lines.Add(String.Join(";", columns.ToArray()));
                    }
                    reportrows.Clear();
                    System.IO.File.WriteAllLines(saveCSVFileDialog.FileName, lines.ToArray(), Encoding.Default);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

    }
}
