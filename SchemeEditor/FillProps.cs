using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

namespace SchemeEditor
{
    public partial class FillProps : Form
    {
        int LastColorIndex = 0;
        int LastPatternColorIndex = 0;
        Fill fill = new Fill();

        public FillProps(Draws drw)
        {
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            InitializeComponent();
            // -------------------------------------------------------------------
            cbColor.Items.Clear();
            cbColor.Items.AddRange(DrawUtils.GetAllColorNames()); // получение всех имён доступных цветов
            cbColor.Items.Add("Выбор цвета..."); // добавление пункта выбора цвета
            cbColor.Text = DrawUtils.GetColorNameFromIndex(LastColorIndex);
            // -------------------------------------------------------------------
            cbColorPattern.Items.Clear();
            cbColorPattern.Items.AddRange(DrawUtils.GetAllColorNames()); // получение всех имён доступных цветов
            cbColorPattern.Items.Add("Выбор цвета..."); // добавление пункта выбора цвета
            cbColorPattern.Text = DrawUtils.GetColorNameFromIndex(LastPatternColorIndex);
            // -------------------------------------------------------------------
            cbPattern.Items.Clear();
            cbPattern.Items.AddRange(DrawUtils.GetAllPatternNames()); // получение всех имён доступных паттернов
            cbPattern.SelectedIndex = 1;
            // -------------------------------------------------------------------
            if (drw != null) fill.Assign(drw.Fill);
            // -------------------------------
            int Index = DrawUtils.ColorToIndex(fill.Color);
            if (Index < 0)
            {
                DrawUtils.AddCustomColor(fill.Color);
                cbColor.Items.Insert(cbColor.Items.Count - 1, "Мой цвет");
                Index = cbColor.Items.Count - 2;
            }
            if (Index >= 0) cbColor.SelectedIndex = Index;
            // -------------------------------
            Index = DrawUtils.ColorToIndex(fill.PatternColor);
            if (Index < 0)
            {
                DrawUtils.AddCustomColor(fill.PatternColor);
                cbColorPattern.Items.Insert(cbColorPattern.Items.Count - 1, "Мой цвет");
                Index = cbColorPattern.Items.Count - 2;
            }
            if (Index >= 0) cbColorPattern.SelectedIndex = Index;
            // -------------------------------
            tbTrasparent.Value = 255 - fill.Alpha;
            lbTrasparent.Text = String.Format(CultureInfo.InvariantCulture, "{0}", 
                                                (int)(tbTrasparent.Value / 255.0 * 100.0))+
                                                " %";
            // -------------------------------
            cbPattern.SelectedIndex = fill.PatternIndex;
            cbColorPattern.Enabled = (cbPattern.SelectedIndex > 1);
        }

        public Fill Fill { get { return(fill); } set { fill = value; } }

