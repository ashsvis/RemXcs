using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BaseServer;
using Draws.Plugins;
using Points.Plugins;

namespace RemXcs
{
    public partial class frmOverview : Form
    {
        List<Draw> drawslist = new List<Draw>();
        IDictionary<string, List<Draw>> fetchlist = new Dictionary<string, List<Draw>>();
        string SchemeName = "NONAME";
        IUpdatePanel updater;
        Form PanelHost;
        public frmOverview(Form parent)
        {
            InitializeComponent();
            this.MouseWheel += drawBox_MouseWheel;
            PanelHost = parent;
            updater = parent as IUpdatePanel;
        }

        public bool LoadScheme(string schemename)
        {
            if (backgroundFetch.IsBusy) backgroundFetch.CancelAsync();
            drawBox.Location = new Point(0, 0);
            Size pnlSize = updater.GetPanelSize();
            drawBox.Width = pnlSize.Width; 
            drawBox.Height = pnlSize.Height; 
            if (Data.SchemeExists(schemename))
            {
                timerFetch.Enabled = false;
                Cursor = Cursors.WaitCursor;
                SchemeName = schemename;
                drawslist = DrawPlugin.LoadScheme(
                    SchemeName, Application.StartupPath + "\\images\\");
                if (drawslist.Count > 0)
                {
                    Background bkg = (Background)drawslist[0];
                    drawBox.BackColor = bkg.BackColor;
                    this.BackColor = bkg.BackColor;
                    this.Text = bkg.Descriptor;
                    if (updater != null) updater.UpdateCaptionText(this.Text);
                    this.MaximumSize = new System.Drawing.Size();
                    this.WindowState = FormWindowState.Maximized;
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.MaximizeBox = true;
                }
                foreach (Draw drw in drawslist)
                {
                    if (drw.Props.ContainsKey("Quality"))
                        drw.Props["Quality"] = "GOOD";
                    else
                        drw.Props.Add("Quality", "GOOD");
                }
                fetchlist.Clear();
                foreach (Draw drw in drawslist)
                {
                    if (drw.Props.ContainsKey("PtName") &&
                        !String.IsNullOrWhiteSpace((string)drw.Props["PtName"]))
                    {
                        string ptname = (string)drw.Props["PtName"];
                        if (fetchlist.ContainsKey(ptname))
                            fetchlist[ptname].Add(drw);
                        else
                            fetchlist.Add(ptname, new List<Draw>() { drw });
                    }
                }
                LoadFirst();
                FetchBase();
                drawBox.Refresh();
                pnlScroll.VerticalScroll.Value = pnlScroll.HorizontalScroll.Value = 0;
                if (updater != null) updater.UpdateCurrentSchemeName(SchemeName, this.Text);
                timerFetch.Enabled = true;
                Cursor = Cursors.Default;
                return true;
            }
            else
                return false;
        }

        private bool controlPressed = false;
        private void frmOverview_KeyDown(object sender, KeyEventArgs e)
        {
            controlPressed = e.Control;
        }

        private void frmOverview_KeyUp(object sender, KeyEventArgs e)
        {
            controlPressed = e.Control;
        }

        private void drawBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (drawslist.Count > 0)
            {
                Background bkg = (Background)drawslist[0];
                if (!bkg.Expanded)
                {
                    int step = 32;
                    if (e.Delta > 0)
                    {
                        Point pos = drawBox.Location;
                        if (controlPressed)
                        {
                            if (drawBox.Width > pnlScroll.Width)
                            {
                                pos.X += step;
                                if (pos.X > 0) pos.X = 0;
                            }
                        }
                        else
                        {
                            if (drawBox.Height > pnlScroll.Height)
                            {
                                pos.Y += step;
                                if (pos.Y > 0) pos.Y = 0;
                            }
                        }
                        drawBox.Location = pos;
                    }
                    if (e.Delta < 0)
                    {
                        Point pos = drawBox.Location;
                        if (controlPressed)
                        {
                            if (drawBox.Width > pnlScroll.Width)
                            {
                                pos.X -= step;
                                if (pnlScroll.Width - pos.X > drawBox.Width)
                                    pos.X = pnlScroll.Width - drawBox.Width;
                            }
                        }
                        else
                        {
                            if (drawBox.Height > pnlScroll.Height)
                            {
                                pos.Y -= step;
                                if (pnlScroll.Height - pos.Y > drawBox.Height)
                                    pos.Y = pnlScroll.Height - drawBox.Height;
                            }
                        }
                        drawBox.Location = pos;
                    }
                }
                else
                {
                    drawBox.Location = new Point();
                }
            }
        }

