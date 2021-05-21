using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.CodeDom.Compiler;

namespace SchemeEditor
{
    public enum FillMode { None, Solid, Hatch, LinearGradient };

    [Serializable]
    public class Fill
    {
        private int patternIndex = 1;
        public Fill() : this(Color.White) { }
        public Fill(Color color)
        {
            this.Color = color;
            this.Alpha = 255;
            this.PatternColor = Color.Black;
            this.PatternIndex = 1; // FillMode.Solid;
            this.Mode = FillMode.Solid;
            this.HatchMode = HatchStyle.Percent50;
            this.LinearMode = LinearGradientMode.Horizontal;
        }
        public Color Color { get; set; }
        public int Alpha { get; set; }
        public Color PatternColor { get; set; }
        public FillMode Mode { get; set; }
        public HatchStyle HatchMode { get; set; }
        public LinearGradientMode LinearMode { get; set; }
        public int PatternIndex
        {
            get
            {
                return (patternIndex);
            }
            set
            {
                patternIndex = value;
                this.Mode = DrawUtils.FillModeFromIndex(patternIndex);
                this.HatchMode = DrawUtils.HatchStyleFromIndex(patternIndex);
                this.LinearMode = DrawUtils.LinearGradientModeFromIndex(patternIndex);
            }
        }
        public Brush Brush(RectangleF rect)
        {
            Brush brush = null;
            switch (Mode)
            {
                case FillMode.None: brush = new SolidBrush(Color.Transparent); break;
                case FillMode.Solid: brush = new SolidBrush(Color.FromArgb(Alpha, Color)); break;
                case FillMode.Hatch: brush = new HatchBrush(HatchMode,
                                                            Color.FromArgb(Alpha, PatternColor),
                                                            Color.FromArgb(Alpha, Color)); break;
                case FillMode.LinearGradient:
                    brush = new LinearGradientBrush(rect, Color.FromArgb(Alpha, PatternColor),
                                                Color.FromArgb(Alpha, Color), LinearMode); break;
            }
            return (brush);
        }
        public void Assign(Fill fill)
        {
            if (fill != null)
            {
                this.Color = fill.Color;
                this.Alpha = fill.Alpha;
                this.PatternColor = fill.PatternColor;
                this.PatternIndex = fill.PatternIndex;
                this.Mode = fill.Mode;
                this.HatchMode = fill.HatchMode;
                this.LinearMode = fill.LinearMode;
            }
        }
    }
}
