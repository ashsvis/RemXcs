using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using IniFiles.Net;
using BaseServer;
using Points.Plugins;
using System.Threading;
using System.IO;
using System.IO.Ports;

namespace FetchVirtual
{
    public delegate void exitApp();

    public static class FetchVirtualFunctions
    {
        private static int Station = 0;
        private static string ClientID = String.Empty;
        private static BackgroundWorker worker = new BackgroundWorker();

        public static void StartFetch(Tuple<int, string, exitApp> args)
        {
            Station = args.Item1;
            if (!worker.IsBusy)
            {
                worker.DoWork += worker_DoWork;
                worker.WorkerSupportsCancellation = true;
                worker.RunWorkerAsync(new Tuple<string, exitApp>(args.Item2, args.Item3));
            }
        }

        public static void StopFetch()
        {
            //Код, выполняемый при остановке процесса
            if (worker.IsBusy) worker.CancelAsync();
            Data.SendToSystemLog(Station, "Опрос виртуальных", "Сервер опроса выгружен");
            Data.ClientLogout(ClientID);
            Data.ClearClientFetchList(ClientID);
        }

        private static void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            Tuple<string, exitApp> args = (Tuple<string, exitApp>)e.Argument;

            MemIniFile mif = new MemIniFile(String.Empty);
            mif.FromString(args.Item1);
            string section = "Application";
            int station = mif.ReadInteger(section, "Station", 1);
            string ApplicationStartupPath = mif.ReadString(section, "StartupPath", ".");
            bool Registered = mif.ReadBool(section, "Registered", false);
            bool Bonus = mif.ReadBool(section, "Bonus", false);
            exitApp DoExitApp = args.Item2;

