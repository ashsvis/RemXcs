namespace SchemeEditor
{
    partial class StrokeProps
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbLineJoin = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbTrasparent = new System.Windows.Forms.Label();
            this.tbTrasparent = new System.Windows.Forms.TrackBar();
            this.cbColor = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbWidth = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbPattern = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dlgSelectColor = new System.Windows.Forms.ColorDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbEndCap = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbStartCap = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTrasparent)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(180, 373);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(106, 373);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(68, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pbPreview);
            this.groupBox2.Location = new System.Drawing.Point(14, 268);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 91);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Образец ";
            // 
            // pbPreview
            // 
            this.pbPreview.BackgroundImage = global::SchemeEditor.Properties.Resources.transparent;
            this.pbPreview.Location = new System.Drawing.Point(9, 20);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(224, 64);
            this.pbPreview.TabIndex = 0;
            this.pbPreview.TabStop = false;
            this.pbPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pbPreview_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbLineJoin);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lbTrasparent);
            this.groupBox1.Controls.Add(this.tbTrasparent);
            this.groupBox1.Controls.Add(this.cbColor);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbWidth);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbPattern);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 164);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Линия ";
            // 
            // cbLineJoin
            // 
            this.cbLineJoin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLineJoin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbLineJoin.FormattingEnabled = true;
            this.cbLineJoin.Location = new System.Drawing.Point(104, 99);
            this.cbLineJoin.Name = "cbLineJoin";
            this.cbLineJoin.Size = new System.Drawing.Size(131, 21);
            this.cbLineJoin.TabIndex = 3;
            this.cbLineJoin.SelectionChangeCommitted += new System.EventHandler(this.cbLineJoin_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Соединение:";
            // 
            // lbTrasparent
            // 
            this.lbTrasparent.AutoSize = true;
            this.lbTrasparent.Location = new System.Drawing.Point(201, 134);
            this.lbTrasparent.Name = "lbTrasparent";
            this.lbTrasparent.Size = new System.Drawing.Size(24, 13);
            this.lbTrasparent.TabIndex = 5;
            this.lbTrasparent.Text = "0%";
            this.lbTrasparent.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbTrasparent
            // 
            this.tbTrasparent.AutoSize = false;
            this.tbTrasparent.LargeChange = 10;
            this.tbTrasparent.Location = new System.Drawing.Point(95, 130);
            this.tbTrasparent.Maximum = 255;
            this.tbTrasparent.Name = "tbTrasparent";
            this.tbTrasparent.Size = new System.Drawing.Size(100, 30);
            this.tbTrasparent.TabIndex = 4;
            this.tbTrasparent.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbTrasparent.Scroll += new System.EventHandler(this.tbTrasparent_Scroll);
            // 
            // cbColor
            // 
            this.cbColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbColor.FormattingEnabled = true;
            this.cbColor.Location = new System.Drawing.Point(104, 71);
            this.cbColor.Name = "cbColor";
            this.cbColor.Size = new System.Drawing.Size(131, 22);
            this.cbColor.TabIndex = 2;
            this.cbColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbColor_DrawItem);
            this.cbColor.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
            this.cbColor.SelectionChangeCommitted += new System.EventHandler(this.cbColor_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Прозрачность:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Цвет:";
            // 
            // cbWidth
            // 
            this.cbWidth.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWidth.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbWidth.FormattingEnabled = true;
            this.cbWidth.Location = new System.Drawing.Point(104, 44);
            this.cbWidth.Name = "cbWidth";
            this.cbWidth.Size = new System.Drawing.Size(131, 22);
            this.cbWidth.TabIndex = 1;
            this.cbWidth.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbWidth_DrawItem);
            this.cbWidth.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.cbWidth_MeasureItem);
            this.cbWidth.SelectionChangeCommitted += new System.EventHandler(this.cbWidth_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Ширина:";
            // 
            // cbPattern
            // 
            this.cbPattern.BackColor = System.Drawing.SystemColors.Window;
            this.cbPattern.DisplayMember = "Color";
            this.cbPattern.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbPattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPattern.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbPattern.FormattingEnabled = true;
            this.cbPattern.Location = new System.Drawing.Point(104, 17);
            this.cbPattern.Name = "cbPattern";
            this.cbPattern.Size = new System.Drawing.Size(131, 22);
            this.cbPattern.TabIndex = 0;
            this.cbPattern.ValueMember = "Color";
            this.cbPattern.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbPattern_DrawItem);
            this.cbPattern.SelectionChangeCommitted += new System.EventHandler(this.cbPattern_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Шаблон:";
            // 
            // dlgSelectColor
            // 
            this.dlgSelectColor.AnyColor = true;
            this.dlgSelectColor.FullOpen = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbEndCap);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cbStartCap);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(14, 182);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(239, 80);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " Окончания линий ";
            // 
            // cbEndCap
            // 
            this.cbEndCap.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbEndCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEndCap.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbEndCap.FormattingEnabled = true;
            this.cbEndCap.Location = new System.Drawing.Point(102, 48);
            this.cbEndCap.Name = "cbEndCap";
            this.cbEndCap.Size = new System.Drawing.Size(131, 22);
            this.cbEndCap.TabIndex = 6;
            this.cbEndCap.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbStartCap_DrawItem);
            this.cbEndCap.SelectionChangeCommitted += new System.EventHandler(this.cbEndCap_SelectionChangeCommitted);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Конец:";
            // 
            // cbStartCap
            // 
            this.cbStartCap.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbStartCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStartCap.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbStartCap.FormattingEnabled = true;
            this.cbStartCap.Location = new System.Drawing.Point(102, 20);
            this.cbStartCap.Name = "cbStartCap";
            this.cbStartCap.Size = new System.Drawing.Size(131, 22);
            this.cbStartCap.TabIndex = 5;
            this.cbStartCap.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbStartCap_DrawItem);
            this.cbStartCap.SelectionChangeCommitted += new System.EventHandler(this.cbStartCap_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Начало:";
            // 
            // StrokeProps
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(269, 410);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StrokeProps";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Свойства линий фигуры";
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTrasparent)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbTrasparent;
        private System.Windows.Forms.TrackBar tbTrasparent;
        private System.Windows.Forms.ComboBox cbColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbPattern;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColorDialog dlgSelectColor;
        private System.Windows.Forms.ComboBox cbLineJoin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbEndCap;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbStartCap;
        private System.Windows.Forms.Label label6;
    }
}