namespace Draws.Digital
{
    partial class frmDinLine
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
            this.tabSelector = new System.Windows.Forms.TabControl();
            this.tabNoParam = new System.Windows.Forms.TabPage();
            this.cbColor = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbLineStyle = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.udLineWidth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.tabOff = new System.Windows.Forms.TabPage();
            this.cbColor0 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbLineStyle0 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.udLineWidth0 = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.tabOn = new System.Windows.Forms.TabPage();
            this.cbColor1 = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbLineStyle1 = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.udLineWidth1 = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.tbPtName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbLineKind = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.udHeight = new System.Windows.Forms.NumericUpDown();
            this.udTop = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.udWidth = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.udLeft = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tabSelector.SuspendLayout();
            this.tabNoParam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udLineWidth)).BeginInit();
            this.tabOff.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udLineWidth0)).BeginInit();
            this.tabOn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udLineWidth1)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.tabSelector);
            this.groupBox1.Controls.Add(this.btnSelect);
            this.groupBox1.Controls.Add(this.tbPtName);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cbLineKind);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(365, 287);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Свойства";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(263, 250);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 25);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // tabSelector
            // 
            this.tabSelector.Controls.Add(this.tabNoParam);
            this.tabSelector.Controls.Add(this.tabOff);
            this.tabSelector.Controls.Add(this.tabOn);
            this.tabSelector.Location = new System.Drawing.Point(10, 119);
            this.tabSelector.Name = "tabSelector";
            this.tabSelector.SelectedIndex = 0;
            this.tabSelector.Size = new System.Drawing.Size(340, 125);
            this.tabSelector.TabIndex = 4;
            this.tabSelector.SelectedIndexChanged += new System.EventHandler(this.tabSelector_SelectedIndexChanged);
            // 
            // tabNoParam
            // 
            this.tabNoParam.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabNoParam.Controls.Add(this.cbColor);
            this.tabNoParam.Controls.Add(this.label8);
            this.tabNoParam.Controls.Add(this.cbLineStyle);
            this.tabNoParam.Controls.Add(this.label7);
            this.tabNoParam.Controls.Add(this.udLineWidth);
            this.tabNoParam.Controls.Add(this.label2);
            this.tabNoParam.Location = new System.Drawing.Point(4, 23);
            this.tabNoParam.Name = "tabNoParam";
            this.tabNoParam.Padding = new System.Windows.Forms.Padding(3);
            this.tabNoParam.Size = new System.Drawing.Size(332, 98);
            this.tabNoParam.TabIndex = 0;
            this.tabNoParam.Text = "Просто линия";
            // 
            // cbColor
            // 
            this.cbColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbColor.FormattingEnabled = true;
            this.cbColor.Location = new System.Drawing.Point(124, 66);
            this.cbColor.Name = "cbColor";
            this.cbColor.Size = new System.Drawing.Size(131, 23);
            this.cbColor.TabIndex = 2;
            this.cbColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbColor_DrawItem);
            this.cbColor.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
            this.cbColor.SelectionChangeCommitted += new System.EventHandler(this.cbColor_SelectionChangeCommitted);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(80, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 14);
            this.label8.TabIndex = 12;
            this.label8.Text = "Цвет:";
            // 
            // cbLineStyle
            // 
            this.cbLineStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLineStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLineStyle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbLineStyle.FormattingEnabled = true;
            this.cbLineStyle.Location = new System.Drawing.Point(124, 37);
            this.cbLineStyle.Name = "cbLineStyle";
            this.cbLineStyle.Size = new System.Drawing.Size(131, 23);
            this.cbLineStyle.TabIndex = 1;
            this.cbLineStyle.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbLineStyle_DrawItem);
            this.cbLineStyle.SelectionChangeCommitted += new System.EventHandler(this.cbLineStyle_SelectionChangeCommitted);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(74, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 14);
            this.label7.TabIndex = 12;
            this.label7.Text = "Стиль:";
            // 
            // udLineWidth
            // 
            this.udLineWidth.Location = new System.Drawing.Point(124, 9);
            this.udLineWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udLineWidth.Name = "udLineWidth";
            this.udLineWidth.Size = new System.Drawing.Size(48, 22);
            this.udLineWidth.TabIndex = 0;
            this.udLineWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udLineWidth.ValueChanged += new System.EventHandler(this.udLineWidth_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "Толщина:";
            // 
            // tabOff
            // 
            this.tabOff.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabOff.Controls.Add(this.cbColor0);
            this.tabOff.Controls.Add(this.label9);
            this.tabOff.Controls.Add(this.cbLineStyle0);
            this.tabOff.Controls.Add(this.label11);
            this.tabOff.Controls.Add(this.udLineWidth0);
            this.tabOff.Controls.Add(this.label12);
            this.tabOff.Location = new System.Drawing.Point(4, 23);
            this.tabOff.Name = "tabOff";
            this.tabOff.Padding = new System.Windows.Forms.Padding(3);
            this.tabOff.Size = new System.Drawing.Size(332, 98);
            this.tabOff.TabIndex = 1;
            this.tabOff.Text = "При \"0\"";
            // 
            // cbColor0
            // 
            this.cbColor0.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbColor0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColor0.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbColor0.FormattingEnabled = true;
            this.cbColor0.Location = new System.Drawing.Point(124, 66);
            this.cbColor0.Name = "cbColor0";
            this.cbColor0.Size = new System.Drawing.Size(131, 23);
            this.cbColor0.TabIndex = 2;
            this.cbColor0.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbColor_DrawItem);
            this.cbColor0.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
            this.cbColor0.SelectionChangeCommitted += new System.EventHandler(this.cbColor_SelectionChangeCommitted);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(80, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 14);
            this.label9.TabIndex = 17;
            this.label9.Text = "Цвет:";
            // 
            // cbLineStyle0
            // 
            this.cbLineStyle0.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLineStyle0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLineStyle0.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbLineStyle0.FormattingEnabled = true;
            this.cbLineStyle0.Location = new System.Drawing.Point(124, 37);
            this.cbLineStyle0.Name = "cbLineStyle0";
            this.cbLineStyle0.Size = new System.Drawing.Size(131, 23);
            this.cbLineStyle0.TabIndex = 1;
            this.cbLineStyle0.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbLineStyle_DrawItem);
            this.cbLineStyle0.SelectionChangeCommitted += new System.EventHandler(this.cbLineStyle_SelectionChangeCommitted);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(74, 40);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 14);
            this.label11.TabIndex = 18;
            this.label11.Text = "Стиль:";
            // 
            // udLineWidth0
            // 
            this.udLineWidth0.Location = new System.Drawing.Point(124, 9);
            this.udLineWidth0.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udLineWidth0.Name = "udLineWidth0";
            this.udLineWidth0.Size = new System.Drawing.Size(48, 22);
            this.udLineWidth0.TabIndex = 0;
            this.udLineWidth0.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udLineWidth0.ValueChanged += new System.EventHandler(this.udLineWidth0_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(56, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 14);
            this.label12.TabIndex = 13;
            this.label12.Text = "Толщина:";
            // 
            // tabOn
            // 
            this.tabOn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabOn.Controls.Add(this.cbColor1);
            this.tabOn.Controls.Add(this.label13);
            this.tabOn.Controls.Add(this.cbLineStyle1);
            this.tabOn.Controls.Add(this.label14);
            this.tabOn.Controls.Add(this.udLineWidth1);
            this.tabOn.Controls.Add(this.label15);
            this.tabOn.Location = new System.Drawing.Point(4, 23);
            this.tabOn.Name = "tabOn";
            this.tabOn.Padding = new System.Windows.Forms.Padding(3);
            this.tabOn.Size = new System.Drawing.Size(332, 98);
            this.tabOn.TabIndex = 2;
            this.tabOn.Text = "При \"1\"";
            // 
            // cbColor1
            // 
            this.cbColor1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbColor1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColor1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbColor1.FormattingEnabled = true;
            this.cbColor1.Location = new System.Drawing.Point(124, 66);
            this.cbColor1.Name = "cbColor1";
            this.cbColor1.Size = new System.Drawing.Size(131, 23);
            this.cbColor1.TabIndex = 2;
            this.cbColor1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbColor_DrawItem);
            this.cbColor1.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
            this.cbColor1.SelectionChangeCommitted += new System.EventHandler(this.cbColor_SelectionChangeCommitted);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(80, 69);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 14);
            this.label13.TabIndex = 17;
            this.label13.Text = "Цвет:";
            // 
            // cbLineStyle1
            // 
            this.cbLineStyle1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLineStyle1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLineStyle1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbLineStyle1.FormattingEnabled = true;
            this.cbLineStyle1.Location = new System.Drawing.Point(124, 37);
            this.cbLineStyle1.Name = "cbLineStyle1";
            this.cbLineStyle1.Size = new System.Drawing.Size(131, 23);
            this.cbLineStyle1.TabIndex = 1;
            this.cbLineStyle1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbLineStyle_DrawItem);
            this.cbLineStyle1.SelectionChangeCommitted += new System.EventHandler(this.cbLineStyle_SelectionChangeCommitted);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(74, 40);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 14);
            this.label14.TabIndex = 18;
            this.label14.Text = "Стиль:";
            // 
            // udLineWidth1
            // 
            this.udLineWidth1.Location = new System.Drawing.Point(124, 9);
            this.udLineWidth1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udLineWidth1.Name = "udLineWidth1";
            this.udLineWidth1.Size = new System.Drawing.Size(48, 22);
            this.udLineWidth1.TabIndex = 0;
            this.udLineWidth1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udLineWidth1.ValueChanged += new System.EventHandler(this.udLineWidth1_ValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(56, 11);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 14);
            this.label15.TabIndex = 13;
            this.label15.Text = "Толщина:";
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
            // cbLineKind
            // 
            this.cbLineKind.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cbLineKind.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLineKind.DropDownHeight = 196;
            this.cbLineKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLineKind.FormattingEnabled = true;
            this.cbLineKind.IntegralHeight = false;
            this.cbLineKind.ItemHeight = 64;
            this.cbLineKind.Location = new System.Drawing.Point(269, 41);
            this.cbLineKind.Name = "cbLineKind";
            this.cbLineKind.Size = new System.Drawing.Size(81, 70);
            this.cbLineKind.TabIndex = 3;
            this.cbLineKind.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbLineKind_DrawItem);
            this.cbLineKind.SelectionChangeCommitted += new System.EventHandler(this.cbLineKind_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(266, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "Тип линии:";
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
            // frmDinLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(389, 312);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDinLine";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор элемента \"Динамическая линия\"";
            this.Load += new System.EventHandler(this.frmDinLine_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabSelector.ResumeLayout(false);
            this.tabNoParam.ResumeLayout(false);
            this.tabNoParam.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udLineWidth)).EndInit();
            this.tabOff.ResumeLayout(false);
            this.tabOff.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udLineWidth0)).EndInit();
            this.tabOn.ResumeLayout(false);
            this.tabOn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udLineWidth1)).EndInit();
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
        private System.Windows.Forms.ComboBox cbLineKind;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.TabControl tabSelector;
        private System.Windows.Forms.TabPage tabNoParam;
        private System.Windows.Forms.TabPage tabOff;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabPage tabOn;
        private System.Windows.Forms.NumericUpDown udLineWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbColor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbLineStyle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbColor0;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbLineStyle0;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown udLineWidth0;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbColor1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbLineStyle1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown udLineWidth1;
        private System.Windows.Forms.Label label15;
    }
}