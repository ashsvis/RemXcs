namespace SPT961Emularor
{
    partial class Form1
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
            this.backUpdate = new System.ComponentModel.BackgroundWorker();
            this.lbList = new System.Windows.Forms.ListBox();
            this.lbRecords = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // backUpdate
            // 
            this.backUpdate.WorkerReportsProgress = true;
            this.backUpdate.WorkerSupportsCancellation = true;
            this.backUpdate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backUpdate_DoWork);
            this.backUpdate.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backUpdate_ProgressChanged);
            this.backUpdate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backUpdate_RunWorkerCompleted);
            // 
            // lbList
            // 
            this.lbList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbList.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbList.FormattingEnabled = true;
            this.lbList.ItemHeight = 15;
            this.lbList.Location = new System.Drawing.Point(13, 13);
            this.lbList.Name = "lbList";
            this.lbList.Size = new System.Drawing.Size(74, 244);
            this.lbList.TabIndex = 0;
            this.lbList.SelectedIndexChanged += new System.EventHandler(this.lbList_SelectedIndexChanged);
            // 
            // lbRecords
            // 
            this.lbRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbRecords.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbRecords.FormattingEnabled = true;
            this.lbRecords.ItemHeight = 15;
            this.lbRecords.Location = new System.Drawing.Point(93, 13);
            this.lbRecords.Name = "lbRecords";
            this.lbRecords.Size = new System.Drawing.Size(327, 244);
            this.lbRecords.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 273);
            this.Controls.Add(this.lbRecords);
            this.Controls.Add(this.lbList);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backUpdate;
        private System.Windows.Forms.ListBox lbList;
        private System.Windows.Forms.ListBox lbRecords;
    }
}

