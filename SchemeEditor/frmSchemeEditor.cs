using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace SchemeEditor
{
    public interface ISchemeEditor
    {
        void NewSchemeForm(string FileName);
        void OpenSchemeFormAs();
        void SaveSchemeAs();
    }

    public partial class frmSchemeEditor : Form
    {
        PointF FstPnt = new PointF();
        PointF LstPnt = new PointF();
        PointF SelMrkPnt = new PointF();
        AllDraws DrawsList = new AllDraws();
        AllDraws SelList = new AllDraws();
        bool controlPressed;
        int markerIndex = 0;
        string CurrFileName = String.Empty;
        bool fileChanged;
        DataFormats.Format DrawsFormat = DataFormats.GetFormat("clipboardFormat");
        StackMemory UndoStack = new StackMemory(10);
        StackMemory RedoStack = new StackMemory(10);
        Color DrawsForeColor = Color.Black;
        Color DrawsBackColor = Color.White;

        public int ItemsCount()
        {
            return (DrawsList.Count);
        }
        public void SetItemColor(Color color)
        {
            if (DrawsList.Count > 0)
            {
                Draws drw = (Draws)DrawsList[0];
                drw.Fill.Color = color;
            }
        }

        public frmSchemeEditor()
        {
            InitializeComponent();
            controlPressed = false;
            FileChanged = false;
            timerEnabled.Enabled = true;
            drawBox.Location = new Point(0, 0);
            drawBox.Size = new Size(1280, 1024);
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

        public bool FileChanged
        {
            get
            {
                return (fileChanged);
            }
            set
            {
                fileChanged = value;
                PrepareToUndo(fileChanged);
                PrepareToRedo(false);
            }
        }

        public bool IsCanRedoChanges()
        {
            return (RedoStack.Count > 0);
        }

        public void RedoChanges()
        {
            if (IsCanRedoChanges())
            {
                PrepareToUndo(true);
                SelList.Clear();
                DrawsList.Clear();
                GC.Collect();
                using (MemoryStream stream = new MemoryStream())
                {
                    RedoStack.Pop(stream);
                    List<Draws> list = LoadFromStream(stream);
                    foreach (Draws drw in list) DrawsList.Add(drw);
                }
                this.Refresh();
            }
        }

        public bool IsCanUndoChanges()
        {
            return (UndoStack.Count > 0);
        }

        public void UndoChanges()
        {
            if (IsCanUndoChanges())
            {
                PrepareToRedo(true);
                SelList.Clear();
                DrawsList.Clear();
                GC.Collect();
                using (MemoryStream stream = new MemoryStream())
                {
                    UndoStack.Pop(stream);
                    List<Draws> list = LoadFromStream(stream);
                    foreach (Draws drw in list) DrawsList.Add(drw);
                }
                this.Refresh();
            }
        }

        private void frmChildMdi_Load(object sender, EventArgs e)
        {
            FileChanged = false;
            SelList.Clear();
            this.WindowState = FormWindowState.Maximized;
        }

        private void drawBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            // Отрисовка массива фигур
            foreach (Draws drw in DrawsList) drw.DrawFigure(g);
            // Прямоугольник выбранного объекта
            using (Pen pen = new Pen(Color.Blue))
            {
                pen.Width = 0;
                SelList.DrawFocusFigures(g, pen, PointF.Empty, markerIndex);
            }
        }

        private static void drawFocusRect(Graphics g, Pen p, RectangleF rect)
        {
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddRectangle(rect);
                g.DrawPath(p, gp); // Рисование контура
            }
        }

        private static void drawFocusEllipse(Graphics g, Pen p, RectangleF rect)
        {
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddEllipse(rect);
                g.DrawPath(p, gp); // Рисование контура
            }
        }

        private void drawBox_MouseDown(object sender, MouseEventArgs e)
        {
            FstPnt = new PointF(e.X, e.Y);
            LstPnt = FstPnt;
            if (!tsbSelObj.Checked) SelList.Clear();
            if (tsbSelObj.Checked || (e.Button == MouseButtons.Right))
            {
                Draws drw = DrawsList.PointInFigure(FstPnt);
                if (drw != null)
                { // элемент был выбран мышкой
                    markerIndex = drw.MarkerIndex;
                    if (markerIndex >= 0)
                        foreach (Draws d in SelList) d.MarkerIndex = markerIndex; // раздача всем остальным
                    if (controlPressed)
                    {
                        if (SelList.IndexOf(drw) >= 0)
                        { // удаление из списка уже выделенного элемента
                            if (SelList.Count > 1) // последний элемент при Ctrl не убирается
                            { drw.Selected = false; SelList.Remove(drw); }
                        }
                        else SelList.Add(drw); // добавление к списку
                    }
                    else
                    {
                        if (!SelList.FigureInList(drw))
                        {
                            SelList.Clear(); // очистка списков
                            SelList.Add(drw); // выделение одного элемента
                        }
                    }
                    this.Refresh();
                }
                else
                {
                    SelList.Clear();  // очистка списков                   
                    drawBox.Capture = true; // захват мышки
                }
            }
            // вызов контекстного меню
            if ((e.Button == MouseButtons.Right))
            {
                if (SelList.Count > 0)
                {
                    Draws drw = DrawsList.PointInFigure(FstPnt);
                    bool ModifyFiguresNode = (drw != null) && drw.NodeChanging;
                    if (drw != null) { markerIndex = drw.MarkerIndex; SelMrkPnt = e.Location; }
                    else markerIndex = 0;
                    miAddFigureNode.Visible = ModifyFiguresNode && (drw.MarkerIndex == 0);
                    if (drw is Polygones)
                        miDeleteFigureNode.Visible = ModifyFiguresNode && (drw.MarkerIndex < 0) &&
                                                                   (((Polygones)drw).Points.Count > 2);
                    else
                        miDeleteFigureNode.Visible = false;
                    miBeginChangeNodes.Visible = (SelList.Count == 1) && (drw is Polygones) &&
                                                 drw.CanNodeChanging() && !drw.NodeChanging;
                    miEndChangeNodes.Visible = (SelList.Count == 1) && (drw is Polygones) && drw.NodeChanging;
                    miGroupFigures.Enabled = (SelList.Count > 1);
                    miUngroupFigures.Enabled = drw is Groups;
                    cmsFigPopup.Tag = drw;
                    cmsFigPopup.Show(drawBox, e.Location);
                }
                else
                {
                    cmsBkgPopup.Show(drawBox, e.Location);
                }
            }
        }

        private static RectangleF CurRct(PointF p1, PointF p2)
        {
            RectangleF rect = new RectangleF();
            // корректировка прямоугольника выбора
            if (p2.X - p1.X > 0) 
                { rect.X = p1.X; rect.Width = p2.X - p1.X; }
            else 
                { rect.X = p2.X; rect.Width = p1.X - p2.X; }
            if (p2.Y - p1.Y > 0) 
                { rect.Y = p1.Y; rect.Height = p2.Y - p1.Y; }
            else 
                { rect.Y = p2.Y; rect.Height = p1.Y - p2.Y; }

            return (rect);
        }

        private void drawBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0)
            {
                int step = -Math.Sign(e.Delta) * 32;
                try
                {
                    pnlScrollBox.VerticalScroll.Value += step;
                }
                catch { }
            }
        }
        private void drawBox_MouseMove(object sender, MouseEventArgs e)
        {
            if ((drawBox.Capture) && (e.Delta != 0))
            {
                pnlScrollBox.VerticalScroll.Value += e.Delta;
            }
            else
            if ((e.Button == MouseButtons.Left) && drawBox.Capture)
            {
                PointF p = new PointF(e.X, e.Y);
                if (SelList.Count == 0)
                {   // корректировка последней точки
                    LstPnt.X = p.X; LstPnt.Y = p.Y;
                    // прорисовка прямоугольника выбора
                }
                else
                {   // вычисление смещения при перетаскивании
                    LstPnt.X = p.X - FstPnt.X;
                    LstPnt.Y = p.Y - FstPnt.Y;
                }
                this.Refresh();
                // Текущий прямоугольник выбора
                RectangleF rect = CurRct(FstPnt, LstPnt);
                using (Graphics g = drawBox.CreateGraphics())
                {
                    if (tsbSelObj.Checked && (SelList.Count == 0)) drawFocusRect(g, Pens.Blue, rect);
                    else if (tsbAddLine.Checked) g.DrawLine(Pens.Black, FstPnt, LstPnt);
                    else if (tsbAddRect.Checked || tsbAddPolygon.Checked) drawFocusRect(g, Pens.Black, rect);
                    else if (tsbAddEllipse.Checked) drawFocusEllipse(g, Pens.Black, rect);
                    else
                    {
                        using (Pen pen = new Pen(Color.Blue))
                        {
                            pen.Width = 0;
                            SelList.DrawFocusFigures(g, pen, LstPnt, markerIndex);
                        }
                    }
                }
            }
        }

        private void drawBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (drawBox.Capture)
            {
                drawBox.Capture = false;
                if (tsbSelObj.Checked)
                {
                    if (SelList.Count == 0) // Выбор прямоугольником
                    {
                        RectangleF rect = CurRct(FstPnt, LstPnt);
                        foreach (Draws drw in DrawsList)
                            if (rect.Contains(drw.GetBounds)) SelList.Add(drw);
                        this.Refresh();
                    }
                    else
                        if (!((LstPnt.X == FstPnt.X) && (LstPnt.Y == FstPnt.Y)))
                        {
                            FileChanged = true;
                            if (markerIndex == 0)
                                SelList.Offset(LstPnt);
                            else
                                SelList.UpdateSize(LstPnt);
                            this.Refresh();
                        }
                }
                else
                    if (tsbAddLine.Checked && (!((FstPnt.X == LstPnt.X) && (FstPnt.Y == LstPnt.Y))))
                    {   // Рисование полилинии (изначально линии)
                        Polylines rect = new Polylines(FstPnt.X, FstPnt.Y, LstPnt.X, LstPnt.Y);
                        rect.Fill.Mode = FillMode.None;
                        rect.Fill.PatternIndex = 0;
                        rect.Fill.Color = DrawsBackColor;
                        rect.Stroke.Color = DrawsForeColor;
                        FileChanged = true;
                        DrawsList.Add(rect);
                        SelList.Add(rect);
                        this.Refresh();
                    }
                    else
                    {
                        RectangleF rect = CurRct(FstPnt, LstPnt);
                        if (tsbAddRect.Checked && (rect.Width > 1) && (rect.Height > 1))
                        {   // Рисование полигона (изначально прямоугольника)
                            Rects rectangle = new Rects(rect);
                            rectangle.Fill.Color = DrawsBackColor;
                            rectangle.Stroke.Color = DrawsForeColor;
                            FileChanged = true;
                            DrawsList.Add(rectangle);
                            SelList.Add(rectangle);
                            this.Refresh();
                        }
                        else
                            if (tsbAddEllipse.Checked && (rect.Width > 1) && (rect.Height > 1))
                            {   // Рисование эллипса
                                Ovals ellipse = new Ovals(rect);
                                ellipse.Fill.Color = DrawsBackColor;
                                ellipse.Stroke.Color = DrawsForeColor;
                                FileChanged = true;
                                DrawsList.Add(ellipse);
                                SelList.Add(ellipse);
                                this.Refresh();
                            }
                            else
                                if (tsbAddPolygon.Checked && (rect.Width > 1) && (rect.Height > 1))
                                {   // Рисование полигона (изначально прямоугольника)
                                    Polygones poligon = new Polygones(rect);
                                    poligon.Fill.Color = DrawsBackColor;
                                    poligon.Stroke.Color = DrawsForeColor;
                                    FileChanged = true;
                                    DrawsList.Add(poligon);
                                    SelList.Add(poligon);
                                    this.Refresh();
                                }
                    }
            }
            FstPnt = LstPnt;
        }

        private void DoSelectObjMode()
        {
            foreach (ToolStripButton btn in tsFigures.Items) btn.Checked = false;
            tsbSelObj.Checked = true;
        }

        private void ChangeMode_Click(object sender, EventArgs e)
        {
            foreach (ToolStripButton btn in tsFigures.Items) btn.Checked = false;
            ((ToolStripButton)sender).Checked = true;
        }

        public void SaveScheme()
        {
            if (CurrFileName != "") SaveFileAs(CurrFileName);
            else
            {
                ISchemeEditor mf = (ISchemeEditor)this.MdiParent;
                mf.SaveSchemeAs();
            }
        }

        private void SaveToStream(MemoryStream stream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            List<Draws> list = new List<Draws>();
            foreach (Draws drw in DrawsList) list.Add(drw);
            formatter.Serialize(stream, list);
            stream.Position = 0;
        }

        private List<Draws> LoadFromStream(MemoryStream stream)
        {
            List<Draws> list = new List<Draws>();
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                stream.Position = 0;
                list = (List<Draws>)formatter.Deserialize(stream);
                return (list);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
        }

        public void SaveFileAs(string FileName)
        {
            CurrFileName = FileName;
            this.Text = CurrFileName;
            using (FileStream fs = new FileStream(FileName, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                //SoapFormatter formatter = new SoapFormatter();
                AllDraws alldrw = new AllDraws();
                try
                {
                    foreach (Draws drw in DrawsList) alldrw.Add(drw);
                    formatter.Serialize(fs, alldrw);
                    FileChanged = false;
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                    throw;
                }
            }
        }

        public void LoadFile(string FileName)
        {
            if (FileName != "")
            {
                CurrFileName = FileName;
                this.Text = CurrFileName;
                FileStream fs = new FileStream(FileName, FileMode.Open);
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    DrawsList = (AllDraws)formatter.Deserialize(fs);
                    foreach (Draws drw in DrawsList) drw.Initialize();
                    FileChanged = false;
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
                this.Refresh();
            }
        }

        public void SelectAllFigures()
        {   // выбор всех объектов
            SelList.Clear();
            foreach (Draws drw in DrawsList) SelList.Add(drw);
            this.Refresh();
        }

        private void frmChildMdi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control) controlPressed = true;
            Single step = 8; if (controlPressed) step = 1;
            if (e.KeyCode == Keys.Up)
            { FileChanged = true; SelList.MoveUp(step); this.Refresh(); }
            else if (e.KeyCode == Keys.Down)
            { FileChanged = true; SelList.MoveDown(step); this.Refresh(); }
            else if (e.KeyCode == Keys.Left)
            { FileChanged = true; SelList.MoveLeft(step); this.Refresh(); }
            else if (e.KeyCode == Keys.Right)
            { FileChanged = true; SelList.MoveRight(step); this.Refresh(); }
            else if (e.KeyCode == Keys.Delete) // удаление выделенных объектов
            {
                if ((SelList.Count > 0) &&
                    (MessageBox.Show("Удалить выделенные объекты?", "Редактор примитивов",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    FileChanged = true;
                    foreach (Draws drw in SelList) DrawsList.Remove(drw);
                    SelList.Clear();
                    GC.Collect();
                    this.Refresh();
                }
            }
            e.Handled = true;
        }

        private void frmChildMdi_KeyUp(object sender, KeyEventArgs e)
        {
            if (!e.Control) controlPressed = false;
        }

        private void miFillChange_Click(object sender, EventArgs e)
        {
            Draws d = (Draws)cmsFigPopup.Tag;
            using (FillProps form = new FillProps(d))
            {
                if (SelList.Count > 0)
                {
                    form.Fill = new Fill();
                    form.Fill.Assign(d.Fill);
                }
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    FileChanged = true;
                    foreach (Draws drw in SelList) drw.Fill.Assign(form.Fill);
                    this.Refresh();
                }
            }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            ISchemeEditor mf = (ISchemeEditor)this.MdiParent;
            mf.NewSchemeForm("");
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            ISchemeEditor mf = (ISchemeEditor)this.MdiParent;
            mf.OpenSchemeFormAs();
        }

        public bool HasSelectedFigures()
        {
            return (SelList.Count > 0);
        }

        public bool HasFiguresInClipboard()
        {
            return (Clipboard.ContainsData(DrawsFormat.Name));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tsbSave.Enabled = FileChanged;
            tsbCut.Enabled = HasSelectedFigures();
            miCut.Enabled = tsbCut.Enabled;
            miCutPopup.Enabled = tsbCut.Enabled;
            tsbCopy.Enabled = HasSelectedFigures();
            miCopy.Enabled = tsbCopy.Enabled;
            miCopyPopup.Enabled = tsbCopy.Enabled;
            tsbPaste.Enabled = HasFiguresInClipboard();
            miPaste.Enabled = tsbPaste.Enabled;
            miPasteBkg.Enabled = tsbPaste.Enabled;
            tsbUndo.Enabled = IsCanUndoChanges();
            miUndo.Enabled = tsbUndo.Enabled;
            tsbRedo.Enabled = IsCanRedoChanges();
            miRedo.Enabled = tsbRedo.Enabled;
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            this.SaveScheme();
        }

        private void miStrokeChange_Click(object sender, EventArgs e)
        {
            Draws d = (Draws)cmsFigPopup.Tag;
            using (StrokeProps form = new StrokeProps(d))
            {
                if (SelList.Count > 0)
                {
                    form.Stroke = new Stroke();
                    form.Stroke.Assign(d.Stroke);
                }
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    FileChanged = true;
                    foreach (Draws drw in SelList) drw.Stroke.Assign(form.Stroke);
                    this.Refresh();
                }
            }
        }

        private void miTurnLeft90_Click(object sender, EventArgs e)
        {
            FileChanged = true;
            foreach (Draws drw in SelList) drw.Rotate(-90F);
            this.Refresh();
        }

        private void miTurnRight90_Click(object sender, EventArgs e)
        {
            FileChanged = true;
            foreach (Draws drw in SelList) drw.Rotate(90F);
            this.Refresh();
        }

       private void miDeleteFigureNode_Click(object sender, EventArgs e)
        {
            Draws drw = (Draws)cmsFigPopup.Tag;
            if (drw != null)
            {
                FileChanged = true;
                drw.DeleteFigureNode(markerIndex);
                this.Refresh();
            }
        }

        private void miAddFigureNode_Click(object sender, EventArgs e)
        {
            Draws drw = (Draws)cmsFigPopup.Tag;
            if (drw != null)
            {
                FileChanged = true;
                drw.InsertFigureNode(SelMrkPnt);
                this.Refresh();
            }
        }

        private void miFlipVertical_Click(object sender, EventArgs e)
        { // отразить фигуру по вертикали
            FileChanged = true;
            foreach (Draws drw in SelList) drw.FlipVertical();
            this.Refresh();
        }

        private void miFlipHorizontal_Click(object sender, EventArgs e)
        { // отразить фигуру по горизонтали
            FileChanged = true;
            foreach (Draws drw in SelList) drw.FlipHorizontal();
            this.Refresh();
        }

        private void miBringToFront_Click(object sender, EventArgs e)
        { // переместить фигуру выше всех фигур
            FileChanged = true;
            foreach (Draws drw in SelList)
            {
                DrawsList.Remove(drw);
                DrawsList.Add(drw);
            }
            this.Refresh();
        }

        private void miSendToBack_Click(object sender, EventArgs e)
        { // переместить фигуру ниже всех вигур
            FileChanged = true;
            foreach (Draws drw in SelList)
            {
                DrawsList.Remove(drw);
                DrawsList.Insert(0, drw);
            }
            this.Refresh();
        }

        private void miBeginChangeNodes_Click(object sender, EventArgs e)
        {
            if (SelList.Count == 1)
            {
                Draws drw = (Draws)SelList[0];
                if (!drw.NodeChanging)
                {
                    drw.NodeChanging = true;
                    DoSelectObjMode();
                    this.Refresh();
                }
            }
        }

        private void miEndChangeNodes_Click(object sender, EventArgs e)
        {
            if (SelList.Count == 1)
            {
                Draws drw = (Draws)SelList[0];
                if (drw.NodeChanging)
                {
                    drw.NodeChanging = false;
                    this.Refresh();
                }
            }
        }

        private void miGroupFigures_Click(object sender, EventArgs e)
        {
            if (SelList.Count > 1)
            {
                FileChanged = true;
                Groups dg = new Groups();
                foreach (Draws drw in SelList)
                {
                    if (drw is Groups)
                    {
                        List<Draws> list = UngroupFigures(drw);
                        foreach (Draws d in list) dg.AddFigure(d);
                    }
                    else
                        dg.AddFigure(drw);
                }
                // удаление перемещенных в группу
                foreach (Draws drw in SelList) DrawsList.Remove(drw);
                SelList.Clear(); // очистка списков
                GC.Collect();
                DrawsList.Add(dg);
                SelList.Add(dg);
                this.Refresh();
            }
        }

        private List<Draws> UngroupFigures(Draws drw)
        {
            List<Draws> par = new List<Draws>();
            par.Add(drw);
            return (UngroupFigures(par));
        }

        private List<Draws> UngroupFigures(List<Draws> sellist)
        {
            List<Draws> res = new List<Draws>();
            foreach (Draws drw in sellist)
            {
                Groups dg = drw as Groups;
                // элемент dg является группой
                if (dg != null)
                {   // извлечение элементов из группы
                    ArrayList list = dg.ExtractFigures();
                    // добавление извлеченных в общий список
                    foreach (Draws d in list) res.Add(d);
                    // удаление ненужной группы
                    DrawsList.Remove(dg);
                }
            }
            return (res);
        }

        private void miUngroupFigures_Click(object sender, EventArgs e)
        {
            FileChanged = true;
            List<Draws> list = UngroupFigures(SelList);
            SelList.Clear(); // очистка списка выбора
            GC.Collect();
            foreach (Draws drw in list)
            {
                DrawsList.Add(drw);
                SelList.Add(drw);
            }
            this.Refresh();
        }

        public void CopySelectedToClipboard()
        {
            List<Draws> forcopy = new List<Draws>();
            foreach (Draws drw in SelList) forcopy.Add(drw);
            DataObject clipboardDataObject = new DataObject(DrawsFormat.Name, forcopy);
            Clipboard.SetDataObject(clipboardDataObject, false);
        }

        private void miCopy_Click(object sender, EventArgs e)
        {
            CopySelectedToClipboard();
        }

        public void PasteFromClipboardAndSelected()
        {
            if (Clipboard.ContainsData(DrawsFormat.Name))
            {
                FileChanged = true;
                IDataObject clipboardRetrievedObject = Clipboard.GetDataObject();
                List<Draws> pastedObject =
                     (List<Draws>)clipboardRetrievedObject.GetData(DrawsFormat.Name);
                SelList.Clear();
                foreach (Draws drw in pastedObject)
                {
                    drw.Offset(new PointF(5, 5));
                    DrawsList.Add(drw);
                    SelList.Add(drw);
                }
                this.Refresh();
            }
        }

        private void miPaste_Click(object sender, EventArgs e)
        {
            PasteFromClipboardAndSelected();
        }

        public void CutSelectedToClipboard()
        {
            List<Draws> forcopy = new List<Draws>();
            foreach (Draws drw in SelList) forcopy.Add(drw);
            DataObject clipboardDataObject = new DataObject(DrawsFormat.Name, forcopy);
            Clipboard.SetDataObject(clipboardDataObject, false);
            FileChanged = true;
            foreach (Draws drw in SelList) DrawsList.Remove(drw);
            SelList.Clear();
            GC.Collect();
            this.Refresh();
        }

       private void miCut_Click(object sender, EventArgs e)
        {
            CutSelectedToClipboard();
        }

       private void tsbUndo_Click(object sender, EventArgs e)
       {
           UndoChanges();
       }

       private void tsbRedo_Click(object sender, EventArgs e)
       {
           RedoChanges();
       }

        private void miScriptEdit_Click(object sender, EventArgs e)
       {
           if (SelList.Count == 1)
           {
               Draws drw = (Draws)SelList[0];
               using (ScriptProps form =
                   new ScriptProps(drw, drw.ScriptOnDoubleClick, drw.ScriptErrors()))
               {
                   if (form.ShowDialog(this) == DialogResult.OK)
                   {
                       FileChanged = true;
                       drw.ScriptOnDoubleClick = form.GetEditedText();
                   }
               }
           }
       }

       private void drawBox_DoubleClick(object sender, EventArgs e)
       {
           Draws drw = DrawsList.PointInFigure(LstPnt);
           if (drw != null)
           {   // элемент был выбран мышкой
               using (EditText form = new EditText(drw.Text.Caption))
               {
                   if (form.ShowDialog(this) == DialogResult.OK)
                   {
                       drw.Text.Caption = form.Caption;
                       FileChanged = true;
                       this.Refresh();
                   }
               }
           }
       }

       private void tsbColors_Paint(object sender, PaintEventArgs e)
       {
           Graphics g = e.Graphics;
           g.Clear(Color.White);
           g.DrawImage(tsbColors.Image, 2, 2);
           Rectangle forerect = new Rectangle(8, 9, 11, 11);
           using (SolidBrush brush = new SolidBrush(DrawsForeColor))
           {
               g.FillRectangle(brush, forerect);
           }
           Point[] pts = new Point[6];
           pts[0].X = 21; pts[0].Y = 16;
           pts[1].X = 26; pts[1].Y = 16;
           pts[2].X = 26; pts[2].Y = 27;
           pts[3].X = 15; pts[3].Y = 27;
           pts[4].X = 15; pts[4].Y = 22;
           pts[5].X = 21; pts[5].Y = 22;
           using (SolidBrush brush = new SolidBrush(DrawsBackColor))
           {
               g.FillPolygon(brush, pts);
           }
       }

       private Color MousePosToColor(int x, int y)
       {
           Color result = Color.Transparent;
           Rectangle[] rects = new Rectangle[28];
           int xh = 35;
           for (int i = 0; i < 14; i++)
           {
               rects[i].X = xh;
               rects[i].Y = 4;
               rects[i].Width = 10;
               rects[i].Height = 10;
               xh += 16;
           }
           xh = 35;
           for (int i = 14; i < 28; i++)
           {
               rects[i].X = xh;
               rects[i].Y = 20;
               rects[i].Width = 10;
               rects[i].Height = 10;
               xh += 16;
           }
           using (Bitmap image = new Bitmap(tsbColors.Image))
           {
               for (int i = 0; i < 28; i++)
               {
                   if (rects[i].Contains(x, y))
                   {
                       if (i == 14)
                           result = Color.White;
                       else
                           result = image.GetPixel(x, y);
                       break;
                   }
               }
           }
           return (result);
       }

       private void tsbColors_MouseDown(object sender, MouseEventArgs e)
       {
           Color selcolor;
           if (e.Button == MouseButtons.Left)
           {
               selcolor = MousePosToColor(e.X, e.Y);
               if (selcolor != Color.Transparent)
               {
                   DrawsForeColor = selcolor;
                   Text = selcolor.ToString();
                   tsColors.Refresh();
                   foreach (Draws drw in SelList) drw.Stroke.Color = selcolor;
                   drawBox.Refresh();
               }
           }
           else if (e.Button == MouseButtons.Right)
           {
               selcolor = MousePosToColor(e.X, e.Y);
               if (selcolor != Color.Transparent)
               {
                   DrawsBackColor = selcolor;
                   Text = selcolor.ToString();
                   tsColors.Refresh();
                   foreach (Draws drw in SelList) drw.Fill.Color = selcolor;
                   drawBox.Refresh();
               }
           }
       }

       private void miText_Click(object sender, EventArgs e)
       {
           Draws d = (Draws)cmsFigPopup.Tag;
           using (TextProps form = new TextProps(d))
           {
               if (SelList.Count > 0)
               {
                   form.DrawText = new Text();
                   form.DrawText.Assign(d.Text);
               }
               if (form.ShowDialog(this) == DialogResult.OK)
               {
                   FileChanged = true;
                   foreach (Draws drw in SelList) drw.Text.Assign(form.DrawText);
                   this.Refresh();
               }
           }
       }

       private void exitToolStripMenuItem_Click(object sender, EventArgs e)
       {
           Close();
       }

       private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
       {
           ISchemeEditor mf = (ISchemeEditor)this.MdiParent;
           mf.SaveSchemeAs();
       }

       private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
       {
           //using (frmAbout form = new frmAbout("Редактор мнемосхем","1.0"))
           //{
           //    form.ShowDialog();
           //}
       }
    }
}
