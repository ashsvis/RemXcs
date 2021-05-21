using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Management;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BaseServer;
using Draws.Plugins;
using IniFiles.Net;
using Points.Plugins;

namespace RemXcs
{
    public partial class frmMain : Form
    {
        private frmPanel[] panels;
        private DateTime baseVersion;
        int LastDay = -1;
        int LastMinute = -1;
        int LastHour = -1;

        public frmMain()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size();
        }

        public int StationNumber { get; set; }
        public UserLevel UserLevel { get; set; }
        public string UserName { get; set; }
        public bool AlarmAsked { get; set; }
        public string EntityAsked { get; set; }
        public static bool MustWinLogOff { get; set; }
        public string ClientID = String.Empty; 

        public frmAbout SplashForm;

        private void startFetchServices()
        {
            //string configfilename = Application.StartupPath + "\\config.ini";
            //if (File.Exists(configfilename))
            //{
            //    MemIniFile mif = new MemIniFile(configfilename);
            //    string section = "FetchServices";
            //    if (mif.SectionExists(section))
            //    {
            //        string content = mif.ToString();
            //        ///FetchKontrastFunctions.StartFetch(new string[] { Application.StartupPath,
            //        ///                                  content, StationNumber.ToString()});
            //        ///FetchVirtualFunctions.StartFetch(new string[] { Application.StartupPath,
            //        ///                                  content, StationNumber.ToString()});
            //        /*
            //        IDictionary<string, string> arrServices = new Dictionary<string, string>();
            //        // Create a new ManagementClass object binded to the Win32_Service WMI class
            //        using (ManagementClass mcServices = new ManagementClass("Win32_Service"))
            //        {
            //            // Loop through each service
            //            foreach (ManagementObject moService in mcServices.GetInstances())
            //            {
            //                // Create a new array that holds the ListBox item ID and service name
            //                arrServices.Add(
            //                        moService.GetPropertyValue("Name").ToString(),
            //                        moService.GetPropertyValue("Caption").ToString());
            //            }
            //        }
            //        foreach (string serviceControllerName in mif.ReadSectionKeys(section))
            //        {
            //            if (mif.ReadBool(section, serviceControllerName, false) &&
            //                arrServices.ContainsKey(serviceControllerName))
            //            {
            //                using (System.ServiceProcess.ServiceController s =
            //                    new System.ServiceProcess.ServiceController(serviceControllerName))
            //                {
            //                    //s.Start(new string[] { Application.StartupPath,
            //                    //                        mif.ToString(), StationNumber.ToString()});
            //                }
            //            }
            //        }
            //        */
            //    }
            //}
        }

