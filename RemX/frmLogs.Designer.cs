namespace RemXcs
{
    partial class frmLogs
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
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.lvLogView = new BaseServer.ListViewEx();
            this.chSnapTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPosition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chParameter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSetPoint = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDescriptor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbPreview = new System.Windows.Forms.ToolStripButton();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBackward = new System.Windows.Forms.ToolStripButton();
            this.tsbForward = new System.Windows.Forms.ToolStripButton();
            this.tsbEnd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAnchor = new System.Windows.Forms.ToolStripButton();
            this.printDoc = new System.Drawing.Printing.PrintDocument();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.saveCSVFileDialog = new System.Windows.Forms.SaveFileDialog();
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
            // 
            // chSnapTime
            // 
            this.chSnapTime.Text = "Дата и время";
            this.chSnapTime.Width = 180;
            // 
            // chStation
            // 
            this.chStation.Text = "Ст.";
            this.chStation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chStation.Width = 40;
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
            this.chMessage.Width = 300;
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
            this.tsbPrint,
            this.tsbPreview,
            this.tsbExport,
            this.toolStripSeparator2,
            this.tsbBackward,
            this.tsbForward,
            this.tsbEnd,
            this.toolStripSeparator1,
            this.tsbAnchor});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(541, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // tsbPrint
            // 
            this.tsbPrint.Image = global::RemXcs.Properties.Resources.print;
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(76, 22);
            this.tsbPrint.Text = "Печать...";
            this.tsbPrint.Click += new System.EventHandler(this.tsbPrint_Click);
            // 
            // tsbPreview
            // 
            this.tsbPreview.Image = global::RemXcs.Properties.Resources.preview;
            this.tsbPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPreview.Name = "tsbPreview";
            this.tsbPreview.Size = new System.Drawing.Size(75, 22);
            this.tsbPreview.Text = "Просмотр";
            this.tsbPreview.Click += new System.EventHandler(this.tsbPreview_Click);
            // 
            // tsbExport
            // 
            this.tsbExport.Image = global::RemXcs.Properties.Resources.exportxl;
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(69, 22);
            this.tsbExport.Text = "Экспорт";
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // tsbAnchor
            // 
            this.tsbAnchor.Image = global::RemXcs.Properties.Resources.anchor;
            this.tsbAnchor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAnchor.Name = "tsbAnchor";
            this.tsbAnchor.Size = new System.Drawing.Size(77, 22);
            this.tsbAnchor.Text = "Обновить";
            this.tsbAnchor.Click += new System.EventHandler(this.tsbAnchor_Click);
            // 
            // printDoc
            // 
            this.printDoc.DocumentName = "1";
            // 
            // printDialog
            // 
            this.printDialog.Document = this.printDoc;
            this.printDialog.UseEXDialog = true;
            // 
            // saveCSVFileDialog
            // 
            this.saveCSVFileDialog.DefaultExt = "csv";
            this.saveCSVFileDialog.Filter = "Файлы *.csv|*.csv";
            this.saveCSVFileDialog.InitialDirectory = ".";
            this.saveCSVFileDialog.Title = "Экспорт журнала в формат CSV";
            // 
            // frmLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 268);
            this.Controls.Add(this.toolStripContainer1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "frmLogs";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Архив аварийных значений";
            this.Activated += new System.EventHandler(this.frmLogs_Activated);
            this.Load += new System.EventHandler(this.frmLogs_Load);
            this.Shown += new System.EventHandler(this.frmLogs_Shown);
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
        private System.Windows.Forms.ToolStripButton tsbAnchor;
        private System.Windows.Forms.ToolStripButton tsbExport;
        private System.Windows.Forms.ToolStripButton tsbPreview;
        private System.Windows.Forms.ToolStripButton tsbPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Drawing.Printing.PrintDocument printDoc;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.SaveFileDialog saveCSVFileDialog;
    }
}