using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace RemXcs
{
    public partial class frmTuning : Form
    {
        public frmTuning()
        {
            InitializeComponent();
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            SaveGeneralSettings();
        }

        private void frmTuning_Load(object sender, EventArgs e)
        {
            cbSystemShell.Click -= cbSystemShell_Click;
            cbWindowMode.Click -= cbWindowMode_Click; 
            LoadGeneralSettings();
            cbSystemShell.Click += cbSystemShell_Click;
            cbWindowMode.Click += cbWindowMode_Click;
        }

        private void contextFetchServers_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            miDeleteFetchServer.Enabled = lvFetchServers.FocusedItem != null;
        }

        private void miAddFetchServer_Click(object sender, EventArgs e)
        {
            if (openFetchServerDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in openFetchServerDialog.FileNames)
                {
                    string filepath = Path.GetDirectoryName(filename);
                    if (filepath == Application.StartupPath)
                    {
                        string item = Path.GetFileNameWithoutExtension(filename);
                        if (lvFetchServers.FindItemWithText(item) == null)
                        {
                            FileVersionInfo myFileVersionInfo =
                                FileVersionInfo.GetVersionInfo(filename);
                            lvFetchServers.Items.Add(item).SubItems.
                                Add(myFileVersionInfo.Comments);
                        }
                    }
                }
            }
        }

        private void miDeleteFetchServer_Click(object sender, EventArgs e)
        {
            for (int i = lvFetchServers.Items.Count - 1; i >= 0; i--)
            {
                if (lvFetchServers.Items[i].Selected)
                    lvFetchServers.Items.Remove(lvFetchServers.Items[i]);
            }
        }

        private void cbSystemShell_Click(object sender, EventArgs e)
        {
            if (cbSystemShell.Checked)
            {
                cbWindowMode.Checked = false;
                cbWindowMode.Enabled = false;
            }
            else
                cbWindowMode.Enabled = true;
        }

        private void cbWindowMode_Click(object sender, EventArgs e)
        {
            if (cbWindowMode.Checked)
            {
                cbSystemShell.Checked = false;
                cbSystemShell.Enabled = false;
            }
            else
                cbSystemShell.Enabled = true;
        }

        private void lbFetchServers_MouseDown(object sender, MouseEventArgs e)
        {
            lvFetchServers.FocusedItem = lvFetchServers.GetItemAt(e.X, e.Y);
        }

        private void tsmiAddRemoteCamera_Click(object sender, EventArgs e)
        {
            using (frmChangeVideoMonAddr form = new frmChangeVideoMonAddr())
            {
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
                    form.tbWMEncoderAddr.Text.Trim().Length > 0 &&
                    form.tbWindowCaption.Text.Trim().Length > 0)
                {
                    lvRemoteCameras.Items.Add(form.tbWMEncoderAddr.Text
                        ).SubItems.Add(form.tbWindowCaption.Text);
                    lvRemoteCameras.FocusedItem =
                        lvRemoteCameras.Items[lvRemoteCameras.Items.Count - 1];
                    lvRemoteCameras.FocusedItem.Selected = true;
                }
            }
        }

        private void tsmiDeleteRemoteCamera_Click(object sender, EventArgs e)
        {
            for (int i = lvRemoteCameras.Items.Count - 1; i >= 0; i--)
            {
                if (lvRemoteCameras.Items[i].Selected)
                    lvRemoteCameras.Items.Remove(lvRemoteCameras.Items[i]);
            }
        }

    }
}
