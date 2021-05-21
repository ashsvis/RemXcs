namespace RemXcs
{
    partial class frmSelectDateRange
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.btnFromEqual = new System.Windows.Forms.Button();
            this.btnFromYesterday = new System.Windows.Forms.Button();
            this.btnFromNow = new System.Windows.Forms.Button();
            this.btnFromTomorow = new System.Windows.Forms.Button();
            this.btnFromLess = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnToTomorow = new System.Windows.Forms.Button();
            this.btnToNow = new System.Windows.Forms.Button();
            this.btnToYesterday = new System.Windows.Forms.Button();
            this.btnToMore = new System.Windows.Forms.Button();
            this.btnToEqual = new System.Windows.Forms.Button();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnFromStartDay = new System.Windows.Forms.Button();
            this.btnFromStartHour = new System.Windows.Forms.Button();
            this.btnYesterdayOnly = new System.Windows.Forms.Button();
            this.btnLastMore = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFromTomorow);
            this.groupBox1.Controls.Add(this.btnFromNow);
            this.groupBox1.Controls.Add(this.btnFromYesterday);
            this.groupBox1.Controls.Add(this.btnFromLess);
            this.groupBox1.Controls.Add(this.btnFromEqual);
            this.groupBox1.Controls.Add(this.dtpFrom);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(491, 89);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Начиная с ...";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd.MM.yyyy ddd HH:mm:ss";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(7, 23);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(203, 22);
            this.dtpFrom.TabIndex = 0;
            // 
            // btnFromEqual
            // 
            this.btnFromEqual.Location = new System.Drawing.Point(7, 51);
            this.btnFromEqual.Name = "btnFromEqual";
            this.btnFromEqual.Size = new System.Drawing.Size(203, 29);
            this.btnFromEqual.TabIndex = 1;
            this.btnFromEqual.Text = "Начальная дата равна конечной";
            this.btnFromEqual.UseVisualStyleBackColor = true;
            // 
            // btnFromYesterday
            // 
            this.btnFromYesterday.Location = new System.Drawing.Point(231, 22);
            this.btnFromYesterday.Name = "btnFromYesterday";
            this.btnFromYesterday.Size = new System.Drawing.Size(75, 23);
            this.btnFromYesterday.TabIndex = 2;
            this.btnFromYesterday.Text = "Вчера";
            this.btnFromYesterday.UseVisualStyleBackColor = true;
            // 
            // btnFromNow
            // 
            this.btnFromNow.Location = new System.Drawing.Point(312, 21);
            this.btnFromNow.Name = "btnFromNow";
            this.btnFromNow.Size = new System.Drawing.Size(84, 23);
            this.btnFromNow.TabIndex = 3;
            this.btnFromNow.Text = "Сегодня";
            this.btnFromNow.UseVisualStyleBackColor = true;
            // 
            // btnFromTomorow
            // 
            this.btnFromTomorow.Location = new System.Drawing.Point(402, 21);
            this.btnFromTomorow.Name = "btnFromTomorow";
            this.btnFromTomorow.Size = new System.Drawing.Size(75, 23);
            this.btnFromTomorow.TabIndex = 4;
            this.btnFromTomorow.Text = "Завтра";
            this.btnFromTomorow.UseVisualStyleBackColor = true;
            // 
            // btnFromLess
            // 
            this.btnFromLess.Location = new System.Drawing.Point(231, 51);
            this.btnFromLess.Name = "btnFromLess";
            this.btnFromLess.Size = new System.Drawing.Size(246, 29);
            this.btnFromLess.TabIndex = 5;
            this.btnFromLess.Text = "Начальная дата меньше конечной на...";
            this.btnFromLess.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnToTomorow);
            this.groupBox2.Controls.Add(this.btnToNow);
            this.groupBox2.Controls.Add(this.btnToYesterday);
            this.groupBox2.Controls.Add(this.btnToMore);
            this.groupBox2.Controls.Add(this.btnToEqual);
            this.groupBox2.Controls.Add(this.dtpTo);
            this.groupBox2.Location = new System.Drawing.Point(13, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(491, 89);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Заканчивая ...";
            // 
            // btnToTomorow
            // 
            this.btnToTomorow.Location = new System.Drawing.Point(402, 21);
            this.btnToTomorow.Name = "btnToTomorow";
            this.btnToTomorow.Size = new System.Drawing.Size(75, 23);
            this.btnToTomorow.TabIndex = 4;
            this.btnToTomorow.Text = "Завтра";
            this.btnToTomorow.UseVisualStyleBackColor = true;
            // 
            // btnToNow
            // 
            this.btnToNow.Location = new System.Drawing.Point(312, 21);
            this.btnToNow.Name = "btnToNow";
            this.btnToNow.Size = new System.Drawing.Size(84, 23);
            this.btnToNow.TabIndex = 3;
            this.btnToNow.Text = "Сегодня";
            this.btnToNow.UseVisualStyleBackColor = true;
            // 
            // btnToYesterday
            // 
            this.btnToYesterday.Location = new System.Drawing.Point(231, 22);
            this.btnToYesterday.Name = "btnToYesterday";
            this.btnToYesterday.Size = new System.Drawing.Size(75, 23);
            this.btnToYesterday.TabIndex = 2;
            this.btnToYesterday.Text = "Вчера";
            this.btnToYesterday.UseVisualStyleBackColor = true;
            // 
            // btnToMore
            // 
            this.btnToMore.Location = new System.Drawing.Point(231, 51);
            this.btnToMore.Name = "btnToMore";
            this.btnToMore.Size = new System.Drawing.Size(246, 29);
            this.btnToMore.TabIndex = 5;
            this.btnToMore.Text = "Конечная дата больше начальной на...";
            this.btnToMore.UseVisualStyleBackColor = true;
            // 
            // btnToEqual
            // 
            this.btnToEqual.Location = new System.Drawing.Point(7, 51);
            this.btnToEqual.Name = "btnToEqual";
            this.btnToEqual.Size = new System.Drawing.Size(203, 29);
            this.btnToEqual.TabIndex = 1;
            this.btnToEqual.Text = "Конечная дата равна начальной";
            this.btnToEqual.UseVisualStyleBackColor = true;
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd.MM.yyyy ddd HH:mm:ss";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(7, 23);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(203, 22);
            this.dtpTo.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnLastMore);
            this.groupBox3.Controls.Add(this.btnFromStartHour);
            this.groupBox3.Controls.Add(this.btnYesterdayOnly);
            this.groupBox3.Controls.Add(this.btnFromStartDay);
            this.groupBox3.Location = new System.Drawing.Point(13, 204);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(491, 84);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Установить границы периода выборки:";
            // 
            // btnFromStartDay
            // 
            this.btnFromStartDay.Location = new System.Drawing.Point(7, 22);
            this.btnFromStartDay.Name = "btnFromStartDay";
            this.btnFromStartDay.Size = new System.Drawing.Size(247, 23);
            this.btnFromStartDay.TabIndex = 0;
            this.btnFromStartDay.Text = "С начала месяца по текущую дату";
            this.btnFromStartDay.UseVisualStyleBackColor = true;
            // 
            // btnFromStartHour
            // 
            this.btnFromStartHour.Location = new System.Drawing.Point(260, 21);
            this.btnFromStartHour.Name = "btnFromStartHour";
            this.btnFromStartHour.Size = new System.Drawing.Size(217, 23);
            this.btnFromStartHour.TabIndex = 1;
            this.btnFromStartHour.Text = "С начала суток по текущее время";
            this.btnFromStartHour.UseVisualStyleBackColor = true;
            // 
            // btnYesterdayOnly
            // 
            this.btnYesterdayOnly.Location = new System.Drawing.Point(7, 50);
            this.btnYesterdayOnly.Name = "btnYesterdayOnly";
            this.btnYesterdayOnly.Size = new System.Drawing.Size(247, 23);
            this.btnYesterdayOnly.TabIndex = 2;
            this.btnYesterdayOnly.Text = "Только за предыдущие сутки";
            this.btnYesterdayOnly.UseVisualStyleBackColor = true;
            // 
            // btnLastMore
            // 
            this.btnLastMore.Location = new System.Drawing.Point(260, 50);
            this.btnLastMore.Name = "btnLastMore";
            this.btnLastMore.Size = new System.Drawing.Size(217, 23);
            this.btnLastMore.TabIndex = 3;
            this.btnLastMore.Text = "За последние...";
            this.btnLastMore.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnClear.Location = new System.Drawing.Point(76, 294);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(147, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Выключить фильтр";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(339, 294);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(88, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Применить";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(433, 294);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(71, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmSelectDateRange
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(519, 327);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectDateRange";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Установка границ периода выборки";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFromTomorow;
        private System.Windows.Forms.Button btnFromNow;
        private System.Windows.Forms.Button btnFromYesterday;
        private System.Windows.Forms.Button btnFromLess;
        private System.Windows.Forms.Button btnFromEqual;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnToTomorow;
        private System.Windows.Forms.Button btnToNow;
        private System.Windows.Forms.Button btnToYesterday;
        private System.Windows.Forms.Button btnToMore;
        private System.Windows.Forms.Button btnToEqual;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnLastMore;
        private System.Windows.Forms.Button btnFromStartHour;
        private System.Windows.Forms.Button btnYesterdayOnly;
        private System.Windows.Forms.Button btnFromStartDay;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}