using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Draws.Plugins;

namespace Draws.Digital
{
    [Serializable]
    public class DinValve : IDrawPlugin
    {
        public string PluginShortType { get { return "DinValve"; } }
        public string PluginDescriptor { get { return "Управление задвижкой"; } }
        public string PluginCategory { get { return "Дискретные элементы"; } }

        public RectangleF SizedBoundsRect(IDictionary<string, object> props)
        {
            PointF location = new PointF((float)props["Left"], (float)props["Top"]);
            SizeF size = new SizeF((float)props["Width"], (float)props["Height"]);
            RectangleF rect = new RectangleF(location, size);
            int spinvalve = (int)props["SpinValve"];
            List<PointF> points = new List<PointF>();
            DrawUtils.AddPointsRange(points, new PointF[4]
                { new PointF(rect.X, rect.Y),
                  new PointF(rect.X + rect.Width, rect.Y + rect.Height),
                  new PointF(rect.X + rect.Width, rect.Y),
                  new PointF(rect.X, rect.Y + rect.Height)});
            float[] angles = new float[4] { 0, 90, -45, 45 };
            DrawUtils.Rotate(points, angles[spinvalve], rect);
            float minX, maxX, minY, maxY;
            minX = rect.Right;
            minY = rect.Bottom;
            maxX = rect.Left; 
            maxY = rect.Top;
            foreach (PointF pnt in points)
            {
                if (minX > pnt.X) minX = pnt.X;
                if (minY > pnt.Y) minY = pnt.Y;
                if (maxX < pnt.X) maxX = pnt.X;
                if (maxY < pnt.Y) maxY = pnt.Y;
            }
            rect.X = minX;
            rect.Y = minY;
            rect.Size = new SizeF(maxX - minX, maxY - minY);
            return rect;
        }

        public void DrawFigure(Graphics g, IDictionary<string, object> props)
        {
            PointF location = new PointF((float)props["Left"], (float)props["Top"]);
            SizeF size = new SizeF((float)props["Width"], (float)props["Height"]);
            RectangleF rect = new RectangleF(location, size);
            int spinvalve = (int)props["SpinValve"];
            int pv = (int)props["PV"];
            bool noparam = String.IsNullOrWhiteSpace((string)props["PtName"]);
            Color backcolor;
            Color forecolor;
            if (noparam || !noparam && !props["Quality"].ToString().Equals("GOOD"))
            {
                backcolor = Color.Blue;
                forecolor = Color.DarkBlue;
            }
            else
            {
                backcolor = (Color)props["BackColor" + pv];
                forecolor = (Color)props["ForeColor" + pv];
            }
            List<PointF> points = new List<PointF>();
            DrawUtils.AddPointsRange(points, new PointF[4]
                { new PointF(rect.X, rect.Y),
                  new PointF(rect.X + rect.Width, rect.Y + rect.Height),
                  new PointF(rect.X + rect.Width, rect.Y),
                  new PointF(rect.X, rect.Y + rect.Height)});
            float[] angles = new float[4] { 0, 90, -45, 45 };
            using (Pen pen = new Pen(forecolor, (pv < 3) ? 1f : 2f))
            {
                using (Brush brush = new SolidBrush(backcolor))
                {
                    #region прорисовка по типу линии
                    DrawUtils.Rotate(points, angles[spinvalve], rect);
                    g.FillPolygon(brush, DrawUtils.GetPoints(points));
                    g.DrawPolygon(pen, DrawUtils.GetPoints(points));
                    #endregion
                }
            }
            bool quit = (bool)props["QuitAlarms"];
            bool alarms = (bool)props["HasAlarms"];
            bool lostalarms = (bool)props["HasLostAlarms"];
            bool blink = (bool)props["Blink"];
            if (!quit && blink && (alarms || lostalarms))
            {
                rect = new RectangleF(location, size);
                using (Pen alarmpen = new Pen(backcolor))
                {
                    alarmpen.Width = 2;
                    g.DrawPolygon(alarmpen, DrawUtils.GetPoints(points));
                }
            }
        }

        public System.Drawing.Bitmap PluginPicture
        {
            get { return Draws.Digital.Properties.Resources.DinValve; }
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
            props.Add("PV", (int)0); //000 0-ход; 1-закрыто; 2-закрыто; 3..7-авария;
            props.Add("OP", (int)0); //00: 0-нет команды; 1-закрыть; 2-открыть;
            props.Add("SpinValve", (int)0); // 0- --; 1- |; 2- /; 3- \; 
            // Состояние 000 - ХОД
            props.Add("BackColor0", Color.FromArgb(0xFF, 0xFF, 0xFF));
            props.Add("ForeColor0", Color.FromArgb(0xFF, 0xFF, 0xFF));
            // Состояние 001 - ЗАКРЫТО
            props.Add("BackColor1", Color.FromArgb(0x00, 0x00, 0x00));
            props.Add("ForeColor1", Color.FromArgb(0x00, 0x00, 0x00));
            // Состояние 010 - ОТКРЫТО
            props.Add("BackColor2", Color.FromArgb(0x00, 0xFF, 0x00));
            props.Add("ForeColor2", Color.FromArgb(0x00, 0xFF, 0x00));
            // Состояние 011 - ОБА ЗАМКНУТЫ
            props.Add("BackColor3", Color.FromArgb(0xFF, 0x00, 0x00));
            props.Add("ForeColor3", Color.FromArgb(0x00, 0x00, 0x00));
            // Состояние 100 - ХОД ( + АВАРИЯ)
            props.Add("BackColor4", Color.FromArgb(0xFF, 0xFF, 0xFF));
            props.Add("ForeColor4", Color.FromArgb(0xFF, 0x00, 0x00));
            // Состояние 101 - ЗАКРЫТО ( + АВАРИЯ)
            props.Add("BackColor5", Color.FromArgb(0x00, 0x00, 0x00));
            props.Add("ForeColor5", Color.FromArgb(0xFF, 0x00, 0x00));
            // Состояние 110 - ОТКРЫТО ( + АВАРИЯ)
            props.Add("BackColor6", Color.FromArgb(0x00, 0xFF, 0x00));
            props.Add("ForeColor6", Color.FromArgb(0xFF, 0x00, 0x00));
            // Состояние 111 - ОБА ЗАМКНУТЫ ( + АВАРИЯ)
            props.Add("BackColor7", Color.FromArgb(0xFF, 0x00, 0x00));
            props.Add("ForeColor7", Color.FromArgb(0xFF, 0x00, 0x00));
            props.Add("QuitAlarms", true); // авария квитирована 
            props.Add("HasAlarms", false); // авария есть 
            props.Add("HasLostAlarms", false); // авария упущена 
            props.Add("Blink", false); // флаг мигания 
            props.Add("Quality", String.Empty); // качество данных
            return props;
        }

        public Form ShowEditor(Draw element, UpdateDraw updater, SelectData selector)
        {
            return new frmDinValve(element, updater, selector);
        }

        public SelectDataKind PluginSelectDataKind
        {
            get { return SelectDataKind.DataPoints; }
        }

        public RectangleF BoundsRect
        { 
            get 
            {
                RectangleF rect = BoundsRect;
                return rect;
            } 
        }

    }
}
