using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Draws.Plugins;

namespace Draws.Digital
{
    [Serializable]
    public class DinText : IDrawPlugin
    {
        public string PluginShortType { get { return "DinText"; } }
        public string PluginDescriptor { get { return "Динамический текст"; } }
        public string PluginCategory { get { return "Дискретные элементы"; } }

        public RectangleF SizedBoundsRect(IDictionary<string, object> props)
        {
            PointF location = new PointF((float)props["Left"], (float)props["Top"]);
            SizeF size = new SizeF((float)props["Width"], (float)props["Height"]);
            return new RectangleF(location, size);
        }

        public void DrawFigure(Graphics g, IDictionary<string, object> props)
        {
            PointF location = new PointF((float)props["Left"], (float)props["Top"]);
            SizeF size = new SizeF((float)props["Width"], (float)props["Height"]);
            Color alarmcolor = (Color)props["BackColor"];
            bool quit = (bool)props["QuitAlarms"];
            bool alarms = (bool)props["HasAlarms"];
            bool lostalarms = (bool)props["HasLostAlarms"];
            bool blink = (bool)props["Blink"];
            RectangleF rect = new RectangleF(location, size);
            if ((bool)props["ShowPanel"]) // включен режим показа панели
            {
                #region Внешняя рамка
                DrawUtils.DrawBorder(g, rect, SystemColors.ButtonHighlight, SystemColors.ButtonShadow, 2);
                rect.Inflate(-2, -2);
                using (Pen pen = new Pen(SystemColors.ButtonFace, 2))
                    g.DrawRectangle(pen, Rectangle.Ceiling(rect));
                rect.Inflate(-1, -1); // толщина панели
                #endregion
                #region Внутренняя рамка
                DrawUtils.DrawBorder(g, rect, SystemColors.ButtonShadow, SystemColors.ButtonHighlight, 2);
                rect.Inflate(-2, -2); // толщина границы панели
                #endregion
            }
            #region Вывод текста
            bool noparam = String.IsNullOrWhiteSpace((string)props["PtName"]);
            if ((bool)props["Solid"]) // включен режим заливки фона
            {
                Color backcolor;
                if (noparam) backcolor = (Color)props["Color"];
                else if ((bool)props["PV"]) backcolor = (Color)props["Color1"];
                else backcolor = (Color)props["Color0"];
                using (Brush backbrush = new SolidBrush(backcolor))
                    g.FillRectangle(backbrush, rect);
            }
           if ((bool)props["Framed"]) // включен режим вывода рамки
                using (Pen framepen = new Pen((Color)props["FrameColor"]))
                    g.DrawRectangles(framepen, new RectangleF[] { rect });
            string fontname = "Tahoma";
            float fontsize = 9f;
            FontStyle fontstyle = FontStyle.Regular;
            Color fontcolor;
            string value;
            if (noparam)
            {
                if ((bool)props["FontBold"]) fontstyle |= FontStyle.Bold;
                if ((bool)props["FontItalic"]) fontstyle |= FontStyle.Italic;
                if ((bool)props["FontUnderline"]) fontstyle |= FontStyle.Underline;
                if ((bool)props["FontStrikeOut"]) fontstyle |= FontStyle.Strikeout;
                fontname = (string)props["FontName"];
                fontsize = (float)props["FontSize"];
                fontcolor = (Color)props["FontColor"];
                value = (string)props["Text"];
            }
            else if ((bool)props["PV"])
            {
                if ((bool)props["Font1Bold"]) fontstyle |= FontStyle.Bold;
                if ((bool)props["Font1Italic"]) fontstyle |= FontStyle.Italic;
                if ((bool)props["Font1Underline"]) fontstyle |= FontStyle.Underline;
                if ((bool)props["Font1StrikeOut"]) fontstyle |= FontStyle.Strikeout;
                fontname = (string)props["Font1Name"];
                fontsize = (float)props["Font1Size"];
                fontcolor = (Color)props["Font1Color"];
                value = (string)props["Text1"];
            }
            else
            {
                if ((bool)props["Font0Bold"]) fontstyle |= FontStyle.Bold;
                if ((bool)props["Font0Italic"]) fontstyle |= FontStyle.Italic;
                if ((bool)props["Font0Underline"]) fontstyle |= FontStyle.Underline;
                if ((bool)props["Font0StrikeOut"]) fontstyle |= FontStyle.Strikeout;
                fontname = (string)props["Font0Name"];
                fontsize = (float)props["Font0Size"];
                fontcolor = (Color)props["Font0Color"];
                value = (string)props["Text0"];
            }
            try
            {
                using (Font font = new Font(fontname, fontsize, fontstyle))
                {
                    if (!noparam && !props["Quality"].ToString().Equals("GOOD"))
                        fontcolor = Color.Blue;
                    using (Brush brush = new SolidBrush(fontcolor))
                    {
                        using (StringFormat sf = new StringFormat())
                        {
                            sf.LineAlignment = StringAlignment.Center;
                            sf.Alignment = StringAlignment.Center;
                            g.DrawString(value, font, brush, rect, sf);
                        }
                    }
                }
            }
            catch { g.DrawString("Шрифт не поддерживается", SystemFonts.DefaultFont,
                SystemBrushes.ControlText, rect); }
            if (!quit && blink && (alarms || lostalarms))
            {
                rect = new RectangleF(location, size);
                using (Pen alarmpen = new Pen(alarmcolor))
                {
                    alarmpen.Width = 2;
                    g.DrawRectangles(alarmpen, new RectangleF[] { rect });
                }
            }
            #endregion
        }

