namespace FetchSPT961
{
    partial class frmFetchingSPT961
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFetchingSPT961));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miFetchList = new System.Windows.Forms.ToolStripMenuItem();
            this.miTuning = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miClose = new System.Windows.Forms.ToolStripMenuItem();
            this.contextItems = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miFreshList = new System.Windows.Forms.ToolStripMenuItem();
            this.fetchTimer = new System.Windows.Forms.Timer(this.components);
            this.backgroundUpdateChilds = new System.ComponentModel.BackgroundWorker();
            this.backImLive = new System.ComponentModel.BackgroundWorker();
            this.backCheckCommand = new System.ComponentModel.BackgroundWorker();
            this.backUpdate = new System.ComponentModel.BackgroundWorker();
            this.contextNotifyIcon.SuspendLayout();
            this.contextItems.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextNotifyIcon;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Сервер опроса СПТ961.2";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextNotifyIcon
            // 
            this.contextNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFetchList,
            this.miTuning,
            this.toolStripMenuItem1,
            this.miClose});
            this.contextNotifyIcon.Name = "contextMenuStrip1";
            this.contextNotifyIcon.Size = new System.Drawing.Size(221, 76);
            // 
            // miFetchList
            // 
            this.miFetchList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.miFetchList.Name = "miFetchList";
            this.miFetchList.Size = new System.Drawing.Size(220, 22);
            this.miFetchList.Text = "Список опроса...";
            this.miFetchList.Click += new System.EventHandler(this.miFetchList_Click);
            // 
            // miTuning
            // 
            this.miTuning.Name = "miTuning";
            this.miTuning.Size = new System.Drawing.Size(220, 22);
            this.miTuning.Text = "Настроить подключение...";
            this.miTuning.Click += new System.EventHandler(this.miTuning_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(217, 6);
            // 
            // miClose
            // 
            this.miClose.Name = "miClose";
            this.miClose.Size = new System.Drawing.Size(220, 22);
            this.miClose.Text = "Выгрузить";
            this.miClose.Click += new System.EventHandler(this.miClose_Click);
            // 
            // contextItems
            // 
            this.contextItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFreshList});
            this.contextItems.Name = "contextItems";
            this.contextItems.Size = new System.Drawing.Size(180, 26);
            // 
            // miFreshList
            // 
            this.miFreshList.Name = "miFreshList";
            this.miFreshList.Size = new System.Drawing.Size(179, 22);
            this.miFreshList.Text = "Обновить выборку";
            this.miFreshList.Click += new System.EventHandler(this.miFreshList_Click);
            // 
            // fetchTimer
            // 
            this.fetchTimer.Interval = 1000;
            this.fetchTimer.Tick += new System.EventHandler(this.fetchTimer_Tick);
            // 
            // backgroundUpdateChilds
            // 
            this.backgroundUpdateChilds.WorkerReportsProgress = true;
            this.backgroundUpdateChilds.WorkerSupportsCancellation = true;
            this.backgroundUpdateChilds.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundUpdateChilds_DoWork);
            this.backgroundUpdateChilds.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundUpdateChilds_ProgressChanged);
            this.backgroundUpdateChilds.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundUpdateChilds_RunWorkerCompleted);
            // 
            // backImLive
            // 
            this.backImLive.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backImLive_DoWork);
            this.backImLive.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backImLive_RunWorkerCompleted);
            // 
            // backCheckCommand
            // 
            this.backCheckCommand.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backCheckCommand_DoWork);
            this.backCheckCommand.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backCheckCommand_RunWorkerCompleted);
            // 
            // backUpdate
            // 
            this.backUpdate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backUpdate_DoWork);
            // 
            // frmFetchingSPT961
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 27);
            this.ControlBox = false;
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFetchingSPT961";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сервер опроса СПТ961.2";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFetching_FormClosing);
            this.Load += new System.EventHandler(this.frmFetching_Load);
            this.Resize += new System.EventHandler(this.frmFetchingSPT961_Resize);
            this.contextNotifyIcon.ResumeLayout(false);
            this.contextItems.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem miClose;
        private System.Windows.Forms.ToolStripMenuItem miFetchList;
        private System.Windows.Forms.Timer fetchTimer;
        private System.Windows.Forms.ContextMenuStrip contextItems;
        private System.Windows.Forms.ToolStripMenuItem miFreshList;
        private System.ComponentModel.BackgroundWorker backgroundUpdateChilds;
        private System.Windows.Forms.ToolStripMenuItem miTuning;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.ComponentModel.BackgroundWorker backImLive;
        private System.ComponentModel.BackgroundWorker backCheckCommand;
        private System.ComponentModel.BackgroundWorker backUpdate;
    }
}

