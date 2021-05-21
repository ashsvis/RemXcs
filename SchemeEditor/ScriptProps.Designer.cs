namespace SchemeEditor
{
    partial class ScriptProps
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tbScriptText = new System.Windows.Forms.TextBox();
            this.lbScriptErrors = new System.Windows.Forms.ListBox();
            this.btnCheckCompile = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tbScriptText);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lbScriptErrors);
            this.splitContainer1.Size = new System.Drawing.Size(908, 380);
            this.splitContainer1.SplitterDistance = 263;
            this.splitContainer1.TabIndex = 1;
            // 
            // tbScriptText
            // 
            this.tbScriptText.AcceptsReturn = true;
            this.tbScriptText.AcceptsTab = true;
            this.tbScriptText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbScriptText.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbScriptText.Location = new System.Drawing.Point(0, 0);
            this.tbScriptText.Multiline = true;
            this.tbScriptText.Name = "tbScriptText";
            this.tbScriptText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbScriptText.Size = new System.Drawing.Size(908, 263);
            this.tbScriptText.TabIndex = 1;
            this.tbScriptText.WordWrap = false;
            // 
            // lbScriptErrors
            // 
            this.lbScriptErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbScriptErrors.FormattingEnabled = true;
            this.lbScriptErrors.Location = new System.Drawing.Point(0, 0);
            this.lbScriptErrors.Name = "lbScriptErrors";
            this.lbScriptErrors.Size = new System.Drawing.Size(908, 113);
            this.lbScriptErrors.TabIndex = 0;
            // 
            // btnCheckCompile
            // 
            this.btnCheckCompile.Location = new System.Drawing.Point(667, 398);
            this.btnCheckCompile.Name = "btnCheckCompile";
            this.btnCheckCompile.Size = new System.Drawing.Size(85, 26);
            this.btnCheckCompile.TabIndex = 2;
            this.btnCheckCompile.Text = "Проверить";
            this.btnCheckCompile.UseVisualStyleBackColor = true;
            this.btnCheckCompile.Click += new System.EventHandler(this.btnCheckCompile_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(758, 398);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(78, 26);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(842, 398);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 26);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // ScriptProps
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(932, 436);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCheckCompile);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScriptProps";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Скрипт";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tbScriptText;
        private System.Windows.Forms.ListBox lbScriptErrors;
        private System.Windows.Forms.Button btnCheckCompile;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}