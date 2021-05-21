namespace Points.KR500
{
    partial class frmAP
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
            this.lbEUDesc = new System.Windows.Forms.Label();
            this.lbPVEULO = new System.Windows.Forms.Label();
            this.lbPVEUHI = new System.Windows.Forms.Label();
            this.lbPV = new System.Windows.Forms.Label();
            this.lbPtName1 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lbAlgoblock = new System.Windows.Forms.Label();
            this.lbNode = new System.Windows.Forms.Label();
            this.lbPtType = new System.Windows.Forms.Label();
            this.lbChannel = new System.Windows.Forms.Label();
            this.lbOffset = new System.Windows.Forms.Label();
            this.lbRaw = new System.Windows.Forms.Label();
            this.lbPVFormat = new System.Windows.Forms.Label();
            this.lbActived = new System.Windows.Forms.Label();
            this.lbKoeff = new System.Windows.Forms.Label();
            this.lbPtName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.timerFetch = new System.Windows.Forms.Timer(this.components);
            this.label14 = new System.Windows.Forms.Label();
            this.lbPVCalc = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lbPVPercent = new System.Windows.Forms.Label();
            this.pbBar = new System.Windows.Forms.PictureBox();
            this.lbFetchGroup = new System.Windows.Forms.Label();
            this.lbGroupFetchTime = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.backgroundFetch = new System.ComponentModel.BackgroundWorker();
            this.lbGroupFactTime = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbPlace = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbRawDataType = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbFetchStatus = new System.Windows.Forms.Label();
            this.btnSize = new System.Windows.Forms.Button();
            this.btnChangeValue = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbBar)).BeginInit();
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
            // lbEUDesc
            // 
            this.lbEUDesc.ForeColor = System.Drawing.Color.Cyan;
            this.lbEUDesc.Location = new System.Drawing.Point(23, 332);
            this.lbEUDesc.Name = "lbEUDesc";
            this.lbEUDesc.Size = new System.Drawing.Size(120, 16);
            this.lbEUDesc.TabIndex = 42;
            this.lbEUDesc.Text = "EUDESC";
            this.lbEUDesc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbPVEULO
            // 
            this.lbPVEULO.AutoEllipsis = true;
            this.lbPVEULO.ForeColor = System.Drawing.Color.White;
            this.lbPVEULO.Location = new System.Drawing.Point(329, 395);
            this.lbPVEULO.Name = "lbPVEULO";
            this.lbPVEULO.Size = new System.Drawing.Size(172, 16);
            this.lbPVEULO.TabIndex = 47;
            this.lbPVEULO.Text = "PVEULO";
            this.lbPVEULO.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbPVEUHI
            // 
            this.lbPVEUHI.AutoEllipsis = true;
            this.lbPVEUHI.ForeColor = System.Drawing.Color.White;
            this.lbPVEUHI.Location = new System.Drawing.Point(329, 379);
            this.lbPVEUHI.Name = "lbPVEUHI";
            this.lbPVEUHI.Size = new System.Drawing.Size(172, 16);
            this.lbPVEUHI.TabIndex = 36;
            this.lbPVEUHI.Text = "PVEUHI";
            this.lbPVEUHI.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbPV
            // 
            this.lbPV.AutoEllipsis = true;
            this.lbPV.BackColor = System.Drawing.Color.Transparent;
            this.lbPV.ForeColor = System.Drawing.Color.White;
            this.lbPV.Location = new System.Drawing.Point(40, 361);
            this.lbPV.Name = "lbPV";
            this.lbPV.Size = new System.Drawing.Size(103, 16);
            this.lbPV.TabIndex = 37;
            this.lbPV.Text = "------";
            this.lbPV.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            // lbAlgoblock
            // 
            this.lbAlgoblock.AutoSize = true;
            this.lbAlgoblock.ForeColor = System.Drawing.Color.Cyan;
            this.lbAlgoblock.Location = new System.Drawing.Point(770, 376);
            this.lbAlgoblock.Name = "lbAlgoblock";
            this.lbAlgoblock.Size = new System.Drawing.Size(48, 16);
            this.lbAlgoblock.TabIndex = 38;
            this.lbAlgoblock.Text = "BLOCK";
            // 
            // lbNode
            // 
            this.lbNode.AutoSize = true;
            this.lbNode.ForeColor = System.Drawing.Color.Cyan;
            this.lbNode.Location = new System.Drawing.Point(770, 360);
            this.lbNode.Name = "lbNode";
            this.lbNode.Size = new System.Drawing.Size(40, 16);
            this.lbNode.TabIndex = 49;
            this.lbNode.Text = "NODE";
            // 
            // lbPtType
            // 
            this.lbPtType.AutoSize = true;
            this.lbPtType.ForeColor = System.Drawing.Color.Cyan;
            this.lbPtType.Location = new System.Drawing.Point(770, 328);
            this.lbPtType.Name = "lbPtType";
            this.lbPtType.Size = new System.Drawing.Size(56, 16);
            this.lbPtType.TabIndex = 60;
            this.lbPtType.Text = "PTTYPE";
            // 
            // lbChannel
            // 
            this.lbChannel.AutoSize = true;
            this.lbChannel.ForeColor = System.Drawing.Color.Cyan;
            this.lbChannel.Location = new System.Drawing.Point(770, 344);
            this.lbChannel.Name = "lbChannel";
            this.lbChannel.Size = new System.Drawing.Size(64, 16);
            this.lbChannel.TabIndex = 59;
            this.lbChannel.Text = "CHANNEL";
            // 
            // lbOffset
            // 
            this.lbOffset.AutoSize = true;
            this.lbOffset.ForeColor = System.Drawing.Color.Cyan;
            this.lbOffset.Location = new System.Drawing.Point(329, 165);
            this.lbOffset.Name = "lbOffset";
            this.lbOffset.Size = new System.Drawing.Size(56, 16);
            this.lbOffset.TabIndex = 58;
            this.lbOffset.Text = "OFFSET";
            // 
            // lbRaw
            // 
            this.lbRaw.AutoSize = true;
            this.lbRaw.ForeColor = System.Drawing.Color.White;
            this.lbRaw.Location = new System.Drawing.Point(329, 196);
            this.lbRaw.Name = "lbRaw";
            this.lbRaw.Size = new System.Drawing.Size(56, 16);
            this.lbRaw.TabIndex = 61;
            this.lbRaw.Text = "------";
            // 
            // lbPVFormat
            // 
            this.lbPVFormat.AutoEllipsis = true;
            this.lbPVFormat.ForeColor = System.Drawing.Color.Cyan;
            this.lbPVFormat.Location = new System.Drawing.Point(762, 214);
            this.lbPVFormat.Name = "lbPVFormat";
            this.lbPVFormat.Size = new System.Drawing.Size(88, 16);
            this.lbPVFormat.TabIndex = 50;
            this.lbPVFormat.Text = "Нет";
            // 
            // lbActived
            // 
            this.lbActived.AutoEllipsis = true;
            this.lbActived.ForeColor = System.Drawing.Color.Cyan;
            this.lbActived.Location = new System.Drawing.Point(762, 69);
            this.lbActived.Name = "lbActived";
            this.lbActived.Size = new System.Drawing.Size(92, 16);
            this.lbActived.TabIndex = 53;
            this.lbActived.Text = "Нет";
            // 
            // lbKoeff
            // 
            this.lbKoeff.AutoSize = true;
            this.lbKoeff.ForeColor = System.Drawing.Color.Cyan;
            this.lbKoeff.Location = new System.Drawing.Point(329, 149);
            this.lbKoeff.Name = "lbKoeff";
            this.lbKoeff.Size = new System.Drawing.Size(48, 16);
            this.lbKoeff.TabIndex = 56;
            this.lbKoeff.Text = "KOEFF";
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(691, 376);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 17;
            this.label6.Text = "Алгоблок";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(684, 327);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "Тип точки";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(676, 360);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(88, 16);
            this.label21.TabIndex = 9;
            this.label21.Text = "Контроллер";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(716, 344);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(48, 16);
            this.label20.TabIndex = 8;
            this.label20.Text = "Канал";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(251, 165);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(72, 16);
            this.label19.TabIndex = 7;
            this.label19.Text = "Смещение";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(195, 196);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(128, 16);
            this.label41.TabIndex = 12;
            this.label41.Text = "Исходные данные";
            this.label41.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(205, 395);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(120, 16);
            this.label59.TabIndex = 30;
            this.label59.Text = "Нижняя граница";
            this.label59.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(197, 379);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(128, 16);
            this.label58.TabIndex = 31;
            this.label58.Text = "Верхняя граница";
            this.label58.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(684, 214);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(80, 16);
            this.label44.TabIndex = 23;
            this.label44.Text = "Формат PV";
            this.label44.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(668, 69);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(96, 16);
            this.label31.TabIndex = 22;
            this.label31.Text = "Опрос точки";
            this.label31.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(155, 269);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(64, 16);
            this.label22.TabIndex = 21;
            this.label22.Text = "Статус:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(243, 149);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(72, 16);
            this.label18.TabIndex = 26;
            this.label18.Text = "Делитель";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label57.ForeColor = System.Drawing.Color.Lime;
            this.label57.Location = new System.Drawing.Point(326, 351);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(112, 16);
            this.label57.TabIndex = 4;
            this.label57.Text = "Шкала прибора";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label43.ForeColor = System.Drawing.Color.Lime;
            this.label43.Location = new System.Drawing.Point(647, 185);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(160, 16);
            this.label43.TabIndex = 2;
            this.label43.Text = "Данные конфигурации";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.ForeColor = System.Drawing.Color.Lime;
            this.label15.Location = new System.Drawing.Point(675, 41);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(104, 16);
            this.label15.TabIndex = 6;
            this.label15.Text = "Данные точки";
            // 
            // timerFetch
            // 
            this.timerFetch.Interval = 500;
            this.timerFetch.Tick += new System.EventHandler(this.timerFetch_Tick);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(187, 228);
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
            this.lbPVCalc.Location = new System.Drawing.Point(329, 228);
            this.lbPVCalc.Name = "lbPVCalc";
            this.lbPVCalc.Size = new System.Drawing.Size(16, 16);
            this.lbPVCalc.TabIndex = 61;
            this.lbPVCalc.Text = "0";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(155, 244);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(168, 16);
            this.label23.TabIndex = 12;
            this.label23.Text = "В процентах от шкалы";
            this.label23.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbPVPercent
            // 
            this.lbPVPercent.AutoSize = true;
            this.lbPVPercent.ForeColor = System.Drawing.Color.White;
            this.lbPVPercent.Location = new System.Drawing.Point(329, 244);
            this.lbPVPercent.Name = "lbPVPercent";
            this.lbPVPercent.Size = new System.Drawing.Size(16, 16);
            this.lbPVPercent.TabIndex = 61;
            this.lbPVPercent.Text = "0";
            // 
            // pbBar
            // 
            this.pbBar.BackColor = System.Drawing.Color.Black;
            this.pbBar.Location = new System.Drawing.Point(12, 41);
            this.pbBar.Name = "pbBar";
            this.pbBar.Size = new System.Drawing.Size(110, 244);
            this.pbBar.TabIndex = 65;
            this.pbBar.TabStop = false;
            this.pbBar.Paint += new System.Windows.Forms.PaintEventHandler(this.pbBar_Paint);
            // 
            // lbFetchGroup
            // 
            this.lbFetchGroup.AutoEllipsis = true;
            this.lbFetchGroup.ForeColor = System.Drawing.Color.Cyan;
            this.lbFetchGroup.Location = new System.Drawing.Point(762, 117);
            this.lbFetchGroup.Name = "lbFetchGroup";
            this.lbFetchGroup.Size = new System.Drawing.Size(88, 16);
            this.lbFetchGroup.TabIndex = 69;
            this.lbFetchGroup.Text = "------";
            this.lbFetchGroup.DoubleClick += new System.EventHandler(this.lbFetchGroup_DoubleClick);
            // 
            // lbGroupFetchTime
            // 
            this.lbGroupFetchTime.AutoEllipsis = true;
            this.lbGroupFetchTime.ForeColor = System.Drawing.Color.Cyan;
            this.lbGroupFetchTime.Location = new System.Drawing.Point(762, 133);
            this.lbGroupFetchTime.Name = "lbGroupFetchTime";
            this.lbGroupFetchTime.Size = new System.Drawing.Size(88, 16);
            this.lbGroupFetchTime.TabIndex = 70;
            this.lbGroupFetchTime.Text = "1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(652, 117);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 16);
            this.label9.TabIndex = 67;
            this.label9.Text = "Группа опроса";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(668, 133);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(96, 16);
            this.label37.TabIndex = 68;
            this.label37.Text = "Опрос (сек)";
            this.label37.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // backgroundFetch
            // 
            this.backgroundFetch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundFetch_DoWork);
            this.backgroundFetch.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundFetch_RunWorkerCompleted);
            // 
            // lbGroupFactTime
            // 
            this.lbGroupFactTime.AutoEllipsis = true;
            this.lbGroupFactTime.ForeColor = System.Drawing.Color.Cyan;
            this.lbGroupFactTime.Location = new System.Drawing.Point(762, 149);
            this.lbGroupFactTime.Name = "lbGroupFactTime";
            this.lbGroupFactTime.Size = new System.Drawing.Size(88, 16);
            this.lbGroupFactTime.TabIndex = 72;
            this.lbGroupFactTime.Text = "нет опроса";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(629, 149);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(136, 16);
            this.label10.TabIndex = 71;
            this.label10.Text = "Фактически (сек)";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbPlace
            // 
            this.lbPlace.AutoSize = true;
            this.lbPlace.ForeColor = System.Drawing.Color.Cyan;
            this.lbPlace.Location = new System.Drawing.Point(771, 395);
            this.lbPlace.Name = "lbPlace";
            this.lbPlace.Size = new System.Drawing.Size(48, 16);
            this.lbPlace.TabIndex = 74;
            this.lbPlace.Text = "PLACE";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(612, 395);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(152, 16);
            this.label11.TabIndex = 73;
            this.label11.Text = "Параметр алгоблока";
            // 
            // lbRawDataType
            // 
            this.lbRawDataType.AutoSize = true;
            this.lbRawDataType.ForeColor = System.Drawing.Color.White;
            this.lbRawDataType.Location = new System.Drawing.Point(329, 212);
            this.lbRawDataType.Name = "lbRawDataType";
            this.lbRawDataType.Size = new System.Drawing.Size(56, 16);
            this.lbRawDataType.TabIndex = 84;
            this.lbRawDataType.Text = "------";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 16);
            this.label2.TabIndex = 83;
            this.label2.Text = "Тип исходных данных";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbFetchStatus
            // 
            this.lbFetchStatus.ForeColor = System.Drawing.Color.Yellow;
            this.lbFetchStatus.Location = new System.Drawing.Point(225, 269);
            this.lbFetchStatus.Name = "lbFetchStatus";
            this.lbFetchStatus.Size = new System.Drawing.Size(311, 49);
            this.lbFetchStatus.TabIndex = 85;
            // 
            // btnSize
            // 
            this.btnSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSize.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSize.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnSize.Location = new System.Drawing.Point(830, 6);
            this.btnSize.Name = "btnSize";
            this.btnSize.Size = new System.Drawing.Size(22, 23);
            this.btnSize.TabIndex = 86;
            this.btnSize.Text = ">";
            this.btnSize.UseVisualStyleBackColor = false;
            this.btnSize.Click += new System.EventHandler(this.btnSize_Click);
            // 
            // btnChangeValue
            // 
            this.btnChangeValue.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnChangeValue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangeValue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnChangeValue.ForeColor = System.Drawing.Color.Black;
            this.btnChangeValue.Location = new System.Drawing.Point(26, 388);
            this.btnChangeValue.Name = "btnChangeValue";
            this.btnChangeValue.Size = new System.Drawing.Size(97, 23);
            this.btnChangeValue.TabIndex = 87;
            this.btnChangeValue.Text = "Изменить...";
            this.btnChangeValue.UseVisualStyleBackColor = false;
            this.btnChangeValue.Click += new System.EventHandler(this.btnChangeValue_Click);
            // 
            // frmAP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(857, 425);
            this.Controls.Add(this.btnChangeValue);
            this.Controls.Add(this.btnSize);
            this.Controls.Add(this.lbFetchStatus);
            this.Controls.Add(this.lbRawDataType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbPlace);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lbGroupFactTime);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lbFetchGroup);
            this.Controls.Add(this.lbGroupFetchTime);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbPtDesc);
            this.Controls.Add(this.lbEUDesc);
            this.Controls.Add(this.lbPVEULO);
            this.Controls.Add(this.lbPVEUHI);
            this.Controls.Add(this.lbPV);
            this.Controls.Add(this.lbPtName1);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.lbAlgoblock);
            this.Controls.Add(this.lbNode);
            this.Controls.Add(this.lbPtType);
            this.Controls.Add(this.lbChannel);
            this.Controls.Add(this.lbOffset);
            this.Controls.Add(this.lbPVPercent);
            this.Controls.Add(this.lbPVCalc);
            this.Controls.Add(this.lbRaw);
            this.Controls.Add(this.lbPVFormat);
            this.Controls.Add(this.lbActived);
            this.Controls.Add(this.lbKoeff);
            this.Controls.Add(this.lbPtName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.label59);
            this.Controls.Add(this.label58);
            this.Controls.Add(this.label44);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label57);
            this.Controls.Add(this.label43);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.pbBar);
            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Silver;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAP";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Паспорт значения";
            this.Load += new System.EventHandler(this.frmAI_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAI_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbPtDesc;
        private System.Windows.Forms.Label lbEUDesc;
        private System.Windows.Forms.Label lbPVEULO;
        private System.Windows.Forms.Label lbPVEUHI;
        private System.Windows.Forms.Label lbPV;
        private System.Windows.Forms.Label lbPtName1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lbAlgoblock;
        private System.Windows.Forms.Label lbNode;
        private System.Windows.Forms.Label lbPtType;
        private System.Windows.Forms.Label lbChannel;
        private System.Windows.Forms.Label lbOffset;
        private System.Windows.Forms.Label lbRaw;
        private System.Windows.Forms.Label lbPVFormat;
        private System.Windows.Forms.Label lbActived;
        private System.Windows.Forms.Label lbKoeff;
        private System.Windows.Forms.Label lbPtName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Timer timerFetch;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbPVCalc;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lbPVPercent;
        private System.Windows.Forms.PictureBox pbBar;
        private System.Windows.Forms.Label lbFetchGroup;
        private System.Windows.Forms.Label lbGroupFetchTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label37;
        private System.ComponentModel.BackgroundWorker backgroundFetch;
        private System.Windows.Forms.Label lbGroupFactTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbPlace;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbRawDataType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbFetchStatus;
        private System.Windows.Forms.Button btnSize;
        private System.Windows.Forms.Button btnChangeValue;

    }
}