        private void stopFetchServices()
        {
            ///FetchKontrastFunctions.StopFetch();
            ///FetchVirtualFunctions.StopFetch();
            /*
            string configfilename = Application.StartupPath + "\\config.ini";
            if (File.Exists(configfilename))
            {
                MemIniFile mif = new MemIniFile(configfilename);
                string section = "FetchServices";
                if (mif.SectionExists(section))
                {
                    IDictionary<string, string> arrServices = new Dictionary<string, string>();
                    // Create a new ManagementClass object binded to the Win32_Service WMI class
                    using (ManagementClass mcServices = new ManagementClass("Win32_Service"))
                    {
                        // Loop through each service
                        foreach (ManagementObject moService in mcServices.GetInstances())
                        {
                            // Create a new array that holds the ListBox item ID and service name
                            arrServices.Add(
                                    moService.GetPropertyValue("Name").ToString(),
                                    moService.GetPropertyValue("Caption").ToString());
                        }
                    }
                    foreach (string serviceControllerName in mif.ReadSectionKeys(section))
                    {
                        if (mif.ReadBool(section, serviceControllerName, false) &&
                            arrServices.ContainsKey(serviceControllerName))
                        {
                            using (System.ServiceProcess.ServiceController s =
                                new System.ServiceProcess.ServiceController(serviceControllerName))
                            {
                                //if (s.CanStop)
                                //    s.Stop();
                            }
                        }
                    }
                }
            }
           */
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            #region Защита от повторного запуска
            Process process = RunningInstance();
            if (process != null) { Application.Exit(); return; }
            #endregion
            this.Width = 0;
            this.Height = 1;
            MustWinLogOff = false;
            AlarmAsked = true;
            EntityAsked = String.Empty;
            this.StationNumber = Properties.Settings.Default.Station;
            SplashForm = new frmAbout();
            SplashForm.Show();
            SplashForm.Refresh();
            LoadUsersList(Application.StartupPath + "\\users.xml");
            IDictionary<string, IPointPlugin> plugins = PointPlugin.LoadPlugins(Application.StartupPath);
            DrawPlugin.LoadPlugins(Application.StartupPath);
            Data.RestoreSQLsettings(Application.StartupPath);
            Settings.CreateDataAndFetchBases();
            #region Создание кэша картинок на диске
            try { DrawPlugin.RestoreImageCatalog(Application.StartupPath + "\\images\\"); }
            catch (Exception ex)
            {
                Data.SendToSystemLog(StationNumber, "Картинки",
                    "Ошибка: " + ex.Message);
            }
            #endregion
            baseVersion = Data.LoadBase(plugins);
            timerClock.Enabled = true;
            timerFetch.Enabled = true;
            Data.SendToChangeLog(StationNumber, "Станция RemX", "Старт", String.Empty, "Работа",
                "Автономно", "Уровень доступа: " + UserLevel.ToString());
            Data.SendToSystemLog(StationNumber, "Станция RemX", "Рабочая станция загружена");
            ClientID = Data.ClientLogin(ClientID, "S", StationNumber, Properties.Settings.Default.StationName);
            LoadFetchServers();

            //if (ServerSQL.HostIsLocalhost()) startFetchServices();

            bool winmode = Properties.Settings.Default.WindowMode;
            Screen[] monitors = Screen.AllScreens;
            panels = new frmPanel[monitors.Length];
            for (int i = 0; i < monitors.Length; i++)
            {
                if (!winmode || winmode && monitors[i].Primary)
                {
                    panels[i] = new frmPanel(this, monitors[i].Primary, monitors[i].WorkingArea);
                    if (winmode)
                    {
                        panels[i].FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                        panels[i].pnlCaption.Visible = false;
                        panels[i].stpStatus.Visible = false;
                        panels[i].Size = new System.Drawing.Size(
                            Properties.Settings.Default.PanelWidth,
                            Properties.Settings.Default.PanelHeight);
                        panels[i].Location = new System.Drawing.Point(
                            Properties.Settings.Default.PanelLeft,
                            Properties.Settings.Default.PanelTop);
                    }
                    panels[i].Show(this);
                }
            }
            PeriodicalCheck();
        }

        /*  Примерчик получения доп. сведений о службах и их свойствах:
         * 
            ManagementClass mcServices = new ManagementClass("Win32_Service");
            // Loop through each service
            foreach (ManagementObject moService in mcServices.GetInstances())
            {
                // Get back the array with the index of the selected ListBox item from the ArrayList
                string[] srvArray = (string[])arrServices[lstServices.SelectedIndex];
                // If the current service name
                if (moService.GetPropertyValue("Name").ToString() == srvArray[1])
                {
                    // Set the fields accordingly
                    txtDesc.Text = moService.GetPropertyValue("Description").ToString();
                    lblPath.Text = "Path: " + moService.GetPropertyValue("PathName");
                    lblType.Text = "Type: " + moService.GetPropertyValue("ServiceType");
                    lblState.Text = "State:  " + moService.GetPropertyValue("State");
                    lblStartup.Text = "Start-up Type:  " + moService.GetPropertyValue("StartMode");
                }
            }
         * 
         */
        /* Примерчик для выкючения, перезагрузки компьютера:
         * 
         * Dim objWMIService, objComputer As Object
           objWMIService = GetObject("Winmgmts:{impersonationLevel=impersonate,(Debug,Shutdown)}")
           For Each objComputer In objWMIService.InstancesOf("Win32_OperatingSystem")
            objComputer.Win32shutdown(12, 0) '12 - выключить компьютер, 2 - перезагрузить
           Next
'полный список значений, которые можно использовать
'0 (0x0)       - Log Off
'4 (0x4)       - Forced Log Off (0 + 4)
'1 (0x1)       - Shutdown
'5 (0x5)       - Forced Shutdown (1 + 4)
'2 (0x2)       - Reboot
'6 (0x6)       - Forced Reboot (2 + 4)
'8 (0x8)       - Power Off
'12 (0xC)     - Forced Power Off (8 + 4)
         */

        public void SayEuAlarm()
        {
            if (!backEuAlarm.IsBusy)
            {
                if (backHiAlarm.IsBusy) backHiAlarm.CancelAsync();
                if (backLoAlarm.IsBusy) backLoAlarm.CancelAsync();
                backEuAlarm.RunWorkerAsync(Properties.Settings.Default.SoundMode);
            }
        }

