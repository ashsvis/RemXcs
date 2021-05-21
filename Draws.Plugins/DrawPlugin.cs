using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using BaseServer;
using System.Text.RegularExpressions;
using System.Drawing.Printing;


namespace Draws.Plugins
{
    [Serializable]
    public class Draw
    {
        protected IDictionary<string, object> values;
        private static string GetUniqueKey(int length)
        {
            string guidResult = string.Empty;
            while (guidResult.Length < length)
            {
                // Get the GUID. 
                guidResult += Guid.NewGuid().ToString().GetHashCode().ToString("x");
            }
            // Make sure length is valid. 
            if (length <= 0 || length > guidResult.Length)
                throw new ArgumentException("Length must be between 1 and " + guidResult.Length);
            // Return the first length bytes. 
            return guidResult.Substring(0, length);
        }
        public Draw()
        {
            this.Location = new PointF();
            this.Size = new SizeF();
            this.plugin = null;
            this.values = new Dictionary<string, object>();
        }
        public Draw(float X, float Y, IDrawPlugin plugin)
        {
            this.Location = new PointF(X, Y);
            this.Size = new SizeF();
            this.plugin = plugin;
            this.values = plugin.DefaultValues();
            values["Name"] = GetUniqueKey(10);
            values["Left"] = X;
            values["Top"] = Y;
        }

        public void GenerateNewName()
        {
            values["Name"] = GetUniqueKey(10);
        }
        public virtual string Name { get { return (string)values["Name"]; } set { values["Name"] = value; } }
        public virtual string Category { get { return plugin.PluginDescriptor; } }
        public virtual SelectDataKind DataKind { get { return plugin.PluginSelectDataKind; } }
        public IDictionary<string, object> Props { get { return values; } }
        public RectangleF BoundsRect { get { return new RectangleF(Location, Size); } }
        public void SetPropValue(string name, string value)
        {
            if (values.ContainsKey(name))
            {
                object defval = plugin.DefaultValues()[name];
                Type valtype = defval.GetType();
                switch (valtype.Name)
                {
                    case "Boolean":
                        values[name] = (int.Parse(value) == 1) ? true : false;
                        break;
                    case "Int32":
                        values[name] = int.Parse(value);
                        break;
                    case "Color":
                        string[] cols = value.Split(new char[] { ';' });
                        int A = int.Parse(cols[0]);
                        int R = int.Parse(cols[1]);
                        int G = int.Parse(cols[2]);
                        int B = int.Parse(cols[3]);
                        values[name] = Color.FromArgb(A, R, G, B);
                        break;
                    case "Single":
                        values[name] = Single.Parse(value);
                        break;
                    default:
                        values[name] = value;
                        break;
                }
            }
        }
        #region Определение свойств
        protected IDrawPlugin plugin;
        public virtual Form Editor(UpdateDraw updater, SelectData selector)
        {
            return plugin.ShowEditor(this, updater, selector);
        }
        public void Assign(Draw item)
        {
            PointF location = Location;
            string name = Name;
            foreach (KeyValuePair<string, object> kvp in item.values)
                this.values[kvp.Key] = kvp.Value;
            this.values["Name"] = name;
            values["Left"] = location.X;
            values["Top"] = location.Y;
        }
        public PointF Location
        { 
            get
            {
                PointF location = new PointF();
                location.X = (float)values["Left"]; location.Y = (float)values["Top"];
                return location;
            }
            set
            {
                PointF location = value;
                if (values != null)
                {
                    values["Left"] = location.X; values["Top"] = location.Y;
                }
            }
        }
        public SizeF Size
        {
            get
            {
                SizeF size = new SizeF();
                size.Width = (float)values["Width"]; size.Height = (float)values["Height"];
                return size;
            }
            set
            {
                SizeF size = value;
                if (values != null)
                {
                    values["Width"] = size.Width; values["Height"] = size.Height;
                }
            }
        }
        public IDrawPlugin Plugin { get { return plugin; } }
        #endregion

        public virtual void DrawFigure(Graphics g)
        {
            plugin.DrawFigure(g, values);
        }

        public virtual RectangleF SizedBoundsRect
        {
            get { return plugin.SizedBoundsRect(values); }
        }
    }

