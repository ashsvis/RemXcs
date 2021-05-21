using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Threading;
using BaseServer;
using IniFiles.Net;
using Points.Plugins;

namespace FetchModbus
{
    public delegate void exitApp();

    public static class FetchModbusFunctions
    {
        private static int Station = 0;
        private static List<
            Tuple<string, BackgroundWorker>> workers =
            new List<Tuple<string, BackgroundWorker>>();

        public static void StartFetch(Tuple<int, string[], string, exitApp, int> args)
        {
            Station = args.Item1;
            string[] channels = args.Item2;
            foreach (string chaninfo in channels)
            {
                if (chaninfo.StartsWith("Channel"))
                {
                    string sChannel = chaninfo.Substring(7, chaninfo.IndexOf("=") - 7);
                    int channel;
                    if (int.TryParse(sChannel, out channel))
                    {
                        string info = chaninfo.Substring(chaninfo.IndexOf("=") + 1).Trim();
                        if (info.Length > 0)
                        {
                            string desc = "Канал " + channel + " сервера опроса";
                            Data.SendToSystemLog(Station, "Опрос Modbus", desc + " загружен");
                            string ClientID = Data.ClientLogin(String.Empty, "F", Station, desc + " Modbus");
                            BackgroundWorker worker = new BackgroundWorker();
                            workers.Add(new Tuple<string,BackgroundWorker>(ClientID, worker));
                            worker.DoWork += worker_DoWork;
                            worker.WorkerSupportsCancellation = true;
                            worker.RunWorkerAsync(new Tuple<int, string, string, exitApp, string, int>(
                                channel, info, args.Item3, args.Item4, ClientID, args.Item5));
                        }
                    }
                }
            }
        }

        public static void StopFetch()
        {
            //Код, выполняемый при остановке процесса
            foreach (Tuple<string, BackgroundWorker> item in workers)
            {
                string clientID = item.Item1;
                BackgroundWorker worker = item.Item2;
                if (worker.IsBusy) worker.CancelAsync();
                Data.SendToSystemLog(Station, "Опрос Modbus", "Сервер опроса выгружен");
                Data.ClientLogout(clientID);
                Data.ClearClientFetchList(clientID);
            }
        }

        private static void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Tuple<int, string, string, exitApp, string, int> args =
                (Tuple<int, string, string, exitApp, string, int>)e.Argument;
            int channel = args.Item1;
            string info = args.Item2;
            MemIniFile mif = new MemIniFile(String.Empty);
            mif.FromString(args.Item3);
            exitApp DoExitApp = args.Item4;
            string ClientID = args.Item5;
            int timeout = args.Item6;

            string section = "Application";
            int station = mif.ReadInteger(section, "Station", 1);
            string ApplicationStartupPath = mif.ReadString(section, "StartupPath", ".");
            bool Registered = mif.ReadBool(section, "Registered", false);
            bool Bonus = mif.ReadBool(section, "Bonus", false);

            Data.RestoreSQLsettingsFromString(args.Item3);
            BackgroundWorker worker = (BackgroundWorker)sender;
            DateTime TurnOnTime = DateTime.Now;
            IDictionary<string, IPointPlugin> plugins = 
                PointPlugin.LoadPlugin(ApplicationStartupPath + "\\Points.Modbus.dll");
            // Инициализация, создание баз, если их раньше не было
            Settings.CreateDataAndFetchBases();
            string desc = "Канал " + channel + " сервера опроса"; 
            // Заполнение списка для опроса
            LoadFirst(worker, ClientID, channel, plugins);

            bool _exit = false;
            bool _continue = true;
            while (_continue)
            {
                if (!Data.ImLive(ClientID, "F", Station, desc + " Modbus"))
                {
                    ClientID = Data.ClientLogin(ClientID, "F", station, desc + " Modbus");
                }
                string[] command = Data.GetClientCommand(ClientID);
                //if (command[0].Equals("RELOAD"))
                //{
                //}
                if (!Registered)
                {
                    if (DateTime.Now.AddHours(-2.5) > TurnOnTime)
                    {
                        Bonus = false;
                    }
                }
                if (Registered || Bonus)
                {
                    try
                    {
                        FetchBase(worker, ClientID, channel, info, timeout, plugins);
                        Thread.Sleep(50);
                    }
                    catch (Exception ex)
                    {
                        Data.SendToSystemLog(0, "Опрос Modbus", ex.Message);
                    }
                }
                else
                    if (!_exit)
                    {
                        _exit = true;
                        Data.SendToSystemLog(Station,
                                "Опрос Modbus", "Прекращена работа неавторизованного сервера опроса");
                        DoExitApp();
                    }
            }
        }

