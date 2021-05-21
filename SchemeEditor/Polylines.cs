using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SchemeEditor
{
    [Serializable]
    public class Polylines: Polygones
    {
        private List<PointF> points = new List<PointF>();
        public Polylines(Single x1, Single y1, Single x2, Single y2) :
            this(new Stroke(), new Fill(), new Text(), new RectangleF(), x1, y1, x2, y2) { }
        public Polylines(Stroke stroke, Fill fill, Text text, RectangleF rect, Single x1, Single y1, Single x2, Single y2) :
        base(stroke, fill, text, rect)
        {
            points.Add(new PointF(x1, y1));
            points.Add(new PointF(x2, y2));
        }
        public override RectangleF GetBounds
        {
            get
            {
                using (GraphicsPath gp = new GraphicsPath())
                {
                    gp.AddLines(GetPoints());
                    RectangleF rect = gp.GetBounds();
                    if (rect.Width == 0) { rect.X -= 2; rect.Width += 4; }
                    if (rect.Height == 0) { rect.Y -= 2; rect.Height += 4; }
                    return (rect);
                }
            }
        }
        public override void AddGraphicsContent(GraphicsPath gp, RectangleF rect)
        {   // определяет содержимое графичеcкой части
            gp.AddLines(GetPoints());
        }
        public override PointF[] GetPoints()
        {   // возвращает массив точек линии
            PointF[] ps = new PointF[points.Count];
            points.CopyTo(ps);
            return (ps);
        }
        public override void AddPointsRange(PointF[] pts)
        {
            points.Clear();
            points.AddRange(pts);
        }
        protected override void DrawCustomFigure(Graphics g, Pen p, PointF[] ps)
        {
            g.DrawLines(p, ps); // Рисование контура
        }

        public override Boolean PointInFigure(PointF p)
        {
            using (GraphicsPath gp = new GraphicsPath())
            {
                PointF[] ps = GetPoints();
                for (int i = 1; i < ps.Length; i++)
                {
                    gp.StartFigure();
                    gp.AddPolygon(onebuff((PointF)ps[i - 1], (PointF)ps[i]));
                    gp.CloseFigure();
                }
                return (gp.IsVisible(p));
            }
        }

        protected override void RemovePointByIndex(int markerIndex)
        {
            points.RemoveAt(markerIndex);
        }
        protected override void InsertPointBeforeIndex(int markerIndex, PointF pnt)
        {
            points.Insert(markerIndex, pnt);
        }
    }
}
