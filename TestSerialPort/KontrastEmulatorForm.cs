using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using IniFiles.Net;

namespace TestSerialPort
{
    public partial class KontrastEmulatorForm : Form
    {

        struct RemicontHeader
        {
            public byte NodeAddr;
            public byte StationAddr;
            public ushort DataSize;
            public byte CtrlBits;
        }

        private static List<byte> FetchBuff = new List<byte>();
        private static MemIniFile Mif;

        public KontrastEmulatorForm()
        {
            InitializeComponent();
        }

        private void removeTabs()
        {
            tc.TabPages.Remove(tpKontur);
            tc.TabPages.Remove(tpParOut);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IniFile ifi = new IniFile(Application.StartupPath + "\\serialport.ini");
            string[] portnames = SerialPort.GetPortNames();
            cbPortName.Items.AddRange(portnames);
            cbPortName.Text = ifi.ReadString("SerialPort", "PortName", "COM1");
            cbBaudRate.Text = ifi.ReadString("SerialPort", "BaudRate", "9600");
            cbParity.Items.AddRange(Enum.GetNames(typeof(Parity)));
            cbParity.Text = ifi.ReadString("SerialPort", "Parity", "None");
            cbDataBits.Text = ifi.ReadString("SerialPort", "DataBits", "8");
            cbStopBits.Items.AddRange(Enum.GetNames(typeof(StopBits)));
            cbStopBits.Text = ifi.ReadString("SerialPort", "StopBits", "One");
            Mif = new MemIniFile(Application.StartupPath + "\\RemicontEmulator.ini");
            fillTree();
        }

        private void fillTree()
        {
            IDictionary<string, TreeNode> hs = new Dictionary<string, TreeNode>();
            tv.Nodes.Clear();
            string[] sections = Mif.ReadSections();
            foreach (string section in sections)
            {
                TreeNode node = tv.Nodes.Add(section);
                string[] keys = Mif.ReadSectionKeys(section);
                TreeNode konturs = node.Nodes.Add("Контуры");
                TreeNode inr = node.Nodes.Add("ИНР");
                TreeNode kd = node.Nodes.Add("КД");
                TreeNode apar = node.Nodes.Add("Параметры");
                TreeNode aout = node.Nodes.Add("Выходы");
                foreach (string key in keys)
                {
                    if (key.StartsWith("K"))
                        konturs.Nodes.Add(key);
                    else
                        if (key.StartsWith("I"))
                            inr.Nodes.Add(key);
                        else
                            if (key.StartsWith("D"))
                                kd.Nodes.Add(key);
                            else
                                if (key.StartsWith("A") && key.IndexOf('P') >= 0)
                                {
                                    string algoblock = key.Substring(0, key.IndexOf('P'));
                                    TreeNode ab;
                                    if (hs.ContainsKey(section + algoblock))
                                        ab = hs[section + algoblock];
                                    else
                                    {
                                        ab = apar.Nodes.Add(algoblock);
                                        hs.Add(section + algoblock, ab);
                                    }
                                    ab.Nodes.Add(key.Substring(algoblock.Length));
                                }
                                else
                                    if (key.StartsWith("A") && key.IndexOf('O') >= 0)
                                    {
                                        string algoblock = key.Substring(0, key.IndexOf('O'));
                                        TreeNode ab;
                                        if (hs.ContainsKey(section + algoblock))
                                            ab = hs[section + algoblock];
                                        else
                                        {
                                            ab = aout.Nodes.Add(algoblock);
                                            hs.Add(section + algoblock, ab);
                                        }
                                        ab.Nodes.Add(key.Substring(algoblock.Length));
                                    }
                                    else
                                        node.Nodes.Add(key);
                }
                if (konturs.Nodes.Count == 0) tv.Nodes.Remove(konturs);
                if (inr.Nodes.Count == 0) tv.Nodes.Remove(inr);
                if (kd.Nodes.Count == 0) tv.Nodes.Remove(kd);
                if (apar.Nodes.Count == 0) tv.Nodes.Remove(apar);
                if (aout.Nodes.Count == 0) tv.Nodes.Remove(aout);
            }
            tv.Sort();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Mif.UpdateFile();
            backgroundFetch.CancelAsync();
        }

