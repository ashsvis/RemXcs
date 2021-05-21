using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Security.Cryptography;

namespace Draws.Plugins
{
    public enum FillMode { None, Solid, Hatch, LinearGradient };

    public class NamedColor
    {
        public NamedColor(Color color, string colorName)
        {
            this.Color = color;
            this.Name = colorName;
        }
        public Color Color { get; set; }
        public string Name { get; set; }
    }

    public static class DrawUtils
    {
        static private List<NamedColor> namedColors = new List<NamedColor>() 
        {   new NamedColor(Color.Black, "Чёрный"),
            new NamedColor(Color.White, "Белый"),
            new NamedColor(Color.Red, "Красный"),
            new NamedColor(Color.Lime, "Ярко-зелёный"),
            new NamedColor(Color.Blue, "Синий"),
            new NamedColor(Color.Yellow, "Жёлтый"),
            new NamedColor(Color.Magenta, "Лиловый"),
            new NamedColor(Color.Cyan, "Бирюзовый"),
            new NamedColor(Color.Brown, "Коричневый"),
            new NamedColor(Color.Green, "Зелёный"),
            new NamedColor(Color.Navy, "Тёмно-синий"),
            new NamedColor(Color.Olive, "Коричнево-зелёный"),
            new NamedColor(Color.DarkMagenta, "Фиолетовый"),
            new NamedColor(Color.DarkCyan, "Тёмно-бирюзовый"),
            new NamedColor(Color.WhiteSmoke, "Серый 10%"),
            new NamedColor(Color.Gainsboro, "Серый 25%"),
            new NamedColor(Color.LightGray, "Серый 40%"),
            new NamedColor(Color.Silver, "Серый 50%"),
            new NamedColor(Color.DarkGray, "Серый 60%"),
            new NamedColor(Color.Gray, "Серый 75%"),
            new NamedColor(Color.DimGray, "Серый 90%"),
            new NamedColor(Color.Transparent, "Прозрачный")
        };
        static private List<Color> customColors = new List<Color>();
        static HatchStyle[] hatchStyleArray = (HatchStyle[])HatchStyle.GetValues(typeof(HatchStyle));
        static int hatchStyleCount = hatchStyleArray.Length - 3;
        static LinearGradientMode[] linearGradientModeArray =
            (LinearGradientMode[])LinearGradientMode.GetValues(typeof(LinearGradientMode));
        static int linearGradientModeCount = linearGradientModeArray.Length;
        static DashStyle[] dashStyleArray = (DashStyle[])DashStyle.GetValues(typeof(DashStyle));
        static int dashStyleCount = dashStyleArray.Length - 1;
        public static string[] GetPenPatternNames()
        {
            string[] dashNameArray = DashStyle.GetNames(typeof(DashStyle));
            int n = 1 + dashStyleCount;
            string[] names = new string[n];
            names[0] = "Нет";
            int i = 1;
            for (int j = 0; j < dashStyleCount; j++) { names[i] = dashNameArray[j]; i++; }
            return (names);
        }

        public static string[] GetLineJoinNames()
        {
            string[] names = new string[3];
            names[0] = "Угловое";
            names[1] = "Скошенное";
            names[2] = "Круговое";
            return (names);
        }

        public static string[] GetLineCapNames()
        {
            string[] names = new string[4];
            names[0] = "Плоское";
            names[1] = "Квадратное";
            names[2] = "Круглое";
            names[3] = "Треугольное";
            return (names);
        }

