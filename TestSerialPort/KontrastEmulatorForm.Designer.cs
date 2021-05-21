namespace TestSerialPort
{
    partial class KontrastEmulatorForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundFetch = new System.ComponentModel.BackgroundWorker();
            this.tv = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.tc = new System.Windows.Forms.TabControl();
            this.tpParOut = new System.Windows.Forms.TabPage();
            this.nudValue = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tpKontur = new System.Windows.Forms.TabPage();
            this.nudOP = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.nudDV = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.nudPV = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.nudSP = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.nudHV = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPortName = new System.Windows.Forms.ComboBox();
            this.cbDataBits = new System.Windows.Forms.ComboBox();
            this.cbStopBits = new System.Windows.Forms.ComboBox();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.cbKonturError = new System.Windows.Forms.CheckBox();
            this.cbKonturMode = new System.Windows.Forms.ComboBox();
            this.cbEngUnits = new System.Windows.Forms.CheckBox();
            this.tc.SuspendLayout();
            this.tpParOut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudValue)).BeginInit();
            this.tpKontur.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHV)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundFetch
            // 
            this.backgroundFetch.WorkerReportsProgress = true;
            this.backgroundFetch.WorkerSupportsCancellation = true;
            this.backgroundFetch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundFetch_DoWork);
            this.backgroundFetch.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundFetch_ProgressChanged);
            this.backgroundFetch.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundFetch_RunWorkerCompleted);
            // 
            // tv
            // 
            this.tv.HideSelection = false;
            this.tv.Location = new System.Drawing.Point(12, 206);
            this.tv.Name = "tv";
            this.tv.Size = new System.Drawing.Size(231, 134);
            this.tv.TabIndex = 1;
            this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 352);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Необходимо настроить и открыть порт...";
            // 
            // tc
            // 
            this.tc.Controls.Add(this.tpParOut);
            this.tc.Controls.Add(this.tpKontur);
            this.tc.Location = new System.Drawing.Point(250, 13);
            this.tc.Name = "tc";
            this.tc.SelectedIndex = 0;
            this.tc.Size = new System.Drawing.Size(298, 327);
            this.tc.TabIndex = 3;
            this.tc.Visible = false;
            // 
            // tpParOut
            // 
            this.tpParOut.Controls.Add(this.nudValue);
            this.tpParOut.Controls.Add(this.label3);
            this.tpParOut.Controls.Add(this.label2);
            this.tpParOut.Controls.Add(this.comboBox1);
            this.tpParOut.Location = new System.Drawing.Point(4, 22);
            this.tpParOut.Name = "tpParOut";
            this.tpParOut.Padding = new System.Windows.Forms.Padding(3);
            this.tpParOut.Size = new System.Drawing.Size(290, 301);
            this.tpParOut.TabIndex = 0;
            this.tpParOut.Text = "Параметр или выход";
            this.tpParOut.UseVisualStyleBackColor = true;
            // 
            // nudValue
            // 
            this.nudValue.DecimalPlaces = 3;
            this.nudValue.Location = new System.Drawing.Point(81, 40);
            this.nudValue.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudValue.Minimum = new decimal(new int[] {
            65536,
            0,
            0,
            -2147483648});
            this.nudValue.Name = "nudValue";
            this.nudValue.Size = new System.Drawing.Size(121, 20);
            this.nudValue.TabIndex = 2;
            this.nudValue.ValueChanged += new System.EventHandler(this.nudValue_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Значение:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Тип данных:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(81, 10);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // tpKontur
            // 
            this.tpKontur.Controls.Add(this.cbKonturMode);
            this.tpKontur.Controls.Add(this.cbEngUnits);
            this.tpKontur.Controls.Add(this.cbKonturError);
            this.tpKontur.Controls.Add(this.nudOP);
            this.tpKontur.Controls.Add(this.label13);
            this.tpKontur.Controls.Add(this.nudDV);
            this.tpKontur.Controls.Add(this.label12);
            this.tpKontur.Controls.Add(this.nudPV);
            this.tpKontur.Controls.Add(this.label11);
            this.tpKontur.Controls.Add(this.nudSP);
            this.tpKontur.Controls.Add(this.label10);
            this.tpKontur.Controls.Add(this.nudHV);
            this.tpKontur.Controls.Add(this.label9);
            this.tpKontur.Location = new System.Drawing.Point(4, 22);
            this.tpKontur.Name = "tpKontur";
            this.tpKontur.Padding = new System.Windows.Forms.Padding(3);
            this.tpKontur.Size = new System.Drawing.Size(290, 301);
            this.tpKontur.TabIndex = 1;
            this.tpKontur.Text = "Контур";
            this.tpKontur.UseVisualStyleBackColor = true;
            // 
            // nudOP
            // 
            this.nudOP.DecimalPlaces = 3;
            this.nudOP.Location = new System.Drawing.Point(75, 115);
            this.nudOP.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudOP.Minimum = new decimal(new int[] {
            65536,
            0,
            0,
            -2147483648});
            this.nudOP.Name = "nudOP";
            this.nudOP.Size = new System.Drawing.Size(121, 20);
            this.nudOP.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(44, 117);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(25, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "OP:";
            // 
            // nudDV
            // 
            this.nudDV.DecimalPlaces = 3;
            this.nudDV.Location = new System.Drawing.Point(75, 89);
            this.nudDV.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudDV.Minimum = new decimal(new int[] {
            65536,
            0,
            0,
            -2147483648});
            this.nudDV.Name = "nudDV";
            this.nudDV.Size = new System.Drawing.Size(121, 20);
            this.nudDV.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(44, 91);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "DV:";
            // 
            // nudPV
            // 
            this.nudPV.DecimalPlaces = 3;
            this.nudPV.Location = new System.Drawing.Point(75, 63);
            this.nudPV.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudPV.Minimum = new decimal(new int[] {
            65536,
            0,
            0,
            -2147483648});
            this.nudPV.Name = "nudPV";
            this.nudPV.Size = new System.Drawing.Size(121, 20);
            this.nudPV.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(44, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "PV:";
            // 
            // nudSP
            // 
            this.nudSP.DecimalPlaces = 3;
            this.nudSP.Location = new System.Drawing.Point(75, 37);
            this.nudSP.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudSP.Minimum = new decimal(new int[] {
            65536,
            0,
            0,
            -2147483648});
            this.nudSP.Name = "nudSP";
            this.nudSP.Size = new System.Drawing.Size(121, 20);
            this.nudSP.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(44, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "SP:";
            // 
            // nudHV
            // 
            this.nudHV.DecimalPlaces = 3;
            this.nudHV.Location = new System.Drawing.Point(75, 11);
            this.nudHV.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudHV.Minimum = new decimal(new int[] {
            65536,
            0,
            0,
            -2147483648});
            this.nudHV.Name = "nudHV";
            this.nudHV.Size = new System.Drawing.Size(121, 20);
            this.nudHV.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(44, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "HV:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbPortName);
            this.groupBox1.Controls.Add(this.cbDataBits);
            this.groupBox1.Controls.Add(this.cbStopBits);
            this.groupBox1.Controls.Add(this.cbParity);
            this.groupBox1.Controls.Add(this.cbBaudRate);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 187);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройка последовательного порта";
            // 
            // btnClose
            // 
            this.btnClose.Enabled = false;
            this.btnClose.Location = new System.Drawing.Point(124, 154);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(43, 154);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Открыть";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.EnabledChanged += new System.EventHandler(this.btnOpen_EnabledChanged);
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Битов данных";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Стоповых битов";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Паритет";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Скорость, бод";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Имя порта";
            // 
            // cbPortName
            // 
            this.cbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPortName.FormattingEnabled = true;
            this.cbPortName.Location = new System.Drawing.Point(124, 19);
            this.cbPortName.Name = "cbPortName";
            this.cbPortName.Size = new System.Drawing.Size(101, 21);
            this.cbPortName.TabIndex = 0;
            this.cbPortName.DropDown += new System.EventHandler(this.cbPortName_DropDown);
            this.cbPortName.SelectionChangeCommitted += new System.EventHandler(this.cbPortName_SelectionChangeCommitted);
            // 
            // cbDataBits
            // 
            this.cbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataBits.FormattingEnabled = true;
            this.cbDataBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cbDataBits.Location = new System.Drawing.Point(124, 100);
            this.cbDataBits.Name = "cbDataBits";
            this.cbDataBits.Size = new System.Drawing.Size(101, 21);
            this.cbDataBits.TabIndex = 1;
            this.cbDataBits.SelectionChangeCommitted += new System.EventHandler(this.cbDataBits_SelectionChangeCommitted);
            // 
            // cbStopBits
            // 
            this.cbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStopBits.FormattingEnabled = true;
            this.cbStopBits.Location = new System.Drawing.Point(124, 127);
            this.cbStopBits.Name = "cbStopBits";
            this.cbStopBits.Size = new System.Drawing.Size(101, 21);
            this.cbStopBits.TabIndex = 1;
            this.cbStopBits.SelectionChangeCommitted += new System.EventHandler(this.cbStopBits_SelectionChangeCommitted);
            // 
            // cbParity
            // 
            this.cbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Location = new System.Drawing.Point(124, 73);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(101, 21);
            this.cbParity.TabIndex = 1;
            this.cbParity.SelectionChangeCommitted += new System.EventHandler(this.cbParity_SelectionChangeCommitted);
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Items.AddRange(new object[] {
            "110",
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "56000",
            "57600",
            "115200"});
            this.cbBaudRate.Location = new System.Drawing.Point(124, 46);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(101, 21);
            this.cbBaudRate.TabIndex = 1;
            this.cbBaudRate.SelectionChangeCommitted += new System.EventHandler(this.cbBaudRate_SelectionChangeCommitted);
            // 
            // cbKonturError
            // 
            this.cbKonturError.AutoSize = true;
            this.cbKonturError.Location = new System.Drawing.Point(47, 147);
            this.cbKonturError.Name = "cbKonturError";
            this.cbKonturError.Size = new System.Drawing.Size(154, 17);
            this.cbKonturError.TabIndex = 5;
            this.cbKonturError.Text = "Признак ошибки контура";
            this.cbKonturError.UseVisualStyleBackColor = true;
            this.cbKonturError.Click += new System.EventHandler(this.cbKonturError_Click);
            // 
            // cbKonturMode
            // 
            this.cbKonturMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKonturMode.FormattingEnabled = true;
            this.cbKonturMode.Items.AddRange(new object[] {
            "РУЧ",
            "АВТ",
            "КАС"});
            this.cbKonturMode.Location = new System.Drawing.Point(47, 170);
            this.cbKonturMode.Name = "cbKonturMode";
            this.cbKonturMode.Size = new System.Drawing.Size(149, 21);
            this.cbKonturMode.TabIndex = 6;
            this.cbKonturMode.SelectionChangeCommitted += new System.EventHandler(this.cbKonturMode_SelectionChangeCommitted);
            // 
            // cbEngUnits
            // 
            this.cbEngUnits.AutoSize = true;
            this.cbEngUnits.Location = new System.Drawing.Point(47, 198);
            this.cbEngUnits.Name = "cbEngUnits";
            this.cbEngUnits.Size = new System.Drawing.Size(138, 17);
            this.cbEngUnits.TabIndex = 5;
            this.cbEngUnits.Text = "Технические единицы";
            this.cbEngUnits.UseVisualStyleBackColor = true;
            this.cbEngUnits.Click += new System.EventHandler(this.cbEngUnits_Click);
            // 
            // KontrastEmulatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 386);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tv);
            this.Name = "KontrastEmulatorForm";
            this.Text = "Эмулятор обмена по последовательной связи";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tc.ResumeLayout(false);
            this.tpParOut.ResumeLayout(false);
            this.tpParOut.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudValue)).EndInit();
            this.tpKontur.ResumeLayout(false);
            this.tpKontur.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHV)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundFetch;
        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tc;
        private System.Windows.Forms.TabPage tpParOut;
        private System.Windows.Forms.TabPage tpKontur;
        private System.Windows.Forms.NumericUpDown nudValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbPortName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbParity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbDataBits;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbStopBits;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.NumericUpDown nudOP;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown nudDV;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nudPV;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudSP;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudHV;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbKonturMode;
        private System.Windows.Forms.CheckBox cbEngUnits;
        private System.Windows.Forms.CheckBox cbKonturError;
    }
}

