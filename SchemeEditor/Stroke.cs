using System;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace SchemeEditor
{
    [Serializable]
    public class Stroke
    {
        public Stroke() : this(Color.Black, 1.0F) { }
        public Stroke(Color color, Single width)
        {
            this.Color = color;
            this.Width = width;
            this.DashStyle = DashStyle.Solid;
            this.Alpha = 255;
        }
        public Color Color { get; set; }
        public Single Width { get; set; }
        public DashStyle DashStyle { get; set; }
        public int Alpha { get; set; }
        public LineCap StartCap { get; set; }
        public LineCap EndCap { get; set; }
        public LineJoin LineJoin { get; set; }
        public void Assign(Stroke stroke)
        {
            this.Color = stroke.Color;
            this.Width = stroke.Width;
            this.DashStyle = stroke.DashStyle;
            this.Alpha = stroke.Alpha;
            this.LineJoin = stroke.LineJoin;
            this.StartCap = stroke.StartCap;
            this.EndCap = stroke.EndCap;
        }
        [NonSerialized]
        private static Pen pen = new System.Drawing.Pen(Color.Black);
        public Pen Pen()
        {
//            Pen pen = new System.Drawing.Pen(Color.FromArgb(this.Alpha, this.Color));
            pen.Color = Color.FromArgb(this.Alpha, this.Color);
            pen.Width = this.Width;
            if (this.DashStyle == DashStyle.Custom)
            {
                pen.DashStyle = DashStyle.Solid;
                pen.Color = Color.Transparent;
            }
            else
                pen.DashStyle = this.DashStyle;
            pen.LineJoin = this.LineJoin;
            pen.StartCap = this.StartCap;
            pen.EndCap = this.EndCap;
            return (pen);
        }
    }
}
