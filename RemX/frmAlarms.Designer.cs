namespace RemXcs
{
    partial class frmAlarms
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
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.lvLogView = new BaseServer.ListViewEx();
            this.chKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSnapTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPosition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chParameter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSetPoint = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDescriptor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbBackward = new System.Windows.Forms.ToolStripButton();
            this.tsbForward = new System.Windows.Forms.ToolStripButton();
            this.tsbEnd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbQuitAll = new System.Windows.Forms.ToolStripButton();
            this.timerBlink = new System.Windows.Forms.Timer(this.components);
            this.backgroundFetch = new System.ComponentModel.BackgroundWorker();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.lvLogView);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(819, 243);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(819, 268);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // lvLogView
            // 
            this.lvLogView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chKey,
            this.chSnapTime,
            this.chStation,
            this.chPosition,
            this.chParameter,
            this.chValue,
            this.chSetPoint,
            this.chMessage,
            this.chDescriptor});
            this.lvLogView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLogView.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lvLogView.FullRowSelect = true;
            this.lvLogView.GridLines = true;
            this.lvLogView.HideSelection = false;
            this.lvLogView.Location = new System.Drawing.Point(0, 0);
            this.lvLogView.MultiSelect = false;
            this.lvLogView.Name = "lvLogView";
            this.lvLogView.ShowGroups = false;
            this.lvLogView.ShowItemToolTips = true;
            this.lvLogView.Size = new System.Drawing.Size(819, 243);
            this.lvLogView.TabIndex = 0;
            this.lvLogView.UseCompatibleStateImageBehavior = false;
            this.lvLogView.View = System.Windows.Forms.View.Details;
            this.lvLogView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvLogView_MouseDown);
            // 
            // chKey
            // 
            this.chKey.Text = "";
            this.chKey.Width = 30;
            // 
            // chSnapTime
            // 
            this.chSnapTime.Text = "Время";
            this.chSnapTime.Width = 140;
            // 
            // chStation
            // 
            this.chStation.Text = "Ст";
            this.chStation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chStation.Width = 30;
            // 
            // chPosition
            // 
            this.chPosition.Text = "Позиция";
            this.chPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chPosition.Width = 110;
            // 
            // chParameter
            // 
            this.chParameter.Text = "Параметр";
            this.chParameter.Width = 110;
            // 
            // chValue
            // 
            this.chValue.Text = "Значение";
            this.chValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chValue.Width = 110;
            // 
            // chSetPoint
            // 
            this.chSetPoint.Text = "Уставка";
            this.chSetPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chSetPoint.Width = 110;
            // 
            // chMessage
            // 
            this.chMessage.Text = "Сообщение";
            this.chMessage.Width = 260;
            // 
            // chDescriptor
            // 
            this.chDescriptor.Text = "Описание позиции";
            this.chDescriptor.Width = 300;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbBackward,
            this.tsbForward,
            this.tsbEnd,
            this.toolStripSeparator1,
            this.tsbQuitAll});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(352, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // tsbBackward
            // 
            this.tsbBackward.Image = global::RemXcs.Properties.Resources.left;
            this.tsbBackward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBackward.Name = "tsbBackward";
            this.tsbBackward.Size = new System.Drawing.Size(58, 22);
            this.tsbBackward.Text = "Назад";
            this.tsbBackward.Click += new System.EventHandler(this.tsbBackward_Click);
            // 
            // tsbForward
            // 
            this.tsbForward.Enabled = false;
            this.tsbForward.Image = global::RemXcs.Properties.Resources.right;
            this.tsbForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbForward.Name = "tsbForward";
            this.tsbForward.Size = new System.Drawing.Size(59, 22);
            this.tsbForward.Text = "Далее";
            this.tsbForward.Click += new System.EventHandler(this.tsbForward_Click);
            // 
            // tsbEnd
            // 
            this.tsbEnd.Enabled = false;
            this.tsbEnd.Image = global::RemXcs.Properties.Resources.rightend;
            this.tsbEnd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEnd.Name = "tsbEnd";
            this.tsbEnd.Size = new System.Drawing.Size(105, 22);
            this.tsbEnd.Text = "Конец журнала";
            this.tsbEnd.Click += new System.EventHandler(this.tsbEnd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbQuitAll
            // 
            this.tsbQuitAll.Image = global::RemXcs.Properties.Resources.switchlog;
            this.tsbQuitAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbQuitAll.Name = "tsbQuitAll";
            this.tsbQuitAll.Size = new System.Drawing.Size(114, 22);
            this.tsbQuitAll.Text = "Квитировать всё";
            this.tsbQuitAll.Click += new System.EventHandler(this.tsbQuitAll_Click);
            // 
            // timerBlink
            // 
            this.timerBlink.Interval = 500;
            this.timerBlink.Tick += new System.EventHandler(this.timerBlink_Tick);
            // 
            // backgroundFetch
            // 
            this.backgroundFetch.WorkerReportsProgress = true;
            this.backgroundFetch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundFetch_DoWork);
            this.backgroundFetch.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundFetch_ProgressChanged);
            // 
            // frmAlarms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 268);
            this.Controls.Add(this.toolStripContainer1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "frmAlarms";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Аварийные значения";
            this.Activated += new System.EventHandler(this.frmAlarms_Activated);
            this.Load += new System.EventHandler(this.frmAlarms_Load);
            this.Shown += new System.EventHandler(this.frmAlarms_Shown);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private BaseServer.ListViewEx lvLogView;
        private System.Windows.Forms.ColumnHeader chSnapTime;
        private System.Windows.Forms.ColumnHeader chStation;
        private System.Windows.Forms.ColumnHeader chPosition;
        private System.Windows.Forms.ColumnHeader chParameter;
        private System.Windows.Forms.ColumnHeader chValue;
        private System.Windows.Forms.ColumnHeader chSetPoint;
        private System.Windows.Forms.ColumnHeader chMessage;
        private System.Windows.Forms.ColumnHeader chDescriptor;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbBackward;
        private System.Windows.Forms.ToolStripButton tsbForward;
        private System.Windows.Forms.ToolStripButton tsbEnd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ColumnHeader chKey;
        private System.Windows.Forms.Timer timerBlink;
        private System.Windows.Forms.ToolStripButton tsbQuitAll;
        private System.ComponentModel.BackgroundWorker backgroundFetch;
    }
}