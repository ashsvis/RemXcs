using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Security.Permissions;
using System.Windows.Forms;
using BaseServer;
using Points.Plugins;

namespace FetchOPC
{
    public partial class frmFetchingOPC : Form
    {
        private IDictionary<string, Entity> fetchlist = new Dictionary<string, Entity>();
        OpcBridgeSupport opc;
        bool Registered = false;
        bool Bonus = false;
        DateTime TurnOnTime;
        string ClientID = String.Empty;

        public frmFetchingOPC()
        {
            InitializeComponent();
            opc = new OpcBridgeSupport();
            this.Size = new System.Drawing.Size();
        }

        private void frmFetching_Load(object sender, EventArgs e)
        {
            Process process = RunningInstance();
            if (process != null)
            {
                Application.Exit();
                return;
            }
            Data.RestoreSQLsettings(Application.StartupPath);
            TurnOnTime = DateTime.Now;
            // Проверка авторизации
            Properties.Settings settings = Properties.Settings.Default;
            Registered = Data.Authorization(settings.CopyOwner, settings.CopyKey);
            if (!Registered) notifyIcon.Text += " (нет авторизации)";
            Bonus = !Registered;
            // Загрузка плагина OPC точек
            PointPlugin.LoadPlugin(Application.StartupPath + "\\Points.OPC.dll");
            // Инициализация
            opc.InitOPC();
            // Инициализация, создание баз, если их раньше не было
            Settings.CreateDataAndFetchBases();
            Data.SendToSystemLog(Properties.Settings.Default.Station,
                "Опрос OPC", "Сервер опроса загружен");
            ClientID = Data.ClientLogin(ClientID, "F", Properties.Settings.Default.Station,
                "Сервер опроса OPC");
            // Заполнение списка для визуализации
            LoadFirst();
            fetchTimer.Enabled = true;
        }

        private void miClose_Click(object sender, EventArgs e)
        {
            fetchTimer.Enabled = false;
            backgroundUpdateChilds.CancelAsync();
            Application.Exit();
        }

        private void fillShowList()
        {
            if (showlist != null && showlist.Text.Length > 0)
            {
                showlist.lvItems.BeginUpdate();
                try
                {
                    showlist.lvItems.Items.Clear();
                    foreach (KeyValuePair<string, Entity> kvp in Data.Entities())
                    {
                        string ptname = kvp.Key;
                        Entity ent = kvp.Value;
                        int ptKind = int.Parse(ent.Values["PtKind"].ToString());
                        ////if (ptKind != PtKind.Group && ptKind != PtKind.Table) continue;
                        ListViewItem item = showlist.lvItems.Items.Add(ptname);
                        string ptdesc = ent.Values["PtDesc"].ToString();
                        item.SubItems.Add(ptdesc);
                        item.SubItems.Add(""); // valur
                        item.SubItems.Add(""); // kind
                        item.SubItems.Add(""); // time
                    }
                }
                finally
                {
                    showlist.lvItems.EndUpdate();
                }
            }
        }

