using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Draws.Plugins;

namespace Draws.Analog
{
    [Serializable]
    public class DinAnalog : IDrawPlugin
    {
        public string PluginShortType { get { return "DinAnalog"; } }
        public string PluginDescriptor { get { return "Аналоговое значение"; } }
        public string PluginCategory { get { return "Аналоговые элементы"; } }

        public RectangleF SizedBoundsRect(IDictionary<string, object> props)
        {
            PointF location = new PointF((float)props["Left"], (float)props["Top"]);
            SizeF size = new SizeF((float)props["Width"], (float)props["Height"]);
            return new RectangleF(location, size);
        }

        private static decimal FloatParse(string value)
        {
            decimal result = 0m;
            string sval = value.Replace(',', '.');
            if (decimal.TryParse(sval, out result))
                return result;
            else
            {
                sval = value.Replace('.', ',');
                if (decimal.TryParse(sval, out result))
                    return result;
                else
                {
                    bool bres = false;
                    if (bool.TryParse(sval, out bres))
                        return (bres) ? 1m : 0m;
                    else
                    {
                        DateTime dt;
                        DateTime now = DateTime.Now;
                        if (DateTime.TryParse(sval, out dt))
                        {
                            if (dt.Year == now.Year && dt.Month == now.Month && dt.Day == now.Day &&
                                (dt.Hour + dt.Minute + dt.Second) > 0)  // время
                                return Decimal.Parse(dt.ToString("HHmmss"));
                            else // дата
                                return Decimal.Parse(dt.ToString("ddMMyy"));
                        }
                    }
                }
            }
            return result;
        }