    [Serializable]
    public class Background : Draw
    {
        public Background()
        {
            values.Add("Name", "Background");
            values.Add("SchemeName", String.Empty);
            values.Add("Descriptor", String.Empty);
            values.Add("Left", 0f);
            values.Add("Top", 0f);
            values.Add("Width", 0f);
            values.Add("Height", 0f);
            values.Add("BackColor", Color.Gray);
            values.Add("Expanded", false);
            values.Add("SaveAspect", true);
        }
        public override string Category { get { return "Background"; } }
        public override SelectDataKind DataKind { get { return SelectDataKind.DataSchemes; } }
        public override string Name { get { return "Background"; } set { values["Name"] = "Background"; } }
        public string SchemeName
            { get { return (string)values["SchemeName"]; } set { values["SchemeName"] = value; } }
        public string Descriptor
            { get { return (string)values["Descriptor"]; } set { values["Descriptor"] = value; } }
        public float Width
            { get { return (float)values["Width"]; } set { values["Width"] = value; } }
        public float Height
            { get { return (float)values["Height"]; } set { values["Height"] = value; } }
        public Color BackColor
            { get { return (Color)values["BackColor"]; } set { values["BackColor"] = value; } }
        public bool SaveAspect
            { get { return (bool)values["SaveAspect"]; } set { values["SaveAspect"] = value; } }
        public bool Expanded
            { get { return (bool)values["Expanded"]; } set { values["Expanded"] = value; } }
        public override void DrawFigure(Graphics g) { /* заглушка */ }
        public override Form Editor(UpdateDraw updater, SelectData selector)
        {
            return new frmBackground(this, updater, selector);
        }
    }

    public static class DrawPlugin
    {
        //private static IDictionary<string, IDrawPlugin> plugins =
        //    new Dictionary<string, IDrawPlugin>();

        //public static IDictionary<string, IDrawPlugin> Plugins { get { return plugins; } }

        //public static void ClearPlugins() { plugins.Clear(); }

