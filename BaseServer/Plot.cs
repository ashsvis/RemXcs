using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BaseServer
{
    [Serializable]
    public class Plot
    {
        public PointF Location
        {
            get
            {
                return new PointF((Single)Props["Left"], (Single)Props["Top"]);
            }
            set
            {
                Props["Left"] = value.X;
                Props["Top"] = value.Y;
            }
        }

        public SizeF Size
        {
            get
            {
                return new SizeF((Single)Props["Width"], (Single)Props["Height"]);
            }
            set
            {
                Props["Width"] = value.Width;
                Props["Height"] = value.Height;
            }
        }

        public string Name
        {
            get
            {
                return (string)Props["Name"];
            }
            set
            {
                Props["Name"] = value;
            }
        }

        public int Kind
        {
            get
            {
                return (int)Props["PlotKind"];
            }
        }

        public IDictionary<string, object> Props;

        public Plot(int plotkind)
        {
            this.Props = getDefaultValues();
            this.Location = new PointF(0f, 0f);
            this.Size = new SizeF(100f, 20f);
            Props["Name"] = GetUniqueKey(10);
            Props["PlotKind"] = plotkind;
            switch (plotkind)
            {
                case 1: Props["Text"] = "Текст"; break;
                case 2: Props["Text"] = "Текущее"; break;
                case 3: Props["Text"] = "Часовое"; break;
                case 4: Props["Text"] = "Суточное"; Props["Snap"] = 1; break;
                case 5: Props["Text"] = "Месячное"; Props["Snap"] = 1; break;
            }
        }

        private static string GetUniqueKey(int length)
        {
            string guidResult = string.Empty;
            while (guidResult.Length < length)
            {
                // Get the GUID. 
                guidResult += Guid.NewGuid().ToString().GetHashCode().ToString("x");
            }
            // Make sure length is valid. 
            if (length <= 0 || length > guidResult.Length)
                throw new ArgumentException("Length must be between 1 and " + guidResult.Length);
            // Return the first length bytes. 
            return guidResult.Substring(0, length);
        }

        public void GenerateNewName()
        {
            Props["Name"] = GetUniqueKey(10);
        }

        private IDictionary<string, object> getDefaultValues()
        {
            IDictionary<string, object> props = new Dictionary<string, object>();
            props.Add("Name", String.Empty); // имя элемента
            props.Add("PlotKind", (int)0); // тип элемента
            props.Add("Left", 0f); // x - координата
            props.Add("Top", 0f); // y - координата
            props.Add("Width", 0f); // ширина
            props.Add("Height", 0f); // высота
            props.Add("FontName", "Tahoma"); // имя шрифта
            props.Add("FontColor", Color.Black); // цвет текста
            props.Add("FontSize", 10f); // размер шрифта
            props.Add("FontBold", false); // жирный
            props.Add("FontItalic", false); // наклонный
            props.Add("FontUnderline", false); // подчёркнутый
            props.Add("FontStrikeOut", false); // перечёркнутый
            props.Add("Alignment", (int)1); // выравнивание 0-влево 1-по центру 2-вправо
            props.Add("Framed", false); // обрамление
            props.Add("Text", String.Empty); // текст
            props.Add("PtName", String.Empty); // полное имя параметра
            props.Add("Snap", (int)0); // час, день или месяц
            props.Add("Offset", (int)0); // смещение
            props.Add("ColumnLines", (int)1); // строк в столбце
            props.Add("LineBetween", (int)0); // расстояние между строками
            props.Add("Agregate", (int)0); // встроенная функция 0-нет 1-сумма 2-минимум 3-максимум 4-среднее
            return props;
        }

        public RectangleF BoundsRect
        {
            get { return new RectangleF(Location, Size); }
        }

        public void SetPropValue(string key, object value)
        {
            if (this.Props.ContainsKey(key))
                this.Props[key] = value;
            else
                this.Props.Add(key, value);
        }

        public void DrawFigure(Graphics g, bool editmode = false)
        {
            if (editmode)
            {
                RectangleF r = BoundsRect;
                drawEdges(g, r);
                string kind;
                switch (Kind)
                {
                    case 2: kind = "Текущее"; break;
                    case 3: kind = "Часовое"; break;
                    case 4: kind = "Суточное"; break;
                    case 5: kind = "Месячное"; break;
                    default: kind = String.Empty; break;
                }
                g.DrawString(kind, SystemFonts.DefaultFont, Brushes.LightGray, new PointF(r.X, r.Y));
            }
            int alignment = (int)Props["Alignment"];
            FontStyle style = FontStyle.Regular;
            if ((bool)Props["FontBold"]) style |= FontStyle.Bold;
            if ((bool)Props["FontItalic"]) style |= FontStyle.Italic;
            if ((bool)Props["FontUnderline"]) style |= FontStyle.Underline;
            if ((bool)Props["FontStrikeOut"]) style |= FontStyle.Strikeout;
            try
            {
                using (Font font = new Font((string)Props["FontName"],
                                            (float)Props["FontSize"], style))
                {
                    Color fontcolor = (Color)Props["FontColor"];
                    using (Brush fontbrush = new SolidBrush(fontcolor))
                    {
                        using (StringFormat sf = new StringFormat())
                        {
                            sf.LineAlignment = StringAlignment.Center;
                            switch (alignment)
                            {
                                case 0: sf.Alignment = StringAlignment.Near; break;
                                case 1: sf.Alignment = StringAlignment.Center; break;
                                case 2: sf.Alignment = StringAlignment.Far; break;
                            }
                            string text = (string)Props["Text"];
                            if (Kind == 1 && text.Length > 0 && text.IndexOf("[{") >= 0 &&
                                Props.ContainsKey("TextExt")) text = (string)Props["TextExt"];
                            RectangleF rect = BoundsRect;
                            g.DrawString(text, font, fontbrush, rect, sf);
                            int agregate = (int)Props["Agregate"];
                            if (agregate == 0)
                            {
                                int count = (int)Props["ColumnLines"];
                                if (count > 1)
                                {
                                    int ah = (int)Props["LineBetween"];
                                    for (int i = 1; i < count; i++)
                                    {
                                        string nexttext = "Text" + i;
                                        if (Props.ContainsKey(nexttext))
                                        {
                                            text = (string)Props[nexttext];
                                            rect.Offset(new PointF(0, rect.Height + ah));
                                            if (editmode) drawEdges(g, rect);
                                            g.DrawString(text, font, fontbrush, rect, sf);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                using (Font font = new Font("Tahoma", (float)Props["FontSize"], style))
                {
                    using (Brush fontbrush = new SolidBrush((Color)Props["FontColor"]))
                        g.DrawString("Шрифт?", font, fontbrush, BoundsRect);
                }
            }
        }

        private static void drawEdges(Graphics g, RectangleF r)
        {
            g.DrawLines(Pens.LightGray, new PointF[]
                    { new PointF(r.X, r.Y + 3), new PointF(r.X, r.Y),
                      new PointF(r.X + 3, r.Y) });
            g.DrawLines(Pens.LightGray, new PointF[]
                    { new PointF(r.X + r.Width - 3, r.Y),
                      new PointF(r.X + r.Width, r.Y),
                      new PointF(r.X + r.Width, r.Y + 3) });
            g.DrawLines(Pens.LightGray, new PointF[]
                    { new PointF(r.X + r.Width, r.Y + r.Height - 3),
                      new PointF(r.X + r.Width, r.Y + r.Height),
                      new PointF(r.X + r.Width - 3, r.Y + r.Height) });
            g.DrawLines(Pens.LightGray, new PointF[]
                    { new PointF(r.X, r.Y + r.Height - 3),
                      new PointF(r.X, r.Y + r.Height),
                      new PointF(r.X + 3, r.Y + r.Height) });
        }
    }
}
