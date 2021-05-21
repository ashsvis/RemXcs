using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Draws.Plugins;

namespace Draws.Common
{
    public partial class frmDinDraw : Form
    {
        UpdateDraw updater;
        SelectData selector;
        Draw drw;
        int LastColorIndex = 0;

        public frmDinDraw(Draw drw, UpdateDraw updater, SelectData selector)
        {
            InitializeComponent();
            this.drw = drw;
            this.updater = updater;
            this.selector = selector;
            #region Свойства положения и размера
            udLeft.ValueChanged -= udLeft_ValueChanged;
            udLeft.Value = (decimal)(float)drw.Props["Left"];
            udLeft.ValueChanged += udLeft_ValueChanged;
            udTop.ValueChanged -= udTop_ValueChanged;
            udTop.Value = (decimal)(float)drw.Props["Top"];
            udTop.ValueChanged += udTop_ValueChanged;
            udWidth.ValueChanged -= udWidth_ValueChanged;
            udWidth.Value = (decimal)(float)drw.Props["Width"];
            udWidth.ValueChanged += udWidth_ValueChanged;
            udHeight.ValueChanged -= udHeight_ValueChanged;
            udHeight.Value = (decimal)(float)drw.Props["Height"];
            udHeight.ValueChanged += udHeight_ValueChanged;
            #endregion
            #region Свойства фона
            DrawPlugin.colorComboFillList(cbBackColor, LastColorIndex);
            DrawPlugin.colorComboSelectInList(cbBackColor, (Color)drw.Props["BackColor"]);
            cbBackPattern.Items.AddRange(DrawUtils.GetAllPatternNames()); // получение всех имён доступных паттернов
            cbBackPattern.SelectedIndex = (int)drw.Props["PatternMode"];
            cbBackPatternColor.Enabled = (cbBackPattern.SelectedIndex > 1);
            DrawPlugin.colorComboFillList(cbBackPatternColor, LastColorIndex);
            DrawPlugin.colorComboSelectInList(cbBackPatternColor, (Color)drw.Props["PatternColor"]);
            #endregion
            tbTransparent.Value = 255 - (int)drw.Props["BackAlpha"];
            lbTransparent.Text = ((int)(tbTransparent.Value / 255.0 * 100.0)) + " %";
            DrawPlugin.colorComboFillList(cbStrokeColor, LastColorIndex);
            DrawPlugin.colorComboSelectInList(cbStrokeColor, (Color)drw.Props["StrokeColor"]);


        }

        private void cbBackColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawPlugin.colorComboDrawItem(sender, e);
        }

        private void cbBackColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawPlugin.colorComboSelectedIndexChanged(sender, ref LastColorIndex,
                cbBackColor_SelectionChangeCommitted);
        }

        private void cbBackColor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Color color = DrawPlugin.colorComboSelectionChangeCommitted(sender);
            ComboBox cbox = (ComboBox)sender;
            if (cbox.Equals(cbBackColor)) drw.Props["BackColor"] = color;
            else if (cbox.Equals(cbBackPatternColor)) drw.Props["PatternColor"] = color;
            else if (cbox.Equals(cbBackPatternColor)) drw.Props["StrokeColor"] = color;
            else return;
            cbBackPattern.Invalidate();
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void udLeft_ValueChanged(object sender, EventArgs e)
        {
            drw.Props["Left"] = (float)udLeft.Value;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void udTop_ValueChanged(object sender, EventArgs e)
        {
            drw.Props["Top"] = (float)udTop.Value;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void udWidth_ValueChanged(object sender, EventArgs e)
        {
            drw.Props["Width"] = (float)udWidth.Value;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void udHeight_ValueChanged(object sender, EventArgs e)
        {
            drw.Props["Height"] = (float)udHeight.Value;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void frmDinDraw_Load(object sender, EventArgs e)
        {
            if (updater != null) updater(drw, UpdateKind.UpdateBefore);
        }

        private void tbTransparent_Scroll(object sender, EventArgs e)
        {
            lbTransparent.Text = (int)(tbTransparent.Value / 255.0 * 100.0) + " %";
            drw.Props["BackAlpha"] = 255 - tbTransparent.Value;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbPattern_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Graphics g = e.Graphics;
            // Draw the background of the item.
            e.DrawBackground();
            Rectangle largerect = new Rectangle(e.Bounds.X, e.Bounds.Top,
                e.Bounds.Width - 1, e.Bounds.Height - 1);
            try
            {
                switch (DrawUtils.FillModeFromIndex(e.Index))
                {
                    case Draws.Plugins.FillMode.None: ShowItemText(e, cb, g, largerect); break;
                    case Draws.Plugins.FillMode.Solid: ShowItemText(e, cb, g, largerect); break;
                    case Draws.Plugins.FillMode.LinearGradient:
                        using (LinearGradientBrush brush =
                               new LinearGradientBrush(largerect,
                                   (Color)drw.Props["PatternColor"], (Color)drw.Props["BackColor"],
                                        DrawUtils.LinearGradientModeFromIndex(e.Index)))
                        {
                            g.FillRectangle(brush, largerect);
                        }
                        break;
                    case Draws.Plugins.FillMode.Hatch:
                        using (HatchBrush brush =
                               new HatchBrush(DrawUtils.HatchStyleFromIndex(e.Index),
                                        (Color)drw.Props["PatternColor"], (Color)drw.Props["BackColor"]))
                        {
                            g.FillRectangle(brush, largerect);
                        }
                        break;
                }
            }
            catch //(Exception ex)
            {
                g.FillRectangle(Brushes.White, largerect);
                g.DrawRectangle(Pens.Red, largerect.X, largerect.Y, largerect.Width, largerect.Height);
                //Console.WriteLine("{0} Exception caught.", ex);
                //throw;
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

        private void cbPattern_SelectionChangeCommitted(object sender, EventArgs e)
        {
            drw.Props["PatternMode"] = cbBackPattern.SelectedIndex;
            cbBackPatternColor.Enabled = (cbBackPattern.SelectedIndex > 1);
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }
    }
}