        public System.Drawing.Bitmap PluginPicture
        {
            get { return Draws.Digital.Properties.Resources.DinText; }
        }

        public IDictionary<string, object> DefaultValues()
        {
            IDictionary<string, object> props = new Dictionary<string, object>();
            props.Add("Name", String.Empty); // имя элемента
            props.Add("Plugin", PluginShortType); // имя плагина
            props.Add("Left", 0f); // x - координата
            props.Add("Top", 0f); // y - координата
            props.Add("Width", 50f); // ширина
            props.Add("Height", 20f); // высота
            props.Add("DinType", 2);
            props.Add("PtName", String.Empty);
            props.Add("PtParam", "PV");
            props.Add("Color", Color.FromArgb(0xFF, 0xFF, 0xFF));
            props.Add("Text", "Текст"); // отображаемый текст по умолчанию
            props.Add("PV", false);
            props.Add("BackColor", Color.FromArgb(0xFF, 0xFF, 0xFF)); // цвет фона аварии
            props.Add("ForeColor", Color.FromArgb(0x00, 0x00, 0x00)); // цвет шрифта аварии
            props.Add("QuitAlarms", true); // авария квитирована 
            props.Add("HasAlarms", false); // авария есть 
            props.Add("HasLostAlarms", false); // авария упущена 
            props.Add("Blink", false); // флаг мигания 
            props.Add("FontName", "Tahoma");
            props.Add("FontColor", Color.FromArgb(0x00, 0x00, 0x00));
            props.Add("FontSize", 9f);
            props.Add("FontBold", false);
            props.Add("FontItalic", false);
            props.Add("FontUnderline", false);
            props.Add("FontStrikeOut", false);
            props.Add("ShowPanel", false);
            props.Add("FrameColor", Color.FromArgb(0x00, 0x00, 0x00));
            props.Add("Framed", false);
            props.Add("Solid", false);
            props.Add("Color0", Color.FromArgb(0xFF, 0xFF, 0xFF));
            props.Add("Text0", String.Empty); // отображаемый текст 0
            props.Add("Font0Name", "Tahoma");
            props.Add("Font0Color", Color.FromArgb(0x00, 0x00, 0x00));
            props.Add("Font0Size", 9f);
            props.Add("Font0Bold", false);
            props.Add("Font0Italic", false);
            props.Add("Font0Underline", false);
            props.Add("Font0StrikeOut", false);
            props.Add("Color1", Color.FromArgb(0xFF, 0xFF, 0xFF));
            props.Add("Text1", String.Empty); // отображаемый текст 1
            props.Add("Font1Name", "Tahoma");
            props.Add("Font1Color", Color.FromArgb(0x00, 0x00, 0x00));
            props.Add("Font1Size", 9f);
            props.Add("Font1Bold", false);
            props.Add("Font1Italic", false);
            props.Add("Font1Underline", false);
            props.Add("Font1StrikeOut", false);
            props.Add("Quality", String.Empty); // качество данных
            return props;
        }

        public Form ShowEditor(Draw element, UpdateDraw updater, SelectData selector)
        {
            return new frmDinText(element, updater, selector);
        }

        public SelectDataKind PluginSelectDataKind
        {
            get { return SelectDataKind.DataPoints; }
        }
    }
}