        private Point ScalePoint(Rectangle source, int width, int height, Point point)
        {
            if (drawslist.Count > 0)
            {
                Background bkg = (Background)drawslist[0];
                if (bkg.Expanded)
                {
                    if (bkg.SaveAspect)
                    {
                        Rectangle scaledRect = ScaleRect(source, width, height);
                        float kX = source.Width * 1f / scaledRect.Width;
                        float kY = source.Height * 1f / scaledRect.Height;
                        Point descPoint = new Point(
                            (int)(point.X * kX - (width - scaledRect.Width) * kX / 2f),
                            (int)(point.Y * kY - (height - scaledRect.Height) * kY / 2f));
                        return descPoint;
                    }
                    else
                    {
                        float kX = source.Width * 1f / width;
                        float kY = source.Height * 1f / height;
                        Point descPoint = new Point(
                            (int)(point.X * kX),
                            (int)(point.Y * kY));
                        return descPoint;
                    }
                }
                else
                    return point;
            }
            else
                return point;
        }

        static Rectangle ScaleRect(Rectangle source, int width, int height)
        {
            Rectangle desc;

            float srcwidth = source.Width;
            float srcheight = source.Height;
            float dstwidth = width;
            float dstheight = height;

            if (srcwidth <= dstwidth && srcheight <= dstheight)  // Исходное меньше целевого
            {
                int left = (width - source.Width) / 2;
                int top = (height - source.Height) / 2;
                desc = new Rectangle(left, top, source.Width, source.Height);
            }
            else if (srcwidth / srcheight > dstwidth / dstheight)  // Пропорции исходного более широкие
            {
                float cy = srcheight / srcwidth * dstwidth;
                float top = ((float)dstheight - cy) / 2.0f;
                if (top < 1.0f) top = 0;
                desc = new Rectangle(0, (int)top, (int)dstwidth, (int)cy);
            }
            else  // Пропорции исходного более узкие
            {
                float cx = srcwidth / srcheight * dstheight;
                float left = ((float)dstwidth - cx) / 2.0f;
                if (left < 1.0f) left = 0;
                desc = new Rectangle((int)left, 0, (int)cx, (int)dstheight);
            }
            return desc;
        }

        /// <summary>
        /// Масштабирование картинки до заданного размера.
        /// </summary>
        /// <param name="source"> Исходное изображение. </param>
        /// <param name="width"> Ширина целевого изображения. </param>
        /// <param name="height"> Высота целевого изображения. </param>
        /// <param name="color"> Цвет заполнения внешних полей. </param>
        /// <returns> Масштабированное изображение. </returns>
        static Image ScaleImage(Image source, int width, int height, Color color)
        {
            Image dest = new Bitmap(width, height);
            using (Graphics gr = Graphics.FromImage(dest))
            {
                using (SolidBrush brush = new SolidBrush(color))
                    gr.FillRectangle(brush, 0, 0, width, height);  // Очищаем экран
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                float srcwidth = source.Width;
                float srcheight = source.Height;
                float dstwidth = width;
                float dstheight = height;

                if (srcwidth <= dstwidth && srcheight <= dstheight)  // Исходное изображение меньше целевого
                {
                    int left = (width - source.Width) / 2;
                    int top = (height - source.Height) / 2;
                    gr.DrawImage(source, left, top, source.Width, source.Height);
                }
                else if (srcwidth / srcheight > dstwidth / dstheight)  // Пропорции исходного изображения более широкие
                {
                    float cy = srcheight / srcwidth * dstwidth;
                    float top = ((float)dstheight - cy) / 2.0f;
                    if (top < 1.0f) top = 0;
                    gr.DrawImage(source, 0, top, dstwidth, cy);
                }
                else  // Пропорции исходного изображения более узкие
                {
                    float cx = srcwidth / srcheight * dstheight;
                    float left = ((float)dstwidth - cx) / 2.0f;
                    if (left < 1.0f) left = 0;
                    gr.DrawImage(source, left, 0, cx, dstheight);
                }

                return dest;
            }
        }


