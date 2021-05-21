using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace SPT961Emularor
{
    public partial class Form1 : Form
    {
        IDictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            backUpdate.RunWorkerAsync();
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

        private List<string> EncodeFetchRequest(byte[] receivedBytes, BackgroundWorker worker)
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
                worker.ReportProgress(0, "Ошибка контрольной суммы");
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
            List<byte> dataset = new List<byte>(); // содержит данные запроса
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
            //bool addr = true;
            //int index = 0;
            n = 0;
            StringBuilder item = new StringBuilder();
            List<byte> data = new List<byte>();
            for (int i = 0; i < dataset.Count; i++)
            {
                switch (dataset[i])
                {
                    case 0x09:
                        if (data.Count > 0)
                            item.Append(cp866unicode.UnicodeString(data.ToArray()));
                        if (item.Length > 0)
                            result.Add(item.ToString());
                        item.Clear();
                        data.Clear();
                        break;
                    case 0x0c:
                        if (data.Count > 0)
                            item.Append(cp866unicode.UnicodeString(data.ToArray()));
                        if (item.Length > 0)
                            result.Add(item.ToString());
                        item.Clear();
                        data.Clear();
                        break;
                    default:
                        data.Add(dataset[i]);
                        break;
                }
            }
            return result;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            backUpdate.CancelAsync();
        }

        private void backUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            bool NoConnectSay = false;
            BackgroundWorker worker = sender as BackgroundWorker;
            try
            {
                Int32 port = 4002;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                // создание и запуск сервера
                TcpListener server = new TcpListener(localAddr, port);
                server.Start();
                Byte[] bytes = new Byte[256];
                //String data = null;
                while (true)
                {
                    if (worker.CancellationPending) break;
                    if (server.Pending())
                    {
                        NoConnectSay = false;
                        // прослушивание и ожидание подключения
                        TcpClient client = server.AcceptTcpClient();
                        //data = null;
                        // произошло подключение, создается поток сообщений
                        NetworkStream stream = client.GetStream();
                        // чтение сообщения
                        int n = stream.Read(bytes, 0, bytes.Length);
                        if (n > 0)
                        {
                            Byte[] receivedBytes = new Byte[n];
                            Array.Copy(bytes, receivedBytes, n);
                            List<string> answer = EncodeFetchRequest(receivedBytes, worker);
                            if (answer.Count >= 2)
                            {
                                string ChannelNumberReturn = answer[0] + ";" + answer[1]; // ChannelNumberReturn
                                //string ParameterReturn = answer[1];     // ParameterReturn
                                string ParameterReturn = String.Join(";", answer.GetRange(2, answer.Count - 2));
                                if (ParameterReturn.Length > 0)
                                {
                                    string[] dt = ParameterReturn.Split(new char[] { ';' });
                                    ParameterReturn = DateTime.Parse(dt[0] + "." + dt[1] + "." + dt[2] + " " +
                                        dt[3] + ":" + dt[4] + ":" + dt[5]).ToString("dd.MM.yy HH:mm") + " - " +
                                        DateTime.Parse(dt[6] + "." + dt[7] + "." + dt[8] + " " +
                                        dt[9] + ":" + dt[10] + ":" + dt[11]).ToString("dd.MM.yy HH:mm");
                                }
                                worker.ReportProgress(1,
                                    new Tuple<string, string>(ChannelNumberReturn, ParameterReturn));

                            }
                        }
                        //data = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
                        // преобразование данных
                        //data = data.ToUpper();
                        // отправка сообщения
                        //byte[] msg = System.Text.Encoding.UTF8.GetBytes(data);
                        //stream.Write(msg, 0, msg.Length);
                        // завершение работы с потоком сообщений
                        client.Close();
                    }
                    else
                    {
                        if (!NoConnectSay)
                        {
                            NoConnectSay = true;
                            worker.ReportProgress(0, "Нет подключений");
                        }
                    }
                }
            }
            catch (SocketException ex)
            {
                // ошибки соединения
                worker.ReportProgress(0, "Ошибка соединения " + ex.ErrorCode);
            }
        }

        private void backUpdate_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 0:
                    this.Text = e.UserState.ToString();
                    break;
                case 1:
                    Tuple<string, string> args = (Tuple<string, string>)e.UserState;
                    string item = args.Item1;
                    if (!lbList.Items.Contains(item))
                        lbList.Items.Add(item);
                    if (!dict.ContainsKey(item)) dict.Add(item, new List<string>());
                    dict[item].Add(args.Item2);
                    //if (lbList.Items.Count > 20) lbList.Items.RemoveAt(0); 
                    break;
            }
        }

        private void backUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void lbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbList.SelectedItem != null)
            {
                string item = lbList.SelectedItem.ToString();
                lbRecords.Items.Clear();
                foreach (string st in dict[item])
                    lbRecords.Items.Add(st);
            }
        }
    }
}
