using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Points.Plugins;

namespace Points.OPC
{
    class DP : ClassPoint, IPointPlugin
    {
        private void InitEnumerations()
        {
            enums.Add("Colors", DigitalTextColors);
        }
        public DP()
        {
            Add("PtName", "Шифр позиции", PropType.String, String.Empty);
            Add("Version", "Версия", PropType.Invisible, DateTime.MinValue);
            Add("Plugin", "Плагин", PropType.Invisible, PluginCategory);
            Add("PtType", "Тип точки", PropType.Invisible, PluginShortType);
            Add("PtDesc", "Дескриптор позиции", PropType.String, PluginDescriptor);
            Add("PtKind", "Тип данных", PropType.Invisible, PtKind.Digital);
            Add("Actived", "Опрос", PropType.Boolean, false);
            Add("FetchTime", "Время опроса (сек)", PropType.Integer, 1);
            Add("Logged", "Сигнализация", PropType.Boolean, false);
            Add("Asked", "Квитирование", PropType.Boolean, false);
            Add("Server", "Сервер", PropType.LinkOPC, String.Empty);
            Add("Group", "Группа", PropType.LinkOPC, String.Empty);
            Add("Item", "Параметр", PropType.LinkOPC, String.Empty);
            Add("CDT", "Канонический тип данных", PropType.TypeOPC, 0);
            Add("Trend", "Тренд", PropType.Boolean, false);
            Add("Invert", "Инверсия данных", PropType.Boolean, false);
            Add("AlarmUp", "Авария при \"0\"->\"1\"", PropType.Boolean, false);
            Add("AlarmDown", "Авария при \"1\"->\"0\"", PropType.Boolean, false);
            Add("SwitchUp", "Переключение при \"0\"->\"1\"", PropType.Boolean, false);
            Add("SwitchDown", "Переключение при \"1\"->\"0\"", PropType.Boolean, false);
            Add("ColorUp", "Цвет при \"0\"->\"1\"", PropType.Enumeration, 10);
            Add("ColorDown", "Цвет при \"1\"->\"0\"", PropType.Enumeration, 6);
            Add("TextUp", "Текст при \"0\"->\"1\"", PropType.String, "ВКЛ");
            Add("TextDown", "Текст при \"1\"->\"0\"", PropType.String, "ОТКЛ");
            InitEnumerations();
            Add("ColorUp", "Colors");
            Add("ColorDown", "Colors");
            //--------------------------
            Add("FetchTime", new[] { 1, 3600 });
        }
        #region Члены IPointPlugin
        public new int PluginType { get { return PointPlaginType.OPC; } }
        public new string PluginCategory { get { return "OPC"; } }
        public new string PluginName()
        {
            return String.Format("{0} - {1}", PluginShortType, PluginDescriptor);
        }
        public new string PluginVersion { get { return "1.0"; } }
        public new string PluginShortType { get { return "DP"; } }
        public new string PluginDescriptor { get { return "Дискретное значение"; } }
        public new Color GetIconColor { get { return Color.FromArgb(234, 242, 160); } }
        public new IDictionary<string, string> Fetch(string ptname, Entity ent)
        {
            return ent.FetchDigital();
        }
        public new Form Passport(Entity entity, ImportRealValues updater, ExportRealValues saver)
        {
            return new frmDP(entity, updater, saver);
        }
        #endregion
    }
}
