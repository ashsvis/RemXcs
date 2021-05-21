using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BaseServer;

namespace RemXcs
{
    public partial class frmSwitchs : Form
    {
        Form PanelHost;
        string LogSQL = String.Empty;
        private int viewPos = 0;
        private int viewCount = 34;
        IUpdatePanel updater;
        public frmSwitchs(Form panelhost)
        {
            InitializeComponent();
            PanelHost = panelhost;
            updater = PanelHost as IUpdatePanel;
        }

        private void frmSwitchs_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            timerBlink.Enabled = true;
        }

        private void frmSwitchs_Shown(object sender, EventArgs e)
        {
            int sum = 0;
            ListView lv = lvLogView;
            LogSQL = "select `snaptime`,`station`,`name`,`param`,`value`," +
                    "`descriptor` from `switchs`" +
                    " order by str_to_date(`snaptime`,'%Y.%m.%d %H:%i:%s.%f') desc limit {0},{1}";
            sum = 0;
            for (int i = 0; i < 5; i++) sum += lv.Columns[i].Width;
            lv.Columns[5].Width = lv.ClientSize.Width - sum;
            viewCount = calcRowsCount(lvLogView);
            lvLogView.SetDoubleBuffered(true);
            LoadLog(viewPos, viewCount, tsbBackward);
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

        public int LoadLog(int pos, int count, ToolStripButton button = null)
        {
            int result = 0;
            if (pos < 0) return result;
            if (String.IsNullOrWhiteSpace(LogSQL)) return result;
            string SQL = String.Format(LogSQL, pos, Math.Abs(count));
            if (!backgroundFetch.IsBusy)
            {
                object[] arguments = new object[] { SQL, button, count };
                backgroundFetch.RunWorkerAsync(arguments);
            }
            return result;
        }

        private void backgroundFetch_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            object[] arguments = (object[])e.Argument;
            string SQL = (string)arguments[0];
            ToolStripButton button = (ToolStripButton)arguments[1];
            int count = (int)arguments[2];
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase, true)) // чтение
            {
                if (mySQL.Connected)
                {
                    ArrayList result = mySQL.GetSwitchRecords(SQL);
                    object[] results = new object[] { result, button, count };
                    BackgroundWorker worker = sender as BackgroundWorker;
                    worker.ReportProgress(0, results);
                }
            }
        }

        private void backgroundFetch_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            object[] results = (object[])e.UserState;
            ArrayList result = (ArrayList)results[0];
            ToolStripButton button = (ToolStripButton)results[1];
            int count = (int)results[2];
            if (count > 0)
            {
                lvLogView.Items.Clear();
                int row = 0;
                foreach (string[] rec in result)
                {
                    ListViewItem item = new ListViewItem(rec[0]);
                    if (row % 2 != 0) item.BackColor = Color.FromArgb(240, 240, 240);
                    for (int i = 1; i < rec.Length; i++) item.SubItems.Add(rec[i]);
                    lvLogView.Items.Add(item);
                    row++;
                }

            }
            if (button != null)
                button.Enabled = (result.Count > 0) ? true : false;
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
            LoadLog(viewPos, viewCount, tsbBackward);
            GotoRecord(GoalRecord.FirstRecord);
            tsbForward.Enabled = true;
            tsbEnd.Enabled = true;
        }

        private void tsbForward_Click(object sender, EventArgs e)
        {
            if (viewPos - viewCount >= 0)
            {
                viewPos -= viewCount;
                LoadLog(viewPos < 0 ? 0 : viewPos, viewCount, tsbForward);
                GotoRecord(GoalRecord.LastRecord);
                tsbEnd.Enabled = tsbForward.Enabled;
                tsbBackward.Enabled = true;
            }
            else
                tsbForward.Enabled = false;
        }

        private void SelectEndRecords()
        {
            viewPos = 0;
            LoadLog(viewPos, viewCount, tsbBackward);
            //GotoRecord(GoalRecord.FirstRecord);
            tsbForward.Enabled = false;
            tsbEnd.Enabled = false;
        }

        private void tsbEnd_Click(object sender, EventArgs e)
        {
            SelectEndRecords();
        }

        private void frmAlarms_Activated(object sender, EventArgs e)
        {
            if (!tsbEnd.Enabled) SelectEndRecords();
        }

        private void timerBlink_Tick(object sender, EventArgs e)
        {
            //if (updater.SQLServerConnected())
            //{
                if (!tsbEnd.Enabled) SelectEndRecords();
            //}
        }

    }
}
