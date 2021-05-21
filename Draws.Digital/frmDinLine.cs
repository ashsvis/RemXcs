using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Draws.Plugins;

namespace Draws.Digital
{
    public partial class frmDinLine : Form
    {
        UpdateDraw updater;
        SelectData selector;
        Draw drw;
        int LastColorIndex = 0;
        int LastKindIndex = 0;
        int LastStyleIndex = 0;

        public frmDinLine(Draw drw, UpdateDraw updater, SelectData selector)
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
            tbPtName.Text = (string)drw.Props["PtName"];
            updateSelector();
            kindComboFillList(cbLineKind, LastKindIndex);
            kindComboSelectInList(cbLineKind, (int)drw.Props["LineKind"]);
            #region Свойства цвета линии
            DrawPlugin.colorComboFillList(cbColor, LastColorIndex);
            DrawPlugin.colorComboSelectInList(cbColor, (Color)drw.Props["Color"]);
            #endregion
            #region Свойства стиля линии
            styleComboFillList(cbLineStyle, LastStyleIndex);
            styleComboSelectInList(cbLineStyle, (int)drw.Props["LineStyle"]);
            #endregion
            udLineWidth.Value = (decimal)(float)drw.Props["LineWidth"];
            #region Свойства цвета линии 0
            DrawPlugin.colorComboFillList(cbColor0, LastColorIndex);
            DrawPlugin.colorComboSelectInList(cbColor0, (Color)drw.Props["Color0"]);
            #endregion
            #region Свойства стиля линии 0
            styleComboFillList(cbLineStyle0, LastStyleIndex);
            styleComboSelectInList(cbLineStyle0, (int)drw.Props["LineStyle0"]);
            #endregion
            udLineWidth0.Value = (decimal)(float)drw.Props["LineWidth0"];
            #region Свойства цвета линии 1
            DrawPlugin.colorComboFillList(cbColor1, LastColorIndex);
            DrawPlugin.colorComboSelectInList(cbColor1, (Color)drw.Props["Color1"]);
            #endregion
            #region Свойства стиля линии 1
            styleComboFillList(cbLineStyle1, LastStyleIndex);
            styleComboSelectInList(cbLineStyle1, (int)drw.Props["LineStyle1"]);
            #endregion
            udLineWidth1.Value = (decimal)(float)drw.Props["LineWidth1"];
        }

        private void kindComboFillList(object sender, int LastKindIndex)
        {
            ComboBox cbox = (ComboBox)sender;
            cbox.Items.Clear();
            // получение всех имён доступных стилей
            for (int i = 0; i < 10; i++) cbox.Items.Add(i.ToString());
            cbox.Text =LastKindIndex.ToString();
        }

        private void kindComboSelectInList(object sender, int kind)
        {
            ComboBox cbox = (ComboBox)sender;
            int Index = kind;
            if (Index >= 0) cbox.SelectedIndex = Index;
        }

