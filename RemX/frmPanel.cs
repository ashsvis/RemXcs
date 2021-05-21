using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BaseServer;
using Draws.Plugins;
using Points.Plugins;
using Microsoft.Win32;
using System.Security.Permissions;

namespace RemXcs
{
    public interface IUpdatePanel
    {
        void UpdateStatusMessage(string message);
        void UpdateUserLogged(string shortUsername, UserLevel userLevel);
        void UpdateSwitchMessage(string activeswitch);
        void UpdateAlarmMessage(string activealarm);
        void UpdateTuningMenu(bool enable);
        void UpdateCaptionText(string text);
        void UpdateCurrentSchemeName(string schemeName, string schemeCaption);
        void LoadScheme(string schemeName);
        string UserName();
        UserLevel UserLevel();
        //bool SQLServerConnected();
        void AlarmShortUp();
        void ShowBaseEditor(string ptname);
        void UpdateCurrentGroupNo(int groupno);
        string ClientID();
        bool FindAndShow(string caption);
        void CloseAndRemove(string caption);
        List<Form> ChildForms();
        Form GetHost();
        Size GetPanelSize();
    }

    public partial class frmPanel : Form, IUpdatePanel, IViewUpdate
    {
        private Rectangle WorkArea;
        private List<Form> childforms = new List<Form>();
        private string rootSchemeName = Properties.Settings.Default.RootScheme;
        private string currentSchemeName;
        private string currentSchemeCaption;
        private int currenGroupNo = 1;

        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public frmPanel(frmMain form, bool primary, Rectangle rect)
        {
            InitializeComponent();
            this.Host = form;
            WorkArea = rect;
            this.Primary = primary;
            currentSchemeName = rootSchemeName;
            currentSchemeCaption = this.Text;
        }

        public frmMain Host { get; set; }
        public bool Primary { get; set; }

// ================  Реализация IUpdatePanel ==========================================

        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public void ShowBaseEditor(string ptname = "")
        {
            showDataEditor(String.Format("-p\"{0}\"", ptname));
        }

        public void AlarmShortUp()
        {
            Host.AlarmAsked = true;
            Host.AlarmShortUp();
        }

        public Form GetHost()
        {
            return Host;
        }

        public void UpdateCaptionText(string text)
        {
            if (!Properties.Settings.Default.WindowMode)
            {
                this.Text = text;
                lblCaption.Text = text;
            }
            else
                this.Text = "RemX";
        }

        public void UpdateTuningMenu(bool enable)
        {
            miTuning.Enabled = enable;
        }
        public void UpdateStatusMessage(string message)
        {
            tlbStatusMessage.Text = message;
            stpStatus.Refresh();
            tmrStatusClear.Enabled = false;
            tmrStatusClear.Enabled = true;
        }
        public void UpdateUserLogged(string shortUsername, UserLevel userlevel)
        {
            string oldUsername = (Host.UserName != null) ? Host.UserName : String.Empty;
            Data.SendToChangeLog(Host.StationNumber, "Регистрация", "Пользователь",
                oldUsername, shortUsername,
                ((shortUsername.Equals("Нет регистрации")) ? oldUsername : shortUsername),
                "Уровень доступа: " + userlevel.ToString());
            tlbUserLogged.Text = shortUsername;
            Host.UserName = shortUsername;
            Host.UserLevel = userlevel;
        }
        public void UpdateSwitchMessage(string activeswitch)
        {
            tlbSwitchMessage.Text = activeswitch;
        }
        public void UpdateAlarmMessage(string activealarm)
        {
            tlbAlarmMessage.Text = activealarm;
        }
        public void UpdateCurrentSchemeName(string schemeName, string schemeCaption)
        {
            currentSchemeName = schemeName;
            currentSchemeCaption = schemeCaption;
        }

        public void UpdateCurrentGroupNo(int groupno)
        {
            currenGroupNo = groupno;
        }

