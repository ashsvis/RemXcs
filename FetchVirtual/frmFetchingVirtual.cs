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

namespace FetchVirtual
{
    public partial class frmFetchingVirtual : Form
    {
        private IDictionary<string, Entity> fetchlist = new Dictionary<string, Entity>();
        //int LastMin = -1; int LastHour = -1; int LastDay = -1; int LastMonth = -1;
        bool Registered = false;
        bool Bonus = false;
        DateTime TurnOnTime;
        string ClientID = String.Empty;

        public frmFetchingVirtual()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size();
        }

        private void frmFetchingVirtual_Load(object sender, EventArgs e)
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
                FetchVirtualFunctions.StartFetch(new Tuple<int, string, exitApp>(
                    Properties.Settings.Default.Station, mif.ToString(), closeServer));
            }

        }

        private void closeServer()
        {
            Application.Exit();
        }

        private void frmFetchingVirtual_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
            {
                // Завершение
                FetchVirtualFunctions.StopFetch();
            }
            else
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
            }
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

        private void frmFetchingVirtual_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Minimized;
            }

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
            using (frmTuningVirtuals form = new frmTuningVirtuals())
            {
                form.cbStation.SelectedIndex = Properties.Settings.Default.Station - 1;
                form.tbCopyOwner.Text = Properties.Settings.Default.CopyOwner;
                form.tbCopyCode.Text = Data.MachineCode(Properties.Settings.Default.CopyOwner);
                form.tbCopyKey.Text = Properties.Settings.Default.CopyKey;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings settings = Properties.Settings.Default;
                    settings.Station = form.cbStation.SelectedIndex + 1;
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
    }
}
