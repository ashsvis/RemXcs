namespace Draws.Common
{
    partial class frmDinDraw
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
            this.btnClose = new System.Windows.Forms.Button();
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
            this.lbTransparent = new System.Windows.Forms.Label();
            this.tbTransparent = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbBackPatternColor = new System.Windows.Forms.ComboBox();
            this.cbBackPattern = new System.Windows.Forms.ComboBox();
            this.cbBackColor = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbStrokeColor = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbStrokePattern = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLeft)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTransparent)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Controls.Add(this.groupBox8);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(533, 231);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Свойства";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(442, 202);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
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
            this.groupBox8.Controls.Add(this.lbTransparent);
            this.groupBox8.Controls.Add(this.tbTransparent);
            this.groupBox8.Controls.Add(this.label8);
            this.groupBox8.Controls.Add(this.label7);
            this.groupBox8.Controls.Add(this.label2);
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Controls.Add(this.cbBackPatternColor);
            this.groupBox8.Controls.Add(this.cbBackPattern);
            this.groupBox8.Controls.Add(this.cbBackColor);
            this.groupBox8.Location = new System.Drawing.Point(9, 83);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(253, 140);
            this.groupBox8.TabIndex = 1;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Фон фигуры";
            // 
            // lbTransparent
            // 
            this.lbTransparent.Location = new System.Drawing.Point(203, 105);
            this.lbTransparent.Name = "lbTransparent";
            this.lbTransparent.Size = new System.Drawing.Size(44, 24);
            this.lbTransparent.TabIndex = 5;
            this.lbTransparent.Text = "0 %";
            this.lbTransparent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbTransparent
            // 
            this.tbTransparent.AutoSize = false;
            this.tbTransparent.Location = new System.Drawing.Point(96, 106);
            this.tbTransparent.Maximum = 255;
            this.tbTransparent.Name = "tbTransparent";
            this.tbTransparent.Size = new System.Drawing.Size(101, 28);
            this.tbTransparent.TabIndex = 4;
            this.tbTransparent.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbTransparent.Scroll += new System.EventHandler(this.tbTransparent_Scroll);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 14);
            this.label8.TabIndex = 3;
            this.label8.Text = "Прозрачность:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 14);
            this.label7.TabIndex = 3;
            this.label7.Text = "Цвет шаблона:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Шаблон:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "Цвет:";
            // 
            // cbBackPatternColor
            // 
            this.cbBackPatternColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbBackPatternColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBackPatternColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbBackPatternColor.FormattingEnabled = true;
            this.cbBackPatternColor.Location = new System.Drawing.Point(103, 77);
            this.cbBackPatternColor.Name = "cbBackPatternColor";
            this.cbBackPatternColor.Size = new System.Drawing.Size(135, 23);
            this.cbBackPatternColor.TabIndex = 2;
            this.cbBackPatternColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbBackColor_DrawItem);
            this.cbBackPatternColor.SelectedIndexChanged += new System.EventHandler(this.cbBackColor_SelectedIndexChanged);
            this.cbBackPatternColor.SelectionChangeCommitted += new System.EventHandler(this.cbBackColor_SelectionChangeCommitted);
            // 
            // cbBackPattern
            // 
            this.cbBackPattern.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbBackPattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBackPattern.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbBackPattern.FormattingEnabled = true;
            this.cbBackPattern.Location = new System.Drawing.Point(103, 48);
            this.cbBackPattern.Name = "cbBackPattern";
            this.cbBackPattern.Size = new System.Drawing.Size(135, 23);
            this.cbBackPattern.TabIndex = 2;
            this.cbBackPattern.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbPattern_DrawItem);
            this.cbBackPattern.SelectionChangeCommitted += new System.EventHandler(this.cbPattern_SelectionChangeCommitted);
            // 
            // cbBackColor
            // 
            this.cbBackColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbBackColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBackColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbBackColor.FormattingEnabled = true;
            this.cbBackColor.Location = new System.Drawing.Point(103, 19);
            this.cbBackColor.Name = "cbBackColor";
            this.cbBackColor.Size = new System.Drawing.Size(135, 23);
            this.cbBackColor.TabIndex = 2;
            this.cbBackColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbBackColor_DrawItem);
            this.cbBackColor.SelectedIndexChanged += new System.EventHandler(this.cbBackColor_SelectedIndexChanged);
            this.cbBackColor.SelectionChangeCommitted += new System.EventHandler(this.cbBackColor_SelectionChangeCommitted);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbStrokeColor);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cbStrokePattern);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(269, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(248, 174);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Граница фигуры";
            // 
            // cbStrokeColor
            // 
            this.cbStrokeColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbStrokeColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStrokeColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbStrokeColor.FormattingEnabled = true;
            this.cbStrokeColor.Location = new System.Drawing.Point(70, 21);
            this.cbStrokeColor.Name = "cbStrokeColor";
            this.cbStrokeColor.Size = new System.Drawing.Size(135, 23);
            this.cbStrokeColor.TabIndex = 2;
            this.cbStrokeColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbBackColor_DrawItem);
            this.cbStrokeColor.SelectedIndexChanged += new System.EventHandler(this.cbBackColor_SelectedIndexChanged);
            this.cbStrokeColor.SelectionChangeCommitted += new System.EventHandler(this.cbBackColor_SelectionChangeCommitted);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 14);
            this.label9.TabIndex = 3;
            this.label9.Text = "Цвет:";
            // 
            // cbStrokePattern
            // 
            this.cbStrokePattern.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbStrokePattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStrokePattern.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbStrokePattern.FormattingEnabled = true;
            this.cbStrokePattern.Location = new System.Drawing.Point(70, 50);
            this.cbStrokePattern.Name = "cbStrokePattern";
            this.cbStrokePattern.Size = new System.Drawing.Size(135, 23);
            this.cbStrokePattern.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 14);
            this.label10.TabIndex = 3;
            this.label10.Text = "Шаблон:";
            // 
            // frmDinDraw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 254);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDinDraw";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор элемента \"Фигура\"";
            this.Load += new System.EventHandler(this.frmDinDraw_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLeft)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTransparent)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.ComboBox cbBackColor;
        private System.Windows.Forms.TrackBar tbTransparent;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbBackPatternColor;
        private System.Windows.Forms.ComboBox cbBackPattern;
        private System.Windows.Forms.Label lbTransparent;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbStrokeColor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbStrokePattern;
        private System.Windows.Forms.Label label10;
    }
}