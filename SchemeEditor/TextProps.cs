using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace SchemeEditor
{
    public partial class TextProps : Form
    {
        int LastColorIndex = 0;
        Text text = new Text();
        public TextProps(Draws drw)
        {
            InitializeComponent();
            FontFamily[] ffam = FontFamily.Families;
            cbFont.Items.Clear();
            foreach (FontFamily ff in ffam)
            {
                cbFont.Items.Add(ff.Name);
            }
            // -------------------------------------------------------------------
            cbFont.SelectedIndex = cbFont.Items.IndexOf(cbFont.Text);
            cbColor.Items.Clear();
            // получение всех имён доступных цветов
            cbColor.Items.AddRange(DrawUtils.GetAllColorNames());
            // добавление пункта выбора цвета
            cbColor.Items.Add("Выбор цвета..."); 
            cbColor.Text = DrawUtils.GetColorNameFromIndex(LastColorIndex);
            // -------------------------------------------------------------------
            text.Assign(drw.Text);
            // -------------------------------
            int Index = DrawUtils.ColorToIndex(text.Color);
            if (Index < 0)
            {
                DrawUtils.AddCustomColor(text.Color);
                cbColor.Items.Insert(cbColor.Items.Count - 1, "Мой цвет");
                Index = cbColor.Items.Count - 2;
            }
            if (Index >= 0) cbColor.SelectedIndex = Index;
            // -------------------------------
            tbTrasparent.Value = 255 - text.Alpha;
            lbTrasparent.Text = String.Format("{0} %", 
                (int)(tbTrasparent.Value / 255.0 * 100.0));
            // -------------------------------
            cbFont.Text = text.FontName;
            // -------------------------------
            cbSize.Text = text.FontSize.ToString();
            // -------------------------------
            if (text.Bold && text.Italic) cbStyle.SelectedIndex = 3;
            else if (text.Bold) cbStyle.SelectedIndex = 2;
            else if (text.Italic) cbStyle.SelectedIndex = 1;
            else cbStyle.SelectedIndex = 0;
            // -------------------------------
            cbUnderline.Checked = text.Underline;
            // -------------------------------
            cbSrikeout.Checked = text.Strikeout;
            // -------------------------------
            cbHorizontal.SelectedIndex = (int)(text.Alignment);
            cbVertical.SelectedIndex = (int)(text.LineAlignment);
            cbVerticalText.Checked = text.Vertical;
        }

        public Text DrawText { get { return (text); } set { text = value; } }

        private void cbColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Graphics g = e.Graphics;
            Color brushColor = DrawUtils.ColorFromIndex(e.Index);
            // Draw the background of the item.
            e.DrawBackground();
            Rectangle largerect = new Rectangle(e.Bounds.X, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1);
            Rectangle colorrect = new Rectangle(4, e.Bounds.Top + 2, e.Bounds.Height - 2, e.Bounds.Height - 5);
            // отрисовка рамки цвета пунктов основеых цветов
            if (DrawUtils.IsNamedColorIndex(e.Index)) 
            {
                using (SolidBrush brush = new SolidBrush(brushColor))
                    g.FillRectangle(brush, colorrect);
                g.DrawRectangle(Pens.Black, colorrect);
            }
            RectangleF textRect = new RectangleF(e.Bounds.X + colorrect.Width + 5, 
                e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height);
            using (SolidBrush textColor = new SolidBrush(e.ForeColor))
            {
                if (DrawUtils.IsNamedColorIndex(e.Index))
                {// отрисовка пунктов основных цветов
                    g.DrawString(DrawUtils.GetColorNameFromIndex(e.Index), cb.Font, 
                        textColor, textRect);
                }
                else // отрисовка пунктов дополнительных цветов
                    if (DrawUtils.IsCustomColorIndex(e.Index))
                    {
                        using (SolidBrush brush = new SolidBrush(brushColor))
                            g.FillRectangle(brush, largerect);
                        using (Pen pen = new Pen(cb.BackColor))
                            g.DrawRectangle(pen, largerect);
                    }
                    else // отрисовка последнего пункта: Выбор цвета...
                        g.DrawString(cb.Items[e.Index].ToString(), cb.Font, 
                            textColor, largerect);
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
                    dlgSelectColor.Color = DrawUtils.ColorFromIndex(LastColorIndex);
                    selIndex = LastColorIndex;
                    if (dlgSelectColor.ShowDialog() == DialogResult.OK)
                    {
                        Color selColor = dlgSelectColor.Color;
                        text.Color = selColor;
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

        private void cbColor_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            text.Color = DrawUtils.ColorFromIndex(cb.SelectedIndex);
            pbPreview.Refresh();

        }

        private void tbTrasparent_Scroll(object sender, System.EventArgs e)
        {
            lbTrasparent.Text = String.Format(CultureInfo.InvariantCulture, "{0}", 
                (int)(tbTrasparent.Value / 255.0 * 100.0)) + " %";
            text.Alpha = 255 - tbTrasparent.Value;
            pbPreview.Refresh();
        }

        private void pbPreview_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            PictureBox pb = (PictureBox)sender;
            RectangleF rect = new RectangleF(0, 0, pb.Width, pb.Height);
            try
            {
                using (Font font = text.Font())
                {
                    using (Brush brush = text.FontBrush())
                    {
                        StringFormatFlags options = text.Vertical ?
                            StringFormatFlags.DirectionVertical : (StringFormatFlags)0;
                        using (StringFormat format = new StringFormat(options))
                        {
                            format.Alignment = text.Alignment;
                            format.LineAlignment = text.LineAlignment;
                            g.DrawString("Съешь ещё этих хрустящих французских булочек...",
                                font, brush, rect, format);
                            
                        }
                    }
                }
            }
            catch
            {
                g.DrawString("Начертание не поддерживается.", SystemFonts.DialogFont,
                    Brushes.Black, rect);
            }
        }

        private void cbFont_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            text.FontName = cb.Items[cb.SelectedIndex].ToString();
            pbPreview.Refresh();
        }

        private void cbStyle_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            switch (cb.SelectedIndex)
            {
                case 0:
                    text.Bold = false;
                    text.Italic = false;
                    break;
                case 1:
                    text.Bold = false;
                    text.Italic = true;
                    break;
                case 2:
                    text.Bold = true;
                    text.Italic = false;
                    break;
                case 3:
                    text.Bold = true;
                    text.Italic = true;
                    break;
            }
            pbPreview.Refresh();
        }

        private void cbUnderline_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            text.Underline = cb.Checked;
            pbPreview.Refresh();
        }

        private void cbSrikeout_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            text.Strikeout = cb.Checked;
            pbPreview.Refresh();
        }

        private void cbSize_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            try
            {
                text.FontSize = Single.Parse(cb.Items[cb.SelectedIndex].ToString());
                pbPreview.Refresh();
            }
            catch
            {
            }
        }

        private void sbSize_TextChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            try
            {
                text.FontSize = Single.Parse(cb.Text);
                pbPreview.Refresh();
            }
            catch
            {
            }
        }

        private void cbHorizontal_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            text.Alignment = (StringAlignment)(cb.SelectedIndex);
            pbPreview.Refresh();
        }

        private void cbVertical_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            text.LineAlignment = (StringAlignment)(cb.SelectedIndex);
            pbPreview.Refresh();
        }

        private void cbVerticalText_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            text.Vertical = cb.Checked;
            pbPreview.Refresh();
        }
    }
}
