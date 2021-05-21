using System;

namespace Points.Plugins
{
    [Serializable]
    public enum UserLevel {Диспетчер, Оператор, Приборист, Технолог, Инженер, Программист}

    [Serializable]
    public class UserCategory
    {
        public UserLevel Style { get; set; }
        public string Label { get; set; }
        public UserCategory(UserLevel style, string label)
        {
            this.Style = style;
            this.Label = label;
        }
    }

    public static class UserInfo
    {
        public static UserLevel GetCurrentLevel(string slevel)
        {
            UserLevel[] levels = (UserLevel[])UserLevel.GetValues(typeof(UserLevel));
            UserLevel level = UserLevel.Диспетчер;
            foreach (UserLevel item in levels)
                if (item.ToString().Equals(slevel)) { level = item; break; }
            return level;
        }

        public static int UserLevelToInt(UserLevel level)
        {
            UserLevel[] levels = (UserLevel[])UserLevel.GetValues(typeof(UserLevel));
            int n = 0;
            foreach (UserLevel item in levels)
            {
                if (item == level) break;
                n++;
            }
            return n;
        }

        public static UserLevel IntToUserLevel(int index)
        {
            UserLevel[] levels = (UserLevel[])UserLevel.GetValues(typeof(UserLevel));
            UserLevel level = UserLevel.Диспетчер;
            int n = 0;
            foreach (UserLevel item in levels)
            {
                if (n == index) { level = item; break; }
                n++;
            }
            return level;
        }

    }
}
