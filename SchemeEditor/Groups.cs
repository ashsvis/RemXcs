using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SchemeEditor
{
    [Serializable]
    public class Groups: Polygones
    {
        private List<Draws> list = new List<Draws>();
        public Groups() : this(new Stroke(), new Fill(), new Text(), new RectangleF()) { }
        public Groups(Stroke stroke, Fill fill, Text text, RectangleF rect) : 
            base(stroke, fill, text, rect) { }
        public void AddFigure(Draws drw)
        {
            list.Add(drw);
        }

        public override void Initialize()
        {
            base.Initialize();
            foreach (Draws drw in list)
            {
                if (drw.Stroke == null) drw.Stroke = new Stroke();
                if (drw.Fill == null) drw.Fill = new Fill();
                if (drw.Text == null) drw.Text = new Text();
            }
        }

        public List<Draws> Items
        {
            get { return list; }
        }

        public ArrayList ExtractFigures()
        {
            ArrayList figs = new ArrayList();
            foreach (Draws drw in list) figs.Add(drw);
            return (figs);
        }
 
        public override void DrawFigure(Graphics g)
        {
            foreach (Draws drw in list) drw.DrawFigure(g);
        }

        public override void AddGraphicsContent(GraphicsPath gp, RectangleF rect)
        {   // определяет содержимое графичеcкой части
            foreach (Draws drw in list)
            {
                drw.AddGraphicsContent(gp, drw.GetBounds);
            }
        }

        public override void DrawFocusFigure(Graphics g, Pen p, PointF pnt, int markerIndex)
        {
            if (pnt.IsEmpty) base.DrawFocusFigure(g, p, pnt, markerIndex);
            else
            {   // тянут мышкой за тело фигуры или тянут мышкой за маркер
                using (GraphicsPath gp = new GraphicsPath())
                {
                    gp.AddRectangle(CalcFocusRect(pnt));
                    if (g is Graphics) g.DrawPath(p, gp);
                }
            }
        }

        public override Boolean PointInFigure(PointF p)
        {
            bool found = false;
            foreach (Draws drw in list)
            {
                if (drw.PointInFigure(p)) {found = true; break; }
            }
            return (found);
        }

        public override void UpdateSize(PointF offset)
        {
            if (!this.NodeChanging)
            { // перемещение границ
                RectangleF oldrect = CalcFocusRect(new PointF());
                RectangleF newrect = CalcFocusRect(offset);
                PointF[] pts;
                foreach (Draws drw in list)
                {
                    pts = drw.GetPoints();
                    for (int i = 0; i < pts.Length; i++)
                    {
                        pts[i].X = newrect.Left + (pts[i].X - oldrect.Left) / oldrect.Width * newrect.Width;
                        pts[i].Y = newrect.Top + (pts[i].Y - oldrect.Top) / oldrect.Height * newrect.Height;
                    }
                    drw.AddPointsRange(pts);
                }
            }
        }

        public override void FlipVertical()
        {
            RectangleF rect = GetBounds;
            Single cx = rect.X + rect.Width * 0.5F;
            PointF[] pts;
            foreach (Draws drw in list)
            {
                pts = drw.GetPoints();
                for (int i = 0; i < pts.Length; i++)
                {
                    if (pts[i].X < cx) pts[i].X += (cx - pts[i].X) * 2F;
                    else if (pts[i].X > cx) pts[i].X -= (pts[i].X - cx) * 2F;
                }
                drw.AddPointsRange(pts);
            }
        }

        public override void FlipHorizontal()
        {
            RectangleF rect = GetBounds;
            Single cy = rect.Y + rect.Height * 0.5F;
            PointF[] pts;
            foreach (Draws drw in list)
            {
                pts = drw.GetPoints();
                for (int i = 0; i < pts.Length; i++)
                {
                    if (pts[i].Y < cy) pts[i].Y += (cy - pts[i].Y) * 2F;
                    else if (pts[i].Y > cy) pts[i].Y -= (pts[i].Y - cy) * 2F;
                }
                drw.AddPointsRange(pts);
            }
        }

        public override void RotateAt(Single angle, Single cx, Single cy)
        {
            GraphicsPath gp;
            foreach (Draws drw in list)
            {
                using (gp = new GraphicsPath())
                {
                    drw.AddGraphicsContent(gp, GetBounds);
                    using (Matrix m = new Matrix())
                    {
                        m.RotateAt(angle, new PointF(cx, cy));
                        gp.Transform(m);
                        PointF[] ps = gp.PathPoints;
                        drw.AddPointsRange(ps);
                    }
                }
            }
        }

        public override void Offset(PointF p) 
        { 
            foreach (Draws drw in list) drw.Offset(p);
        }

        public override void MoveUp(Single step) { Offset(new PointF(0, -step)); }
        public override void MoveDown(Single step) { Offset(new PointF(0, step)); }
        public override void MoveLeft(Single step) { Offset(new PointF(-step, 0)); }
        public override void MoveRight(Single step) { Offset(new PointF(step, 0)); }

        public override RectangleF GetBounds
        {
            get
            {
                using (GraphicsPath gp = new GraphicsPath())
                {
                    AddGraphicsContent(gp, new RectangleF());
                    RectangleF rect = gp.GetBounds();
                    if (rect.Width == 0) { rect.X -= 2; rect.Width += 4; }
                    if (rect.Height == 0) { rect.Y -= 2; rect.Height += 4; }
                    return (rect);
                }
            }
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
