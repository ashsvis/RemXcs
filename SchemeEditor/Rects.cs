using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SchemeEditor
{
    [Serializable]
    public class Rects : Polygones
    {
        private List<PointF> points = new List<PointF>();
        public Rects(RectangleF rect) : this(new Stroke(), new Fill(), new Text(), rect) { }
        public Rects(Stroke stroke, Fill fill, Text text, RectangleF rect) :
        base(stroke, fill, text, rect)
        {
            points.Add(new PointF(rect.X, rect.Y));
            points.Add(new PointF(rect.X + rect.Width, rect.Y));
            points.Add(new PointF(rect.X + rect.Width, rect.Y + rect.Height));
            points.Add(new PointF(rect.X, rect.Y + rect.Height));
        }
        public override void AddPointsRange(PointF[] pts)
        {
            points.Clear();
            points.AddRange(pts);
        }
        protected override void DrawCustomFigure(Graphics g, Pen p, PointF[] ps)
        {   // перегрузка этого метода позволяет перемещать фигуры
            g.DrawPolygon(p, ps); // Рисование контура
        }
        public override void AddGraphicsContent(GraphicsPath gp, RectangleF rect) // перегружен
        {
            // определяет содержимое графической части
            gp.AddPolygon(GetPoints());
        }
        public override PointF[] GetPoints()
        {   // возвращает массив точек линии
            PointF[] ps = new PointF[points.Count];
            points.CopyTo(ps);
            return (ps);
        }
        public override bool CanNodeChanging()
        {   // индикатор возможности изменения узлов 
            return (false);
        }
        protected override void SetNodeChanging(bool value)
        {   // запрещает изменение узлов
            ChangingNode = false;
        }
    }
}
