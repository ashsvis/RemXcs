using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Draws.Plugins;
using System.Drawing.Drawing2D;

namespace Draws.Digital
{
    public partial class frmDinValve : Form
    {
        UpdateDraw updater;
        SelectData selector;
        Draw drw;
        int LastColorIndex = 0;
        int LastSpinIndex = 0;
        List<PointF> points = new List<PointF>();

        public frmDinValve(Draw drw, UpdateDraw updater, SelectData selector)
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
            //updateSelector();
            kindComboFillList(cbSpinValve, LastSpinIndex);
            kindComboSelectInList(cbSpinValve, (int)drw.Props["SpinValve"]);
            #region Свойства цвета линии
            DrawPlugin.colorComboFillList(cbForeColor, LastColorIndex);
            DrawPlugin.colorComboSelectInList(cbForeColor, (Color)drw.Props["ForeColor0"]);
            #endregion
            #region Свойства цвета заполнения
            DrawPlugin.colorComboFillList(cbBackColor, LastColorIndex);
            DrawPlugin.colorComboSelectInList(cbBackColor, (Color)drw.Props["BackColor0"]);
            #endregion
        }

        private void kindComboFillList(object sender, int LastKindIndex)
        {
            ComboBox cbox = (ComboBox)sender;
            cbox.Items.Clear();
            // получение всех имён доступных стилей
            for (int i = 0; i < 4; i++) cbox.Items.Add(i.ToString());
            cbox.Text =LastKindIndex.ToString();
        }

        private void kindComboSelectInList(object sender, int kind)
        {
            ComboBox cbox = (ComboBox)sender;
            int Index = kind;
            if (Index >= 0) cbox.SelectedIndex = Index;
        }

        private void cbValveSpin_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int spinkind = e.Index;
            // Draw the background of the item.
            e.DrawBackground();
            Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1);
            Rectangle valverect = rect;
            valverect.Inflate(-5, -5);
            valverect.Height /= 2;
            valverect.Offset(0, valverect.Height / 2);
            DrawUtils.AddPointsRange(points, new PointF[4]
                { new PointF(valverect.X, valverect.Y),
                  new PointF(valverect.X + valverect.Width, valverect.Y + valverect.Height),
                  new PointF(valverect.X + valverect.Width, valverect.Y),
                  new PointF(valverect.X, valverect.Y + valverect.Height)});
            g.FillRectangle(SystemBrushes.ButtonFace, rect);
            g.DrawRectangle(SystemPens.ControlDark, rect);
            Color backcolor = (Color)drw.Props["BackColor" + tbValveState.Value];
            Color forecolor = (Color)drw.Props["ForeColor" + tbValveState.Value];
            float[] angles = new float[4] { 0, 90, -45, 45 };
            using (Pen pen = new Pen(forecolor, (tbValveState.Value < 3) ? 1f : 2f))
            {
                using (Brush brush = new SolidBrush(backcolor))
                {
                    DrawUtils.Rotate(points, angles[spinkind], rect);
                    g.FillPolygon(brush, DrawUtils.GetPoints(points));
                    g.DrawPolygon(pen, DrawUtils.GetPoints(points));
                }
            }
            // Draw the focus rectangle if the mouse hovers over an item.
            e.DrawFocusRectangle();
        }

        private void cbSpinValve_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cbox = (ComboBox)sender;
            drw.Props["SpinValve"] = cbox.SelectedIndex;
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
            if (cbox.Equals(cbBackColor)) drw.Props["BackColor" + tbValveState.Value] = color;
            else if (cbox.Equals(cbForeColor)) drw.Props["ForeColor" + tbValveState.Value] = color;
            else return;
            cbSpinValve.Invalidate();
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
                //updateSelector();
                if (updater != null) updater(drw, UpdateKind.UpdateAfter);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (selector != null)
            {
                string[] vals = selector("DinValve",
                    (string)drw.Props["PtName"],
                    String.Empty).ToString().Split(new char[] { '.' });
                if (vals.Length == 2)
                {
                    drw.Props["PtName"] = vals[0];
                    tbPtName.Text = vals[0];
                    tbPtName.SelectAll();
                    if (updater != null) updater(drw, UpdateKind.UpdateAfter);
                }
            }
        }

        //private void tabSelector_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    drw.Props["PV"] = (tabSelector.SelectedIndex == 1) ? true : false;
        //    if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        //}

        private void frmDinText_FormClosing(object sender, FormClosingEventArgs e)
        {
            drw.Props["PV"] = (int)0;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void frmDinLine_Load(object sender, EventArgs e)
        {
            if (updater != null) updater(drw, UpdateKind.UpdateBefore);
        }

        private void tbValveState_Scroll(object sender, EventArgs e)
        {
            string[] states = new string[8] 
                {"ХОД","ЗАКРЫТО","ОТКРЫТО","ОШИБКА","ХОД (АВАРИЯ)",
                 "ЗАКРЫТО (АВАРИЯ)","ОТКРЫТО (АВАРИЯ)","ОШИБКА (АВАРИЯ)"};
            lbValveState.Text = states[tbValveState.Value];
            DrawPlugin.colorComboSelectInList(cbBackColor, (Color)drw.Props["BackColor" + tbValveState.Value]);
            DrawPlugin.colorComboSelectInList(cbForeColor, (Color)drw.Props["ForeColor" + tbValveState.Value]);
            drw.Props["PV"] = (int)tbValveState.Value;
            cbSpinValve.Invalidate();
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void ResetTestValue()
        {
            drw.Props["PV"] = (int)0;
            tbValveState.Value = 0;
            lbValveState.Text = "ХОД";
        }

        private void frmDinValve_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResetTestValue();
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }
    }
}
