namespace DataEditor
{
    partial class frmPageProp
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
            this.rbA3 = new System.Windows.Forms.RadioButton();
            this.rbA4 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbHorizontal = new System.Windows.Forms.RadioButton();
            this.rbVertical = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbBottom = new System.Windows.Forms.TextBox();
            this.tbRight = new System.Windows.Forms.TextBox();
            this.tbTop = new System.Windows.Forms.TextBox();
            this.tbLeft = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbA3);
            this.groupBox1.Controls.Add(this.rbA4);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(73, 71);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Бумага";
            // 
            // rbA3
            // 
            this.rbA3.AutoSize = true;
            this.rbA3.Location = new System.Drawing.Point(10, 46);
            this.rbA3.Name = "rbA3";
            this.rbA3.Size = new System.Drawing.Size(38, 17);
            this.rbA3.TabIndex = 1;
            this.rbA3.Text = "A3";
            this.rbA3.UseVisualStyleBackColor = true;
            this.rbA3.Click += new System.EventHandler(this.rbA3_Click);
            // 
            // rbA4
            // 
            this.rbA4.AutoSize = true;
            this.rbA4.Checked = true;
            this.rbA4.Location = new System.Drawing.Point(10, 22);
            this.rbA4.Name = "rbA4";
            this.rbA4.Size = new System.Drawing.Size(38, 17);
            this.rbA4.TabIndex = 0;
            this.rbA4.TabStop = true;
            this.rbA4.Text = "A4";
            this.rbA4.UseVisualStyleBackColor = true;
            this.rbA4.Click += new System.EventHandler(this.rbA4_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbHorizontal);
            this.groupBox2.Controls.Add(this.rbVertical);
            this.groupBox2.Location = new System.Drawing.Point(95, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(148, 71);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ориентация";
            // 
            // rbHorizontal
            // 
            this.rbHorizontal.AutoSize = true;
            this.rbHorizontal.Location = new System.Drawing.Point(13, 45);
            this.rbHorizontal.Name = "rbHorizontal";
            this.rbHorizontal.Size = new System.Drawing.Size(82, 17);
            this.rbHorizontal.TabIndex = 1;
            this.rbHorizontal.Text = "Альбомная";
            this.rbHorizontal.UseVisualStyleBackColor = true;
            this.rbHorizontal.Click += new System.EventHandler(this.rbHorizontal_Click);
            // 
            // rbVertical
            // 
            this.rbVertical.AutoSize = true;
            this.rbVertical.Checked = true;
            this.rbVertical.Location = new System.Drawing.Point(13, 21);
            this.rbVertical.Name = "rbVertical";
            this.rbVertical.Size = new System.Drawing.Size(80, 17);
            this.rbVertical.TabIndex = 0;
            this.rbVertical.TabStop = true;
            this.rbVertical.Text = "Портреная";
            this.rbVertical.UseVisualStyleBackColor = true;
            this.rbVertical.Click += new System.EventHandler(this.rbVertical_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbBottom);
            this.groupBox3.Controls.Add(this.tbRight);
            this.groupBox3.Controls.Add(this.tbTop);
            this.groupBox3.Controls.Add(this.tbLeft);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(13, 91);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(230, 82);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Поля страницы";
            // 
            // tbBottom
            // 
            this.tbBottom.Location = new System.Drawing.Point(174, 50);
            this.tbBottom.Name = "tbBottom";
            this.tbBottom.Size = new System.Drawing.Size(41, 22);
            this.tbBottom.TabIndex = 3;
            this.tbBottom.Text = "5";
            this.tbBottom.TextChanged += new System.EventHandler(this.tbBottom_TextChanged);
            // 
            // tbRight
            // 
            this.tbRight.Location = new System.Drawing.Point(174, 19);
            this.tbRight.Name = "tbRight";
            this.tbRight.Size = new System.Drawing.Size(41, 22);
            this.tbRight.TabIndex = 1;
            this.tbRight.Text = "5";
            this.tbRight.TextChanged += new System.EventHandler(this.tbRight_TextChanged);
            // 
            // tbTop
            // 
            this.tbTop.Location = new System.Drawing.Point(68, 47);
            this.tbTop.Name = "tbTop";
            this.tbTop.Size = new System.Drawing.Size(41, 22);
            this.tbTop.TabIndex = 2;
            this.tbTop.Text = "5";
            this.tbTop.TextChanged += new System.EventHandler(this.tbTop_TextChanged);
            // 
            // tbLeft
            // 
            this.tbLeft.Location = new System.Drawing.Point(68, 19);
            this.tbLeft.Name = "tbLeft";
            this.tbLeft.Size = new System.Drawing.Size(41, 22);
            this.tbLeft.TabIndex = 0;
            this.tbLeft.Text = "20";
            this.tbLeft.TextChanged += new System.EventHandler(this.tbLeft_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(115, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "Нижнее";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "Правое";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "Верхнее";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Левое";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(168, 179);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(87, 179);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Ввод";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // frmPageProp
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(256, 212);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmPageProp";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Параметры страницы";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbA3;
        private System.Windows.Forms.RadioButton rbA4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbHorizontal;
        private System.Windows.Forms.RadioButton rbVertical;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbBottom;
        private System.Windows.Forms.TextBox tbRight;
        private System.Windows.Forms.TextBox tbTop;
        private System.Windows.Forms.TextBox tbLeft;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}