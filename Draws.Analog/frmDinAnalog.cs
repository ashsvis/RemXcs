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
    public partial class frmDinAnalog : Form
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

        public frmDinAnalog(Draw drw, UpdateDraw updater, SelectData selector)
        {
            InitializeComponent();
            this.drw = drw;
            this.updater = updater;
            this.selector = selector;
            #region Свойства для панели
            cbShowPanel.Checked = (bool)drw.Props["ShowPanel"];
            cbShowValue.Checked = (bool)drw.Props["ShowValue"];
            cbShowUnit.Checked = (bool)drw.Props["ShowUnit"];
            cbShowTag.Checked = (bool)drw.Props["ShowTag"];
            EnableFontGroup(cbShowValue.Checked || cbShowUnit.Checked);
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
            #region Свойства рамки и ее цвета
            cbFramed.Checked = (bool)drw.Props["Framed"];
            DrawPlugin.colorComboFillList(cbFrameColor, LastColorIndex);
            DrawPlugin.colorComboSelectInList(cbFrameColor, (Color)drw.Props["FrameColor"]);
            cbFrameColor.Enabled = cbFramed.Checked;
            #endregion
            #region Свойства указателя бара (уровня)
            cbBarLevelVisible.Checked = (bool)drw.Props["BarLevelVisible"];
            cbBarLevelInverse.Checked = (bool)drw.Props["BarLevelInverse"];
            rbShowLevel.Checked = (bool)drw.Props["ShowLevel"];
            rbShowBar.Checked = (bool)drw.Props["ShowBar"];
            DrawPlugin.colorComboFillList(cbBarLevelColor, LastColorIndex);
            DrawPlugin.colorComboSelectInList(cbBarLevelColor, (Color)drw.Props["BarLevelColor"]);
            cbBarLevelInverse.Enabled = cbBarLevelVisible.Checked;
            rbShowLevel.Enabled = cbBarLevelVisible.Checked;
            rbShowBar.Enabled = cbBarLevelVisible.Checked;
            cbBarLevelColor.Enabled = cbBarLevelVisible.Checked;
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
            tbPtName.Text = (string)drw.Props["PtName"] + "." + (string)drw.Props["PtParam"];
        }

        private void frmDinAnalog_Load(object sender, EventArgs e)
        {
            if (updater != null) updater(drw, UpdateKind.UpdateBefore);            
        }

        private void cbShowPanel_Click(object sender, EventArgs e)
        {
            drw.Props["ShowPanel"] = cbShowPanel.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbShowValue_Click(object sender, EventArgs e)
        {
            drw.Props["ShowValue"] = cbShowValue.Checked;
            EnableFontGroup(cbShowValue.Checked || cbShowUnit.Checked);
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbShowUnit_Click(object sender, EventArgs e)
        {
            drw.Props["ShowUnit"] = cbShowUnit.Checked;
            EnableFontGroup(cbShowValue.Checked || cbShowUnit.Checked);
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbShowTag_Click(object sender, EventArgs e)
        {
            drw.Props["ShowTag"] = cbShowTag.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbFramed_Click(object sender, EventArgs e)
        {
            drw.Props["Framed"] = cbFramed.Checked;
            cbFrameColor.Enabled = cbFramed.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void udLeft_ValueChanged(object sender, EventArgs e)
        {
            drw.Props["Framed"] = cbFramed.Checked;
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
            if (cbox.Equals(cbFrameColor))
                drw.Props["FrameColor"] = color;
            else if (cbox.Equals(cbBarLevelColor))
                drw.Props["BarLevelColor"] = color;
            else if (cbox.Equals(cbFontColor))
                drw.Props["FontColor"] = color;
            else if (cbox.Equals(cbColor))
                drw.Props["Color"] = color;
            else
                return;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbBarLevelVisible_Click(object sender, EventArgs e)
        {
            drw.Props["BarLevelVisible"] = cbBarLevelVisible.Checked;
            cbBarLevelInverse.Enabled = cbBarLevelVisible.Checked;
            rbShowLevel.Enabled = cbBarLevelVisible.Checked;
            rbShowBar.Enabled = cbBarLevelVisible.Checked;
            cbBarLevelColor.Enabled = cbBarLevelVisible.Checked;
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

        private void rbShowLevel_Click(object sender, EventArgs e)
        {
            drw.Props["ShowLevel"] = rbShowLevel.Checked;
            drw.Props["ShowBar"] = rbShowBar.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbBarLevelInverse_Click(object sender, EventArgs e)
        {
            drw.Props["BarLevelInverse"] = cbBarLevelInverse.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void tbPtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                string[] vals = tbPtName.Text.Split(new char[] { '.' });
                drw.Props["PtName"] = vals[0];
                if (vals.Length >= 1)
                    drw.Props["PtParam"] = vals[1];
                else
                    drw.Props["PtParam"] = "PV";
                tbPtName.SelectAll();
                ResetTestValue();
                if (updater != null) updater(drw, UpdateKind.UpdateAfter);
            }
        }

        private void ResetTestValue()
        {
            tbTest.Value = 0;
            drw.Props["PVText"] = "------";
            drw.Props["PVPercent"] = 0m;
            lbTest.Text = String.Format("{0} %", tbTest.Value);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (selector != null)
            {
                string[] vals = selector("DinAnalog",
                    (string)drw.Props["PtName"],
                    (string)drw.Props["PtParam"]).ToString().Split(new char[] { '.' });
                if (vals.Length == 2)
                {
                    drw.Props["PtName"] = vals[0];
                    drw.Props["PtParam"] = vals[1];
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
            decimal pveuhi = (decimal)drw.Props["PVEUHi"];
            decimal pveulo = (decimal)drw.Props["PVEULo"];
            int formatpv = (int)drw.Props["FormatPV"];

            drw.Props["PVText"] = DrawUtils.Float((pveuhi - pveulo) * value / 100m + pveulo, formatpv);
            drw.Props["PVPercent"] = value;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void frmDinAnalog_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResetTestValue();
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

    }
}