        public void SayHiAlarm()
        {
            if (!backEuAlarm.IsBusy && !backHiAlarm.IsBusy)
            {
                if (backLoAlarm.IsBusy) backLoAlarm.CancelAsync();
                backHiAlarm.RunWorkerAsync(Properties.Settings.Default.SoundMode);
            }
        }

        public void SayLoAlarm()
        {
            if (!backEuAlarm.IsBusy && !backHiAlarm.IsBusy && !backLoAlarm.IsBusy)
                backLoAlarm.RunWorkerAsync(Properties.Settings.Default.SoundMode);
        }

        public void AlarmShortUp()
        {
            if (backEuAlarm.IsBusy) backEuAlarm.CancelAsync();
            if (backHiAlarm.IsBusy) backHiAlarm.CancelAsync();
            if (backLoAlarm.IsBusy) backLoAlarm.CancelAsync();
        }

        IDictionary<string, Process> FetchServers = new Dictionary<string, Process>();

        private void LoadFetchServers()
        {
            string loadedservers = Properties.Settings.Default.LoadedFetchServers;
            if (loadedservers.Length > 0)
            {
                string[] items = loadedservers.Split(new char[] { ';' });
                foreach (string item in items)
                {
                    string filename = Application.StartupPath + "\\" + item + ".exe";
                    if (File.Exists(filename))
                    {
                        string modulename = Path.GetFileNameWithoutExtension(filename);
                        if (!RunningProcess(modulename) && !FetchServers.ContainsKey(modulename))
                        {
                            ProcessStartInfo psi = new ProcessStartInfo(filename);
                            psi.CreateNoWindow = false;
                            Process proc = Process.Start(psi);
                            FetchServers.Add(modulename, proc);
                            Thread.Sleep(1000);
                        }
                    }
                }
            }
        }

        private void CloseFetchServers()
        {
            foreach (KeyValuePair<string, Process> kvp in FetchServers)
            {
                if (!kvp.Value.HasExited)
                {
                    kvp.Value.CloseMainWindow();
                    kvp.Value.Close();
                }
            }
            FetchServers.Clear();
        }

        private void KillFetchServers()
        {
            string loadedservers = Properties.Settings.Default.LoadedFetchServers;
            if (loadedservers.Length > 0)
            {
                string[] items = loadedservers.Split(new char[] { ';' });
                foreach (string item in items)
                {
                    string filename = Application.StartupPath + "\\" + item + ".exe";
                    int Id;
                    string processname = Path.GetFileNameWithoutExtension(filename);
                    if (frmMain.RunningProcess(processname, out Id))
                    {
                        frmMain.KillProcess(processname, Id);
                    }
                }
            }
        }

        public static void WinLogOff()
        {
            //Interaction.WindowsLogOff();
            OperatingWin32(0);
        }

        public static void OperatingWin32(int mode)
        {
            ManagementBaseObject mboShutdown = null;
            using (ManagementClass mcWin32 = new ManagementClass("Win32_OperatingSystem"))
            {
                mcWin32.Get();
                // без прав ничего не выйдет
                mcWin32.Scope.Options.EnablePrivileges = true;
                ManagementBaseObject mboShutdownParams =
                    mcWin32.GetMethodParameters("Win32Shutdown");
                // 0 - завершение сеанса
                // 1 - завершение работы системы
                // 2 - перезагрузка
                // 4 - принудительное завершение сеанса
                // 5 - принудительное завершение работы системы
                // 6 - принудительная перезагрузка
                // 8 - выключение питания
                // 12 - принудительное выключение питания
                mboShutdownParams["Flags"] = mode.ToString();
                mboShutdownParams["Reserved"] = "0";
                foreach (ManagementObject manObj in mcWin32.GetInstances())
                {
                    mboShutdown = manObj.InvokeMethod("Win32Shutdown", mboShutdownParams, null);
                }
            }
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            PanelBringToFront();
        }

