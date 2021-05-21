using System;
using System.Windows.Forms;
using BaseServer;

namespace FetchROC809
{
    public partial class frmTuningROC809 : Form
    {
        public frmTuningROC809()
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

        private void frmTuningROC809_Shown(object sender, EventArgs e)
        {
            CheckAuthorization();
        }
    }
}
