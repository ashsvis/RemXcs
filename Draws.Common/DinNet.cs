using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Draws.Plugins;

namespace Draws.Common
{
    [Serializable]
    public class DinNet : IDrawPlugin
    {
        public string PluginShortType { get { return "DinNet"; } }
        public string PluginDescriptor { get { return "Сетка"; } }
        public string PluginCategory { get { return "Общие элементы"; } }

        public RectangleF SizedBoundsRect(IDictionary<string, object> props)
        {
            PointF location = new PointF((float)props["Left"], (float)props["Top"]);
            SizeF size = new SizeF((float)props["Width"], (float)props["Height"]);
            return new RectangleF(location, size);
        }

        private Single rWidth = 60;
        private Single lWidth = 80;
        private RectangleF trendrect;
        private void DrawTrendInit(Graphics g, RectangleF rect, Pen pen)
        {
            trendrect = new RectangleF(rect.Left + lWidth, rect.Top + 10,
                rect.Width - lWidth - rWidth, rect.Height - 20);
            g.DrawRectangle(pen, trendrect.X, trendrect.Y,
                                trendrect.Width, trendrect.Height);
        }
        private void drawSmallLeftRisc(Graphics g, PointF pt, Single h, RectangleF rect, Pen pen)
        {
            int k = 10;
            if (h < 36) k = 5;
            for (int i = 0; i < k; i++)
            {
                int w = (i == 5) ? 6 : 3;
                g.DrawLine(pen, pt, new PointF(pt.X - w, pt.Y));
                pt.Y -= h / k;
                if ((pt.Y < rect.Top) || (pt.Y > rect.Bottom)) break;
            }
        }
        private void drawSmallRightRisc(Graphics g, PointF pt, Single h, RectangleF rect, Pen pen)
        {
            int k = 10;
            if (h < 36) k = 5;
            for (int i = 0; i < k; i++)
            {
                int w = (i == 5) ? 4 : 2;
                g.DrawLine(pen, pt, new PointF(pt.X + w, pt.Y));
                pt.Y -= h / k;
                if ((pt.Y < rect.Top) || (pt.Y > rect.Bottom)) break;
            }
        }
        private void drawLeftRisc(Graphics g, Single value, PointF pt1, RectangleF rect,
            Font font, Brush brush, Pen pen, bool hideText)
        {
            PointF pt2 = new PointF(pt1.X - 6, pt1.Y);
            PointF pt3 = new PointF(rect.Left - lWidth, pt2.Y - 8);
            g.DrawLine(pen, pt1, pt2);
            string text = value.ToString();
            using (StringFormat stringFormat = new StringFormat())
            {
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Near;
                RectangleF r = new RectangleF(pt3, new SizeF(lWidth - 8, font.GetHeight()));
                if (!hideText) g.DrawString(text, font, brush, r, stringFormat);
                if ((pt1.Y > rect.Top) && (pt1.Y < rect.Bottom))
                {
                    PointF pt4 = new PointF(pt1.X + rect.Width, pt1.Y);
                    pen.DashStyle = DashStyle.Dash;
                    g.DrawLine(pen, pt1, pt4);
                    pen.DashStyle = DashStyle.Solid;
                }
            }
        }
        private void drawRightRisc(Graphics g, Single value, PointF pt1, RectangleF rect,
            Font font, Brush brush, Pen pen, bool hideText)
        {
            PointF pt2 = new PointF(pt1.X + 6, pt1.Y);
            PointF pt3 = new PointF(pt2.X + 3, pt2.Y - 8);
            g.DrawLine(pen, pt1, pt2);
            string sval = value.ToString("0 \\%");
            if (!hideText) g.DrawString(sval, font, brush, pt3);
        }
        private void DrawRightAxis(Graphics g, Font font, Brush brush, Pen pen, bool hideText)
        {
            Single h = 30;
            Single k = (Single)Math.Ceiling((trendrect.Height) / h);
            if (k >= 20) k = 20;
            else if (k >= 10) k = 10;
            else if (k >= 5) k = 5;
            else if (k >= 4) k = 4;
            else if (k >= 2) k = 2;
            if (k > 0) h = (trendrect.Height) / k;

            PointF pt = new PointF(trendrect.Right, trendrect.Bottom);
            Single HighRange = 100;
            Single LowRange = 0;
            Single HiLoRange = HighRange - LowRange;
            Single n = LowRange;
            do
            {
                drawRightRisc(g, n, pt, trendrect, font, brush, pen, hideText);
                drawSmallRightRisc(g, pt, h, trendrect, pen);
                pt.Y -= h;
                n += HiLoRange / k;
            } while (pt.Y > trendrect.Top);
            drawRightRisc(g, n, pt, trendrect, font, brush, pen, hideText);
        }
        public void DrawLeftAxis(Graphics g, Single LowRange, Single HighRange, Font font, Brush brush,
            Pen pen, bool hideText)
        {
            Single h = 30;
            Single k = (Single)Math.Ceiling((trendrect.Height) / h);
            if (k >= 20) k = 20;
            else if (k >= 10) k = 10;
            else if (k >= 5) k = 5;
            else if (k >= 4) k = 4;
            else if (k >= 2) k = 2;
            if (k > 0) h = (trendrect.Height) / k;
            PointF pt = new PointF(trendrect.Left, trendrect.Bottom);
            Single HiLoRange = HighRange - LowRange;
            Single n = LowRange;
            do
            {
                drawLeftRisc(g, n, pt, trendrect, font, brush, pen, hideText);
                drawSmallLeftRisc(g, pt, h, trendrect, pen);
                pt.Y -= h;
                n += HiLoRange / k;
            } while (pt.Y > trendrect.Top);
            drawLeftRisc(g, n, pt, trendrect, font, brush, pen, hideText);
            g.DrawLine(pen, new PointF(trendrect.Left, trendrect.Top),
                new PointF(trendrect.Left, trendrect.Bottom));
            g.DrawLine(pen, new PointF(trendrect.Right, trendrect.Top),
                new PointF(trendrect.Right, trendrect.Bottom));
        }
        public void DrawFigure(Graphics g, IDictionary<string, object> props)
        {
            g.SmoothingMode = SmoothingMode.HighSpeed;
            PointF location = new PointF((float)props["Left"], (float)props["Top"]);
            SizeF size = new SizeF((float)props["Width"], (float)props["Height"]);
            RectangleF rect = new RectangleF(location, size);
            FontStyle fontstyle = FontStyle.Regular;
            if ((bool)props["FontBold"]) fontstyle |= FontStyle.Bold;
            if ((bool)props["FontItalic"]) fontstyle |= FontStyle.Italic;
            if ((bool)props["FontUnderline"]) fontstyle |= FontStyle.Underline;
            if ((bool)props["FontStrikeOut"]) fontstyle |= FontStyle.Strikeout;
            string fontname = (string)props["FontName"];
            float fontsize = (float)props["FontSize"];
            bool showPersents = (bool)props["ShowPersents"];
            bool hideText = (bool)props["HideText"];
            using (Font font = new Font(fontname, fontsize, fontstyle))
            {
                Color fontcolor = (Color)props["FontColor"];
                using (Brush brush = new SolidBrush(fontcolor))
                {
                    using (Pen pen = new Pen(fontcolor))
                    {
                        //StringFormat sf = new StringFormat();
                        //sf.LineAlignment = StringAlignment.Center;
                        //sf.Alignment = StringAlignment.Center;
                        lWidth = hideText ? 5 : 60;
                        rWidth = showPersents ? 50 : 5;
                        rWidth = hideText ? 5 : rWidth;
                        g.Clip = new Region(rect);
                        DrawTrendInit(g, rect, pen);
                        DrawLeftAxis(g, (float)props["ScaleLow"], (float)props["ScaleHigh"],
                            font, brush, pen, hideText);
                        if (showPersents) DrawRightAxis(g, font, brush, pen, hideText);
                        g.ResetClip();
                    }
                }
            }
        }

        public System.Drawing.Bitmap PluginPicture
        {
            get { return Draws.Common.Properties.Resources.DinNet; }
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
            props.Add("DinType", 0);
            props.Add("Solid", false); // режим рисования фона
            props.Add("FontName", "Tahoma");
            props.Add("FontColor", Color.FromArgb(0x00, 0x00, 0x00));
            props.Add("FontSize", 9f);
            props.Add("FontBold", false);
            props.Add("FontItalic", false);
            props.Add("FontUnderline", false);
            props.Add("FontStrikeOut", false);
            props.Add("ShowPersents", false); // показывать правую шкалу (%)
            props.Add("HideText", false); // спрятать текст шкал
            props.Add("ScaleLow", 0f); // низ левой шкалы
            props.Add("ScaleHigh", 100f); // верх левой шкалы
            return props;
        }

        public Form ShowEditor(Draw element, UpdateDraw updater, SelectData selector)
        {
            return new frmDinNet(element, updater, selector);
        }

        public SelectDataKind PluginSelectDataKind
        {
            get { return SelectDataKind.DataSchemes; }
        }
    }
}
