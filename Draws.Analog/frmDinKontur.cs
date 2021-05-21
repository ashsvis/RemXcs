using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Draws.Plugins;

namespace Draws.Analog
{
    public partial class frmDinKontur : Form
    {
        UpdateDraw updater;
        SelectData selector;
        Draw drw;
        int LastColorIndex = 0;

        private void EnableFontGroup(bool enable)
        {
            cbFontName.Enabled = enable;
            udFontSize.Enabled = enable;
            cbFontColor.Enabled = enable;
            cbBold.Enabled = enable;
            cbItalic.Enabled = enable;
            cbUnderline.Enabled = enable;
            cbStrikeout.Enabled = enable;
        }

        public frmDinKontur(Draw drw, UpdateDraw updater, SelectData selector)
        {
            InitializeComponent();
            this.drw = drw;
            this.updater = updater;
            this.selector = selector;
            #region Свойства для панели
            EnableFontGroup(true);
            #endregion
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
            #region Свойства шрифта
            DrawPlugin.fontComboFillList(cbFontName);
            cbFontName.Text = (string)drw.Props["FontName"];
            udFontSize.ValueChanged -= udFontSize_ValueChanged;
            udFontSize.Value = (decimal)(float)drw.Props["FontSize"];
            udFontSize.ValueChanged += udFontSize_ValueChanged;
            cbBold.Checked = (bool)drw.Props["FontBold"];
            cbItalic.Checked = (bool)drw.Props["FontItalic"];
            cbUnderline.Checked = (bool)drw.Props["FontUnderline"];
            cbStrikeout.Checked = (bool)drw.Props["FontStrikeOut"];
            DrawPlugin.colorComboFillList(cbFontColor, LastColorIndex);
            DrawPlugin.colorComboSelectInList(cbFontColor, (Color)drw.Props["FontColor"]);
            #endregion
            #region Свойства цвета фона
            DrawPlugin.colorComboFillList(cbColor, LastColorIndex);
            DrawPlugin.colorComboSelectInList(cbColor, (Color)drw.Props["Color"]);
            #endregion
            tbPtName.Text = (string)drw.Props["PtName"];
        }

        private void frmDinKontur_Load(object sender, EventArgs e)
        {
            if (updater != null) updater(drw, UpdateKind.UpdateBefore);
        }

        private void cbFrameColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawPlugin.colorComboDrawItem(sender, e);
        }

        private void cbFrameColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawPlugin.colorComboSelectedIndexChanged(sender, ref LastColorIndex,
                cbFrameColor_SelectionChangeCommitted);
        }

        private void cbFrameColor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Color color = DrawPlugin.colorComboSelectionChangeCommitted(sender);
            ComboBox cbox = (ComboBox)sender;
            if (cbox.Equals(cbFontColor)) drw.Props["FontColor"] = color;
            else if (cbox.Equals(cbColor)) drw.Props["Color"] = color;
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

        private void cbFontName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            drw.Props["FontName"] = cbFontName.Items[cbFontName.SelectedIndex].ToString();
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbFontName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                int index = cbFontName.Items.IndexOf(cbFontName.Text);
                if (index >= 0)
                {
                    drw.Props["FontName"] = cbFontName.Items[index].ToString();
                    cbFontName.SelectAll();
                    if (updater != null) updater(drw, UpdateKind.UpdateAfter);
                }
            }
        }

        private void udFontSize_ValueChanged(object sender, EventArgs e)
        {
            drw.Props["FontSize"] = (float)udFontSize.Value;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbBold_Click(object sender, EventArgs e)
        {
            drw.Props["FontBold"] = cbBold.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbItalic_Click(object sender, EventArgs e)
        {
            drw.Props["FontItalic"] = cbItalic.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbUnderline_Click(object sender, EventArgs e)
        {
            drw.Props["FontUnderline"] = cbUnderline.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbStrikeout_Click(object sender, EventArgs e)
        {
            drw.Props["FontStrikeOut"] = cbStrikeout.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void tbPtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                drw.Props["PtName"] = tbPtName.Text;
                tbPtName.SelectAll();
                if (updater != null) updater(drw, UpdateKind.UpdateAfter);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (selector != null)
            {
                string[] vals = selector("DinKontur", (string)drw.Props["PtName"],
                    String.Empty).ToString().Split(new char[] { '.' });
                if (vals.Length == 2)
                {
                    drw.Props["PtName"] = vals[0];
                    tbPtName.Text = vals[0];
                    tbPtName.SelectAll();
                    ResetTestValue();
                    if (updater != null) updater(drw, UpdateKind.UpdateAfter);
                }
            }
        }

        private void tbTest_Scroll(object sender, EventArgs e)
        {
            lbTest.Text = String.Format("{0} %", tbTest.Value);
            decimal value = tbTest.Value;
            int formatpv = (int)drw.Props["FormatPV"];
            decimal pveuhi = (decimal)drw.Props["PVEUHi"];
            decimal pveulo = (decimal)drw.Props["PVEULo"];
            drw.Props["PVText"] = DrawUtils.Float((pveuhi - pveulo) * value / 100m + pveulo, formatpv);
            drw.Props["PVPercent"] = value;
            decimal speuhi = (decimal)drw.Props["SPEUHi"];
            decimal speulo = (decimal)drw.Props["SPEULo"];
            drw.Props["SPText"] = DrawUtils.Float((speuhi - speulo) * value / 100m + speulo, formatpv);
            drw.Props["SPPercent"] = value;
            decimal opeuhi = (decimal)drw.Props["OPEUHi"];
            decimal opeulo = (decimal)drw.Props["OPEULo"];
            drw.Props["OPText"] = DrawUtils.Float((opeuhi - opeulo) * value / 100m + opeulo, formatpv);
            drw.Props["OPPercent"] = value;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void ResetTestValue()
        {
            tbTest.Value = 0;
            drw.Props["PVText"] = "------";
            drw.Props["PVPercent"] = 0m;
            drw.Props["SPText"] = "------";
            drw.Props["SPPercent"] = 0m;
            drw.Props["OPText"] = "------";
            drw.Props["OPPercent"] = 0m;
            lbTest.Text = String.Format("{0} %", tbTest.Value);
        }

        private void frmDinKontur_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResetTestValue();
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

    }
}