        public void DrawFigure(Graphics g, IDictionary<string, object> props)
        {
            PointF location = new PointF((float)props["Left"], (float)props["Top"]);
            SizeF size = new SizeF((float)props["Width"], (float)props["Height"]);
            RectangleF rect = new RectangleF(location, size);
            bool showtag = (bool)props["ShowTag"];
            bool showunit = (bool)props["ShowUnit"];
            bool showvalue = (bool)props["ShowValue"];
            bool showpanel = (bool)props["ShowPanel"];
            int alignment = (int)props["Alignment"];
            float percent = (float)FloatParse(props["PVPercent"].ToString());
            bool showlevelbar = (bool)props["BarLevelVisible"];
            Color backcolor = (Color)props["BackColor"];
            Color forecolor = (Color)props["ForeColor"];
            bool quit = (bool)props["QuitAlarms"];
            bool alarms = (bool)props["HasAlarms"];
            bool lostalarms = (bool)props["HasLostAlarms"];
            bool blink = (bool)props["Blink"];
            if (showpanel) // включен режим показа панели
            {
                g.FillRectangle(SystemBrushes.ButtonFace, rect);
                #region Внешняя рамка
                DrawUtils.DrawBorder(g, rect, SystemColors.ButtonHighlight, SystemColors.ButtonShadow, 2);
                rect.Inflate(-2, -2); // толщина панели
                #endregion
                #region Показать имя тега, параметр и единицу измерения
                if (showtag || showunit)
                {
                    RectangleF tagunitrect = new RectangleF(rect.X, rect.Y, rect.Width, 16);
                    DrawUtils.DrawBorder(g, tagunitrect, SystemColors.ButtonShadow, SystemColors.ButtonHighlight, 1);
                    using (StringFormat sf = new StringFormat())
                    {
                        sf.LineAlignment = StringAlignment.Center;
                        sf.Alignment = StringAlignment.Center;
                        StringBuilder tagunit = new StringBuilder();
                        #region Показать имя тега и параметра
                        if (showtag)
                        {
                            tagunit.Append((string)props["PtName"]);
                        }
                        #endregion
                        #region Показать единицу измерения
                        if (showunit)
                        {
                            string eudesc = (string)props["EUDesc"];
                            eudesc = modifyEuDesc(props, showtag, eudesc);
                            if (eudesc.Trim().Length > 0)
                            {
                                if (showtag) tagunit.Append(" ");
                                tagunit.Append("[");
                                tagunit.Append(eudesc);
                                tagunit.Append("]");
                            }
                        }
                        #endregion
                        using (Font panelfont = new Font("Courier New", 8))
                        {
                            g.DrawString(tagunit.ToString(), panelfont,
                                SystemBrushes.ControlText, tagunitrect, sf);
                        }
                    }
                    rect.Y += tagunitrect.Height; // высота панели для тега и юнита
                    rect.Height -= tagunitrect.Height;
                }
                #endregion
                #region Внутренняя рамка
                DrawUtils.DrawBorder(g, rect, SystemColors.ButtonShadow, SystemColors.ButtonHighlight, 2);
                rect.Inflate(-1, -1); // толщина границы панели
                #endregion
            }
            if ((bool)props["Framed"]) // включен режим вывода рамки
            {
                using (Pen framepen = new Pen((Color)props["FrameColor"]))
                    g.DrawRectangles(framepen, new RectangleF[] { rect });
                rect.Inflate(-1, -1);
            }

            #region Вывод значения
            using (Brush backbrush = new SolidBrush((Color)props["Color"]))
                g.FillRectangle(backbrush, rect);
            rect.Inflate(-1, -1);
            rect.Height--;
            if ((bool)props["BarLevelVisible"])
            {
                bool aslevel = (bool)props["ShowLevel"];
                bool asbar = (bool)props["ShowBar"];
                bool inverse = (bool)props["BarLevelInverse"];
                RectangleF barrect = rect;
                float kf = (percent <= 100f) ? percent / 100f : 1f; 
                if (asbar)
                {
                    barrect.Width *= kf;
                    if (inverse) barrect.Offset(rect.Width - barrect.Width, 0f);
                }
                else if (aslevel)
                {
                    barrect.Height *= kf;
                    if (!inverse) barrect.Offset(0f, rect.Height - barrect.Height);
                }
                barrect.Width += 1;
                barrect.Height += 1;
                using (Brush barbrush = new SolidBrush((Color)props["BarLevelColor"]))
                    g.FillRectangle(barbrush, barrect); 
            }
            if (showvalue || showtag)
            {
                FontStyle style = FontStyle.Regular;
                if ((bool)props["FontBold"]) style |= FontStyle.Bold;
                if ((bool)props["FontItalic"]) style |= FontStyle.Italic;
                if ((bool)props["FontUnderline"]) style |= FontStyle.Underline;
                if ((bool)props["FontStrikeOut"]) style |= FontStyle.Strikeout;
                try
                {
                    using (Font font = new Font((string)props["FontName"],
                                                (float)props["FontSize"], style))
                    {
                        Color fontcolor = (alarms) ? backcolor : (Color)props["FontColor"];
                        if (!props["Quality"].ToString().Equals("GOOD"))
                        {
                            fontcolor = Color.Blue;
                        }
                        using (Brush fontbrush = new SolidBrush(fontcolor))
                        {
                            using (StringFormat sf = new StringFormat())
                            {
                                sf.LineAlignment = StringAlignment.Center;
                                switch (alignment)
                                {
                                    case 0: sf.Alignment = StringAlignment.Near; break;
                                    case 1: sf.Alignment = StringAlignment.Center; break;
                                    case 2: sf.Alignment = StringAlignment.Far; break;
                                }
                                StringBuilder value = new StringBuilder();
                                if (showvalue)
                                {
                                    string paramname = (string)props["PtParam"];
                                    if (paramname == "PV")
                                        value.Append((string)props["PVText"]);
                                    else
                                        value.Append((string)props[paramname]);
                                }
                                if (showunit && !showpanel)
                                {
                                    if (showvalue) value.Append(" ");
                                    string eudesc = (string)props["EUDesc"];
                                    eudesc = modifyEuDesc(props, showtag, eudesc);
                                    value.Append(eudesc);
                                }
                                string text = value.ToString();
                                if (g.MeasureString(text, font).Width > rect.Width) text = "<-?->";
                                rect.Offset(0, 2);
                                g.DrawString(text, font, fontbrush, rect, sf);
                            }
                        }
                    }
                }
                catch
                {
                    using (Font font = new Font("Tahoma", (float)props["FontSize"], style))
                    {
                        using (Brush fontbrush = new SolidBrush((Color)props["FontColor"]))
                            g.DrawString("FONT?", font, fontbrush, rect);
                    }
                }
            }
            if (!quit && blink && (alarms || lostalarms))
            {
                rect = new RectangleF(location, size);
                using (Pen alarmpen = new Pen(backcolor))
                {
                    alarmpen.Width = 2;
                    g.DrawRectangles(alarmpen, new RectangleF[] { rect });
                }
            }
            #endregion
        }

