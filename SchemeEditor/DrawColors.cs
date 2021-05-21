using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

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

    public class DrawColors
    {
        private List<NamedColor> namedColors = new List<NamedColor>();
        private List<Color> customColors = new List<Color>();
        public DrawColors()
        {
            namedColors.Add(new NamedColor(Color.Black, "Чёрный"));
            namedColors.Add(new NamedColor(Color.White, "Белый"));
            namedColors.Add(new NamedColor(Color.Red, "Красный"));
            namedColors.Add(new NamedColor(Color.Lime, "Ярко-зелёный"));
            namedColors.Add(new NamedColor(Color.Blue, "Синий"));
            namedColors.Add(new NamedColor(Color.Yellow, "Жёлтый"));
            namedColors.Add(new NamedColor(Color.Magenta, "Лиловый"));
            namedColors.Add(new NamedColor(Color.Cyan, "Бирюзовый"));
            namedColors.Add(new NamedColor(Color.Brown, "Коричневый"));
            namedColors.Add(new NamedColor(Color.Green, "Зелёный"));
            namedColors.Add(new NamedColor(Color.Navy, "Тёмно-синий"));
            namedColors.Add(new NamedColor(Color.Olive, "Коричнево-зелёный"));
            namedColors.Add(new NamedColor(Color.DarkMagenta, "Фиолетовый"));
            namedColors.Add(new NamedColor(Color.DarkCyan, "Тёмно-бирюзовый"));
            namedColors.Add(new NamedColor(Color.WhiteSmoke, "Серый 10%"));
            namedColors.Add(new NamedColor(Color.Gainsboro, "Серый 25%"));
            namedColors.Add(new NamedColor(Color.LightGray, "Серый 40%"));
            namedColors.Add(new NamedColor(Color.Silver, "Серый 50%"));
            namedColors.Add(new NamedColor(Color.DarkGray, "Серый 60%"));
            namedColors.Add(new NamedColor(Color.Gray, "Серый 75%"));
            namedColors.Add(new NamedColor(Color.DimGray, "Серый 90%"));
        }
        public bool FindColor(Color color)
        {
            bool found = false;
            for (int i = 0; i < namedColors.Count; i++) if (namedColors[i].Color == color) { found = true; break; }
            if (!found) { for (int i = 0; i < customColors.Count; i++) if (customColors[i] == color) { found = true; break; } }
            return (found);
        }
        public bool IsNamedColorIndex(int index)
        {
            return ((index >= 0) && (index < namedColors.Count));
        }
        public bool IsCustomColorIndex(int index)
        {
            return ((index >= namedColors.Count) && (index < namedColors.Count + customColors.Count));
        }
        public Color ColorFromIndex(int index)
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
        public int ColorToIndex(Color color)
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
        public int[] GetCustomColors()
        {
            int count = customColors.Count;
            int[] argbColors = new int[count];
            for (int i = 0; i < count; i++) argbColors[i] = customColors[i].ToArgb();
            return (argbColors);
        }

        public void AddCustomColor(Color color)
        {
            customColors.Add(color);
        }
        public string[] GetAllColorNames()
        {
            int n = namedColors.Count + customColors.Count;
            string[] names = new string[n];
            int nc = 0;
            foreach (NamedColor brushColor in namedColors) names[nc++] = brushColor.Name;
            int i = 1;
            foreach (Color color in customColors) names[nc++] = (i++).ToString("Цвет {0}");
            return (names);
        }
        public string GetNameFromIndex(int index)
        {
            string colorName = "";
            if (IsNamedColorIndex(index)) colorName = namedColors[index].Name;
            else 
                if (IsCustomColorIndex(index)) colorName = 
                    (index - namedColors.Count).ToString("Цвет {0}");
            return (colorName);
        }
    }
}