        public Size GetPanelSize()
        {
            Size result;
            int screensize = Properties.Settings.Default.ScreenSize;
            switch (screensize)
            {
                case 0: result = new Size(1024, 768); break; //XGA (1024 x 768)
                case 1: result = new Size(1152, 864); break; //XGA+ (1152 x 864)
                case 2: result = new Size(1280, 720); break; //WXGA (1280 x 720)
                case 3: result = new Size(1280, 768); break; //WXGA (1280 x 768)
                case 4: result = new Size(1280, 800); break; //WXGA (1280 x 800)
                case 5: result = new Size(1280, 960); break; //WXGA (1280 x 960)
                case 6: result = new Size(1280, 1024); break; //SXGA (1280 x 1024)
                case 7: result = new Size(1360, 768); break; //SXGA (1360 x 768)
                case 8: result = new Size(1400, 1050); break; //SXGA+ (1400 x 1050)
                case 9: result = new Size(1440, 900); break; //WXGA+ (1440 x 900)
                case 10: result = new Size(1540, 940); break; //XJXGA (1540 x 940)
                case 11: result = new Size(1600, 900); break; //WXGA++ (1600 x 900)
                case 12: result = new Size(1600, 1024); break; //WSXGA (1600 x 1024)
                case 13: result = new Size(1680, 1050); break; //WSXGA+ (1680 x 1050)
                case 14: result = new Size(1600, 1200); break; //UXGA (1600 x 1200)
                case 15: result = new Size(1920, 1080); break; //Full HD (1920 x 1080)
                case 16: result = new Size(1920, 1200); break; //WUXGA (1920 x 1200)
                case 17: result = new Size(2048, 1536); break; //QXGA (2048 x 1536)
                case 18: result = new Size(2048, 1152); break; //QWXGA (2048 x 1152)
                case 19: result = new Size(2560, 1440); break; //WQXGA (2560 x 1440)
                case 20: result = new Size(2560, 1600); break; //WQXGA (2560 x 1600)
                case 21: result = new Size(2560, 2048); break; //QSXGA (2560 x 2048)
                default: result = new Size(1280, 1024); break;
            }
            result.Height -= pnlCaption.Height + mnuMenu.Height + stpStatus.Height + 4;
            result.Width -= 4;
            return result;
        }

        public void LoadScheme(string schemeName)
        {
            frmOverview form = new frmOverview(this);
            childforms.Add(form);
            form.MdiParent = this;
            form.Width = this.Width;
            form.Height = this.Height;
            form.LoadScheme(schemeName);
            form.Show();
        }

        public string UserName()
        {
            return Host.UserName;
        }

        public UserLevel UserLevel()
        {
            return Host.UserLevel;
        }

        public string ClientID()
        {
            return Host.ClientID;
        }

        public bool FindAndShow(string Caption)
        {
            bool found = false;
            foreach (Form form in MdiChildren)
            {
                if (form.Text == Caption)
                {
                    form.Show();
                    form.BringToFront();
                    found = true;
                    break;
                }
            }
            return found;
        }

        public void CloseAndRemove(string Caption)
        {
            foreach (Form form in MdiChildren)
            {
                if (form.Text == Caption)
                {
                    form.Close();
                    childforms.Remove(form);
                    break;
                }
            }
        }

        public List<Form> ChildForms()
        {
            return childforms;
        }

        // =====================================================================

        private void frmPanel_Load(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.WindowMode)
            {
                this.Left = WorkArea.X;
                this.Top = WorkArea.Y;
                this.Width = WorkArea.Width;
                this.Height = WorkArea.Height;
            }
            loadRootScheme();
        }

