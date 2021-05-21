namespace SyncServer
{
    partial class frmShowList
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
            this.lbMessage = new System.Windows.Forms.Label();
            this.lvStatus = new BaseServer.ListViewEx();
            this.chTableName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPosition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSnapTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvPopup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miUpdateTableAll = new System.Windows.Forms.ToolStripMenuItem();
            this.lvPopup.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbMessage
            // 
            this.lbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMessage.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbMessage.Location = new System.Drawing.Point(12, 9);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(567, 18);
            this.lbMessage.TabIndex = 4;
            this.lbMessage.Text = "Нет сообщений";
            // 
            // lvStatus
            // 
            this.lvStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTableName,
            this.chPosition,
            this.chStatus,
            this.chSnapTime});
            this.lvStatus.FullRowSelect = true;
            this.lvStatus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvStatus.Location = new System.Drawing.Point(15, 30);
            this.lvStatus.MultiSelect = false;
            this.lvStatus.Name = "lvStatus";
            this.lvStatus.ShowGroups = false;
            this.lvStatus.ShowItemToolTips = true;
            this.lvStatus.Size = new System.Drawing.Size(564, 122);
            this.lvStatus.TabIndex = 6;
            this.lvStatus.UseCompatibleStateImageBehavior = false;
            this.lvStatus.View = System.Windows.Forms.View.Details;
            this.lvStatus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvStatus_MouseDown);
            // 
            // chTableName
            // 
            this.chTableName.Text = "Таблица";
            this.chTableName.Width = 110;
            // 
            // chPosition
            // 
            this.chPosition.Text = "Позиция";
            this.chPosition.Width = 140;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Статус синхронизации";
            this.chStatus.Width = 150;
            // 
            // chSnapTime
            // 
            this.chSnapTime.Text = "Время";
            this.chSnapTime.Width = 140;
            // 
            // lvPopup
            // 
            this.lvPopup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miUpdateTableAll});
            this.lvPopup.Name = "lvPopup";
            this.lvPopup.Size = new System.Drawing.Size(229, 48);
            // 
            // miUpdateTableAll
            // 
            this.miUpdateTableAll.Name = "miUpdateTableAll";
            this.miUpdateTableAll.Size = new System.Drawing.Size(228, 22);
            this.miUpdateTableAll.Text = "Обновить таблицу полностью";
            this.miUpdateTableAll.Click += new System.EventHandler(this.miUpdateTableAll_Click);
            // 
            // frmShowList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 164);
            this.Controls.Add(this.lvStatus);
            this.Controls.Add(this.lbMessage);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmShowList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сервер репликации баз данных";
            this.lvPopup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.ColumnHeader chTableName;
        private System.Windows.Forms.ColumnHeader chStatus;
        public BaseServer.ListViewEx lvStatus;
        private System.Windows.Forms.ColumnHeader chSnapTime;
        private System.Windows.Forms.ColumnHeader chPosition;
        private System.Windows.Forms.ContextMenuStrip lvPopup;
        private System.Windows.Forms.ToolStripMenuItem miUpdateTableAll;
    }
}