using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BaseServer;
using Points.Plugins;

namespace FetchSPT961
{
    public partial class frmFetchingSPT961 : Form
    {
        private IDictionary<string, Entity> fetchlist = new Dictionary<string, Entity>();
        //int LastMin = -1; int LastHour = -1; int LastDay = -1; int LastMonth = -1;
        //bool SQLServerConnected = false;
        bool Registered = false;
        bool Bonus = false;
        DateTime TurnOnTime;
        string ClientID = String.Empty;

        public frmFetchingSPT961()
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
            // Загрузка плагина SPT961 точек
            PointPlugin.LoadPlugin(Application.StartupPath + "\\Points.SPT961.dll");
            // Инициализация, создание баз, если их раньше не было
            Settings.CreateDataAndFetchBases();
            Data.SendToSystemLog(Properties.Settings.Default.Station,
                "Опрос СПТ961", "Сервер опроса загружен");
            ClientID = Data.ClientLogin(ClientID, "F", Properties.Settings.Default.Station,
                "Сервер опроса СПТ961");
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
            // Загрузка SPT961 точек из базы данных
            Data.LoadBase(PointPlugin.LoadPlugin(Application.StartupPath + "\\Points.SPT961.dll"));
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
                                    ent.SetRealProp("DataCount", ((int)ent.Values["RecordCount"] - 1).ToString());
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
            // завершение работы
            Data.SendToSystemLog(Properties.Settings.Default.Station,
                "Опрос СПТ961", "Сервер опроса выгружен");
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
                        "Сервер опроса СПТ961"});

                if (!Registered)
                {
                    if (DateTime.Now.AddHours(-2.5) > TurnOnTime)
                    {
                        Bonus = false;
                        Data.SendToSystemLog(Properties.Settings.Default.Station,
                            "Опрос СПТ961", "Прекращена работа неавторизованного сервера опроса");
                    }
                }
                if (Registered || Bonus)
                {
                    if (!backCheckCommand.IsBusy)
                        backCheckCommand.RunWorkerAsync(
                            new object[] { ClientID, Properties.Settings.Default.Station,
                            "Сервер опроса СПТ961" });

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
                    "Опрос СПТ961", "Сервер опроса перезагрузил свой список опроса");
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
        private int CRCode(byte[] msg)
        {
            int crc = 0;
            foreach (byte b in msg)
            {
                crc = crc ^ (int)b << 8;
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x8000) > 0)
                        crc = (crc << 1) ^ 0x1021;
                    else
                        crc <<= 1;
                }
            }
            return crc;
        }

        private byte[] PrepareFetchParam(byte dad, byte sad, int channel, int parameter)
        {
            List<byte> list = new List<byte>();
            list.Add(0x10); list.Add(0x01); // DLE SOH
            list.Add(dad); // DAD - адрес СПГ в СП-сети: 0 
            list.Add(sad); // SAD - адрес станции в СП-сети: 1
            list.Add(0x10); list.Add(0x1f); // DLE IS1
            list.Add(0x1d); // 1D - чтение параметра 
            list.Add(0x10); list.Add(0x02); // DLE STX
            list.Add(0x09); // HT
            list.AddRange(cp866unicode.OemString(channel.ToString()));
            list.Add(0x09); // HT
            list.AddRange(cp866unicode.OemString(parameter.ToString()));
            list.Add(0x0c); // FF
            list.Add(0x10); list.Add(0x03); // DLE ETX
            // контрольная сумма
            byte[] crcbuff = list.ToArray();
            byte[] arg = new byte[crcbuff.Length - 2];
            Array.Copy(crcbuff, 2, arg, 0, crcbuff.Length - 2);
            int crc = CRCode(arg);
            list.Add((byte)(crc >> 8)); // high crc parth
            list.Add((byte)(crc & 0xff)); // low crc parth
            return list.ToArray(); 
        }

        string[] formats = new string[4] { "dd-MM-yy HH:mm:00", "dd-MM-yy HH:00:00",
                    "dd-MM-yy 00:00:00", "01-MM-yy 00:00:00" };

        private byte[] PrepareTableParam(byte dad, byte sad, int channel, int parameter,
            int tabletype, DateTime snaptime)
        {
            DateTime dt = DateTime.Parse(snaptime.ToString(formats[tabletype]));
            //DateTime dt = DateTime.Parse("17-06-11 00:00:00");
            List<byte> list = new List<byte>();
            list.Add(0x10); list.Add(0x01); // DLE SOH
            list.Add(dad); // DAD - адрес СПГ в СП-сети: 0 
            list.Add(sad); // SAD - адрес станции в СП-сети: 1
            list.Add(0x10); list.Add(0x1f); // DLE IS1
            list.Add(0x0e); // 0E - чтение элемента временного массива 
            list.Add(0x10); list.Add(0x02); // DLE STX
            list.Add(0x09); // HT
            list.AddRange(cp866unicode.OemString(channel.ToString()));
            list.Add(0x09); // HT
            list.AddRange(cp866unicode.OemString(parameter.ToString()));
            list.Add(0x0c); // FF
            // первая дата
            list.Add(0x09); // HT
            list.AddRange(cp866unicode.OemString(dt.AddMinutes(10).Day.ToString()));
            list.Add(0x09); // HT
            list.AddRange(cp866unicode.OemString(dt.AddMinutes(10).Month.ToString()));
            list.Add(0x09); // HT
            list.AddRange(cp866unicode.OemString(dt.AddMinutes(10).Year.ToString().Substring(2)));
            list.Add(0x09); // HT
            list.AddRange(cp866unicode.OemString(dt.AddMinutes(10).Hour.ToString()));
            list.Add(0x09); // HT
            list.AddRange(cp866unicode.OemString(dt.AddMinutes(10).Minute.ToString()));
            list.Add(0x09); // HT
            list.AddRange(cp866unicode.OemString(dt.AddMinutes(10).Second.ToString()));
            list.Add(0x0c); // FF
            // вторая дата
            list.Add(0x09); // HT
            list.AddRange(cp866unicode.OemString(dt.AddMinutes(-10).Day.ToString()));
            list.Add(0x09); // HT
            list.AddRange(cp866unicode.OemString(dt.AddMinutes(-10).Month.ToString()));
            list.Add(0x09); // HT
            list.AddRange(cp866unicode.OemString(dt.AddMinutes(-10).Year.ToString().Substring(2)));
            list.Add(0x09); // HT
            list.AddRange(cp866unicode.OemString(dt.AddMinutes(-10).Hour.ToString()));
            list.Add(0x09); // HT
            list.AddRange(cp866unicode.OemString(dt.AddMinutes(-10).Minute.ToString()));
            list.Add(0x09); // HT
            list.AddRange(cp866unicode.OemString(dt.AddMinutes(-10).Second.ToString()));
            list.Add(0x0c); // FF
            list.Add(0x10); list.Add(0x03); // DLE ETX
            // контрольная сумма
            byte[] crcbuff = list.ToArray();
            byte[] arg = new byte[crcbuff.Length - 2];
            Array.Copy(crcbuff, 2, arg, 0, crcbuff.Length - 2);
            int crc = CRCode(arg);
            list.Add((byte)(crc >> 8)); // high crc parth
            list.Add((byte)(crc & 0xff)); // low crc parth
            return list.ToArray();
        }

        private List<string> EncodeFetchAnswer(byte[] receivedBytes, BackgroundWorker worker)
        {
            List<string> result = new List<string>();
            // очистка телеграммы от мусора
            List<byte> source = new List<byte>();
            bool SOH = false;
            bool DLE = false;
            foreach (byte b in receivedBytes)
            {
                if (SOH) source.Add(b);
                if ((b == 0x01) && DLE && !SOH) { SOH = true; source.Add(0x10); source.Add(0x01); }
                DLE = (b == 0x10);
            }
            byte[] buff = source.ToArray();
            // проверка КС
            byte[] test = new byte[buff.Length - 2];
            Array.Copy(buff, 2, test, 0, buff.Length - 2);
            int crc = CRCode(test);
            if (crc != 0)
            {
                worker.ReportProgress(0, "ОШИБКА;Ошибка контрольной суммы");
                return result;
            }
            // анти-стаффинг
            List<byte> arr = new List<byte>();
            DLE = false;
            foreach (byte b in buff)
            {
                if ((b != 0x10) || (b == 0x10) && DLE) arr.Add(b);
                DLE = (b == 0x10);
            }
            // разборка телеграммы
            // [0] - SOH; [1] - DAD; [2] - SAD; [3] - IS1; [4] - FNC
            int n = 0;
            byte DAD = 0;
            byte SAD = 0;
            byte FNC = 0;
            bool STX = false;
            bool ETX = false;
            List<byte> dataset = new List<byte>(); // содержит данные ответа
            while (n < arr.Count)
            {
                switch (n)
                {
                    case 1: DAD = arr[n]; break;
                    case 2: SAD = arr[n]; break;
                    case 4: FNC = arr[n]; break;
                    default:
                        if (n > 4)
                        {
                            if (!ETX && (arr[n] == 0x03)) ETX = true;
                            if (STX && !ETX) dataset.Add(arr[n]);
                            if (!STX && (arr[n] == 0x02)) STX = true;
                        }
                        break;
                }
                n++;
            }
            // разбор блока данных
            byte[] channel = new byte[] { };
            byte[] param = new byte[] { };
            byte[] value = new byte[] { };
            byte[] eu = new byte[] { };
            byte[] time = new byte[] { };
            bool addr = true;
            int index = 0;
            n = 0;
            List<byte> data = new List<byte>();
            while (n < dataset.Count)
            {
                if ((dataset[n] == 0x09) || (dataset[n] == 0x0c))
                {
                    switch (index)
                    {
                        case 1:
                            if (addr)
                                channel = data.ToArray();
                            else
                                value = data.ToArray();
                            break;
                        case 2:
                            if (addr)
                                param = data.ToArray();
                            else
                                eu = data.ToArray();
                            break;
                        case 3:
                            if (!addr)
                                time = data.ToArray();
                            break;
                    }
                    data.Clear();
                    index++;
                }
                else
                    data.Add(dataset[n]);
                if (addr && (dataset[n] == 0x0c))
                {
                    addr = false;
                    index = 0;
                }
                n++;
            }
            result.Add(cp866unicode.UnicodeString(channel));
            result.Add(cp866unicode.UnicodeString(param));
            result.Add(cp866unicode.UnicodeString(value));
            result.Add(cp866unicode.UnicodeString(eu));
            result.Add(cp866unicode.UnicodeString(time));
            return result;
        }

        private List<string> EncodeTableAnswer(byte[] receivedBytes, BackgroundWorker worker)
        {
            List<string> result = new List<string>();
            // очистка телеграммы от мусора
            List<byte> source = new List<byte>();
            bool SOH = false;
            bool DLE = false;
            foreach (byte b in receivedBytes)
            {
                if (SOH) source.Add(b);
                if ((b == 0x01) && DLE && !SOH) { SOH = true; source.Add(0x10); source.Add(0x01); }
                DLE = (b == 0x10);
            }
            byte[] buff = source.ToArray();
            // проверка КС
            byte[] test = new byte[buff.Length - 2];
            Array.Copy(buff, 2, test, 0, buff.Length - 2);
            int crc = CRCode(test);
            if (crc != 0)
            {
                worker.ReportProgress(0, "ОШИБКА;Ошибка контрольной суммы");
                return result;
            }
            // анти-стаффинг
            List<byte> arr = new List<byte>();
            DLE = false;
            foreach (byte b in buff)
            {
                if ((b != 0x10) || (b == 0x10) && DLE) arr.Add(b);
                DLE = (b == 0x10);
            }
            // разборка телеграммы
            // [0] - SOH; [1] - DAD; [2] - SAD; [3] - IS1; [4] - FNC
            int n = 0;
            byte DAD = 0;
            byte SAD = 0;
            byte FNC = 0;
            bool STX = false;
            bool ETX = false;
            List<byte> dataset = new List<byte>(); // содержит данные ответа
            while (n < arr.Count)
            {
                switch (n)
                {
                    case 1: DAD = arr[n]; break;
                    case 2: SAD = arr[n]; break;
                    case 4: FNC = arr[n]; break;
                    default:
                        if (n > 4)
                        {
                            if (!ETX && (arr[n] == 0x03)) ETX = true;
                            if (STX && !ETX) dataset.Add(arr[n]);
                            if (!STX && (arr[n] == 0x02)) STX = true;
                        }
                        break;
                }
                n++;
            }
            // разбор блока данных
            byte[] channel = new byte[] { };
            byte[] param = new byte[] { };
            byte[] value = new byte[] { };
            byte[] eu = new byte[] { };
            byte[] time = new byte[] { };
            bool addr = true;
            bool time1 = false;
            bool time2 = false;
            int index = 0;
            n = 0;
            List<byte> data = new List<byte>();
            while (n < dataset.Count)
            {
                if ((dataset[n] == 0x09) || (dataset[n] == 0x0c))
                {
                    switch (index)
                    {
                        case 1:
                            if (addr && !time1 && !time2)
                                channel = data.ToArray();
                            else
                                value = data.ToArray();
                            break;
                        case 2:
                            if (addr && !time1 && !time2)
                                param = data.ToArray();
                            else
                                eu = data.ToArray();
                            break;
                        case 3:
                            if (!addr && !time1 && !time2)
                                time = data.ToArray();
                            break;
                    }
                    data.Clear();
                    index++;
                }
                else
                    data.Add(dataset[n]);
                if (addr && !time1 && !time2 && (dataset[n] == 0x0c))
                {
                    addr = false;
                    time1 = true;
                    index = 0;
                }
                else if (!addr && time1 && !time2 && (dataset[n] == 0x0c))
                {
                    time1 = false;
                    time2 = true;
                    index = 0;
                }
                else if (!addr && !time1 && time2 && (dataset[n] == 0x0c))
                {
                    time2 = false;
                    index = 0;
                }
                n++;
            }
            result.Add(cp866unicode.UnicodeString(channel));
            result.Add(cp866unicode.UnicodeString(param));
            result.Add(cp866unicode.UnicodeString(value));
            result.Add(cp866unicode.UnicodeString(eu));
            result.Add(cp866unicode.UnicodeString(time));
            return result;
        }

        private List<string> FetchTCP(byte[] sendBytes, BackgroundWorker worker, bool istable)
        {
            List<string> result = new List<string>();
            #region ----------- Обмен с сокетом по TCP/IP -----------------
            //создаём соккет для отправки запроса серверу
            using (Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                ProtocolType.Tcp))
            {
                //создаём соединение                   
                try
                {
                    IPEndPoint remoteEP = new IPEndPoint(
                        IPAddress.Parse(Properties.Settings.Default.Host), //172.29.163.179
                        int.Parse(Properties.Settings.Default.Port)); // 4002
                    try
                    {
                        sock.Connect(remoteEP);//здесь указать нужный IP
                        //отправляем запрос на сервер
                        sock.SendTimeout = 5000; // таймаут 5 секунд на запрос
                        sock.Send(sendBytes);
                        Thread.Sleep(Properties.Settings.Default.WaitTime); // задержка чтения ответа
                        byte[] receivedBytes = new byte[1024];
                        sock.ReceiveTimeout = 5000; // таймаут 5 секунд на ответ
                        int numBytes = sock.Receive(receivedBytes); // получаем numBytes
                        sock.Disconnect(true);
                        // разборка ответа устройства
                        if (numBytes > 2)
                        {
                            if (istable)
                                result = EncodeTableAnswer(receivedBytes, worker);
                            else
                                result = EncodeFetchAnswer(receivedBytes, worker);
                        }
                    }
                    catch (Exception except)
                    {
                        worker.ReportProgress(0, "ОШИБКА;Ошибка связи: " + except.Message);
                        // test time
                        //byte[] receivedBytes = new byte[]
                        //{0xFF,0xFF,0x10,0x01,0x01,0x00,0x10,0x1F,0x03,0x10,0x02,0x09,0x30,0x09,0x30,0x36,
                        // 0x31,0x0C,0x09,0x31,0x31,0x3A,0x33,0x30,0x3A,0x34,0x30,0x09,0xE7,0xE7,0x3A,0xAC,
                        // 0xAC,0x3A,0xE1,0xE1,0x0C,0x10,0x03,0x84,0x8E};
                        // test date
                        //byte[] receivedBytes = new byte[]
                        //{0xFF,0xFF,0x10,0x01,0x01,0x00,0x10,0x1F,0x03,0x10,0x02,0x09,0x30,0x09,0x30,0x36,
                        // 0x30,0x0C,0x09,0x32,0x37,0x2D,0x30,0x35,0x2D,0x31,0x31,0x09,0xA4,0xA4,0x2D,0xAC,
                        // 0xAC,0x2D,0xA3,0xA3,0x0C,0x10,0x03,0xAC,0x65};
                        // test archive
                        //byte[] receivedBytes = new byte[]
                        //{0xFF,0xFF,0x10,0x01,0x01,0x00,0x10,0x1F,0x16,0x10,0x02,
                        // 0x09,0x31,0x09,0x32,0x31,0x36,0x0C,
                        // 0x09,0x31,0x37,0x09,0x36,0x09,0x31,0x31,0x09,0x30,0x09,0x30,0x09,0x30,0x09,0x0C,
                        // 0x09,0x31,0x37,0x09,0x36,0x09,0x31,0x31,0x09,0x30,0x09,0x30,0x09,0x30,0x09,0x0C,
                        // 0x09,0x31,0x2E,0x32,0x33,0x09,0xE2,0x09,0x32,0x37,0x2D,0x30,0x35,0x2D,
                        // 0x31,0x31,0x2F,0x20,0x30,0x3A,0x30,0x30,0x3A,0x30,0x30,0x0C,0x10,0x03,0x48,0xE9};
                        //result = EncodeTableAnswer(receivedBytes, worker);
                    }
                }
                catch (Exception except)
                {
                    worker.ReportProgress(0, "ОШИБКА;Ошибка конфигурирования: " + except.Message);
                }
            }
            #endregion ----------------------------------------------------
            return result;
        }

        private string getStringValue(BackgroundWorker worker, int par)
        {
            byte dad = Properties.Settings.Default.NodeNetAddr;
            byte sad = Properties.Settings.Default.FetchNetAddr;
            int chan = (int)0;
            // чтение параметра
            byte[] sendBytes = PrepareFetchParam(dad, sad, chan, par);
            List<string> answer = FetchTCP(sendBytes, worker, false);
            if (answer.Count == 5)
            {
                // answer[0] // ChannelNumberReturn
                // answer[1] // ParameterReturn
                // answer[2] // ValueReturn
                StringBuilder value = new StringBuilder(answer[2]);
                // answer[3] // EUDescReturn
                // answer[4] // SnapTimeReturn
                while (value.Length < 6) value.Insert(0, "0");
                return value.ToString();
            }
            else
                return String.Empty;
        }

        private DateTime DateTimeFromController(BackgroundWorker worker)
        {
            string sdate = getStringValue(worker, Properties.Settings.Default.DateAddr); //ддммгг
            string stime = getStringValue(worker, Properties.Settings.Default.TimeAddr); //ччммсс
            if (sdate.Length == 6 && stime.Length == 6)
            {
                int year = 0;
                int.TryParse(sdate.Substring(4, 2), out year);
                int month = 0;
                int.TryParse(sdate.Substring(2, 2), out month);
                int day = 0;
                int.TryParse(sdate.Substring(0, 2), out day);
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
            using (ServerSQL dataSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
            {
                using (ServerSQL fetchSQL = new ServerSQL(DatabaseFrom.Fetchbase, true)) // чтение
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
                        //            if (LastMonth != MM)
                        //            {
                        //                LastMonth = MM; tabletype = 3;
                        //            }
                        //        }
                        //    }
                        //}
                        #endregion
                        int count = 1;
                        foreach (KeyValuePair<string, Entity> kvp in list)
                        {
                            if (worker.CancellationPending) break;
                            string ptname = kvp.Key;
                            Entity ent = kvp.Value;
                            int fetchtime = (int)ent.Values["FetchTime"];
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
                                    ent.SetRealProp("Seconds", seconds.ToString("#.#"));
                                    fetch = seconds >= (double)fetchtime;
                                }
                                else fetch = true;
                                if (fetch)
                                {
                                    if (ent.Values.ContainsKey("Channel") && ent.Values.ContainsKey("Node"))
                                    {
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
                                        switch (ptKind)
                                        {
                                            case PtKind.Group:
                                                #region Обработка группы опроса
                                                for (int i = 1; i <= 32; i++)
                                                {
                                                    if (worker.CancellationPending) break;
                                                    string childname = (string)ent.Values["Child" + i];
                                                    if (!String.IsNullOrWhiteSpace(childname))
                                                    {
                                                        Entity child = Data.GetEntity(childname, dataSQL);
                                                        if (!child.Empty && (bool)child.Values["Actived"])
                                                        {
                                                            byte dad = Properties.Settings.Default.NodeNetAddr;
                                                            byte sad = Properties.Settings.Default.FetchNetAddr;
                                                            int chan = (int)child.Values["ChannelNumber"];
                                                            int par = (int)child.Values["Parameter"];
                                                            // чтение параметра
                                                            byte[] sendBytes = PrepareFetchParam(dad, sad, chan, par);
                                                            List<string> answer = FetchTCP(sendBytes, worker, false);
                                                            if (answer.Count == 5)
                                                            {
                                                                // присвоение данных точке
                                                                child.Reals = Data.GetRealValues(childname, fetchSQL, dataSQL);
                                                                child.SetRealProp("ChannelNumberReturn", answer[0]);
                                                                child.SetRealProp("ParameterReturn", answer[1]);
                                                                child.SetRealProp("ValueReturn", answer[2]);
                                                                decimal value = Data.FloatParse(answer[2]);
                                                                child.SetRealProp("EUDescReturn", answer[3]);
                                                                child.SetRealProp("SnapTimeReturn", answer[4]);
                                                                if (!Double.IsNaN((Double)value))
                                                                {
                                                                    Double raw = (Double)value;
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
                                                                // индикация нормального выполнения
                                                                DateTime LastSnap = NowTime;
                                                                worker.ReportProgress(0, ptname + ";" +
                                                                    LastSnap.ToString());
                                                            }
                                                            else
                                                                worker.ReportProgress(0,
                                                                    ptname + ";нет связи с СПТ961");
                                                        }
                                                    }
                                                }
                                                #endregion
                                                break;
                                            case PtKind.Table:
                                                int thistabletype = (int)ent.Values["TableType"];
                                                bool stable = false;
                                                if (ent.Reals.ContainsKey("Stable"))
                                                    stable = bool.Parse(ent.Reals["Stable"]);
                                                //if (stable)
                                                //{
                                                //    if (thistabletype > tabletype)
                                                //        continue;
                                                //}
                                                if (!ent.Reals.ContainsKey("DataCount"))
                                                    ent.SetRealProp("DataCount", ((int)ent.Values["RecordCount"] - 1).ToString());
                                                int datacount = int.Parse(ent.Reals["DataCount"]);
                                                bool isHour = false;
                                                bool isDay = false;
                                                bool isMonth = false;
                                                bool isCount = false;
                                                DateTime asktime = NowTime;
                                                for (int i = 1; i <= 32; i++)
                                                {
                                                    if (worker.CancellationPending) break;
                                                    string[] childrec =
                                                           ent.Values["Child" + i].ToString().Split(new char[] { ';' });
                                                    string childname = childrec[0];
                                                    int accumtype = (childrec.Length == 2) ? int.Parse(childrec[1]) : 0;
                                                    if (!String.IsNullOrWhiteSpace(childname))
                                                    {
                                                        Entity child = Data.GetEntity(childname, dataSQL);
                                                        if (!child.Empty && (bool)child.Values["Actived"])
                                                        {
                                                            byte dad = Properties.Settings.Default.NodeNetAddr;
                                                            byte sad = Properties.Settings.Default.FetchNetAddr;
                                                            int chan = (int)child.Values["ChannelNumber"];
                                                            int par;
                                                            //DateTime asktime = NowTime;
                                                            switch (thistabletype)
                                                            {
                                                                case 1:
                                                                    par = (int)child.Values["HourArray"];
                                                                    if (!isHour)
                                                                    {
                                                                        isHour = true;
                                                                        asktime = asktime.AddHours(-datacount);
                                                                    }
                                                                    break;
                                                                case 2:
                                                                    par = (int)child.Values["DayArray"];
                                                                    if (!isDay)
                                                                    {
                                                                        isDay = true;
                                                                        asktime = asktime.AddDays(-datacount);
                                                                    }
                                                                    break;
                                                                case 3:
                                                                    par = (int)child.Values["MonthArray"];
                                                                    if (!isMonth)
                                                                    {
                                                                        isMonth = true;
                                                                        asktime = asktime.AddMonths(-datacount);
                                                                    }
                                                                    break;
                                                                default:
                                                                    par = (int)child.Values["Parameter"];
                                                                    break;
                                                            }
                                                            byte[] sendBytes;
                                                            List<string> answer;
                                                            if (thistabletype > 0)
                                                            {
                                                                // чтение элемента временного массива
                                                                sendBytes = PrepareTableParam(dad, sad, chan, par, thistabletype, asktime);
                                                                answer = FetchTCP(sendBytes, worker, true);
                                                            }
                                                            else
                                                            {
                                                                // чтение параметра
                                                                sendBytes = PrepareFetchParam(dad, sad, chan, par);
                                                                answer = FetchTCP(sendBytes, worker, false);
                                                            }
                                                            if (answer.Count == 5)
                                                            {
                                                                // присвоение данных точке
                                                                //child.Reals = Data.GetRealValues(childname);
                                                                //child.SetRealProp("ChannelNumberReturn", answer[0]);
                                                                //child.SetRealProp("ParameterReturn", answer[1]);
                                                                //child.SetRealProp("ValueReturn", answer[2]);
                                                                decimal value = Data.FloatParse(answer[2]);
                                                                //child.SetRealProp("EUDescReturn", answer[3]);
                                                                //child.SetRealProp("SnapTimeReturn", answer[4]);
                                                                string[] sdt = answer[4].Split(new char[] { '/' });
                                                                DateTime LastSnap = NowTime;
                                                                if (sdt.Length == 2)
                                                                {
                                                                    DateTime D;
                                                                    if (DateTime.TryParse(sdt[0] + " " + sdt[1], out D))
                                                                    {
                                                                        // коррекция даты, получаемой от СПТ961, добавлено 20.12.2012
                                                                        switch (thistabletype)
                                                                        {
                                                                            case 1: D = D.AddHours(-1); break;
                                                                            case 2: D = D.AddDays(-1); break;
                                                                            case 3: D = D.AddMonths(-1); break;
                                                                        }
                                                                        LastSnap = D;
                                                                        if (!Double.IsNaN((Double)value))
                                                                        {
                                                                            Single raw = (Single)value;
                                                                            Data.AddIntoTable(D, childname + ".PV", raw, true,
                                                                                thistabletype, accumtype, dataSQL);
                                                                        }
                                                                        else
                                                                            Data.AddIntoTable(D, childname + ".PV", 0, false,
                                                                                thistabletype, accumtype, dataSQL);
                                                                        // индикация нормального выполнения
                                                                        worker.ReportProgress(count / list.Count, ptname + ";" +
                                                                            LastSnap.ToString());
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    Data.AddIntoTable(asktime, childname + ".PV", 0, false,
                                                                        thistabletype, accumtype, dataSQL);
                                                                    worker.ReportProgress(count / list.Count, ptname + ";" +
                                                                        asktime.ToString());
                                                                }
                                                            }
                                                            else
                                                                worker.ReportProgress(count / list.Count,
                                                                    ptname + ";нет связи с СПТ961");
                                                            if (!isCount)
                                                            {
                                                                isCount = true;
                                                                if (datacount > 0)
                                                                {
                                                                    datacount--;
                                                                    ent.SetRealProp("DataCount", datacount.ToString());
                                                                    ent.SetRealProp("Stable", false.ToString());
                                                                }
                                                                else
                                                                    ent.SetRealProp("Stable", true.ToString());
                                                                Data.WriteRealVals(ent, fetchSQL);
                                                            }
                                                        }
                                                    }
                                                }
                                                break;
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
                        "Опрос СПТ961", lastError);
                }
            }
            else
            {
                string ptname = info[0];
                string time = info[1];
                //Entity ent = Data.GetEntity(ptname);
                //if (!ent.Empty)
                //{
                //    string ptdesc = (string)ent.Values["PtDesc"];
                //    Data.UpdateClientFetchList(ClientID, ptname, ptdesc, "", "", time);
                //}
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
            using (frmTuningSPT961 form = new frmTuningSPT961())
            {
                form.cbStation.SelectedIndex = Properties.Settings.Default.Station - 1;
                form.tbIPAddress.Text = Properties.Settings.Default.Host;
                form.tbIPPort.Text = Properties.Settings.Default.Port;
                form.tbNodeNetAddr.Text = Properties.Settings.Default.NodeNetAddr.ToString();
                form.tbFetchNetAddr.Text = Properties.Settings.Default.FetchNetAddr.ToString();
                form.tbWaitTime.Text = Properties.Settings.Default.WaitTime.ToString();
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
                    int waittime;
                    if (int.TryParse(form.tbWaitTime.Text, out waittime))
                        settings.WaitTime = waittime;
                    else
                        form.tbWaitTime.Text = settings.WaitTime.ToString();
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
                    byte nodenetaddr;
                    if (byte.TryParse(form.tbNodeNetAddr.Text, out nodenetaddr))
                        settings.NodeNetAddr = nodenetaddr;
                    else
                        form.tbNodeNetAddr.Text = settings.NodeNetAddr.ToString();
                    byte fetchnetaddr;
                    if (byte.TryParse(form.tbFetchNetAddr.Text, out fetchnetaddr))
                        settings.FetchNetAddr = fetchnetaddr;
                    else
                        form.tbFetchNetAddr.Text = settings.FetchNetAddr.ToString();
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

        private void frmFetchingSPT961_Resize(object sender, EventArgs e)
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
                clientID = Data.ClientLogin(ClientID, "F", stationNumber, serverName);
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
                Data.SendToSystemLog(stationNumber, "Опрос СПТ961", mess);
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
