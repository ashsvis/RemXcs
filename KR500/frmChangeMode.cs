using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Points.KR500
{
    public partial class frmChangeMode : Form
    {
        public frmChangeMode()
        {
            InitializeComponent();
        }

        private int mode = 0;

        public int Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
                switch (mode)
                {
                    case 0:
                        rbHand.Checked = true;
                        break;
                    case 1:
                         rbAuto.Checked = true;
                       break;
                    //case 2: rbCascad.Checked = true; break;
                }
            }
        }

        private void rbAuto_Click(object sender, EventArgs e)
        {
            mode = 1;
        }

        private void rbHand_Click(object sender, EventArgs e)
        {
            mode = 0;
        }
    }
}