        private void cbLineKind_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int linekind = e.Index;
            // Draw the background of the item.
            e.DrawBackground();
            Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1);
            rect.Inflate(-3, -3);
            using (Pen pen = new Pen(SystemColors.ControlText, 3f))
            {
                switch (linekind)
                {
                    case 0: // горизонтально
                        g.DrawLine(pen, new PointF(rect.X, rect.Y + rect.Height / 2),
                            new PointF(rect.X + rect.Width, rect.Y + rect.Height / 2));
                        break;
                    case 1: // вертикально
                        g.DrawLine(pen, new PointF(rect.X + rect.Width / 2, rect.Y),
                            new PointF(rect.X + rect.Width / 2, rect.Y + rect.Height));
                        break;
                    case 2: // диагональ снизу-вверх
                        g.DrawLine(pen, new PointF(rect.X, rect.Y + rect.Height),
                            new PointF(rect.X + rect.Width, rect.Y));
                        break;
                    case 3: // диагональ сверху-вниз
                        g.DrawLine(pen, new PointF(rect.X, rect.Y),
                            new PointF(rect.X + rect.Width, rect.Y + rect.Height));
                        break;
                    case 4: // верх-право
                        g.DrawLines(pen, new PointF[] {
                            new PointF(rect.X, rect.Y),
                            new PointF(rect.X + rect.Width, rect.Y),
                            new PointF(rect.X + rect.Width, rect.Y + rect.Height)});
                        break;
                    case 5: // низ-право
                        g.DrawLines(pen, new PointF[] {
                            new PointF(rect.X, rect.Y + rect.Height),
                            new PointF(rect.X + rect.Width, rect.Y + rect.Height),
                            new PointF(rect.X + rect.Width, rect.Y)});
                        break;
                    case 6: // лево-низ
                        g.DrawLines(pen, new PointF[] {
                            new PointF(rect.X, rect.Y),
                            new PointF(rect.X, rect.Y + rect.Height),
                            new PointF(rect.X + rect.Width, rect.Y + rect.Height)});
                        break;
                    case 7: // лево-вверх
                        g.DrawLines(pen, new PointF[] {
                            new PointF(rect.X, rect.Y + rect.Height),
                            new PointF(rect.X, rect.Y),
                            new PointF(rect.X + rect.Width, rect.Y)});
                        break;
                    case 8: // квадрат
                        g.DrawRectangle(pen, rect);
                        break;
                    case 9: // круг
                        g.DrawEllipse(pen, rect);
                        break;
                }
            }
            // Draw the focus rectangle if the mouse hovers over an item.
            e.DrawFocusRectangle();
        }

        private void cbLineKind_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cbox = (ComboBox)sender;
            drw.Props["LineKind"] = cbox.SelectedIndex;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawPlugin.colorComboDrawItem(sender, e);
        }

        private void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawPlugin.colorComboSelectedIndexChanged(sender, ref LastColorIndex,
                cbColor_SelectionChangeCommitted);
        }

        private void cbColor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Color color = DrawPlugin.colorComboSelectionChangeCommitted(sender);
            ComboBox cbox = (ComboBox)sender;
            if (cbox.Equals(cbColor)) drw.Props["Color"] = color;
            else if (cbox.Equals(cbColor0)) drw.Props["Color0"] = color;
            else if (cbox.Equals(cbColor1)) drw.Props["Color1"] = color;
            else return;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void styleComboFillList(object sender, int LastStyleIndex)
        {
            ComboBox cbox = (ComboBox)sender;
            cbox.Items.Clear();
            // получение всех имён доступных стилей
            cbox.Items.AddRange(DrawUtils.GetPenPatternNames()); // получение всех имён доступных типов линий
            cbox.Items.RemoveAt(0);
        }

        private void styleComboSelectInList(object sender, int style)
        {
            ComboBox cbox = (ComboBox)sender;
            int Index = style;
            if (Index >= 0) cbox.SelectedIndex = Index;
        }

        private void styleComboDrawItem(object sender, DrawItemEventArgs e)
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
                    p.Width = 2;
                    p.DashStyle = (System.Drawing.Drawing2D.DashStyle)(e.Index);
                    g.DrawLine(p, new Point(rect.Left, rect.Top + rect.Height / 2),
                                    new Point(rect.Right, rect.Top + rect.Height / 2));
                }
            }
            catch { }
            // Draw the focus rectangle if the mouse hovers over an item.
            e.DrawFocusRectangle();
        }

        private void cbLineStyle_DrawItem(object sender, DrawItemEventArgs e)
        {
            styleComboDrawItem(sender, e);
        }

        private void cbLineStyle_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cbox = (ComboBox)sender;
            int style = cbox.SelectedIndex;
            if (cbox.Equals(cbLineStyle)) drw.Props["LineStyle"] = style;
            else if (cbox.Equals(cbLineStyle0)) drw.Props["LineStyle0"] = style;
            else if (cbox.Equals(cbLineStyle1)) drw.Props["LineStyle1"] = style;
            else return;
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

        private void tbPtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                drw.Props["PtName"] = tbPtName.Text;
                tbPtName.SelectAll();
                updateSelector();
                if (updater != null) updater(drw, UpdateKind.UpdateAfter);
            }
        }

        private void updateSelector()
        {
            tabSelector.TabPages.Clear();
            if (String.IsNullOrWhiteSpace(tbPtName.Text))
            {
                tabSelector.TabPages.Add(tabNoParam);
            }
            else
            {
                tabSelector.TabPages.Add(tabOff);
                tabSelector.TabPages.Add(tabOn);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (selector != null)
            {
                string[] vals = selector("DinLine",
                    (string)drw.Props["PtName"],
                    (string)drw.Props["PtParam"]).ToString().Split(new char[] { '.' });
                if (vals.Length == 2)
                {
                    drw.Props["PtName"] = vals[0];
                    drw.Props["PtParam"] = vals[1];
                    tbPtName.Text = vals[0];
                    tbPtName.SelectAll();
                    updateSelector();
                    if (updater != null) updater(drw, UpdateKind.UpdateAfter);
                }
            }
        }

        private void tabSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            drw.Props["PV"] = (tabSelector.SelectedIndex == 1) ? true : false;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void frmDinText_FormClosing(object sender, FormClosingEventArgs e)
        {
            drw.Props["PV"] = false;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void udLineWidth_ValueChanged(object sender, EventArgs e)
        {
            drw.Props["LineWidth"] = (float)udLineWidth.Value;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void udLineWidth0_ValueChanged(object sender, EventArgs e)
        {
            drw.Props["LineWidth0"] = (float)udLineWidth0.Value;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void udLineWidth1_ValueChanged(object sender, EventArgs e)
        {
            drw.Props["LineWidth1"] = (float)udLineWidth1.Value;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void frmDinLine_Load(object sender, EventArgs e)
        {
            if (updater != null) updater(drw, UpdateKind.UpdateBefore);
        }
    }
}
