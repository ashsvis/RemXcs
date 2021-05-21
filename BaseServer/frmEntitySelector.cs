using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Points.Plugins;

namespace BaseServer
{
    public enum PointSelector { AllPoints, TrendPoints, TablePoints, AnalogPoints, DigitalPoints,
                                KonturPoints, ValvePoints}

    public partial class frmEntitySelector : Form
    {
        IDictionary<string, int> IconList = new Dictionary<string, int>();
        public frmEntitySelector(string name, string param, PointSelector kind)
        {
            InitializeComponent();
            IDictionary<string, IPointPlugin> plugins = PointPlugin.LoadPlugins(Application.StartupPath);
            foreach (KeyValuePair<string, IPointPlugin> plugin in plugins)
            {
                using (Bitmap bmp = new Bitmap(16, 16))
                {
                    Graphics g = Graphics.FromImage(bmp);
                    Data.DrawIconImage(Graphics.FromImage(bmp),
                        plugin.Value.GetIconColor, plugin.Value.PluginShortType);
                    ilList.Images.Add(bmp);
                }
                IconList.Add(plugin.Value.PluginCategory + plugin.Value.PluginShortType,
                    ilList.Images.Count - 1);
            }
            switch (kind)
            {
                case PointSelector.AllPoints: FillItemsForAllPoints(ilList, IconList); break;
                case PointSelector.TrendPoints: FillItemsForTrendPoints(ilList, IconList); break;
                case PointSelector.TablePoints: FillItemsForTablePoints(ilList, IconList); break;
                case PointSelector.AnalogPoints: FillItemsForAnalogPoints(ilList, IconList); break;
                case PointSelector.DigitalPoints: FillItemsForDigitalPoints(ilList, IconList); break;
                case PointSelector.KonturPoints: FillItemsForKonturPoints(ilList, IconList); break;
                case PointSelector.ValvePoints: FillItemsForValvePoints(ilList, IconList); break;
            }
            this.EntityName = name;
            this.EntityParam = param;
            ListViewItem item = lvItems.FindItemWithText(name +
                ((param.Length > 0) ? "." + param : String.Empty));
            if (item != null)
            {
                item.EnsureVisible();
                lvItems.FocusedItem = item;
                item.Selected = true;
                btnOk.Enabled = true;
            }
        }

        private void FillItemsForAllPoints(ImageList imagelist,
            IDictionary<string, int> iconlist)
        {
            lvItems.Items.Clear();
            ListViewItem item;
            foreach (KeyValuePair<string, Entity> kvp in Data.Entities())
            {
                Entity e = kvp.Value;
                int ptKind = (int)e.Values["PtKind"];
                if (ptKind == PtKind.Analog || ptKind == PtKind.Digital)
                {
                    item = lvItems.Items.Add(kvp.Key + ".PV");
                    item.ImageIndex = iconlist[e.Plugin.PluginCategory + e.Plugin.PluginShortType];
                    item.SubItems.Add((string)kvp.Value.Values["PtDesc"]);
                }
            }
        }

        private void FillItemsForAnalogPoints(ImageList imagelist,
            IDictionary<string, int> iconlist)
        {
            lvItems.Items.Clear();
            ListViewItem item;
            foreach (KeyValuePair<string, Entity> kvp in Data.Entities())
            {
                Entity e = kvp.Value;
                int ptKind = (int)e.Values["PtKind"];
                if (ptKind == PtKind.Analog)
                {
                    item = lvItems.Items.Add(kvp.Key + ".PV");
                    item.ImageIndex = iconlist[e.Plugin.PluginCategory + e.Plugin.PluginShortType];
                    item.SubItems.Add((string)kvp.Value.Values["PtDesc"]);
                }
            }
        }

        private void FillItemsForKonturPoints(ImageList imagelist,
            IDictionary<string, int> iconlist)
        {
            lvItems.Items.Clear();
            ListViewItem item;
            foreach (KeyValuePair<string, Entity> kvp in Data.Entities())
            {
                Entity e = kvp.Value;
                int ptKind = (int)e.Values["PtKind"];
                if (ptKind == PtKind.Kontur)
                {
                    item = lvItems.Items.Add(kvp.Key);
                    item.ImageIndex = iconlist[e.Plugin.PluginCategory + e.Plugin.PluginShortType];
                    item.SubItems.Add((string)kvp.Value.Values["PtDesc"]);
                }
            }
        }

        private void FillItemsForValvePoints(ImageList imagelist,
            IDictionary<string, int> iconlist)
        {
            lvItems.Items.Clear();
            ListViewItem item;
            foreach (KeyValuePair<string, Entity> kvp in Data.Entities())
            {
                Entity e = kvp.Value;
                int ptKind = (int)e.Values["PtKind"];
                if (ptKind == PtKind.Valve)
                {
                    item = lvItems.Items.Add(kvp.Key);
                    item.ImageIndex = iconlist[e.Plugin.PluginCategory + e.Plugin.PluginShortType];
                    item.SubItems.Add((string)kvp.Value.Values["PtDesc"]);
                }
            }
        }

