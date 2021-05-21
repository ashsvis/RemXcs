namespace Points.Virtuals
{
    partial class frmVC
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
            this.components = new System.ComponentModel.Container();
            this.lbPtDesc = new System.Windows.Forms.Label();
            this.lbPV = new System.Windows.Forms.Label();
            this.lbPtName1 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lbPtType = new System.Windows.Forms.Label();
            this.lbRaw = new System.Windows.Forms.Label();
            this.lbFetchTime = new System.Windows.Forms.Label();
            this.lbAsked = new System.Windows.Forms.Label();
            this.lbLogged = new System.Windows.Forms.Label();
            this.lbActived = new System.Windows.Forms.Label();
            this.lbPtName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timerFetch = new System.Windows.Forms.Timer(this.components);
            this.label14 = new System.Windows.Forms.Label();
            this.lbPVCalc = new System.Windows.Forms.Label();
            this.btnQuit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbCommOnState = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbCommOffState = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbStateAlmState = new System.Windows.Forms.Label();
            this.lbStateOnState = new System.Windows.Forms.Label();
            this.lbOnColor = new System.Windows.Forms.Label();
            this.lbOn = new System.Windows.Forms.Label();
            this.lbOff = new System.Windows.Forms.Label();
            this.lbOffColor = new System.Windows.Forms.Label();
            this.backgroundFetch = new System.ComponentModel.BackgroundWorker();
            this.label3 = new System.Windows.Forms.Label();
            this.lbCommOnSource = new System.Windows.Forms.Label();
            this.lbCommOffSource = new System.Windows.Forms.Label();
            this.lbStateAlmSource = new System.Windows.Forms.Label();
            this.lbStateOnSource = new System.Windows.Forms.Label();
            this.lbStateOffSource = new System.Windows.Forms.Label();
            this.lbStateOffState = new System.Windows.Forms.Label();
            this.lbGroupFactTime = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbPtDesc
            // 
            this.lbPtDesc.AutoSize = true;
            this.lbPtDesc.ForeColor = System.Drawing.Color.Cyan;
            this.lbPtDesc.Location = new System.Drawing.Point(155, 9);
            this.lbPtDesc.Name = "lbPtDesc";
            this.lbPtDesc.Size = new System.Drawing.Size(56, 16);
            this.lbPtDesc.TabIndex = 43;
            this.lbPtDesc.Text = "PTDESC";
            // 
            // lbPV
            // 
            this.lbPV.AutoEllipsis = true;
            this.lbPV.BackColor = System.Drawing.Color.Transparent;
            this.lbPV.ForeColor = System.Drawing.Color.White;
            this.lbPV.Location = new System.Drawing.Point(40, 361);
            this.lbPV.Name = "lbPV";
            this.lbPV.Size = new System.Drawing.Size(82, 16);
            this.lbPV.TabIndex = 37;
            this.lbPV.Text = "------";
            this.lbPV.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbPtName1
            // 
            this.lbPtName1.ForeColor = System.Drawing.Color.Cyan;
            this.lbPtName1.Location = new System.Drawing.Point(23, 314);
            this.lbPtName1.Name = "lbPtName1";
            this.lbPtName1.Size = new System.Drawing.Size(120, 18);
            this.lbPtName1.TabIndex = 40;
            this.lbPtName1.Text = "PTNAME";
            this.lbPtName1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 361);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(24, 16);
            this.label17.TabIndex = 39;
            this.label17.Text = "PV";
            // 
            // lbPtType
            // 
            this.lbPtType.AutoSize = true;
            this.lbPtType.ForeColor = System.Drawing.Color.Cyan;
            this.lbPtType.Location = new System.Drawing.Point(311, 450);
            this.lbPtType.Name = "lbPtType";
            this.lbPtType.Size = new System.Drawing.Size(56, 16);
            this.lbPtType.TabIndex = 60;
            this.lbPtType.Text = "PTTYPE";
            // 
            // lbRaw
            // 
            this.lbRaw.AutoSize = true;
            this.lbRaw.ForeColor = System.Drawing.Color.White;
            this.lbRaw.Location = new System.Drawing.Point(319, 210);
            this.lbRaw.Name = "lbRaw";
            this.lbRaw.Size = new System.Drawing.Size(56, 16);
            this.lbRaw.TabIndex = 61;
            this.lbRaw.Text = "------";
            // 
            // lbFetchTime
            // 
            this.lbFetchTime.AutoEllipsis = true;
            this.lbFetchTime.ForeColor = System.Drawing.Color.Cyan;
            this.lbFetchTime.Location = new System.Drawing.Point(639, 117);
            this.lbFetchTime.Name = "lbFetchTime";
            this.lbFetchTime.Size = new System.Drawing.Size(88, 16);
            this.lbFetchTime.TabIndex = 63;
            this.lbFetchTime.Text = "1";
            // 
            // lbAsked
            // 
            this.lbAsked.AutoEllipsis = true;
            this.lbAsked.ForeColor = System.Drawing.Color.Cyan;
            this.lbAsked.Location = new System.Drawing.Point(639, 101);
            this.lbAsked.Name = "lbAsked";
            this.lbAsked.Size = new System.Drawing.Size(92, 16);
            this.lbAsked.TabIndex = 62;
            this.lbAsked.Text = "Нет";
            // 
            // lbLogged
            // 
            this.lbLogged.AutoEllipsis = true;
            this.lbLogged.ForeColor = System.Drawing.Color.Cyan;
            this.lbLogged.Location = new System.Drawing.Point(639, 85);
            this.lbLogged.Name = "lbLogged";
            this.lbLogged.Size = new System.Drawing.Size(92, 16);
            this.lbLogged.TabIndex = 51;
            this.lbLogged.Text = "Нет";
            // 
            // lbActived
            // 
            this.lbActived.AutoEllipsis = true;
            this.lbActived.ForeColor = System.Drawing.Color.Cyan;
            this.lbActived.Location = new System.Drawing.Point(639, 69);
            this.lbActived.Name = "lbActived";
            this.lbActived.Size = new System.Drawing.Size(92, 16);
            this.lbActived.TabIndex = 53;
            this.lbActived.Text = "Нет";
            // 
            // lbPtName
            // 
            this.lbPtName.AutoSize = true;
            this.lbPtName.ForeColor = System.Drawing.Color.Cyan;
            this.lbPtName.Location = new System.Drawing.Point(10, 9);
            this.lbPtName.Name = "lbPtName";
            this.lbPtName.Size = new System.Drawing.Size(56, 16);
            this.lbPtName.TabIndex = 55;
            this.lbPtName.Text = "PTNAME";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(225, 450);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "Тип точки";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(177, 210);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(128, 16);
            this.label41.TabIndex = 12;
            this.label41.Text = "Исходные данные";
            this.label41.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(545, 117);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(96, 16);
            this.label37.TabIndex = 10;
            this.label37.Text = "Опрос (сек)";
            this.label37.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(537, 101);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(104, 16);
            this.label35.TabIndex = 20;
            this.label35.Text = "Квитирование";
            this.label35.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(537, 85);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(104, 16);
            this.label33.TabIndex = 32;
            this.label33.Text = "Сигнализация";
            this.label33.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(545, 69);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(96, 16);
            this.label31.TabIndex = 22;
            this.label31.Text = "Опрос точки";
            this.label31.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(155, 255);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(64, 16);
            this.label22.TabIndex = 21;
            this.label22.Text = "Статус:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.ForeColor = System.Drawing.Color.Lime;
            this.label15.Location = new System.Drawing.Point(552, 41);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(104, 16);
            this.label15.TabIndex = 6;
            this.label15.Text = "Данные точки";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(214, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Подключения и сигнализация";
            // 
            // timerFetch
            // 
            this.timerFetch.Interval = 500;
            this.timerFetch.Tick += new System.EventHandler(this.timerFetch_Tick);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(169, 228);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(136, 16);
            this.label14.TabIndex = 12;
            this.label14.Text = "Расчётные данные";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbPVCalc
            // 
            this.lbPVCalc.AutoSize = true;
            this.lbPVCalc.ForeColor = System.Drawing.Color.White;
            this.lbPVCalc.Location = new System.Drawing.Point(319, 228);
            this.lbPVCalc.Name = "lbPVCalc";
            this.lbPVCalc.Size = new System.Drawing.Size(56, 16);
            this.lbPVCalc.TabIndex = 61;
            this.lbPVCalc.Text = "------";
            // 
            // btnQuit
            // 
            this.btnQuit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnQuit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnQuit.ForeColor = System.Drawing.Color.Black;
            this.btnQuit.Location = new System.Drawing.Point(30, 385);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(96, 23);
            this.btnQuit.TabIndex = 66;
            this.btnQuit.Text = "Квитировать";
            this.btnQuit.UseVisualStyleBackColor = false;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 16);
            this.label2.TabIndex = 23;
            this.label2.Text = "Команда \"ОТКРЫТЬ\"";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbCommOnState
            // 
            this.lbCommOnState.AutoEllipsis = true;
            this.lbCommOnState.ForeColor = System.Drawing.Color.White;
            this.lbCommOnState.Location = new System.Drawing.Point(434, 69);
            this.lbCommOnState.Name = "lbCommOnState";
            this.lbCommOnState.Size = new System.Drawing.Size(32, 16);
            this.lbCommOnState.TabIndex = 50;
            this.lbCommOnState.Text = "---";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(155, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 16);
            this.label4.TabIndex = 23;
            this.label4.Text = "Команда \"ЗАКРЫТЬ\"";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbCommOffState
            // 
            this.lbCommOffState.AutoEllipsis = true;
            this.lbCommOffState.ForeColor = System.Drawing.Color.White;
            this.lbCommOffState.Location = new System.Drawing.Point(434, 85);
            this.lbCommOffState.Name = "lbCommOffState";
            this.lbCommOffState.Size = new System.Drawing.Size(32, 16);
            this.lbCommOffState.TabIndex = 50;
            this.lbCommOffState.Text = "---";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.Color.Lime;
            this.label8.Location = new System.Drawing.Point(214, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(200, 16);
            this.label8.TabIndex = 5;
            this.label8.Text = "Подключения и управление";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(153, 145);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(152, 16);
            this.label9.TabIndex = 23;
            this.label9.Text = "Состояние \"АВАРИЯ\"";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(153, 161);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(160, 16);
            this.label10.TabIndex = 23;
            this.label10.Text = "Состояние \"ОТКРЫТО\"";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbStateAlmState
            // 
            this.lbStateAlmState.AutoEllipsis = true;
            this.lbStateAlmState.ForeColor = System.Drawing.Color.White;
            this.lbStateAlmState.Location = new System.Drawing.Point(434, 145);
            this.lbStateAlmState.Name = "lbStateAlmState";
            this.lbStateAlmState.Size = new System.Drawing.Size(32, 16);
            this.lbStateAlmState.TabIndex = 50;
            this.lbStateAlmState.Text = "---";
            // 
            // lbStateOnState
            // 
            this.lbStateOnState.AutoEllipsis = true;
            this.lbStateOnState.ForeColor = System.Drawing.Color.White;
            this.lbStateOnState.Location = new System.Drawing.Point(434, 161);
            this.lbStateOnState.Name = "lbStateOnState";
            this.lbStateOnState.Size = new System.Drawing.Size(32, 16);
            this.lbStateOnState.TabIndex = 50;
            this.lbStateOnState.Text = "---";
            // 
            // lbOnColor
            // 
            this.lbOnColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbOnColor.Location = new System.Drawing.Point(55, 69);
            this.lbOnColor.Name = "lbOnColor";
            this.lbOnColor.Size = new System.Drawing.Size(50, 50);
            this.lbOnColor.TabIndex = 67;
            // 
            // lbOn
            // 
            this.lbOn.ForeColor = System.Drawing.Color.Silver;
            this.lbOn.Location = new System.Drawing.Point(23, 131);
            this.lbOn.Name = "lbOn";
            this.lbOn.Size = new System.Drawing.Size(120, 18);
            this.lbOn.TabIndex = 40;
            this.lbOn.Text = "ОТКРЫТО";
            this.lbOn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbOff
            // 
            this.lbOff.ForeColor = System.Drawing.Color.Silver;
            this.lbOff.Location = new System.Drawing.Point(23, 255);
            this.lbOff.Name = "lbOff";
            this.lbOff.Size = new System.Drawing.Size(120, 18);
            this.lbOff.TabIndex = 40;
            this.lbOff.Text = "ЗАКРЫТО";
            this.lbOff.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbOffColor
            // 
            this.lbOffColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbOffColor.Location = new System.Drawing.Point(55, 193);
            this.lbOffColor.Name = "lbOffColor";
            this.lbOffColor.Size = new System.Drawing.Size(50, 50);
            this.lbOffColor.TabIndex = 67;
            // 
            // backgroundFetch
            // 
            this.backgroundFetch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundFetch_DoWork);
            this.backgroundFetch.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundFetch_RunWorkerCompleted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(153, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 16);
            this.label3.TabIndex = 23;
            this.label3.Text = "Состояние \"ЗАКРЫТО\"";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbCommOnSource
            // 
            this.lbCommOnSource.AutoEllipsis = true;
            this.lbCommOnSource.ForeColor = System.Drawing.Color.Cyan;
            this.lbCommOnSource.Location = new System.Drawing.Point(319, 69);
            this.lbCommOnSource.Name = "lbCommOnSource";
            this.lbCommOnSource.Size = new System.Drawing.Size(109, 16);
            this.lbCommOnSource.TabIndex = 53;
            this.lbCommOnSource.Text = "------";
            this.lbCommOnSource.DoubleClick += new System.EventHandler(this.lbCommOnSource_DoubleClick);
            // 
            // lbCommOffSource
            // 
            this.lbCommOffSource.AutoEllipsis = true;
            this.lbCommOffSource.ForeColor = System.Drawing.Color.Cyan;
            this.lbCommOffSource.Location = new System.Drawing.Point(319, 85);
            this.lbCommOffSource.Name = "lbCommOffSource";
            this.lbCommOffSource.Size = new System.Drawing.Size(109, 16);
            this.lbCommOffSource.TabIndex = 53;
            this.lbCommOffSource.Text = "------";
            this.lbCommOffSource.DoubleClick += new System.EventHandler(this.lbCommOnSource_DoubleClick);
            // 
            // lbStateAlmSource
            // 
            this.lbStateAlmSource.AutoEllipsis = true;
            this.lbStateAlmSource.ForeColor = System.Drawing.Color.Cyan;
            this.lbStateAlmSource.Location = new System.Drawing.Point(319, 145);
            this.lbStateAlmSource.Name = "lbStateAlmSource";
            this.lbStateAlmSource.Size = new System.Drawing.Size(109, 16);
            this.lbStateAlmSource.TabIndex = 53;
            this.lbStateAlmSource.Text = "------";
            this.lbStateAlmSource.DoubleClick += new System.EventHandler(this.lbCommOnSource_DoubleClick);
            // 
            // lbStateOnSource
            // 
            this.lbStateOnSource.AutoEllipsis = true;
            this.lbStateOnSource.ForeColor = System.Drawing.Color.Cyan;
            this.lbStateOnSource.Location = new System.Drawing.Point(319, 161);
            this.lbStateOnSource.Name = "lbStateOnSource";
            this.lbStateOnSource.Size = new System.Drawing.Size(109, 16);
            this.lbStateOnSource.TabIndex = 53;
            this.lbStateOnSource.Text = "------";
            this.lbStateOnSource.DoubleClick += new System.EventHandler(this.lbCommOnSource_DoubleClick);
            // 
            // lbStateOffSource
            // 
            this.lbStateOffSource.AutoEllipsis = true;
            this.lbStateOffSource.ForeColor = System.Drawing.Color.Cyan;
            this.lbStateOffSource.Location = new System.Drawing.Point(319, 177);
            this.lbStateOffSource.Name = "lbStateOffSource";
            this.lbStateOffSource.Size = new System.Drawing.Size(109, 16);
            this.lbStateOffSource.TabIndex = 53;
            this.lbStateOffSource.Text = "------";
            this.lbStateOffSource.DoubleClick += new System.EventHandler(this.lbCommOnSource_DoubleClick);
            // 
            // lbStateOffState
            // 
            this.lbStateOffState.AutoEllipsis = true;
            this.lbStateOffState.ForeColor = System.Drawing.Color.White;
            this.lbStateOffState.Location = new System.Drawing.Point(434, 177);
            this.lbStateOffState.Name = "lbStateOffState";
            this.lbStateOffState.Size = new System.Drawing.Size(32, 16);
            this.lbStateOffState.TabIndex = 50;
            this.lbStateOffState.Text = "---";
            // 
            // lbGroupFactTime
            // 
            this.lbGroupFactTime.AutoEllipsis = true;
            this.lbGroupFactTime.ForeColor = System.Drawing.Color.Cyan;
            this.lbGroupFactTime.Location = new System.Drawing.Point(638, 133);
            this.lbGroupFactTime.Name = "lbGroupFactTime";
            this.lbGroupFactTime.Size = new System.Drawing.Size(88, 16);
            this.lbGroupFactTime.TabIndex = 74;
            this.lbGroupFactTime.Text = "нет опроса";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(505, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 16);
            this.label5.TabIndex = 73;
            this.label5.Text = "Фактически (сек)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnSize
            // 
            this.btnSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSize.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSize.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnSize.Location = new System.Drawing.Point(703, 6);
            this.btnSize.Name = "btnSize";
            this.btnSize.Size = new System.Drawing.Size(22, 23);
            this.btnSize.TabIndex = 75;
            this.btnSize.Text = ">";
            this.btnSize.UseVisualStyleBackColor = false;
            this.btnSize.Click += new System.EventHandler(this.btnSize_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnClose.Location = new System.Drawing.Point(30, 443);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 23);
            this.btnClose.TabIndex = 77;
            this.btnClose.Tag = "CLOSE";
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOpen.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnOpen.Location = new System.Drawing.Point(30, 414);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(96, 23);
            this.btnOpen.TabIndex = 76;
            this.btnOpen.Tag = "OPEN";
            this.btnOpen.Text = "Открыть";
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // frmVC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(730, 475);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnSize);
            this.Controls.Add(this.lbGroupFactTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbOffColor);
            this.Controls.Add(this.lbOnColor);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbPtDesc);
            this.Controls.Add(this.lbOff);
            this.Controls.Add(this.lbPV);
            this.Controls.Add(this.lbOn);
            this.Controls.Add(this.lbPtName1);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.lbPtType);
            this.Controls.Add(this.lbPVCalc);
            this.Controls.Add(this.lbRaw);
            this.Controls.Add(this.lbFetchTime);
            this.Controls.Add(this.lbAsked);
            this.Controls.Add(this.lbLogged);
            this.Controls.Add(this.lbStateOffState);
            this.Controls.Add(this.lbStateOnState);
            this.Controls.Add(this.lbCommOffState);
            this.Controls.Add(this.lbStateAlmState);
            this.Controls.Add(this.lbCommOnState);
            this.Controls.Add(this.lbStateOffSource);
            this.Controls.Add(this.lbStateOnSource);
            this.Controls.Add(this.lbStateAlmSource);
            this.Controls.Add(this.lbCommOffSource);
            this.Controls.Add(this.lbCommOnSource);
            this.Controls.Add(this.lbActived);
            this.Controls.Add(this.lbPtName);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Silver;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVC";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Паспорт значения";
            this.Load += new System.EventHandler(this.frmFL_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmFL_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbPtDesc;
        private System.Windows.Forms.Label lbPV;
        private System.Windows.Forms.Label lbPtName1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lbPtType;
        private System.Windows.Forms.Label lbRaw;
        private System.Windows.Forms.Label lbFetchTime;
        private System.Windows.Forms.Label lbAsked;
        private System.Windows.Forms.Label lbLogged;
        private System.Windows.Forms.Label lbActived;
        private System.Windows.Forms.Label lbPtName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerFetch;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbPVCalc;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbCommOnState;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbCommOffState;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbStateAlmState;
        private System.Windows.Forms.Label lbStateOnState;
        private System.Windows.Forms.Label lbOnColor;
        private System.Windows.Forms.Label lbOn;
        private System.Windows.Forms.Label lbOff;
        private System.Windows.Forms.Label lbOffColor;
        private System.ComponentModel.BackgroundWorker backgroundFetch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbCommOnSource;
        private System.Windows.Forms.Label lbCommOffSource;
        private System.Windows.Forms.Label lbStateAlmSource;
        private System.Windows.Forms.Label lbStateOnSource;
        private System.Windows.Forms.Label lbStateOffSource;
        private System.Windows.Forms.Label lbStateOffState;
        private System.Windows.Forms.Label lbGroupFactTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSize;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOpen;

    }
}