using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Threading;
using System.Text;
using BaseServer;
using IniFiles.Net;
using Points.Plugins;

namespace FetchKR500
{
    public delegate void exitApp();

    public static class FetchKR500Functions
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
                            Data.SendToSystemLog(Station, "Опрос KR500", desc + " загружен");
                            string ClientID = Data.ClientLogin(String.Empty, "F", Station, desc + " KR500");
                            BackgroundWorker worker = new BackgroundWorker();
                            workers.Add(new Tuple<string,BackgroundWorker>(ClientID, worker));
                            worker.DoWork += worker_DoWork;
                            worker.WorkerSupportsCancellation = true;
                            worker.RunWorkerAsync(
                                new Tuple<int, string, string, exitApp, string, int>(
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
                Data.SendToSystemLog(Station, "Опрос KR500", "Сервер опроса выгружен");
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
                PointPlugin.LoadPlugin(ApplicationStartupPath + "\\Points.KR500.dll");
            // Инициализация, создание баз, если их раньше не было
            Settings.CreateDataAndFetchBases();
            string desc = "Канал " + channel + " сервера опроса"; 
            // Заполнение списка для опроса
            LoadFirst(worker, ClientID, channel, plugins);

            bool _exit = false;
            bool _continue = true;
            while (_continue)
            {
                if (!Data.ImLive(ClientID, "F", Station, desc + " KR500"))
                {
                    ClientID = Data.ClientLogin(ClientID, "F", station, desc + " KR500");
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
                        Data.SendToSystemLog(0, "Опрос КР500", ex.Message);
                    }
                }
                else
                    if (!_exit)
                    {
                        _exit = true;
                        Data.SendToSystemLog(Station,
                                "Опрос КР500", "Прекращена работа неавторизованного сервера опроса");
                        DoExitApp();
                    }
            }
        }

        private static DateTime DateTimeFromController(BackgroundWorker worker)
        {
            return DateTime.Now;
        }