        private void PanelBringToFront()
        {
            if (panels != null)
            {
                foreach (frmPanel pan in panels)
                {
                    pan.BringToFront();
                    if (pan.Primary)
                    {
                        pan.Focus();
                        break;
                    }
                }
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // финализация
            Data.SendToChangeLog(StationNumber, "Станция RemX", "Стоп", "Работа", String.Empty,
                ((UserName != null) ? UserName : "Автономно"), "Уровень доступа: Система");
            Data.SendToSystemLog(StationNumber, "Станция RemX", "Рабочая станция выгружена");
            //KillFetchServers();

            //if (ServerSQL.HostIsLocalhost()) stopFetchServices();

            CloseFetchServers();
            Data.ClientLogout(ClientID);
        }

        private void SendToAllPanels(string text)
        {
            if (panels != null)
            {
                IUpdatePanel updater;
                for (int i = 0; i < panels.Length; i++)
                    if (panels[i].Text.Length > 0)
                    {
                        updater = panels[i] as IUpdatePanel;
                        if (updater != null) updater.UpdateStatusMessage(text);
                    }
            }
        }

        private void UpdateViewToAllPanels()
        {
            if (panels != null)
            {
                IViewUpdate updater;
                for (int i = 0; i < panels.Length; i++)
                    if (panels[i].Text.Length > 0)
                    {
                        updater = panels[i] as IViewUpdate;
                        if (updater != null) updater.UpdateView();
                    }
            }
        }

        private void PeriodicalCheck()
        {
            // подача сигнала о работоспособности клиента
            if (!backImLive.IsBusy)
                backImLive.RunWorkerAsync(new object[] { ClientID, StationNumber,
                    Properties.Settings.Default.StationName } );
            // ------------------------------------------
            UpdateViewToAllPanels();
            int dd;
            int mm;
            int hh;
            DateTime Now = DateTime.Now;
            dd = Now.Day;
            hh = Now.Hour;
            mm = Now.Minute;
            if (LastMinute != mm)
            {
                LastMinute = mm;
                if (LastHour != hh)
                {
                    LastHour = hh;
                    if (LastDay != dd)
                    {
                        Cursor = Cursors.WaitCursor;
                        timerClock.Enabled = false;
                        try
                        {
                            LastDay = dd;
                            Properties.Settings settings = Properties.Settings.Default;
                            int minutes = settings.RemoveMinutes;
                            if (minutes > 0) Data.DeleteFromTable(calctime(Now, minutes), "minutes");
                            int hours = settings.RemoveHours;
                            if (hours > 0) Data.DeleteFromTable(calctime(Now, hours), "hours");
                            int days = settings.RemoveDays;
                            if (days > 0) Data.DeleteFromTable(calctime(Now, days), "days");
                            int months = settings.RemoveMonths;
                            if (months > 0) Data.DeleteFromTable(calctime(Now, months), "months");
                            int trends = settings.RemoveTrends;
                            if (trends > 0) Data.DeleteFromTrends(calctime(Now, trends));
                            int logs = settings.RemoveLogs;
                            if (logs > 0) Data.DeleteFromLogs(calctime(Now, logs));
                            int reports = settings.RemoveReports;
                            if (reports > 0) Data.DeleteFromReports(calctime(Now, reports));
                            SendToAllPanels("Готово");
                        }
                        finally
                        {
                            timerClock.Enabled = true;
                            Cursor = Cursors.Default;
                        }
                    }
                }
                AutoPrintReports(Now);
            }
            // Закрытие главного окна, когда все панели уже закрыты
            if (panels != null)
            {

                int panelscount = 0;
                for (int i = 0; i < panels.Length; i++)
                    if (!String.IsNullOrWhiteSpace(panels[i].Text)) panelscount++;
                if (panelscount == 0) Application.Exit();
            }
        }

        private static PrintReport printReport;
        private void AutoPrintReports(DateTime now)
        {
            MemIniFile mif = new MemIniFile(String.Empty);
            mif.FromString(Properties.Settings.Default.ReportsConfig);
            // выбор автозапускаемых отчётов на текущее время в список list
            List<string> list = new List<string>();
            foreach (string section in mif.ReadSections())
            {
                bool auto = mif.ReadBool(section, "PrintAuto", false);
                if (auto)
                {
                    string name = mif.ReadString(section, "ReportName", section);
                    DateTime time = mif.ReadDate(section, "PrintTime", DateTime.Parse("08:05:00"));
                    int period = mif.ReadInteger(section, "PrintPeriod", 0);
                    if (period == 0) // ежедневно
                    {
                        if (time.Hour == now.Hour && time.Minute == now.Minute)
                            list.Add(name);
                    }
                    else if (period == 1) // ежемесячно 1 числа
                    {
                        if (now.Day == 1 && time.Hour == now.Hour &&
                            time.Minute == now.Minute)
                            list.Add(name);
                    }
                }
            }
            // отправка на печать отчётов, печатаемых в настоящий момент
            foreach (string name in list)
            {
                using (PrintDocument printDoc = new PrintDocument())
                {
                    printDoc.PrintPage += printDoc_PrintPage;
                    printReport = new PrintReport(printDoc);
                    printReport.LoadReport(name, Properties.Settings.Default.ReportsConfig);
                    printDoc.Print();
                    printDoc.PrintPage -= printDoc_PrintPage;
                }

            }
        }

        private static void printDoc_PrintPage(object sender,
            System.Drawing.Printing.PrintPageEventArgs e)
        {
            printReport.PaintPage(e.Graphics);
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            PeriodicalCheck();
        }

        private DateTime calctime(DateTime now, int index)
        {
            switch (index)
            {
                case 1: return now.AddDays(-1);
                case 2: return now.AddDays(-3);
                case 3: return now.AddDays(-5);
                case 4: return now.AddDays(-7);
                case 5: return now.AddDays(-10);
                case 6: return now.AddDays(-14);
                case 7: return now.AddMonths(-1);
                case 8: return now.AddDays(-45);
                case 9: return now.AddDays(-60);
                case 10: return now.AddMonths(-3);
                case 11: return now.AddMonths(-6);
                case 12: return now.AddYears(-1);
                case 13: return now.AddYears(-2);
                case 14: return now.AddYears(-3);
                case 15: return now.AddYears(-5);
                case 16: return now.AddYears(-10);
                default: return now;
            }
        }

        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public static void SetForegroundOtherWindow(string ModuleName, string WindowName)
        {
            foreach (Process p in Process.GetProcesses(System.Environment.MachineName))
            {
                if (p.MainWindowHandle != IntPtr.Zero)
                {
                    if (String.Compare(p.MainModule.ModuleName, ModuleName, true) == 0 &&
                        p.MainWindowTitle.StartsWith(WindowName))
                    {
                        Interaction.SetForegroundWindow(p.MainWindowHandle);
                        return;
                    }
                }
            }
        }

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;

        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public static void CloseOtherWindow(string ModuleName, string WindowName = "")
        {
            foreach (Process p in Process.GetProcesses(System.Environment.MachineName))
            {
                if (p.MainWindowHandle != IntPtr.Zero)
                {
                    if (String.Compare(p.MainModule.ModuleName, ModuleName, true) == 0 &&
                        ((WindowName.Length > 0 && p.MainWindowTitle.StartsWith(WindowName)) ||
                        WindowName.Length == 0))
                    {
                        Interaction.SendMessage(p.MainWindowHandle, WM_SYSCOMMAND, SC_CLOSE, 0);
                        return;
                    }
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        struct COPYDATASTRUCT
        {
            public int dwData;
            public int cbData;
            public int lpData;
        }

        private const int WM_COPYDATA = 0x4A;

        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public static void SendMessageToOtherWindow(string strData, string ModuleName,
            string WindowName = "")
        {
            foreach (Process p in Process.GetProcesses(System.Environment.MachineName))
            {
                if (p.MainWindowHandle != IntPtr.Zero)
                {
                    if (String.Compare(p.MainModule.ModuleName, ModuleName, true) == 0 &&
                        ((WindowName.Length > 0 && p.MainWindowTitle.StartsWith(WindowName)) ||
                        WindowName.Length == 0))
                    {
                        Process current = Process.GetCurrentProcess();
                        COPYDATASTRUCT CDS = new COPYDATASTRUCT();
                        CDS.dwData = 0;         
                        byte[] Buff = ASCIIEncoding.Unicode.GetBytes(strData);
                        CDS.cbData = Buff.Length;
                        // Initialize unmanged memory to hold the struct.
                        IntPtr pnt = Marshal.AllocHGlobal(CDS.cbData);
                        try
                        {
                            Marshal.Copy(Buff, 0, pnt, CDS.cbData);
                            CDS.lpData = (int)pnt;
                            IntPtr ptrCDS = Marshal.AllocHGlobal(Marshal.SizeOf(CDS));
                            try
                            {
                                Marshal.StructureToPtr(CDS, ptrCDS, false);
                                Interaction.SendMessage(p.MainWindowHandle, WM_COPYDATA,
                                    current.MainWindowHandle.ToInt32(), ptrCDS.ToInt32());
                            }
                            finally
                            {
                                // Free the unmanaged memory.
                                Marshal.FreeHGlobal(ptrCDS);
                            }
                        }
                        finally
                        {
                            // Free the unmanaged memory.
                            Marshal.FreeHGlobal(pnt);
                        }
                    }
                }
            }
        }

        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public static void KillProcess(string ProcessName, int Id)
        {
            Process[] processes = Process.GetProcessesByName(ProcessName);
            // Просматриваем все процессы
            foreach (Process process in processes)
            {
                if (process.Id == Id)
                {
                    process.Kill();
                    return;
                }
            }
        }

        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public static bool RunningProcess(string ProcessName)
        {
            int Id;
            return RunningProcess(ProcessName, out Id);
        }

        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public static bool RunningProcess(string ProcessName, out int Id)
        {
            Process[] processes = Process.GetProcessesByName(ProcessName);
            // Просматриваем все процессы
            foreach (Process process in processes)
            {
                if (process.ProcessName == ProcessName)
                {
                    Id = process.Id;
                    return true;
                }
            }
            Id = 0;
            return false;
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

        private void backHiAlarm_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int mode = (int)e.Argument;
            BackgroundWorker worker = sender as BackgroundWorker;
            while (true)
            {
                if (worker.CancellationPending) { e.Cancel = true; break; }
                switch (mode)
                {
                    case 1:
                        Interaction.Beep(1800, 100);
                        break;
                    case 2:
                        SoundPlayer player = new SoundPlayer(Properties.Resources.buzzer);
                        player.PlaySync();
                        break;
                }
                Thread.Sleep(100);
            }
        }

        private void backLoAlarm_DoWork(object sender, DoWorkEventArgs e)
        {
            int mode = (int)e.Argument;
            BackgroundWorker worker = sender as BackgroundWorker;
            while (true)
            {
                if (worker.CancellationPending) { e.Cancel = true; break; }
                switch (mode)
                {
                    case 1:
                        Interaction.Beep(900, 100);
                        break;
                    case 2:
                        SoundPlayer player = new SoundPlayer(Properties.Resources.RING);
                        player.PlaySync();
                        break;
                }
                Thread.Sleep(100);
            }
        }

        private void backEuAlarm_DoWork(object sender, DoWorkEventArgs e)
        {
            int mode = (int)e.Argument;
            BackgroundWorker worker = sender as BackgroundWorker;
            while (true)
            {
                if (worker.CancellationPending) { e.Cancel = true; break; }
                switch (mode)
                {
                    case 1:
                        Interaction.Beep(2500, 100);
                        break;
                    case 2:
                        SoundPlayer player = new SoundPlayer(Properties.Resources.Infected);
                        player.PlaySync();
                        break;
                }
                Thread.Sleep(100);
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void backImLive_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] args = (object[])e.Argument;
            string clientID = (string)args[0];
            int stationNumber = (int)args[1];
            string stationName = (string)args[2];
            if (!Data.ImLive(clientID, "S", stationNumber, stationName))
            {
                clientID = Data.ClientLogin(clientID, "S", stationNumber, stationName);
                e.Result = clientID;
            }
        }

        private void backImLive_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
                ClientID = (string)e.Result;
        }

        private void timerFetch_Tick(object sender, EventArgs e)
        {
            if (!backCheckCommand.IsBusy)
                backCheckCommand.RunWorkerAsync(
                    new object[] { ClientID, Properties.Settings.Default.Station,
                        Properties.Settings.Default.StationName });
        }

        private void backCheckCommand_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] args = (object[])e.Argument;
            string clientID = (string)args[0];
            int stationNumber = (int)args[1];
            string stationName = (string)args[2];

            string[] command = Data.GetClientCommand(clientID);
            if (command[0].Equals("RELOAD"))
            {
                Data.ClearClientFetchList(clientID);
                Data.SendClientAnswers(clientID, "RELOADED");
                string mess = "Станция \"" + stationName +
                    "\" будет перезагружена по требованию";
                Data.SendToSystemLog(stationNumber, "Станция RemX", mess);
                e.Result = "RELOAD";
            }
            if (command[0].Equals("SHORTUP"))
            {
                Data.SendClientAnswers(clientID, "SHORTUPED");
                e.Result = "SHORTUP";
            }
        }

        private void backCheckCommand_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                switch ((string)e.Result)
                {
                    case "RELOAD":
                        Application.Restart();
                        break;
                    case "SHORTUP":
                        AlarmShortUp();
                        break;
                }
            }
        }
    }
}
