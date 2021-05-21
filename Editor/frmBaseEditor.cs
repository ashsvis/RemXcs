using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using Points.Plugins;
using BaseServer;

namespace DataEditor
{
    public partial class frmBaseEditor : Form
    {
        OpcBridgeSupport opc;

        IDictionary<string, int> IconList = new Dictionary<string, int>();
        private string lastPtName = String.Empty;
        private TreeNode EnsureChild(TreeNode parentNode, string text)
        {
            foreach (TreeNode n in parentNode.Nodes)
            {
                if (n.Text.Equals(text)) return n;
            }
            return parentNode.Nodes.Add(text);
        }

        private TreeNode EnsureServer(IDictionary<string, TreeNode> indexServers,
            TreeNode OPC, string server)
        {
            // Определение сервера связи
            string keyServer = server;
            TreeNode parentServer;
            if (indexServers.ContainsKey(keyServer))
                return indexServers[keyServer];
            else
            {
                if (String.IsNullOrWhiteSpace(server))
                    parentServer = OPC;
                else
                {
                    parentServer = OPC.Nodes.Add(server);
                    indexServers.Add(keyServer, parentServer);
                }
                return parentServer;
            }
        }

        private TreeNode EnsureGroup(IDictionary<string, TreeNode> indexGroups,
            string server, string group, TreeNode parentServer, string text)
        {
            // Определение устройства
            string keyNode = String.Format("{0}{1}", server, group);
            TreeNode parentNode;
            if (indexGroups.ContainsKey(keyNode))
            {
                parentNode = indexGroups[keyNode];
                if (String.IsNullOrWhiteSpace(text))
                    return parentNode;
                else
                    return EnsureChild(parentNode, text);
            }
            else
            {
                if (String.IsNullOrWhiteSpace(group)) group = "Без привязки";
                parentNode = parentServer.Nodes.Add(group);
                indexGroups.Add(keyNode, parentNode);
                if (String.IsNullOrWhiteSpace(text))
                    return parentNode;
                else
                    return parentNode.Nodes.Add(text);
            }
        }

        private TreeNode EnsureChannel(IDictionary<string, TreeNode> indexChannels,
            TreeNode Station, int iChannel)
        {
            // Определение канала связи
            string keyChannel = String.Format("CN{0}", iChannel);
            TreeNode parentChannel;
            if (indexChannels.ContainsKey(keyChannel))
                return indexChannels[keyChannel];
            else
            {
                parentChannel = Station.Nodes.Add("Канал связи " + iChannel);
                indexChannels.Add(keyChannel, parentChannel);
                return parentChannel;
            }
        }

        private TreeNode EnsureNode(IDictionary<string, TreeNode> indexNodes, int iChannel,
            int iNode, TreeNode parentChannel, string text)
        {
            // Определение устройства
            string keyNode = String.Format("C{0}N{1}", iChannel, iNode);
            TreeNode parentNode;
            if (indexNodes.ContainsKey(keyNode))
            {
                parentNode = indexNodes[keyNode];
                if (String.IsNullOrWhiteSpace(text))
                    return parentNode;
                else
                    return EnsureChild(parentNode, text);
            }
            else
            {
                parentNode = parentChannel.Nodes.Add("Устройство " + iNode);
                indexNodes.Add(keyNode, parentNode);
                if (String.IsNullOrWhiteSpace(text))
                    return parentNode;
                else
                    return parentNode.Nodes.Add(text);
            }
        }

        private void SetImageIndex(ref TreeNode node, IPointPlugin plugin)
        {
            int index = IconList[plugin.PluginCategory + plugin.PluginShortType];
            node.ImageIndex = index; 
            node.SelectedImageIndex = index;
        }

        private void SetImageIndex(ref ListViewItem item, IPointPlugin plugin)
        {
            int index = IconList[plugin.PluginCategory + plugin.PluginShortType];
            item.ImageIndex = index; 
        }

        private void SetImageIndex(ref TreeNode node)
        {
            node.ImageIndex = 0;
            node.SelectedImageIndex = 0;
        }

