using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Points.Plugins;
using System.IO;
using System.Drawing.Printing;
using System.Management;
using System.Threading;
using IniFiles.Net;

namespace BaseServer
{
    #region Класс места группы GroupItem

    public struct GroupItem
    {
        public int Group;
        public int Place;
        public string Name;
        public string Param;
        public string Caption;

        public GroupItem(int group, int place)
        {
            this.Group = group;
            this.Place = place;
            this.Name = String.Empty;
            this.Param = String.Empty;
            this.Caption = String.Empty;
        }
    }
    #endregion

    public class SupportUpdate : ILogUpdate
    {
        public IDictionary<string, double> GetTableValues(Entity point)
        {
            IDictionary<string, double> result = new Dictionary<string, double>();
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database))
            {
                if (mySQL.Connected && !point.Empty)
                {
                    string ptname = (string)point.Values["PtName"] + ".PV";
                    DateTime dt = DateTime.Now;
                    result.Add("DAY", mySQL.GetDailyData(ptname, dt));
                    result.Add("LASTDAY", mySQL.GetDailyData(ptname, dt.AddDays(-1)));
                    result.Add("MONTH", mySQL.GetMonthData(ptname, dt));
                    result.Add("LASTMONTH", mySQL.GetMonthData(ptname, dt.AddMonths(-1)));
                }
            }
            return result;
        }

        public void UpdateAlarmValue(Entity point)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // запись
            {
                if (mySQL.Connected && !point.Empty)
                {
                    string ptname = (string)point.Values["PtName"];
                    string value = (point.Reals.ContainsKey("PVText")) ?
                        point.Reals["PVText"] : "------";
                    int station = (point.Reals.ContainsKey("Station")) ?
                        int.Parse(point.Reals["Station"]) : 0;
                    mySQL.UpdateAlarmValue(station, ptname, value);
                }
            }
        }

        public void AddAlarm(string type, Entity point, string message)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // запись
            {
                if (mySQL.Connected && !point.Empty)
                {
                    string ptname = (string)point.Values["PtName"];
                    string param = "PV" + type + "TP";
                    string value = (point.Reals.ContainsKey("PVText")) ? point.Reals["PVText"] : "------";
                    string eudesc = (point.Values.ContainsKey("EUDesc")) ?
                        (string)point.Values["EUDesc"] : String.Empty;
                    string setpoint = String.Empty;
                    switch (param)
                    {
                        case "PVHHTP":
                        case "PVHITP":
                        case "PVLOTP":
                        case "PVLLTP":
                            setpoint = Data.FloatEx((string)point.Values[param]).ToString();
                            if (String.IsNullOrWhiteSpace(message))
                                message = point.Plugin.GetPropDesc(param);
                            break;
                        case "PVHETP":
                            setpoint = Data.FloatEx((string)point.Values["PVEUHi"]).ToString();
                            if (String.IsNullOrWhiteSpace(message))
                                message = point.Plugin.GetPropDesc("PVEUHi");
                            break;
                        case "PVLETP":
                            setpoint = Data.FloatEx((string)point.Values["PVEULo"]).ToString();
                            if (String.IsNullOrWhiteSpace(message))
                                message = point.Plugin.GetPropDesc("PVEULo");
                            break;
                        case "PVHATP":
                            setpoint = (string)point.Values["TextUp"];
                            if (String.IsNullOrWhiteSpace(message))
                                message = point.Plugin.GetPropDesc("AlarmUp");
                            break;
                        case "PVLATP":
                            setpoint = (string)point.Values["TextDown"];
                            if (String.IsNullOrWhiteSpace(message))
                                message = point.Plugin.GetPropDesc("AlarmDown");
                            break;
                        case "PVWATP":
                            setpoint = "Ошибка";
                            if (String.IsNullOrWhiteSpace(message))
                                message = "Оба конечника замкнуты";
                            break;
                        case "PVEMTP":
                            setpoint = "Авария";
                            if (String.IsNullOrWhiteSpace(message))
                                message = "Авария устройства";
                            break;
                    }
                    string descriptor = (string)point.Values["PtDesc"];
                    int station = (point.Reals.ContainsKey("Station")) ?
                        int.Parse(point.Reals["Station"]) : 0;
                    mySQL.UpdateAlarm(type, station, ptname, param, value, setpoint + " " + eudesc,
                        message, descriptor);
                }
            }
        }

        public void RemoveAlarm(string type, Entity point)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // запись
            {
                if (mySQL.Connected && !point.Empty)
                {
                    string ptname = (string)point.Values["PtName"];
                    string param = "PV" + type + "TP";
                    mySQL.RemoveAlarm(type, ptname, param);
                }
            }
        }

        public void RemoveAlarms(Entity point)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // запись
            {
                if (mySQL.Connected && !point.Empty)
                {
                    string ptname = (string)point.Values["PtName"];
                    mySQL.RemoveAlarms(ptname);
                }
            }
        }

        public void AddSwitch(string type, Entity point)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // запись
            {
                if (mySQL.Connected && !point.Empty)
                {
                    string ptname = (string)point.Values["PtName"];
                    string param = "PV" + type + "TP";
                    string descriptor = (string)point.Values["PtDesc"];
                    string value = (point.Reals.ContainsKey("PVText")) ? point.Reals["PVText"] : "------";
                    int station = (point.Reals.ContainsKey("Station")) ?
                        int.Parse(point.Reals["Station"]) : 0;
                    mySQL.UpdateSwitch(station, ptname, param, value, descriptor);
                }
            }
        }

        public void RemoveSwitch(string type, Entity point)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // запись
            {
                if (mySQL.Connected && !point.Empty)
                {
                    string ptname = (string)point.Values["PtName"];
                    string param = "PV" + type + "TP";
                    mySQL.RemoveSwitch(ptname, param);
                }
            }
        }

        public void RemoveSwitchs(Entity point)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // запись
            {
                if (mySQL.Connected && !point.Empty)
                {
                    string ptname = (string)point.Values["PtName"];
                    mySQL.RemoveSwitchs(ptname);
                }
            }
        }

        public void SendToAlarmLog(string type, Entity point, string message)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
            {
                if (mySQL.Connected && !point.Empty)
                {
                    string ptname = (string)point.Values["PtName"];
                    string param = "PV" + type + "TP";
                    string value = (point.Reals.ContainsKey("PVText")) ? point.Reals["PVText"] : "------";
                    string eudesc = (point.Values.ContainsKey("EUDesc")) ? 
                        (string)point.Values["EUDesc"] : String.Empty;
                    string setpoint = String.Empty;
                    switch (param)
                    {
                        case "PVHHTP": case "PVHITP": case "PVLOTP": case "PVLLTP":
                            setpoint = Data.FloatEx((string)point.Values[param]).ToString();
                            if (String.IsNullOrWhiteSpace(message))
                                message = point.Plugin.GetPropDesc(param);
                            break;
                        case "PVHETP":
                            setpoint = Data.FloatEx((string)point.Values["PVEUHi"]).ToString();
                            if (String.IsNullOrWhiteSpace(message))
                                message = point.Plugin.GetPropDesc("PVEUHi");
                            break;
                        case "PVLETP":
                            setpoint = Data.FloatEx((string)point.Values["PVEULo"]).ToString();
                            if (String.IsNullOrWhiteSpace(message))
                                message = point.Plugin.GetPropDesc("PVEULo");
                            break;
                        case "PVHATP":
                            setpoint = (string)point.Values["TextUp"];
                            if (String.IsNullOrWhiteSpace(message))
                                message = point.Plugin.GetPropDesc("AlarmUp");
                            break;
                        case "PVLATP":
                            setpoint = (string)point.Values["TextDown"];
                            if (String.IsNullOrWhiteSpace(message))
                                message = point.Plugin.GetPropDesc("AlarmDown");
                            break;
                        case "PVWATP":
                            setpoint = "Ошибка";
                            if (String.IsNullOrWhiteSpace(message))
                                message = "Оба конечника замкнуты";
                            break;
                        case "PVEMTP":
                            setpoint = "Авария";
                            if (String.IsNullOrWhiteSpace(message))
                                message = "Авария устройства";
                            break;
                    }
                    string descriptor = (string)point.Values["PtDesc"];
                    int station = (point.Reals.ContainsKey("Station")) ?
                        int.Parse(point.Reals["Station"]) : 0;
                    mySQL.AddToAlarmLog(station, ptname, param, value, setpoint + " " + eudesc,
                        message, descriptor);
                }
            }
        }

        public void SendToSwitchLog(string type, Entity point, bool oldvalue, bool newvalue)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
            {
                if (mySQL.Connected && !point.Empty)
                {
                    string ptname = (string)point.Values["PtName"];
                    string param = "PV" + type + "TP";
                    string descriptor = (string)point.Values["PtDesc"];
                    string newtext = (point.Reals.ContainsKey("PVText")) ? point.Reals["PVText"] : "------";
                    string oldtext = (newtext.Equals("------")) ? "------" :
                        ((newtext.Equals((string)point.Values["TextUp"])) ?
                            (string)point.Values["TextDown"] : (string)point.Values["TextUp"]);
                    int station = (point.Reals.ContainsKey("Station")) ?
                        int.Parse(point.Reals["Station"]) : 0;
                    mySQL.AddToSwitchLog(station, ptname, param, oldtext, newtext, descriptor); 
                }
            }
        }

        public void SendToChangeLog(Entity point, string param, string oldvalue, string newvalue,
            string autor)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
            {
                if (mySQL.Connected && !point.Empty)
                {
                    string ptname = (string)point.Values["PtName"];
                    string descriptor = (string)point.Values["PtDesc"];
                    int station = (point.Reals.ContainsKey("Station")) ?
                        int.Parse(point.Reals["Station"]) : 0;
                    mySQL.AddToChangeLog(station, ptname, param, oldvalue, newvalue,
                        autor, descriptor);
                }
            }
        }

        public void SendToChangeLog(int station, string name, string param, string oldvalue, string newvalue,
            string autor, string descriptor)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
                if (mySQL.Connected)
                    mySQL.AddToChangeLog(station, name, param, oldvalue, newvalue,
                        autor, descriptor);
        }

        public void SendToSystemLog(int station, string posname, string parname)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
            {
                if (mySQL.Connected)
                    mySQL.AddToSystemLog(station, posname, parname);
            }
        }

        public void SendToTrend(Entity point, string parname, bool kind)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
            {
                if (mySQL.Connected && !point.Empty && point.Reals.ContainsKey("Quality"))
                {
                    string ptname = (string)point.Values["PtName"];
                    double value;
                    switch (parname)
                    {
                        case "PV":
                            if (point.Reals.ContainsKey("PV"))
                            {
                                bool bv;
                                if (bool.TryParse(point.Reals["PV"], out bv))
                                    value = (bv) ? 100 : 0;
                                else
                                    value = (double)point.Plugin.FloatParse(point.Reals["PV"]);
                                mySQL.AddToTrend(ptname + ".PV", value,
                                    point.Reals["Quality"].Equals("GOOD"));
                            }
                            break;
                        case "SP":
                            if (point.Reals.ContainsKey("SP"))
                            {
                                value = (double)point.Plugin.FloatParse(point.Reals["SP"]);
                                mySQL.AddToTrend(ptname + ".SP", value,
                                    point.Reals["Quality"].Equals("GOOD"));
                            }
                            break;
                        case "OP":
                            if (point.Reals.ContainsKey("OP"))
                            {
                                value = (double)point.Plugin.FloatParse(point.Reals["OP"]);
                                mySQL.AddToTrend(ptname + ".OP", value,
                                    point.Reals["Quality"].Equals("GOOD"));
                            }
                            break;
                    }
                }
            }
        }

        public void SendToTable(Entity point, string parname, int tablekind)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
            {
                if (mySQL.Connected && !point.Empty && point.Reals.ContainsKey("Quality"))
                {
                    string ptname = (string)point.Values["PtName"];
                    double value;
                    switch (parname)
                    {
                        case "PV":
                            if (point.Reals.ContainsKey("PV"))
                            {
                                bool bv;
                                if (bool.TryParse(point.Reals["PV"], out bv))
                                    value = (bv) ? 100 : 0;
                                else
                                    value = (double)point.Plugin.FloatParse(point.Reals["PV"]);
                                if (tablekind > 0) // 0-"Отключено", 1-"Значение", 2-"Накопление", 3-"Усреднение"
                                {
                                    int accumtype = tablekind - 1;  // 0-"Значение", 1-"Накопление", 2-"Усреднение"
                                    DateTime dt = DateTime.Now;
                                    for (int tabletype = 0; tabletype <= 3; tabletype++) 
                                        // 0-"Минутные", 1-"Часовые", 2-"Суточные", 3-"Месячные"
                                    {
                                        mySQL.AddIntoTable(dt, ptname + ".PV", (float)value, true,
                                            tabletype, (tabletype == 0 && tablekind == 2) ? 2 : accumtype);
                                    }
                                }
                            }
                            break;
                    }
                }
            }

            //throw new NotImplementedException();
        }
    }

    public static class Data
    {
        public static DateTime Version = DateTime.MinValue;
        private static IDictionary<string, Entity> entities = new Dictionary<string, Entity>();
        public static IDictionary<string, Entity> Entities() { return entities; }
        private static SupportUpdate logupdater = new SupportUpdate();

        public static void RestoreSQLsettings(string configfilepath)
        {
            string filename = configfilepath + "\\config.ini";
            if (File.Exists(filename))
            {
                MemIniFile mif = new MemIniFile(filename);
                string section = "ServerSQL";
                if (mif.SectionExists(section))
                {
                    Settings.Host = mif.ReadString(section, "Host", "localhost");
                    Settings.Port = mif.ReadString(section, "Port", "3306");
                    Settings.User = mif.ReadString(section, "User", "root");
                    Settings.Password = mif.ReadString(section, "Password", "3141592653");
                    Settings.Fetchbase = mif.ReadString(section, "Fetchbase", "");
                    Settings.Database = mif.ReadString(section, "Database", "");
                }
            }
        }

        public static void RestoreSQLsettingsFromString(string configcontent)
        {
            MemIniFile mif = new MemIniFile(String.Empty);
            mif.FromString(configcontent);
            string section = "ServerSQL";
            if (mif.SectionExists(section))
            {
                Settings.Host = mif.ReadString(section, "Host", "localhost");
                Settings.Port = mif.ReadString(section, "Port", "3306");
                Settings.User = mif.ReadString(section, "User", "root");
                Settings.Password = mif.ReadString(section, "Password", "3141592653");
                Settings.Fetchbase = mif.ReadString(section, "Fetchbase", "");
                Settings.Database = mif.ReadString(section, "Database", "");
            }
        }

        public static void DrawIconImage(Graphics g, Color color, string text)
        {
            //Bitmap bmp = new Bitmap(16, 16);
            //Graphics g = Graphics.FromImage(bmp);
            using (SolidBrush brush = new SolidBrush(color))
            {
                g.FillRectangle(brush, new Rectangle(0, 0, 16, 16));
                g.DrawLines(Pens.DarkGray, new Point[] { new Point(1, 15), new Point(15, 15), new Point(15, 1) });
                using (Font font = new Font("Courier New", 8))
                {
                    g.DrawString(text, font, Brushes.Black, new RectangleF(-1, 0, 20, 20));
                }
            }
            //return bmp;
        }

        public static string GetUniqueKey(int length)
        {
            string guidResult = string.Empty;
            while (guidResult.Length < length)
            {
                // Get the GUID. 
                guidResult += Guid.NewGuid().ToString().GetHashCode().ToString("x");
            }
            // Make sure length is valid. 
            if (length <= 0 || length > guidResult.Length)
                throw new ArgumentException("Length must be between 1 and " + guidResult.Length);
            // Return the first length bytes. 
            return guidResult.Substring(0, length);
        }

        public static string ClientLogin(string clientID, string kind, int station, string desc)
        {
            if (clientID.Length == 0)
                clientID = GetUniqueKey(20);
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // запись
            {
                if (mySQL.Connected)
                {
                    string SQL = String.Format("replace into `Clients`" +
                        " (`kind`,`station`,`id`,`descriptor`,`snaptime`)" +
                        " values ('{0}',{1},'{2}','{3}','{4}')",
                        kind, station, clientID, desc, 
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    mySQL.ExecSQL(SQL);
                    return clientID;
                }
            }
            return String.Empty;
        }

        public static bool ImLive(string clientID,
            string clientKind, int stationNumber, string stationName)
        {
            const uint lostseconds = 300; // 5 минут ожидания
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // запись
            {
                if (mySQL.Connected)
                {
                    List<string> list = mySQL.GetLostClients(lostseconds);
                    // удаление "висящих" списков опроса
                    foreach (string item in list) mySQL.ClearClientFetchList(item);
                   // удаление "висящих" клиентов
                    mySQL.ClearLostClients(lostseconds);
                    // Я "жив"
                    if (mySQL.ImLive(clientID, clientKind, stationNumber, stationName))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static ArrayList GetClientFetchList(string id)
        {
            ArrayList result = new ArrayList();
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // чтение удаленно
            {
                if (mySQL.Connected)
                    return mySQL.GetClientFetchList(id);
            }
            return result;
        }

        public static void UpdateClientFetchList(string id, string name, string desc, string value,
            string quality, string snaptime, ServerSQL fetchSQL)
        {
            fetchSQL.UpdateClientFetchList(id, name, desc, value, quality, snaptime);
        }

        public static void UpdateClientFetchList(string id, string name, string desc, string value,
            string quality, string snaptime)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // запись
            {
                if (mySQL.Connected)
                    mySQL.UpdateClientFetchList(id, name, desc, value, quality, snaptime);
            }
        }

        public static List<string> GetClients(int station)
        {
            List<string> result = new List<string>();
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // чтение удаленно
            {
                if (mySQL.Connected)
                    return mySQL.GetClients(station);
            }
            return result;
        }

        public static string[] GetClientTypeAndDesc(string clientID)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // чтение удаленно
            {
                if (mySQL.Connected)
                    return mySQL.GetClientTypeAndDesc(clientID);
            }
            return new string[] { "", "" };
        }

        public static void SendClientCommand(string clientID, string command, string args = "")
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // запись
            {
                if (mySQL.Connected)
                    mySQL.SendClientCommand(clientID, command, args);
            }
        }

        public static void SendShortUpCommand()
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // запись
            {
                if (mySQL.Connected)
                    mySQL.SendShortUpCommand();
            }
        }

        public static void ClearClientFetchList(string clientID)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // запись
            {
                if (mySQL.Connected)
                    mySQL.ClearClientFetchList(clientID);
            }
        }

        public static void SendClientAnswers(string clientID, string answers)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // запись
            {
                if (mySQL.Connected)
                    mySQL.SendClientAnswers(clientID, answers);
            }
        }

        public static string[] GetClientCommand(string clientID)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // чтение удалённо
            {
                if (mySQL.Connected)
                    return mySQL.GetClientCommand(clientID);
            }
            return new string[] { "", "" };
        }

        public static void ClientLogout(string clientID)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // запись
            {
                if (mySQL.Connected)
                {
                    string SQL = String.Format("delete from `Clients` where (`id`='{0}')",
                        clientID);
                    mySQL.ExecSQL(SQL);
                    // очистка списков опроса при выходе
                    mySQL.ClearClientFetchList(clientID);
                }
            }
        }

        public static bool Authorization(string username, string keyword)
        {
            if (username.Length > 0)
            {
                UInt32 big = GetMachineCode(username);
                big = (big ^ 0x12369156) | 0x11236915;
                string result = big.ToString("X4");
                return (result == keyword.ToUpper());
            }
            else
                return false;
        }

        public static string MachineCode(string username)
        {
            StringBuilder result = new StringBuilder(GetMachineCode(username).ToString("X4"));
            while (result.Length < 8) result.Insert(0, "0");
            return result.ToString();
        }

        private static UInt32 GetMachineCode(string username)
        {
            WqlObjectQuery query = new WqlObjectQuery("Select * from Win32_Processor");
            using (ManagementObjectSearcher find = new ManagementObjectSearcher(query))
            {
                StringBuilder ProcessorID = new StringBuilder();
                foreach (ManagementObject mo in find.Get())
                {
                    ProcessorID.Append(mo["ProcessorID"].ToString());
                    break;
                }
                while (ProcessorID.Length < 16) ProcessorID.Append("5");
                int Sum = 0;
                byte[] buff = Encoding.UTF8.GetBytes(username.ToUpper());
                foreach (byte b in buff) Sum += b;
                while (Sum > 256) Sum = ((Sum & 0xff00) >> 8) + (Sum & 0xff);
                byte Mask = (byte)(Sum & 0xff);
                buff = Encoding.UTF8.GetBytes(ProcessorID.ToString());
                UInt32 big = 0;
                for (int i = 0; i < buff.Length; i++)
                {
                    buff[i] ^= Mask;
                    big <<= 8;
                    big += buff[i];
                }
                return big;
            }
        }

        #region Для групп параметров

        private static int groupCount = 500;
        private static int placeCount = 10;
        private static int groupTableCount = 500;
        private static int placeTableCount = 10;

        public static void SendToChangeLog(int station, string name, string param, string oldvalue,
            string newvalue, string autor, string descriptor)
        {
            logupdater.SendToChangeLog(station, name, param, oldvalue, newvalue, autor, descriptor);
        }
        public static void SendToSystemLog(int station, string posname, string parname)
        {
            logupdater.SendToSystemLog(station, posname, parname);
        }

        public static int GroupCount(ParamGroup kind)
        { 
            if (kind == ParamGroup.Trend)
                return groupCount;
            else
                return groupTableCount;
        }

        public static int PlaceCount(ParamGroup kind)
        {
            if (kind == ParamGroup.Trend)
                return placeCount;
            else
                return placeTableCount;
        }

        public static void UpdateGroupDesc(int groupno, string desc, ParamGroup kind,
            bool localhost = false)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, localhost)) // запись
                if (mySQL.Connected) mySQL.UpdateGroupDesc(groupno, desc, kind);
        }

        public static string GetGroupDesc(int groupno, ParamGroup kind)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
                if (mySQL.Connected)
                    return mySQL.GetGroupDesc(groupno, kind);
                else
                    return String.Empty;
        }

        public static int[] GetGroupNo(string name, ParamGroup kind)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
                if (mySQL.Connected)
                    return mySQL.GetGroupNo(name, kind);
                else
                    return new int[] { };
        }

        public static string GetGroupDesc(int groupno, ServerSQL mySQL, ParamGroup kind)
        {
            return mySQL.GetGroupDesc(groupno, kind);
        }

        public static void DeleteGroupItemsLocal(ParamGroup kind)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // запись
            {
                if (mySQL.Connected)
                {
                    string tablename = (kind == ParamGroup.Trend) ? "Groups" : "TableGroups";
                    string SQL = String.Format("delete from `{0}`", tablename);
                    mySQL.ExecSQL(SQL);
                }
            }
        }

        public static void DeleteGroupNamesLocal(ParamGroup kind)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // запись
            {
                if (mySQL.Connected)
                {
                    string tablename = (kind == ParamGroup.Trend) ? "GroupNames" : "TableGroupNames";
                    string SQL = String.Format("delete from `{0}`", tablename);
                    mySQL.ExecSQL(SQL);
                }
            }
        }

        public static void SaveGroupItem(GroupItem item, ParamGroup kind, bool localhost = false)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, localhost)) // запись
            {
                if (mySQL.Connected)
                {
                    string tablename = (kind == ParamGroup.Trend) ? "Groups" : "TableGroups";
                    string SQL;
                    if (String.IsNullOrWhiteSpace(item.Name))
                        SQL = String.Format("delete from `{2}`" +
                                " where (`group`={0} and `place`={1})", item.Group, item.Place, tablename);
                    else
                        SQL = String.Format("replace into `{3}`" +
                                " (`group`,`place`,`name`) values ({0},{1},'{2}')",
                                item.Group, item.Place, item.Caption, tablename);

                    mySQL.ExecSQL(SQL);
                }
            }
        }
        #endregion

        public static void ImportBaseFrom(IDictionary<string, IPointPlugin> plugins,
            string filename, Action<string> updateMessage)
        {
            string ptName = String.Empty;
            string[] lines = System.IO.File.ReadAllLines(filename);
            EmptyPoints();
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // чтение удалённо
            {
                if (mySQL.Connected)
                {
                    foreach (string line in lines)
                    {
                        if (!String.IsNullOrWhiteSpace(line))
                        {
                            string[] items = line.Split(new char[] { '=' });
                            if (items.Length == 2 && !String.IsNullOrWhiteSpace(items[0]) &&
                                !String.IsNullOrWhiteSpace(items[1]))
                            {
                                if (items[0].Equals("PtName"))
                                {
                                    ptName = items[1];
                                    updateMessage("Импорт точки\"" + ptName + "\"...");
                                }
                                mySQL.WritePropValue(ptName, items[0], items[1]);
                            }
                        }
                    }
                }
            }
            updateMessage("Получение версии базы данных...");
            Version = LoadBase(plugins);
        }

        public static void ExportBaseAs(string filename, Action<string> updateMessage)
        {
            List<string> lines = new List<string>();
            foreach (KeyValuePair<string, Entity> item in Data.Entities())
            {
                updateMessage("Экспорт точки \"" + item.Key + "\"...");
                lines.Add("[" + item.Key + "]");
                foreach (KeyValuePair<string, object> prop in item.Value.Values)
                    if (!prop.Key.Equals("Version") && !prop.Key.Equals("BaseVersion"))
                        lines.Add(prop.Key + "=" + prop.Value.ToString());
                lines.Add("");
            }
            System.IO.File.WriteAllLines(filename, lines.ToArray());
        }

        public static void ImportGroupsFrom(string filename, ParamGroup kind)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);
            EmptyGroups(kind);
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
            {
                if (mySQL.Connected)
                {
                    int group = 0;
                    foreach (string line in lines)
                    {
                        if (!String.IsNullOrWhiteSpace(line))
                        {
                            if (line.StartsWith("[Группа "))
                            {
                                group = int.Parse(line.Substring(8, line.Length - 9));
                            }
                            else
                            {
                                string[] items = line.Split(new char[] { '=' });
                                if (items.Length == 2 && !String.IsNullOrWhiteSpace(items[0]) &&
                                    !String.IsNullOrWhiteSpace(items[1]))
                                {
                                    int place;
                                    if (int.TryParse(items[0], out place))
                                    {
                                        string[] vals = items[1].Split(new char[] { '.' });
                                        GroupItem gi = new GroupItem(group, place);
                                        gi.Name = vals[0];
                                        gi.Param = (vals.Length == 2) ? vals[1] : String.Empty;
                                        gi.Caption = (gi.Name.Length > 0) ?
                                            (gi.Name + "." +
                                            ((gi.Param.Length > 0) ? gi.Param : "PV")) : String.Empty;
                                        SaveGroupItem(gi, kind);
                                    }
                                    else
                                        if (items[0].Equals("Descriptor"))
                                        {
                                            UpdateGroupDesc(group, items[1], kind);
                                        }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void ExportGroupsAs(string filename, ParamGroup kind)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // чтение удалённо
            {
                if (mySQL.Connected)
                {
                    List<string> lines = new List<string>();
                    for (int group = 1; group <= GroupCount(kind); group++)
                    {
                        string[] places = GroupItems(group, mySQL, ParamGroup.Trend);
                        bool empty = true;
                        foreach (string name in places)
                            if (!String.IsNullOrWhiteSpace(name)) { empty = false; break; }
                        if (!empty)
                        {
                            lines.Add("[Группа " + group + "]");
                            lines.Add("Descriptor=" + GetGroupDesc(group, mySQL, ParamGroup.Trend));
                            for (int place = 1; place <= PlaceCount(kind); place++)
                            {
                                GroupItem item = GroupItem(group, place, mySQL, ParamGroup.Trend);
                                if (!String.IsNullOrWhiteSpace(item.Name))
                                    lines.Add(place + "=" + item.Caption);
                            }
                            lines.Add("");
                        }
                    }
                    System.IO.File.WriteAllLines(filename, lines.ToArray());
                }
            }
        }

        public static void DeleteFromTrends(DateTime snaptime)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
                if (mySQL.Connected) mySQL.DeleteFromTrends(snaptime);
        }
        public static void DeleteFromLogs(DateTime snaptime)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
                if (mySQL.Connected) mySQL.DeleteFromLogs(snaptime);
        }
        public static void DeleteFromTable(DateTime snaptime, string tabletype)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
                if (mySQL.Connected) mySQL.DeleteFromTable(snaptime, tabletype);
        }

        public static void DeleteFromReports(DateTime snaptime)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
                if (mySQL.Connected) mySQL.DeleteFromReportLog(snaptime);
        }

        public static void WriteRealVals(Entity point)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // запись
            {
                if (mySQL.Connected && !point.Empty)
                {
                    string ptName = point.Values["PtName"].ToString();
                    foreach (KeyValuePair<string, string> item in point.Reals)
                    {
                        string propName = item.Key;
                        string propValue = item.Value;
                        if (!propName.Equals("Version") && !propName.Equals("BaseVersion"))
                            mySQL.WriteRealValue(ptName, propName, propValue);
                    }
                }
            }
        }

        public static void WriteRealVals(Entity point, ServerSQL fetchSQL)
        {
            if (!point.Empty)
            {
                string ptName = point.Values["PtName"].ToString();
                foreach (KeyValuePair<string, string> item in point.Reals)
                {
                    string propName = item.Key;
                    string propValue = item.Value;
                    if (!propName.Equals("Version") && !propName.Equals("BaseVersion"))
                        fetchSQL.WriteRealValue(ptName, propName, propValue);
                }
            }
        }

        public static void WriteRealValue(string ptName, string propName, string propValue)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // запись
            {
                if (mySQL.Connected && !propName.Equals("Version") && !propName.Equals("BaseVersion"))
                    mySQL.WriteRealValue(ptName, propName, propValue);
            }
        }

        public static void WriteRealValue(string ptName, string propName, string propValue, ServerSQL mySQL)
        {
            if (!propName.Equals("Version") && !propName.Equals("BaseVersion"))
                mySQL.WriteRealValue(ptName, propName, propValue);
        }
       
        public static void WriteEntityProp(Entity point, string propName, string propValue)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
            {
                if (mySQL.Connected && !point.Empty &&
                    !propName.Equals("Version") && !propName.Equals("BaseVersion"))
                {
                    mySQL.WritePropValue(point.Values["PtName"].ToString(), propName, propValue);
                }
            }
        }

        public static bool SchemeExists(string schemename)
        {
            IDictionary<string, string> list = GetSchemesList();
            return list.ContainsKey(schemename);
        }

        public static byte[] GetImageData(string name)
        {
            byte[] result = new byte[] { };
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
                if (mySQL.Connected) result = mySQL.GetImageData(name);
            return result;
        }

        public static DateTime GetImageFileTime(string name)
        {
            DateTime result = DateTime.MinValue;
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
                if (mySQL.Connected) result = mySQL.GetImageFileTime(name);
            return result;
        }

        public static List<string> GetImagesList()
        {
            List<string> result = new List<string>();
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
                if (mySQL.Connected) result = mySQL.GetImagesList();
            return result;
        }

        public static byte[] GetReportData(string name)
        {
            byte[] result = new byte[] { };
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
                if (mySQL.Connected) result = mySQL.GetReportData(name);
            return result;
        }

        //public static void SetReportProps(string name, bool auto, DateTime time, int period)
        //{
        //    using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
        //        if (mySQL.Connected) mySQL.SetReportProps(name, auto, time, period);
        //}

        //public static IDictionary<string, object> GetReportProps(string name)
        //{
        //    IDictionary<string, object> result = new Dictionary<string, object>();
        //    using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
        //        if (mySQL.Connected) result = mySQL.GetReportProps(name);
        //    return result;
        //}

        public static DateTime GetReportFileTime(string name)
        {
            DateTime result = DateTime.MinValue;
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
                if (mySQL.Connected) result = mySQL.GetReportFileTime(name);
            return result;
        }

        public static IDictionary<string, string> GetReportsList()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
                if (mySQL.Connected)
                {
                    ArrayList list = mySQL.GetReportsList();
                    char[] splitters = new char[] { ';' };
                    foreach (string line in list)
                    {
                        string[] items = line.Split(splitters);
                        if (!result.ContainsKey(items[0]))
                            result.Add(items[0], items[1]);
                    }
                }
            return result;
        }

        public static IDictionary<string, string> GetSchemesList()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
            {
                if (mySQL.Connected)
                {
                    ArrayList list = mySQL.GetSchemesList();
                    char[] splitters = new char[] { ';' };
                    foreach (string line in list)
                    {
                        string[] items = line.Split(splitters);
                        result.Add(items[0], items[1]);
                    }
                }
            }
            return result;
        }

        public static List<string> GetDinList(string schemename, ServerSQL mySQL)
        {
            List<string> result = new List<string>();
            ArrayList list = mySQL.GetDinList(schemename);
            foreach (string line in list)
                result.Add(line);
            return result;
        }

        public static string ColorToBase(Color color)
        {
            return String.Format("{0};{1};{2};{3}", color.A, color.R, color.G, color.B);
        }

        public static Color ColorFromBase(string color)
        {
            string[] cols = color.Split(new char[] { ';' });
            int A = int.Parse(cols[0]);
            int R = int.Parse(cols[1]);
            int G = int.Parse(cols[2]);
            int B = int.Parse(cols[3]);
            return Color.FromArgb(A, R, G, B);
        }

        public static IDictionary<string, string> GetDinProps(string schemename, string dinname,
            ServerSQL mySQL)
        {
            return mySQL.ReadDinProps(schemename, dinname);
        }

        public static void WriteDrawProp(string schemeName, int npp, string dinName, string propName,
            string propValue, ServerSQL mySQL)
        {
            mySQL.WriteDinValue(schemeName, npp, dinName, propName, propValue);
        }

        //public static void WriteDrawProp(string schemeName, int npp, string dinName, string propName,
        //    string propValue)
        //{
        //    using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database))
        //    {
        //        if (mySQL.Connected)
        //        {
        //            mySQL.WriteDinValue(schemeName, npp, dinName, propName, propValue);
        //        }
        //    }
        //}

        public static void DeleteDraw(string schemeName, string dinName)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
                if (mySQL.Connected) mySQL.DeleteDin(schemeName, dinName);
        }
        public static void EmptyImages()
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
                if (mySQL.Connected) mySQL.EmptyImages();
        }
        public static void AddImage(string name, DateTime filetime, byte[] image)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
                if (mySQL.Connected) mySQL.AddImage(name, filetime, image);
        }

        public static void EmptyReports()
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
                if (mySQL.Connected) mySQL.EmptyReports();
        }

        public static void AddReport(string name, DateTime filetime, byte[] image,
            string descriptor)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
                if (mySQL.Connected) mySQL.AddReport(name, filetime, image, descriptor);
        }

        public static void EmptyReport(string reportname)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
                if (mySQL.Connected) mySQL.EmptyReport(reportname);
        }

        public static void RenameScheme(string oldschemename, string newschemename, ServerSQL mySQL)
        {
            mySQL.RenameScheme(oldschemename, newschemename);
        }

        public static void EmptyScheme(string schemename, ServerSQL mySQL)
        {
            mySQL.EmptyDinamics(schemename);
        }

        public static void EmptyScheme(string schemename)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
                if (mySQL.Connected) mySQL.EmptyDinamics(schemename);
        }

        public static bool DeleteEntity(string ptname)
        {
            try
            {
                using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
                {
                    if (mySQL.Connected)
                    {
                        mySQL.EraseEntity(ptname);
                        entities.Remove(ptname);
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (ArgumentException)
            {
                return false;
            }
        }        

        public static bool RenameEntity(string oldname, string newname) 
        {
            try
            {
                using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
                {
                    if (mySQL.Connected)
                    {
                        Entity oldEntity = (Entity)entities[oldname];
                        Entity newEntity = new Entity(oldEntity.Plugin, oldEntity.LogUpdate);
                        foreach (KeyValuePair<string, object> prop in oldEntity.Values)
                            newEntity.Values.Add(prop.Key, prop.Value);
                        newEntity.Values["PtName"] = newname;
                        entities.Add(newname, newEntity);
                        entities.Remove(oldname);
                        mySQL.RenameEntity(oldname, newname);
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public static bool DoubleEntity(string oldname, string newname) 
        {
            try
            {
                using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
                {
                    if (mySQL.Connected)
                    {
                        Entity oldEntity = (Entity)entities[oldname];
                        Entity newEntity = new Entity(oldEntity.Plugin, oldEntity.LogUpdate);
                        foreach (KeyValuePair<string, object> prop in oldEntity.Values)
                            newEntity.Values.Add(prop.Key, prop.Value);
                        newEntity.Values["PtName"] = newname;
                        entities.Add(newname, newEntity);
                        CreateEntity(newname, newEntity, mySQL);
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public static bool GroupIsEmpty(int index, ParamGroup kind)
        {
            bool result = true;
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
            {
                if (mySQL.Connected)
                    return mySQL.GroupIsEmpty(index, kind);
            }
            return result;
        }

        public static bool GroupIsEmpty(int index, ServerSQL mySQL, ParamGroup kind)
        {
            return mySQL.GroupIsEmpty(index, kind);
        }

        public static string GroupEntity(int group, int place, ParamGroup kind)
        {
            string result = String.Empty;
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
            {
                if (mySQL.Connected)
                    return mySQL.GroupEntity(group, place, kind);
            }
            return result;
        }

        public static GroupItem GroupItem(int group, int place, ParamGroup kind)
        {
            GroupItem result = new GroupItem();
            result.Group = 0;
            result.Place = 0;
            result.Name = String.Empty;
            result.Param = String.Empty;
            result.Caption = String.Empty;
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
            {
                if (mySQL.Connected)
                {
                    string[] items = mySQL.GroupItem(group, place, kind);
                    result.Group = int.Parse(items[0]);
                    result.Place = int.Parse(items[1]);
                    result.Name = items[2];
                    result.Param = items[3];
                    result.Caption = items[4];
                }
            }
            return result;
        }

        public static GroupItem GroupItem(int group, int place, ServerSQL mySQL, ParamGroup kind)
        {
            GroupItem result = new GroupItem();
            string[] items = mySQL.GroupItem(group, place, kind);
            result.Group = int.Parse(items[0]);
            result.Place = int.Parse(items[1]);
            result.Name = items[2];
            result.Param = items[3];
            result.Caption = items[4];
            return result;
        }

        public static string[] GroupItems(int group, ParamGroup kind)
        {
            string[] items = new string[placeCount];
            for (int i = 0; i < items.Length; i++) items[i] = String.Empty;
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
            {
                if (mySQL.Connected)
                    return mySQL.GroupItems(group, placeCount, kind);
            }
            return items;
        }

        public static string[] GroupItems(int group, ServerSQL mySQL, ParamGroup kind)
        {
            return mySQL.GroupItems(group, placeCount, kind);
        }

        public static void EmptyGroups(ParamGroup kind)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
            {
                if (mySQL.Connected)
                    mySQL.EmptyGroups(kind);
            }
        }

        public static void EmptyPoints()
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
            {
                entities.Clear();
                mySQL.EmptyPoints();
            }
        }



        public static DateTime LoadBase(IDictionary<string, IPointPlugin> plugins,
            string ptname = "")
        {
            DateTime result = DateTime.MinValue;
            if (ptname.Length == 0)
            {
                entities.Clear();
                using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
                {
                    if (mySQL.Connected)
                    {
                        IDictionary<string, string> list = mySQL.ReadPoints();
                        foreach (KeyValuePair<string, string> kvp in list)
                            if (plugins.ContainsKey(kvp.Value))
                            {
                                AddEntity(kvp.Key, plugins[kvp.Value]); // с данными по умолчанию
                                Entity ent = GetEntity(kvp.Key, mySQL);
                                entities[kvp.Key] = ent; // актуализируем данные
                                if (ent.Values.ContainsKey("Version")) // дата изменения
                                {
                                    DateTime dt = DateTime.Parse(ent.Values["Version"].ToString());
                                    if (dt > result) result = dt;
                                }
                            }
                    }
                }
            }
            else
            {
                using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
                {
                    if (mySQL.Connected)
                    {
                        IDictionary<string, string> list = mySQL.ReadPoints();
                        if (list.ContainsKey(ptname) && entities.ContainsKey(ptname))
                        {
                            entities[ptname] = GetEntity(ptname, mySQL);
                        }
                        if (entities.ContainsKey(ptname) &&
                            entities[ptname].Values.ContainsKey("Version")) // дата изменения
                        {
                            DateTime dt = DateTime.Parse(entities[ptname].Values["Version"].ToString());
                            if (dt > result) result = dt;
                        }
                    }
                }
            }
            Version = result;
            return result;
        }

        public static IDictionary<string, Entity> LoadBase(IDictionary<string, IPointPlugin> plugins,
            ServerSQL DatabaseSQL)
        {
            IDictionary<string, Entity> result = new Dictionary<string, Entity>();
            IDictionary<string, string> list = DatabaseSQL.ReadPoints();
            entities.Clear();
            foreach (KeyValuePair<string, string> kvp in list)
            {
                if (plugins.ContainsKey(kvp.Value))
                {
                    AddEntity(kvp.Key, plugins[kvp.Value]); // с данными по умолчанию
                    Entity ent = GetEntity(kvp.Key, DatabaseSQL);
                    result[kvp.Key] = ent; // актуализируем данные
                }
            }
            return result;
        }

        public static HashSet<string> LoadEnitityNames(IDictionary<string, IPointPlugin> plugins,
            ServerSQL DatabaseSQL)
        {
            HashSet<string> result = new HashSet<string>();
            IDictionary<string, string> list = DatabaseSQL.ReadPoints();
            foreach (KeyValuePair<string, string> kvp in list)
            {
                if (plugins.ContainsKey(kvp.Value))
                    result.Add(kvp.Key);
            }
            return result;
        }

        public static bool CreateEntity(string ptname, Entity e, ServerSQL mySQL)
        {
            foreach (KeyValuePair<string, object> prop in e.Plugin.PropValueDefs())
            {
                string value;
                if (e.Values.ContainsKey(prop.Key))
                    value = e.Values[prop.Key].ToString();
                else
                    value = prop.Value.ToString();
                if (prop.Key.Equals("PtName")) value = ptname;
                mySQL.WritePropValue(ptname, prop.Key, value);
            }
            return true;
        }

        public static bool CreateEntity(string ptname, Entity e)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
            {
                if (mySQL.Connected)
                    return CreateEntity(ptname, e, mySQL);
                else
                    return false;
            }
        }

        public static bool AddEntity(string ptname, IPointPlugin plugin) 
        {
            try
            {
                Entity e = new Entity(plugin, logupdater as ILogUpdate);
                // копируем из значений свойств по умолчанию в рабочие свойства точки
                foreach (KeyValuePair<string, object> prop in plugin.PropValueDefs())
                    e.Values.Add(prop.Key, prop.Value);
                e.Values["PtName"] = ptname;
                entities.Add(ptname, e);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        static string[] formats = new string[] { "{0:0}", "{0:0.0}", "{0:0.00}", "{0:0.000}",
                                                 "{0:0000}", "{0:0.00000}", "{0:0.000000}" };
        public static string Float(object value, int format)
        {
            decimal val = FloatParse(value.ToString());
            return String.Format(formats[format], val);
        }
        public static decimal FloatEx(string value)
        {
            string[] vals = value.Split(new char[] { ';' });
            return FloatParse(vals[0]);
        }
        public static decimal FloatParse(string value)
        {
            decimal result = 0m;
            string sval = value.Replace(',', '.');
            if (decimal.TryParse(sval, out result))
                return result;
            else
            {
                sval = value.Replace('.', ',');
                if (decimal.TryParse(sval, out result))
                    return result;
                else
                {
                    bool bres = false;
                    if (bool.TryParse(sval, out bres))
                        return (bres) ? 1m : 0m;
                    else
                    {
                        DateTime dt;
                        DateTime now = DateTime.Now;
                        if (DateTime.TryParse(sval, out dt))
                        {
                            if (dt.Year == now.Year && dt.Month == now.Month && dt.Day == now.Day &&
                                (dt.Hour + dt.Minute + dt.Second) > 0)  // время
                                return Decimal.Parse(dt.ToString("HHmmss"));
                            else // дата
                                return Decimal.Parse(dt.ToString("ddMMyy"));
                        }
                    }
                }
            }
            return result;
        }

        public static IDictionary<string, string> GetRealValues(string ptname)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase, true)) // чтение
                if (mySQL.Connected)
                    result = mySQL.ReadRealValues(ptname);
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
                if (mySQL.Connected)
                {
                    result.Add("Version", mySQL.ReadVersion(ptname).ToString());
                    result.Add("BaseVersion", Version.ToString());
                }
            return result;
        }

        public static IDictionary<string, string> GetPropValues(string ptname,
            ServerSQL dataSQL)
        {
            return dataSQL.ReadPropValues(ptname);
        }

        public static IDictionary<string, string> GetRealValues(string ptname,
            ServerSQL fetchSQL, ServerSQL dataSQL)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            result = fetchSQL.ReadRealValues(ptname);
            result.Add("Version", dataSQL.ReadVersion(ptname).ToString());
            result.Add("BaseVersion", Version.ToString());
            return result;
        }

        public static void RemoveAlarms(string ptname)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase)) // запись
                if (mySQL.Connected) mySQL.RemoveAlarms(ptname);
        }

        public static void RemoveAlarms(string ptname, ServerSQL fetchSQL)
        {
            fetchSQL.RemoveAlarms(ptname);
        }

        public static Entity GetEntity(string ptname)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
            {
                if (mySQL.Connected)
                {
                    return GetEntity(ptname, mySQL);
                }
                else
                    return new Entity();
            }
        }

        public static Entity GetEntity(string ptname, ServerSQL dataSQL)
        {
            return GetEntity(ptname, dataSQL, entities);
        }

        public static Entity GetEntity(string ptname, ServerSQL dataSQL,
            IDictionary<string, Entity> ents)
        {
            if (ents.ContainsKey(ptname))
            {
                Entity e = (Entity)ents[ptname];
                IDictionary<string, string> values = dataSQL.ReadPropValues(ptname);
                foreach (KeyValuePair<string, string> prop in values)
                {
                    string propname = prop.Key;
                    if (e.Values.ContainsKey(propname))
                    {
                        switch (e.Plugin.GetPropType(propname))
                        {
                            case PropType.Boolean:
                                e.Values[propname] = Boolean.Parse(prop.Value);
                                break;
                            case PropType.Enumeration:
                                e.Values[propname] = int.Parse(prop.Value);
                                break;
                            case PropType.Float:
                                e.Values[propname] = FloatParse(prop.Value);
                                break;
                            case PropType.Integer:
                                e.Values[propname] = int.Parse(prop.Value);
                                break;
                            case PropType.Invisible:
                                switch (propname)
                                {
                                    case "PtKind":
                                        e.Values[propname] = int.Parse(prop.Value);
                                        break;
                                    case "DataCount":
                                        e.Values[propname] = int.Parse(prop.Value);
                                        break;
                                    default:
                                        e.Values[propname] = prop.Value;
                                        break;
                                }
                                break;
                            default:
                                e.Values[propname] = prop.Value;
                                break;
                        }
                    }
                }
                return e;
            }
            else
                return new Entity();
        }

        public static Double GetHourData(string name, DateTime dt, ServerSQL mySQL)
        {
            return mySQL.GetHourData(name, DateTime.Parse(dt.ToString("dd.MM.yyyy HH:00:00")));
        }

        public static string GetHourData(string name, DateTime dt, ServerSQL mySQL, int formatPV)
        {
            Double value = mySQL.GetHourData(name, DateTime.Parse(dt.ToString("dd.MM.yyyy HH:00:00")));
            return Double.IsNaN(value) ? "------" : Float(value, formatPV);
        }

        public static Double GetDailyData(string name, DateTime dt, ServerSQL mySQL)
        {
            return mySQL.GetDailyData(name, DateTime.Parse(dt.ToString("dd.MM.yyyy 00:00:00")));
        }

        public static string GetDailyData(string name, DateTime dt, ServerSQL mySQL, int formatPV)
        {
            Double value = mySQL.GetDailyData(name, DateTime.Parse(dt.ToString("dd.MM.yyyy 00:00:00")));
            return Double.IsNaN(value) ? "------" : Float(value, formatPV);
        }

        public static Double GetMonthData(string name, DateTime dt, ServerSQL mySQL)
        {
            return mySQL.GetMonthData(name, DateTime.Parse(dt.ToString("01.MM.yyyy 00:00:00")));
        }

        public static string GetMonthData(string name, DateTime dt, ServerSQL mySQL, int formatPV)
        {
            Double value = mySQL.GetMonthData(name, DateTime.Parse(dt.ToString("01.MM.yyyy 00:00:00")));
            return Double.IsNaN(value) ? "------" : Float(value, formatPV);
        }

        public static SortedList<DateTime, double> LoadTrend(string trendname, DateTime from, DateTime to,
            ServerSQL dataSQL, bool avg)
        {
            return dataSQL.LoadTrend(trendname, from, to, avg);
        }

        public static void AddIntoTable(DateTime dt, string name, Single value, bool kind,
            int tabletype, int accumtype)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
            {
                if (mySQL.Connected) mySQL.AddIntoTable(dt, name, value, kind, tabletype, accumtype);
            }
        }

        public static void AddIntoTable(DateTime dt, string name, Single value, bool kind,
            int tabletype, int accumtype, ServerSQL dataSQL)
        {
            dataSQL.AddIntoTable(dt, name, value, kind, tabletype, accumtype);
        }

        public static string GetLastAlarm()
        {
            string result = String.Empty;
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase, true)) // чтение 
                if (mySQL.Connected) result = mySQL.GetLastAlarm();
            return result;
        }

        public static string GetLastAlarm(ServerSQL fetchSQL)
        {
            return fetchSQL.GetLastAlarm();
        }

        public static string GetLastSwitch()
        {
            string result = String.Empty;
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Fetchbase, true)) // чтение
                if (mySQL.Connected) result = mySQL.GetLastSwitch();
            return result;
        }

        public static string GetLastSwitch(ServerSQL fetchSQL)
        {
            return fetchSQL.GetLastSwitch();
        }
    }
}