        private void FillItemsForDigitalPoints(ImageList imagelist,
            IDictionary<string, int> iconlist)
        {
            lvItems.Items.Clear();
            ListViewItem item;
            foreach (KeyValuePair<string, Entity> kvp in Data.Entities())
            {
                Entity e = kvp.Value;
                int ptKind = (int)e.Values["PtKind"];
                if (ptKind == PtKind.Digital)
                {
                    item = lvItems.Items.Add(kvp.Key + ".PV");
                    item.ImageIndex = iconlist[e.Plugin.PluginCategory + e.Plugin.PluginShortType];
                    item.SubItems.Add((string)kvp.Value.Values["PtDesc"]);
                }
            }
        }

        private void FillItemsForTrendPoints(ImageList imagelist,
            IDictionary<string, int> iconlist)
        {
            lvItems.Items.Clear();
            ListViewItem item;
            foreach (KeyValuePair<string, Entity> kvp in Data.Entities())
            {
                Entity e = kvp.Value;
                int ptKind = (int)e.Values["PtKind"];
                if (ptKind == PtKind.Analog || ptKind == PtKind.Digital || ptKind == PtKind.Kontur)
                {
                    if (kvp.Value.Values.ContainsKey("Trend") &&
                        (bool)kvp.Value.Values["Trend"])
                    {
                        item = lvItems.Items.Add(kvp.Key + ".PV");
                        item.ImageIndex = iconlist[e.Plugin.PluginCategory + e.Plugin.PluginShortType];
                        item.SubItems.Add((string)kvp.Value.Values["PtDesc"]);
                        if (ptKind == PtKind.Kontur)
                        {
                            item = lvItems.Items.Add(kvp.Key + ".SP");
                            item.ImageIndex = iconlist[e.Plugin.PluginCategory + e.Plugin.PluginShortType];
                            item.SubItems.Add((string)kvp.Value.Values["PtDesc"] + " (задание)");
                            item = lvItems.Items.Add(kvp.Key + ".OP");
                            item.ImageIndex = iconlist[e.Plugin.PluginCategory + e.Plugin.PluginShortType];
                            item.SubItems.Add((string)kvp.Value.Values["PtDesc"] + " (выход)");
                        }
                    }
                }
            }
        }

        private void FillItemsForTablePoints(ImageList imagelist,
            IDictionary<string, int> iconlist)
        {
            HashSet<string> list = new HashSet<string>();
            lvItems.Items.Clear();
            ListViewItem item;
            foreach (KeyValuePair<string, Entity> kvp in Data.Entities())
            {
                Entity e = kvp.Value;
                int ptKind = (int)e.Values["PtKind"];
                if (ptKind == PtKind.Table)
                {

                    foreach (KeyValuePair<string, object> prop in e.Values)
                    {
                        if (prop.Key.StartsWith("Child") &&
                            !String.IsNullOrWhiteSpace((string)prop.Value))
                        {
                            string childname = ((string)e.Values[prop.Key]).Split(new char[]{';'})[0];
                            if (!list.Contains(childname))
                            {
                                list.Add(childname);
                                Entity child = Data.GetEntity(childname);
                                if (!child.Empty && child.Values.ContainsKey("PtDesc"))
                                {
                                    item = lvItems.Items.Add(childname + ".PV");
                                    item.ImageIndex = iconlist[child.Plugin.PluginCategory + child.Plugin.PluginShortType];
                                    item.SubItems.Add((string)child.Values["PtDesc"]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    int tablekind = e.Values.ContainsKey("Table") ? (int)e.Values["Table"] : 0;
                    if (tablekind > 0)
                    {
                        string ptname = (string)e.Values["PtName"];
                        if (!list.Contains(ptname))
                        {
                            list.Add(ptname);
                            if (e.Values.ContainsKey("PtDesc"))
                            {
                                item = lvItems.Items.Add(ptname + ".PV");
                                item.ImageIndex = iconlist[e.Plugin.PluginCategory +
                                    e.Plugin.PluginShortType];
                                item.SubItems.Add((string)e.Values["PtDesc"]);
                            }
                        }
                    }
                }
            }
        }

        public string EntityName { get; set; }
        public string EntityParam { get; set; }

        private void lvItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem item = lvItems.FocusedItem;
            if (item != null)
            {
                string[] buff = item.Text.Split(new Char[] { '.' });
                if (buff.Length == 2)
                {
                    EntityName = buff[0];
                    EntityParam = buff[1];
                }
                else if (buff.Length == 1)
                    EntityName = buff[0];
                btnOk.Enabled = true;
            }
            else
                btnOk.Enabled = false;
        }

        private void lvItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = lvItems.FocusedItem;
            if (item != null)
                this.DialogResult = DialogResult.OK;
            else
                this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lvItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) e.Handled = true;
        }

        private int lastColumn = -1;
        private void lvItems_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (lastColumn != e.Column)
            {
                this.lvItems.ListViewItemSorter = new ListViewItemComparer(e.Column);
                lastColumn = e.Column;
            }
            else
            {
                if (this.lvItems.ListViewItemSorter is ListViewItemComparer)
                    this.lvItems.ListViewItemSorter = new ListViewItemReverseComparer(e.Column);
                else
                    this.lvItems.ListViewItemSorter = new ListViewItemComparer(e.Column);
            }
            if (this.lvItems.FocusedItem != null)
                this.lvItems.FocusedItem.EnsureVisible();
        }
    }

}
