namespace FetchROC809
{
    partial class frmTuningROC809
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
            this.tbIPPort = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbIPAddress = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbDateAddr = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbTimeAddr = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tbNodeAddr = new System.Windows.Forms.TextBox();
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
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(201, 336);
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
            this.btnOk.Location = new System.Drawing.Point(117, 336);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ввод";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // tbIPPort
            // 
            this.tbIPPort.Location = new System.Drawing.Point(100, 51);
            this.tbIPPort.Name = "tbIPPort";
            this.tbIPPort.Size = new System.Drawing.Size(62, 22);
            this.tbIPPort.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(55, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 14);
            this.label10.TabIndex = 23;
            this.label10.Text = "Порт:";
            // 
            // tbIPAddress
            // 
            this.tbIPAddress.Location = new System.Drawing.Point(100, 21);
            this.tbIPAddress.Name = "tbIPAddress";
            this.tbIPAddress.Size = new System.Drawing.Size(148, 22);
            this.tbIPAddress.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(35, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 14);
            this.label9.TabIndex = 20;
            this.label9.Text = "IP-адрес:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbDateAddr);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tbTimeAddr);
            this.groupBox1.Controls.Add(this.tbIPAddress);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.tbNodeAddr);
            this.groupBox1.Controls.Add(this.tbIPPort);
            this.groupBox1.Location = new System.Drawing.Point(12, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 171);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Связь с ROC809";
            // 
            // tbDateAddr
            // 
            this.tbDateAddr.Location = new System.Drawing.Point(100, 107);
            this.tbDateAddr.Name = "tbDateAddr";
            this.tbDateAddr.Size = new System.Drawing.Size(62, 22);
            this.tbDateAddr.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 14);
            this.label7.TabIndex = 26;
            this.label7.Text = "Адрес даты:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 138);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 14);
            this.label8.TabIndex = 27;
            this.label8.Text = "и времени:";
            // 
            // tbTimeAddr
            // 
            this.tbTimeAddr.Location = new System.Drawing.Point(100, 135);
            this.tbTimeAddr.Name = "tbTimeAddr";
            this.tbTimeAddr.Size = new System.Drawing.Size(62, 22);
            this.tbTimeAddr.TabIndex = 4;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(20, 82);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(74, 14);
            this.label15.TabIndex = 23;
            this.label15.Text = "Адрес узла:";
            // 
            // tbNodeAddr
            // 
            this.tbNodeAddr.Location = new System.Drawing.Point(100, 79);
            this.tbNodeAddr.Name = "tbNodeAddr";
            this.tbNodeAddr.Size = new System.Drawing.Size(62, 22);
            this.tbNodeAddr.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 14);
            this.label1.TabIndex = 20;
            this.label1.Text = "Владелец копии:";
            // 
            // tbCopyOwner
            // 
            this.tbCopyOwner.Location = new System.Drawing.Point(118, 21);
            this.tbCopyOwner.Name = "tbCopyOwner";
            this.tbCopyOwner.Size = new System.Drawing.Size(114, 22);
            this.tbCopyOwner.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 14);
            this.label2.TabIndex = 20;
            this.label2.Text = "Ключ копии:";
            // 
            // tbCopyKey
            // 
            this.tbCopyKey.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbCopyKey.Location = new System.Drawing.Point(118, 77);
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
            this.groupBox2.Location = new System.Drawing.Point(12, 215);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(261, 111);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Авторизация сервера опроса";
            // 
            // tbCopyCode
            // 
            this.tbCopyCode.Location = new System.Drawing.Point(118, 49);
            this.tbCopyCode.Name = "tbCopyCode";
            this.tbCopyCode.ReadOnly = true;
            this.tbCopyCode.Size = new System.Drawing.Size(114, 22);
            this.tbCopyCode.TabIndex = 1;
            this.tbCopyCode.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 52);
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
            // frmTuningROC809
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(288, 371);
            this.Controls.Add(this.cbStation);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTuningROC809";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройка сервера опроса ROC809";
            this.Shown += new System.EventHandler(this.frmTuningROC809_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.TextBox tbIPPort;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox tbIPAddress;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
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
        public System.Windows.Forms.TextBox tbDateAddr;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox tbTimeAddr;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.TextBox tbNodeAddr;
    }
}