        public static IDictionary<string, IDrawPlugin> LoadPlugin(string pathfile)
        {
            IDictionary<string, IDrawPlugin> result = new Dictionary<string, IDrawPlugin>();
            if (File.Exists(pathfile))
            {
                var asm = Assembly.LoadFile(pathfile);
                Type pluginClass;
                IDrawPlugin plugin;
                foreach (Type type in asm.GetTypes())
                {
                    if (!type.IsAbstract && type.IsClass &&
                        type.GetInterface("IDrawPlugin") != null)
                    {
                        pluginClass = type.GetInterface("IDrawPlugin");
                        plugin = Activator.CreateInstance(type) as IDrawPlugin;
                        if (plugin != null)
                        {
                            string key = plugin.PluginShortType;
                            if (!result.ContainsKey(key))
                            {
                                result.Add(key, plugin);
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static IDictionary<string, IDrawPlugin> LoadPlugins(string path)
        {
            IDictionary<string, IDrawPlugin> result = new Dictionary<string, IDrawPlugin>();
            foreach (string f in Directory.GetFiles(path))
            {
                FileInfo fi = new FileInfo(f);
                if (fi.Extension.Equals(".dll"))
                {
                    string[] items = fi.Name.Split(new char[] { '.' });
                    if (items.Length > 0 && items[0].Equals("Draws") &&
                        !items[1].Equals("Plugins"))
                    {
                        IDictionary<string, IDrawPlugin> list = 
                            LoadPlugin(path + "\\" + fi.Name);
                        IEnumerable<KeyValuePair<string, IDrawPlugin>> query =
                            list.OrderBy(kvp => kvp.Key);
                        foreach (KeyValuePair<string, IDrawPlugin> kvp in query)
                        {
                            result.Add(kvp);
                        }
                    }
                }
            }
            return result;
        }

        public static void colorComboFillList(object sender, int LastColorIndex)
        {
            ComboBox cbox = (ComboBox)sender;
            cbox.Items.Clear();
            // получение всех имён доступных цветов
            cbox.Items.AddRange(DrawUtils.GetAllColorNames());
            // добавление пункта выбора цвета
            cbox.Items.Add("Выбор цвета...");
            cbox.Text = DrawUtils.GetColorNameFromIndex(LastColorIndex);
        }

        public static void fontComboFillList(object sender)
        {
            ComboBox cbox = (ComboBox)sender;
            FontFamily[] ffam = FontFamily.Families;
            cbox.Items.Clear();
            foreach (FontFamily ff in ffam) cbox.Items.Add(ff.Name);
            cbox.SelectedIndex = cbox.Items.IndexOf(cbox.Text);
        }

        public static void colorComboDrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Graphics g = e.Graphics;
            Color brushColor = DrawUtils.ColorFromIndex(e.Index);
            // Draw the background of the item.
            e.DrawBackground();
            Rectangle largerect = new Rectangle(e.Bounds.X, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1);
            Rectangle colorrect = new Rectangle(4, e.Bounds.Top + 2, e.Bounds.Height - 2, e.Bounds.Height - 5);
            // отрисовка рамки цвета пунктов основных цветов
            if (DrawUtils.IsNamedColorIndex(e.Index))
            {
                using (SolidBrush brush = new SolidBrush(brushColor))
                    g.FillRectangle(brush, colorrect);
                g.DrawRectangle(Pens.Black, colorrect);
            }
            RectangleF textRect = new RectangleF(e.Bounds.X + colorrect.Width + 5,
                e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height);
            using (SolidBrush textColor = new SolidBrush(e.ForeColor))
            {
                if (DrawUtils.IsNamedColorIndex(e.Index))
                {// отрисовка пунктов основных цветов
                    g.DrawString(DrawUtils.GetColorNameFromIndex(e.Index), cb.Font,
                        textColor, textRect);
                }
                else // отрисовка пунктов дополнительных цветов
                    if (DrawUtils.IsCustomColorIndex(e.Index))
                    {
                        using (SolidBrush brush = new SolidBrush(brushColor))
                            g.FillRectangle(brush, largerect);
                        using (Pen pen = new Pen(cb.BackColor))
                            g.DrawRectangle(pen, largerect);
                    }
                    else // отрисовка последнего пункта: Выбор цвета...
                        g.DrawString(cb.Items[e.Index].ToString(), cb.Font,
                            textColor, largerect);
            }
            // Draw the focus rectangle if the mouse hovers over an item.
            e.DrawFocusRectangle();
        }

        public delegate void ChangeCommitted(object sender, EventArgs e);

        public static void colorComboSelectedIndexChanged(object sender, ref int LastColorIndex,
            ChangeCommitted changed)
        {
            ComboBox cbox = (ComboBox)sender;
            if (cbox.SelectedIndex == cbox.Items.Count - 1)
            {
                try
                {
                    int selIndex;
                    using (ColorDialog dlgSelectColor = new ColorDialog())
                    {
                        dlgSelectColor.Color = DrawUtils.ColorFromIndex(LastColorIndex);
                        selIndex = LastColorIndex;
                        if (dlgSelectColor.ShowDialog() == DialogResult.OK)
                        {
                            Color selColor = dlgSelectColor.Color;
                            if (!DrawUtils.FindColor(selColor))
                            {
                                DrawUtils.AddCustomColor(selColor);
                                dlgSelectColor.CustomColors = DrawUtils.GetCustomColors();
                                cbox.Items.Insert(cbox.Items.Count - 1, "Мой цвет");
                                cbox.SelectedIndex = cbox.Items.Count - 2;
                            }
                            else
                                cbox.SelectedIndex = DrawUtils.ColorToIndex(selColor);
                            if (changed != null) changed(sender, new EventArgs()); 
                        }
                        else
                            cbox.SelectedIndex = selIndex;
                    }
                }
                catch
                { }
            }
            else
            {
                LastColorIndex = cbox.SelectedIndex;
                cbox.Refresh();
            }
        }

        public static Color colorComboSelectionChangeCommitted(object sender)
        {
            ComboBox cbox = (ComboBox)sender;
            return DrawUtils.ColorFromIndex(cbox.SelectedIndex);
        }

        public static void colorComboSelectInList(object sender, Color color)
        {
            ComboBox cbox = (ComboBox)sender;
            int Index = DrawUtils.ColorToIndex(color);
            if (Index < 0)
            {
                DrawUtils.AddCustomColor(color);
                cbox.Items.Insert(cbox.Items.Count - 1, "Мой цвет");
                Index = cbox.Items.Count - 2;
            }
            if (Index >= 0) cbox.SelectedIndex = Index;
        }

        public static List<Draw> LoadScheme(string schemename, string imagepath)
        {
            List<Draw> drawslist = new List<Draw>();
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database, true)) // чтение
            {
                if (mySQL.Connected)
                {
                    List<string> dinlist = Data.GetDinList(schemename, mySQL);
                    foreach (string dinname in dinlist)
                    {
                        IDictionary<string, string> dinprops =
                            Data.GetDinProps(schemename, dinname, mySQL);
                        if (dinname.Equals("Background") &&
                            dinprops.ContainsKey("Left") &&
                            dinprops.ContainsKey("Top") &&
                            dinprops.ContainsKey("Width") &&
                                dinprops.ContainsKey("Height") &&
                                dinprops.ContainsKey("BackColor") &&
                                dinprops.ContainsKey("Descriptor") &&
                                dinprops.ContainsKey("Expanded") &&
                                dinprops.ContainsKey("SaveAspect")
                            )
                        {
                            Background drw = new Background();
                            drw.Location = new Point(int.Parse(dinprops["Left"]),
                                                     int.Parse(dinprops["Top"]));
                            drw.Width = int.Parse(dinprops["Width"]);
                            drw.Height = int.Parse(dinprops["Height"]);
                            drw.BackColor = Data.ColorFromBase(dinprops["BackColor"]);
                            drw.Descriptor = dinprops["Descriptor"];
                            drw.Expanded = int.Parse(dinprops["Expanded"]) > 0;
                            drw.SaveAspect = int.Parse(dinprops["SaveAspect"]) > 0;
                            drawslist.Add(drw);
                        }
                        else
                        {
                            if (dinprops.ContainsKey("Plugin") &&
                                dinprops.ContainsKey("Left") &&
                                dinprops.ContainsKey("Top") &&
                                dinprops.ContainsKey("Name"))
                            {
                                string plugname = dinprops["Plugin"];
                                IDictionary<string, IDrawPlugin> plugins =
                                    DrawPlugin.LoadPlugins(Application.StartupPath);
                                if (plugins.ContainsKey(plugname))
                                {
                                    IDrawPlugin plugin = plugins[plugname];
                                    float x = float.Parse(dinprops["Left"]);
                                    float y = float.Parse(dinprops["Top"]);
                                    Draw drw = new Draw(x, y, plugin);
                                    drw.Name = dinprops["Name"];
                                    drawslist.Add(drw);
                                    foreach (KeyValuePair<string, string> prop in dinprops)
                                        drw.SetPropValue(prop.Key, prop.Value);
                                    if (drw.Props.ContainsKey("ImagePath"))
                                        drw.SetPropValue("ImagePath", imagepath);
                                }
                            }
                        }
                    }
                }
            }
            return drawslist;
        }

        public static void ImportImagesFrom(string path, Action<string> updateMessage)
        {
            if (Directory.Exists(path))
            {
                Data.EmptyImages();
                string[] files = Directory.GetFiles(path, "*.*");
                foreach (string filename in files)
                {
                    string onlyname = Path.GetFileName(filename);
                    if (FitsOneOfMultipleMasks(onlyname, "*.png,*.bmp,*.jpg,*.emf,*.wmf,*.gif"))
                    {
                        updateMessage("Импорт картинки из файла \"" + filename.ToUpper() + "\"...");
                        byte[] image = File.ReadAllBytes(filename);
                        DateTime filetime = File.GetLastWriteTime(filename);
                        Data.AddImage(onlyname, filetime, image);
                    }
                }
            }
        }

        private static bool FitsMask(string fileName, string fileMask)
        { 
            Regex mask = new Regex('^' + fileMask.Replace(".", "[.]").Replace("*", ".*").
                Replace("?", ".") + '$', RegexOptions.IgnoreCase); return mask.IsMatch(fileName); }
        private static bool FitsOneOfMultipleMasks(string fileName, string fileMasks)
        { 
            return fileMasks.Split(new string[] { "\r\n", "\n", ",", "|", " " },
                StringSplitOptions.RemoveEmptyEntries).Any(fileMask => FitsMask(fileName, fileMask));
        }

        public static void ImportReportsFrom(string path, Action<string> updateMessage)
        {
            if (Directory.Exists(path))
            {
                Data.EmptyReports();
                string[] files = Directory.GetFiles(path, "*.ini");
                foreach (string filename in files)
                {
                    updateMessage("Импорт отчёта из файла \"" + filename.ToUpper() + "\"...");
                    using (PrintDocument pd = new PrintDocument())
                    {
                        PrinterSettings ps = pd.PrinterSettings;
                        string[] lines = System.IO.File.ReadAllLines(filename);
                        PrintReport printReport = new PrintReport(pd);
                        printReport.ImportLines(ref ps, lines);
                        printReport.SaveToBase();
                    }
                }
            }
        }

        public static void ImportSchemesFrom(string path, Action<string> updateMessage)
        {
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path, "*.ini");
                foreach (string filename in files)
                {
                    updateMessage("Импорт мнемосхемы из файла \"" + filename.ToUpper() + "\"...");
                    ImportSchemeFrom(filename);
                }
            }
        }

        public static string ImportSchemeFrom(string filename)
        {
            IDictionary<string, IDrawPlugin> plugins =
                DrawPlugin.LoadPlugins(Application.StartupPath);
            string[] lines = System.IO.File.ReadAllLines(filename);
            bool background = false;
            string name = String.Empty;
            Draw drw = null;
            Background bkg = null;
            List<Draw> drawslist = new List<Draw>();
            foreach (string line in lines)
            {
                if (!String.IsNullOrWhiteSpace(line))
                {
                    if (line.Equals("[Background]"))
                    {
                        background = true;
                        bkg = new Background();
                        drawslist.Add(bkg);
                    }
                    else if (background)
                    {
                        string[] items = line.Split(new char[] { '=' });
                        switch (items[0])
                        {
                            case "Width": bkg.Width = int.Parse(items[1]); break;
                            case "Height": bkg.Height = int.Parse(items[1]); break;
                            case "BackColor":
                                bkg.BackColor = Data.ColorFromBase(items[1]);
                                break;
                            case "SchemeName": bkg.SchemeName = items[1]; break;
                            case "Descriptor": bkg.Descriptor = items[1]; break;
                        }
                    }
                    else if (drw == null)
                    {
                        string[] items = line.Split(new char[] { '=' });
                        string value = (items.Length == 2) ? items[1] : String.Empty;
                        switch (items[0])
                        {
                            case "Name": name = value; break;
                            case "Plugin":
                                if (plugins.ContainsKey(value))
                                {
                                    IDrawPlugin plugin = plugins[value];
                                    drw = new Draw(0, 0, plugin);
                                    drw.Name = name;
                                    drawslist.Add(drw);
                                }
                                break;
                        }
                    }
                    else if (drw != null)
                    {
                        string[] items = line.Split(new char[] { '=' });
                        drw.SetPropValue(items[0], items[1]);
                    }
                }
                else
                {
                    drw = null;
                    background = false;
                }
            }
            string SchemeName = bkg.SchemeName;
            using (ServerSQL mySQL = new ServerSQL(DatabaseFrom.Database)) // запись
            {
                if (mySQL.Connected)
                {
                    string ukey = Data.GetUniqueKey(20);
                    int step = 0;
                    try
                    {
                        int npp = 0;
                        foreach (Draw item in drawslist)
                            saveOneDraw(npp++, ukey, item, mySQL);
                        step++;
                        Data.EmptyScheme(SchemeName, mySQL);
                        Data.RenameScheme(ukey, SchemeName, mySQL);
                    }
                    catch
                    {
                        switch (step)
                        {
                            case 0:
                                Data.EmptyScheme(ukey, mySQL);
                                break;
                        }
                    }
                }
            }
            return SchemeName;
        }

        public static void saveOneDraw(int npp, string SchemeName, Draw item, ServerSQL mySQL)
        {
            string dinname = item.Name;
            foreach (KeyValuePair<string, object> prop in item.Props)
            {
                if (!prop.Key.Equals("ImagePath"))
                {
                    Type valtype = prop.Value.GetType();
                    switch (valtype.Name)
                    {
                        case "Boolean":
                            Data.WriteDrawProp(SchemeName, npp, dinname, prop.Key,
                                ((bool)prop.Value) ? "1" : "0", mySQL);
                            break;
                        case "Color":
                            Color color = (Color)prop.Value;
                            Data.WriteDrawProp(SchemeName, npp, dinname, prop.Key,
                                                Data.ColorToBase(color), mySQL);
                            break;
                        default:
                            string value = prop.Value.ToString();
                            if (prop.Key.Equals("ScreenName"))
                            {
                                if (value.ToUpper().EndsWith(".SCM"))
                                    value = value.Substring(0, value.Length - 4);
                            }
                            Data.WriteDrawProp(SchemeName, npp, dinname, prop.Key, value, mySQL);
                            break;
                    }
                }
            }
        }

        public static void RestoreImageCatalog(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            List<string> imagelist = Data.GetImagesList();
            foreach (string name in imagelist)
            {
                string filename = path + name;
                if (File.Exists(filename) &&
                    (File.GetLastWriteTime(filename) < Data.GetImageFileTime(name)) ||
                    !File.Exists(filename))
                    File.WriteAllBytes(path + name, Data.GetImageData(name));
            }
        }

        public static void RestoreReportCatalog(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            IDictionary<string, string> reportlist = Data.GetReportsList();
            foreach (KeyValuePair<string, string> name in reportlist)
            {
                string filename = path + name.Key + ".RPT";
                if (File.Exists(filename) &&
                    (File.GetLastWriteTime(filename) < Data.GetReportFileTime(name.Key)) ||
                    !File.Exists(filename))
                    File.WriteAllBytes(filename, Data.GetReportData(name.Key));
            }
        }

        public static void ExportImagesAs(string path, Action<string> updateMessage)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            List<string> imagelist = Data.GetImagesList();
            foreach (string name in imagelist)
            {
                updateMessage("Экспорт картинки: " + name);
                File.WriteAllBytes(path + name, Data.GetImageData(name));
            }
        }