        private void drawBox_Paint(object sender, PaintEventArgs e)
        {
            if (drawslist.Count > 0)
            {
                Background bkg = (Background)drawslist[0];
                if (bkg.Expanded)
                {
                    using (Bitmap bmp = new Bitmap(drawBox.Width, drawBox.Height))
                    {
                        Graphics g = Graphics.FromImage(bmp);
                        Rectangle rect = new Rectangle(0, 0, drawBox.Width, drawBox.Height);
                        using (SolidBrush brush = new SolidBrush(drawBox.BackColor))
                            g.FillRectangle(brush, rect);
                        // Отрисовка массива фигур
                        foreach (Draw drw in drawslist) drw.DrawFigure(g);
                        if (bkg.SaveAspect)
                            e.Graphics.DrawImage(ScaleImage(bmp, pnlScroll.Size.Width,
                                pnlScroll.Size.Height, drawBox.BackColor), 0, 0);
                        else
                            e.Graphics.DrawImage(bmp, pnlScroll.ClientRectangle, rect,
                                GraphicsUnit.Pixel);
                    }
                }
                else
                {
                    Graphics g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    // Отрисовка массива фигур
                    foreach (Draw drw in drawslist) drw.DrawFigure(g);
                }
            }
        }

        private Draw GetDrawAt(int X, int Y)
        {
            Point calcPoint = ScalePoint(drawBox.ClientRectangle,
                        pnlScroll.Size.Width, pnlScroll.Size.Height, new Point(X, Y));
            for (int i = drawslist.Count - 1; i >= 0; i--)
            {
                Draw drw = drawslist[i];
                RectangleF rect = new RectangleF(drw.Location, drw.Size);
                if (rect.Contains(calcPoint)) return drw;
            }
            return null;
        }

        string lastdrw = String.Empty;
        private void drawBox_MouseMove(object sender, MouseEventArgs e)
        {           
            Draw drw = GetDrawAt(e.X, e.Y);
            if (drw != null && !drw.Name.Equals("Background"))
            {
                bool isJump = drw.Plugin.PluginShortType.Equals("DinJump") &&
                    !String.IsNullOrWhiteSpace((string)drw.Props["ScreenName"]);
                bool isButton = drw.Plugin.PluginShortType.Equals("DinButton") &&
                    !String.IsNullOrWhiteSpace((string)drw.Props["PtName"]);
                int currlevel = (updater != null) ?
                    UserInfo.UserLevelToInt(updater.UserLevel()) : 0;
                if (isJump && (currlevel >= (int)drw.Props["UserLevel"]) ||
                    isButton && currlevel >= UserInfo.UserLevelToInt(UserLevel.Оператор))
                    Cursor = Cursors.Hand;
                else
                    Cursor = Cursors.Default;
                if (drw.Props.ContainsKey("PtName"))
                {
                    string ptname = drw.Props["PtName"].ToString().Trim();
                    if (ptname.Length > 0 && !lastdrw.Equals(drw.Name))
                    {
                        lastdrw = drw.Name;
                        Entity ent = Data.GetEntity(ptname);
                        if (!ent.Empty)
                        {
                            toolTip.Hide(drawBox);
                            toolTip.Show(ptname + " - " + ent.Values["PtDesc"], drawBox, e.X, e.Y, 1500);
                        }
                    }
                }
                else
                    lastdrw = String.Empty;
            }
            else
            {
                toolTip.Hide(drawBox);
                Cursor = Cursors.Default;
                lastdrw = String.Empty;
            }
        }

