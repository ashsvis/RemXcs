using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using Points.Plugins;
using BaseServer;

namespace DataEditor
{
    public partial class frmTableGroupEditor : Form
    {
        private string lastGroupNo = String.Empty;
        private ParamGroup ParamGroupKind = ParamGroup.Trend;

        private List<TreeNode> fillEntityTree() // выполняется в потоке!
        {
            List<string> found = new List<string>();
            List<TreeNode> result = new List<TreeNode>();
            TreeNode rootGroups = new TreeNode(
                String.Format("Группы {0}", (ParamGroupKind == ParamGroup.Trend) ? "трендов" : "таблиц"));
            int GroupsCount = (ParamGroupKind == ParamGroup.Trend) ?
                Properties.Settings.Default.GroupsCount : Properties.Settings.Default.TableGroupsCount;
            TreeNode childGroups;
            for (int i = 1; i <= GroupsCount; i += 20)
            {
                int grStart = i;
                int grEnd = i + 19;
                childGroups = rootGroups.Nodes.Add(
                    String.Format("Группы с {0} по {1}", grStart.ToString().PadLeft(3),
                                                         grEnd.ToString().PadLeft(3)));
                childGroups.Tag = new Size(grStart, grEnd);
                for (int j = i; j < i + 20; j++)
                    childGroups.Nodes.Add(
                        String.Format("Группа {0}", j.ToString().PadLeft(3))).Tag = j;
            }
            result.Add(rootGroups);
            return result;
        }

        public frmTableGroupEditor(Form panel, int groupno, ParamGroup kind)
        {
            InitializeComponent();
            ParamGroupKind = kind;
            lastGroupNo = String.Format("Группа {0}", groupno.ToString().PadLeft(3));
            int GroupsCount = (ParamGroupKind == ParamGroup.Trend) ?
                Properties.Settings.Default.GroupsCount : Properties.Settings.Default.TableGroupsCount;
            tstbGroupCount.Text = GroupsCount.ToString();
            lvList.SetDoubleBuffered(true);
            lvEntity.SetDoubleBuffered(true);
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
                backgroundBuildTree.RunWorkerAsync(Data.Entities());
            }
        }

