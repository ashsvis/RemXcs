namespace FetchModbus
{
    partial class frmFetchingModbus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFetchingModbus));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miTuning = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miClose = new System.Windows.Forms.ToolStripMenuItem();
            this.contextNotifyIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextNotifyIcon;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Сервер опроса значений  Modbus";
            this.notifyIcon.Visible = true;
            // 
            // contextNotifyIcon
            // 
            this.contextNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miTuning,
            this.toolStripMenuItem1,
            this.miClose});
            this.contextNotifyIcon.Name = "contextMenuStrip1";
            this.contextNotifyIcon.Size = new System.Drawing.Size(214, 54);
            // 
            // miTuning
            // 
            this.miTuning.Name = "miTuning";
            this.miTuning.Size = new System.Drawing.Size(213, 22);
            this.miTuning.Text = "Настроить подключение...";
            this.miTuning.Click += new System.EventHandler(this.miTuning_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(210, 6);
            // 
            // miClose
            // 
            this.miClose.Name = "miClose";
            this.miClose.Size = new System.Drawing.Size(213, 22);
            this.miClose.Text = "Выгрузить";
            this.miClose.Click += new System.EventHandler(this.miClose_Click);
            // 
            // frmFetchingModbus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 39);
            this.ControlBox = false;
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFetchingModbus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сервер опроса значений  Modbus";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFetchingModbus_FormClosing);
            this.Load += new System.EventHandler(this.frmFetchingModbus_Load);
            this.Resize += new System.EventHandler(this.frmFetchingModbus_Resize);
            this.contextNotifyIcon.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem miTuning;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miClose;
    }
}