        private static void FetchBase(BackgroundWorker worker, string ClientID, int channel,
            string channelConfig, int timeout, IDictionary<string, IPointPlugin> plugins)
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
                                else
                                    fetch = true;
                                if (fetch)
                                {
                                    if (ent.Values.ContainsKey("Channel") && ent.Values.ContainsKey("Node"))
                                    {
                                        switch (ptKind)
                                        {
                                            case PtKind.Group:
                                                string pttype = (string)ent.Values["PtType"];
                                                switch (pttype)
                                                {
                                                    case "GR":
                                                        SeekKonturCommands(worker, channelConfig, timeout, ent, fetchSQL, dataSQL);
                                                        FetchKonturData(worker, channelConfig, timeout, ent, fetchSQL, dataSQL, ClientID);
                                                        continue;
                                                    case "GP":
                                                        SeekGroupParamsCommands(worker, channelConfig, timeout, ent, fetchSQL, dataSQL);
                                                        FetchGroupParamsData(worker, channelConfig, timeout, ent, fetchSQL, dataSQL, ClientID);
                                                        continue;
                                                    case "GO":
                                                        FetchGroupOutsData(worker, channelConfig, timeout, ent, fetchSQL, dataSQL, ClientID);
                                                        continue;
                                                    case "FD":
                                                        FetchDecoder32Data(worker, channelConfig, timeout, ent, fetchSQL, dataSQL, ClientID);
                                                        continue;
                                                }
                                                break;
                                            default:
                                                // другие точки
                                                break;
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

        private static void SeekGroupParamsCommands(BackgroundWorker worker, string channelConfig,
            int timeout, Entity ent, ServerSQL fetchSQL, ServerSQL dataSQL)
        {
            if (ent.Values["PtType"].ToString().Equals("GP"))
            {
                List<string> childs = GetChilds(ent);
                foreach (string childname in childs)
                {
                    Entity child = Data.GetEntity(childname, dataSQL);
                    if (!child.Empty)
                    {
                        child.Reals = Data.GetRealValues(childname, fetchSQL, dataSQL);
                        if (child.Reals.ContainsKey("Command") &&
                            child.Reals["Command"].Trim().Length > 0)
                        {
                            child.Reals = Data.GetRealValues(childname, fetchSQL, dataSQL);
                            if (!child.Reals.ContainsKey("Raw")) child.Reals.Add("Raw", "0");
                            bool analog = child.Values["PtType"].ToString().Equals("AP");
                            if (analog)
                            {
                                SendAnalogParamValue(worker, channelConfig, timeout, child, fetchSQL);
                                continue;
                            }
                            else
                            {
                                SendDigitalParamValue(worker, channelConfig, timeout, child, fetchSQL);
                                continue;
                            }
                        }
                    }
                }
            }
        }

        private static void FetchDecoder32Data(BackgroundWorker worker, string channelConfig,
            int timeout, Entity ent, ServerSQL fetchSQL, ServerSQL dataSQL, string ClientID)
        {
            List<byte> request = new List<byte>();
            byte node = (byte)(int)ent.Values["Node"];
            request.AddRange(new byte[] { node, 0xFE, 6, 0x00, 0x40, 1, 4, 1 });
            ushort algoblock = (ushort)(int)ent.Values["Block"];
            request.AddRange(BitConverter.GetBytes(algoblock));
            byte place = (byte)(int)ent.Values["Place"];
            request.Add(place);
            request.Add(0);
            request[request.Count - 1] = CheckSum(request);
            FetchData(worker, channelConfig, timeout, ent, fetchSQL, dataSQL, ClientID, request);
        }

        private static void FetchGroupParamsData(BackgroundWorker worker, string channelConfig,
            int timeout, Entity ent, ServerSQL fetchSQL, ServerSQL dataSQL, string ClientID)
        {
            FetchGroupsData(1, worker, channelConfig, timeout, ent, fetchSQL, dataSQL, ClientID);
        }

        private static void FetchGroupOutsData(BackgroundWorker worker, string channelConfig,
            int timeout, Entity ent, ServerSQL fetchSQL, ServerSQL dataSQL, string ClientID)
        {
            FetchGroupsData(4, worker, channelConfig, timeout, ent, fetchSQL, dataSQL, ClientID);
        }

        private static void FetchGroupsData(byte code, BackgroundWorker worker, string channelConfig,
            int timeout, Entity ent, ServerSQL fetchSQL, ServerSQL dataSQL, string ClientID)
        {
            List<byte> request = new List<byte>();
            byte node = (byte)(int)ent.Values["Node"];
            List<string> childs = GetChilds(ent);
            byte count = (byte)childs.Count;
            if (count > 0 && count <= 14)
            {
                request.AddRange(new byte[] { node, 0xFE, (byte)(count * 3 + 3), 0x00, 0x40, 1, code, count });
                foreach (string childname in childs)
                {
                    Entity child = Data.GetEntity(childname, dataSQL);
                    if (!child.Empty)
                    {
                        ushort algoblock = (ushort)(int)child.Values["Block"];
                        request.AddRange(BitConverter.GetBytes(algoblock));
                        byte place = (byte)(int)child.Values["Place"];
                        request.Add(place);
                    }
                }
                request.Add(0);
                request[request.Count - 1] = CheckSum(request);
                FetchData(worker, channelConfig, timeout, ent, fetchSQL, dataSQL, ClientID, request);
            }
        }

        private static void SeekKonturCommands(BackgroundWorker worker, string channelConfig,
            int timeout, Entity ent, ServerSQL fetchSQL, ServerSQL dataSQL)
        {
            if (ent.Values["PtType"].ToString().Equals("GR"))
            {
                List<string> childs = GetChilds(ent);
                foreach (string childname in childs)
                {
                    Entity child = Data.GetEntity(childname, dataSQL);
                    if (!child.Empty)
                    {
                        child.Reals = Data.GetRealValues(childname, fetchSQL, dataSQL);
                        if (child.Reals.ContainsKey("Command") &&
                            child.Reals["Command"].Trim().Length > 0)
                            SendKonturCommand(worker, channelConfig, timeout, child, fetchSQL);
                    }
                }
            }
        }

        private static void FetchKonturData(BackgroundWorker worker, string channelConfig,
            int timeout, Entity ent, ServerSQL fetchSQL, ServerSQL dataSQL, string ClientID)
        {
            List<byte> request = new List<byte>();
            byte node = (byte)(int)ent.Values["Node"];
            List<string> childs = GetChilds(ent);
            byte count = (byte)childs.Count;
            if (count > 0 && count <= 4)
            {                
                request.AddRange(new byte[] { node, 0xFE, (byte)(count + 3), 0x00, 0x40, 1, 2, count });
                foreach (string childname in childs)
                {
                    Entity child = Data.GetEntity(childname, dataSQL);
                    if (!child.Empty)
                    {
                        byte kontur = (byte)(int)child.Values["Kontur"];
                        request.Add(kontur);
                    }
                }
                request.Add(0);
                request[request.Count - 1] = CheckSum(request);
                FetchData(worker, channelConfig, timeout, ent, fetchSQL, dataSQL, ClientID, request);
            }
        }

        private static void FetchData(BackgroundWorker worker, string channelConfig,
            int timeout, Entity ent, ServerSQL fetchSQL, ServerSQL dataSQL, string ClientID, 
            List<byte> request)
        {
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
            #region ----------- Обмен с последовательным портом -----------------
            bool fetchError;
            List<byte> results;
            fetchError = FetchSerial(worker, channelConfig, timeout, request, out results);
            #endregion ----------------------------------------------------
            string ptname = (string)ent.Values["PtName"];
            string ptdesc = (string)ent.Values["PtDesc"];
            if (!fetchError)
            {
                if (results.Count == 10)
                {
                    ushort nodeerror = BitConverter.ToUInt16(results.ToArray(), 7);
                    Data.WriteRealValue(ptname, "CommErrorCode", nodeerror.ToString(), fetchSQL);
                    Data.WriteRealValue(ptname, "Command", String.Empty, fetchSQL);
                }
                else
                    DecodeFetchData(Station, worker, ent, results, fetchSQL, dataSQL, seconds);
                Data.UpdateClientFetchList(ClientID, ptname, ptdesc, "", "GOOD", time, fetchSQL);
            }
            else
                Data.UpdateClientFetchList(ClientID, ptname, ptdesc, "Нет опроса", "BAD", time, fetchSQL);
        }

        private static void SendKonturCommand(BackgroundWorker worker, string channelConfig,
            int timeout, Entity ent, ServerSQL fetchSQL)
        {
            List<byte> request = new List<byte>();
            byte node = (byte)(int)ent.Values["Node"];
            request.AddRange(new byte[] { node, 0xFE, 0x00, 0x00, 0x00, 1, 2 });
            byte kontur = (byte)(int)ent.Values["Kontur"];
            request.Add(kontur);
            if (ent.Values["PtType"].ToString().Equals("CR") &&
                ent.Reals.ContainsKey("Command"))
            {
                string command = ent.Reals["Command"];
                byte mode;
                byte commcode = 0;
                float value;
                bool error = true;
                string repeatcommand = String.Empty;
                if (command.StartsWith("Mode=") &&
                    byte.TryParse(command.Substring(5), out mode))
                {
                    request[2] = 4;
                    commcode = (byte)((mode == 1) ? 4 : 5); // автоматическое управление; 5- ручное управление
                    request.Add(commcode);
                    error = false;
                }
                else
                    if (command.StartsWith("SP="))
                    {
                        request[2] = 8;
                        if (float.TryParse(command.Substring(3), out value))
                        {
                            commcode = 10; // установка сигнала ручного задания
                            request.Add(commcode);
                            request.AddRange(BitConverter.GetBytes(value));
                            error = false;
                        }
                    }
                    else
                        if (command.StartsWith("OP="))
                        {
                            request[2] = 8;
                            if (float.TryParse(command.Substring(3), out value))
                            {
                                commcode = 11; // установка выходного сигнала
                                request.Add(commcode);
                                request.AddRange(BitConverter.GetBytes(value));
                                error = false;
                            }
                        }
                request.Add(0);
                request[request.Count - 1] = CheckSum(request);
                if (!error)
                {
                    #region ----------- Обмен с последовательным портом -----------------
                    bool fetchError;
                    List<byte> results;
                    fetchError = FetchSerial(worker, channelConfig, timeout, request, out results);
                    #endregion ----------------------------------------------------
                    if (!fetchError && results.Count == 10)
                    {
                        ushort nodeerror = BitConverter.ToUInt16(results.ToArray(), 7);
                        string ptname = (string)ent.Values["PtName"];
                        Data.WriteRealValue(ptname, "CommErrorCode", nodeerror.ToString(), fetchSQL);
                        Data.WriteRealValue(ptname, "Command", repeatcommand, fetchSQL);
                    }
                }
            }
        }

        private static void SendDigitalParamValue(BackgroundWorker worker, string channelConfig,
            int timeout, Entity ent, ServerSQL fetchSQL)
        {
            List<byte> request = new List<byte>();
            byte node = (byte)(int)ent.Values["Node"];
            request.AddRange(new byte[] { node, 0xFE, 0x09, 0x00, 0x00, 1, 1 });
            ushort algoblock = (ushort)(int)ent.Values["Block"];
            request.AddRange(BitConverter.GetBytes(algoblock));
            byte place = (byte)(int)ent.Values["Place"];
            request.Add(place);
            float value;
            if (ent.Values["PtType"].ToString().Equals("DP") &&
                ent.Reals.ContainsKey("Command") &&
                ent.Reals.ContainsKey("Raw") &&
                float.TryParse(ent.Reals["Raw"], out value))
            {
                string command = ent.Reals["Command"];
                #region для кнопок
                string repeatcommand = String.Empty;
                bool invert = (bool)ent.Values["Invert"];
                switch (command)
                {
                    case "CLICK":
                        value = (invert) ? 0 : 1;
                        repeatcommand = "NEXT";
                        break;
                    case "NEXT":
                        value = (invert) ? 1 : 0;
                        repeatcommand = String.Empty;
                        break;
                    case "ON":
                        value = (invert) ? 0 : 1;
                        repeatcommand = String.Empty;
                        break;
                    case "OFF":
                        value = (invert) ? 1 : 0;
                        repeatcommand = String.Empty;
                        break;
                    case "REVERSE":
                        value = (value > 0) ? 0 : 1;
                        repeatcommand = String.Empty;
                        break;
                }
                #endregion
                request.AddRange(BitConverter.GetBytes(value));
                request.Add(0);
                request[request.Count - 1] = CheckSum(request);
                #region ----------- Обмен с последовательным портом -----------------
                bool fetchError;
                List<byte> results;
                fetchError = FetchSerial(worker, channelConfig, timeout, request, out results);
                #endregion ----------------------------------------------------
                if (!fetchError && results.Count == 10)
                {
                    ushort nodeerror = BitConverter.ToUInt16(results.ToArray(), 7);
                    string ptname = (string)ent.Values["PtName"];
                    Data.WriteRealValue(ptname, "CommErrorCode", nodeerror.ToString(), fetchSQL);
                    Data.WriteRealValue(ptname, "Command", repeatcommand, fetchSQL);
                }
            }
        }

        private static void SendAnalogParamValue(BackgroundWorker worker, string channelConfig,
            int timeout, Entity ent, ServerSQL fetchSQL)
        {
            List<byte> request = new List<byte>();
            byte node = (byte)(int)ent.Values["Node"];
            request.AddRange(new byte[] { node, 0xFE, 0x09, 0x00, 0x00, 1, 1 });
            ushort algoblock = (ushort)(int)ent.Values["Block"];
            request.AddRange(BitConverter.GetBytes(algoblock));
            byte place = (byte)(int)ent.Values["Place"];
            request.Add(place);
            float value;
            if (ent.Values["PtType"].ToString().Equals("AP") &&
                ent.Reals.ContainsKey("Command") &&
                float.TryParse(ent.Reals["Command"], out value))
            {
                request.AddRange(BitConverter.GetBytes(value));
                request.Add(0);
                request[request.Count - 1] = CheckSum(request);
                #region ----------- Обмен с последовательным портом -----------------
                bool fetchError;
                List<byte> results;
                fetchError = FetchSerial(worker, channelConfig, timeout, request, out results);
                #endregion ----------------------------------------------------
                if (!fetchError && results.Count == 10)
                {
                    ushort nodeerror = BitConverter.ToUInt16(results.ToArray(), 7);
                    string ptname = (string)ent.Values["PtName"];
                    Data.WriteRealValue(ptname, "CommErrorCode", nodeerror.ToString(), fetchSQL);
                    Data.WriteRealValue(ptname, "Command", String.Empty, fetchSQL);
                }                         
            }
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
                case "GR":
                    DecodeGR(results, fetchSQL, dataSQL, seconds, ent);
                    break;
                case "GO":
                case "GP":
                    DecodeDO(results, fetchSQL, dataSQL, seconds, ent);
                    break;
                case "FD":
                    DecodeFD(results, fetchSQL, dataSQL, seconds, ent);
                    break;
            }
        }

        private static void DecodeFD(List<byte> results,
            ServerSQL fetchSQL, ServerSQL dataSQL, double seconds, Entity ent)
        {
            if (results[7] * 8 + 9 != results.Count) return;

            WriteFetchAnswer(ent, results, fetchSQL);
            
            List<string> childs = GetChilds(ent);

            int n;
            UInt32 values = (UInt32)BitConverter.ToSingle(results.ToArray(), 12);

            ent.SetRealProp("Raw", values.ToString());
            ent.SetRealProp("Seconds", seconds.ToString("#.#"));
            Data.WriteRealVals(ent, fetchSQL);

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
                    child.SetRealProp("Seconds", seconds.ToString("#.#"));
                    Data.WriteRealVals(child, fetchSQL);
                }
                n++;
            }
        }

