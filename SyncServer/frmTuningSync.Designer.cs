namespace SyncServer
{
    partial class frmTuningVirtual
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
            this.label7 = new System.Windows.Forms.Label();
            this.nudRecCount = new System.Windows.Forms.NumericUpDown();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRecCount)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(201, 196);
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
            this.btnOk.Location = new System.Drawing.Point(117, 196);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ввод";
            this.btnOk.UseVisualStyleBackColor = true;
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
            this.tbCopyOwner.Size = new System.Drawing.Size(122, 22);
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
            this.tbCopyKey.Size = new System.Drawing.Size(122, 22);
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
            this.groupBox2.Location = new System.Drawing.Point(15, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(261, 121);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Авторизация сервера репликации";
            // 
            // tbCopyCode
            // 
            this.tbCopyCode.Location = new System.Drawing.Point(118, 49);
            this.tbCopyCode.Name = "tbCopyCode";
            this.tbCopyCode.ReadOnly = true;
            this.tbCopyCode.Size = new System.Drawing.Size(122, 22);
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 14);
            this.label7.TabIndex = 5;
            this.label7.Text = "Запрос записей";
            // 
            // nudRecCount
            // 
            this.nudRecCount.Location = new System.Drawing.Point(112, 35);
            this.nudRecCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudRecCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRecCount.Name = "nudRecCount";
            this.nudRecCount.Size = new System.Drawing.Size(62, 22);
            this.nudRecCount.TabIndex = 1;
            this.nudRecCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // frmTuningVirtual
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(288, 231);
            this.Controls.Add(this.nudRecCount);
            this.Controls.Add(this.cbStation);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTuningVirtual";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройка сервера репликации";
            this.Shown += new System.EventHandler(this.frmTuningVirtual_Shown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRecCount)).EndInit();
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
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.NumericUpDown nudRecCount;
    }
}