using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Windows.Forms;
using BaseServer;
using IniFiles.Net;
using Points.Plugins;

namespace FetchModbus
{
    public partial class frmFetchingModbus : Form
    {
        private IDictionary<string, Entity> fetchlist = new Dictionary<string, Entity>();
        //int LastMin = -1; int LastHour = -1; int LastDay = -1; int LastMonth = -1;
        bool Registered = false;
        bool Bonus = false;
        DateTime TurnOnTime;
        string ClientID = String.Empty;

        public frmFetchingModbus()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size();
        }

        private void miClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void miTuning_Click(object sender, EventArgs e)
        {
            Tuning();
        }

        private void Tuning()
        {
            using (frmTuningModbus form = new frmTuningModbus())
            {
                form.cbStation.SelectedIndex = Properties.Settings.Default.Station - 1;
                string[] channels = Properties.Settings.Default.Channels.Split('\t');
                int i = 0;
                foreach (string chan in channels)
                {
                    if (chan.StartsWith("Channel"))
                    {
                        string sChannel = chan.Substring(7, chan.IndexOf("=") - 7);
                        int channel;
                        if (int.TryParse(sChannel, out channel) &&
                            channel > 0 && channel <= 16)
                        {
                            string info = chan.Substring(chan.IndexOf("=") + 1).Trim();
                            if (info.Length > 0)
                            {
                                form.checkboxes[i].Checked = true;
                                form.channelboxes[i].Enabled = true;
                                form.channelboxes[i].SelectedIndex = channel - 1;
                                form.comboboxes[i].Enabled = true;
                                form.comboboxes[i].Text = info;
                                i++;
                            }
                        }
                    }
                }
                form.tbTimeOut.Text = Properties.Settings.Default.TimeOut.ToString();
                form.tbCopyOwner.Text = Properties.Settings.Default.CopyOwner;
                form.tbCopyCode.Text = Data.MachineCode(Properties.Settings.Default.CopyOwner);
                form.tbCopyKey.Text = Properties.Settings.Default.CopyKey;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings settings = Properties.Settings.Default;
                    settings.Station = form.cbStation.SelectedIndex + 1;
                    int n = 0;
                    List<string> chanlist = new List<string>();
                    foreach (CheckBox cb in form.checkboxes)
                    {
                        if (cb.Checked &&
                            form.channelboxes[n].SelectedIndex >= 0 &&
                            form.comboboxes[n].Text.Trim().Length > 0)
                        {
                            chanlist.Add(String.Format("Channel{0}={1}",
                                form.channelboxes[n].Text, form.comboboxes[n].Text));
                        }
                        n++;
                    }
                    if (chanlist.Count > 0)
                        settings.Channels = String.Join("\t", chanlist.ToArray());
                    int timeout;
                    if (int.TryParse(form.tbTimeOut.Text, out timeout) &&
                        timeout >= 0)
                    {
                        settings.TimeOut = timeout;
                    }
                    if (settings.CopyOwner != form.tbCopyOwner.Text ||
                        settings.CopyKey != form.tbCopyKey.Text)
                    {
                        settings.CopyOwner = form.tbCopyOwner.Text;
                        settings.CopyKey = form.tbCopyKey.Text;
                        string mess = notifyIcon.Text;
                        if (!form.CheckAuthorization())
                            MessageBox.Show(this, "Ключ копии ошибочный!", "Авторизация",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    settings.Save();
                }
            }
        }

        private void frmFetchingModbus_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Minimized;
            }
        }

        private void frmFetchingModbus_Load(object sender, EventArgs e)
        {
            Process process = RunningInstance();
            if (process != null)
            {
                Application.Exit();
                return;
            }
            Data.RestoreSQLsettings(Application.StartupPath);
            TurnOnTime = DateTime.Now;
            LoadFirst();
        }

        private void LoadFirst()
        {
            // Проверка авторизации
            Properties.Settings settings = Properties.Settings.Default;
            Registered = Data.Authorization(settings.CopyOwner, settings.CopyKey);
            Bonus = !Registered;

            string configfilename = Application.StartupPath + "\\config.ini";
            if (File.Exists(configfilename))
            {
                MemIniFile mif = new MemIniFile(configfilename);
                string section = "Application";
                mif.WriteInteger(section, "Station", Properties.Settings.Default.Station);
                mif.WriteString(section, "StartupPath", Application.StartupPath);
                mif.WriteBool(section, "Registered", Registered);
                mif.WriteBool(section, "Bonus", Bonus);
                string[] channels = Properties.Settings.Default.Channels.Split('\t');

                // Загрузка плагина точек Modbus
                PointPlugin.LoadPlugin(Application.StartupPath + "\\Points.Modbus.dll");

                FetchModbusFunctions.StartFetch(
                    new Tuple<int, string[], string, exitApp, int>(
                    Properties.Settings.Default.Station,
                    channels, mif.ToString(), closeServer,
                    Properties.Settings.Default.TimeOut));
            }

        }

        private void closeServer()
        {
            Application.Exit();
        }

        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            // Просматриваем все процессы
            foreach (Process process in processes)
            {
                // игнорируем текщий процесс
                if (process.Id != current.Id)
                {
                    // проверяем, что процесс запущен из того же файла
                    if (Assembly.GetExecutingAssembly().Location.Replace(
                        "/", "\\") == current.MainModule.FileName)
                    {
                        // Да, это и есть копия нашего приложения
                        return process;
                    }
                }
            }
            // нет, таких процессов не найдено
            return null;
        }

        private void frmFetchingModbus_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
            {
                // Завершение
                FetchModbusFunctions.StopFetch();
            }
            else
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
            }
        }
    }
}
