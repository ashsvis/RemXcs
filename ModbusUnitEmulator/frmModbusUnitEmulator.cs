using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using IniFiles.Net;

namespace ModbusUnitEmulator
{
    public partial class frmModbusUnitEmulator : Form
    {
        public frmModbusUnitEmulator()
        {
            InitializeComponent();
        }

        private void frmModbusUnitEmulator_Load(object sender, EventArgs e)
        {
            string[] portnames = SerialPort.GetPortNames();
            cbPortName.Items.AddRange(portnames);
            IniFile ifi = new IniFile(Application.StartupPath + "\\serialport.ini");
            cbPortName.Text = ifi.ReadString("SerialPort", "PortName", "COM1");
            cbBaudRate.Text = ifi.ReadString("SerialPort", "BaudRate", "9600");
            cbParity.Items.AddRange(Enum.GetNames(typeof(Parity)));
            cbParity.Text = ifi.ReadString("SerialPort", "Parity", "None");
            cbDataBits.Text = ifi.ReadString("SerialPort", "DataBits", "8");
            cbStopBits.Items.AddRange(Enum.GetNames(typeof(StopBits)));
            cbStopBits.Items.RemoveAt(0);
            cbStopBits.Text = ifi.ReadString("SerialPort", "StopBits", "One");
        }

        #region Сохранение настроек порта

        private void WriteStringToIni(string paramname, string value)
        {
            IniFile ifi = new IniFile(Application.StartupPath + "\\serialport.ini");
            ifi.WriteString("SerialPort", paramname, value);
        }

        private void cbPortName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            WriteStringToIni("PortName", cbPortName.Text);
        }

        private void cbBaudRate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            WriteStringToIni("BaudRate", cbBaudRate.Text);
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

        #endregion

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (!bckgWorker.IsBusy)
            {
                btnOpen.Enabled = false;
                btnClose.Enabled = true;
                RunBackgroundWork();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (bckgWorker.IsBusy)
            {
                bckgWorker.CancelAsync();
            }
        }

        private void RunBackgroundWork()
        {
            #region вычитывание данных настройки 
            IniFile ifi = new IniFile(Application.StartupPath + "\\serialport.ini");
            string portname = ifi.ReadString("SerialPort", "PortName", "COM1");
            int baudrate = int.Parse(ifi.ReadString("SerialPort", "BaudRate", "9600"));
            Parity parity = (Parity)Enum.Parse(typeof(Parity),
                ifi.ReadString("SerialPort", "Parity", "None"));
            int databits = int.Parse(ifi.ReadString("SerialPort", "DataBits", "8"));
            StopBits stopbits = (StopBits)Enum.Parse(typeof(StopBits),
                ifi.ReadString("SerialPort", "StopBits", "One"));
            #endregion
            bckgWorker.RunWorkerAsync(new Tuple<string, int, Parity, int, StopBits>(
                portname, baudrate, parity, databits, stopbits));
        }

