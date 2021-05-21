using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using BaseServer;

namespace DataEditor
{
    public partial class frmReportEditor : Form
    {
        public string ReportName
        {
            get
            {
                return (printReport != null) ? printReport.ReportName : String.Empty;
            }
        }
        PrintReport printReport;

        public frmReportEditor(Form parent, string reportName = "")
        {
            InitializeComponent();
            this.MouseWheel += drawBox_MouseWheel;
            printReport = new PrintReport(printDocument);
        }

        private bool controlPressed = false;
        private bool shiftPressed = false;
        private bool altPressed = false;

        private void drawBox_MouseWheel(object sender, MouseEventArgs e)
        {
            int step = 32;
            if (e.Delta > 0)
            {
                Point pos = drawBox.Location;
                if (controlPressed)
                {
                    if (drawBox.Width > pnlScroll.Width)
                    {
                        pos.X += step;
                        if (pos.X > 0) pos.X = 0;
                    }
                }
                else
                {
                    if (drawBox.Height > pnlScroll.Height)
                    {
                        pos.Y += step;
                        if (pos.Y > 0) pos.Y = 0;
                    }
                }
                drawBox.Location = pos;
                CalculateScrollBars();
            }
            if (e.Delta < 0)
            {
                Point pos = drawBox.Location;
                if (controlPressed)
                {
                    if (drawBox.Width > pnlScroll.Width)
                    {
                        pos.X -= step;
                        if (pnlScroll.Width - pos.X > drawBox.Width)
                            pos.X = pnlScroll.Width - drawBox.Width;
                    }
                }
                else
                {
                    if (drawBox.Height > pnlScroll.Height)
                    {
                        pos.Y -= step;
                        if (pnlScroll.Height - pos.Y > drawBox.Height)
                            pos.Y = pnlScroll.Height - drawBox.Height;
                    }
                }
                drawBox.Location = pos;
                CalculateScrollBars();
            }
        }

        private void frmReportEditor_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            //---------------------------
            tscbFontName.Items.Clear();
            using (InstalledFontCollection ifc = new InstalledFontCollection())
            {
                IEnumerator ie;
                ie = ifc.Families.GetEnumerator();
                while (ie.MoveNext())
                {
                    string fontFamily = ie.Current.ToString();
                    //[FontFamily: Name=Algerian]
                    tscbFontName.Items.Add(
                        fontFamily.Substring(18, fontFamily.Length - 19));
                }
            }
            //---------------------------
            printDocument.PrinterSettings.DefaultPageSettings.Margins =
                    new Margins(20, 20, 20, 20);
            calcWorkRects();
        }

        private void calcWorkRects()
        {
            printDocument.PrinterSettings.PrinterName = printReport.PrinterName;
            PageSettings ps =
                printDocument.PrinterSettings.DefaultPageSettings;
            printReport.PageRect = ps.Bounds;
            drawBox.Size = printReport.PageRect.Size;
            printReport.DrawRect = new Rectangle(ps.Margins.Left, ps.Margins.Top,
                printReport.PageRect.Right - ps.Margins.Right - ps.Margins.Left,
                printReport.PageRect.Bottom - ps.Margins.Bottom - ps.Margins.Top);
            UpdateDrawBox();
            CalculateScrollBars();
        }

