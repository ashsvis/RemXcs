using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using BaseServer;
using MySql.Data.MySqlClient;

namespace SyncServer
{
    public partial class frmSyncServer : Form
    {
        DateTime TurnOnTime;
        string ClientID = String.Empty;
        bool Registered = false;
        bool Bonus = false;

        public frmSyncServer()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size();
        }

        private void frmSyncServer_Load(object sender, EventArgs e)
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
            // Инициализация, создание баз, если их раньше не было
            Settings.CreateDataAndFetchBases();
            Data.SendToSystemLog(Properties.Settings.Default.Station,
                "Репликация", "Сервер репликации загружен");
            ClientID = Data.ClientLogin(ClientID, "F", Properties.Settings.Default.Station,
                "Сервер репликации баз данных");
            // Заполнение списка для синхронизации
            //LoadFirst();
            fetchTimer.Enabled = true;
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

        private void frmSyncServer_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Minimized;
                ShowList();
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowList();
        }

        private void ShowList()
        {
            if (!(showlist != null && showlist.Text.Length > 0))
            {
                showlist = new frmShowList();
                //fillShowList();
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
                        "Сервер репликации баз данных"});
                
                if (!Registered)
                {
                    if (DateTime.Now.AddHours(-2.5) > TurnOnTime)
                    {
                        Bonus = false;
                        Data.SendToSystemLog(Properties.Settings.Default.Station,
                            "Репликация", "Прекращена работа неавторизованного сервера репликации");
                    }
                }
                if (Registered || Bonus)
                {
                    if (!backCheckCommand.IsBusy)
                        backCheckCommand.RunWorkerAsync(
                            new object[] { ClientID, Properties.Settings.Default.Station,
                            "Сервер репликации баз данных" });
                    SyncBase(Properties.Settings.Default.FetchRecords);
                }
                else
                    this.Close();
            }
            finally
            {
                fetchTimer.Enabled = true;
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
                clientID = Data.ClientLogin(ClientID, "F", stationNumber, serverName);
                e.Result = clientID;
            }
        }

        private void backImLive_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
                ClientID = (string)e.Result;
        }

        private void SyncBase(int fetchrecords)
        {
            if (mustReload)
            {
                hashTableGroups = String.Empty;                
                hashGroups = String.Empty;
                hashGroupNames = String.Empty;
                hashTableGroupNames = String.Empty;
                //LoadFirst();
                Data.SendToSystemLog(Properties.Settings.Default.Station,
                    "Репликация", "Сервер репликации перезагрузил свой список синхронизации");
                mustReload = false;
            }
            if (!backgroundUpdateChilds.IsBusy)
            {
                backgroundUpdateChilds.RunWorkerAsync(fetchrecords);
            }
            if (!backgroundDeleteChilds.IsBusy)
            {
                backgroundDeleteChilds.RunWorkerAsync(fetchrecords);
            }
        }

        private void miClose_Click(object sender, EventArgs e)
        {
            fetchTimer.Enabled = false;
            backgroundUpdateChilds.CancelAsync();
            backgroundDeleteChilds.CancelAsync();
            Application.Exit();
        }

        private void backgroundUpdateChilds_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            SyncronizationAsync(e.Argument, worker, e);
        }

        private void SyncronizationAsync(object argument, BackgroundWorker worker, DoWorkEventArgs e)
        {
            updatestatus(worker, "", DateTime.Now.ToString());
            SyncRealVals(worker, e);
            
            SyncAlarms(worker, e);
            SyncSwitchs(worker, e);
            SyncTrends(worker, e);
            /* исключено из синхронизации
            SyncTables(worker, e);
            SyncAlarmLog(worker, e);
            SyncSwitchLog(worker, e);
            SyncChangeLog(worker, e);
            SyncSystemLog(worker, e);
             */
            updatestatus(worker, "Ожидание изменений...", DateTime.Now.ToString());
        }

        private static void updatestatus(BackgroundWorker worker, string status, string snaptime)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("status", status);
            dict.Add("snaptime", snaptime);
            worker.ReportProgress(0, dict);
        }

        private static string hashClients = String.Empty;

        private static void SyncSwitchs(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL localbaseSQL = new ServerSQL(DatabaseFrom.Fetchbase, true)) // чтение локально
            {
                if (localbaseSQL.Connected)
                {
                    List<object[]> list = new List<object[]>();
                    using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Fetchbase))
                    {
                        if (baseSQL.Connected)
                        {
                            #region выборка позднейшей даты записи в локальной таблице
                            string SQLmaxtime = "select max(str_to_date(`snaptime`,'%Y.%m.%d %H:%i:%s.%f')) from `Switchs`";
                            DateTime maxlocalSnapTime = DateTime.MinValue;
                            using (MySqlCommand localcommand = new MySqlCommand(SQLmaxtime,
                                localbaseSQL.myConnection))
                            {
                                object obj = localcommand.ExecuteScalar();
                                byte[] test = new byte[0];
                                if (obj.GetType() == test.GetType())
                                {
                                    string sobj = Encoding.Default.GetString((byte[])obj);
                                    DateTime.TryParse(sobj, out maxlocalSnapTime);
                                }
                            }
                            #endregion
                            #region выборка из базовой таблицы новых записей в список
                            string SQLlist = "select `station`,`name`,`param`,`value`," +
                                "`descriptor`,`snaptime` from `Switchs`" +
                                " where (str_to_date(`snaptime`,'%Y.%m.%d %H:%i:%s.%f')>@snaptime)";
                            using (MySqlCommand command = new MySqlCommand(SQLlist,
                                baseSQL.myConnection))
                            {
                                command.Parameters.AddWithValue("@snaptime", maxlocalSnapTime);
                                try
                                {
                                    using (MySqlDataReader reader = command.ExecuteReader())
                                    {
                                        if (reader.HasRows)
                                        {
                                            while (reader.Read())
                                            {
                                                if (worker.CancellationPending) { e.Cancel = true; break; }
                                                int station = reader.GetInt32(0);
                                                string name = reader.GetString(1);
                                                string param = reader.GetString(2);
                                                string value = reader.GetString(3);
                                                string descriptor = reader.GetString(4);
                                                string snaptime = reader.GetString(5);
                                                list.Add(new object[6] { station, name, param, value,
                                                    descriptor, snaptime });
                                            }
                                        }
                                    }
                                }
                                catch (MySqlException ex)
                                {
                                    updatestatus(worker, 0, "Switchs", "Ошибка чтения:",
                                        ex.Message, DateTime.Now.ToString());
                                }
                            }
                            #endregion
                        }
                    }
                    #region вставка новых записей из списка в локальную таблицу
                    int row = 0;
                    foreach (object[] item in list)
                    {
                        if (worker.CancellationPending) { e.Cancel = true; break; }
                        int station = (int)item[0];
                        string name = (string)item[1];
                        string param = (string)item[2];
                        string value = (string)item[3];
                        string descriptor = (string)item[4];
                        string snaptime = (string)item[5];
                        localbaseSQL.UpdateSwitch(station, name, param, value, descriptor, snaptime);
                        updatestatus(worker, row++, "Switchs", name + "." + param, "обновление...", snaptime);
                    }
                    updatestatus(worker, row, "Switchs", "синхронизировано",
                        "записей: " + row, DateTime.Now.ToString());
                    #endregion
                }
            }
        }

        private static void SyncAlarms(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL localbaseSQL = new ServerSQL(DatabaseFrom.Fetchbase, true)) // чтение локально
            {
                if (localbaseSQL.Connected)
                {
                    List<object[]> list = new List<object[]>();
                    using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Fetchbase))
                    {
                        if (baseSQL.Connected)
                        {
                            #region выборка позднейшей даты записи в локальной таблице
                            DateTime maxlocalSnapTime = DateTime.MinValue;
                            string SQLmaxtime = "select max(str_to_date(`snaptime`,'%Y.%m.%d %H:%i:%s.%f')) from `Alarms`";
                            using (MySqlCommand localcommand = new MySqlCommand(SQLmaxtime,
                                localbaseSQL.myConnection))
                            {
                                object obj = localcommand.ExecuteScalar();
                                byte[] test = new byte[0];
                                if (obj.GetType() == test.GetType())
                                {
                                    string sobj = Encoding.Default.GetString((byte[])obj);
                                    DateTime.TryParse(sobj, out maxlocalSnapTime);
                                }
                            }
                            #endregion
                            #region выборка из базовой таблицы новых записей в список
                            string SQLlist = "select `key`,`station`,`name`,`param`,`value`,`setpoint`," +
                                "`message`,`descriptor`,`snaptime` from `Alarms`" +
                                " where (str_to_date(`snaptime`,'%Y.%m.%d %H:%i:%s.%f')>@snaptime)";
                            using (MySqlCommand command = new MySqlCommand(SQLlist,
                                baseSQL.myConnection))
                            {
                                command.Parameters.AddWithValue("@snaptime", maxlocalSnapTime);
                                try
                                {
                                    using (MySqlDataReader reader = command.ExecuteReader())
                                    {
                                        if (reader.HasRows)
                                        {
                                            while (reader.Read())
                                            {
                                                if (worker.CancellationPending) { e.Cancel = true; break; }
                                                string key = reader.GetString(0);
                                                int station = reader.GetInt32(1);
                                                string name = reader.GetString(2);
                                                string param = reader.GetString(3);
                                                string value = reader.GetString(4);
                                                string setpoint = reader.GetString(5);
                                                string message = reader.GetString(6);
                                                string descriptor = reader.GetString(7);
                                                string snaptime = reader.GetString(8);
                                                list.Add(new object[9] { key, station, name, param, value,
                                                                    setpoint, message, descriptor, snaptime });
                                            }
                                        }
                                    }
                                }
                                catch (MySqlException ex)
                                {
                                    updatestatus(worker, 0, "Alarms", "Ошибка чтения:",
                                        ex.Message, DateTime.Now.ToString());
                                }
                            }
                            #endregion
                        }
                    }
                    #region вставка новых записей из списка в локальную таблицу
                    int row = 0;
                    foreach (object[] item in list)
                    {
                        if (worker.CancellationPending) { e.Cancel = true; break; }
                        string key = (string)item[0];
                        int station = (int)item[1];
                        string name = (string)item[2];
                        string param = (string)item[3];
                        string value = (string)item[4];
                        string setpoint = (string)item[5];
                        string message = (string)item[6];
                        string descriptor = (string)item[7];
                        string snaptime = (string)item[8];
                        localbaseSQL.UpdateAlarm(key, station, name, param, value, setpoint,
                            message, descriptor, snaptime);
                        updatestatus(worker, row++, "Alarms", name + "." + param, "обновление...", snaptime);
                    }
                    updatestatus(worker, row, "Alarms", "синхронизировано",
                        "записей: " + row, DateTime.Now.ToString());
                    #endregion
                }
            }
        }

        private static void SyncTables(BackgroundWorker worker, DoWorkEventArgs e)
        {
            string[] tables = new string[4] { "minutes", "hours", "days", "months" };
            int tabletype = 0;
            foreach (string table in tables)
            {
                List<object[]> list = new List<object[]>();
                using (ServerSQL localbaseSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение локально
                {
                    if (localbaseSQL.Connected)
                    {
                        string SQLmaxtime = String.Format("select max(`snaptime`) from `{0}`", table);
                        using (MySqlCommand localcommand = new MySqlCommand(SQLmaxtime,
                            localbaseSQL.myConnection))
                        {
                            object obj = localcommand.ExecuteScalar();
                            DateTime maxlocalSnapTime;
                            if (!DateTime.TryParse(obj.ToString(), out maxlocalSnapTime))
                                maxlocalSnapTime = DateTime.MinValue;
                            using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Database))
                            {
                                if (baseSQL.Connected)
                                {
                                    string SQLlist = String.Format("select `name`,`value`,`kind`," +
                                        "`snaptime` from `{0}`" +
                                        " where (`snaptime`>@snaptime)", table);
                                    using (MySqlCommand command = new MySqlCommand(SQLlist,
                                        baseSQL.myConnection))
                                    {
                                        try
                                        {
                                            if (maxlocalSnapTime > DateTime.MinValue)
                                            switch (table)
                                            {
                                                case "minutes":
                                                    maxlocalSnapTime = maxlocalSnapTime.AddMinutes(-2);
                                                    break;
                                                case "hours":
                                                    maxlocalSnapTime = maxlocalSnapTime.AddHours(-2);
                                                    break;
                                                case "days":
                                                    maxlocalSnapTime = maxlocalSnapTime.AddDays(-2);
                                                    break;
                                                case "months":
                                                    maxlocalSnapTime = maxlocalSnapTime.AddMonths(-2);
                                                    break;
                                            }
                                        }
                                        catch
                                        {
                                            maxlocalSnapTime = DateTime.MinValue;
                                        }
                                        command.Parameters.AddWithValue("@snaptime", maxlocalSnapTime);
                                        try
                                        {
                                            using (MySqlDataReader reader = command.ExecuteReader())
                                            {
                                                while (reader.Read())
                                                {
                                                    if (worker.CancellationPending) { e.Cancel = true; break; }
                                                    string name = reader.GetString(0);
                                                    double value = reader.GetDouble(1);
                                                    bool kind = (reader.GetString(2) == "Y") ? true : false;
                                                    DateTime snaptime = reader.GetDateTime(3);
                                                    list.Add(new object[] { name, value, kind, snaptime });
                                                }
                                            }
                                        }
                                        catch (MySqlException ex)
                                        {
                                            updatestatus(worker, 0, table, "Ошибка чтения:",
                                                ex.Message, DateTime.Now.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        int row = 0;
                        foreach (object[] item in list)
                        {
                            string name = (string)item[0];
                            double value = (double)item[1];
                            bool kind = (bool)item[2];
                            DateTime snaptime = (DateTime)item[3];
                            localbaseSQL.AddIntoTable(snaptime, name, (float)value, kind, tabletype, 0);
                            updatestatus(worker, row++, table, name, "обновление...", snaptime.ToString());
                        }
                        updatestatus(worker, row, table, "синхронизировано",
                            "записей: " + list.Count, DateTime.Now.ToString());
                    }

                }
                tabletype++;
            }
        }

        static string getMd5Hash(string input)
        {
            StringBuilder sBuilder = new StringBuilder();
            using (MD5 md5Hasher = MD5.Create())
            {
                byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
                for (int i = 0; i < data.Length; i++)
                    sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool verifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = getMd5Hash(input);
            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
                return true;
            else
                return false;
        }

        private static string hashTableGroups = String.Empty;
        private static void SyncTableGroups(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Database))
            {
                if (baseSQL.Connected)
                {
                    string SQLlist = "select `group`,`place`,`name` from `TableGroups`";
                    using (MySqlCommand command = new MySqlCommand(SQLlist,
                        baseSQL.myConnection))
                    {
                        StringBuilder text = new StringBuilder();
                        List<GroupItem> list = new List<GroupItem>();
                        try
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        if (worker.CancellationPending) { e.Cancel = true; break; }
                                        int group = reader.GetInt32(0);
                                        int place = reader.GetInt32(1);
                                        string name = reader.GetString(2);
                                        GroupItem gitem = new GroupItem(group, place);
                                        gitem.Caption = name;
                                        string[] item = name.Split(new char[] { '.' });
                                        gitem.Name = item[0];
                                        gitem.Param = (item.Length > 0) ? item[1] : "PV";
                                        list.Add(gitem);
                                        text.Append(group);
                                        text.Append(place);
                                        text.Append(name);
                                    }
                                }
                            }
                            string localhash = getMd5Hash(text.ToString());
                            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                            if (0 != comparer.Compare(localhash, hashTableGroups))
                            {
                                hashTableGroups = localhash;
                                Data.DeleteGroupItemsLocal(ParamGroup.Table);
                                int row = 0;
                                foreach (GroupItem gitem in list)
                                {
                                    Data.SaveGroupItem(gitem, ParamGroup.Table, true);
                                    updatestatus(worker, row++, "TableGroups", gitem.Caption, "обновление...", DateTime.Now.ToString());
                                }
                                updatestatus(worker, row, "TableGroups", "синхронизировано",
                                    "записей: " + row, DateTime.Now.ToString());
                            }
                        }
                        catch (MySqlException ex)
                        {
                            updatestatus(worker, 0, "TableGroups", "Ошибка чтения:",
                                ex.Message, DateTime.Now.ToString());
                        }
                    }
                }
            }
        }

        private static string hashTableGroupNames = String.Empty;
        private static void SyncTableGroupNames(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Database))
            {
                if (baseSQL.Connected)
                {
                    string SQLlist = "select `group`,`descriptor` from `TableGroupNames`";
                    using (MySqlCommand command = new MySqlCommand(SQLlist,
                        baseSQL.myConnection))
                    {
                        StringBuilder text = new StringBuilder();
                        List<object[]> list = new List<object[]>();
                        try
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        if (worker.CancellationPending) { e.Cancel = true; break; }
                                        int group = reader.GetInt32(0);
                                        string descriptor = reader.GetString(1);
                                        object[] item = new object[2];
                                        item[0] = group;
                                        item[1] = descriptor;
                                        list.Add(item);
                                        text.Append(group);
                                        text.Append(descriptor);
                                    }
                                }
                            }
                            string localhash = getMd5Hash(text.ToString());
                            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                            if (0 != comparer.Compare(localhash, hashTableGroupNames))
                            {
                                hashTableGroupNames = localhash;
                                Data.DeleteGroupNamesLocal(ParamGroup.Table);
                                int row = 0;
                                foreach (object[] item in list)
                                {
                                    Data.UpdateGroupDesc((int)item[0], (string)item[1],
                                        ParamGroup.Table, true);
                                    updatestatus(worker, row++, "TableGroupNames", item[1].ToString(), "обновление...", DateTime.Now.ToString());
                                }
                                updatestatus(worker, row, "TableGroupNames", "синхронизировано",
                                    "записей: " + row, DateTime.Now.ToString());
                            }
                        }
                        catch (MySqlException ex)
                        {
                            updatestatus(worker, 0, "TableGroupNames", "Ошибка чтения:",
                                ex.Message, DateTime.Now.ToString());
                        }
                    }
                }
            }
        }

        private static string hashGroupNames = String.Empty;
        private static void SyncGroupNames(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Database))
            {
                if (baseSQL.Connected)
                {
                    string SQLlist = "select `group`,`descriptor` from `GroupNames`";
                    using (MySqlCommand command = new MySqlCommand(SQLlist,
                        baseSQL.myConnection))
                    {
                        StringBuilder text = new StringBuilder();
                        List<object[]> list = new List<object[]>();
                        try
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        if (worker.CancellationPending) { e.Cancel = true; break; }
                                        int group = reader.GetInt32(0);
                                        string descriptor = reader.GetString(1);
                                        object[] item = new object[2];
                                        item[0] = group;
                                        item[1] = descriptor;
                                        list.Add(item);
                                        text.Append(group);
                                        text.Append(descriptor);
                                    }
                                }
                            }
                            string localhash = getMd5Hash(text.ToString());
                            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                            if (0 != comparer.Compare(localhash, hashGroupNames))
                            {
                                hashGroupNames = localhash;
                                Data.DeleteGroupNamesLocal(ParamGroup.Trend);
                                int row = 0;
                                foreach (object[] item in list)
                                {
                                    Data.UpdateGroupDesc((int)item[0], (string)item[1],
                                        ParamGroup.Trend, true);
                                    updatestatus(worker, row++, "GroupNames", item[1].ToString(), "обновление...", DateTime.Now.ToString());
                                }
                                updatestatus(worker, row, "GroupNames", "синхронизировано",
                                    "записей: " + row, DateTime.Now.ToString());
                            }
                        }
                        catch (MySqlException ex)
                        {
                            updatestatus(worker, 0, "GroupNames", "Ошибка чтения:",
                                ex.Message, DateTime.Now.ToString());
                        }
                    }
                }
            }
        }

        private static string hashGroups = String.Empty;
        private static void SyncGroups(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Database))
            {
                if (baseSQL.Connected)
                {
                    string SQLlist = "select `group`,`place`,`name` from `Groups`";
                    using (MySqlCommand command = new MySqlCommand(SQLlist,
                        baseSQL.myConnection))
                    {
                        StringBuilder text = new StringBuilder();
                        List<GroupItem> list = new List<GroupItem>();
                        try
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        if (worker.CancellationPending) { e.Cancel = true; break; }
                                        int group = reader.GetInt32(0);
                                        int place = reader.GetInt32(1);
                                        string name = reader.GetString(2);
                                        GroupItem gitem = new GroupItem(group, place);
                                        gitem.Caption = name;
                                        string[] item = name.Split(new char[] { '.' });
                                        gitem.Name = item[0];
                                        gitem.Param = (item.Length > 0) ? item[1] : "PV";
                                        list.Add(gitem);
                                        text.Append(group);
                                        text.Append(place);
                                        text.Append(name);
                                    }
                                }
                            }
                            string localhash = getMd5Hash(text.ToString());
                            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                            if (0 != comparer.Compare(localhash, hashGroups))
                            {
                                hashGroups = localhash;
                                Data.DeleteGroupItemsLocal(ParamGroup.Trend);
                                int row = 0;
                                foreach (GroupItem gitem in list)
                                {
                                    Data.SaveGroupItem(gitem, ParamGroup.Trend, true);
                                    updatestatus(worker, row++, "Groups", gitem.Caption, "обновление...", DateTime.Now.ToString());
                                }
                                updatestatus(worker, row, "Groups", "синхронизировано",
                                    "записей: " + row, DateTime.Now.ToString());
                            }
                        }
                        catch (MySqlException ex)
                        {
                            updatestatus(worker, 0, "Groups", "Ошибка чтения:",
                                ex.Message, DateTime.Now.ToString());
                        }
                    }
                }
            }
        }

        private static void SyncSystemLog(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL localbaseSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение локально
            {
                if (localbaseSQL.Connected)
                {
                    string SQLmaxtime = "select max(`snaptime`) from `SystemLog`";
                    using (MySqlCommand localcommand = new MySqlCommand(SQLmaxtime,
                        localbaseSQL.myConnection))
                    {
                        object obj = localcommand.ExecuteScalar();
                        DateTime maxlocalSnapTime;
                        if (!DateTime.TryParse(obj.ToString(), out maxlocalSnapTime))
                            maxlocalSnapTime = DateTime.MinValue;
                        using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Database))
                        {
                            if (baseSQL.Connected)
                            {
                                string SQLlist = "select `station`,`name`,`descriptor`,`snaptime` from `SystemLog`" +
                                        " where (`snaptime`>@snaptime)";
                                using (MySqlCommand command = new MySqlCommand(SQLlist,
                                    baseSQL.myConnection))
                                {
                                    command.Parameters.AddWithValue("@snaptime", maxlocalSnapTime);
                                    try
                                    {
                                        using (MySqlDataReader reader = command.ExecuteReader())
                                        {
                                            int row = 0;
                                            while (reader.Read())
                                            {
                                                if (worker.CancellationPending) { e.Cancel = true; break; }
                                                int station = reader.GetInt32(0);
                                                string name = reader.GetString(1);
                                                string descriptor = reader.GetString(2);
                                                DateTime snaptime = reader.GetDateTime(3);
                                                localbaseSQL.AddToSystemLog(station, name, descriptor, snaptime);
                                                updatestatus(worker, row++, "SystemLog", name, "обновление...", snaptime.ToString());
                                            }
                                            updatestatus(worker, row, "SystemLog", "синхронизировано",
                                                "записей: " + row, DateTime.Now.ToString());
                                        }
                                    }
                                    catch (MySqlException ex)
                                    {
                                        updatestatus(worker, 0, "SystemLog", "Ошибка чтения:",
                                            ex.Message, DateTime.Now.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void SyncChangeLog(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL localbaseSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение локально
            {
                if (localbaseSQL.Connected)
                {
                    string SQLmaxtime = "select max(str_to_date(`snaptime`,'%Y.%m.%d %H:%i:%s.%f')) from `ChangeLog`";
                    using (MySqlCommand localcommand = new MySqlCommand(SQLmaxtime,
                        localbaseSQL.myConnection))
                    {
                        object obj = localcommand.ExecuteScalar();
                        DateTime maxlocalSnapTime;
                        byte[] test = new byte[0];
                        if (obj.GetType() == test.GetType())
                        {
                            string sobj = Encoding.Default.GetString((byte[])obj);
                            if (!DateTime.TryParse(sobj, out maxlocalSnapTime))
                                maxlocalSnapTime = DateTime.MinValue;
                        }
                        else
                            maxlocalSnapTime = DateTime.MinValue;
                        using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Database))
                        {
                            if (baseSQL.Connected)
                            {
                                string SQLlist = "select `station`,`name`,`param`,`oldvalue`,`newvalue`,`autor`," +
                                    "`descriptor`,`snaptime` from `ChangeLog`" +
                                    " where (str_to_date(`snaptime`,'%Y.%m.%d %H:%i:%s.%f')>@snaptime)";
                                using (MySqlCommand command = new MySqlCommand(SQLlist,
                                    baseSQL.myConnection))
                                {
                                    command.Parameters.AddWithValue("@snaptime", maxlocalSnapTime);
                                    try
                                    {
                                        using (MySqlDataReader reader = command.ExecuteReader())
                                        {
                                            int row = 0;
                                            while (reader.Read())
                                            {
                                                if (worker.CancellationPending) { e.Cancel = true; break; }
                                                int station = reader.GetInt32(0);
                                                string name = reader.GetString(1);
                                                string param = reader.GetString(2);
                                                string oldvalue = reader.GetString(3);
                                                string newvalue = reader.GetString(4);
                                                string autor = reader.GetString(5);
                                                string descriptor = reader.GetString(6);
                                                string snaptime = reader.GetString(7);
                                                localbaseSQL.AddToChangeLog(station, name, param, oldvalue,
                                                    newvalue, autor, descriptor, snaptime);
                                                updatestatus(worker, row++, "ChangeLog", name + "." + param, "обновление...", snaptime);
                                            }
                                            updatestatus(worker, row, "ChangeLog", "синхронизировано",
                                                "записей: " + row, DateTime.Now.ToString());
                                        }
                                    }
                                    catch (MySqlException ex)
                                    {
                                        updatestatus(worker, 0, "ChangeLog", "Ошибка чтения:",
                                            ex.Message, DateTime.Now.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void SyncSwitchLog(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL localbaseSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение локально
            {
                if (localbaseSQL.Connected)
                {
                    string SQLmaxtime = "select max(str_to_date(`snaptime`,'%Y.%m.%d %H:%i:%s.%f')) from `SwitchLog`";
                    using (MySqlCommand localcommand = new MySqlCommand(SQLmaxtime,
                        localbaseSQL.myConnection))
                    {
                        object obj = localcommand.ExecuteScalar();
                        DateTime maxlocalSnapTime;
                        byte[] test = new byte[0];
                        if (obj.GetType() == test.GetType())
                        {
                            string sobj = Encoding.Default.GetString((byte[])obj);
                            if (!DateTime.TryParse(sobj, out maxlocalSnapTime))
                                maxlocalSnapTime = DateTime.MinValue;
                        }
                        else
                            maxlocalSnapTime = DateTime.MinValue;
                        using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Database))
                        {
                            if (baseSQL.Connected)
                            {
                                string SQLlist = "select `station`,`name`,`param`,`oldvalue`,`newvalue`,`descriptor`,`snaptime` from `SwitchLog`" +
                                        " where (str_to_date(`snaptime`,'%Y.%m.%d %H:%i:%s.%f')>@snaptime)";
                                using (MySqlCommand command = new MySqlCommand(SQLlist,
                                    baseSQL.myConnection))
                                {
                                    command.Parameters.AddWithValue("@snaptime", maxlocalSnapTime);
                                    try
                                    {
                                        using (MySqlDataReader reader = command.ExecuteReader())
                                        {
                                            int row = 0;
                                            while (reader.Read())
                                            {
                                                if (worker.CancellationPending) { e.Cancel = true; break; }
                                                int station = reader.GetInt32(0);
                                                string name = reader.GetString(1);
                                                string param = reader.GetString(2);
                                                string oldvalue = reader.GetString(3);
                                                string newvalue = reader.GetString(4);
                                                string descriptor = reader.GetString(5);
                                                string snaptime = reader.GetString(6);
                                                localbaseSQL.AddToSwitchLog(station, name, param, oldvalue, newvalue, descriptor, snaptime);
                                                 updatestatus(worker, row++, "SwitchLog", name + "." + param, "обновление...", snaptime);
                                            }
                                            updatestatus(worker, row, "SwitchLog", "синхронизировано",
                                                "записей: " + row, DateTime.Now.ToString());
                                        }
                                    }
                                    catch (MySqlException ex)
                                    {
                                        updatestatus(worker, 0, "AlarmLog", "Ошибка чтения:",
                                            ex.Message, DateTime.Now.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void SyncAlarmLog(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL localbaseSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение локально
            {
                if (localbaseSQL.Connected)
                {
                    string SQLmaxtime = "select max(str_to_date(`snaptime`,'%Y.%m.%d %H:%i:%s.%f')) from `AlarmLog`";
                    using (MySqlCommand localcommand = new MySqlCommand(SQLmaxtime,
                        localbaseSQL.myConnection))
                    {
                        object obj = localcommand.ExecuteScalar();
                        byte[] test = new byte[0];
                        DateTime maxlocalSnapTime;
                        if (obj.GetType() == test.GetType())
                        {
                            string sobj = Encoding.Default.GetString((byte[])obj);
                            if (!DateTime.TryParse(sobj, out maxlocalSnapTime))
                                maxlocalSnapTime = DateTime.MinValue;
                        }
                        else
                            maxlocalSnapTime = DateTime.MinValue;
                        using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Database))
                        {
                            if (baseSQL.Connected)
                            {
                                string SQLlist = "select `station`,`name`,`param`,`value`,`setpoint`,`message`,`descriptor`,`snaptime` from `AlarmLog`" +
                                        " where (str_to_date(`snaptime`,'%Y.%m.%d %H:%i:%s.%f')>@snaptime)";
                                using (MySqlCommand command = new MySqlCommand(SQLlist,
                                    baseSQL.myConnection))
                                {
                                    command.Parameters.AddWithValue("@snaptime", maxlocalSnapTime);
                                    try
                                    {
                                        using (MySqlDataReader reader = command.ExecuteReader())
                                        {
                                            int row = 0;
                                            while (reader.Read())
                                            {
                                                if (worker.CancellationPending) { e.Cancel = true; break; }
                                                int station = reader.GetInt32(0);
                                                string name = reader.GetString(1);
                                                string param = reader.GetString(2);
                                                string value = reader.GetString(3);
                                                string setpoint = reader.GetString(4);
                                                string message = reader.GetString(5);
                                                string descriptor = reader.GetString(6);
                                                string snaptime = reader.GetString(7);
                                                localbaseSQL.AddToAlarmLog(station, name, param, value, setpoint, message, descriptor, snaptime);
                                                updatestatus(worker, row++, "AlarmLog", name + "." + param, "обновление...", snaptime);
                                            }
                                            updatestatus(worker, row, "AlarmLog", "синхронизировано",
                                                "записей: " + row, DateTime.Now.ToString());
                                        }
                                    }
                                    catch (MySqlException ex)
                                    {
                                        updatestatus(worker, 0, "AlarmLog", "Ошибка чтения:",
                                            ex.Message, DateTime.Now.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void updatestatus(BackgroundWorker worker, int percent, string table,
            string point, string status, string snaptime)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("table", table);
            dict.Add("point", point);
            dict.Add("status", status);
            dict.Add("snaptime", snaptime);
            worker.ReportProgress(percent, dict);
        }

        private static void SyncImages(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL localbaseSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение локально
            {
                if (localbaseSQL.Connected)
                {
                    using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Database))
                        {
                            if (baseSQL.Connected)
                            {
                                #region выборка позднейшей даты записи в локальной таблице
                                string SQLmaxtime = "select max(`filetime`) from `Images`";
                                DateTime maxlocalSnapTime = DateTime.MinValue;
                                using (MySqlCommand localcommand = new MySqlCommand(SQLmaxtime,
                                    localbaseSQL.myConnection))
                                {
                                    object obj = localcommand.ExecuteScalar();                                    
                                    DateTime.TryParse(obj.ToString(), out maxlocalSnapTime);
                                }
                                #endregion
                                #region выборка из базовой таблицы новых записей в список
                                List<object[]> list = new List<object[]>();
                                string SQLlist = "select `name`,`filetime` from `Images`" +
                                        " where (`filetime`>@filetime)";
                                using (MySqlCommand command = new MySqlCommand(SQLlist,
                                    baseSQL.myConnection))
                                {
                                    command.Parameters.AddWithValue("@filetime", maxlocalSnapTime);
                                    try
                                    {
                                        using (MySqlDataReader reader = command.ExecuteReader())
                                        {
                                            while (reader.Read())
                                            {
                                                if (worker.CancellationPending) { e.Cancel = true; break; }
                                                string name = reader.GetString(0);
                                                DateTime filetime = reader.GetDateTime(1);
                                                list.Add(new object[2] {name, filetime} );
                                            }
                                        }
                                    }
                                    catch (MySqlException ex)
                                    {
                                        updatestatus(worker, 0, "Images", "Ошибка чтения:",
                                            ex.Message, DateTime.Now.ToString());
                                    }
                                }
                                #endregion
                                #region запись списка в локальную таблицу
                                int row = 0;
                                foreach (object[] item in list)
                                {
                                    if (worker.CancellationPending) { e.Cancel = true; break; }
                                    string name = (string)item[0];
                                    DateTime filetime = (DateTime)item[1];
                                    byte[] image = baseSQL.GetImageData(name);
                                    localbaseSQL.AddImage(name, filetime, image);
                                    updatestatus(worker, row++, "Images", name, "обновление...", filetime.ToString());
                                }
                                #endregion
                            }
                        }
                    }
            }
        }

        private static void SyncReports(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL localbaseSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение локально
            {
                if (localbaseSQL.Connected)
                {
                    using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Database))
                    {
                        if (baseSQL.Connected)
                        {
                            #region выборка позднейшей даты записи в локальной таблице
                            string SQLmaxtime = "select max(`filetime`) from `Reports`";
                            DateTime maxlocalSnapTime = DateTime.MinValue;
                            using (MySqlCommand localcommand = new MySqlCommand(SQLmaxtime,
                                localbaseSQL.myConnection))
                            {
                                object obj = localcommand.ExecuteScalar();
                                DateTime.TryParse(obj.ToString(), out maxlocalSnapTime);
                            }
                            #endregion
                            #region выборка из базовой таблицы новых записей в список
                            List<object[]> list = new List<object[]>();
                            string SQLlist = "select `name`,`descriptor`,`auto`,`time`," +
                                "`period`,`filetime` from `Reports`" +
                                " where (`filetime`>@filetime)";
                            using (MySqlCommand command = new MySqlCommand(SQLlist,
                                baseSQL.myConnection))
                            {
                                command.Parameters.AddWithValue("@filetime", maxlocalSnapTime);
                                try
                                {
                                    using (MySqlDataReader reader = command.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            if (worker.CancellationPending) { e.Cancel = true; break; }
                                            string name = reader.GetString(0);
                                            string descriptor = reader.GetString(1);
                                            string auto = reader.GetString(2);
                                            string time = reader.GetString(3);
                                            int period = reader.GetInt32(4);
                                            DateTime filetime = reader.GetDateTime(5);
                                            list.Add(new object[6] { name, descriptor, auto, time, period, filetime });
                                        }
                                    }
                                }
                                catch (MySqlException ex)
                                {
                                    updatestatus(worker, 0, "SystemLog", "Ошибка чтения:",
                                        ex.Message, DateTime.Now.ToString());
                                }

                            }
                            #endregion
                            #region запись списка в локальную таблицу
                            int row = 0;
                            foreach (object[] item in list)
                            {
                                if (worker.CancellationPending) { e.Cancel = true; break; }
                                string name = (string)item[0];
                                string descriptor = (string)item[1];
                                string auto = (string)item[2];
                                string time = (string)item[3];
                                int period = (int)item[4];
                                DateTime filetime = (DateTime)item[5];
                                byte[] image = baseSQL.GetReportData(name);
                                localbaseSQL.AddReport(name, filetime, image, descriptor,
                                    auto, time, period);
                                updatestatus(worker, row++, "Reports", name + " " + descriptor, "обновление...", filetime.ToString());
                            }
                            #endregion
                        }
                    }
                }
            }
        }

        private static void SyncDinamics(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL localbaseSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение локально
            {
                if (localbaseSQL.Connected)
                {
                    using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Database))
                    {
                        if (baseSQL.Connected)
                        {
                            #region выборка позднейшей даты записи в локальной таблице
                            string SQLmaxtime = "select max(`snaptime`) from `Dinamics`";
                            DateTime maxlocalSnapTime = DateTime.MinValue;
                            using (MySqlCommand localcommand = new MySqlCommand(SQLmaxtime,
                                localbaseSQL.myConnection))
                            {
                                object obj = localcommand.ExecuteScalar();
                                DateTime.TryParse(obj.ToString(), out maxlocalSnapTime);
                            }
                            #endregion
                            #region выборка из базовой таблицы новых записей и запись в локальную таблицу
                            int offset = 0;
                            int step = (int)e.Argument;
                            int row = 0;
                            while (true)
                            {
                                if (worker.CancellationPending) { e.Cancel = true; break; }
                                string SQLlist = String.Format("select `scheme`,`npp`,`name`,`prop`,`value`," +
                                    "`snaptime` from `Dinamics` where (`snaptime`>@snaptime) limit {0},{1}",
                                    offset, step);
                                using (MySqlCommand command = new MySqlCommand(SQLlist,
                                    baseSQL.myConnection))
                                {
                                    command.Parameters.AddWithValue("@snaptime", maxlocalSnapTime);
                                    try
                                    {
                                        using (MySqlDataReader reader = command.ExecuteReader())
                                        {
                                            if (reader.HasRows)
                                            {
                                                while (reader.Read())
                                                {
                                                    if (worker.CancellationPending) { e.Cancel = true; break; }
                                                    string scheme = reader.GetString(0);
                                                    int npp = reader.GetInt32(1);
                                                    string name = reader.GetString(2);
                                                    string prop = reader.GetString(3);
                                                    string value = reader.GetString(4);
                                                    DateTime snaptime = reader.GetDateTime(5);
                                                    localbaseSQL.WriteDinValue(scheme, npp, name, prop,
                                                        value, snaptime);
                                                    updatestatus(worker, row++, "Dinamics", name + "." + prop, "обновление...", snaptime.ToString());
                                                }
                                            }
                                            else
                                                break;
                                        }
                                    }
                                    catch (MySqlException ex)
                                    {
                                        updatestatus(worker, 0, "SystemLog", "Ошибка чтения:",
                                            ex.Message, DateTime.Now.ToString());
                                    }

                                }
                                offset += step;
                            }
                            updatestatus(worker, row, "Dinamics", "синхронизировано",
                                            "записей: " + row, DateTime.Now.ToString());
                            #endregion
                        }
                    }
                }
            }
        }

        private static void SyncTrends(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL localbaseSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение локально
            {
                if (localbaseSQL.Connected)
                {
                    string SQLmaxtime = "select max(`snaptime`) from `Trends`";
                    using (MySqlCommand localcommand = new MySqlCommand(SQLmaxtime,
                        localbaseSQL.myConnection))
                    {
                        object obj = localcommand.ExecuteScalar();
                        DateTime maxlocalSnapTime;
                        if (!DateTime.TryParse(obj.ToString(), out maxlocalSnapTime))
                            maxlocalSnapTime = DateTime.MinValue;
                        using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Database))
                        {
                            if (baseSQL.Connected)
                            {
                                int offset = 0;
                                int step = (int)e.Argument;
                                int row = 0;
                                while (true)
                                {
                                    if (worker.CancellationPending) { e.Cancel = true; break; }
                                    string SQLlist = String.Format("select `name`,`value`,`kind`,`snaptime` from `Trends`" +
                                            " where (`snaptime`>@snaptime) limit {0},{1}", offset, step);
                                    using (MySqlCommand command = new MySqlCommand(SQLlist,
                                        baseSQL.myConnection))
                                    {
                                        command.Parameters.AddWithValue("@snaptime", maxlocalSnapTime);
                                        try
                                        {
                                            using (MySqlDataReader reader = command.ExecuteReader())
                                            {
                                                if (reader.HasRows)
                                                {
                                                    while (reader.Read())
                                                    {
                                                        if (worker.CancellationPending) { e.Cancel = true; break; }
                                                        string name = reader.GetString(0);
                                                        double value = reader.GetDouble(1);
                                                        bool kind = (reader.GetString(2) == "Y") ? true : false;
                                                        DateTime snaptime = reader.GetDateTime(3);
                                                        localbaseSQL.AddToTrend(name, value, kind, snaptime);
                                                        updatestatus(worker, row++, "Trends", name, "обновление...", snaptime.ToString());
                                                    }
                                                }
                                                else
                                                    break;
                                            }
                                        }
                                        catch (MySqlException ex)
                                        {
                                            updatestatus(worker, 0, "SystemLog", "Ошибка чтения:",
                                                ex.Message, DateTime.Now.ToString());
                                        }

                                    }
                                    offset += step;
                                }
                                updatestatus(worker, row, "Trends", "синхронизировано",
                                    "записей: " + row, DateTime.Now.ToString());
                            }
                        }
                    }
                }
            }
        }

        private static void SyncPoints(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL localbaseSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение локально
            {
                if (localbaseSQL.Connected)
                {
                    using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Database))
                    {
                        if (baseSQL.Connected)
                        {
                            #region выборка позднейшей даты записи в локальной таблице
                            string SQLmaxtime = "select max(`snaptime`) from `Points`";
                            DateTime maxlocalSnapTime = DateTime.MinValue;
                            using (MySqlCommand localcommand = new MySqlCommand(SQLmaxtime,
                                localbaseSQL.myConnection))
                            {
                                object obj = localcommand.ExecuteScalar();
                                DateTime.TryParse(obj.ToString(), out maxlocalSnapTime);
                            }
                            #endregion
                            #region выборка из базовой таблицы новых записей и запись в локальную таблицу
                            int offset = 0;
                            int step = (int)e.Argument;
                            int row = 0;
                            while (true)
                            {
                                if (worker.CancellationPending) { e.Cancel = true; break; }
                                string SQLlist = String.Format("select `name`,`prop`,`value`,`snaptime` from `Points`" +
                                " where (`snaptime`>@snaptime) limit {0}, {1}", offset, step);
                                using (MySqlCommand command = new MySqlCommand(SQLlist,
                                    baseSQL.myConnection))
                                {
                                    command.Parameters.AddWithValue("@snaptime", maxlocalSnapTime);
                                    try
                                    {
                                        using (MySqlDataReader reader = command.ExecuteReader())
                                        {
                                            if (reader.HasRows)
                                            {
                                                while (reader.Read())
                                                {
                                                    if (worker.CancellationPending) { e.Cancel = true; break; }
                                                    string name = reader.GetString(0);
                                                    string prop = reader.GetString(1);
                                                    string value = reader.GetString(2);
                                                    DateTime snaptime = reader.GetDateTime(3);
                                                    localbaseSQL.WritePropValue(name, prop, value, snaptime);
                                                    updatestatus(worker, row++, "Points", name + "." + prop, "обновление...", snaptime.ToString());
                                                }
                                            }
                                            else
                                                break;
                                        }
                                    }
                                    catch (MySqlException ex)
                                    {
                                        updatestatus(worker, 0, "SystemLog", "Ошибка чтения:",
                                            ex.Message, DateTime.Now.ToString());
                                    }

                                }
                                offset += step;
                            }
                            updatestatus(worker, row, "Points", "синхронизировано",
                                "записей: " + row, DateTime.Now.ToString());
                            #endregion
                        }
                    }
                }
            }
        }

        private static void SyncRealVals(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL localfetchSQL = new ServerSQL(DatabaseFrom.Fetchbase, true)) // чтение локально
            {
                if (localfetchSQL.Connected)
                {
                    using (ServerSQL fetchSQL = new ServerSQL(DatabaseFrom.Fetchbase))
                    {
                        if (fetchSQL.Connected)
                        {
                            #region выборка позднейшей даты записи в локальной таблице
                            string SQLmaxtime = "select max(`snaptime`) from `RealVals`";
                            DateTime maxlocalSnapTime = DateTime.MinValue;
                            using (MySqlCommand localcommand = new MySqlCommand(SQLmaxtime,
                                localfetchSQL.myConnection))
                            {
                                object obj = localcommand.ExecuteScalar();
                                DateTime.TryParse(obj.ToString(), out maxlocalSnapTime);
                            }
                            #endregion
                            #region выборка из базовой таблицы новых записей и запись в локальную таблицу
                            int offset = 0;
                            int step = (int)e.Argument;
                            int row = 0;
                            while (true)
                            {
                                if (worker.CancellationPending) { e.Cancel = true; break; }
                                string SQLlist = String.Format("select `name`,`prop`,`value`,`snaptime` from `RealVals`" +
                                        " where (`snaptime`>@snaptime) limit {0}, {1}", offset, step);
                                using (MySqlCommand command = new MySqlCommand(SQLlist,
                                    fetchSQL.myConnection))
                                {
                                    command.Parameters.AddWithValue("@snaptime", maxlocalSnapTime);
                                    try
                                    {
                                        using (MySqlDataReader reader = command.ExecuteReader())
                                        {
                                            if (reader.HasRows)
                                            {
                                                while (reader.Read())
                                                {
                                                    if (worker.CancellationPending) { e.Cancel = true; break; }
                                                    string name = reader.GetString(0);
                                                    string prop = reader.GetString(1);
                                                    string value = reader.GetString(2);
                                                    DateTime snaptime = reader.GetDateTime(3);
                                                    localfetchSQL.WriteRealValue(name, prop, value, snaptime);
                                                    updatestatus(worker, row++, "RealVals", name + "." + prop, "обновление...", snaptime.ToString());
                                                }
                                            }
                                            else
                                                break;
                                        }
                                    }
                                    catch (MySqlException ex)
                                    {
                                        updatestatus(worker, 0, "SystemLog", "Ошибка чтения:",
                                            ex.Message, DateTime.Now.ToString());
                                    }
                                }
                                offset += step;
                            }
                            updatestatus(worker, row, "RealVals", "синхронизировано",
                                "записей: " + row, DateTime.Now.ToString());
                            #endregion
                        }
                    }
                }
            }
        }

        private void backgroundUpdateChilds_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (showlist != null && showlist.Text.Length > 0)
            {
                IDictionary<string, string> dict = (IDictionary<string, string>)e.UserState;
                if (dict.ContainsKey("table") &&
                    dict.ContainsKey("point") &&
                    dict.ContainsKey("status") &&
                    dict.ContainsKey("snaptime"))
                {
                    string table = dict["table"];
                    string point = dict["point"];
                    string status = dict["status"];
                    string snaptime = dict["snaptime"];
                    ListViewItem item = showlist.lvStatus.FindItemWithText(table);
                    if (item == null)
                    {
                        item = showlist.lvStatus.Items.Add(table);
                        item.SubItems.Add(point);
                        item.SubItems.Add(status);
                        item.SubItems.Add(snaptime);
                        DateTime time;
                        if (DateTime.TryParse(snaptime, out time))
                            item.SubItems.Add(time.ToString("dd.MM.yy ddd HH:mm:ss")).Tag = time;
                        else
                            item.SubItems.Add(snaptime);
                    }
                    else
                    {
                        item.SubItems[1].Text = point;
                        item.SubItems[2].Text = status;
                        DateTime time;
                        if (DateTime.TryParse(snaptime, out time))
                        {
                            item.SubItems[3].Text = time.ToString("dd.MM.yy ddd HH:mm:ss");
                            item.SubItems[3].Tag = time;
                        }
                        else
                        {
                            item.SubItems[3].Text = snaptime;
                            item.SubItems[3].Tag = null;
                        }
                    }
                }
                else
                    if (dict.ContainsKey("status") &&
                        dict.ContainsKey("snaptime"))
                    {
                        string status = dict["status"];
                        string snaptime = dict["snaptime"];
                        showlist.lbMessage.Text = snaptime + " " + status;
                    }
            }
        }

        private void backgroundUpdateChilds_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) Application.Exit();
        }

        private void frmSyncServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Завершение
            Data.SendToSystemLog(Properties.Settings.Default.Station,
                "Репликация", "Сервер репликации выгружен");
            Data.ClientLogout(ClientID);
        }

        private void miTuning_Click(object sender, EventArgs e)
        {
            using (frmTuningVirtual form = new frmTuningVirtual())
            {
                form.cbStation.SelectedIndex = Properties.Settings.Default.Station - 1;
                form.nudRecCount.Value = Properties.Settings.Default.FetchRecords;
                form.tbCopyOwner.Text = Properties.Settings.Default.CopyOwner;
                form.tbCopyCode.Text = Data.MachineCode(Properties.Settings.Default.CopyOwner);
                form.tbCopyKey.Text = Properties.Settings.Default.CopyKey;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings settings = Properties.Settings.Default;
                    settings.Station = form.cbStation.SelectedIndex + 1;
                    settings.FetchRecords = (int)form.nudRecCount.Value;
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
                showlist.Show();
            }
        }

        private void backgroundDeleteChilds_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            DeleteAsync(e.Argument, worker, e);
        }

        private void backgroundDeleteChilds_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) Application.Exit();
        }

        private void DeleteAsync(object argument, BackgroundWorker worker, DoWorkEventArgs e)
        {
            updatestatus(worker, "", DateTime.Now.ToString());
            DeletePoints(worker, e);
            SyncPoints(worker, e);
            DeleteDinamics(worker, e);
            SyncDinamics(worker, e);
            DeleteReports(worker, e);
            SyncReports(worker, e);
            DeleteImages(worker, e);
            SyncImages(worker, e);
            SyncGroups(worker, e);
            SyncGroupNames(worker, e);
            SyncTableGroups(worker, e);
            SyncTableGroupNames(worker, e);
            DeleteAlarms(worker, e);
            DeleteSwitchs(worker, e);
            DeleteRealVals(worker, e);
            updatestatus(worker, "Ожидание изменений...", DateTime.Now.ToString());
        }

        #region удаление дублирования в таблице Dinamics
        private static void DeleteDinamics(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL localbaseSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение локально
            {
                if (localbaseSQL.Connected)
                {
                    using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Database))
                    {
                        if (baseSQL.Connected)
                        {
                            HashSet<string> locallist = new HashSet<string>();
                            HashSet<string> remotelist = new HashSet<string>();
                            locallist = getKeyItems(worker, e, localbaseSQL,
                                "Dinamics", "concat(`scheme`,`name`,`prop`)", (int)e.Argument);
                            remotelist = getKeyItems(worker, e, baseSQL,
                                "Dinamics", "concat(`scheme`,`name`,`prop`)", (int)e.Argument);
                            locallist.ExceptWith(remotelist);
                            if (locallist.Count == 0)
                                updatestatus(worker, 0, "Dinamics", "синхронизировано", "", DateTime.Now.ToString());
                           else
                            {
                                int row = 0;
                                foreach (string item in locallist)
                                {
                                    if (worker.CancellationPending) { e.Cancel = true; break; }
                                    localbaseSQL.ExecSQL("delete from `Dinamics`" +
                                        " where concat(`scheme`,`name`,`prop`)='" + item + "'");
                                    updatestatus(worker, row++, "Dinamics", item, "удаление...", DateTime.Now.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
        private static HashSet<string> getKeyItems(BackgroundWorker worker, DoWorkEventArgs e,
            ServerSQL serverSQL, string tableName, string keyFields, int step)
        {
            int count = 0;
            string[] conn = serverSQL.myConnection.ConnectionString.Split(new char[] { ';' });
            bool localhost = false;
            foreach (string item in conn)
            {
                string[] items = item.Split(new char[] { '=' });
                if (items.Length == 2 && items[0].ToLower().Equals("server") &&
                    items[1].ToLower().Equals("localhost"))
                {
                    localhost = true;
                    break;
                }
            }

            using (MySqlCommand command = new MySqlCommand(
                String.Format("select count(*) from `{0}`", tableName), serverSQL.myConnection))
            {
                object obj = command.ExecuteScalar();
                int.TryParse(obj.ToString(), out count);
            }

            HashSet<string> list = new HashSet<string>();
            int offset = 0;
            int lines = 0;
            int num = 0;
            while (true)
            {
                string SQL = String.Format("select {0} from `{1}` limit {2},{3}",
                    keyFields, tableName, offset, step);
                lines = 0;
                using (MySqlCommand command = new MySqlCommand(SQL, serverSQL.myConnection))
                {
                    try
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (worker.CancellationPending) { e.Cancel = true; break; }
                                list.Add(reader.GetString(0));
                                lines++;
                                num++;
                            }
                        }
                        if (lines != step) break;
                        updatestatus(worker, 0, tableName, (0.5 * num / count + ((localhost) ? 0.0 : 0.5)).ToString("P"),
                            "вычитывание...", DateTime.Now.ToString());
                        offset += step;
                    }
                    catch (MySqlException ex)
                    {
                        updatestatus(worker, 0, "Dinamics", "Ошибка чтения:",
                            ex.Message, DateTime.Now.ToString());
                        break;
                    }
                }
            }
            return list;
        }

        private static void DeletePoints(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL localbaseSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение локально
            {
                if (localbaseSQL.Connected)
                {
                    using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Database))
                    {
                        if (baseSQL.Connected)
                        {
                            HashSet<string> locallist = new HashSet<string>();
                            HashSet<string> remotelist = new HashSet<string>();
                            locallist = getKeyItems(worker, e, localbaseSQL,
                                "Points", "concat(`name`,`prop`)", (int)e.Argument);
                            remotelist = getKeyItems(worker, e, baseSQL,
                                "Points", "concat(`name`,`prop`)", (int)e.Argument);
                            locallist.ExceptWith(remotelist);
                            if (locallist.Count == 0)
                                updatestatus(worker, 0, "Points", "синхронизировано", "", DateTime.Now.ToString());
                            else
                            {
                                int row = 0;
                                foreach (string item in locallist)
                                {
                                    if (worker.CancellationPending) { e.Cancel = true; break; }
                                    localbaseSQL.ExecSQL("delete from `Points`" +
                                        " where concat(`name`,`prop`)='" + item + "'");
                                    updatestatus(worker, row++, "Points", item, "удаление...", DateTime.Now.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }

        #region удаление дублирования в таблице Alarms
        private static void DeleteAlarms(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL localfetchSQL = new ServerSQL(DatabaseFrom.Fetchbase, true)) // чтение локально
            {
                if (localfetchSQL.Connected)
                {
                    using (ServerSQL fetchSQL = new ServerSQL(DatabaseFrom.Fetchbase))
                    {
                        if (fetchSQL.Connected)
                        {
                            HashSet<string> locallist = new HashSet<string>();
                            HashSet<string> remotelist = new HashSet<string>();
                            locallist = getKeyItems(worker, e, localfetchSQL,
                                "Alarms", "concat(`key`,`station`,`name`,`param`)", (int)e.Argument);
                            remotelist = getKeyItems(worker, e, fetchSQL,
                                "Alarms", "concat(`key`,`station`,`name`,`param`)", (int)e.Argument);
                            locallist.ExceptWith(remotelist);
                            if (locallist.Count == 0)
                                updatestatus(worker, 0, "Alarms", "синхронизировано", "", DateTime.Now.ToString());
                            else
                            {
                                int row = 0;
                                foreach (string item in locallist)
                                {
                                    if (worker.CancellationPending) { e.Cancel = true; break; }
                                    localfetchSQL.ExecSQL("delete from `Alarms`" +
                                        " where concat(`key`,`station`,`name`,`param`)='" + item + "'");
                                    updatestatus(worker, row++, "Alarms", item, "удаление...", DateTime.Now.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region удаление дублирования в таблице Switchs
        private static void DeleteSwitchs(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL localfetchSQL = new ServerSQL(DatabaseFrom.Fetchbase, true)) // чтение локально
            {
                if (localfetchSQL.Connected)
                {
                    using (ServerSQL fetchSQL = new ServerSQL(DatabaseFrom.Fetchbase))
                    {
                        if (fetchSQL.Connected)
                        {
                            HashSet<string> locallist = new HashSet<string>();
                            HashSet<string> remotelist = new HashSet<string>();
                            locallist = getKeyItems(worker, e, localfetchSQL,
                                "Switchs", "concat(`station`,`name`,`param`)", (int)e.Argument);
                            remotelist = getKeyItems(worker, e, fetchSQL,
                                "Switchs", "concat(`station`,`name`,`param`)", (int)e.Argument);
                            locallist.ExceptWith(remotelist);
                            if (locallist.Count == 0)
                                updatestatus(worker, 0, "Switchs", "синхронизировано", "", DateTime.Now.ToString());
                            else
                            {
                                int row = 0;
                                foreach (string item in locallist)
                                {
                                    if (worker.CancellationPending) { e.Cancel = true; break; }
                                    localfetchSQL.ExecSQL("delete from `Switchs`" +
                                        " where concat(`station`,`name`,`param`)='" + item + "'");
                                    updatestatus(worker, row++, "Switchs", item, "удаление...", DateTime.Now.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region удаление дублирования в таблице Images
        private static void DeleteImages(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL localbaseSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение локально
            {
                if (localbaseSQL.Connected)
                {
                    using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Database))
                    {
                        if (baseSQL.Connected)
                        {
                            HashSet<string> locallist = new HashSet<string>();
                            HashSet<string> remotelist = new HashSet<string>();
                            locallist = getKeyItems(worker, e, localbaseSQL,
                                "Images", "concat(`name`, `filetime`)", (int)e.Argument);
                            remotelist = getKeyItems(worker, e, baseSQL,
                                "Images", "concat(`name`, `filetime`)", (int)e.Argument);
                            locallist.ExceptWith(remotelist);
                            if (locallist.Count == 0)
                                updatestatus(worker, 0, "Images", "синхронизировано", "", DateTime.Now.ToString());
                            else
                            {
                                int row = 0;
                                foreach (string item in locallist)
                                {
                                    if (worker.CancellationPending) { e.Cancel = true; break; }
                                    localbaseSQL.ExecSQL("delete from `Images`" +
                                        " where concat(`name`, `filetime`)='" + item + "'");
                                    updatestatus(worker, row++, "Images", item, "удаление...", DateTime.Now.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region удаление дублирования в таблице Reports
        private static void DeleteReports(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL localbaseSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение локально
            {
                if (localbaseSQL.Connected)
                {
                    using (ServerSQL baseSQL = new ServerSQL(DatabaseFrom.Database))
                    {
                        if (baseSQL.Connected)
                        {
                            HashSet<string> locallist = new HashSet<string>();
                            HashSet<string> remotelist = new HashSet<string>();
                            locallist = getKeyItems(worker, e, localbaseSQL,
                                "Reports", "concat(`name`, `filetime`)", (int)e.Argument);
                            remotelist = getKeyItems(worker, e, baseSQL,
                                "Reports", "concat(`name`, `filetime`)", (int)e.Argument);
                            locallist.ExceptWith(remotelist);
                            if (locallist.Count == 0)
                                updatestatus(worker, 0, "Reports", "синхронизировано", "", DateTime.Now.ToString());
                            else
                            {
                                int row = 0;
                                foreach (string item in locallist)
                                {
                                    if (worker.CancellationPending) { e.Cancel = true; break; }
                                    localbaseSQL.ExecSQL("delete from `Reports`" +
                                        " where concat(`name`, `filetime`)='" + item + "'");
                                    updatestatus(worker, row++, "Reports", item, "удаление...", DateTime.Now.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
        #region удаление дублирования в таблице RealVals
        private static HashSet<string> getRealValsItems(BackgroundWorker worker, DoWorkEventArgs e,
            ServerSQL localfetchSQL)
        {
            HashSet<string> list = new HashSet<string>();
            int offset = 0;
            int step = 10;
            int lines = 0;
            while (true)
            {
                string SQLlocallist = String.Format("select concat(`name`,`prop`)" +
                    " from `RealVals` limit {0},{1}", offset, step);
                lines = 0;
                using (MySqlCommand localcommand = new MySqlCommand(SQLlocallist,
                    localfetchSQL.myConnection))
                {
                    using (MySqlDataReader reader = localcommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (worker.CancellationPending) { e.Cancel = true; break; }
                            list.Add(reader.GetString(0));
                            lines++;
                        }
                    }
                }
                if (lines != step) break;
                offset += step;
            };
            return list;
        }

        private static void DeleteRealVals(BackgroundWorker worker, DoWorkEventArgs e)
        {
            using (ServerSQL localfetchSQL = new ServerSQL(DatabaseFrom.Fetchbase, true)) // чтение локально
            {
                if (localfetchSQL.Connected)
                {
                    using (ServerSQL fetchSQL = new ServerSQL(DatabaseFrom.Fetchbase))
                    {
                        if (fetchSQL.Connected)
                        {
                            HashSet<string> locallist = new HashSet<string>();
                            HashSet<string> remotelist = new HashSet<string>();
                            locallist = getKeyItems(worker, e, localfetchSQL,
                                "RealVals", "concat(`name`,`prop`)", (int)e.Argument);
                            remotelist = getKeyItems(worker, e, fetchSQL,
                                "RealVals", "concat(`name`,`prop`)", (int)e.Argument);
                            locallist.ExceptWith(remotelist);
                            if (locallist.Count == 0)
                                updatestatus(worker, 0, "RealVals", "синхронизировано", "", DateTime.Now.ToString());
                            else
                            {
                                int row = 0;
                                foreach (string item in locallist)
                                {
                                    if (worker.CancellationPending) { e.Cancel = true; break; }
                                    localfetchSQL.ExecSQL("delete from `RealVals`" +
                                        " where concat(`name`,`prop`)='" + item + "'");
                                    updatestatus(worker, row++, "RealVals", item, "удаление...", DateTime.Now.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

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
                Data.SendToSystemLog(stationNumber, "Репликация", mess);
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
