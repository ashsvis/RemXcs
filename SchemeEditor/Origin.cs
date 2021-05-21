using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SchemeEditor
{
    public class Origin
    {
        public Origin(int x, int y)
        {
            this.X = x;
            X = (Single)Math.Round(X / 8) * 8;
            this.Y = y;
            Y = (Single)Math.Round(Y / 8) * 8;
        }
        public Single X { get; set; }
        public Single Y { get; set; }
        public void Draw(Graphics g, Color color)
        {
            Single r = 10;
            using (Pen p = new Pen(color))
            {
                p.DashStyle = DashStyle.Dash;
                RectangleF rect = new RectangleF(X - r, Y - r, r * 2, r * 2);
                rect.Offset(-X, -Y);
                g.DrawEllipse(p, rect);
                rect.Inflate(r, r);
                g.DrawEllipse(p, rect);
                rect.Inflate(r, r);
                Single cx = rect.X + rect.Width / 2;
                Single cy = rect.Y + rect.Height / 2;
                g.DrawLine(p, new PointF(cx, rect.Top), new PointF(cx, rect.Bottom));
                g.DrawLine(p, new PointF(rect.Left, cy), new PointF(rect.Right, cy));
            }
        }
        public void MoveTo(Single x, Single y)
        {
            this.X = x;
            X = (Single)Math.Round(X / 8) * 8;
            this.Y = y;
            Y = (Single)Math.Round(Y / 8) * 8;
        }
        public void MoveTo(SizeF size)
        {
            this.X = size.Width / 2;
            X = (Single)Math.Round(X / 8) * 8;
            this.Y = size.Height / 2;
            Y = (Single)Math.Round(Y / 8) * 8;
        }
    }
}