        public static bool FindColor(Color color)
        {
            bool found = false;
            for (int i = 0; i < namedColors.Count; i++) if (namedColors[i].Color == color) { found = true; break; }
            if (!found) { for (int i = 0; i < customColors.Count; i++) if (customColors[i] == color) { found = true; break; } }
            return (found);
        }
        public static bool IsNamedColorIndex(int index)
        {
            return ((index >= 0) && (index < namedColors.Count));
        }
        public static bool IsCustomColorIndex(int index)
        {
            return ((index >= namedColors.Count) && (index < namedColors.Count + customColors.Count));
        }
        public static Color ColorFromIndex(int index)
        {
            Color color = new Color();
            if ((index >= 0) && (index < namedColors.Count)) color = namedColors[index].Color;
            else
            {
                int moreIndex = index - namedColors.Count;
                if ((moreIndex >= 0) && (moreIndex < customColors.Count)) color = customColors[moreIndex];
            }
            return (color);
        }
        private static string sARGB(Color color)
        {
            StringBuilder value = new StringBuilder();
            value.Append(color.A);
            value.Append(color.R);
            value.Append(color.G);
            value.Append(color.B);
            return value.ToString();
        }
        public static int ColorToIndex(Color color)
        {
            int index = -1;
            for (int i = 0; i < namedColors.Count; i++)
            {
                if (sARGB(namedColors[i].Color) == sARGB(color))
                    { index = i; break; }
            }
            if (index < 0)
            {
                for (int i = 0; i < customColors.Count; i++)
                {
                    if (sARGB(customColors[i]) == sARGB(color))
                        { index = i + namedColors.Count; break; }
                }
            }
            return (index);
        }
        public static int[] GetCustomColors()
        {
            int count = customColors.Count;
            int[] argbColors = new int[count];
            for (int i = 0; i < count; i++) argbColors[i] = customColors[i].ToArgb();
            return (argbColors);
        }
        public static void AddCustomColor(Color color)
        {
            customColors.Add(color);
        }
        public static string[] GetAllColorNames()
        {
            int n = namedColors.Count + customColors.Count;
            string[] names = new string[n];
            int nc = 0;
            for (int i = 0; i < namedColors.Count; i++) 
                names[nc++] = namedColors[i].Name;
            for (int i = 0; i < customColors.Count; i++)
                names[nc++] = String.Format(CultureInfo.InvariantCulture, "Цвет {0}", i);
           return (names);
        }
        public static string GetColorNameFromIndex(int index)
        {
            string colorName = "";
            if (IsNamedColorIndex(index)) colorName = namedColors[index].Name;
            else 
                if (IsCustomColorIndex(index)) colorName =
                    String.Format(CultureInfo.InvariantCulture, "Цвет {0}", 
                                  index - namedColors.Count);
            return (colorName);
        }
        public static string[] GetAllPatternNames()
        {
            string[] hatchNameArray = HatchStyle.GetNames(typeof(HatchStyle));
            string[] linearGradientNameArray = LinearGradientMode.GetNames(typeof(LinearGradientMode));

            int n = 2 + linearGradientModeCount + hatchStyleCount;
            string[] names = new string[n];
            names[0] = "Прозрачный";
            names[1] = "Сплошной";
            int i = 2;
            for (int j = 0; j < linearGradientModeCount; j++) { names[i] = linearGradientNameArray[j]; i++; }
            for (int j = 0; j < hatchStyleCount; j++) { names[i] = hatchNameArray[j]; i++; }
            return (names);
        }
        public static bool IsNonePatternIndex(int index)
        {
            return (index == 0);
        }
        public static bool IsSolidPatternIndex(int index)
        {
            return (index == 1);
        }

        public static bool IsLinearGradientPatternIndex(int index)
        {
            checked
            {
                int idx = index - 2;
                return ((idx >= 0) && (idx < linearGradientModeCount));
            }
        }

        public static bool IsHatchPatternIndex(int index)
        {
            checked
            {
                int idx = index - 2 - linearGradientModeCount;
                return ((idx >= 0) && (idx < hatchStyleCount));
            }
        }

        public static FillMode FillModeFromIndex(int index)
        {
            FillMode fillmode = FillMode.None;
            if (IsNonePatternIndex(index)) fillmode = FillMode.None;
            else if (IsSolidPatternIndex(index)) fillmode = FillMode.Solid;
            else if (IsLinearGradientPatternIndex(index)) fillmode = FillMode.LinearGradient;
            else if (IsHatchPatternIndex(index)) fillmode = FillMode.Hatch;
            return (fillmode);
        }

        public static HatchStyle HatchStyleFromIndex(int index)
        {
            checked
            {
                return (HatchStyle)(index - 2 - linearGradientModeCount);
            }
        }

