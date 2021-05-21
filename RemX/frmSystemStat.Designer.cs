namespace RemXcs
{
    partial class frmSystemStat
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvNodes = new System.Windows.Forms.TreeView();
            this.lvFetchList = new BaseServer.ListViewEx();
            this.chTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chQuality = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tmrFresh = new System.Windows.Forms.Timer(this.components);
            this.cmTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miRefreshFetchList = new System.Windows.Forms.ToolStripMenuItem();
            this.miReloadStation = new System.Windows.Forms.ToolStripMenuItem();
            this.backBuildTree = new System.ComponentModel.BackgroundWorker();
            this.backBuildList = new System.ComponentModel.BackgroundWorker();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cmTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(814, 257);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(814, 282);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvNodes);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvFetchList);
            this.splitContainer1.Size = new System.Drawing.Size(814, 257);
            this.splitContainer1.SplitterDistance = 271;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvNodes
            // 
            this.tvNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvNodes.HideSelection = false;
            this.tvNodes.Location = new System.Drawing.Point(0, 0);
            this.tvNodes.Name = "tvNodes";
            this.tvNodes.Size = new System.Drawing.Size(271, 257);
            this.tvNodes.TabIndex = 0;
            this.tvNodes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvNodes_AfterSelect);
            this.tvNodes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvNodes_MouseDown);
            // 
            // lvFetchList
            // 
            this.lvFetchList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTag,
            this.chDesc,
            this.chValue,
            this.chQuality,
            this.chTime});
            this.lvFetchList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFetchList.FullRowSelect = true;
            this.lvFetchList.HideSelection = false;
            this.lvFetchList.Location = new System.Drawing.Point(0, 0);
            this.lvFetchList.MultiSelect = false;
            this.lvFetchList.Name = "lvFetchList";
            this.lvFetchList.Size = new System.Drawing.Size(538, 257);
            this.lvFetchList.TabIndex = 0;
            this.lvFetchList.UseCompatibleStateImageBehavior = false;
            this.lvFetchList.View = System.Windows.Forms.View.Details;
            this.lvFetchList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvFetchList_ColumnClick);
            this.lvFetchList.SelectedIndexChanged += new System.EventHandler(this.lvFetchList_SelectedIndexChanged);
            // 
            // chTag
            // 
            this.chTag.Text = "Имя тега";
            this.chTag.Width = 120;
            // 
            // chDesc
            // 
            this.chDesc.Text = "Дескриптор";
            this.chDesc.Width = 300;
            // 
            // chValue
            // 
            this.chValue.Text = "Значение";
            this.chValue.Width = 100;
            // 
            // chQuality
            // 
            this.chQuality.Text = "Качество";
            this.chQuality.Width = 100;
            // 
            // chTime
            // 
            this.chTime.Text = "Время опроса";
            this.chTime.Width = 150;
            // 
            // tmrFresh
            // 
            this.tmrFresh.Interval = 1000;
            this.tmrFresh.Tick += new System.EventHandler(this.tmrFresh_Tick);
            // 
            // cmTree
            // 
            this.cmTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miRefreshFetchList,
            this.miReloadStation});
            this.cmTree.Name = "cmTree";
            this.cmTree.Size = new System.Drawing.Size(200, 48);
            // 
            // miRefreshFetchList
            // 
            this.miRefreshFetchList.Name = "miRefreshFetchList";
            this.miRefreshFetchList.Size = new System.Drawing.Size(199, 22);
            this.miRefreshFetchList.Text = "Обновить список опроса";
            this.miRefreshFetchList.Click += new System.EventHandler(this.miRefreshFetchList_Click);
            // 
            // miReloadStation
            // 
            this.miReloadStation.Name = "miReloadStation";
            this.miReloadStation.Size = new System.Drawing.Size(199, 22);
            this.miReloadStation.Text = "Перезагрузить станцию";
            this.miReloadStation.Click += new System.EventHandler(this.miReloadStation_Click);
            // 
            // backBuildTree
            // 
            this.backBuildTree.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backBuildTree_DoWork);
            this.backBuildTree.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backBuildTree_RunWorkerCompleted);
            // 
            // backBuildList
            // 
            this.backBuildList.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backBuildList_DoWork);
            this.backBuildList.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backBuildList_RunWorkerCompleted);
            // 
            // frmSystemStat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 282);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "frmSystemStat";
            this.Text = "frmSystemStat";
            this.Load += new System.EventHandler(this.frmSystemStat_Load);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.cmTree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvNodes;
        private System.Windows.Forms.Timer tmrFresh;
        private BaseServer.ListViewEx lvFetchList;
        private System.Windows.Forms.ColumnHeader chTag;
        private System.Windows.Forms.ColumnHeader chDesc;
        private System.Windows.Forms.ColumnHeader chValue;
        private System.Windows.Forms.ColumnHeader chQuality;
        private System.Windows.Forms.ColumnHeader chTime;
        private System.Windows.Forms.ContextMenuStrip cmTree;
        private System.Windows.Forms.ToolStripMenuItem miRefreshFetchList;
        private System.Windows.Forms.ToolStripMenuItem miReloadStation;
        private System.ComponentModel.BackgroundWorker backBuildTree;
        private System.ComponentModel.BackgroundWorker backBuildList;
    }
}