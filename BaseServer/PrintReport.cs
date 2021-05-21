using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using Points.Plugins;
using System.Globalization;
using IniFiles.Net;

namespace BaseServer
{
    public class PrintReport
    {
        public string ReportName { get; set; }
        public string Descriptor  { get; set; }
        private string printername = String.Empty;
        public string PrinterName
        {
            get { return printername; }
            set
            {
                printername = printDocument.PrinterSettings.PrinterName;
                foreach (string strPrinter in PrinterSettings.InstalledPrinters)
                if (strPrinter == value)
                {
                    printername = value;
                    break;
                }
            }
        }
        private bool selectPrinter { get; set; }
        public Rectangle PageRect { get; set; }
        public Rectangle DrawRect { get; set; }
        public List<Plot> PlotList;
        public List<Plot> SelList;
        public bool DragByRect = false;
        public Rectangle SelRect = new Rectangle();
        PrintDocument printDocument;
        private DateTime reporttime = DateTime.Now;
        public DateTime PrintDate
        {
            get
            {
                return reporttime;
            }
            set
            {
                reporttime = value;
                Update();
            }
        }
        public bool PrintAuto { get; set; }
        public DateTime PrintTime { get; set; }
        public int PrintPeriod { get; set; }

        public PrintReport(PrintDocument prtdoc)
        {
            this.PrintAuto = false;
            this.PrintTime = DateTime.Parse("08:00:00");
            this.PrintPeriod = 0;
            printDocument = prtdoc;
            PrinterName = prtdoc.PrinterSettings.PrinterName; //"HP LaserJet P1005";
            this.Descriptor = String.Empty;
            selectPrinter = false;
            PlotList = new List<Plot>();
            SelList = new List<Plot>();
        }

        public void LoadReport(string reportname, string reportsconfig)
        {
            if (reportname.Length > 0)
            {
                ReportName = reportname;
                byte[] image = Data.GetReportData(reportname);
                if (image != null)
                {
                    MemIniFile mif = new MemIniFile(String.Empty);
                    mif.FromString(reportsconfig);
                    this.PrintAuto = mif.ReadBool(reportname, "PrintAuto", false);
                    this.PrintTime = mif.ReadDate(reportname, "PrintTime", DateTime.Parse("08:05:00"));
                    this.PrintPeriod = mif.ReadInteger(reportname, "PrintPeriod", 0);
                    //------------------------------------------
                    SelList.Clear();
                    PlotList.Clear();
                    GC.Collect();
                    string content = Encoding.Unicode.GetString(image);
                    PrinterSettings ps = printDocument.PrinterSettings;
                    string[] lines = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    ImportLines(ref ps, lines);
                    printDocument.PrinterSettings = ps;
                    Update();
                }
            }
        }

        private string GetHourData(string name, int daybefore, int hour, DateTime dt, ServerSQL mySQL,
            int formatPV)
        {
            string frm = String.Format("dd.MM.yyyy 00:00:00");
            string sdate = dt.ToString(frm);
            DateTime result;
            if (DateTime.TryParse(sdate, out result))
                return Data.GetHourData(name, result.AddDays(-daybefore).AddHours(hour), mySQL, formatPV);
            else
                return String.Empty;
        }

        private string GetHourAgregate(string name, int daybefore, int hour, DateTime dt, ServerSQL mySQL,
            int formatPV, int count, int agregate)
        {
            List<Double> arr = new List<double>();
            string frm = String.Format("dd.MM.yyyy 00:00:00");
            string sdate = dt.ToString(frm);
            DateTime result;
            if (DateTime.TryParse(sdate, out result))
            {
                Double value = Data.GetHourData(name, result.AddDays(-daybefore).AddHours(hour), mySQL);
                if (!Double.IsNaN(value)) arr.Add(value);
                int snap = hour;
                for (int i = 1; i < count; i++)
                {
                    snap++;
                    if (DateTime.TryParse(sdate, out result))
                    {
                        value = Data.GetHourData(name, result.AddDays(-daybefore).AddHours(snap), mySQL);
                        if (!Double.IsNaN(value)) arr.Add(value);
                    }
                }
                if (arr.Count > 0)
                {
                    switch (agregate)
                    {
                        case 1: // сумма
                            value = arr.ToArray().Sum();
                            return Double.IsNaN(value) ? "------" : Data.Float(value, formatPV);
                        case 2: // минимум
                            value = arr.ToArray().Min();
                            return Double.IsNaN(value) ? "------" : Data.Float(value, formatPV);
                        case 3: // максимум
                            value = arr.ToArray().Max();
                            return Double.IsNaN(value) ? "------" : Data.Float(value, formatPV);
                        case 4: // среднее
                            value = arr.ToArray().Average();
                            return Double.IsNaN(value) ? "------" : Data.Float(value, formatPV);
                        default: // не реализовано
                            return "Агрегат?";
                    }
                }
                else
                    return "------";
            }
            else
                return String.Empty;
        }


