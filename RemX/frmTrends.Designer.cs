namespace RemXcs
{
    partial class frmTrends
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrends));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pbTrendView = new System.Windows.Forms.PictureBox();
            this.cbInput = new System.Windows.Forms.TextBox();
            this.dtpOffset = new System.Windows.Forms.DateTimePicker();
            this.lvTrends = new BaseServer.ListViewEx();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listMenu = new System.Windows.Forms.ContextMenuStrip();
            this.tsmiChangeTag = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEmptyTag = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChangeColor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiResetColor = new System.Windows.Forms.ToolStripMenuItem();
            this.ilColors = new System.Windows.Forms.ImageList();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbPreview = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPrevTime = new System.Windows.Forms.ToolStripButton();
            this.tsbTimeSelect = new System.Windows.Forms.ToolStripDropDownButton();
            this.mi20minutes = new System.Windows.Forms.ToolStripMenuItem();
            this.mi1hour = new System.Windows.Forms.ToolStripMenuItem();
            this.mi2hours = new System.Windows.Forms.ToolStripMenuItem();
            this.mi4hours = new System.Windows.Forms.ToolStripMenuItem();
            this.mi8hours = new System.Windows.Forms.ToolStripMenuItem();
            this.mi12hours = new System.Windows.Forms.ToolStripMenuItem();
            this.mi24hours = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbNextTime = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPrevGroup = new System.Windows.Forms.ToolStripButton();
            this.tsbGroupSelect = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbNextGroup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAnchor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.timerUpdateList = new System.Windows.Forms.Timer();
            this.backDrawTrends = new System.ComponentModel.BackgroundWorker();
            this.printDoc = new System.Drawing.Printing.PrintDocument();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTrendView)).BeginInit();
            this.listMenu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(926, 281);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(926, 306);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pbTrendView);
            this.splitContainer1.Panel1MinSize = 100;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cbInput);
            this.splitContainer1.Panel2.Controls.Add(this.dtpOffset);
            this.splitContainer1.Panel2.Controls.Add(this.lvTrends);
            this.splitContainer1.Panel2MinSize = 46;
            this.splitContainer1.Size = new System.Drawing.Size(926, 281);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_MouseUp);
            // 
            // pbTrendView
            // 
            this.pbTrendView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbTrendView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbTrendView.Location = new System.Drawing.Point(0, 0);
            this.pbTrendView.Name = "pbTrendView";
            this.pbTrendView.Size = new System.Drawing.Size(926, 100);
            this.pbTrendView.TabIndex = 0;
            this.pbTrendView.TabStop = false;
            this.pbTrendView.Paint += new System.Windows.Forms.PaintEventHandler(this.pbTrendView_Paint);
            this.pbTrendView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbTrendView_MouseDown);
            this.pbTrendView.MouseEnter += new System.EventHandler(this.pbTrendView_MouseEnter);
            this.pbTrendView.MouseLeave += new System.EventHandler(this.pbTrendView_MouseLeave);
            this.pbTrendView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbTrendView_MouseMove);
            this.pbTrendView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbTrendView_MouseUp);
            this.pbTrendView.Resize += new System.EventHandler(this.pbTrendView_Resize);
            // 
            // cbInput
            // 
            this.cbInput.Location = new System.Drawing.Point(465, 32);
            this.cbInput.Name = "cbInput";
            this.cbInput.Size = new System.Drawing.Size(78, 22);
            this.cbInput.TabIndex = 2;
            this.cbInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cbInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbInput_KeyDown);
            this.cbInput.Leave += new System.EventHandler(this.cbInput_Leave);
            // 
            // dtpOffset
            // 
            this.dtpOffset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpOffset.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpOffset.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpOffset.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOffset.Location = new System.Drawing.Point(772, 0);
            this.dtpOffset.MinDate = new System.DateTime(2011, 9, 1, 0, 0, 0, 0);
            this.dtpOffset.Name = "dtpOffset";
            this.dtpOffset.ShowUpDown = true;
            this.dtpOffset.Size = new System.Drawing.Size(137, 21);
            this.dtpOffset.TabIndex = 1;
            this.dtpOffset.ValueChanged += new System.EventHandler(this.dtpOffset_ValueChanged);
            // 
            // lvTrends
            // 
            this.lvTrends.CheckBoxes = true;
            this.lvTrends.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lvTrends.ContextMenuStrip = this.listMenu;
            this.lvTrends.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTrends.FullRowSelect = true;
            this.lvTrends.GridLines = true;
            this.lvTrends.HideSelection = false;
            this.lvTrends.LabelWrap = false;
            this.lvTrends.Location = new System.Drawing.Point(0, 0);
            this.lvTrends.MultiSelect = false;
            this.lvTrends.Name = "lvTrends";
            this.lvTrends.ShowItemToolTips = true;
            this.lvTrends.Size = new System.Drawing.Size(926, 177);
            this.lvTrends.SmallImageList = this.ilColors;
            this.lvTrends.TabIndex = 0;
            this.lvTrends.UseCompatibleStateImageBehavior = false;
            this.lvTrends.View = System.Windows.Forms.View.Details;
            this.lvTrends.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvTrends_ItemChecked);
            this.lvTrends.SelectedIndexChanged += new System.EventHandler(this.lvTrends_SelectedIndexChanged);
            this.lvTrends.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvTrends_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Позиция";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Текущее значение";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Ед.изм.";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Шкала низ";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader5.Width = 80;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Шкала верх";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader6.Width = 100;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Дескриптор позиции";
            this.columnHeader7.Width = 300;
            // 
            // listMenu
            // 
            this.listMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiChangeTag,
            this.tsmiEmptyTag,
            this.tsmiChangeColor,
            this.tsmiResetColor});
            this.listMenu.Name = "listMenu";
            this.listMenu.Size = new System.Drawing.Size(165, 92);
            this.listMenu.Opening += new System.ComponentModel.CancelEventHandler(this.listMenu_Opening);
            // 
            // tsmiChangeTag
            // 
            this.tsmiChangeTag.Name = "tsmiChangeTag";
            this.tsmiChangeTag.Size = new System.Drawing.Size(164, 22);
            this.tsmiChangeTag.Text = "Изменить...";
            this.tsmiChangeTag.Click += new System.EventHandler(this.tsmiChangeTag_Click);
            // 
            // tsmiEmptyTag
            // 
            this.tsmiEmptyTag.Name = "tsmiEmptyTag";
            this.tsmiEmptyTag.Size = new System.Drawing.Size(164, 22);
            this.tsmiEmptyTag.Text = "Очистить";
            this.tsmiEmptyTag.Click += new System.EventHandler(this.tsmiEmptyTag_Click);
            // 
            // tsmiChangeColor
            // 
            this.tsmiChangeColor.Name = "tsmiChangeColor";
            this.tsmiChangeColor.Size = new System.Drawing.Size(164, 22);
            this.tsmiChangeColor.Text = "Изменить цвет...";
            this.tsmiChangeColor.Click += new System.EventHandler(this.tsmiChangeColor_Click);
            // 
            // tsmiResetColor
            // 
            this.tsmiResetColor.Name = "tsmiResetColor";
            this.tsmiResetColor.Size = new System.Drawing.Size(164, 22);
            this.tsmiResetColor.Text = "Вернуть цвет";
            this.tsmiResetColor.Click += new System.EventHandler(this.tsmiResetColor_Click);
            // 
            // ilColors
            // 
            this.ilColors.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilColors.ImageStream")));
            this.ilColors.TransparentColor = System.Drawing.Color.Transparent;
            this.ilColors.Images.SetKeyName(0, "black.png");
            this.ilColors.Images.SetKeyName(1, "blue.png");
            this.ilColors.Images.SetKeyName(2, "red.png");
            this.ilColors.Images.SetKeyName(3, "green.png");
            this.ilColors.Images.SetKeyName(4, "cyan.png");
            this.ilColors.Images.SetKeyName(5, "magenta.png");
            this.ilColors.Images.SetKeyName(6, "violet.png");
            this.ilColors.Images.SetKeyName(7, "yellow.png");
            this.ilColors.Images.SetKeyName(8, "grey.png");
            this.ilColors.Images.SetKeyName(9, "olive.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPrint,
            this.tsbPreview,
            this.toolStripSeparator1,
            this.tsbPrevTime,
            this.tsbTimeSelect,
            this.tsbNextTime,
            this.toolStripSeparator2,
            this.tsbPrevGroup,
            this.tsbGroupSelect,
            this.tsbNextGroup,
            this.toolStripSeparator3,
            this.tsbAnchor,
            this.toolStripSeparator4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(926, 25);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPrevTime
            // 
            this.tsbPrevTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrevTime.Image = global::RemXcs.Properties.Resources.left;
            this.tsbPrevTime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrevTime.Name = "tsbPrevTime";
            this.tsbPrevTime.Size = new System.Drawing.Size(23, 22);
            this.tsbPrevTime.Text = "Предыдущий период";
            this.tsbPrevTime.Click += new System.EventHandler(this.tsbPrevTime_Click);
            // 
            // tsbTimeSelect
            // 
            this.tsbTimeSelect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi20minutes,
            this.mi1hour,
            this.mi2hours,
            this.mi4hours,
            this.mi8hours,
            this.mi12hours,
            this.mi24hours});
            this.tsbTimeSelect.Image = global::RemXcs.Properties.Resources.clock;
            this.tsbTimeSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTimeSelect.Name = "tsbTimeSelect";
            this.tsbTimeSelect.Size = new System.Drawing.Size(85, 22);
            this.tsbTimeSelect.Text = "20 минут";
            // 
            // mi20minutes
            // 
            this.mi20minutes.Name = "mi20minutes";
            this.mi20minutes.Size = new System.Drawing.Size(123, 22);
            this.mi20minutes.Tag = "20";
            this.mi20minutes.Text = "20 минут";
            this.mi20minutes.Click += new System.EventHandler(this.miTimeSelect_Click);
            // 
            // mi1hour
            // 
            this.mi1hour.Name = "mi1hour";
            this.mi1hour.Size = new System.Drawing.Size(123, 22);
            this.mi1hour.Tag = "60";
            this.mi1hour.Text = "1 час";
            this.mi1hour.Click += new System.EventHandler(this.miTimeSelect_Click);
            // 
            // mi2hours
            // 
            this.mi2hours.Name = "mi2hours";
            this.mi2hours.Size = new System.Drawing.Size(123, 22);
            this.mi2hours.Tag = "120";
            this.mi2hours.Text = "2 часа";
            this.mi2hours.Click += new System.EventHandler(this.miTimeSelect_Click);
            // 
            // mi4hours
            // 
            this.mi4hours.Name = "mi4hours";
            this.mi4hours.Size = new System.Drawing.Size(123, 22);
            this.mi4hours.Tag = "240";
            this.mi4hours.Text = "4 часа";
            this.mi4hours.Click += new System.EventHandler(this.miTimeSelect_Click);
            // 
            // mi8hours
            // 
            this.mi8hours.Name = "mi8hours";
            this.mi8hours.Size = new System.Drawing.Size(123, 22);
            this.mi8hours.Tag = "480";
            this.mi8hours.Text = "8 часов";
            this.mi8hours.Click += new System.EventHandler(this.miTimeSelect_Click);
            // 
            // mi12hours
            // 
            this.mi12hours.Name = "mi12hours";
            this.mi12hours.Size = new System.Drawing.Size(123, 22);
            this.mi12hours.Tag = "720";
            this.mi12hours.Text = "12 часов";
            this.mi12hours.Click += new System.EventHandler(this.miTimeSelect_Click);
            // 
            // mi24hours
            // 
            this.mi24hours.Name = "mi24hours";
            this.mi24hours.Size = new System.Drawing.Size(123, 22);
            this.mi24hours.Tag = "1440";
            this.mi24hours.Text = "24 часа";
            this.mi24hours.Click += new System.EventHandler(this.miTimeSelect_Click);
            // 
            // tsbNextTime
            // 
            this.tsbNextTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNextTime.Enabled = false;
            this.tsbNextTime.Image = global::RemXcs.Properties.Resources.right;
            this.tsbNextTime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNextTime.Name = "tsbNextTime";
            this.tsbNextTime.Size = new System.Drawing.Size(23, 22);
            this.tsbNextTime.Text = "Следующий период";
            this.tsbNextTime.Click += new System.EventHandler(this.tsbNextTime_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            this.tsbGroupSelect.Image = global::RemXcs.Properties.Resources.archives;
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbAnchor
            // 
            this.tsbAnchor.AutoSize = false;
            this.tsbAnchor.Image = global::RemXcs.Properties.Resources.on;
            this.tsbAnchor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAnchor.Name = "tsbAnchor";
            this.tsbAnchor.Size = new System.Drawing.Size(99, 22);
            this.tsbAnchor.Text = "Отслеживать";
            this.tsbAnchor.ToolTipText = "Отслеживать последние значения трендов";
            this.tsbAnchor.Click += new System.EventHandler(this.tsbAnchor_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // timerUpdateList
            // 
            this.timerUpdateList.Enabled = true;
            this.timerUpdateList.Interval = 1000;
            this.timerUpdateList.Tick += new System.EventHandler(this.timerUpdateList_Tick);
            // 
            // backDrawTrends
            // 
            this.backDrawTrends.WorkerReportsProgress = true;
            this.backDrawTrends.WorkerSupportsCancellation = true;
            this.backDrawTrends.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backDrawTrends_DoWork);
            this.backDrawTrends.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backDrawTrends_ProgressChanged);
            this.backDrawTrends.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backDrawTrends_RunWorkerCompleted);
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
            // frmTrends
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 306);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.KeyPreview = true;
            this.Name = "frmTrends";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Просмотр графиков";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTrends_FormClosing);
            this.Load += new System.EventHandler(this.frmTrends_Load);
            this.Shown += new System.EventHandler(this.frmTrends_Shown);
            this.Enter += new System.EventHandler(this.frmTrends_Enter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTrends_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmTrends_KeyUp);
            this.Leave += new System.EventHandler(this.frmTrends_Leave);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbTrendView)).EndInit();
            this.listMenu.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pbTrendView;
        private BaseServer.ListViewEx lvTrends;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ImageList ilColors;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbPrint;
        private System.Windows.Forms.ToolStripButton tsbPreview;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbPrevTime;
        private System.Windows.Forms.ToolStripDropDownButton tsbTimeSelect;
        private System.Windows.Forms.ToolStripMenuItem mi20minutes;
        private System.Windows.Forms.ToolStripMenuItem mi1hour;
        private System.Windows.Forms.ToolStripMenuItem mi2hours;
        private System.Windows.Forms.ToolStripMenuItem mi4hours;
        private System.Windows.Forms.ToolStripMenuItem mi8hours;
        private System.Windows.Forms.ToolStripMenuItem mi12hours;
        private System.Windows.Forms.ToolStripMenuItem mi24hours;
        private System.Windows.Forms.ToolStripButton tsbNextTime;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbAnchor;
        private System.Windows.Forms.ToolStripButton tsbPrevGroup;
        private System.Windows.Forms.ToolStripDropDownButton tsbGroupSelect;
        private System.Windows.Forms.ToolStripButton tsbNextGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Timer timerUpdateList;
        private System.ComponentModel.BackgroundWorker backDrawTrends;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        public System.Drawing.Printing.PrintDocument printDoc;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ContextMenuStrip listMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiChangeTag;
        private System.Windows.Forms.ToolStripMenuItem tsmiEmptyTag;
        private System.Windows.Forms.DateTimePicker dtpOffset;
        private System.Windows.Forms.ToolStripMenuItem tsmiChangeColor;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ToolStripMenuItem tsmiResetColor;
        private System.Windows.Forms.TextBox cbInput;
    }
}