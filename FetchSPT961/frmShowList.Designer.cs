namespace FetchSPT961
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
            this.lvItems = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lvItems
            // 
            this.lvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chDesc,
            this.chTime});
            this.lvItems.FullRowSelect = true;
            this.lvItems.LabelWrap = false;
            this.lvItems.Location = new System.Drawing.Point(13, 11);
            this.lvItems.MultiSelect = false;
            this.lvItems.Name = "lvItems";
            this.lvItems.ShowItemToolTips = true;
            this.lvItems.Size = new System.Drawing.Size(647, 187);
            this.lvItems.TabIndex = 3;
            this.lvItems.UseCompatibleStateImageBehavior = false;
            this.lvItems.View = System.Windows.Forms.View.Details;
            // 
            // chName
            // 
            this.chName.Text = "Шифр позиции";
            this.chName.Width = 120;
            // 
            // chDesc
            // 
            this.chDesc.Text = "Дескриптор";
            this.chDesc.Width = 320;
            // 
            // chTime
            // 
            this.chTime.Text = "Время";
            this.chTime.Width = 180;
            // 
            // lbMessage
            // 
            this.lbMessage.Location = new System.Drawing.Point(12, 201);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(648, 14);
            this.lbMessage.TabIndex = 4;
            this.lbMessage.Text = "Нет сообщений";
            // 
            // frmShowList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 226);
            this.Controls.Add(this.lvItems);
            this.Controls.Add(this.lbMessage);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmShowList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сервер опроса СПТ961.2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chDesc;
        private System.Windows.Forms.ColumnHeader chTime;
        public System.Windows.Forms.ListView lvItems;
        public System.Windows.Forms.Label lbMessage;
    }
}