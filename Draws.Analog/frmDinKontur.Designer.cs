namespace Draws.Analog
{
    partial class frmDinKontur
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
            this.tbTest = new System.Windows.Forms.TrackBar();
            this.btnSelect = new System.Windows.Forms.Button();
            this.tbPtName = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.udHeight = new System.Windows.Forms.NumericUpDown();
            this.udTop = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.udWidth = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.udLeft = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.lbTest = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.cbFontName = new System.Windows.Forms.ComboBox();
            this.cbStrikeout = new System.Windows.Forms.CheckBox();
            this.cbUnderline = new System.Windows.Forms.CheckBox();
            this.cbFontColor = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbItalic = new System.Windows.Forms.CheckBox();
            this.udFontSize = new System.Windows.Forms.NumericUpDown();
            this.cbBold = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.cbColor = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTest)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLeft)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbColor);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tbTest);
            this.groupBox1.Controls.Add(this.btnSelect);
            this.groupBox1.Controls.Add(this.tbPtName);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Controls.Add(this.lbTest);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.groupBox8);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 265);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Свойства";
            // 
            // tbTest
            // 
            this.tbTest.AutoSize = false;
            this.tbTest.Location = new System.Drawing.Point(201, 223);
            this.tbTest.Maximum = 100;
            this.tbTest.Name = "tbTest";
            this.tbTest.Size = new System.Drawing.Size(104, 20);
            this.tbTest.TabIndex = 6;
            this.tbTest.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbTest.Scroll += new System.EventHandler(this.tbTest_Scroll);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(292, 173);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(27, 23);
            this.btnSelect.TabIndex = 5;
            this.btnSelect.Text = "...";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // tbPtName
            // 
            this.tbPtName.Location = new System.Drawing.Point(183, 173);
            this.tbPtName.Name = "tbPtName";
            this.tbPtName.Size = new System.Drawing.Size(108, 22);
            this.tbPtName.TabIndex = 4;
            this.tbPtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPtName_KeyDown);
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
            this.groupBox7.Location = new System.Drawing.Point(6, 20);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(249, 70);
            this.groupBox7.TabIndex = 1;
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
            // lbTest
            // 
            this.lbTest.AutoSize = true;
            this.lbTest.Location = new System.Drawing.Point(235, 242);
            this.lbTest.Name = "lbTest";
            this.lbTest.Size = new System.Drawing.Size(30, 14);
            this.lbTest.TabIndex = 1;
            this.lbTest.Text = "0 %";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(180, 152);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 14);
            this.label10.TabIndex = 1;
            this.label10.Text = "Имя позиции:";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.cbFontName);
            this.groupBox8.Controls.Add(this.cbStrikeout);
            this.groupBox8.Controls.Add(this.cbUnderline);
            this.groupBox8.Controls.Add(this.cbFontColor);
            this.groupBox8.Controls.Add(this.label8);
            this.groupBox8.Controls.Add(this.cbItalic);
            this.groupBox8.Controls.Add(this.udFontSize);
            this.groupBox8.Controls.Add(this.cbBold);
            this.groupBox8.Controls.Add(this.label11);
            this.groupBox8.Controls.Add(this.label9);
            this.groupBox8.Location = new System.Drawing.Point(6, 90);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(171, 166);
            this.groupBox8.TabIndex = 2;
            this.groupBox8.TabStop = false;
            // 
            // cbFontName
            // 
            this.cbFontName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbFontName.FormattingEnabled = true;
            this.cbFontName.Location = new System.Drawing.Point(6, 33);
            this.cbFontName.Name = "cbFontName";
            this.cbFontName.Size = new System.Drawing.Size(109, 22);
            this.cbFontName.TabIndex = 0;
            this.cbFontName.SelectionChangeCommitted += new System.EventHandler(this.cbFontName_SelectionChangeCommitted);
            this.cbFontName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbFontName_KeyDown);
            // 
            // cbStrikeout
            // 
            this.cbStrikeout.AutoSize = true;
            this.cbStrikeout.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbStrikeout.Location = new System.Drawing.Point(123, 116);
            this.cbStrikeout.Name = "cbStrikeout";
            this.cbStrikeout.Size = new System.Drawing.Size(33, 18);
            this.cbStrikeout.TabIndex = 6;
            this.cbStrikeout.Text = "S";
            this.cbStrikeout.UseVisualStyleBackColor = true;
            this.cbStrikeout.Click += new System.EventHandler(this.cbStrikeout_Click);
            // 
            // cbUnderline
            // 
            this.cbUnderline.AutoSize = true;
            this.cbUnderline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbUnderline.Location = new System.Drawing.Point(86, 116);
            this.cbUnderline.Name = "cbUnderline";
            this.cbUnderline.Size = new System.Drawing.Size(34, 18);
            this.cbUnderline.TabIndex = 5;
            this.cbUnderline.Text = "U";
            this.cbUnderline.UseVisualStyleBackColor = true;
            this.cbUnderline.Click += new System.EventHandler(this.cbUnderline_Click);
            // 
            // cbFontColor
            // 
            this.cbFontColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFontColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFontColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbFontColor.FormattingEnabled = true;
            this.cbFontColor.Location = new System.Drawing.Point(8, 85);
            this.cbFontColor.Name = "cbFontColor";
            this.cbFontColor.Size = new System.Drawing.Size(157, 23);
            this.cbFontColor.TabIndex = 2;
            this.cbFontColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbFrameColor_DrawItem);
            this.cbFontColor.SelectedIndexChanged += new System.EventHandler(this.cbFrameColor_SelectedIndexChanged);
            this.cbFontColor.SelectionChangeCommitted += new System.EventHandler(this.cbFrameColor_SelectionChangeCommitted);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 14);
            this.label8.TabIndex = 1;
            this.label8.Text = "Шрифт";
            // 
            // cbItalic
            // 
            this.cbItalic.AutoSize = true;
            this.cbItalic.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbItalic.Location = new System.Drawing.Point(50, 116);
            this.cbItalic.Name = "cbItalic";
            this.cbItalic.Size = new System.Drawing.Size(30, 18);
            this.cbItalic.TabIndex = 4;
            this.cbItalic.Text = "I";
            this.cbItalic.UseVisualStyleBackColor = true;
            this.cbItalic.Click += new System.EventHandler(this.cbItalic_Click);
            // 
            // udFontSize
            // 
            this.udFontSize.Location = new System.Drawing.Point(121, 33);
            this.udFontSize.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.udFontSize.Name = "udFontSize";
            this.udFontSize.Size = new System.Drawing.Size(44, 22);
            this.udFontSize.TabIndex = 1;
            this.udFontSize.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.udFontSize.ValueChanged += new System.EventHandler(this.udFontSize_ValueChanged);
            // 
            // cbBold
            // 
            this.cbBold.AutoSize = true;
            this.cbBold.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbBold.Location = new System.Drawing.Point(10, 116);
            this.cbBold.Name = "cbBold";
            this.cbBold.Size = new System.Drawing.Size(34, 18);
            this.cbBold.TabIndex = 3;
            this.cbBold.Text = "B";
            this.cbBold.UseVisualStyleBackColor = true;
            this.cbBold.Click += new System.EventHandler(this.cbBold_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 14);
            this.label11.TabIndex = 1;
            this.label11.Text = "Цвет шрифта:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(120, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 14);
            this.label9.TabIndex = 0;
            this.label9.Text = "Размер";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(235, 206);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 14);
            this.label12.TabIndex = 1;
            this.label12.Text = "Тест";
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Location = new System.Drawing.Point(257, 284);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Закрыть";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // cbColor
            // 
            this.cbColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbColor.FormattingEnabled = true;
            this.cbColor.Location = new System.Drawing.Point(183, 122);
            this.cbColor.Name = "cbColor";
            this.cbColor.Size = new System.Drawing.Size(136, 23);
            this.cbColor.TabIndex = 3;
            this.cbColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbFrameColor_DrawItem);
            this.cbColor.SelectedIndexChanged += new System.EventHandler(this.cbFrameColor_SelectedIndexChanged);
            this.cbColor.SelectionChangeCommitted += new System.EventHandler(this.cbFrameColor_SelectionChangeCommitted);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(183, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 14);
            this.label7.TabIndex = 10;
            this.label7.Text = "Цвет фона:";
            // 
            // frmDinKontur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOk;
            this.ClientSize = new System.Drawing.Size(350, 315);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDinKontur";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор элемента \"Контур регулирования\"";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDinKontur_FormClosing);
            this.Load += new System.EventHandler(this.frmDinKontur_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTest)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLeft)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udFontSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TextBox tbPtName;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.NumericUpDown udHeight;
        private System.Windows.Forms.NumericUpDown udTop;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown udWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown udLeft;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox cbFontName;
        private System.Windows.Forms.CheckBox cbUnderline;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbItalic;
        private System.Windows.Forms.NumericUpDown udFontSize;
        private System.Windows.Forms.CheckBox cbBold;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox cbStrikeout;
        private System.Windows.Forms.ComboBox cbFontColor;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TrackBar tbTest;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbTest;
        private System.Windows.Forms.ComboBox cbColor;
        private System.Windows.Forms.Label label7;

    }
}