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
    public class DinKontur : IDrawPlugin
    {
        public string PluginShortType { get { return "DinKontur"; } }
        public string PluginDescriptor { get { return "Контур регулирования"; } }
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

        private RectangleF MakeSquare(RectangleF rect)
        {
            float side = Math.Min(rect.Width, rect.Height);
            RectangleF r = new RectangleF(rect.Left, rect.Top, side, side);
            if (side < rect.Width)
                r.Offset((rect.Width - side) / 2, 0);
            else
                r.Offset(0, (rect.Height - side) / 2);
            return r;
        }

        public void DrawFigure(Graphics g, IDictionary<string, object> props)
        {
            PointF location = new PointF((float)props["Left"], (float)props["Top"]);
            SizeF size = new SizeF((float)props["Width"], (float)props["Height"]);
            RectangleF rect = new RectangleF(location, size);
            float pvpercent = (float)FloatParse(props["PVPercent"].ToString());
            float sppercent = (float)FloatParse(props["SPPercent"].ToString());
            float oppercent = (float)FloatParse(props["OPPercent"].ToString());
            Color backcolor = (Color)props["BackColor"];
            Color forecolor = (Color)props["ForeColor"];
            bool quit = (bool)props["QuitAlarms"];
            bool alarms = (bool)props["HasAlarms"];
            bool lostalarms = (bool)props["HasLostAlarms"];
            bool blink = (bool)props["Blink"];
            int konturmode = (int)props["KonturMode"];
            rect = MakeSquare(rect);
            g.FillRectangle(SystemBrushes.ButtonFace, rect);
            #region Внешняя рамка
            DrawUtils.DrawBorder(g, rect, SystemColors.ButtonHighlight, SystemColors.ButtonShadow, 2);
            rect.Inflate(-2, -2); // толщина панели
            #endregion
            #region Показать имя тега, параметр и единицу измерения
            RectangleF tagunitrect = new RectangleF(rect.X, rect.Y, rect.Width, 16);
            DrawUtils.DrawBorder(g, tagunitrect, SystemColors.ButtonShadow, SystemColors.ButtonHighlight, 1);
            // рисовать тег
            using (StringFormat sf = new StringFormat())
            {
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                StringBuilder tagunit = new StringBuilder();
                tagunit.Append((string)props["PtName"]);
                using (Font panelfont = new Font("Courier New", 8))
                {
                    g.DrawString(tagunit.ToString(), panelfont,
                        SystemBrushes.ControlText, tagunitrect, sf);
                }
            }
            rect.Y += tagunitrect.Height; // высота панели для тега
            rect.Height -= tagunitrect.Height;
            // рисовать единицу измерения и режим контура
            tagunitrect.Offset(0, tagunitrect.Height);
            using (StringFormat sf = new StringFormat())
            {
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Near;
                StringBuilder tagunit = new StringBuilder();
                tagunit.Append((string)props["EUDesc"]);
                using (Font panelfont = new Font("Courier New", 8))
                {
                    g.DrawString(tagunit.ToString(), panelfont,
                        SystemBrushes.ControlText, tagunitrect, sf);
                }
                sf.Alignment = StringAlignment.Far;
                using (Font panelfont = new Font("Courier New", 8))
                {
                    string[] modes = new string[3] { "РУЧ", "АВТ", "КАС" };
                    Color[] colors = new Color[3] { Color.Yellow, SystemColors.ButtonFace, Color.Aqua };
                    SizeF smodesize = g.MeasureString(modes[konturmode], panelfont);
                    RectangleF moderect = tagunitrect;
                    moderect.Width = smodesize.Width;
                    moderect.X = tagunitrect.X + tagunitrect.Width - moderect.Width;
                    using (Brush brush = new SolidBrush(colors[konturmode]))
                        g.FillRectangle(brush, moderect);
                    g.DrawString(modes[konturmode], panelfont,
                        SystemBrushes.ControlText, tagunitrect, sf);
                }
            }
            rect.Y += tagunitrect.Height; // высота панели для юнита и режима контура
            rect.Height -= tagunitrect.Height;
            #endregion
            RectangleF valrect = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height / 3 - 2);
            for (int i = 0; i < 3; i++)
            {
                #region Внутренняя рамка
                DrawUtils.DrawBorder(g, valrect, SystemColors.ButtonShadow, SystemColors.ButtonHighlight, 2);
                #endregion
                #region Вывод значения
                RectangleF barrect = valrect;
                using (Brush backbrush = new SolidBrush((Color)props["Color"]))
                    g.FillRectangle(backbrush, barrect);
                barrect.Inflate(-1, -1);
                float percent = 0;
                string text = String.Empty;
                Color barlevelcolor = Color.Black;
                switch (i)
                {
                    case 0:
                        percent = pvpercent; text = (string)props["PVText"];
                        barlevelcolor = (Color)props["BarLevelColor"];
                        break;
                    case 1:
                        percent = sppercent; text = (string)props["SPText"];
                        barlevelcolor = (Color)props["BarLevelSPColor"];
                        break;
                    case 2:
                        percent = oppercent; text = (string)props["OPText"] + " %";
                        barlevelcolor = (Color)props["BarLevelOPColor"];
                        break;
                }
                float kf = (percent <= 100f) ? percent / 100f : 1f;
                barrect.Width *= kf;
                using (Brush barbrush = new SolidBrush(barlevelcolor))
                    g.FillRectangle(barbrush, barrect);
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
                                sf.Alignment = StringAlignment.Center;
                                if (g.MeasureString(text, font).Width > valrect.Width) text = "#WIDTH#";
                                valrect.Offset(0, 2);
                                g.DrawString(text, font, fontbrush, valrect, sf);
                                valrect.Offset(0, -2);
                            }
                        }
                    }
                }
                catch
                {
                    using (Font font = new Font("Tahoma", (float)props["FontSize"], style))
                    {
                        using (Brush fontbrush = new SolidBrush((Color)props["FontColor"]))
                            g.DrawString("#FONT#", font, fontbrush, valrect);
                    }
                }
                #endregion
                valrect.Offset(0, valrect.Height + 3);
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
        }

        public System.Drawing.Bitmap PluginPicture
        {
            get { return Draws.Analog.Properties.Resources.dincontur; }
        }

        public IDictionary<string, object> DefaultValues()
        {
            IDictionary<string, object> props = new Dictionary<string, object>();
            props.Add("Name", String.Empty); // имя элемента
            props.Add("Plugin", PluginShortType); // имя плагина
            props.Add("Left", 0f); // x - координата
            props.Add("Top", 0f); // y - координата
            props.Add("Width", 60f); // ширина
            props.Add("Height", 60f); // высота
            props.Add("PVText", "------"); // отображаемый текст по умолчанию
            props.Add("PVPercent", 0m); // процент от шкалы
            props.Add("PVEUHi", 100m); // верхняя граница шкалы
            props.Add("PVEULo", 0m); // нижняя граница шкалы
            props.Add("FormatPV", (int)1);
            props.Add("SPText", "------"); // отображаемый текст по умолчанию
            props.Add("SPPercent", 0m); // процент от шкалы
            props.Add("SPEUHi", 100m); // верхняя граница шкалы
            props.Add("SPEULo", 0m); // нижняя граница шкалы
            props.Add("OPText", "------"); // отображаемый текст по умолчанию
            props.Add("OPPercent", 0m); // процент от шкалы
            props.Add("OPEUHi", 100m); // верхняя граница шкалы
            props.Add("OPEULo", 0m); // нижняя граница шкалы
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
            props.Add("EUDesc", String.Empty); // единица измерения
            props.Add("BackColor", Color.FromArgb(0xFF, 0xFF, 0xFF)); // цвет фона аварии
            props.Add("ForeColor", Color.FromArgb(0x00, 0x00, 0x00)); // цвет шрифта аварии
            props.Add("QuitAlarms", true); // авария квитирована 
            props.Add("HasAlarms", false); // авария есть 
            props.Add("HasLostAlarms", false); // авария упущена 
            props.Add("Blink", false); // флаг мигания 
            props.Add("BarLevelColor", Color.FromArgb(0x00, 0x80, 0x00)); // цвет столбика
            props.Add("BarLevelSPColor", Color.FromArgb(0x00, 0x80, 0x80)); // цвет столбика SP
            props.Add("BarLevelOPColor", Color.FromArgb(0x80, 0x80, 0x00)); // цвет столбика OP
            props.Add("KonturMode", (int)0); // 0 - РУЧной, 1 - АВТоматический, 2 - КАСкадный режим
            props.Add("Quality", String.Empty); // качество данных
            return props;
        }

        public Form ShowEditor(Draw element, UpdateDraw updater, SelectData selector)
        {
            return new frmDinKontur(element, updater, selector);
        }

        public SelectDataKind PluginSelectDataKind
        {
            get { return SelectDataKind.DataPoints; }
        }
    }
}
