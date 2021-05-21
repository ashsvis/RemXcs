namespace Draws.Digital
{
    partial class frmDinButton
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
            this.cbDirect = new System.Windows.Forms.CheckBox();
            this.cbFixed = new System.Windows.Forms.CheckBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.tbPtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.tbText = new System.Windows.Forms.TextBox();
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
            this.groupBox1.SuspendLayout();
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
            this.groupBox1.Controls.Add(this.cbDirect);
            this.groupBox1.Controls.Add(this.cbFixed);
            this.groupBox1.Controls.Add(this.btnSelect);
            this.groupBox1.Controls.Add(this.tbPtName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.tbText);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Controls.Add(this.groupBox8);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 281);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Свойства";
            // 
            // cbDirect
            // 
            this.cbDirect.AutoSize = true;
            this.cbDirect.Location = new System.Drawing.Point(191, 168);
            this.cbDirect.Name = "cbDirect";
            this.cbDirect.Size = new System.Drawing.Size(90, 18);
            this.cbDirect.TabIndex = 8;
            this.cbDirect.Text = "С запросом";
            this.cbDirect.UseVisualStyleBackColor = true;
            this.cbDirect.Click += new System.EventHandler(this.cbDirect_Click);
            // 
            // cbFixed
            // 
            this.cbFixed.AutoSize = true;
            this.cbFixed.Location = new System.Drawing.Point(191, 144);
            this.cbFixed.Name = "cbFixed";
            this.cbFixed.Size = new System.Drawing.Size(100, 18);
            this.cbFixed.TabIndex = 7;
            this.cbFixed.Text = "С фиксацией";
            this.cbFixed.UseVisualStyleBackColor = true;
            this.cbFixed.Click += new System.EventHandler(this.cbFixed_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(306, 116);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(27, 23);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "...";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // tbPtName
            // 
            this.tbPtName.Location = new System.Drawing.Point(191, 116);
            this.tbPtName.Name = "tbPtName";
            this.tbPtName.Size = new System.Drawing.Size(118, 22);
            this.tbPtName.TabIndex = 3;
            this.tbPtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPtName_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(188, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 14);
            this.label1.TabIndex = 14;
            this.label1.Text = "Имя позиции:";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(271, 248);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // tbText
            // 
            this.tbText.Location = new System.Drawing.Point(13, 248);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(252, 22);
            this.tbText.TabIndex = 9;
            this.tbText.TextChanged += new System.EventHandler(this.tbText_TextChanged);
            this.tbText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbText_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 231);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 14);
            this.label10.TabIndex = 13;
            this.label10.Text = "Текст надписи:";
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
            this.groupBox7.Location = new System.Drawing.Point(11, 12);
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
            this.groupBox8.Location = new System.Drawing.Point(11, 83);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(171, 145);
            this.groupBox8.TabIndex = 1;
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
            this.cbStrikeout.Location = new System.Drawing.Point(123, 121);
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
            this.cbUnderline.Location = new System.Drawing.Point(86, 121);
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
            this.cbFontColor.Location = new System.Drawing.Point(7, 84);
            this.cbFontColor.Name = "cbFontColor";
            this.cbFontColor.Size = new System.Drawing.Size(158, 23);
            this.cbFontColor.TabIndex = 2;
            this.cbFontColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbFontColor_DrawItem);
            this.cbFontColor.SelectedIndexChanged += new System.EventHandler(this.cbFontColor_SelectedIndexChanged);
            this.cbFontColor.SelectionChangeCommitted += new System.EventHandler(this.cbFontColor_SelectionChangeCommitted);
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
            this.cbItalic.Location = new System.Drawing.Point(50, 121);
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
            this.cbBold.Location = new System.Drawing.Point(10, 121);
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
            this.label11.Location = new System.Drawing.Point(6, 67);
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
            // frmDinButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(385, 305);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDinButton";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор элемента \"Кнопка\"";
            this.Load += new System.EventHandler(this.frmDinButton_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.NumericUpDown udHeight;
        private System.Windows.Forms.NumericUpDown udTop;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown udWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown udLeft;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox cbFontName;
        private System.Windows.Forms.CheckBox cbStrikeout;
        private System.Windows.Forms.CheckBox cbUnderline;
        private System.Windows.Forms.ComboBox cbFontColor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbItalic;
        private System.Windows.Forms.NumericUpDown udFontSize;
        private System.Windows.Forms.CheckBox cbBold;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TextBox tbPtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbDirect;
        private System.Windows.Forms.CheckBox cbFixed;
    }
}