        private void backgroundBuildTree_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            e.Result = fillEntityTree();
        }

        private void findNode(TreeNode node, string item)
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
                        findNode(n, item);
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
            findNode(null, name);
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
            RestoreTreePos(lastGroupNo);
            if (tvEntity.SelectedNode == null) findNode(null,
                String.Format("Группы {0}", (ParamGroupKind == ParamGroup.Trend) ? "трендов" : "таблиц"));
        }

        private void frmBaseEditor_Shown(object sender, EventArgs e)
        {
            fillEntityTreeAsync();
        }

        private void filllvGroups(int start, int end)
        {
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
            {
                if (mySQL.Connected)
                {
                    lvList.BeginUpdate();
                    Cursor = Cursors.WaitCursor;
                    lvList.Items.Clear();
                    for (int i = start; i <= end; i++)
                    {
                        string[] places = Data.GroupItems(i, mySQL, ParamGroupKind);
                        if (!String.IsNullOrWhiteSpace(String.Join(String.Empty, places)))
                            lvList.Items.Add(String.Format("Группа {0}",
                                    i.ToString().PadLeft(3))).SubItems.Add(
                                    Data.GetGroupDesc(i, mySQL, ParamGroupKind));
                    }
                    lvList.EndUpdate();
                    Cursor = Cursors.Default;
                }
            }
        }

        private void filllvGroup(int groupno)
        {
            lvEntity.Items.Clear();
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
            {
                if (mySQL.Connected)
                {
                    lvEntity.Items.Add("Группа №").SubItems.Add(groupno.ToString());
                    lvEntity.Items.Add("Дескриптор группы").
                        SubItems.Add(Data.GetGroupDesc(groupno, mySQL, ParamGroupKind));
                    string[] places = Data.GroupItems(groupno, mySQL, ParamGroupKind);
                    for (int i = 0; i < places.Length; i++)
                    {
                        ListViewItem item = lvEntity.Items.Add(
                            String.Format("{0}. {1}", (i + 1).ToString().PadLeft(2), places[i]));
                        GroupItem gi = Data.GroupItem(groupno, i + 1, mySQL, ParamGroupKind);
                        gi.Group = groupno;
                        gi.Place = i + 1;
                        item.Tag = gi;
                        string ptname = gi.Name;
                        string ptdesc = String.Empty;
                        if (!String.IsNullOrWhiteSpace(ptname))
                        {
                            Entity e = Data.GetEntity(ptname, mySQL);
                            if (!e.Empty) ptdesc = (string)e.Values["PtDesc"];
                        }
                        item.SubItems.Add(ptdesc);
                    }
                }
            }
        }

        private void tvEntity_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int groupno = 0;
            TreeNode node = tvEntity.SelectedNode;
            if (node != null && node.Level == 2 && node.Tag != null &&
                int.TryParse(node.Tag.ToString(), out groupno))
            { // заполнение группы
                filllvGroup(groupno);
                listContainer.Panel1Collapsed = true;
                listContainer.Panel2Collapsed = false;
                resizeColumns(lvEntity, 1);
            }
            else if (node != null && node.Level == 1 && node.Tag != null)
            { // заполнение списков подгрупп
                Size gg = (Size)node.Tag;
                int grStart = gg.Width;
                int grEnd = gg.Height;
                filllvGroups(grStart, grEnd);
                listContainer.Panel2Collapsed = true;
                listContainer.Panel1Collapsed = false;
                resizeColumns(lvList, 1);
            }
            else if (node != null && node.Level == 0)
            {
                // заполнение списков групп
                int GroupsCount = (ParamGroupKind == ParamGroup.Trend) ?
                    Properties.Settings.Default.GroupsCount : Properties.Settings.Default.TableGroupsCount;
                filllvGroups(1, GroupsCount);
                listContainer.Panel2Collapsed = true;
                listContainer.Panel1Collapsed = false;
                resizeColumns(lvList, 1);
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

        private void SendToChangeLog(Entity pt, string param, string oldvalue, string newvalue)
        {
            Data.SendToChangeLog(Properties.Settings.Default.Station,
                pt.Values["PtName"].ToString(), param, oldvalue, newvalue,
                Properties.Settings.Default.CurrentUser, pt.Values["PtDesc"].ToString());
        }

        private void UpdateChangeLog(Entity point, string propName, object propValue)
        {
            string propFine = point.Plugin.GetPropDesc(propName);
            string oldvalue = point.Plugin.GetFineValue(propName, point.Values[propName]);
            string newvalue = point.Plugin.GetFineValue(propName, propValue);
            SendToChangeLog(point, propFine, oldvalue, newvalue);
        }

        private void miChangeString_Click(object sender, EventArgs e)
        {
            ListViewItem item = lvEntity.FocusedItem;
            TreeNode node = tvEntity.SelectedNode;
            if (item != null && node != null && node.Level == 2 && node.Tag != null)
            {
                string propValue = (string)((ToolStripMenuItem)sender).Tag;
                using (frmInputString form = new frmInputString())
                {
                    form.Value = propValue;
                    form.Text = item.Text;
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        propValue = form.Value;
                        item.SubItems[1].Text = propValue;
                        Data.UpdateGroupDesc((int)node.Tag, propValue, ParamGroupKind);
                    }
                }
            }
        }

        private void updateGroupItem(GroupItem gitem, string name, string param)
        {
            gitem.Name = name;
            gitem.Param = param;
            gitem.Caption = (name.Length > 0) ?
                (name + "." + ((param.Length > 0) ? param : "PV")) : String.Empty;
            Data.SaveGroupItem(gitem, ParamGroupKind);
            filllvGroup(gitem.Group);
        }

        string lastPtName = String.Empty;
        string lastPtParam = String.Empty;
        private void miChangeLink_Click(object sender, EventArgs e)
        {
            ListViewItem item = lvEntity.FocusedItem;
            if (item != null && item.Tag != null)
            {
                GroupItem gitem = (GroupItem)item.Tag;
                string PtName = (gitem.Name.Length > 0) ? gitem.Name : lastPtName;
                string PtParam = (gitem.Param.Length > 1) ? gitem.Param : lastPtParam;
                PointSelector kind = (ParamGroupKind == ParamGroup.Trend) ?
                    PointSelector.TrendPoints : PointSelector.TablePoints;
                using (frmEntitySelector form =
                    new frmEntitySelector(PtName, PtParam, kind))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        int index = item.Index;
                        lastPtName = form.EntityName;
                        lastPtParam = form.EntityParam;
                        updateGroupItem(gitem, lastPtName, lastPtParam);
                        UpdateGroupItemsList(index);
                    }
                }
            }
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

        private void miDeleteLink_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Удалить связь?",
                "Удаление связи", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) ==
                DialogResult.OK)
            {
                ListViewItem item = lvEntity.FocusedItem;
                if (item != null && item.Tag != null)
                {
                    GroupItem gi = (GroupItem)item.Tag;
                    int index = item.Index;
                    updateGroupItem(gi, String.Empty, String.Empty);
                    UpdateGroupItemsList(index);
                }
            }
        }

        private void tsmgSaveAs_Click(object sender, EventArgs e)
        {
            exportFileDialog.InitialDirectory = Application.StartupPath;
            exportFileDialog.FileName = (ParamGroupKind == ParamGroup.Trend) ?
                "exporttrends.ini" : "exporttables.ini";
            if (exportFileDialog.ShowDialog() == DialogResult.OK)
                Data.ExportGroupsAs(exportFileDialog.FileName, ParamGroupKind);
        }

        private void tsmgRestoreFrom_Click(object sender, EventArgs e)
        {
            importFileDialog.InitialDirectory = Application.StartupPath;
            if (importFileDialog.ShowDialog() == DialogResult.OK)
            {
                Data.ImportGroupsFrom(importFileDialog.FileName, ParamGroupKind);
                lvEntity.Items.Clear();
                fillEntityTreeAsync();
            }
        }

        private void tsmgClearAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Вы действительно хотите удалить все данные групп параметров?",
                "Очистка групп параметров", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) ==
                DialogResult.OK)
            {
                Data.EmptyGroups(ParamGroupKind);
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
            list.Columns[index].Width = width - sum - SystemInformation.VerticalScrollBarWidth - 5;
        }

        private void lvList_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem item = lvList.FocusedItem;
            if (item != null) findNode(null, item.Text);
        }

        private void lvEntity_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ListViewItem item = lvEntity.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    miChangeString.Visible = false;
                    miChangeLink.Visible = miDeleteLink.Visible = false;
                    switch (item.Index)
                    {
                        case 1:
                            miChangeString.Visible = true;
                            string sValue = item.SubItems[1].Text;
                            miChangeString.Tag = sValue;
                            break;
                        default:
                            if (item.Index > 1)
                            {
                                miChangeLink.Visible = true;
                                miChangeLink.Tag = item.Tag;
                                if (item.Tag != null)
                                {
                                    miDeleteLink.Visible = true;
                                    miDeleteLink.Tag = item.Tag;
                                }
                            }
                            break;
                    }
                    Point p = lvEntity.PointToScreen(new Point(e.X, e.Y));
                    cmsProps.Show(p.X, p.Y);
                }
            }
        }

        private void tstbGroupCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Properties.Settings settings = Properties.Settings.Default;
                int GroupsCount;
                if (int.TryParse(tstbGroupCount.Text, out GroupsCount))
                {
                    if (GroupsCount < 20)
                    {
                        GroupsCount = 20;
                        tstbGroupCount.Text = GroupsCount.ToString();
                    }
                    else if (GroupsCount > Data.GroupCount(ParamGroupKind))
                    {
                        GroupsCount = Data.GroupCount(ParamGroupKind);
                        tstbGroupCount.Text = GroupsCount.ToString();
                    }
                    tstbGroupCount.SelectAll();
                    if (ParamGroupKind == ParamGroup.Trend)
                        settings.GroupsCount = GroupsCount;
                    else
                        settings.TableGroupsCount = GroupsCount;
                    settings.Save();
                    fillEntityTreeAsync();
                }
            }
        }
    }
}
