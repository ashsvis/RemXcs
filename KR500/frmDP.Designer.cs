namespace Points.KR500
{
    partial class frmDP
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
            this.lbAlgoblock = new System.Windows.Forms.Label();
            this.lbNode = new System.Windows.Forms.Label();
            this.lbPtType = new System.Windows.Forms.Label();
            this.lbChannel = new System.Windows.Forms.Label();
            this.lbRaw = new System.Windows.Forms.Label();
            this.lbInvert = new System.Windows.Forms.Label();
            this.lbActived = new System.Windows.Forms.Label();
            this.lbPtName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.timerFetch = new System.Windows.Forms.Timer(this.components);
            this.label14 = new System.Windows.Forms.Label();
            this.lbPVCalc = new System.Windows.Forms.Label();
            this.lbOnColor = new System.Windows.Forms.Label();
            this.lbOn = new System.Windows.Forms.Label();
            this.lbOff = new System.Windows.Forms.Label();
            this.lbOffColor = new System.Windows.Forms.Label();
            this.lbFetchGroup = new System.Windows.Forms.Label();
            this.lbGroupFetchTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.backgroundFetch = new System.ComponentModel.BackgroundWorker();
            this.lbGroupFactTime = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbPlace = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbStrob = new System.Windows.Forms.Label();
            this.btnOff = new System.Windows.Forms.Button();
            this.btnOn = new System.Windows.Forms.Button();
            this.btnSize = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbRawDataType = new System.Windows.Forms.Label();
            this.lbFetchStatus = new System.Windows.Forms.Label();
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
            this.lbPV.Size = new System.Drawing.Size(103, 16);
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
            // lbAlgoblock
            // 
            this.lbAlgoblock.AutoSize = true;
            this.lbAlgoblock.ForeColor = System.Drawing.Color.Cyan;
            this.lbAlgoblock.Location = new System.Drawing.Point(329, 375);
            this.lbAlgoblock.Name = "lbAlgoblock";
            this.lbAlgoblock.Size = new System.Drawing.Size(48, 16);
            this.lbAlgoblock.TabIndex = 38;
            this.lbAlgoblock.Text = "BLOCK";
            // 
            // lbNode
            // 
            this.lbNode.AutoSize = true;
            this.lbNode.ForeColor = System.Drawing.Color.Cyan;
            this.lbNode.Location = new System.Drawing.Point(329, 359);
            this.lbNode.Name = "lbNode";
            this.lbNode.Size = new System.Drawing.Size(40, 16);
            this.lbNode.TabIndex = 49;
            this.lbNode.Text = "NODE";
            // 
            // lbPtType
            // 
            this.lbPtType.AutoSize = true;
            this.lbPtType.ForeColor = System.Drawing.Color.Cyan;
            this.lbPtType.Location = new System.Drawing.Point(329, 327);
            this.lbPtType.Name = "lbPtType";
            this.lbPtType.Size = new System.Drawing.Size(56, 16);
            this.lbPtType.TabIndex = 60;
            this.lbPtType.Text = "PTTYPE";
            // 
            // lbChannel
            // 
            this.lbChannel.AutoSize = true;
            this.lbChannel.ForeColor = System.Drawing.Color.Cyan;
            this.lbChannel.Location = new System.Drawing.Point(329, 343);
            this.lbChannel.Name = "lbChannel";
            this.lbChannel.Size = new System.Drawing.Size(64, 16);
            this.lbChannel.TabIndex = 59;
            this.lbChannel.Text = "CHANNEL";
            // 
            // lbRaw
            // 
            this.lbRaw.AutoSize = true;
            this.lbRaw.ForeColor = System.Drawing.Color.White;
            this.lbRaw.Location = new System.Drawing.Point(328, 182);
            this.lbRaw.Name = "lbRaw";
            this.lbRaw.Size = new System.Drawing.Size(56, 16);
            this.lbRaw.TabIndex = 61;
            this.lbRaw.Text = "------";
            // 
            // lbInvert
            // 
            this.lbInvert.AutoEllipsis = true;
            this.lbInvert.ForeColor = System.Drawing.Color.Cyan;
            this.lbInvert.Location = new System.Drawing.Point(639, 214);
            this.lbInvert.Name = "lbInvert";
            this.lbInvert.Size = new System.Drawing.Size(88, 16);
            this.lbInvert.TabIndex = 50;
            this.lbInvert.Text = "Нет";
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(250, 374);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 17;
            this.label6.Text = "Алгоблок";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(243, 327);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "Тип точки";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(235, 359);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(88, 16);
            this.label21.TabIndex = 9;
            this.label21.Text = "Контроллер";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(275, 343);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(48, 16);
            this.label20.TabIndex = 8;
            this.label20.Text = "Канал";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(194, 182);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(128, 16);
            this.label41.TabIndex = 12;
            this.label41.Text = "Исходные данные";
            this.label41.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(569, 214);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(72, 16);
            this.label44.TabIndex = 23;
            this.label44.Text = "Инверсия";
            this.label44.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label43.ForeColor = System.Drawing.Color.Lime;
            this.label43.Location = new System.Drawing.Point(524, 185);
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
            this.label15.Location = new System.Drawing.Point(552, 41);
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
            this.label14.Location = new System.Drawing.Point(187, 214);
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
            this.lbPVCalc.Location = new System.Drawing.Point(329, 214);
            this.lbPVCalc.Name = "lbPVCalc";
            this.lbPVCalc.Size = new System.Drawing.Size(72, 16);
            this.lbPVCalc.TabIndex = 61;
            this.lbPVCalc.Text = "Лог. \"0\"";
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
            this.lbOn.Text = "ВКЛЮЧИТЬ";
            this.lbOn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbOff
            // 
            this.lbOff.ForeColor = System.Drawing.Color.Silver;
            this.lbOff.Location = new System.Drawing.Point(23, 255);
            this.lbOff.Name = "lbOff";
            this.lbOff.Size = new System.Drawing.Size(120, 18);
            this.lbOff.TabIndex = 40;
            this.lbOff.Text = "ОТКЛЮЧИТЬ";
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
            // lbFetchGroup
            // 
            this.lbFetchGroup.AutoEllipsis = true;
            this.lbFetchGroup.ForeColor = System.Drawing.Color.Cyan;
            this.lbFetchGroup.Location = new System.Drawing.Point(639, 117);
            this.lbFetchGroup.Name = "lbFetchGroup";
            this.lbFetchGroup.Size = new System.Drawing.Size(88, 16);
            this.lbFetchGroup.TabIndex = 72;
            this.lbFetchGroup.Text = "------";
            this.lbFetchGroup.DoubleClick += new System.EventHandler(this.lbFetchGroup_DoubleClick);
            // 
            // lbGroupFetchTime
            // 
            this.lbGroupFetchTime.AutoEllipsis = true;
            this.lbGroupFetchTime.ForeColor = System.Drawing.Color.Cyan;
            this.lbGroupFetchTime.Location = new System.Drawing.Point(639, 133);
            this.lbGroupFetchTime.Name = "lbGroupFetchTime";
            this.lbGroupFetchTime.Size = new System.Drawing.Size(88, 16);
            this.lbGroupFetchTime.TabIndex = 73;
            this.lbGroupFetchTime.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(529, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 70;
            this.label3.Text = "Группа опроса";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(545, 133);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(96, 16);
            this.label37.TabIndex = 71;
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
            this.lbGroupFactTime.Location = new System.Drawing.Point(639, 149);
            this.lbGroupFactTime.Name = "lbGroupFactTime";
            this.lbGroupFactTime.Size = new System.Drawing.Size(88, 16);
            this.lbGroupFactTime.TabIndex = 75;
            this.lbGroupFactTime.Text = "нет опроса";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(506, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 16);
            this.label5.TabIndex = 74;
            this.label5.Text = "Фактически (сек)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbPlace
            // 
            this.lbPlace.AutoSize = true;
            this.lbPlace.ForeColor = System.Drawing.Color.Cyan;
            this.lbPlace.Location = new System.Drawing.Point(330, 390);
            this.lbPlace.Name = "lbPlace";
            this.lbPlace.Size = new System.Drawing.Size(48, 16);
            this.lbPlace.TabIndex = 77;
            this.lbPlace.Text = "PLACE";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(171, 389);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(152, 16);
            this.label12.TabIndex = 76;
            this.label12.Text = "Параметр алгоблока";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(481, 230);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 16);
            this.label1.TabIndex = 23;
            this.label1.Text = "Время строба (мсек)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbStrob
            // 
            this.lbStrob.AutoEllipsis = true;
            this.lbStrob.ForeColor = System.Drawing.Color.Cyan;
            this.lbStrob.Location = new System.Drawing.Point(639, 230);
            this.lbStrob.Name = "lbStrob";
            this.lbStrob.Size = new System.Drawing.Size(88, 16);
            this.lbStrob.TabIndex = 50;
            this.lbStrob.Text = "------";
            // 
            // btnOff
            // 
            this.btnOff.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnOff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOff.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOff.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnOff.Location = new System.Drawing.Point(79, 385);
            this.btnOff.Name = "btnOff";
            this.btnOff.Size = new System.Drawing.Size(60, 23);
            this.btnOff.TabIndex = 79;
            this.btnOff.Tag = "OFF";
            this.btnOff.Text = "Откл";
            this.btnOff.UseVisualStyleBackColor = false;
            this.btnOff.Click += new System.EventHandler(this.btnOn_Click);
            // 
            // btnOn
            // 
            this.btnOn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnOn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOn.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnOn.Location = new System.Drawing.Point(13, 385);
            this.btnOn.Name = "btnOn";
            this.btnOn.Size = new System.Drawing.Size(60, 23);
            this.btnOn.TabIndex = 78;
            this.btnOn.Tag = "ON";
            this.btnOn.Text = "Вкл";
            this.btnOn.UseVisualStyleBackColor = false;
            this.btnOn.Click += new System.EventHandler(this.btnOn_Click);
            // 
            // btnSize
            // 
            this.btnSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSize.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSize.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnSize.Location = new System.Drawing.Point(702, 6);
            this.btnSize.Name = "btnSize";
            this.btnSize.Size = new System.Drawing.Size(22, 23);
            this.btnSize.TabIndex = 80;
            this.btnSize.Text = ">";
            this.btnSize.UseVisualStyleBackColor = false;
            this.btnSize.Click += new System.EventHandler(this.btnSize_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Тип исходных данных";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbRawDataType
            // 
            this.lbRawDataType.AutoSize = true;
            this.lbRawDataType.ForeColor = System.Drawing.Color.White;
            this.lbRawDataType.Location = new System.Drawing.Point(329, 198);
            this.lbRawDataType.Name = "lbRawDataType";
            this.lbRawDataType.Size = new System.Drawing.Size(56, 16);
            this.lbRawDataType.TabIndex = 61;
            this.lbRawDataType.Text = "------";
            // 
            // lbFetchStatus
            // 
            this.lbFetchStatus.ForeColor = System.Drawing.Color.Yellow;
            this.lbFetchStatus.Location = new System.Drawing.Point(163, 274);
            this.lbFetchStatus.Name = "lbFetchStatus";
            this.lbFetchStatus.Size = new System.Drawing.Size(311, 49);
            this.lbFetchStatus.TabIndex = 81;
            // 
            // frmDP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(730, 425);
            this.Controls.Add(this.lbFetchStatus);
            this.Controls.Add(this.btnSize);
            this.Controls.Add(this.btnOff);
            this.Controls.Add(this.btnOn);
            this.Controls.Add(this.lbPlace);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lbGroupFactTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbFetchGroup);
            this.Controls.Add(this.lbGroupFetchTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.lbOffColor);
            this.Controls.Add(this.lbOnColor);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbPtDesc);
            this.Controls.Add(this.lbOff);
            this.Controls.Add(this.lbPV);
            this.Controls.Add(this.lbOn);
            this.Controls.Add(this.lbPtName1);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.lbAlgoblock);
            this.Controls.Add(this.lbNode);
            this.Controls.Add(this.lbPtType);
            this.Controls.Add(this.lbChannel);
            this.Controls.Add(this.lbPVCalc);
            this.Controls.Add(this.lbRawDataType);
            this.Controls.Add(this.lbRaw);
            this.Controls.Add(this.lbStrob);
            this.Controls.Add(this.lbInvert);
            this.Controls.Add(this.lbActived);
            this.Controls.Add(this.lbPtName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label44);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label43);
            this.Controls.Add(this.label15);
            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Silver;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDP";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Паспорт значения";
            this.Load += new System.EventHandler(this.frmDI_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDI_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbPtDesc;
        private System.Windows.Forms.Label lbPV;
        private System.Windows.Forms.Label lbPtName1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lbAlgoblock;
        private System.Windows.Forms.Label lbNode;
        private System.Windows.Forms.Label lbPtType;
        private System.Windows.Forms.Label lbChannel;
        private System.Windows.Forms.Label lbRaw;
        private System.Windows.Forms.Label lbInvert;
        private System.Windows.Forms.Label lbActived;
        private System.Windows.Forms.Label lbPtName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Timer timerFetch;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbPVCalc;
        private System.Windows.Forms.Label lbOnColor;
        private System.Windows.Forms.Label lbOn;
        private System.Windows.Forms.Label lbOff;
        private System.Windows.Forms.Label lbOffColor;
        private System.Windows.Forms.Label lbFetchGroup;
        private System.Windows.Forms.Label lbGroupFetchTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label37;
        private System.ComponentModel.BackgroundWorker backgroundFetch;
        private System.Windows.Forms.Label lbGroupFactTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbPlace;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbStrob;
        private System.Windows.Forms.Button btnOff;
        private System.Windows.Forms.Button btnOn;
        private System.Windows.Forms.Button btnSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbRawDataType;
        private System.Windows.Forms.Label lbFetchStatus;

    }
}