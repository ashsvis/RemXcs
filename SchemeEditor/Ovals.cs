using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace SchemeEditor
{
    [Serializable]
    public class Ovals : Polygones
    {
        private List<PointF> points = new List<PointF>();
        public Ovals(RectangleF rect) : this(new Stroke(), new Fill(), new Text(), rect) { }
        public Ovals(Stroke stroke, Fill fill, Text text, RectangleF rect) :
        base(stroke, fill, text, rect)
        {
            Single DXY = 0.55228475F;
            Single[] X = new Single[13];
            Single[] Y = new Single[13];
            PointF[] Pts = new PointF[13];
            Single A = rect.Width * 0.5F;
            Single B = rect.Height * 0.5F;
            Single DX = A * DXY;
            Single DY = B * DXY;
            X[0] = A;    Y[0] = 0;
            X[1] = A;    Y[1] = DY;
            X[2] = DX;   Y[2] = B;
            X[3] = 0;    Y[3] = B;
            X[4] = -DX;  Y[4] = B;
            X[5] = -A;   Y[5] = DY;
            X[6] = -A;   Y[6] = 0;
            X[7] = -A;   Y[7] = -DY;
            X[8] = -DX;  Y[8] = -B;
            X[9] = 0;    Y[9] = -B;
            X[10] = DX;  Y[10] = -B;
            X[11] = A;   Y[11] = -DY;
            X[12] = A;   Y[12] = 0;
            Single CX = rect.X + A;
            Single CY = rect.Y + B;
            for (int i = 0; i < 13; i++)
            {
                Pts[i].X = CX + X[i];
                Pts[i].Y = CY + Y[i];
            }
            for (int i = 0; i < Pts.Length; i++) points.Add(Pts[i]);
        }
        public override void AddPointsRange(PointF[] pts)
        {
            points.Clear();
            points.AddRange(pts);
        }
        protected override void DrawCustomFigure(Graphics g, Pen p, PointF[] ps)
        {   // перегрузка этого метода позволяет перемещать фигуры
            g.DrawBeziers(p, ps); // Рисование контура
        }
        public override void AddGraphicsContent(GraphicsPath gp, RectangleF rect) // перегружен
        {
           // определяет содержимое графической части
            gp.AddBeziers(GetPoints());
            gp.CloseFigure();
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