        private void LoadFirst()
        {
            // Загрузка OPC точек из базы данных
            Data.LoadBase(PointPlugin.LoadPlugin(Application.StartupPath + "\\Points.OPC.dll"));
            string station = Properties.Settings.Default.Station.ToString();
            fillShowList();
            using (ServerSQL dataSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
            {
                using (ServerSQL fetchSQL = new ServerSQL(DatabaseFrom.Fetchbase, true)) // чтение
                {
                    if (dataSQL.Connected && fetchSQL.Connected)
                    {
                        foreach (KeyValuePair<string, Entity> kvp in Data.Entities())
                        {
                            string ptname = kvp.Key;
                            Entity ent = kvp.Value;
                            ent.Reals = Data.GetRealValues(ptname, fetchSQL, dataSQL);
                            ent.SetRealProp("Station", station);
                            ent.SetRealProp("Alarms", String.Empty);
                            ent.SetRealProp("LostAlarms", String.Empty);
                            ent.SetRealProp("QuitAlarms", true.ToString());
                            Data.WriteRealVals(ent, fetchSQL);
                            //ent.RemoveAlarms();
                            //ent.RemoveSwitchs();
                            if (ent.Values.ContainsKey("Server") && ent.Values.ContainsKey("Group") &&
                                ent.Values.ContainsKey("Item"))
                            {
                                string server = ent.Values["Server"].ToString();
                                string group = ent.Values["Group"].ToString();
                                string param = ent.Values["Item"].ToString();
                                opc.AddItem(server, group, param);
                            }
                        }
                    }
                }
            }
        }

        frmShowList showlist = null;
        private void miFetchList_Click(object sender, EventArgs e)
        {
            if (showlist != null && showlist.Text.Length > 0)
            {
                showlist.Show();
                showlist.BringToFront();
            }
            else
            {
                showlist = new frmShowList();
                fillShowList();
                showlist.Show();
            }
        }

        private void frmFetching_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Завершение
            Data.SendToSystemLog(Properties.Settings.Default.Station,
                "Опрос OPC", "Сервер опроса выгружен");
            Data.ClientLogout(ClientID);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowList();
        }

        private void ShowList()
        {
            if (!(showlist != null && showlist.Text.Length > 0))
            {
                showlist = new frmShowList();
                fillShowList();
            }
            showlist.Show();
            showlist.BringToFront();
        }

        bool mustReload = false;
        private void fetchTimer_Tick(object sender, EventArgs e)
        {
            fetchTimer.Enabled = false;
            try
            {
                if (!backImLive.IsBusy)
                    backImLive.RunWorkerAsync(
                        new Tuple<string, int, string>(ClientID,
                            Properties.Settings.Default.Station,
                            "Сервер опроса OPC"));

                if (!Registered)
                {
                    if (DateTime.Now.AddHours(-2.5) > TurnOnTime)
                    {
                        Bonus = false;
                        Data.SendToSystemLog(Properties.Settings.Default.Station,
                            "Опрос OPC", "Прекращена работа неавторизованного сервера опроса");
                    }
                }
                if (Registered || Bonus)
                {
                    if (!backCheckCommand.IsBusy)
                        backCheckCommand.RunWorkerAsync(
                            new Tuple<string, int, string>(ClientID,
                            Properties.Settings.Default.Station,
                            "Сервер опроса OPC"));
                            //new object[] { ClientID, Properties.Settings.Default.Station,
                            //"Сервер опроса OPC" });

                    FetchBase();
                }
                else
                    this.Close();
            }
            finally
            { 
                fetchTimer.Enabled = true;
            }
        }

        private void FetchBase()
        {
            if (mustReload)
            { 
                LoadFirst();
                Data.SendToSystemLog(Properties.Settings.Default.Station,
                    "Опрос OPC", "Сервер опроса перезагрузил свой список опроса");
                mustReload = false;
            }
            if (!backgroundUpdateChilds.IsBusy)
            {
                if (fetchlist.Count == 0)
                    foreach (KeyValuePair<string, Entity> kvp in Data.Entities())
                        fetchlist.Add(kvp.Key, kvp.Value);
                backgroundUpdateChilds.RunWorkerAsync(fetchlist);
            }
        }

        private void backgroundUpdateChilds_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            FetchAsync(e.Argument, worker, e);
        }