        private static DateTime DateTimeFromController(BackgroundWorker worker)
        {
            return DateTime.Now;
        }

        private static byte[] Swap(byte[] buff)
        {
            int count = buff.Length;
            if ((count & 1) > 0) count++;
            byte[] result = new byte[count];
            Array.Copy(buff, result, buff.Length);
            if (buff.Length < count) result[count - 1] = 0;
            int i = 1;
            while (i < count)
            {
                byte m = result[i - 1];
                result[i - 1] = result[i];
                result[i] = m;
                i += 2;
            }
            return result;
        }

        private static UInt32 BigSwap(UInt32 value)
        {
            byte[] buff = BitConverter.GetBytes(value);
            byte[] buff1 = new byte[4];
            buff1[0] = buff[2];
            buff1[1] = buff[3];
            buff1[2] = buff[0];
            buff1[3] = buff[1];
            return BitConverter.ToUInt32(buff1, 0);
        }

        private static ushort CRC(byte[] buff, int len)
        {   // контрольная сумма MODBUS RTU
            ushort result = 0xFFFF;
            if (len <= buff.Length)
            {
                for (int i = 0; i < len; i++)
                {
                    result ^= buff[i];
                    for (int j = 0; j < 8; j++)
                    {
                        bool flag = (result & 0x0001) > 0;
                        result >>= 1;
                        if (flag) result ^= 0xA001;
                    }
                }
            }
            return result;
        }