        private string GetDailyData(string name, int monthbefore, int day, DateTime dt, ServerSQL mySQL,
            int formatPV)
        {
            string frm = String.Format("{0}.MM.yyyy 00:00:00", day);
            string sdate = dt.AddMonths(-monthbefore).ToString(frm);
            DateTime result;
            if (DateTime.TryParse(sdate, out result))
                return Data.GetDailyData(name, result, mySQL, formatPV);
            else
                return String.Empty;
        }

        private string GetDailyAgregate(string name, int monthbefore, int day, DateTime dt, ServerSQL mySQL,
            int formatPV, int count, int agregate)
        {
            List<Double> arr = new List<Double>();
            string frm = String.Format("{0}.MM.yyyy 00:00:00", day);
            string sdate = dt.AddMonths(-monthbefore).ToString(frm);
            DateTime result;
            if (DateTime.TryParse(sdate, out result))
            {
                Double value = Data.GetDailyData(name, result.AddDays(day - 1), mySQL);
                if (!Double.IsNaN(value)) arr.Add(value);
                int snap = day;
                for (int i = 1; i < count; i++)
                {
                    snap++;
                    if (DateTime.TryParse(sdate, out result))
                    {
                        value = Data.GetDailyData(name, result.AddDays(snap - 1), mySQL);
                        if (!Double.IsNaN(value)) arr.Add(value);
                    }
                }
                if (arr.Count > 0)
                {
                    switch (agregate)
                    {
                        case 1: // сумма
                            value = arr.ToArray().Sum();
                            return Double.IsNaN(value) ? "------" : Data.Float(value, formatPV);
                        case 2: // минимум
                            value = arr.ToArray().Min();
                            return Double.IsNaN(value) ? "------" : Data.Float(value, formatPV);
                        case 3: // максимум
                            value = arr.ToArray().Max();
                            return Double.IsNaN(value) ? "------" : Data.Float(value, formatPV);
                        case 4: // среднее
                            value = arr.ToArray().Average();
                            return Double.IsNaN(value) ? "------" : Data.Float(value, formatPV);
                        default: // не реализовано
                            return "Агрегат?";
                    }
                }
                else
                    return "------";
            }
            else
                return String.Empty;
        }

        private string GetMonthData(string name, int yearbefore, int month, DateTime dt, ServerSQL mySQL,
            int formatPV)
        {
            string frm = String.Format("01.{0}.yyyy 00:00:00", month);
            string sdate = dt.AddYears(-yearbefore).ToString(frm);
            DateTime result;
            if (DateTime.TryParse(sdate, out result))
                return Data.GetMonthData(name, result, mySQL, formatPV);
            else
                return String.Empty;
        }