        private static void WriteFetchAnswer(Entity ent, List<byte> results, ServerSQL dataSQL)
        {
            string ptname = (string)ent.Values["PtName"];
            StringBuilder sb = new StringBuilder(BitConverter.ToString(results.ToArray()));

            int count = 0;
            while (sb.Length > 0)
            {
                string sdata = (sb.Length > 50) ? sb.ToString().Substring(0, 50) : sb.ToString();
                Data.WriteRealValue(ptname, "FetchAnswer" + count, sdata, dataSQL);
                if (sb.Length > 50)
                    sb.Remove(0, 50);
                else
                    sb.Remove(0, sb.Length);
                count++;
            }
            Data.WriteRealValue(ptname, "FetchAnswerCount", count.ToString(), dataSQL);
        }

        private static void DecodeDO(List<byte> results, ServerSQL fetchSQL, ServerSQL dataSQL,
            double seconds, Entity ent)
        {
            if (results[7] * 8 + 9 != results.Count) return;
            List<string> childs = GetChilds(ent);

            WriteFetchAnswer(ent, results, fetchSQL);

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
                        bool ro = ((results[n + 3] & 0x40) == 0); // только для чтения
                        child.SetRealProp("RawReadOnly", ro.ToString());
                        bool lowtime = ((results[n + 3] & 0x80) == 0); // мл. диапазон вр.кф
                        child.SetRealProp("RawLowTime", lowtime.ToString());
                        byte typ = (byte)((results[n + 3] >> 1) & 0x0f); //11
                        child.SetRealProp("RawDataType", typ.ToString());
                        byte errcode = (results[n + 3] == 0xff) ? results[n + 4] : (byte)0;
                        child.SetRealProp("RawErrorCode", errcode.ToString());
                        float value = Single.NaN;
                        if (errcode == 0)
                        {
                            switch (child.Values["PtType"].ToString())
                            {
                                case "DP":
                                case "DI":
                                    value = ((results[n + 4] & 0x01) > 0) ? 1 : 0; // 12   13,14,15 - мусор
                                    break;
                                case "AP":
                                case "AI":
                                    value = BitConverter.ToSingle(results.ToArray(), n + 4); //12,13,14,15
                                    break;
                            }
                        }
                        if (!Single.IsNaN(value))
                        {
                            if (errcode == 0)
                            {
                                Single raw = value;
                                child.SetRealProp("Raw", raw.ToString());
                                child.SetRealProp("Quality", "GOOD");
                            }
                            else
                                child.SetRealProp("Quality", "BAD");
                        }
                        else
                            child.SetRealProp("Quality", "NAN");
                        child.Reals = child.Plugin.Fetch(childname, child);
                    }
                    child.SetRealProp("Seconds", seconds.ToString("#.#"));
                    Data.WriteRealVals(child, fetchSQL);
                }
                n += 8;
            }
        }

        private static void DecodeGR(List<byte> results, ServerSQL fetchSQL, ServerSQL dataSQL,
            double seconds, Entity ent)
        {
            if (results[7] * 23 + 9 != results.Count) return;

            WriteFetchAnswer(ent, results, fetchSQL);
            
            List<string> childs = GetChilds(ent);

            int n = 8;
            foreach (string childname in childs)
            {
                Entity child = Data.GetEntity(childname, dataSQL);
                if (!child.Empty && (bool)child.Values["Actived"])
                {
                    child.Reals = Data.GetRealValues(childname, fetchSQL, dataSQL);
                    byte kontur = results[n]; //8
                    byte confkontur = (byte)(int)child.Values["Kontur"];
                    if (confkontur == kontur)
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
                            child.SetRealProp("Quality", "NAN");
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
                        child.SetRealProp("RawErrorCode", "0");
                        child.Reals = child.Plugin.Fetch(childname, child);
                    }
                    else
                        if ((confkontur ^ 0xff) + 1 == kontur) // ошибка обмена контура
                        {
                            byte errcode = results[n + 1];
                            if (errcode == 0) errcode = 24;
                            child.SetRealProp("RawErrorCode", errcode.ToString());
                            child.SetRealProp("Quality", "BAD");
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

        private static string LastError = String.Empty;

        private static bool FetchSerial(BackgroundWorker worker, string cnannelConfig,
             int timeout, List<byte> requestdata,out List<byte> buff)
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
                                sp.WriteTimeout = timeout * 1000;
                                sp.ReadTimeout = timeout * 1000;
                                sp.Open();
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
                                                        LastError = "Ошибочная длина пакета";
                                                        Data.SendToSystemLog(Station, "Опрос KR500", LastError);
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
                                            LastError = "Устройство не ответило вовремя";
                                            Data.SendToSystemLog(Station, "Опрос KR500", LastError);
                                            buff.Clear();
                                            break;
                                        }
                                    }
                                    if (buff.Count > 0 && CheckSum(buff) == buff[buff.Count - 1])
                                    {
                                        // данные получены правильно
                                        LastError = String.Empty;
                                        result = false;
                                    }
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

        private static void LoadFirst(BackgroundWorker worker, string ClientID, int channel,
            IDictionary<string, IPointPlugin> plugins)
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
                                }
                            }
                            catch (Exception e)
                            {
                                Data.SendToSystemLog(Station, "Загрузка опроса КР500", e.Message);
                            }
                        }
                    }
                }
            }
        }

    }
}
