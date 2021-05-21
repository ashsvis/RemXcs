namespace Draws.Digital
{
    partial class frmDinDigital
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
            this.rbTestOn = new System.Windows.Forms.RadioButton();
            this.rbTestOff = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbRichBorder = new System.Windows.Forms.RadioButton();
            this.rbSingleBorder = new System.Windows.Forms.RadioButton();
            this.rbNoBorder = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbEllipce = new System.Windows.Forms.RadioButton();
            this.rbRectangle = new System.Windows.Forms.RadioButton();
            this.rbRing = new System.Windows.Forms.RadioButton();
            this.rbSquare = new System.Windows.Forms.RadioButton();
            this.btnSelect = new System.Windows.Forms.Button();
            this.tbPtName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.udHeight = new System.Windows.Forms.NumericUpDown();
            this.udTop = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.udWidth = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.udLeft = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbTestOn);
            this.groupBox1.Controls.Add(this.rbTestOff);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnSelect);
            this.groupBox1.Controls.Add(this.tbPtName);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 276);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Свойства";
            // 
            // rbTestOn
            // 
            this.rbTestOn.AutoSize = true;
            this.rbTestOn.Location = new System.Drawing.Point(210, 247);
            this.rbTestOn.Name = "rbTestOn";
            this.rbTestOn.Size = new System.Drawing.Size(44, 17);
            this.rbTestOn.TabIndex = 10;
            this.rbTestOn.Tag = "True";
            this.rbTestOn.Text = "Вкл";
            this.rbTestOn.UseVisualStyleBackColor = true;
            this.rbTestOn.Click += new System.EventHandler(this.rbTestOff_Click);
            // 
            // rbTestOff
            // 
            this.rbTestOff.AutoSize = true;
            this.rbTestOff.Checked = true;
            this.rbTestOff.Location = new System.Drawing.Point(151, 247);
            this.rbTestOff.Name = "rbTestOff";
            this.rbTestOff.Size = new System.Drawing.Size(52, 17);
            this.rbTestOff.TabIndex = 10;
            this.rbTestOff.TabStop = true;
            this.rbTestOff.Tag = "False";
            this.rbTestOff.Text = "Выкл";
            this.rbTestOff.UseVisualStyleBackColor = true;
            this.rbTestOff.Click += new System.EventHandler(this.rbTestOff_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(182, 228);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 14);
            this.label12.TabIndex = 9;
            this.label12.Text = "Тест";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbRichBorder);
            this.groupBox3.Controls.Add(this.rbSingleBorder);
            this.groupBox3.Controls.Add(this.rbNoBorder);
            this.groupBox3.Location = new System.Drawing.Point(144, 125);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(115, 100);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Рамка";
            // 
            // rbRichBorder
            // 
            this.rbRichBorder.AutoSize = true;
            this.rbRichBorder.Location = new System.Drawing.Point(7, 70);
            this.rbRichBorder.Name = "rbRichBorder";
            this.rbRichBorder.Size = new System.Drawing.Size(78, 17);
            this.rbRichBorder.TabIndex = 0;
            this.rbRichBorder.Tag = "2";
            this.rbRichBorder.Text = "Объёмная";
            this.rbRichBorder.UseVisualStyleBackColor = true;
            this.rbRichBorder.Click += new System.EventHandler(this.rbNoBorder_Click);
            // 
            // rbSingleBorder
            // 
            this.rbSingleBorder.AutoSize = true;
            this.rbSingleBorder.Location = new System.Drawing.Point(7, 46);
            this.rbSingleBorder.Name = "rbSingleBorder";
            this.rbSingleBorder.Size = new System.Drawing.Size(68, 17);
            this.rbSingleBorder.TabIndex = 0;
            this.rbSingleBorder.Tag = "1";
            this.rbSingleBorder.Text = "Простая";
            this.rbSingleBorder.UseVisualStyleBackColor = true;
            this.rbSingleBorder.Click += new System.EventHandler(this.rbNoBorder_Click);
            // 
            // rbNoBorder
            // 
            this.rbNoBorder.AutoSize = true;
            this.rbNoBorder.Checked = true;
            this.rbNoBorder.Location = new System.Drawing.Point(7, 22);
            this.rbNoBorder.Name = "rbNoBorder";
            this.rbNoBorder.Size = new System.Drawing.Size(82, 17);
            this.rbNoBorder.TabIndex = 0;
            this.rbNoBorder.TabStop = true;
            this.rbNoBorder.Tag = "0";
            this.rbNoBorder.Text = "Отсутсвует";
            this.rbNoBorder.UseVisualStyleBackColor = true;
            this.rbNoBorder.Click += new System.EventHandler(this.rbNoBorder_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbEllipce);
            this.groupBox2.Controls.Add(this.rbRectangle);
            this.groupBox2.Controls.Add(this.rbRing);
            this.groupBox2.Controls.Add(this.rbSquare);
            this.groupBox2.Location = new System.Drawing.Point(10, 125);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(128, 128);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Вид";
            // 
            // rbEllipce
            // 
            this.rbEllipce.AutoSize = true;
            this.rbEllipce.Location = new System.Drawing.Point(7, 94);
            this.rbEllipce.Name = "rbEllipce";
            this.rbEllipce.Size = new System.Drawing.Size(62, 17);
            this.rbEllipce.TabIndex = 0;
            this.rbEllipce.Tag = "3";
            this.rbEllipce.Text = "Эллипс";
            this.rbEllipce.UseVisualStyleBackColor = true;
            this.rbEllipce.Click += new System.EventHandler(this.rbSquare_Click);
            // 
            // rbRectangle
            // 
            this.rbRectangle.AutoSize = true;
            this.rbRectangle.Location = new System.Drawing.Point(7, 70);
            this.rbRectangle.Name = "rbRectangle";
            this.rbRectangle.Size = new System.Drawing.Size(105, 17);
            this.rbRectangle.TabIndex = 0;
            this.rbRectangle.Tag = "2";
            this.rbRectangle.Text = "Прямоугольник";
            this.rbRectangle.UseVisualStyleBackColor = true;
            this.rbRectangle.Click += new System.EventHandler(this.rbSquare_Click);
            // 
            // rbRing
            // 
            this.rbRing.AutoSize = true;
            this.rbRing.Location = new System.Drawing.Point(7, 46);
            this.rbRing.Name = "rbRing";
            this.rbRing.Size = new System.Drawing.Size(48, 17);
            this.rbRing.TabIndex = 0;
            this.rbRing.Tag = "1";
            this.rbRing.Text = "Круг";
            this.rbRing.UseVisualStyleBackColor = true;
            this.rbRing.Click += new System.EventHandler(this.rbSquare_Click);
            // 
            // rbSquare
            // 
            this.rbSquare.AutoSize = true;
            this.rbSquare.Checked = true;
            this.rbSquare.Location = new System.Drawing.Point(7, 22);
            this.rbSquare.Name = "rbSquare";
            this.rbSquare.Size = new System.Drawing.Size(67, 17);
            this.rbSquare.TabIndex = 0;
            this.rbSquare.TabStop = true;
            this.rbSquare.Tag = "0";
            this.rbSquare.Text = "Квадрат";
            this.rbSquare.UseVisualStyleBackColor = true;
            this.rbSquare.Click += new System.EventHandler(this.rbSquare_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(232, 90);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(27, 23);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "...";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // tbPtName
            // 
            this.tbPtName.Location = new System.Drawing.Point(103, 91);
            this.tbPtName.Name = "tbPtName";
            this.tbPtName.Size = new System.Drawing.Size(129, 22);
            this.tbPtName.TabIndex = 1;
            this.tbPtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPtName_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 94);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 14);
            this.label10.TabIndex = 7;
            this.label10.Text = "Имя позиции:";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.udHeight);
            this.groupBox7.Controls.Add(this.udTop);
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Controls.Add(this.udWidth);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Controls.Add(this.udLeft);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Location = new System.Drawing.Point(10, 14);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(249, 70);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            // 
            // udHeight
            // 
            this.udHeight.Location = new System.Drawing.Point(186, 42);
            this.udHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.udHeight.Name = "udHeight";
            this.udHeight.Size = new System.Drawing.Size(52, 22);
            this.udHeight.TabIndex = 3;
            this.udHeight.ValueChanged += new System.EventHandler(this.udHeight_ValueChanged);
            // 
            // udTop
            // 
            this.udTop.Location = new System.Drawing.Point(63, 42);
            this.udTop.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.udTop.Name = "udTop";
            this.udTop.Size = new System.Drawing.Size(52, 22);
            this.udTop.TabIndex = 1;
            this.udTop.ValueChanged += new System.EventHandler(this.udTop_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(125, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "Высота:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "Сверху:";
            // 
            // udWidth
            // 
            this.udWidth.Location = new System.Drawing.Point(186, 14);
            this.udWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.udWidth.Name = "udWidth";
            this.udWidth.Size = new System.Drawing.Size(52, 22);
            this.udWidth.TabIndex = 2;
            this.udWidth.ValueChanged += new System.EventHandler(this.udWidth_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(125, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "Ширина:";
            // 
            // udLeft
            // 
            this.udLeft.Location = new System.Drawing.Point(63, 14);
            this.udLeft.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.udLeft.Name = "udLeft";
            this.udLeft.Size = new System.Drawing.Size(52, 22);
            this.udLeft.TabIndex = 0;
            this.udLeft.ValueChanged += new System.EventHandler(this.udLeft_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "Слева:";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(198, 295);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 25);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // frmDinDigital
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(297, 329);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDinDigital";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор элемента \"Дискретное значение\"";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDinDigital_FormClosing);
            this.Load += new System.EventHandler(this.frmDinDigital_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLeft)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.NumericUpDown udHeight;
        private System.Windows.Forms.NumericUpDown udTop;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown udWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown udLeft;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TextBox tbPtName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbRichBorder;
        private System.Windows.Forms.RadioButton rbSingleBorder;
        private System.Windows.Forms.RadioButton rbNoBorder;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbEllipce;
        private System.Windows.Forms.RadioButton rbRectangle;
        private System.Windows.Forms.RadioButton rbRing;
        private System.Windows.Forms.RadioButton rbSquare;
        private System.Windows.Forms.RadioButton rbTestOff;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton rbTestOn;
    }
}