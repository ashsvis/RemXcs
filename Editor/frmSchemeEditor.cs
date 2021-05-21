using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Points.Plugins;
using BaseServer;
using Draws.Plugins;

namespace DataEditor
{
    public partial class frmSchemeEditor : Form
    {
        HashSet<string> proplist = new HashSet<string>();
        IDictionary<string, object> propvalueslist = new Dictionary<string, object>();
        bool FormatCopyPermanent = false;
        List<Draw> drawslist = new List<Draw>();
        List<Draw> sellist = new List<Draw>();
        IDrawPlugin seldraw = null;

        string Descriptor = String.Empty;
        DataFormats.Format DrawsFormat = DataFormats.GetFormat("clipboardDrawsFormat");
        ToolStripButton m;

        public string SchemeName { get; set; }

        public frmSchemeEditor(Form parent, string schemeName = "")
        {
            InitializeComponent();
            this.SchemeName = (schemeName.Length > 0) ? schemeName : "NONAME";
            //this.MouseWheel += drawBox_MouseWheel;
            IDictionary<string, ToolStrip> categories = new Dictionary<string, ToolStrip>();
            IDictionary<string, ToolStripMenuItem> items = new Dictionary<string, ToolStripMenuItem>();
            IDictionary<string, IDrawPlugin> plugins = DrawPlugin.LoadPlugins(Application.StartupPath);
            string lastcategory = String.Empty;

            IEnumerable<KeyValuePair<string, IDrawPlugin>> query =
                plugins.OrderBy(kvp => kvp.Value.PluginCategory + kvp.Value.PluginDescriptor);

            foreach (KeyValuePair<string, IDrawPlugin> plugin in query)
            {
                if (!lastcategory.Equals(plugin.Value.PluginCategory))
                {
                    lastcategory = plugin.Value.PluginCategory;
                    tsPlugins.Items.Add(new ToolStripSeparator());
                }
                m = new ToolStripButton();
                m.ToolTipText = plugin.Value.PluginDescriptor;
                m.Image = plugin.Value.PluginPicture;
                m.Tag = plugin.Value;
                m.Click += miCreateDinamicElement_Click;
                tsPlugins.Items.Add(m);
            }
            drawBox.Size = GetPanelSize();
        }

        private void miCreateDinamicElement_Click(object sender, EventArgs e)
        {
            UnselectedMenuDinamicElements();
            ToolStripButton element = (ToolStripButton)sender;
            element.Checked = true;
            seldraw = (IDrawPlugin)element.Tag;
        }

        private void UnselectedMenuDinamicElements()
        {
            foreach (ToolStripItem m in tsPlugins.Items)
                if (m is ToolStripButton)
                    ((ToolStripButton)m).Checked = false;
        }

        private void frmScheme_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        public void SelectOrNewScheme()
        {
            using (frmSchemeOpen form = new frmSchemeOpen(SchemeName))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadScheme(form.SelectedScheme());
                else
                    if (!DoNewScheme()) this.Close();
            }
        }

