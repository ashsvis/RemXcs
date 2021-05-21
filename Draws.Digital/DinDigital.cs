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
    public class DinDigital : IDrawPlugin
    {
        public string PluginShortType { get { return "DinDigital"; } }
        public string PluginDescriptor { get { return "Дискретное значение"; } }
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
            int digitalkind = (int)props["DigitalKind"];
            int borderkind = (int)props["BorderKind"];
            bool noparam = String.IsNullOrWhiteSpace((string)props["PtName"]);
            Color backcolor = (Color)props["BackColor"];
            bool quit = (bool)props["QuitAlarms"];
            bool alarms = (bool)props["HasAlarms"];
            bool lostalarms = (bool)props["HasLostAlarms"];
            bool blink = (bool)props["Blink"];
            Color color;
            if (noparam || !noparam && !props["Quality"].ToString().Equals("GOOD"))
            {
                color = Color.Blue; //(Color)props["Color"];
            }
            else if ((bool)props["PV"])
            {
                color = (Color)props["Color1"];
            }
            else
            {
                color = (Color)props["Color0"];
            }
            using (Brush brush = new SolidBrush(color))
            {
                #region прорисовка по типу элемента
                switch (digitalkind)
                {
                    case 0: // квадрат
                        rect = MakeSquare(rect);
                        g.FillRectangle(brush, rect);
                        break;
                    case 1: // круг
                        rect = MakeSquare(rect);
                        g.FillEllipse(brush, rect);
                        break;
                    case 2: // прямоугольник
                        g.FillRectangle(brush, rect);
                        break;
                    case 3: // эллипс
                        g.FillEllipse(brush, rect);
                        break;
                }
                #endregion
                #region прорисовка по типу рамки
                switch (borderkind)
                {
                    case 0: // нет рамки
                        break;
                    case 1: // одиночная рамка
                        switch (digitalkind)
                        {
                            case 0: // квадрат
                                rect = MakeSquare(rect);
                                g.DrawRectangles(Pens.Black, new RectangleF[] { rect });
                                break;
                            case 1: // круг
                                rect = MakeSquare(rect);
                                g.DrawEllipse(Pens.Black, rect);
                                break;
                            case 2: // прямоугольник
                                g.DrawRectangles(Pens.Black, new RectangleF[] { rect });
                                break;
                            case 3: // эллипс
                                g.DrawEllipse(Pens.Black, rect);
                                break;
                        }
                        break;
                    case 2: // рамка с тенью
                        switch (digitalkind)
                        {
                            case 0: // квадрат
                                rect = MakeSquare(rect);
                                DrawRectBorder(g, rect);
                                break;
                            case 1: // круг
                                rect = MakeSquare(rect);
                                DrawEllipceBorder(g, rect);
                                break;
                            case 2: // прямоугольник
                                DrawRectBorder(g, rect);
                                break;
                            case 3: // эллипс
                                DrawEllipceBorder(g, rect);
                                break;
                        }
                        break;
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
        }

        private void DrawRectBorder(Graphics g, RectangleF rect)
        {
            #region Внешняя рамка
            DrawUtils.DrawBorder(g, rect, SystemColors.ButtonHighlight, SystemColors.ButtonShadow, 1);
            rect.Inflate(-1, -1);
            using (Pen pen = new Pen(SystemColors.ButtonFace, 2))
                g.DrawRectangle(pen, Rectangle.Ceiling(rect));
            rect.Inflate(-1, -1); // толщина панели
            #endregion
            #region Внутренняя рамка
            DrawUtils.DrawBorder(g, rect, SystemColors.ButtonShadow, SystemColors.ButtonHighlight, 1);
            #endregion
        }

        private void DrawEllipceBorder(Graphics g, RectangleF rect)
        {
            #region Внешняя рамка
            using (Pen pen = new Pen(SystemColors.ButtonHighlight, 1))
                g.DrawArc(pen, rect, 135, 135 + 45);
            using (Pen pen = new Pen(SystemColors.ButtonShadow, 1))
                g.DrawArc(pen, rect, -45, -45 + 180 + 45);
            rect.Inflate(-1, -1);
            using (Pen pen = new Pen(SystemColors.ButtonFace, 2))
                g.DrawEllipse(pen, rect);
            rect.Inflate(-2, -2); // толщина панели
            #endregion
            #region Внутренняя рамка
            using (Pen pen = new Pen(SystemColors.ButtonShadow, 1))
                g.DrawArc(pen, rect, 135, 135 + 45);
            using (Pen pen = new Pen(SystemColors.ButtonHighlight, 1))
                g.DrawArc(pen, rect, -45, -45 + 180 + 45);
            #endregion
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

        public System.Drawing.Bitmap PluginPicture
        {
            get { return Draws.Digital.Properties.Resources.DinFlag; }
        }

        public IDictionary<string, object> DefaultValues()
        {
            IDictionary<string, object> props = new Dictionary<string, object>();
            props.Add("Name", String.Empty); // имя элемента
            props.Add("Plugin", PluginShortType); // имя плагина
            props.Add("Left", 0f); // x - координата
            props.Add("Top", 0f); // y - координата
            props.Add("Width", 20f); // ширина
            props.Add("Height", 20f); // высота
            props.Add("DinType", 7);
            props.Add("PtName", String.Empty);
            props.Add("PtParam", "PV");
            props.Add("Color", Color.FromArgb(0x00, 0x00, 0xFF));
            props.Add("PV", false);
            props.Add("DigitalKind", 0);
            props.Add("BorderKind", 0);
            props.Add("Color0", Color.FromArgb(0x00, 0x00, 0xFF));
            props.Add("Color1", Color.FromArgb(0x00, 0x00, 0x80));
            props.Add("BackColor", Color.FromArgb(0xFF, 0xFF, 0xFF)); // цвет фона аварии
            props.Add("QuitAlarms", true); // авария квитирована 
            props.Add("HasAlarms", false); // авария есть 
            props.Add("HasLostAlarms", false); // авария упущена 
            props.Add("Blink", false); // флаг мигания 
            props.Add("Quality", String.Empty); // качество данных
            return props;
        }

        public Form ShowEditor(Draw element, UpdateDraw updater, SelectData selector)
        {
            return new frmDinDigital(element, updater, selector);
        }

        public SelectDataKind PluginSelectDataKind
        {
            get { return SelectDataKind.DataPoints; }
        }
    }
}
