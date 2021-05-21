using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using BaseServer;

namespace RemXcs
{
    public struct Trend
    {
        public string TrendName;
        public Single LowRange;
        public Single HighRange;
        public int Format;
        public bool Visible;
        public bool Selected;
        public Color Color;
        public SortedList<DateTime, double> Values;
        public Double kY;
        public Double bY;
    }

    public class Trends
    {
        private Single bHeight = 30;
        private Single rWidth = 70;
        private Single lWidth = 50;
        Single kY = 1;
        Single bY = 0;
        Single pkY = 1;
        Single pbY = 0;
        private RectangleF trendrect;
        private Color[] trendcolors = new Color[10]
        {   Color.FromArgb(0, 0, 0), // black
            Color.FromArgb(0, 0, 255), // blue
            Color.FromArgb(255, 0, 0), // red
            Color.FromArgb(0, 128, 0), // green
            Color.FromArgb(0, 128, 128), // cyan
            Color.FromArgb(128, 0, 128), // magenta
            Color.FromArgb(64, 0, 128), // violet
            Color.FromArgb(255, 128, 0), // yellow
            Color.FromArgb(128, 128, 128), // gray
            Color.FromArgb(128, 128, 0)}; // olive
        Single lowrange;
        Single highrange;
        public Single LowRange
        {
            get
            {
                return lowrange * this.kY + this.bY;
            }
            set
            {
                lowrange = value;
            }
        }
        public Single HighRange
        {
            get
            {
                return highrange * this.kY + this.bY;
            }
            set
            {
                highrange = value;
            }
        }
        Single plowrange = 0F;
        Single phighrange = 100F;
        public Single pLowRange
        {
            get
            {
                return plowrange * this.pkY + this.pbY;
            }
            set
            {
                plowrange = value;
            }
        }
        public Single pHighRange
        {
            get
            {
                return phighrange * this.pkY + this.pbY;
            }
            set
            {
                phighrange = value;
            }
        }

        public Trend[] Items;

