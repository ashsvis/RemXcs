namespace RemXcs
{
    partial class frmPrintPreview
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
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.printPreview = new System.Windows.Forms.PrintPreviewControl();
            this.printDoc = new System.Drawing.Printing.PrintDocument();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbPrintDialog = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tssbZoom = new System.Windows.Forms.ToolStripSplitButton();
            this.tsmZoomAuto = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmZoom25 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmZoom50 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmZoom75 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmZoom100 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmZoom150 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmZoom200 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.printPreview);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(897, 249);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(897, 274);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // printPreview
            // 
            this.printPreview.AutoZoom = false;
            this.printPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printPreview.Document = this.printDoc;
            this.printPreview.Location = new System.Drawing.Point(0, 0);
            this.printPreview.Name = "printPreview";
            this.printPreview.Size = new System.Drawing.Size(897, 249);
            this.printPreview.TabIndex = 1;
            this.printPreview.Zoom = 1D;
            // 
            // printDoc
            // 
            this.printDoc.DocumentName = "1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPrint,
            this.tsbPrintDialog,
            this.toolStripSeparator1,
            this.tssbZoom,
            this.tsbZoomOut,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(897, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            // 
            // tsbPrint
            // 
            this.tsbPrint.Image = global::RemXcs.Properties.Resources.print;
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(64, 22);
            this.tsbPrint.Text = "Печать";
            this.tsbPrint.Click += new System.EventHandler(this.tsbPrint_Click);
            // 
            // tsbPrintDialog
            // 
            this.tsbPrintDialog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrintDialog.Image = global::RemXcs.Properties.Resources.printdialog;
            this.tsbPrintDialog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrintDialog.Name = "tsbPrintDialog";
            this.tsbPrintDialog.Size = new System.Drawing.Size(23, 22);
            this.tsbPrintDialog.Text = "Печать...";
            this.tsbPrintDialog.Click += new System.EventHandler(this.tsbPrintDialog_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tssbZoom
            // 
            this.tssbZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tssbZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmZoomAuto,
            this.tsmZoom25,
            this.tsmZoom50,
            this.tsmZoom75,
            this.tsmZoom100,
            this.tsmZoom150,
            this.tsmZoom200});
            this.tssbZoom.Image = global::RemXcs.Properties.Resources.zoomin;
            this.tssbZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssbZoom.Name = "tssbZoom";
            this.tssbZoom.Size = new System.Drawing.Size(32, 22);
            this.tssbZoom.Text = "Масштаб";
            this.tssbZoom.ButtonClick += new System.EventHandler(this.tssbZoom_ButtonClick);
            // 
            // tsmZoomAuto
            // 
            this.tsmZoomAuto.Name = "tsmZoomAuto";
            this.tsmZoomAuto.Size = new System.Drawing.Size(152, 22);
            this.tsmZoomAuto.Tag = "0";
            this.tsmZoomAuto.Text = "Автоматически";
            this.tsmZoomAuto.Click += new System.EventHandler(this.tsmZoomAuto_Click);
            // 
            // tsmZoom25
            // 
            this.tsmZoom25.Name = "tsmZoom25";
            this.tsmZoom25.Size = new System.Drawing.Size(152, 22);
            this.tsmZoom25.Tag = "25";
            this.tsmZoom25.Text = "25%";
            this.tsmZoom25.Click += new System.EventHandler(this.tsmZoomAuto_Click);
            // 
            // tsmZoom50
            // 
            this.tsmZoom50.Name = "tsmZoom50";
            this.tsmZoom50.Size = new System.Drawing.Size(152, 22);
            this.tsmZoom50.Tag = "50";
            this.tsmZoom50.Text = "50%";
            this.tsmZoom50.Click += new System.EventHandler(this.tsmZoomAuto_Click);
            // 
            // tsmZoom75
            // 
            this.tsmZoom75.Name = "tsmZoom75";
            this.tsmZoom75.Size = new System.Drawing.Size(152, 22);
            this.tsmZoom75.Tag = "75";
            this.tsmZoom75.Text = "75%";
            this.tsmZoom75.Click += new System.EventHandler(this.tsmZoomAuto_Click);
            // 
            // tsmZoom100
            // 
            this.tsmZoom100.Name = "tsmZoom100";
            this.tsmZoom100.Size = new System.Drawing.Size(152, 22);
            this.tsmZoom100.Tag = "100";
            this.tsmZoom100.Text = "100%";
            this.tsmZoom100.Click += new System.EventHandler(this.tsmZoomAuto_Click);
            // 
            // tsmZoom150
            // 
            this.tsmZoom150.Name = "tsmZoom150";
            this.tsmZoom150.Size = new System.Drawing.Size(152, 22);
            this.tsmZoom150.Tag = "150";
            this.tsmZoom150.Text = "150%";
            this.tsmZoom150.Click += new System.EventHandler(this.tsmZoomAuto_Click);
            // 
            // tsmZoom200
            // 
            this.tsmZoom200.Name = "tsmZoom200";
            this.tsmZoom200.Size = new System.Drawing.Size(152, 22);
            this.tsmZoom200.Tag = "200";
            this.tsmZoom200.Text = "200%";
            this.tsmZoom200.Click += new System.EventHandler(this.tsmZoomAuto_Click);
            // 
            // tsbZoomOut
            // 
            this.tsbZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomOut.Image = global::RemXcs.Properties.Resources.zoomout;
            this.tsbZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomOut.Name = "tsbZoomOut";
            this.tsbZoomOut.Size = new System.Drawing.Size(23, 22);
            this.tsbZoomOut.Text = "Уменьшить";
            this.tsbZoomOut.Click += new System.EventHandler(this.tsbZoomOut_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // printDialog
            // 
            this.printDialog.Document = this.printDoc;
            this.printDialog.UseEXDialog = true;
            // 
            // frmPrintPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 274);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "frmPrintPreview";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Предварительный просмотр";
            this.Load += new System.EventHandler(this.frmPrintPreview_Load);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbPrint;
        private System.Windows.Forms.ToolStripButton tsbPrintDialog;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.PrintPreviewControl printPreview;
        public System.Drawing.Printing.PrintDocument printDoc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton tssbZoom;
        private System.Windows.Forms.ToolStripMenuItem tsmZoomAuto;
        private System.Windows.Forms.ToolStripMenuItem tsmZoom25;
        private System.Windows.Forms.ToolStripMenuItem tsmZoom50;
        private System.Windows.Forms.ToolStripMenuItem tsmZoom75;
        private System.Windows.Forms.ToolStripMenuItem tsmZoom100;
        private System.Windows.Forms.ToolStripMenuItem tsmZoom150;
        private System.Windows.Forms.ToolStripMenuItem tsmZoom200;
        private System.Windows.Forms.ToolStripButton tsbZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}