        private string GetMonthAgregate(string name, int yearbefore, int month, DateTime dt, ServerSQL mySQL,
            int formatPV, int count, int agregate)
        {
            List<Double> arr = new List<double>();
            string frm = String.Format("01.01.yyyy 00:00:00");
            string sdate = dt.AddYears(-yearbefore).ToString(frm);
            DateTime result;
            if (DateTime.TryParse(sdate, out result))
            {
                Double value = Data.GetMonthData(name, result.AddMonths(month - 1), mySQL);
                if (!Double.IsNaN(value)) arr.Add(value);
                int snap = month;
                for (int i = 1; i < count; i++)
                {
                    snap++;
                    if (DateTime.TryParse(sdate, out result))
                    {
                        value = Data.GetMonthData(name, result.AddMonths(snap - 1), mySQL);
                        if (!Double.IsNaN(value)) arr.Add(value);
                    }
                }
                if (arr.Count > 0)
                {
                    switch (agregate)
                    {
                        case 1: // сумма
                            value = arr.ToArray().Sum();
                            return Double.IsNaN(value) ? "------" : Data.Float(value, formatPV);
                        case 2: // минимум
                            value = arr.ToArray().Min();
                            return Double.IsNaN(value) ? "------" : Data.Float(value, formatPV);
                        case 3: // максимум
                            value = arr.ToArray().Max();
                            return Double.IsNaN(value) ? "------" : Data.Float(value, formatPV);
                        case 4: // среднее
                            value = arr.ToArray().Average();
                            return Double.IsNaN(value) ? "------" : Data.Float(value, formatPV);
                        default: // не реализовано
                            return "Агрегат?";
                    }
                }
                else
                    return "------";
            }
            else
                return String.Empty;
        }

        private string BuildInFunc(Plot plt, string name, DateTime dt, ServerSQL dataSQL)
        {
            Calendar cal;
            int formatPV;
            switch (name.ToUpper())
            {
                case "PRINTTIME": return DateTime.Now.ToString("dd.MM.yyyy ddd HH:mm:ss");
                case "PRINTDAYSTART": return dt.ToString("dd.MM.yyyy ddd 00:00:00");
                case "PRINTDAYFINISH": return dt.ToString("dd.MM.yyyy ddd 23:59:59");
                case "PRINTSHIFT1START": return dt.ToString("dd.MM.yyyy ddd 08:00:00");
                case "PRINTSHIFT1FINISH": return dt.ToString("dd.MM.yyyy ddd 19:59:59");
                case "PRINTPREVDAYSTART": return dt.AddDays(-1).ToString("dd.MM.yyyy ddd 00:00:00");
                case "PRINTPREVDAYFINISH": return dt.AddDays(-1).ToString("dd.MM.yyyy ddd 23:59:59");
                case "PRINTSHIFT2START": return dt.AddDays(-1).ToString("dd.MM.yyyy ddd 20:00:00");
                case "PRINTSHIFT2FINISH": return dt.ToString("dd.MM.yyyy ddd 07:59:59");
                case "PRINTMONTHSTART": return dt.ToString("01.MM.yyyy ddd 00:00:00");
                case "PRINTMONTHFINISH":
                    cal = new GregorianCalendar(GregorianCalendarTypes.Localized);
                    return dt.ToString(String.Format("{0}.MM.yyyy ddd 23:59:59",
                        cal.GetDaysInMonth(dt.Year, dt.Month)));
                case "PRINTPREVMONTHSTART": return dt.AddMonths(-1).ToString("01.MM.yyyy ddd 00:00:00");
                case "PRINTPREVMONTHFINISH":
                    cal = new GregorianCalendar(GregorianCalendarTypes.Localized);
                    return dt.AddMonths(-1).ToString(String.Format("{0}.MM.yyyy ddd 23:59:59",
                        cal.GetDaysInMonth(dt.AddMonths(-1).Year, dt.AddMonths(-1).Month)));
                default:
                    string value = name.ToUpper();
                    if (value.StartsWith("HOUR(") && value.EndsWith(")")) // [{HOUR(N, POINT.PV)}] где N - offset (часов)
                    {
                        string[] args =
                            value.Substring(5, value.IndexOf(')') - 5).Split(new char[] { ',' });
                        if (args.Length == 2)
                        {
                            double offset;
                            formatPV = getFormatPV(args[1].Trim(), plt, dataSQL);
                            if (double.TryParse(args[0].Trim(), out offset))
                            {
                                return Data.GetHourData(args[1].Trim(), dt.AddHours(-offset), dataSQL,
                                    formatPV);
                            }
                            else
                                return value.Substring(0, value.IndexOf(',') - 1) + "?";
                        }
                        else
                            return "HOUR(?,?)";
                    }
                    else if (value.StartsWith("DAILY(") && value.EndsWith(")")) // [{DAILY(N, POINT.PV)}] где N - offset (дней)
                    {
                        string[] args =
                            value.Substring(6, value.IndexOf(')') - 6).Split(new char[] { ',' });
                        if (args.Length == 2)
                        {
                            double offset;
                            formatPV = getFormatPV(args[1].Trim(), plt, dataSQL);
                            if (double.TryParse(args[0].Trim(), out offset))
                            {
                                return Data.GetDailyData(args[1].Trim(), dt.AddDays(-offset), dataSQL,
                                    formatPV);
                            }
                            else
                                return value.Substring(0, value.IndexOf(',') - 1) + "?";
                        }
                        else
                            return "DAILY(?,?)";
                    }
                    else if (value.StartsWith("MONTH(") && value.EndsWith(")")) // [{MONTH(N, POINT.PV)}] где N - offset (месяцев)
                    {
                        string[] args =
                            value.Substring(6, value.IndexOf(')') - 6).Split(new char[] { ',' });
                        if (args.Length == 2)
                        {
                            int offset;
                            formatPV = getFormatPV(args[1].Trim(), plt, dataSQL);
                            if (int.TryParse(args[0].Trim(), out offset))
                            {
                                return Data.GetMonthData(args[1].Trim(), dt.AddMonths(-offset), dataSQL,
                                    formatPV);
                            }
                            else
                                return value.Substring(0, value.IndexOf(',') - 1) + "?";
                        }
                        else
                            return "MONTH(?,?)";
                    }
                    return "{" + name + "?}";
            }
        }

