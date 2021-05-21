namespace Points.KR500
{
    partial class frmGO
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
            this.components = new System.ComponentModel.Container();
            this.lbPtDesc = new System.Windows.Forms.Label();
            this.lbNode = new System.Windows.Forms.Label();
            this.lbPtType = new System.Windows.Forms.Label();
            this.lbChannel = new System.Windows.Forms.Label();
            this.lbActived = new System.Windows.Forms.Label();
            this.lbPtName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.timerFetch = new System.Windows.Forms.Timer(this.components);
            this.lbGroupFetchTime = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.backgroundFetch = new System.ComponentModel.BackgroundWorker();
            this.lbGroupFactTime = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lvList = new Points.Plugins.ListViewEx();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.lbFetchAnswer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbPtDesc
            // 
            this.lbPtDesc.AutoSize = true;
            this.lbPtDesc.ForeColor = System.Drawing.Color.Cyan;
            this.lbPtDesc.Location = new System.Drawing.Point(133, 9);
            this.lbPtDesc.Name = "lbPtDesc";
            this.lbPtDesc.Size = new System.Drawing.Size(56, 16);
            this.lbPtDesc.TabIndex = 43;
            this.lbPtDesc.Text = "PTDESC";
            // 
            // lbNode
            // 
            this.lbNode.AutoSize = true;
            this.lbNode.ForeColor = System.Drawing.Color.Cyan;
            this.lbNode.Location = new System.Drawing.Point(639, 378);
            this.lbNode.Name = "lbNode";
            this.lbNode.Size = new System.Drawing.Size(40, 16);
            this.lbNode.TabIndex = 49;
            this.lbNode.Text = "NODE";
            // 
            // lbPtType
            // 
            this.lbPtType.AutoSize = true;
            this.lbPtType.ForeColor = System.Drawing.Color.Cyan;
            this.lbPtType.Location = new System.Drawing.Point(639, 346);
            this.lbPtType.Name = "lbPtType";
            this.lbPtType.Size = new System.Drawing.Size(56, 16);
            this.lbPtType.TabIndex = 60;
            this.lbPtType.Text = "PTTYPE";
            // 
            // lbChannel
            // 
            this.lbChannel.AutoSize = true;
            this.lbChannel.ForeColor = System.Drawing.Color.Cyan;
            this.lbChannel.Location = new System.Drawing.Point(639, 362);
            this.lbChannel.Name = "lbChannel";
            this.lbChannel.Size = new System.Drawing.Size(64, 16);
            this.lbChannel.TabIndex = 59;
            this.lbChannel.Text = "CHANNEL";
            // 
            // lbActived
            // 
            this.lbActived.AutoEllipsis = true;
            this.lbActived.ForeColor = System.Drawing.Color.Cyan;
            this.lbActived.Location = new System.Drawing.Point(639, 69);
            this.lbActived.Name = "lbActived";
            this.lbActived.Size = new System.Drawing.Size(92, 16);
            this.lbActived.TabIndex = 53;
            this.lbActived.Text = "Нет";
            // 
            // lbPtName
            // 
            this.lbPtName.AutoSize = true;
            this.lbPtName.ForeColor = System.Drawing.Color.Cyan;
            this.lbPtName.Location = new System.Drawing.Point(10, 9);
            this.lbPtName.Name = "lbPtName";
            this.lbPtName.Size = new System.Drawing.Size(56, 16);
            this.lbPtName.TabIndex = 55;
            this.lbPtName.Text = "PTNAME";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(553, 346);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "Тип точки";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(545, 378);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(88, 16);
            this.label21.TabIndex = 9;
            this.label21.Text = "Контроллер";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(585, 362);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(48, 16);
            this.label20.TabIndex = 8;
            this.label20.Text = "Канал";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(545, 69);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(96, 16);
            this.label31.TabIndex = 22;
            this.label31.Text = "Опрос точки";
            this.label31.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(12, 346);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(64, 16);
            this.label22.TabIndex = 21;
            this.label22.Text = "Статус:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.ForeColor = System.Drawing.Color.Lime;
            this.label15.Location = new System.Drawing.Point(552, 41);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(104, 16);
            this.label15.TabIndex = 6;
            this.label15.Text = "Данные точки";
            // 
            // timerFetch
            // 
            this.timerFetch.Interval = 500;
            this.timerFetch.Tick += new System.EventHandler(this.timerFetch_Tick);
            // 
            // lbGroupFetchTime
            // 
            this.lbGroupFetchTime.AutoEllipsis = true;
            this.lbGroupFetchTime.ForeColor = System.Drawing.Color.Cyan;
            this.lbGroupFetchTime.Location = new System.Drawing.Point(639, 101);
            this.lbGroupFetchTime.Name = "lbGroupFetchTime";
            this.lbGroupFetchTime.Size = new System.Drawing.Size(88, 16);
            this.lbGroupFetchTime.TabIndex = 73;
            this.lbGroupFetchTime.Text = "1";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(545, 101);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(96, 16);
            this.label37.TabIndex = 71;
            this.label37.Text = "Опрос (сек)";
            this.label37.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // backgroundFetch
            // 
            this.backgroundFetch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundFetch_DoWork);
            this.backgroundFetch.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundFetch_RunWorkerCompleted);
            // 
            // lbGroupFactTime
            // 
            this.lbGroupFactTime.AutoEllipsis = true;
            this.lbGroupFactTime.ForeColor = System.Drawing.Color.Cyan;
            this.lbGroupFactTime.Location = new System.Drawing.Point(639, 117);
            this.lbGroupFactTime.Name = "lbGroupFactTime";
            this.lbGroupFactTime.Size = new System.Drawing.Size(88, 16);
            this.lbGroupFactTime.TabIndex = 75;
            this.lbGroupFactTime.Text = "нет опроса";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(506, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 16);
            this.label5.TabIndex = 74;
            this.label5.Text = "Фактически (сек)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lvList
            // 
            this.lvList.BackColor = System.Drawing.SystemColors.WindowText;
            this.lvList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvList.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lvList.ForeColor = System.Drawing.Color.Silver;
            this.lvList.FullRowSelect = true;
            this.lvList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvList.Location = new System.Drawing.Point(13, 69);
            this.lvList.MultiSelect = false;
            this.lvList.Name = "lvList";
            this.lvList.ShowItemToolTips = true;
            this.lvList.Size = new System.Drawing.Size(473, 274);
            this.lvList.TabIndex = 78;
            this.lvList.UseCompatibleStateImageBehavior = false;
            this.lvList.View = System.Windows.Forms.View.Details;
            this.lvList.DoubleClick += new System.EventHandler(this.lvList_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 30;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            this.columnHeader2.Width = 90;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "";
            this.columnHeader3.Width = 270;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(133, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Список опрашиваемых точек";
            // 
            // lbFetchAnswer
            // 
            this.lbFetchAnswer.ForeColor = System.Drawing.SystemColors.Window;
            this.lbFetchAnswer.Location = new System.Drawing.Point(20, 365);
            this.lbFetchAnswer.Name = "lbFetchAnswer";
            this.lbFetchAnswer.Size = new System.Drawing.Size(519, 51);
            this.lbFetchAnswer.TabIndex = 79;
            // 
            // frmGO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(730, 425);
            this.Controls.Add(this.lbFetchAnswer);
            this.Controls.Add(this.lvList);
            this.Controls.Add(this.lbGroupFactTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbGroupFetchTime);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbPtDesc);
            this.Controls.Add(this.lbNode);
            this.Controls.Add(this.lbPtType);
            this.Controls.Add(this.lbChannel);
            this.Controls.Add(this.lbActived);
            this.Controls.Add(this.lbPtName);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label15);
            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Silver;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGO";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Паспорт значения";
            this.Load += new System.EventHandler(this.frmFD_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDI_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbPtDesc;
        private System.Windows.Forms.Label lbNode;
        private System.Windows.Forms.Label lbPtType;
        private System.Windows.Forms.Label lbChannel;
        private System.Windows.Forms.Label lbActived;
        private System.Windows.Forms.Label lbPtName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Timer timerFetch;
        private System.Windows.Forms.Label lbGroupFetchTime;
        private System.Windows.Forms.Label label37;
        private System.ComponentModel.BackgroundWorker backgroundFetch;
        private System.Windows.Forms.Label lbGroupFactTime;
        private System.Windows.Forms.Label label5;
        private Points.Plugins.ListViewEx lvList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbFetchAnswer;

    }
}