using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BaseServer;
using Points.Plugins;

namespace RemXcs
{
    public partial class frmAlarms : Form
    {
        Form PanelHost;
        string LogSQL = String.Empty;
        private int viewPos = 0;
        private int viewCount = 34;
        IUpdatePanel updater;

        public frmAlarms(Form panelhost)
        {
            InitializeComponent();
            PanelHost = panelhost;
            updater = panelhost as IUpdatePanel;
        }

        private void frmAlarms_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            timerBlink.Enabled = true;
        }

        private void frmAlarms_Shown(object sender, EventArgs e)
        {
            int sum = 0;
            ListView lv = lvLogView;
            LogSQL = "select `key`,`snaptime`,`station`,`name`,`param`,`value`," +
                    "`setpoint`,`message`,`descriptor` from `alarms`" +
                    " order by str_to_date(`snaptime`,'%Y.%m.%d %H:%i:%s.%f') desc limit {0},{1}";
            sum = 0;
            for (int i = 0; i < 8; i++) sum += lv.Columns[i].Width;
            lv.Columns[8].Width = lv.ClientSize.Width - sum;
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

        private bool blink = false;
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
                    ArrayList result = mySQL.GetAlarmRecords(SQL);
                    object[] results = new object[] { result, button, count };
                    BackgroundWorker worker = sender as BackgroundWorker;
                    worker.ReportProgress(0, results);
                }
            }
        }

        private void backgroundFetch_ProgressChanged(object sender,
            System.ComponentModel.ProgressChangedEventArgs e)
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
                    item.UseItemStyleForSubItems = false;
                    if (row % 2 != 0) item.BackColor = Color.FromArgb(240, 240, 240);
                    for (int i = 1; i < rec.Length; i++)
                    {
                        item.SubItems.Add(rec[i]);
                        item.SubItems[i].BackColor = item.BackColor;
                    }
                    string ptname = item.SubItems[3].Text;
                    IDictionary<string, string> Reals = Data.GetRealValues(ptname);
                    Color BackColor = (Reals.ContainsKey("BackColor")) ?
                        Color.FromArgb(80, Color.FromArgb(int.Parse(Reals["BackColor"]))) : SystemColors.ButtonFace;
                    Color ForeColor = (Reals.ContainsKey("ForeColor")) ?
                        Color.FromArgb(int.Parse(Reals["ForeColor"])) : SystemColors.ControlText;
                    bool quit = (Reals.ContainsKey("QuitAlarms")) ?
                        bool.Parse(Reals["QuitAlarms"]) : true;
                    bool alarm = (Reals.ContainsKey("HasAlarms")) ?
                        bool.Parse(Reals["HasAlarms"]) : false;
                    bool lostalarm = (Reals.ContainsKey("HasLostAlarms")) ?
                        bool.Parse(Reals["HasLostAlarms"]) : false;
                    string key = item.SubItems[0].Text;
                    switch (key)
                    {
                        case "LL":
                        case "HH":
                        case "LA":
                        case "HA":
                        case "EM":
                            BackColor = Color.FromArgb(80, Color.Red);
                            ForeColor = Color.White;
                            break;
                        case "LE":
                        case "HE":
                            BackColor = Color.FromArgb(80, Color.Magenta);
                            ForeColor = SystemColors.ControlText;
                            break;
                        case "LO":
                        case "HI":
                        case "WA":
                            BackColor = Color.FromArgb(80, Color.Yellow);
                            ForeColor = SystemColors.ControlText;
                            break;
                    }
                    if (!quit && blink)
                    {
                        BackColor = Color.Transparent;
                        ForeColor = SystemColors.ControlText;
                    }
                    item.BackColor = BackColor;
                    item.ForeColor = ForeColor;
                    item.SubItems[0].BackColor = BackColor;
                    item.SubItems[0].ForeColor = ForeColor;
                    lvLogView.Items.Add(item);
                    row++;
                }
            }
            if (button != null)
                button.Enabled = (result.Count  > 0) ? true : false;
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
                if (!tsbEnd.Enabled)
                    SelectEndRecords();
                else
                    LoadLog(viewPos, viewCount);
                tsbQuitAll.Enabled = (updater.UserLevel() >= UserLevel.Оператор);
                blink = !blink;
            //}
        }

        private void tsbQuitAll_Click(object sender, EventArgs e)
        {
            if (updater.UserLevel() >= UserLevel.Оператор)
            {
                updater.AlarmShortUp();
                Data.SendShortUpCommand();
                timerBlink.Enabled = false;
                try
                {
                    using (ServerSQL dataSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
                    {
                        using (ServerSQL fetchlocalSQL = new ServerSQL(DatabaseFrom.Fetchbase, true)) // чтение
                        {
                            if (dataSQL.Connected && fetchlocalSQL.Connected)
                            {
                                HashSet<string> list = new HashSet<string>();
                                for (int i = 0; i < lvLogView.Items.Count; i++)
                                {
                                    string ptname = lvLogView.Items[i].SubItems[3].Text;
                                    if (!list.Contains(ptname))
                                    {
                                        list.Add(ptname);
                                        Entity ent = Data.GetEntity(ptname, dataSQL);
                                        using (ServerSQL fetchSQL = new ServerSQL(DatabaseFrom.Fetchbase)) // запись
                                        {
                                            if (!ent.Empty && fetchSQL.Connected)
                                            {
                                                ent.Reals = Data.GetRealValues(ptname, fetchlocalSQL, dataSQL);
                                                bool propexists = ent.Reals.ContainsKey("QuitAlarms");
                                                if (propexists && !bool.Parse(ent.Reals["QuitAlarms"]) || !propexists)
                                                {
                                                    ent.QuitAlarms(updater.UserName());
                                                    ent.SetRealProp("QuitAlarms", true.ToString());
                                                    Data.WriteRealVals(ent, fetchSQL);
                                                }
                                            }
                                            else
                                                Data.RemoveAlarms(ptname, fetchSQL);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                finally
                { 
                    timerBlink.Enabled = true;
                }
            }
        }

        private void lvLogView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (updater.UserLevel() >= UserLevel.Оператор)
                {
                    updater.AlarmShortUp();
                    Data.SendShortUpCommand();
                    timerBlink.Enabled = false;
                    try
                    {
                        ListViewItem item = lvLogView.GetItemAt(e.X, e.Y);
                        if (item != null)
                        {
                            string ptname = item.SubItems[3].Text;
                            Entity ent = Data.GetEntity(ptname);
                            if (!ent.Empty)
                            {
                                ent.Reals = Data.GetRealValues(ptname);
                                bool propexists = ent.Reals.ContainsKey("QuitAlarms");
                                if (propexists && !bool.Parse(ent.Reals["QuitAlarms"]) || !propexists)
                                {
                                    ent.QuitAlarms(updater.UserName());
                                    ent.SetRealProp("QuitAlarms", true.ToString());
                                    Data.WriteRealVals(ent);
                                }
                            }
                            else
                                Data.RemoveAlarms(ptname);
                        }
                    }
                    finally { timerBlink.Enabled = true; }
                }
                else
                    MessageBox.Show(this,
                        "Недостаточно полномочий у текущего пользователя для выполнения этой операции!",
                        "Квитирование сигнализации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