        public void LoadReport(string reportname)
        {
            if (reportname.Length > 0)
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                    printReport.LoadReport(reportname, String.Empty);
                    drawBox.Size = printReport.PageRect.Size;
                    UpdateDrawBox();
                    CalculateScrollBars();
                    tsbCut.Enabled = false;
                    tsbCopy.Enabled = false;
                    GC.Collect();
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        public void SelectOrNewReport()
        {
            using (frmReportOpen form = new frmReportOpen(printReport.ReportName))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    try
                    {
                        LoadReport(form.SelectedReport());
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
                else
                    if (!DoNewReport()) this.Close();
                drawBox.Invalidate();
            }
        }

        private void tsbPageProps_Click(object sender, EventArgs e)
        {
            PrinterSettings ps = printDocument.PrinterSettings;
            using (frmPageProp form = new frmPageProp(
                ps.DefaultPageSettings.Landscape,
                ps.DefaultPageSettings.Margins,
                ps.DefaultPageSettings.PaperSize.PaperName,
                ps.PaperSizes))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    hasChanges = true;
                    ps.DefaultPageSettings.Landscape = form.Landscape;
                    ps.DefaultPageSettings.Margins = form.Margins;
                    SetPaperSize(ref ps, form.PaperName);
                    calcWorkRects();
                }
            }
        }

        private static void SetPaperSize(ref PrinterSettings ps, string PaperName)
        {
            for (int i = 0; i < ps.PaperSizes.Count; i++)
                if (ps.PaperSizes[i].PaperName == PaperName)
                {
                    ps.DefaultPageSettings.PaperSize = ps.PaperSizes[i];
                    break;
                }
        }

        private void tsbReportProps_Click(object sender, EventArgs e)
        {
            using (frmReportProp form = new frmReportProp(printReport.PrinterName,
                printReport.Descriptor))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    hasChanges = true;
                    printReport.PrinterName = form.PrinterName;
                    printReport.Descriptor = form.Descriptor;
                    calcWorkRects();
                }
            }
        }

        private void drawBox_Paint(object sender, PaintEventArgs e)
        {
            printReport.PaintPage(e.Graphics, true);
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            printDocument.Print();
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            printReport.PaintPage(e.Graphics);
        }


        private void pnlScroll_Resize(object sender, EventArgs e)
        {
            UpdateDrawBox();
        }

        private void UpdateDrawBox()
        {
            Point location = new Point();
            if (pnlScroll.Width > drawBox.Width)
                location.X = (pnlScroll.Width - drawBox.Width) / 2;
            if (pnlScroll.Height > drawBox.Height)
                location.Y = (pnlScroll.Height - drawBox.Height) / 2;
            drawBox.Location = location;
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
                foreach (Plot plt in printReport.SelList)
                {
                    if (shiftPressed)
                    {
                        SizeF size = plt.Size;
                        if (size.Height - step >= 0)
                        {
                            size.Height -= step; plt.Size = size;
                            drawBox.Invalidate();
                        }
                    }
                    else
                    {
                        PointF offset = plt.Location;
                        if (offset.Y - step >= 0)
                        {
                            offset.Y -= step; plt.Location = offset;
                            drawBox.Invalidate();
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                hasChanges = true;
                foreach (Plot plt in printReport.SelList)
                {
                    if (shiftPressed)
                    {
                        SizeF size = plt.Size;
                        size.Height += step; plt.Size = size;
                        drawBox.Invalidate();
                    }
                    else
                    {
                        PointF offset = plt.Location;
                        if (offset.Y + plt.BoundsRect.Height + step <= drawBox.Height)
                        {
                            offset.Y += step; plt.Location = offset;
                            drawBox.Invalidate();
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                hasChanges = true;
                foreach (Plot plt in printReport.SelList)
                {
                    if (shiftPressed)
                    {
                        SizeF size = plt.Size;
                        if (size.Width - step >= 0)
                        {
                            size.Width -= step; plt.Size = size;
                            drawBox.Invalidate();
                        }
                    }
                    else
                    {
                        PointF offset = plt.Location;
                        if (offset.X > 0)
                        {
                            offset.X -= step; plt.Location = offset;
                            drawBox.Invalidate();
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                hasChanges = true;
                foreach (Plot plt in printReport.SelList)
                {
                    if (shiftPressed)
                    {
                        SizeF size = plt.Size;
                        size.Width += step; plt.Size = size;
                        drawBox.Invalidate();
                    }
                    else
                    {
                        PointF offset = plt.Location;
                        if (offset.X + plt.BoundsRect.Width < drawBox.Width)
                        {
                            offset.X += step; plt.Location = offset;
                            drawBox.Invalidate();
                        }
                    }
                }
            }
            #endregion
            if (e.KeyCode == Keys.Delete)
            {
                deleteSelected();
            }
        }

        private void deleteSelected()
        {
            hasChanges = true;
            foreach (Plot plt in printReport.SelList) printReport.PlotList.Remove(plt);
            printReport.SelList.Clear();
            tsbCut.Enabled = false;
            tsbCopy.Enabled = false;
            drawBox.Invalidate();
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

        private Point selpoint = new Point();
        //private Rectangle selrect = new Rectangle();
        private Point selrectpoint = new Point();
        bool selectByRect = false;
        //bool dragByRect = false;
        int selplot = 0;

        private Plot SelectExitsElement(MouseEventArgs e)
        {
            Plot plt = printReport.GetPlotAt(e.X, e.Y);
            return plt;
        }

        private void buildUnionSelRect(Plot plt)
        {
            printReport.SelRect = Rectangle.Ceiling(plt.BoundsRect);
            foreach (Plot item in printReport.SelList)
                printReport.SelRect = Rectangle.Union(printReport.SelRect,
                    Rectangle.Ceiling(item.BoundsRect));
        }

        private void drawBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // запоминание точки первого нажатия для выбора прямоугольником
                selpoint.X = e.X;
                selpoint.Y = e.Y;
                printReport.SelRect.Location = selpoint;
                Plot plt = SelectExitsElement(e);
                if (plt == null)
                {
                    printReport.SelList.Clear();
                    tsbCut.Enabled = false;
                    tsbCopy.Enabled = false;
                    selectByRect = true;
                    tstbText.Text = String.Empty;
                    tsEdit.Enabled = false;
                    tsHourOffset.Enabled = false;
                }
                else
                {
                    // выбор мышью существующего элемента
                    if (!controlPressed && printReport.SelList.IndexOf(plt) < 0)
                        printReport.SelList.Clear();
                    if (printReport.SelList.IndexOf(plt) < 0)
                    {
                        printReport.SelList.Add(plt);
                        UpdateFontsPanel(plt);
                        updateTextInput(plt);
                    }
                    else
                        if (controlPressed) printReport.SelList.Remove(plt);
                    if (printReport.SelList.Count > 0)
                    {
                        tsbCut.Enabled = true;
                        tsbCopy.Enabled = true;
                    }
                    drawBox.Refresh();
                    printReport.DragByRect = true;
                    buildUnionSelRect(plt);
                    selrectpoint.X = printReport.SelRect.X;
                    selrectpoint.Y = printReport.SelRect.Y;
                 }
            }
            else if (e.Button == MouseButtons.Right)
            {
                #region Вызов контектсного меню
                Plot plt = SelectExitsElement(e);
                if (plt == null)
                {
                    printReport.SelList.Clear();
                    tstbText.Text = String.Empty;
                    tsEdit.Enabled = false;
                    tsbCut.Enabled = false;
                    tsbCopy.Enabled = false;
                }
                else
                {
                    if (printReport.SelList.IndexOf(plt) < 0)
                    {
                        printReport.SelList.Clear();
                        printReport.SelList.Add(plt);
                        updateTextInput(plt);
                    }
                    tsbCut.Enabled = true;
                    tsbCopy.Enabled = true;
                }
                drawBox.Invalidate();
                #endregion
            }
            tbDrawBox.Focus();
        }

        private void updateTextInput(Plot plt)
        {
            tsEdit.Enabled = true;
            switch (plt.Kind)
            {
                case 2:
                    tslTextDesc.Text = "Параметр:";
                    tstbText.Text = (string)plt.Props["PtName"];
                    tsHourOffset.Enabled = false;
                    break;
                case 3:
                    tslTextDesc.Text = "Параметр:";
                    tstbText.Text = (string)plt.Props["PtName"];
                    tsHourOffset.Enabled = true;
                    tslSnapName.Text = "Час:";
                    tscbSnap.Items.Clear();
                    for (int i = 0; i <= 23; i++) tscbSnap.Items.Add(i.ToString());
                    tscbSnap.SelectedIndexChanged -= tscbSnap_SelectedIndexChanged;
                    tscbSnap.SelectedIndex = tscbSnap.Items.IndexOf(plt.Props["Snap"].ToString());
                    tscbSnap.SelectedIndexChanged += tscbSnap_SelectedIndexChanged;
                    tslOffsetName.Text = "Суток назад:";
                    tscbOffset.Items.Clear();
                    for (int i = 0; i <= 32; i++) tscbOffset.Items.Add(i.ToString());
                    tscbOffset.SelectedIndexChanged -= tscbOffset_SelectedIndexChanged;
                    tscbOffset.SelectedIndex = tscbOffset.Items.IndexOf(plt.Props["Offset"].ToString());
                    tscbOffset.SelectedIndexChanged += tscbOffset_SelectedIndexChanged;
                    updateColumnLines(plt);
                    break;
                case 4:
                    tslTextDesc.Text = "Параметр:";
                    tstbText.Text = (string)plt.Props["PtName"];
                    tsHourOffset.Enabled = true;
                    tslSnapName.Text = "День:";
                    tscbSnap.Items.Clear();
                    for (int i = 1; i <= 31; i++) tscbSnap.Items.Add(i.ToString());
                    tscbSnap.SelectedIndexChanged -= tscbSnap_SelectedIndexChanged;
                    tscbSnap.SelectedIndex = tscbSnap.Items.IndexOf(plt.Props["Snap"].ToString());
                    tscbSnap.SelectedIndexChanged += tscbSnap_SelectedIndexChanged;
                    tslOffsetName.Text = "Месяцев назад:";
                    tscbOffset.Items.Clear();
                    for (int i = 0; i <= 24; i++) tscbOffset.Items.Add(i.ToString());
                    tscbOffset.SelectedIndexChanged -= tscbOffset_SelectedIndexChanged;
                    tscbOffset.SelectedIndex = tscbOffset.Items.IndexOf(plt.Props["Offset"].ToString());
                    tscbOffset.SelectedIndexChanged += tscbOffset_SelectedIndexChanged;
                    updateColumnLines(plt);
                    break;
                case 5:
                    tslTextDesc.Text = "Параметр:";
                    tstbText.Text = (string)plt.Props["PtName"];
                    tsHourOffset.Enabled = true;
                    tslSnapName.Text = "Месяц:";
                    tscbSnap.Items.Clear();
                    for (int i = 1; i <= 12; i++) tscbSnap.Items.Add(i.ToString());
                    tscbSnap.SelectedIndexChanged -= tscbSnap_SelectedIndexChanged;
                    tscbSnap.SelectedIndex = tscbSnap.Items.IndexOf(plt.Props["Snap"].ToString());
                    tscbSnap.SelectedIndexChanged += tscbSnap_SelectedIndexChanged;
                    tslOffsetName.Text = "Лет назад:";
                    tscbOffset.Items.Clear();
                    for (int i = 0; i <= 10; i++) tscbOffset.Items.Add(i.ToString());
                    tscbOffset.SelectedIndexChanged -= tscbOffset_SelectedIndexChanged;
                    tscbOffset.SelectedIndex = tscbOffset.Items.IndexOf(plt.Props["Offset"].ToString());
                    tscbOffset.SelectedIndexChanged += tscbOffset_SelectedIndexChanged;
                    updateColumnLines(plt);
                    break;
                default:
                    tslTextDesc.Text = "Текст:";
                    tstbText.Text = (string)plt.Props["Text"];
                    tsHourOffset.Enabled = false;
                    break;
            }
            tstbText.SelectAll();
        }

        private void updateColumnLines(Plot plt)
        {
            tscbColumnLines.SelectedIndexChanged -= tscbColumnLines_SelectedIndexChanged;
            tscbColumnLines.SelectedIndex = tscbColumnLines.Items.
                IndexOf(plt.Props["ColumnLines"].ToString());
            tscbColumnLines.SelectedIndexChanged += tscbColumnLines_SelectedIndexChanged;
            //--------------------------------------------------------------------------
            tscbLineBetween.SelectedIndexChanged -= tscbLineBetween_SelectedIndexChanged;
            tscbLineBetween.SelectedIndex = tscbLineBetween.Items.
                IndexOf(plt.Props["LineBetween"].ToString());
            tscbLineBetween.SelectedIndexChanged += tscbLineBetween_SelectedIndexChanged;
            //--------------------------------------------------------------------------
            tscbAgregate.SelectedIndexChanged -= tscbAgregate_SelectedIndexChanged;
            tscbAgregate.SelectedIndex = (int)plt.Props["Agregate"];
            tscbAgregate.SelectedIndexChanged += tscbAgregate_SelectedIndexChanged;
        }

        private void UpdateFontsPanel(Plot plt)
        {
            tscbFontName.SelectedIndexChanged -= tscbFontName_SelectedIndexChanged;
            tscbFontName.Text = (string)plt.Props["FontName"];
            tscbFontName.SelectedIndexChanged += tscbFontName_SelectedIndexChanged;
            tscbFontSize.SelectedIndexChanged -= tscbFontSize_SelectedIndexChanged;
            tscbFontSize.Text = plt.Props["FontSize"].ToString();
            tscbFontSize.SelectedIndexChanged += tscbFontSize_SelectedIndexChanged;
            tsbBold.Checked = (bool)plt.Props["FontBold"];
            tsbItalic.Checked = (bool)plt.Props["FontItalic"];
            tsbUnderline.Checked = (bool)plt.Props["FontUnderline"];
            int align = (int)plt.Props["Alignment"];
            tsbAlignLeft.Checked = align == 0;
            tsbAlignCenter.Checked = align == 1;
            tsbAlignRight.Checked = align == 2;
        }

        private void drawBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectByRect)
            {
                if (e.X >= selpoint.X && e.Y >= selpoint.Y)
                    printReport.SelRect = new Rectangle(selpoint.X, selpoint.Y,
                        e.X - selpoint.X, e.Y - selpoint.Y);
                else if (e.X < selpoint.X && e.Y >= selpoint.Y)
                    printReport.SelRect = new Rectangle(e.X, selpoint.Y,
                        selpoint.X - e.X, e.Y - selpoint.Y);
                else if (e.X >= selpoint.X && e.Y < selpoint.Y)
                    printReport.SelRect = new Rectangle(selpoint.X, e.Y,
                        e.X - selpoint.X, selpoint.Y - e.Y);
                else
                    printReport.SelRect = new Rectangle(e.X, e.Y,
                        selpoint.X - e.X, selpoint.Y - e.Y);
                drawBox.Refresh();
            }
            if (printReport.DragByRect)
            {
                printReport.SelRect.Location = selrectpoint;
                printReport.SelRect.Offset(e.X - selpoint.X, e.Y - selpoint.Y);
                drawBox.Refresh();
            }
        }

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

        private void selectArrow()
        {
            selplot = 0;
            tsbText.Checked = false;
            tsbReal.Checked = false;
            tsbHour.Checked = false;
            tsbDaily.Checked = false;
            tsbMonth.Checked = false;
            tsbArrow.Checked = true;
        }

        private void drawBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (selectByRect)
            {
                selectByRect = false;
                if (selplot > 0)
                {
                    #region Создание нового элемента
                    hasChanges = true;
                    Plot plt = new Plot(selplot);
                    plt.Location = printReport.SelRect.Location;
                    plt.Size = printReport.SelRect.Size;
                    if (printReport.SelRect.Width > 3 && printReport.SelRect.Height > 3)
                        plt.Size = new SizeF(printReport.SelRect.Width, printReport.SelRect.Height);
                    else
                        plt.Size = new SizeF(100f, 25f);
                    plt.Props["FontName"] = tscbFontName.Text;
                    Single size;
                    if (Single.TryParse(tscbFontSize.Text, out size))
                        plt.Props["FontSize"] = size;
                    plt.Props["FontBold"] = tsbBold.Checked;
                    plt.Props["FontItalic"] = tsbItalic.Checked;
                    plt.Props["FontUndeline"] = tsbUnderline.Checked;
                    if (tsbAlignLeft.Checked) plt.Props["Alignment"] = (int)0;
                    else if (tsbAlignCenter.Checked) plt.Props["Alignment"] = (int)1;
                    else if (tsbAlignRight.Checked) plt.Props["Alignment"] = (int)2;
                    printReport.PlotList.Add(plt);
                    UpdateFontsPanel(plt);
                    updateTextInput(plt);
                    printReport.SelList.Clear(); printReport.SelList.Add(plt);
                    tsbCut.Enabled = true;
                    tsbCopy.Enabled = true;
                    selectArrow();
                    #endregion
                }
                else
                { // выбор прямоугольником существующего элемента
                    foreach (Plot plt in printReport.PlotList)
                    {
                        if (printReport.SelRect.Contains(Rectangle.Ceiling(plt.BoundsRect)))
                            printReport.SelList.Add(plt);
                    }
                    if (printReport.SelList.Count > 0)
                    {
                        tsbCut.Enabled = true;
                        tsbCopy.Enabled = true;
                    }
                }
                printReport.SelRect = new Rectangle();
                drawBox.Invalidate();
            }
            else if (printReport.DragByRect)
            {
                printReport.DragByRect = false;
                printReport.SelRect.Location = selrectpoint;
                if (e.X != selpoint.X || e.Y != selpoint.Y)
                {
                    hasChanges = true;
                    printReport.SelRect.Offset(e.X - selpoint.X, e.Y - selpoint.Y);
                    foreach (Plot plt in printReport.SelList)
                    {
                        PointF offset = plt.Location;
                        offset.X += e.X - selpoint.X;
                        offset.Y += e.Y - selpoint.Y;
                        plt.Location = offset;
                    }
                }
                printReport.SelRect = new Rectangle();
                drawBox.Invalidate();
            }
        }

        private void tsbText_Click(object sender, EventArgs e)
        {
            selectArrow();
            tsbArrow.Checked = false;
            ToolStripButton tsb = (ToolStripButton)sender;
            tsb.Checked = true;
            selplot = int.Parse(tsb.Tag.ToString());
        }

        private void tsbArrow_Click(object sender, EventArgs e)
        {
            selectArrow();
        }

        private void tsbBold_Click(object sender, EventArgs e)
        {
            hasChanges = true;
            foreach (Plot plt in printReport.SelList)
                plt.Props["FontBold"] = tsbBold.Checked;
            drawBox.Invalidate();
        }

        private void tsbItalic_Click(object sender, EventArgs e)
        {
            hasChanges = true;
            foreach (Plot plt in printReport.SelList)
                plt.Props["FontItalic"] = tsbItalic.Checked;
            drawBox.Invalidate();
        }

        private void tsbUnderline_Click(object sender, EventArgs e)
        {
            hasChanges = true;
            foreach (Plot plt in printReport.SelList)
                plt.Props["FontUnderline"] = tsbUnderline.Checked;
            drawBox.Invalidate();
        }

        private void tscbFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            hasChanges = true;
            foreach (Plot plt in printReport.SelList)
                plt.Props["FontName"] = tscbFontName.Text;
            drawBox.Invalidate();
            tbDrawBox.Focus();
        }

        private void tscbFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Single size;
            if (Single.TryParse(tscbFontSize.Text, out size))
            {
                hasChanges = true;
                foreach (Plot plt in printReport.SelList)
                    plt.Props["FontSize"] = size;
                drawBox.Invalidate();
                tbDrawBox.Focus();
            }
        }

        private void tsbAlignLeft_Click(object sender, EventArgs e)
        {
            tsbAlignLeft.Checked = true;
            tsbAlignCenter.Checked = false;
            tsbAlignRight.Checked = false;
            hasChanges = true;
            foreach (Plot plt in printReport.SelList)
                plt.Props["Alignment"] = 0;
            drawBox.Invalidate();
        }

        private void tsbAlignCenter_Click(object sender, EventArgs e)
        {
            tsbAlignLeft.Checked = false;
            tsbAlignCenter.Checked = true;
            tsbAlignRight.Checked = false;
            hasChanges = true;
            foreach (Plot plt in printReport.SelList)
                plt.Props["Alignment"] = 1;
            drawBox.Invalidate();
        }

        private void tsbAlignRight_Click(object sender, EventArgs e)
        {
            tsbAlignLeft.Checked = false;
            tsbAlignCenter.Checked = false;
            tsbAlignRight.Checked = true;
            hasChanges = true;
            foreach (Plot plt in printReport.SelList)
                plt.Props["Alignment"] = 2;
            drawBox.Invalidate();
        }

        private void tsbFontMore_Click(object sender, EventArgs e)
        {
            Single fontsize = 0;
            foreach (Plot plt in printReport.SelList)
            {
                Single size = (Single)plt.Props["FontSize"];
                if (size > fontsize) fontsize = size;
            }
            if (fontsize < 72)
            {
                hasChanges = true;
                foreach (Plot plt in printReport.SelList)
                {
                    Single size = (Single)plt.Props["FontSize"];
                    size += 1;
                    plt.Props["FontSize"] = size;
                }
                drawBox.Invalidate();
                incFontSizeText();
            }
        }

        private void incFontSizeText()
        {
            Single size;
            if (Single.TryParse(tscbFontSize.Text, out size))
            {
                tscbFontSize.SelectedIndexChanged -= tscbFontSize_SelectedIndexChanged;
                size += 1f;
                tscbFontSize.Text = size.ToString();
                tscbFontSize.SelectedIndexChanged += tscbFontSize_SelectedIndexChanged;
            }
        }

        private void decFontSizeText()
        {
            Single size;
            if (Single.TryParse(tscbFontSize.Text, out size))
            {
                tscbFontSize.SelectedIndexChanged -= tscbFontSize_SelectedIndexChanged;
                size -= 1f;
                tscbFontSize.Text = size.ToString();
                tscbFontSize.SelectedIndexChanged += tscbFontSize_SelectedIndexChanged;
            }
        }

        private void tsbFontLess_Click(object sender, EventArgs e)
        {
            Single fontsize = 72;
            foreach (Plot plt in printReport.SelList)
            {
                Single size = (Single)plt.Props["FontSize"];
                if (size < fontsize) fontsize = size;
            }
            if (fontsize > 8)
            {
                hasChanges = true;
                foreach (Plot plt in printReport.SelList)
                {
                    Single size = (Single)plt.Props["FontSize"];
                    size -= 1;
                    plt.Props["FontSize"] = size;
                }
                drawBox.Invalidate();
                decFontSizeText();
            }
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
            List<Plot> list = new List<Plot>();
            foreach (Plot drw in printReport.PlotList) list.Add(drw);
            formatter.Serialize(stream, list);
            stream.Position = 0;
        }

        private List<Plot> LoadFromStream(MemoryStream stream)
        {
            List<Plot> list = new List<Plot>();
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                stream.Position = 0;
                list = (List<Plot>)formatter.Deserialize(stream);
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
                printReport.SelList.Clear();
                tsbCut.Enabled = false;
                tsbCopy.Enabled = false;
                printReport.PlotList.Clear();
                GC.Collect();
                using (MemoryStream stream = new MemoryStream())
                {
                    RedoStack.Pop(stream);
                    List<Plot> list = LoadFromStream(stream);
                    foreach (Plot plt in list)
                        printReport.PlotList.Add(plt);
                }
                this.Refresh();
            }
        }

        public void UndoChanges()
        {
            if (IsCanUndoChanges())
            {
                PrepareToRedo(true);
                printReport.SelList.Clear();
                tsbCut.Enabled = false;
                tsbCopy.Enabled = false;
                printReport.PlotList.Clear();
                GC.Collect();
                using (MemoryStream stream = new MemoryStream())
                {
                    UndoStack.Pop(stream);
                    List<Plot> list = LoadFromStream(stream);
                    foreach (Plot plt in list)
                        printReport.PlotList.Add(plt);
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

        DataFormats.Format PlotsFormat = DataFormats.GetFormat("clipboardPlotsFormat");

        public void CopySelectedToClipboard()
        {
            List<Plot> forcopy = new List<Plot>();
            foreach (Plot plt in printReport.SelList) forcopy.Add(plt);
            DataObject clipboardDataObject = new DataObject(PlotsFormat.Name, forcopy);
            Clipboard.SetDataObject(clipboardDataObject, false);
            tsbPaste.Enabled = true;
        }

        private void tsbCopy_Click(object sender, EventArgs e)
        {
            CopySelectedToClipboard();
        }

        public void CutSelectedToClipboard()
        {
            List<Plot> forcopy = new List<Plot>();
            foreach (Plot plt in printReport.SelList) forcopy.Add(plt);
            DataObject clipboardDataObject = new DataObject(PlotsFormat.Name, forcopy);
            Clipboard.SetDataObject(clipboardDataObject, false);
            hasChanges = true;
            foreach (Plot plt in printReport.SelList)
                printReport.PlotList.Remove(plt);
            printReport.SelList.Clear();
            tsbCut.Enabled = false;
            tsbCopy.Enabled = false;
            tsbPaste.Enabled = true;
            GC.Collect();
            drawBox.Refresh();
        }

        private void tsbCut_Click(object sender, EventArgs e)
        {
            CutSelectedToClipboard();
        }

        public void PasteFromClipboardAndSelected()
        {
            if (Clipboard.ContainsData(PlotsFormat.Name))
            {
                hasChanges = true;
                IDataObject clipboardRetrievedObject = Clipboard.GetDataObject();
                List<Plot> pastedObject =
                     (List<Plot>)clipboardRetrievedObject.GetData(PlotsFormat.Name);
                if (pastedObject != null)
                {
                    printReport.SelList.Clear();
                    foreach (Plot plt in pastedObject)
                    {
                        plt.GenerateNewName();
                        printReport.PlotList.Add(plt);
                        printReport.SelList.Add(plt);
                    }
                    if (printReport.SelList.Count > 0)
                    {
                        buildUnionSelRect(printReport.SelList[0]);
                        int dx = (pnlScroll.Width - printReport.SelRect.Width) / 2;
                        int dy = (pnlScroll.Height - printReport.SelRect.Height) / 2;
                        foreach (Plot plt in printReport.SelList)
                        {
                            PointF location = plt.Location;
                            location.X += dx - printReport.SelRect.Left - drawBox.Left;
                            location.Y += dy - printReport.SelRect.Top - drawBox.Top;
                            plt.Location = location;
                        }
                        printReport.SelRect = new Rectangle();
                        tsbCut.Enabled = true;
                        tsbCopy.Enabled = true;
                    }
                    drawBox.Invalidate();
                }
            }
        }

        private void tsbPaste_Click(object sender, EventArgs e)
        {
            PasteFromClipboardAndSelected();
        }

        private void frmReportEditor_Activated(object sender, EventArgs e)
        {
            tsbPaste.Enabled = Clipboard.ContainsData(PlotsFormat.Name);
        }

        private void newReport()
        {
            printReport.NewReport();
            tsbCut.Enabled = false;
            tsbCopy.Enabled = false;
            GC.Collect();
            drawBox.BackColor = Color.White;
            drawBox.Refresh();
            hasChanges = false;
            CalculateScrollBars();
        }

        public bool DoNewReport()
        {
            bool result = false;
            using (frmInputString form = new frmInputString())
            {
                form.DoUpper();
                form.Text = "Введите имя нового отчёта";
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (!String.IsNullOrWhiteSpace(form.Value))
                    {
                        string newname = form.Value;
                        if (!Data.GetReportsList().ContainsKey(newname))
                        {
                            newReport();
                            printReport.ReportName = newname;
                            hasChanges = true;
                            printReport.Descriptor = "Новый отчёт";
                            this.Text = String.Format("Отчёт [{0}] - {1}", printReport.ReportName,
                                printReport.Descriptor);
                            result = true;
                        }
                        else
                            MessageBox.Show(this,
                                String.Format("Отчёт \"{0}\" уже используется!", newname),
                                "Новый отчёт", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return result;
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            DoNewReport();
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            using (frmReportOpen form = new frmReportOpen(printReport.ReportName))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadReport(form.SelectedReport());
                }
            }

        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            printReport.SaveToBase();
            hasChanges = false;
            Cursor = Cursors.Default;
        }

        private void tsbPreview_Click(object sender, EventArgs e)
        {
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.ShowDialog();
        }

        private void tsbImport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Текущий отчёт будет заменён! Продолжить?",
                "Восстановление отчёта с диска", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                importFileDialog.InitialDirectory = Application.StartupPath + "\\reports";
                if (importFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    PrinterSettings ps = printDocument.PrinterSettings;
                    string[] lines = System.IO.File.ReadAllLines(importFileDialog.FileName);
                    printReport.ImportLines(ref ps, lines);
                    calcWorkRects();
                    printReport.SaveToBase();
                    hasChanges = false;
                    Cursor = Cursors.Default;
                }
            }
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\reports";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            exportFileDialog.InitialDirectory = path;
            exportFileDialog.FileName = printReport.ReportName.ToLower() + ".ini";
            if (exportFileDialog.ShowDialog() == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                List<string> lines = printReport.ExportLines();
                System.IO.File.WriteAllLines(exportFileDialog.FileName, lines.ToArray());
                Cursor = Cursors.Default;
            }
        }

        private void tsbOk_Click(object sender, EventArgs e)
        {
            hasChanges = true;
            printReport.UpdatePlotText(tstbText.Text);
            drawBox.Invalidate();
            tsbCancel.Enabled = tsbOk.Enabled = false;
            tbDrawBox.Focus();
        }

        private void tstbText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                hasChanges = true;
                printReport.UpdatePlotText(tstbText.Text);
                drawBox.Invalidate();
                tsbCancel.Enabled = tsbOk.Enabled = false;
                tbDrawBox.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                cancelPlotText();
                tsbCancel.Enabled = tsbOk.Enabled = false;
                tbDrawBox.Focus();
            }
        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            cancelPlotText();
            tsbCancel.Enabled = tsbOk.Enabled = false;
            tbDrawBox.Focus();
        }

        private void cancelPlotText()
        {
            foreach (Plot plt in printReport.SelList)
            {
                updateTextInput(plt);
                break;
            }
        }

        private void tstbText_Enter(object sender, EventArgs e)
        {
            tsbCancel.Enabled = tsbOk.Enabled = true;
        }

        private void frmReportEditor_Resize(object sender, EventArgs e)
        {
            tsGeneral.Location = new Point(0, 0);
            tsFont.Location = new Point(tsGeneral.Right, 0);
            tsEdit.Location = new Point(0, tsGeneral.Bottom);
            tsHourOffset.Location = new Point(tsEdit.Right, tsEdit.Top);
            vScrollBar.Value = 0;
            hScrollBar.Value = 0;
            CalculateScrollBars();
        }

        private void CalculateScrollBars()
        {
            if (drawBox.Height > pnlScroll.Height)
            {
                vScrollBar.Visible = true;
                vScrollBar.Scroll -= vScrollBar_Scroll;
                vScrollBar.Maximum = drawBox.Height - pnlScroll.Height + hScrollBar.Height + 10;
                vScrollBar.Value = -drawBox.Location.Y;
                vScrollBar.Scroll += vScrollBar_Scroll;
            }
            else
                vScrollBar.Visible = false;
            if (drawBox.Width > pnlScroll.Width)
            {
                hScrollBar.Visible = true;
                hScrollBar.Scroll -= hScrollBar_Scroll;
                hScrollBar.Maximum = drawBox.Width - pnlScroll.Width + vScrollBar.Width + 10;
                hScrollBar.Value = -drawBox.Location.X;
                hScrollBar.Scroll += hScrollBar_Scroll;
            }
            else
                hScrollBar.Visible = false;
        }

        private void tscbSnap_SelectedIndexChanged(object sender, EventArgs e)
        {
            hasChanges = true;
            foreach (Plot plt in printReport.SelList)
            {
                if (plt.Kind > 2)
                {
                    plt.Props["Snap"] =
                        int.Parse(tscbSnap.Items[tscbSnap.SelectedIndex].ToString());
                }
            }
            printReport.Update();
            drawBox.Invalidate();
            tbDrawBox.Focus();
        }

        private void tscbOffset_SelectedIndexChanged(object sender, EventArgs e)
        {
            hasChanges = true;
            foreach (Plot plt in printReport.SelList)
            {
                if (plt.Kind > 2)
                {
                    plt.Props["Offset"] =
                        int.Parse(tscbOffset.Items[tscbOffset.SelectedIndex].ToString());
                }
            }
            printReport.Update();
            drawBox.Invalidate();
            tbDrawBox.Focus();
        }

        private void tscbColumnLines_SelectedIndexChanged(object sender, EventArgs e)
        {
            hasChanges = true;
            foreach (Plot plt in printReport.SelList)
            {
                if (plt.Kind > 2)
                {
                    plt.Props["ColumnLines"] =
                        int.Parse(tscbColumnLines.Items[tscbColumnLines.SelectedIndex].ToString());
                }
            }
            printReport.Update();
            drawBox.Invalidate();
            tbDrawBox.Focus();
        }

        private void tscbLineBetween_SelectedIndexChanged(object sender, EventArgs e)
        {
            hasChanges = true;
            foreach (Plot plt in printReport.SelList)
            {
                if (plt.Kind > 2)
                {
                    plt.Props["LineBetween"] =
                        int.Parse(tscbLineBetween.Items[tscbLineBetween.SelectedIndex].ToString());
                }
            }
            printReport.Update();
            drawBox.Invalidate();
            tbDrawBox.Focus();
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

        private void frmReportEditor_KeyDown(object sender, KeyEventArgs e)
        {
            controlPressed = e.Control;
            shiftPressed = e.Shift;
            altPressed = e.Alt;
        }

        private void frmReportEditor_KeyUp(object sender, KeyEventArgs e)
        {
            controlPressed = e.Control;
            shiftPressed = e.Shift;
            altPressed = e.Alt;
        }

        private void tscbAgregate_SelectedIndexChanged(object sender, EventArgs e)
        {
            hasChanges = true;
            foreach (Plot plt in printReport.SelList)
            {
                if (plt.Kind > 2)
                {
                    plt.Props["Agregate"] = tscbAgregate.SelectedIndex;
                }
            }
            printReport.Update();
            drawBox.Invalidate();
            tbDrawBox.Focus();
        }

        private void tsmiDeleteReport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Этот отчёт будет удалён безвозвратно! Удалить?",
                "Удаление отчёта с сервера", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Data.EmptyReport(ReportName);
                newReport();
            }
        }

    }

}
