using System;
using System.Drawing;
using System.Windows.Forms;
using Draws.Plugins;

namespace Draws.Common
{
    public partial class frmDinImage : Form
    {
        UpdateDraw updater;
        SelectData selector;
        Draw drw;

        public frmDinImage(Draw drw, UpdateDraw updater, SelectData selector)
        {
            InitializeComponent();
            this.drw = drw;
            this.updater = updater;
            this.selector = selector;
            #region Свойства положения и размера
            udLeft.ValueChanged -= udLeft_ValueChanged;
            decimal value = (decimal)(float)drw.Props["Left"];
            udLeft.Value = (value > 0) ? value : 0;
            udLeft.ValueChanged += udLeft_ValueChanged;
            udTop.ValueChanged -= udTop_ValueChanged;
            value = (decimal)(float)drw.Props["Top"];
            udTop.Value = (value > 0) ? value : 0;
            udTop.ValueChanged += udTop_ValueChanged;
            udWidth.ValueChanged -= udWidth_ValueChanged;
            udWidth.Value = (decimal)(float)drw.Props["Width"];
            udWidth.ValueChanged += udWidth_ValueChanged;
            udHeight.ValueChanged -= udHeight_ValueChanged;
            udHeight.Value = (decimal)(float)drw.Props["Height"];
            udHeight.ValueChanged += udHeight_ValueChanged;
            #endregion
            #region Свойства для картинки
            cbMozaika.Checked = (bool)drw.Props["Mozaika"]; // замостить
            cbStretch.Checked = (bool)drw.Props["Stretch"]; // растянуть или сжать
            cbTransparent.Checked = (bool)drw.Props["Transparent"]; // прозрачный
            #endregion
            Image image = (Image)selector("ImageFileName", (string)drw.Props["ImageName"]);
            lbFileName.Text = drw.Props["ImageName"].ToString().ToLower();
            if (image != null) pbPreview.Image = image;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            pbPreview.Image = null;
            drw.Props["ImageName"] = String.Empty;
            lbFileName.Text = String.Empty;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbMozaika_Click(object sender, EventArgs e)
        {
            drw.Props["Mozaika"] = cbMozaika.Checked;
            if (cbMozaika.Checked)
            {
                cbStretch.Checked = false;
                drw.Props["Stretch"] = false;
            }
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbStretch_Click(object sender, EventArgs e)
        {
            drw.Props["Stretch"] = cbStretch.Checked;
            if (cbStretch.Checked)
            {
                cbMozaika.Checked = false;
                drw.Props["Mozaika"] = false;
            }
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void cbTransparent_Click(object sender, EventArgs e)
        {
            drw.Props["Transparent"] = cbTransparent.Checked;
            if (updater != null) updater(drw, UpdateKind.UpdateAfter);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string imageName = (string)selector("SelectImageName", (string)drw.Props["ImageName"]);
            if (!String.IsNullOrWhiteSpace(imageName))
            {
                Image image = (Image)selector("ImageFileName", imageName);
                if (image != null)
                {
                    pbPreview.Image = image;
                    drw.Props["ImageName"] = imageName.ToLower();
                    lbFileName.Text = imageName.ToLower();
                    if (updater != null) updater(drw, UpdateKind.UpdateAfter);
                }
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

        private void frmDinImage_Load(object sender, EventArgs e)
        {
            if (updater != null) updater(drw, UpdateKind.UpdateBefore);
        }
    }
}
