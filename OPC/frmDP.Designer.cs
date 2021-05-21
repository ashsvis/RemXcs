namespace Points.OPC
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
            this.lbParameter = new System.Windows.Forms.Label();
            this.lbGroup = new System.Windows.Forms.Label();
            this.lbPtType = new System.Windows.Forms.Label();
            this.lbServer = new System.Windows.Forms.Label();
            this.lbRaw = new System.Windows.Forms.Label();
            this.lbFetchTime = new System.Windows.Forms.Label();
            this.lbAsked = new System.Windows.Forms.Label();
            this.lbLogged = new System.Windows.Forms.Label();
            this.lbInvert = new System.Windows.Forms.Label();
            this.lbActived = new System.Windows.Forms.Label();
            this.lbPtName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbCDT = new System.Windows.Forms.Label();
            this.timerFetch = new System.Windows.Forms.Timer(this.components);
            this.label14 = new System.Windows.Forms.Label();
            this.lbPVCalc = new System.Windows.Forms.Label();
            this.btnQuit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbAlarmUp = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbAlarmDown = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbSwitchUp = new System.Windows.Forms.Label();
            this.lbSwitchDown = new System.Windows.Forms.Label();
            this.lbOnColor = new System.Windows.Forms.Label();
            this.lbOn = new System.Windows.Forms.Label();
            this.lbOff = new System.Windows.Forms.Label();
            this.lbOffColor = new System.Windows.Forms.Label();
            this.backgroundFetch = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // lbPtDesc
            // 
            this.lbPtDesc.AutoSize = true;
            this.lbPtDesc.ForeColor = System.Drawing.Color.Cyan;
            this.lbPtDesc.Location = new System.Drawing.Point(133, 9);
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
            // lbParameter
            // 
            this.lbParameter.AutoSize = true;
            this.lbParameter.ForeColor = System.Drawing.Color.Cyan;
            this.lbParameter.Location = new System.Drawing.Point(329, 380);
            this.lbParameter.Name = "lbParameter";
            this.lbParameter.Size = new System.Drawing.Size(80, 16);
            this.lbParameter.TabIndex = 38;
            this.lbParameter.Text = "PARAMETER";
            // 
            // lbGroup
            // 
            this.lbGroup.AutoSize = true;
            this.lbGroup.ForeColor = System.Drawing.Color.Cyan;
            this.lbGroup.Location = new System.Drawing.Point(329, 364);
            this.lbGroup.Name = "lbGroup";
            this.lbGroup.Size = new System.Drawing.Size(48, 16);
            this.lbGroup.TabIndex = 49;
            this.lbGroup.Text = "GROUP";
            // 
            // lbPtType
            // 
            this.lbPtType.AutoSize = true;
            this.lbPtType.ForeColor = System.Drawing.Color.Cyan;
            this.lbPtType.Location = new System.Drawing.Point(329, 332);
            this.lbPtType.Name = "lbPtType";
            this.lbPtType.Size = new System.Drawing.Size(56, 16);
            this.lbPtType.TabIndex = 60;
            this.lbPtType.Text = "PTTYPE";
            // 
            // lbServer
            // 
            this.lbServer.AutoSize = true;
            this.lbServer.ForeColor = System.Drawing.Color.Cyan;
            this.lbServer.Location = new System.Drawing.Point(329, 348);
            this.lbServer.Name = "lbServer";
            this.lbServer.Size = new System.Drawing.Size(56, 16);
            this.lbServer.TabIndex = 59;
            this.lbServer.Text = "SERVER";
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
            this.label6.Location = new System.Drawing.Point(251, 380);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 17;
            this.label6.Text = "Параметр";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(243, 332);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "Тип точки";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(267, 364);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(56, 16);
            this.label21.TabIndex = 9;
            this.label21.Text = "Группа";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(267, 348);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(56, 16);
            this.label20.TabIndex = 8;
            this.label20.Text = "Сервер";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(267, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Аварийный журнал";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(187, 395);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(136, 16);
            this.label13.TabIndex = 17;
            this.label13.Text = "Канонический тип";
            // 
            // lbCDT
            // 
            this.lbCDT.AutoSize = true;
            this.lbCDT.ForeColor = System.Drawing.Color.Cyan;
            this.lbCDT.Location = new System.Drawing.Point(329, 395);
            this.lbCDT.Name = "lbCDT";
            this.lbCDT.Size = new System.Drawing.Size(32, 16);
            this.lbCDT.TabIndex = 38;
            this.lbCDT.Text = "CDT";
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
            // btnQuit
            // 
            this.btnQuit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnQuit.ForeColor = System.Drawing.Color.Black;
            this.btnQuit.Location = new System.Drawing.Point(26, 388);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(97, 23);
            this.btnQuit.TabIndex = 66;
            this.btnQuit.Text = "Квитировать";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(264, 16);
            this.label2.TabIndex = 23;
            this.label2.Text = "Сигнал при переходе из \"0\" в \"1\"";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbAlarmUp
            // 
            this.lbAlarmUp.AutoEllipsis = true;
            this.lbAlarmUp.ForeColor = System.Drawing.Color.White;
            this.lbAlarmUp.Location = new System.Drawing.Point(447, 69);
            this.lbAlarmUp.Name = "lbAlarmUp";
            this.lbAlarmUp.Size = new System.Drawing.Size(32, 16);
            this.lbAlarmUp.TabIndex = 50;
            this.lbAlarmUp.Text = "Нет";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(177, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(264, 16);
            this.label4.TabIndex = 23;
            this.label4.Text = "Сигнал при переходе из \"1\" в \"0\"";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbAlarmDown
            // 
            this.lbAlarmDown.AutoEllipsis = true;
            this.lbAlarmDown.ForeColor = System.Drawing.Color.White;
            this.lbAlarmDown.Location = new System.Drawing.Point(447, 85);
            this.lbAlarmDown.Name = "lbAlarmDown";
            this.lbAlarmDown.Size = new System.Drawing.Size(32, 16);
            this.lbAlarmDown.TabIndex = 50;
            this.lbAlarmDown.Text = "Нет";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.Color.Lime;
            this.label8.Location = new System.Drawing.Point(249, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(160, 16);
            this.label8.TabIndex = 5;
            this.label8.Text = "Журнал переключений";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(153, 145);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(288, 16);
            this.label9.TabIndex = 23;
            this.label9.Text = "Сообщение при переходе из \"0\" в \"1\"";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(153, 161);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(288, 16);
            this.label10.TabIndex = 23;
            this.label10.Text = "Сообщение при переходе из \"1\" в \"0\"";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbSwitchUp
            // 
            this.lbSwitchUp.AutoEllipsis = true;
            this.lbSwitchUp.ForeColor = System.Drawing.Color.White;
            this.lbSwitchUp.Location = new System.Drawing.Point(447, 145);
            this.lbSwitchUp.Name = "lbSwitchUp";
            this.lbSwitchUp.Size = new System.Drawing.Size(32, 16);
            this.lbSwitchUp.TabIndex = 50;
            this.lbSwitchUp.Text = "Нет";
            // 
            // lbSwitchDown
            // 
            this.lbSwitchDown.AutoEllipsis = true;
            this.lbSwitchDown.ForeColor = System.Drawing.Color.White;
            this.lbSwitchDown.Location = new System.Drawing.Point(447, 161);
            this.lbSwitchDown.Name = "lbSwitchDown";
            this.lbSwitchDown.Size = new System.Drawing.Size(32, 16);
            this.lbSwitchDown.TabIndex = 50;
            this.lbSwitchDown.Text = "Нет";
            // 
            // lbOnColor
            // 
            this.lbOnColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.lbOn.Text = "PVON";
            this.lbOn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbOff
            // 
            this.lbOff.ForeColor = System.Drawing.Color.Silver;
            this.lbOff.Location = new System.Drawing.Point(23, 255);
            this.lbOff.Name = "lbOff";
            this.lbOff.Size = new System.Drawing.Size(120, 18);
            this.lbOff.TabIndex = 40;
            this.lbOff.Text = "PVOFF";
            this.lbOff.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbOffColor
            // 
            this.lbOffColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            // frmDP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(730, 425);
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
            this.Controls.Add(this.lbCDT);
            this.Controls.Add(this.lbParameter);
            this.Controls.Add(this.lbGroup);
            this.Controls.Add(this.lbPtType);
            this.Controls.Add(this.lbServer);
            this.Controls.Add(this.lbPVCalc);
            this.Controls.Add(this.lbRaw);
            this.Controls.Add(this.lbFetchTime);
            this.Controls.Add(this.lbAsked);
            this.Controls.Add(this.lbLogged);
            this.Controls.Add(this.lbSwitchDown);
            this.Controls.Add(this.lbAlarmDown);
            this.Controls.Add(this.lbSwitchUp);
            this.Controls.Add(this.lbAlarmUp);
            this.Controls.Add(this.lbInvert);
            this.Controls.Add(this.lbActived);
            this.Controls.Add(this.lbPtName);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label44);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label43);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Silver;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDP";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Паспорт значения";
            this.Load += new System.EventHandler(this.frmDP_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDP_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbPtDesc;
        private System.Windows.Forms.Label lbPV;
        private System.Windows.Forms.Label lbPtName1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lbParameter;
        private System.Windows.Forms.Label lbGroup;
        private System.Windows.Forms.Label lbPtType;
        private System.Windows.Forms.Label lbServer;
        private System.Windows.Forms.Label lbRaw;
        private System.Windows.Forms.Label lbFetchTime;
        private System.Windows.Forms.Label lbAsked;
        private System.Windows.Forms.Label lbLogged;
        private System.Windows.Forms.Label lbInvert;
        private System.Windows.Forms.Label lbActived;
        private System.Windows.Forms.Label lbPtName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbCDT;
        private System.Windows.Forms.Timer timerFetch;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbPVCalc;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbAlarmUp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbAlarmDown;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbSwitchUp;
        private System.Windows.Forms.Label lbSwitchDown;
        private System.Windows.Forms.Label lbOnColor;
        private System.Windows.Forms.Label lbOn;
        private System.Windows.Forms.Label lbOff;
        private System.Windows.Forms.Label lbOffColor;
        private System.ComponentModel.BackgroundWorker backgroundFetch;

    }
}