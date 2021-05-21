using System.Windows.Forms;
using BaseServer;

namespace SyncServer
{
    public partial class frmShowList : Form
    {
        public frmShowList()
        {
            InitializeComponent();
            lvStatus.SetDoubleBuffered(true);
            //lvStatus.ListViewItemSorter = new ListViewItemReverseDateComparer();
        }

        private void miUpdateTableAll_Click(object sender, System.EventArgs e)
        {
            string tablename = miUpdateTableAll.Tag.ToString();
            // TODO: реализовать, нужна информация о таблице и базе данных
        }

        private void lvStatus_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //ListViewItem lvi = lvStatus.GetItemAt(e.X, e.Y);
                //if (lvi != null)
                //{
                //    miUpdateTableAll.Tag = lvi.Text;
                //    lvPopup.Show(lvStatus, e.Location);
                //}
            }
        }

    }
}