        private List<TreeNode> fillEntityTree(object argument) // выполняется в потоке!
        {
            HashSet<string> found = new HashSet<string>();
            IDictionary<string, Entity> entities = (IDictionary<string, Entity>)argument;
            List<TreeNode> result = new List<TreeNode>();
            IDictionary<string, TreeNode> indexChannels = new Dictionary<string, TreeNode>();
            IDictionary<string, TreeNode> indexNodes = new Dictionary<string, TreeNode>();
            IDictionary<string, TreeNode> indexGroups = new Dictionary<string, TreeNode>();
            IDictionary<string, TreeNode> indexTables = new Dictionary<string, TreeNode>();
            TreeNode stationNode = new TreeNode("Рабочая станция");
            SetImageIndex(ref stationNode);
            TreeNode virtualNode = null;
            TreeNode opcNode = null;
            result.Add(stationNode);
            // Виртуальные группы и в группах
            foreach (KeyValuePair<string, Entity> kvp in entities)
            {
                string ptname = kvp.Key;
                if (!found.Contains(ptname))
                {
                    IPointPlugin plugin = kvp.Value.Plugin;
                    if (plugin.PluginType == PointPlaginType.Virtual)
                    {
                        Entity e = entities[ptname];
                        if (!e.Empty && e.Values.ContainsKey("PtKind"))
                        {
                            // Определение типа точки
                            int ptKind = int.Parse(e.Values["PtKind"].ToString());
                            if (ptKind != PtKind.Group) continue; // пропускаем только группы
                            if (virtualNode == null)
                            {
                                virtualNode = stationNode.Nodes.Add("Виртуальные");
                                SetImageIndex(ref virtualNode);
                            }
                            TreeNode group = virtualNode.Nodes.Add(ptname);
                            group.Tag = plugin;
                            SetImageIndex(ref group, plugin);
                            found.Add(ptname);
                            foreach (KeyValuePair<string, object> prop in e.Values)
                            {
                                if (prop.Key.StartsWith("Child") &&
                                    !String.IsNullOrWhiteSpace((string)prop.Value))
                                {
                                    string childname = (string)e.Values[prop.Key];
                                    TreeNode node = group.Nodes.Add(childname);
                                    Entity child = entities[childname];
                                    if (!child.Empty)
                                    {
                                        node.Tag = child.Plugin;
                                        SetImageIndex(ref node, child.Plugin);
                                    }
                                    found.Add(childname);
                                }
                            }
                        }
                    }
                }
            }
            // Виртуальные не в группах
            foreach (KeyValuePair<string, Entity> kvp in entities)
            {
                string ptname = kvp.Key;
                if (!found.Contains(ptname))
                {
                    IPointPlugin plugin = kvp.Value.Plugin;
                    if (plugin.PluginType == PointPlaginType.Virtual)
                    {
                        Entity e = entities[ptname];
                        if (!e.Empty && e.Values.ContainsKey("PtKind"))
                        {
                            // Определение типа точки
                            int ptKind = int.Parse(e.Values["PtKind"].ToString());
                            if (ptKind != PtKind.Group) // пропускаем кроме групп
                            {
                                if (virtualNode == null)
                                {
                                    virtualNode = stationNode.Nodes.Add("Виртуальные");
                                    SetImageIndex(ref virtualNode);
                                }
                                TreeNode node = virtualNode.Nodes.Add(ptname);
                                node.Tag = plugin;
                                SetImageIndex(ref node, plugin);
                                found.Add(ptname);
                            }
                        }
                    }
                }
            }
            // OPC
            foreach (KeyValuePair<string, Entity> kvp in entities)
            {
                string ptname = kvp.Key;
                if (!found.Contains(ptname))
                {
                    IPointPlugin plugin = kvp.Value.Plugin;
                    if (plugin.PluginType == PointPlaginType.OPC)
                    {
                        Entity e = entities[ptname]; 
                        if (!e.Empty && e.Values.ContainsKey("Server") &&
                            e.Values.ContainsKey("Group") && e.Values.ContainsKey("Item"))
                        {
                            if (opcNode == null) opcNode = stationNode.Nodes.Add("Серверы ОРС");
                            string server = e.Values["Server"].ToString();
                            string group = e.Values["Group"].ToString();
                            TreeNode groupNode = EnsureGroup(indexNodes, server, group,
                                EnsureServer(indexChannels, opcNode, server), String.Empty);
                            TreeNode node = groupNode.Nodes.Add(ptname);
                            node.Tag = plugin;
                            SetImageIndex(ref node, plugin);
                            found.Add(ptname);
                        }
                    }
                }
            }
            // Полевые, группы опроса
            foreach (KeyValuePair<string, Entity> kvp in entities)
            {
                string ptname = kvp.Key;
                if (!found.Contains(ptname))
                {
                    IPointPlugin plugin = kvp.Value.Plugin;
                    if (plugin.PluginType == PointPlaginType.Field)
                    {
                        Entity e = entities[ptname];
                        if (!e.Empty && e.Values.ContainsKey("Channel") &&
                            e.Values.ContainsKey("Node") && e.Values.ContainsKey("PtKind"))
                        {
                            // Определение типа точки
                            int ptKind = int.Parse(e.Values["PtKind"].ToString());
                            if (ptKind != PtKind.Group) continue;
                            // Определение канала связи
                            int iChannel = int.Parse(e.Values["Channel"].ToString()); ;
                            // Определение устройства
                            int iNode = int.Parse(e.Values["Node"].ToString());
                            TreeNode groupNode = EnsureNode(indexNodes, iChannel, iNode,
                                EnsureChannel(indexChannels, stationNode, iChannel), "Группы опроса");
                            TreeNode node = groupNode.Nodes.Add(ptname);
                            node.Tag = plugin;
                            indexGroups.Add(ptname, node);
                            SetImageIndex(ref node, plugin);
                            found.Add(ptname);
                        }
                    }
                }
            }
            // Полевые, группы архива
            foreach (KeyValuePair<string, Entity> kvp in entities)
            {
                string ptname = kvp.Key;
                if (!found.Contains(ptname))
                {
                    IPointPlugin plugin = kvp.Value.Plugin;
                    if (plugin.PluginType == PointPlaginType.Field)
                    {
                        Entity e = entities[ptname];
                        if (!e.Empty && e.Values.ContainsKey("Channel") &&
                            e.Values.ContainsKey("Node") && e.Values.ContainsKey("PtKind"))
                        {
                            // Определение типа точки
                            int ptKind = int.Parse(e.Values["PtKind"].ToString());
                            if (ptKind != PtKind.Table) continue;
                            // Определение канала связи
                            int iChannel = int.Parse(e.Values["Channel"].ToString());
                            // Определение устройства
                            int iNode = int.Parse(e.Values["Node"].ToString());
                            TreeNode groupNode = EnsureNode(indexNodes, iChannel, iNode,
                                EnsureChannel(indexChannels, stationNode, iChannel), "Группы архива");
                            TreeNode node = groupNode.Nodes.Add(ptname);
                            node.Tag = plugin;
                            //indexGroups.Add(ptname, node); // не добавлять дочерние точки
                            SetImageIndex(ref node, plugin);
                            found.Add(ptname);
                        }
                    }
                }
            }
            // Полевые, в группах
            foreach (KeyValuePair<string, TreeNode> group in indexGroups)
            {
                string ptname = group.Key;
                Entity e = entities[ptname];
                if (!e.Empty && e.Plugin.PluginType == PointPlaginType.Field)
                {
                    foreach (KeyValuePair<string, object> prop in e.Values)
                    {
                        if (prop.Key.StartsWith("Child") &&
                            !String.IsNullOrWhiteSpace((string)prop.Value))
                        {
                            string childname = (string)e.Values[prop.Key];
                            TreeNode node = group.Value.Nodes.Add(childname);
                            Entity child = entities[childname];
                            if (!child.Empty)
                            {
                                node.Tag = child.Plugin;
                                SetImageIndex(ref node, child.Plugin);
                            }
                            found.Add(childname);
                        }
                    }
                }
            }
            // Полевые, не в группах
            foreach (KeyValuePair<string, Entity> kvp in entities)
            {
                string ptname = kvp.Key;
                if (!found.Contains(ptname))
                {
                    IPointPlugin plugin = kvp.Value.Plugin;
                    if (plugin.PluginType == PointPlaginType.Field)
                    {
                        Entity e = entities[ptname]; 
                        if (!e.Empty && e.Values.ContainsKey("Channel") &&
                            e.Values.ContainsKey("Node") && e.Values.ContainsKey("PtKind"))
                        {
                            // Определение типа точки
                            int ptKind = int.Parse(e.Values["PtKind"].ToString());
                            if (ptKind == PtKind.Group || ptKind == PtKind.Table) continue;
                            // Определение канала связи
                            int iChannel = int.Parse(e.Values["Channel"].ToString());
                            // Определение устройства
                            int iNode = int.Parse(e.Values["Node"].ToString());
                            TreeNode parentNode = EnsureNode(indexNodes, iChannel, iNode,
                                EnsureChannel(indexChannels, stationNode, iChannel), "Вне групп");
                            TreeNode node = parentNode.Nodes.Add(ptname);
                            node.Tag = plugin;
                            SetImageIndex(ref node, plugin);
                            found.Add(ptname);
                        }
                    }
                }
            }
            return result;
        }

