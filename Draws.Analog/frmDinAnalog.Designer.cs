namespace Draws.Analog
{
    partial class frmDinAnalog
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
            this.btnSelect = new System.Windows.Forms.Button();
            this.tbPtName = new System.Windows.Forms.TextBox();
            this.cbColor = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbFrameColor = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFramed = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.udHeight = new System.Windows.Forms.NumericUpDown();
            this.udTop = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.udWidth = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.udLeft = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbShowTag = new System.Windows.Forms.CheckBox();
            this.cbShowUnit = new System.Windows.Forms.CheckBox();
            this.cbShowValue = new System.Windows.Forms.CheckBox();
            this.cbShowPanel = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbBarLevelColor = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rbShowBar = new System.Windows.Forms.RadioButton();
            this.rbShowLevel = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbBarLevelInverse = new System.Windows.Forms.CheckBox();
            this.cbBarLevelVisible = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.tbTest = new System.Windows.Forms.TrackBar();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbTest = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLeft)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udFontSize)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTest)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelect);
            this.groupBox1.Controls.Add(this.tbPtName);
            this.groupBox1.Controls.Add(this.cbColor);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.groupBox8);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(459, 230);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Свойства";
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(349, 197);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 6;
            this.btnSelect.Text = "Выбор...";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // tbPtName
            // 
            this.tbPtName.Location = new System.Drawing.Point(333, 169);
            this.tbPtName.Name = "tbPtName";
            this.tbPtName.Size = new System.Drawing.Size(118, 22);
            this.tbPtName.TabIndex = 5;
            this.tbPtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPtName_KeyDown);
            // 
            // cbColor
            // 
            this.cbColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbColor.FormattingEnabled = true;
            this.cbColor.Location = new System.Drawing.Point(330, 114);
            this.cbColor.Name = "cbColor";
            this.cbColor.Size = new System.Drawing.Size(121, 23);
            this.cbColor.TabIndex = 4;
            this.cbColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbFrameColor_DrawItem);
            this.cbColor.SelectedIndexChanged += new System.EventHandler(this.cbFrameColor_SelectedIndexChanged);
            this.cbColor.SelectionChangeCommitted += new System.EventHandler(this.cbFrameColor_SelectionChangeCommitted);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbFrameColor);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cbFramed);
            this.groupBox3.Location = new System.Drawing.Point(6, 132);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(141, 89);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // cbFrameColor
            // 
            this.cbFrameColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFrameColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFrameColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbFrameColor.FormattingEnabled = true;
            this.cbFrameColor.Location = new System.Drawing.Point(7, 55);
            this.cbFrameColor.Name = "cbFrameColor";
            this.cbFrameColor.Size = new System.Drawing.Size(126, 23);
            this.cbFrameColor.TabIndex = 1;
            this.cbFrameColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbFrameColor_DrawItem);
            this.cbFrameColor.SelectedIndexChanged += new System.EventHandler(this.cbFrameColor_SelectedIndexChanged);
            this.cbFrameColor.SelectionChangeCommitted += new System.EventHandler(this.cbFrameColor_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Цвет рамки";
            // 
            // cbFramed
            // 
            this.cbFramed.AutoSize = true;
            this.cbFramed.Location = new System.Drawing.Point(6, 12);
            this.cbFramed.Name = "cbFramed";
            this.cbFramed.Size = new System.Drawing.Size(59, 17);
            this.cbFramed.TabIndex = 0;
            this.cbFramed.Text = "Рамка";
            this.cbFramed.UseVisualStyleBackColor = true;
            this.cbFramed.Click += new System.EventHandler(this.cbFramed_Click);
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
            this.groupBox7.Location = new System.Drawing.Point(153, 15);
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
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(330, 148);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 14);
            this.label10.TabIndex = 1;
            this.label10.Text = "Имя позиции:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(330, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 14);
            this.label7.TabIndex = 1;
            this.label7.Text = "Цвет фона:";
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
            this.groupBox8.Location = new System.Drawing.Point(153, 81);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(171, 140);
            this.groupBox8.TabIndex = 3;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbShowTag);
            this.groupBox2.Controls.Add(this.cbShowUnit);
            this.groupBox2.Controls.Add(this.cbShowValue);
            this.groupBox2.Controls.Add(this.cbShowPanel);
            this.groupBox2.Location = new System.Drawing.Point(6, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(141, 111);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // cbShowTag
            // 
            this.cbShowTag.AutoSize = true;
            this.cbShowTag.Location = new System.Drawing.Point(6, 84);
            this.cbShowTag.Name = "cbShowTag";
            this.cbShowTag.Size = new System.Drawing.Size(70, 17);
            this.cbShowTag.TabIndex = 3;
            this.cbShowTag.Text = "Позиция";
            this.cbShowTag.UseVisualStyleBackColor = true;
            this.cbShowTag.Click += new System.EventHandler(this.cbShowTag_Click);
            // 
            // cbShowUnit
            // 
            this.cbShowUnit.AutoSize = true;
            this.cbShowUnit.Location = new System.Drawing.Point(6, 60);
            this.cbShowUnit.Name = "cbShowUnit";
            this.cbShowUnit.Size = new System.Drawing.Size(94, 17);
            this.cbShowUnit.TabIndex = 2;
            this.cbShowUnit.Text = "Размерность";
            this.cbShowUnit.UseVisualStyleBackColor = true;
            this.cbShowUnit.Click += new System.EventHandler(this.cbShowUnit_Click);
            // 
            // cbShowValue
            // 
            this.cbShowValue.AutoSize = true;
            this.cbShowValue.Location = new System.Drawing.Point(6, 36);
            this.cbShowValue.Name = "cbShowValue";
            this.cbShowValue.Size = new System.Drawing.Size(74, 17);
            this.cbShowValue.TabIndex = 1;
            this.cbShowValue.Text = "Значение";
            this.cbShowValue.UseVisualStyleBackColor = true;
            this.cbShowValue.Click += new System.EventHandler(this.cbShowValue_Click);
            // 
            // cbShowPanel
            // 
            this.cbShowPanel.AutoSize = true;
            this.cbShowPanel.Location = new System.Drawing.Point(6, 12);
            this.cbShowPanel.Name = "cbShowPanel";
            this.cbShowPanel.Size = new System.Drawing.Size(64, 17);
            this.cbShowPanel.TabIndex = 0;
            this.cbShowPanel.Text = "Панель";
            this.cbShowPanel.UseVisualStyleBackColor = true;
            this.cbShowPanel.Click += new System.EventHandler(this.cbShowPanel_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbBarLevelColor);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Location = new System.Drawing.Point(13, 250);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(350, 84);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = " Указатель бара (уровня)";
            // 
            // cbBarLevelColor
            // 
            this.cbBarLevelColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbBarLevelColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBarLevelColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbBarLevelColor.FormattingEnabled = true;
            this.cbBarLevelColor.Location = new System.Drawing.Point(216, 52);
            this.cbBarLevelColor.Name = "cbBarLevelColor";
            this.cbBarLevelColor.Size = new System.Drawing.Size(121, 23);
            this.cbBarLevelColor.TabIndex = 2;
            this.cbBarLevelColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbFrameColor_DrawItem);
            this.cbBarLevelColor.SelectedIndexChanged += new System.EventHandler(this.cbFrameColor_SelectedIndexChanged);
            this.cbBarLevelColor.SelectionChangeCommitted += new System.EventHandler(this.cbFrameColor_SelectionChangeCommitted);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rbShowBar);
            this.groupBox6.Controls.Add(this.rbShowLevel);
            this.groupBox6.Location = new System.Drawing.Point(116, 16);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(94, 62);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            // 
            // rbShowBar
            // 
            this.rbShowBar.AutoSize = true;
            this.rbShowBar.Location = new System.Drawing.Point(6, 36);
            this.rbShowBar.Name = "rbShowBar";
            this.rbShowBar.Size = new System.Drawing.Size(44, 17);
            this.rbShowBar.TabIndex = 3;
            this.rbShowBar.TabStop = true;
            this.rbShowBar.Text = "Бар";
            this.rbShowBar.UseVisualStyleBackColor = true;
            this.rbShowBar.Click += new System.EventHandler(this.rbShowLevel_Click);
            // 
            // rbShowLevel
            // 
            this.rbShowLevel.AutoSize = true;
            this.rbShowLevel.Location = new System.Drawing.Point(6, 13);
            this.rbShowLevel.Name = "rbShowLevel";
            this.rbShowLevel.Size = new System.Drawing.Size(69, 17);
            this.rbShowLevel.TabIndex = 2;
            this.rbShowLevel.TabStop = true;
            this.rbShowLevel.Text = "Уровень";
            this.rbShowLevel.UseVisualStyleBackColor = true;
            this.rbShowLevel.Click += new System.EventHandler(this.rbShowLevel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Цвет бара (уровня)";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbBarLevelInverse);
            this.groupBox5.Controls.Add(this.cbBarLevelVisible);
            this.groupBox5.Location = new System.Drawing.Point(6, 15);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(104, 62);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            // 
            // cbBarLevelInverse
            // 
            this.cbBarLevelInverse.AutoSize = true;
            this.cbBarLevelInverse.Location = new System.Drawing.Point(6, 38);
            this.cbBarLevelInverse.Name = "cbBarLevelInverse";
            this.cbBarLevelInverse.Size = new System.Drawing.Size(76, 17);
            this.cbBarLevelInverse.TabIndex = 1;
            this.cbBarLevelInverse.Text = "Инверсия";
            this.cbBarLevelInverse.UseVisualStyleBackColor = true;
            this.cbBarLevelInverse.Click += new System.EventHandler(this.cbBarLevelInverse_Click);
            // 
            // cbBarLevelVisible
            // 
            this.cbBarLevelVisible.AutoSize = true;
            this.cbBarLevelVisible.Location = new System.Drawing.Point(6, 14);
            this.cbBarLevelVisible.Name = "cbBarLevelVisible";
            this.cbBarLevelVisible.Size = new System.Drawing.Size(73, 17);
            this.cbBarLevelVisible.TabIndex = 0;
            this.cbBarLevelVisible.Text = "Видимый";
            this.cbBarLevelVisible.UseVisualStyleBackColor = true;
            this.cbBarLevelVisible.Click += new System.EventHandler(this.cbBarLevelVisible_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Location = new System.Drawing.Point(389, 308);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "Закрыть";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // tbTest
            // 
            this.tbTest.AutoSize = false;
            this.tbTest.Location = new System.Drawing.Point(369, 266);
            this.tbTest.Maximum = 100;
            this.tbTest.Name = "tbTest";
            this.tbTest.Size = new System.Drawing.Size(104, 20);
            this.tbTest.TabIndex = 9;
            this.tbTest.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbTest.Scroll += new System.EventHandler(this.tbTest_Scroll);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(403, 249);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 14);
            this.label12.TabIndex = 1;
            this.label12.Text = "Тест";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(403, 282);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 14);
            this.label13.TabIndex = 1;
            this.label13.Text = "Тест";
            // 
            // lbTest
            // 
            this.lbTest.AutoSize = true;
            this.lbTest.Location = new System.Drawing.Point(403, 285);
            this.lbTest.Name = "lbTest";
            this.lbTest.Size = new System.Drawing.Size(30, 14);
            this.lbTest.TabIndex = 1;
            this.lbTest.Text = "0 %";
            // 
            // frmDinAnalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOk;
            this.ClientSize = new System.Drawing.Size(484, 343);
            this.Controls.Add(this.tbTest);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbTest);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDinAnalog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактор элемента \"Аналоговое значение\"";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDinAnalog_FormClosing);
            this.Load += new System.EventHandler(this.frmDinAnalog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLeft)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udFontSize)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTest)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbFrameColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbFramed;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbShowTag;
        private System.Windows.Forms.CheckBox cbShowUnit;
        private System.Windows.Forms.CheckBox cbShowValue;
        private System.Windows.Forms.CheckBox cbShowPanel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rbShowBar;
        private System.Windows.Forms.RadioButton rbShowLevel;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox cbBarLevelInverse;
        private System.Windows.Forms.CheckBox cbBarLevelVisible;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TextBox tbPtName;
        private System.Windows.Forms.ComboBox cbColor;
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox cbFontName;
        private System.Windows.Forms.CheckBox cbUnderline;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbItalic;
        private System.Windows.Forms.NumericUpDown udFontSize;
        private System.Windows.Forms.CheckBox cbBold;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbBarLevelColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox cbStrikeout;
        private System.Windows.Forms.ComboBox cbFontColor;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TrackBar tbTest;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbTest;

    }
}