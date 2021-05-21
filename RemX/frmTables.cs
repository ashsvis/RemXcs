using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Text;
using BaseServer;
using Points.Plugins;

namespace RemXcs
{
    public partial class frmTables : Form, IViewUpdate
    {
        Form DataHost;
        Form PanelHost;
        List<ColumnHeader> headers = new List<ColumnHeader>();
        string viewTable = String.Empty;
        string TableSQL = String.Empty;
        private int viewPos = 0;
        private int viewCount;
        private int currGroup = 1;
        private bool checking = true;
        private bool Scrolling
        {
            get { return checking; }
            set
            {
                checking = value;
                if (checking)
                {
                    tsbAnchor.Image = Properties.Resources.on;
                    SelectEndRecords();
                }
                else
                    tsbAnchor.Image = Properties.Resources.off;
            }
        }
        private string lastSnapTime = String.Empty;

        IUpdatePanel updater;

        public frmTables(Form host, Form panelhost, string table, int curgroup)
        {
            string grdesc;
            InitializeComponent();
            currGroup = curgroup;
            viewTable = table;
            DataHost = host;
            PanelHost = panelhost;
            updater = panelhost as IUpdatePanel;
            bool anyGroupExists = false;
            int j = 1;
            int n = 20;
            int GroupsCount = Properties.Settings.Default.TableGroupsCount;
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, false)) // чтение удалённо
            {
                if (mySQL.Connected)
                {
                    for (int i = 1; i <= GroupsCount / n; i++)
                    {
                        ToolStripDropDownItem item = (ToolStripDropDownItem)tsbGroupSelect.DropDownItems.Add(
                            String.Format("Группы с {0} по {1}", j, j + n - 1));
                        for (int k = j; k < j + n; k++)
                        {
                            if (!Data.GroupIsEmpty(k, mySQL, ParamGroup.Table))
                            {
                                grdesc = Data.GetGroupDesc(k, mySQL, ParamGroup.Table);
                                ToolStripItem groupItem = item.DropDownItems.Add(
                                    String.Format("Группа {0}{1}", k,
                                    String.IsNullOrWhiteSpace(grdesc) ? String.Empty : " - " + grdesc),
                                    null, miGroup_Click);
                                groupItem.ImageIndex = k;
                                anyGroupExists = true;
                            }
                        }
                        j += n;
                    }
                    tsbNextGroup.Enabled = anyGroupExists;
                    for (int i = 1; i <= Data.PlaceCount(ParamGroup.Table); i++)
                    {
                        ColumnHeader header = new ColumnHeader();
                        headers.Add(header);
                        header.TextAlign = HorizontalAlignment.Right;
                        header.Width = 80;
                        header.Text = String.Empty;
                        lvTableView.Columns.Add(header);
                    }
                    grdesc = Data.GetGroupDesc(currGroup, mySQL, ParamGroup.Table);
                    lbGroupCaption.Text = String.Format("Группа {0} {1}", currGroup,
                        String.IsNullOrWhiteSpace(grdesc) ? "(без названия)" : " - " + grdesc);
                }
            }
            if (!Data.GroupIsEmpty(currGroup, ParamGroup.Table))
            {
                updater.UpdateCurrentGroupNo(currGroup);
                UpdateGroupCaptions();
            }
            tsbPrevGroup.Enabled = currGroup > 1;
        }

        private void UpdateGroupCaptions()
        {
            lastSnapTime = String.Empty;
            tsbGroupSelect.Text = String.Format("Группа {0}", currGroup);
            string grdesc = Data.GetGroupDesc(currGroup, ParamGroup.Table);
            lbGroupCaption.Text = String.Format("Группа {0} {1}", currGroup,
                String.IsNullOrWhiteSpace(grdesc) ? "(без названия)" : " - " + grdesc);
            switch (viewTable)
            {
                case "minutes":
                    this.Text = String.Format("Архив минутных значений группы {0}", currGroup);
                    break;
                case "hours":
                    this.Text = String.Format("Архив часовых значений группы {0}", currGroup);
                    break;
                case "days":
                    this.Text = String.Format("Архив суточных значений группы {0}", currGroup);
                    break;
                case "months":
                    this.Text = String.Format("Архив месячных значений группы {0}", currGroup);
                    break;
            }
        }

        private void miGroup_Click(object sender, EventArgs e)
        {
            ToolStripItem groupItem = (ToolStripItem)sender;
            currGroup = groupItem.ImageIndex;
            UpdateTableView();
        }

        private void UpdateTableView()
        {
            updater.UpdateCurrentGroupNo(currGroup);
            UpdateGroupCaptions();
            Scrolling = true;
            tsbPrevGroup.Enabled = true;
            tsbNextGroup.Enabled = true;
        }

        private void frmTables_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private int calcRowsCount(ListView lv)
        {   // Автоматический подсчет количества строк, которые умещаются в ListView без прокрутки
            int i = 0;
            ListViewItem item;
            lv.Items.Clear();
            do
            {
                item = new ListViewItem(i++.ToString());
                lv.Items.Add(item);
                item.Selected = true;
                lv.EnsureVisible(item.Index);
            } while (lv.TopItem.Text == "0");
            lv.Items.Clear();
            return i - 1;
        }

        private void EmptyTable()
        {
            for (int i = 0; i < lvTableView.Items.Count; i++)
                for (int j = 0; j < Data.PlaceCount(ParamGroup.Table); j++)
                    lvTableView.Items[i].SubItems[j].Text = String.Empty;
        }

        private void frmTables_Shown(object sender, EventArgs e)
        {
            UpdateColumnWidths();
            viewCount = calcRowsCount(lvTableView);
            lvTableView.SetDoubleBuffered(true);
            ListView lv = lvTableView;
            TableSQL = "select `snaptime`,`name`,`value`,`kind` from `" + viewTable + "` " +
                        "where (`name`='{0}') order by `snaptime` desc limit {1},{2}";
            LoadTable(viewPos, viewCount);
            tsbBackward.Enabled = LoadTable(viewPos + viewCount, -1) > 0 ? true : false;
        }

        private void UpdateColumnWidths()
        {
            int panelwidth = lvTableView.ClientSize.Width;
            int width = (panelwidth -
                lvTableView.Columns[0].Width) / Data.PlaceCount(ParamGroup.Table);
            for (int i = 1; i <= Data.PlaceCount(ParamGroup.Table); i++)
                lvTableView.Columns[i].Width = width;
        }

        List<ListViewItem> tablerows = new List<ListViewItem>();
        public int LoadTable(int pos, int count, bool print = false)
        {
            int result = 0;
            if (pos < 0) return result;
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, false)) // чтение удалённо
            {
                if (mySQL.Connected)
                {
                    string[] items = Data.GroupItems(currGroup, mySQL, ParamGroup.Table);
                    int[] formats = new int[items.Length];
                    int n = 0;
                    foreach (string item in items)
                    {
                        string ptname = item.Split(new char[] { '.' })[0];
                        Entity ent = Data.GetEntity(ptname, mySQL);
                        if (!ent.Empty && ent.Values.ContainsKey("FormatPV"))
                            formats[n] = (int)ent.Values["FormatPV"];
                        else
                            formats[n] = 3;
                        n++;
                    }
                    for (int i = 1; i <= Data.PlaceCount(ParamGroup.Table); i++)
                    {
                        ColumnHeader header = lvTableView.Columns[i];
                        if (!header.Text.Equals(items[i - 1]))
                        {
                            for (int j = 0; j < lvTableView.Items.Count; j++)
                                lvTableView.Items[j].SubItems[i].Text = String.Empty;
                        }
                        header.Text = items[i - 1];
                        GroupItem gi = Data.GroupItem(currGroup, i, mySQL, ParamGroup.Table);
                        gi.Group = currGroup;
                        gi.Place = i;
                        header.Tag = gi;
                    }
                    try
                    {
                        ArrayList results = mySQL.GetTableMatrix(TableSQL, items, formats, pos,
                            Math.Abs(count));
                        if (count > 0)
                        {
                            string[] lastrow;
                            if (results.Count > 0)
                                lastrow = (string[])results[results.Count - 1];
                            else
                            {
                                lastrow = new string[1];
                                lastrow[0] = String.Empty;
                            }
                            //if (!lastSnapTime.Equals(lastrow[0]))
                            //{
                                lastSnapTime = lastrow[0];
                                tablerows.Clear();
                                int row = 0;
                                foreach (string[] cols in results)
                                {
                                    ListViewItem item = new ListViewItem();
                                    if (row % 2 != 0) item.BackColor = Color.FromArgb(240, 240, 240);
                                    for (int col = 0; col < cols.Length; col++)
                                    {
                                        if (col == 0)
                                            item.Text = cols[col];
                                        else
                                            item.SubItems.Add(cols[col]);
                                    }
                                    tablerows.Add(item);
                                    row++;
                                }
                                if (!print)
                                {
                                    lvTableView.Items.Clear();
                                    lvTableView.Items.AddRange(tablerows.ToArray());
                                }
                                if (Scrolling) GotoRecord(GoalRecord.LastRecord);
                            //}
                        }
                         result = results.Count;
                    }
                    catch (Exception e)
                    {
                        updater.UpdateStatusMessage(e.Message);
                    }
                }
                else
                {
                    updater.UpdateStatusMessage("База данных '" +
                        mySQL.CurrentDatabase + "' сервера недоступна!");
                }
            }
            return result;
        }

        enum GoalRecord
        {
            FirstRecord,
            LastRecord
        }

        private void GotoRecord(GoalRecord goal)
        {
            if (lvTableView.Items.Count > 0)
            {
                ListViewItem item;
                if (goal == GoalRecord.FirstRecord)
                    item = lvTableView.Items[0];
                else
                    item = lvTableView.Items[lvTableView.Items.Count - 1];
                item.Selected = true;
                lvTableView.FocusedItem = item;
                item.EnsureVisible();
            }
        }

        private void tsbBackward_Click(object sender, EventArgs e)
        {
            Scrolling = false;
            viewPos += viewCount;
            LoadTable(viewPos, viewCount);
            GotoRecord(GoalRecord.FirstRecord);
            tsbBackward.Enabled = LoadTable(viewPos + viewCount, -1) > 0 ? true : false;
            tsbForward.Enabled = true;
            tsbEnd.Enabled = true;
        }

        private void tsbForward_Click(object sender, EventArgs e)
        {
            if (viewPos - viewCount >= 0)
            {
                viewPos -= viewCount;
                LoadTable(viewPos < 0 ? 0 : viewPos, viewCount);
                GotoRecord(GoalRecord.LastRecord);
                tsbForward.Enabled = LoadTable(viewPos - viewCount, -1) > 0 ? true : false;
                tsbEnd.Enabled = tsbForward.Enabled;
                tsbBackward.Enabled = true;
            }
            else
                tsbForward.Enabled = false;
        }

        private void SelectEndRecords()
        {
            viewPos = 0;
            LoadTable(viewPos, viewCount);
            GotoRecord(GoalRecord.LastRecord);
            tsbBackward.Enabled = LoadTable(viewPos + viewCount, -1) > 0 ? true : false;
            tsbForward.Enabled = false;
            tsbEnd.Enabled = false;
        }

        private void tsbEnd_Click(object sender, EventArgs e)
        {
            SelectEndRecords();
        }

        private void tsbAnchor_Click(object sender, EventArgs e)
        {
            Scrolling = !Scrolling;
        }

        private void tsbNextGroup_Click(object sender, EventArgs e)
        {
            tsbPrevGroup.Enabled = true;
            int lastGroup = currGroup;
            int GroupsCount = Properties.Settings.Default.TableGroupsCount;
            while (currGroup++ < GroupsCount)
            {
                if (!Data.GroupIsEmpty(currGroup, ParamGroup.Table))
                {
                    updater.UpdateCurrentGroupNo(currGroup);
                    UpdateGroupCaptions();
                    Scrolling = true;
                    return;
                }
            }
            currGroup = lastGroup;
            updater.UpdateCurrentGroupNo(currGroup);
            tsbNextGroup.Enabled = false;
        }

        private void tsbPrevGroup_Click(object sender, EventArgs e)
        {
            tsbNextGroup.Enabled = true;
            int lastGroup = currGroup;
            while (currGroup-- > 1)
            {
                if (!Data.GroupIsEmpty(currGroup, ParamGroup.Table))
                {
                    updater.UpdateCurrentGroupNo(currGroup);
                    UpdateGroupCaptions();
                    Scrolling = true;
                    return;
                }
            }
            currGroup = lastGroup;
            updater.UpdateCurrentGroupNo(currGroup);
            tsbPrevGroup.Enabled = false;
        }

        public void UpdateView()
        {
            if(Scrolling) SelectEndRecords();
        }

        frmTables tableform = null;
        private void tsbMinutes_Click(object sender, EventArgs e)
        {
            string Caption = "Архив минутных значений группы " + currGroup;
            if (!updater.FindAndShow(Caption))
            {
                tableform = new frmTables(DataHost, PanelHost, "minutes", currGroup);
                updater.ChildForms().Add(tableform);
                tableform.Text = Caption;
                tableform.MdiParent = PanelHost;
                tableform.Show();
            }

        }

        private void tsbHours_Click(object sender, EventArgs e)
        {
            string Caption = "Архив часовых значений группы " + currGroup;
            if (!updater.FindAndShow(Caption))
            {
                tableform = new frmTables(DataHost, PanelHost, "hours", currGroup);
                updater.ChildForms().Add(tableform);
                tableform.Text = Caption;
                tableform.MdiParent = PanelHost;
                tableform.Show();
            }

        }

        private void tsbDailys_Click(object sender, EventArgs e)
        {
            string Caption = "Архив суточных значений группы " + currGroup;
            if (!updater.FindAndShow(Caption))
            {
                tableform = new frmTables(DataHost, PanelHost, "days", currGroup);
                updater.ChildForms().Add(tableform);
                tableform.Text = Caption;
                tableform.MdiParent = PanelHost;
                tableform.Show();
            }

        }

        private void tsbMonths_Click(object sender, EventArgs e)
        {
            string Caption = "Архив месячных значений группы " + currGroup;
            if (!updater.FindAndShow(Caption))
            {
                tableform = new frmTables(DataHost, PanelHost, "months", currGroup);
                updater.ChildForms().Add(tableform);
                tableform.Text = Caption;
                tableform.MdiParent = PanelHost;
                tableform.Show();
            }

        }

        frmPrintPreview previewform = null;
        private void tsbPreview_Click(object sender, EventArgs e)
        {
            string Caption = "Предварительный просмотр";
            updater.CloseAndRemove(Caption);
            previewform = new frmPrintPreview();
            updater.ChildForms().Add(previewform);
            previewform.printDoc.QueryPageSettings += printDoc_QueryPageSettings;
            previewform.printDoc.PrintPage += printDoc_PrintPage;
            previewform.Text = Caption;
            previewform.MdiParent = PanelHost;
            previewform.Show();
        }

        private void printDoc_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
            e.PageSettings.Landscape = true;
        }

        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            RectangleF prnrect = e.PageBounds;
            prnrect.Inflate(new Size(-30, -50));
            string sysinfo = "RemX - Автоматизированная система контроля и управления";
            string printdate = "Отпечатано: " + DateTime.Now.ToString();
            string printname = this.Text;
            string grdesc = "Группа " + currGroup + ": " + Data.GetGroupDesc(currGroup, ParamGroup.Table);
            SizeF strSize;
            PointF strPoint;
            using (Font printfont = new Font("Arial", 8, FontStyle.Italic))
            {
                strSize = e.Graphics.MeasureString(sysinfo, printfont);
                strPoint = prnrect.Location;
                strPoint.Y -= strSize.Height;
                e.Graphics.DrawString(sysinfo, printfont, Brushes.Black, strPoint);
                //------------------
                strSize = e.Graphics.MeasureString(printdate, printfont);
                strPoint = prnrect.Location;
                strPoint.Y += prnrect.Height;
                e.Graphics.DrawString(printdate, printfont, Brushes.Black, strPoint);
                //------------------
                strSize = e.Graphics.MeasureString(printname, printfont);
                strPoint = prnrect.Location;
                strPoint.X += prnrect.Width - strSize.Width;
                strPoint.Y += prnrect.Height;
                e.Graphics.DrawString(printname, printfont, Brushes.Black, strPoint);
                //------------------
                strSize = e.Graphics.MeasureString(grdesc, printfont);
                strPoint = prnrect.Location;
                strPoint.X += prnrect.Width - strSize.Width;
                strPoint.Y -= strSize.Height;
                e.Graphics.DrawString(grdesc, printfont, Brushes.Black, strPoint);
            }
            string tabletext = "Xy";
            int prnviewCount = 0;
            Single chWidth = prnrect.Width / lvTableView.Columns.Count;
            using (Font printfont = new Font("Arial", 8, FontStyle.Regular))
            {
                //Single strHeight = prnrect.Height / (viewCount + 1);
                Single blankHeight = 0;
                Single strHeight = e.Graphics.MeasureString(tabletext, printfont).Height + blankHeight;
                prnviewCount = int.Parse(Math.Truncate(prnrect.Height / strHeight).ToString()) - 1;
                LoadTable(viewPos, prnviewCount, true);
                strPoint.Y = prnrect.Y;
                // Печать заголовка таблицы
                strPoint.X = prnrect.X;
                using (Font headerfont = new Font("Arial Narrow", 8, FontStyle.Bold))
                {
                    foreach (ColumnHeader ch in lvTableView.Columns)
                    {
                        tabletext = ch.Text;
                        strSize = e.Graphics.MeasureString(tabletext, headerfont);
                        PointF pch = strPoint;
                        if (ch.Index > 0)
                        {
                            pch.X += chWidth - strSize.Width - 5;
                        }
                        e.Graphics.DrawString(tabletext, headerfont, Brushes.Black, pch);
                        strPoint.X += chWidth;
                    }
                }
                strPoint.Y += strHeight;
                int row = 0;
                // Печать строк таблицы
                foreach (ListViewItem lvi in tablerows)
                {
                    strPoint.X = prnrect.X;
                    if (row % 2 != 0)
                        using (SolidBrush brush = new SolidBrush(Color.FromArgb(240, 240, 240))) 
                            e.Graphics.FillRectangle(brush,
                                strPoint.X, strPoint.Y, prnrect.Width, strHeight);
                    row++;
                    foreach (ColumnHeader ch in lvTableView.Columns)
                    {
                        tabletext = lvi.SubItems[ch.Index].Text;
                        strSize = e.Graphics.MeasureString(tabletext, printfont);
                        PointF pch = strPoint;
                        if (ch.Index > 0)
                        {
                            pch.X += chWidth - strSize.Width - 5;
                        }
                        e.Graphics.DrawString(tabletext, printfont, Brushes.Black, pch);
                        strPoint.X += chWidth;
                    }
                    strPoint.Y += strHeight;
                }
                tablerows.Clear();
            }
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog(this) == DialogResult.OK)
            {
                printDoc.Print();
            }
        }

        string lastPtName = String.Empty;
        string lastPtParam = String.Empty;
        private void tsmiChangeItem_Click(object sender, EventArgs e)
        {
            ColumnHeader curcolumn = (ColumnHeader)headerMenu.Tag;
            string[] args = curcolumn.Text.Split(new char[] { '.' });
            string PtName = (args[0].Length > 0) ? args[0] : lastPtName;
            string PtParam = (args.Length > 1) ? args[1].ToString() : lastPtParam;
            GroupItem gi = (GroupItem)curcolumn.Tag;
            using (frmEntitySelector form =
                new frmEntitySelector(PtName, PtParam, PointSelector.TablePoints))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    lastPtName = form.EntityName;
                    lastPtParam = form.EntityParam;
                    updateGroupItem(gi, lastPtName, lastPtParam);
                    curcolumn.Text = lastPtName + "." + lastPtParam;
                    UpdateTableView();
                }
            }
        }

        private void tsmiDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Удалить связь?",
                "Удаление связи", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) ==
                DialogResult.OK)
            {
                ColumnHeader curcolumn = (ColumnHeader)headerMenu.Tag;
                string[] args = curcolumn.Text.Split(new char[] { '.' });
                string PtName = args[0];
                string PtParam = (args.Length > 1) ? args[1].ToString() : String.Empty;
                GroupItem gi = (GroupItem)curcolumn.Tag;
                updateGroupItem(gi, String.Empty, String.Empty);
                curcolumn.Text = String.Empty;
                UpdateTableView();
            }
        }

        private void updateGroupItem(GroupItem gitem, string name, string param)
        {
            gitem.Name = name;
            gitem.Param = param;
            gitem.Caption = (name.Length > 0) ?
                (name + "." + ((param.Length > 0) ? param : "PV")) : String.Empty;
            Data.SaveGroupItem(gitem, ParamGroup.Table);
        }

        // This returns an array of ColumnHeaders in the order they are
        // displayed by the ListView.  
        private static ColumnHeader[] GetOrderedHeaders(ListView lv)
        {
            ColumnHeader[] arr = new ColumnHeader[lv.Columns.Count];

            foreach (ColumnHeader header in lv.Columns)
            {
                arr[header.DisplayIndex] = header;
            }

            return arr;
        }

        private void cmsGroupItemChange_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // This call indirectly calls EnumWindowCallBack which sets _headerRect
            // to the area occupied by the ListView's header bar.
            Interaction.EnumChildWindows(
                lvTableView.Handle, 
                new EnumWinCallBack(Interaction.EnumWindowCallBack), IntPtr.Zero);

            // If the mouse position is in the header bar, cancel the display
            // of the regular context menu and display the column header context 
            // menu instead.
            if (Interaction._headerRect.Contains(Control.MousePosition))
            {
                e.Cancel = true;

                // The xoffset is how far the mouse is from the left edge of the header.
                int xoffset = Control.MousePosition.X - Interaction._headerRect.Left;

                // Iterate through the column headers in the order they are displayed, 
                // adding up their widths as we go.  When the sum exceeds the xoffset, 
                // we know the mouse is on the current header. 
                int sum = 0;
                foreach (ColumnHeader header in GetOrderedHeaders(lvTableView))
                {
                    sum += header.Width;
                    if (sum > xoffset)
                    {
                        // This code displays the same context menu for 
                        // every header, but changes the menu item
                        // text based on the header. It sets the context 
                        // menu tag to the header object so
                        // the handler for whatever command the user 
                        // clicks can know the column.
                        if (header.Index > 0)
                        {
                            headerMenu.Tag = header;
                            tsmiDelete.Visible = (header.Text.Length > 0);
                            headerMenu.Show(Control.MousePosition);
                        }
                        break;
                    }
                }
            }
            else
            {
                // Allow the regular context menu to be displayed.
                // We may want to update the menu here.
            }
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            saveCSVFileDialog.FileName = this.Text + ".csv";
            if (saveCSVFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                    List<string> lines = new List<string>(10001);
                    List<string> columns = new List<string>(11);
                    foreach (ColumnHeader ch in lvTableView.Columns) columns.Add(ch.Text);
                    lines.Add(String.Join(";", columns.ToArray()));
                    LoadTable(0, 10000, true);
                    foreach (ListViewItem lvi in tablerows)
                    {
                        columns.Clear();
                        foreach (ColumnHeader ch in lvTableView.Columns) columns.Add(lvi.SubItems[ch.Index].Text);
                        lines.Add(String.Join(";", columns.ToArray()));
                    }
                    tablerows.Clear();
                    System.IO.File.WriteAllLines(saveCSVFileDialog.FileName, lines.ToArray(), Encoding.Default);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }

        }

        private void frmTables_ResizeEnd(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.WindowMode)
                UpdateColumnWidths();
        }
    }

}