        private void FetchAsync(object argument, BackgroundWorker worker, DoWorkEventArgs e)
        {
            IDictionary<string, Entity> list = (IDictionary<string, Entity>)argument;
            if (list.Count == 0)
            {
                worker.ReportProgress(0, "ОШИБКА;Список опроса пуст!");
                return;
            }
            using (ServerSQL dataSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
            {
                using (ServerSQL fetchSQL = new ServerSQL(DatabaseFrom.Fetchbase, true)) // чтение
                {
                    if (dataSQL.Connected && fetchSQL.Connected)
                    {
                        int count = 1;
                        foreach (KeyValuePair<string, Entity> kvp in list)
                        {
                            string ptname = kvp.Key;
                            Entity ent = kvp.Value;
                            ent.Reals = Data.GetRealValues(ptname, fetchSQL, dataSQL);
                            if (ent.Reals.Count == 0)
                            {
                                worker.ReportProgress(count / list.Count, "ОШИБКА;Нет связи с SQL");
                                continue;
                            }
                            if (!mustReload) mustReload = (ent.Reals.ContainsKey("Version")) ?
                                DateTime.Parse(ent.Reals["Version"]) > Data.Version : false;
                            if ((bool)ent.Values["Actived"])
                            {
                                bool fetch;
                                if (ent.Reals.ContainsKey("FactTime"))
                                {
                                    double seconds = (DateTime.Now - DateTime.Parse(ent.Reals["FactTime"])).TotalSeconds;
                                    fetch = seconds >= (double)(int)ent.Values["FetchTime"];
                                }
                                else fetch = true;
                                if (fetch)
                                {
                                    if (ent.Values.ContainsKey("Server") && ent.Values.ContainsKey("Group") &&
                                        ent.Values.ContainsKey("Item") && ent.Values.ContainsKey("CDT"))
                                    {
                                        string server = ent.Values["Server"].ToString();
                                        string group = ent.Values["Group"].ToString();
                                        string param = ent.Values["Item"].ToString();
                                        int cdt = int.Parse(ent.Values["CDT"].ToString());
                                        worker.ReportProgress(count / list.Count, "ОШИБКА;Опрос " + ptname + "...");
                                        try
                                        {
                                            string rawvalue = opc.FetchItem(server, group, param); // получение данных
                                            string[] vals = rawvalue.Split(new char[] { ';' });
                                            if (vals.Length == 3)
                                            {
                                                string value = vals[0];
                                                string quality = vals[1];
                                                string time = vals[2];
                                                ent.SetRealProp("Raw", value);
                                                ent.SetRealProp("Quality", quality);
                                                ent.Reals = ent.Plugin.Fetch(ptname, ent); // Выполнение кода плагина
                                                Data.WriteRealVals(ent, fetchSQL);
                                                string pv = (ent.Reals.ContainsKey("PV")) ? ent.Reals["PV"] : "------";
                                                worker.ReportProgress(count / list.Count,
                                                    String.Format("{0};{1};{2};{3}",
                                                    pv, ptname, quality, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                            }
                                            else
                                                worker.ReportProgress(count / list.Count,
                                                    "ОШИБКА;Ошибки связи с сервером OPC \"" + server + "\"!");

                                        }
                                        catch
                                        {
                                            worker.ReportProgress(count / list.Count,
                                                "ОШИБКА;Нет связи с сервером OPC \"" + server + "\"!");
                                        }
                                    }
                                }
                            }
                            if (worker.CancellationPending) { e.Cancel = true; break; }
                            count++;
                        }
                    }
                }
            }
        }

        private string lastError = String.Empty;
        private void backgroundUpdateChilds_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            string[] info = e.UserState.ToString().Split(new char[] { ';' });
            if (info[0].Equals("ОШИБКА"))
            {
                if (showlist != null && showlist.Text.Length > 0)
                    showlist.lbMessage.Text = info[1];
                if (!info[1].StartsWith("Опрос") && lastError != info[1])
                {
                    lastError = info[1];
                    Data.SendToSystemLog(Properties.Settings.Default.Station,
                        "Опрос OPC", lastError);
                }
            }
            else
            {
                string pv = info[0];
                string ptname = info[1];
                string quality = info[2];
                string time = info[3];
                ShowInList(pv, ptname, quality, time);
                if (!backUpdate.IsBusy)
                    backUpdate.RunWorkerAsync(new string[] { pv, ptname, quality, time, ClientID });
            }
        }

        private void backUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] info = (string[])e.Argument;
            string pv = info[0];
            string ptname = info[1];
            string quality = info[2];
            string time = info[3];
            string clientID = info[4];
            Entity ent = Data.GetEntity(ptname);
            if (!ent.Empty)
            {
                string ptdesc = (string)ent.Values["PtDesc"];
                Data.UpdateClientFetchList(clientID, ptname, ptdesc, pv, quality, time);
            }
        }

