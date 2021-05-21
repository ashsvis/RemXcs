using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Globalization;

namespace SchemeEditor
{
    public partial class StrokeProps : Form
    {
        int LastColorIndex = 0;
        Stroke stroke = new Stroke();
        public StrokeProps(Draws drw)
        {
            InitializeComponent();
            // -------------------------------------------------------------------
            cbPattern.Items.Clear();
            cbPattern.Items.AddRange(DrawUtils.GetPenPatternNames()); // получение всех имён доступных типов линий
            cbPattern.SelectedIndex = 1;
            // -------------------------------------------------------------------
            cbWidth.Items.Clear();
            for (int i = 1; i < 61; i++) cbWidth.Items.Add(i.ToString());
            // -------------------------------------------------------------------
            cbColor.Items.Clear();
            cbColor.Items.AddRange(DrawUtils.GetAllColorNames()); // получение всех имён доступных цветов
            cbColor.Items.Add("Выбор цвета..."); // добавление пункта выбора цвета
            cbColor.Text = DrawUtils.GetColorNameFromIndex(LastColorIndex);
            // -------------------------------------------------------------------
            stroke.Assign(drw.Stroke);
            // -------------------------------
            int Index = DrawUtils.ColorToIndex(stroke.Color);
            if (Index < 0)
            {
                DrawUtils.AddCustomColor(stroke.Color);
                cbColor.Items.Insert(cbColor.Items.Count - 1, "Мой цвет");
                Index = cbColor.Items.Count - 2;
            }
            if (Index >= 0) cbColor.SelectedIndex = Index;
            // -------------------------------
            tbTrasparent.Value = 255 - stroke.Alpha;
            lbTrasparent.Text = String.Format("{0} %", (int)(tbTrasparent.Value / 255.0 * 100.0));
            // -------------------------------
            cbWidth.SelectedIndex = (int)stroke.Width-1;
            // -------------------------------
            if (stroke.DashStyle == DashStyle.Custom)
                cbPattern.SelectedIndex = 0;
            else
                cbPattern.SelectedIndex = (int)stroke.DashStyle + 1;
            // -------------------------------------------------------------------
            cbLineJoin.Items.Clear();
            // получение всех имён доступных типов соединений линий
            cbLineJoin.Items.AddRange(DrawUtils.GetLineJoinNames()); 
            cbLineJoin.SelectedIndex = (int)stroke.LineJoin;
            // -------------------------------------------------------------------
            cbStartCap.Items.Clear();
            // получение всех имён доступных типов окончаний линий
            cbStartCap.Items.AddRange(DrawUtils.GetLineCapNames());
            cbStartCap.SelectedIndex = (int)stroke.StartCap;
            // -------------------------------------------------------------------
            cbEndCap.Items.Clear();
            // получение всех имён доступных типов окончаний линий
            cbEndCap.Items.AddRange(DrawUtils.GetLineCapNames());
            cbEndCap.SelectedIndex = (int)stroke.EndCap;
        }
        public Stroke Stroke { get { return (stroke); } set { stroke = value; } }

        private void cbColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Graphics g = e.Graphics;
            Color brushColor = DrawUtils.ColorFromIndex(e.Index);
            // Draw the background of the item.
            e.DrawBackground();
            Rectangle largerect = new Rectangle(e.Bounds.X, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1);
            Rectangle colorrect = new Rectangle(4, e.Bounds.Top + 2, e.Bounds.Height - 2, e.Bounds.Height - 5);
            if (DrawUtils.IsNamedColorIndex(e.Index)) // отрисовка рамки цвета пунктов основеых цветов
            {
                using (SolidBrush brush = new SolidBrush(brushColor))
                    g.FillRectangle(brush, colorrect);
                g.DrawRectangle(Pens.Black, colorrect);
            }
            RectangleF textRect = new RectangleF(e.Bounds.X + colorrect.Width + 5, e.Bounds.Y + 1,
                                                 e.Bounds.Width, e.Bounds.Height);
            using (SolidBrush textColor = new SolidBrush(e.ForeColor))
            {
                if (DrawUtils.IsNamedColorIndex(e.Index))
                {// отрисовка пунктов основных цветов
                    g.DrawString(DrawUtils.GetColorNameFromIndex(e.Index), cb.Font, textColor, textRect);
                }
                else
                    if (DrawUtils.IsCustomColorIndex(e.Index)) // отрисовка пунктов дополнительных цветов
                    {
                        using (SolidBrush brush = new SolidBrush(brushColor))
                            g.FillRectangle(brush, largerect);
                        using (Pen pen = new Pen(cb.BackColor))
                            g.DrawRectangle(pen, largerect);
                    }
                    else // отрисовка последнего пункта: Выбор цвета...
                        g.DrawString(cb.Items[e.Index].ToString(), cb.Font, textColor, largerect);
            }
            // Draw the focus rectangle if the mouse hovers over an item.
            e.DrawFocusRectangle();
        }

