namespace DataEditor
{
    partial class frmDataEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataEditor));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.miBaseEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.miTrendGroups = new System.Windows.Forms.ToolStripMenuItem();
            this.miTableGroups = new System.Windows.Forms.ToolStripMenuItem();
            this.miSchemeEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.miReportEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.miUserListEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miSaveAllDataInFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.miRestoreServerDataFromFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miTuningLink = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.miHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.miVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.miCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.stpStatus = new System.Windows.Forms.StatusStrip();
            this.tlbStatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrStatusClear = new System.Windows.Forms.Timer(this.components);
            this.tmrLive = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.stpStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miEditor,
            this.miWindows});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.miWindows;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(703, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miEditor
            // 
            this.miEditor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miBaseEditor,
            this.miTrendGroups,
            this.miTableGroups,
            this.miSchemeEditor,
            this.miReportEditor,
            this.miUserListEditor,
            this.toolStripMenuItem1,
            this.miSaveAllDataInFiles,
            this.miRestoreServerDataFromFiles,
            this.toolStripMenuItem2,
            this.miTuningLink,
            this.miExit});
            this.miEditor.Name = "miEditor";
            this.miEditor.Size = new System.Drawing.Size(72, 20);
            this.miEditor.Text = "Редактор";
            // 
            // miBaseEditor
            // 
            this.miBaseEditor.Image = global::DataEditor.Properties.Resources.baseedit;
            this.miBaseEditor.Name = "miBaseEditor";
            this.miBaseEditor.Size = new System.Drawing.Size(310, 22);
            this.miBaseEditor.Text = "База данных";
            this.miBaseEditor.Click += new System.EventHandler(this.miBaseEditor_Click);
            // 
            // miTrendGroups
            // 
            this.miTrendGroups.Image = global::DataEditor.Properties.Resources.groups;
            this.miTrendGroups.Name = "miTrendGroups";
            this.miTrendGroups.Size = new System.Drawing.Size(310, 22);
            this.miTrendGroups.Text = "Группы трендов ";
            this.miTrendGroups.Click += new System.EventHandler(this.miTrendGroups_Click);
            // 
            // miTableGroups
            // 
            this.miTableGroups.Image = global::DataEditor.Properties.Resources.groups;
            this.miTableGroups.Name = "miTableGroups";
            this.miTableGroups.Size = new System.Drawing.Size(310, 22);
            this.miTableGroups.Text = "Группы таблиц ";
            this.miTableGroups.Click += new System.EventHandler(this.miTableGroups_Click);
            // 
            // miSchemeEditor
            // 
            this.miSchemeEditor.Image = global::DataEditor.Properties.Resources.schemeedit;
            this.miSchemeEditor.Name = "miSchemeEditor";
            this.miSchemeEditor.Size = new System.Drawing.Size(310, 22);
            this.miSchemeEditor.Text = "Мнемосхемы";
            this.miSchemeEditor.Click += new System.EventHandler(this.miSchemeEditor_Click);
            // 
            // miReportEditor
            // 
            this.miReportEditor.Image = global::DataEditor.Properties.Resources.reportedit;
            this.miReportEditor.Name = "miReportEditor";
            this.miReportEditor.Size = new System.Drawing.Size(310, 22);
            this.miReportEditor.Text = "Отчёты";
            this.miReportEditor.Click += new System.EventHandler(this.miReportEditor_Click);
            // 
            // miUserListEditor
            // 
            this.miUserListEditor.Image = global::DataEditor.Properties.Resources.usersedit;
            this.miUserListEditor.Name = "miUserListEditor";
            this.miUserListEditor.Size = new System.Drawing.Size(310, 22);
            this.miUserListEditor.Text = "Список пользователей";
            this.miUserListEditor.Click += new System.EventHandler(this.miUserListEditor_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(307, 6);
            // 
            // miSaveAllDataInFiles
            // 
            this.miSaveAllDataInFiles.Name = "miSaveAllDataInFiles";
            this.miSaveAllDataInFiles.Size = new System.Drawing.Size(310, 22);
            this.miSaveAllDataInFiles.Text = "Сохранить все данные в файлах";
            this.miSaveAllDataInFiles.Click += new System.EventHandler(this.miSaveAllDataInFiles_Click);
            // 
            // miRestoreServerDataFromFiles
            // 
            this.miRestoreServerDataFromFiles.Name = "miRestoreServerDataFromFiles";
            this.miRestoreServerDataFromFiles.Size = new System.Drawing.Size(310, 22);
            this.miRestoreServerDataFromFiles.Text = "Восстановить данные сервера из файлов";
            this.miRestoreServerDataFromFiles.Click += new System.EventHandler(this.miRestoreServerDataFromFiles_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(307, 6);
            // 
            // miTuningLink
            // 
            this.miTuningLink.Name = "miTuningLink";
            this.miTuningLink.Size = new System.Drawing.Size(310, 22);
            this.miTuningLink.Text = "Настройка...";
            this.miTuningLink.Click += new System.EventHandler(this.miTuningLink_Click);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(310, 22);
            this.miExit.Text = "Выход";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miWindows
            // 
            this.miWindows.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miHorizontal,
            this.miVertical,
            this.miCascade});
            this.miWindows.Name = "miWindows";
            this.miWindows.Size = new System.Drawing.Size(47, 20);
            this.miWindows.Text = "Окна";
            // 
            // miHorizontal
            // 
            this.miHorizontal.Name = "miHorizontal";
            this.miHorizontal.Size = new System.Drawing.Size(159, 22);
            this.miHorizontal.Text = "Горизонтально";
            this.miHorizontal.Click += new System.EventHandler(this.miHorizontal_Click);
            // 
            // miVertical
            // 
            this.miVertical.Name = "miVertical";
            this.miVertical.Size = new System.Drawing.Size(159, 22);
            this.miVertical.Text = "Вертикально";
            this.miVertical.Click += new System.EventHandler(this.miVertical_Click);
            // 
            // miCascade
            // 
            this.miCascade.Name = "miCascade";
            this.miCascade.Size = new System.Drawing.Size(159, 22);
            this.miCascade.Text = "Каскадом";
            this.miCascade.Click += new System.EventHandler(this.miCascade_Click);
            // 
            // stpStatus
            // 
            this.stpStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbStatusMessage});
            this.stpStatus.Location = new System.Drawing.Point(0, 232);
            this.stpStatus.Name = "stpStatus";
            this.stpStatus.Size = new System.Drawing.Size(703, 22);
            this.stpStatus.TabIndex = 2;
            this.stpStatus.Text = "statusStrip1";
            // 
            // tlbStatusMessage
            // 
            this.tlbStatusMessage.Name = "tlbStatusMessage";
            this.tlbStatusMessage.Size = new System.Drawing.Size(97, 17);
            this.tlbStatusMessage.Text = "Инициализация...";
            // 
            // tmrStatusClear
            // 
            this.tmrStatusClear.Interval = 10000;
            this.tmrStatusClear.Tick += new System.EventHandler(this.tmrStatusClear_Tick);
            // 
            // tmrLive
            // 
            this.tmrLive.Interval = 10000;
            this.tmrLive.Tick += new System.EventHandler(this.tmrLive_Tick);
            // 
            // frmDataEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 254);
            this.Controls.Add(this.stpStatus);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmDataEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Редактор ресурсов RemX";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDataEditor_FormClosing);
            this.Load += new System.EventHandler(this.frmDataEditor_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.stpStatus.ResumeLayout(false);
            this.stpStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miEditor;
        private System.Windows.Forms.ToolStripMenuItem miBaseEditor;
        private System.Windows.Forms.ToolStripMenuItem miTrendGroups;
        private System.Windows.Forms.ToolStripMenuItem miSchemeEditor;
        private System.Windows.Forms.ToolStripMenuItem miUserListEditor;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem miWindows;
        private System.Windows.Forms.ToolStripMenuItem miHorizontal;
        private System.Windows.Forms.ToolStripMenuItem miVertical;
        private System.Windows.Forms.ToolStripMenuItem miCascade;
        private System.Windows.Forms.StatusStrip stpStatus;
        private System.Windows.Forms.ToolStripMenuItem miSaveAllDataInFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem miRestoreServerDataFromFiles;
        private System.Windows.Forms.ToolStripStatusLabel tlbStatusMessage;
        private System.Windows.Forms.Timer tmrStatusClear;
        private System.Windows.Forms.ToolStripMenuItem miReportEditor;
        private System.Windows.Forms.Timer tmrLive;
        private System.Windows.Forms.ToolStripMenuItem miTuningLink;
        private System.Windows.Forms.ToolStripMenuItem miTableGroups;
    }
}

