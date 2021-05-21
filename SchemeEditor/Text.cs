using System;
using System.Drawing;

namespace SchemeEditor
{
    [Serializable]
    public class Text
    {
        public Text()
        {
            this.Color = Color.Black;
            this.Alpha = 255;
            this.FontName = "Arial";
            this.Bold = false;
            this.Italic = false;
            this.Underline = false;
            this.Strikeout = false;
            this.FontSize = 10;
            this.Caption = "";
            this.LineAlignment = StringAlignment.Center;
            this.Alignment = StringAlignment.Center;
            this.Vertical = false;
        }
        public Color Color { get; set; }
        public int Alpha { get; set; }
        public string FontName { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public bool Underline { get; set; }
        public bool Strikeout { get; set; }
        public Single FontSize { get; set; }
        public string Caption { get; set; }
        public StringAlignment LineAlignment { get; set; }
        public StringAlignment Alignment { get; set; }
        public bool Vertical { get; set; }

        public Brush FontBrush()
        {
            return new SolidBrush(Color.FromArgb(this.Alpha, this.Color));
        }

        public Font Font()
        {
            FontStyle fontstyle = FontStyle.Regular;
            if (this.Bold) fontstyle |= FontStyle.Bold;
            if (this.Italic) fontstyle |= FontStyle.Italic;
            if (this.Underline) fontstyle |= FontStyle.Underline;
            if (this.Strikeout) fontstyle |= FontStyle.Strikeout;
            return new Font(this.FontName, this.FontSize, fontstyle);
        }

        public void Assign(Text text)
        {
            this.Color = text.Color;
            this.Alpha = text.Alpha;
            this.FontName = text.FontName;
            this.Bold = text.Bold;
            this.Italic = text.Italic;
            this.Underline = text.Underline;
            this.Strikeout = text.Strikeout;
            this.FontSize = text.FontSize;
            this.Caption = text.Caption;
            this.LineAlignment = text.LineAlignment;
            this.Alignment = text.Alignment;
            this.Vertical = text.Vertical;
        }
    }
}
