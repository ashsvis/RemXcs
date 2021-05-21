namespace DataEditor
{
    partial class frmSchemeEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSchemeEditor));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.pnlScroll = new System.Windows.Forms.Panel();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.tbDrawBox = new System.Windows.Forms.TextBox();
            this.drawBox = new System.Windows.Forms.PictureBox();
            this.tsPlugins = new System.Windows.Forms.ToolStrip();
            this.tsbArrow = new System.Windows.Forms.ToolStripButton();
            this.tsFiles = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCut = new System.Windows.Forms.ToolStripButton();
            this.tsbCopy = new System.Windows.Forms.ToolStripButton();
            this.tsbPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFormatCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbRedo = new System.Windows.Forms.ToolStripButton();
            this.exportFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.importFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miProps = new System.Windows.Forms.ToolStripMenuItem();
            this.miDinDouble = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.miDinCut = new System.Windows.Forms.ToolStripMenuItem();
            this.miDinCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miDinPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.miDinDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miDinArrange = new System.Windows.Forms.ToolStripMenuItem();
            this.выравниваниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьРазмерToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.miDinBringToFront = new System.Windows.Forms.ToolStripMenuItem();
            this.miDinSendToBack = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgSelectColor = new System.Windows.Forms.ColorDialog();
            this.dlgSelectImage = new System.Windows.Forms.OpenFileDialog();
            this.contextBackground = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tbPropBackground = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miDeleteScheme = new System.Windows.Forms.ToolStripMenuItem();
            this.miPasteFromBuffer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.pnlScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).BeginInit();
            this.tsPlugins.SuspendLayout();
            this.tsFiles.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.contextBackground.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AutoScroll = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlScroll);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(914, 262);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(914, 287);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsFiles);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsPlugins);
            // 
            // pnlScroll
            // 
            this.pnlScroll.Controls.Add(this.hScrollBar);
            this.pnlScroll.Controls.Add(this.vScrollBar);
            this.pnlScroll.Controls.Add(this.tbDrawBox);
            this.pnlScroll.Controls.Add(this.drawBox);
            this.pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScroll.Location = new System.Drawing.Point(0, 0);
            this.pnlScroll.Name = "pnlScroll";
            this.pnlScroll.Size = new System.Drawing.Size(914, 262);
            this.pnlScroll.TabIndex = 3;
            // 
            // hScrollBar
            // 
            this.hScrollBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar.Location = new System.Drawing.Point(0, 245);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(897, 17);
            this.hScrollBar.TabIndex = 6;
            this.hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            // 
            // vScrollBar
            // 
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar.Location = new System.Drawing.Point(897, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 262);
            this.vScrollBar.TabIndex = 5;
            this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
            // 
            // tbDrawBox
            // 
            this.tbDrawBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDrawBox.Location = new System.Drawing.Point(911, 260);
            this.tbDrawBox.Name = "tbDrawBox";
            this.tbDrawBox.Size = new System.Drawing.Size(100, 22);
            this.tbDrawBox.TabIndex = 3;
            this.tbDrawBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbDrawBox_KeyDown);
            this.tbDrawBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbDrawBox_KeyUp);
            // 
            // drawBox
            // 
            this.drawBox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.drawBox.Location = new System.Drawing.Point(0, 0);
            this.drawBox.Name = "drawBox";
            this.drawBox.Size = new System.Drawing.Size(929, 279);
            this.drawBox.TabIndex = 1;
            this.drawBox.TabStop = false;
            this.drawBox.Paint += new System.Windows.Forms.PaintEventHandler(this.drawBox_Paint);
            this.drawBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawBox_MouseDown);
            this.drawBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawBox_MouseMove);
            this.drawBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawBox_MouseUp);
            // 
            // tsPlugins
            // 
            this.tsPlugins.Dock = System.Windows.Forms.DockStyle.None;
            this.tsPlugins.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbArrow});
            this.tsPlugins.Location = new System.Drawing.Point(325, 0);
            this.tsPlugins.Name = "tsPlugins";
            this.tsPlugins.Size = new System.Drawing.Size(35, 25);
            this.tsPlugins.TabIndex = 0;
            // 
            // tsbArrow
            // 
            this.tsbArrow.Checked = true;
            this.tsbArrow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbArrow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbArrow.Image = global::DataEditor.Properties.Resources.arrow;
            this.tsbArrow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbArrow.Name = "tsbArrow";
            this.tsbArrow.Size = new System.Drawing.Size(23, 22);
            this.tsbArrow.Text = " Выбор элемента";
            this.tsbArrow.Click += new System.EventHandler(this.tsbArrow_Click);
            // 
            // tsFiles
            // 
            this.tsFiles.Dock = System.Windows.Forms.DockStyle.None;
            this.tsFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbOpen,
            this.tsbSave,
            this.toolStripSeparator2,
            this.tsbImport,
            this.tsbExport,
            this.toolStripSeparator3,
            this.tsbCut,
            this.tsbCopy,
            this.tsbPaste,
            this.toolStripSeparator4,
            this.tsbFormatCopy,
            this.toolStripSeparator1,
            this.tsbUndo,
            this.tsbRedo});
            this.tsFiles.Location = new System.Drawing.Point(3, 0);
            this.tsFiles.Name = "tsFiles";
            this.tsFiles.Size = new System.Drawing.Size(289, 25);
            this.tsFiles.TabIndex = 1;
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNew.Image = global::DataEditor.Properties.Resources.newpage;
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(23, 22);
            this.tsbNew.Text = "Новая мнемосхема";
            this.tsbNew.Click += new System.EventHandler(this.tsbNew_Click);
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = global::DataEditor.Properties.Resources.open;
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbOpen.Text = "Открыть...";
            this.tsbOpen.ToolTipText = "Открыть...";
            this.tsbOpen.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Enabled = false;
            this.tsbSave.Image = global::DataEditor.Properties.Resources.save;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Text = "Сохранить";
            this.tsbSave.ToolTipText = "Сохранить";
            this.tsbSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbImport
            // 
            this.tsbImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImport.Image = global::DataEditor.Properties.Resources.import;
            this.tsbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.Size = new System.Drawing.Size(23, 22);
            this.tsbImport.Text = "Восстановить из...";
            this.tsbImport.ToolTipText = "Восстановить мнемосхему из...";
            this.tsbImport.Click += new System.EventHandler(this.tsmiRestoreFrom_Click);
            // 
            // tsbExport
            // 
            this.tsbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExport.Image = global::DataEditor.Properties.Resources.export;
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(23, 22);
            this.tsbExport.Text = "Экспортировать...";
            this.tsbExport.Click += new System.EventHandler(this.tsmiSaveAs_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbCut
            // 
            this.tsbCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCut.Enabled = false;
            this.tsbCut.Image = global::DataEditor.Properties.Resources.cut;
            this.tsbCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCut.Name = "tsbCut";
            this.tsbCut.Size = new System.Drawing.Size(23, 22);
            this.tsbCut.Text = "Вырезать";
            this.tsbCut.Click += new System.EventHandler(this.tsbCut_Click);
            // 
            // tsbCopy
            // 
            this.tsbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCopy.Enabled = false;
            this.tsbCopy.Image = global::DataEditor.Properties.Resources.copy;
            this.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopy.Name = "tsbCopy";
            this.tsbCopy.Size = new System.Drawing.Size(23, 22);
            this.tsbCopy.Text = "Копировать";
            this.tsbCopy.Click += new System.EventHandler(this.tsbCopy_Click);
            // 
            // tsbPaste
            // 
            this.tsbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPaste.Enabled = false;
            this.tsbPaste.Image = global::DataEditor.Properties.Resources.paste;
            this.tsbPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPaste.Name = "tsbPaste";
            this.tsbPaste.Size = new System.Drawing.Size(23, 22);
            this.tsbPaste.Text = "Вставить";
            this.tsbPaste.Click += new System.EventHandler(this.tsbPaste_Click);
            this.tsbPaste.EnabledChanged += new System.EventHandler(this.tsbPaste_EnabledChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbFormatCopy
            // 
            this.tsbFormatCopy.CheckOnClick = true;
            this.tsbFormatCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFormatCopy.DoubleClickEnabled = true;
            this.tsbFormatCopy.Image = global::DataEditor.Properties.Resources.CopyProps;
            this.tsbFormatCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFormatCopy.Name = "tsbFormatCopy";
            this.tsbFormatCopy.Size = new System.Drawing.Size(23, 22);
            this.tsbFormatCopy.Text = "Копировать формат";
            this.tsbFormatCopy.CheckedChanged += new System.EventHandler(this.tsbFormatCopy_CheckedChanged);
            this.tsbFormatCopy.Click += new System.EventHandler(this.tsbFormatCopy_Click);
            this.tsbFormatCopy.DoubleClick += new System.EventHandler(this.tsbFormatCopy_DoubleClick);
            this.tsbFormatCopy.EnabledChanged += new System.EventHandler(this.tsbFormatCopy_EnabledChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbUndo
            // 
            this.tsbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUndo.Enabled = false;
            this.tsbUndo.Image = global::DataEditor.Properties.Resources.undo;
            this.tsbUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUndo.Name = "tsbUndo";
            this.tsbUndo.Size = new System.Drawing.Size(23, 22);
            this.tsbUndo.Text = "Отменить";
            this.tsbUndo.Click += new System.EventHandler(this.tsbUndo_Click);
            // 
            // tsbRedo
            // 
            this.tsbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRedo.Enabled = false;
            this.tsbRedo.Image = global::DataEditor.Properties.Resources.redo;
            this.tsbRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRedo.Name = "tsbRedo";
            this.tsbRedo.Size = new System.Drawing.Size(23, 22);
            this.tsbRedo.Text = "Вернуть";
            this.tsbRedo.Click += new System.EventHandler(this.tsbRedo_Click);
            // 
            // exportFileDialog
            // 
            this.exportFileDialog.DefaultExt = "ini";
            this.exportFileDialog.Filter = "*.ini|*.ini";
            // 
            // importFileDialog
            // 
            this.importFileDialog.DefaultExt = "ini";
            this.importFileDialog.Filter = "*.ini|*.ini";
            // 
            // contextMenu
            // 
            this.contextMenu.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miProps,
            this.miDinDouble,
            this.toolStripMenuItem3,
            this.miDinCut,
            this.miDinCopy,
            this.miDinPaste,
            this.miDinDelete,
            this.toolStripMenuItem1,
            this.miDinArrange,
            this.выравниваниеToolStripMenuItem,
            this.изменитьРазмерToolStripMenuItem,
            this.toolStripMenuItem4,
            this.miDinBringToFront,
            this.miDinSendToBack});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(190, 264);
            // 
            // miProps
            // 
            this.miProps.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.miProps.Name = "miProps";
            this.miProps.Size = new System.Drawing.Size(189, 22);
            this.miProps.Text = "Свойства...";
            this.miProps.Click += new System.EventHandler(this.miShowProps_Click);
            // 
            // miDinDouble
            // 
            this.miDinDouble.Name = "miDinDouble";
            this.miDinDouble.Size = new System.Drawing.Size(189, 22);
            this.miDinDouble.Text = "Дублировать";
            this.miDinDouble.Click += new System.EventHandler(this.miDinDouble_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(186, 6);
            // 
            // miDinCut
            // 
            this.miDinCut.Name = "miDinCut";
            this.miDinCut.Size = new System.Drawing.Size(189, 22);
            this.miDinCut.Text = "Вырезать";
            this.miDinCut.Click += new System.EventHandler(this.tsbCut_Click);
            // 
            // miDinCopy
            // 
            this.miDinCopy.Name = "miDinCopy";
            this.miDinCopy.Size = new System.Drawing.Size(189, 22);
            this.miDinCopy.Text = "Копировать";
            this.miDinCopy.Click += new System.EventHandler(this.tsbCopy_Click);
            // 
            // miDinPaste
            // 
            this.miDinPaste.Enabled = false;
            this.miDinPaste.Name = "miDinPaste";
            this.miDinPaste.Size = new System.Drawing.Size(189, 22);
            this.miDinPaste.Text = "Вставить из буфера";
            this.miDinPaste.Click += new System.EventHandler(this.tsbPaste_Click);
            // 
            // miDinDelete
            // 
            this.miDinDelete.Name = "miDinDelete";
            this.miDinDelete.Size = new System.Drawing.Size(189, 22);
            this.miDinDelete.Text = "Удалить";
            this.miDinDelete.Click += new System.EventHandler(this.miDinDelete_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(186, 6);
            // 
            // miDinArrange
            // 
            this.miDinArrange.Name = "miDinArrange";
            this.miDinArrange.Size = new System.Drawing.Size(189, 22);
            this.miDinArrange.Text = "Выровнять по сетке";
            // 
            // выравниваниеToolStripMenuItem
            // 
            this.выравниваниеToolStripMenuItem.Name = "выравниваниеToolStripMenuItem";
            this.выравниваниеToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.выравниваниеToolStripMenuItem.Text = "Выравнивание...";
            // 
            // изменитьРазмерToolStripMenuItem
            // 
            this.изменитьРазмерToolStripMenuItem.Name = "изменитьРазмерToolStripMenuItem";
            this.изменитьРазмерToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.изменитьРазмерToolStripMenuItem.Text = "Изменить размер...";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(186, 6);
            // 
            // miDinBringToFront
            // 
            this.miDinBringToFront.Name = "miDinBringToFront";
            this.miDinBringToFront.Size = new System.Drawing.Size(189, 22);
            this.miDinBringToFront.Text = "Выдвинуть вперёд";
            this.miDinBringToFront.Click += new System.EventHandler(this.miBringToFront_Click);
            // 
            // miDinSendToBack
            // 
            this.miDinSendToBack.Name = "miDinSendToBack";
            this.miDinSendToBack.Size = new System.Drawing.Size(189, 22);
            this.miDinSendToBack.Text = "Поместить назад";
            this.miDinSendToBack.Click += new System.EventHandler(this.miSendToBack_Click);
            // 
            // dlgSelectColor
            // 
            this.dlgSelectColor.AnyColor = true;
            this.dlgSelectColor.FullOpen = true;
            // 
            // dlgSelectImage
            // 
            this.dlgSelectImage.Filter = "*.png;*.emf;*.jpg;*.bmp|*.png;*.emf;*.jpg;*.bmp";
            this.dlgSelectImage.InitialDirectory = ".\\\\images";
            // 
            // contextBackground
            // 
            this.contextBackground.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbPropBackground,
            this.toolStripMenuItem2,
            this.miPasteFromBuffer,
            this.toolStripSeparator5,
            this.miDeleteScheme});
            this.contextBackground.Name = "contextBackground";
            this.contextBackground.Size = new System.Drawing.Size(192, 104);
            // 
            // tbPropBackground
            // 
            this.tbPropBackground.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tbPropBackground.Name = "tbPropBackground";
            this.tbPropBackground.Size = new System.Drawing.Size(191, 22);
            this.tbPropBackground.Text = "Свойства...";
            this.tbPropBackground.Click += new System.EventHandler(this.tbPropBackground_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(188, 6);
            // 
            // miDeleteScheme
            // 
            this.miDeleteScheme.Name = "miDeleteScheme";
            this.miDeleteScheme.Size = new System.Drawing.Size(191, 22);
            this.miDeleteScheme.Text = "Удалить мнемосхему";
            this.miDeleteScheme.Click += new System.EventHandler(this.miDeleteScheme_Click);
            // 
            // miPasteFromBuffer
            // 
            this.miPasteFromBuffer.Name = "miPasteFromBuffer";
            this.miPasteFromBuffer.Size = new System.Drawing.Size(191, 22);
            this.miPasteFromBuffer.Text = "Вставить из буфера";
            this.miPasteFromBuffer.Visible = false;
            this.miPasteFromBuffer.Click += new System.EventHandler(this.tsbPaste_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(188, 6);
            this.toolStripSeparator5.Visible = false;
            // 
            // frmSchemeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 287);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmSchemeEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Редактор мнемосхем";
            this.Activated += new System.EventHandler(this.frmSchemeEditor_Activated);
            this.Load += new System.EventHandler(this.frmScheme_Load);
            this.Resize += new System.EventHandler(this.frmSchemeEditor_Resize);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.pnlScroll.ResumeLayout(false);
            this.pnlScroll.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).EndInit();
            this.tsPlugins.ResumeLayout(false);
            this.tsPlugins.PerformLayout();
            this.tsFiles.ResumeLayout(false);
            this.tsFiles.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.contextBackground.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip tsPlugins;
        private System.Windows.Forms.ToolStripButton tsbArrow;
        private System.Windows.Forms.ToolStrip tsFiles;
        private System.Windows.Forms.SaveFileDialog exportFileDialog;
        private System.Windows.Forms.OpenFileDialog importFileDialog;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem miDinBringToFront;
        private System.Windows.Forms.ToolStripMenuItem miDinSendToBack;
        private System.Windows.Forms.ColorDialog dlgSelectColor;
        private System.Windows.Forms.ToolStripMenuItem miProps;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem miDinDouble;
        private System.Windows.Forms.ToolStripMenuItem miDinCut;
        private System.Windows.Forms.ToolStripMenuItem miDinCopy;
        private System.Windows.Forms.ToolStripMenuItem miDinPaste;
        private System.Windows.Forms.ToolStripMenuItem miDinDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miDinArrange;
        private System.Windows.Forms.ToolStripMenuItem выравниваниеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьРазмерToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.OpenFileDialog dlgSelectImage;
        private System.Windows.Forms.Panel pnlScroll;
        private System.Windows.Forms.PictureBox drawBox;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbImport;
        private System.Windows.Forms.ToolStripButton tsbExport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbCopy;
        private System.Windows.Forms.ToolStripButton tsbCut;
        private System.Windows.Forms.ToolStripButton tsbPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbUndo;
        private System.Windows.Forms.ToolStripButton tsbRedo;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.ContextMenuStrip contextBackground;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem miDeleteScheme;
        private System.Windows.Forms.ToolStripMenuItem tbPropBackground;
        private System.Windows.Forms.TextBox tbDrawBox;
        private System.Windows.Forms.HScrollBar hScrollBar;
        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbFormatCopy;
        private System.Windows.Forms.ToolStripMenuItem miPasteFromBuffer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}