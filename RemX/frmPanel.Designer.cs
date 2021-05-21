namespace RemXcs
{
    using System.Security.Permissions;
    partial class frmPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPanel));
            this.pnlCaption = new System.Windows.Forms.Panel();
            this.lblDateStation = new System.Windows.Forms.Label();
            this.lblCaption = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.stpStatus = new System.Windows.Forms.StatusStrip();
            this.tlbStatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlbUserLogged = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlbSwitchMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlbAlarmMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnuMenu = new System.Windows.Forms.MenuStrip();
            this.miSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.miOverview = new System.Windows.Forms.ToolStripMenuItem();
            this.miUserRegistration = new System.Windows.Forms.ToolStripMenuItem();
            this.miOtherPrograms = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miRestart = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miLogs = new System.Windows.Forms.ToolStripMenuItem();
            this.miActiveAlarms = new System.Windows.Forms.ToolStripMenuItem();
            this.miActiveSwitchs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miAlarmsLog = new System.Windows.Forms.ToolStripMenuItem();
            this.miSwitchsLog = new System.Windows.Forms.ToolStripMenuItem();
            this.miUsersLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.miSystemLog = new System.Windows.Forms.ToolStripMenuItem();
            this.miStorage = new System.Windows.Forms.ToolStripMenuItem();
            this.miTrends = new System.Windows.Forms.ToolStripMenuItem();
            this.miTables = new System.Windows.Forms.ToolStripMenuItem();
            this.miReports = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.miReportLog = new System.Windows.Forms.ToolStripMenuItem();
            this.miTuning = new System.Windows.Forms.ToolStripMenuItem();
            this.miBaseEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.miSystemTuning = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.miSchemeEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.miReportEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miUsersEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.miTrendsGroups = new System.Windows.Forms.ToolStripMenuItem();
            this.miTablesGroups = new System.Windows.Forms.ToolStripMenuItem();
            this.miHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.miWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.miHardwareStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMonitoring = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrClock = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tmrStatusClear = new System.Windows.Forms.Timer(this.components);
            this.processDataEditor = new System.Diagnostics.Process();
            this.backgroundFetch = new System.ComponentModel.BackgroundWorker();
            this.pnlCaption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.stpStatus.SuspendLayout();
            this.mnuMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCaption
            // 
            this.pnlCaption.BackColor = System.Drawing.Color.Navy;
            this.pnlCaption.Controls.Add(this.lblDateStation);
            this.pnlCaption.Controls.Add(this.lblCaption);
            this.pnlCaption.Controls.Add(this.pbLogo);
            this.pnlCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCaption.Location = new System.Drawing.Point(0, 0);
            this.pnlCaption.Name = "pnlCaption";
            this.pnlCaption.Size = new System.Drawing.Size(816, 18);
            this.pnlCaption.TabIndex = 0;
            // 
            // lblDateStation
            // 
            this.lblDateStation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDateStation.AutoSize = true;
            this.lblDateStation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDateStation.ForeColor = System.Drawing.Color.White;
            this.lblDateStation.Location = new System.Drawing.Point(587, 1);
            this.lblDateStation.Name = "lblDateStation";
            this.lblDateStation.Size = new System.Drawing.Size(229, 14);
            this.lblDateStation.TabIndex = 2;
            this.lblDateStation.Text = "Станция №1, 26.12.10 Вс 16:12:01";
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCaption.ForeColor = System.Drawing.Color.White;
            this.lblCaption.Location = new System.Drawing.Point(45, 1);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(348, 14);
            this.lblCaption.TabIndex = 1;
            this.lblCaption.Text = "Автоматизированная система контроля и управления";
            // 
            // pbLogo
            // 
            this.pbLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(0, 0);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(42, 18);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // stpStatus
            // 
            this.stpStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbStatusMessage,
            this.tlbUserLogged,
            this.tlbSwitchMessage,
            this.tlbAlarmMessage});
            this.stpStatus.Location = new System.Drawing.Point(0, 297);
            this.stpStatus.Name = "stpStatus";
            this.stpStatus.Size = new System.Drawing.Size(816, 25);
            this.stpStatus.SizingGrip = false;
            this.stpStatus.TabIndex = 1;
            this.stpStatus.Text = "statusStrip1";
            // 
            // tlbStatusMessage
            // 
            this.tlbStatusMessage.AutoSize = false;
            this.tlbStatusMessage.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tlbStatusMessage.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tlbStatusMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tlbStatusMessage.IsLink = true;
            this.tlbStatusMessage.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.tlbStatusMessage.LinkColor = System.Drawing.SystemColors.ControlText;
            this.tlbStatusMessage.Name = "tlbStatusMessage";
            this.tlbStatusMessage.Size = new System.Drawing.Size(201, 20);
            this.tlbStatusMessage.Spring = true;
            this.tlbStatusMessage.Text = "Инициализация...";
            this.tlbStatusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlbUserLogged
            // 
            this.tlbUserLogged.AutoSize = false;
            this.tlbUserLogged.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tlbUserLogged.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tlbUserLogged.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tlbUserLogged.Image = ((System.Drawing.Image)(resources.GetObject("tlbUserLogged.Image")));
            this.tlbUserLogged.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tlbUserLogged.IsLink = true;
            this.tlbUserLogged.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.tlbUserLogged.LinkColor = System.Drawing.SystemColors.ControlText;
            this.tlbUserLogged.Name = "tlbUserLogged";
            this.tlbUserLogged.Size = new System.Drawing.Size(200, 20);
            this.tlbUserLogged.Text = "Нет регистрации";
            this.tlbUserLogged.VisitedLinkColor = System.Drawing.SystemColors.ControlText;
            this.tlbUserLogged.Click += new System.EventHandler(this.tlbUserLogged_Click);
            // 
            // tlbSwitchMessage
            // 
            this.tlbSwitchMessage.AutoSize = false;
            this.tlbSwitchMessage.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tlbSwitchMessage.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tlbSwitchMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tlbSwitchMessage.IsLink = true;
            this.tlbSwitchMessage.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.tlbSwitchMessage.LinkColor = System.Drawing.SystemColors.ControlText;
            this.tlbSwitchMessage.Name = "tlbSwitchMessage";
            this.tlbSwitchMessage.Size = new System.Drawing.Size(200, 20);
            this.tlbSwitchMessage.Click += new System.EventHandler(this.tlbSwitchMessage_Click);
            // 
            // tlbAlarmMessage
            // 
            this.tlbAlarmMessage.AutoSize = false;
            this.tlbAlarmMessage.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tlbAlarmMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tlbAlarmMessage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tlbAlarmMessage.IsLink = true;
            this.tlbAlarmMessage.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.tlbAlarmMessage.LinkColor = System.Drawing.SystemColors.ControlText;
            this.tlbAlarmMessage.Name = "tlbAlarmMessage";
            this.tlbAlarmMessage.Size = new System.Drawing.Size(200, 20);
            this.tlbAlarmMessage.Click += new System.EventHandler(this.tlbAlarmMessage_Click);
            // 
            // mnuMenu
            // 
            this.mnuMenu.AllowItemReorder = true;
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSystem,
            this.miLogs,
            this.miStorage,
            this.miTuning,
            this.miHelp,
            this.miWindow});
            this.mnuMenu.Location = new System.Drawing.Point(0, 18);
            this.mnuMenu.MdiWindowListItem = this.miWindow;
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.Size = new System.Drawing.Size(816, 24);
            this.mnuMenu.TabIndex = 2;
            this.mnuMenu.Text = "menuStrip1";
            // 
            // miSystem
            // 
            this.miSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOverview,
            this.miUserRegistration,
            this.miOtherPrograms,
            this.toolStripMenuItem1,
            this.miRestart,
            this.miExit});
            this.miSystem.Image = ((System.Drawing.Image)(resources.GetObject("miSystem.Image")));
            this.miSystem.MergeIndex = 0;
            this.miSystem.Name = "miSystem";
            this.miSystem.Size = new System.Drawing.Size(82, 20);
            this.miSystem.Text = "Система";
            // 
            // miOverview
            // 
            this.miOverview.Image = ((System.Drawing.Image)(resources.GetObject("miOverview.Image")));
            this.miOverview.Name = "miOverview";
            this.miOverview.Size = new System.Drawing.Size(181, 22);
            this.miOverview.Text = "Общий вид";
            this.miOverview.Click += new System.EventHandler(this.miOverview_Click);
            // 
            // miUserRegistration
            // 
            this.miUserRegistration.Image = ((System.Drawing.Image)(resources.GetObject("miUserRegistration.Image")));
            this.miUserRegistration.Name = "miUserRegistration";
            this.miUserRegistration.Size = new System.Drawing.Size(181, 22);
            this.miUserRegistration.Text = "Регистрация";
            this.miUserRegistration.Click += new System.EventHandler(this.miUserRegistration_Click);
            // 
            // miOtherPrograms
            // 
            this.miOtherPrograms.Image = ((System.Drawing.Image)(resources.GetObject("miOtherPrograms.Image")));
            this.miOtherPrograms.Name = "miOtherPrograms";
            this.miOtherPrograms.Size = new System.Drawing.Size(181, 22);
            this.miOtherPrograms.Text = "Другие программы";
            this.miOtherPrograms.Visible = false;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(178, 6);
            // 
            // miRestart
            // 
            this.miRestart.Image = global::RemXcs.Properties.Resources.exit;
            this.miRestart.Name = "miRestart";
            this.miRestart.Size = new System.Drawing.Size(181, 22);
            this.miRestart.Text = "Перезагрузка";
            this.miRestart.Click += new System.EventHandler(this.miRestart_Click);
            // 
            // miExit
            // 
            this.miExit.Image = ((System.Drawing.Image)(resources.GetObject("miExit.Image")));
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(181, 22);
            this.miExit.Text = "Выход";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miLogs
            // 
            this.miLogs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miActiveAlarms,
            this.miActiveSwitchs,
            this.toolStripMenuItem2,
            this.miAlarmsLog,
            this.miSwitchsLog,
            this.miUsersLog,
            this.toolStripMenuItem3,
            this.miSystemLog});
            this.miLogs.Image = ((System.Drawing.Image)(resources.GetObject("miLogs.Image")));
            this.miLogs.MergeIndex = 2;
            this.miLogs.Name = "miLogs";
            this.miLogs.Size = new System.Drawing.Size(88, 20);
            this.miLogs.Text = "Журналы";
            // 
            // miActiveAlarms
            // 
            this.miActiveAlarms.Image = ((System.Drawing.Image)(resources.GetObject("miActiveAlarms.Image")));
            this.miActiveAlarms.Name = "miActiveAlarms";
            this.miActiveAlarms.Size = new System.Drawing.Size(240, 22);
            this.miActiveAlarms.Text = "Аварийные сообщения";
            this.miActiveAlarms.Click += new System.EventHandler(this.miActiveAlarms_Click);
            // 
            // miActiveSwitchs
            // 
            this.miActiveSwitchs.Image = ((System.Drawing.Image)(resources.GetObject("miActiveSwitchs.Image")));
            this.miActiveSwitchs.Name = "miActiveSwitchs";
            this.miActiveSwitchs.Size = new System.Drawing.Size(240, 22);
            this.miActiveSwitchs.Text = "Переключения";
            this.miActiveSwitchs.Click += new System.EventHandler(this.miActiveSwitchs_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(237, 6);
            // 
            // miAlarmsLog
            // 
            this.miAlarmsLog.Image = ((System.Drawing.Image)(resources.GetObject("miAlarmsLog.Image")));
            this.miAlarmsLog.Name = "miAlarmsLog";
            this.miAlarmsLog.Size = new System.Drawing.Size(240, 22);
            this.miAlarmsLog.Text = "Архив аварийных сообщений";
            this.miAlarmsLog.Click += new System.EventHandler(this.miAlarmsLog_Click);
            // 
            // miSwitchsLog
            // 
            this.miSwitchsLog.Image = ((System.Drawing.Image)(resources.GetObject("miSwitchsLog.Image")));
            this.miSwitchsLog.Name = "miSwitchsLog";
            this.miSwitchsLog.Size = new System.Drawing.Size(240, 22);
            this.miSwitchsLog.Text = "Архив переключений";
            this.miSwitchsLog.Click += new System.EventHandler(this.miSwitchsLog_Click);
            // 
            // miUsersLog
            // 
            this.miUsersLog.Image = ((System.Drawing.Image)(resources.GetObject("miUsersLog.Image")));
            this.miUsersLog.Name = "miUsersLog";
            this.miUsersLog.Size = new System.Drawing.Size(240, 22);
            this.miUsersLog.Text = "Действия пользователя";
            this.miUsersLog.Click += new System.EventHandler(this.miUsersLog_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(237, 6);
            // 
            // miSystemLog
            // 
            this.miSystemLog.Image = ((System.Drawing.Image)(resources.GetObject("miSystemLog.Image")));
            this.miSystemLog.Name = "miSystemLog";
            this.miSystemLog.Size = new System.Drawing.Size(240, 22);
            this.miSystemLog.Text = "Сообщения системы";
            this.miSystemLog.Click += new System.EventHandler(this.miSystemLog_Click);
            // 
            // miStorage
            // 
            this.miStorage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miTrends,
            this.miTables,
            this.miReports,
            this.toolStripMenuItem5,
            this.miReportLog});
            this.miStorage.Image = ((System.Drawing.Image)(resources.GetObject("miStorage.Image")));
            this.miStorage.MergeIndex = 2;
            this.miStorage.Name = "miStorage";
            this.miStorage.Size = new System.Drawing.Size(69, 20);
            this.miStorage.Text = "Архив";
            this.miStorage.DropDownOpening += new System.EventHandler(this.miStorage_DropDownOpening);
            // 
            // miTrends
            // 
            this.miTrends.Image = ((System.Drawing.Image)(resources.GetObject("miTrends.Image")));
            this.miTrends.Name = "miTrends";
            this.miTrends.Size = new System.Drawing.Size(158, 22);
            this.miTrends.Text = "Графики";
            this.miTrends.Click += new System.EventHandler(this.miTrends_Click);
            // 
            // miTables
            // 
            this.miTables.Image = ((System.Drawing.Image)(resources.GetObject("miTables.Image")));
            this.miTables.Name = "miTables";
            this.miTables.Size = new System.Drawing.Size(158, 22);
            this.miTables.Text = "Таблицы";
            this.miTables.Click += new System.EventHandler(this.miTables_Click);
            // 
            // miReports
            // 
            this.miReports.Image = ((System.Drawing.Image)(resources.GetObject("miReports.Image")));
            this.miReports.Name = "miReports";
            this.miReports.Size = new System.Drawing.Size(158, 22);
            this.miReports.Text = "Отчёты";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(155, 6);
            this.toolStripMenuItem5.Visible = false;
            // 
            // miReportLog
            // 
            this.miReportLog.Enabled = false;
            this.miReportLog.Image = ((System.Drawing.Image)(resources.GetObject("miReportLog.Image")));
            this.miReportLog.Name = "miReportLog";
            this.miReportLog.Size = new System.Drawing.Size(158, 22);
            this.miReportLog.Text = "Архив отчётов";
            this.miReportLog.Visible = false;
            // 
            // miTuning
            // 
            this.miTuning.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miBaseEditor,
            this.miSystemTuning,
            this.toolStripMenuItem6,
            this.miSchemeEditor,
            this.miReportEdit,
            this.miUsersEditor,
            this.toolStripMenuItem4,
            this.miTrendsGroups,
            this.miTablesGroups});
            this.miTuning.Enabled = false;
            this.miTuning.Image = ((System.Drawing.Image)(resources.GetObject("miTuning.Image")));
            this.miTuning.MergeIndex = 2;
            this.miTuning.Name = "miTuning";
            this.miTuning.Size = new System.Drawing.Size(94, 20);
            this.miTuning.Text = "Настройка";
            // 
            // miBaseEditor
            // 
            this.miBaseEditor.Image = global::RemXcs.Properties.Resources.baseedit;
            this.miBaseEditor.Name = "miBaseEditor";
            this.miBaseEditor.Size = new System.Drawing.Size(258, 22);
            this.miBaseEditor.Text = "База данных";
            this.miBaseEditor.Click += new System.EventHandler(this.miBaseEditor_Click);
            // 
            // miSystemTuning
            // 
            this.miSystemTuning.Image = ((System.Drawing.Image)(resources.GetObject("miSystemTuning.Image")));
            this.miSystemTuning.Name = "miSystemTuning";
            this.miSystemTuning.Size = new System.Drawing.Size(258, 22);
            this.miSystemTuning.Text = "Настройка RemX";
            this.miSystemTuning.Click += new System.EventHandler(this.miSystemTuning_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(255, 6);
            // 
            // miSchemeEditor
            // 
            this.miSchemeEditor.Image = global::RemXcs.Properties.Resources.schemeedit;
            this.miSchemeEditor.Name = "miSchemeEditor";
            this.miSchemeEditor.Size = new System.Drawing.Size(258, 22);
            this.miSchemeEditor.Text = "Редактор мнемосхем";
            this.miSchemeEditor.Click += new System.EventHandler(this.miSchemeEditor_Click);
            // 
            // miReportEdit
            // 
            this.miReportEdit.Image = global::RemXcs.Properties.Resources.reportedit;
            this.miReportEdit.Name = "miReportEdit";
            this.miReportEdit.Size = new System.Drawing.Size(258, 22);
            this.miReportEdit.Text = "Редактор отчётов";
            this.miReportEdit.Click += new System.EventHandler(this.miReportEdit_Click);
            // 
            // miUsersEditor
            // 
            this.miUsersEditor.Image = global::RemXcs.Properties.Resources.users;
            this.miUsersEditor.Name = "miUsersEditor";
            this.miUsersEditor.Size = new System.Drawing.Size(258, 22);
            this.miUsersEditor.Text = "Редактор списка пользователей";
            this.miUsersEditor.Click += new System.EventHandler(this.miUsersEditor_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(255, 6);
            // 
            // miTrendsGroups
            // 
            this.miTrendsGroups.Image = global::RemXcs.Properties.Resources.groups;
            this.miTrendsGroups.Name = "miTrendsGroups";
            this.miTrendsGroups.Size = new System.Drawing.Size(258, 22);
            this.miTrendsGroups.Text = "Группы трендов";
            this.miTrendsGroups.Click += new System.EventHandler(this.miTrendsGroups_Click);
            // 
            // miTablesGroups
            // 
            this.miTablesGroups.Image = global::RemXcs.Properties.Resources.groups;
            this.miTablesGroups.Name = "miTablesGroups";
            this.miTablesGroups.Size = new System.Drawing.Size(258, 22);
            this.miTablesGroups.Text = "Группы таблиц";
            this.miTablesGroups.Click += new System.EventHandler(this.miTablesGroups_Click);
            // 
            // miHelp
            // 
            this.miHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAbout});
            this.miHelp.Image = ((System.Drawing.Image)(resources.GetObject("miHelp.Image")));
            this.miHelp.MergeIndex = 2;
            this.miHelp.Name = "miHelp";
            this.miHelp.Size = new System.Drawing.Size(80, 20);
            this.miHelp.Text = "Помощь";
            // 
            // miAbout
            // 
            this.miAbout.Image = ((System.Drawing.Image)(resources.GetObject("miAbout.Image")));
            this.miAbout.Name = "miAbout";
            this.miAbout.Size = new System.Drawing.Size(161, 22);
            this.miAbout.Text = "О программе...";
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // miWindow
            // 
            this.miWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miHardwareStatus,
            this.tsmiMonitoring,
            this.toolStripMenuItem8,
            this.tsmiHorizontal,
            this.tsmiVertical,
            this.tsmiCascade});
            this.miWindow.Image = ((System.Drawing.Image)(resources.GetObject("miWindow.Image")));
            this.miWindow.Name = "miWindow";
            this.miWindow.Size = new System.Drawing.Size(63, 20);
            this.miWindow.Text = "Окна";
            this.miWindow.Visible = false;
            this.miWindow.DropDownOpening += new System.EventHandler(this.miWindow_DropDownOpening);
            // 
            // miHardwareStatus
            // 
            this.miHardwareStatus.Name = "miHardwareStatus";
            this.miHardwareStatus.Size = new System.Drawing.Size(197, 22);
            this.miHardwareStatus.Text = "Компоненты системы";
            this.miHardwareStatus.Click += new System.EventHandler(this.miHardwareStatus_Click);
            // 
            // tsmiMonitoring
            // 
            this.tsmiMonitoring.Name = "tsmiMonitoring";
            this.tsmiMonitoring.Size = new System.Drawing.Size(197, 22);
            this.tsmiMonitoring.Text = "Видеомониторинг";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(194, 6);
            // 
            // tsmiHorizontal
            // 
            this.tsmiHorizontal.Name = "tsmiHorizontal";
            this.tsmiHorizontal.Size = new System.Drawing.Size(197, 22);
            this.tsmiHorizontal.Text = "Горизонтально";
            this.tsmiHorizontal.Click += new System.EventHandler(this.tsmiHorizontal_Click);
            // 
            // tsmiVertical
            // 
            this.tsmiVertical.Name = "tsmiVertical";
            this.tsmiVertical.Size = new System.Drawing.Size(197, 22);
            this.tsmiVertical.Text = "Вертикально";
            this.tsmiVertical.Click += new System.EventHandler(this.tsmiVertical_Click);
            // 
            // tsmiCascade
            // 
            this.tsmiCascade.Image = global::RemXcs.Properties.Resources.cascade;
            this.tsmiCascade.Name = "tsmiCascade";
            this.tsmiCascade.Size = new System.Drawing.Size(197, 22);
            this.tsmiCascade.Text = "Каскадом";
            this.tsmiCascade.Click += new System.EventHandler(this.tsmiCascade_Click);
            // 
            // tmrClock
            // 
            this.tmrClock.Enabled = true;
            this.tmrClock.Interval = 500;
            this.tmrClock.Tick += new System.EventHandler(this.tmrClock_Tick);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "plt";
            this.saveFileDialog.Filter = "*.plt|*.plt";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "plt";
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "*.plt|*.plt";
            // 
            // tmrStatusClear
            // 
            this.tmrStatusClear.Interval = 10000;
            this.tmrStatusClear.Tick += new System.EventHandler(this.tmrStatusClear_Tick);
            // 
            // processDataEditor
            // 
            this.processDataEditor.EnableRaisingEvents = true;
            this.processDataEditor.StartInfo.Domain = "";
            this.processDataEditor.StartInfo.LoadUserProfile = false;
            this.processDataEditor.StartInfo.Password = null;
            this.processDataEditor.StartInfo.StandardErrorEncoding = null;
            this.processDataEditor.StartInfo.StandardOutputEncoding = null;
            this.processDataEditor.StartInfo.UserName = "";
            this.processDataEditor.SynchronizingObject = this;
            this.processDataEditor.Exited += new System.EventHandler(this.processDataEditor_Exited);
            // 
            // backgroundFetch
            // 
            this.backgroundFetch.WorkerReportsProgress = true;
            this.backgroundFetch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundFetch_DoWork);
            this.backgroundFetch.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundFetch_ProgressChanged);
            // 
            // frmPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(816, 322);
            this.Controls.Add(this.stpStatus);
            this.Controls.Add(this.mnuMenu);
            this.Controls.Add(this.pnlCaption);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuMenu;
            this.Name = "frmPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Рабочая панель";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPanel_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPanel_FormClosed);
            this.Load += new System.EventHandler(this.frmPanel_Load);
            this.MdiChildActivate += new System.EventHandler(this.frmPanel_MdiChildActivate);
            this.Shown += new System.EventHandler(this.frmPanel_Shown);
            this.Resize += new System.EventHandler(this.frmPanel_Resize);
            this.pnlCaption.ResumeLayout(false);
            this.pnlCaption.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.stpStatus.ResumeLayout(false);
            this.stpStatus.PerformLayout();
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMenu;
        private System.Windows.Forms.ToolStripMenuItem miSystem;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem miOverview;
        private System.Windows.Forms.ToolStripMenuItem miUserRegistration;
        private System.Windows.Forms.ToolStripMenuItem miOtherPrograms;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miLogs;
        private System.Windows.Forms.ToolStripMenuItem miStorage;
        private System.Windows.Forms.ToolStripMenuItem miHelp;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lblDateStation;
        private System.Windows.Forms.Timer tmrClock;
        private System.Windows.Forms.ToolStripStatusLabel tlbStatusMessage;
        private System.Windows.Forms.ToolStripStatusLabel tlbSwitchMessage;
        private System.Windows.Forms.ToolStripStatusLabel tlbAlarmMessage;
        private System.Windows.Forms.ToolStripStatusLabel tlbUserLogged;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem miWindow;
        private System.Windows.Forms.ToolStripMenuItem miActiveAlarms;
        private System.Windows.Forms.ToolStripMenuItem miActiveSwitchs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem miAlarmsLog;
        private System.Windows.Forms.ToolStripMenuItem miSwitchsLog;
        private System.Windows.Forms.ToolStripMenuItem miUsersLog;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem miSystemLog;
        private System.Windows.Forms.ToolStripMenuItem miTrends;
        private System.Windows.Forms.ToolStripMenuItem miTables;
        private System.Windows.Forms.ToolStripMenuItem miReports;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem miReportLog;
        private System.Windows.Forms.ToolStripMenuItem miAbout;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Timer tmrStatusClear;
        private System.Diagnostics.Process processDataEditor;
        private System.Windows.Forms.ToolStripMenuItem miRestart;
        private System.ComponentModel.BackgroundWorker backgroundFetch;
        private System.Windows.Forms.ToolStripMenuItem tsmiHorizontal;
        private System.Windows.Forms.ToolStripMenuItem tsmiVertical;
        private System.Windows.Forms.ToolStripMenuItem tsmiCascade;
        public System.Windows.Forms.Panel pnlCaption;
        public System.Windows.Forms.StatusStrip stpStatus;
        private System.Windows.Forms.ToolStripMenuItem miTuning;
        private System.Windows.Forms.ToolStripMenuItem miSystemTuning;
        private System.Windows.Forms.ToolStripMenuItem miUsersEditor;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem miBaseEditor;
        private System.Windows.Forms.ToolStripMenuItem miTrendsGroups;
        private System.Windows.Forms.ToolStripMenuItem miSchemeEditor;
        private System.Windows.Forms.ToolStripMenuItem miReportEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiMonitoring;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem miHardwareStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem miTablesGroups;
    }
}