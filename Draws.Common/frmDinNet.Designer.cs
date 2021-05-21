namespace Draws.Common
{
    partial class frmDinNet
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
            this.cbHideText = new System.Windows.Forms.CheckBox();
            this.udScaleLow = new System.Windows.Forms.NumericUpDown();
            this.cbShowPersents = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
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
            this.udScaleHigh = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udScaleLow)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLeft)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScaleHigh)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbHideText);
            this.groupBox1.Controls.Add(this.udScaleLow);
            this.groupBox1.Controls.Add(this.cbShowPersents);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Controls.Add(this.groupBox8);
            this.groupBox1.Controls.Add(this.udScaleHigh);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 231);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Свойства";
            // 
            // cbHideText
            // 
            this.cbHideText.AutoSize = true;
            this.cbHideText.Location = new System.Drawing.Point(195, 116);
            this.cbHideText.Name = "cbHideText";
            this.cbHideText.Size = new System.Drawing.Size(124, 17);
            this.cbHideText.TabIndex = 3;
            this.cbHideText.Text = "Скрыть текст шкал";
            this.cbHideText.UseVisualStyleBackColor = true;
            this.cbHideText.Click += new System.EventHandler(this.cbHideText_Click);
            // 
            // udScaleLow
            // 
            this.udScaleLow.DecimalPlaces = 3;
            this.udScaleLow.Location = new System.Drawing.Point(269, 171);
            this.udScaleLow.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.udScaleLow.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.udScaleLow.Name = "udScaleLow";
            this.udScaleLow.Size = new System.Drawing.Size(75, 22);
            this.udScaleLow.TabIndex = 5;
            this.udScaleLow.ValueChanged += new System.EventHandler(this.udScaleLow_ValueChanged);
            // 
            // cbShowPersents
            // 
            this.cbShowPersents.AutoSize = true;
            this.cbShowPersents.Location = new System.Drawing.Point(195, 95);
            this.cbShowPersents.Name = "cbShowPersents";
            this.cbShowPersents.Size = new System.Drawing.Size(116, 17);
            this.cbShowPersents.TabIndex = 2;
            this.cbShowPersents.Text = "Правая шкала (%)";
            this.cbShowPersents.UseVisualStyleBackColor = true;
            this.cbShowPersents.Click += new System.EventHandler(this.cbShowPersents_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(220, 200);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "Низ шкалы:";
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
            this.groupBox7.Location = new System.Drawing.Point(9, 12);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(253, 70);
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
            this.groupBox8.Location = new System.Drawing.Point(9, 83);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(171, 140);
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
            this.label11.Location = new System.Drawing.Point(7, 68);
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
            // udScaleHigh
            // 
            this.udScaleHigh.DecimalPlaces = 3;
            this.udScaleHigh.Location = new System.Drawing.Point(269, 143);
            this.udScaleHigh.Maximum = new decimal(new int[] {
            900000,
            0,
            0,
            0});
            this.udScaleHigh.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.udScaleHigh.Name = "udScaleHigh";
            this.udScaleHigh.Size = new System.Drawing.Size(75, 22);
            this.udScaleHigh.TabIndex = 4;
            this.udScaleHigh.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.udScaleHigh.ValueChanged += new System.EventHandler(this.udScaleHigh_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(186, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Верх шкалы:";
            // 
            // frmDinNet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 254);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDinNet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор элемента \"Сетка\"";
            this.Load += new System.EventHandler(this.frmDinNet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udScaleLow)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLeft)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScaleHigh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClose;
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
        private System.Windows.Forms.CheckBox cbHideText;
        private System.Windows.Forms.CheckBox cbShowPersents;
        private System.Windows.Forms.NumericUpDown udScaleLow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown udScaleHigh;
        private System.Windows.Forms.Label label1;
    }
}