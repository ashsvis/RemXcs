using System.Windows.Forms;

namespace DataEditor
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