            Data.RestoreSQLsettingsFromString(args.Item1);
            DateTime TurnOnTime = DateTime.Now;
            // Загрузка плагина виртуальных точек
            IDictionary<string, IPointPlugin> plugins =
                PointPlugin.LoadPlugin(ApplicationStartupPath + "\\Points.Virtuals.dll");
            // Инициализация, создание баз, если их раньше не было
            Settings.CreateDataAndFetchBases();
            // Загрузка виртуальных точек из базы данных
            Data.LoadBase(plugins);
            string desc = "Сервер опроса виртуальных значений";
            Data.SendToSystemLog(station, "Опрос виртуальных", "Сервер опроса загружен");
            ClientID = Data.ClientLogin(ClientID, "F", Station, desc);
            // Заполнение списка для опроса
            LoadFirst(worker);
            bool _exit = false;
            bool _continue = true;
            while (_continue)
            {
                _continue = !worker.CancellationPending;
                if (!Data.ImLive(ClientID, "F", Station, desc))
                {
                    ClientID = Data.ClientLogin(ClientID, "F", Station, desc);
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
                        FetchBase(worker, plugins);
                        Thread.Sleep(50);
                    }
                    catch (Exception ex)
                    {
                        Data.SendToSystemLog(0, "Опрос виртуальных", ex.Message);
                    }
                }
                else
                if (!_exit)
                {
                    _exit = true;
                    Data.SendToSystemLog(station,
                            "Опрос виртуальных", "Прекращена работа неавторизованного сервера опроса");
                    DoExitApp();
                }
            }
        }

        private static void LoadFirst(BackgroundWorker worker)
        {
            // Загрузка виртуальных точек из базы данных
            string station = Station.ToString();
            using (ServerSQL dataSQL = new ServerSQL(DatabaseFrom.Database, false)) // чтение
            {
                using (ServerSQL fetchSQL = new ServerSQL(DatabaseFrom.Fetchbase, false)) // чтение
                {
                    if (dataSQL.Connected && fetchSQL.Connected)
                    {
                        foreach (KeyValuePair<string, Entity> kvp in Data.Entities())
                        {
                            string ptname = kvp.Key;
                            try
                            {
                                Entity ent = kvp.Value;
                                if (!(bool)ent.Values["Actived"])
                                {
                                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    Data.UpdateClientFetchList(ClientID, ptname,
                                        (string)ent.Values["PtDesc"], "Снята с опроса", "BAD",
                                        time, fetchSQL);
                                }
                                int ptKind = int.Parse(ent.Values["PtKind"].ToString());
                                int fetchtime;
                                switch (ptKind)
                                {
                                    case PtKind.Valve:
                                        if ((bool)ent.Values["Actived"])
                                        {
                                            ent.Reals = Data.GetRealValues((string)ent.Values["PtName"],
                                                fetchSQL, dataSQL);
                                            ent.SetRealProp("Station", station);
                                            ent.SetRealProp("Alarms", String.Empty);
                                            ent.SetRealProp("LostAlarms", String.Empty);
                                            ent.SetRealProp("QuitAlarms", true.ToString());
                                            Data.WriteRealVals(ent, fetchSQL);
                                        }

                                        break;
                                    case PtKind.Group:
                                        fetchtime = (int)ent.Values["FetchTime"];
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
                                                        child.SetRealProp("GroupFetchTime",
                                                            fetchtime.ToString());
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
                                Data.SendToSystemLog(0, "Загрузка виртуальных", e.Message);
                            }
                        }
                    }
                }
            }
        }

        private static void FetchBase(BackgroundWorker worker, IDictionary<string, IPointPlugin> plugins)
        {
            IDictionary<string, Entity> fetchlist = new Dictionary<string, Entity>();
            #region Опрос
            using (ServerSQL dataSQL = new ServerSQL(DatabaseFrom.Database, false)) // чтение
            {
                using (ServerSQL fetchSQL = new ServerSQL(DatabaseFrom.Fetchbase, false)) // чтение
                {
                    if (dataSQL.Connected && fetchSQL.Connected)
                    {
                        Data.LoadBase(plugins, dataSQL);
                        foreach (KeyValuePair<string, Entity> kvp in Data.Entities())
                        {
                            if (worker.CancellationPending) return;
                            Entity ent = kvp.Value;
                            if (!ent.Empty)
                            {
                                int ptKind = int.Parse(ent.Values["PtKind"].ToString());
                                fetchlist.Add(kvp.Key, kvp.Value);
                            }
                        }
                        #region определение типа таблицы tabletype
                        DateTime NowTime = DateTime.Now;
                        #endregion
                        int count = 1;
                        foreach (KeyValuePair<string, Entity> kvp in fetchlist)
                        {
                            if (worker.CancellationPending) return;
                            string ptname = kvp.Key;
                            Entity ent = kvp.Value;
                            string ptdesc = (string)ent.Values["PtDesc"];
                            int ptKind = int.Parse(ent.Values["PtKind"].ToString());
                            ent.Reals = Data.GetRealValues(ptname, fetchSQL, dataSQL);
                            if (ent.Reals.Count == 0)
                            {
                                //    worker.ReportProgress(0, "ОШИБКА;Нет связи с SQL");
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
                                    string value = String.Empty;
                                    string state = "UNCERTAIN";
                                    switch (ptKind)
                                    {
                                        case PtKind.Digital:
                                        case PtKind.Analog:
                                            #region расчёт алармов
                                            if (ent.Reals != null)
                                            {
                                                if (!ent.Reals.ContainsKey("Raw"))
                                                    ent.Reals.Add("Raw", "0");
                                                if (ent.Reals.ContainsKey("Command") &&
                                                    ent.Reals["Command"].Trim().Length > 0)
                                                {
                                                    ent.Reals["Raw"] = ent.Reals["Command"];
                                                    ent.Reals["Command"] = String.Empty;
                                                }
                                                ent.Reals = ent.Plugin.Fetch(ptname, ent);
                                                if (ent.Reals.ContainsKey("PVText"))
                                                    value = ent.Reals["PVText"];
                                                state = "GOOD";
                                            }
                                            else
                                                state = "BAD";
                                            #endregion
                                            break;
                                        case PtKind.Valve:
                                            DecodeValve(ptname, ref ent, ref value, ref state);
                                            break;
                                        case PtKind.Group:

                                            break;
                                        default:
                                            // другие точки
                                            break;
                                    }
                                    ent.SetRealProp("Quality", state);
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

                                    Data.UpdateClientFetchList(ClientID, ptname, ptdesc,
                                        value, state, time, fetchSQL);

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

        private static void DecodeValve(string ptname, ref Entity ent, ref string value, ref string state)
        {
            string[] statenames = new string[5] { "StatOFF", "StatON", "StatALM", "CommOFF", "CommON" };
            ushort states = 0;
            int entindex = 0;
            foreach (string statename in statenames)
            {
                string childname = ent.Values[statename].ToString().Trim();
                bool bitval;
                if (childname.Length > 0)
                {
                    IDictionary<string, string> Reals =
                        Data.GetRealValues(childname);
                    if (Reals.ContainsKey("PV") &&
                        bool.TryParse(Reals["PV"], out bitval))
                    {
                        ent.SetBit(ref states, entindex, bitval);
                        ent.SetRealProp(statename + "State",
                            bitval.ToString());
                    }
                }
                else
                    ent.SetRealProp(statename + "State", false.ToString());
                entindex++;
            }
            int lastpv;
            if (ent.Reals.ContainsKey("PV"))
                lastpv = int.Parse(ent.Reals["PV"]);
            else
            {
                lastpv = 0;
                ent.SetRealProp("PV", lastpv.ToString());
            }
            ent.SetRealProp("Raw", states.ToString());
            int pv = states;
            ent.SetRealProp("PV", pv.ToString());
            string[] textstates = new string[4] { "ХОД","ЗАКРЫТО","ОТКРЫТО","ОШИБКА" };
            string textstate = textstates[states & 0x03];
            if (ent.GetBit(states, 2))
                textstate = "!" + textstate + "!";
            ent.SetRealProp("PVText", textstate);
            value = textstate;
            state = "GOOD";
            #region расчёт алармов
            ent.Reals = ent.Plugin.Fetch(ptname, ent);
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

    }
}
