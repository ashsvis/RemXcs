using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Draws.Plugins;

namespace Draws.Common
{
    [Serializable]
    public class DinDraw : IDrawPlugin
    {
        List<PointF> points = new List<PointF>();
        public string PluginShortType { get { return "DinDraw"; } }
        public string PluginDescriptor { get { return "Фигура"; } }
        public string PluginCategory { get { return "Общие элементы"; } }

        public RectangleF SizedBoundsRect(IDictionary<string, object> props)
        {
            PointF location = new PointF((float)props["Left"], (float)props["Top"]);
            SizeF size = new SizeF((float)props["Width"], (float)props["Height"]);
            return new RectangleF(location, size);
        }

        public void DrawFigure(Graphics g, IDictionary<string, object> props)
        {
            g.SmoothingMode = SmoothingMode.HighSpeed;
            PointF location = new PointF((float)props["Left"], (float)props["Top"]);
            SizeF size = new SizeF((float)props["Width"], (float)props["Height"]);
            RectangleF rect = new RectangleF(location, size);
            Color backcolor = (Color)props["BackColor"];
            int backalpha = (int)props["BackAlpha"];
            int patternmode = (int)props["PatternMode"];
            Color patterncolor = (Color)props["PatternColor"];
            Color calcbackcolor = Color.FromArgb(backalpha, backcolor);
            points.Clear();
            points.Add(new PointF(rect.Left, rect.Top));
            points.Add(new PointF(rect.Left + rect.Width, rect.Top));
            points.Add(new PointF(rect.Left + rect.Width, rect.Top + rect.Height));
            points.Add(new PointF(rect.Left, rect.Top + rect.Height));
            bool drawexists = false;
            try
            {
                switch (DrawUtils.FillModeFromIndex(patternmode))
                {
                    case Draws.Plugins.FillMode.None: 
                        break;
                    case Draws.Plugins.FillMode.Solid: 
                        using (SolidBrush brush =
                               new SolidBrush(calcbackcolor))
                        {
                            g.FillPolygon(brush, points.ToArray());
                        }
                        drawexists = true;
                        break;
                    case Draws.Plugins.FillMode.LinearGradient:
                        using (LinearGradientBrush brush =
                               new LinearGradientBrush(rect,
                                   patterncolor, calcbackcolor,
                                        DrawUtils.LinearGradientModeFromIndex(patternmode)))
                        {
                            g.FillPolygon(brush, points.ToArray());
                        }
                        drawexists = true;
                        break;
                    case Draws.Plugins.FillMode.Hatch:
                        using (HatchBrush brush =
                               new HatchBrush(DrawUtils.HatchStyleFromIndex(patternmode),
                                        patterncolor, calcbackcolor))
                        {
                            g.FillPolygon(brush, points.ToArray());
                        }
                        drawexists = true;
                        break;
                }
                if (calcbackcolor.A == 0 && patterncolor.A == 0)
                    drawexists = false;
            }
            catch //(Exception ex)
            {
                g.FillRectangle(Brushes.White, rect);
                g.DrawRectangle(Pens.Red, rect.X, rect.Y, rect.Width, rect.Height);
                //Console.WriteLine("{0} Exception caught.", ex);
                //throw;
            }
            /*
            //using (Brush brush = new SolidBrush(backcolor))
            //{
            int Alpha = 255;
            Color PatternColor = (Color)props["PatternColor"];
            LinearGradientMode LinearMode = LinearGradientMode.BackwardDiagonal;
            GraphicsPath gp = new GraphicsPath();
            RectangleF r = rect;
            r.Inflate(r.Width / 2, r.Height / 2);
            //r.Offset(r.Width / 2, r.Height / 2);
            gp.AddEllipse(r);
            using (Brush brush = new SolidBrush(backcolor))
            {
                g.FillPolygon(brush, points.ToArray());
            }
            using (PathGradientBrush brush = new PathGradientBrush(gp))
            //using (Brush brush = new LinearGradientBrush(rect, Color.FromArgb(Alpha, PatternColor),
            //            Color.FromArgb(Alpha, backcolor), LinearMode))
            {
                brush.CenterColor = PatternColor;
                brush.SurroundColors = new Color[] { backcolor };
                //brush.SetSigmaBellShape(0F);
                //brush.FocusScales = pt;
                g.FillPolygon(brush, points.ToArray());
                drawexists = true;
            }
            gp.Dispose();
             */
            if (!drawexists)
            {
                using (Pen pen = new Pen(Color.Black))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    g.DrawRectangles(pen, new RectangleF[] { rect });
                }
            }
        }

        public System.Drawing.Bitmap PluginPicture
        {
            get { return Draws.Common.Properties.Resources.DinDraw; }
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
            props.Add("Points", String.Empty); // содержимое фигуры
            props.Add("BackColor", Color.FromArgb(0x00, 0x00, 0x00));
            props.Add("BackAlpha", (int)255);
            props.Add("PatternColor", Color.FromArgb(0xFF, 0xFF, 0xFF));
            props.Add("PatternMode", (int)1); // сплошной цвет
            props.Add("StrokeColor", Color.FromArgb(0x00, 0x00, 0x00));
            return props;
        }

        public Form ShowEditor(Draw element, UpdateDraw updater, SelectData selector)
        {
            return new frmDinDraw(element, updater, selector);
        }

        public SelectDataKind PluginSelectDataKind
        {
            get { return SelectDataKind.DataSchemes; }
        }
    }
}
