﻿using System;
using System.Windows.Forms;
using BaseServer;

namespace FetchOPC
{
    public partial class frmTuningOPC : Form
    {
        public frmTuningOPC()
        {
            InitializeComponent();
        }

        public bool CheckAuthorization()
        {
            this.tbCopyCode.Text = Data.MachineCode(this.tbCopyOwner.Text);
            if (Data.Authorization(this.tbCopyOwner.Text, this.tbCopyKey.Text))
            {
                errorProvider.SetError(this.tbCopyKey, String.Empty);
                return true;
            }
            else
            {
                errorProvider.SetError(this.tbCopyKey, "Ключ копии ошибочный!");
                return false;
            }
        }

        private void frmTuningOPC_Shown(object sender, EventArgs e)
        {
            CheckAuthorization();
        }
    }
}
