using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SchemeEditor
{
    [Serializable]
    public class Polygones: Draws
    {
        private List<PointF> points = new List<PointF>();
        public Polygones(RectangleF rect) : this(new Stroke(), new Fill(), new Text(), rect) { }
        public Polygones(Stroke stroke, Fill fill, Text text, RectangleF rect) :
        base(stroke, fill, text)
        {
            points.Add(new PointF(rect.X, rect.Y));
            points.Add(new PointF(rect.X + rect.Width, rect.Y));
            points.Add(new PointF(rect.X + rect.Width, rect.Y + rect.Height));
            points.Add(new PointF(rect.X, rect.Y + rect.Height));
        }
        public List<PointF> Points { get { return (points); } set { points = value; } }
        protected void AddRectanglePath(GraphicsPath gp) 
        { 
            GetRectanglePath(gp, GetBounds);
        }
        protected void GetRectanglePath(GraphicsPath gp, RectangleF rect)
        {
            AddGraphicsContent(gp, rect);
        }
        public override void DrawFigure(Graphics g)
        {
            using (GraphicsPath gp = new GraphicsPath())
            {
                AddRectanglePath(gp);
                RectangleF bounds = gp.GetBounds();
                g.FillPath(this.Fill.Brush(bounds), gp); // Заливка фона
                g.DrawPath(this.Stroke.Pen(), gp); // Рисование контура
                using (Font font = this.Text.Font())
                {
                    StringFormatFlags options = this.Text.Vertical ?
                        StringFormatFlags.DirectionVertical : (StringFormatFlags)0;
                    using (StringFormat format = new StringFormat(options))
                    {
                        format.LineAlignment = this.Text.LineAlignment;
                        format.Alignment = this.Text.Alignment;
                        using (Brush brush = this.Text.FontBrush())
                            g.DrawString(this.Text.Caption, font, brush, bounds, format);
                    }
                }
            }
        }
        public override void AddGraphicsContent(GraphicsPath gp, RectangleF rect)
        {   // определяет содержимое графичеcкой части
            gp.AddPolygon(GetPoints());
        }
        public override PointF[] GetPoints()
        {   // возвращает массив точек линии
            PointF[] ps = new PointF[points.Count];
            points.CopyTo(ps);
            return (ps);
        }
        public override RectangleF[] GetNodeMarkers()
        {   // возвращает массив узловых маркеров
            PointF[] ps = GetPoints();
            RectangleF[] rects = new RectangleF[ps.Length];
            for (int i = 0; i < ps.Length; i++)
            {
                PointF pt = ps[i];
                rects[i] = RectangleF.FromLTRB(pt.X - 3, pt.Y - 3, pt.X + 3, pt.Y + 3);
            }
            return (rects);
        }
        protected virtual void DrawCustomFigure(Graphics g, Pen p, PointF[] ps)
        {
            g.DrawPolygon(p, ps); // Рисование контура
        }
        public override void DrawFocusFigure(Graphics g, Pen p, PointF pnt, int markerIndex)
        {
            if (pnt.IsEmpty)
            {
                if (NodeChanging)
                { // маркеры узлов рисуем круглыми
                    using (GraphicsPath gp = new GraphicsPath())
                    {
                        RectangleF[] rects = this.GetNodeMarkers();
                        for (int i = 0; i < rects.Length; i++) gp.AddEllipse(rects[i]);
                        g.FillPath(Brushes.White, gp);
                        using (Pen pen = new Pen(Color.Black))
                        {
                            pen.Width = 0;
                            g.DrawPath(pen, gp);
                        }
                    }
                }
                else
                { // маркеры границ рисуем прямоугольными
                    using (GraphicsPath gp = new GraphicsPath())
                    {
                        RectangleF rect = GetBounds;
                        RectangleF[] rects = base.GetBoundMarkers(rect);
                        for (int i = 0; i < rects.Length; i++) if (rects[i].Width > 2) gp.AddRectangle(rects[i]);
                        g.FillPath(Brushes.White, gp);
                        using (Pen pen = new Pen(Color.Black))
                        {
                            pen.Width = 0;
                            g.DrawPath(pen, gp);
                        }
                    }
                }
            }
            else
            {
                if (markerIndex == 0)
                { // тянут мышкой за тело фигуры
                    using (GraphicsPath gp = new GraphicsPath())
                    {
                        GetLinePath(gp, pnt);
                        PointF[] ps = gp.PathPoints;
                        for (int i = 0; i < ps.Length; i++)
                        {
                            ps[i].X += pnt.X;
                            ps[i].Y += pnt.Y;
                        }
                        DrawCustomFigure(g, p, ps); // Рисование контура
                    }
                }
                else
                    if (NodeChanging && (markerIndex < 0))
                    { // тянут мышкой за маркер, изменяющий положение узла
                        using (GraphicsPath gp = new GraphicsPath())
                        {
                            GetLinePath(gp, new PointF());
                            PointF[] ps = gp.PathPoints;
                            int i = Math.Abs(markerIndex) - 1;
                            if ((i >= 0) && (i < ps.Length))
                            {
                                ps[i].X += pnt.X;
                                ps[i].Y += pnt.Y;
                                DrawCustomFigure(g, p, ps); // Рисование контура
                            }
                        }
                    }
                    else if (!NodeChanging && (markerIndex > 0))
                    {
                        PointF[] pts = GetPoints();
                        RectangleF oldrect = CalcFocusRect(new PointF());
                        RectangleF newrect = CalcFocusRect(pnt);
                        for (int i = 0; i < pts.Length; i++)
                        {
                            pts[i].X = newrect.Left + (pts[i].X - oldrect.Left) / oldrect.Width * newrect.Width;
                            pts[i].Y = newrect.Top + (pts[i].Y - oldrect.Top) / oldrect.Height * newrect.Height;
                        }
                        DrawCustomFigure(g, p, pts); // Рисование контура
                    }
            }
        }
        public override void AddPointsRange(PointF[] pts)
        {
            points.Clear();
            points.AddRange(pts);
        }
        public override void FlipVertical()
        {
            RectangleF rect = GetBounds;
            Single cx = rect.X + rect.Width * 0.5F;
            PointF[] pts = GetPoints();
            for (int i = 0; i < pts.Length; i++)
            {
                if (pts[i].X < cx) pts[i].X += (cx - pts[i].X) * 2F;
                else if (pts[i].X > cx) pts[i].X -= (pts[i].X - cx) * 2F;
            }
            AddPointsRange(pts);
        }
        public override void FlipHorizontal()
        {
            RectangleF rect = GetBounds;
            Single cy = rect.Y + rect.Height * 0.5F;
            PointF[] pts = GetPoints();
            for (int i = 0; i < pts.Length; i++)
            {
                if (pts[i].Y < cy) pts[i].Y += (cy - pts[i].Y) * 2F;
                else if (pts[i].Y > cy) pts[i].Y -= (pts[i].Y - cy) * 2F;
            }
            AddPointsRange(pts);
        }

        public override void Rotate(Single angle)
        {
            RectangleF rect = GetBounds;
            Single cx = rect.X + rect.Width * 0.5F;
            Single cy = rect.Y + rect.Height * 0.5F;
            RotateAt(angle, cx, cy);
        }

        public override void RotateAt(Single angle, Single cx, Single cy)
        {
            using (GraphicsPath gp = new GraphicsPath())
            {
                AddGraphicsContent(gp, GetBounds);
                using (Matrix m = new Matrix())
                {
                    m.RotateAt(angle, new PointF(cx, cy));
                    gp.Transform(m);
                }
                PointF[] ps = gp.PathPoints;
                AddPointsRange(ps);
            }
        }

        private void GetLinePath(GraphicsPath gp, PointF offset)
        {
            AddGraphicsContent(gp, GetBounds);
        }

        protected PointF[] onebuff(PointF p1, PointF p2)
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

        public override Boolean PointInFigure(PointF p)
        {
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddPolygon(GetPoints());
                return (gp.IsVisible(p));
            }
        }

        public override void Offset(PointF p) 
        {
            PointF[] pts = GetPoints();
            for (int i = 0; i < pts.Length; i++)
            {
                pts[i].X += p.X;
                pts[i].Y += p.Y;
            }
            AddPointsRange(pts);
        }
        public override void MoveUp(Single step) { this.Offset(new PointF(0, -step)); }
        public override void MoveDown(Single step) { this.Offset(new PointF(0, step)); }
        public override void MoveLeft(Single step) { this.Offset(new PointF(-step, 0)); }
        public override void MoveRight(Single step) { this.Offset(new PointF(step, 0)); }

        public override RectangleF GetBounds
        {
            get
            {
                using (GraphicsPath gp = new GraphicsPath())
                {
                    gp.AddPolygon(GetPoints());
                    RectangleF rect = gp.GetBounds();
                    if (rect.Width == 0) { rect.X -= 2; rect.Width += 4; }
                    if (rect.Height == 0) { rect.Y -= 2; rect.Height += 4; }
                    return (rect);
                }
            }
        }

        public override void UpdateSize(PointF offset)
        {
            PointF[] pts;
            if (!this.NodeChanging && (this.MarkerIndex > 0))
            { // перемещение границ
                pts = GetPoints();
                RectangleF oldrect = CalcFocusRect(new PointF());
                RectangleF newrect = CalcFocusRect(offset);
                for (int i = 0; i < pts.Length; i++)
                {
                    pts[i].X = newrect.Left + (pts[i].X - oldrect.Left) / oldrect.Width * newrect.Width;
                    pts[i].Y = newrect.Top + (pts[i].Y - oldrect.Top) / oldrect.Height * newrect.Height;
                }
                AddPointsRange(pts);
            }
            else
                if (this.NodeChanging && (this.MarkerIndex < 0))
                { // перемещение узлов
                    pts = GetPoints();
                    int index = Math.Abs(this.MarkerIndex) - 1;
                    if ((index >= 0) && (index < pts.Length))
                    {
                        pts[index].X += offset.X;
                        pts[index].Y += offset.Y;
                        AddPointsRange(pts);
                    }
                }
        }

        protected virtual void RemovePointByIndex(int markerIndex)
        {
            points.RemoveAt(markerIndex);
        }

        public override void DeleteFigureNode(int markerIndex)
        {
            if (NodeChanging)
            {
                PointF[] ps = GetPoints();
                if ((ps.Length > 2) && (markerIndex < ps.Length - 1))
                {
                    RemovePointByIndex(Math.Abs(markerIndex) - 1);
                }
            }
        }

        private bool ptInRange(PointF p, PointF p1, PointF p2)
        {
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddPolygon(onebuff(p1, p2));
                using (Region rg = new Region(gp))
                    return (rg.IsVisible(p));
            }
        }
        protected virtual void InsertPointBeforeIndex(int markerIndex, PointF pnt)
        {
            points.Insert(markerIndex, pnt);
        }
        public override void InsertFigureNode(PointF pnt)
        {
            if (NodeChanging)
            {
                PointF[] ps = GetPoints();
                PointF[] pts = new PointF[ps.Length + 1];
                ps.CopyTo(pts, 0);
                pts[pts.Length - 1].X = pts[0].X;
                pts[pts.Length - 1].Y = pts[0].Y;
                for (int i = 1; i < pts.Length; i++)
                {
                    if (ptInRange(pnt, pts[i - 1], pts[i]))
                    {
                        InsertPointBeforeIndex(i, new PointF(pnt.X, pnt.Y));
                        break;
                    }
                }
            }
        }
    }
}
