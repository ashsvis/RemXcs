namespace DataEditor
{
    partial class frmBaseEditor
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
                opc.Dispose();
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
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Загрузка...");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBaseEditor));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tvTechnology = new System.Windows.Forms.TreeView();
            this.tvEntity = new System.Windows.Forms.TreeView();
            this.ilTree = new System.Windows.Forms.ImageList(this.components);
            this.listContainer = new System.Windows.Forms.SplitContainer();
            this.lvList = new BaseServer.ListViewEx();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvEntity = new BaseServer.ListViewEx();
            this.chKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tsFilterByEntity = new System.Windows.Forms.ToolStrip();
            this.tsbDatabase = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRestoreFrom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiClearAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbPoints = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbbackward = new System.Windows.Forms.ToolStripButton();
            this.tsbforward = new System.Windows.Forms.ToolStripButton();
            this.cmsEntity = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miDouble = new System.Windows.Forms.ToolStripMenuItem();
            this.miRename = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsProps = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miBoolNo = new System.Windows.Forms.ToolStripMenuItem();
            this.miBoolYes = new System.Windows.Forms.ToolStripMenuItem();
            this.miChangeString = new System.Windows.Forms.ToolStripMenuItem();
            this.miChangeInteger = new System.Windows.Forms.ToolStripMenuItem();
            this.miChangeFloat = new System.Windows.Forms.ToolStripMenuItem();
            this.miChangeLink = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeleteLink = new System.Windows.Forms.ToolStripMenuItem();
            this.miChangeLinkOPC = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeleteLinkOPC = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundBuildTree = new System.ComponentModel.BackgroundWorker();
            this.exportFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.importFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.cmsList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.опросToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miFetchOff = new System.Windows.Forms.ToolStripMenuItem();
            this.miFetchOn = new System.Windows.Forms.ToolStripMenuItem();
            this.времяОпросаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miOneSec = new System.Windows.Forms.ToolStripMenuItem();
            this.секToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.секToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.секToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.секToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.секToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.секToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.секToolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.секToolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.минToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.минToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.минToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.минToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.минToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.минToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.минToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.часToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.сигнализацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miLoggedOff = new System.Windows.Forms.ToolStripMenuItem();
            this.miLoggedOn = new System.Windows.Forms.ToolStripMenuItem();
            this.квитированиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miAskedOff = new System.Windows.Forms.ToolStripMenuItem();
            this.miAskedOn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listContainer)).BeginInit();
            this.listContainer.Panel1.SuspendLayout();
            this.listContainer.Panel2.SuspendLayout();
            this.listContainer.SuspendLayout();
            this.tsFilterByEntity.SuspendLayout();
            this.cmsEntity.SuspendLayout();
            this.cmsProps.SuspendLayout();
            this.cmsList.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(839, 222);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(839, 247);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsFilterByEntity);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listContainer);
            this.splitContainer1.Size = new System.Drawing.Size(839, 222);
            this.splitContainer1.SplitterDistance = 278;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tvTechnology);
            this.splitContainer2.Panel1Collapsed = true;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tvEntity);
            this.splitContainer2.Size = new System.Drawing.Size(278, 222);
            this.splitContainer2.SplitterDistance = 107;
            this.splitContainer2.TabIndex = 0;
            // 
            // tvTechnology
            // 
            this.tvTechnology.BackColor = System.Drawing.Color.Black;
            this.tvTechnology.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTechnology.ForeColor = System.Drawing.Color.LightGreen;
            this.tvTechnology.Location = new System.Drawing.Point(0, 0);
            this.tvTechnology.Name = "tvTechnology";
            this.tvTechnology.Size = new System.Drawing.Size(150, 107);
            this.tvTechnology.TabIndex = 0;
            // 
            // tvEntity
            // 
            this.tvEntity.BackColor = System.Drawing.Color.Black;
            this.tvEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvEntity.ForeColor = System.Drawing.Color.White;
            this.tvEntity.FullRowSelect = true;
            this.tvEntity.HideSelection = false;
            this.tvEntity.ImageIndex = 0;
            this.tvEntity.ImageList = this.ilTree;
            this.tvEntity.Location = new System.Drawing.Point(0, 0);
            this.tvEntity.Name = "tvEntity";
            treeNode2.Name = "Узел1";
            treeNode2.Text = "Загрузка...";
            this.tvEntity.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.tvEntity.SelectedImageIndex = 0;
            this.tvEntity.ShowNodeToolTips = true;
            this.tvEntity.Size = new System.Drawing.Size(278, 222);
            this.tvEntity.TabIndex = 0;
            this.tvEntity.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvEntity_BeforeCollapse);
            this.tvEntity.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvEntity_BeforeExpand);
            this.tvEntity.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvEntity_BeforeSelect);
            this.tvEntity.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvEntity_AfterSelect);
            this.tvEntity.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvEntity_MouseDown);
            // 
            // ilTree
            // 
            this.ilTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTree.ImageStream")));
            this.ilTree.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTree.Images.SetKeyName(0, "folder.png");
            this.ilTree.Images.SetKeyName(1, "folderopen.png");
            // 
            // listContainer
            // 
            this.listContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listContainer.Location = new System.Drawing.Point(0, 0);
            this.listContainer.Name = "listContainer";
            this.listContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // listContainer.Panel1
            // 
            this.listContainer.Panel1.Controls.Add(this.lvList);
            // 
            // listContainer.Panel2
            // 
            this.listContainer.Panel2.Controls.Add(this.lvEntity);
            this.listContainer.Size = new System.Drawing.Size(557, 222);
            this.listContainer.SplitterDistance = 111;
            this.listContainer.TabIndex = 1;
            // 
            // lvList
            // 
            this.lvList.BackColor = System.Drawing.SystemColors.Window;
            this.lvList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lvList.FullRowSelect = true;
            this.lvList.HideSelection = false;
            this.lvList.LabelWrap = false;
            this.lvList.Location = new System.Drawing.Point(0, 0);
            this.lvList.Name = "lvList";
            this.lvList.Size = new System.Drawing.Size(557, 111);
            this.lvList.SmallImageList = this.ilTree;
            this.lvList.TabIndex = 2;
            this.lvList.UseCompatibleStateImageBehavior = false;
            this.lvList.View = System.Windows.Forms.View.Details;
            this.lvList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvList_ColumnClick);
            this.lvList.DoubleClick += new System.EventHandler(this.lvList_DoubleClick);
            this.lvList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvList_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Шифр позиции";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Дескриптор";
            this.columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Опрос";
            this.columnHeader3.Width = 40;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Сигнализация";
            this.columnHeader4.Width = 40;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Квитирование";
            this.columnHeader5.Width = 40;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Время опроса";
            this.columnHeader6.Width = 80;
            // 
            // lvEntity
            // 
            this.lvEntity.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chKey,
            this.chValue});
            this.lvEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvEntity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lvEntity.FullRowSelect = true;
            this.lvEntity.HideSelection = false;
            this.lvEntity.LabelWrap = false;
            this.lvEntity.Location = new System.Drawing.Point(0, 0);
            this.lvEntity.Name = "lvEntity";
            this.lvEntity.Size = new System.Drawing.Size(557, 107);
            this.lvEntity.SmallImageList = this.ilTree;
            this.lvEntity.TabIndex = 1;
            this.lvEntity.UseCompatibleStateImageBehavior = false;
            this.lvEntity.View = System.Windows.Forms.View.Details;
            this.lvEntity.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvEntity_MouseDown);
            // 
            // chKey
            // 
            this.chKey.Text = "Свойство";
            this.chKey.Width = 250;
            // 
            // chValue
            // 
            this.chValue.Text = "Значение";
            this.chValue.Width = 500;
            // 
            // tsFilterByEntity
            // 
            this.tsFilterByEntity.Dock = System.Windows.Forms.DockStyle.None;
            this.tsFilterByEntity.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbDatabase,
            this.tsbPoints,
            this.toolStripSeparator1,
            this.tsbbackward,
            this.tsbforward});
            this.tsFilterByEntity.Location = new System.Drawing.Point(3, 0);
            this.tsFilterByEntity.Name = "tsFilterByEntity";
            this.tsFilterByEntity.Size = new System.Drawing.Size(280, 25);
            this.tsFilterByEntity.TabIndex = 0;
            // 
            // tsbDatabase
            // 
            this.tsbDatabase.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSaveAs,
            this.tsmiRestoreFrom,
            this.toolStripMenuItem2,
            this.tsmiClearAll});
            this.tsbDatabase.Image = global::DataEditor.Properties.Resources.baseedit;
            this.tsbDatabase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDatabase.Name = "tsbDatabase";
            this.tsbDatabase.Size = new System.Drawing.Size(103, 22);
            this.tsbDatabase.Text = "База данных";
            // 
            // tsmiSaveAs
            // 
            this.tsmiSaveAs.Name = "tsmiSaveAs";
            this.tsmiSaveAs.Size = new System.Drawing.Size(196, 22);
            this.tsmiSaveAs.Text = "Сохранить как...";
            this.tsmiSaveAs.Click += new System.EventHandler(this.tsmiSaveAs_Click);
            // 
            // tsmiRestoreFrom
            // 
            this.tsmiRestoreFrom.Name = "tsmiRestoreFrom";
            this.tsmiRestoreFrom.Size = new System.Drawing.Size(196, 22);
            this.tsmiRestoreFrom.Text = "Восстановить из...";
            this.tsmiRestoreFrom.Click += new System.EventHandler(this.tsmiRestoreFrom_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(193, 6);
            // 
            // tsmiClearAll
            // 
            this.tsmiClearAll.Name = "tsmiClearAll";
            this.tsmiClearAll.Size = new System.Drawing.Size(196, 22);
            this.tsmiClearAll.Text = "Очистить базу данных";
            this.tsmiClearAll.Click += new System.EventHandler(this.tsmiClearAll_Click);
            // 
            // tsbPoints
            // 
            this.tsbPoints.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCreate});
            this.tsbPoints.Image = global::DataEditor.Properties.Resources.edit;
            this.tsbPoints.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPoints.Name = "tsbPoints";
            this.tsbPoints.Size = new System.Drawing.Size(113, 22);
            this.tsbPoints.Text = "Точки опроса";
            // 
            // tsmiCreate
            // 
            this.tsmiCreate.Name = "tsmiCreate";
            this.tsmiCreate.Size = new System.Drawing.Size(117, 22);
            this.tsmiCreate.Text = "Создать";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbbackward
            // 
            this.tsbbackward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbbackward.Enabled = false;
            this.tsbbackward.Image = global::DataEditor.Properties.Resources.back;
            this.tsbbackward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbbackward.Name = "tsbbackward";
            this.tsbbackward.Size = new System.Drawing.Size(23, 22);
            this.tsbbackward.Text = "Назад";
            this.tsbbackward.Click += new System.EventHandler(this.tsbbackward_Click);
            // 
            // tsbforward
            // 
            this.tsbforward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbforward.Enabled = false;
            this.tsbforward.Image = global::DataEditor.Properties.Resources.forward;
            this.tsbforward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbforward.Name = "tsbforward";
            this.tsbforward.Size = new System.Drawing.Size(23, 22);
            this.tsbforward.Text = "Вперёд";
            this.tsbforward.Click += new System.EventHandler(this.tsbforward_Click);
            // 
            // cmsEntity
            // 
            this.cmsEntity.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miDouble,
            this.miRename,
            this.miDelete});
            this.cmsEntity.Name = "cmsEntity";
            this.cmsEntity.Size = new System.Drawing.Size(162, 70);
            // 
            // miDouble
            // 
            this.miDouble.Name = "miDouble";
            this.miDouble.Size = new System.Drawing.Size(161, 22);
            this.miDouble.Text = "Дублировать";
            this.miDouble.Click += new System.EventHandler(this.miDoubleEntity_Click);
            // 
            // miRename
            // 
            this.miRename.Name = "miRename";
            this.miRename.Size = new System.Drawing.Size(161, 22);
            this.miRename.Text = "Переименовать";
            this.miRename.Click += new System.EventHandler(this.miRenameEntity_Click);
            // 
            // miDelete
            // 
            this.miDelete.Name = "miDelete";
            this.miDelete.Size = new System.Drawing.Size(161, 22);
            this.miDelete.Text = "Удалить";
            this.miDelete.Click += new System.EventHandler(this.miDeleteEntity_Click);
            // 
            // cmsProps
            // 
            this.cmsProps.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miBoolNo,
            this.miBoolYes,
            this.miChangeString,
            this.miChangeInteger,
            this.miChangeFloat,
            this.miChangeLink,
            this.miDeleteLink,
            this.miChangeLinkOPC,
            this.miDeleteLinkOPC});
            this.cmsProps.Name = "cmsProps";
            this.cmsProps.Size = new System.Drawing.Size(192, 202);
            // 
            // miBoolNo
            // 
            this.miBoolNo.Name = "miBoolNo";
            this.miBoolNo.Size = new System.Drawing.Size(191, 22);
            this.miBoolNo.Text = "Нет";
            this.miBoolNo.Click += new System.EventHandler(this.miBoolYesNo_Click);
            // 
            // miBoolYes
            // 
            this.miBoolYes.Name = "miBoolYes";
            this.miBoolYes.Size = new System.Drawing.Size(191, 22);
            this.miBoolYes.Text = "Да";
            this.miBoolYes.Click += new System.EventHandler(this.miBoolYesNo_Click);
            // 
            // miChangeString
            // 
            this.miChangeString.Name = "miChangeString";
            this.miChangeString.Size = new System.Drawing.Size(191, 22);
            this.miChangeString.Text = "Изменить текст...";
            this.miChangeString.Click += new System.EventHandler(this.miChangeString_Click);
            // 
            // miChangeInteger
            // 
            this.miChangeInteger.Name = "miChangeInteger";
            this.miChangeInteger.Size = new System.Drawing.Size(191, 22);
            this.miChangeInteger.Text = "Изменить число...";
            this.miChangeInteger.Click += new System.EventHandler(this.miChangeInteger_Click);
            // 
            // miChangeFloat
            // 
            this.miChangeFloat.Name = "miChangeFloat";
            this.miChangeFloat.Size = new System.Drawing.Size(191, 22);
            this.miChangeFloat.Text = "Изменить значение...";
            this.miChangeFloat.Click += new System.EventHandler(this.miChangeFloat_Click);
            // 
            // miChangeLink
            // 
            this.miChangeLink.Name = "miChangeLink";
            this.miChangeLink.Size = new System.Drawing.Size(191, 22);
            this.miChangeLink.Text = "Изменить связь...";
            this.miChangeLink.Click += new System.EventHandler(this.miChangeLink_Click);
            // 
            // miDeleteLink
            // 
            this.miDeleteLink.Name = "miDeleteLink";
            this.miDeleteLink.Size = new System.Drawing.Size(191, 22);
            this.miDeleteLink.Text = "Удалить связь";
            this.miDeleteLink.Click += new System.EventHandler(this.miDeleteLink_Click);
            // 
            // miChangeLinkOPC
            // 
            this.miChangeLinkOPC.Name = "miChangeLinkOPC";
            this.miChangeLinkOPC.Size = new System.Drawing.Size(191, 22);
            this.miChangeLinkOPC.Text = "Изменить привязку...";
            this.miChangeLinkOPC.Click += new System.EventHandler(this.miChangeLinkOPC_Click);
            // 
            // miDeleteLinkOPC
            // 
            this.miDeleteLinkOPC.Name = "miDeleteLinkOPC";
            this.miDeleteLinkOPC.Size = new System.Drawing.Size(191, 22);
            this.miDeleteLinkOPC.Text = "Удалить привязку";
            this.miDeleteLinkOPC.Click += new System.EventHandler(this.miDeleteLinkOPC_Click);
            // 
            // backgroundBuildTree
            // 
            this.backgroundBuildTree.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundBuildTree_DoWork);
            this.backgroundBuildTree.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundBuildTree_RunWorkerCompleted);
            // 
            // exportFileDialog
            // 
            this.exportFileDialog.DefaultExt = "ini";
            this.exportFileDialog.Filter = "*.ini|*.ini";
            // 
            // importFileDialog
            // 
            this.importFileDialog.DefaultExt = "ini";
            this.importFileDialog.Filter = "*.ini|*.ini";
            // 
            // cmsList
            // 
            this.cmsList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.опросToolStripMenuItem,
            this.времяОпросаToolStripMenuItem,
            this.toolStripMenuItem3,
            this.сигнализацияToolStripMenuItem,
            this.квитированиеToolStripMenuItem});
            this.cmsList.Name = "cmsList";
            this.cmsList.Size = new System.Drawing.Size(153, 98);
            // 
            // опросToolStripMenuItem
            // 
            this.опросToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFetchOff,
            this.miFetchOn});
            this.опросToolStripMenuItem.Name = "опросToolStripMenuItem";
            this.опросToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.опросToolStripMenuItem.Text = "Опрос";
            // 
            // miFetchOff
            // 
            this.miFetchOff.Name = "miFetchOff";
            this.miFetchOff.Size = new System.Drawing.Size(184, 22);
            this.miFetchOff.Tag = "False";
            this.miFetchOff.Text = "Снять с опроса";
            this.miFetchOff.Click += new System.EventHandler(this.miFetchOff_Click);
            // 
            // miFetchOn
            // 
            this.miFetchOn.Name = "miFetchOn";
            this.miFetchOn.Size = new System.Drawing.Size(184, 22);
            this.miFetchOn.Tag = "True";
            this.miFetchOn.Text = "Поставить на опрос";
            this.miFetchOn.Click += new System.EventHandler(this.miFetchOff_Click);
            // 
            // времяОпросаToolStripMenuItem
            // 
            this.времяОпросаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOneSec,
            this.секToolStripMenuItem1,
            this.секToolStripMenuItem2,
            this.секToolStripMenuItem3,
            this.секToolStripMenuItem4,
            this.секToolStripMenuItem5,
            this.секToolStripMenuItem6,
            this.секToolStripMenuItem7,
            this.секToolStripMenuItem8,
            this.минToolStripMenuItem,
            this.минToolStripMenuItem1,
            this.минToolStripMenuItem2,
            this.минToolStripMenuItem3,
            this.минToolStripMenuItem4,
            this.минToolStripMenuItem5,
            this.toolStripMenuItem5,
            this.минToolStripMenuItem6,
            this.часToolStripMenuItem});
            this.времяОпросаToolStripMenuItem.Name = "времяОпросаToolStripMenuItem";
            this.времяОпросаToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.времяОпросаToolStripMenuItem.Text = "Время опроса";
            // 
            // miOneSec
            // 
            this.miOneSec.Name = "miOneSec";
            this.miOneSec.Size = new System.Drawing.Size(112, 22);
            this.miOneSec.Tag = "1";
            this.miOneSec.Text = "1 сек";
            this.miOneSec.Click += new System.EventHandler(this.miOneSec_Click);
            // 
            // секToolStripMenuItem1
            // 
            this.секToolStripMenuItem1.Name = "секToolStripMenuItem1";
            this.секToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.секToolStripMenuItem1.Tag = "2";
            this.секToolStripMenuItem1.Text = "2 сек";
            this.секToolStripMenuItem1.Click += new System.EventHandler(this.miOneSec_Click);
            // 
            // секToolStripMenuItem2
            // 
            this.секToolStripMenuItem2.Name = "секToolStripMenuItem2";
            this.секToolStripMenuItem2.Size = new System.Drawing.Size(112, 22);
            this.секToolStripMenuItem2.Tag = "3";
            this.секToolStripMenuItem2.Text = "3 сек";
            this.секToolStripMenuItem2.Click += new System.EventHandler(this.miOneSec_Click);
            // 
            // секToolStripMenuItem3
            // 
            this.секToolStripMenuItem3.Name = "секToolStripMenuItem3";
            this.секToolStripMenuItem3.Size = new System.Drawing.Size(112, 22);
            this.секToolStripMenuItem3.Tag = "5";
            this.секToolStripMenuItem3.Text = "5 сек";
            this.секToolStripMenuItem3.Click += new System.EventHandler(this.miOneSec_Click);
            // 
            // секToolStripMenuItem4
            // 
            this.секToolStripMenuItem4.Name = "секToolStripMenuItem4";
            this.секToolStripMenuItem4.Size = new System.Drawing.Size(112, 22);
            this.секToolStripMenuItem4.Tag = "10";
            this.секToolStripMenuItem4.Text = "10 сек";
            this.секToolStripMenuItem4.Click += new System.EventHandler(this.miOneSec_Click);
            // 
            // секToolStripMenuItem5
            // 
            this.секToolStripMenuItem5.Name = "секToolStripMenuItem5";
            this.секToolStripMenuItem5.Size = new System.Drawing.Size(112, 22);
            this.секToolStripMenuItem5.Tag = "15";
            this.секToolStripMenuItem5.Text = "15 сек";
            this.секToolStripMenuItem5.Click += new System.EventHandler(this.miOneSec_Click);
            // 
            // секToolStripMenuItem6
            // 
            this.секToolStripMenuItem6.Name = "секToolStripMenuItem6";
            this.секToolStripMenuItem6.Size = new System.Drawing.Size(112, 22);
            this.секToolStripMenuItem6.Tag = "20";
            this.секToolStripMenuItem6.Text = "20 сек";
            this.секToolStripMenuItem6.Click += new System.EventHandler(this.miOneSec_Click);
            // 
            // секToolStripMenuItem7
            // 
            this.секToolStripMenuItem7.Name = "секToolStripMenuItem7";
            this.секToolStripMenuItem7.Size = new System.Drawing.Size(112, 22);
            this.секToolStripMenuItem7.Tag = "30";
            this.секToolStripMenuItem7.Text = "30 сек";
            this.секToolStripMenuItem7.Click += new System.EventHandler(this.miOneSec_Click);
            // 
            // секToolStripMenuItem8
            // 
            this.секToolStripMenuItem8.Name = "секToolStripMenuItem8";
            this.секToolStripMenuItem8.Size = new System.Drawing.Size(112, 22);
            this.секToolStripMenuItem8.Tag = "45";
            this.секToolStripMenuItem8.Text = "45 сек";
            this.секToolStripMenuItem8.Click += new System.EventHandler(this.miOneSec_Click);
            // 
            // минToolStripMenuItem
            // 
            this.минToolStripMenuItem.Name = "минToolStripMenuItem";
            this.минToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.минToolStripMenuItem.Tag = "60";
            this.минToolStripMenuItem.Text = "1 мин";
            this.минToolStripMenuItem.Click += new System.EventHandler(this.miOneSec_Click);
            // 
            // минToolStripMenuItem1
            // 
            this.минToolStripMenuItem1.Name = "минToolStripMenuItem1";
            this.минToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.минToolStripMenuItem1.Tag = "120";
            this.минToolStripMenuItem1.Text = "2 мин";
            this.минToolStripMenuItem1.Click += new System.EventHandler(this.miOneSec_Click);
            // 
            // минToolStripMenuItem2
            // 
            this.минToolStripMenuItem2.Name = "минToolStripMenuItem2";
            this.минToolStripMenuItem2.Size = new System.Drawing.Size(112, 22);
            this.минToolStripMenuItem2.Tag = "180";
            this.минToolStripMenuItem2.Text = "3 мин";
            this.минToolStripMenuItem2.Click += new System.EventHandler(this.miOneSec_Click);
            // 
            // минToolStripMenuItem3
            // 
            this.минToolStripMenuItem3.Name = "минToolStripMenuItem3";
            this.минToolStripMenuItem3.Size = new System.Drawing.Size(112, 22);
            this.минToolStripMenuItem3.Tag = "300";
            this.минToolStripMenuItem3.Text = "5 мин";
            this.минToolStripMenuItem3.Click += new System.EventHandler(this.miOneSec_Click);
            // 
            // минToolStripMenuItem4
            // 
            this.минToolStripMenuItem4.Name = "минToolStripMenuItem4";
            this.минToolStripMenuItem4.Size = new System.Drawing.Size(112, 22);
            this.минToolStripMenuItem4.Tag = "600";
            this.минToolStripMenuItem4.Text = "10 мин";
            this.минToolStripMenuItem4.Click += new System.EventHandler(this.miOneSec_Click);
            // 
            // минToolStripMenuItem5
            // 
            this.минToolStripMenuItem5.Name = "минToolStripMenuItem5";
            this.минToolStripMenuItem5.Size = new System.Drawing.Size(112, 22);
            this.минToolStripMenuItem5.Tag = "900";
            this.минToolStripMenuItem5.Text = "15 мин";
            this.минToolStripMenuItem5.Click += new System.EventHandler(this.miOneSec_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem5.Tag = "1200";
            this.toolStripMenuItem5.Text = "20 мин";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.miOneSec_Click);
            // 
            // минToolStripMenuItem6
            // 
            this.минToolStripMenuItem6.Name = "минToolStripMenuItem6";
            this.минToolStripMenuItem6.Size = new System.Drawing.Size(112, 22);
            this.минToolStripMenuItem6.Tag = "1800";
            this.минToolStripMenuItem6.Text = "30 мин";
            this.минToolStripMenuItem6.Click += new System.EventHandler(this.miOneSec_Click);
            // 
            // часToolStripMenuItem
            // 
            this.часToolStripMenuItem.Name = "часToolStripMenuItem";
            this.часToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.часToolStripMenuItem.Tag = "3600";
            this.часToolStripMenuItem.Text = "1 час";
            this.часToolStripMenuItem.Click += new System.EventHandler(this.miOneSec_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(149, 6);
            // 
            // сигнализацияToolStripMenuItem
            // 
            this.сигнализацияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLoggedOff,
            this.miLoggedOn});
            this.сигнализацияToolStripMenuItem.Name = "сигнализацияToolStripMenuItem";
            this.сигнализацияToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.сигнализацияToolStripMenuItem.Text = "Сигнализация";
            // 
            // miLoggedOff
            // 
            this.miLoggedOff.Name = "miLoggedOff";
            this.miLoggedOff.Size = new System.Drawing.Size(94, 22);
            this.miLoggedOff.Tag = "False";
            this.miLoggedOff.Text = "Нет";
            this.miLoggedOff.Click += new System.EventHandler(this.miLoggedOff_Click);
            // 
            // miLoggedOn
            // 
            this.miLoggedOn.Name = "miLoggedOn";
            this.miLoggedOn.Size = new System.Drawing.Size(94, 22);
            this.miLoggedOn.Tag = "True";
            this.miLoggedOn.Text = "Да";
            this.miLoggedOn.Click += new System.EventHandler(this.miLoggedOff_Click);
            // 
            // квитированиеToolStripMenuItem
            // 
            this.квитированиеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAskedOff,
            this.miAskedOn});
            this.квитированиеToolStripMenuItem.Name = "квитированиеToolStripMenuItem";
            this.квитированиеToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.квитированиеToolStripMenuItem.Text = "Квитирование";
            // 
            // miAskedOff
            // 
            this.miAskedOff.Name = "miAskedOff";
            this.miAskedOff.Size = new System.Drawing.Size(94, 22);
            this.miAskedOff.Tag = "False";
            this.miAskedOff.Text = "Нет";
            this.miAskedOff.Click += new System.EventHandler(this.miAskedOff_Click);
            // 
            // miAskedOn
            // 
            this.miAskedOn.Name = "miAskedOn";
            this.miAskedOn.Size = new System.Drawing.Size(94, 22);
            this.miAskedOn.Tag = "True";
            this.miAskedOn.Text = "Да";
            this.miAskedOn.Click += new System.EventHandler(this.miAskedOff_Click);
            // 
            // frmBaseEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 247);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBaseEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Редактор базы данных";
            this.Load += new System.EventHandler(this.frmBaseEditor_Load);
            this.Shown += new System.EventHandler(this.frmBaseEditor_Shown);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.listContainer.Panel1.ResumeLayout(false);
            this.listContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listContainer)).EndInit();
            this.listContainer.ResumeLayout(false);
            this.tsFilterByEntity.ResumeLayout(false);
            this.tsFilterByEntity.PerformLayout();
            this.cmsEntity.ResumeLayout(false);
            this.cmsProps.ResumeLayout(false);
            this.cmsList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView tvTechnology;
        private System.Windows.Forms.TreeView tvEntity;
        private System.Windows.Forms.ToolStrip tsFilterByEntity;
        private System.Windows.Forms.ContextMenuStrip cmsEntity;
        private System.ComponentModel.BackgroundWorker backgroundBuildTree;
        private System.Windows.Forms.ToolStripDropDownButton tsbPoints;
        private System.Windows.Forms.ToolStripMenuItem tsmiCreate;
        private System.Windows.Forms.ToolStripDropDownButton tsbDatabase;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveAs;
        private System.Windows.Forms.ToolStripMenuItem tsmiRestoreFrom;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmiClearAll;
        private System.Windows.Forms.ToolStripMenuItem miDouble;
        private System.Windows.Forms.ToolStripMenuItem miRename;
        private System.Windows.Forms.ToolStripMenuItem miDelete;
        private System.Windows.Forms.SaveFileDialog exportFileDialog;
        private System.Windows.Forms.OpenFileDialog importFileDialog;
        private System.Windows.Forms.ContextMenuStrip cmsProps;
        private System.Windows.Forms.ToolStripMenuItem miBoolNo;
        private System.Windows.Forms.ToolStripMenuItem miBoolYes;
        private System.Windows.Forms.ToolStripMenuItem miChangeString;
        private System.Windows.Forms.ToolStripMenuItem miChangeInteger;
        private System.Windows.Forms.ToolStripMenuItem miChangeFloat;
        private System.Windows.Forms.ToolStripMenuItem miChangeLink;
        private System.Windows.Forms.ToolStripMenuItem miDeleteLink;
        private System.Windows.Forms.ToolStripMenuItem miChangeLinkOPC;
        private System.Windows.Forms.ToolStripMenuItem miDeleteLinkOPC;
        private System.Windows.Forms.SplitContainer listContainer;
        private BaseServer.ListViewEx lvList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private BaseServer.ListViewEx lvEntity;
        private System.Windows.Forms.ColumnHeader chKey;
        private System.Windows.Forms.ColumnHeader chValue;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ContextMenuStrip cmsList;
        private System.Windows.Forms.ToolStripMenuItem опросToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem времяОпросаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miOneSec;
        private System.Windows.Forms.ToolStripMenuItem секToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem секToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem секToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem секToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem секToolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem секToolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem секToolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem секToolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem минToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem минToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem минToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem минToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem минToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem минToolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem минToolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem часToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem сигнализацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem квитированиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miFetchOff;
        private System.Windows.Forms.ToolStripMenuItem miFetchOn;
        private System.Windows.Forms.ToolStripMenuItem miLoggedOff;
        private System.Windows.Forms.ToolStripMenuItem miLoggedOn;
        private System.Windows.Forms.ToolStripMenuItem miAskedOff;
        private System.Windows.Forms.ToolStripMenuItem miAskedOn;
        private System.Windows.Forms.ImageList ilTree;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbbackward;
        private System.Windows.Forms.ToolStripButton tsbforward;


    }
}