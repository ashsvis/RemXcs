using System;
using System.Drawing;
using System.Windows.Forms;
using Draws.Plugins;

namespace Draws.Digital
{
    public partial class frmDinButton : Form
    {
        UpdateDraw updater;
        SelectData selector;
        Draw drw;
        int LastColorIndex = 0;

        public frmDinButton(Draw drw, UpdateDraw updater, SelectData selector)
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
            tbPtName.Text = (string)drw.Props["PtName"];
            cbFixed.Checked = (bool)drw.Props["Fixed"];
            cbDirect.Checked = (bool)drw.Props["Direct"];
            tbText.Text = (string)drw.Props["Text"];
        }

        private void cbFontColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawPlugin.colorComboDrawItem(sender, e);
        }

        private void cbFontColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawPlugin.colorComboSelectedIndexChanged(sender, ref LastColorIndex,
                cbFontColor_SelectionChangeCommitted);
        }

        private void cbFontColor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Color color = DrawPlugin.colorComboSelectionChangeCommitted(sender);
            ComboBox cbox = (ComboBox)sender;
            if (cbox.Equals(cbFontColor)) drw.Props["FontColor"] = color;
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
                string[] vals = selector("DinButton",
                    (string)drw.Props["PtName"],
                    (string)drw.Props["PtParam"]).ToString().Split(new char[] { '.' });
                if (vals.Length == 2)
                {
                    drw.Props["PtName"] = vals[0];
                    drw.Props["PtParam"] = vals[1];
                    tbPtName.Text = vals[0];
                    tbPtName.SelectAll();
                    if (updater != null) updater(drw, UpdateKind.UpdateAfter);
                }
            }
        }
        private void cbFixed_Click(object sender, EventArgs e)
        {
            drw.Props["Fixed"] = cbFixed.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbDirect_Click(object sender, EventArgs e)
        {
            drw.Props["Direct"] = cbDirect.Checked;
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

        private void frmDinButton_Load(object sender, EventArgs e)
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
