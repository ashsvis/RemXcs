namespace RemXcs
{
    partial class frmUsers
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Диспетчеры");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Операторы");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Прибористы");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Инженеры-технологи");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Инженеры АСУ ТП");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Программисты");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUsers));
            this.btnCancelRegistration = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tlpTable = new System.Windows.Forms.TableLayoutPanel();
            this.tvList = new System.Windows.Forms.TreeView();
            this.imlTree = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tlpTable.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancelRegistration
            // 
            this.btnCancelRegistration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelRegistration.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnCancelRegistration.Enabled = false;
            this.btnCancelRegistration.Location = new System.Drawing.Point(3, 2);
            this.btnCancelRegistration.Name = "btnCancelRegistration";
            this.btnCancelRegistration.Size = new System.Drawing.Size(142, 23);
            this.btnCancelRegistration.TabIndex = 1;
            this.btnCancelRegistration.Text = "Отмена регистрации";
            this.btnCancelRegistration.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(266, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // tlpTable
            // 
            this.tlpTable.AutoSize = true;
            this.tlpTable.ColumnCount = 1;
            this.tlpTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpTable.Controls.Add(this.tvList, 0, 1);
            this.tlpTable.Controls.Add(this.panel1, 0, 2);
            this.tlpTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTable.Location = new System.Drawing.Point(0, 0);
            this.tlpTable.Name = "tlpTable";
            this.tlpTable.RowCount = 3;
            this.tlpTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpTable.Size = new System.Drawing.Size(350, 344);
            this.tlpTable.TabIndex = 3;
            // 
            // tvList
            // 
            this.tvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvList.HideSelection = false;
            this.tvList.ImageIndex = 0;
            this.tvList.ImageList = this.imlTree;
            this.tvList.Location = new System.Drawing.Point(3, 3);
            this.tvList.Name = "tvList";
            treeNode1.Name = "ndDisps";
            treeNode1.Text = "Диспетчеры";
            treeNode2.Name = "ndOpers";
            treeNode2.Text = "Операторы";
            treeNode3.Name = "ndInsts";
            treeNode3.Text = "Прибористы";
            treeNode4.Name = "ndTechs";
            treeNode4.Text = "Инженеры-технологи";
            treeNode5.Name = "ndEngs";
            treeNode5.Text = "Инженеры АСУ ТП";
            treeNode6.Name = "ndProgs";
            treeNode6.Text = "Программисты";
            this.tvList.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            this.tvList.SelectedImageIndex = 0;
            this.tvList.Size = new System.Drawing.Size(344, 304);
            this.tvList.TabIndex = 1;
            this.tvList.DoubleClick += new System.EventHandler(this.tvList_DoubleClick);
            this.tvList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvList_KeyDown);
            // 
            // imlTree
            // 
            this.imlTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTree.ImageStream")));
            this.imlTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imlTree.Images.SetKeyName(0, "users.png");
            this.imlTree.Images.SetKeyName(1, "user.png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancelRegistration);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 313);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 28);
            this.panel1.TabIndex = 2;
            // 
            // frmUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(350, 344);
            this.Controls.Add(this.tlpTable);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUsers";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Регистрация пользователя";
            this.Load += new System.EventHandler(this.frmUsers_Load);
            this.tlpTable.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancelRegistration;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TableLayoutPanel tlpTable;
        private System.Windows.Forms.TreeView tvList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList imlTree;
    }
}

