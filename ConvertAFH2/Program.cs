using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using IniFiles.Net;

namespace ConvertAFH2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = System.Environment.CurrentDirectory + "\\";
            string filename = path + "base_afh2.txt";
            string ininame = path + "base_afh2.ini";
            if (File.Exists(filename))
            {
                MemIniFile mif = new MemIniFile(ininame);
                mif.Clear();
                string[] lines = System.IO.File.ReadAllLines(filename);
                string[] headers = new string[0];
                bool first = true;
                foreach(string line in lines)
                {
                    if (first)
                    {
                        first = false;
                        headers = line.Split(new char[] { '\t' });
                    }
                    else
                    {
                        string[] values = line.Split(new char[] { '\t' });
                        if (headers.Length >= values.Length)
                        {
                            string npp = String.Empty;
                            string ngroup = String.Empty;
                            string ptname = String.Empty;
                            string ptdesc = String.Empty;
                            string pttype = String.Empty;
                            for (int i = 0; i < headers.Length; i++)
                            {
                                string header = headers[i];
                                string value = (i < values.Length) ? values[i].Trim() : String.Empty;
                                switch (i)
                                {
                                    case 0: npp = value; break;
                                    case 1: ngroup = value; break;
                                    case 2:
                                        ptname = value;
                                        mif.WriteString(ptname, header, value);
                                        mif.WriteString(ptname, "Plugin", "KR500");
                                        break;
                                    case 3:
                                        ptdesc = value;
                                        mif.WriteString(ptname, header, value);
                                        break;
                                    case 4:
                                        if (value.Length == 0) value = "FA";
                                        pttype = value;
                                        mif.WriteString(ptname, header, value);
                                        switch (value)
                                        {
                                            case "ND":
                                            case "SM":
                                                mif.WriteString(ptname, "PtKind", "0");
                                                break;
                                            case "AI":
                                            case "AO":
                                            case "CR":
                                            case "FA":
                                                mif.WriteString(ptname, "PtKind", "1");
                                                mif.WriteString(ptname, "Trend", "False");
                                                mif.WriteString(ptname, "Koeff", "1");
                                                mif.WriteString(ptname, "Offset", "0");
                                                break;
                                            case "DI":
                                            case "DO":
                                                mif.WriteString(ptname, "PtKind", "2");
                                                mif.WriteString(ptname, "Trend", "False");
                                                break;
                                            case "GP":
                                            case "GO":
                                            case "GR":
                                            case "FD":
                                            case "VC":
                                                mif.WriteString(ptname, "PtKind", "3");
                                                break;
                                        }
                                        break;
                                    default:
                                        if (value.Length > 0 && !header.Equals("Station") &&
                                            !header.Equals("TimeUnit"))
                                        {
                                            switch (header)
                                            {
                                                case "Opros":   header = "Actived"; break;
                                                case "Alarm":   header = "Logged";  break;
                                                case "Confirm": header = "Asked";   break;
                                                case "TimeBase": header = "FetchTime"; break;
                                                case "Controller": header = "Node"; break;
                                                case "Algoblock": header = "Block"; break;
                                                case "InOut": header = "Place"; break;
                                                case "PVSource": header = "Source"; value = ""; break;
                                                case "PVFormat": header = "FormatPV"; break;
                                                case "PVEUHI": header = "PVEUHi"; break;
                                                case "PVEULO": header = "PVEULo"; break;
                                            }
                                            if (header.StartsWith("Link"))
                                                header = "Child" + header.Substring(4);
                                            switch (value)
                                            {
                                                case "D0": value = "0"; break;
                                                case "D1": value = "1"; break;
                                                case "D2": value = "2"; break;
                                                case "D3": value = "3"; break;
                                                case "Нет": value = "False"; break;
                                                case "Да": value = "True"; break;
                                            }
                                            mif.WriteString(ptname, header, value);
                                            switch (header)
                                            {
                                                case "Source":
                                                    mif.WriteString(ptname, "Parent", "");
                                                    break;
                                                case "PVEUHi":
                                                    mif.WriteString(ptname, "PVSUHi", value);
                                                    break;
                                                case "PVEULo":
                                                    mif.WriteString(ptname, "PVSULo", value);
                                                    break;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
                mif.UpdateFile();
            }
        }
    }
}
