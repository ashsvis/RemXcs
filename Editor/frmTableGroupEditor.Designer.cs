namespace DataEditor
{
    partial class frmTableGroupEditor
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Загрузка...");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTableGroupEditor));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tvTechnology = new System.Windows.Forms.TreeView();
            this.tvEntity = new System.Windows.Forms.TreeView();
            this.listContainer = new System.Windows.Forms.SplitContainer();
            this.lvList = new BaseServer.ListViewEx();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvEntity = new BaseServer.ListViewEx();
            this.chKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tsFilterByEntity = new System.Windows.Forms.ToolStrip();
            this.tsbGroupParams = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmgSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmgRestoreFrom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmgClearAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tstbGroupCount = new System.Windows.Forms.ToolStripTextBox();
            this.cmsProps = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miChangeString = new System.Windows.Forms.ToolStripMenuItem();
            this.miChangeLink = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeleteLink = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundBuildTree = new System.ComponentModel.BackgroundWorker();
            this.exportFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.importFileDialog = new System.Windows.Forms.OpenFileDialog();
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
            this.cmsProps.SuspendLayout();
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
            this.tvEntity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tvEntity.FullRowSelect = true;
            this.tvEntity.HideSelection = false;
            this.tvEntity.Location = new System.Drawing.Point(0, 0);
            this.tvEntity.Name = "tvEntity";
            treeNode1.Name = "Узел1";
            treeNode1.Text = "Загрузка...";
            this.tvEntity.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tvEntity.ShowNodeToolTips = true;
            this.tvEntity.Size = new System.Drawing.Size(278, 222);
            this.tvEntity.TabIndex = 0;
            this.tvEntity.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvEntity_AfterSelect);
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
            this.lvList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lvList.FullRowSelect = true;
            this.lvList.HideSelection = false;
            this.lvList.LabelWrap = false;
            this.lvList.Location = new System.Drawing.Point(0, 0);
            this.lvList.Name = "lvList";
            this.lvList.Size = new System.Drawing.Size(557, 111);
            this.lvList.TabIndex = 2;
            this.lvList.UseCompatibleStateImageBehavior = false;
            this.lvList.View = System.Windows.Forms.View.Details;
            this.lvList.DoubleClick += new System.EventHandler(this.lvList_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Номер группы";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Дескриптор группы";
            this.columnHeader2.Width = 200;
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
            this.tsbGroupParams,
            this.toolStripLabel1,
            this.tstbGroupCount});
            this.tsFilterByEntity.Location = new System.Drawing.Point(3, 0);
            this.tsFilterByEntity.Name = "tsFilterByEntity";
            this.tsFilterByEntity.Size = new System.Drawing.Size(221, 25);
            this.tsFilterByEntity.TabIndex = 0;
            // 
            // tsbGroupParams
            // 
            this.tsbGroupParams.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmgSaveAs,
            this.tsmgRestoreFrom,
            this.toolStripMenuItem1,
            this.tsmgClearAll});
            this.tsbGroupParams.Image = global::DataEditor.Properties.Resources.groups;
            this.tsbGroupParams.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGroupParams.Name = "tsbGroupParams";
            this.tsbGroupParams.Size = new System.Drawing.Size(74, 22);
            this.tsbGroupParams.Text = "Группы";
            // 
            // tsmgSaveAs
            // 
            this.tsmgSaveAs.Name = "tsmgSaveAs";
            this.tsmgSaveAs.Size = new System.Drawing.Size(226, 22);
            this.tsmgSaveAs.Text = "Сохранить как...";
            this.tsmgSaveAs.Click += new System.EventHandler(this.tsmgSaveAs_Click);
            // 
            // tsmgRestoreFrom
            // 
            this.tsmgRestoreFrom.Name = "tsmgRestoreFrom";
            this.tsmgRestoreFrom.Size = new System.Drawing.Size(226, 22);
            this.tsmgRestoreFrom.Text = "Восстановить из...";
            this.tsmgRestoreFrom.Click += new System.EventHandler(this.tsmgRestoreFrom_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(223, 6);
            // 
            // tsmgClearAll
            // 
            this.tsmgClearAll.Name = "tsmgClearAll";
            this.tsmgClearAll.Size = new System.Drawing.Size(226, 22);
            this.tsmgClearAll.Text = "Очистить группы параметров";
            this.tsmgClearAll.Click += new System.EventHandler(this.tsmgClearAll_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(74, 22);
            this.toolStripLabel1.Text = " Количество:";
            // 
            // tstbGroupCount
            // 
            this.tstbGroupCount.Name = "tstbGroupCount";
            this.tstbGroupCount.Size = new System.Drawing.Size(30, 25);
            this.tstbGroupCount.Text = "500";
            this.tstbGroupCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tstbGroupCount_KeyDown);
            // 
            // cmsProps
            // 
            this.cmsProps.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miChangeString,
            this.miChangeLink,
            this.miDeleteLink});
            this.cmsProps.Name = "cmsProps";
            this.cmsProps.Size = new System.Drawing.Size(170, 70);
            // 
            // miChangeString
            // 
            this.miChangeString.Name = "miChangeString";
            this.miChangeString.Size = new System.Drawing.Size(169, 22);
            this.miChangeString.Text = "Изменить текст...";
            this.miChangeString.Click += new System.EventHandler(this.miChangeString_Click);
            // 
            // miChangeLink
            // 
            this.miChangeLink.Name = "miChangeLink";
            this.miChangeLink.Size = new System.Drawing.Size(169, 22);
            this.miChangeLink.Text = "Изменить связь...";
            this.miChangeLink.Click += new System.EventHandler(this.miChangeLink_Click);
            // 
            // miDeleteLink
            // 
            this.miDeleteLink.Name = "miDeleteLink";
            this.miDeleteLink.Size = new System.Drawing.Size(169, 22);
            this.miDeleteLink.Text = "Удалить связь";
            this.miDeleteLink.Click += new System.EventHandler(this.miDeleteLink_Click);
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
            // frmTableGroupEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 247);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTableGroupEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Редактор групп параметров";
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
            this.cmsProps.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView tvTechnology;
        private System.Windows.Forms.TreeView tvEntity;
        private System.Windows.Forms.ToolStrip tsFilterByEntity;
        private System.ComponentModel.BackgroundWorker backgroundBuildTree;
        private System.Windows.Forms.SaveFileDialog exportFileDialog;
        private System.Windows.Forms.OpenFileDialog importFileDialog;
        private System.Windows.Forms.ContextMenuStrip cmsProps;
        private System.Windows.Forms.ToolStripMenuItem miChangeString;
        private System.Windows.Forms.ToolStripMenuItem miChangeLink;
        private System.Windows.Forms.ToolStripMenuItem miDeleteLink;
        private System.Windows.Forms.ToolStripDropDownButton tsbGroupParams;
        private System.Windows.Forms.ToolStripMenuItem tsmgSaveAs;
        private System.Windows.Forms.ToolStripMenuItem tsmgRestoreFrom;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmgClearAll;
        private System.Windows.Forms.SplitContainer listContainer;
        private BaseServer.ListViewEx lvList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private BaseServer.ListViewEx lvEntity;
        private System.Windows.Forms.ColumnHeader chKey;
        private System.Windows.Forms.ColumnHeader chValue;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tstbGroupCount;


    }
}