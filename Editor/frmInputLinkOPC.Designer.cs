namespace DataEditor
{
    partial class frmInputLinkOPC
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
            this.tvServers = new System.Windows.Forms.TreeView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEnter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.tbGroup = new System.Windows.Forms.TextBox();
            this.tbItem = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbCDT = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tvServers
            // 
            this.tvServers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvServers.HideSelection = false;
            this.tvServers.Location = new System.Drawing.Point(12, 12);
            this.tvServers.Name = "tvServers";
            this.tvServers.ShowNodeToolTips = true;
            this.tvServers.Size = new System.Drawing.Size(424, 199);
            this.tvServers.TabIndex = 0;
            this.tvServers.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvServers_AfterCollapse);
            this.tvServers.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvServers_BeforeExpand);
            this.tvServers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvServers_AfterSelect);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(361, 305);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnEnter
            // 
            this.btnEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnter.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnEnter.Enabled = false;
            this.btnEnter.Location = new System.Drawing.Point(280, 305);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(75, 23);
            this.btnEnter.TabIndex = 4;
            this.btnEnter.Text = "Ввод";
            this.btnEnter.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "Сервер";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 248);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "Группа";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 277);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 14);
            this.label3.TabIndex = 7;
            this.label3.Text = "Параметр";
            // 
            // tbServer
            // 
            this.tbServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbServer.Location = new System.Drawing.Point(84, 217);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(352, 22);
            this.tbServer.TabIndex = 1;
            this.tbServer.TextChanged += new System.EventHandler(this.tbServer_TextChanged);
            // 
            // tbGroup
            // 
            this.tbGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbGroup.Location = new System.Drawing.Point(84, 245);
            this.tbGroup.Name = "tbGroup";
            this.tbGroup.Size = new System.Drawing.Size(352, 22);
            this.tbGroup.TabIndex = 2;
            this.tbGroup.TextChanged += new System.EventHandler(this.tbServer_TextChanged);
            // 
            // tbItem
            // 
            this.tbItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbItem.Location = new System.Drawing.Point(84, 274);
            this.tbItem.Name = "tbItem";
            this.tbItem.Size = new System.Drawing.Size(352, 22);
            this.tbItem.TabIndex = 3;
            this.tbItem.TextChanged += new System.EventHandler(this.tbServer_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 305);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "Каноничекий тип:";
            // 
            // lbCDT
            // 
            this.lbCDT.AutoSize = true;
            this.lbCDT.Location = new System.Drawing.Point(132, 305);
            this.lbCDT.Name = "lbCDT";
            this.lbCDT.Size = new System.Drawing.Size(49, 14);
            this.lbCDT.TabIndex = 7;
            this.lbCDT.Text = "(пусто)";
            // 
            // frmInputLinkOPC
            // 
            this.AcceptButton = this.btnEnter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(448, 340);
            this.Controls.Add(this.tbItem);
            this.Controls.Add(this.tbGroup);
            this.Controls.Add(this.tbServer);
            this.Controls.Add(this.lbCDT);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.tvServers);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInputLinkOPC";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Привязка данных OPC сервера";
            this.Load += new System.EventHandler(this.frmInputLinkOPC_Load);
            this.Shown += new System.EventHandler(this.frmInputLinkOPC_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvServers;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.TextBox tbGroup;
        private System.Windows.Forms.TextBox tbItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbCDT;
    }
}