        private void cbColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Graphics g = e.Graphics;
            Color brushColor = DrawUtils.ColorFromIndex(e.Index);
            // Draw the background of the item.
            e.DrawBackground();
            Rectangle largerect = new Rectangle(e.Bounds.X, e.Bounds.Top, e.Bounds.Width-1, e.Bounds.Height-1);
            Rectangle colorrect = new Rectangle(4, e.Bounds.Top + 2, e.Bounds.Height - 2, e.Bounds.Height - 5);
            if (DrawUtils.IsNamedColorIndex(e.Index)) // отрисовка рамки цвета пунктов основеых цветов
            {
                using (SolidBrush brush = new SolidBrush(brushColor))
                {
                    g.FillRectangle(brush, colorrect);
                }
                g.DrawRectangle(Pens.Black, colorrect);
            }
            RectangleF textRect = new RectangleF(e.Bounds.X + colorrect.Width + 5, e.Bounds.Y+1, 
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
                        {
                            g.FillRectangle(brush, largerect);
                        }
                        using (Pen pen = new Pen(cb.BackColor))
                        {
                            g.DrawRectangle(pen, largerect);
                        }
                    }
                    else // отрисовка последнего пункта: Выбор цвета...
                        g.DrawString(cb.Items[e.Index].ToString(), cb.Font, textColor, largerect);
            }
            // Draw the focus rectangle if the mouse hovers over an item.
            e.DrawFocusRectangle();
       
        }

        private void cbColor_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ComboBox cbox = (ComboBox)sender;
            if (cbox.SelectedIndex == cbox.Items.Count - 1)
            {
                try
                {
                    int selIndex;
                    if (cbox == cbColor)
                    {
                        dlgSelectColor.Color = DrawUtils.ColorFromIndex(LastColorIndex);
                        selIndex = LastColorIndex;
                    }
                    else
                    {
                        dlgSelectColor.Color = DrawUtils.ColorFromIndex(LastPatternColorIndex);
                        selIndex = LastPatternColorIndex;
                    }
                    if (dlgSelectColor.ShowDialog() == DialogResult.OK)
                    {
                        Color selColor = dlgSelectColor.Color;
                        if (cbox == cbColor) fill.Color = selColor; else fill.PatternColor = selColor;
                        if (!DrawUtils.FindColor(selColor))
                        {
                            DrawUtils.AddCustomColor(selColor);
                            dlgSelectColor.CustomColors = DrawUtils.GetCustomColors();
                            cbColor.Items.Insert(cbColor.Items.Count - 1, "Мой цвет");
                            cbColorPattern.Items.Insert(cbColorPattern.Items.Count - 1, "Мой цвет");
                            if (cbox == cbColor)
                                cbColor.SelectedIndex = cbColor.Items.Count - 2;
                            else
                                cbColorPattern.SelectedIndex = cbColorPattern.Items.Count - 2;
                        }
                        else
                            cbox.SelectedIndex = DrawUtils.ColorToIndex(selColor);
                    }
                    else
                        cbox.SelectedIndex = selIndex;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("{0} Exception caught.", ex);
                    throw;
                }
            }
            else
            {
                if (cbox == cbColor)
                    LastColorIndex = cbox.SelectedIndex;
                else
                    LastPatternColorIndex = cbox.SelectedIndex;
                cbox.Refresh();
                pbPreview.Refresh();
            }
        }

        private void cbColor_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb == cbColor)
                fill.Color = DrawUtils.ColorFromIndex(cb.SelectedIndex);
            else
                fill.PatternColor = DrawUtils.ColorFromIndex(cb.SelectedIndex);
            cbPattern.Refresh();
            pbPreview.Refresh();
        }

        private void pbPreview_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            PictureBox pb = (PictureBox)sender;
            RectangleF rect = new RectangleF(0, 0, pb.Width, pb.Height);
            g.FillRectangle(fill.Brush(rect), rect);
        }

        private void cbPattern_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Graphics g = e.Graphics;
            // Draw the background of the item.
            e.DrawBackground();
            Rectangle largerect = new Rectangle(e.Bounds.X, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1);
            try
            {
                switch (DrawUtils.FillModeFromIndex(e.Index))
                {
                    case FillMode.None: ShowItemText(e, cb, g, largerect); break;
                    case FillMode.Solid: ShowItemText(e, cb, g, largerect); break;
                    case FillMode.LinearGradient:
                        using (LinearGradientBrush brush = 
                               new LinearGradientBrush(largerect, fill.PatternColor, fill.Color,
                                        DrawUtils.LinearGradientModeFromIndex(e.Index)))
                        {
                            g.FillRectangle(brush, largerect);
                        }
                        break;
                    case FillMode.Hatch:
                        using (HatchBrush brush = 
                               new HatchBrush(DrawUtils.HatchStyleFromIndex(e.Index),
                                        fill.PatternColor, fill.Color))
                        {
                            g.FillRectangle(brush, largerect);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                throw;
            }
            // Draw the focus rectangle if the mouse hovers over an item.
            e.DrawFocusRectangle();
        }

        private static void ShowItemText(DrawItemEventArgs e, ComboBox cb, Graphics g, Rectangle largerect)
        {
            using (SolidBrush textColor = new SolidBrush(e.ForeColor))
            {
                g.DrawString(cb.Items[e.Index].ToString(),
                             cb.Font, textColor, largerect);
            }
        }

        private void tbTrasparent_Scroll(object sender, EventArgs e)
        {
            lbTrasparent.Text = String.Format("{0} %", (int)(tbTrasparent.Value/255.0*100.0));
            fill.Alpha = 255 - tbTrasparent.Value;
            cbPattern.Refresh();
            pbPreview.Refresh();
        }

        private void cbPattern_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fill.PatternIndex = cbPattern.SelectedIndex;
            pbPreview.Refresh();
            cbColorPattern.Enabled = (cbPattern.SelectedIndex > 1);
        }

    }
}