        private int getFormatPV(string name, Plot plt, ServerSQL dataSQL)
        {
            int formatPV = 3;
            string[] items = name.Split(new char[] { '.' });
            string ptname = items[0];
            string parname = (items.Length == 2) ? items[1] : "PV";
            Entity ent = Data.GetEntity(ptname, dataSQL);
            if (!ent.Empty && ent.Values.ContainsKey("FormatPV"))
            {
                formatPV = (int)ent.Values["FormatPV"];
                int index = PlotList.IndexOf(plt);
                if (index >= 0)
                {
                    PlotList[index].SetPropValue("FormatPV", formatPV);
                }
            }
            return formatPV;
        }

        public void Update()
        {
            int snap = 0;
            int offset = 0;
            int count = 1;
            int formatPV = 3;
            int agregate = 0;

            using (ServerSQL dataSQL = new ServerSQL(DatabaseFrom.Database, false)) // чтение удалённо
            {
                using (ServerSQL fetchSQL = new ServerSQL(DatabaseFrom.Fetchbase, true)) // чтение
                {
                    if (dataSQL.Connected && fetchSQL.Connected)
                    {
                        foreach (Plot plt in PlotList)
                        {
                            if (plt.Kind == 1 || plt.Kind > 1 && plt.Props.ContainsKey("PtName"))
                            {
                                string name = (string)plt.Props["PtName"];
                                switch (plt.Kind)
                                {
                                    case 1: // простой текст с элементами подстановки [{...}]
                                        string text = (string)plt.Props["Text"];
                                        if (text.Length > 0 && text.IndexOf("[{") >= 0)
                                        {
                                            while (text.IndexOf("[{") >= 0 && text.IndexOf("}]") >= 0)
                                            {
                                                int first = text.IndexOf("[{");
                                                string begin = text.Substring(0, first);
                                                int last = text.IndexOf("}]");
                                                string end = text.Substring(last + 2, text.Length - last - 2);
                                                string match = text.Substring(first + 2, last - first - 2);
                                                text = begin + BuildInFunc(plt, match, reporttime, dataSQL) + end;
                                            }
                                            plt.SetPropValue("TextExt", text);
                                        }
                                        break;
                                    case 2: // текущие значения из базы опроса
                                        plt.Props["Text"] = name + "?";
                                        string[] items = name.Split(new char[] { '.' });
                                        string ptname = items[0];
                                        string parname = (items.Length == 2) ? items[1] : "PV";
                                        Entity ent = Data.GetEntity(ptname, dataSQL);
                                        if (!ent.Empty)
                                        {
                                            ent.Reals = Data.GetRealValues(ptname, fetchSQL, dataSQL);
                                            if (ent.Reals.Count > 0)
                                            {
                                                if (ent.Reals.ContainsKey(parname))
                                                    plt.Props["Text"] = ent.Reals[parname];
                                                else if (ent.Values.ContainsKey(parname))
                                                {
                                                    plt.Props["Text"] =
                                                        ent.Plugin.GetFineValue(parname, ent.Values[parname]);
                                                }
                                                else
                                                    plt.Props["Text"] = "." + parname + "?";
                                            }
                                        }
                                        break;
                                    case 3: // часовые накопления из таблицы
                                        plt.Props["Text"] = name + "?";
                                        formatPV = getFormatPV(name, plt, dataSQL);
                                        snap = (int)plt.Props["Snap"];
                                        offset = (int)plt.Props["Offset"];
                                        count = (int)plt.Props["ColumnLines"];
                                        agregate = (int)plt.Props["Agregate"];
                                        if (agregate == 0)
                                        {
                                            plt.Props["Text"] =
                                                GetHourData(name, offset, snap, reporttime, dataSQL,
                                                formatPV); // смещение суток и час
                                            for (int i = 1; i < count; i++)
                                            {
                                                snap++;
                                                plt.SetPropValue("Text" + i,
                                                    GetHourData(name, offset, snap, reporttime, dataSQL,
                                                    formatPV));
                                            }
                                        }
                                        else
                                            plt.Props["Text"] =
                                                GetHourAgregate(name, offset, snap, reporttime, dataSQL,
                                                formatPV, count, agregate);
                                        break;
                                    case 4: // суточные накопления из таблицы
                                        plt.Props["Text"] = name + "?";
                                        formatPV = getFormatPV(name, plt, dataSQL);
                                        snap = (int)plt.Props["Snap"];
                                        offset = (int)plt.Props["Offset"];
                                        count = (int)plt.Props["ColumnLines"];
                                        agregate = (int)plt.Props["Agregate"];
                                        if (agregate == 0)
                                        {
                                            plt.Props["Text"] =
                                                GetDailyData(name, offset, snap, reporttime, dataSQL,
                                                formatPV); // смещение месяца и день
                                            for (int i = 1; i < count; i++)
                                            {
                                                snap++;
                                                plt.SetPropValue("Text" + i,
                                                    GetDailyData(name, offset, snap, reporttime, dataSQL,
                                                    formatPV));
                                            }
                                        }
                                        else
                                            plt.Props["Text"] =
                                                GetDailyAgregate(name, offset, snap, reporttime, dataSQL,
                                                formatPV, count, agregate);
                                        break;
                                    case 5: // месячные накопления из таблицы
                                        plt.Props["Text"] = name + "?";
                                        formatPV = getFormatPV(name, plt, dataSQL);
                                        snap = (int)plt.Props["Snap"];
                                        offset = (int)plt.Props["Offset"];
                                        count = (int)plt.Props["ColumnLines"];
                                        agregate = (int)plt.Props["Agregate"];
                                        if (agregate == 0)
                                        {
                                            plt.Props["Text"] =
                                                GetMonthData(name, offset, snap, reporttime, dataSQL,
                                                formatPV); // смещение года и месяц
                                            for (int i = 1; i < count; i++)
                                            {
                                                snap++;
                                                plt.SetPropValue("Text" + i,
                                                    GetMonthData(name, offset, snap, reporttime, dataSQL,
                                                    formatPV));
                                            }
                                        }
                                        else
                                            plt.Props["Text"] =
                                                GetMonthAgregate(name, offset, snap, reporttime, dataSQL,
                                                formatPV, count, agregate);
                                        break;
                                }

                            }
                        }
                    }
                }
            }
        }

