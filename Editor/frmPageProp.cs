using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace DataEditor
{
    public partial class frmPageProp : Form
    {
        public bool Landscape { get; set; }
        public Margins Margins { get; set; }
        public string PaperName { get; set; }

        public frmPageProp(bool landscape, Margins margins, string paperName,
            PrinterSettings.PaperSizeCollection papersizes)
        {
            InitializeComponent();
            this.Landscape = landscape;
            this.Margins = margins;
            rbHorizontal.Checked = landscape;
            tbLeft.Text = margins.Left.ToString();
            tbRight.Text = margins.Right.ToString();
            tbTop.Text = margins.Top.ToString();
            tbBottom.Text = margins.Bottom.ToString();
            rbA4.Checked = paperName.Equals("A4");
            rbA3.Checked = paperName.Equals("A3");
            rbA4.Enabled = false;
            rbA3.Enabled = false;
            PaperSize pkSize;
            for (int i = 0; i < papersizes.Count; i++)
            {
                pkSize = papersizes[i];
                if (!rbA4.Enabled && pkSize.PaperName.Equals("A4"))
                    rbA4.Enabled = true;
                if (!rbA3.Enabled && pkSize.PaperName.Equals("A3"))
                    rbA3.Enabled = true;
            }            
        }

        private void rbVertical_Click(object sender, EventArgs e)
        {
            this.Landscape = false;
        }

        private void rbHorizontal_Click(object sender, EventArgs e)
        {
            this.Landscape = true;
        }

        private void tbLeft_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (int.TryParse(tbLeft.Text, out value) && value >= 0)
            {
                this.Margins.Left = value;
            }
        }

        private void tbRight_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (int.TryParse(tbRight.Text, out value) && value >= 0)
            {
                this.Margins.Right = value;
            }
        }

        private void tbTop_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (int.TryParse(tbTop.Text, out value) && value >= 0)
            {
                this.Margins.Top = value;
            }
        }

        private void tbBottom_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (int.TryParse(tbBottom.Text, out value) && value >= 0)
            {
                this.Margins.Bottom = value;
            }
        }

        private void rbA4_Click(object sender, EventArgs e)
        {
            this.PaperName = "A4";
        }

        private void rbA3_Click(object sender, EventArgs e)
        {
            this.PaperName = "A3";
        }

    }
}
