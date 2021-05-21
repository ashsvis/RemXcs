using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Draws.Plugins;
using Points.Plugins;

namespace Draws.Digital
{
    [Serializable]
    public class DinButton : IDrawPlugin
    {
        public string PluginShortType { get { return "DinButton"; } }
        public string PluginDescriptor { get { return "Кнопка управления"; } }
        public string PluginCategory { get { return "Дискретные элементы"; } }

        public RectangleF SizedBoundsRect(IDictionary<string, object> props)
        {
            PointF location = new PointF((float)props["Left"], (float)props["Top"]);
            SizeF size = new SizeF((float)props["Width"], (float)props["Height"]);
            return new RectangleF(location, size);
        }

        public void DrawFigure(Graphics g, IDictionary<string, object> props)
        {
            bool noparam = String.IsNullOrWhiteSpace((string)props["PtName"]);
            PointF location = new PointF((float)props["Left"], (float)props["Top"]);
            SizeF size = new SizeF((float)props["Width"], (float)props["Height"]);
            RectangleF rect = new RectangleF(location, size);
            g.FillRectangle(SystemBrushes.ButtonFace, rect);
            bool pv = false;
            #region Внешняя рамка
            if (noparam || !noparam && !props["Quality"].ToString().Equals("GOOD"))
            {
                DrawUtils.DrawBorder(g, rect, SystemColors.ButtonHighlight, SystemColors.ButtonShadow, 2);
            }
            else
            {
                DrawUtils.DrawBorder(g, rect, SystemColors.ButtonHighlight, SystemColors.ButtonShadow, 2);
                if ((bool)props["PV"])
                {
                    pv = true;
                    rect.Inflate(-1, -1); // толщина панели
                    DrawUtils.DrawBorder(g, rect, SystemColors.ButtonShadow, SystemColors.ButtonHighlight, 2);
                }
            }
            rect.Inflate(-2, -2); // толщина панели
            #endregion
            #region Вывод текста
            FontStyle style = FontStyle.Regular;
            if ((bool)props["FontBold"]) style |= FontStyle.Bold;
            if ((bool)props["FontItalic"]) style |= FontStyle.Italic;
            if ((bool)props["FontUnderline"]) style |= FontStyle.Underline;
            if ((bool)props["FontStrikeOut"]) style |= FontStyle.Strikeout;
            string fontname = (string)props["FontName"];
            float fontsize = (float)props["FontSize"];
            int ilevel = (int)props["UserLevel"];
            bool enabled = ilevel >= UserInfo.UserLevelToInt(UserLevel.Оператор);
            using (Font font = new Font(fontname, fontsize, style))
            {
                using (Brush brush = new SolidBrush((Color)props["FontColor"]))
                {
                    using (StringFormat sf = new StringFormat())
                    {
                        sf.LineAlignment = StringAlignment.Center;
                        sf.Alignment = StringAlignment.Center;
                        string value = (string)props["Text"];
                        if (pv) rect.Offset(1, 1);
                        if (enabled)
                            g.DrawString(value, font, brush, rect, sf);
                        else
                        {
                            rect.Offset(1, 1);
                            g.DrawString(value, font, SystemBrushes.ButtonHighlight , rect, sf);
                            rect.Offset(-1, -1);
                            g.DrawString(value, font, SystemBrushes.GrayText, rect, sf);
                        }
                    }
                }
            }
            #endregion
        }

        public System.Drawing.Bitmap PluginPicture
        {
            get { return Draws.Digital.Properties.Resources.DinButton; }
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
            props.Add("Text", "Кнопка"); // отображаемый текст по умолчанию
            props.Add("DinType", 6);
            props.Add("PtName", String.Empty);
            props.Add("PtParam", "PV"); // имя параметра
            props.Add("Fixed", false); // кнопка с фиксацией
            props.Add("Direct", true);
            props.Add("FontName", "Tahoma");
            props.Add("FontColor", Color.FromArgb(0x00, 0x00, 0x00));
            props.Add("FontSize", 9f);
            props.Add("FontBold", false);
            props.Add("FontItalic", false);
            props.Add("FontUnderline", false);
            props.Add("FontStrikeOut", false);
            props.Add("PV", false);
            props.Add("Quality", String.Empty); // качество данных
            props.Add("UserLevel", UserInfo.UserLevelToInt(UserLevel.Оператор)); // уровень доступа для перехода
            return props;
        }

        public Form ShowEditor(Draw element, UpdateDraw updater, SelectData selector)
        {
            return new frmDinButton(element, updater, selector);
        }

        public SelectDataKind PluginSelectDataKind
        {
            get { return SelectDataKind.DataPoints; }
        }
    }
}