        //private int addIconImage(Color color, string text)
        //{
        //    using (Bitmap bmp = new Bitmap(16, 16))
        //    {
        //        Graphics g = Graphics.FromImage(bmp);
        //        using (SolidBrush brush = new SolidBrush(color))
        //        {
        //            g.FillRectangle(brush, new Rectangle(0, 0, 16, 16));
        //            g.DrawLines(Pens.DarkGray, new Point[] { new Point(1, 15), new Point(15, 15), new Point(15, 1) });
        //            using (Font font = new Font("Courier New", 8))
        //            {
        //                g.DrawString(text, font, Brushes.Black, new RectangleF(-1, 0, 20, 20));
        //            }
        //        }
        //        ilTree.Images.Add(bmp);
        //        return ilTree.Images.Count - 1;
        //    }
        //}

        public frmBaseEditor(Form panel, string ptname)
        {
            InitializeComponent();
            lastPtName = ptname;
            lvList.SetDoubleBuffered(true);
            lvEntity.SetDoubleBuffered(true);
            IDictionary<string, ToolStripMenuItem> categories =
                new Dictionary<string, ToolStripMenuItem>();
            IDictionary<string, ToolStripMenuItem> items =
                new Dictionary<string, ToolStripMenuItem>();
            IDictionary<string, IPointPlugin> plugins =
                PointPlugin.LoadPlugins(Application.StartupPath);
            foreach (KeyValuePair<string, IPointPlugin> plugin in plugins)
            {
                string category = plugin.Value.PluginCategory;
                using (Bitmap bmp = new Bitmap(16, 16))
                {
                    Data.DrawIconImage(Graphics.FromImage(bmp),
                        plugin.Value.GetIconColor, plugin.Value.PluginShortType);
                    ilTree.Images.Add(bmp);
                }
                int iconIndex = ilTree.Images.Count - 1;
                IconList.Add(plugin.Value.PluginCategory + plugin.Value.PluginShortType, iconIndex);
                ToolStripMenuItem cat;
                if (categories.ContainsKey(category))
                    cat = categories[category];
                else
                {
                    cat = tsmiCreate.DropDownItems.Add(category) as ToolStripMenuItem;
                    categories.Add(category, cat);
                }
                ToolStripMenuItem m = cat.DropDownItems.Add(plugin.Value.PluginDescriptor)
                                        as ToolStripMenuItem;
                m.Image = ilTree.Images[iconIndex];
                m.Tag = plugin.Value;
                m.Click += miCreateEntity_Click;

            }

            opc = new OpcBridgeSupport();
        }

        private void frmBaseEditor_Load(object sender, EventArgs e)
        {            
            this.WindowState = FormWindowState.Maximized;
        }

        private void fillEntityTreeAsync()
        {
            if (!backgroundBuildTree.IsBusy)
            {
                tvEntity.UseWaitCursor = true;
                Data.LoadBase(PointPlugin.LoadPlugins(Application.StartupPath));
                backgroundBuildTree.RunWorkerAsync(Data.Entities());
            }
        }

        private void backgroundBuildTree_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            e.Result = fillEntityTree(e.Argument);
        }

        private void findNode(string item, TreeNode node = null)
        {
            TreeNodeCollection nodes = (node != null) ? node.Nodes : tvEntity.Nodes; 
            foreach (TreeNode n in nodes)
            {
                if (n.Nodes.Count > 0)
                {
                    if (n.Text == item)
                    {
                        tvEntity.SelectedNode = n;
                        return;
                    }
                    else
                        findNode(item, n);
                }
                else
                {
                    if (n.Text == item)
                    {
                        tvEntity.SelectedNode = n;
                        return;
                    }
                }
            }
        }

        public void RestoreTreePos(string name)
        {
            findNode(name);
        }

        private void backgroundBuildTree_RunWorkerCompleted(object sender,
            System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            List<TreeNode> nodes = (List<TreeNode>)e.Result;
            tvEntity.Nodes.Clear();
            tvEntity.Nodes.AddRange(nodes.ToArray());
            tvEntity.Sort();
            tvEntity.UseWaitCursor = false;
            Cursor = Cursors.Default;
            RestoreTreePos(lastPtName);
            if (tvEntity.SelectedNode == null) findNode("Рабочая станция");
        }

        private void frmBaseEditor_Shown(object sender, EventArgs e)
        {
            fillEntityTreeAsync();
        }

        private void SendToChangeLog(Entity pt, string param, string oldvalue, string newvalue)
        {
            Data.SendToChangeLog(Properties.Settings.Default.Station,
                pt.Values["PtName"].ToString(), param, oldvalue, newvalue,
                Properties.Settings.Default.CurrentUser, pt.Values["PtDesc"].ToString());
        }

