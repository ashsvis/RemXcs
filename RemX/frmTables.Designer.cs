namespace RemXcs
{
    partial class frmTables
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTables));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.lvTableView = new BaseServer.ListViewEx();
            this.chDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbPreview = new System.Windows.Forms.ToolStripButton();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBackward = new System.Windows.Forms.ToolStripButton();
            this.tsbForward = new System.Windows.Forms.ToolStripButton();
            this.tsbEnd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAnchor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbMinutes = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHours = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDailys = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbMonths = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPrevGroup = new System.Windows.Forms.ToolStripButton();
            this.tsbGroupSelect = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbNextGroup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.lbGroupCaption = new System.Windows.Forms.ToolStripLabel();
            this.printDoc = new System.Drawing.Printing.PrintDocument();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.headerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiChange = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCSVFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.headerMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.lvTableView);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(847, 233);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(847, 283);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
            // 
            // lvTableView
            // 
            this.lvTableView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDateTime});
            this.lvTableView.ContextMenuStrip = this.listMenu;
            this.lvTableView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTableView.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lvTableView.FullRowSelect = true;
            this.lvTableView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTableView.HideSelection = false;
            this.lvTableView.LabelWrap = false;
            this.lvTableView.Location = new System.Drawing.Point(0, 0);
            this.lvTableView.MultiSelect = false;
            this.lvTableView.Name = "lvTableView";
            this.lvTableView.Size = new System.Drawing.Size(847, 233);
            this.lvTableView.TabIndex = 2;
            this.lvTableView.UseCompatibleStateImageBehavior = false;
            this.lvTableView.View = System.Windows.Forms.View.Details;
            this.lvTableView.Resize += new System.EventHandler(this.frmTables_ResizeEnd);
            // 
            // chDateTime
            // 
            this.chDateTime.Text = "Дата и время";
            this.chDateTime.Width = 132;
            // 
            // listMenu
            // 
            this.listMenu.Name = "cmsGroupItemChange";
            this.listMenu.Size = new System.Drawing.Size(61, 4);
            this.listMenu.Opening += new System.ComponentModel.CancelEventHandler(this.cmsGroupItemChange_Opening);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPrint,
            this.tsbPreview,
            this.tsbExport,
            this.toolStripSeparator1,
            this.tsbBackward,
            this.tsbForward,
            this.tsbEnd,
            this.toolStripSeparator6,
            this.tsbAnchor,
            this.toolStripSeparator7});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(847, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            // 
            // tsbPrint
            // 
            this.tsbPrint.Image = global::RemXcs.Properties.Resources.print;
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(75, 22);
            this.tsbPrint.Text = "Печать...";
            this.tsbPrint.Click += new System.EventHandler(this.tsbPrint_Click);
            // 
            // tsbPreview
            // 
            this.tsbPreview.Image = global::RemXcs.Properties.Resources.preview;
            this.tsbPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPreview.Name = "tsbPreview";
            this.tsbPreview.Size = new System.Drawing.Size(84, 22);
            this.tsbPreview.Text = "Просмотр";
            this.tsbPreview.Click += new System.EventHandler(this.tsbPreview_Click);
            // 
            // tsbExport
            // 
            this.tsbExport.Image = global::RemXcs.Properties.Resources.exportxl;
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(72, 22);
            this.tsbExport.Text = "Экспорт";
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbBackward
            // 
            this.tsbBackward.Image = global::RemXcs.Properties.Resources.left;
            this.tsbBackward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBackward.Name = "tsbBackward";
            this.tsbBackward.Size = new System.Drawing.Size(59, 22);
            this.tsbBackward.Text = "Назад";
            this.tsbBackward.Click += new System.EventHandler(this.tsbBackward_Click);
            // 
            // tsbForward
            // 
            this.tsbForward.Enabled = false;
            this.tsbForward.Image = global::RemXcs.Properties.Resources.right;
            this.tsbForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbForward.Name = "tsbForward";
            this.tsbForward.Size = new System.Drawing.Size(60, 22);
            this.tsbForward.Text = "Далее";
            this.tsbForward.Click += new System.EventHandler(this.tsbForward_Click);
            // 
            // tsbEnd
            // 
            this.tsbEnd.Enabled = false;
            this.tsbEnd.Image = global::RemXcs.Properties.Resources.rightend;
            this.tsbEnd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEnd.Name = "tsbEnd";
            this.tsbEnd.Size = new System.Drawing.Size(112, 22);
            this.tsbEnd.Text = "Конец таблицы";
            this.tsbEnd.Click += new System.EventHandler(this.tsbEnd_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbAnchor
            // 
            this.tsbAnchor.AutoSize = false;
            this.tsbAnchor.Image = global::RemXcs.Properties.Resources.on;
            this.tsbAnchor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAnchor.Name = "tsbAnchor";
            this.tsbAnchor.Size = new System.Drawing.Size(99, 22);
            this.tsbAnchor.Text = "Отслеживать";
            this.tsbAnchor.ToolTipText = "Отслеживать последние значения";
            this.tsbAnchor.Click += new System.EventHandler(this.tsbAnchor_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMinutes,
            this.toolStripSeparator8,
            this.tsbHours,
            this.toolStripSeparator4,
            this.tsbDailys,
            this.toolStripSeparator5,
            this.tsbMonths,
            this.toolStripSeparator3,
            this.tsbPrevGroup,
            this.tsbGroupSelect,
            this.tsbNextGroup,
            this.toolStripSeparator9,
            this.lbGroupCaption});
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(847, 25);
            this.toolStrip2.Stretch = true;
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbMinutes
            // 
            this.tsbMinutes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbMinutes.Image = ((System.Drawing.Image)(resources.GetObject("tsbMinutes.Image")));
            this.tsbMinutes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMinutes.Name = "tsbMinutes";
            this.tsbMinutes.Size = new System.Drawing.Size(56, 22);
            this.tsbMinutes.Text = "Минуты";
            this.tsbMinutes.ToolTipText = "Архив минутных значений";
            this.tsbMinutes.Click += new System.EventHandler(this.tsbMinutes_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbHours
            // 
            this.tsbHours.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbHours.Image = ((System.Drawing.Image)(resources.GetObject("tsbHours.Image")));
            this.tsbHours.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHours.Name = "tsbHours";
            this.tsbHours.Size = new System.Drawing.Size(40, 22);
            this.tsbHours.Text = "Часы";
            this.tsbHours.ToolTipText = "Архив часовых значений";
            this.tsbHours.Click += new System.EventHandler(this.tsbHours_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbDailys
            // 
            this.tsbDailys.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDailys.Image = ((System.Drawing.Image)(resources.GetObject("tsbDailys.Image")));
            this.tsbDailys.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDailys.Name = "tsbDailys";
            this.tsbDailys.Size = new System.Drawing.Size(43, 22);
            this.tsbDailys.Text = "Сутки";
            this.tsbDailys.ToolTipText = "Архив суточных значений";
            this.tsbDailys.Click += new System.EventHandler(this.tsbDailys_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbMonths
            // 
            this.tsbMonths.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbMonths.Image = ((System.Drawing.Image)(resources.GetObject("tsbMonths.Image")));
            this.tsbMonths.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMonths.Name = "tsbMonths";
            this.tsbMonths.Size = new System.Drawing.Size(56, 22);
            this.tsbMonths.Text = "Месяцы";
            this.tsbMonths.ToolTipText = "Архив месячных значений";
            this.tsbMonths.Click += new System.EventHandler(this.tsbMonths_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPrevGroup
            // 
            this.tsbPrevGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrevGroup.Enabled = false;
            this.tsbPrevGroup.Image = global::RemXcs.Properties.Resources.left;
            this.tsbPrevGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrevGroup.Name = "tsbPrevGroup";
            this.tsbPrevGroup.Size = new System.Drawing.Size(23, 22);
            this.tsbPrevGroup.Text = "Предыдущая группа";
            this.tsbPrevGroup.Click += new System.EventHandler(this.tsbPrevGroup_Click);
            // 
            // tsbGroupSelect
            // 
            this.tsbGroupSelect.Image = ((System.Drawing.Image)(resources.GetObject("tsbGroupSelect.Image")));
            this.tsbGroupSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGroupSelect.Name = "tsbGroupSelect";
            this.tsbGroupSelect.Size = new System.Drawing.Size(84, 22);
            this.tsbGroupSelect.Text = "Группа 1";
            // 
            // tsbNextGroup
            // 
            this.tsbNextGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNextGroup.Enabled = false;
            this.tsbNextGroup.Image = global::RemXcs.Properties.Resources.right;
            this.tsbNextGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNextGroup.Name = "tsbNextGroup";
            this.tsbNextGroup.Size = new System.Drawing.Size(23, 22);
            this.tsbNextGroup.Text = "Следующая группа";
            this.tsbNextGroup.Click += new System.EventHandler(this.tsbNextGroup_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // lbGroupCaption
            // 
            this.lbGroupCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbGroupCaption.Name = "lbGroupCaption";
            this.lbGroupCaption.Size = new System.Drawing.Size(49, 22);
            this.lbGroupCaption.Text = "Группа:";
            // 
            // printDoc
            // 
            this.printDoc.DocumentName = "1";
            this.printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDoc_PrintPage);
            this.printDoc.QueryPageSettings += new System.Drawing.Printing.QueryPageSettingsEventHandler(this.printDoc_QueryPageSettings);
            // 
            // printDialog
            // 
            this.printDialog.Document = this.printDoc;
            this.printDialog.UseEXDialog = true;
            // 
            // headerMenu
            // 
            this.headerMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiChange,
            this.tsmiDelete});
            this.headerMenu.Name = "headerMenu";
            this.headerMenu.Size = new System.Drawing.Size(135, 48);
            // 
            // tsmiChange
            // 
            this.tsmiChange.Name = "tsmiChange";
            this.tsmiChange.Size = new System.Drawing.Size(134, 22);
            this.tsmiChange.Text = "Изменить...";
            this.tsmiChange.Click += new System.EventHandler(this.tsmiChangeItem_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(134, 22);
            this.tsmiDelete.Text = "Удалить";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDeleteItem_Click);
            // 
            // saveCSVFileDialog
            // 
            this.saveCSVFileDialog.DefaultExt = "csv";
            this.saveCSVFileDialog.Filter = "Файлы *.csv|*.csv";
            this.saveCSVFileDialog.InitialDirectory = ".";
            this.saveCSVFileDialog.Title = "Экспорт журнала в формат CSV";
            // 
            // frmTables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 283);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "frmTables";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Таблицы";
            this.Load += new System.EventHandler(this.frmTables_Load);
            this.Shown += new System.EventHandler(this.frmTables_Shown);
            this.ResizeEnd += new System.EventHandler(this.frmTables_ResizeEnd);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.headerMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbPrint;
        private System.Windows.Forms.ToolStripButton tsbPreview;
        private System.Windows.Forms.ToolStripButton tsbExport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbBackward;
        private System.Windows.Forms.ToolStripButton tsbForward;
        private System.Windows.Forms.ToolStripButton tsbEnd;
        private System.Windows.Forms.ToolStripButton tsbAnchor;
        private BaseServer.ListViewEx lvTableView;
        private System.Windows.Forms.ColumnHeader chDateTime;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbMinutes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbHours;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbDailys;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbMonths;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripLabel lbGroupCaption;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        public System.Drawing.Printing.PrintDocument printDoc;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.ContextMenuStrip listMenu;
        private System.Windows.Forms.ToolStripButton tsbPrevGroup;
        private System.Windows.Forms.ToolStripDropDownButton tsbGroupSelect;
        private System.Windows.Forms.ToolStripButton tsbNextGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ContextMenuStrip headerMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiChange;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.SaveFileDialog saveCSVFileDialog;
    }
}