        private static void FetchBase(BackgroundWorker worker, string ClientID, int channel,
            string cnannelConfig, int timeout, IDictionary<string, IPointPlugin> plugins)
        {
            #region Опрос
            IDictionary<string, Entity> fetchlist = new Dictionary<string, Entity>();
            using (ServerSQL dataSQL = new ServerSQL(DatabaseFrom.Database, false)) // чтение
            {
                using (ServerSQL fetchSQL = new ServerSQL(DatabaseFrom.Fetchbase, false)) // чтение
                {
                    if (dataSQL.Connected && fetchSQL.Connected)
                    {
                        IDictionary<string, Entity> entities =
                            Data.LoadBase(plugins, dataSQL);
                        foreach (KeyValuePair<string, Entity> kvp in entities)
                        {
                            if (worker.CancellationPending) return;
                            Entity ent = kvp.Value;
                            if (!ent.Empty && (int)ent.Values["Channel"] == channel)
                            {
                                int ptKind = int.Parse(ent.Values["PtKind"].ToString());
                                if (ptKind != PtKind.Group && ptKind != PtKind.Table) continue;
                                fetchlist.Add(kvp.Key, kvp.Value);
                            }
                        }
                        #region определение типа таблицы tabletype
                        DateTime NowTime = DateTimeFromController(worker);
                        #endregion
                        int count = 1;
                        foreach (KeyValuePair<string, Entity> kvp in fetchlist)
                        {
                            if (worker.CancellationPending) return;
                            string ptname = kvp.Key;
                            Entity ent = kvp.Value;
                            string ptdesc = (string)ent.Values["PtDesc"];
                            int ptKind = int.Parse(ent.Values["PtKind"].ToString());
                            if (ptKind != PtKind.Group && ptKind != PtKind.Table) continue;
                            ent.Reals = Data.GetRealValues(ptname, fetchSQL, dataSQL);
                            if (ent.Reals.Count == 0)
                            {
                                //worker.ReportProgress(0, "ОШИБКА;Нет связи с SQL");
                                continue;
                            }
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
                                    List<byte> request = new List<byte>();
                                    if (ent.Values.ContainsKey("Channel") && ent.Values.ContainsKey("Node"))
                                    {
                                        List<string> childs;
                                        //int channel = (int)ent.Values["Channel"];
                                        byte addr = (byte)(int)ent.Values["Node"];
                                        switch (ptKind)
                                        {
                                            case PtKind.Group:
                                                string pttype = (string)ent.Values["PtType"];
                                                switch (pttype)
                                                {
                                                    case "GA":
                                                        childs = GetChilds(ent);
                                                        request.Add(addr);
                                                        byte func = (byte)(int)ent.Values["FuncCode"];
                                                        request.Add(func);
                                                        // добавить начальный адрес
                                                        UInt16 startAddr = (UInt16)(int)ent.Values["Address"];
                                                        request.AddRange(Swap(BitConverter.GetBytes(startAddr)));
                                                        // добавить количество регистров
                                                        UInt16 regsCount = (UInt16)((int)ent.Values["DataCount"] * 2);
                                                        request.AddRange(Swap(BitConverter.GetBytes(regsCount)));
                                                        // добавить контрольную сумму
                                                        request.AddRange(BitConverter.GetBytes(
                                                            CRC(request.ToArray(), request.Count)));
                                                        break;
                                                }
                                                break;
                                            default:
                                                // другие точки
                                                break;
                                        }
                                        ent.SetRealProp("Quality", "GOOD");
                                        double seconds = 0;
                                        if (ent.Reals.ContainsKey("FactTime"))
                                        {
                                            DateTime facttime = DateTime.Parse(ent.Reals["FactTime"]);
                                            seconds = (DateTime.Now - facttime).TotalSeconds;
                                            ent.SetRealProp("Seconds", seconds.ToString("#.#"));
                                        }
                                        string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                        ent.SetRealProp("FactTime", time);
                                        Data.WriteRealVals(ent, fetchSQL);
                                        if (ptKind == PtKind.Table)
                                        {
                                            Data.UpdateClientFetchList(ClientID, ptname, ptdesc, "", "GOOD", time, fetchSQL);
                                            continue;
                                        }
                                        //worker.ReportProgress(0, "ОШИБКА;Опрос " + ptname + "...");
                                        #region ----------- Обмен с последовательным портом -----------------
                                        bool fetchError;
                                        List<byte> results;
                                        fetchError = FetchSerial(worker, cnannelConfig, timeout, request, out results);
                                        #endregion ----------------------------------------------------
                                        
                                        if (!fetchError)
                                        {
                                            DateTime LastSnap = DateTime.MinValue;
                                            switch (ptKind)
                                            {
                                                case PtKind.Group:
                                                    int station = Station;
                                                    DecodeFetchData(station, worker, ent, results, fetchSQL, dataSQL, seconds);
                                                    time = NowTime.ToString("yyyy-MM-dd HH:mm:ss");
                                                    //worker.ReportProgress(0, ptname + ";" + time);
                                                    Data.UpdateClientFetchList(ClientID, ptname, ptdesc, "", "GOOD", time, fetchSQL);

                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            time = NowTime.ToString("yyyy-MM-dd HH:mm:ss");
                                            Data.UpdateClientFetchList(ClientID, ptname, ptdesc, "Нет опроса", "BAD", time, fetchSQL);
                                            //worker.ReportProgress(0, ptname + ";нет связи с KR500");
                                        }
                                    }
                                }
                            }
                            if (worker.CancellationPending) return;
                            count++;
                        }
                    }
                }
            }
            #endregion
        }

        private static List<string> GetChilds(Entity ent, bool allvalues = false)
        {
            List<string> childs = new List<string>();
            string pttype = (string)ent.Values["PtType"];
            for (int i = 1; i <= 32; i++)
            {
                string key = "Child" + i;
                if (allvalues)
                {
                    if (ent.Values.ContainsKey(key))
                        childs.Add((string)ent.Values[key]);
                }
                else
                {
                    if (ent.Values.ContainsKey(key) && ent.Values[key].ToString().Length > 0)
                        childs.Add((string)ent.Values[key]);
                }
            }
            return childs;
        }

        private static void DecodeFetchData(int station, BackgroundWorker worker, Entity ent,
            List<byte> results, ServerSQL fetchSQL, ServerSQL dataSQL, double seconds)
        {
            switch (ent.Values["PtType"].ToString())
            {
                case "GA":
                    DecodeGA(results, fetchSQL, dataSQL, seconds, ent);
                    break;
            }
        }

        private static void DecodeGA(List<byte> results, ServerSQL fetchSQL,
            ServerSQL dataSQL, double seconds, Entity ent)
        {
            //     0        1        2        3        4         5        6   
            // "UINT16", "INT16", "UINT32", "INT32", "FLOAT", "INT32_INV", "FLOAT_INV"
            int datatype = (int)ent.Values["DataType"];
            List<string> childs = GetChilds(ent);
            int n = 3;
            foreach (string childname in childs)
            {
                Entity child = Data.GetEntity(childname, dataSQL);
                if (!child.Empty && (bool)child.Values["Actived"])
                {
                    child.Reals = Data.GetRealValues(childname, fetchSQL, dataSQL);
                    UInt16 smallbuff;
                    UInt32 largebuff;
                    switch (datatype)
                    {
                        case 0: // UINT16
                            smallbuff = BitConverter.ToUInt16(results.ToArray(), n);
                            child.SetRealProp("Raw", BitConverter.ToUInt16(
                                Swap(BitConverter.GetBytes(smallbuff)), 0).ToString());
                            child.SetRealProp("Quality", "GOOD");
                            n += 2;
                            break;
                        case 1: // INT16
                            smallbuff = BitConverter.ToUInt16(results.ToArray(), n);
                            child.SetRealProp("Raw", BitConverter.ToInt16(
                                Swap(BitConverter.GetBytes(smallbuff)), 0).ToString());
                            child.SetRealProp("Quality", "GOOD");
                            n += 2;
                            break;
                        case 2: // UINT32
                            largebuff = BitConverter.ToUInt32(results.ToArray(), n);
                            child.SetRealProp("Raw", BitConverter.ToUInt32(
                                Swap(BitConverter.GetBytes(largebuff)), 0).ToString());
                            child.SetRealProp("Quality", "GOOD");
                            n += 4;
                            break;
                        case 3: // INT32
                            largebuff = BitConverter.ToUInt32(results.ToArray(), n);
                            child.SetRealProp("Raw", BitConverter.ToInt32(
                                Swap(BitConverter.GetBytes(largebuff)), 0).ToString());
                            child.SetRealProp("Quality", "GOOD");
                            n += 4;
                            break;
                        case 4: // FLOAT
                            largebuff = BitConverter.ToUInt32(results.ToArray(), n);
                            child.SetRealProp("Raw", BitConverter.ToSingle(
                                Swap(BitConverter.GetBytes(largebuff)), 0).ToString());
                            child.SetRealProp("Quality", "GOOD");
                            n += 4;
                            break;
                        case 5: // INT32_INV
                            largebuff = BitConverter.ToUInt32(results.ToArray(), n);
                            child.SetRealProp("Raw", BitConverter.ToInt32(
                                Swap(BitConverter.GetBytes(BigSwap(largebuff))), 0).ToString());
                            child.SetRealProp("Quality", "GOOD");
                            n += 4;
                            break;
                        case 6: // FLOAT_INV
                            largebuff = BitConverter.ToUInt32(results.ToArray(), n);
                            child.SetRealProp("Raw", BitConverter.ToSingle(
                                Swap(BitConverter.GetBytes(BigSwap(largebuff))), 0).ToString());
                            child.SetRealProp("Quality", "GOOD");
                            n += 4;
                            break;
                        default: 
                            child.SetRealProp("Quality", "BAD");
                            break;
                    }
                    child.Reals = child.Plugin.Fetch(childname, child);
                    child.SetRealProp("Seconds", seconds.ToString("#.#"));
                    Data.WriteRealVals(child, fetchSQL);
                }
            }
        }

        private static bool FetchSerial(BackgroundWorker worker, string cnannelConfig,
            int timeout, List<byte> requestdata, out List<byte> buff)
        {
            buff = new List<byte>();
            bool result = true; // данные не получены
            if (cnannelConfig.StartsWith("COM"))
            {
                string[] args = cnannelConfig.Split(new char[] { ':' });
                if (args.Length == 2)
                {
                    string portname = args[0];
                    string portparams = args[1];
                    string[] pars = portparams.Split(new char[] { ',' });
                    int baud, databits;
                    if (pars.Length == 4 &&
                        int.TryParse(pars[0], out baud) &&
                        int.TryParse(pars[2], out databits) &&
                        (pars[3] == "1" || pars[3] == "1.5" || pars[3] == "2") &&
                        (pars[1] == "N" || pars[1] == "E" || pars[1] == "O" ||
                         pars[1] == "M" || pars[1] == "S"))
                    {
                        Parity parity = Parity.None;
                        switch (pars[1])
                        {
                            case "N": parity = Parity.None; break;
                            case "E": parity = Parity.Even; break;
                            case "O": parity = Parity.Odd; break;
                            case "M": parity = Parity.Mark; break;
                            case "S": parity = Parity.Space; break;
                        }
                        StopBits stops = StopBits.One;
                        switch (pars[3])
                        {
                            case "1": stops = StopBits.One; break;
                            case "1.5": stops = StopBits.OnePointFive; break;
                            case "2": stops = StopBits.Two; break;
                        }
                        // Создаём последовательный порт для отправки запроса данных контроллеру
                        using (SerialPort sp = new SerialPort(portname, baud, parity, databits, stops))
                        {
                            try
                            {
                                sp.WriteTimeout = timeout * 1000; // 5000;
                                sp.ReadTimeout = timeout * 1000; // 5000;
                                sp.Open();
                                if (sp.IsOpen)
                                {
                                    sp.DiscardInBuffer();
                                    sp.DiscardOutBuffer();
                                    sp.Write(requestdata.ToArray(), 0, requestdata.Count);
                                    int len = (requestdata[4] * 256 + requestdata[5]) * 2 + 5;
                                    while (true)
                                    {
                                        try
                                        {
                                            int onebyte = sp.ReadByte();
                                            if (onebyte < 0) break; // буфер приёма пуст, ошибка
                                            else
                                            {
                                                buff.Add((byte)onebyte);
                                                if (buff.Count == len)
                                                {
                                                    // конец приёма блока данных
                                                    break;
                                                }
                                            }
                                        }
                                        catch (TimeoutException)
                                        {
                                            // устройство не ответило вовремя, ошибка
                                            buff.Clear();
                                            break;
                                        }
                                    }
                                    if (buff.Count > 0)
                                    {
                                        ushort crcCalc = CRC(buff.ToArray(), buff.Count - 2);
                                        ushort crcBuff =
                                            BitConverter.ToUInt16(buff.ToArray(), buff.Count - 2);
                                        if (crcCalc == crcBuff)
                                        {
                                            // данные получены правильно
                                            result = false;
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Data.SendToSystemLog(Station, "Опрос Modbus", e.Message);
                            }
                        } // end of using
                    }
                }
            }
            return result;
        }

        private static void LoadFirst(BackgroundWorker worker, string ClientID, int channel,
            IDictionary<string, IPointPlugin> plugins)
        {
            // Загрузка Modbus точек из базы данных
            string station = Station.ToString();
            using (ServerSQL dataSQL = new ServerSQL(DatabaseFrom.Database, false)) // чтение
            {
                using (ServerSQL fetchSQL = new ServerSQL(DatabaseFrom.Fetchbase, false)) // чтение
                {
                    if (dataSQL.Connected && fetchSQL.Connected)
                    {
                        IDictionary<string, Entity> entities =
                            Data.LoadBase(plugins, dataSQL);
                        foreach (KeyValuePair<string, Entity> kvp in entities)
                        {
                            string ptname = kvp.Key;
                            try
                            {
                                Entity ent = kvp.Value;
                                if (!(bool)ent.Values["Actived"])
                                {
                                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    Data.UpdateClientFetchList(ClientID, ptname, (string)ent.Values["PtDesc"], "Снята с опроса", "BAD", time, fetchSQL);
                                }
                                int ptKind = int.Parse(ent.Values["PtKind"].ToString());
                                if (ptKind != PtKind.Group && ptKind != PtKind.Table) continue;
                                switch (ptKind)
                                {
                                    case PtKind.Group:
                                        int fetchtime = (int)ent.Values["FetchTime"];
                                        for (int i = 1; i <= 32; i++)
                                        {
                                            string childkey = "Child" + i;
                                            if (ent.Values.ContainsKey(childkey))
                                            {
                                                string childname = (string)ent.Values[childkey];
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
                                        }
                                        break;
                                }
                            }
                            catch (Exception e)
                            {
                                Data.SendToSystemLog(Station, "Загрузка опроса Modbus", e.Message);
                            }
                        }
                    }
                }
            }
        }

    }
}