        private void miCreateEntity_Click(object sender, EventArgs e)
        {
            using (frmInputPtName form = new frmInputPtName())
            {
                ToolStripMenuItem m = (ToolStripMenuItem)sender;
                IPointPlugin plugin = (IPointPlugin)m.Tag;
                form.EnteredType = plugin;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string PtName = form.EnteredValue;
                    if (Data.AddEntity(PtName, plugin))
                    {
                        Entity point = Data.GetEntity(PtName);
                        if (!point.Empty && Data.CreateEntity(PtName, point))
                        {
                            if (tvEntity.Nodes.Count > 0)
                            {
                                Entity pt = Data.GetEntity(PtName);
                                if (!pt.Empty)
                                {
                                    SendToChangeLog(pt, "Создана", String.Empty, PtName);
                                    TreeNode node = tvEntity.Nodes.Add(PtName);
                                    node.Tag = pt.Plugin;
                                    tvEntity.SelectedNode = node;
                                    node.EnsureVisible();
                                    lastPtName = PtName;
                                    fillEntityTreeAsync();
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(this,
                            String.Format("Идентификатор точки \"{0}\" дублируется!", PtName),
                            "Создание новой точки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        RestoreTreePos(PtName);
                    }
                }
            }
        }

        private void tvEntity_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                tvEntity.SelectedNode = tvEntity.GetNodeAt(e.X, e.Y);
                if (tvEntity.SelectedNode != null)
                {
                    miRename.Visible = miDouble.Visible =
                        miDelete.Visible = (tvEntity.SelectedNode.Tag is IPointPlugin);
                    Point p = tvEntity.PointToScreen(new Point(e.X, e.Y));
                    cmsEntity.Show(p.X, p.Y);
                }
            }
        }

        private void miDeleteEntity_Click(object sender, EventArgs e)
        {
            TreeNode node = tvEntity.SelectedNode;
            if (node != null && node.Tag != null)
            {
                if (MessageBox.Show(this, String.Format("Удалить точку \"{0}\" из списка?", node.Text),
                    "Удаление точки", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) ==
                    DialogResult.OK)
                {
                    string PtName = node.Text;
                    Entity pt = Data.GetEntity(PtName);
                    if (!pt.Empty) SendToChangeLog(pt, "Удалена", PtName, String.Empty);
                    if (Data.DeleteEntity(PtName))
                    {
                        tvEntity.SelectedNode = node.Parent;
                        tvEntity.Nodes.Remove(node);
                        lvEntity.Items.Clear();
                    }
                }
            }
        }

        private void miRenameEntity_Click(object sender, EventArgs e)
        {
            TreeNode node = tvEntity.SelectedNode;
            if (node != null && node.Tag != null)
            {
                using (frmInputPtName form = new frmInputPtName())
                {
                    string oldName = node.Text;
                    form.EnteredValue = oldName;
                    IPointPlugin plugin = node.Tag as IPointPlugin;
                    if (plugin != null)
                    {
                        form.EnteredType = plugin;
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            string newName = form.EnteredValue;
                            Entity pt = Data.GetEntity(oldName);
                            if (!pt.Empty) SendToChangeLog(pt, "Переименована", oldName, newName);
                            if (Data.RenameEntity(oldName, newName))
                            {
                                node.Text = newName;
                                tvEntity.SelectedNode = null;
                                RestoreTreePos(newName);
                                filllvEntity(newName);
                            }
                            else
                            {
                                MessageBox.Show(this,
                                    String.Format("Идентификатор точки \"{0}\" дублируется!", newName),
                                    "Переименование точки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void miDoubleEntity_Click(object sender, EventArgs e)
        {
            TreeNode node = tvEntity.SelectedNode;
            if (node != null && node.Tag != null)
            {
                using (frmInputPtName form = new frmInputPtName())
                {
                    string oldName = node.Text;
                    form.EnteredValue = oldName;
                    IPointPlugin plugin = node.Tag as IPointPlugin;
                    if (plugin != null)
                    {
                        form.EnteredType = plugin;
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            string newName = form.EnteredValue;
                            if (Data.DoubleEntity(oldName, newName))
                            {
                                Entity pt = Data.GetEntity(newName);
                                if (!pt.Empty) SendToChangeLog(pt, "Дублирована", oldName, newName);
                                TreeNode newNode = node.Parent.Nodes.Add(newName);
                                newNode.Tag = plugin;
                                tvEntity.SelectedNode = newNode;
                                lastPtName = newName;
                                fillEntityTreeAsync();
                            }
                            else
                            {
                                MessageBox.Show(this,
                                    String.Format("Идентификатор точки \"{0}\" дублируется!", newName),
                                    "Дублирование точки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void filllvEntity(string PtName)
        {
            Entity entity = Data.GetEntity(PtName);
            if (entity.Empty) return;
            IPointPlugin plugin = entity.Plugin;
            lvEntity.Items.Clear();
            int i = 0;
            foreach (FineRow prop in
                            plugin.GetFinePropList(PtName, entity.Values, getChildDesc))
            {
                ListViewItem item = lvEntity.Items.Add(prop.FineKey);
                if (i == 0) SetImageIndex(ref item, plugin);
                item.SubItems.Add(prop.FineValue);
                item.SubItems.Add(prop.Key);
                item.SubItems.Add(prop.Value.ToString());
                item.Tag = prop.PropType;
                if (i > 0 &&
                    (prop.PropType == PropType.Link || prop.PropType == PropType.LinkEx ||
                     prop.PropType == PropType.LinkOPC) && prop.Value.ToString().Length > 0)
                {
                    Entity child = Data.GetEntity(prop.Value.ToString());
                    if (!child.Empty)
                        SetImageIndex(ref item, child.Plugin);
                }
                i++;
            }
        }

        Stack<string> backwardlist = new Stack<string>(1000);
        Stack<string> forwardlist = new Stack<string>(1000);

        private void tvEntity_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.lvList.ListViewItemSorter = null;
            TreeNode node = tvEntity.SelectedNode;
            if (node != null && node.Tag is IPointPlugin)
            { // заполнение свойств точки
                lastPtName = node.Text;
                filllvEntity(lastPtName);
                listContainer.Panel1Collapsed = true;
                listContainer.Panel2Collapsed = false;
                resizeColumns(lvEntity, 1);
            }
            else
                { // заполнение списков
                    if (node != null)
                    {
                        Cursor = Cursors.WaitCursor;
                        lvList.BeginUpdate();
                        lvList.Items.Clear();
                        try
                        {
                            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
                            {
                                if (mySQL.Connected)
                                {
                                    goNode(node, mySQL);
                                }
                            }
                        }
                        finally
                        { 
                            lvList.EndUpdate();
                            Cursor = Cursors.Default;
                        }
                    }
                    listContainer.Panel2Collapsed = true;
                    listContainer.Panel1Collapsed = false;
                    resizeColumns(lvList, 1);
                }
        }

        private void goNode(TreeNode node, ServerSQL mySQL)
        {
            TreeNodeCollection nodes = (node != null) ? node.Nodes : tvEntity.Nodes;
            foreach (TreeNode n in nodes)
            {
                if (n.Tag is IPointPlugin)
                {
                    string ptname = n.Text;
                    Entity ent = Data.GetEntity(ptname, mySQL);
                    if (!ent.Empty)
                    {
                        ListViewItem item = lvList.Items.Add(ptname);
                        SetImageIndex(ref item, (IPointPlugin)n.Tag);
                        item.SubItems.Add((string)ent.Values["PtDesc"]);
                        item.SubItems.Add(ent.Plugin.GetFineValue("Actived", ent.Values["Actived"]));
                        if (ent.Values.ContainsKey("Logged"))
                            item.SubItems.Add(ent.Plugin.GetFineValue("Logged", ent.Values["Logged"]));
                        else
                            item.SubItems.Add(String.Empty);
                        if (ent.Values.ContainsKey("Asked"))
                            item.SubItems.Add(ent.Plugin.GetFineValue("Asked", ent.Values["Asked"]));
                        else
                            item.SubItems.Add(String.Empty);
                        if (ent.Values.ContainsKey("FetchTime"))
                            item.SubItems.Add(ent.Plugin.GetFineValue("FetchTime",
                                ent.Values["FetchTime"]) + " сек");
                        else
                            item.SubItems.Add(String.Empty);
                    }
                }
                if (n.Nodes.Count > 0) goNode(n, mySQL);
            }
        }

        private string getChildDesc(string name)
        {
            Entity e = Data.GetEntity(name);
            if (e.Empty)
                return "Нет такой точки";
            else
                return e.Values["PtDesc"].ToString();
        }

        private void tsmiSaveAs_Click(object sender, EventArgs e)
        {
            exportFileDialog.InitialDirectory = Application.StartupPath;
            exportFileDialog.FileName = "exportbase.ini";
            if (exportFileDialog.ShowDialog() == DialogResult.OK)
                Data.ExportBaseAs(exportFileDialog.FileName, Stub);
        }

        private void Stub(string mess) { }

        private void tsmiRestoreFrom_Click(object sender, EventArgs e)
        {
            importFileDialog.InitialDirectory = Application.StartupPath;
            if (importFileDialog.ShowDialog() == DialogResult.OK)
            {
                IDictionary<string, IPointPlugin> plugins =
                    PointPlugin.LoadPlugins(Application.StartupPath);
                Data.ImportBaseFrom(plugins, importFileDialog.FileName, Stub);
                fillEntityTreeAsync();
            }
        }

        private void tsmiClearAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Вы действительно хотите удалить все данные точек?",
                "Очистка базы данных", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) ==
                DialogResult.OK)
            {
                Data.EmptyPoints();
                lvEntity.Items.Clear();
                fillEntityTreeAsync();
            }
        }

        private List<ToolStripItem> enumItems = new List<ToolStripItem>();
        private void lvEntity_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ListViewItem item = lvEntity.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    miBoolNo.Tag = false; miBoolYes.Tag = true;
                    miBoolNo.Visible = miBoolYes.Visible = miChangeString.Visible =
                    miChangeInteger.Visible = miChangeFloat.Visible =
                    miChangeLink.Visible = miDeleteLink.Visible =
                    miChangeLinkOPC.Visible = miDeleteLinkOPC.Visible = false;
                    foreach (ToolStripItem m in enumItems) cmsProps.Items.Remove(m);
                    enumItems.Clear();
                    int propType = (int)item.Tag;
                    switch (propType)
                    {
                        case PropType.Boolean:
                            miBoolNo.Visible = miBoolYes.Visible = true;
                            bool bValue = bool.Parse(item.SubItems[3].Text);
                            miBoolNo.Checked = !bValue;
                            miBoolYes.Checked = bValue;
                            break;
                        case PropType.String:
                            if (item.SubItems[2].Text.Equals("PtName"))
                            {
                                Point pt = lvEntity.PointToScreen(new Point(e.X, e.Y));
                                cmsEntity.Show(pt.X, pt.Y);
                            }
                            else
                            {
                                miChangeString.Visible = true;
                                string sValue = item.SubItems[3].Text;
                                miChangeString.Tag = sValue;
                            }
                            break;
                        case PropType.Integer:
                            miChangeInteger.Visible = true;
                            int iValue = int.Parse(item.SubItems[3].Text);
                            miChangeInteger.Tag = iValue;
                            break;
                        case PropType.Float:
                            miChangeFloat.Visible = true;
                            decimal fValue = Data.FloatParse(item.SubItems[3].Text);
                            miChangeFloat.Tag = fValue;
                            break;
                        case PropType.FloatEx:
                            miChangeFloat.Visible = true;
                            string[] vals = item.SubItems[3].Text.Split(new char[] { ';' });
                            string exValue = item.SubItems[3].Text;
                            miChangeFloat.Tag = exValue;
                            Entity point = Data.GetEntity(lastPtName);
                            string propName = item.SubItems[2].Text;
                            object propValue = (vals.Length == 2) ? int.Parse(vals[1]) : 0;
                            if (!point.Empty)
                            {
                                ToolStripItem tsp = cmsProps.Items.Add("-");
                                int i = 0;
                                foreach (string ename in point.Plugin.GetEnumItems(propName))
                                {
                                    ToolStripMenuItem tsi =
                                        (ToolStripMenuItem)cmsProps.Items.Add(ename);
                                    if (i == (int)propValue) tsi.Checked = true;
                                    tsi.Tag = vals[0] + ";" + i;
                                    tsi.Click += miEnumerate_Click;
                                    enumItems.Add(tsi);
                                    i++;
                                }
                                enumItems.Add(tsp); // в последнюю очередь
                            }
                            break;
                        case PropType.Enumeration:
                            point = Data.GetEntity(lastPtName);
                            propName = item.SubItems[2].Text;
                            propValue = int.Parse(item.SubItems[3].Text);
                            if (!point.Empty)
                            {
                                int i = 0;
                                foreach (string ename in point.Plugin.GetEnumItems(propName))
                                {
                                    ToolStripMenuItem tsi =
                                        (ToolStripMenuItem)cmsProps.Items.Add(ename);
                                    if (i == (int)propValue) tsi.Checked = true;
                                    tsi.Tag = i;
                                    tsi.Click += miEnumerate_Click;
                                    enumItems.Add(tsi);
                                    i++;
                                }
                            }
                            break;
                        case PropType.Link:
                            propName = item.SubItems[2].Text;
                            if (propName != "Source")
                            {
                                miChangeLink.Visible = true;
                                propValue = item.SubItems[3].Text;
                                miChangeLink.Tag = propValue.ToString();
                                if (!String.IsNullOrWhiteSpace(propValue.ToString()))
                                {
                                    miDeleteLink.Visible = true;
                                    miDeleteLink.Tag = propValue.ToString();
                                }
                            }
                            break;
                        case PropType.LinkEx:
                            miChangeLink.Visible = true;
                            propValue = item.SubItems[3].Text;
                            miChangeLink.Tag = propValue.ToString();
                            if (!String.IsNullOrWhiteSpace(propValue.ToString()))
                            {
                                miDeleteLink.Visible = true;
                                miDeleteLink.Tag = propValue.ToString();
                                vals = propValue.ToString().Split(new char[] { ';' });
                                int enumValue = (vals.Length == 2) ? int.Parse(vals[1]) : 0;
                                point = Data.GetEntity(lastPtName);

                                propName = item.SubItems[2].Text;
                                if (!point.Empty)
                                {
                                    ToolStripItem tsp = cmsProps.Items.Add("-");
                                    int i = 0;
                                    foreach (string ename in point.Plugin.GetEnumItems(propName))
                                    {
                                        ToolStripMenuItem tsi =
                                            (ToolStripMenuItem)cmsProps.Items.Add(ename);
                                        if (i == enumValue) tsi.Checked = true;
                                        tsi.Tag = vals[0] + ";" + i;
                                        tsi.Click += miEnumerate_Click;
                                        enumItems.Add(tsi);
                                        i++;
                                    }
                                    enumItems.Add(tsp); // в последнюю очередь
                                }
                            }
                            break;
                        case PropType.LinkOPC:
                            miChangeLinkOPC.Visible = true;
                            propValue = item.SubItems[3].Text;
                            miChangeLinkOPC.Tag = propValue.ToString();
                            if (!String.IsNullOrWhiteSpace(propValue.ToString()))
                            {
                                miDeleteLinkOPC.Visible = true;
                                miDeleteLinkOPC.Tag = propValue.ToString();
                            }
                            break;
                        case PropType.TypeOPC:
                            miChangeLinkOPC.Visible = true;
                            propValue = item.SubItems[3].Text;
                            miChangeLinkOPC.Tag = propValue.ToString();
                            if (int.Parse(propValue.ToString()) > 0)
                            {
                                miDeleteLinkOPC.Visible = true;
                                miDeleteLinkOPC.Tag = propValue.ToString();
                            }
                            break;
                    }
                    Point p = lvEntity.PointToScreen(new Point(e.X, e.Y));
                    cmsProps.Show(p.X, p.Y);
                }
            }
        }

        private void UpdateChangeLog(Entity point, string propName, object propValue)
        {
            string propFine = point.Plugin.GetPropDesc(propName);
            string oldvalue = point.Plugin.GetFineValue(propName, point.Values[propName]);
            string newvalue = point.Plugin.GetFineValue(propName, propValue);
            SendToChangeLog(point, propFine, oldvalue, newvalue);
        }

        private void miBoolYesNo_Click(object sender, EventArgs e)
        {
            Entity point = Data.GetEntity(lastPtName);
            ListViewItem item = lvEntity.FocusedItem;
            if (!point.Empty && item != null)
            {
                string propName = item.SubItems[2].Text;
                bool propValue = (bool)((ToolStripMenuItem)sender).Tag;
                UpdateChangeLog(point, propName, propValue);
                point.Values[propName] = propValue;
                Data.WriteEntityProp(point, propName, propValue.ToString());
                UpdatePropsList();
            }
        }

        private void miEnumerate_Click(object sender, EventArgs e)
        {
            Entity point = Data.GetEntity(lastPtName);
            ListViewItem item = lvEntity.FocusedItem;
            if (!point.Empty && item != null)
            {
                string propName = item.SubItems[2].Text;
                object tagVal = ((ToolStripMenuItem)sender).Tag;
                try
                {   // простое перечисление, значение int типа
                    int propValue = (int)tagVal;
                    UpdateChangeLog(point, propName, propValue);
                    point.Values[propName] = propValue;
                    Data.WriteEntityProp(point, propName, propValue.ToString());
                    UpdatePropsList();
                }
                catch
                {   // перечисление из типа FloatEx, форма: (decimal;int)
                    int enumVal = 0;
                    decimal propValue = 0m;
                    string[] vals = tagVal.ToString().Split(new char[] { ';' });
                    if (vals.Length == 2) enumVal = int.Parse(vals[1]);
                    bool floaxEx = false;
                    try
                    {
                        Decimal.Parse(vals[0]);
                        propValue = Data.FloatParse(vals[0]);
                        floaxEx = true;
                    }
                    catch
                    {   // Перечисление из типа LinkEx, форма: (string;int)
                        floaxEx = false;
                    }
                    if (floaxEx)
                    {   // Обрабатываем перечисление из типа FloatEx
                        string sfloatEx = propValue + ";" + enumVal;
                        UpdateChangeLog(point, propName, sfloatEx);
                        point.Values[propName] = sfloatEx;
                        Data.WriteEntityProp(point, propName, sfloatEx);
                        UpdatePropsList();
                    }
                    else
                    {   // Обрабатываем перечисление из типа LinkEx 
                        string slinkEx = vals[0] + ";" + enumVal;
                        UpdateChangeLog(point, propName, slinkEx);
                        point.Values[propName] = slinkEx;
                        Data.WriteEntityProp(point, propName, slinkEx);
                        UpdatePropsList();
                    }
                }
            }
        }

        private void miChangeString_Click(object sender, EventArgs e)
        {
            ListViewItem item = lvEntity.FocusedItem;
            Entity point = Data.GetEntity(lastPtName);
            if (!point.Empty && item != null)
            {
                string propName = item.SubItems[2].Text;
                string propValue = (string)((ToolStripMenuItem)sender).Tag;
                using (frmInputString form = new frmInputString())
                {
                    form.Value = propValue;
                    form.Text = item.Text;
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        propValue = form.Value;
                        UpdateChangeLog(point, propName, propValue);
                        point.Values[propName] = propValue;
                        Data.WriteEntityProp(point, propName, propValue.ToString());
                        UpdatePropsList();
                    }
                }
            }
        }

        private void miChangeInteger_Click(object sender, EventArgs e)
        {
            Entity point = Data.GetEntity(lastPtName);
            ListViewItem item = lvEntity.FocusedItem;
            if (!point.Empty && item != null)
            {
                string propName = item.SubItems[2].Text;
                int propValue = (int)((ToolStripMenuItem)sender).Tag;
                using (frmInputNumber form = new frmInputNumber())
                {
                    int[] Ranges = point.Plugin.GetIntPropRanges(propName);
                    form.SetIntValue(propValue, Ranges[0], Ranges[1]);
                    form.Text = item.Text;
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        propValue = form.IntValue;
                        UpdateChangeLog(point, propName, propValue);
                        point.Values[propName] = propValue;
                        Data.WriteEntityProp(point, propName, propValue.ToString());
                        if (propName == "Channel" || propName == "Node")
                            fillEntityTreeAsync();
                        else
                            UpdatePropsList();
                    }
                }
            }
        }

        private void miChangeFloat_Click(object sender, EventArgs e)
        {
            Entity point = Data.GetEntity(lastPtName);
            ListViewItem item = lvEntity.FocusedItem;
            if (!point.Empty && item != null)
            {
                string propName = item.SubItems[2].Text;
                object tagVal = ((ToolStripMenuItem)sender).Tag;
                int enumVal = 0;
                decimal propValue;
                bool floatEx = false;
                try { propValue = (decimal)tagVal; }
                catch
                {
                    string[] vals = tagVal.ToString().Split(new char[] { ';' });
                    propValue = Data.FloatParse(vals[0]);
                    if (vals.Length == 2) enumVal = int.Parse(vals[1]);
                    floatEx = true;
                }
                using (frmInputNumber form = new frmInputNumber())
                {
                    int ptformat = (point.Values.ContainsKey("FormatPV")) ?
                        int.Parse(point.Values["FormatPV"].ToString()) : 3;
                    if (propName.Equals("Koeff") || propName.Equals("Offset"))
                        ptformat = 6;
                    form.SetNumberValue(propValue, -99999999999m, 100000000000m, ptformat);
                    form.Text = item.Text;
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        propValue = form.NumberValue;
                        if (floatEx)
                        {
                            string sfloatEx = propValue.ToString() + ";" + enumVal.ToString();
                            UpdateChangeLog(point, propName, sfloatEx);
                            point.Values[propName] = sfloatEx;
                            Data.WriteEntityProp(point, propName, sfloatEx);
                            UpdatePropsList();
                        }
                        else
                        {
                            UpdateChangeLog(point, propName, propValue);
                            point.Values[propName] = propValue;
                            Data.WriteEntityProp(point, propName, propValue.ToString());
                            UpdatePropsList();
                        }
                    }
                }
            }
        }

        string lastname = String.Empty;
        private void miChangeLink_Click(object sender, EventArgs e)
        {
            ListViewItem item = lvEntity.FocusedItem;
            Entity point = Data.GetEntity(lastPtName);
            string ptname = String.Empty;
            if (!point.Empty && item != null)
            {
                string propName = item.SubItems[2].Text;
                object tagVal = ((ToolStripMenuItem)sender).Tag;
                string[] vals = tagVal.ToString().Split(new char[] { ';' });
                ptname = (vals[0].Length > 0) ? vals[0] : lastname;
                using (frmInputLink form = new frmInputLink(ptname, point, ilTree, IconList))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        string value = form.GetValue;
                        lastname = value;
                        if ((int)point.Values["PtKind"] == PtKind.Table && vals.Length == 2)
                        { // значение от LinkEx
                            value += ";" + vals[1];
                            UpdateChangeLog(point, propName, value);
                            point.Values[propName] = value;
                            Data.WriteEntityProp(point, propName, value);
                        }
                        else
                        { // значение от Link
                            UpdateChangeLog(point, propName, value);
                            point.Values[propName] = value;
                            Data.WriteEntityProp(point, propName, value);
                            CorrectDataCount(ref point);
                        }
                        fillEntityTreeAsync();
                        //UpdatePropsList();
                    }
                }
            }
        }

        private void CorrectDataCount(ref Entity point)
        {
            int datacount = 0;
            for (int i = 32; i >= 1; i--)
            {
                string key = "Child" + i;
                if (point.Values.ContainsKey(key) &&
                    point.Values[key].ToString() != String.Empty)
                {
                    datacount = i;
                    break;
                }
            }
            point.Values["DataCount"] = datacount;
            Data.WriteEntityProp(point, "DataCount", datacount.ToString());
        }

        private void UpdateGroupItemsList(int index)
        {
            if (index < lvEntity.Items.Count)
            {
                lvEntity.FocusedItem = lvEntity.Items[index];
                if (lvEntity.FocusedItem != null)
                {
                    lvEntity.FocusedItem.Selected = true;
                    lvEntity.EnsureVisible(index);
                }
            }
        }
        private void UpdatePropsList()
        {
            ListViewItem item = lvEntity.FocusedItem;
            if (item != null)
            {
                int index = item.Index;
                filllvEntity(lastPtName);
                if (index < lvEntity.Items.Count)
                {
                    lvEntity.FocusedItem = lvEntity.Items[index];
                    if (lvEntity.FocusedItem != null)
                    {
                        lvEntity.FocusedItem.Selected = true;
                        lvEntity.EnsureVisible(index);
                    }
                }
            }
        }
 
        private void miDeleteLink_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Удалить связь?",
                "Удаление связи", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) ==
                DialogResult.OK)
            {
                ListViewItem item = lvEntity.FocusedItem;
                Entity point = Data.GetEntity(lastPtName);
                if (!point.Empty && item != null)
                {
                    string propName = item.SubItems[2].Text;
                    UpdateChangeLog(point, propName, String.Empty);
                    point.Values[propName] = String.Empty;
                    Data.WriteEntityProp(point, propName, String.Empty);
                    CorrectDataCount(ref point);
                    //UpdatePropsList();
                    fillEntityTreeAsync();
                }
            }
        }

        frmInputLinkOPC formLinkOPC = null;
        private void miChangeLinkOPC_Click(object sender, EventArgs e)
        {
            Entity point = Data.GetEntity(lastPtName);
            ListViewItem item = lvEntity.FocusedItem;
            if (!point.Empty && item != null)
            {
                if (formLinkOPC == null)
                {
                    Cursor = Cursors.WaitCursor;
                    formLinkOPC = new frmInputLinkOPC(opc);
                    Cursor = Cursors.Default;
                }
                formLinkOPC.Server = point.Values["Server"].ToString();
                formLinkOPC.Group = point.Values["Group"].ToString();
                formLinkOPC.Item = point.Values["Item"].ToString();
                formLinkOPC.CDT = int.Parse(point.Values["CDT"].ToString());
                if (formLinkOPC.ShowDialog() == DialogResult.OK)
                {
                    string server = formLinkOPC.Server;
                    if (!point.Values["Server"].ToString().Equals(server))
                        UpdateChangeLog(point, "Server", server);
                    point.Values["Server"] = server;
                    Data.WriteEntityProp(point, "Server", server);
                    string group = formLinkOPC.Group;
                    if (!point.Values["Group"].ToString().Equals(group))
                        UpdateChangeLog(point, "Group", group);
                    point.Values["Group"] = group;
                    Data.WriteEntityProp(point, "Group", group);
                    string param = formLinkOPC.Item;
                    if (!point.Values["Item"].ToString().Equals(param))
                        UpdateChangeLog(point, "Item", param);
                    point.Values["Item"] = param;
                    Data.WriteEntityProp(point, "Item", param);
                    int cdt = formLinkOPC.CDT;
                    if (!point.Values["CDT"].ToString().Equals(param))
                        UpdateChangeLog(point, "CDT", cdt);
                    point.Values["CDT"] = cdt;
                    Data.WriteEntityProp(point, "CDT", cdt.ToString());
                    fillEntityTreeAsync();
                }
            }
        }
        private void miDeleteLinkOPC_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Удалить привязку?",
                "Удаление привязки", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) ==
                DialogResult.OK)
            {
                Entity point = Data.GetEntity(lastPtName);
                ListViewItem item = lvEntity.FocusedItem;
                if (!point.Empty && item != null)
                {
                    UpdateChangeLog(point, "Server", String.Empty);
                    point.Values["Server"] = String.Empty;
                    Data.WriteEntityProp(point, "Server", String.Empty);
                    UpdateChangeLog(point, "Group", String.Empty);
                    point.Values["Group"] = String.Empty;
                    Data.WriteEntityProp(point, "Group", String.Empty);
                    UpdateChangeLog(point, "Item", String.Empty);
                    point.Values["Item"] = String.Empty;
                    Data.WriteEntityProp(point, "Item", String.Empty);
                    UpdateChangeLog(point, "CDT", 0);
                    point.Values["CDT"] = 0;
                    Data.WriteEntityProp(point, "CDT", "0");
                    fillEntityTreeAsync();
                }
            }
        }

        private void tsmgClearAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this,
                "Вы действительно хотите удалить все данные групп параметров?",
                "Очистка групп параметров", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) ==
                DialogResult.OK)
            {
                Data.EmptyGroups(ParamGroup.Trend);
                lvEntity.Items.Clear();
                fillEntityTreeAsync();
            }
        }

        private void resizeColumns(ListView list, int index)
        {
            int width = list.Size.Width;
            int sum = 0;
            foreach (ColumnHeader column in list.Columns)
                if (column.Index != index) sum += column.Width;
            list.Columns[index].Width = width - sum -SystemInformation.VerticalScrollBarWidth - 5;
        }

        private void lvList_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem item = lvList.FocusedItem;
            if (item != null) findNode(item.Text);
        }

        private void miOneSec_Click(object sender, EventArgs e)
        {
            // изменить время опроса для выделенных позиций
            ToolStripItem tsi = (ToolStripItem)sender;
            int sec = int.Parse(tsi.Tag.ToString());
            for (int i = 0; i < lvList.Items.Count; i++) if (lvList.Items[i].Selected)
            {
                Entity point = Data.GetEntity(lvList.Items[i].Text);
                if (!point.Empty)
                {
                    string propName = "FetchTime";
                    if (point.Values.ContainsKey(propName))
                    {
                        int propValue = sec;
                        UpdateChangeLog(point, propName, propValue);
                        point.Values[propName] = propValue;
                        Data.WriteEntityProp(point, propName, propValue.ToString());
                        lvList.Items[i].SubItems[5].Text = point.Plugin.GetFineValue(propName,
                            point.Values[propName]) + " сек";
                    }
                }
            }
        }

        private void lvList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ListViewItem item = lvList.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    Point p = lvList.PointToScreen(new Point(e.X, e.Y));
                    cmsList.Show(p.X, p.Y);
                }
            }
        }

        private void miFetchOff_Click(object sender, EventArgs e)
        {
            // изменить флаг опроса для выделенных позиций
            ToolStripItem tsi = (ToolStripItem)sender;
            bool fetch = bool.Parse(tsi.Tag.ToString());
            for (int i = 0; i < lvList.Items.Count; i++) if (lvList.Items[i].Selected)
                {
                    Entity point = Data.GetEntity(lvList.Items[i].Text);
                    if (!point.Empty)
                    {
                        string propName = "Actived";
                        if (point.Values.ContainsKey(propName))
                        {
                            bool propValue = fetch;
                            UpdateChangeLog(point, propName, propValue);
                            point.Values[propName] = propValue;
                            Data.WriteEntityProp(point, propName, propValue.ToString());
                            lvList.Items[i].SubItems[2].Text = point.Plugin.GetFineValue(propName,
                                point.Values[propName]);
                        }
                    }
                }
        }

        private void miLoggedOff_Click(object sender, EventArgs e)
        {
            // изменить флаг сигнализации для выделенных позиций
            ToolStripItem tsi = (ToolStripItem)sender;
            bool logged = bool.Parse(tsi.Tag.ToString());
            for (int i = 0; i < lvList.Items.Count; i++) if (lvList.Items[i].Selected)
                {
                    Entity point = Data.GetEntity(lvList.Items[i].Text);
                    if (!point.Empty)
                    {
                        string propName = "Logged";
                        if (point.Values.ContainsKey(propName))
                        {
                            bool propValue = logged;
                            UpdateChangeLog(point, propName, propValue);
                            point.Values[propName] = propValue;
                            Data.WriteEntityProp(point, propName, propValue.ToString());
                            lvList.Items[i].SubItems[3].Text = point.Plugin.GetFineValue(propName,
                                point.Values[propName]);
                        }
                    }
                }
        }

        private void miAskedOff_Click(object sender, EventArgs e)
        {
            // изменить флаг сигнализации для выделенных позиций
            ToolStripItem tsi = (ToolStripItem)sender;
            bool asked = bool.Parse(tsi.Tag.ToString());
            for (int i = 0; i < lvList.Items.Count; i++) if (lvList.Items[i].Selected)
                {
                    Entity point = Data.GetEntity(lvList.Items[i].Text);
                    if (!point.Empty)
                    {
                        string propName = "Asked";
                        if (point.Values.ContainsKey(propName))
                        {
                            bool propValue = asked;
                            UpdateChangeLog(point, propName, propValue);
                            point.Values[propName] = propValue;
                            Data.WriteEntityProp(point, propName, propValue.ToString());
                            lvList.Items[i].SubItems[4].Text = point.Plugin.GetFineValue(propName,
                                point.Values[propName]);
                        }
                    }
                }
        }

        private int lastColumn = -1;
        private void lvList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (lastColumn != e.Column)
            {
                this.lvList.ListViewItemSorter = new BaseServer.ListViewItemComparer(e.Column);
                lastColumn = e.Column;
            }
            else
            {
                if (this.lvList.ListViewItemSorter is BaseServer.ListViewItemComparer)
                    this.lvList.ListViewItemSorter = new BaseServer.ListViewItemReverseComparer(e.Column);
                else
                    this.lvList.ListViewItemSorter = new BaseServer.ListViewItemComparer(e.Column);
            }
            if (this.lvList.FocusedItem != null)
                this.lvList.FocusedItem.EnsureVisible();
        }

        private void tvEntity_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node != null && e.Node.ImageIndex < 2)
            {
                e.Node.ImageIndex = 1;
                e.Node.SelectedImageIndex = 1;
            }
        }

        private void tvEntity_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node != null && e.Node.ImageIndex < 2)
            {
                e.Node.ImageIndex = 0;
                e.Node.SelectedImageIndex = 0;
            }
        }

        private void tsbbackward_Click(object sender, EventArgs e)
        {
            if (backwardlist.Count > 0)
            {
                if (tvEntity.SelectedNode != null)
                {
                    forwardlist.Push(tvEntity.SelectedNode.Text);
                    tsbforward.Enabled = true;
                }
                string ptname = backwardlist.Pop();
                this.Text = ptname;
                tsbbackward.Enabled = backwardlist.Count != 0;
                tvEntity.BeforeSelect -= tvEntity_BeforeSelect;
                RestoreTreePos(ptname);
                tvEntity.BeforeSelect += tvEntity_BeforeSelect;
            }
        }

        private void tsbforward_Click(object sender, EventArgs e)
        {
            if (forwardlist.Count > 0)
            {
                if (tvEntity.SelectedNode != null)
                {
                    backwardlist.Push(tvEntity.SelectedNode.Text);
                    tsbbackward.Enabled = true;
                }
                string ptname = forwardlist.Pop();
                this.Text = ptname;
                tsbforward.Enabled = forwardlist.Count != 0;
                tvEntity.BeforeSelect -= tvEntity_BeforeSelect;
                RestoreTreePos(ptname);
                tvEntity.BeforeSelect += tvEntity_BeforeSelect;
            }
        }

        private void tvEntity_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (tvEntity.SelectedNode != null &&
                tvEntity.SelectedNode.Tag is IPointPlugin)
            {
                backwardlist.Push(tvEntity.SelectedNode.Text);
                tsbbackward.Enabled = true;
                forwardlist.Clear();
                tsbforward.Enabled = false;
            }
        }
    }
}