        public void ImportLines(ref PrinterSettings ps, string[] lines)
        {
            bool report = false;
            string name = String.Empty;
            Plot plt = null;
            SelList.Clear();
            PlotList.Clear();
            foreach (string line in lines)
            {
                if (!String.IsNullOrWhiteSpace(line))
                {
                    if (line.Equals("[Report]")) report = true;
                    else if (report)
                    {
                        string[] items = line.Split(new char[] { '=' });
                        switch (items[0])
                        {
                            case "Descriptor":
                                Descriptor = items[1];
                                break;
                            case "ReportName": ReportName = items[1]; break;
                            case "PrinterName":
                                ps.PrinterName = PrinterName = items[1];
                                break;
                            case "PaperName":
                                SetPaperSize(ref ps, items[1]);
                                break;
                            case "Landscape":
                                int landscape;
                                if (int.TryParse(items[1], out landscape))
                                    ps.DefaultPageSettings.Landscape = landscape > 0;
                                break;
                            case "Left":
                                int left;
                                if (int.TryParse(items[1], out left))
                                    ps.DefaultPageSettings.Margins.Left = left;
                                break;
                            case "Right":
                                int right;
                                if (int.TryParse(items[1], out right))
                                    ps.DefaultPageSettings.Margins.Right = right;
                                break;
                            case "Top":
                                int top;
                                if (int.TryParse(items[1], out top))
                                    ps.DefaultPageSettings.Margins.Top = top;
                                break;
                            case "Bottom":
                                int bottom;
                                if (int.TryParse(items[1], out bottom))
                                    ps.DefaultPageSettings.Margins.Bottom = bottom;
                                break;
                        }
                    }
                    else if (plt == null)
                    {
                        string[] items = line.Split(new char[] { '=' });
                        string value = (items.Length == 2) ? items[1] : String.Empty;
                        switch (items[0])
                        {
                            case "Name": name = value; break;
                            case "PlotKind":
                                int plotkind;
                                if (int.TryParse(value, out plotkind))
                                {
                                    plt = new Plot(plotkind);
                                    plt.Name = name;
                                    PlotList.Add(plt);
                                }
                                break;
                        }
                    }
                    else if (plt != null)
                    {
                        string[] items = line.Split(new char[] { '=' });
                        string propname = items[0];
                        if (plt.Props.ContainsKey(propname))
                        {
                            string typename = plt.Props[propname].GetType().Name;
                            switch (typename)
                            {
                                case "Single":
                                    Single sv;
                                    if (Single.TryParse(items[1], out sv))
                                        plt.Props[propname] = sv;
                                    break;
                                case "Color":
                                    plt.Props[propname] = Data.ColorFromBase(items[1]);
                                    break;
                                case "Boolean":
                                    int bv;
                                    if (int.TryParse(items[1], out bv))
                                        plt.Props[propname] = (bv > 0);
                                    break;
                                case "Int32":
                                    int iv;
                                    if (int.TryParse(items[1], out iv))
                                        plt.Props[propname] = iv;
                                    break;
                                default:
                                    plt.Props[propname] = items[1];
                                    break;
                            }
                        }
                        else
                            plt.SetPropValue(propname, items[1]);
                        //if (items[0].Equals("ImagePath"))
                        //    plt.SetPropValue("ImagePath",
                        //        Application.StartupPath + "\\images\\");
                    }
                }
                else
                {
                    plt = null;
                    report = false;
                }
            }
            calcWorkRects();
        }