        private void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbox = (ComboBox)sender;
            if (cbox.SelectedIndex == cbox.Items.Count - 1)
            {
                try
                {
                    int selIndex;
                    dlgSelectColor.Color = DrawUtils.ColorFromIndex(LastColorIndex);
                    selIndex = LastColorIndex;
                    if (dlgSelectColor.ShowDialog() == DialogResult.OK)
                    {
                        Color selColor = dlgSelectColor.Color;
                        stroke.Color = selColor;
                        if (!DrawUtils.FindColor(selColor))
                        {
                            DrawUtils.AddCustomColor(selColor);
                            dlgSelectColor.CustomColors = DrawUtils.GetCustomColors();
                            cbColor.Items.Insert(cbColor.Items.Count - 1, "Мой цвет");
                            cbColor.SelectedIndex = cbColor.Items.Count - 2;
                        }
                        else
                            cbox.SelectedIndex = DrawUtils.ColorToIndex(selColor);
                    }
                    else
                        cbox.SelectedIndex = selIndex;
                }
                catch
                { }
            }
            else
            {
                LastColorIndex = cbox.SelectedIndex;
                cbox.Refresh();
                pbPreview.Refresh();
            }
        }

        private void cbColor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            stroke.Color = DrawUtils.ColorFromIndex(cb.SelectedIndex);
            pbPreview.Refresh();
        }

        private void cbPattern_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Graphics g = e.Graphics;
            // Draw the background of the item.
            e.DrawBackground();
            Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1);
            try
            {
                rect.Inflate(-4, 0);
                if (e.Index == 0)
                    ShowItemText(e, cb, g, rect);
                else
                {
                    using (Pen p = new Pen(e.ForeColor))
                    {
                        p.Width = 2;
                        p.DashStyle = (DashStyle)(e.Index - 1);
                        g.DrawLine(p, new Point(rect.Left, rect.Top + rect.Height / 2),
                                      new Point(rect.Right, rect.Top + rect.Height / 2));
                    }
                }
            }
            catch { }
            // Draw the focus rectangle if the mouse hovers over an item.
            e.DrawFocusRectangle();
        }
        private static void ShowItemText(DrawItemEventArgs e, ComboBox cb, Graphics g, Rectangle largerect)
        {
            using (SolidBrush textColor = new SolidBrush(e.ForeColor))
            {
                string showing = cb.Items[e.Index].ToString();
                g.DrawString(showing, cb.Font, textColor, largerect);
            }
        }

        private void cbPattern_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedIndex == 0) stroke.DashStyle = DashStyle.Custom;
            else stroke.DashStyle = (DashStyle)(cb.SelectedIndex - 1);
            pbPreview.Refresh();
        }

        private void cbWidth_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Graphics g = e.Graphics;
            // Draw the background of the item.
            e.DrawBackground();
            Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);
            try
            {
                rect.Inflate(-4, 0);
                using (Pen p = new Pen(e.ForeColor))
                {
                    p.Width = e.Index + 1;
                    g.DrawLine(p, new Point(rect.Left, rect.Top + rect.Height / 2),
                                  new Point(rect.Right, rect.Top + rect.Height / 2));
                    if (e.Index >= 9)
                    {
                        using (SolidBrush textColor = new SolidBrush(e.BackColor))
                        {
                            rect.Offset(0, 2);
                            string showing = String.Format("{0} точек", cb.Items[e.Index].ToString());
                            g.DrawString(showing, cb.Font, textColor, rect);
                        }
                    }
                }
            }
            catch { }
            // Draw the focus rectangle if the mouse hovers over an item.
            e.DrawFocusRectangle();
        }

        private void cbWidth_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            stroke.Width = cb.SelectedIndex+1;
            pbPreview.Refresh();
        }

        private void tbTrasparent_Scroll(object sender, EventArgs e)
        {
            lbTrasparent.Text = String.Format(CultureInfo.InvariantCulture, "{0}", (int)(tbTrasparent.Value / 255.0 * 100.0)) + " %";
            stroke.Alpha = 255 - tbTrasparent.Value;
            pbPreview.Refresh();
        }

        private void pbPreview_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            PictureBox pb = (PictureBox)sender;
            RectangleF rect = new RectangleF(0, 0, pb.Width, pb.Height);
            PointF[] ps = new PointF[3];
            ps[0] = new PointF(rect.Left + rect.Width / 2, rect.Top + rect.Height / 8);
            ps[1] = new PointF(rect.Left + rect.Width /4, rect.Top + 7 * rect.Height / 8);
            ps[2] = new PointF(rect.Right - rect.Width / 8, rect.Bottom - rect.Height / 8);
            g.DrawLines(stroke.Pen(), ps);
        }

        private void cbWidth_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (e.Index < cb.Height-8)
                e.ItemHeight = cb.Height;
            else
                e.ItemHeight = e.Index + 8;
        }

        private void cbStartCap_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Graphics g = e.Graphics;
            // Draw the background of the item.
            e.DrawBackground();
            Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1);
            try
            {
                rect.Inflate(-4, 0);
                using (Pen p = new Pen(e.ForeColor))
                {
                    p.Width = 9;
                    if (cb == cbStartCap)
                    {
                        p.StartCap = (LineCap)e.Index;
                        g.DrawLine(p, new Point(rect.Left + rect.Width / 8, rect.Top + rect.Height / 2),
                                      new Point(rect.Right, rect.Top + rect.Height / 2));
                    }
                    else
                    {
                        p.EndCap = (LineCap)e.Index;
                        g.DrawLine(p, new Point(rect.Left, rect.Top + rect.Height / 2),
                                      new Point(rect.Right - rect.Width / 8, rect.Top + rect.Height / 2));
                    }
                }
            }
            catch { }
            // Draw the focus rectangle if the mouse hovers over an item.
            e.DrawFocusRectangle();
        }

        private void cbLineJoin_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            stroke.LineJoin = (LineJoin)(cb.SelectedIndex);
            pbPreview.Refresh();
        }

        private void cbStartCap_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            stroke.StartCap = (LineCap)(cb.SelectedIndex);
            pbPreview.Refresh();
        }

        private void cbEndCap_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            stroke.EndCap = (LineCap)(cb.SelectedIndex);
            pbPreview.Refresh();
        }
    }
}