        private bool TuningPort(SerialPort port,
            Tuple<string, int, Parity, int, StopBits> args)
        {
            try
            {
                // Allow the user to set the appropriate properties.
                port.PortName = args.Item1;
                port.BaudRate = args.Item2;
                port.Parity = args.Item3;
                port.DataBits = args.Item4;
                port.StopBits = args.Item5;
                port.Handshake = Handshake.None;
                // Set the read/write timeouts
                port.ReadTimeout = 500;
                port.WriteTimeout = 500;
                port.ReadBufferSize = 250;
                port.WriteBufferSize = 250;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private ushort CRC(byte[] buff, int len)
        {   // контрольная сумма MODBUS RTU
              //Res:=$FFFF;
              //for i:=1 to Length(S) do
              //begin
              //  Res:=Res xor Ord(S[i]);
              //  for j:=1 to 8 do
              //  begin
              //    Flag:=(Res and $0001) > 0;
              //    Res:=Res shr 1;
              //    if Flag then Res:=Res xor $A001;
              //  end;
              //end;
              //Result:=Chr(Lo(Res))+Chr(Hi(Res)); ???
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

        private byte[] Swap(byte[] buff)
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

        private void bckgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            float[] fbuff = new float[64];
            for (int i = 0; i < fbuff.Length; i++) fbuff[i] = 12.345678F;

            Int32[] lbuff = new Int32[64];
            for (int i = 0; i < lbuff.Length; i++) lbuff[i] = 12345678;

            Int16[] wbuff = new Int16[128];
            for (int i = 0; i < wbuff.Length; i++) wbuff[i] = 12345;

            BackgroundWorker worker = (BackgroundWorker)sender;
            using (SerialPort _sPort = new SerialPort())
            {
                if (TuningPort(_sPort,
                    (Tuple<string, int, Parity, int, StopBits>)e.Argument))
                {
                    _sPort.Open();
                    if (_sPort.IsOpen)
                    {
                        List<byte> inbuff = new List<byte>();
                        _sPort.DiscardInBuffer();
                        while (!worker.CancellationPending)
                        {
                            try
                            {
                                int onebyte = _sPort.ReadByte();
                                if (onebyte < 0)
                                {
                                    inbuff.Clear();
                                    continue; // буфер приёма пуст, ошибка
                                }
                                else
                                {
                                    inbuff.Add((byte)onebyte);
                                    if (inbuff.Count == 8)
                                    {
                                        ushort crcCalc = CRC(inbuff.ToArray(), 6);
                                        ushort crcBuff =
                                            BitConverter.ToUInt16(inbuff.ToArray(), 6);
                                        if (crcCalc == crcBuff)
                                        {
                                            byte addr = inbuff[0];
                                            byte func = inbuff[1];
                                            byte addrHi = inbuff[2];
                                            byte addrLo = inbuff[3];
                                            int startAddr = (inbuff[2] << 8) + inbuff[3];
                                            byte noregsHi = inbuff[4];
                                            byte noregsLo = inbuff[5];
                                            int regsCount = (inbuff[4] << 8) + inbuff[5];
                                            if (addr > 0 && addr <= 247 &&
                                                func > 0 && func <= 255 &&
                                                startAddr >= 0 &&
                                                regsCount > 0 && regsCount <= 127)
                                            {
                                                List<byte> outbuff = new List<byte>();
                                                switch (func)
                                                {
                                                    case 3: // holding registers
                                                        AnswerRegs(lbuff, addr, func, regsCount, outbuff);
                                                        break;
                                                    case 4: // input registers
                                                        AnswerRegs(fbuff, addr, func, regsCount, outbuff);
                                                        break;
                                                }
                                                _sPort.DiscardOutBuffer();
                                                _sPort.Write(outbuff.ToArray(), 0, outbuff.Count);
                                            }
                                        }
                                        inbuff.Clear();
                                    }
                                }
                            }
                            catch (TimeoutException)
                            {
                                // устройство не ответило вовремя, ошибка
                                inbuff.Clear();
                            }
                        }
                    }
                }
            }
        }

        private void AnswerRegs(Int16[] wbuff, byte addr, byte func,
            int regsCount, List<byte> outbuff)
        {
            AddAnswerRegsPrefix(addr, func, regsCount, outbuff);
            // добавить данные
            // каждый Int16 занимает один регистр (по 2 байта)
            for (int i = 0; i < regsCount; i++)
            {
                byte[] buff = BitConverter.GetBytes(wbuff[i]);
                outbuff.AddRange(Swap(buff));
            }
            AddAnswerPostfix(outbuff);
        }

        private void AnswerRegs(Int32[] lbuff, byte addr, byte func,
            int regsCount, List<byte> outbuff)
        {
            AddAnswerRegsPrefix(addr, func, regsCount, outbuff);
            // добавить данные
            // каждый Int32 занимает два регистра (по 2 байта)
            for (int i = 0; i < regsCount / 2; i++)
            {
                byte[] buff = BitConverter.GetBytes(lbuff[i]);
                outbuff.AddRange(Swap(buff));
            }
            AddAnswerPostfix(outbuff);
        }

        private void AnswerRegs(float[] fbuff, byte addr, byte func,
            int regsCount, List<byte> outbuff)
        {
            AddAnswerRegsPrefix(addr, func, regsCount, outbuff);
            // добавить данные
            // каждый float занимает два регистра (по 2 байта)
            for (int i = 0; i < regsCount / 2; i++)
            {
                byte[] buff = BitConverter.GetBytes(fbuff[i]);
                outbuff.AddRange(Swap(buff));
            }
            AddAnswerPostfix(outbuff);
        }

        private static void AddAnswerRegsPrefix(byte addr, byte func,
            int regsCount, List<byte> outbuff)
        {
            outbuff.Add(addr);
            outbuff.Add(func);
            // добавить счётчик байтов
            byte bytecount = (byte)(regsCount * 2);
            outbuff.Add(bytecount);
        }

        private void AddAnswerPostfix(List<byte> outbuff)
        {
            // добавить контрольную сумму
            outbuff.AddRange(
                BitConverter.GetBytes(
                CRC(outbuff.ToArray(),
                    outbuff.Count)));
        }

        private void bckgWorker_RunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs e)
        {
            btnOpen.Enabled = true;
            btnClose.Enabled = false;
        }

        private void bckgWorker_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {

        }

    }
}
