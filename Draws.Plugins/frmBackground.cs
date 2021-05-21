using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Draws.Plugins
{
    public partial class frmBackground : Form
    {
        UpdateDraw updater;
        SelectData selector;
        Draw drw;
        int LastColorIndex = 0;

        public frmBackground(Draw drw, UpdateDraw updater, SelectData selector)
        {
            InitializeComponent();
            this.drw = drw;
            this.updater = updater;
            this.selector = selector;
            cbExpanded.Click -= rbExpanded_Click;
            cbExpanded.Checked = (bool)drw.Props["Expanded"];
            cbExpanded.Click += rbExpanded_Click;
            cbSaveAspect.Click -= cbSaveAspect_Click;
            cbSaveAspect.Checked = (bool)drw.Props["SaveAspect"];
            cbSaveAspect.Click += cbSaveAspect_Click;
            cbSaveAspect.Enabled = udLeft.Enabled = udTop.Enabled = udWidth.Enabled =
                udHeight.Enabled = cbExpanded.Checked; 
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
            #region Свойства цвета фона
            DrawPlugin.colorComboFillList(cbBackColor, LastColorIndex);
            DrawPlugin.colorComboSelectInList(cbBackColor, (Color)drw.Props["BackColor"]);
            #endregion
            tbSchemeName.Text = (string)drw.Props["SchemeName"];
            tbDescriptor.Text = (string)drw.Props["Descriptor"];
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
            else return;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void tbDescriptor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                tbDescriptor.SelectAll();
                drw.Props["Descriptor"] = tbDescriptor.Text;
                if (updater != null) updater(drw, UpdateKind.UpdateAfter);
            }
        }

        private void tbSchemeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                tbDescriptor.SelectAll();
                if (selector != null)
                {
                    string screenname = tbSchemeName.Text;
                    if (!String.IsNullOrWhiteSpace(screenname) && !(bool)selector(screenname))
                    {
                        drw.Props["SchemeName"] = screenname;
                        tbSchemeName.SelectAll();
                        if (updater != null) updater(drw, UpdateKind.UpdateAfter);
                    }
                    else
                        MessageBox.Show(this, "Новое имя недопустимо или уже существует!",
                            "Переименование менмосхемы", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void rbExpanded_Click(object sender, EventArgs e)
        {
            drw.Props["Expanded"] = cbExpanded.Checked;
            cbSaveAspect.Enabled = udLeft.Enabled = udTop.Enabled = udWidth.Enabled =
                udHeight.Enabled = cbExpanded.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbSaveAspect_Click(object sender, EventArgs e)
        {
            drw.Props["SaveAspect"] = cbSaveAspect.Checked;
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

        private void frmBackground_Load(object sender, EventArgs e)
        {
            if (updater != null) updater(drw, UpdateKind.UpdateBefore);
        }

        private void tbDescriptor_TextChanged(object sender, EventArgs e)
        {
            drw.Props["Descriptor"] = tbDescriptor.Text;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

    }
}
