using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using BaseServer;
using Points.Plugins;
using IniFiles.Net;

namespace RemXcs
{
    public partial class frmTrends : Form, IViewUpdate
    {
        Form DataHost;
        Form PanelHost;
        private int currGroup = 1;
        private int currPlace = 1;
        private Double minutes = 20;
        DateTime ofs = DateTime.Now;
        private int timesCount = 0;
        private bool checking = false;
        private bool Scrolling
        {
            get
            {
                return checking;
            }
            set
            {
                checking = value;
                if (checking)
                {
                    tsbAnchor.Image = Properties.Resources.on;
                    offset = DateTime.Now;
                    interval = offset - offset.AddMinutes(-minutes);
                    UpdateOffsetSelector();
                    timesCount = 0;
                    tsbNextTime.Enabled = false;
                }
                else
                    tsbAnchor.Image = Properties.Resources.off;
            }
        }

        private void UpdateOffsetSelector()
        {
            dtpOffset.ValueChanged -= dtpOffset_ValueChanged;
            dtpOffset.Value = offset;
            dtpOffset.ValueChanged += dtpOffset_ValueChanged;
        }

        private TimeSpan interval = new TimeSpan();

        private DateTime offset
        {
            get { return ofs; }
            set
            {
                if (ofs.CompareTo(value) > 0)
                {
                    timesCount++;
                    tsbNextTime.Enabled = true;
                }
                else if (ofs.CompareTo(value) < 0)
                {
                    timesCount--;
                    if (timesCount == 0) tsbNextTime.Enabled = false;
                }
                ofs = value;
            }
        }

        IUpdatePanel updater;
        Trends trends;
        bool overviewCall = false;
        List<Tuple<int, bool, string, string[]>> GroupsInfo =
            new List<Tuple<int, bool, string, string[]>>(); 

        public frmTrends(Form host, Form panelhost, int group = 0, int place = 0)
        {
            InitializeComponent();
            if (group > 0)
                currGroup = group;
            if (place > 0)
                currPlace = place;
            if (group > 0 && place > 0)
            {
                overviewCall = true;
            }
            updater = panelhost as IUpdatePanel;
            trends = new Trends();
            for (int i = 0; i < trends.Items.Length; i++)
            {
                trends.Items[i].Values = new SortedList<DateTime, double>();
                updateColorImage(i, trends.Items[i].Color);
            }
            lvTrends.SetDoubleBuffered(true);
            DataHost = host;
            PanelHost = panelhost;
            InitTrendsList();
            FillTrendsList();
        }