        private bool RunningAsShell()
        {
            RegistryKey regkey;
            using (regkey = Registry.CurrentUser.OpenSubKey(
                        "Software\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon"))
            {
                string exepath = (string)regkey.GetValue("Shell", String.Empty);
                return exepath.Equals(Application.ExecutablePath);
            }
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Работа станции RemX будет завершена. Продолжить?",
                "Завершение работы станции RemX", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) ==
                DialogResult.OK)
            {
                Host.Close();
                if (RunningAsShell() || frmMain.MustWinLogOff) frmMain.WinLogOff();
            }
        }

        private bool blink = false;
        private void tmrClock_Tick(object sender, EventArgs e)
        {
            string fs = String.Format("Станция №{0}, dd.MM.yy ddd HH:mm:ss",
                Host.StationNumber);
            lblDateStation.Text = DateTime.Now.ToString(fs);
            miWindow.Visible = this.MdiChildren.Length > 0;
            if (!backgroundFetch.IsBusy) backgroundFetch.RunWorkerAsync();
        }

        private void backgroundFetch_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            using (ServerSQL dataSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
            {
                using (ServerSQL fetchSQL = new ServerSQL(DatabaseFrom.Fetchbase, true)) // чтение
                {
                    if (dataSQL.Connected && fetchSQL.Connected)
                    {
                        #region Обработка LastAlarm
                        string lastalarm = Data.GetLastAlarm(fetchSQL);
                        if (lastalarm.Length > 0)
                        {
                            string ptname = lastalarm.Split(new char[] { '.' })[0];
                            IDictionary<string, string> Reals =
                                Data.GetRealValues(ptname, fetchSQL, dataSQL);
                            object[] arguments = new object[] { lastalarm, Reals };
                            worker.ReportProgress(0, arguments);
                        }
                        else
                        {
                            object[] arguments = new object[] { String.Empty,
                                        SystemColors.ButtonFace, SystemColors.ControlText};
                            worker.ReportProgress(0, arguments);
                        }
                        #endregion
                        #region Обработка LastSwitch
                        string lastswitch = Data.GetLastSwitch(fetchSQL);
                        if (lastswitch.Length > 0)
                        {
                            string ptname = lastswitch.Split(new char[] { '.' })[0];
                            IDictionary<string, string> Reals =
                                Data.GetRealValues(ptname, fetchSQL, dataSQL);
                            worker.ReportProgress(1, (Reals.ContainsKey("PVText")) ?
                                ptname + " [" + Reals["PVText"] + "] " : lastswitch);
                        }
                        else
                            worker.ReportProgress(1, String.Empty);
                        #endregion
                    }
                }
            }
        }

        private void backgroundFetch_ProgressChanged(object sender,
            System.ComponentModel.ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 0:
                    object[] arguments = (object[])e.UserState;
                    #region Обработка LastAlarm
                    if (arguments.Length == 2)
                    {
                        string lastalarm = (string)arguments[0];
                        IDictionary<string, string> Reals =
                            (IDictionary<string, string>)arguments[1];
                        if (lastalarm.Length > 0)
                        {
                            string ptname = lastalarm.Split(new char[] { '.' })[0];
                            tlbAlarmMessage.Text = lastalarm;
                            tlbAlarmMessage.BackColor = (Reals.ContainsKey("BackColor")) ?
                                Color.FromArgb(int.Parse(Reals["BackColor"])) : SystemColors.ButtonFace;
                            tlbAlarmMessage.LinkColor = (Reals.ContainsKey("ForeColor")) ?
                                Color.FromArgb(int.Parse(Reals["ForeColor"])) : SystemColors.ControlText;
                            bool quit = (Reals.ContainsKey("QuitAlarms")) ?
                                bool.Parse(Reals["QuitAlarms"]) : true;
                            bool alarm = (Reals.ContainsKey("HasAlarms")) ?
                                bool.Parse(Reals["HasAlarms"]) : false;
                            bool lostalarm = (Reals.ContainsKey("HasLostAlarms")) ?
                                bool.Parse(Reals["HasLostAlarms"]) : false;
                            if (!quit && blink)
                            {
                                Host.EntityAsked = ptname;
                                Host.AlarmAsked = false;
                                tlbAlarmMessage.BackColor = Color.Transparent;
                                tlbAlarmMessage.LinkColor = SystemColors.ControlText;
                            }
                            blink = !blink;
                            if (!Host.AlarmAsked)
                            {
                                if (Reals.ContainsKey("Alarms"))
                                {
                                    string alarms = Reals["Alarms"];
                                    if (alarms.Length > 0)
                                    {
                                        string[] items = alarms.Split(new char[] { ';' });
                                        bool found = false;
                                        foreach (string item in items)
                                        {
                                            if (item == "LE=False" || item == "HE=False")
                                            {
                                                Host.SayEuAlarm();
                                                found = true;
                                                break;
                                            }
                                        }
                                        if (!found)
                                            foreach (string item in items)
                                            {
                                                if (item == "LL=False" || item == "HH=False" ||
                                                    item == "LA=False" || item == "HA=False")
                                                {
                                                    Host.SayHiAlarm();
                                                    found = true;
                                                    break;
                                                }
                                            }
                                        if (!found)
                                            foreach (string item in items)
                                            {
                                                if (item == "LO=False" || item == "HI=False")
                                                {
                                                    Host.SayLoAlarm();
                                                    found = true;
                                                    break;
                                                }
                                            }
                                    }
                                }
                            }
                        }
                    }
                    else if (arguments.Length == 3)
                    {
                        tlbAlarmMessage.Text = (string)arguments[0];
                        tlbAlarmMessage.BackColor = (Color)arguments[1];
                        tlbAlarmMessage.ForeColor = (Color)arguments[2];
                    }
                    #endregion
                    break;
                case 1:
                    #region Обработка LastSwitch
                    tlbSwitchMessage.Text = (string)e.UserState;
                    #endregion
                    break;
            }
        }

        private void ShowRegisterDialog()
        {
            using (frmUsers form = new frmUsers(Host))
            {   // Выбор пользователя из списка
                DialogResult result = form.ShowDialog(this);
                if (result == DialogResult.Abort)
                {
                    IUserInfo ui = (IUserInfo)Host;
                    ui.ResetLogin();
                }
            }
        
        }
        private void miUserRegistration_Click(object sender, EventArgs e)
        {
            ShowRegisterDialog();
        }

        private void tlbUserLogged_Click(object sender, EventArgs e)
        {
            ShowRegisterDialog();
        }

        private void loadRootScheme()
        {
            string Caption = currentSchemeCaption;
            if (!FindAndShow(Caption))
            {
                frmOverview form = new frmOverview(this);
                childforms.Add(form);
                form.MdiParent = this;
                form.Width = this.Width;
                form.Height = this.Height;
                if (form.LoadScheme(currentSchemeName))
                    form.Show();
                else
                {
                    childforms.Remove(form);
                    form.Close();
                }
            }
            else
                if (currentSchemeName != rootSchemeName)
                {
                    foreach (Form frm in this.MdiChildren)
                    {
                        if (frm is frmOverview)
                        {
                            frmOverview form = (frmOverview)frm;
                            if (form.LoadScheme(rootSchemeName))
                                form.Show();
                            else
                            {
                                childforms.Remove(form);
                                form.Close();
                            }
                            break;
                        }
                    }
                }
        }

        private void miOverview_Click(object sender, EventArgs e)
        {
            loadRootScheme();
        }

        private void miAlarmsLog_Click(object sender, EventArgs e)
        {
            string Caption = "Архив аварийных значений";
            if (!FindAndShow(Caption))
            {
                frmLogs form = new frmLogs(this, "AlarmLog");
                childforms.Add(form);
                form.Text = Caption;
                form.MdiParent = this;
                form.Show();
            }
        }
        private void miUsersLog_Click(object sender, EventArgs e)
        {
            string Caption = "Действия пользователя";
            if (!FindAndShow(Caption))
            {
                frmLogs form = new frmLogs(this, "ChangeLog");
                childforms.Add(form);
                form.Text = Caption;
                form.MdiParent = this;
                form.Show();
            }
        }
        private void miSwitchsLog_Click(object sender, EventArgs e)
        {
            string Caption = "Архив переключений";
            if (!FindAndShow(Caption))
            {
                frmLogs form = new frmLogs(this, "SwitchLog");
                childforms.Add(form);
                form.Text = Caption;
                form.MdiParent = this;
                form.Show();
            }
        }
        private void miSystemLog_Click(object sender, EventArgs e)
        {
            string Caption = "Сообщения системы";
            if (!FindAndShow(Caption))
            {
                frmLogs form = new frmLogs(this, "SystemLog");
                childforms.Add(form);
                form.Text = Caption;
                form.MdiParent = this;
                form.Show();
            }
        }

        private void frmPanel_MdiChildActivate(object sender, EventArgs e)
        {
            miWindow.Visible = this.MdiChildren.Length > 0;
            if (this.ActiveMdiChild != null)
            {
                lblCaption.Text = this.ActiveMdiChild.Text;
            }
        }

        private void miAbout_Click(object sender, EventArgs e)
        {
            using (frmAbout form = new frmAbout())
            {
                form.ShowDialog();
            }
        }

        private void miSystemTuning_Click(object sender, EventArgs e)
        {
            using (frmTuning form = new frmTuning())
            {
                form.ShowDialog();
            }
        }

        private void miTrends_Click(object sender, EventArgs e)
        {
            string Caption = "Просмотр графиков группы 1";
            if (!FindAndShow(Caption))
            {
                frmTrends form = new frmTrends(Host, this);
                childforms.Add(form);
                form.Text = Caption;
                form.MdiParent = this;
                form.Show();
                form.UpdateView();
            }
        }

        private void tmrStatusClear_Tick(object sender, EventArgs e)
        {
            tlbStatusMessage.Text = "Нет сообщений";
            tmrStatusClear.Enabled = false;
        }

        private void miActiveAlarms_Click(object sender, EventArgs e)
        {
            ShowAlarms();
        }
 
        private void ShowAlarms()
        {
            string Caption = "Аварийные сообщения";
            if (!FindAndShow(Caption))
            {
                frmAlarms form = new frmAlarms(this);
                childforms.Add(form);
                form.Text = Caption;
                form.MdiParent = this;
                form.Show();
            }
        }
        private void miActiveSwitchs_Click(object sender, EventArgs e)
        {
            ShowSwitchs();
        }
        private void ShowSwitchs()
        {
            string Caption = "Актуальные переключения";
            if (!FindAndShow(Caption))
            {
                frmSwitchs form = new frmSwitchs(this);
                childforms.Add(form);
                form.Text = Caption;
                form.MdiParent = this;
                form.Show();
            }
        }
        private void tlbAlarmMessage_Click(object sender, EventArgs e)
        {
            ShowAlarms();
        }
        private void tlbSwitchMessage_Click(object sender, EventArgs e)
        {
            ShowSwitchs();
        }

        public void UpdateView()
        {
            foreach (Form form in MdiChildren)
            {
                if (!String.IsNullOrWhiteSpace(form.Text))
                {
                    IViewUpdate updater = form as IViewUpdate;
                    if (updater != null) updater.UpdateView();
                }
            }
        }

        //private WebBrowser PrepareWebBrowser()
        //{
        //    WebBrowser rep = new WebBrowser();
        //    rep.Visible = false;
        //    rep.Parent = this;
        //    rep.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(showReport);
        //    return rep;
        //}

        //private void showReport(object sender, WebBrowserDocumentCompletedEventArgs e)
        //{
        //    // Print the document now that it is fully loaded.
        //    //((WebBrowser)sender).Print();
        //    ((WebBrowser)sender).ShowPrintPreviewDialog();

        //    // Dispose the WebBrowser now that the task is complete. 
        //    ((WebBrowser)sender).Dispose();
        //}

        private void showSchemeEditor(string schemename)
        {
            showDataEditor(String.Format("-d\"{0}\"", schemename));
        }

        private void showGroupsEditor(ParamGroup kind, int groupno = 0)
        {
            switch (kind)
            {
                case ParamGroup.Trend:
                    showDataEditor(String.Format("-g{0}", groupno));
                    break;
                case ParamGroup.Table:
                    showDataEditor(String.Format("-q{0}", groupno));
                    break;
            }
        }

        private void showUsersEditor()
        {
            showDataEditor("-u");
        }

        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        private void showDataEditor(string addparams = "")
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                String arguments = String.Format("-s{0} -r\"{1}\"", Host.StationNumber,
                    Host.UserName);
                if (addparams.Length > 0) arguments += " " + addparams;
                string filename = Application.StartupPath + "\\DataEditor.exe";
                if (File.Exists(filename))
                {
                    if (!frmMain.RunningProcess(Path.GetFileNameWithoutExtension(filename)))
                    {
                        processDataEditor.StartInfo.FileName = filename;
                        processDataEditor.StartInfo.WorkingDirectory = Application.StartupPath;
                        processDataEditor.StartInfo.Arguments = arguments;
                        processDataEditor.Start();
                        Data.SendToSystemLog(Host.StationNumber, "Станция RemX",
                            "Редактор ресурсов загружен");
                    }
                    else
                    {
                        string modulename = Path.GetFileName(filename);
                        string windowname = "Редактор ресурсов RemX";
                        frmMain.SetForegroundOtherWindow(modulename, windowname);
                        frmMain.SendMessageToOtherWindow(arguments, modulename, windowname);
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void processDataEditor_Exited(object sender, EventArgs e)
        {
            Data.SendToSystemLog(Host.StationNumber, "Станция RemX", "Редактор ресурсов выгружен");
        }

        private void frmPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            string filename = Application.StartupPath + "\\" + processDataEditor.StartInfo.FileName;
            int Id;
            string processname = Path.GetFileNameWithoutExtension(filename);
            if (frmMain.RunningProcess(processname, out Id))
            {
                //frmMain.KillProcess(processname, Id);
                frmMain.CloseOtherWindow(Path.GetFileName(filename), "Редактор ресурсов RemX");
            }
        }

        private void miRestart_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Cтанция RemX будет перезапущена. Продолжить?",
                "Перезапуск рабочей станции", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) ==
                DialogResult.OK)
            {
                Data.SendToSystemLog(Host.StationNumber, "Cтанция RemX", "Перезапуск рабочей станции");
                Host.Close();
                frmMain.WinLogOff();
            }
        }

        private void frmPanel_Shown(object sender, EventArgs e)
        {
            if (Host.SplashForm != null)
            {
                Host.SplashForm.Close();
                Host.SplashForm = null;
            }
        }

        private void miBaseEditor_Click(object sender, EventArgs e)
        {
            ShowBaseEditor("#");
        }

        private void miSchemeEditor_Click(object sender, EventArgs e)
        {
            showSchemeEditor(currentSchemeName);
        }

        private void miTrendsGroups_Click(object sender, EventArgs e)
        {
            showGroupsEditor(ParamGroup.Trend, currenGroupNo);
        }

        private void miTablesGroups_Click(object sender, EventArgs e)
        {
            showGroupsEditor(ParamGroup.Table, currenGroupNo);
        }

        private void miUsersEditor_Click(object sender, EventArgs e)
        {
            showUsersEditor();
        }

        private void miStorage_DropDownOpening(object sender, EventArgs e)
        {
            miReports.DropDownItems.Clear();
            foreach (KeyValuePair<string, string> kvp in Data.GetReportsList())
            {
                ToolStripItem tsi = miReports.DropDownItems.Add(kvp.Value);
                tsi.Tag = kvp.Key;
                tsi.Click += miShowReportPreview_Click;
            }
        }

        private void miShowReportPreview_Click(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;
            string reportname = tsi.Tag.ToString();
            string Caption = tsi.Text;
            Cursor = Cursors.WaitCursor;
            try
            {
                if (!FindAndShow(Caption))
                {
                    frmReportPreview form = new frmReportPreview(reportname);
                    childforms.Add(form);
                    form.Text = Caption;
                    form.MdiParent = this;
                    form.Show();
                }
                else
                {
                    foreach (Form form in MdiChildren)
                    {
                        if (form.Text == Caption)
                        {
                            form.Show();
                            form.BringToFront();
                            ((frmReportPreview)form).LoadReport(reportname);
                            break;
                        }
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void miHardwareStatus_Click(object sender, EventArgs e)
        {
            string Caption = "Компоненты системы";
            if (!FindAndShow(Caption))
            {
                frmSystemStat form = new frmSystemStat(this);
                childforms.Add(form);
                form.Text = Caption;
                form.MdiParent = this;
                form.Show();
            }
        }

        private void showReportEditor(string reportname)
        {
            showDataEditor(String.Format("-t\"{0}\"", reportname));
        }

        private void miReportEdit_Click(object sender, EventArgs e)
        {
            showReportEditor(String.Empty);
        }

        private void tsmiHorizontal_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tsmiVertical_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void tsmiCascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void miTables_Click(object sender, EventArgs e)
        {
            string Caption = "Архив часовых значений группы 1";
            if (!FindAndShow(Caption))
            {
                frmTables form = new frmTables(Host, this, "hours", 1);
                childforms.Add(form);
                form.Text = Caption;
                form.MdiParent = this;
                form.Show();
            }
        }

        private void frmPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.WindowMode &&
                this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings settings = Properties.Settings.Default;
                if (settings.PanelLeft != this.Left ||
                    settings.PanelTop != this.Top ||
                    settings.PanelWidth != this.Width ||
                    settings.PanelHeight != this.Height)
                {
                    settings.PanelLeft = this.Left;
                    settings.PanelTop = this.Top;
                    settings.PanelWidth = this.Width;
                    settings.PanelHeight = this.Height;
                    settings.Save();
                }
            }
        }

        private void tsmiMonitoring1_Click(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;
            string Caption = tsi.Text;
            if (!FindAndShow(Caption))
            {
                frmRemoteView form = new frmRemoteView(tsi.Tag.ToString()); // "http://mobilepc:8080"
                childforms.Add(form);
                form.Text = Caption;
                form.MdiParent = this;
                form.Show();
            }
        }

        private void miWindow_DropDownOpening(object sender, EventArgs e)
        {
            tsmiMonitoring.DropDownItems.Clear();
            string remotecameras = Properties.Settings.Default.RemoteCameras;
            if (remotecameras.Length > 0)
            {
                tsmiMonitoring.Visible = true;
                string[] items = remotecameras.Split(new char[] { ';' });
                int n = 0;
                foreach (string item in items)
                {
                    if ((n & 0x01) > 0)
                        tsmiMonitoring.DropDownItems[
                            tsmiMonitoring.DropDownItems.Count - 1].Text = item;
                    else
                    {
                        ToolStripItem tsi = tsmiMonitoring.DropDownItems.Add(item);
                        tsi.Tag = item;
                        tsi.Click += tsmiMonitoring1_Click;
                    }
                    n++;
                }
            }
            else
                tsmiMonitoring.Visible = false;
        }

        private void frmPanel_Resize(object sender, EventArgs e)
        {
            foreach (Form form in childforms)
            {
                if (form is frmOverview)
                    ((frmOverview)form).drawBox.Invalidate();
            }
        }

    }
}