        private void backgroundFetch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnClose.Enabled = false;
            btnOpen.Enabled = true;
        }

        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            System.ComponentModel.BackgroundWorker worker = backgroundFetch;
            SerialPort sp = (SerialPort)sender;
            int len = sp.BytesToRead;
            if (len > 0)
            {
                byte[] buff = new byte[len];
                if (sp.Read(buff, 0, len) == len)
                {
                    FetchBuff.AddRange(buff);
                    int headerSize = 5;
                    if (FetchBuff.Count >= headerSize)
                    {
                        RemicontHeader header = new RemicontHeader();
                        header.NodeAddr = FetchBuff[0];
                        string section = "Контроллер " + header.NodeAddr.ToString();
                        header.StationAddr = FetchBuff[1];
                        header.DataSize = (ushort)(FetchBuff[2] + 256 * FetchBuff[3]);
                        header.CtrlBits = (byte)(FetchBuff[4] >> 6);
                        int telegrammSize = headerSize + header.DataSize + 1;
                        if (FetchBuff.Count == telegrammSize) // блок получен полностью
                        {
                            byte csum = FetchBuff[FetchBuff.Count - 1];
                            if (csum == CheckSum(FetchBuff)) // проверка контрольной суммы
                            {
                                List<byte> OutBuff = new List<byte>(); 
                                int n;
                                byte npar;
                                string ident;
                                byte[] arr;
                                // обработка данных
                                switch (FetchBuff[5]) // код категории
                                {
                                    case 1: // запрос ФАБЛ
                                        switch (FetchBuff[6]) // код оператора
                                        {
                                            case 0: // ИНР
                                                OutBuff.Clear();
                                                OutBuff.Add(0x81); // адрес приёмника
                                                OutBuff.Add(header.NodeAddr); // адрес передатчика
                                                OutBuff.Add(2 + 30 * 4); // длина блока
                                                OutBuff.Add(0); // ответа
                                                OutBuff.Add(0x80); // управление
                                                OutBuff.Add(1);
                                                OutBuff.Add(0);
                                                ushort INRNum = (ushort)(FetchBuff[7] + 256 * FetchBuff[8]);
                                                ident = "I" + INRNum;
                                                arr = getBytesFromString(Mif.ReadString(section, ident, String.Empty), 30 * 4);
                                                if (!Mif.KeyExists(section, ident))
                                                    Mif.WriteString(section, ident, getStringFromBytes(arr, 30 * 4));
                                                foreach (byte b in arr) OutBuff.Add(b);
                                                OutBuff.Add(0); // кс, пока не посчитанная
                                                OutBuff[OutBuff.Count - 1] = CheckSum(OutBuff);
                                                if (sp.IsOpen)
                                                    sp.Write(OutBuff.ToArray(), 0, OutBuff.Count);
                                                worker.ReportProgress(0,
                                                    "Категория: " + FetchBuff[5] + " функция: " + FetchBuff[6] + " отвечено.");
                                                break;
                                            case 1: // параметры настройки алгоблока ФАБЛ
                                                if (header.CtrlBits == 1) // запрос
                                                {
                                                    npar = FetchBuff[7];
                                                    OutBuff.Clear();
                                                    OutBuff.Add(0x81); // адрес приёмника
                                                    OutBuff.Add(header.NodeAddr); // адрес передатчика
                                                    OutBuff.Add((byte)(3 + 8 * npar)); // длина блока
                                                    OutBuff.Add(0); // ответа
                                                    OutBuff.Add(0x80); // управление
                                                    OutBuff.Add(1);
                                                    OutBuff.Add(1);
                                                    OutBuff.Add(npar);
                                                    n = 8;
                                                    for (int i = 0; i < npar; i++)
                                                    {
                                                        ushort AlgoNum = (ushort)(FetchBuff[n] + 256 * FetchBuff[n + 1]);
                                                        OutBuff.Add(FetchBuff[n]);
                                                        OutBuff.Add(FetchBuff[n + 1]);
                                                        byte PlaceNum = FetchBuff[n + 2];
                                                        OutBuff.Add(PlaceNum);
                                                        //OutBuff.Add(0); // тип значения
                                                        //OutBuff.Add(0);
                                                        //OutBuff.Add(0);
                                                        //OutBuff.Add(0);
                                                        //OutBuff.Add(0);
                                                        ident = "A" + AlgoNum + "P" + PlaceNum;
                                                        arr = getInOutFromString(Mif.ReadString(section, ident, String.Empty));
                                                        if (!Mif.KeyExists(section, ident))
                                                            Mif.WriteString(section, ident, getStringFromInOut(arr));
                                                        foreach (byte b in arr) OutBuff.Add(b);
                                                        n += 3;
                                                    }
                                                    OutBuff.Add(0); // кс, пока не посчитанная
                                                    OutBuff[OutBuff.Count - 1] = CheckSum(OutBuff);
                                                    if (sp.IsOpen)
                                                        sp.Write(OutBuff.ToArray(), 0, OutBuff.Count);
                                                    worker.ReportProgress(0,
                                                        "Категория: " + FetchBuff[5] + " функция: " + FetchBuff[6] + " отвечено.");
                                                }
                                                else
                                                    if (header.CtrlBits == 0) //команда
                                                    {
                                                    }
                                                break;
                                            case 2: // контуры регулирования
                                                if (header.CtrlBits == 1) // запрос
                                                {
                                                    try
                                                    {
                                                        npar = FetchBuff[7];
                                                        OutBuff.Clear();
                                                        OutBuff.Add(0x81); // адрес приёмника
                                                        OutBuff.Add(header.NodeAddr); // адрес передатчика
                                                        OutBuff.Add((byte)(3 + 23 * npar)); // длина блока
                                                        OutBuff.Add(0); // ответа
                                                        OutBuff.Add(0x80); // управление
                                                        OutBuff.Add(1);
                                                        OutBuff.Add(1);
                                                        OutBuff.Add(npar);
                                                        n = 8;
                                                        for (int i = 0; i < npar; i++)
                                                        {
                                                            byte Kontur = FetchBuff[n];
                                                            OutBuff.Add(Kontur);
                                                            ident = "K" + Kontur;
                                                            arr = getKonturFromString(Mif.ReadString(section, ident, String.Empty));
                                                            if (!Mif.KeyExists(section, ident))
                                                                Mif.WriteString(section, ident, getStringFromKontur(arr));
                                                            foreach (byte b in arr) OutBuff.Add(b);
                                                            n += 1;
                                                        }
                                                        OutBuff.Add(0); // кс, пока не посчитанная
                                                        OutBuff[OutBuff.Count - 1] = CheckSum(OutBuff);
                                                        if (sp.IsOpen)
                                                            sp.Write(OutBuff.ToArray(), 0, OutBuff.Count);
                                                        worker.ReportProgress(0,
                                                            "Категория: " + FetchBuff[5] + " функция: " + FetchBuff[6] + " отвечено.");
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        worker.ReportProgress(0, ex.Message);
                                                    }
                                                }
                                                else
                                                    if (header.CtrlBits == 0) //команда
                                                    {
                                                    }
                                                break;
                                            case 4: // выходы алгоблока ФАБЛ
                                                try
                                                {
                                                    npar = FetchBuff[7];
                                                    OutBuff.Clear();
                                                    OutBuff.Add(0x81); // адрес приёмника
                                                    OutBuff.Add(header.NodeAddr); // адрес передатчика
                                                    OutBuff.Add((byte)(3 + 8 * npar)); // длина блока
                                                    OutBuff.Add(0); // ответа
                                                    OutBuff.Add(0x80); // управление
                                                    OutBuff.Add(1);
                                                    OutBuff.Add(1);
                                                    OutBuff.Add(npar);
                                                    n = 8;
                                                    for (int i = 0; i < npar; i++)
                                                    {
                                                        ushort AlgoNum = (ushort)(FetchBuff[n] + 256 * FetchBuff[n + 1]);
                                                        OutBuff.Add(FetchBuff[n]);
                                                        OutBuff.Add(FetchBuff[n + 1]);
                                                        byte PlaceNum = FetchBuff[n + 2];
                                                        OutBuff.Add(PlaceNum);
                                                        //OutBuff.Add(0); // тип значения
                                                        //OutBuff.Add(0);
                                                        //OutBuff.Add(0);
                                                        //OutBuff.Add(0);
                                                        //OutBuff.Add(0);
                                                        ident = "A" + AlgoNum + "O" + PlaceNum;
                                                        arr = getInOutFromString(Mif.ReadString(section, ident, String.Empty));
                                                        if (!Mif.KeyExists(section, ident))
                                                            Mif.WriteString(section, ident, getStringFromInOut(arr));
                                                        foreach (byte b in arr) OutBuff.Add(b);
                                                        n += 3;
                                                    }
                                                    OutBuff.Add(0); // кс, пока не посчитанная
                                                    OutBuff[OutBuff.Count - 1] = CheckSum(OutBuff);
                                                    if (sp.IsOpen)
                                                        sp.Write(OutBuff.ToArray(), 0, OutBuff.Count);
                                                    worker.ReportProgress(0,
                                                        "Категория: " + FetchBuff[5] + " функция: " + FetchBuff[6] + " отвечено.");
                                                }
                                                catch (Exception ex)
                                                {
                                                    worker.ReportProgress(0, ex.Message);
                                                }
                                                break;
                                            case 6: // дата и время контроллера
                                                if (header.CtrlBits == 1) // запрос
                                                {
                                                    OutBuff.Clear();
                                                    OutBuff.Add(0x81); // адрес приёмника
                                                    OutBuff.Add(header.NodeAddr); // адрес передатчика
                                                    OutBuff.Add(9); // длина блока
                                                    OutBuff.Add(0); // ответа
                                                    OutBuff.Add(0x80); // управление
                                                    OutBuff.Add(1);
                                                    OutBuff.Add(6);
                                                    DateTime dt = DateTime.Now;
                                                    OutBuff.Add((byte)(dt.Year - 2000));
                                                    OutBuff.Add((byte)dt.Month);
                                                    OutBuff.Add((byte)dt.Day);
                                                    OutBuff.Add((byte)dt.DayOfWeek);
                                                    OutBuff.Add((byte)dt.Hour);
                                                    OutBuff.Add((byte)dt.Minute);
                                                    OutBuff.Add((byte)dt.Second);
                                                    OutBuff.Add(0); // кс, пока не посчитанная
                                                    OutBuff[OutBuff.Count - 1] = CheckSum(OutBuff);
                                                    if (sp.IsOpen)
                                                        sp.Write(OutBuff.ToArray(), 0, OutBuff.Count);
                                                    worker.ReportProgress(0,
                                                        "Категория: " + FetchBuff[5] + " функция: " + FetchBuff[6] + " отвечено.");
                                                }
                                                else
                                                    if (header.CtrlBits == 0) //команда
                                                    {
                                                    }
                                                break;
                                            case 9: // ошибки контроллера РК-131/300
                                                OutBuff.Clear();
                                                OutBuff.Add(0x81); // адрес приёмника
                                                OutBuff.Add(header.NodeAddr); // адрес передатчика
                                                OutBuff.Add(2 + 15 * 8); // длина блока
                                                OutBuff.Add(0); // ответа
                                                OutBuff.Add(0x80); // управление
                                                OutBuff.Add(1);
                                                OutBuff.Add(9);
                                                for (int i = 0; i < 15; i++)
                                                {
                                                    OutBuff.Add(0); // тип неисправности
                                                    OutBuff.Add(0); // место неисправности, мл
                                                    OutBuff.Add(0); // место неисправности, ст
                                                    OutBuff.Add(0); // месяц
                                                    OutBuff.Add(0); // число
                                                    OutBuff.Add(0); // час
                                                    OutBuff.Add(0); // минуты
                                                    OutBuff.Add(0); // секунды
                                                }
                                                OutBuff.Add(0); // кс, пока не посчитанная
                                                OutBuff[OutBuff.Count - 1] = CheckSum(OutBuff);
                                                if (sp.IsOpen)
                                                    sp.Write(OutBuff.ToArray(), 0, OutBuff.Count);
                                                worker.ReportProgress(0,
                                                    "Категория: " + FetchBuff[5] + " функция: " + FetchBuff[6] + " отвечено.");
                                                break;
                                            case 14: // концентратор данных
                                                OutBuff.Clear();
                                                OutBuff.Add(0x81); // адрес приёмника
                                                OutBuff.Add(header.NodeAddr); // адрес передатчика
                                                OutBuff.Add(2 + 30 * 4); // длина блока
                                                OutBuff.Add(0); // ответа
                                                OutBuff.Add(0x80); // управление
                                                OutBuff.Add(1);
                                                OutBuff.Add(14);
                                                ushort KDAlgoNum = (ushort)(FetchBuff[7] + 256 * FetchBuff[8]);
                                                ident = "D" + KDAlgoNum;
                                                arr = getBytesFromString(Mif.ReadString(section, ident, String.Empty), 30 * 4);
                                                if (!Mif.KeyExists(section, ident))
                                                    Mif.WriteString(section, ident, getStringFromBytes(arr, 30 * 4));
                                                foreach (byte b in arr) OutBuff.Add(b);
                                                OutBuff.Add(0); // кс, пока не посчитанная
                                                OutBuff[OutBuff.Count - 1] = CheckSum(OutBuff);
                                                if (sp.IsOpen)
                                                    sp.Write(OutBuff.ToArray(), 0, OutBuff.Count);
                                                worker.ReportProgress(0,
                                                    "Категория: " + FetchBuff[5] + " функция: " + FetchBuff[6] + " отвечено.");
                                                break;
                                            default:
                                                worker.ReportProgress(0,
                                                    "Категория: " + FetchBuff[5] + " функция: " + FetchBuff[6]);
                                                break;
                                        }
                                        break;
                                    case 4: // Операторы системных сообщений
                                        switch (FetchBuff[6]) // код оператора
                                        {
                                            case 1: // режим работы
                                                if (header.CtrlBits == 1) // запрос
                                                {
                                                    OutBuff.Clear();
                                                    OutBuff.Add(0x81); // адрес приёмника
                                                    OutBuff.Add(header.NodeAddr); // адрес передатчика
                                                    OutBuff.Add(3); // длина блока
                                                    OutBuff.Add(0); // ответа
                                                    OutBuff.Add(0x80); // управление
                                                    OutBuff.Add(4);
                                                    OutBuff.Add(1);
                                                    OutBuff.Add(0); // режим работы контроллера: 0x00 -работа; 0x01 -программирование 
                                                    OutBuff.Add(0); // кс, пока не посчитанная
                                                    OutBuff[OutBuff.Count - 1] = CheckSum(OutBuff);
                                                    if (sp.IsOpen)
                                                        sp.Write(OutBuff.ToArray(), 0, OutBuff.Count);
                                                    worker.ReportProgress(0,
                                                        "Категория: " + FetchBuff[5] + " функция: " + FetchBuff[6] + " отвечено.");
                                                }
                                                else
                                                    if (header.CtrlBits == 0) //команда
                                                    {
                                                    }
                                                break;
                                            case 21: // тип контроллера
                                                OutBuff.Clear();
                                                OutBuff.Add(0x81); // адрес приёмника
                                                OutBuff.Add(header.NodeAddr); // адрес передатчика
                                                OutBuff.Add(3); // длина блока
                                                OutBuff.Add(0); // ответа
                                                OutBuff.Add(0x80); // управление
                                                OutBuff.Add(4);
                                                OutBuff.Add(21);
                                                OutBuff.Add(0); // тип контроллера: 0 - РК-131/300; 1 - КР300
                                                OutBuff.Add(0); // кс, пока не посчитанная
                                                OutBuff[OutBuff.Count - 1] = CheckSum(OutBuff);
                                                if (sp.IsOpen)
                                                    sp.Write(OutBuff.ToArray(), 0, OutBuff.Count);
                                                worker.ReportProgress(0,
                                                    "Категория: " + FetchBuff[5] + " функция: " + FetchBuff[6] + " отвечено.");
                                                break;
                                            default:
                                                worker.ReportProgress(0,
                                                    "Категория: " + FetchBuff[5] + " функция: " + FetchBuff[6]);
                                                break;
                                        }
                                        break;
                                    default:
                                        worker.ReportProgress(0,
                                            "Категория: " + FetchBuff[5]);
                                        break;
                                }
                            }
                        }
                        // очистка буфера при получении всех байтов или мусора
                        if (telegrammSize > 128 || FetchBuff.Count >= telegrammSize)
                        {
                            FetchBuff.Clear();
                            if (sp.IsOpen)
                                sp.DiscardInBuffer();
                        }
                    }
                }
            }
        }

        private byte[] getBytesFromString(string value, int count)
        {
            byte[] buff = new byte[count];
            for (int i=0; i < count; i++) buff[i] = 0;
            string[] vals = value.Split(new char[] { '-' });
            if (vals.Length == count)
            {
                for (int i = 0; i < count; i++)
                    buff[i] = byte.Parse(vals[i], System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            return buff;
        }

        private string getStringFromBytes(byte[] value, int count)
        {
            if (value.Length == count)
                return BitConverter.ToString(value, 0, count);
            else
                return String.Empty;
        }

        private byte[] getKonturFromString(string value)
        {
            return getBytesFromString(value, 22);
        }

        private string getStringFromKontur(byte[] value)
        {
            return getStringFromBytes(value, 22);
        }

        private byte[] getInOutFromString(string value)
        {
            return getBytesFromString(value, 5);
        }

        private string getStringFromInOut(byte[] value)
        {
            return getStringFromBytes(value, 5);
        }

        private byte CheckSum(List<byte> buff)
        {
            byte[] value = new byte[buff.Count - 1];
            buff.CopyTo(0, value, 0, value.Length);
            // Контрольная сумма Контрастов
            int Sum = 0; 
            foreach (byte b in value) Sum += b;
            while (Sum >= 256) Sum = Sum % 256 + Sum / 256;
            return (byte)(0x100 - (Sum & 0xff));
        }

        private void serialPort_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {
            System.ComponentModel.BackgroundWorker worker = backgroundFetch;
            SerialPort sp = (SerialPort)sender;
            switch (e.EventType)
            {
                case SerialError.Frame: // Оборудованием обнаружена ошибка кадрирования.
                    sp.DiscardInBuffer();
                    break;
                case SerialError.Overrun: // Переполнение буфера символов.
                                          // Следующий символ потерян.
                    sp.DiscardInBuffer();
                    break;
                case SerialError.RXOver: // Переполнение входного буфера.
                                         //  Во входном буфере нет места, или после символа
                                         // конца файла (EOF) получен еще один символ.
                    sp.DiscardInBuffer();
                    break;
                case SerialError.RXParity: // Оборудованием обнаружена ошибка четности.
                    sp.DiscardInBuffer();
                    break;
                case SerialError.TXFull: // Приложение пытается передать символ, однако
                                        //выходной буфер заполнен.
                    sp.DiscardOutBuffer();
                    break;
            }
            worker.ReportProgress(0,
                "Ошибка " + e.EventType.ToString());
        }

        private void backgroundFetch_DoWork(object sender, DoWorkEventArgs e)
        {
            System.ComponentModel.BackgroundWorker worker =
                (System.ComponentModel.BackgroundWorker)sender;
            Tuple<string, int, Parity, int, StopBits> args = 
                (Tuple<string, int, Parity, int, StopBits>)e.Argument;
            using (SerialPort _serialPort = new SerialPort())
            {
                try
                {
                    // Allow the user to set the appropriate properties.
                    _serialPort.PortName = args.Item1;       //SetPortName(_serialPort.PortName);
                    _serialPort.BaudRate = args.Item2;       //SetPortBaudRate(_serialPort.BaudRate);
                    _serialPort.Parity = args.Item3;         //SetPortParity(_serialPort.Parity);
                    _serialPort.DataBits = args.Item4;       //SetPortDataBits(_serialPort.DataBits);
                    _serialPort.StopBits = args.Item5;     //SetPortStopBits(_serialPort.StopBits);
                    _serialPort.Handshake = Handshake.None;  //SetPortHandshake(_serialPort.Handshake);
                    // Set the read/write timeouts
                    _serialPort.ReadTimeout = 500;
                    _serialPort.WriteTimeout = 500;
                    _serialPort.ReadBufferSize = 250;
                    _serialPort.WriteBufferSize = 250;
                    // ---------------------------
                    _serialPort.DataReceived += serialPort_DataReceived;
                    _serialPort.ErrorReceived += serialPort_ErrorReceived;
                    // ---------------------------
                    string[] portnames = SerialPort.GetPortNames();
                    if (new List<string>(portnames).Contains(_serialPort.PortName))
                    {
                        bool _continue;
                        try
                        {
                            _serialPort.Open();
                            worker.ReportProgress(0,
                                "Порт " + _serialPort.PortName + " открыт.");
                            _continue = true;
                            while (_continue)
                            {
                                _continue = !((System.ComponentModel.BackgroundWorker)sender).CancellationPending;
                            }
                            _serialPort.Close();
                            worker.ReportProgress(0,
                                "Порт " + _serialPort.PortName + " закрыт.");
                        }
                        catch (Exception ex)
                        {
                            _continue = false;
                            worker.ReportProgress(0,
                                "Ошибка настройки последовательного порта: " + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    worker.ReportProgress(0,
                        "Ошибка настройки последовательного порта: " + ex.Message);
                }
            }
        }

        private void backgroundFetch_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int index = e.ProgressPercentage;
            string args = (string)e.UserState;
            label1.Text = args;
        }

        private bool GetBit(ushort value, int index)
        {
            Int32[] uarr = new Int32[1];
            uarr[0] = value;
            BitArray ba = new BitArray(uarr);
            return ba.Get(index);
        }

        private void SetBit(ref ushort value, int index, bool bit)
        {
            Int32[] uarr = new Int32[1];
            uarr[0] = value;
            BitArray ba = new BitArray(uarr);
            ba.Set(index, bit);
            ba.CopyTo(uarr, 0);
            value = (ushort)uarr[0];
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count == 0)
            {
                string[] vals = e.Node.FullPath.Split(new char[] { '\\' });
                string section = vals[0];
                string ident;
                if (vals[vals.Length - 1].StartsWith("D") ||
                    vals[vals.Length - 1].StartsWith("I") ||
                    vals[vals.Length - 1].StartsWith("K"))
                    ident = vals[vals.Length - 1];
                else
                    ident = vals[vals.Length - 2] + vals[vals.Length - 1];
                if (ident.StartsWith("K"))
                {
                    removeTabs();
                    tc.TabPages.Add(tpKontur);
                    byte[] bytes = getBytesFromString(Mif.ReadString(section, ident, String.Empty), 22);
                    Single value = BitConverter.ToSingle(bytes, 0);
                    nudHV.Tag = new Tuple<string, string, string>(section, ident, "HV");
                    nudHV.ValueChanged -= nudHV_ValueChanged;
                    nudHV.Value = (decimal)value;
                    nudHV.ValueChanged += nudHV_ValueChanged;
                    value = BitConverter.ToSingle(bytes, 4);
                    nudSP.Tag = new Tuple<string, string, string>(section, ident, "SP");
                    nudSP.ValueChanged -= nudHV_ValueChanged;
                    nudSP.Value = (decimal)value;
                    nudSP.ValueChanged += nudHV_ValueChanged;
                    value = BitConverter.ToSingle(bytes, 8);
                    nudPV.Tag = new Tuple<string, string, string>(section, ident, "PV");
                    nudPV.ValueChanged -= nudHV_ValueChanged;
                    nudPV.Value = (decimal)value;
                    nudPV.ValueChanged += nudHV_ValueChanged;
                    value = BitConverter.ToSingle(bytes, 12);
                    nudDV.Tag = new Tuple<string, string, string>(section, ident, "DV");
                    nudDV.ValueChanged -= nudHV_ValueChanged;
                    nudDV.Value = (decimal)value;
                    nudDV.ValueChanged += nudHV_ValueChanged;
                    value = BitConverter.ToSingle(bytes, 16);
                    nudOP.Tag = new Tuple<string, string, string>(section, ident, "OP");
                    nudOP.ValueChanged -= nudHV_ValueChanged;
                    nudOP.Value = (decimal)value;
                    nudOP.ValueChanged += nudHV_ValueChanged;
                    ushort mode = BitConverter.ToUInt16(bytes, 20);
                    cbKonturError.Checked = GetBit(mode, 1); // 0000 0000 0000 0010
                    cbKonturError.Tag = new Tuple<string, string>(section, ident);
                    if (GetBit(mode, 14)) // 0100 0000 0000 0000
                        cbKonturMode.SelectedIndex = 2;
                    else
                        if (GetBit(mode, 4)) // 0000 0000 0001 0000
                            cbKonturMode.SelectedIndex = 0;
                        else
                            cbKonturMode.SelectedIndex = 1;
                    cbKonturMode.Tag = new Tuple<string, string>(section, ident);
                    cbEngUnits.Checked = GetBit(mode, 12); // 0001 0000 0000 0000
                    cbEngUnits.Tag = new Tuple<string, string>(section, ident);
                }
                else if (ident.StartsWith("D") || ident.StartsWith("I"))
                {
                    removeTabs();
                    byte[] bytes = getBytesFromString(Mif.ReadString(section, ident, String.Empty), 30 * 4);
                }
                else if (ident.StartsWith("A"))
                {
                    removeTabs();
                    tc.TabPages.Add(tpParOut);
                    tpParOut.Text = (ident.IndexOf('P') > 0) ? "Параметр алгоблока" : "Выход алгоблока";
                    byte[] bytes = getBytesFromString(Mif.ReadString(section, ident, String.Empty), 5);
                    Single value = BitConverter.ToSingle(bytes, 1);
                    nudValue.Tag = new Tuple<string, string>(section, ident);
                    nudValue.ValueChanged -= nudValue_ValueChanged;
                    nudValue.Value = (decimal)value;
                    nudValue.ValueChanged += nudValue_ValueChanged;
                }
                tc.Visible = true;
            }
            else
                tc.Visible = false;
        }

        private void nudHV_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            Tuple<string, string, string> arg = (Tuple<string, string, string>)nud.Tag;
            Single value = (Single)nud.Value;
            byte[] bytes = getBytesFromString(Mif.ReadString(arg.Item1, arg.Item2, String.Empty), 22);
            byte[] valbytes = BitConverter.GetBytes(value);
            int n;
            switch (arg.Item3)
            {
                case "HV":
                    n = 0;
                    foreach (byte b in valbytes) bytes[n++] = b;
                    break;
                case "SP":
                    n = 4;
                    foreach (byte b in valbytes) bytes[n++] = b;
                    break;
                case "PV":
                    n = 8;
                    foreach (byte b in valbytes) bytes[n++] = b;
                    break;
                case "DV":
                    n = 12;
                    foreach (byte b in valbytes) bytes[n++] = b;
                    break;
                case "OP":
                    n = 16;
                    foreach (byte b in valbytes) bytes[n++] = b;
                    break;
            }
            List<byte> list = new List<byte>();
            list.AddRange(bytes);
            Mif.WriteString(arg.Item1, arg.Item2, getStringFromBytes(list.ToArray(), 22));
            Mif.UpdateFile();
        }

        private void nudValue_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            Tuple<string, string> arg = (Tuple<string, string>)nud.Tag;
            Single value = (Single)nud.Value;
            List<byte> list = new List<byte>();
            list.Add(0);
            byte[] bytes = BitConverter.GetBytes(value);
            list.AddRange(bytes);
            Mif.WriteString(arg.Item1, arg.Item2, getStringFromBytes(list.ToArray(), 5));
            Mif.UpdateFile();
        }

        private void cbBaudRate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            WriteStringToIni("BaudRate", cbBaudRate.Text);
        }

        private void WriteStringToIni(string paramname, string value)
        {
            IniFile ifi = new IniFile(Application.StartupPath + "\\serialport.ini");
            ifi.WriteString("SerialPort", paramname, value);
        }

        private void cbPortName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            WriteStringToIni("PortName", cbPortName.Text);
        }

        private void cbParity_SelectionChangeCommitted(object sender, EventArgs e)
        {
            WriteStringToIni("Parity", cbParity.Text);
        }

        private void cbDataBits_SelectionChangeCommitted(object sender, EventArgs e)
        {
            WriteStringToIni("DataBits", cbDataBits.Text);
        }

        private void cbStopBits_SelectionChangeCommitted(object sender, EventArgs e)
        {
            WriteStringToIni("StopBits", cbStopBits.Text);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            IniFile ifi = new IniFile(Application.StartupPath + "\\serialport.ini");
            string[] portnames = SerialPort.GetPortNames();
            cbPortName.Items.Clear();
            cbPortName.Items.AddRange(portnames);
            cbPortName.Text = ifi.ReadString("SerialPort", "PortName", "COM1");
            cbBaudRate.Text = ifi.ReadString("SerialPort", "BaudRate", "9600");
            int baudrate = int.Parse(cbBaudRate.Text);
            cbParity.Items.Clear();
            cbParity.Items.AddRange(Enum.GetNames(typeof(Parity)));
            cbParity.Text = ifi.ReadString("SerialPort", "Parity", "None");
            Parity parity = (Parity)Enum.Parse(typeof(Parity), cbParity.Text);
            cbDataBits.Text = ifi.ReadString("SerialPort", "DataBits", "8");
            int databits = int.Parse(cbDataBits.Text);
            cbStopBits.Items.Clear();
            cbStopBits.Items.AddRange(Enum.GetNames(typeof(StopBits)));
            cbStopBits.Text = ifi.ReadString("SerialPort", "StopBits", "One");
            StopBits stopbits = (StopBits)Enum.Parse(typeof(StopBits), cbStopBits.Text);
            backgroundFetch.RunWorkerAsync(new Tuple<string, int, Parity, int, StopBits>(
                cbPortName.Text, baudrate, parity, databits, stopbits));
            btnOpen.Enabled = false;
            btnClose.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            backgroundFetch.CancelAsync();
        }

        private void btnOpen_EnabledChanged(object sender, EventArgs e)
        {
            cbPortName.Enabled = btnOpen.Enabled;
            cbBaudRate.Enabled = btnOpen.Enabled;
            cbParity.Enabled = btnOpen.Enabled;
            cbDataBits.Enabled = btnOpen.Enabled;
            cbStopBits.Enabled = btnOpen.Enabled;
        }

        private void cbPortName_DropDown(object sender, EventArgs e)
        {
            string lastvalue = cbPortName.Text;
            string[] portnames = SerialPort.GetPortNames();
            cbPortName.Items.Clear();
            cbPortName.Items.AddRange(portnames);
            cbPortName.Text = lastvalue;
        }

        private void cbKonturError_Click(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            Tuple<string, string> arg = (Tuple<string, string>)cb.Tag;
            byte[] bytes = getBytesFromString(Mif.ReadString(arg.Item1, arg.Item2, String.Empty), 22);
            ushort value = BitConverter.ToUInt16(bytes, 20);
            SetBit(ref value, 1, cb.Checked);
            value = (ushort)(value | 0x0101);
            byte[] valbytes = BitConverter.GetBytes(value);
            int n = 20;
            foreach (byte b in valbytes) bytes[n++] = b;
            List<byte> list = new List<byte>();
            list.AddRange(bytes);
            Mif.WriteString(arg.Item1, arg.Item2, getStringFromBytes(list.ToArray(), 22));
            Mif.UpdateFile();
        }

        private void cbKonturMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Tuple<string, string> arg = (Tuple<string, string>)cb.Tag;
            byte[] bytes = getBytesFromString(Mif.ReadString(arg.Item1, arg.Item2, String.Empty), 22);
            ushort value = BitConverter.ToUInt16(bytes, 20);
            if (cb.SelectedIndex == 2)
            {
                SetBit(ref value, 14, true); // 0100 0000 0000 0000
                SetBit(ref value, 13, false);
            }
            else
            {
                SetBit(ref value, 14, false);
                SetBit(ref value, 13, true);
                SetBit(ref value, 4, (cb.SelectedIndex == 0)); // 0000 0000 0001 0000
            }
            value = (ushort)(value | 0x0101);
            byte[] valbytes = BitConverter.GetBytes(value);
            int n = 20;
            foreach (byte b in valbytes) bytes[n++] = b;
            List<byte> list = new List<byte>();
            list.AddRange(bytes);
            Mif.WriteString(arg.Item1, arg.Item2, getStringFromBytes(list.ToArray(), 22));
            Mif.UpdateFile();
        }

        private void cbEngUnits_Click(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            Tuple<string, string> arg = (Tuple<string, string>)cb.Tag;
            byte[] bytes = getBytesFromString(Mif.ReadString(arg.Item1, arg.Item2, String.Empty), 22);
            ushort value = BitConverter.ToUInt16(bytes, 20);
            SetBit(ref value, 12, cb.Checked);
            value = (ushort)(value | 0x0101);
            byte[] valbytes = BitConverter.GetBytes(value);
            int n = 20;
            foreach (byte b in valbytes) bytes[n++] = b;
            List<byte> list = new List<byte>();
            list.AddRange(bytes);
            Mif.WriteString(arg.Item1, arg.Item2, getStringFromBytes(list.ToArray(), 22));
            Mif.UpdateFile();
        }

    }
}