        private void drawBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            // Отрисовка массива фигур
            foreach (Draw drw in drawslist) drw.DrawFigure(g);
            // Отрисовка выбранных
            if (!dragByRect)
            {
                foreach (Draw drw in sellist)
                {
                    using (Pen pen = new Pen(Color.Yellow))
                    {
                        pen.DashStyle = DashStyle.Dash;
                        RectangleF rect = drw.BoundsRect;
                        rect.Inflate(1f, 1f);
                        g.DrawRectangles(pen, new RectangleF[] { rect });
                    }
                }
            }
            // отрисовка прямоугольника выбора
            using (Pen pen = new Pen(Color.Yellow))
            {
                pen.DashStyle = DashStyle.Dash;
                g.DrawRectangle(pen, selrect);
            }
            // граница картинки
            //using (Pen pen = new Pen(Color.Red))
            //{
            //    pen.Width = 1;
            //    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            //    RectangleF rect = new RectangleF(0, 0, drawBox.Width - 1, drawBox.Height - 1);
            //    //RectangleF rect = new RectangleF(0, 0, 1276 - 1, 953 - 1);
            //    g.DrawRectangles(pen, new RectangleF[] { rect });
            //}

        }

        private bool controlPressed = false;
        private bool shiftPressed = false;
        private bool altPressed = false;

        //private void drawBox_MouseWheel(object sender, MouseEventArgs e)
        //{
        //    int step = 32;
        //    if (e.Delta > 0)
        //    {
        //        Point pos = drawBox.Location;
        //        if (controlPressed)
        //        {
        //            if (drawBox.Width > pnlScroll.Width)
        //            {
        //                pos.X += step;
        //                if (pos.X > 0) pos.X = 0;
        //            }
        //        }
        //        else
        //        {
        //            if (drawBox.Height > pnlScroll.Height)
        //            {
        //                pos.Y += step;
        //                if (pos.Y > 0) pos.Y = 0;
        //            }
        //        }
        //        drawBox.Location = pos;
        //    }
        //    if (e.Delta < 0)
        //    {
        //        Point pos = drawBox.Location;
        //        if (controlPressed)
        //        {
        //            if (drawBox.Width > pnlScroll.Width)
        //            {
        //                pos.X -= step;
        //                if (pnlScroll.Width - pos.X > drawBox.Width)
        //                    pos.X = pnlScroll.Width - drawBox.Width;
        //            }
        //        }
        //        else
        //        {
        //            if (drawBox.Height > pnlScroll.Height)
        //            {
        //                pos.Y -= step;
        //                if (pnlScroll.Height - pos.Y > drawBox.Height)
        //                    pos.Y = pnlScroll.Height - drawBox.Height;
        //            }
        //        }
        //        drawBox.Location = pos;
        //    }
        //}

        private Draw GetDrawAt(int X, int Y)
        {
            for (int i = drawslist.Count - 1; i >= 0; i--)
            {
                Draw drw = drawslist[i];
                RectangleF rect = new RectangleF(drw.Location, drw.Size);
                if (rect.Contains(new Point(X, Y))) return drw;
            }
            return null;
        }

        private Point selpoint = new Point();
        private Rectangle selrect = new Rectangle();
        private Point selrectpoint = new Point();

        bool selectByRect = false;
        bool dragByRect = false;

        bool hchanges = false;
        bool hasChanges
        {
            get { return hchanges; }
            set
            { 
                hchanges = value;
                PrepareToUndo(hchanges);
                PrepareToRedo(false);
                tsbUndo.Enabled = IsCanUndoChanges();
                tsbRedo.Enabled = IsCanRedoChanges();
                tsbSave.Enabled = hchanges;
            }
        }

        private void buildUnionSelRect(Draw drw)
        {
            selrect = Rectangle.Ceiling(drw.BoundsRect);
            foreach (Draw item in sellist)
                selrect = Rectangle.Union(selrect, Rectangle.Ceiling(item.BoundsRect));
        }

        private void drawBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // запоминание точки первого нажатия для выбора прямоугольником
                selpoint.X = e.X;
                selpoint.Y = e.Y;
                selrect.Location = selpoint;
                Draw drw = SelectExitsElement(e);
                if (drw == null || drw is Background)
                {
                    sellist.Clear();
                    tsbCut.Enabled = false;
                    tsbCopy.Enabled = false;
                    tsbFormatCopy.Enabled = false;
                    selectByRect = true;
                }
                else
                {
                    // выбор мышью существующего элемента
                    if (!controlPressed && sellist.IndexOf(drw) < 0) sellist.Clear();
                    if (sellist.IndexOf(drw) < 0)
                    {
                        if (!(drw is Background)) sellist.Add(drw);
                    }
                    else
                        if (controlPressed) sellist.Remove(drw);
                    if (sellist.Count > 0)
                    {
                        tsbCut.Enabled = true;
                        tsbCopy.Enabled = true;
                        // Копирование форматов
                        if (tsbFormatCopy.Enabled && tsbFormatCopy.Checked && propvalueslist.Count > 0)
                        {
                            hasChanges = true;
                            foreach (KeyValuePair<string, object> kvp in propvalueslist)
                            {
                                if ((kvp.Key.StartsWith("Font") || kvp.Key.StartsWith("Color") ||
                                    kvp.Key.StartsWith("Frame") || kvp.Key.StartsWith("Show") ||
                                    kvp.Key.StartsWith("BarLevel") ||
                                    kvp.Key.StartsWith("Alignment") ||
                                    kvp.Key.EndsWith("Kind") ||
                                    kvp.Key.Equals("Solid")) && drw.Props.ContainsKey(kvp.Key))
                                {
                                    drw.Props[kvp.Key] = kvp.Value;
                                }
                            }
                            if (!FormatCopyPermanent)
                            {
                                proplist.Clear();
                                propvalueslist.Clear();
                            }
                        }
                        tsbFormatCopy.Enabled = true;
                        tsbFormatCopy.Checked = FormatCopyPermanent;
                    }
                    drawBox.Refresh();
                    dragByRect = true;
                    buildUnionSelRect(drw);
                    selrectpoint.X = selrect.X;
                    selrectpoint.Y = selrect.Y;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                #region Вызов контектсного меню
                Draw drw = SelectExitsElement(e);
                if (drw == null || drw is Background)
                {
                    sellist.Clear();
                    contextBackground.Show(drawBox, e.Location);
                    tsbCut.Enabled = false;
                    tsbCopy.Enabled = false;
                    tsbFormatCopy.Enabled = false;
                }
                else
                {
                    if (sellist.IndexOf(drw) < 0)
                    { 
                        sellist.Clear();
                        if (!(drw is Background)) sellist.Add(drw);
                    }
                    miProps.Enabled = (sellist.Count == 1);
                    contextMenu.Show(drawBox, e.Location);
                    tsbCut.Enabled = true;
                    tsbCopy.Enabled = true;
                    tsbFormatCopy.Enabled = true;
                }
                drawBox.Invalidate();
                #endregion
            }
            tbDrawBox.Focus();
        }

        private void drawBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectByRect)
            {
                if (e.X >= selpoint.X && e.Y >= selpoint.Y)
                    selrect = new Rectangle(selpoint.X, selpoint.Y, e.X - selpoint.X, e.Y - selpoint.Y);
                else if (e.X < selpoint.X && e.Y >= selpoint.Y)
                    selrect = new Rectangle(e.X, selpoint.Y, selpoint.X - e.X, e.Y - selpoint.Y);
                else if (e.X >= selpoint.X && e.Y < selpoint.Y)
                    selrect = new Rectangle(selpoint.X, e.Y, e.X - selpoint.X, selpoint.Y - e.Y);
                else
                    selrect = new Rectangle(e.X, e.Y, selpoint.X - e.X, selpoint.Y - e.Y);
                drawBox.Refresh();
            }
            if (dragByRect)
            {
                selrect.Location = selrectpoint;
                int dx = e.X - selpoint.X;
                int dy = e.Y - selpoint.Y;
                selrect.Offset(dx, dy);
                if (selrect.Left < 0)
                    selrect.Location = new Point(0, selrect.Top);
                        //PointF offset = drw.Location;
                        //if (offset.Y + drw.BoundsRect.Height + step <= drawBox.Height)
                        //{
                if (selrect.Top < 0)
                    selrect.Location = new Point(selrect.Left, 0);
                drawBox.Refresh();
            }
        }

        private void drawBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (selectByRect)
            {
                selectByRect = false;
                if (seldraw != null)
                {   
                    #region Создание нового элемента
                    hasChanges = true;
                    Draw drw = new Draw(selrect.Location.X, selrect.Location.Y, seldraw);
                    if (drw.Props.ContainsKey("ImagePath"))
                        drw.SetPropValue("ImagePath", Application.StartupPath + "\\images\\");
                    if (selrect.Width > 3 && selrect.Height > 3)
                        drw.Size = new SizeF(selrect.Width, selrect.Height);
                    drawslist.Add(drw);
                    sellist.Clear(); sellist.Add(drw);
                    tsbCut.Enabled = true;
                    tsbCopy.Enabled = true;
                    tsbFormatCopy.Enabled = true;
                    tsbFormatCopy.Checked = false;
                    selectArrow();
                    #endregion
                }
                else
                { // выбор прямоугольником существующего элемента
                    foreach (Draw drw in drawslist)
                    {
                        if (drw is Background) continue;
                        if (selrect.Contains(Rectangle.Ceiling(drw.BoundsRect)))
                            sellist.Add(drw);
                    }
                    if (sellist.Count > 0)
                    {
                        tsbCut.Enabled = true;
                        tsbCopy.Enabled = true;
                        tsbFormatCopy.Enabled = true;
                        tsbFormatCopy.Checked = false;
                    }
                }
                selrect = new Rectangle();
                drawBox.Invalidate();
            }
            else if (dragByRect)
            {
                dragByRect = false;
                selrect.Location = selrectpoint;
                if (e.X != selpoint.X || e.Y != selpoint.Y)
                {
                    hasChanges = true;
                    int dx = e.X - selpoint.X;
                    int dy = e.Y - selpoint.Y;
                    selrect.Offset(dx, dy);
                    if (selrect.Left < 0)
                    {
                        dx -= selrect.Left;
                        selrect.Location = new Point(0, selrect.Top);
                    }
                    if (selrect.Top < 0)
                    {
                        dy -= selrect.Top;
                        selrect.Location = new Point(selrect.Left, 0);
                    }
                    foreach (Draw drw in sellist)
                    {
                        if (drw is Background) continue;
                        PointF offset = drw.Location;
                        offset.X += dx;
                        offset.Y += dy;
                        drw.Location = offset;
                    }
                }
                selrect = new Rectangle();
                drawBox.Invalidate();
            }
        }

        private void refreshDraw(Draw drw)
        {
            if (drw != null)
                drawBox.Invalidate(Rectangle.Ceiling(drw.BoundsRect));
        }

        private Draw SelectExitsElement(MouseEventArgs e)
        {
            Draw drw = GetDrawAt(e.X, e.Y);
            return drw;
        }

        private void selectArrow()
        {
            seldraw = null;
            UnselectedMenuDinamicElements();
            tsbArrow.Checked = true;
        }

        private void tsbArrow_Click(object sender, EventArgs e)
        {
            selectArrow();
        }

        public void LoadScheme(string schemename)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                SchemeName = schemename;
                sellist.Clear();
                drawslist = DrawPlugin.LoadScheme(SchemeName,
                    Application.StartupPath + "\\images\\");
                if (drawslist.Count > 0)
                {
                    Background bkg = (Background)drawslist[0];
                    // не мешает правильной работе 
                    if (!bkg.Expanded)
                    {
                        Size size = GetPanelSize();
                        bkg.Width = drawBox.Width = size.Width;
                        bkg.Height = drawBox.Height = size.Height;
                    }
                    drawBox.Width = (int)bkg.Width;
                    drawBox.Height = (int)bkg.Height;
                    drawBox.BackColor = bkg.BackColor;
                    Descriptor = bkg.Descriptor;
                    bkg.SchemeName = SchemeName;
                    this.Text = String.Format("Мнемосхема [{0}] - {1}", SchemeName, Descriptor);
                    CalculateScrollBars();
                    //updater.UpdateCaptionText(this.Text);
                }
                foreach (Draw drw in drawslist)
                {
                    if (drw.Props.ContainsKey("Quality"))
                        drw.Props["Quality"] = "GOOD";
                    else
                        drw.Props.Add("Quality", "GOOD");
                }
            }
            finally
            {
                Cursor = Cursors.Default;
                hasChanges = false;
                tsbCut.Enabled = false;
                tsbCopy.Enabled = false;
                tsbFormatCopy.Enabled = false;
                drawBox.Invalidate();
            }
        }

        private void miOpen_Click(object sender, EventArgs e)
        {
            using (frmSchemeOpen form = new frmSchemeOpen(SchemeName))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadScheme(form.SelectedScheme());
                }
            }
        }

        private void Stub(string mess) { }

        private void tsmiSaveAs_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Существующие данные на диске будут заменены! Продолжить?",
                "Сохранение мнемосхемы на локальный диск пользователя", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string path = Application.StartupPath + "\\schemes\\";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                exportFileDialog.InitialDirectory = path;
                exportFileDialog.FileName = SchemeName.ToLower() + ".ini";
                if (exportFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    DrawPlugin.ExportOneSchemeAs(path, SchemeName);
                    Cursor = Cursors.Default;
                }
            }
        }

        private void tsmiRestoreFrom_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Текущая мнемосхема будет заменена! Продолжить?",
                "Восстановление мнемосхемы с диска", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                importFileDialog.InitialDirectory = Application.StartupPath;
                if (importFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    SchemeName = DrawPlugin.ImportSchemeFrom(importFileDialog.FileName);
                    LoadScheme(SchemeName);
                    drawBox.Refresh();
                    Cursor = Cursors.Default;
                    hasChanges = false;
                }
            }
        }

        private void saveToBase()
        {
            if (hasChanges)
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                    using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
                    {
                        if (mySQL.Connected)
                        {
                            string ukey = Data.GetUniqueKey(20);
                            int step = 0;
                            try
                            {
                                int npp = 0;
                                foreach (Draw item in drawslist)
                                    DrawPlugin.saveOneDraw(npp++, ukey, item, mySQL);
                                step++;
                                Data.EmptyScheme(SchemeName, mySQL);
                                Data.RenameScheme(ukey, SchemeName, mySQL);
                                hasChanges = false;
                            }
                            catch
                            {
                                switch (step)
                                {
                                    case 0:
                                        Data.EmptyScheme(ukey, mySQL);
                                        break;
                                }
                            }
                        }
                    }
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void miBringToFront_Click(object sender, EventArgs e)
        {
            foreach (Draw drw in sellist) drawslist.Remove(drw);
            foreach (Draw drw in sellist) drawslist.Add(drw);
            hasChanges = true;
            drawBox.Invalidate();
        }

        private void miSendToBack_Click(object sender, EventArgs e)
        {
            foreach (Draw drw in sellist) drawslist.Remove(drw);
            foreach (Draw drw in sellist) drawslist.Insert(1, drw);
            hasChanges = true;
            drawBox.Refresh();
        }

        private void miSave_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            saveToBase();
            Cursor = Cursors.Default;
        }

        string hash_element = String.Empty;
        string pointname = String.Empty;
        bool beforehaschanges = false;
        private void updateDraw(Draw element, UpdateKind BeforeAfter)
        {
            switch (BeforeAfter)
            {
                case UpdateKind.UpdateBefore:
                    pointname = UpdateScaleValues(element.Props);
                    hash_element = DrawUtils.getMd5Hash(element.Props);
                    beforehaschanges = hasChanges;
                    break;
                case UpdateKind.UpdateAfter:
                    if (hash_element.Length > 0)
                    {
                        if (!beforehaschanges)
                            hasChanges = !DrawUtils.verifyMd5Hash(element.Props, hash_element);
                        if (element is Background)
                        {
                            Background bkg = (Background)element;
                            drawBox.BackColor = bkg.BackColor;
                            SchemeName = bkg.SchemeName;
                            Descriptor = bkg.Descriptor;
                            
                            this.Text = String.Format("Мнемосхема [{0}] - {1}", SchemeName, Descriptor);
                            //updater.UpdateCaptionText(this.Text);
                        }
                        else
                            if (hasChanges && element.Props.ContainsKey("PtName") &&
                                (string)element.Props["PtName"] != pointname)
                                pointname = UpdateScaleValues(element.Props);
                        drawBox.Invalidate();
                    }
                    break;
            }
        }

        private static string UpdateScaleValues(IDictionary<string, object> props)
        {
            string ptname = String.Empty;
            if (props.ContainsKey("Plugin"))
            {
                Entity ent;
                switch ((string)props["Plugin"])
                {
                    case "DinDigital":
                        ptname = (string)props["PtName"];
                        ent = Data.GetEntity(ptname);
                        if (!ent.Empty)
                        {
                            if (ent.Values.ContainsKey("ColorUp") &&
                                ent.Values.ContainsKey("ColorDown") &&
                                (int)ent.Values["ColorUp"] < ClassPoint.DigitalColors.Length &&
                                (int)ent.Values["ColorDown"] < ClassPoint.DigitalColors.Length)
                            {
                                props["Color1"] = (Color)ClassPoint.DigitalColors[(int)ent.Values["ColorUp"]];
                                props["Color0"] = (Color)ClassPoint.DigitalColors[(int)ent.Values["ColorDown"]];
                            }
                            else
                            {
                                props["Color1"] = Color.LightGreen;
                                props["Color0"] = Color.Red;
                            }
                        }
                        else
                        {
                            props["Color1"] = Color.LightBlue;
                            props["Color0"] = Color.Blue;
                        }
                        break;
                    case "DinAnalog":
                        ptname = (string)props["PtName"];
                        ent = Data.GetEntity(ptname);
                        if (!ent.Empty &&
                            ent.Values.ContainsKey("PVEUHi") &&
                            ent.Values.ContainsKey("PVEULo") &&
                            ent.Values.ContainsKey("EUDesc") &&
                            ent.Values.ContainsKey("FormatPV"))
                        {
                            props["PVEUHi"] = Data.FloatEx(ent.Values["PVEUHi"].ToString());
                            props["PVEULo"] = Data.FloatEx(ent.Values["PVEULo"].ToString());
                            props["EUDesc"] = ent.Values["EUDesc"].ToString();
                            props["FormatPV"] = (int)ent.Values["FormatPV"];
                        }
                        break;
                    case "DinKontur":
                        ptname = (string)props["PtName"];
                        ent = Data.GetEntity(ptname);
                        if (!ent.Empty &&
                            ent.Values.ContainsKey("PVEUHi") &&
                            ent.Values.ContainsKey("PVEULo") &&
                            ent.Values.ContainsKey("SPEUHi") &&
                            ent.Values.ContainsKey("SPEULo") &&
                            ent.Values.ContainsKey("OPEUHi") &&
                            ent.Values.ContainsKey("OPEULo") &&
                            ent.Values.ContainsKey("EUDesc") &&
                            ent.Values.ContainsKey("FormatPV"))
                        {
                            props["PVEUHi"] = Data.FloatEx(ent.Values["PVEUHi"].ToString());
                            props["PVEULo"] = Data.FloatEx(ent.Values["PVEULo"].ToString());
                            props["FormatPV"] = (int)ent.Values["FormatPV"];
                            props["EUDesc"] = ent.Values["EUDesc"].ToString();
                            props["SPEUHi"] = Data.FloatEx(ent.Values["SPEUHi"].ToString());
                            props["SPEULo"] = Data.FloatEx(ent.Values["SPEULo"].ToString());
                            props["OPEUHi"] = Data.FloatEx(ent.Values["OPEUHi"].ToString());
                            props["OPEULo"] = Data.FloatEx(ent.Values["OPEULo"].ToString());
                        }
                        break;
                }
            }
            return ptname;
        }

        private void miShowProps_Click(object sender, EventArgs e)
        {
            Draw drw = sellist[0];
            SelectData extdata = null;
            switch (drw.DataKind)
            {
                case SelectDataKind.DataPoints: extdata = SelectPoint; break;
                case SelectDataKind.DataImages: extdata = SelectImage; break;
                case SelectDataKind.DataSchemes: extdata = SelectScheme; break;
            }
            Form editor = drw.Editor(updateDraw, extdata);
            if (editor != null) editor.ShowDialog(this);
        }

        private object SelectScheme(params object[] args)
        {
            if (args.Length == 1)
            {
                string screenname = (string)args[0];
                using (frmSchemeOpen form = new frmSchemeOpen(screenname))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                        return form.SelectedScheme();
                }
            }
            return String.Empty;
        }

        private object SelectImage(params object[] args)
        {
            if (args.Length == 2 && args[0].ToString().Equals("SelectImageName"))
            {
                string pathfile = Application.StartupPath + "\\images\\";
                string filename = (string)args[1];
                dlgSelectImage.InitialDirectory = pathfile;
                dlgSelectImage.FileName = filename;
                if (dlgSelectImage.ShowDialog(this) == DialogResult.OK)
                {
                    filename = dlgSelectImage.FileName;
                    return filename.Substring(filename.LastIndexOf('\\') + 1);
                }
                return String.Empty;
            }
            else if (args.Length == 2 && args[0].ToString().Equals("ImageFileName"))
            {
                string pathfile = Application.StartupPath + "\\images\\";
                string filename = pathfile + (string)args[1];
                if (File.Exists(filename))
                {
                    Image image;
                    if (filename.ToUpper().EndsWith(".EMF"))
                        image = Metafile.FromFile(filename);
                    else
                        image = Bitmap.FromFile(filename);
                    return image;
                }
            }
            return null;
        }

        private object SelectPoint(params object[] args)
        {
            if (args.Length == 3)
            {
                string DinKind = args[0].ToString();
                string PtName = args[1].ToString();
                string PtParam = args[2].ToString();
                PointSelector ps; 
                switch (DinKind)
                {
                    case "DinValve":  ps = PointSelector.ValvePoints; break;
                    case "DinKontur": ps = PointSelector.KonturPoints; break;
                    case "DinAnalog": ps = PointSelector.AnalogPoints; break;
                    case "DinDigital":
                    case "DinLine":
                    case "DinText":   ps = PointSelector.DigitalPoints; break;
                    case "DinButton": ps = PointSelector.DigitalPoints; break;
                    default: ps = PointSelector.AllPoints; break;
                }
                using (frmEntitySelector form =
                    new frmEntitySelector(PtName, PtParam, ps))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                        return form.EntityName + "." + form.EntityParam;
                }
            }
            return String.Empty;
        }

        private void selectedAll()
        {
            sellist.Clear();
            foreach (Draw drw in drawslist)
            {
                if (drw is Background) continue;
                sellist.Add(drw);
            }
            tsbCut.Enabled = true;
            tsbCopy.Enabled = true;
            tsbFormatCopy.Enabled = true;
            drawBox.Invalidate();
        }

        private void deleteSelected()
        {
            hasChanges = true;
            foreach (Draw drw in sellist) drawslist.Remove(drw);
            sellist.Clear();
            tsbCut.Enabled = false;
            tsbCopy.Enabled = false;
            tsbFormatCopy.Enabled = false;
            drawBox.Invalidate();
        }

        private void miDinDelete_Click(object sender, EventArgs e)
        {
            deleteSelected();
        }

        private void miDinDouble_Click(object sender, EventArgs e)
        {
            List<Draw> list = new List<Draw>();
            foreach (Draw item in sellist)
            {
                IDrawPlugin dp = item.Plugin;
                Draw drw = new Draw(item.Location.X + 5, item.Location.Y + 5, dp);
                drw.Assign(item);
                list.Add(drw);
            }
            sellist.Clear();
            hasChanges = true;
            foreach (Draw drw in list)
            {
                drawslist.Add(drw);
                sellist.Add(drw);
            }
            tsbCut.Enabled = true;
            tsbCopy.Enabled = true;
            tsbFormatCopy.Enabled = true;
            tsbFormatCopy.Checked = false;
            drawBox.Invalidate();
        }

        private void tbDrawBox_KeyDown(object sender, KeyEventArgs e)
        {
            controlPressed = e.Control;
            shiftPressed = e.Shift;
            altPressed = e.Alt;
            int step = (controlPressed) ? 5 : 1;
            #region Перемещение элементов стрелками
            if (e.KeyCode == Keys.Up)
            {
                hasChanges = true;
                foreach (Draw drw in sellist)
                {
                    if (shiftPressed)
                    {
                        SizeF size = drw.Size;
                        if (size.Height - step >= 0)
                        {
                            size.Height -= step; drw.Size = size;
                            drawBox.Invalidate();
                        }
                    }
                    else
                    {
                        PointF offset = drw.Location;
                        if (offset.Y - step >= 0)
                        {
                            offset.Y -= step; drw.Location = offset;
                            drawBox.Invalidate();
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                hasChanges = true;
                foreach (Draw drw in sellist)
                {
                    if (shiftPressed)
                    {
                        SizeF size = drw.Size;
                        size.Height += step; drw.Size = size;
                        drawBox.Invalidate();
                    }
                    else
                    {
                        PointF offset = drw.Location;
                        if (offset.Y + drw.BoundsRect.Height + step <= drawBox.Height)
                        {
                            offset.Y += step; drw.Location = offset;
                            drawBox.Invalidate();
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                hasChanges = true;
                foreach (Draw drw in sellist)
                {
                    if (shiftPressed)
                    {
                        SizeF size = drw.Size;
                        if (size.Width - step >= 0)
                        {
                            size.Width -= step; drw.Size = size;
                            drawBox.Invalidate();
                        }
                    }
                    else
                    {
                        PointF offset = drw.Location;
                        if (offset.X > 0)
                        {
                            offset.X -= step; drw.Location = offset;
                            drawBox.Invalidate();
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                hasChanges = true;
                foreach (Draw drw in sellist)
                {
                    if (shiftPressed)
                    {
                        SizeF size = drw.Size;
                        size.Width += step; drw.Size = size;
                        drawBox.Invalidate();
                    }
                    else
                    {
                        PointF offset = drw.Location;
                        if (offset.X + drw.BoundsRect.Width < drawBox.Width)
                        {
                            offset.X += step; drw.Location = offset;
                            drawBox.Invalidate();
                        }
                    }
                }
            }
            #endregion

            switch (e.KeyCode)
            {
                case Keys.A: if (controlPressed) selectedAll(); break;
                case Keys.Delete: deleteSelected(); break;
                case Keys.C: if (controlPressed) CopySelectedToClipboard(); break;
                case Keys.X: if (controlPressed) CutSelectedToClipboard(); break;
                case Keys.V: if (controlPressed) PasteFromClipboardAndSelected(); break;
            }
        }

        private void tbDrawBox_KeyUp(object sender, KeyEventArgs e)
        {
            controlPressed = e.Control;
            shiftPressed = e.Shift;
            altPressed = e.Alt;
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down ||
                e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                drawBox.Refresh();
                hasChanges = true;
            }
        }

        public void CopySelectedToClipboard()
        {
            if (sellist.Count > 0)
            {
                List<Draw> forcopy = new List<Draw>();
                foreach (Draw drw in sellist) forcopy.Add(drw);
                DataObject clipboardDataObject = new DataObject(DrawsFormat.Name, forcopy);
                Clipboard.SetDataObject(clipboardDataObject, false);
                tsbPaste.Enabled = true;
            }
        }

        private void tsbCopy_Click(object sender, EventArgs e)
        {
            CopySelectedToClipboard();
        }

        public void CutSelectedToClipboard()
        {
            if (sellist.Count > 0)
            {
                List<Draw> forcopy = new List<Draw>();
                foreach (Draw drw in sellist) forcopy.Add(drw);
                DataObject clipboardDataObject = new DataObject(DrawsFormat.Name, forcopy);
                Clipboard.SetDataObject(clipboardDataObject, false);
                hasChanges = true;
                foreach (Draw drw in sellist) drawslist.Remove(drw);
                sellist.Clear();
                tsbCut.Enabled = false;
                tsbCopy.Enabled = false;
                tsbPaste.Enabled = true;
                tsbFormatCopy.Enabled = false;
                GC.Collect();
                drawBox.Refresh();
            }
        }

        private void tsbCut_Click(object sender, EventArgs e)
        {
            CutSelectedToClipboard();
        }

        public void PasteFromClipboardAndSelected()
        {
            if (Clipboard.ContainsData(DrawsFormat.Name))
            {
                hasChanges = true;
                IDataObject clipboardRetrievedObject = Clipboard.GetDataObject();
                List<Draw> pastedObject =
                     (List<Draw>)clipboardRetrievedObject.GetData(DrawsFormat.Name);
                if (pastedObject != null)
                {
                    sellist.Clear();
                    foreach (Draw drw in pastedObject)
                    {
                        drw.GenerateNewName();
                        drawslist.Add(drw);
                        sellist.Add(drw);
                    }
                    if (sellist.Count > 0)
                    {
                        buildUnionSelRect(sellist[0]);
                        int dx = (pnlScroll.Width - selrect.Width) / 2;
                        int dy = (pnlScroll.Height - selrect.Height) / 2;
                        foreach (Draw drw in sellist)
                        {
                            PointF location = drw.Location;
                            location.X += dx - selrect.Left - drawBox.Left;
                            location.Y += dy - selrect.Top - drawBox.Top;
                            drw.Location = location;
                        }
                        selrect = new Rectangle();
                        tsbCut.Enabled = true;
                        tsbCopy.Enabled = true;
                        tsbFormatCopy.Enabled = true;
                        tsbFormatCopy.Checked = false;
                    }
                    drawBox.Invalidate();
                }
            }
        }

        private void tsbPaste_Click(object sender, EventArgs e)
        {
            PasteFromClipboardAndSelected();
        }

        public bool IsCanRedoChanges()
        {
            return (RedoStack.Count > 0);
        }

        public bool IsCanUndoChanges()
        {
            return (UndoStack.Count > 0);
        }

        StackMemory UndoStack = new StackMemory(10);
        StackMemory RedoStack = new StackMemory(10);

        private void SaveToStream(MemoryStream stream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            List<Draw> list = new List<Draw>();
            foreach (Draw drw in drawslist) list.Add(drw);
            formatter.Serialize(stream, list);
            stream.Position = 0;
        }

        private List<Draw> LoadFromStream(MemoryStream stream)
        {
            List<Draw> list = new List<Draw>();
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                stream.Position = 0;
                list = (List<Draw>)formatter.Deserialize(stream);
                return (list);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
        }

        private void PrepareToUndo(bool changed)
        {
            if (changed)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    SaveToStream(stream);
                    UndoStack.Push(stream);
                }
            }
            else
                UndoStack.Clear();
        }

        private void PrepareToRedo(bool changed)
        {
            if (changed)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    SaveToStream(stream);
                    RedoStack.Push(stream);
                }
            }
            else
                RedoStack.Clear();
        }

        public void RedoChanges()
        {
            if (IsCanRedoChanges())
            {
                PrepareToUndo(true);
                sellist.Clear();
                tsbCut.Enabled = false;
                tsbCopy.Enabled = false;
                tsbFormatCopy.Enabled = false;
                drawslist.Clear();
                GC.Collect();
                using (MemoryStream stream = new MemoryStream())
                {
                    RedoStack.Pop(stream);
                    List<Draw> list = LoadFromStream(stream);
                    foreach (Draw drw in list) drawslist.Add(drw);
                }
                this.Refresh();
            }
        }

        public void UndoChanges()
        {
            if (IsCanUndoChanges())
            {
                PrepareToRedo(true);
                sellist.Clear();
                tsbCut.Enabled = false;
                tsbCopy.Enabled = false;
                tsbFormatCopy.Enabled = false;
                drawslist.Clear();
                GC.Collect();
                using (MemoryStream stream = new MemoryStream())
                {
                    UndoStack.Pop(stream);
                    List<Draw> list = LoadFromStream(stream);
                    foreach (Draw drw in list) drawslist.Add(drw);
                }
                drawBox.Refresh();
            }
        }

        private void tsbUndo_Click(object sender, EventArgs e)
        {
            UndoChanges();
            tsbUndo.Enabled = IsCanUndoChanges();
            tsbRedo.Enabled = IsCanRedoChanges();
        }

        private void tsbRedo_Click(object sender, EventArgs e)
        {
            RedoChanges();
            tsbUndo.Enabled = IsCanUndoChanges();
            tsbRedo.Enabled = IsCanRedoChanges();
        }

        private void tsbSaveAs_Click(object sender, EventArgs e)
        {

        }

        private void newScheme()
        {
            sellist.Clear();
            tsbCut.Enabled = false;
            tsbCopy.Enabled = false;
            tsbFormatCopy.Enabled = false;
            drawslist.Clear();
            GC.Collect();
            drawBox.BackColor = Color.Gray;
            SchemeName = "NONAME";
            Descriptor = String.Empty;
            Background bkg = new Background();
            bkg.SchemeName = SchemeName;
            Size pansize = GetPanelSize();
            bkg.Width = drawBox.Width = pansize.Width;
            bkg.Height = drawBox.Height = pansize.Height;
            drawslist.Add(bkg);
            drawBox.Refresh();
            hasChanges = false;
            CalculateScrollBars();
        }

        public bool DoNewScheme()
        {
            bool result = false;
            using (frmInputString form = new frmInputString())
            {
                form.DoUpper();
                form.Text = "Введите имя новой мнемосхемы";
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (!String.IsNullOrWhiteSpace(form.Value))
                    {
                        string newname = form.Value;
                        if (!Data.GetSchemesList().ContainsKey(newname))
                        {
                            newScheme();
                            SchemeName = newname;
                            hasChanges = true;
                            Descriptor = "Новая мнемосхема";
                            Background bkg = (Background)drawslist[0];
                            bkg.SchemeName = SchemeName;
                            bkg.Descriptor = Descriptor;
                            this.Text = String.Format("Мнемосхема [{0}] - {1}", SchemeName, Descriptor);
                            //updater.UpdateCaptionText(this.Text);
                            result = true;
                        }
                        else
                            MessageBox.Show(this,
                                String.Format("Мнемосхема \"{0}\" уже используется!", newname),
                                "Новая мнемосхема", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return result;
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            DoNewScheme();
        }

         private void miDeleteScheme_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Эта мнемосхема будет удалена безвозвратно! Удалить?",
                "Удаление мнемосхемы с сервера", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Data.EmptyScheme(SchemeName);
                newScheme();
            }
        }

         private object SchemeExists(params object[] args)
         {
             if (args.Length == 1)
             {
                 string screenname = (string)args[0];
                 return Data.SchemeExists(screenname);
             }
             return false;
         }
         
         private void tbPropBackground_Click(object sender, EventArgs e)
        {
            if (drawslist.Count > 0)
            {
                Draw drw = drawslist[0];
                SelectData extdata = SchemeExists;
                Form editor = drw.Editor(updateDraw, extdata);
                if (editor != null) editor.ShowDialog(this);
                drawBox.Width = int.Parse(drw.Props["Width"].ToString());
                drawBox.Height = int.Parse(drw.Props["Height"].ToString());
            }
        }

         public Size GetPanelSize()
         {
             Size result;
             int screensize = Properties.Settings.Default.ScreenSize;
             switch (screensize)
             {
                 case 0: result = new Size(1024, 768); break; //XGA (1024 x 768)
                 case 1: result = new Size(1152, 864); break; //XGA+ (1152 x 864)
                 case 2: result = new Size(1280, 720); break; //WXGA (1280 x 720)
                 case 3: result = new Size(1280, 768); break; //WXGA (1280 x 768)
                 case 4: result = new Size(1280, 800); break; //WXGA (1280 x 800)
                 case 5: result = new Size(1280, 960); break; //WXGA (1280 x 960)
                 case 6: result = new Size(1280, 1024); break; //SXGA (1280 x 1024)
                 case 7: result = new Size(1360, 768); break; //SXGA (1360 x 768)
                 case 8: result = new Size(1400, 1050); break; //SXGA+ (1400 x 1050)
                 case 9: result = new Size(1440, 900); break; //WXGA+ (1440 x 900)
                 case 10: result = new Size(1540, 940); break; //XJXGA (1540 x 940)
                 case 11: result = new Size(1600, 900); break; //WXGA++ (1600 x 900)
                 case 12: result = new Size(1600, 1024); break; //WSXGA (1600 x 1024)
                 case 13: result = new Size(1680, 1050); break; //WSXGA+ (1680 x 1050)
                 case 14: result = new Size(1600, 1200); break; //UXGA (1600 x 1200)
                 case 15: result = new Size(1920, 1080); break; //Full HD (1920 x 1080)
                 case 16: result = new Size(1920, 1200); break; //WUXGA (1920 x 1200)
                 case 17: result = new Size(2048, 1536); break; //QXGA (2048 x 1536)
                 case 18: result = new Size(2048, 1152); break; //QWXGA (2048 x 1152)
                 case 19: result = new Size(2560, 1440); break; //WQXGA (2560 x 1440)
                 case 20: result = new Size(2560, 1600); break; //WQXGA (2560 x 1600)
                 case 21: result = new Size(2560, 2048); break; //QSXGA (2560 x 2048)
                 default: result = new Size(1280, 1024); break;
             }
             //result.Height -= pnlCaption.Height + mnuMenu.Height + stpStatus.Height;
             result.Height -= 18 + 24 + 25 + 4;
             result.Width -= 4;
             return result;
         }

         private void frmSchemeEditor_Activated(object sender, EventArgs e)
         {
             tsbPaste.Enabled = Clipboard.ContainsData(DrawsFormat.Name);
         }

         private void frmSchemeEditor_Resize(object sender, EventArgs e)
         {
             tsFiles.Location = new Point(0, 0);
             tsPlugins.Location = new Point(tsFiles.Right, 0);
             drawBox.Location = new Point(0, 0);
             vScrollBar.Value = 0;
             hScrollBar.Value = 0;
             CalculateScrollBars();
         }

         private void CalculateScrollBars()
         {
             if (drawBox.Height > pnlScroll.Height)
             {
                 vScrollBar.Visible = true;
                 vScrollBar.Maximum = drawBox.Height - pnlScroll.Height + hScrollBar.Height;
             }
             else
                 vScrollBar.Visible = false;
             if (drawBox.Width + (vScrollBar.Visible ? vScrollBar.Width : 0) > pnlScroll.Width)
             {
                 hScrollBar.Visible = true;
                 hScrollBar.Maximum = drawBox.Width - pnlScroll.Width + vScrollBar.Width;
             }
             else
                 hScrollBar.Visible = false;
             vScrollBar.Maximum += hScrollBar.Visible ? hScrollBar.Height - 8 : 0;
             hScrollBar.Maximum += vScrollBar.Visible ? vScrollBar.Width - 8 : 0;
         }

         private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
         {
             Point pos = drawBox.Location;
             pos.Y = -vScrollBar.Value;
             drawBox.Location = pos;
         }

         private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
         {
             Point pos = drawBox.Location;
             pos.X = -hScrollBar.Value;
             drawBox.Location = pos;
         }

         private void tsbFormatCopy_EnabledChanged(object sender, EventArgs e)
         {
             if (!tsbFormatCopy.Enabled)
             {
                 tsbFormatCopy.Checked = false;
                 FormatCopyPermanent = false; ;
             }
         }

         private void tsbFormatCopy_DoubleClick(object sender, EventArgs e)
         {
             tsbFormatCopy.Checked = true;
             FormatCopyPermanent = true;
         }

        // Копирование совпадающих значений свойств
         private void tsbFormatCopy_CheckedChanged(object sender, EventArgs e)
         {
             if (tsbFormatCopy.Checked)
             {
                 proplist.Clear();
                 propvalueslist.Clear();
                 bool first = true;
                 foreach (Draw drw in sellist)
                 {
                     if (drw is Background) continue;
                     HashSet<string> list = new HashSet<string>();
                     foreach (KeyValuePair<string, object> kvp in drw.Props)
                         list.Add(kvp.Key + '\t' + kvp.Value.ToString());
                     if (first)
                     {
                         first = false;
                         proplist.UnionWith(list);
                     }
                     else
                        proplist.IntersectWith(list);
                 }
                 if (sellist.Count > 0)
                 {
                     Draw drw = sellist[0];
                     foreach (string key in proplist)
                     {
                         string propkey = key.Split(new char[] { '\t' })[0];
                         if (drw.Props.ContainsKey(propkey))
                             propvalueslist.Add(propkey, drw.Props[propkey]);
                     }
                 }
                 tsbFormatCopy.Checked = true;
             }
         }

         private void tsbFormatCopy_Click(object sender, EventArgs e)
         {
             if (tsbFormatCopy.Checked)
                 FormatCopyPermanent = false;
         }

         private void tsbPaste_EnabledChanged(object sender, EventArgs e)
         {
             miDinPaste.Enabled = miPasteFromBuffer.Visible = toolStripSeparator5.Visible = tsbPaste.Enabled;
         }

    }
}
