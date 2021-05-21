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
    public partial class frmDinDigital : Form
    {
        UpdateDraw updater;
        SelectData selector;
        Draw drw;

        public frmDinDigital(Draw drw, UpdateDraw updater, SelectData selector)
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
            switch ((int)drw.Props["DigitalKind"])
            {
                case 0: rbSquare.Checked = true; break;
                case 1: rbRing.Checked = true; break;
                case 2: rbRectangle.Checked = true; break;
                case 3: rbEllipce.Checked = true; break;
            }
            switch ((int)drw.Props["BorderKind"])
            {
                case 0: rbNoBorder.Checked = true; break;
                case 1: rbSingleBorder.Checked = true; break;
                case 2: rbRichBorder.Checked = true; break;
            }
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
                if (updater != null) updater(drw, UpdateKind.UpdateAfter);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (selector != null)
            {
                string[] vals = selector("DinDigital",
                    (string)drw.Props["PtName"],
                    (string)drw.Props["PtParam"]).ToString().Split(new char[] { '.' });
                if (vals.Length == 2)
                {
                    drw.Props["PtName"] = vals[0];
                    drw.Props["PtParam"] = vals[1];
                    tbPtName.Text = vals[0];
                    tbPtName.SelectAll();
                    rbTestOff.Checked = true;
                    if (updater != null) updater(drw, UpdateKind.UpdateAfter);
                }
            }
        }

        private void rbSquare_Click(object sender, EventArgs e)
        {
            RadioButton item = (RadioButton)sender;
            drw.Props["DigitalKind"] = int.Parse(item.Tag.ToString());
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void rbNoBorder_Click(object sender, EventArgs e)
        {
            RadioButton item = (RadioButton)sender;
            drw.Props["BorderKind"] = int.Parse(item.Tag.ToString());
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void rbTestOff_Click(object sender, EventArgs e)
        {
            RadioButton item = (RadioButton)sender;
            drw.Props["PV"] = Boolean.Parse(item.Tag.ToString());
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void frmDinDigital_FormClosing(object sender, FormClosingEventArgs e)
        {
            drw.Props["PV"] = false;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void frmDinDigital_Load(object sender, EventArgs e)
        {
            if (updater != null) updater(drw, UpdateKind.UpdateBefore);
        }

    }
}
