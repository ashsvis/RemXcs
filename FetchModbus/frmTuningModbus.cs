using System;
using System.Windows.Forms;
using BaseServer;

namespace FetchModbus
{
    public partial class frmTuningModbus : Form
    {
        public CheckBox[] checkboxes;
        public ComboBox[] channelboxes;
        public ComboBox[] comboboxes;

        public frmTuningModbus()
        {
            InitializeComponent();
            checkboxes = new CheckBox[] { cbItem1, cbItem2, cbItem3, cbItem4 };
            channelboxes = new ComboBox[] { cbChan1, cbChan2, cbChan3, cbChan4 };
            comboboxes = new ComboBox[] { comboBox1, comboBox2, comboBox3, comboBox4 };
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

        private void frmTuningVirtuals_Shown(object sender, EventArgs e)
        {
            CheckAuthorization();
        }

        private void cbItem1_Click(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            int index;
            if (cb.Tag != null && int.TryParse(cb.Tag.ToString(), out index) &&
                index > 0 && index <= checkboxes.Length)
            {
                channelboxes[index - 1].Enabled = cb.Checked;
                comboboxes[index - 1].Enabled = cb.Checked;
                if (!cb.Checked)
                {
                    channelboxes[index - 1].SelectedIndex = -1;
                    comboboxes[index - 1].Text = String.Empty;
                }
            }
        }
    }
}
