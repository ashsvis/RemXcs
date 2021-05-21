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
    public partial class frmDinText : Form
    {
        UpdateDraw updater;
        SelectData selector;
        Draw drw;
        int LastColorIndex = 0;

        public frmDinText(Draw drw, UpdateDraw updater, SelectData selector)
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
            cbShowPanel.Checked = (bool)drw.Props["ShowPanel"];
            cbSolid.Checked = (bool)drw.Props["Solid"];
            #region Свойства рамки и ее цвета
            cbFramed.Checked = (bool)drw.Props["Framed"];
            DrawPlugin.colorComboFillList(cbFrameColor, LastColorIndex);
            DrawPlugin.colorComboSelectInList(cbFrameColor, (Color)drw.Props["FrameColor"]);
            cbFrameColor.Enabled = cbFramed.Checked;
            #endregion
            tbPtName.Text = (string)drw.Props["PtName"];
            updateSelector();
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
            tbText.Text = (string)drw.Props["Text"];
            #region Свойства шрифта при "0"
            DrawPlugin.fontComboFillList(cbFontName0);
            cbFontName0.Text = (string)drw.Props["Font0Name"];
            udFontSize0.Value = (decimal)(float)drw.Props["Font0Size"];
            cbBold0.Checked = (bool)drw.Props["Font0Bold"];
            cbItalic0.Checked = (bool)drw.Props["Font0Italic"];
            cbUnderline0.Checked = (bool)drw.Props["Font0Underline"];
            cbStrikeout0.Checked = (bool)drw.Props["Font0StrikeOut"];
            DrawPlugin.colorComboFillList(cbFontColor0, LastColorIndex);
            DrawPlugin.colorComboSelectInList(cbFontColor0, (Color)drw.Props["Font0Color"]);
            #endregion
            #region Свойства цвета фона при "0"
            DrawPlugin.colorComboFillList(cbColor0, LastColorIndex);
            DrawPlugin.colorComboSelectInList(cbColor0, (Color)drw.Props["Color0"]);
            #endregion
            tbText0.Text = (string)drw.Props["Text0"];
            #region Свойства шрифта при "1"
            DrawPlugin.fontComboFillList(cbFontName1);
            cbFontName1.Text = (string)drw.Props["Font1Name"];
            udFontSize1.Value = (decimal)(float)drw.Props["Font1Size"];
            cbBold1.Checked = (bool)drw.Props["Font1Bold"];
            cbItalic1.Checked = (bool)drw.Props["Font1Italic"];
            cbUnderline1.Checked = (bool)drw.Props["Font1Underline"];
            cbStrikeout1.Checked = (bool)drw.Props["Font1StrikeOut"];
            DrawPlugin.colorComboFillList(cbFontColor1, LastColorIndex);
            DrawPlugin.colorComboSelectInList(cbFontColor1, (Color)drw.Props["Font1Color"]);
            #endregion
            #region Свойства цвета фона при "1"
            DrawPlugin.colorComboFillList(cbColor1, LastColorIndex);
            DrawPlugin.colorComboSelectInList(cbColor1, (Color)drw.Props["Color1"]);
            #endregion
            tbText1.Text = (string)drw.Props["Text1"];
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
            if (cbox.Equals(cbFrameColor)) drw.Props["FrameColor"] = color;
            else if (cbox.Equals(cbFontColor)) drw.Props["FontColor"] = color;
            else if (cbox.Equals(cbColor)) drw.Props["Color"] = color;
            else if (cbox.Equals(cbFontColor0)) drw.Props["Font0Color"] = color;
            else if (cbox.Equals(cbColor0)) drw.Props["Color0"] = color;
            else if (cbox.Equals(cbFontColor1)) drw.Props["Font1Color"] = color;
            else if (cbox.Equals(cbColor1)) drw.Props["Color1"] = color;
            else return;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbShowPanel_Click(object sender, EventArgs e)
        {
            drw.Props["ShowPanel"] = cbShowPanel.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbSolid_Click(object sender, EventArgs e)
        {
            drw.Props["Solid"] = cbSolid.Checked;
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

        private void tbText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                drw.Props["Text"] = tbText.Text;
                tbText.SelectAll();
                if (updater != null) updater(drw, UpdateKind.UpdateAfter);
            }
        }

        private void cbFontName0_SelectionChangeCommitted(object sender, EventArgs e)
        {
            drw.Props["Font0Name"] = cbFontName0.Items[cbFontName0.SelectedIndex].ToString();
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbFontName0_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                int index = cbFontName0.Items.IndexOf(cbFontName0.Text);
                if (index >= 0)
                {
                    drw.Props["Font0Name"] = cbFontName0.Items[index].ToString();
                    cbFontName0.SelectAll();
                    if (updater != null) updater(drw, UpdateKind.UpdateAfter);
                }
            }
        }

        private void udFontSize0_ValueChanged(object sender, EventArgs e)
        {
            drw.Props["Font0Size"] = (float)udFontSize0.Value;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbBold0_Click(object sender, EventArgs e)
        {
            drw.Props["Font0Bold"] = cbBold0.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbItalic0_Click(object sender, EventArgs e)
        {
            drw.Props["Font0Italic"] = cbItalic0.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbUnderline0_Click(object sender, EventArgs e)
        {
            drw.Props["Font0Underline"] = cbUnderline0.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbStrikeout0_Click(object sender, EventArgs e)
        {
            drw.Props["Font0StrikeOut"] = cbStrikeout0.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void tbText0_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                drw.Props["Text0"] = tbText0.Text;
                tbText0.SelectAll();
                if (updater != null) updater(drw, UpdateKind.UpdateAfter);
            }
        }

        private void cbFontName1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            drw.Props["Font1Name"] = cbFontName1.Items[cbFontName1.SelectedIndex].ToString();
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbFontName1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                int index = cbFontName1.Items.IndexOf(cbFontName1.Text);
                if (index >= 0)
                {
                    drw.Props["Font1Name"] = cbFontName1.Items[index].ToString();
                    cbFontName1.SelectAll();
                    if (updater != null) updater(drw, UpdateKind.UpdateAfter);
                }
            }
        }

        private void udFontSize1_ValueChanged(object sender, EventArgs e)
        {
            drw.Props["Font1Size"] = (float)udFontSize1.Value;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbBold1_Click(object sender, EventArgs e)
        {
            drw.Props["Font1Bold"] = cbBold1.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbItalic1_Click(object sender, EventArgs e)
        {
            drw.Props["Font1Italic"] = cbItalic1.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbUnderline1_Click(object sender, EventArgs e)
        {
            drw.Props["Font1Underline"] = cbUnderline1.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbStrikeout1_Click(object sender, EventArgs e)
        {
            drw.Props["Font1StrikeOut"] = cbStrikeout1.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void tbText1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                drw.Props["Text1"] = tbText1.Text;
                tbText1.SelectAll();
                if (updater != null) updater(drw, UpdateKind.UpdateAfter);
            }
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
                tabSelector.TabPages.Add(tabNoParam);
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
                string[] vals = selector("DinText",
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

        private void frmDinText_Load(object sender, EventArgs e)
        {
            if (updater != null) updater(drw, UpdateKind.UpdateBefore);
        }

        private void tbText_TextChanged(object sender, EventArgs e)
        {
            drw.Props["Text"] = tbText.Text;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }
    }
}
