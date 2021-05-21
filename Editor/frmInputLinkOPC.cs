using System;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using Points.Plugins;

namespace DataEditor
{
    public partial class frmInputLinkOPC : Form
    {
        OpcBridgeSupport opc;
        private void findServer(string server)
        {
            foreach (TreeNode node in tvServers.Nodes)
            {
                if (node.Nodes.Count > 0)
                {
                    foreach (TreeNode name in node.Nodes)
                    {
                        if (name.Text == server)
                        {
                            tvServers.SelectedNode = name;
                            name.Expand();
                            return;
                        }
                    }
                }
            }
        }

        public string Server
        {
            get { return tbServer.Text.Trim(); }
            set
            {
                string server;
                if (String.IsNullOrWhiteSpace(value)) server = tbServer.Text;
                else server = value;
                tbServer.Text = server;
            }
        }
        public string Group
        { 
            get { return tbGroup.Text.Trim(); }
            set
            {
                if (!String.IsNullOrWhiteSpace(value)) tbGroup.Text = value;
            }
        }

        private void findItem(string item)
        {
            TreeNode node = tvServers.SelectedNode;
            if (node != null)
            {
                fillNodes(node);
                findNode(node, item);
            }
        }

        public string Item
        { 
            get { return tbItem.Text.Trim(); }
            set
            {
                string item;
                if (String.IsNullOrWhiteSpace(value)) item = tbItem.Text;
                else item = value;
                tbItem.Text = item;
            }
        }

        int cdt;
        public int CDT { get { return cdt; } set { cdt = value; } }

        private void findNode(TreeNode node, string item)
        {
            foreach (TreeNode n in node.Nodes)
            {
                if (n.Nodes.Count > 0)
                    findNode(n, item);
                else
                {
                    if (n.Tag != null)
                    {
                        string s = n.Tag.ToString();
                        if (s.Substring(0, s.IndexOf('=')) == item)
                        {
                            tvServers.SelectedNode = n;
                            return;
                        }
                    }
                }
            }
        }

        public frmInputLinkOPC(OpcBridgeSupport opc)
        {
            InitializeComponent();
            this.CDT = 0;
            this.opc = opc;
            string servers = opc.GetServers();
            if (!String.IsNullOrWhiteSpace(servers))
            {
                tvServers.Nodes.Clear();
                string[] list = servers.Split(new char[] { ';' });
                string root = String.Empty;
                TreeNode node = null;
                foreach (string line in list)
                {
                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        string[] items = line.Split(new char[] { '=' });
                        if (items[0] != root)
                        {
                            root = items[0];
                            node = tvServers.Nodes.Add(root);
                        }
                        if (node != null && items.Length == 2)
                            node.Nodes.Add(items[1]).Nodes.Add("stab");
                    }
                }
            }
        }

        char[] splitters = new char[] { ':', '.', '/', '\\' };

        private char[] sp(string value)
        {
            foreach (char ch in splitters)
            {
                if (value.IndexOf(ch) > 0)
                    return new char[] { ch };
            }
            return new char[] { };
        }

        private void fillNodes(TreeNode node)
        {
            if (node != null && node.Level == 1)
            {
                Cursor = Cursors.WaitCursor;
                IDictionary<string, TreeNode> keys = new Dictionary<string, TreeNode>();
                node.Nodes.Clear();
                string server = node.Text;
                string props = opc.GetProps(server);
                if (!String.IsNullOrWhiteSpace(props))
                {
                    string[] proplist = props.Split(new char[] { ';' });
                    foreach (string fullpathprop in proplist)
                    {
                        TreeNode n = node;
                        string[] pathprops = fullpathprop.Split(new char[] { '=' });
                        string pathprop = pathprops[0];
                        string cdtprop = (pathprops.Length == 2) ? pathprops[1] : "0";
                        string[] items = pathprop.Split(sp(pathprop));
                        string prop = pathprop + "=" + cdtprop;
                        //n = n.Nodes.Add(pathprop);
                        //n.Tag = prop;
                        StringBuilder key = new StringBuilder();
                        for (int i = 0; i < items.Length; i++)
                        {
                            string item = items[i];
                            if (i < items.Length - 1)
                            {
                                key.Append(item);
                                if (!keys.ContainsKey(key.ToString()))
                                {
                                    n = n.Nodes.Add(item);
                                    keys.Add(key.ToString(), n);
                                }
                                else
                                    n = keys[key.ToString()];
                            }
                            else
                            {
                                n = n.Nodes.Add(item);
                                n.Tag = prop;
                            }
                        }
                    }
                }
                Cursor = Cursors.Default;
            }
        }

        private void tvServers_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            fillNodes(e.Node);
        }

        private void tvServers_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            if (node != null && node.Level == 1)
            {
                node.Nodes.Clear();
                node.Nodes.Add("stab");
            }
        }

        private void tvServers_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            if (node != null && node.Level > 1 && node.Nodes.Count == 0)
            {
                string item = node.Tag.ToString();
                cdt = int.Parse(item.Substring(item.IndexOf('=') + 1));
               
                lbCDT.Text = Points.Plugins.PropType.TextCDT(cdt);
                tbItem.Text = item.Substring(0, item.IndexOf('='));
                while (node.Level > 1) node = node.Parent;
                tbServer.Text = node.Text;
            }
        }

        private void tbServer_TextChanged(object sender, EventArgs e)
        {
            btnEnter.Enabled = !String.IsNullOrWhiteSpace(Server) &&
                !String.IsNullOrWhiteSpace(Group) && !String.IsNullOrWhiteSpace(Item);
        }

        private void frmInputLinkOPC_Load(object sender, EventArgs e)
        {
            findServer(tbServer.Text);
            findItem(tbItem.Text);
        }

        private void frmInputLinkOPC_Shown(object sender, EventArgs e)
        {
            tvServers.Focus();
        }
    }
}