        public static void ExportReportsAs(string path, Action<string> updateMessage, string pathtoconfig)
        {
            // pathtoconfig - путь для хранения "reports.ini", с настройками по автозапуску и времени выполнения 
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            IDictionary<string, string> reportlist = Data.GetReportsList();
            foreach (KeyValuePair<string, string> name in reportlist)
            {
                updateMessage("Экспорт отчёта: " + name.Key);
                using (PrintDocument pd = new PrintDocument())
                {
                    PrintReport printReport = new PrintReport(pd);
                    printReport.LoadReport(name.Key, pathtoconfig);
                    string FileName = path + "\\" + name.Key.ToLower() + ".ini";
                    List<string> lines = printReport.ExportLines();
                    System.IO.File.WriteAllLines(FileName, lines.ToArray());
                }
            }
        }

        public static void ExportSchemesAs(string path, Action<string> updateMessage)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            IDictionary<string, string> schemelist = Data.GetSchemesList();
            foreach (string schemename in schemelist.Keys)
            {
                updateMessage("Экспорт мнемосхемы: " + schemename);
                ExportOneSchemeAs(path, schemename);
            }
        }

        public static void ExportOneSchemeAs(string path, string schemename)
        {
            List<Draw> drawslist = LoadScheme(schemename, String.Empty);
            if (drawslist.Count > 0)
                ((Background)drawslist[0]).SchemeName = schemename;
            List<string> lines = new List<string>();
            foreach (Draw item in drawslist)
            {
                lines.Add("[" + item.Name + "]");
                foreach (KeyValuePair<string, object> prop in item.Props)
                {
                    if (!prop.Key.Equals("ImagePath"))
                    {
                        Type valtype = prop.Value.GetType();
                        switch (valtype.Name)
                        {
                            case "Boolean":
                                lines.Add(prop.Key + "=" + (((bool)prop.Value) ? "1" : "0"));
                                break;
                            case "Color":
                                Color color = (Color)prop.Value;
                                lines.Add(prop.Key + "=" + Data.ColorToBase(color));
                                break;
                            default:
                                string value = prop.Value.ToString();
                                lines.Add(prop.Key + "=" + value);
                                break;
                        }
                    }
                }
                lines.Add("");
            }
            File.WriteAllLines(path + schemename.ToLower() + ".ini", lines.ToArray());
        }
    }
}