        public Trends()
        {
            Items = new Trend[10];
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i].Values == null)
                    Items[i].Values = new SortedList<DateTime, double>();
                Items[i].Color = trendcolors[i];
                Items[i].kY = 1.0;
                Items[i].bY = 0.0;
            }
        }

        public void ResetColors()
        {
            for (int i = 0; i < Items.Length; i++)
                Items[i].Color = trendcolors[i];
        }

        public Color ResetTrendColor(int index)
        {
            if (index >= 0 && index < Items.Length)
            {
                Color color = trendcolors[index];
                Items[index].Color = color;
                return color;
            }
            else
                return Color.Black;

        }

        public Color GetDefaultTrendColor(int index)
        {
            if (index >= 0 && index < Items.Length)
                return trendcolors[index];
            else
                return Color.Black;
        }

        public Color GetTrendColor(int index)
        {
            if (index >= 0 && index < Items.Length)
                return Items[index].Color;
            else
                return Color.Black;
        }

        public void SetTrendColor(int index, Color color)
        {
            if (index >= 0 && index < Items.Length)
            {
                Items[index].Color = color;
            }
        }

        public Rectangle TrendRect()
        {
            return Rectangle.Ceiling(trendrect);
        }

        public void DrawTrendInit(Graphics g, RectangleF rect, float kY, float bY,
            float pkY, float pbY)
        {
            this.kY = kY;
            this.bY = bY;
            this.pkY = pkY;
            this.pbY = pbY;
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(240, 240, 240)))
                g.FillRectangle(brush, rect);
            trendrect = new RectangleF(lWidth + rect.X, 10 + rect.Y, rect.Width - lWidth - rWidth,
                rect.Height - 10 - bHeight);
            g.FillRectangle(SystemBrushes.Window, trendrect);
            g.DrawRectangle(SystemPens.WindowText, trendrect.X, trendrect.Y,
                                trendrect.Width, trendrect.Height);
        }

        public void DrawBottomAxis(Graphics g, DateTime offset, Double minutes)
        {
            Single w = 120;
            Single k = (Single)Math.Ceiling((trendrect.Width) / w);
            if (k >= 36) k = 36;
            else if (k >= 24) k = 24;
            else if (k >= 12) k = 12;
            else if (k >= 6) k = 6;
            else if (k >= 4) k = 4;
            else if (k >= 2) k = 2;
            if (k > 0)
            {
                w = (trendrect.Width) / k;
                // k - количество интервалов, w - ширина одного интервала в пикселах
                int min = offset.Minute;
                int mm = 0;
                int sec = offset.Second;
                if (minutes == 20)
                {
                    if (min > 40) { min -= 40; mm = 40; }
                    else if (min > 20) { min -= 20; mm = 20; }
                }

                RectangleF bottomrect = trendrect;
                bottomrect.Height += bHeight;
                bottomrect.X -= lWidth;
                bottomrect.Width += lWidth + rWidth;
                g.Clip = new Region(bottomrect);

                Double ps = (min + sec / 60F) / minutes;
                PointF pt = new PointF(trendrect.Right - (Single)(ps * trendrect.Width), trendrect.Bottom);
                DateTime HighRange = new DateTime(offset.Year, offset.Month, offset.Day,
                    offset.Hour, mm, sec);
                DateTime LowRange = HighRange.AddMinutes(-minutes);
                TimeSpan HiLoRange = HighRange.Subtract(LowRange);
                DateTime n = HighRange;
                // построение назад
                drawSmallBottomRisc(g, new PointF(pt.X + w, pt.Y), w, trendrect);
                do
                {
                    drawBottomRisc(g, n, pt, trendrect);
                    drawSmallBottomRisc(g, pt, w, trendrect);
                    pt.X -= w;
                    n = n.AddMinutes(-minutes / k);
                } while (pt.X > trendrect.Left);
                drawBottomRisc(g, n, pt, trendrect);
                drawSmallBottomRisc(g, pt, w, trendrect);
                // построение вперёд
                pt = new PointF(trendrect.Right - (Single)(ps * trendrect.Width), trendrect.Bottom);
                n = HighRange;
                drawSmallBottomRisc(g, new PointF(pt.X + w, pt.Y), w, trendrect);
                do
                {
                    drawBottomRisc(g, n, pt, trendrect);
                    drawSmallBottomRisc(g, pt, w, trendrect);
                    pt.X += w;
                    n = n.AddMinutes(minutes / k);
                } while (pt.X <= trendrect.Right);
                drawBottomRisc(g, n, pt, trendrect);
                drawSmallBottomRisc(g, pt, w, trendrect);
                //
                using (Pen pen = new Pen(SystemColors.WindowText, 2))
                    g.DrawLine(pen, new PointF(trendrect.Left, trendrect.Bottom + 1),
                        new PointF(trendrect.Right, trendrect.Bottom + 1));
                g.ResetClip();
            }
        }

        private void drawSmallBottomRisc(Graphics g, PointF pt, Single w, RectangleF rect)
        {
            int k = 4;
            for (int i = 0; i < k; i++)
            {
                if ((pt.X >= rect.Left) && (pt.X <= rect.Right))
                    g.DrawLine(SystemPens.WindowText, pt, new PointF(pt.X, pt.Y + 3));
                pt.X -= w / k;
            }
        }

        private void drawBottomRisc(Graphics g, DateTime value, PointF pt1, RectangleF rect)
        {
            PointF pt2 = new PointF(pt1.X, pt1.Y + 6);
            if ((pt2.X >= rect.Left) && (pt2.X <= rect.Right))
                g.DrawLine(SystemPens.WindowText, pt1, pt2);
            PointF pt3 = new PointF(pt2.X - 42, pt2.Y + 3);
            string sval = value.ToString("dd.MM ddd HH:mm");
            if (pt3.X > 0 &&
                pt3.X <= (rect.Right - g.MeasureString(sval, SystemFonts.CaptionFont).Width / 2))
                g.DrawString(sval, SystemFonts.CaptionFont, 
                    SystemBrushes.WindowText, pt3);
            if ((pt1.X >= rect.Left) && (pt1.X <= rect.Right))
            {
                PointF pt4 = new PointF(pt1.X, pt1.Y - rect.Height);
                using (Pen p = new Pen(SystemColors.ButtonShadow))
                {
                    p.DashStyle = DashStyle.Dash;
                    g.DrawLine(p, pt1, pt4);
                }
            }
        }

        public void DrawLeftAxis(Graphics g)
        {
            Single h = 30;
            Single k = (Single)Math.Ceiling((trendrect.Height) / h);
            if (k >= 20) k = 20;
            else if (k >= 10) k = 10;
            else if (k >= 5) k = 5;
            else if (k >= 4) k = 4;
            else if (k >= 2) k = 2;
            if (k > 0) h = (trendrect.Height) / k;

            RectangleF leftrect = trendrect;
            leftrect.Width = lWidth;
            leftrect.X -= lWidth;
            leftrect.Y -= 10;
            leftrect.Height += 20;
            g.Clip = new Region(leftrect);

            PointF pt = new PointF(trendrect.Left, trendrect.Bottom);
            Single HiLoRange = pHighRange - pLowRange;
            Single n = pLowRange;
            do
            {
                drawLeftRisc(g, n, pt, trendrect);
                drawSmallLeftRisc(g, pt, h, trendrect);
                pt.Y -= h;
                n += HiLoRange / k;
            } while (pt.Y > trendrect.Top);
            drawLeftRisc(g, n, pt, trendrect);
            g.ResetClip();

        }

        private void drawSmallLeftRisc(Graphics g, PointF pt, Single h, RectangleF rect)
        {
            int k = 10;
            if (h < 36) k = 5;
            for (int i = 0; i < k; i++)
            {
                int w = (i == 5) ? 6 : 3;
                g.DrawLine(SystemPens.WindowText, pt, new PointF(pt.X - w, pt.Y));
                pt.Y -= h / k;
                if ((pt.Y < rect.Top) || (pt.Y > rect.Bottom)) break;
            }
        }

        private void drawLeftRisc(Graphics g, Single value, PointF pt1, RectangleF rect)
        {
            PointF pt2 = new PointF(pt1.X - 6, pt1.Y);
            PointF pt3 = new PointF(rect.Left - lWidth, pt2.Y - 8);
            g.DrawLine(SystemPens.WindowText, pt1, pt2);
            string text = value.ToString("0 \\%");
            using (StringFormat stringFormat = new StringFormat())
            {
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Near;
                RectangleF r = new RectangleF(pt3, new SizeF(lWidth - 8, SystemFonts.CaptionFont.GetHeight()));
                g.DrawString(text, SystemFonts.CaptionFont, SystemBrushes.WindowText, r, stringFormat);
            }
        }

        public void DrawRightAxis(Graphics g)
        {
            Single h = 30;
            Single k = (Single)Math.Ceiling((trendrect.Height) / h);
            if (k >= 20) k = 20;
            else if (k >= 10) k = 10;
            else if (k >= 5) k = 5;
            else if (k >= 4) k = 4;
            else if (k >= 2) k = 2;
            if (k > 0) h = (trendrect.Height) / k;

            RectangleF rightrect = trendrect;
            rightrect.Width = rWidth + trendrect.Width;
            rightrect.Y -= 10;
            rightrect.Height += 20;
            g.Clip = new Region(rightrect);
            
            PointF pt = new PointF(trendrect.Right, trendrect.Bottom);
            Single HiLoRange = HighRange - LowRange;
            Single n = LowRange;
            do
            {
                drawRightRisc(g, n, pt, trendrect);
                drawSmallRightRisc(g, pt, h, trendrect);
                pt.Y -= h;
                n += HiLoRange / k;
            } while (pt.Y > trendrect.Top);
            drawRightRisc(g, n, pt, trendrect);
            using (Pen pen = new Pen(SystemColors.WindowText, 2))
            {
                g.DrawLine(pen, new PointF(trendrect.Left, trendrect.Top),
                    new PointF(trendrect.Left, trendrect.Bottom));
                g.DrawLine(pen, new PointF(trendrect.Right, trendrect.Top),
                    new PointF(trendrect.Right, trendrect.Bottom));
            }
            g.ResetClip();
        }

        private void drawSmallRightRisc(Graphics g, PointF pt, Single h, RectangleF rect)
        {
            int k = 10;
            if (h < 36) k = 5;
            for (int i = 0; i < k; i++)
            {
                int w = (i == 5) ? 4 : 2;
                g.DrawLine(SystemPens.WindowText, pt, new PointF(pt.X + w, pt.Y));
                pt.Y -= h / k;
                if ((pt.Y < rect.Top) || (pt.Y > rect.Bottom)) break;
            }
        }

        private int getFormat()
        {
            foreach (Trend item in Items) if (item.Selected) return item.Format;
            return 0;
        }

        private void drawRightRisc(Graphics g, Single value, PointF pt1, RectangleF rect)
        {
            PointF pt2 = new PointF(pt1.X + 6, pt1.Y);
            PointF pt3 = new PointF(pt2.X + 3, pt2.Y - 8);
            g.DrawLine(SystemPens.WindowText, pt1, pt2);
            string sval = Data.Float(value, getFormat());
            g.DrawString(sval, SystemFonts.CaptionFont, SystemBrushes.WindowText, pt3);
            if ((pt1.Y > rect.Top) && (pt1.Y < rect.Bottom))
            {
                PointF pt4 = new PointF(pt1.X - rect.Width, pt1.Y);
                using (Pen p = new Pen(SystemColors.ButtonShadow))
                {
                    p.DashStyle = DashStyle.Dash;
                    g.DrawLine(p, pt1, pt4);
                }
            }
        }

        public void DrawTrend(Graphics g, Trend item, DateTime DateLowRange, DateTime DateHighRange)
        {
            TimeSpan DateHiLoRange = DateHighRange.Subtract(DateLowRange);
            Single ValueHiLoRange = HighRange - LowRange;
            if (ValueHiLoRange >= 0.0001F)
            {
                List<PointF> points = new List<PointF>();
                using (Pen pen = new Pen(item.Color))
                {
                    pen.Width = item.Selected ? 3 : 1;
                    foreach (KeyValuePair<DateTime, double> kvp in  item.Values)
                    {
                        DateTime dt =  kvp.Key;
                        bool kind = !double.IsNaN(kvp.Value);
                        Double value = (kind) ? kvp.Value : 0;
                        if (kind)
                        {
                            TimeSpan dtoffset = dt.Subtract(DateLowRange);
                            Double kX = dtoffset.TotalSeconds / DateHiLoRange.TotalSeconds;
                            float x = (float)(trendrect.Width * kX + trendrect.X);
                            Double kY = (value - LowRange) / ValueHiLoRange;
                            kY = item.kY * kY + item.bY;
                            //kY = 0.5 * kY + 0.0;
                            float y = (float)(trendrect.Height - trendrect.Height * kY + trendrect.Y);
                            points.Add(new PointF(x, y));
                        }
                    }
                    if (points.Count > 1)
                    {
                        g.Clip = new Region(trendrect);
                        g.DrawLines(pen, points.ToArray());
                        g.ResetClip();
                    }
                }
            }
        }
    }
}
