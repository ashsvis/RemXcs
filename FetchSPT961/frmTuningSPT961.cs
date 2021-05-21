using System;
using System.Windows.Forms;
using BaseServer;

namespace FetchSPT961
{
    public partial class frmTuningSPT961 : Form
    {
        public frmTuningSPT961()
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

        private void frmTuningSPT961_Shown(object sender, EventArgs e)
        {
            CheckAuthorization();
        }

    }
}
