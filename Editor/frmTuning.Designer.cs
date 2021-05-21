namespace DataEditor
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
            this.tbPortNumber = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbDatabase = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbFetchBase = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbHostName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbScreenSize = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbPortNumber
            // 
            this.tbPortNumber.Location = new System.Drawing.Point(105, 49);
            this.tbPortNumber.Name = "tbPortNumber";
            this.tbPortNumber.Size = new System.Drawing.Size(54, 22);
            this.tbPortNumber.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(58, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 14);
            this.label10.TabIndex = 12;
            this.label10.Text = "Порт:";
            // 
            // tbDatabase
            // 
            this.tbDatabase.Location = new System.Drawing.Point(105, 161);
            this.tbDatabase.Name = "tbDatabase";
            this.tbDatabase.Size = new System.Drawing.Size(136, 22);
            this.tbDatabase.TabIndex = 5;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(27, 136);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 14);
            this.label14.TabIndex = 11;
            this.label14.Text = "БД опроса:";
            // 
            // tbFetchBase
            // 
            this.tbFetchBase.Location = new System.Drawing.Point(105, 133);
            this.tbFetchBase.Name = "tbFetchBase";
            this.tbFetchBase.Size = new System.Drawing.Size(136, 22);
            this.tbFetchBase.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 164);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 14);
            this.label13.TabIndex = 10;
            this.label13.Text = "БД хранения:";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(105, 105);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(136, 22);
            this.tbPassword.TabIndex = 3;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(45, 108);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 14);
            this.label12.TabIndex = 6;
            this.label12.Text = "Пароль:";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(105, 77);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(136, 22);
            this.tbUserName.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 80);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 14);
            this.label11.TabIndex = 9;
            this.label11.Text = "Пользователь:";
            // 
            // tbHostName
            // 
            this.tbHostName.Location = new System.Drawing.Point(105, 21);
            this.tbHostName.Name = "tbHostName";
            this.tbHostName.Size = new System.Drawing.Size(136, 22);
            this.tbHostName.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 14);
            this.label9.TabIndex = 7;
            this.label9.Text = "Размещение:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(211, 283);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(130, 283);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ввод";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbHostName);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.tbPortNumber);
            this.groupBox1.Controls.Add(this.tbUserName);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.tbDatabase);
            this.groupBox1.Controls.Add(this.tbPassword);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.tbFetchBase);
            this.groupBox1.Location = new System.Drawing.Point(12, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 197);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Сервер SQL";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbScreenSize);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(271, 61);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Редактор мнемосхем";
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
            this.cbScreenSize.Location = new System.Drawing.Point(105, 25);
            this.cbScreenSize.Name = "cbScreenSize";
            this.cbScreenSize.Size = new System.Drawing.Size(154, 22);
            this.cbScreenSize.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 14);
            this.label1.TabIndex = 8;
            this.label1.Text = "Размер экрана:";
            // 
            // frmTuning
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(298, 318);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmTuning";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.TextBox tbPortNumber;
        public System.Windows.Forms.TextBox tbDatabase;
        public System.Windows.Forms.TextBox tbFetchBase;
        public System.Windows.Forms.TextBox tbPassword;
        public System.Windows.Forms.TextBox tbUserName;
        public System.Windows.Forms.TextBox tbHostName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cbScreenSize;
    }
}