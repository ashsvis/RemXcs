namespace FetchKR500
{
    partial class frmTuningKR500
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCopyOwner = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCopyKey = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbCopyCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbStation = new System.Windows.Forms.ComboBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbChan4 = new System.Windows.Forms.ComboBox();
            this.cbChan3 = new System.Windows.Forms.ComboBox();
            this.cbChan2 = new System.Windows.Forms.ComboBox();
            this.cbChan1 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.cbItem4 = new System.Windows.Forms.CheckBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.cbItem3 = new System.Windows.Forms.CheckBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.cbItem2 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cbItem1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTimeOut = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(201, 342);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(117, 342);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ввод";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 14);
            this.label1.TabIndex = 20;
            this.label1.Text = "Владелец копии:";
            // 
            // tbCopyOwner
            // 
            this.tbCopyOwner.Location = new System.Drawing.Point(125, 21);
            this.tbCopyOwner.Name = "tbCopyOwner";
            this.tbCopyOwner.Size = new System.Drawing.Size(114, 22);
            this.tbCopyOwner.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 14);
            this.label2.TabIndex = 20;
            this.label2.Text = "Ключ копии:";
            // 
            // tbCopyKey
            // 
            this.tbCopyKey.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbCopyKey.Location = new System.Drawing.Point(125, 77);
            this.tbCopyKey.Name = "tbCopyKey";
            this.tbCopyKey.Size = new System.Drawing.Size(114, 22);
            this.tbCopyKey.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbCopyOwner);
            this.groupBox2.Controls.Add(this.tbCopyCode);
            this.groupBox2.Controls.Add(this.tbCopyKey);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(18, 225);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(264, 111);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Авторизация сервера опроса";
            // 
            // tbCopyCode
            // 
            this.tbCopyCode.Location = new System.Drawing.Point(125, 49);
            this.tbCopyCode.Name = "tbCopyCode";
            this.tbCopyCode.ReadOnly = true;
            this.tbCopyCode.Size = new System.Drawing.Size(114, 22);
            this.tbCopyCode.TabIndex = 1;
            this.tbCopyCode.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 14);
            this.label5.TabIndex = 20;
            this.label5.Text = "Код авторизации:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 14);
            this.label6.TabIndex = 5;
            this.label6.Text = "Номер станции";
            // 
            // cbStation
            // 
            this.cbStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStation.FormattingEnabled = true;
            this.cbStation.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cbStation.Location = new System.Drawing.Point(112, 10);
            this.cbStation.Name = "cbStation";
            this.cbStation.Size = new System.Drawing.Size(62, 22);
            this.cbStation.TabIndex = 0;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbChan4);
            this.groupBox1.Controls.Add(this.cbChan3);
            this.groupBox1.Controls.Add(this.cbChan2);
            this.groupBox1.Controls.Add(this.cbChan1);
            this.groupBox1.Controls.Add(this.comboBox4);
            this.groupBox1.Controls.Add(this.cbItem4);
            this.groupBox1.Controls.Add(this.comboBox3);
            this.groupBox1.Controls.Add(this.cbItem3);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.cbItem2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.cbItem1);
            this.groupBox1.Location = new System.Drawing.Point(12, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 136);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройка каналов связи";
            // 
            // cbChan4
            // 
            this.cbChan4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChan4.Enabled = false;
            this.cbChan4.FormattingEnabled = true;
            this.cbChan4.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cbChan4.Location = new System.Drawing.Point(33, 101);
            this.cbChan4.Name = "cbChan4";
            this.cbChan4.Size = new System.Drawing.Size(44, 22);
            this.cbChan4.TabIndex = 10;
            this.cbChan4.Tag = "4";
            // 
            // cbChan3
            // 
            this.cbChan3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChan3.Enabled = false;
            this.cbChan3.FormattingEnabled = true;
            this.cbChan3.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cbChan3.Location = new System.Drawing.Point(33, 73);
            this.cbChan3.Name = "cbChan3";
            this.cbChan3.Size = new System.Drawing.Size(44, 22);
            this.cbChan3.TabIndex = 7;
            this.cbChan3.Tag = "3";
            // 
            // cbChan2
            // 
            this.cbChan2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChan2.Enabled = false;
            this.cbChan2.FormattingEnabled = true;
            this.cbChan2.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cbChan2.Location = new System.Drawing.Point(33, 47);
            this.cbChan2.Name = "cbChan2";
            this.cbChan2.Size = new System.Drawing.Size(44, 22);
            this.cbChan2.TabIndex = 4;
            this.cbChan2.Tag = "2";
            // 
            // cbChan1
            // 
            this.cbChan1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChan1.Enabled = false;
            this.cbChan1.FormattingEnabled = true;
            this.cbChan1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cbChan1.Location = new System.Drawing.Point(33, 19);
            this.cbChan1.Name = "cbChan1";
            this.cbChan1.Size = new System.Drawing.Size(44, 22);
            this.cbChan1.TabIndex = 2;
            this.cbChan1.Tag = "1";
            // 
            // comboBox4
            // 
            this.comboBox4.Enabled = false;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "COM1:9600,N,8,1",
            "TCP://127.0.0.1:1000"});
            this.comboBox4.Location = new System.Drawing.Point(83, 101);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(156, 22);
            this.comboBox4.TabIndex = 11;
            this.comboBox4.Tag = "4";
            // 
            // cbItem4
            // 
            this.cbItem4.AutoSize = true;
            this.cbItem4.Location = new System.Drawing.Point(12, 105);
            this.cbItem4.Name = "cbItem4";
            this.cbItem4.Size = new System.Drawing.Size(15, 14);
            this.cbItem4.TabIndex = 9;
            this.cbItem4.Tag = "4";
            this.cbItem4.UseVisualStyleBackColor = true;
            this.cbItem4.Click += new System.EventHandler(this.cbItem1_Click);
            // 
            // comboBox3
            // 
            this.comboBox3.Enabled = false;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "COM1:9600,N,8,1",
            "TCP://127.0.0.1:1000"});
            this.comboBox3.Location = new System.Drawing.Point(83, 73);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(156, 22);
            this.comboBox3.TabIndex = 8;
            this.comboBox3.Tag = "3";
            // 
            // cbItem3
            // 
            this.cbItem3.AutoSize = true;
            this.cbItem3.Location = new System.Drawing.Point(12, 77);
            this.cbItem3.Name = "cbItem3";
            this.cbItem3.Size = new System.Drawing.Size(15, 14);
            this.cbItem3.TabIndex = 6;
            this.cbItem3.Tag = "3";
            this.cbItem3.UseVisualStyleBackColor = true;
            this.cbItem3.Click += new System.EventHandler(this.cbItem1_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.Enabled = false;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "COM1:9600,N,8,1",
            "TCP://127.0.0.1:1000"});
            this.comboBox2.Location = new System.Drawing.Point(83, 47);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(156, 22);
            this.comboBox2.TabIndex = 5;
            this.comboBox2.Tag = "2";
            // 
            // cbItem2
            // 
            this.cbItem2.AutoSize = true;
            this.cbItem2.Location = new System.Drawing.Point(12, 51);
            this.cbItem2.Name = "cbItem2";
            this.cbItem2.Size = new System.Drawing.Size(15, 14);
            this.cbItem2.TabIndex = 3;
            this.cbItem2.Tag = "2";
            this.cbItem2.UseVisualStyleBackColor = true;
            this.cbItem2.Click += new System.EventHandler(this.cbItem1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "COM1:9600,N,8,1",
            "TCP://127.0.0.1:1000"});
            this.comboBox1.Location = new System.Drawing.Point(83, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(156, 22);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.Tag = "2";
            // 
            // cbItem1
            // 
            this.cbItem1.AutoSize = true;
            this.cbItem1.Location = new System.Drawing.Point(12, 23);
            this.cbItem1.Name = "cbItem1";
            this.cbItem1.Size = new System.Drawing.Size(15, 14);
            this.cbItem1.TabIndex = 0;
            this.cbItem1.Tag = "1";
            this.cbItem1.UseVisualStyleBackColor = true;
            this.cbItem1.Click += new System.EventHandler(this.cbItem1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "Время ожидания ответа, сек:";
            // 
            // tbTimeOut
            // 
            this.tbTimeOut.Location = new System.Drawing.Point(201, 187);
            this.tbTimeOut.Name = "tbTimeOut";
            this.tbTimeOut.Size = new System.Drawing.Size(56, 22);
            this.tbTimeOut.TabIndex = 2;
            // 
            // frmTuningKR500
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(288, 373);
            this.Controls.Add(this.tbTimeOut);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbStation);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTuningKR500";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройка сервера опроса значений  КР500";
            this.Shown += new System.EventHandler(this.frmTuningVirtuals_Shown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox tbCopyOwner;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox tbCopyKey;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.TextBox tbCopyCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ComboBox cbStation;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ComboBox comboBox4;
        public System.Windows.Forms.CheckBox cbItem4;
        public System.Windows.Forms.ComboBox comboBox3;
        public System.Windows.Forms.CheckBox cbItem3;
        public System.Windows.Forms.ComboBox comboBox2;
        public System.Windows.Forms.CheckBox cbItem2;
        public System.Windows.Forms.ComboBox comboBox1;
        public System.Windows.Forms.CheckBox cbItem1;
        public System.Windows.Forms.ComboBox cbChan4;
        public System.Windows.Forms.ComboBox cbChan3;
        public System.Windows.Forms.ComboBox cbChan2;
        public System.Windows.Forms.ComboBox cbChan1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox tbTimeOut;
    }
}