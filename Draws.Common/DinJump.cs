using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Draws.Plugins;
using Points.Plugins;

namespace Draws.Common
{
    [Serializable]
    public class DinJump : IDrawPlugin
    {
        public string PluginShortType { get { return "DinJump"; } }
        public string PluginDescriptor { get { return "Переход"; } }
        public string PluginCategory { get { return "Общие элементы"; } }

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
            RectangleF rect = new RectangleF(location, size);
            if ((bool)props["Framed"]) // включен режим показа рамки
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
                #endregion
            }
            #region Вывод текста
            if ((bool)props["Solid"]) // включен режим заливки фона
            {
                using (Brush backbrush = new SolidBrush((Color)props["Color"]))
                {
                    g.FillRectangle(backbrush, rect);
                }
            }
            FontStyle fontstyle = FontStyle.Regular;
            if ((bool)props["FontBold"]) fontstyle |= FontStyle.Bold;
            if ((bool)props["FontItalic"]) fontstyle |= FontStyle.Italic;
            if ((bool)props["FontUnderline"]) fontstyle |= FontStyle.Underline;
            if ((bool)props["FontStrikeOut"]) fontstyle |= FontStyle.Strikeout;
            string fontname = (string)props["FontName"];
            float fontsize = (float)props["FontSize"];
            int keylevel = (int)props["KeyLevel"];
            int сurrlevel = (int)props["UserLevel"];
            bool enabled = сurrlevel >= keylevel;
            using (Font font = new Font(fontname, fontsize, fontstyle))
            {
                using (Brush brush = new SolidBrush((Color)props["FontColor"]))
                {
                    using (StringFormat sf = new StringFormat())
                    {
                        sf.LineAlignment = StringAlignment.Center;
                        sf.Alignment = StringAlignment.Center;
                        string value = (string)props["Text"];
                        g.DrawString(value, font, brush, rect, sf);
                    }
                }
            }
            #endregion
        }

        public System.Drawing.Bitmap PluginPicture
        {
            get { return Draws.Common.Properties.Resources.DinJump; }
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
            props.Add("Text", String.Empty); // отображаемый текст по умолчанию
            props.Add("DinType", 0);
            props.Add("Framed", false); // режим рисования рамки
            props.Add("FrameWidth", 2f);
            props.Add("Solid", false); // режим рисования фона
            props.Add("ScreenName", String.Empty);
            props.Add("Color", Color.FromArgb(0x00, 0x00, 0x00));
            props.Add("KeyLevel", (int)0); // уровень доступа для перехода
            props.Add("UserLevel", (int)0); // текущий уровень доступа
            props.Add("FontName", "Tahoma");
            props.Add("FontColor", Color.FromArgb(0xFF, 0xFF, 0xFF));
            props.Add("FontSize", 9f);
            props.Add("FontBold", false);
            props.Add("FontItalic", false);
            props.Add("FontUnderline", false);
            props.Add("FontStrikeOut", false);
            return props;
        }

        public Form ShowEditor(Draw element, UpdateDraw updater, SelectData selector)
        {
            return new frmDinJump(element, updater, selector);
        }

        public SelectDataKind PluginSelectDataKind
        {
            get { return SelectDataKind.DataSchemes; }
        }
    }
}