        public static LinearGradientMode LinearGradientModeFromIndex(int index)
        {
            checked
            {
                return (LinearGradientMode)(index - 2);
            }
        }

        public static void DrawBorder(Graphics g, RectangleF rect, Color lefttop, Color rightdown,
                                       float width)
        {
            using (Pen pen = new Pen(lefttop, width))
                g.DrawLines(pen, new PointF[]
                                { new PointF(rect.X, rect.Y + rect.Height),
                                  new PointF(rect.X, rect.Y),
                                  new PointF(rect.X + rect.Width, rect.Y)});
            using (Pen pen = new Pen(rightdown, width))
                g.DrawLines(pen, new PointF[]
                                { new PointF(rect.X + rect.Width, rect.Y),
                                  new PointF(rect.X + rect.Width, rect.Y + rect.Height),
                                  new PointF(rect.X, rect.Y + rect.Height)});
        }

        public static string getMd5Hash(IDictionary<string, object> input)
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, object> kvp in input)
                list.Add(kvp.Key.ToString() + kvp.Value);
            return getMd5Hash(String.Join("", list.ToArray()));
        }

        // Hash an input string and return the hash as
        // a 32 character hexadecimal string.
        public static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            using (MD5 md5Hasher = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        public static bool verifyMd5Hash(IDictionary<string, object> input, string hash)
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, object> kvp in input)
                list.Add(kvp.Key.ToString() + kvp.Value);
            return verifyMd5Hash(String.Join("", list.ToArray()), hash);
        }

        // Verify a hash against a string.
        public static bool verifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = getMd5Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static decimal FloatParse(string value)
        {
            decimal result = 0m;
            string sval = value.Replace(',', '.');
            if (decimal.TryParse(sval, out result))
                return result;
            else
            {
                sval = value.Replace('.', ',');
                if (decimal.TryParse(sval, out result))
                    return result;
                else
                {
                    bool bres = false;
                    if (bool.TryParse(sval, out bres))
                        return (bres) ? 1m : 0m;
                    else
                    {
                        DateTime dt;
                        DateTime now = DateTime.Now;
                        if (DateTime.TryParse(sval, out dt))
                        {
                            if (dt.Year == now.Year && dt.Month == now.Month && dt.Day == now.Day &&
                                (dt.Hour + dt.Minute + dt.Second) > 0)  // время
                                return Decimal.Parse(dt.ToString("HHmmss"));
                            else // дата
                                return Decimal.Parse(dt.ToString("ddMMyy"));
                        }
                    }
                }
            }
            return result;
        }

        static string[] formats = new string[] { "{0:0}", "{0:0.0}", "{0:0.00}", "{0:0.000}",
                                                 "{0:0000}", "{0:0.00000}", "{0:0.000000}" };
        public static string Float(object value, int format)
        {
            decimal val = FloatParse(value.ToString());
            return String.Format(formats[format], val);
        }

        public static PointF[] GetPoints(List<PointF> points)
        {   // возвращает массив точек линии
            PointF[] ps = new PointF[points.Count];
            points.CopyTo(ps);
            return (ps);
        }

        public static void AddGraphicsContent(GraphicsPath gp, List<PointF> points)
        {   // определяет содержимое графичеcкой части
            gp.AddPolygon(GetPoints(points));
        }

        public static void AddPointsRange(List<PointF> points, PointF[] pts)
        {
            points.Clear();
            points.AddRange(pts);
        }

        public static void Rotate(List<PointF> points, Single angle, RectangleF rect)
        {
            Single cx = rect.X + rect.Width * 0.5F;
            Single cy = rect.Y + rect.Height * 0.5F;
            RotateAt(points, angle, cx, cy);
        }

        public static void RotateAt(List<PointF> points, Single angle, Single cx, Single cy)
        {
            using (GraphicsPath gp = new GraphicsPath())
            {
                AddGraphicsContent(gp, points);
                using (Matrix m = new Matrix())
                {
                    m.RotateAt(angle, new PointF(cx, cy));
                    gp.Transform(m);
                }
                PointF[] ps = gp.PathPoints;
                AddPointsRange(points, ps);
            }
        }

    }
}
