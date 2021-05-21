using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BaseServer;

namespace RemXcs
{
    public struct KD
    {
        public string Key;
        public string Descriptor;
    }

    public partial class frmSystemStat : Form
    {
        IUpdatePanel updater;
        string lastname = String.Empty;
        string lasttag = String.Empty;

        public frmSystemStat(Form panelhost)
        {
            InitializeComponent();
            updater = panelhost as IUpdatePanel;
        }

        private void frmSystemStat_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            lvFetchList.SetDoubleBuffered(true);
            FillTree();
            tmrFresh.Enabled = true;
        }

        private KD getVals(string value)
        {
            KD kd = new KD();
            string[] items = value.Split(new char[] { '=' });
            kd.Key = items[0];
            kd.Descriptor = (items.Length == 2) ? items[1] : String.Empty;
            return kd;
        }

        private void findNode(string item, TreeNode node = null)
        {
            TreeNodeCollection nodes = (node != null) ? node.Nodes : tvNodes.Nodes;
            foreach (TreeNode n in nodes)
            {
                if (n.Nodes.Count > 0)
                {
                    if (n.Text == item)
                    {
                        tvNodes.SelectedNode = n;
                        return;
                    }
                    else
                        findNode(item, n);
                }
                else
                {
                    if (n.Text == item)
                    {
                        tvNodes.SelectedNode = n;
                        return;
                    }
                }
            }
        }

        public void RestoreTreePos(string name)
        {
            findNode(name);
        }

        private void FillTree()
        {
            if (!backBuildTree.IsBusy)
                backBuildTree.RunWorkerAsync();
        }

        private void tmrFresh_Tick(object sender, EventArgs e)
        {
            FillTree();
        }

        private void tvNodes_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                lastname = e.Node.Text;
                fillFetchList(e.Node.Tag);
            }
        }

        private void findTag(string name)
        {
            for (int i=0; i < lvFetchList.Items.Count; i++)
                if (lvFetchList.Items[i].Text == name)
                {
                    ListViewItem lvi = lvFetchList.Items[i];
                    lvi.Selected = true;
                    lvFetchList.FocusedItem = lvi;
                    lvi.EnsureVisible();
                    break;
                }
        }

        private void fillFetchList(object key)
        {
            if (!backBuildList.IsBusy)
                backBuildList.RunWorkerAsync(key);
        }

        private void lvFetchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvFetchList.FocusedItem != null)
                lasttag = lvFetchList.FocusedItem.Text;
        }

        private void tvNodes_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                tvNodes.SelectedNode = tvNodes.GetNodeAt(e.X, e.Y);
                if (tvNodes.SelectedNode != null && tvNodes.SelectedNode.Tag != null)
                {
                    string key = tvNodes.SelectedNode.Tag.ToString();
                    string[] typedesc = Data.GetClientTypeAndDesc(key);
                    miRefreshFetchList.Tag = key;
                    miRefreshFetchList.Visible = (typedesc[0] == "F");
                    miReloadStation.Tag = key;
                    miReloadStation.Visible = (typedesc[0] == "S");
                    Point p = tvNodes.PointToScreen(new Point(e.X, e.Y));
                    cmTree.Show(p.X, p.Y);
                }
            }
        }

        private void miRefreshFetchList_Click(object sender, EventArgs e)
        {
            string key = ((ToolStripMenuItem)sender).Tag.ToString();
            if (key.Length > 0)
            {
                string[] typedesc = Data.GetClientTypeAndDesc(key);
                Data.SendToSystemLog(Properties.Settings.Default.Station,
                    "Станция RemX",
                    String.Format("Требование на перезагрузку списка опроса для \"{0}\"",
                    typedesc[1]));
                Data.SendClientCommand(key, "RELOAD");
            }
        }

        private void miReloadStation_Click(object sender, EventArgs e)
        {
            string key = ((ToolStripMenuItem)sender).Tag.ToString();
            if (key.Length > 0)
            {
                string[] typedesc = Data.GetClientTypeAndDesc(key);
                Data.SendToSystemLog(Properties.Settings.Default.Station,
                    "Станция RemX",
                    String.Format("Требование на перезагрузку для \"{0}\"",
                    typedesc[1]));
                Data.SendClientCommand(key, "RELOAD");
            }
        }

        private int lastColumn = -1;
        private void lvFetchList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (lastColumn != e.Column)
            {
                this.lvFetchList.ListViewItemSorter = new ListViewItemComparer(e.Column);
                lastColumn = e.Column;
            }
            else
            {
                if (this.lvFetchList.ListViewItemSorter is ListViewItemComparer)
                    this.lvFetchList.ListViewItemSorter = new ListViewItemReverseComparer(e.Column);
                else
                    this.lvFetchList.ListViewItemSorter = new ListViewItemComparer(e.Column);
            }
            if (this.lvFetchList.FocusedItem != null)
                this.lvFetchList.FocusedItem.EnsureVisible();
        }

        private void backBuildTree_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            object[] lists = new object[8];
            for (int i = 1; i <= 8; i++)
            {
                List<string> list = Data.GetClients(i);
                lists[i - 1] = list;
            }
            e.Result = lists;
        }

        private void backBuildTree_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            object[] lists = (object[])e.Result;
            tvNodes.BeginUpdate();
            try
            {
                tvNodes.Nodes.Clear();
                for (int i = 1; i <= 8; i++)
                {
                    List<string> list = (List<string>)lists[i - 1]; //Data.GetClients(i);
                    if (list.Count > 0)
                    {
                        KD kd = getVals(list[0]);
                        TreeNode nd = tvNodes.Nodes.Add(i + ". " + kd.Descriptor);
                        nd.Tag = kd.Key;
                        for (int j = 1; j < list.Count; j++)
                        {
                            kd = getVals(list[j]);
                            nd.Nodes.Add(kd.Descriptor).Tag = kd.Key;
                        }
                        nd.Expand();
                    }
                }
            }
            finally
            {
                tvNodes.EndUpdate();
            }
            RestoreTreePos(lastname);
            if (tvNodes.SelectedNode == null)
                lvFetchList.Items.Clear();
        }

        private void backBuildList_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            object key = e.Argument;
            ArrayList list = Data.GetClientFetchList(key.ToString());
            e.Result = new object[] { key, list };
        }

        private void backBuildList_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            object[] results = (object[])e.Result;
            object key = results[0];
            lvFetchList.BeginUpdate();
            try
            {
                lvFetchList.Items.Clear();
                if (key != null && key.ToString().Length > 0)
                {
                    ArrayList list = (ArrayList)results[1]; //Data.GetClientFetchList(key.ToString());
                    foreach (object obj in list)
                    {
                        List<string> values = (List<string>)obj;
                        if (values.Count > 0)
                        {
                            ListViewItem lvi = lvFetchList.Items.Add(values[0]);
                            for (int i = 1; i < values.Count; i++)
                                lvi.SubItems.Add(values[i]);
                        }
                    }
                }
            }
            finally
            {
                lvFetchList.EndUpdate();
            }
            findTag(lasttag);
        }
    }
}
