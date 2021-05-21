namespace RemXcs
{
    partial class frmTuning
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
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.nudDisplayTimeout = new System.Windows.Forms.NumericUpDown();
            this.cbWindowMode = new System.Windows.Forms.CheckBox();
            this.cbSystemShell = new System.Windows.Forms.CheckBox();
            this.cbRootScheme = new System.Windows.Forms.ComboBox();
            this.cbSoundMode = new System.Windows.Forms.ComboBox();
            this.cbScreenSize = new System.Windows.Forms.ComboBox();
            this.cbScreensCount = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbStationNumber = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbStationName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbObjectName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabArchives = new System.Windows.Forms.TabPage();
            this.udTableGroups = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.udGroups = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbReports = new System.Windows.Forms.ComboBox();
            this.cbLogs = new System.Windows.Forms.ComboBox();
            this.cbMonths = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.cbDays = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cbHours = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cbMinutes = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbTrends = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.tabFetchServers = new System.Windows.Forms.TabPage();
            this.lvFetchServers = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextFetchServers = new System.Windows.Forms.ContextMenuStrip();
            this.miAddFetchServer = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeleteFetchServer = new System.Windows.Forms.ToolStripMenuItem();
            this.tabRemoteCameras = new System.Windows.Forms.TabPage();
            this.lvRemoteCameras = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextRemoteCameras = new System.Windows.Forms.ContextMenuStrip();
            this.tsmiAddRemoteCamera = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteRemoteCamera = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.openFetchServerDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDisplayTimeout)).BeginInit();
            this.tabArchives.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udTableGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udGroups)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabFetchServers.SuspendLayout();
            this.contextFetchServers.SuspendLayout();
            this.tabRemoteCameras.SuspendLayout();
            this.contextRemoteCameras.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabGeneral);
            this.tabControl1.Controls.Add(this.tabArchives);
            this.tabControl1.Controls.Add(this.tabFetchServers);
            this.tabControl1.Controls.Add(this.tabRemoteCameras);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(496, 293);
            this.tabControl1.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.nudDisplayTimeout);
            this.tabGeneral.Controls.Add(this.cbWindowMode);
            this.tabGeneral.Controls.Add(this.cbSystemShell);
            this.tabGeneral.Controls.Add(this.cbRootScheme);
            this.tabGeneral.Controls.Add(this.cbSoundMode);
            this.tabGeneral.Controls.Add(this.cbScreenSize);
            this.tabGeneral.Controls.Add(this.cbScreensCount);
            this.tabGeneral.Controls.Add(this.label23);
            this.tabGeneral.Controls.Add(this.label9);
            this.tabGeneral.Controls.Add(this.label22);
            this.tabGeneral.Controls.Add(this.label8);
            this.tabGeneral.Controls.Add(this.cbStationNumber);
            this.tabGeneral.Controls.Add(this.label6);
            this.tabGeneral.Controls.Add(this.label7);
            this.tabGeneral.Controls.Add(this.label2);
            this.tabGeneral.Controls.Add(this.tbStationName);
            this.tabGeneral.Controls.Add(this.label3);
            this.tabGeneral.Controls.Add(this.tbObjectName);
            this.tabGeneral.Controls.Add(this.label1);
            this.tabGeneral.Location = new System.Drawing.Point(4, 23);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(488, 266);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "Общие параметры";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // nudDisplayTimeout
            // 
            this.nudDisplayTimeout.Location = new System.Drawing.Point(157, 209);
            this.nudDisplayTimeout.Maximum = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.nudDisplayTimeout.Name = "nudDisplayTimeout";
            this.nudDisplayTimeout.Size = new System.Drawing.Size(48, 22);
            this.nudDisplayTimeout.TabIndex = 10;
            // 
            // cbWindowMode
            // 
            this.cbWindowMode.AutoSize = true;
            this.cbWindowMode.Location = new System.Drawing.Point(369, 239);
            this.cbWindowMode.Name = "cbWindowMode";
            this.cbWindowMode.Size = new System.Drawing.Size(106, 18);
            this.cbWindowMode.TabIndex = 9;
            this.cbWindowMode.Text = "Работа в окне";
            this.cbWindowMode.UseVisualStyleBackColor = true;
            this.cbWindowMode.Click += new System.EventHandler(this.cbWindowMode_Click);
            // 
            // cbSystemShell
            // 
            this.cbSystemShell.AutoSize = true;
            this.cbSystemShell.Location = new System.Drawing.Point(157, 184);
            this.cbSystemShell.Name = "cbSystemShell";
            this.cbSystemShell.Size = new System.Drawing.Size(134, 18);
            this.cbSystemShell.TabIndex = 9;
            this.cbSystemShell.Text = "Оболочка системы";
            this.cbSystemShell.UseVisualStyleBackColor = true;
            this.cbSystemShell.Click += new System.EventHandler(this.cbSystemShell_Click);
            // 
            // cbRootScheme
            // 
            this.cbRootScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRootScheme.FormattingEnabled = true;
            this.cbRootScheme.Items.AddRange(new object[] {
            "Отключен",
            "Динамик системного блока",
            "Звуковая карта"});
            this.cbRootScheme.Location = new System.Drawing.Point(157, 237);
            this.cbRootScheme.Name = "cbRootScheme";
            this.cbRootScheme.Size = new System.Drawing.Size(200, 22);
            this.cbRootScheme.TabIndex = 8;
            // 
            // cbSoundMode
            // 
            this.cbSoundMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSoundMode.FormattingEnabled = true;
            this.cbSoundMode.Items.AddRange(new object[] {
            "Отключен",
            "Динамик системного блока",
            "Звуковая карта"});
            this.cbSoundMode.Location = new System.Drawing.Point(157, 155);
            this.cbSoundMode.Name = "cbSoundMode";
            this.cbSoundMode.Size = new System.Drawing.Size(200, 22);
            this.cbSoundMode.TabIndex = 8;
            // 
            // cbScreenSize
            // 
            this.cbScreenSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScreenSize.FormattingEnabled = true;
            this.cbScreenSize.Items.AddRange(new object[] {
            "XGA (1024 x 768)",
            "XGA+ (1152 x 864)",
            "WXGA (1280 x 720)",
            "WXGA (1280 x 768)",
            "WXGA (1280 x 800)",
            "WXGA (1280 x 960)",
            "SXGA (1280 x 1024)",
            "SXGA (1360 x 768)",
            "SXGA+ (1400 x 1050)",
            "WXGA+ (1440 x 900)",
            "XJXGA (1540 x 940)",
            "WXGA++ (1600 x 900)",
            "WSXGA (1600 x 1024)",
            "WSXGA+ (1680 x 1050)",
            "UXGA (1600 x 1200)",
            "Full HD (1920 x 1080)",
            "WUXGA (1920 x 1200)",
            "QXGA (2048 x 1536)",
            "QWXGA (2048 x 1152)",
            "WQXGA (2560 x 1440)",
            "WQXGA (2560 x 1600)",
            "QSXGA (2560 x 2048)"});
            this.cbScreenSize.Location = new System.Drawing.Point(157, 99);
            this.cbScreenSize.Name = "cbScreenSize";
            this.cbScreenSize.Size = new System.Drawing.Size(200, 22);
            this.cbScreenSize.TabIndex = 6;
            // 
            // cbScreensCount
            // 
            this.cbScreensCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScreensCount.FormattingEnabled = true;
            this.cbScreensCount.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cbScreensCount.Location = new System.Drawing.Point(157, 127);
            this.cbScreensCount.Name = "cbScreensCount";
            this.cbScreensCount.Size = new System.Drawing.Size(48, 22);
            this.cbScreensCount.TabIndex = 7;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(211, 211);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(26, 14);
            this.label23.TabIndex = 0;
            this.label23.Text = "сек";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 240);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(145, 14);
            this.label9.TabIndex = 0;
            this.label9.Text = "Начальная мнемосхема:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(28, 211);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(123, 14);
            this.label22.TabIndex = 0;
            this.label22.Text = "Таймаут индикации:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(66, 158);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 14);
            this.label8.TabIndex = 0;
            this.label8.Text = "Звук станции:";
            // 
            // cbStationNumber
            // 
            this.cbStationNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStationNumber.FormattingEnabled = true;
            this.cbStationNumber.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cbStationNumber.Location = new System.Drawing.Point(157, 43);
            this.cbStationNumber.Name = "cbStationNumber";
            this.cbStationNumber.Size = new System.Drawing.Size(48, 22);
            this.cbStationNumber.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "Размер экрана:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 14);
            this.label7.TabIndex = 0;
            this.label7.Text = "Количество мониторов:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "Номер станции:";
            // 
            // tbStationName
            // 
            this.tbStationName.Location = new System.Drawing.Point(157, 71);
            this.tbStationName.Name = "tbStationName";
            this.tbStationName.Size = new System.Drawing.Size(318, 22);
            this.tbStationName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "Наименование станции:";
            // 
            // tbObjectName
            // 
            this.tbObjectName.Location = new System.Drawing.Point(157, 15);
            this.tbObjectName.Name = "tbObjectName";
            this.tbObjectName.Size = new System.Drawing.Size(318, 22);
            this.tbObjectName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Наименование объекта:";
            // 
            // tabArchives
            // 
            this.tabArchives.Controls.Add(this.udTableGroups);
            this.tabArchives.Controls.Add(this.label21);
            this.tabArchives.Controls.Add(this.udGroups);
            this.tabArchives.Controls.Add(this.groupBox1);
            this.tabArchives.Controls.Add(this.label20);
            this.tabArchives.Location = new System.Drawing.Point(4, 23);
            this.tabArchives.Name = "tabArchives";
            this.tabArchives.Padding = new System.Windows.Forms.Padding(3);
            this.tabArchives.Size = new System.Drawing.Size(488, 266);
            this.tabArchives.TabIndex = 2;
            this.tabArchives.Text = "Архивы";
            this.tabArchives.UseVisualStyleBackColor = true;
            // 
            // udTableGroups
            // 
            this.udTableGroups.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.udTableGroups.Location = new System.Drawing.Point(305, 233);
            this.udTableGroups.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.udTableGroups.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.udTableGroups.Name = "udTableGroups";
            this.udTableGroups.Size = new System.Drawing.Size(64, 22);
            this.udTableGroups.TabIndex = 3;
            this.udTableGroups.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(243, 235);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(59, 14);
            this.label21.TabIndex = 2;
            this.label21.Text = ", таблиц:";
            // 
            // udGroups
            // 
            this.udGroups.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.udGroups.Location = new System.Drawing.Point(178, 233);
            this.udGroups.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.udGroups.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.udGroups.Name = "udGroups";
            this.udGroups.Size = new System.Drawing.Size(62, 22);
            this.udGroups.TabIndex = 1;
            this.udGroups.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbReports);
            this.groupBox1.Controls.Add(this.cbLogs);
            this.groupBox1.Controls.Add(this.cbMonths);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.cbDays);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.cbHours);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.cbMinutes);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbTrends);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 220);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Автоматически удалять накопленные данные, через:";
            // 
            // cbReports
            // 
            this.cbReports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReports.FormattingEnabled = true;
            this.cbReports.Items.AddRange(new object[] {
            "не удалять",
            "1 сутки",
            "3 суток",
            "5 суток",
            "1 неделю",
            "10 суток",
            "2 недели",
            "1 месяц",
            "45 суток",
            "60 суток",
            "3 месяца",
            "6 месяцев",
            "1 год",
            "2 года",
            "3 года",
            "5 лет",
            "10 лет"});
            this.cbReports.Location = new System.Drawing.Point(241, 187);
            this.cbReports.Name = "cbReports";
            this.cbReports.Size = new System.Drawing.Size(121, 22);
            this.cbReports.TabIndex = 6;
            // 
            // cbLogs
            // 
            this.cbLogs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLogs.FormattingEnabled = true;
            this.cbLogs.Items.AddRange(new object[] {
            "не удалять",
            "1 сутки",
            "3 суток",
            "5 суток",
            "1 неделю",
            "10 суток",
            "2 недели",
            "1 месяц",
            "45 суток",
            "60 суток",
            "3 месяца",
            "6 месяцев",
            "1 год",
            "2 года",
            "3 года",
            "5 лет",
            "10 лет"});
            this.cbLogs.Location = new System.Drawing.Point(241, 159);
            this.cbLogs.Name = "cbLogs";
            this.cbLogs.Size = new System.Drawing.Size(121, 22);
            this.cbLogs.TabIndex = 5;
            // 
            // cbMonths
            // 
            this.cbMonths.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonths.FormattingEnabled = true;
            this.cbMonths.Items.AddRange(new object[] {
            "не удалять",
            "1 сутки",
            "3 суток",
            "5 суток",
            "1 неделю",
            "10 суток",
            "2 недели",
            "1 месяц",
            "45 суток",
            "60 суток",
            "3 месяца",
            "6 месяцев",
            "1 год",
            "2 года",
            "3 года",
            "5 лет",
            "10 лет"});
            this.cbMonths.Location = new System.Drawing.Point(241, 131);
            this.cbMonths.Name = "cbMonths";
            this.cbMonths.Size = new System.Drawing.Size(121, 22);
            this.cbMonths.TabIndex = 4;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(140, 190);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(95, 14);
            this.label19.TabIndex = 0;
            this.label19.Text = "Архив отчётов:";
            // 
            // cbDays
            // 
            this.cbDays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDays.FormattingEnabled = true;
            this.cbDays.Items.AddRange(new object[] {
            "не удалять",
            "1 сутки",
            "3 суток",
            "5 суток",
            "1 неделю",
            "10 суток",
            "2 недели",
            "1 месяц",
            "45 суток",
            "60 суток",
            "3 месяца",
            "6 месяцев",
            "1 год",
            "2 года",
            "3 года",
            "5 лет",
            "10 лет"});
            this.cbDays.Location = new System.Drawing.Point(241, 103);
            this.cbDays.Name = "cbDays";
            this.cbDays.Size = new System.Drawing.Size(121, 22);
            this.cbDays.TabIndex = 3;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(122, 162);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(113, 14);
            this.label18.TabIndex = 0;
            this.label18.Text = "Архивы журналов:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(47, 134);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(188, 14);
            this.label17.TabIndex = 0;
            this.label17.Text = "Месячные значения (таблицы):";
            // 
            // cbHours
            // 
            this.cbHours.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHours.FormattingEnabled = true;
            this.cbHours.Items.AddRange(new object[] {
            "не удалять",
            "1 сутки",
            "3 суток",
            "5 суток",
            "1 неделю",
            "10 суток",
            "2 недели",
            "1 месяц",
            "45 суток",
            "60 суток",
            "3 месяца",
            "6 месяцев",
            "1 год",
            "2 года",
            "3 года",
            "5 лет",
            "10 лет"});
            this.cbHours.Location = new System.Drawing.Point(241, 75);
            this.cbHours.Name = "cbHours";
            this.cbHours.Size = new System.Drawing.Size(121, 22);
            this.cbHours.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(50, 106);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(185, 14);
            this.label16.TabIndex = 0;
            this.label16.Text = "Суточные значения (таблицы):";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(57, 78);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(178, 14);
            this.label15.TabIndex = 0;
            this.label15.Text = "Часовые значения (таблицы):";
            // 
            // cbMinutes
            // 
            this.cbMinutes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMinutes.FormattingEnabled = true;
            this.cbMinutes.Items.AddRange(new object[] {
            "не удалять",
            "1 сутки",
            "3 суток",
            "5 суток",
            "1 неделю",
            "10 суток",
            "2 недели",
            "1 месяц",
            "45 суток",
            "60 суток",
            "3 месяца",
            "6 месяцев",
            "1 год",
            "2 года",
            "3 года",
            "5 лет",
            "10 лет"});
            this.cbMinutes.Location = new System.Drawing.Point(241, 47);
            this.cbMinutes.Name = "cbMinutes";
            this.cbMinutes.Size = new System.Drawing.Size(121, 22);
            this.cbMinutes.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(187, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "Минутные значения (таблицы):";
            // 
            // cbTrends
            // 
            this.cbTrends.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrends.FormattingEnabled = true;
            this.cbTrends.Items.AddRange(new object[] {
            "не удалять",
            "1 сутки",
            "3 суток",
            "5 суток",
            "1 неделю",
            "10 суток",
            "2 недели",
            "1 месяц",
            "45 суток",
            "60 суток",
            "3 месяца",
            "6 месяцев",
            "1 год",
            "2 года",
            "3 года",
            "5 лет",
            "10 лет"});
            this.cbTrends.Location = new System.Drawing.Point(241, 19);
            this.cbTrends.Name = "cbTrends";
            this.cbTrends.Size = new System.Drawing.Size(121, 22);
            this.cbTrends.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(119, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "Графики (тренды):";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(8, 235);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(165, 14);
            this.label20.TabIndex = 0;
            this.label20.Text = "Количество групп трендов:";
            // 
            // tabFetchServers
            // 
            this.tabFetchServers.Controls.Add(this.lvFetchServers);
            this.tabFetchServers.Location = new System.Drawing.Point(4, 23);
            this.tabFetchServers.Name = "tabFetchServers";
            this.tabFetchServers.Padding = new System.Windows.Forms.Padding(3);
            this.tabFetchServers.Size = new System.Drawing.Size(488, 266);
            this.tabFetchServers.TabIndex = 1;
            this.tabFetchServers.Text = "Загружаемые серверы опроса";
            this.tabFetchServers.UseVisualStyleBackColor = true;
            // 
            // lvFetchServers
            // 
            this.lvFetchServers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvFetchServers.ContextMenuStrip = this.contextFetchServers;
            this.lvFetchServers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFetchServers.FullRowSelect = true;
            this.lvFetchServers.HideSelection = false;
            this.lvFetchServers.Location = new System.Drawing.Point(3, 3);
            this.lvFetchServers.Name = "lvFetchServers";
            this.lvFetchServers.Size = new System.Drawing.Size(482, 260);
            this.lvFetchServers.TabIndex = 0;
            this.lvFetchServers.UseCompatibleStateImageBehavior = false;
            this.lvFetchServers.View = System.Windows.Forms.View.Details;
            this.lvFetchServers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbFetchServers_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Имя файла";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Описание";
            this.columnHeader2.Width = 305;
            // 
            // contextFetchServers
            // 
            this.contextFetchServers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAddFetchServer,
            this.miDeleteFetchServer});
            this.contextFetchServers.Name = "contextFetchServers";
            this.contextFetchServers.Size = new System.Drawing.Size(136, 48);
            this.contextFetchServers.Opening += new System.ComponentModel.CancelEventHandler(this.contextFetchServers_Opening);
            // 
            // miAddFetchServer
            // 
            this.miAddFetchServer.Name = "miAddFetchServer";
            this.miAddFetchServer.Size = new System.Drawing.Size(135, 22);
            this.miAddFetchServer.Text = "Добавить...";
            this.miAddFetchServer.Click += new System.EventHandler(this.miAddFetchServer_Click);
            // 
            // miDeleteFetchServer
            // 
            this.miDeleteFetchServer.Name = "miDeleteFetchServer";
            this.miDeleteFetchServer.Size = new System.Drawing.Size(135, 22);
            this.miDeleteFetchServer.Text = "Удалить";
            this.miDeleteFetchServer.Click += new System.EventHandler(this.miDeleteFetchServer_Click);
            // 
            // tabRemoteCameras
            // 
            this.tabRemoteCameras.Controls.Add(this.lvRemoteCameras);
            this.tabRemoteCameras.Location = new System.Drawing.Point(4, 23);
            this.tabRemoteCameras.Name = "tabRemoteCameras";
            this.tabRemoteCameras.Padding = new System.Windows.Forms.Padding(3);
            this.tabRemoteCameras.Size = new System.Drawing.Size(488, 266);
            this.tabRemoteCameras.TabIndex = 3;
            this.tabRemoteCameras.Text = "Видеомониторинг";
            this.tabRemoteCameras.UseVisualStyleBackColor = true;
            // 
            // lvRemoteCameras
            // 
            this.lvRemoteCameras.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.lvRemoteCameras.ContextMenuStrip = this.contextRemoteCameras;
            this.lvRemoteCameras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvRemoteCameras.FullRowSelect = true;
            this.lvRemoteCameras.HideSelection = false;
            this.lvRemoteCameras.Location = new System.Drawing.Point(3, 3);
            this.lvRemoteCameras.Name = "lvRemoteCameras";
            this.lvRemoteCameras.Size = new System.Drawing.Size(482, 260);
            this.lvRemoteCameras.TabIndex = 0;
            this.lvRemoteCameras.UseCompatibleStateImageBehavior = false;
            this.lvRemoteCameras.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Адрес сервера раздачи";
            this.columnHeader3.Width = 180;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Заголовок окна";
            this.columnHeader4.Width = 275;
            // 
            // contextRemoteCameras
            // 
            this.contextRemoteCameras.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddRemoteCamera,
            this.tsmiDeleteRemoteCamera});
            this.contextRemoteCameras.Name = "contextFetchServers";
            this.contextRemoteCameras.Size = new System.Drawing.Size(136, 48);
            // 
            // tsmiAddRemoteCamera
            // 
            this.tsmiAddRemoteCamera.Name = "tsmiAddRemoteCamera";
            this.tsmiAddRemoteCamera.Size = new System.Drawing.Size(135, 22);
            this.tsmiAddRemoteCamera.Text = "Добавить...";
            this.tsmiAddRemoteCamera.Click += new System.EventHandler(this.tsmiAddRemoteCamera_Click);
            // 
            // tsmiDeleteRemoteCamera
            // 
            this.tsmiDeleteRemoteCamera.Name = "tsmiDeleteRemoteCamera";
            this.tsmiDeleteRemoteCamera.Size = new System.Drawing.Size(135, 22);
            this.tsmiDeleteRemoteCamera.Text = "Удалить";
            this.tsmiDeleteRemoteCamera.Click += new System.EventHandler(this.tsmiDeleteRemoteCamera_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(349, 312);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ввод";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(430, 312);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // openFetchServerDialog
            // 
            this.openFetchServerDialog.DefaultExt = "exe";
            this.openFetchServerDialog.Filter = "Серверы опроса|Fetch*.exe|Серверы синхронизации|Sync*.exe";
            this.openFetchServerDialog.InitialDirectory = ".";
            this.openFetchServerDialog.Multiselect = true;
            this.openFetchServerDialog.RestoreDirectory = true;
            this.openFetchServerDialog.ShowReadOnly = true;
            this.openFetchServerDialog.Title = "Серверы опроса RemX";
            // 
            // frmTuning
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(521, 347);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnOk);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmTuning";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройка рабочей станции";
            this.Load += new System.EventHandler(this.frmTuning_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDisplayTimeout)).EndInit();
            this.tabArchives.ResumeLayout(false);
            this.tabArchives.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udTableGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udGroups)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabFetchServers.ResumeLayout(false);
            this.contextFetchServers.ResumeLayout(false);
            this.tabRemoteCameras.ResumeLayout(false);
            this.contextRemoteCameras.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabFetchServers;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabPage tabArchives;
        private System.Windows.Forms.ComboBox cbStationNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbStationName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbObjectName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbScreenSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbSystemShell;
        private System.Windows.Forms.ComboBox cbSoundMode;
        private System.Windows.Forms.ComboBox cbScreensCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbMonths;
        private System.Windows.Forms.ComboBox cbDays;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cbHours;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbMinutes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbTrends;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown udGroups;
        private System.Windows.Forms.ComboBox cbReports;
        private System.Windows.Forms.ComboBox cbLogs;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ContextMenuStrip contextFetchServers;
        private System.Windows.Forms.ToolStripMenuItem miAddFetchServer;
        private System.Windows.Forms.ToolStripMenuItem miDeleteFetchServer;
        private System.Windows.Forms.OpenFileDialog openFetchServerDialog;
        private System.Windows.Forms.NumericUpDown udTableGroups;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown nudDisplayTimeout;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.CheckBox cbWindowMode;
        private System.Windows.Forms.ComboBox cbRootScheme;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage tabRemoteCameras;
        private System.Windows.Forms.ListView lvFetchServers;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip contextRemoteCameras;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddRemoteCamera;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteRemoteCamera;
        private System.Windows.Forms.ListView lvRemoteCameras;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}