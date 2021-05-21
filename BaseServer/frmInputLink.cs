using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Points.Plugins;

namespace BaseServer
{
    public partial class frmInputLink : Form
    {
        string selected = String.Empty;
        ImageList ImageList;
        public frmInputLink(string name, Entity ent, ImageList imagelist,
            IDictionary<string, int> iconlist)
        {
            this.ImageList = imagelist;
            int channel = 0;
            int node = 0;
            bool chanNodeFilter = false;
            List<string> childtypes = new List<string>(ent.Plugin.GetFilterChildTypes);

            if (ent.Values != null)
            {
                channel = (ent.Values.ContainsKey("Channel")) ? (int)ent.Values["Channel"] : -1;
                node = (ent.Values.ContainsKey("Node")) ? (int)ent.Values["Node"] : -1;
                chanNodeFilter = (channel >= 0) && (node >= 0);
            }
            InitializeComponent();
            foreach (KeyValuePair<string, Entity> point in Data.Entities())
            {
                Entity e = point.Value;
                int ptKind = int.Parse(e.Values["PtKind"].ToString());
                if (ptKind == PtKind.Group || ptKind == PtKind.Table) continue;
                if (childtypes.Count > 0)
                {
                    if (!childtypes.Contains(e.Values["PtType"].ToString()))
                        continue;
                }
                if (chanNodeFilter && ent.Values != null &&
                    e.Values.ContainsKey("Channel") && e.Values.ContainsKey("Node"))
                {
                    if (channel != (int)e.Values["Channel"] ||
                        node != (int)ent.Values["Node"]) continue;
                }
                ListViewItem item = new ListViewItem(point.Key);
                item.ImageIndex = iconlist[e.Plugin.PluginCategory + e.Plugin.PluginShortType];
                item.SubItems.Add(e.Values["PtDesc"].ToString());
                lvList.Items.Add(item);
                if (point.Key == name)
                {
                    item.Selected = true;
                }
            }
            lvList.SmallImageList = ImageList;
            lvList.FocusedItem = lvList.FindItemWithText(name);
            if (lvList.FocusedItem != null)
                lvList.EnsureVisible(lvList.FocusedItem.Index);
        }

        public string GetValue
        {
            get { return selected; }
        }

        private void lvList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvList.FocusedItem != null)
            {
                selected = lvList.FocusedItem.Text;
                btnEnter.Enabled = true;
            }
        }

        private void lvList_DoubleClick(object sender, EventArgs e)
        {
            if (lvList.FocusedItem != null)
            {
                selected = lvList.FocusedItem.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private int lastColumn = -1;
        private void lvList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (lastColumn != e.Column)
            {
                this.lvList.ListViewItemSorter = new ListViewItemComparer(e.Column);
                lastColumn = e.Column;
            }
            else
            {
                if (this.lvList.ListViewItemSorter is ListViewItemComparer)
                    this.lvList.ListViewItemSorter = new ListViewItemReverseComparer(e.Column);
                else
                    this.lvList.ListViewItemSorter = new ListViewItemComparer(e.Column);
            }
            if (this.lvList.FocusedItem != null)
                this.lvList.FocusedItem.EnsureVisible();
        }
    }
}
