using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Collections.Generic;

namespace SchemeEditor
{
    [Serializable]
    public class Lines : Draws
    {
        private List<PointF> points = new List<PointF>();
        public Lines(Stroke stroke, Fill fill, Single x1, Single y1, Single x2, Single y2) :
        base(stroke, fill)
        {
            points.Add(new PointF(x1, y1));
            points.Add(new PointF(x2, y2));
        }
        public List<PointF> Points
        {
            get
            {
                return (points);
            }
            set
            {
                points = value;
            }
        }
        public override void DeleteFigureNode(int markerIndex)
        {
            if ((points.Count > 2) && (markerIndex < points.Count - 1))
            {
                points.RemoveAt(Math.Abs(markerIndex)-1);
            }
        }
        private bool ptInRange(PointF p, PointF p1, PointF p2)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddPolygon(onebuff(p1, p2));
            Region rg = new Region(gp);
            return (rg.IsVisible(p));
        }
        public override void InsertFigureNode(PointF pnt)
        {
            for (int i = 1; i < points.Count; i++)
            {
                if (ptInRange(pnt, (PointF)points[i-1], (PointF)points[i]))
                {
                    points.Insert(i, new PointF(pnt.X, pnt.Y));
                    break;
                }
            }
        }
        public override void DrawFigure(Graphics g)
        {
            g.DrawPath(this.Stroke.Pen(), this.GetLinePath(new PointF(0, 0))); // Рисование
        }
        public override RectangleF[] GetNodeMarkers()
        {
            RectangleF[] rects = new RectangleF[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                PointF pt = (PointF)points[i];
                rects[i] = RectangleF.FromLTRB(pt.X - 3, pt.Y - 3, pt.X + 3, pt.Y + 3);
            }
            return (rects);
        }
        private PointF[] GetPoints()
        {
            PointF[] pts = new PointF[points.Count]; 
            points.CopyTo(pts);
            return (pts);
        }
        public override void DrawFocusFigure(Graphics g, Pen p, PointF pnt, int markerIndex)
        {
            if (pnt.IsEmpty)
            {
                GraphicsPath gp = new GraphicsPath();
                RectangleF[] rects = this.GetNodeMarkers();
                gp.StartFigure();
                for (int i=0; i<rects.Length; i++) gp.AddEllipse(rects[i]); // маркеры узлов рисуем круглым

                RectangleF rect = this.GetBounds();
                rects = base.GetBoundMarkers(rect);
                gp.StartFigure();
                for (int i = 0; i < rects.Length; i++) if (rects[i].Width > 2) gp.AddRectangle(rects[i]);
                g.FillPath(Brushes.White, gp);
                g.DrawPath(Pens.Black, gp);
            }
            else
            {
                if (markerIndex == 0) // тянут мышкой за тело фигуры
                    g.DrawPath(p, this.GetLinePath(pnt)); // Рисование контура
                else
                if (markerIndex < 0)
                { // тянут мышкой за маркер, изменяющий положение узла
                    PointF[] ps = this.GetLinePath(new PointF(0,0)).PathPoints;
                    int index = Math.Abs(markerIndex) - 1;
                    if ((index >= 0) && (index < ps.Length))
                    {
                        ps[index].X += pnt.X;
                        ps[index].Y += pnt.Y;
                        g.DrawLines(p, ps);
                    }
                }
            }
        }
        public void FlipVertical()
        {
            RectangleF rect = this.GetBounds();
            Single cx = rect.X + rect.Width * 0.5F;
            PointF[] pts = this.GetPoints();
            for (int i = 0; i < pts.Length; i++)
            {
                if (pts[i].X < cx) pts[i].X += (cx - pts[i].X) * 2F;
                else if (pts[i].X > cx) pts[i].X -= (pts[i].X - cx) * 2F;
            }
            points.Clear();
            points.AddRange(pts);
        }
        public void FlipHorizontal()
        {
            RectangleF rect = this.GetBounds();
            Single cy = rect.Y + rect.Height * 0.5F;
            PointF[] pts = this.GetPoints();
            for (int i = 0; i < pts.Length; i++)
            {
                if (pts[i].Y < cy) pts[i].Y += (cy - pts[i].Y) * 2F;
                else if (pts[i].Y > cy) pts[i].Y -= (pts[i].Y - cy) * 2F;
            }
            points.Clear();
            points.AddRange(pts);
        }
        public override void Offset(PointF p) 
        {
            PointF[] pts = this.GetPoints();
            for (int i = 0; i < pts.Length; i++)
            {
                pts[i].X += p.X;
                pts[i].Y += p.Y;
            }
            points.Clear();
            points.AddRange(pts);
        }
        public override void UpdateSize(PointF offset)
        {
            int index = Math.Abs(this.MarkerIndex)-1;
            if ((index >= 0) && (index < points.Count))
            {
                Double angle = this.Angle * Math.PI / 180.0;
                Double cosA = Math.Cos(angle);
                Double sinA = Math.Sin(angle);
                Single dx = offset.X;
                Single dy = offset.Y;
                Single dh = (Single)(dy * cosA - dx * sinA);
                Single dw = (Single)(dx * cosA + dy * sinA);
                PointF[] pts = this.GetPoints();
                pts[index].X += dw;
                pts[index].Y += dh;
                points.Clear();
                points.AddRange(pts);
            }
        }
        public override void MoveUp(Single step) { this.Offset(new PointF(0, -step)); }
        public override void MoveDown(Single step) { this.Offset(new PointF(0, step)); } 
        public override void MoveLeft(Single step) { this.Offset(new PointF(-step, 0)); }
        public override void MoveRight(Single step) { this.Offset(new PointF(step, 0)); }
        private PointF[] onebuff(PointF p1, PointF p2)
        {
            Single selWidth = this.Stroke.Width * 0.5F;
            if ((selWidth < 4F) || (this.Stroke.DashStyle == DashStyle.Custom)) selWidth = 4F;
            Single x1 = p1.X;
            Single y1 = p1.Y;
            Single x2 = p2.X;
            Single y2 = p2.Y;
            Single k = (y2 - y1) / (x2 - x1);
            Single fi1 = (Single)(Math.Atan(k) + Math.PI * 0.5);
            Single fi2 = (Single)(Math.Atan(k) - Math.PI * 0.5);
            PointF[] buff = new PointF[4];
            buff[0].X = (Single)Math.Round(selWidth * Math.Cos(fi1) + x1);
            buff[0].Y = (Single)Math.Round(selWidth * Math.Sin(fi1) + y1);
            buff[1].X = (Single)Math.Round(selWidth * Math.Cos(fi2) + x1);
            buff[1].Y = (Single)Math.Round(selWidth * Math.Sin(fi2) + y1);
            buff[2].X = (Single)Math.Round(selWidth * Math.Cos(fi2) + x2);
            buff[2].Y = (Single)Math.Round(selWidth * Math.Sin(fi2) + y2);
            buff[3].X = (Single)Math.Round(selWidth * Math.Cos(fi1) + x2);
            buff[3].Y = (Single)Math.Round(selWidth * Math.Sin(fi1) + y2);
            return (buff);
        }
        public override Boolean PtInFigure(PointF p)
        {
            GraphicsPath gp = new GraphicsPath();
            for (int i = 1; i < points.Count; i++)
            {
                gp.StartFigure();
                gp.AddPolygon(onebuff((PointF)points[i - 1], (PointF)points[i]));
                gp.CloseFigure();
            }
            Region rg = new Region(gp);
            Region rm = new Region(this.GetMarkersPath(p));
            return (rg.IsVisible(p)||rm.IsVisible(p));
        }

        private GraphicsPath GetLinePath(PointF offset)
        {
            GraphicsPath gp = new GraphicsPath();
            AddGraphicsContent(gp, GetBounds());
            return gp;
        }
 
        protected override RectangleF GetBounds()
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddLines(this.GetPoints());
            return (gp.GetBounds());
        }
        public override RectangleF RectForSelect()
        {
            return (this.GetBounds());
        }
        public override void AddGraphicsContent(GraphicsPath gp, RectangleF rect)
        {
            gp.AddLines(this.GetPoints()); // определяет содержимое графичеcкой части
        }
    }
}
