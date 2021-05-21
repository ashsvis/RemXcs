namespace SchemeEditor
{
    partial class TextProps
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
            this.tcTextTuning = new System.Windows.Forms.TabControl();
            this.tbFont = new System.Windows.Forms.TabPage();
            this.lbTrasparent = new System.Windows.Forms.Label();
            this.tbTrasparent = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.cbColor = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbFontOptions = new System.Windows.Forms.GroupBox();
            this.cbSrikeout = new System.Windows.Forms.CheckBox();
            this.cbUnderline = new System.Windows.Forms.CheckBox();
            this.cbSize = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbStyle = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbFont = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbArrange = new System.Windows.Forms.TabPage();
            this.gbArrange = new System.Windows.Forms.GroupBox();
            this.cbVertical = new System.Windows.Forms.ComboBox();
            this.cbHorizontal = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbExample = new System.Windows.Forms.GroupBox();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.dlgSelectColor = new System.Windows.Forms.ColorDialog();
            this.cbVerticalText = new System.Windows.Forms.CheckBox();
            this.tcTextTuning.SuspendLayout();
            this.tbFont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTrasparent)).BeginInit();
            this.gbFontOptions.SuspendLayout();
            this.tbArrange.SuspendLayout();
            this.gbArrange.SuspendLayout();
            this.gbExample.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // tcTextTuning
            // 
            this.tcTextTuning.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tcTextTuning.Controls.Add(this.tbFont);
            this.tcTextTuning.Controls.Add(this.tbArrange);
            this.tcTextTuning.Location = new System.Drawing.Point(11, 12);
            this.tcTextTuning.Name = "tcTextTuning";
            this.tcTextTuning.SelectedIndex = 0;
            this.tcTextTuning.Size = new System.Drawing.Size(357, 223);
            this.tcTextTuning.TabIndex = 0;
            // 
            // tbFont
            // 
            this.tbFont.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbFont.Controls.Add(this.lbTrasparent);
            this.tbFont.Controls.Add(this.tbTrasparent);
            this.tbFont.Controls.Add(this.label5);
            this.tbFont.Controls.Add(this.cbColor);
            this.tbFont.Controls.Add(this.label4);
            this.tbFont.Controls.Add(this.gbFontOptions);
            this.tbFont.Controls.Add(this.cbSize);
            this.tbFont.Controls.Add(this.label3);
            this.tbFont.Controls.Add(this.cbStyle);
            this.tbFont.Controls.Add(this.label2);
            this.tbFont.Controls.Add(this.cbFont);
            this.tbFont.Controls.Add(this.label1);
            this.tbFont.Location = new System.Drawing.Point(4, 22);
            this.tbFont.Name = "tbFont";
            this.tbFont.Padding = new System.Windows.Forms.Padding(3);
            this.tbFont.Size = new System.Drawing.Size(349, 197);
            this.tbFont.TabIndex = 0;
            this.tbFont.Text = "Шрифт";
            // 
            // lbTrasparent
            // 
            this.lbTrasparent.AutoSize = true;
            this.lbTrasparent.Location = new System.Drawing.Point(310, 158);
            this.lbTrasparent.Name = "lbTrasparent";
            this.lbTrasparent.Size = new System.Drawing.Size(24, 13);
            this.lbTrasparent.TabIndex = 11;
            this.lbTrasparent.Text = "0%";
            this.lbTrasparent.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbTrasparent
            // 
            this.tbTrasparent.AutoSize = false;
            this.tbTrasparent.LargeChange = 10;
            this.tbTrasparent.Location = new System.Drawing.Point(219, 154);
            this.tbTrasparent.Maximum = 255;
            this.tbTrasparent.Name = "tbTrasparent";
            this.tbTrasparent.Size = new System.Drawing.Size(86, 28);
            this.tbTrasparent.TabIndex = 10;
            this.tbTrasparent.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbTrasparent.Scroll += new System.EventHandler(this.tbTrasparent_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(144, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Прозрачность:";
            // 
            // cbColor
            // 
            this.cbColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbColor.FormattingEnabled = true;
            this.cbColor.Location = new System.Drawing.Point(182, 127);
            this.cbColor.Name = "cbColor";
            this.cbColor.Size = new System.Drawing.Size(150, 22);
            this.cbColor.TabIndex = 8;
            this.cbColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbColor_DrawItem);
            this.cbColor.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
            this.cbColor.SelectionChangeCommitted += new System.EventHandler(this.cbColor_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(144, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Цвет:";
            // 
            // gbFontOptions
            // 
            this.gbFontOptions.Controls.Add(this.cbSrikeout);
            this.gbFontOptions.Controls.Add(this.cbUnderline);
            this.gbFontOptions.Location = new System.Drawing.Point(9, 114);
            this.gbFontOptions.Name = "gbFontOptions";
            this.gbFontOptions.Size = new System.Drawing.Size(129, 76);
            this.gbFontOptions.TabIndex = 6;
            this.gbFontOptions.TabStop = false;
            this.gbFontOptions.Text = "Видоизменение";
            // 
            // cbSrikeout
            // 
            this.cbSrikeout.AutoSize = true;
            this.cbSrikeout.Location = new System.Drawing.Point(6, 44);
            this.cbSrikeout.Name = "cbSrikeout";
            this.cbSrikeout.Size = new System.Drawing.Size(98, 17);
            this.cbSrikeout.TabIndex = 1;
            this.cbSrikeout.Text = "Зачеркивание";
            this.cbSrikeout.UseVisualStyleBackColor = true;
            this.cbSrikeout.CheckedChanged += new System.EventHandler(this.cbSrikeout_CheckedChanged);
            // 
            // cbUnderline
            // 
            this.cbUnderline.AutoSize = true;
            this.cbUnderline.Location = new System.Drawing.Point(6, 20);
            this.cbUnderline.Name = "cbUnderline";
            this.cbUnderline.Size = new System.Drawing.Size(106, 17);
            this.cbUnderline.TabIndex = 0;
            this.cbUnderline.Text = "Подчеркивание";
            this.cbUnderline.UseVisualStyleBackColor = true;
            this.cbUnderline.CheckedChanged += new System.EventHandler(this.cbUnderline_CheckedChanged);
            // 
            // cbSize
            // 
            this.cbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbSize.FormattingEnabled = true;
            this.cbSize.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "36",
            "48",
            "72"});
            this.cbSize.Location = new System.Drawing.Point(274, 23);
            this.cbSize.Name = "cbSize";
            this.cbSize.Size = new System.Drawing.Size(63, 85);
            this.cbSize.TabIndex = 5;
            this.cbSize.Text = "10";
            this.cbSize.SelectedValueChanged += new System.EventHandler(this.cbSize_SelectedValueChanged);
            this.cbSize.TextChanged += new System.EventHandler(this.sbSize_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(272, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Размер";
            // 
            // cbStyle
            // 
            this.cbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbStyle.FormattingEnabled = true;
            this.cbStyle.Items.AddRange(new object[] {
            "обычный",
            "курсив",
            "полужирный",
            "полужирный курсив"});
            this.cbStyle.Location = new System.Drawing.Point(141, 23);
            this.cbStyle.Name = "cbStyle";
            this.cbStyle.Size = new System.Drawing.Size(128, 85);
            this.cbStyle.TabIndex = 3;
            this.cbStyle.Text = "обычный";
            this.cbStyle.SelectionChangeCommitted += new System.EventHandler(this.cbStyle_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Начертание";
            // 
            // cbFont
            // 
            this.cbFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbFont.FormattingEnabled = true;
            this.cbFont.Location = new System.Drawing.Point(9, 23);
            this.cbFont.Name = "cbFont";
            this.cbFont.Size = new System.Drawing.Size(128, 85);
            this.cbFont.TabIndex = 1;
            this.cbFont.Text = "Arial";
            this.cbFont.SelectionChangeCommitted += new System.EventHandler(this.cbFont_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Шрифт";
            // 
            // tbArrange
            // 
            this.tbArrange.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbArrange.Controls.Add(this.cbVerticalText);
            this.tbArrange.Controls.Add(this.gbArrange);
            this.tbArrange.Location = new System.Drawing.Point(4, 22);
            this.tbArrange.Name = "tbArrange";
            this.tbArrange.Padding = new System.Windows.Forms.Padding(3);
            this.tbArrange.Size = new System.Drawing.Size(349, 197);
            this.tbArrange.TabIndex = 1;
            this.tbArrange.Text = "Выравнивание";
            // 
            // gbArrange
            // 
            this.gbArrange.Controls.Add(this.cbVertical);
            this.gbArrange.Controls.Add(this.cbHorizontal);
            this.gbArrange.Controls.Add(this.label7);
            this.gbArrange.Controls.Add(this.label6);
            this.gbArrange.Location = new System.Drawing.Point(18, 18);
            this.gbArrange.Name = "gbArrange";
            this.gbArrange.Size = new System.Drawing.Size(229, 79);
            this.gbArrange.TabIndex = 0;
            this.gbArrange.TabStop = false;
            this.gbArrange.Text = "Выравнивание";
            // 
            // cbVertical
            // 
            this.cbVertical.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVertical.FormattingEnabled = true;
            this.cbVertical.Items.AddRange(new object[] {
            "по верхнему краю",
            "по центру",
            "по нижнему краю"});
            this.cbVertical.Location = new System.Drawing.Point(110, 48);
            this.cbVertical.Name = "cbVertical";
            this.cbVertical.Size = new System.Drawing.Size(105, 21);
            this.cbVertical.TabIndex = 3;
            this.cbVertical.SelectionChangeCommitted += new System.EventHandler(this.cbVertical_SelectionChangeCommitted);
            // 
            // cbHorizontal
            // 
            this.cbHorizontal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHorizontal.Items.AddRange(new object[] {
            "по левому краю",
            "по центру",
            "по правому краю"});
            this.cbHorizontal.Location = new System.Drawing.Point(110, 20);
            this.cbHorizontal.Name = "cbHorizontal";
            this.cbHorizontal.Size = new System.Drawing.Size(105, 21);
            this.cbHorizontal.TabIndex = 2;
            this.cbHorizontal.SelectionChangeCommitted += new System.EventHandler(this.cbHorizontal_SelectionChangeCommitted);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "по вертикали:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "по горизонтали:";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(233, 332);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(64, 21);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(303, 332);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 21);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // gbExample
            // 
            this.gbExample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbExample.Controls.Add(this.pbPreview);
            this.gbExample.Location = new System.Drawing.Point(11, 237);
            this.gbExample.Name = "gbExample";
            this.gbExample.Size = new System.Drawing.Size(353, 83);
            this.gbExample.TabIndex = 9;
            this.gbExample.TabStop = false;
            this.gbExample.Text = " Образец ";
            // 
            // pbPreview
            // 
            this.pbPreview.BackgroundImage = global::SchemeEditor.Properties.Resources.transparent;
            this.pbPreview.Location = new System.Drawing.Point(12, 19);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(328, 56);
            this.pbPreview.TabIndex = 0;
            this.pbPreview.TabStop = false;
            this.pbPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pbPreview_Paint);
            // 
            // dlgSelectColor
            // 
            this.dlgSelectColor.AnyColor = true;
            this.dlgSelectColor.FullOpen = true;
            // 
            // cbVerticalText
            // 
            this.cbVerticalText.AutoSize = true;
            this.cbVerticalText.Location = new System.Drawing.Point(21, 119);
            this.cbVerticalText.Name = "cbVerticalText";
            this.cbVerticalText.Size = new System.Drawing.Size(212, 17);
            this.cbVerticalText.TabIndex = 1;
            this.cbVerticalText.Text = "Вертикальное расположение текста";
            this.cbVerticalText.UseVisualStyleBackColor = true;
            this.cbVerticalText.CheckedChanged += new System.EventHandler(this.cbVerticalText_CheckedChanged);
            // 
            // TextProps
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(378, 362);
            this.Controls.Add(this.gbExample);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tcTextTuning);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextProps";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Формат надписи";
            this.tcTextTuning.ResumeLayout(false);
            this.tbFont.ResumeLayout(false);
            this.tbFont.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTrasparent)).EndInit();
            this.gbFontOptions.ResumeLayout(false);
            this.gbFontOptions.PerformLayout();
            this.tbArrange.ResumeLayout(false);
            this.tbArrange.PerformLayout();
            this.gbArrange.ResumeLayout(false);
            this.gbArrange.PerformLayout();
            this.gbExample.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcTextTuning;
        private System.Windows.Forms.TabPage tbFont;
        private System.Windows.Forms.TabPage tbArrange;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbStyle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbFont;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSize;
        private System.Windows.Forms.GroupBox gbFontOptions;
        private System.Windows.Forms.CheckBox cbSrikeout;
        private System.Windows.Forms.CheckBox cbUnderline;
        private System.Windows.Forms.ComboBox cbColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbExample;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Label lbTrasparent;
        private System.Windows.Forms.TrackBar tbTrasparent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColorDialog dlgSelectColor;
        private System.Windows.Forms.GroupBox gbArrange;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbVertical;
        private System.Windows.Forms.ComboBox cbHorizontal;
        private System.Windows.Forms.CheckBox cbVerticalText;
    }
}