using System.Windows.Forms;

namespace RemXcs
{
    public partial class frmPassword : Form
    {
        public frmPassword()
        {
            InitializeComponent();
        }

        public string Password { get { return tbPassword.Text; } }
    }
}
