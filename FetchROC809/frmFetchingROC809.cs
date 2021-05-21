using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using BaseServer;
using Points.Plugins;

namespace FetchROC809
{
    public partial class frmFetchingROC809 : Form
    {
        private IDictionary<string, Entity> fetchlist = new Dictionary<string, Entity>();
        //int LastMin = -1; int LastHour = -1; int LastDay = -1; int LastMonth = -1;
        bool Registered = false;
        bool Bonus = false;
        DateTime TurnOnTime;
        string ClientID = String.Empty;

        public frmFetchingROC809()
        {
            InitializeComponent();
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
            // Загрузка плагина ROC809 точек
            PointPlugin.LoadPlugin(Application.StartupPath + "\\Points.ROC809.dll");
            // Инициализация, создание баз, если их раньше не было
            Settings.CreateDataAndFetchBases();
            Data.SendToSystemLog(Properties.Settings.Default.Station,
                "Опрос ROC809", "Сервер опроса загружен");
            ClientID = Data.ClientLogin(ClientID, "F", Properties.Settings.Default.Station,
                "Сервер опроса ROC809");
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
                        if (ptKind != PtKind.Group && ptKind != PtKind.Table) continue;
                        ListViewItem item = showlist.lvItems.Items.Add(ptname);
                        string ptdesc = ent.Values["PtDesc"].ToString();
                        item.SubItems.Add(ptdesc);
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
            // Загрузка ROC809 точек из базы данных
            Data.LoadBase(PointPlugin.LoadPlugin(Application.StartupPath + "\\Points.ROC809.dll"));
            string station = Properties.Settings.Default.Station.ToString();
            fillShowList();
            using (ServerSQL dataSQL = new ServerSQL(DatabaseFrom.Database, false)) // чтение
            {
                using (ServerSQL fetchSQL = new ServerSQL(DatabaseFrom.Fetchbase, false)) // чтение
                {
                    if (dataSQL.Connected && fetchSQL.Connected)
                    {
                        foreach (KeyValuePair<string, Entity> kvp in Data.Entities())
                        {
                            string ptname = kvp.Key;
                            Entity ent = kvp.Value;
                            int ptKind = int.Parse(ent.Values["PtKind"].ToString());
                            if (ptKind != PtKind.Group && ptKind != PtKind.Table) continue;
                            switch (ptKind)
                            {
                                case PtKind.Group:
                                    int fetchtime = (int)ent.Values["FetchTime"];
                                    for (int i = 1; i <= 32; i++)
                                    {
                                        string childname = (string)ent.Values["Child" + i];
                                        if (!String.IsNullOrWhiteSpace(childname))
                                        {
                                            Entity child = Data.GetEntity(childname, dataSQL);
                                            if (!child.Empty && (bool)child.Values["Actived"])
                                            {
                                                child.Reals =
                                                    Data.GetRealValues(childname, fetchSQL, dataSQL);
                                                child.SetRealProp("FetchGroup", ptname);
                                                child.SetRealProp("GroupFetchTime", fetchtime.ToString());
                                                child.SetRealProp("Station", station);
                                                child.SetRealProp("Alarms", String.Empty);
                                                child.SetRealProp("LostAlarms", String.Empty);
                                                child.SetRealProp("QuitAlarms", true.ToString());
                                                Data.WriteRealVals(child, fetchSQL);
                                                //child.RemoveAlarms();
                                                //ent.RemoveSwitchs();
                                            }
                                        }
                                    }
                                    break;
                                case PtKind.Table:
                                    ent.Reals = Data.GetRealValues(ptname, fetchSQL, dataSQL);
                                    ent.SetRealProp("DataCount", "0");
                                    ent.SetRealProp("Stable", false.ToString());
                                    ent.SetRealProp("LastSnap", DateTime.MinValue.ToString());
                                    Data.WriteRealVals(ent, fetchSQL);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        frmShowList showlist = null;
        private void miFetchList_Click(object sender, EventArgs e)
        {
            ShowList();
        }

        private void frmFetching_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Завершение
            Data.SendToSystemLog(Properties.Settings.Default.Station,
                "Опрос ROC809", "Сервер опроса выгружен");
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
                        new object[] { ClientID, Properties.Settings.Default.Station,
                        "Сервер опроса ROC809"});

                if (!Registered)
                {
                    if (DateTime.Now.AddHours(-2.5) > TurnOnTime)
                    {
                        Bonus = false;
                        Data.SendToSystemLog(Properties.Settings.Default.Station,
                            "Опрос ROC809", "Прекращена работа неавторизованного сервера опроса");
                    }
                }
                if (Registered || Bonus)
                {
                    if (!backCheckCommand.IsBusy)
                        backCheckCommand.RunWorkerAsync(
                            new object[] { ClientID, Properties.Settings.Default.Station,
                            "Сервер опроса ROC809" });
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
                    "Опрос ROC809", "Сервер опроса перезагрузил свой список опроса");
                mustReload = false;
            }
            if (!backgroundUpdateChilds.IsBusy)
            {
                if (fetchlist.Count == 0)
                {
                    foreach (KeyValuePair<string, Entity> kvp in Data.Entities())
                    {
                        Entity ent = kvp.Value;
                        int ptKind = int.Parse(ent.Values["PtKind"].ToString());
                        if (ptKind != PtKind.Group && ptKind != PtKind.Table) continue;
                        fetchlist.Add(kvp.Key, kvp.Value);
                    }
                }
                backgroundUpdateChilds.RunWorkerAsync(fetchlist);
            }
        }

        private void backgroundUpdateChilds_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            FetchAsync(e.Argument, worker, e);
        }

        private byte[] EncodeData(params byte[] list)
        {
            byte[] result = new byte[list.Length];
            for (int i = 0; i < list.Length; i++) result[i] = (byte)(int)list[i];
            return result;
        }

        private string getStringValue(BackgroundWorker worker, int address)
        {
            #region ----------- Обмен с сокетом по TCP/IP -----------------
            int regcount; Single[] fetchvals;
            bool fetchError;
            int node = Properties.Settings.Default.NodeAddr;
            int func = 4;
            int datacount = 1;
            fetchError = FetchTCP(worker, node, func, address, datacount, out regcount, out fetchvals);
            #endregion ----------------------------------------------------
            if (!fetchError && regcount == 1)
            {
                StringBuilder value = new StringBuilder(fetchvals[0].ToString("0"));
                while (value.Length < 6) value.Insert(0, "0");
                return value.ToString();
            }
            else
                return String.Empty;
        }

        private DateTime DateTimeFromController(BackgroundWorker worker)
        {
            string sdate = getStringValue(worker, Properties.Settings.Default.DateAddr); //ммддгг
            string stime = getStringValue(worker, Properties.Settings.Default.TimeAddr); //ччммсс
            if (sdate.Length == 6 && stime.Length == 6)
            {
                int year = 0;
                int.TryParse(sdate.Substring(4, 2), out year);
                int month = 0;
                int.TryParse(sdate.Substring(0, 2), out month);
                int day = 0;
                int.TryParse(sdate.Substring(2, 2), out day);
                int hour = 0;
                int.TryParse(stime.Substring(0, 2), out hour);
                int minute = 0;
                int.TryParse(stime.Substring(2, 2), out minute);
                int second = 0;
                int.TryParse(stime.Substring(4, 2), out second);
                DateTime dt;
                String sdatetime = String.Format("{0:00}.{1:00}.{2:00} {3:00}:{4:00}:{5:00}",
                    day, month, year, hour, minute, second);
                if (DateTime.TryParse(sdatetime, out dt))
                {
                    return dt;
                }
            }
            return DateTime.Now;
        }

        private void FetchAsync(object argument, BackgroundWorker worker, DoWorkEventArgs e)
        {
            IDictionary<string, Entity> list = (IDictionary<string, Entity>)argument;
            if (list.Count == 0)
            {
                worker.ReportProgress(0,"ОШИБКА;Список опроса пуст!");
                return;
            }
            using (ServerSQL dataSQL = new ServerSQL(DatabaseFrom.Database, false)) // чтение
            {
                using (ServerSQL fetchSQL = new ServerSQL(DatabaseFrom.Fetchbase, false)) // чтение
                {
                    if (dataSQL.Connected && fetchSQL.Connected)
                    {
                        #region определение типа таблицы tabletype
                        //int mm, hh, dd, MM;
                        DateTime NowTime = DateTimeFromController(worker); // DateTime.Now;
                        //mm = NowTime.Minute; hh = NowTime.Hour; dd = NowTime.Day; MM = NowTime.Month;
                        //int tabletype = -1;
                        //if (LastMin != mm)
                        //{
                        //    LastMin = mm; tabletype = 0;
                        //    if (LastHour != hh)
                        //    {
                        //        LastHour = hh; tabletype = 1;
                        //        if (LastDay != dd)
                        //        {
                        //            LastDay = dd; tabletype = 2;
                        //            if (LastMonth != MM) { LastMonth = MM; tabletype = 3; }
                        //        }
                        //    }
                        //}
                        #endregion
                        int count = 1;
                        foreach (KeyValuePair<string, Entity> kvp in list)
                        {
                            string ptname = kvp.Key;
                            Entity ent = kvp.Value;
                            int ptKind = int.Parse(ent.Values["PtKind"].ToString());
                            if (ptKind != PtKind.Group && ptKind != PtKind.Table) continue;
                            ent.Reals = Data.GetRealValues(ptname, fetchSQL, dataSQL);
                            if (ent.Reals.Count == 0)
                            {
                                worker.ReportProgress(0, "ОШИБКА;Нет связи с SQL");
                                continue;
                            }
                            if (!mustReload) mustReload = (ent.Reals.ContainsKey("Version")) ?
                                DateTime.Parse(ent.Reals["Version"]) > Data.Version : false;
                            if ((bool)ent.Values["Actived"])
                            {
                                bool fetch;
                                if (ent.Reals.ContainsKey("FactTime"))
                                {
                                    DateTime facttime = DateTime.Parse(ent.Reals["FactTime"]);
                                    double seconds = (DateTime.Now - facttime).TotalSeconds;
                                    fetch = seconds >= (double)(int)ent.Values["FetchTime"];
                                }
                                else fetch = true;
                                if (fetch)
                                {
                                    if (ent.Values.ContainsKey("Channel") && ent.Values.ContainsKey("Node") &&
                                        ent.Values.ContainsKey("Address"))
                                    {
                                        int channel = (int)ent.Values["Channel"];
                                        int node = (int)ent.Values["Node"];
                                        int address = (int)ent.Values["Address"];
                                        int datacount = 0;
                                        if (ptKind == PtKind.Group)
                                            if (ent.Values.ContainsKey("DataCount"))
                                                datacount = (int)ent.Values["DataCount"];
                                        if (ptKind == PtKind.Table)
                                        {
                                            bool stable = false;
                                            if (ent.Reals.ContainsKey("Stable"))
                                                stable = bool.Parse(ent.Reals["Stable"]);
                                            //if (stable)
                                            //{
                                            //    if ((int)ent.Values["TableType"] > tabletype)
                                            //        continue;
                                            //}
                                            if (!ent.Reals.ContainsKey("DataCount")) ent.SetRealProp("DataCount", "0");
                                            datacount = int.Parse(ent.Reals["DataCount"]);
                                        }
                                        ent.SetRealProp("Quality", "GOOD");
                                        double seconds = 0;
                                        if (ent.Reals.ContainsKey("FactTime"))
                                        {
                                            DateTime facttime = DateTime.Parse(ent.Reals["FactTime"]);
                                            seconds = (DateTime.Now - facttime).TotalSeconds;
                                            ent.SetRealProp("Seconds", seconds.ToString("#.#"));
                                        }
                                        ent.SetRealProp("FactTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                        Data.WriteRealVals(ent, fetchSQL);
                                        worker.ReportProgress(0, "ОШИБКА;Опрос " + ptname + "...");
                                        #region ----------- Обмен с сокетом по TCP/IP -----------------
                                        int regcount; Single[] fetchvals;
                                        bool fetchError;
                                        int func = (ptKind == PtKind.Group) ? 4 : 3;
                                        fetchError = FetchTCP(worker, node, func, address, datacount, out regcount, out fetchvals);
                                        #endregion ----------------------------------------------------
                                        if (!fetchError)
                                        {
                                            DateTime LastSnap = DateTime.MinValue;
                                            switch (ptKind)
                                            {
                                                case PtKind.Group:
                                                    int station = Properties.Settings.Default.Station;
                                                    DecodeRegisters(station, worker, ent, regcount, fetchvals,
                                                        fetchSQL, dataSQL, seconds);
                                                    worker.ReportProgress(0, ptname + ";" +
                                                        NowTime.ToString("yyyy-MM-dd HH:mm:ss"));
                                                    break;
                                                case PtKind.Table:
                                                    LastSnap = DecodeTables(worker, ent, datacount, regcount, fetchvals,
                                                        fetchSQL, dataSQL);
                                                    worker.ReportProgress(0, ptname + ";" +
                                                        LastSnap.ToString("yyyy-MM-dd HH:mm:ss"));
                                                        break;
                                            }
                                        }
                                        else
                                            worker.ReportProgress(0, ptname + ";нет связи с ROC809");
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

        private bool FetchTCP(BackgroundWorker worker, int node, int func, int address, int datacount,
            out int regcount, out Single[] fetchvals)
        {
            regcount = 0; fetchvals = new Single[regcount];
            bool fetchError;
            byte[] sendBytes = EncodeData(0, 0, 0, 0, 0, 6, (byte)node, (byte)(func),
                (byte)(address >> 8), (byte)(address & 0xff),
                (byte)(datacount >> 8), (byte)(datacount & 0xff));
            //создаём сокет для отправки запроса серверу
            using (Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                ProtocolType.Tcp))
            {
                //создаём соединение                   
                try
                {
                    string host = Properties.Settings.Default.Host;
                    IPEndPoint remoteEP = new IPEndPoint(
                        IPAddress.Parse(Properties.Settings.Default.Host), // 192.168.0.2
                        int.Parse(Properties.Settings.Default.Port)); // 502
                    try
                    {
                        sock.Connect(remoteEP);//здесь указать нужный IP
                        //отправляем запрос на сервер
                        sock.SendTimeout = 5000; // таймаут 5 секунд на запрос
                        sock.Send(sendBytes);
                        byte[] receivedBytes = new byte[1024];
                        sock.ReceiveTimeout = 5000; // таймаут 5 секунд на ответ
                        int numBytes = sock.Receive(receivedBytes); //считали numBytes байт
                        sock.Disconnect(true);
                        // receivedBytes: [0][1][2][3][4][5] - заголовок: [4]*256+[5]= длина блока
                        // [6] - адрес устройства (как получено);
                        // [7] - код функции; [8] - количество байт ответа Modbus устройства;
                        // [9]..[n] - данные, для функции 4: [8]/4= количество регистров.
                        if ((receivedBytes[4] * 256 + receivedBytes[5] == numBytes - 6) &&
                            receivedBytes[6] == node && receivedBytes[7] == func)
                        {
                            fetchError = false;
                            regcount = receivedBytes[8] / 4;
                            fetchvals = new Single[regcount];
                            int n = 9;
                            for (int i = 0; i < regcount; i++)
                            {
                                byte[] raw = new byte[4];
                                raw[0] = receivedBytes[n + 3];
                                raw[1] = receivedBytes[n + 2];
                                raw[2] = receivedBytes[n + 1];
                                raw[3] = receivedBytes[n];
                                Single fval = BitConverter.ToSingle(raw, 0);
                                if (!Single.IsNegativeInfinity(fval) &&
                                    !Single.IsPositiveInfinity(fval))
                                    fetchvals[i] = fval;
                                else
                                    fetchvals[i] = Single.NaN;
                                n += 4;
                            }
                        }
                        else if ((receivedBytes[4] * 256 + receivedBytes[5] == numBytes - 6) &&
                            receivedBytes[6] == node && receivedBytes[7] == (func | 0x80))
                        {
                            fetchError = true;
                            int errorcode = receivedBytes[8];
                            worker.ReportProgress(0, "ОШИБКА;Ошибка связи MODBUS, код: " + errorcode);
                        }
                        else
                        {
                            fetchError = true;
                            worker.ReportProgress(0, "ОШИБКА;Неправильный заголовок ответа от ROC809!");
                        }
                    }
                    catch (Exception except)
                    {
                        fetchError = true;
                        worker.ReportProgress(0, "ОШИБКА;Ошибка связи: " + except.Message);
                    }
                }
                catch (Exception except)
                {
                    fetchError = true;
                    worker.ReportProgress(0, "ОШИБКА;Ошибка конфигурирования: " + except.Message);
                }
            }
            return fetchError;
        }

        private static DateTime DecodeTables(BackgroundWorker worker, Entity ent, int datacount,
            int regcount, Single[] fetchvals, ServerSQL fetchSQL, ServerSQL dataSQL)
        {
            Single rDate = 10100;
            Single rTime = 0;
            DateTime D = DateTime.MinValue;
            int tabletype = (int)ent.Values["TableType"];
            int MM, dd, yy, hh, mm;
            mm = hh = yy = dd = MM = 0;
            for (int i = 1; i <= regcount; i++)
            {
                switch (i)
                {
                    case 1:
                        rDate = fetchvals[0];
                        StringBuilder sDate = new StringBuilder(rDate.ToString());
                        while (sDate.Length < 6) sDate.Insert(0, "0");
                        MM = int.Parse(sDate.ToString().Substring(0, 2));
                        if (MM == 0) MM = 1;
                        dd = int.Parse(sDate.ToString().Substring(2, 2));
                        if (dd == 0) dd = 1;
                        yy = int.Parse(sDate.ToString().Substring(4, 2));
                        break;
                    case 2:
                        rTime = fetchvals[1];
                        StringBuilder sTime = new StringBuilder(rTime.ToString());
                        while (sTime.Length < 4) sTime.Insert(0, "0");
                        hh = int.Parse(sTime.ToString().Substring(0, 2));
                        mm = int.Parse(sTime.ToString().Substring(2, 2));
                        D = DateTime.Parse(String.Format("{0}.{1}.{2} {3}:{4}:00",
                            dd, MM, yy + 2000, hh, mm));
                        // коррекция даты, получаемой от ROC809
                        switch (tabletype)
                        {
                            case 1: D = D.AddHours(-1); break;
                            case 2: D = D.AddDays(-1); break;
                        }
                        break;
                    default:
                        if (rDate > 10100)
                        {
                            string[] childrec =
                                ent.Values["Child" + (i - 2)].ToString().Split(new char[] { ';' });
                            string childname = childrec[0];
                            int accumtype = (childrec.Length == 2) ? int.Parse(childrec[1]) : 0;
                            if (!String.IsNullOrWhiteSpace(childname))
                            {
                                Entity child = Data.GetEntity(childname, dataSQL);
                                if (!child.Empty && (bool)child.Values["Actived"])
                                {
                                    if (!Single.IsNaN(fetchvals[i - 1]))
                                    {
                                        Single raw = fetchvals[i - 1];
                                        Data.AddIntoTable(D, childname + ".PV", raw, true, tabletype, accumtype,
                                            dataSQL);
                                    }
                                    else
                                        Data.AddIntoTable(D, childname + ".PV", 0, false, tabletype, accumtype,
                                            dataSQL);
                                }
                                else
                                    if (!child.Empty)
                                        Data.AddIntoTable(D, childname + ".PV", 0, false, tabletype, accumtype,
                                            dataSQL);
                            }
                        }
                        break;
                }
                if (worker.CancellationPending) break;
            }
            if (!ent.Reals.ContainsKey("LastSnap"))
                ent.SetRealProp("LastSnap", DateTime.MinValue.ToString());
            DateTime lastsnap = DateTime.Parse(ent.Reals["LastSnap"]);
            if (D >= lastsnap)
            {
                if (datacount < (int)ent.Values["RecordCount"] - 1)
                    datacount++;
                else
                    datacount = 0;
                ent.SetRealProp("DataCount", datacount.ToString());
                ent.SetRealProp("Stable", false.ToString());
                ent.SetRealProp("LastSnap", D.ToString());
            }
            else
                ent.SetRealProp("Stable", true.ToString());
            Data.WriteRealVals(ent, fetchSQL);
            return (rDate > 10100) ? D : lastsnap;
        }

        private static void DecodeRegisters(int station, BackgroundWorker worker, Entity ent,
            int regcount, Single[] fetchvals, ServerSQL fetchSQL, ServerSQL dataSQL, double seconds)
        {
            for (int i = 1; i <= regcount; i++)
            {
                string childname = (string)ent.Values["Child" + i];
                if (!String.IsNullOrWhiteSpace(childname))
                {
                    Entity child = Data.GetEntity(childname, dataSQL);
                    if (!child.Empty && (bool)child.Values["Actived"])
                    {
                        child.Reals = Data.GetRealValues(childname, fetchSQL, dataSQL);
                        if (!Single.IsNaN(fetchvals[i - 1]))
                        {
                            Single raw = fetchvals[i - 1];
                            child.SetRealProp("Raw", raw.ToString());
                            child.SetRealProp("Quality", "GOOD");
                        }
                        else
                        {
                            child.SetRealProp("Raw", "0");
                            child.SetRealProp("Quality", "NAN");
                        }
                        child.Reals = child.Plugin.Fetch(childname, child);
                        child.SetRealProp("Seconds", seconds.ToString("#.#"));
                        Data.WriteRealVals(child, fetchSQL);
                    }
                }
                if (worker.CancellationPending) break;
            }
        }

        private void backgroundUpdateChilds_RunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs e)
        {
            fetchlist.Clear();
        }

        private string lastError = String.Empty;
        private void backgroundUpdateChilds_ProgressChanged(object sender, ProgressChangedEventArgs e)
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
                        "Опрос ROC809", lastError);
                }
            }
            else
            {
                string ptname = info[0];
                string time = info[1];
                ShowInList(info[0], info[1]);
                if (!backUpdate.IsBusy)
                    backUpdate.RunWorkerAsync(new string[] { ptname, time, ClientID });
            }
        }

        private void backUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] info = (string[])e.Argument;
            string ptname = info[0];
            string time = info[1];
            string clientID = info[2];
            Entity ent = Data.GetEntity(ptname);
            if (!ent.Empty)
            {
                string ptdesc = (string)ent.Values["PtDesc"];
                Data.UpdateClientFetchList(clientID, ptname, ptdesc, "", "", time);
            }
        }

        private void ShowInList(string ptname, string time)
        {
            if (showlist != null && showlist.Text.Length > 0)
            {
                foreach (ListViewItem item in showlist.lvItems.Items)
                {
                    if (item.Text.Equals(ptname))
                    {
                        item.SubItems[2].Text = time;
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
            using (frmTuningROC809 form = new frmTuningROC809())
            {
                form.cbStation.SelectedIndex = Properties.Settings.Default.Station - 1;
                form.tbIPAddress.Text = Properties.Settings.Default.Host;
                form.tbIPPort.Text = Properties.Settings.Default.Port;
                form.tbNodeAddr.Text = Properties.Settings.Default.NodeAddr.ToString();
                form.tbDateAddr.Text = Properties.Settings.Default.DateAddr.ToString();
                form.tbTimeAddr.Text = Properties.Settings.Default.TimeAddr.ToString();
                form.tbCopyOwner.Text = Properties.Settings.Default.CopyOwner;
                form.tbCopyCode.Text = Data.MachineCode(Properties.Settings.Default.CopyOwner);
                form.tbCopyKey.Text = Properties.Settings.Default.CopyKey;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings settings = Properties.Settings.Default;
                    settings.Station = form.cbStation.SelectedIndex + 1;
                    settings.Host = form.tbIPAddress.Text;
                    settings.Port = form.tbIPPort.Text;
                    int nodeaddr;
                    if (int.TryParse(form.tbNodeAddr.Text, out nodeaddr))
                        settings.NodeAddr = nodeaddr;
                    else
                        form.tbNodeAddr.Text = settings.NodeAddr.ToString();
                    int dateaddr;
                    if (int.TryParse(form.tbDateAddr.Text, out dateaddr))
                        settings.DateAddr = dateaddr;
                    else
                        form.tbDateAddr.Text = settings.DateAddr.ToString();
                    int timeaddr;
                    if (int.TryParse(form.tbTimeAddr.Text, out timeaddr))
                        settings.TimeAddr = timeaddr;
                    else
                        form.tbTimeAddr.Text = settings.TimeAddr.ToString();
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

        private void frmFetchingROC809_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Minimized;
                ShowList();
            }
        }

        private void backImLive_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] args = (object[])e.Argument;
            string clientID = (string)args[0];
            int stationNumber = (int)args[1];
            string serverName = (string)args[2];

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
            object[] args = (object[])e.Argument;
            string clientID = (string)args[0];
            int stationNumber = (int)args[1];
            string serverName = (string)args[2];

            string[] command = Data.GetClientCommand(clientID);
            if (command[0].Equals("RELOAD"))
            {
                Data.ClearClientFetchList(clientID);
                Data.SendClientAnswers(clientID, "RELOADED");
                string mess = serverName + " будет перезагружен по требованию";
                Data.SendToSystemLog(stationNumber, "Опрос ROC809", mess);
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
