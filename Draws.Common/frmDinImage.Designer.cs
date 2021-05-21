namespace Draws.Common
{
    partial class frmDinImage
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.cbTransparent = new System.Windows.Forms.CheckBox();
            this.cbStretch = new System.Windows.Forms.CheckBox();
            this.cbMozaika = new System.Windows.Forms.CheckBox();
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbFileName = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 290);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Свойства";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.btnLoad);
            this.groupBox2.Controls.Add(this.pbPreview);
            this.groupBox2.Controls.Add(this.cbTransparent);
            this.groupBox2.Controls.Add(this.cbStretch);
            this.groupBox2.Controls.Add(this.cbMozaika);
            this.groupBox2.Location = new System.Drawing.Point(9, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 188);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Картинка";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(216, 47);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(216, 21);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Загрузить";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // pbPreview
            // 
            this.pbPreview.Location = new System.Drawing.Point(10, 22);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(200, 136);
            this.pbPreview.TabIndex = 6;
            this.pbPreview.TabStop = false;
            // 
            // cbTransparent
            // 
            this.cbTransparent.AutoSize = true;
            this.cbTransparent.Location = new System.Drawing.Point(186, 164);
            this.cbTransparent.Name = "cbTransparent";
            this.cbTransparent.Size = new System.Drawing.Size(98, 17);
            this.cbTransparent.TabIndex = 2;
            this.cbTransparent.Text = "Прозрачность";
            this.cbTransparent.UseVisualStyleBackColor = true;
            this.cbTransparent.Click += new System.EventHandler(this.cbTransparent_Click);
            // 
            // cbStretch
            // 
            this.cbStretch.AutoSize = true;
            this.cbStretch.Location = new System.Drawing.Point(100, 164);
            this.cbStretch.Name = "cbStretch";
            this.cbStretch.Size = new System.Drawing.Size(78, 17);
            this.cbStretch.TabIndex = 1;
            this.cbStretch.Text = "Растянуть";
            this.cbStretch.UseVisualStyleBackColor = true;
            this.cbStretch.Click += new System.EventHandler(this.cbStretch_Click);
            // 
            // cbMozaika
            // 
            this.cbMozaika.AutoSize = true;
            this.cbMozaika.Location = new System.Drawing.Point(10, 164);
            this.cbMozaika.Name = "cbMozaika";
            this.cbMozaika.Size = new System.Drawing.Size(81, 17);
            this.cbMozaika.TabIndex = 0;
            this.cbMozaika.Text = "Замостить";
            this.cbMozaika.UseVisualStyleBackColor = true;
            this.cbMozaika.Click += new System.EventHandler(this.cbMozaika_Click);
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
            this.groupBox7.Location = new System.Drawing.Point(9, 14);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(246, 70);
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
            this.btnClose.Location = new System.Drawing.Point(258, 309);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 310);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Файл:";
            // 
            // lbFileName
            // 
            this.lbFileName.Location = new System.Drawing.Point(61, 310);
            this.lbFileName.Name = "lbFileName";
            this.lbFileName.Size = new System.Drawing.Size(191, 22);
            this.lbFileName.TabIndex = 3;
            this.lbFileName.Text = "(пусто)";
            // 
            // frmDinImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(346, 344);
            this.Controls.Add(this.lbFileName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDinImage";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор элемента \"Картинка\"";
            this.Load += new System.EventHandler(this.frmDinImage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLeft)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.CheckBox cbTransparent;
        private System.Windows.Forms.CheckBox cbStretch;
        private System.Windows.Forms.CheckBox cbMozaika;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbFileName;
    }
}