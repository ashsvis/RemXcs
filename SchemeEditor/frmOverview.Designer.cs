namespace SchemeEditor
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
            this.pnlScrollBox = new System.Windows.Forms.Panel();
            this.drawBox = new System.Windows.Forms.PictureBox();
            this.pnlScrollBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlScrollBox
            // 
            this.pnlScrollBox.AutoScroll = true;
            this.pnlScrollBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlScrollBox.Controls.Add(this.drawBox);
            this.pnlScrollBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScrollBox.Location = new System.Drawing.Point(0, 0);
            this.pnlScrollBox.Name = "pnlScrollBox";
            this.pnlScrollBox.Size = new System.Drawing.Size(612, 249);
            this.pnlScrollBox.TabIndex = 3;
            // 
            // drawBox
            // 
            this.drawBox.BackColor = System.Drawing.Color.Silver;
            this.drawBox.Location = new System.Drawing.Point(0, 0);
            this.drawBox.Name = "drawBox";
            this.drawBox.Size = new System.Drawing.Size(607, 243);
            this.drawBox.TabIndex = 2;
            this.drawBox.TabStop = false;
            this.drawBox.Paint += new System.Windows.Forms.PaintEventHandler(this.drawBox_Paint);
            // 
            // frmOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 249);
            this.Controls.Add(this.pnlScrollBox);
            this.Name = "frmOverview";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Общий вид";
            this.Activated += new System.EventHandler(this.frmOverview_Activated);
            this.Load += new System.EventHandler(this.frmOverview_Load);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.drawBox_MouseWheel);
            this.pnlScrollBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlScrollBox;
        private System.Windows.Forms.PictureBox drawBox;
    }
}