        private void backgroundUpdateChilds_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            fetchlist.Clear();
        }

        private void ShowInList(string pv, string ptname, string quality, string time)
        {
            if (showlist != null && showlist.Text.Length > 0)
            {
                foreach (ListViewItem item in showlist.lvItems.Items)
                {
                    if (item.Text.Equals(ptname))
                    {
                        item.SubItems[2].Text = pv;
                        item.SubItems[3].Text = quality;
                        item.SubItems[4].Text = time;
                    }
                }
            }
        }

        private void miFreshList_Click(object sender, EventArgs e)
        {
            fetchTimer.Enabled = false;
            // Заполнение списка для визуализации
            LoadFirst();
            fetchTimer.Enabled = true;
        }

        private void miTuning_Click(object sender, EventArgs e)
        {
            Tuning();
        }

        private void Tuning()
        {
            using (frmTuningOPC form = new frmTuningOPC())
            {
                form.cbStation.SelectedIndex = Properties.Settings.Default.Station - 1;
                form.tbCopyOwner.Text = Properties.Settings.Default.CopyOwner;
                form.tbCopyCode.Text = Data.MachineCode(Properties.Settings.Default.CopyOwner);
                form.tbCopyKey.Text = Properties.Settings.Default.CopyKey;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings settings = Properties.Settings.Default;
                    settings.Station = form.cbStation.SelectedIndex + 1;
                    if (settings.CopyOwner != form.tbCopyOwner.Text ||
                        settings.CopyKey != form.tbCopyKey.Text)
                    {
                        settings.CopyOwner = form.tbCopyOwner.Text;
                        settings.CopyKey = form.tbCopyKey.Text;
                        if (!form.CheckAuthorization())
                            MessageBox.Show(this, "Ключ копии ошибочный!", "Авторизация",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    settings.Save();
                }
            }
        }

        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            // Просматриваем все процессы
            foreach (Process process in processes)
            {
                // игнорируем текщий процесс
                if (process.Id != current.Id)
                {
                    // проверяем, что процесс запущен из того же файла
                    if (Assembly.GetExecutingAssembly().Location.Replace(
                        "/", "\\") == current.MainModule.FileName)
                    {
                        // Да, это и есть копия нашего приложения
                        return process;
                    }
                }
            }
            // нет, таких процессов не найдено
            return null;
        }

        private void frmFetchingOPC_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Minimized;
                ShowList();
            }
        }

        private void backImLive_DoWork(object sender, DoWorkEventArgs e)
        {
            Tuple<string, int, string> args = (Tuple<string, int, string>)e.Argument;
            string clientID = args.Item1; 
            int stationNumber = args.Item2; 
            string serverName = args.Item3; 

            if (!Data.ImLive(clientID, "F", stationNumber, serverName))
            {
                clientID = Data.ClientLogin(clientID, "F", stationNumber, serverName);
                e.Result = clientID;
            }
        }

        private void backImLive_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
                ClientID = (string)e.Result;
        }

        private void backCheckCommand_DoWork(object sender, DoWorkEventArgs e)
        {
            Tuple<string, int, string> args = (Tuple<string, int, string>)e.Argument;
            string clientID = args.Item1; 
            int stationNumber = args.Item2;
            string serverName = args.Item3;

            string[] command = Data.GetClientCommand(clientID);
            if (command[0].Equals("RELOAD"))
            {
                Data.ClearClientFetchList(clientID);
                Data.SendClientAnswers(clientID, "RELOADED");
                string mess = serverName + " будет перезагружен по требованию";
                Data.SendToSystemLog(stationNumber, "Опрос OPC", mess);
                e.Result = "RELOAD";
                return;
            }
        }

        private void backCheckCommand_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                switch ((string)e.Result)
                {
                    case "RELOAD":
                        mustReload = true;
                        break;
                }
            }
        }

    }
}