        private static string modifyEuDesc(IDictionary<string, object> props, bool showtag, string eudesc)
        {
            string ptparam = (string)props["PtParam"];
            string[] paramlist = new string[] { "DAY", "MONTH", "LASTDAY", "LASTMONTH" };
            if (paramlist.Contains(ptparam))
                eudesc = eudesc.Split(new char[] { '/', '\\' })[0];
            return eudesc;
        }

        public System.Drawing.Bitmap PluginPicture
        {
            get { return Draws.Analog.Properties.Resources.dinanalog; }
        }

        public IDictionary<string, object> DefaultValues()
        {
            IDictionary<string, object> props = new Dictionary<string, object>();
            props.Add("Name", String.Empty); // имя элемента
            props.Add("Plugin", PluginShortType); // имя плагина
            props.Add("Left", 0f); // x - координата
            props.Add("Top", 0f); // y - координата
            props.Add("Width", 60f); // ширина
            props.Add("Height", 25f); // высота
            props.Add("PVText", "------"); // отображаемый текст по умолчанию
            props.Add("PVPercent", 0m); // процент от шкалы
            props.Add("PVEUHi", 100m); // верхняя граница шкалы
            props.Add("PVEULo", 0m); // нижняя граница шкалы
            props.Add("FormatPV", (int)1);
            props.Add("DinType", 1);
            props.Add("Color", Color.FromArgb(0x00, 0x00, 0x00)); // цвет фона
            props.Add("FontName", "Courier New"); // имя шрифта
            props.Add("FontColor", Color.FromArgb(0xFF, 0xFF, 0xFF)); // цвет текста
            props.Add("FontSize", 10f); // размер шрифта
            props.Add("FontBold", false); // жирный
            props.Add("FontItalic", false); // наклонный
            props.Add("FontUnderline", false); // подчёркнутый
            props.Add("FontStrikeOut", false); // перечёркнутый
            props.Add("PtName", String.Empty); // имя точки
            props.Add("PtParam", "PV"); // имя параметра
            props.Add("EUDesc", String.Empty); // единица измерения
            props.Add("BackColor", Color.FromArgb(0xFF, 0xFF, 0xFF)); // цвет фона аварии
            props.Add("ForeColor", Color.FromArgb(0x00, 0x00, 0x00)); // цвет шрифта аварии
            props.Add("QuitAlarms", true); // авария квитирована 
            props.Add("HasAlarms", false); // авария есть 
            props.Add("HasLostAlarms", false); // авария упущена 
            props.Add("Blink", false); // флаг мигания 
            props.Add("Alignment", (int)1); // выравнивание 0-влево 1-по центру 2-вправо
            props.Add("Framed", false); // обрамление
            props.Add("FrameColor", Color.FromArgb(0x80, 0x80, 0x80)); // цвет рамки
            props.Add("ShowPanel", false); // панель
            props.Add("ShowBar", true); // столбик
            props.Add("ShowLevel", false); // как уровень
            props.Add("BarLevelVisible", false); // показывать уровень
            props.Add("BarLevelColor", Color.FromArgb(0x00, 0x80, 0x80)); // цвет столбика
            props.Add("BarLevelInverse", false); // столбик растёт в другую сторону
            props.Add("ShowUnit", false); // показывать единицу измерения
            props.Add("ShowValue", true); // показывать значение
            props.Add("ShowTag", false); // показывать имя тега
            props.Add("Quality", String.Empty); // качество данных
            props.Add("DAY", "------"); // накопление текущих суток
            props.Add("LASTDAY", "------"); // накопление прошлых суток
            props.Add("MONTH", "------"); // накопление текущего месяца
            props.Add("LASTMONTH", "------"); // накопление прошлого месяца
            return props;
        }

        public Form ShowEditor(Draw element, UpdateDraw updater, SelectData selector)
        {
            return new frmDinAnalog(element, updater, selector);
        }

        public SelectDataKind PluginSelectDataKind
        {
            get { return SelectDataKind.DataPoints; }
        }
    }
}
