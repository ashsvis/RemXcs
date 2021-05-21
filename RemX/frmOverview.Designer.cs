namespace RemXcs
{
    partial class frmOverview
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
            this.pnlScroll = new System.Windows.Forms.Panel();
            this.drawBox = new System.Windows.Forms.PictureBox();
            this.timerFetch = new System.Windows.Forms.Timer(this.components);
            this.contextDinMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miPassport = new System.Windows.Forms.ToolStripMenuItem();
            this.miFindInDataBase = new System.Windows.Forms.ToolStripMenuItem();
            this.miQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.miTrendSplit = new System.Windows.Forms.ToolStripSeparator();
            this.miTrend = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundBaseReload = new System.ComponentModel.BackgroundWorker();
            this.backgroundFetch = new System.ComponentModel.BackgroundWorker();
            this.contextPageMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ysmiPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).BeginInit();
            this.contextDinMenu.SuspendLayout();
            this.contextPageMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlScroll
            // 
            this.pnlScroll.Controls.Add(this.drawBox);
            this.pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScroll.Location = new System.Drawing.Point(0, 0);
            this.pnlScroll.Name = "pnlScroll";
            this.pnlScroll.Size = new System.Drawing.Size(823, 330);
            this.pnlScroll.TabIndex = 2;
            // 
            // drawBox
            // 
            this.drawBox.BackColor = System.Drawing.Color.Gray;
            this.drawBox.Location = new System.Drawing.Point(0, 0);
            this.drawBox.Name = "drawBox";
            this.drawBox.Size = new System.Drawing.Size(832, 312);
            this.drawBox.TabIndex = 1;
            this.drawBox.TabStop = false;
            this.drawBox.Paint += new System.Windows.Forms.PaintEventHandler(this.drawBox_Paint);
            this.drawBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawBox_MouseDown);
            this.drawBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawBox_MouseMove);
            // 
            // timerFetch
            // 
            this.timerFetch.Interval = 500;
            this.timerFetch.Tick += new System.EventHandler(this.timerFetch_Tick);
            // 
            // contextDinMenu
            // 
            this.contextDinMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miPassport,
            this.miFindInDataBase,
            this.miQuit,
            this.miTrendSplit,
            this.miTrend});
            this.contextDinMenu.Name = "contextMenu";
            this.contextDinMenu.Size = new System.Drawing.Size(197, 120);
            // 
            // miPassport
            // 
            this.miPassport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.miPassport.Name = "miPassport";
            this.miPassport.Size = new System.Drawing.Size(196, 22);
            this.miPassport.Text = "Паспорт для ...";
            this.miPassport.Click += new System.EventHandler(this.miPassport_Click);
            // 
            // miFindInDataBase
            // 
            this.miFindInDataBase.Name = "miFindInDataBase";
            this.miFindInDataBase.Size = new System.Drawing.Size(194, 22);
            this.miFindInDataBase.Text = "Найти в базе данных...";
            this.miFindInDataBase.Click += new System.EventHandler(this.miFindInDataBase_Click);
            // 
            // miQuit
            // 
            this.miQuit.Name = "miQuit";
            this.miQuit.Size = new System.Drawing.Size(194, 22);
            this.miQuit.Text = "Квитировать";
            this.miQuit.Click += new System.EventHandler(this.miQuit_Click);
            // 
            // miTrendSplit
            // 
            this.miTrendSplit.Name = "miTrendSplit";
            this.miTrendSplit.Size = new System.Drawing.Size(191, 6);
            // 
            // miTrend
            // 
            this.miTrend.Name = "miTrend";
            this.miTrend.Size = new System.Drawing.Size(194, 22);
            this.miTrend.Text = "Тренд";
            this.miTrend.Click += new System.EventHandler(this.miTrend_Click);
            // 
            // backgroundBaseReload
            // 
            this.backgroundBaseReload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundBaseReload_DoWork);
            this.backgroundBaseReload.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundBaseReload_RunWorkerCompleted);
            // 
            // backgroundFetch
            // 
            this.backgroundFetch.WorkerReportsProgress = true;
            this.backgroundFetch.WorkerSupportsCancellation = true;
            this.backgroundFetch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundFetch_DoWork);
            this.backgroundFetch.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundFetch_ProgressChanged);
            // 
            // contextPageMenu
            // 
            this.contextPageMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ysmiPrint});
            this.contextPageMenu.Name = "contextPageMenu";
            this.contextPageMenu.Size = new System.Drawing.Size(136, 26);
            // 
            // ysmiPrint
            // 
            this.ysmiPrint.Name = "ysmiPrint";
            this.ysmiPrint.Size = new System.Drawing.Size(135, 22);
            this.ysmiPrint.Text = "Печатать...";
            this.ysmiPrint.Click += new System.EventHandler(this.tsmiPrint_Click);
            // 
            // toolTip
            // 
            this.toolTip.UseAnimation = false;
            this.toolTip.UseFading = false;
            // 
            // frmOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 330);
            this.Controls.Add(this.pnlScroll);
            this.KeyPreview = true;
            this.Name = "frmOverview";
            this.ShowInTaskbar = false;
            this.Text = "frmOverview";
            this.Activated += new System.EventHandler(this.frmOverview_Activated);
            this.Deactivate += new System.EventHandler(this.frmOverview_Deactivate);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOverview_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmOverview_KeyUp);
            this.Resize += new System.EventHandler(this.frmOverview_Resize);
            this.pnlScroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).EndInit();
            this.contextDinMenu.ResumeLayout(false);
            this.contextPageMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlScroll;
        private System.Windows.Forms.Timer timerFetch;
        private System.Windows.Forms.ContextMenuStrip contextDinMenu;
        private System.Windows.Forms.ToolStripMenuItem miPassport;
        private System.Windows.Forms.ToolStripMenuItem miQuit;
        private System.ComponentModel.BackgroundWorker backgroundBaseReload;
        private System.Windows.Forms.ToolStripMenuItem miFindInDataBase;
        private System.ComponentModel.BackgroundWorker backgroundFetch;
        private System.Windows.Forms.ContextMenuStrip contextPageMenu;
        private System.Windows.Forms.ToolStripMenuItem ysmiPrint;
        private System.Windows.Forms.ToolStripSeparator miTrendSplit;
        private System.Windows.Forms.ToolStripMenuItem miTrend;
        private System.Windows.Forms.ToolTip toolTip;
        public System.Windows.Forms.PictureBox drawBox;
    }
}