        private void FillTrendsList()
        {
            bool anyGroupExists = false;
            int j = 1;
            int n = 20;
            int GroupsCount = Properties.Settings.Default.GroupsCount;
            int PlaceCount = Data.PlaceCount(ParamGroup.Trend);
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, false)) // чтение удалённо
            {
                if (mySQL.Connected)
                {
                    for (int i = 1; i <= GroupsCount / n; i++)
                    {
                        ToolStripDropDownItem item =
                            (ToolStripDropDownItem)tsbGroupSelect.DropDownItems.Add(
                                String.Format("Группы с {0} по {1}", j, j + n - 1));
                        for (int k = j; k < j + n; k++)
                        {
                            Tuple<int, bool, string, string[]> groupInfo;
                            string[] placeNames = new string[PlaceCount];
                            for (int m = 0; m < placeNames.Length; m++) placeNames[m] = String.Empty;
                            if (!Data.GroupIsEmpty(k, mySQL, ParamGroup.Trend))
                            {
                                string grdesc = Data.GetGroupDesc(k, mySQL, ParamGroup.Trend);
                                for (int m = 0; m < placeNames.Length; m++) placeNames[m] =
                                    Data.GroupEntity(k, m + 1, ParamGroup.Trend);
                                groupInfo = new Tuple<int, bool, string, string[]>(k, true, grdesc, placeNames);
                                ToolStripItem groupItem = item.DropDownItems.Add(
                                    String.Format("Группа {0}{1}", k,
                                    String.IsNullOrWhiteSpace(grdesc) ? String.Empty : " - " + grdesc),
                                    null, miGroup_Click);
                                groupItem.ImageIndex = k;
                                anyGroupExists = true;
                                tsbPrevGroup.Enabled = true;
                            }
                            else
                                groupInfo = new Tuple<int, bool, string, string[]>(k, false, String.Empty, placeNames);
                            GroupsInfo.Add(groupInfo);
                        }
                        j += n;
                    }
                }
            }
            tsbNextGroup.Enabled = anyGroupExists;
            UpdateGroupCaptions();
        }

        private void InitTrendsList()
        {
            lvTrends.Items.Clear();
            for (int i = 0; i < Data.PlaceCount(ParamGroup.Trend); i++)
            {
                ListViewItem item = new ListViewItem(String.Format("{0}.", i + 1));
                item.ImageIndex = i;
                lvTrends.Items.Add(item);
                if (i + 1 == currPlace)
                {
                    lvTrends.FocusedItem = item;
                    item.Selected = true;
                }
                item.SubItems.Add(String.Empty); // имя тренда
                item.SubItems.Add(String.Empty); // текущее значение
                item.SubItems.Add(String.Empty); // ед.изм
                item.SubItems.Add(String.Empty); // шкала низ
                item.SubItems.Add(String.Empty); // шкала верх
                item.SubItems.Add(String.Empty); // дескриптор
            }
        }

        private void UpdateGroupCaptions()
        {
            cbInput.Visible = false;
            tsbGroupSelect.Text = String.Format("Группа {0}", currGroup);
            this.Text = String.Format("Просмотр графиков группы {0}", currGroup);
            int place = 1;
            int selected = -1;
            lvTrends.ItemChecked -= lvTrends_ItemChecked;
            trends.ResetColors();
            for (int i = 0; i < trends.Items.Length; i++)
            {
                int intcolor = (int)loadSettings(currGroup, i + 1, "TrendColor", -2);
                if (intcolor != -2)
                {
                    trends.Items[i].Color = Color.FromArgb(intcolor);
                }
                updateColorImage(i, trends.Items[i].Color);
            }
            foreach (ListViewItem item in lvTrends.Items)
            {
                item.Checked = false;
                string trendname = Data.GroupEntity(currGroup, place, ParamGroup.Trend);
                //string trendname = GroupsInfo[currGroup - 1].Item4[place - 1];
                string ptname = trendname.Split(new char[] {'.'})[0];
                string ptparam = (trendname.IndexOf('.') > 0) ?
                    trendname.Split(new char[] { '.' })[1] : "PV";
                string curvalue = String.Empty;
                string eu = String.Empty;
                string scalelow = String.Empty;
                string scalehigh = String.Empty;
                string desc = String.Empty;
                trends.Items[place - 1].TrendName = trendname;
                Entity e = Data.GetEntity(ptname);
                if (!e.Empty)
                {
                    bool visible = (bool)loadSettings(currGroup, place, "Visible", true);
                    item.Checked = visible;
                    trends.Items[place - 1].Visible = visible;
                    item.Tag = ptname;
                    int ptkind = (int)e.Values["PtKind"];
                    desc = (string)e.Values["PtDesc"];
                    switch (ptkind)
                    {
                        case PtKind.Analog:
                        case PtKind.Kontur:
                            int frm = (int)e.Values["FormatPV"];
                            trends.Items[place - 1].Format = frm;
                            switch (ptparam)
                            {
                                case "PV":
                                    eu = (string)e.Values["EUDesc"];
                                    decimal PVEUHi = Data.FloatEx((string)e.Values["PVEUHi"]);
                                    trends.Items[place - 1].HighRange = (float)PVEUHi;
                                    decimal PVEULo = Data.FloatEx((string)e.Values["PVEULo"]);
                                    trends.Items[place - 1].LowRange = (float)PVEULo;
                                    scalelow = Data.Float(PVEULo, frm);
                                    scalehigh = Data.Float(PVEUHi, frm);
                                    break;
                                case "SP":
                                    eu = (string)e.Values["EUDesc"];
                                    decimal SPEUHi = Data.FloatParse(e.Values["SPEUHi"].ToString());
                                    trends.Items[place - 1].HighRange = (float)SPEUHi;
                                    decimal SPEULo = Data.FloatParse(e.Values["SPEULo"].ToString());
                                    trends.Items[place - 1].LowRange = (float)SPEULo;
                                    scalelow = Data.Float(SPEULo, frm);
                                    scalehigh = Data.Float(SPEUHi, frm);
                                    break;
                                case "OP":
                                    eu = "%";
                                    decimal OPEUHi = Data.FloatParse(e.Values["OPEUHi"].ToString());
                                    trends.Items[place - 1].HighRange = (float)OPEUHi;
                                    decimal OPEULo = Data.FloatParse(e.Values["OPEULo"].ToString());
                                    trends.Items[place - 1].LowRange = (float)OPEULo;
                                    scalelow = Data.Float(OPEULo, frm);
                                    scalehigh = Data.Float(OPEUHi, frm);
                                    break;
                            }
                            break;
                        case PtKind.Digital:
                            scalelow = "0%"; //"дискретный";
                            scalehigh = "100%"; //String.Empty;;
                            trends.Items[place - 1].HighRange = 100;
                            trends.Items[place - 1].LowRange = 0;
                            trends.Items[place - 1].Format = 0;
                            break;
                    }
                }
                else
                {
                    item.Checked = false;
                    item.Tag = null;
                    trends.Items[place - 1].Visible = false;
                }
                int i = 0;
                foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                {
                    switch (i)
                    {
                        case 0: subitem.Text = String.Format("{0}.", place); break;
                        case 1: // имя тренда
                            subitem.Text = trendname;
                            GroupItem gi = Data.GroupItem(currGroup, item.Index + 1, ParamGroup.Trend);
                            gi.Group = currGroup;
                            gi.Place = item.Index + 1;
                            subitem.Tag = gi;
                            break;
                        case 2: subitem.Text = curvalue; break;  // текущее значение
                        case 3: subitem.Text = eu; break;  // ед.изм
                        case 4: subitem.Text = scalelow; break;  // шкала низ
                        case 5: subitem.Text = scalehigh; break;  // шкала верх
                        case 6: subitem.Text = desc; break;  // дескриптор
                        default: subitem.Text = String.Empty; break;
                    }
                    i++;
                }
                if (item.Selected && item.Checked) selected = place - 1;
                place++;
            }
            lvTrends.ItemChecked += lvTrends_ItemChecked;
            if (selected < 0)
                foreach (ListViewItem item in lvTrends.Items)
                    if (item.Checked)
                    {
                        selected = item.Index;
                        item.Selected = true;
                        lvTrends.FocusedItem = item;
                        break;
                    }
            if (selected >= 0)
            {
                SelectTrend(lvTrends.Items[selected].SubItems[1].Text);
            }
        }

        private bool resetTrends()
        {
            if (backDrawTrends.IsBusy)
            {
                backDrawTrends.CancelAsync();
                return true;
            }
            return false;
        }

        private void frmTrends_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            PrinterSettings ps = printDoc.PrinterSettings;
            ps.DefaultPageSettings.Landscape = true;
            printDoc.PrinterSettings = ps;
        }

        private void frmTrends_Shown(object sender, EventArgs e)
        {
            int sum = 0;
            for (int i = 0; i < lvTrends.Columns.Count - 1; i++) sum += lvTrends.Columns[i].Width;
            lvTrends.Columns[lvTrends.Columns.Count - 1].Width = lvTrends.ClientSize.Width - sum;
            lvTrends.Focus();
            //------------------------------------------------------------
            splitContainer1.SplitterDistance = (int)loadSettings(currGroup, 0, "SplitterDistance",
                splitContainer1.SplitterDistance);
            tsbTimeSelect.Text = (string)loadSettings(currGroup, 0, "TimeSelectText", "20 минут");
            minutes = (double)loadSettings(currGroup, 0, "minutes", 20.0);
            if (!overviewCall)
            {
                currPlace = (int)loadSettings(currGroup, 0, "currPlace", currPlace);
                if (currPlace < lvTrends.Items.Count - 1)
                {
                    lvTrends.FocusedItem = lvTrends.Items[currPlace - 1];
                    lvTrends.FocusedItem.Selected = true;
                }
            }
            Scrolling = true;
            updatebackTrendsValues(); // при смене диапазона выборки
        }

        private void miTimeSelect_Click(object sender, EventArgs e)
        {
            cbInput.Visible = false;
            ToolStripDropDownItem item = (ToolStripDropDownItem)sender;
            tsbTimeSelect.Text = item.Text;
            saveSettings(currGroup, 0, "TimeSelectText", item.Text);
            minutes = int.Parse((string)item.Tag);
            saveSettings(currGroup, 0, "minutes", minutes);
            Scrolling = true;
            updatebackTrendsValues(); // при смене диапазона выборки
        }

        private bool groupValid(int group)
        {
            int GroupsCount = Properties.Settings.Default.GroupsCount;
            if (group < 1 || group > GroupsCount)
                return false;
            else
                return GroupsInfo[group - 1].Item2;
        }

        private void miGroup_Click(object sender, EventArgs e)
        {
            ToolStripItem groupItem = (ToolStripItem)sender;
            currGroup = groupItem.ImageIndex;
            updater.UpdateCurrentGroupNo(currGroup);
            UpdateGroupCaptions();
            EmptyTrends();
            updatebackTrendsValues(); // при смене группы
            tsbPrevGroup.Enabled = groupValid(currGroup - 1);
            tsbNextGroup.Enabled = groupValid(currGroup + 1);
            Scrolling = true;
            first = true;
            tsbAnchor.Checked = false;
            tsbAnchor.Checked = true;
        }

        private void tsbNextGroup_Click(object sender, EventArgs e)
        {
            Scrolling = true;
            first = true;
            tsbPrevGroup.Enabled = true;
            int lastGroup = currGroup;
            int GroupsCount = Properties.Settings.Default.GroupsCount;
            while (currGroup++ < GroupsCount)
            {
                if (groupValid(currGroup))
                {
                    updater.UpdateCurrentGroupNo(currGroup);
                    UpdateGroupCaptions();
                    EmptyTrends();
                    updatebackTrendsValues(); // при смене группы вперед
                    return;
                }
            }
            currGroup = lastGroup;
            updater.UpdateCurrentGroupNo(currGroup);
            tsbNextGroup.Enabled = false;
        }

        private void tsbPrevGroup_Click(object sender, EventArgs e)
        {
            Scrolling = true;
            first = true;
            tsbNextGroup.Enabled = true;
            int lastGroup = currGroup;
            while (currGroup-- > 1)
            {
                if (groupValid(currGroup))
                {
                    updater.UpdateCurrentGroupNo(currGroup);
                    UpdateGroupCaptions();
                    EmptyTrends();
                    updatebackTrendsValues(); // при смене группы назад
                    return;
                }
            }
            currGroup = lastGroup;
            updater.UpdateCurrentGroupNo(currGroup);
            tsbPrevGroup.Enabled = false;
        }

        float kY = 1;
        float bY = 0;
        float pkY = 1;
        float pbY = 0;
        private void pbTrendView_Paint(object sender, PaintEventArgs e)
        {
            drawTrend(e.Graphics, pbTrendView.ClientRectangle);
        }

        private void drawTrend(Graphics g, RectangleF clientrect)
        {
            trends.DrawTrendInit(g, clientrect, kY, bY, pkY, pbY);
            // kX, bX 
            trends.DrawBottomAxis(g, offset, minutes);
            trends.DrawLeftAxis(g);
            trends.DrawRightAxis(g);
            DateTime DateHighRange = offset;
            DateTime DateLowRange = DateHighRange.AddMinutes(-minutes);
            foreach (Trend item in trends.Items)
            {
                if (item.Visible)
                    trends.DrawTrend(g, item, DateLowRange, DateHighRange);
            }
            // рисование вертикального среза
            if (inTrend && keyShift)
            {
                Rectangle r = trends.TrendRect();
                if (trends.TrendRect().Contains(new Point(mouseX, mouseY)))
                {
                    Point x1 = new Point(mouseX, r.Top);
                    Point x2 = new Point(mouseX, r.Top + r.Height);
                    g.DrawLines(Pens.Black, new Point[] { x1, x2 });
                    Double ps = (mouseX - r.X) * 1.0 / r.Width;
                    DateTime tt = DateLowRange.AddMinutes(minutes * ps);
                    updater.UpdateStatusMessage("Временной срез: " + tt.ToString());
                    ShowTimeSelection(tt);
                }
            }
            // рисование прямоугольника выбора
            if (inTrend && leftmouseDown)
            {
                CalcSelRect();
                if (mouseX > locFirst.X && mouseY > locFirst.Y)
                {
                    g.DrawRectangle(Pens.Silver, selRect);
                    g.DrawLine(Pens.Black, new Point(selRect.X, selRect.Y),
                        new Point(selRect.X + selRect.Width, selRect.Y));
                    g.DrawLine(Pens.Black, new Point(selRect.X, selRect.Y + selRect.Height),
                        new Point(selRect.X + selRect.Width, selRect.Y + selRect.Height));
                }
                else
                {
                    g.DrawLine(Pens.Red, new Point(selRect.X, selRect.Y),
                        new Point(selRect.X + selRect.Width, selRect.Y + selRect.Height));
                    g.DrawLine(Pens.Red, new Point(selRect.X, selRect.Y + selRect.Height),
                        new Point(selRect.X + selRect.Width, selRect.Y));
                }
            }
        }

        private void ShowTimeSelection(DateTime tt)
        {
            foreach (ListViewItem item in lvTrends.Items)
            {
                if (item.Tag != null)
                {
                    Trend trend = trends.Items[item.Index];
                    bool found = false;
                    bool kind = false;
                    Double value = 0;
                    foreach (KeyValuePair<DateTime, double> kvp in trend.Values)
                    {
                        DateTime dt = kvp.Key;
                        kind = !double.IsNaN(kvp.Value);
                        value = (kind) ? kvp.Value : 0;
                        if (dt >= tt)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found && kind)
                        item.SubItems[2].Text = value.ToString();
                    else
                        item.SubItems[2].Text = "нет данных";
                       
                }
            }
        }

        private void CalcSelRect()
        {
            if (mouseX > locFirst.X && mouseY > locFirst.Y)
                selRect.Size = new Size(mouseX - locFirst.X, mouseY - locFirst.Y);
            else if (mouseX < locFirst.X && mouseY > locFirst.Y)
            {
                selRect.Size = new Size(locFirst.X - mouseX, mouseY - locFirst.Y);
                selRect.X = mouseX;
            }
            else if (mouseX > locFirst.X && mouseY < locFirst.Y)
            {
                selRect.Size = new Size(mouseX - locFirst.X, locFirst.Y - mouseY);
                selRect.Y = mouseY;
            }
            else
            {
                selRect.Size = new Size(locFirst.X - mouseX, locFirst.Y - mouseY);
                selRect.X = mouseX;
                selRect.Y = mouseY;
            }
        }

        bool waitLoading = false;
        IDictionary<string, object> arg = null;
        private void updatebackTrendsValues(bool parthclear = false)
        {
            DateTime DateHighRange;
            DateTime DateLowRange;
            DateHighRange = offset;
            DateLowRange = offset - interval;
            arg = new Dictionary<string, object>();
            arg.Add("DateLowRange", DateLowRange);
            arg.Add("DateHighRange", DateHighRange);
            arg.Add("TrendsCount", trends.Items.Length);
            int index = 0;
            foreach (Trend item in trends.Items)
            {
                if (item.Visible) arg.Add(index.ToString(), item.TrendName);
                index++;
            }
            arg.Add("ParthClear", parthclear);
            arg.Add("Average", minutes > 20.0);
            waitLoading = resetTrends();
            if (!backDrawTrends.IsBusy) backDrawTrends.RunWorkerAsync(arg);
        }

        private void backDrawTrends_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            DrawTrendAsync(e.Argument, worker, e);
        }

        private void DrawTrendAsync(object argument, BackgroundWorker worker, DoWorkEventArgs e)        
        {
            IDictionary<string, object> arg = (IDictionary<string, object>)argument;
            DateTime DateLowRange = (DateTime)arg["DateLowRange"];
            DateTime DateHighRange = (DateTime)arg["DateHighRange"];
            int TrendsCount = (int)arg["TrendsCount"];
            bool parthclear = (bool)arg["ParthClear"];
            bool avg = (bool)arg["Average"];
            using (ServerSQL dataSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
            {
                using (ServerSQL fetchSQL = new ServerSQL(DatabaseFrom.Fetchbase, true)) // чтение
                {
                    if (dataSQL.Connected && fetchSQL.Connected)
                    {
                        for (int i = 0; i < TrendsCount; i++)
                        {
                            if (worker.CancellationPending) { e.Cancel = true; break; }
                            string trendindex = i.ToString();
                            if (arg.ContainsKey(trendindex))
                            {
                                string TrendName = (string)arg[trendindex];
                                SortedList<DateTime, double> values =
                                    Data.LoadTrend(TrendName, DateLowRange, DateHighRange, dataSQL, avg);
                                #region //Добавление последнего зачения опроса
                                //string ptname = TrendName.Split(new char[] { '.' })[0];
                                //IDictionary<string, string> realvals =
                                //    Data.GetRealValues(ptname, fetchSQL, dataSQL);
                                //if (realvals.ContainsKey("PV"))
                                //{
                                //    Double value;
                                //    if (Double.TryParse(realvals["PV"], out value))
                                //    {
                                //        DateTime dt = DateTime.Now;
                                //        if (!values.ContainsKey(dt)) values.Add(dt, value);
                                //    }
                                //}
                                #endregion
                                worker.ReportProgress(i, 
                                    new Tuple<bool, SortedList<DateTime, double>>(parthclear, values));
                            }
                        }
                    }
                }
            }
        }

        bool drawerror = false;
        private void backDrawTrends_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int index = e.ProgressPercentage;
            Tuple<bool, SortedList<DateTime, double>> args =
                (Tuple<bool, SortedList<DateTime, double>>)e.UserState;
            bool parthclear = args.Item1;
            SortedList<DateTime, double> values = args.Item2;

            if (!parthclear) trends.Items[index].Values.Clear();

            foreach (KeyValuePair<DateTime, double> kvp in values)
                if (!trends.Items[index].Values.ContainsKey(kvp.Key))
                {
                    trends.Items[index].Values.Add(kvp.Key, kvp.Value);
                }
          
            pbTrendView.Invalidate();
        }

        private void backDrawTrends_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                if (waitLoading && !backDrawTrends.IsBusy && arg != null)
                {
                    waitLoading = false;
                    backDrawTrends.RunWorkerAsync(arg);
                    updater.UpdateStatusMessage("Дочитывание трендов...");
                }
            }
            else
            {
                updater.UpdateStatusMessage("Готово");
                pbTrendView.Invalidate();
            }
        }

        private void tsbAnchor_Click(object sender, EventArgs e)
        {
            Scrolling = !Scrolling;
            if (Scrolling)
            {
                first = true;
                offset = DateTime.Now;
                interval = offset - offset.AddMinutes(-minutes);
                UpdateOffsetSelector();
                updatebackTrendsValues();
                lvTrends.Focus();
            }
        }

        private void pbTrendView_Resize(object sender, EventArgs e)
        {
            pbTrendView.Invalidate();
        }

        private void tsbPrevTime_Click(object sender, EventArgs e)
        {
            cbInput.Visible = false;
            offset = offset.AddMinutes(-minutes / 4);
            interval = offset - offset.AddMinutes(-minutes);
            UpdateOffsetSelector();
            updatebackTrendsValues(); // при листании диапазона назад
            Scrolling = false;
        }

        private void tsbNextTime_Click(object sender, EventArgs e)
        {
            cbInput.Visible = false;
            offset = offset.AddMinutes(minutes / 4);
            interval = offset - offset.AddMinutes(-minutes);
            UpdateOffsetSelector();
            updatebackTrendsValues(); // при листании диапазона вперед
        }

        private void timerUpdateList_Tick(object sender, EventArgs e)
        {
            if (!keyShift)
                UpdateRealTimeData();
        }

        private void UpdateRealTimeData()
        {
            foreach (ListViewItem item in lvTrends.Items)
            {
                if (item.Tag != null)
                {
                    string ptname = item.Tag.ToString();
                    IDictionary<string, string> realvals = Data.GetRealValues(ptname);
                    item.SubItems[2].Text = (realvals.ContainsKey("PVText")) ?
                        realvals["PVText"] : "нет данных";
                }
            }
        }

        private object loadSettings(int group, int place, string propname, object defvalue)
        {
            MemIniFile mif = new MemIniFile(String.Empty);
            mif.FromString(Properties.Settings.Default.TrendsConfig);
            string internalname = String.Format("G{0}P{1}", group, place);
            switch (propname)
            {
                case "TimeSelectText":
                    string text = mif.ReadString(internalname, propname, (string)defvalue);
                    return text;
                case "minutes":
                    double minutes = mif.ReadFloat(internalname, propname, (double)defvalue);
                    return minutes;
                case "TrendColor":
                case "currPlace":
                case "SplitterDistance":
                    int distance = mif.ReadInteger(internalname, propname, (int)defvalue);
                    return distance;
                case "Visible":
                case "Zoom":
                    bool visible = mif.ReadBool(internalname, propname, (bool)defvalue);
                    return visible;
        
            }
            return null;
        }

        private void saveSettings(int group, int place, string propname, object propvalue)
        {
            MemIniFile mif = new MemIniFile(String.Empty);
            mif.FromString(Properties.Settings.Default.TrendsConfig);
            string internalname = String.Format("G{0}P{1}", group, place);
            switch (propname)
            {
                case "TimeSelectText":
                    mif.WriteString(internalname, propname, (string)propvalue);
                    break;
                case "minutes":
                    mif.WriteFloat(internalname, propname, (double)propvalue);
                    break;
                case "TrendColor":
                case "currPlace":
                case "SplitterDistance":
                    mif.WriteInteger(internalname, propname, (int)propvalue);
                    break;
                case "Visible":
                case "Zoom":
                    mif.WriteBool(internalname, propname, (bool)propvalue);
                    break;
            //mif.WriteDate(internalname, "PrintTime", printReport.PrintTime);
            }
            Properties.Settings settings = Properties.Settings.Default;
            settings.TrendsConfig = mif.ToString();
            settings.Save();
        }

        private void resetSettings(int group, int place, string propname)
        {
            MemIniFile mif = new MemIniFile(String.Empty);
            mif.FromString(Properties.Settings.Default.TrendsConfig);
            string internalname = String.Format("G{0}P{1}", group, place);
            mif.DeleteKey(internalname, propname);
            Properties.Settings settings = Properties.Settings.Default;
            settings.TrendsConfig = mif.ToString();
            settings.Save();
        }

        private void lvTrends_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(e.Item.SubItems[1].Text)) e.Item.Checked = false;
            trends.Items[e.Item.Index].Visible = e.Item.Checked;
            saveSettings(currGroup, e.Item.Index + 1, "Visible", e.Item.Checked);
            pbTrendView.Invalidate();
        }

        private void lvTrends_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem item = lvTrends.FocusedItem;
            if (item != null)
            {
                currPlace = item.Index + 1;
                saveSettings(currGroup, 0, "currPlace", currPlace);
                if (!String.IsNullOrWhiteSpace(item.SubItems[1].Text))
                {
                    SelectTrend(item.SubItems[1].Text);
                    UnzoomTrend();
                }
            }
        }

        private void SelectTrend(string trendname)
        {
            for (int i = 0; i < trends.Items.Length; i++)
            {
                trends.Items[i].Selected = false;
                if (trends.Items[i].TrendName == trendname)
                {
                    trends.Items[i].Selected = true;
                    trends.LowRange = trends.Items[i].LowRange;
                    trends.HighRange = trends.Items[i].HighRange;
                }
            }
            pbTrendView.Invalidate();
        }

        private void frmTrends_FormClosing(object sender, FormClosingEventArgs e)
        {
            IUpdatePanel updater = PanelHost as IUpdatePanel;
            if (updater != null && drawerror && actived)
                updater.UpdateStatusMessage("Ошибка отрисовки тренда!");
            if (backDrawTrends.IsBusy) backDrawTrends.CancelAsync();
        }

        bool first = false;
        public void UpdateView() // вызывается через интерфейс IViewUpdate
        {
            if (Scrolling || first)
            {
                DateTime now = DateTime.Now;
                interval = (first) ? now - now.AddMinutes(-minutes) : now - offset;
                first = false;
                offset = now;
                timesCount = 0;

                EmptyParthTrends();

                updatebackTrendsValues(true); // при автоматическом обновлении
                tsbNextTime.Enabled = false;
                UpdateOffsetSelector();
            }
        }

        private void EmptyParthTrends()
        {
            for (int index = 0; index < trends.Items.Length; index++)
            {
                List<int> list = new List<int>();
                int n = 0;
                // накопление в списке list индексов n, не входящих в интервал offset-minutes..offset
                foreach (KeyValuePair<DateTime, double> kvp in trends.Items[index].Values)
                {
                    if ((kvp.Key < offset.AddMinutes(-minutes)) ||
                        (kvp.Key > offset)) list.Add(n);
                    n++;
                }
                // удаление из списков Values по индексам в списке list
                foreach (int k in list)
                    if (k < trends.Items[index].Values.Count)
                        trends.Items[index].Values.RemoveAt(k);
            }
        }

        private void EmptyTrends()
        {
            for (int index = 0; index < trends.Items.Length; index++)
            {
                trends.Items[index].Values.Clear();
            }
        }

        bool actived = false;
        private void frmTrends_Enter(object sender, EventArgs e)
        {
            actived = true;
        }

        private void frmTrends_Leave(object sender, EventArgs e)
        {
            actived = false;
        }

        bool inTrend = false;
        private void pbTrendView_MouseEnter(object sender, EventArgs e)
        {
            inTrend = true;
        }

        private void pbTrendView_MouseLeave(object sender, EventArgs e)
        {
            inTrend = false;
        }

        Rectangle selRect = new Rectangle();
        Point locFirst = new Point();
        bool leftmouseDown = false;
        bool rightmouseDown = false;
        private void pbTrendView_MouseDown(object sender, MouseEventArgs e)
        {
            leftmouseDown = (e.Button == MouseButtons.Left);
            rightmouseDown = (e.Button == MouseButtons.Right);
            locFirst = new Point(e.X, e.Y);
            selRect.Location = new Point(e.X, e.Y);
            selRect.Size = new Size();
        }

        int mouseX = 0;
        int mouseY = 0;
        private void pbTrendView_MouseMove(object sender, MouseEventArgs e)
        {
            if (rightmouseDown)
            {
                ScrollTrend(e.X - mouseX, e.Y - mouseY);
            }
            mouseX = e.X;
            mouseY = e.Y;
            pbTrendView.Invalidate();
        }

        private void pbTrendView_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftmouseDown)
            {
                leftmouseDown = false;
                if (e.X > locFirst.X && e.Y > locFirst.Y)
                    ZoomTrend();
                else
                    UnzoomTrend();
                pbTrendView.Invalidate();
            }
            if (rightmouseDown)
            {
                rightmouseDown = false;
            }
        }

        private void ZoomTrend()
        {
            CalcSelRect();
            Rectangle tr = trends.TrendRect();
            if (tr.Size.Width > 0 && tr.Size.Height > 0)
            {
                // для левой оси
                Single Hi = trends.pHighRange;
                Single Lo = trends.pLowRange;
                if (pkY > 0.0001)
                {
                    pkY *= selRect.Size.Height * 1F / tr.Size.Height;
                    Single bkY = (tr.Bottom - selRect.Bottom) * 1F / tr.Size.Height;
                    pbY += (Hi - Lo) * bkY;
                    // для правой оси
                    Hi = trends.HighRange;
                    Lo = trends.LowRange;
                    kY *= selRect.Size.Height * 1F / tr.Size.Height;
                    bkY = (tr.Bottom - selRect.Bottom) * 1F / tr.Size.Height;
                    bY += (Hi - Lo) * bkY;
                }
            }
        }

        private void UnzoomTrend()
        {
            kY = 1;
            bY = 0;
            pkY = 1;
            pbY = 0;
        }

        private void ScrollTrend(int deltaX, int deltaY)
        {
            CalcSelRect();
            Rectangle tr = trends.TrendRect();
            if (tr.Size.Width > 0 && tr.Size.Height > 0)
            {
                // для левой оси
                Single Hi = trends.pHighRange;
                Single Lo = trends.pLowRange;
                Single bkY = deltaY * 1F / tr.Size.Height;
                pbY += (Hi - Lo) * bkY;
                // для правой оси
                Hi = trends.HighRange;
                Lo = trends.LowRange;
                bkY = deltaY * 1F / tr.Size.Height;
                bY += (Hi - Lo) * bkY;
            }
        }

        bool keyShift = false;
        bool keyCtrl = false;
        bool keyAlt = false;
        private void frmTrends_KeyDown(object sender, KeyEventArgs e)
        {
            keyShift = e.Shift;
            keyCtrl = e.Control;
            keyAlt = e.Alt;
            pbTrendView.Invalidate();
        }

        private void frmTrends_KeyUp(object sender, KeyEventArgs e)
        {
            keyShift = e.Shift;
            keyCtrl = e.Control;
            keyAlt = e.Alt;
            pbTrendView.Invalidate();
            if (!keyShift)
            {
                updater.UpdateStatusMessage("Нет сообщений");
                UpdateRealTimeData();
            }
        }

        frmPrintPreview form = null;
        private void tsbPreview_Click(object sender, EventArgs e)
        {
            string Caption = "Предварительный просмотр";
            updater.CloseAndRemove(Caption);
            form = new frmPrintPreview();
            updater.ChildForms().Add(form);
            form.printDoc.QueryPageSettings += printDoc_QueryPageSettings;
            form.printDoc.PrintPage += printDoc_PrintPage;
            form.Text = Caption;
            form.MdiParent = PanelHost;
            form.Show();
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
            DateTime DateHighRange = offset;
            DateTime DateLowRange = DateHighRange.AddMinutes(-minutes);
            string printdate = "Отпечатано: " + DateTime.Now.ToString();
            string printname = String.Format("Время выборки: с {0} до {1}", DateLowRange,
                DateHighRange);
            SizeF strSize;
            PointF strPoint;
            using (Font printfont = new Font("Arial", 8, FontStyle.Italic))
            {
                strSize = e.Graphics.MeasureString(sysinfo, printfont);
                strPoint = prnrect.Location;
                strPoint.Y -= strSize.Height;
                e.Graphics.DrawString(sysinfo, printfont, Brushes.Black, strPoint);
                strSize = e.Graphics.MeasureString(printdate, printfont);
                strPoint = prnrect.Location;
                strPoint.Y += prnrect.Height;
                e.Graphics.DrawString(printdate, printfont, Brushes.Black, strPoint);
                strSize = e.Graphics.MeasureString(printname, printfont);
                strPoint = prnrect.Location;
                strPoint.X += prnrect.Width - strSize.Width;
                strPoint.Y += prnrect.Height;
                e.Graphics.DrawString(printname, printfont, Brushes.Black, strPoint);
            }
            Single sumHeight = 10;
            string tabletext = "Xy";
            using (Font printfont = new Font("Arial Narrow", 8, FontStyle.Regular))
            {
                Single strHeight = e.Graphics.MeasureString(tabletext, printfont).Height;
                foreach (ListViewItem lvi in lvTrends.Items)
                {
                    if (lvi.Checked)
                        sumHeight += strHeight;
                }
                sumHeight += strHeight; // добавляем высоту на заголовок
                strPoint.Y = prnrect.Y + prnrect.Height - sumHeight + 5;
                // Печать заголовка таблицы
                strPoint.X = prnrect.X;
                using (Font headerfont = new Font("Arial Narrow", 8, FontStyle.Underline))
                {
                    foreach (ColumnHeader ch in lvTrends.Columns)
                    {
                        tabletext = ch.Text;
                        strSize = e.Graphics.MeasureString(tabletext, headerfont);
                        PointF pch = strPoint;
                        switch (ch.Index)
                        {
                            case 2:
                            case 4:
                            case 5: pch.X += ch.Width - strSize.Width - 5; break;
                        }
                        e.Graphics.DrawString(tabletext, headerfont, Brushes.Black, pch);
                        strPoint.X += ch.Width;
                    }
                }
                strPoint.Y += strHeight;
                int row = 0;
                // Печать строк таблицы
                foreach (ListViewItem lvi in lvTrends.Items)
                {
                    strPoint.X = prnrect.X;
                    if (lvi.Checked)
                    {
                        if (row % 2 != 0)
                            using (SolidBrush brush = new SolidBrush(Color.FromArgb(240, 240, 240)))
                                e.Graphics.FillRectangle(brush,
                                    strPoint.X, strPoint.Y, prnrect.Width, strHeight);
                        row++;
                        foreach (ColumnHeader ch in lvTrends.Columns)
                        {
                            tabletext = lvi.SubItems[ch.Index].Text;
                            strSize = e.Graphics.MeasureString(tabletext, printfont);
                            PointF pch = strPoint;
                            switch (ch.Index)
                            {
                                case 2:
                                case 4:
                                case 5: pch.X += ch.Width - strSize.Width - 5; break;
                            }
                            e.Graphics.DrawString(tabletext, printfont, Brushes.Black, pch);
                            strPoint.X += ch.Width;
                        }
                        strPoint.Y += strHeight;
                    }
                }
            }
            prnrect.Height -= sumHeight;
            drawTrend(e.Graphics, prnrect);
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog(this) == DialogResult.OK)
            {
                printDoc.Print();
            }
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

        private void listMenu_Opening(object sender, CancelEventArgs e)
        {
            int xoffset = Control.MousePosition.X;
            int sum = 0;
            bool found = false;
            foreach (ColumnHeader header in GetOrderedHeaders(lvTrends))
            {
                sum += header.Width;
                if (sum > xoffset)
                {
                    ListViewItem lvi = lvTrends.FocusedItem;
                    // выбор столбца цвета                    
                    tsmiChangeColor.Visible = false;
                    tsmiResetColor.Visible = false;
                    // выбор столбца тега
                    tsmiChangeTag.Visible = false;
                    tsmiEmptyTag.Visible = false;
                    switch (header.Index)
                    {
                        case 0:
                            tsmiChangeColor.Visible = true;
                            tsmiResetColor.Visible =
                                (trends.GetDefaultTrendColor(lvi.Index) != trends.Items[lvi.Index].Color);
                            listMenu.Tag = lvi.Index;
                            found = true;
                            break;
                        case 1:
                            tsmiChangeTag.Visible = true;
                            if ((lvi != null) && (header.Index < lvi.SubItems.Count) &&
                                (lvi.SubItems[header.Index].Text.Length > 0))
                                tsmiEmptyTag.Visible = true;
                            else
                                tsmiEmptyTag.Visible = false;
                            listMenu.Tag = lvi.SubItems[header.Index].Tag;
                            found = true;
                            break;
                        default:
                            e.Cancel = true;
                            found = true;
                            break;
                    }
                }
                if (found) break;
            }
        }

        string lastPtName = String.Empty;
        string lastPtParam = String.Empty;
        private void tsmiChangeTag_Click(object sender, EventArgs e)
        {
            if (lvTrends.FocusedItem != null)
            {
                string[] args = ((GroupItem)listMenu.Tag).Caption.Split(new char[] { '.' });
                string PtName = (args[0].Length > 0) ? args[0] : lastPtName;
                string PtParam = (args.Length > 1) ? args[1].ToString() : lastPtParam;
                GroupItem gi = (GroupItem)listMenu.Tag;
                using (frmEntitySelector form =
                    new frmEntitySelector(PtName, PtParam, PointSelector.TrendPoints))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        lastPtName = form.EntityName;
                        lastPtParam = form.EntityParam;
                        updateGroupItem(gi, lastPtName, lastPtParam);
                        UpdateGroupCaptions();
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
            Data.SaveGroupItem(gitem, ParamGroup.Trend);
        }

        private void tsmiEmptyTag_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Удалить связь?",
                "Удаление связи", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) ==
                DialogResult.OK)
            {
                string[] args = ((GroupItem)listMenu.Tag).Caption.Split(new char[] { '.' });
                string PtName = args[0];
                string PtParam = (args.Length > 1) ? args[1].ToString() : String.Empty;
                GroupItem gi = (GroupItem)listMenu.Tag;
                updateGroupItem(gi, String.Empty, String.Empty);
                UpdateGroupCaptions();
            }
        }

        private void dtpOffset_ValueChanged(object sender, EventArgs e)
        {
            offset = dtpOffset.Value;
            interval = offset - offset.AddMinutes(-minutes);
            updatebackTrendsValues(); // при задании диапазона напрямую
            Scrolling = false;
        }

        private void tsmiChangeColor_Click(object sender, EventArgs e)
        {
            if (listMenu.Tag != null)
            {
                int index = (int)listMenu.Tag;
                if (index >= 0 && index < ilColors.Images.Count)
                {
                    colorDialog.Color = trends.GetTrendColor(index);
                    if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Color color = colorDialog.Color;
                        trends.SetTrendColor(index, color);
                        pbTrendView.Invalidate();
                        updateColorImage(index, color);
                        saveSettings(currGroup, index + 1, "TrendColor", color.ToArgb());
                    }
                }
            }
        }

        private void updateColorImage(int index, Color color)
        {
            using (Bitmap bmp = new Bitmap(16, 16))
            {
                Graphics g = Graphics.FromImage(bmp);
                using (SolidBrush brush = new SolidBrush(color))
                    g.FillRectangle(brush, new Rectangle(0, 0, 16, 16));
                ilColors.Images[index] = bmp;
            }
        }

        private void tsmiResetColor_Click(object sender, EventArgs e)
        {
            if (listMenu.Tag != null)
            {
                int index = (int)listMenu.Tag;
                if (index >= 0 && index < ilColors.Images.Count)
                {
                    Color color = trends.ResetTrendColor(index);
                    pbTrendView.Invalidate();
                    updateColorImage(index, color);
                    resetSettings(currGroup, index + 1, "TrendColor");
                    lvTrends.Invalidate();
                }
            }
        }

        private void splitContainer1_MouseUp(object sender, MouseEventArgs e)
        {
            saveSettings(currGroup, 0, "SplitterDistance", splitContainer1.SplitterDistance);
        }

        private void lvTrends_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ListViewItem lvi = lvTrends.GetItemAt(e.X, e.Y);
                if (lvi != null && lvi.SubItems.Count > 2 &&
                    lvi.SubItems[1].Text.Length > 0 && lvi.Checked)
                {
                    ListViewItem.ListViewSubItem slvi = lvi.GetSubItemAt(e.X, e.Y);
                    if (slvi != null)
                    {
                        Rectangle rect = slvi.Bounds;
                        rect.Offset(-1, 1);
                        int i = lvi.SubItems.IndexOf(slvi);
                        cbInput.Visible = (i == 4 || i == 5); // 4 - низ шкалы; 5 - верх шкалы
                        if (cbInput.Visible)
                        {
                            cbInput.Focus();
                            cbInput.Bounds = rect;
                            cbInput.Text = slvi.Text;
                            cbInput.SelectAll();
                            cbInput.Tag = i;
                        }
                    }
                }
            }
        }

        private void cbInput_Leave(object sender, EventArgs e)
        {
            cbInput.Visible = false;
        }

        private void cbInput_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbInput.Visible = false;
            changeValue();
        }

        private void cbInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                cbInput.Visible = false;
            else
                if (e.KeyCode == Keys.Enter)
                {
                    cbInput.Visible = false;
                    changeValue();
                }
        }
        /*
                    string trendname = trends.Items[place - 1].TrendName;
                    string ptname = trendname.Split(new char[] {'.'})[0];
                    Entity e = Data.GetEntity(ptname);
                    if (!e.Empty)
                    {
                        bool visible = (bool)loadSettings(currGroup, place, "Visible", true);
                        item.Checked = visible;
                        trends.Items[place - 1].Visible = visible;
                        item.Tag = ptname;
                        int ptkind = (int)e.Values["PtKind"];
                        desc = (string)e.Values["PtDesc"];
                        switch (ptkind)
                        {
                            case PtKind.Analog:
                                eu = (string)e.Values["EUDesc"];
                                decimal PVEUHi = Data.FloatEx((string)e.Values["PVEUHi"]);
                                trends.Items[place - 1].HighRange = (float)PVEUHi;
                                decimal PVEULo = Data.FloatEx((string)e.Values["PVEULo"]);
                                trends.Items[place - 1].LowRange = (float)PVEULo;
         */

        private void changeValue()
        {
            ListViewItem lvi = lvTrends.FocusedItem;
            float value;
            if (lvi != null && cbInput.Tag != null &&
                float.TryParse(cbInput.Text, out value))
            {
                int i = (int)cbInput.Tag;
                switch (i)
                {
                    case 4: // низ шкалы
                        saveSettings(currGroup, lvi.Index + 1, "LowRange", value);
                        trends.Items[lvi.Index].LowRange = value;
                        trends.LowRange = value;
                        lvi.SubItems[i].Text = value.ToString();
                        pbTrendView.Invalidate();
                        break;
                    case 5: // верх шкалы
                        saveSettings(currGroup, lvi.Index + 1, "HighRange", value);
                        trends.Items[lvi.Index].HighRange = value;
                        trends.HighRange = value;
                        lvi.SubItems[i].Text = value.ToString();
                        pbTrendView.Invalidate();
                        break;
                }
            }
        }
    }
}