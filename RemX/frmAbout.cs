using System.Windows.Forms;

namespace RemXcs
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void frmAbout_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void frmAbout_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Close();
        }
    }
}
