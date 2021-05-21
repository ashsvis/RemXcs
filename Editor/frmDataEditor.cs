using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using BaseServer;
using Draws.Plugins;
using IniFiles.Net;
using Points.Plugins;

namespace DataEditor
{
    public partial class frmDataEditor : Form
    {
        private List<Form> childforms = new List<Form>();
        private DateTime baseVersion;

        public frmDataEditor()
        {
            InitializeComponent();
        }

        [StructLayout(LayoutKind.Sequential)]
        struct COPYDATASTRUCT
        {
            public int dwData;
            public int cbData;
            public int lpData;
        }

        private const int WM_COPYDATA = 0x4A; 
        // Window proc that receives the WM_ messages for the associated window handle 
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        protected override void WndProc(ref Message m) 
        { 
           switch(m.Msg) 
           { 
             case WM_COPYDATA: 
                COPYDATASTRUCT CDS = 
                   (COPYDATASTRUCT)m.GetLParam(typeof(COPYDATASTRUCT)); 
                byte[] Buff = new byte[CDS.cbData]; 
                IntPtr lpData = new IntPtr(CDS.lpData); 
                Marshal.Copy(lpData, Buff, 0, CDS.cbData);
                string strData = Encoding.Unicode.GetString(Buff);
                //this.tlbStatusMessage.Text = strData;
                Program.CheckArguments(strData.Split(new char[] { ' ' }));
                SelectedFormShow(strData);
                break; 

             default: 
                // let the base class deal with it 
                base.WndProc(ref m); 
                break; 
           } 
        }

        private void SelectedFormShow(string strData)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Maximized;
            if (strData.IndexOf("-p") > 0)
            {
                string ptname = Properties.Settings.Default.CurrentPoint;
                if (ptname.Length > 0) showBaseEditor(ptname);
            }
            else if (strData.IndexOf("-d") > 0)
            {
                string schemename = Properties.Settings.Default.CurrentScheme;
                showSchemeEditor(schemename);
            }
            else if (strData.IndexOf("-t") > 0)
            {
                string reportname = Properties.Settings.Default.CurrentReport;
                showReportEditor(reportname);
            }
            else if (strData.IndexOf("-g") > 0)
            {
                int groupno = Properties.Settings.Default.CurrentGroup;
                showTableGroupEditor(ParamGroup.Trend, groupno);
            }
            else if (strData.IndexOf("-q") > 0)
            {
                int groupno = Properties.Settings.Default.CurrentGroup;
                showTableGroupEditor(ParamGroup.Table, groupno);
            }
            else if (strData.IndexOf("-u") > 0)
            {
                showUsersEditor();
            }
        }

        private void frmDataEditor_Load(object sender, EventArgs e)
        {
            Process process = RunningInstance();
            if (process != null)
            {
                Application.Exit();
                return;
            }
            Data.RestoreSQLsettings(Application.StartupPath);
            Settings.CreateDataAndFetchBases();
            baseVersion = Data.LoadBase(PointPlugin.LoadPlugins(Application.StartupPath));
            LoadUsersList();
            UpdateStatusMessage("Готово");
            SelectedFormShow(Program.argsString);
            //this.tlbStatusMessage.Text = Program.argsString;
            tmrLive.Enabled = true;
        }

        private void showBaseEditor(string ptname = "")
        {
            string Caption = "Редактор базы данных";
            frmBaseEditor form;
            Form found;
            if (FindAndShow(Caption, out found))
            {
                form = found as frmBaseEditor;
                form.BringToFront();
                if (form != null)
                    form.RestoreTreePos(ptname);
            }
            else
            {
                form = new frmBaseEditor(this, ptname);
                childforms.Add(form);
                form.Text = Caption;
                form.MdiParent = this;
                form.Show();
            }
        }

        private void showTableGroupEditor(ParamGroup kind, int groupno = 0)
        {
            string Caption = (kind == ParamGroup.Trend) ?
                "Редактор групп трендов" : "Редактор групп таблиц";
            frmTableGroupEditor form;
            Form found;
            if (FindAndShow(Caption, out found))
            {
                form = found as frmTableGroupEditor;
                form.BringToFront();
                if (form != null)
                    form.RestoreTreePos(String.Format("Группа {0}",
                                   groupno.ToString().PadLeft(3)));
            }
            else
            {
                form = new frmTableGroupEditor(this, groupno, kind);
                childforms.Add(form);
                form.Text = Caption;
                form.MdiParent = this;
                form.Show();
            }
        }

        private void showSchemeEditor(string schemename = "")
        {
            frmSchemeEditor form;
            if (schemename.Length > 0)
            {
                foreach (Form child in childforms)
                {
                    if (child is frmSchemeEditor)
                    {
                        form = (frmSchemeEditor)child;
                        if (form.SchemeName == schemename && form.Text.Length > 0)
                        {
                            if (form.WindowState == FormWindowState.Minimized)
                              form.WindowState = FormWindowState.Maximized;
                            form.BringToFront();
                            return;
                        }
                    }
                }
            }
            form = new frmSchemeEditor(this);
            childforms.Add(form);
            form.Text = "Редактор мнемосхем";
            form.MdiParent = this;
            form.Show();
            if (schemename.Length > 0)
                form.LoadScheme(schemename);
            else
                form.SelectOrNewScheme();
        }

