using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemXcs
{
    public partial class frmRemoteView : Form
    {
        public frmRemoteView(string urlsource)
        {
            InitializeComponent();
            wpfucRemoteView RemoteView = new RemXcs.wpfucRemoteView(urlsource);
            elementHost1.Child = RemoteView;
        }

        private void frmRemoteView_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