        public List<string> ExportLines()
        {
            List<string> lines = new List<string>();
            lines.Add("[Report]");
            lines.Add("Descriptor=" + Descriptor);
            lines.Add("ReportName=" + ReportName.ToUpper());
            lines.Add("PrinterName=" + PrinterName);
            string papername = printDocument.PrinterSettings.DefaultPageSettings.
                PaperSize.PaperName;
            lines.Add("PaperName=" + papername);
            bool landscape = printDocument.PrinterSettings.DefaultPageSettings.Landscape;
            lines.Add("Landscape=" + ((landscape) ? "1" : "0"));
            Margins mgs = printDocument.PrinterSettings.DefaultPageSettings.Margins;
            lines.Add("Left=" + mgs.Left.ToString());
            lines.Add("Right=" + mgs.Right.ToString());
            lines.Add("Top=" + mgs.Top.ToString());
            lines.Add("Bottom=" + mgs.Bottom.ToString());
            lines.Add("");
            foreach (Plot item in PlotList)
            {
                lines.Add("[" + item.Name + "]");
                foreach (KeyValuePair<string, object> prop in item.Props)
                {
                    if (!prop.Key.Equals("ImagePath"))
                    {
                        Type valtype = prop.Value.GetType();
                        switch (valtype.Name)
                        {
                            case "Boolean":
                                lines.Add(prop.Key + "=" + (((bool)prop.Value) ? "1" : "0"));
                                break;
                            case "Color":
                                Color color = (Color)prop.Value;
                                lines.Add(prop.Key + "=" + Data.ColorToBase(color));
                                break;
                            default:
                                string value = prop.Value.ToString();
                                lines.Add(prop.Key + "=" + value);
                                break;
                        }
                    }
                }
                lines.Add("");
            }
            return lines;
        }