        private void miBaseEditor_Click(object sender, EventArgs e)
        {
            showBaseEditor();
        }

        private void miTrendGroups_Click(object sender, EventArgs e)
        {
            showTableGroupEditor(ParamGroup.Trend);
        }

        private void miSchemeEditor_Click(object sender, EventArgs e)
        {
            showSchemeEditor();
        }

        private void miUserListEditor_Click(object sender, EventArgs e)
        {
            showUsersEditor();
        }

        private void showUsersEditor()
        {
            string Caption = "Редактор списка пользователей";
            frmUsers form;
            Form found;
            if (FindAndShow(Caption, out found))
            {
                form = found as frmUsers;
                form.BringToFront();
            }
            else
            {
                form = new frmUsers(this, UsersEditorMode.Правка);
                childforms.Add(form);
                form.Text = Caption;
                form.MdiParent = this;
                form.Show();
            }
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void miHorizontal_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void miVertical_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void miCascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private bool FindAndShow(string Caption, out Form foundForm)
        {
            bool found = false;
            foundForm = null;
            foreach (Form form in MdiChildren)
            {
                if (form.Text == Caption)
                {
                    foundForm = form;
                    form.Show();
                    form.BringToFront();
                    found = true;
                    break;
                }
            }
            return found;
        }

        private Form FindForm(string Caption)
        {
            foreach (Form form in MdiChildren)
            {
                if (form.Text == Caption)
                    return form;
            }
            return null;
        }

        private void miSaveAllDataInFiles_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Существующие данные на диске будут заменены! Продолжить?",
                "Сохранение данных на локальный диск пользователя", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                    string path = Application.StartupPath + "\\backup\\";
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    UpdateStatusMessage("Экспорт базы данных...");
                    Data.ExportBaseAs(path + "exportbase.ini", UpdateStatusMessage);
                    UpdateStatusMessage("Экспорт групп параметров трендов...");
                    Data.ExportGroupsAs(path + "exporttrends.ini", ParamGroup.Trend);
                    UpdateStatusMessage("Экспорт групп параметров таблиц...");
                    Data.ExportGroupsAs(path + "exporttables.ini", ParamGroup.Table);
                    UpdateStatusMessage("Экспорт мнемосхем...");
                    DrawPlugin.ExportSchemesAs(path + "schemes\\", UpdateStatusMessage);
                    UpdateStatusMessage("Экспорт картинок...");
                    DrawPlugin.ExportImagesAs(path + "images\\", UpdateStatusMessage);
                    UpdateStatusMessage("Экспорт отчётов...");
                    DrawPlugin.ExportReportsAs(path + "reports\\", UpdateStatusMessage,
                        Application.StartupPath + "\\reports.ini");
                    UpdateStatusMessage("Готово.");
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }

        }

        private void miRestoreServerDataFromFiles_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Существующие данные сервера будут заменены! Продолжить?",
    "Восстановление данных с локального диска на сервер", MessageBoxButtons.YesNoCancel,
    MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                    string path = Application.StartupPath + "\\backup\\";
                    string filename = path + "exportbase.ini";
                    if (File.Exists(filename))
                    {
                        UpdateStatusMessage("Импорт базы данных...");
                        IDictionary<string, IPointPlugin> plugins =
                            PointPlugin.LoadPlugins(Application.StartupPath);
                        Data.ImportBaseFrom(plugins, filename, UpdateStatusMessage);
                    }
                    filename = path + "exporttrends.ini";
                    if (File.Exists(filename))
                    {
                        UpdateStatusMessage("Импорт групп параметров трендов...");
                        Data.ImportGroupsFrom(filename, ParamGroup.Trend);
                    }
                    filename = path + "exporttables.ini";
                    if (File.Exists(filename))
                    {
                        UpdateStatusMessage("Импорт групп параметров таблиц...");
                        Data.ImportGroupsFrom(filename, ParamGroup.Table);
                    }
                    UpdateStatusMessage("Импорт динамических элементов мнемосхем...");
                    DrawPlugin.ImportSchemesFrom(path + "schemes\\", UpdateStatusMessage);
                    UpdateStatusMessage("Импорт файлов картинок...");
                    DrawPlugin.ImportImagesFrom(path + "images\\", UpdateStatusMessage);
                    UpdateStatusMessage("Импорт файлов отчётов...");
                    DrawPlugin.ImportReportsFrom(path + "reports\\", UpdateStatusMessage);
                    UpdateStatusMessage("Готово.");
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        public void UpdateStatusMessage(string message)
        {
            tlbStatusMessage.Text = message;
            stpStatus.Refresh();
            tmrStatusClear.Enabled = false;
            tmrStatusClear.Enabled = true;
        }

        private void tmrStatusClear_Tick(object sender, EventArgs e)
        {
            tlbStatusMessage.Text = "Нет сообщений";
            tmrStatusClear.Enabled = false;
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

        private void showReportEditor(string reportname = "")
        {
            frmReportEditor form;
            if (reportname.Length > 0)
            {
                foreach (Form child in childforms)
                {
                    if (child is frmReportEditor)
                    {
                        form = (frmReportEditor)child;
                        if (form.ReportName == reportname && form.Text.Length > 0)
                        {
                            if (form.WindowState == FormWindowState.Minimized)
                                form.WindowState = FormWindowState.Maximized;
                            form.BringToFront();
                            return;
                        }
                    }
                }
            }
            form = new frmReportEditor(this);
            childforms.Add(form);
            form.Text = "Редактор отчётов";
            form.MdiParent = this;
            form.Show();
            if (reportname.Length > 0)
                form.LoadReport(reportname);
            else
                form.SelectOrNewReport();
        }

        private void miReportEditor_Click(object sender, EventArgs e)
        {
            showReportEditor();
        }

        private void tmrLive_Tick(object sender, EventArgs e)
        {
            //Data.ImLive(ClientID);
        }

        private void frmDataEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Data.ClientLogout(ClientID);
        }

        private void miTuningLink_Click(object sender, EventArgs e)
        {
            using (frmTuning form = new frmTuning())
            {
                form.cbScreenSize.SelectedIndex = Properties.Settings.Default.ScreenSize;
                form.tbHostName.Text = BaseServer.Settings.Host;
                form.tbPortNumber.Text = BaseServer.Settings.Port;
                form.tbUserName.Text = BaseServer.Settings.User;
                form.tbPassword.Text = BaseServer.Settings.Password;
                form.tbFetchBase.Text = BaseServer.Settings.Fetchbase;
                form.tbDatabase.Text = BaseServer.Settings.Database;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings settings = Properties.Settings.Default;
                    settings.ScreenSize = form.cbScreenSize.SelectedIndex;
                    settings.Save();
                    if (BaseServer.Settings.Host != form.tbHostName.Text ||
                        BaseServer.Settings.Port != form.tbPortNumber.Text ||
                        BaseServer.Settings.User != form.tbUserName.Text ||
                        BaseServer.Settings.Password != form.tbPassword.Text ||
                        BaseServer.Settings.Fetchbase != form.tbFetchBase.Text ||
                        BaseServer.Settings.Database != form.tbDatabase.Text)
                    {
                        BaseServer.Settings.Host = form.tbHostName.Text;
                        BaseServer.Settings.Port = form.tbPortNumber.Text;
                        BaseServer.Settings.User = form.tbUserName.Text;
                        BaseServer.Settings.Password = form.tbPassword.Text;
                        BaseServer.Settings.Fetchbase = form.tbFetchBase.Text;
                        BaseServer.Settings.Database = form.tbDatabase.Text;

                        SaveSettings();

                        Data.RestoreSQLsettings(Application.StartupPath);
                        if (Settings.CreateDataAndFetchBases())
                        {
                            try { DrawPlugin.RestoreImageCatalog(Application.StartupPath + "\\images\\"); }
                            catch (Exception ex)
                            {
                                Data.SendToSystemLog(Properties.Settings.Default.Station, "Картинки",
                                    "Ошибка: " + ex.Message);
                            }
                            try { DrawPlugin.RestoreReportCatalog(Application.StartupPath + "\\reports\\"); }
                            catch (Exception ex)
                            {
                                Data.SendToSystemLog(Properties.Settings.Default.Station, "Отчёты",
                                    "Ошибка: " + ex.Message);
                            }

                            Data.LoadBase(PointPlugin.LoadPlugins(Application.StartupPath));
                            MessageBox.Show(this, "Подключение к SQL-серверу успешно восстановлено.",
                            "Настройка связи с SQL-сервером", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                           MessageBox.Show(this, "Не удалось восстановить подключение к SQL-серверу.\n" +
                                            Settings.LastError,
                             "Настройка связи с SQL-сервером", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private static void SaveSettings()
        {
            MemIniFile mif = new MemIniFile(Application.StartupPath + "\\config.ini");
            string section = "ServerSQL";
            mif.WriteString(section, "Host", Settings.Host);
            mif.WriteString(section, "Port", Settings.Port);
            mif.WriteString(section, "User", Settings.User);
            mif.WriteString(section, "Password", Settings.Password);
            mif.WriteString(section, "Fetchbase", Settings.Fetchbase);
            mif.WriteString(section, "Database", Settings.Database);
            mif.UpdateFile();
        }

        private void miTableGroups_Click(object sender, EventArgs e)
        {
            showTableGroupEditor(ParamGroup.Table);
        }
    }
}
