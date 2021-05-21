using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using IniFiles.Net;
using BaseServer;
using Points.Plugins;
using System.Threading;
using System.IO.Ports;

namespace FetchProtocols
{
    public static class FetchKontrastFunctions
    {
        private static string servname = "FetchKontrastService";
        private static DateTime TurnOnTime;
        private static int Station = 0;
        private static List<
            Tuple<string, BackgroundWorker>> workers =
            new List<Tuple<string, BackgroundWorker>>();
        private static IDictionary<string, IPointPlugin> plugins =
            new Dictionary<string, IPointPlugin>();

        public static void StartFetch(string[] args)
        {
            string ApplicationStartupPath = (args.Length >= 1) ? args[0] : String.Empty;
            string ConfigContent = (args.Length >= 2) ? args[1] : String.Empty;
            Station = (args.Length >= 3) ? int.Parse(args[2]) : 0;
            //
            MemIniFile mif = new MemIniFile(String.Empty);
            mif.FromString(ConfigContent);
            string section = "FetchServices";
            if (mif.SectionExists(section) &&
                mif.KeyExists(section, servname) &&
                mif.ReadBool(section, servname, false) &&
                mif.SectionExists(servname))
            {
                string[] keys = mif.ReadSectionKeys(servname);
                foreach (string key in keys)
                {
                    int channel; 
                    if (key.StartsWith("Channel") &&
                        int.TryParse(key.Substring(7), out channel))
                    {
                        string desc = "Канал " + channel + " сервера опроса";
                        Data.SendToSystemLog(Station, "Опрос KR500", desc + " загружен");
                        string ClientID = Data.ClientLogin(String.Empty, "F", Station, desc + " KR500");
                        BackgroundWorker worker = new BackgroundWorker();
                        workers.Add(new Tuple<string, BackgroundWorker>(
                            ClientID, worker));
                        worker.DoWork += worker_DoWork;
                        worker.ProgressChanged += worker_ProgressChanged;
                        worker.WorkerSupportsCancellation = true;
                        //worker.WorkerReportsProgress = true;
                        worker.RunWorkerAsync(new Tuple<string, string, string, int, string>(
                            ClientID, ApplicationStartupPath, ConfigContent, channel, key));
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
                Data.SendToSystemLog(Station, "Опрос KR500", "Сервер опроса выгружен");
                Data.ClientLogout(clientID);
                Data.ClearClientFetchList(clientID);
            }
        }

        private static string configString(string content, string channelkey)
        {
            MemIniFile mif = new MemIniFile(String.Empty);
            mif.FromString(content);
            if (mif.SectionExists(servname) && mif.KeyExists(servname, channelkey))
            {
                return mif.ReadString(servname, channelkey, String.Empty);
            }
            return String.Empty;
        }

        private static void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Tuple<string, string, string, int, string> args =
                (Tuple<string, string, string, int, string>)e.Argument;
            string ClientID = args.Item1;
            string ApplicationStartupPath = args.Item2;
            string ConfigContent = args.Item3;
            int channel = args.Item4;
            string channelName = args.Item5;
            string cnannelConfig = configString(ConfigContent, channelName).ToUpper();                                                                                  
            Data.RestoreSQLsettingsFromString(ConfigContent);
            BackgroundWorker worker = (BackgroundWorker)sender;
            TurnOnTime = DateTime.Now;
            // Загрузка плагина KR500 точек
            PointPlugin.LoadPlugin(ApplicationStartupPath + "\\Points.KR500.dll", ref plugins); 
            // Инициализация, создание баз, если их раньше не было
            Settings.CreateDataAndFetchBases();
            string desc = "Канал " + channel + " сервера опроса"; 
            // Заполнение списка для опроса
            LoadFirst(worker, ClientID, channel);

            bool _continue = true;
            while (_continue)
            {
                if (!Data.ImLive(ClientID, "F", Station, desc + " KR500"))
                {
                    ClientID = Data.ClientLogin(ClientID, "F", Station, desc + " KR500");
                }
                string[] command = Data.GetClientCommand(ClientID);
                //if (command[0].Equals("RELOAD"))
                //{
                //}
                //try
                //{
                    FetchBase(worker, ClientID, channel, cnannelConfig);
                    Thread.Sleep(50);
                //}
                //catch (Exception ex)
                //{
                //    Data.SendToSystemLog(0, "Опрос КР500", ex.Message);
                //}
                //worker.ReportProgress(0, DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
                _continue = !worker.CancellationPending;
            }
        }

        private static DateTime DateTimeFromController(BackgroundWorker worker)
        {
            return DateTime.Now;
        }

        private static void FetchBase(BackgroundWorker worker, string ClientID, int channel, string cnannelConfig)
        {
            IDictionary<string, Entity> fetchlist = new Dictionary<string, Entity>();
            #region Опрос
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
                        foreach (KeyValuePair<string, Entity> kvp in fetchlist)
                        {
                            if (worker.CancellationPending) return;
                            string ptname = kvp.Key;
                            Entity ent = kvp.Value;
                            string ptdesc = (string)ent.Values["PtDesc"];
                            int ptKind = int.Parse(ent.Values["PtKind"].ToString());
                            if (ptKind != PtKind.Group && ptKind != PtKind.Table) continue;
                            ent.Reals = Data.GetRealValues(ptname, fetchSQL, dataSQL);
                            //if (ent.Reals.Count == 0)
                            //{
                            //    worker.ReportProgress(0, "ОШИБКА;Нет связи с SQL");
                            //    continue;
                            //}
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
                                        byte node = (byte)(int)ent.Values["Node"];
                                        request.AddRange(new byte[] { node, 0xFE, 0x00, 0x00, 0x40 });
                                        byte datacount = 0;
                                        switch (ptKind)
                                        {
                                            case PtKind.Group:
                                                string pttype = (string)ent.Values["PtType"];
                                                switch (pttype)
                                                {
                                                    case "GR":
                                                        childs = GetChilds(ent);
                                                        request.AddRange(new byte[] { 0x01, 0x02, (byte)childs.Count });
                                                        datacount = 3;
                                                        foreach (string childname in childs)
                                                        {
                                                            Entity child = Data.GetEntity(childname, dataSQL);
                                                            if (!child.Empty)
                                                            {
                                                                byte kontur = (byte)(int)child.Values["Kontur"];
                                                                request.Add(kontur);
                                                                datacount += 1;
                                                            }
                                                        }
                                                        request[2] = datacount;
                                                        request.Add(0);
                                                        request[request.Count - 1] = CheckSum(request);
                                                        break;
                                                    case "GP":
                                                    case "GO":
                                                        childs = GetChilds(ent);
                                                        request.AddRange(new byte[] { 0x01, (byte)((pttype == "GP") ? 0x01 : 0x04), (byte)childs.Count });
                                                        datacount = 3;
                                                        foreach (string childname in childs)
                                                        {
                                                            Entity child = Data.GetEntity(childname, dataSQL);
                                                            if (!child.Empty)
                                                            {
                                                                ushort block = (ushort)(int)child.Values["Block"];
                                                                byte place = (byte)(int)child.Values["Place"];
                                                                request.AddRange(BitConverter.GetBytes(block));
                                                                request.Add(place);
                                                                datacount += 3;
                                                            }
                                                        }
                                                        request[2] = datacount;
                                                        request.Add(0);
                                                        request[request.Count - 1] = CheckSum(request);
                                                        break;
                                                    case "FD":
                                                        request.AddRange(new byte[] { 0x01, 0x04, 1 });
                                                        request.AddRange(
                                                            BitConverter.GetBytes((ushort)(int)ent.Values["Block"]));
                                                        request.Add((byte)(int)ent.Values["Place"]);
                                                        request[2] = 6;
                                                        request.Add(0);
                                                        request[request.Count - 1] = CheckSum(request);
                                                        break;
                                                }
                                                break;
                                            case PtKind.Table:
                                                bool stable = false;
                                                if (ent.Reals.ContainsKey("Stable"))
                                                    stable = bool.Parse(ent.Reals["Stable"]);
                                                //if (stable)
                                                //{
                                                //    if ((int)ent.Values["TableType"] > tabletype)
                                                //        continue;
                                                //}
                                                if (!ent.Reals.ContainsKey("DataCount"))
                                                    ent.SetRealProp("DataCount", "0");
                                                datacount = byte.Parse(ent.Reals["DataCount"]);
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
                                        ent.SetRealProp("FactTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                        Data.WriteRealVals(ent, fetchSQL);
                                        //worker.ReportProgress(0, "ОШИБКА;Опрос " + ptname + "...");
                                        #region ----------- Обмен с последовательным портом -----------------
                                        bool fetchError;
                                        //int func = (ptKind == PtKind.Group) ? 4 : 3;
                                        //fetchError = FetchTCP(worker, node, func, address, datacount, out regcount, out fetchvals);
                                        List<byte> results;
                                        fetchError = FetchSerial(worker, cnannelConfig, request, out results);
                                        #endregion ----------------------------------------------------
                                        
                                        if (!fetchError)
                                        {
                                            DateTime LastSnap = DateTime.MinValue;
                                            switch (ptKind)
                                            {
                                                case PtKind.Group:
                                                    int station = Station;
                                                    DecodeFetchData(station, worker, ent, results, fetchSQL, dataSQL, seconds);
                                                    string time = NowTime.ToString("yyyy-MM-dd HH:mm:ss");
                                                    //worker.ReportProgress(0, ptname + ";" + time);
                                                    Data.UpdateClientFetchList(ClientID, ptname, ptdesc, "", "GOOD", time, fetchSQL);

                                                    break;
                                                case PtKind.Table:
                                                    LastSnap = DateTime.Now; //DecodeTables(worker, ent, datacount, regcount, fetchvals,
                                                    //fetchSQL, dataSQL);
                                                    //worker.ReportProgress(0, ptname + ";" +
                                                    //    LastSnap.ToString("yyyy-MM-dd HH:mm:ss"));
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            string time = NowTime.ToString("yyyy-MM-dd HH:mm:ss");
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
            List<string> childs = GetChilds(ent);
            switch (ent.Values["PtType"].ToString())
            {
                case "GR":
                    DecodeGR(results, fetchSQL, dataSQL, seconds, childs);
                    break;
                case "GO":
                case "GP":
                    DecodeDO(results, fetchSQL, dataSQL, seconds, childs);
                    break;
                case "FD":
                    DecodeFD(ref ent, results, fetchSQL, dataSQL, ref childs);
                    break;
            }
        }

        private static void DecodeFD(ref Entity ent, List<byte> results, ServerSQL fetchSQL, ServerSQL dataSQL, ref List<string> childs)
        {
            if (results[7] * 8 + 9 != results.Count) return;
            int n;
            UInt32 values = (UInt32)BitConverter.ToSingle(results.ToArray(), 12);
            childs = GetChilds(ent, true);
            n = 0;
            foreach (string childname in childs)
            {
                bool boolval = (values & (UInt32)(1 << n)) > 0;
                Entity child = Data.GetEntity(childname, dataSQL);
                if (!child.Empty && (bool)child.Values["Actived"])
                {
                    child.Reals = Data.GetRealValues(childname, fetchSQL, dataSQL);
                    Single raw = (boolval) ? 1f : 0f;
                    child.SetRealProp("Raw", raw.ToString());
                    child.SetRealProp("Quality", "GOOD");
                    child.Reals = child.Plugin.Fetch(childname, child);
                    Data.WriteRealVals(child, fetchSQL);
                }
                n++;
            }
        }

        private static void DecodeDO(List<byte> results, ServerSQL fetchSQL, ServerSQL dataSQL,
            double seconds, List<string> childs)
        {
            if (results[7] * 8 + 9 != results.Count) return;
            int n = 8;
            foreach (string childname in childs)
            {
                Entity child = Data.GetEntity(childname, dataSQL);
                if (!child.Empty && (bool)child.Values["Actived"])
                {
                    child.Reals = Data.GetRealValues(childname, fetchSQL, dataSQL);
                    ushort block = BitConverter.ToUInt16(results.ToArray(), n); //8,9
                    byte place = results[n + 2]; //10
                    if ((int)child.Values["Block"] == (int)block &&
                        (int)child.Values["Place"] == (int)place)
                    {
                        byte typ = results[n + 3]; //11
                        float value = Single.NaN;
                        switch (child.Values["PtType"].ToString())
                        {
                            case "DP":
                            case "DI":
                                value = BitConverter.ToUInt32(results.ToArray(), n + 4); //12,13,14,15
                                break;
                            case "AP":
                            case "AI":
                                value = BitConverter.ToSingle(results.ToArray(), n + 4); //12,13,14,15
                                break;
                        }
                        if (!Single.IsNaN(value))
                        {
                            Single raw = value;
                            child.SetRealProp("Raw", raw.ToString());
                            child.SetRealProp("Quality", "GOOD");
                        }
                        else
                        {
                            child.SetRealProp("Raw", "0");
                            child.SetRealProp("Quality", "NAN");
                        }
                        child.Reals = child.Plugin.Fetch(childname, child);
                    }
                    child.SetRealProp("Seconds", seconds.ToString("#.#"));
                    Data.WriteRealVals(child, fetchSQL);
                }
                n += 8;
            }
        }

        private static void DecodeGR(List<byte> results, ServerSQL fetchSQL, ServerSQL dataSQL,
            double seconds, List<string> childs)
        {
            if (results[7] * 23 + 9 != results.Count) return;
            int n = 8;
            foreach (string childname in childs)
            {
                Entity child = Data.GetEntity(childname, dataSQL);
                if (!child.Empty && (bool)child.Values["Actived"])
                {
                    child.Reals = Data.GetRealValues(childname, fetchSQL, dataSQL);
                    byte kontur = results[n]; //8
                    if ((int)child.Values["Kontur"] == (int)kontur)
                    {
                        float hand = BitConverter.ToSingle(results.ToArray(), n + 1); //9,10,11,12
                        float sp = BitConverter.ToSingle(results.ToArray(), n + 5); //13,14,15,16
                        float pv = BitConverter.ToSingle(results.ToArray(), n + 9); //17,18,19,20
                        float dv = BitConverter.ToSingle(results.ToArray(), n + 13); //21,22,23,24
                        float op = BitConverter.ToSingle(results.ToArray(), n + 17); //25,26,27,28
                        ushort mode = BitConverter.ToUInt16(results.ToArray(), n + 21); //29,30
                        if (!Single.IsNaN(pv))
                        {
                            Single raw = pv;
                            child.SetRealProp("Raw", raw.ToString());
                            child.SetRealProp("Quality", "GOOD");
                        }
                        else
                        {
                            child.SetRealProp("Raw", "0");
                            child.SetRealProp("Quality", "NAN");
                        }
                        child.SetRealProp("HVRaw", hand.ToString());
                        child.SetRealProp("SPRaw", sp.ToString());
                        child.SetRealProp("DVRaw", dv.ToString());
                        child.SetRealProp("OPRaw", op.ToString());
                        int imode;
                        if (child.GetBit(mode, 14))
                            imode = 2; // КАСкадный режим
                        else
                        {
                            if (child.GetBit(mode, 4))
                                imode = 0; // РУЧной режим
                            else
                                imode = 1; // АВТоматический режим работы контура
                        }
                        child.SetRealProp("KonturMode", imode.ToString());
                        child.SetRealProp("KonturErrors", child.GetBit(mode, 1).ToString());
                        child.SetRealProp("KonturEU", child.GetBit(mode, 12).ToString());
                        child.Reals = child.Plugin.Fetch(childname, child);
                    }
                    child.SetRealProp("Seconds", seconds.ToString("#.#"));
                    Data.WriteRealVals(child, fetchSQL);
                }
                n += 23;
            }
        }

        private static byte CheckSum(List<byte> buff)
        {
            byte[] value = new byte[buff.Count - 1];
            buff.CopyTo(0, value, 0, value.Length);
            // Контрольная сумма Контрастов
            int Sum = 0;
            foreach (byte b in value) Sum += b;
            while (Sum >= 256) Sum = Sum % 256 + Sum / 256;
            return (byte)(0x100 - (Sum & 0xff));
        }

        private static bool FetchSerial(BackgroundWorker worker, string cnannelConfig,
            List<byte> requestdata, out List<byte> buff)
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
                                sp.WriteTimeout = 5000;
                                sp.ReadTimeout = 5000;
                                sp.Open();
                                try
                                {
                                    if (sp.IsOpen)
                                    {
                                        sp.DiscardInBuffer();
                                        sp.DiscardOutBuffer();
                                        sp.Write(requestdata.ToArray(), 0, requestdata.Count);
                                        ushort len = 0;
                                        while (true)
                                        {
                                            try
                                            {
                                                int onebyte = sp.ReadByte();
                                                if (onebyte < 0) break; // буфер приёма пуст, ошибка
                                                else
                                                {
                                                    buff.Add((byte)onebyte);
                                                    if (len == 0 && buff.Count == 5)
                                                    {
                                                        byte contr = buff[4]; // 1xxx xxxx при ответе
                                                        len = (ushort)(buff[2] + 256 * buff[3]);
                                                        if ((contr & 0x80) == 0 || len > 128)
                                                        {
                                                            // в старшем бите управления, при ответе, д.б. единица, ошибка
                                                            // ошибочная длина пакета, ошибка
                                                            buff.Clear();
                                                            break;
                                                        }
                                                    }
                                                    else
                                                        if (len > 0 && buff.Count == (len + 6))
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
                                        if (buff.Count > 0 && CheckSum(buff) == buff[buff.Count - 1])
                                        {
                                            // данные получены правильно
                                            result = false;
                                        }
                                    }
                                }
                                finally
                                {
                                    sp.Close();
                                }
                            }
                            catch (Exception e)
                            {
                                Data.SendToSystemLog(Station, "Опрос KR500", e.Message);
                            }
                        } // end of using
                    }
                }
            }
            return result;
        }

        private static void LoadFirst(BackgroundWorker worker, string ClientID, int channel)
        {
            // Загрузка KR500 точек из базы данных
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
                                    case PtKind.Table:
                                        ent.Reals = Data.GetRealValues(ptname, fetchSQL, dataSQL);
                                        ent.SetRealProp("DataCount", "0");
                                        ent.SetRealProp("Stable", false.ToString());
                                        ent.SetRealProp("LastSnap", DateTime.MinValue.ToString());
                                        Data.WriteRealVals(ent, fetchSQL);
                                        break;
                                }
                            }
                            catch (Exception e)
                            {
                                Data.SendToSystemLog(0, "Загрузка опроса КР500", e.Message);
                            }
                        }
                    }
                }
            }
        }

        private static void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //Записываем время в файл или делаем все, что хотим
            //switch (e.ProgressPercentage)
            //{
            //    case 0:
            //        string mess = (string)e.UserState;
            //        this.file.WriteLine(mess);
            //        this.file.Flush();
            //        break;
            //}
        }

    }
}
