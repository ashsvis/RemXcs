namespace RemXcs
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.backHiAlarm = new System.ComponentModel.BackgroundWorker();
            this.backLoAlarm = new System.ComponentModel.BackgroundWorker();
            this.backEuAlarm = new System.ComponentModel.BackgroundWorker();
            this.timerFetch = new System.Windows.Forms.Timer(this.components);
            this.backImLive = new System.ComponentModel.BackgroundWorker();
            this.backCheckCommand = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // timerClock
            // 
            this.timerClock.Interval = 10000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // backHiAlarm
            // 
            this.backHiAlarm.WorkerSupportsCancellation = true;
            this.backHiAlarm.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backHiAlarm_DoWork);
            // 
            // backLoAlarm
            // 
            this.backLoAlarm.WorkerSupportsCancellation = true;
            this.backLoAlarm.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backLoAlarm_DoWork);
            // 
            // backEuAlarm
            // 
            this.backEuAlarm.WorkerSupportsCancellation = true;
            this.backEuAlarm.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backEuAlarm_DoWork);
            // 
            // timerFetch
            // 
            this.timerFetch.Interval = 500;
            this.timerFetch.Tick += new System.EventHandler(this.timerFetch_Tick);
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
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 18);
            this.ControlBox = false;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Рабочая станция";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Activated += new System.EventHandler(this.frmMain_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerClock;
        private System.ComponentModel.BackgroundWorker backHiAlarm;
        private System.ComponentModel.BackgroundWorker backLoAlarm;
        private System.ComponentModel.BackgroundWorker backEuAlarm;
        private System.Windows.Forms.Timer timerFetch;
        private System.ComponentModel.BackgroundWorker backImLive;
        private System.ComponentModel.BackgroundWorker backCheckCommand;
    }
}