        private void drawBox_MouseDown(object sender, MouseEventArgs e)
        {
            Draw drw = GetDrawAt(e.X, e.Y);
            if (drw != null)
            {
                if (e.Button == MouseButtons.Left)
                {  
                    if (drw.Plugin != null)
                    switch (drw.Plugin.PluginShortType)
                    {
                        case "DinJump":
                            string screenname = "ScreenName";
                            int dinlevel = (int)drw.Props["UserLevel"];
                            int currlevel = (updater != null) ?
                                UserInfo.UserLevelToInt(updater.UserLevel()) : 0; 
                            if (drw.Props.ContainsKey(screenname) &&
                                drw.Props[screenname].ToString().Trim().Length > 0 &&
                                currlevel >= dinlevel)
                            {
                                // нажат переход на другую мнемосхему
                                if (controlPressed)
                                {  // если нажат контрол, то открыть в новом окне
                                    if (updater != null) 
                                        updater.LoadScheme((string)drw.Props[screenname]);
                                    controlPressed = false;
                                }
                                else
                                    LoadScheme((string)drw.Props[screenname]);
                            }
                            break;
                        case "DinButton":
                            if (updater != null && 
                                updater.UserLevel() >= UserLevel.Оператор)
                            {
                                string keyname = "PtName";
                                if (drw.Props.ContainsKey(keyname) &&
                                    drw.Props[keyname].ToString().Trim().Length > 0)
                                {
                                    string ptname = drw.Props[keyname].ToString().Trim();
                                    bool askuser = (bool)drw.Props["Direct"];
                                    bool fixbutton = (bool)drw.Props["Fixed"];
                                    string commcode = fixbutton ? "REVERSE" : "CLICK";
                                    Entity ent = Data.GetEntity(ptname);
                                    if (!ent.Empty)
                                    {
                                        if (!askuser || askuser &&
                                            MessageBox.Show(this,
                                            (string)ent.Values["PtDesc"] + ".\nВыполнить?",
                                            "Станция RemX", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            ent.Reals = Data.GetRealValues(ptname);
                                            ent.SetRealProp("Command", commcode);
                                            UpdateRealValue("Command", ptname, ent.Reals);
                                        }
                                    }
                                }
                            }
                            break;
                        default:
                            if (e.Clicks == 2 && drw.Props.ContainsKey("PtName"))
                            {
                                string ptname = (string)drw.Props["PtName"];
                                Entity ent = Data.GetEntity(ptname);
                                if (!ent.Empty)
                                    ShowPassport(ent);
                            }
                            break;
                    }
                }
                // вызов контектсного меню
                string propname = "PtName";
                if (e.Button == MouseButtons.Right)
                {
                    toolTip.Hide(drawBox);
                    Cursor = Cursors.Default;
                    if (drw.Props.ContainsKey(propname))
                    {
                        string ptname = (string)drw.Props[propname];
                        if (!String.IsNullOrWhiteSpace(ptname))
                        {
                            miPassport.Text = "Паспорт для " + ptname;
                            Entity ent = Data.GetEntity(ptname);
                            if (!ent.Empty)
                            {
                                miPassport.Tag = miQuit.Tag = ent;
                                contextDinMenu.Show(drawBox, e.Location);
                                ent.Reals = Data.GetRealValues(ptname);
                                miQuit.Visible = ((updater != null) && 
                                    (updater.UserLevel() >= UserLevel.Оператор) &&
                                    ent.Reals.ContainsKey("QuitAlarms") &&
                                    !bool.Parse(ent.Reals["QuitAlarms"]));
                                miFindInDataBase.Tag = ent;
                                miFindInDataBase.Visible = (updater != null && 
                                    updater.UserLevel() >= UserLevel.Инженер);
                                string ptparam = drw.Props.ContainsKey("PtParam") ?
                                    (string)drw.Props["PtParam"] : "PV";
                                string tagparam = ptname + "." + ptparam;
                                int[] items = Data.GetGroupNo(tagparam, ParamGroup.Trend);
                                miTrend.Tag = items;
                                miTrend.Visible = miTrendSplit.Visible =
                                    (ent.Values.ContainsKey("Trend") && items.Length == 2);
                            }
                        }
                    }
                    else
                    {
                        contextPageMenu.Show(drawBox, e.Location);
                    }
                }
            }
        }

        private void refreshDraw(Draw drw)
        {
            if (drawslist.Count > 0 && drw != null)
            {
                Rectangle rect = Rectangle.Ceiling(drw.SizedBoundsRect);
                Background bkg = (Background)drawslist[0];
                if (bkg.Expanded)
                {
                    if (bkg.SaveAspect && !drawBox.ClientRectangle.IsEmpty)
                    {
                        Rectangle scalerect = ScaleRect(drawBox.ClientRectangle,
                            pnlScroll.Size.Width, pnlScroll.Size.Height);
                        float kX = scalerect.Width * 1f / drawBox.ClientRectangle.Width;
                        float kY = scalerect.Height * 1f / drawBox.ClientRectangle.Height;
                        rect.X = (int)(rect.X * kX + scalerect.X);
                        rect.Y = (int)(rect.Y * kY + scalerect.Y);
                        rect.Size = new Size((int)(rect.Width * kX), (int)(rect.Height * kY));
                    }
                    else
                    {
                        Rectangle scalerect = pnlScroll.ClientRectangle;
                        float kX = scalerect.Width * 1f / drawBox.ClientRectangle.Width;
                        float kY = scalerect.Height * 1f / drawBox.ClientRectangle.Height;
                        rect.X = (int)(rect.X * kX);
                        rect.Y = (int)(rect.Y * kY);
                        rect.Size = new Size((int)(rect.Width * kX), (int)(rect.Height * kY));
                    }
                }
                rect.Inflate(3, 3);
                drawBox.Invalidate(rect);
            }
        }

        private DateTime baseVersion = DateTime.MinValue;
        private void LoadFirst()
        {
            baseVersion = Data.Version;
            using (ServerSQL dataSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
            {
                using (ServerSQL fetchSQL = new ServerSQL(DatabaseFrom.Fetchbase, true)) // чтение
                {
                    if (dataSQL.Connected && fetchSQL.Connected)
                    {
                        foreach (KeyValuePair<string, List<Draw>> item in fetchlist)
                        {
                            string ptname = item.Key;
                            // получение данных
                            Entity ent = Data.GetEntity(ptname, dataSQL);
                            if (!ent.Empty)
                            {
                                ent.Reals = Data.GetRealValues(ptname, fetchSQL, dataSQL);
                                if (ent.Reals.Count == 0)
                                {
                                    if (updater != null) 
                                        updater.UpdateStatusMessage("Нет связи с SQL-сервером");
                                    continue;
                                }
                                // Заполнение данными приёмников данных
                                string[] metadata = new string[] { "EUDesc" };
                                foreach (Draw drw in item.Value)
                                {
                                    foreach (string propname in metadata)
                                    {
                                        if (drw.Props.ContainsKey(propname))
                                            drw.Props[propname] = ent.Values.ContainsKey(propname) ?
                                                                    (string)ent.Values[propname] : "???";
                                    }
                                    refreshDraw(drw);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void timerFetch_Tick(object sender, EventArgs e)
        {
            FetchBase();
        }

        private bool blink = false;
        bool mustReload = false;
        string reloadPtName = String.Empty;
        private void FetchBase()
        {
            for (int i = Pasports.Count - 1; i >= 0; i--) // удаление закрытых паспортов из списка
                if (String.IsNullOrWhiteSpace(Pasports[i].Text)) Pasports.RemoveAt(i);
            if (mustReload && !backgroundBaseReload.IsBusy)
                backgroundBaseReload.RunWorkerAsync(reloadPtName);
            if (!backgroundFetch.IsBusy)
            {
                IDictionary<string, List<Draw>> fetchList =
                    new Dictionary<string, List<Draw>>(fetchlist.Count);
                foreach (KeyValuePair<string, List<Draw>> item in fetchlist)
                    fetchList.Add(item.Key, item.Value);
                backgroundFetch.RunWorkerAsync(fetchList);
            }
            blink = !blink;
        }

        private void FetchBaseAsync(object argument, BackgroundWorker worker,
            System.ComponentModel.DoWorkEventArgs e)
        {
            IDictionary<string, List<Draw>> fetchList =
                (IDictionary<string, List<Draw>>)argument;
            using (ServerSQL dataSQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
            {
                using (ServerSQL fetchSQL = new ServerSQL(DatabaseFrom.Fetchbase, true)) // чтение
                {
                    if (dataSQL.Connected && fetchSQL.Connected)
                    {
                        int i = 0;
                        foreach (KeyValuePair<string, List<Draw>> item in fetchList)
                        {
                            if (worker.CancellationPending) { e.Cancel = true; break; }
                            string ptname = item.Key;
                            // получение данных
                            IDictionary<string, string> realvals =
                                Data.GetRealValues(ptname, fetchSQL, dataSQL);
                            worker.ReportProgress(i,
                                new Tuple<List<Draw>,
                                          IDictionary<string, string>, string>(
                                    item.Value, realvals, ptname));
                            i++;
                        }
                    }
                }
            }
        }

        private void backgroundFetch_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Tuple<List<Draw>, IDictionary<string, string>, string> agruments =
                (Tuple<List<Draw>, IDictionary<string, string>, string>)e.UserState;
            List<Draw> itemValue = agruments.Item1;
            IDictionary<string, string> realvals = agruments.Item2;
            string ptname = agruments.Item3;
            DateTime facttime;
            if (realvals.ContainsKey("Quality") && realvals.ContainsKey("FactTime") &&
                DateTime.TryParse(realvals["FactTime"], out facttime))
            {
                if (DateTime.Now - facttime >
                    TimeSpan.FromSeconds(Properties.Settings.Default.DisplayTimeout))
                {
                    realvals["Quality"] = "TIMEOUT";
                }
            }
            if (!mustReload)
            {
                DateTime ver, basever;
                if (realvals.ContainsKey("Version") && realvals.ContainsKey("BaseVersion") &&
                    DateTime.TryParse(realvals["Version"], out ver) &&
                    DateTime.TryParse(realvals["BaseVersion"], out basever))
                {
                    mustReload = ver > basever;
                    if (mustReload)
                        reloadPtName = ptname;
                }
                else
                    mustReload = false;
            }
            // заполнение данными приемников данных
            string[] fetchdata = new string[]
                    { "PVText", "PV", "PVPercent",
                      "SPText", "SP", "SPPercent",
                      "OPText", "OP", "OPPercent",
                      "HVText", "DVText", "KonturMode",
                      "BackColor", "ForeColor", "QuitAlarms",
                      "HasAlarms", "HasLostAlarms", "Blink", "Quality",
                      "UserLevel",
                      "DAY", "LASTDAY", "MONTH", "LASTMONTH"};
            foreach (Draw drw in itemValue)
            {
                foreach (string propname in fetchdata)
                {
                    if (drw.Props.ContainsKey(propname) && realvals.ContainsKey(propname) ||
                        propname.Equals("Blink") && drw.Props.ContainsKey("Blink"))
                    {
                        string propType = drw.Props[propname].GetType().Name;
                        int intvar;
                        switch (propType)
                        {
                            case "Boolean":
                                if (propname.Equals("Blink"))
                                    drw.Props[propname] = blink;
                                else
                                {
                                    bool boolvar;
                                    if (bool.TryParse(realvals[propname], out boolvar))
                                        drw.Props[propname] = boolvar;
                                }
                                break;
                            case "Single":
                                drw.Props[propname] =
                                    (float)Data.FloatParse(realvals[propname]); break;
                            case "Color":
                                if (propname.Equals("BackColor") || propname.Equals("ForeColor"))
                                {
                                    if (int.TryParse(realvals[propname], out intvar))
                                        drw.Props[propname] = Color.FromArgb(intvar);
                                }
                                break;
                            case "Int32":
                                if (propname.Equals("UserLevel"))
                                    drw.Props[propname] = (updater != null) ?
                                        UserInfo.UserLevelToInt(updater.UserLevel()) : 0;
                                else
                                if (int.TryParse(realvals[propname], out intvar))
                                    drw.Props[propname] = intvar;
                                break;
                            default:
                                    drw.Props[propname] = realvals[propname];
                                break;
                        }
                    }
                }
                refreshDraw(drw);
            }
        }

        private void backgroundFetch_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            FetchBaseAsync(e.Argument, worker, e);
        }

        private void backgroundBaseReload_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string ptname = (string)e.Argument;
            Data.LoadBase(PointPlugin.LoadPlugins(Application.StartupPath), ptname);
        }

        private void backgroundBaseReload_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            mustReload = false;
            LoadFirst();
            // переподключение открытых паспортов к обновленной базе данных
            foreach (Form frm in Pasports)
            {
                IPointConnect pc = frm as IPointConnect;
                if (pc != null)
                {
                    string ptname = (string)pc.Point.Values["PtName"];
                    pc.Point = Data.GetEntity(ptname);
                }
            }
            string mess = "Станция \"" + Properties.Settings.Default.StationName +
                "\" перезагрузила точку опроса \"" + reloadPtName + "\"";
            Data.SendToSystemLog(Properties.Settings.Default.Station, "Станция RemX", mess);
            if (updater != null) updater.UpdateStatusMessage(mess);
        }

        private IDictionary<string, string> GetRealValues(string ptname)
        {
            // получение данных
            IDictionary<string, string> rvals = Data.GetRealValues(ptname);
            if (updater != null)
            {
                if (rvals.ContainsKey("UserLevel"))
                    rvals["UserLevel"] = updater.UserLevel().ToString();
                else
                    rvals.Add("UserLevel", updater.UserLevel().ToString());
                if (rvals.ContainsKey("UserName"))
                    rvals["UserName"] = updater.UserLevel().ToString();
                else
                    rvals.Add("UserName", updater.UserName());
            }
            if (rvals.ContainsKey("Station"))
                rvals["Station"] = Properties.Settings.Default.Station.ToString();
            else
                rvals.Add("Station", Properties.Settings.Default.Station.ToString());
            if (rvals.ContainsKey("DisplayTimeout"))
                rvals["DisplayTimeout"] = Properties.Settings.Default.DisplayTimeout.ToString();
            else
                rvals.Add("DisplayTimeout", Properties.Settings.Default.DisplayTimeout.ToString());
            Entity ent = Data.GetEntity(ptname);
            if (!ent.Empty)
            {
                if (rvals.ContainsKey("PtDesc"))
                    rvals["PtDesc"] = (string)ent.Values["PtDesc"];
                else
                    rvals.Add("PtDesc", (string)ent.Values["PtDesc"]);
            }
            return rvals;
        }

        private void UpdateRealValue(string command, string ptname, IDictionary<string, string> realvals)
        {
            Entity ent;
            switch (command)
            {
                case "Command":
                    ent = Data.GetEntity(ptname);
                    if (!ent.Empty)
                    {
                        ent.Reals = realvals;
                        Data.WriteRealVals(ent);
                        int station;
                        if (ent.Reals.ContainsKey("Station") &&
                            int.TryParse(ent.Reals["Station"], out station) &&
                            ent.Reals.ContainsKey("UserName") &&
                            ent.Reals.ContainsKey("Command"))
                            Data.SendToChangeLog(station, (string)ent.Values["PtName"], "Command",
                                String.Empty, ent.Reals["Command"], ent.Reals["UserName"],
                                (string)ent.Values["PtDesc"]);
                    }
                    break;
                case "QuitAlarm":
                    ent = Data.GetEntity(ptname);
                    if (!ent.Empty)
                    {
                        ent.Reals = realvals;
                        Data.WriteRealVals(ent);
                        if (realvals.ContainsKey("QuitAlarms") &&
                            realvals["QuitAlarms"] == true.ToString())
                                Data.SendShortUpCommand();
                    }
                    break;
                case "ShowPassport":
                    ent = Data.GetEntity(ptname);
                    if (!ent.Empty)
                    {
                        ShowPassport(ent);
                    }
                    break;
            }
        }

        List<Form> Pasports = new List<Form>();

        private void ShowPassport(Entity ent)
        {
            string ptname = (string)ent.Values["PtName"];
            bool found = false;
            Form passport = null;
            foreach (Form frm in Pasports)
            {
                if (frm.Tag != null && frm.Tag.ToString().Equals(ptname))
                {
                    found = true;
                    passport = frm;
                    break;
                }
            }
            if (!found)
            {
                passport = ent.Passport(GetRealValues, UpdateRealValue);
                if (passport != null)
                {
                    passport.Tag = ptname;
                    Pasports.Add(passport);
                    passport.Show(this);
                }
            }
            else
                if (passport != null)
                {
                    passport.BringToFront();
                }
        }

        private void miPassport_Click(object sender, EventArgs e)
        {
            Entity ent = (Entity)miPassport.Tag;
            if (!ent.Empty) 
                ShowPassport(ent);
        }

        private void miQuit_Click(object sender, EventArgs e)
        {
            Entity ent = (Entity)miQuit.Tag;
            if (!ent.Empty && updater != null)
            {
                string ptname = (string)ent.Values["PtName"];
                ent.Reals = Data.GetRealValues(ptname);
                ent.QuitAlarms(updater.UserName());
                ent.SetRealProp("QuitAlarms", true.ToString());
                Data.WriteRealVals(ent);
                updater.AlarmShortUp();
                Data.SendShortUpCommand();
            }
        }

        private void frmOverview_Deactivate(object sender, EventArgs e)
        {
            foreach (Form frm in Pasports) frm.Hide();
        }

        private void frmOverview_Activated(object sender, EventArgs e)
        {
            foreach (Form frm in Pasports) frm.Show();
        }

        private void miFindInDataBase_Click(object sender, EventArgs e)
        {
            Entity ent = (Entity)miFindInDataBase.Tag;
            if (!ent.Empty)
            {
                string ptname = (string)ent.Values["PtName"];
                if (updater != null) updater.ShowBaseEditor(ptname);
            }
        }

        frmPrintPreview printform = null;
        private void tsmiPrint_Click(object sender, EventArgs e)
        {
            if (updater != null)
            {
                string Caption = "Предварительный просмотр";
                updater.CloseAndRemove(Caption);
                printform = new frmPrintPreview();
                updater.ChildForms().Add(printform);
                printform.printDoc.QueryPageSettings += printDoc_QueryPageSettings;
                printform.printDoc.PrintPage += printDoc_PrintPage;
                printform.Text = Caption;
                printform.MdiParent = PanelHost;
                printform.Show();
            }
        }

        private void printDoc_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
            e.PageSettings.Landscape = true;
        }

        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            using (Bitmap bmp = new Bitmap(drawBox.Width, drawBox.Height))
            {
                Graphics g = Graphics.FromImage(bmp);
                Rectangle rect = new Rectangle(0, 0, drawBox.Width, drawBox.Height);
                using (SolidBrush brush = new SolidBrush(drawBox.BackColor))
                    g.FillRectangle(brush, rect);
                // Отрисовка массива фигур
                foreach (Draw drw in drawslist) drw.DrawFigure(g);
                Rectangle prnrect = e.PageBounds;
                prnrect.Inflate(new Size(-30, -50));
                string sysinfo = "RemX - Автоматизированная система контроля и управления";
                string printdate = "Отпечатано: " + DateTime.Now.ToString();
                string printname = "Мнемосхема \"" + this.Text + "\"";
                using (Font printfont = new Font("Arial", 8, FontStyle.Italic))
                {
                    SizeF strSize = e.Graphics.MeasureString(sysinfo, printfont);
                    PointF strPoint = prnrect.Location;
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
                    e.Graphics.DrawImage(
                        ScaleImage(bmp, prnrect.Width,
                                   prnrect.Height - 2 * (int)strSize.Height - 10, Color.White),
                        prnrect.Left, prnrect.Top + 5);
                }
            }
        }

        frmTrends trendform = null;
        private void miTrend_Click(object sender, EventArgs e)
        {
            if (updater != null)
            {
                int[] items = (int[])miTrend.Tag;
                if (items.Length == 2)
                {
                    int group = items[0];
                    int place = items[1];
                    string Caption = "Просмотр графиков группы " + group;
                    updater.CloseAndRemove(Caption);
                    trendform = new frmTrends(updater.GetHost(), PanelHost, group, place);
                    updater.ChildForms().Add(trendform);
                    trendform.Text = Caption;
                    trendform.MdiParent = PanelHost;
                    trendform.Show();
                    trendform.UpdateView();
                }
            }
        }

        private void frmOverview_Resize(object sender, EventArgs e)
        {
            drawBox.Invalidate();
        }

    }
}
