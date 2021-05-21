using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace SchemeEditor
{

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
            new NamedColor(Color.DimGray, "Серый 90%")
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
        public static int ColorToIndex(Color color)
        {
            int index = -1;
            for (int i = 0; i < namedColors.Count; i++)
            {
                if (namedColors[i].Color == color) { index = i; break; }
            }
            if (index < 0)
            {
                for (int i = 0; i < customColors.Count; i++)
                {
                    if (customColors[i] == color) { index = i + namedColors.Count; break; }
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
    }
}