        public void UpdatePlotText(string text)
        {
            bool linkchanges = false;
            foreach (Plot plt in SelList)
            {
                if (plt.Kind > 1)
                {
                    plt.Props["PtName"] = text;
                    linkchanges = true;
                }
                else
                {
                    plt.Props["Text"] = text;
                    if (text.Length > 0 && text.IndexOf("[{") >= 0) linkchanges = true;
                }
            }
            if (linkchanges) Update();
        }

        public Plot GetPlotAt(int X, int Y)
        {
            for (int i = PlotList.Count - 1; i >= 0; i--)
            {
                Plot plt = PlotList[i];
                RectangleF rect = new RectangleF(plt.Location, plt.Size);
                if (rect.Contains(new Point(X, Y))) return plt;
            }
            return null;
        }

        public void NewReport()
        {
            SelList.Clear();
            PlotList.Clear();
            ReportName = "NONAME";
            Descriptor = String.Empty;
        }

        public void SaveToBase()
        {
            Data.EmptyReport(ReportName);
            List<string> lines = ExportLines();
            string content = String.Join("\r\n", lines.ToArray());
            byte[] image = Encoding.Unicode.GetBytes(content);
            Data.AddReport(ReportName, DateTime.Now, image, Descriptor);
        }

        private static void SetPaperSize(ref PrinterSettings ps, string PaperName)
        {
            for (int i = 0; i < ps.PaperSizes.Count; i++)
                if (ps.PaperSizes[i].PaperName == PaperName)
                {
                    ps.DefaultPageSettings.PaperSize = ps.PaperSizes[i];
                    break;
                }
        }

        private void calcWorkRects()
        {
            printDocument.PrinterSettings.PrinterName = PrinterName;
            PageSettings ps =
                printDocument.PrinterSettings.DefaultPageSettings;
            PageRect = ps.Bounds;
            DrawRect = new Rectangle(ps.Margins.Left, ps.Margins.Top,
                PageRect.Right - ps.Margins.Right - ps.Margins.Left,
                PageRect.Bottom - ps.Margins.Bottom - ps.Margins.Top);
            //drawBox.Size = pageRect.Size;
            //UpdateDrawBox();
        }

        public void PaintPage(Graphics g, bool editormode = false)
        {
            if (editormode)
            {
                g.FillRectangle(Brushes.White, PageRect);
                using (Pen pen = new Pen(Color.Silver))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawRectangle(pen, DrawRect);
                }
            }
            // Отрисовка массива фигур
            foreach (Plot plt in PlotList) plt.DrawFigure(g, editormode);
            if (editormode)
            {
                // Отрисовка выбранных
                if (!DragByRect)
                {
                    foreach (Plot plt in SelList)
                    {
                        using (Pen pen = new Pen(Color.Teal))
                        {
                            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                            RectangleF rect = plt.BoundsRect;
                            //rect.Inflate(1f, 1f);
                            g.DrawRectangles(pen, new RectangleF[] { rect });
                        }
                    }
                }
                // отрисовка прямоугольника выбора
                using (Pen pen = new Pen(Color.Teal))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawRectangle(pen, SelRect);
                }
            }
        }
    }
}
