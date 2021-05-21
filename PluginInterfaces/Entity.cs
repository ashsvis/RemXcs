using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Points.Plugins
{
    public sealed class OpcBridgeSupport: IDisposable
    {
        Type opcBridgeServerType;
        object opcBridgeServerObject;

        public OpcBridgeSupport()
        {
            opcBridgeServerType = Type.GetTypeFromProgID("OpcBridgeServer.Application");
            if (opcBridgeServerType != null)
            {
                try
                {
                    opcBridgeServerObject = Activator.CreateInstance(opcBridgeServerType);
                    InitOPC();
                }
                catch
                {
                    opcBridgeServerType = null;
                }
            }
        }

        public void Dispose()
        {
            if (opcBridgeServerType != null)
            {
                try
                {
                    FinitOPC();
                }
                catch
                {
                    opcBridgeServerType = null;
                }
            }
        }

        public void InitOPC()
        {
            if (opcBridgeServerType != null)
            {
                opcBridgeServerType.InvokeMember("InitOPC",
                    System.Reflection.BindingFlags.InvokeMethod,
                    null, opcBridgeServerObject, new object[] { });
            }
        }

        public void FinitOPC()
        {
            if (opcBridgeServerType != null)
            {
                opcBridgeServerType.InvokeMember("FinitOPC",
                    System.Reflection.BindingFlags.InvokeMethod,
                    null, opcBridgeServerObject, new object[] { });
            }
        }

        public string GetServers()
        {
            if (opcBridgeServerType != null)
                return (string)opcBridgeServerType.InvokeMember("GetServers",
                            System.Reflection.BindingFlags.InvokeMethod,
                            null, opcBridgeServerObject, new object[] { });
            else
                return String.Empty;
        }

        public string GetProps(string server)
        {
            if (opcBridgeServerType != null)
                return (string)opcBridgeServerType.InvokeMember("GetProps",
                        System.Reflection.BindingFlags.InvokeMethod,
                        null, opcBridgeServerObject, new object[] { server });
            else
                return String.Empty;
        }

        public void AddItem(string server, string group, string param)
        {
            if (opcBridgeServerType != null)
            {
                opcBridgeServerType.InvokeMember("AddItem",
                    System.Reflection.BindingFlags.InvokeMethod,
                    null, opcBridgeServerObject, new object[] { server, group, param });
            }
        }

        public string FetchItem(string server, string group, string param)
        {
            if (opcBridgeServerType != null)
                return (string)opcBridgeServerType.InvokeMember("FetchItem",
                        System.Reflection.BindingFlags.InvokeMethod,
                        null, opcBridgeServerObject, new object[] { server, group, param });
            else
                return String.Empty;
        }

    }

    #region Члены PointPlugin
    public delegate IDictionary<string, string> ImportRealValues(string ptname);
    public delegate void ExportRealValues(string command, string ptname,
                                            IDictionary<string, string> realvals);
    public partial class ClassPoint
    {
        protected IDictionary<string, string> descs;
        protected IDictionary<string, int> types;
        protected IDictionary<string, object> defs;
        protected IDictionary<string, string[]> enums;
        protected IDictionary<string, string> enumprops;
        protected IDictionary<string, int[]> intRanges;
        protected IDictionary<string, object> reals;
        public ClassPoint()
        {
            descs = new Dictionary<string, string>();
            types = new Dictionary<string, int>();
            defs = new Dictionary<string, object>();
            enums = new Dictionary<string, string[]>();
            enumprops = new Dictionary<string, string>();
            intRanges = new Dictionary<string, int[]>();
            reals = new Dictionary<string, object>();
        }
    }
    public static class PointPlaginType
    {
        public const int Nothing = 0;
        public const int Field = 1;
        public const int Virtual = 2;
        public const int OPC = 3;
    }
    public static class PropType
    {
        public const int Nothing = 0;
        public const int Invisible = 1;
        public const int String = 2;
        public const int Boolean = 3;
        public const int Integer = 4;
        public const int Float = 5;
        public const int FloatEx = 6;
        public const int Enumeration = 7;
        public const int Link = 8;
        public const int LinkEx = 9;
        public const int LinkOPC = 10;
        public const int TypeOPC = 11;
        public static string TextCDT(int cdt)
        {
            if (AType.Count == 0) InitAType();
            return (AType.ContainsKey(cdt)) ? AType[cdt] : System.String.Empty;
        }
        private static IDictionary<int, string> AType = new Dictionary<int, string>();
        private static void InitAType()
        {
            AType.Add(0x00, "VT_EMPTY (any)"); // varEmpty
            AType.Add(0x10, "VT_I1 (ShortInt)"); // varShortInt
            AType.Add(0x11, "VT_UI1 (Byte)"); // varByte
            AType.Add(0x02, "VT_I2 (Smallint)"); // varSmallint
            AType.Add(0x12, "VT_UI2 (Word)"); // varWord
            AType.Add(0x03, "VT_I4 (Integer)"); // varInteger
            AType.Add(0x13, "VT_UI4 (LongWord)"); // varLongWord
            AType.Add(0x14, "VT_I8 (Int64)"); // varInt64
            AType.Add(0x15, "VT_UI8 (Word64)"); // varWord64
            AType.Add(0x04, "VT_R4 (Single)"); // varSingle
            AType.Add(0x05, "VT_R8 (Double)"); // varDouble
            AType.Add(0x06, "VT_CY (Currency)"); // varCurrency
            AType.Add(0x07, "VT_DATE (Date)"); // varDate
            AType.Add(0x08, "VT_BSTR (OleStr)"); // varOleStr
            AType.Add(0x0b, "VT_BOOL (Boolean)"); // varBoolean
            AType.Add(0x2010, "VT_ARRAY | VT_I1"); // VT_ARRAY+varShortInt
            AType.Add(0x2011, "VT_ARRAY | VT_UI1"); // VT_ARRAY+varByte
            AType.Add(0x2002, "VT_ARRAY | VT_I2"); // VT_ARRAY+varSmallint
            AType.Add(0x2012, "VT_ARRAY | VT_UI2"); // VT_ARRAY+varWord
            AType.Add(0x2003, "VT_ARRAY | VT_I4"); // VT_ARRAY+varInteger
            AType.Add(0x2013, "VT_ARRAY | VT_UI4"); // VT_ARRAY+varLongWord
            AType.Add(0x2014, "VT_ARRAY | VT_I8"); // VT_ARRAY+varInt64
            AType.Add(0x2015, "VT_ARRAY | VT_UI8"); // VT_ARRAY+varWord64
            AType.Add(0x2004, "VT_ARRAY | VT_R4"); // VT_ARRAY+varSingle
            AType.Add(0x2005, "VT_ARRAY | VT_R8"); // VT_ARRAY+varDouble
            AType.Add(0x2006, "VT_ARRAY | VT_CY"); // VT_ARRAY+varCurrency
            AType.Add(0x2007, "VT_ARRAY | VT_DATE"); // VT_ARRAY+varDate
            AType.Add(0x2008, "VT_ARRAY | VT_BSTR"); // VT_ARRAY+varOleStr
            AType.Add(0x200b, "VT_ARRAY | VT_BOOL)"); // VT_ARRAY+varBoolean
        }
    }
    public static class PtKind
    {
        public const int Nothing = 0;
        public const int Analog = 1;
        public const int Digital = 2;
        public const int Group = 3;
        public const int Table = 4;
        public const int Kontur = 5;
        public const int Valve = 6;
    }
    public struct Entity
    {
        public bool Empty { get { return Plugin == null; } }
        public IPointPlugin Plugin;
        public ILogUpdate LogUpdate; 
        public IDictionary<string, object> Values;
        public IDictionary<string, string> Reals;
        public Entity(IPointPlugin plugin, ILogUpdate logupdate)
        {
            Plugin = plugin;
            LogUpdate = logupdate;
            Values = new Dictionary<string, object>();
            Reals = new Dictionary<string, string>();
        }
        private bool inSwitchs(string key)
        {
            string value = (Reals.ContainsKey("Switchs")) ? (string)Reals["Switchs"] : String.Empty;
            return value.IndexOf(key + "=") >= 0;
        }
        private bool isSwitchs()
        {
            string value = (Reals.ContainsKey("Switchs")) ? (string)Reals["Switchs"] : String.Empty;
            return value.Length > 0;
        }
        public bool inAlarms(string key)
        {
            string value = (Reals.ContainsKey("Alarms")) ? (string)Reals["Alarms"] : String.Empty;
            return value.IndexOf(key + "=") >= 0;
        }

        public bool isAlarms()
        {
            string value = (Reals.ContainsKey("Alarms")) ? (string)Reals["Alarms"] : String.Empty;
            return value.Length > 0;
        }
        private void addSwitch(string key)
        {
            string value = (Reals.ContainsKey("Switchs")) ? (string)Reals["Switchs"] : String.Empty;
            if (value.IndexOf(key + "=") < 0)
                value += ((value.Length > 0) ? ";" : String.Empty) + key + "=" + false.ToString();
            SetRealProp("Switchs", value);
            LogUpdate.AddSwitch(key, this);
        }
        private void removeSwitch(string key)
        {
            string value = (Reals.ContainsKey("Switchs")) ? (string)Reals["Switchs"] : String.Empty;
            if (value.IndexOf(key + "=") >= 0)
            {
                string[] items = value.Split(new char[] { ';' });
                IDictionary<string, bool> list = new Dictionary<string, bool>();
                foreach (string item in items)
                {
                    string[] vals = item.Split(new char[] { '=' });
                    if (vals.Length == 2) list.Add(vals[0], Boolean.Parse(vals[1]));
                }
                list.Remove(key);
                items = new string[list.Count];
                int i = 0;
                foreach (KeyValuePair<string, bool> kvp in list)
                    items[i++] = kvp.Key + "=" + kvp.Value.ToString();
                SetRealProp("Switchs", String.Join(";", items));
                LogUpdate.RemoveSwitch(key, this);
            }
        }
        public void RemoveSwitchs()
        {
            LogUpdate.RemoveSwitchs(this);
        }

        public void updateAlarmValue()
        {
            LogUpdate.UpdateAlarmValue(this);
        }

        public void addAlarm(string key)
        {
            string value = (Reals.ContainsKey("Alarms")) ? Reals["Alarms"] : String.Empty;
            if (value.IndexOf(key + "=") < 0)
                value += ((value.Length > 0) ? ";" : String.Empty) + key + "=" + false.ToString();
            SetRealProp("Alarms", value);
            value = (Reals.ContainsKey("LostAlarms")) ? Reals["LostAlarms"] : String.Empty;
            if (value.IndexOf(key) >= 0)
            {
                string[] items = value.Split(new char[] { ';' });
                List<string> list = new List<string>();
                foreach (string item in items) list.Add(item);
                list.Remove(key);
                SetRealProp("LostAlarms", String.Join(";", list.ToArray()));
            }
            LogUpdate.AddAlarm(key, this, String.Empty);
            LogUpdate.SendToAlarmLog(key, this, String.Empty);
        }

        public void removeAlarm(string key)
        {
            string value = (Reals.ContainsKey("Alarms")) ? Reals["Alarms"] : String.Empty;
            if (value.IndexOf(key + "=") >= 0)
            {
                string[] items = value.Split(new char[] { ';' });
                IDictionary<string, bool> list = new Dictionary<string, bool>();
                foreach (string item in items)
                {   
                    string[] vals = item.Split(new char[] { '=' });
                    if (vals.Length == 2) list.Add(vals[0], Boolean.Parse(vals[1]));
                }
                value = (Reals.ContainsKey("LostAlarms")) ? Reals["LostAlarms"] : String.Empty;
                if (!list[key] && value.IndexOf(key) < 0)
                {
                    value += ((value.Length > 0) ? ";" : String.Empty) + key;
                    SetRealProp("LostAlarms", value);
                }
                list.Remove(key);
                items = new string[list.Count];
                int i = 0;
                foreach (KeyValuePair<string, bool> kvp in list)
                    items[i++] = kvp.Key + "=" + kvp.Value.ToString();
                SetRealProp("Alarms", String.Join(";", items));
                LogUpdate.RemoveAlarm(key, this);
                LogUpdate.SendToAlarmLog(key, this, "Параметр вернулся в норму");
            }
        }

        public void RemoveAlarms()
        {
            LogUpdate.RemoveAlarms(this);
        }

        public void QuitAlarms(string username)
        {
            string value = (Reals.ContainsKey("Alarms")) ? Reals["Alarms"] : String.Empty;
            if (!String.IsNullOrWhiteSpace(value))
            {
                string[] items = value.Split(new char[] { ';' });
                IDictionary<string, bool> list = new Dictionary<string, bool>();
                foreach (string item in items)
                {
                    string[] vals = item.Split(new char[] { '=' });
                    list.Add(vals[0], true);
                }
                items = new string[list.Count];
                int i = 0;
                foreach (KeyValuePair<string, bool> kvp in list)
                {
                    items[i++] = kvp.Key + "=" + kvp.Value.ToString();
                    LogUpdate.SendToAlarmLog(kvp.Key, this, "Параметр квитировал " + username);
                }
                SetRealProp("Alarms", String.Join(";", items));
            }
            SetRealProp("LostAlarms", String.Empty);
        }

        public bool isQuitAlarms()
        {
            string value = (Reals.ContainsKey("Alarms")) ? Reals["Alarms"] : String.Empty;
            if (!String.IsNullOrWhiteSpace(value))
            {
                string[] items = value.Split(new char[] { ';' });
                IDictionary<string, bool> list = new Dictionary<string, bool>();
                foreach (string item in items)
                {
                    string[] vals = item.Split(new char[] { '=' });
                    if (vals.Length == 2) list.Add(vals[0], Boolean.Parse(vals[1]));
                }
                foreach (KeyValuePair<string, bool> kvp in list)
                    if (!list[kvp.Key]) return false;
            }
            return !isLostAlarms();
        }

        public bool isLostAlarms()
        {
            string value = (Reals.ContainsKey("LostAlarms")) ? Reals["LostAlarms"] : String.Empty;
            return value.Length > 0;
        }

        private decimal calcDB(int value)
        {
            decimal PVEUHi = FloatEx((string)Values["PVEUHi"]);
            decimal PVEULo = FloatEx((string)Values["PVEULo"]);
            switch (value)
            {
                case 2: return Math.Abs(PVEUHi - PVEULo) * 0.005m;
                case 3: return Math.Abs(PVEUHi - PVEULo) * 0.01m;
                case 4: return Math.Abs(PVEUHi - PVEULo) * 0.02m;
                case 5: return Math.Abs(PVEUHi - PVEULo) * 0.03m;
                case 6: return Math.Abs(PVEUHi - PVEULo) * 0.04m;
                case 7: return Math.Abs(PVEUHi - PVEULo) * 0.05m;
                default : return 0;
            }
        }

        public IDictionary<string, string> FetchAnalog()
        {
            if (Reals.ContainsKey("Raw") && Reals.ContainsKey("Quality"))
            {
                if (Reals["Quality"].ToString() == "GOOD")
                {
                    bool logged = (bool)Values["Logged"];
                    bool asked = (bool)Values["Asked"];
                    decimal PVSUHi = (Values.ContainsKey("PVSUHi")) ? (decimal)Values["PVSUHi"] : 100m;
                    decimal PVSULo = (Values.ContainsKey("PVSULo")) ? (decimal)Values["PVSULo"] : 0m;
                    decimal PVEUHi = FloatEx((string)Values["PVEUHi"]);
                    decimal PVEULo = FloatEx((string)Values["PVEULo"]);
                    decimal Koeff = (Values.ContainsKey("Koeff")) ? (decimal)Values["Koeff"] : 1m;
                    decimal Offset = (Values.ContainsKey("Offset")) ? (decimal)Values["Offset"] : 0;
                    int formatpv = (int)Values["FormatPV"];
                    decimal pv = 0m;
                    decimal lastpv;
                    if (Reals.ContainsKey("PV"))
                        lastpv = FloatEx(Reals["PV"]); 
                    else
                    {
                        lastpv = 0m;
                        SetRealProp("PV", lastpv.ToString());
                    }
                    SetRealProp("LastPV", lastpv.ToString());
                    bool quit;
                    if (Reals.ContainsKey("QuitAlarms"))
                        quit = Boolean.Parse(Reals["QuitAlarms"]);
                    else
                    {
                        quit = true;
                        SetRealProp("QuitAlarms", quit.ToString());
                    }
                    decimal raw = this.Plugin.FloatParse(Reals["Raw"].ToString());
                    SetRealProp("Raw", raw.ToString());
                    decimal D = Math.Abs(PVSUHi - PVSULo) * 0.25m;
                    decimal pvsens;
                    if (Math.Abs(PVSUHi - PVSULo) < 0.0001m)
                        pvsens = this.Plugin.EnsureRange(raw, PVSULo - D, PVSUHi + D);
                    else
                        pvsens = (this.Plugin.EnsureRange(raw, PVSULo - D, PVSUHi + D) - PVSULo) * 100m /
                            (PVSUHi - PVSULo);
                    raw = (PVEUHi - PVEULo) * (pvsens / 100m) + PVEULo;
                    D = Math.Abs(PVEUHi - PVEULo) * 0.25m;
                    pv = Math.Round((this.Plugin.EnsureRange(raw, PVEULo - D, PVEUHi + D) + Offset) * Koeff, formatpv);
                    SetRealProp("PV", pv.ToString());
                    SetRealProp("PVText", pv.ToString());
                    decimal percent;
                    if (Math.Abs(PVEUHi - PVEULo) < 0.0001m)
                        percent = 0;
                    else
                        percent = (pv - PVEULo) / (PVEUHi - PVEULo) * 100m;
                    SetRealProp("PVPercent", percent.ToString());
                    bool toup = pv > lastpv;
                    bool todown = pv < lastpv;
                    bool musttrend;
                    if (Reals.ContainsKey("TrendTime"))
                    {
                        double minutes = (DateTime.Now -
                            DateTime.Parse(Reals["TrendTime"])).TotalMinutes;
                        musttrend = (minutes >= 1);
                    }
                    else 
                        musttrend = true;
                    bool trend = (bool)Values["Trend"];
                    int tablekind = Values.ContainsKey("Table") ? (int)Values["Table"] : 0;
                    bool dotrend = (trend || tablekind > 0) && (musttrend || !musttrend && (toup || todown));
                    Decimal euhi = FloatEx((string)Values["PVEUHi"]);
                    Decimal eulo = FloatEx((string)Values["PVEULo"]);
                    Decimal hh = FloatEx((string)Values["PVHHTP"]);
                    Decimal hi = FloatEx((string)Values["PVHITP"]);
                    Decimal lo = FloatEx((string)Values["PVLOTP"]);
                    Decimal ll = FloatEx((string)Values["PVLLTP"]);
                    int ieuhi = EnumEx((string)Values["PVEUHi"]);
                    bool euhiactive = ieuhi > 0;
                    int ieulo = EnumEx((string)Values["PVEULo"]);
                    bool euloactive = ieulo > 0;
                    int ihh = EnumEx((string)Values["PVHHTP"]);
                    bool hhactive = ihh > 0;
                    int ihi = EnumEx((string)Values["PVHITP"]);
                    bool hiactive = ihi > 0;
                    int ilo = EnumEx((string)Values["PVLOTP"]);
                    bool loactive = ilo > 0;
                    int ill = EnumEx((string)Values["PVLLTP"]);
                    bool llactive = ill > 0;
                    #region расчёт алармов
                    if (logged)
                    {
                        if (pv >= eulo + calcDB(ieulo) && inAlarms("LE")) removeAlarm("LE");
                        if (pv > ll + calcDB(ill) && inAlarms("LL")) removeAlarm("LL");
                        if (pv > lo + calcDB(ilo) && inAlarms("LO")) removeAlarm("LO");
                        if (hiactive && pv >= hi && !inAlarms("HI")) addAlarm("HI");
                        if (hhactive && pv >= hh && !inAlarms("HH")) addAlarm("HH");
                        if (euhiactive && pv > euhi + calcDB(ieuhi) && !inAlarms("HE")) addAlarm("HE");

                        if (pv <= euhi - calcDB(ieuhi) && inAlarms("HE")) removeAlarm("HE");
                        if (pv < hh - calcDB(ihh) && inAlarms("HH")) removeAlarm("HH");
                        if (pv < hi - calcDB(ihi) && inAlarms("HI")) removeAlarm("HI");
                        if (loactive && pv <= lo && !inAlarms("LO")) addAlarm("LO");
                        if (llactive && pv <= ll && !inAlarms("LL")) addAlarm("LL");
                        if (euloactive && pv < eulo - calcDB(ieulo) && !inAlarms("LE")) addAlarm("LE");

                        if (isAlarms()) updateAlarmValue();
                    }
                    else
                    {
                        SetRealProp("Alarms", String.Empty);
                        SetRealProp("LostAlarms", String.Empty);
                    }
                    quit = (asked) ? isQuitAlarms() : true;
                    //-----------------------------------
                    Color backcolor, forecolor;
                    if (inAlarms("HE") || inAlarms("LE"))
                    {
                        backcolor = Color.Magenta;
                        forecolor = Color.White;
                    }
                    else if (inAlarms("HH") || inAlarms("LL"))
                    {
                        backcolor = Color.Red;
                        forecolor = Color.White;
                    }
                    else if (inAlarms("HI") || inAlarms("LO"))
                    {
                        backcolor = Color.Yellow;
                        forecolor = Color.Black;
                    }
                    else if (isLostAlarms() && asked)
                    {
                        backcolor = Color.LightGray;
                        forecolor = Color.Black;
                    }
                    else
                    {
                        backcolor = Color.Black;
                        forecolor = Color.White;
                    }
                    #endregion
                    SetRealProp("BackColor", backcolor.ToArgb().ToString());
                    SetRealProp("ForeColor", forecolor.ToArgb().ToString());
                    SetRealProp("QuitAlarms", quit.ToString());
                    bool isactive = hhactive || hiactive || loactive || llactive || euhiactive || euloactive;
                    SetRealProp("HasAlarms", (isactive) ? isAlarms().ToString() : false.ToString());
                    SetRealProp("HasLostAlarms", (isactive) ? isLostAlarms().ToString() : false.ToString());
                    if (dotrend)
                    {
                        LogUpdate.SendToTrend(this, "PV", true);
                        SetRealProp("TrendTime", DateTime.Now.ToString());
                        if (tablekind > 0)
                        {
                            LogUpdate.SendToTable(this, "PV", tablekind);
                            //------------------------------------------
                            IDictionary<string, double> tablevalues = LogUpdate.GetTableValues(this);
                            foreach (KeyValuePair<string, double> kvp in tablevalues)
                            {
                                if (!double.IsNaN(kvp.Value))
                                    SetRealProp(kvp.Key, Math.Round(kvp.Value, formatpv).ToString());
                            }
                        }
                    }
                }
            }
            return this.Reals;
        }

        public IDictionary<string, string> FetchAnalogParam()
        {
            if (Reals.ContainsKey("Raw") && Reals.ContainsKey("Quality"))
            {
                if (Reals["Quality"].ToString() == "GOOD")
                {
                    decimal PVSUHi = (Values.ContainsKey("OPSUHi")) ? (decimal)Values["OPSUHi"] : 100m;
                    decimal PVSULo = (Values.ContainsKey("OPSULo")) ? (decimal)Values["OPSULo"] : 0m;
                    decimal PVEUHi = (decimal)Values["OPEUHi"];
                    decimal PVEULo = (decimal)Values["OPEULo"];
                    decimal Koeff = (decimal)Values["Koeff"];
                    decimal Offset = (decimal)Values["Offset"];
                    int formatpv = (int)Values["FormatPV"];
                    decimal pv = 0m;
                    decimal lastpv;
                    if (Reals.ContainsKey("PV"))
                        lastpv = FloatEx(Reals["PV"]);
                    else
                    {
                        lastpv = 0m;
                        SetRealProp("PV", lastpv.ToString());
                    }
                    decimal raw = this.Plugin.FloatParse(Reals["Raw"].ToString());
                    SetRealProp("Raw", raw.ToString());
                    decimal D = Math.Abs(PVSUHi - PVSULo) * 0.25m;
                    decimal pvsens;
                    if (Math.Abs(PVSUHi - PVSULo) < 0.0001m)
                        pvsens = this.Plugin.EnsureRange(raw, PVSULo - D, PVSUHi + D);
                    else
                        pvsens = (this.Plugin.EnsureRange(raw, PVSULo - D, PVSUHi + D) - PVSULo) * 100m /
                            (PVSUHi - PVSULo);
                    raw = (PVEUHi - PVEULo) * (pvsens / 100m) + PVEULo;
                    D = Math.Abs(PVEUHi - PVEULo) * 0.25m;
                    pv = Math.Round((this.Plugin.EnsureRange(raw, PVEULo - D, PVEUHi + D) + Offset) * Koeff, formatpv);
                    SetRealProp("PV", pv.ToString());
                    SetRealProp("PVText", pv.ToString());
                    decimal percent;
                    if (Math.Abs(PVEUHi - PVEULo) < 0.0001m)
                        percent = 0;
                    else
                        percent = (pv - PVEULo) / (PVEUHi - PVEULo) * 100m;
                    SetRealProp("PVPercent", percent.ToString());
                }
            }
            return this.Reals;
        }

        public void DrawAnalogBar(Graphics g, Rectangle rect, float fontsize, decimal percent,
            decimal sppercent = Decimal.MinValue, decimal oppercent = decimal.MinValue)
        {
            int BarHeight = rect.Height;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            rect.Width -= 1; rect.Height -= 1;
            using (StringFormat strfrm = new StringFormat())
            {
                strfrm.Alignment = StringAlignment.Far;
                Rectangle labrect = rect;
                using (Font font = new Font("Courier New", fontsize))
                {
                    labrect.Width = (int)g.MeasureString("100%", font).Width + 1;
                    strfrm.LineAlignment = StringAlignment.Near;
                    g.DrawString("100%", font, Brushes.Silver, labrect, strfrm);
                    strfrm.LineAlignment = StringAlignment.Center;
                    g.DrawString("50%", font, Brushes.Silver, labrect, strfrm);
                    strfrm.LineAlignment = StringAlignment.Far;
                    g.DrawString("0%", font, Brushes.Silver, labrect, strfrm);
                }
                rect.X += labrect.Width; rect.Width -= labrect.Width;
                rect.Y += 8; rect.Height -= 17;
            }
            bool fullmode = Values.ContainsKey("PVEUHi") && Values.ContainsKey("PVEULo") &&
                Values.ContainsKey("PVHHTP") && Values.ContainsKey("PVHITP") &&
                Values.ContainsKey("PVLOTP") && Values.ContainsKey("PVLLTP");
            if (fullmode)
            {
                Decimal euhi = FloatEx((string)Values["PVEUHi"]);
                Decimal eulo = FloatEx((string)Values["PVEULo"]);
                Decimal range = euhi - eulo;
                Decimal hhps, hips, lops, llps;
                if (Math.Abs(range) > 0.0001m)
                {
                    hhps = (FloatEx((string)Values["PVHHTP"]) - eulo) / range;
                    hips = (FloatEx((string)Values["PVHITP"]) - eulo) / range;
                    lops = (FloatEx((string)Values["PVLOTP"]) - eulo) / range;
                    llps = (FloatEx((string)Values["PVLLTP"]) - eulo) / range;
                }
                else
                    hhps = hips = lops = llps = 0;
                bool euhiactive = EnumEx((string)Values["PVEUHi"]) > 0;
                bool hhactive = EnumEx((string)Values["PVHHTP"]) > 0;
                bool hiactive = EnumEx((string)Values["PVHITP"]) > 0;
                bool loactive = EnumEx((string)Values["PVLOTP"]) > 0;
                bool llactive = EnumEx((string)Values["PVLLTP"]) > 0;
                bool euloactive = EnumEx((string)Values["PVEULo"]) > 0;
                #region верхняя предупредительная уставка HiTP
                if (hiactive)
                {
                    Rectangle hirect = rect;
                    hirect.Height = (int)(hirect.Height * (1m - hips)) + rect.Y;
                    hirect.Y = 0;
                    using (SolidBrush brush = new SolidBrush(Color.Olive))
                        g.FillRectangle(brush, hirect);
                }
                #endregion
                #region нижняя предупредительная уставка LoTP
                if (loactive)
                {
                    Rectangle lorect = rect;
                    lorect.Height = (int)(lorect.Height * lops);
                    lorect.Y = rect.Y + rect.Height - lorect.Height;
                    lorect.Height += BarHeight - (lorect.Y + lorect.Height);
                    using (SolidBrush brush = new SolidBrush(Color.Olive))
                        g.FillRectangle(brush, lorect);
                }
                #endregion
                #region верхняя предаварийная уставка HHTP
                if (hhactive)
                {
                    Rectangle hhrect = rect;
                    hhrect.Height = (int)(hhrect.Height * (1m - hhps)) + rect.Y;
                    hhrect.Y = 0;
                    using (SolidBrush brush = new SolidBrush(Color.DarkRed))
                        g.FillRectangle(brush, hhrect);
                }
                #endregion
                #region нижняя предаварийная уставка LLTP
                if (llactive)
                {
                    Rectangle llrect = rect;
                    llrect.Height = (int)(llrect.Height * llps);
                    llrect.Y = rect.Y + rect.Height - llrect.Height;
                    llrect.Height += BarHeight - (llrect.Y + llrect.Height);
                    using (SolidBrush brush = new SolidBrush(Color.DarkRed))
                        g.FillRectangle(brush, llrect);
                }
                #endregion
                #region верхняя граница шкалы EUHiTP
                if (euhiactive)
                {
                    Rectangle euhirect = rect;
                    euhirect.Height = rect.Y;
                    euhirect.Y = 0;
                    using (SolidBrush brush = new SolidBrush(Color.DarkMagenta))
                        g.FillRectangle(brush, euhirect);
                }
                #endregion
                #region нижняя граница шкалы EULoTP
                if (euloactive)
                {
                    Rectangle eulorect = rect;
                    eulorect.Y = rect.Y + rect.Height;
                    eulorect.Height += BarHeight - (eulorect.Y + eulorect.Height);
                    using (SolidBrush brush = new SolidBrush(Color.DarkMagenta))
                        g.FillRectangle(brush, eulorect);
                }
                #endregion
            }
            #region отрисовка бара значения
            Rectangle valrect = rect;
            valrect.Inflate(-25, 0);
            int h = BarHeight - (rect.Y + rect.Height);
            valrect.Height = (int)(rect.Height * percent) + h;
            valrect.Y = rect.Y + rect.Height - valrect.Height + h;
            valrect.Inflate(0, -1);
            using (Pen pen = new Pen(Color.Lime))
            {
                pen.Width = 3;
                int offset = 8;
                g.DrawLines(pen, new Point[]
                { new Point(valrect.Left, valrect.Bottom),
                  new Point(valrect.Left + offset, valrect.Bottom),
                  new Point(valrect.Left + offset, valrect.Top),
                  new Point(valrect.Right - offset, valrect.Top),
                  new Point(valrect.Right - offset, valrect.Bottom),
                  new Point(valrect.Right, valrect.Bottom)});
            }
            #endregion
            #region отрисовка бара выхода
            if (oppercent > Decimal.MinValue)
            {
                valrect = rect;
                valrect.Width = 17;
                valrect.X = rect.X + rect.Width - valrect.Width - 3;
                h = BarHeight - (rect.Y + rect.Height);
                valrect.Height = (int)(rect.Height * oppercent) + h;
                valrect.Y = rect.Y + rect.Height - valrect.Height + h;
                valrect.Inflate(0, -1);
                using (Pen pen = new Pen(Color.Yellow))
                {
                    pen.Width = 3;
                    int offset = 8;
                    g.DrawLines(pen, new Point[]
                        { new Point(valrect.Left, valrect.Bottom),
                          new Point(valrect.Left + offset, valrect.Bottom),
                          new Point(valrect.Left + offset, valrect.Top),
                          new Point(valrect.Right - offset, valrect.Top),
                          new Point(valrect.Right - offset, valrect.Bottom),
                          new Point(valrect.Right, valrect.Bottom)});
                        }
            }
            #endregion
            #region отрисовка указателя задания
            if (sppercent > Decimal.MinValue)
            {
                valrect = rect;
                valrect.Inflate(-25, 0);
                h = BarHeight - (rect.Y + rect.Height);
                valrect.Height = (int)(rect.Height * sppercent) + h + 1;
                valrect.Y = rect.Y + rect.Height - valrect.Height + h;
                valrect.Inflate(0, -1);
                using (Brush brush = new SolidBrush(Color.Cyan))
                {
                    int offset = 8;
                    g.FillPolygon(brush, new Point[]
                { new Point(valrect.Left + offset, valrect.Top),
                  new Point(valrect.Left, valrect.Top - 5),
                  new Point(valrect.Left, valrect.Top + 5)});
                }
            }
            #endregion
            #region отрисовка линий 0, 25, 50, 75, 100%
            using (Pen pen = new Pen(Color.Silver, 1))
            {
                pen.DashStyle = DashStyle.Dash;
                g.DrawLine(pen, new Point(rect.X, rect.Y), new Point(rect.X + rect.Width, rect.Y));
                g.DrawLine(pen, new Point(rect.X, rect.Y + rect.Height / 4),
                    new Point(rect.X + rect.Width, rect.Y + rect.Height / 4));
                g.DrawLine(pen, new Point(rect.X, rect.Y + rect.Height / 2),
                    new Point(rect.X + rect.Width, rect.Y + rect.Height / 2));
                g.DrawLine(pen, new Point(rect.X, rect.Y + 3 * rect.Height / 4 + 1),
                    new Point(rect.X + rect.Width, rect.Y + 3 * rect.Height / 4 + 1));
                g.DrawLine(pen, new Point(rect.X, rect.Y + rect.Height),
                    new Point(rect.X + rect.Width, rect.Y + rect.Height));
            }
            #endregion
        }

        public IDictionary<string, string> FetchDigital()
        {
            if (Reals.ContainsKey("Raw") && Reals.ContainsKey("Quality"))
            {
                if (Reals["Quality"].ToString() == "GOOD")
                {
                    bool logged = (bool)Values["Logged"];
                    bool asked = (bool)Values["Asked"];
                    string textUp = (string)Values["TextUp"];
                    string textDown = (string)Values["TextDown"];
                    int colorUp = (int)Values["ColorUp"];
                    int colorDown = (int)Values["ColorDown"];
                    bool invert = (bool)Values["Invert"];
                    bool haactive = (bool)Values["AlarmUp"];
                    bool laactive = (bool)Values["AlarmDown"];
                    bool hsactive = (bool)Values["SwitchUp"];
                    bool lsactive = (bool)Values["SwitchDown"];
                    string raw = Reals["Raw"].ToString();
                    bool rawbool;
                    double rawdouble;
                    bool pv = false;
                    bool lastpv;
                    if (Reals.ContainsKey("PV"))
                        lastpv = Boolean.Parse(Reals["PV"]);
                    else
                    {
                        lastpv = false;
                        SetRealProp("PV", lastpv.ToString());
                    }
                    // raw может быть тип "bool" или любой числовой, тип "текст" пропускаем
                    if (bool.TryParse(raw, out rawbool))
                        pv = (invert) ? !rawbool : rawbool;
                    else if (double.TryParse(raw, out rawdouble))
                    {
                        rawbool = (rawdouble > 0) ? true : false;
                        pv = (invert) ? !rawbool : rawbool;
                    }
                    SetRealProp("PV", pv.ToString());
                    SetRealProp("PVText", (pv) ? textUp : textDown);
                    int colorIndex = (pv) ? colorUp : colorDown;
                    SetRealProp("PVColor", colorIndex.ToString());
                    #region расчёт алармов
                    if (hsactive && pv && !inSwitchs("HS"))
                    {
                        addSwitch("HS");
                        LogUpdate.SendToSwitchLog("HS", this, lastpv, pv);
                    }
                    if (!pv && inSwitchs("HS")) removeSwitch("HS");
                    if (lsactive && !pv && !inSwitchs("LS"))
                    {
                        addSwitch("LS");
                        LogUpdate.SendToSwitchLog("LS", this, lastpv, pv);
                    }
                    if (pv && inSwitchs("LS")) removeSwitch("LS");
                    if (logged)
                    {
                        if (pv && inAlarms("LA")) removeAlarm("LA");
                        if (!pv && inAlarms("HA")) removeAlarm("HA");
                        if (haactive && pv && !inAlarms("HA")) addAlarm("HA");
                        if (laactive && !pv && !inAlarms("LA")) addAlarm("LA");
                    }
                    else
                    {
                        SetRealProp("Alarms", String.Empty);
                        SetRealProp("LostAlarms", String.Empty);
                    }
                    #endregion
                    bool quit = (asked) ? isQuitAlarms() : true;
                    //-----------------------------------
                    Color backcolor, forecolor;
                    if (inAlarms("HA") || inAlarms("LA"))
                    {
                        backcolor = Color.Red;
                        forecolor = Color.White;
                    }
                    else if (isLostAlarms() && asked)
                    {
                        backcolor = Color.LightGray;
                        forecolor = Color.Black;
                    }
                    else
                    {
                        backcolor = Color.Black;
                        forecolor = Color.White;
                    }
                    SetRealProp("BackColor", backcolor.ToArgb().ToString());
                    SetRealProp("ForeColor", forecolor.ToArgb().ToString());
                    SetRealProp("QuitAlarms", quit.ToString());
                    bool isactive = haactive || laactive;
                    SetRealProp("HasAlarms", (isactive) ? isAlarms().ToString() : false.ToString());
                    SetRealProp("HasLostAlarms", (isactive) ? isLostAlarms().ToString() : false.ToString());
                }
            }
            return Reals;
        }

        public IDictionary<string, string> FetchDigitalParam()
        {
            if (Reals.ContainsKey("Raw") && Reals.ContainsKey("Quality"))
            {
                if (Reals["Quality"].ToString() == "GOOD")
                {
                    string textUp = "ВКЛ";
                    string textDown = "ОТКЛ";
                    int colorUp = 10;
                    int colorDown = 6;
                    bool invert = (bool)Values["Invert"];
                    string raw = Reals["Raw"].ToString();
                    bool rawbool;
                    double rawdouble;
                    bool pv = false;
                    bool lastpv;
                    if (Reals.ContainsKey("PV"))
                        lastpv = Boolean.Parse(Reals["PV"]);
                    else
                    {
                        lastpv = false;
                        SetRealProp("PV", lastpv.ToString());
                    }
                    // raw может быть тип "bool" или любой числовой, тип "текст" пропускаем
                    if (bool.TryParse(raw, out rawbool))
                        pv = (invert) ? !rawbool : rawbool;
                    else if (double.TryParse(raw, out rawdouble))
                    {
                        rawbool = (rawdouble > 0) ? true : false;
                        pv = (invert) ? !rawbool : rawbool;
                    }
                    SetRealProp("PV", pv.ToString());
                    SetRealProp("PVText", (pv) ? textUp : textDown);
                    int colorIndex = (pv) ? colorUp : colorDown;
                    SetRealProp("PVColor", colorIndex.ToString());
                }
            }
            return Reals;
        }

        public void SetRealProp(string name, string value)
        {
            if (Reals.ContainsKey(name)) Reals[name] = value;
            else Reals.Add(name, value);
        }

        public Form Passport(ImportRealValues updater, ExportRealValues saver)
        {
            return Plugin.Passport(this, updater, saver);
        }

        public decimal FloatEx(string value)
        {
            return Plugin.FloatEx(value);
        }
        public int EnumEx(string value)
        {
            return Plugin.EnumEx(value);
        }
        public Color BaseColor(int colorindex)
        {
            return Plugin.BaseColor(colorindex);
        }
 
        public bool GetBit(ushort value, int index)
        {
            Int32[] uarr = new Int32[1];
            uarr[0] = value;
            BitArray ba = new BitArray(uarr);
            return ba.Get(index);
        }

        public void SetBit(ref ushort value, int index, bool bit)
        {
            Int32[] uarr = new Int32[1];
            uarr[0] = value;
            BitArray ba = new BitArray(uarr);
            ba.Set(index, bit);
            ba.CopyTo(uarr, 0);
            value = (ushort)uarr[0];
        }

    }
    #endregion

    public delegate string ChildDesc(string name);
    public struct FineRow
    {
        public string FineKey;
        public string FineValue;
        public string Key;
        public object Value;
        public int PropType;
        public FineRow(string finekey, string finevalue, string key, object value, int proptype)
        {
            FineKey = finekey;
            FineValue = finevalue;
            Key = key;
            Value = value;
            PropType = proptype;
        }
    }

    public interface IPointPlugin
    {
        string PluginName();
        string PluginVersion { get; }
        string PluginAuthor { get; }
        int PluginType { get; }
        string PluginShortType { get; }
        string PluginDescriptor { get; }
        string PluginCategory { get; }
        IDictionary<string, object> PropValueDefs();
        Func<string, string> GetPropDesc { get; }
        Func<string, int> GetPropType { get; }
        Color GetIconColor { get; }
        string[] GetFilterChildTypes { get; }
        string GetFineValue(string propname, object propvalue);
        string GetFineValue(string propname, object propvalue, int ptformat);
        string GetEnumValue(string propName, object propValue);
        Func<string, IDictionary<string, object>, ChildDesc, List<FineRow>> GetFinePropList { get; }
        List<string> GetEnumItems(string propname);
        int[] GetIntPropRanges(string propname);
        IDictionary<string, string> Fetch(string ptname, Entity entity);
        Form Passport(Entity entity, ImportRealValues import, ExportRealValues export);
        decimal FloatEx(string value);
        int EnumEx(string value);
        decimal FloatParse(string value);
        decimal EnsureRange(decimal value, decimal low, decimal high);
        Color BaseColor(int colorindex);
    }
    public interface IPointConnect { Entity Point { get; set; } }
    public interface ILogUpdate
    {
        void AddAlarm(string type, Entity point, string message);
        void UpdateAlarmValue(Entity point);
        void RemoveAlarm(string type, Entity point);
        void RemoveAlarms(Entity point);
        void AddSwitch(string type, Entity point);
        void RemoveSwitch(string type, Entity point);
        void RemoveSwitchs(Entity point);
        void SendToAlarmLog(string type, Entity point, string message);
        void SendToSwitchLog(string type, Entity point, bool oldvalue, bool newvalue);
        void SendToChangeLog(Entity point, string param, string oldvalue, string newvalue, string autor);
        void SendToSystemLog(int station, string posname, string parname);
        void SendToTrend(Entity point, string parname, bool kind);
        void SendToTable(Entity point, string parname, int tablekind);
        IDictionary<string, double> GetTableValues(Entity point);
    }
    public interface IViewUpdate
    {
        void UpdateView();
    }
    public interface IUserInfo
    {
        List<Пользователь> GetUserList(UserLevel style);
        void AddNewUser(Пользователь user);
        Пользователь FindByFullName(string fullname);
        void ChangeUser(Пользователь user, Пользователь source);
        void DeleteUser(Пользователь user);
        void LoginUser(string fullname, string shortname, UserLevel style);
        void ResetLogin();
        bool UserLogged();
        string CurrentUserFullname();
    }

}
