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
    public class DinLine : IDrawPlugin
    {
        public string PluginShortType { get { return "DinLine"; } }
        public string PluginDescriptor { get { return "Динамическая линия"; } }
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
            RectangleF rect = new RectangleF(location, size);
            int linekind = (int)props["LineKind"];
            bool noparam = String.IsNullOrWhiteSpace((string)props["PtName"]);
            Color color;
            float linewidth;
            int linestyle;
            if (noparam)
            {
                color = (Color)props["Color"];
                linewidth = (float)props["LineWidth"];
                linestyle = (int)props["LineStyle"];
            }
            else if ((bool)props["PV"])
            {
                color = (Color)props["Color1"];
                linewidth = (float)props["LineWidth1"];
                linestyle = (int)props["LineStyle1"];
            }
            else
            {
                color = (Color)props["Color0"];
                linewidth = (float)props["LineWidth0"];
                linestyle = (int)props["LineStyle0"];
            }
            using (Pen pen = new Pen(color, linewidth))
            {
                pen.DashStyle = (System.Drawing.Drawing2D.DashStyle)(linestyle);
                #region прорисовка по типу линии
                switch (linekind)
                {
                    case 0: // горизонтально
                        g.DrawLine(pen, new PointF(rect.X, rect.Y + rect.Height / 2),
                            new PointF(rect.X + rect.Width, rect.Y + rect.Height / 2));
                        break;
                    case 1: // вертикально
                        g.DrawLine(pen, new PointF(rect.X + rect.Width / 2, rect.Y),
                            new PointF(rect.X + rect.Width / 2, rect.Y + rect.Height));
                        break;
                    case 2: // диагональ снизу-вверх
                        g.DrawLine(pen, new PointF(rect.X, rect.Y + rect.Height),
                            new PointF(rect.X + rect.Width, rect.Y));
                        break;
                    case 3: // диагональ сверху-вниз
                        g.DrawLine(pen, new PointF(rect.X, rect.Y),
                            new PointF(rect.X + rect.Width, rect.Y + rect.Height));
                        break;
                    case 4: // верх-право
                        g.DrawLines(pen, new PointF[] {
                            new PointF(rect.X, rect.Y),
                            new PointF(rect.X + rect.Width, rect.Y),
                            new PointF(rect.X + rect.Width, rect.Y + rect.Height)});
                        break;
                    case 5: // низ-право
                        g.DrawLines(pen, new PointF[] {
                            new PointF(rect.X, rect.Y + rect.Height),
                            new PointF(rect.X + rect.Width, rect.Y + rect.Height),
                            new PointF(rect.X + rect.Width, rect.Y)});
                        break;
                    case 6: // лево-низ
                        g.DrawLines(pen, new PointF[] {
                            new PointF(rect.X, rect.Y),
                            new PointF(rect.X, rect.Y + rect.Height),
                            new PointF(rect.X + rect.Width, rect.Y + rect.Height)});
                        break;
                    case 7: // лево-вверх
                        g.DrawLines(pen, new PointF[] {
                            new PointF(rect.X, rect.Y + rect.Height),
                            new PointF(rect.X, rect.Y),
                            new PointF(rect.X + rect.Width, rect.Y)});
                        break;
                    case 8: // квадрат
                        g.DrawRectangles(pen, new RectangleF[] { rect });
                        break;
                    case 9: // круг
                        g.DrawEllipse(pen, rect);
                        break;
                }
                #endregion
            }
        }

        public System.Drawing.Bitmap PluginPicture
        {
            get { return Draws.Digital.Properties.Resources.DinLine; }
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
            props.Add("DinType", 7);
            props.Add("PtName", String.Empty);
            props.Add("PtParam", "PV");
            props.Add("Color", Color.FromArgb(0xFF, 0xFF, 0xFF));
            props.Add("PV", false);
            props.Add("LineKind", 3);
            props.Add("LineWidth", 3f);
            props.Add("LineStyle", 0);
            props.Add("Color0", Color.FromArgb(0xFF, 0xFF, 0xFF));
            props.Add("LineWidth0", 1f);
            props.Add("LineStyle0", 0);
            props.Add("Color1", Color.FromArgb(0xFF, 0xFF, 0xFF));
            props.Add("LineWidth1", 1f);
            props.Add("LineStyle1", 0);
            props.Add("Quality", String.Empty); // качество данных
            return props;
        }

        public Form ShowEditor(Draw element, UpdateDraw updater, SelectData selector)
        {
            return new frmDinLine(element, updater, selector);
        }

        public SelectDataKind PluginSelectDataKind
        {
            get { return SelectDataKind.DataPoints; }
        }
    }
}
