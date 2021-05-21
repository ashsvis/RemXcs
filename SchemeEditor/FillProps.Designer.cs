namespace SchemeEditor
{
    partial class FillProps
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FillProps));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbTrasparent = new System.Windows.Forms.Label();
            this.tbTrasparent = new System.Windows.Forms.TrackBar();
            this.cbColorPattern = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPattern = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbColor = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dlgSelectColor = new System.Windows.Forms.ColorDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTrasparent)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbTrasparent);
            this.groupBox1.Controls.Add(this.tbTrasparent);
            this.groupBox1.Controls.Add(this.cbColorPattern);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbPattern);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbColor);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // lbTrasparent
            // 
            resources.ApplyResources(this.lbTrasparent, "lbTrasparent");
            this.lbTrasparent.Name = "lbTrasparent";
            // 
            // tbTrasparent
            // 
            resources.ApplyResources(this.tbTrasparent, "tbTrasparent");
            this.tbTrasparent.LargeChange = 10;
            this.tbTrasparent.Maximum = 255;
            this.tbTrasparent.Name = "tbTrasparent";
            this.tbTrasparent.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbTrasparent.Scroll += new System.EventHandler(this.tbTrasparent_Scroll);
            // 
            // cbColorPattern
            // 
            this.cbColorPattern.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbColorPattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbColorPattern, "cbColorPattern");
            this.cbColorPattern.FormattingEnabled = true;
            this.cbColorPattern.Name = "cbColorPattern";
            this.cbColorPattern.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbColor_DrawItem);
            this.cbColorPattern.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
            this.cbColorPattern.SelectionChangeCommitted += new System.EventHandler(this.cbColor_SelectionChangeCommitted);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cbPattern
            // 
            this.cbPattern.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbPattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbPattern, "cbPattern");
            this.cbPattern.FormattingEnabled = true;
            this.cbPattern.Name = "cbPattern";
            this.cbPattern.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbPattern_DrawItem);
            this.cbPattern.SelectionChangeCommitted += new System.EventHandler(this.cbPattern_SelectionChangeCommitted);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cbColor
            // 
            this.cbColor.BackColor = System.Drawing.SystemColors.Window;
            this.cbColor.DisplayMember = "Color";
            this.cbColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbColor, "cbColor");
            this.cbColor.FormattingEnabled = true;
            this.cbColor.Name = "cbColor";
            this.cbColor.ValueMember = "Color";
            this.cbColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbColor_DrawItem);
            this.cbColor.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
            this.cbColor.SelectionChangeCommitted += new System.EventHandler(this.cbColor_SelectionChangeCommitted);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pbPreview);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // pbPreview
            // 
            this.pbPreview.BackgroundImage = global::SchemeEditor.Properties.Resources.transparent;
            resources.ApplyResources(this.pbPreview, "pbPreview");
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.TabStop = false;
            this.pbPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pbPreview_Paint);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dlgSelectColor
            // 
            this.dlgSelectColor.Color = System.Drawing.Color.White;
            this.dlgSelectColor.FullOpen = true;
            // 
            // FillProps
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FillProps";
            this.ShowInTaskbar = false;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTrasparent)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbColorPattern;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbPattern;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ColorDialog dlgSelectColor;
        private System.Windows.Forms.ComboBox cbColor;
        private System.Windows.Forms.TrackBar tbTrasparent;
        private System.Windows.Forms.Label lbTrasparent;
    }
}