using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Points.Plugins;

namespace Points.KR500
{
    public class DI : ClassPoint, IPointPlugin
    {
        private void InitEnumerations()
        {
            enums.Add("Colors", DigitalTextColors);
            //enums.Add("TableKinds", new string[] { "Отключено", "Значение", "Накопление", "Усреднение" });
        }
        public DI()
        {
            Add("PtName", "Шифр позиции", PropType.String, String.Empty);
            Add("Version", "Версия", PropType.Invisible, DateTime.MinValue);
            Add("Plugin", "Плагин", PropType.Invisible, "KR500");
            Add("PtType", "Тип точки", PropType.Invisible, "DI");
            Add("PtDesc", "Дескриптор позиции", PropType.String, "Дискретный выход алгоблока");
            Add("PtKind", "Тип данных", PropType.Invisible, PtKind.Digital);
            Add("Actived", "Опрос", PropType.Boolean, false);
            //Add("FetchTime", "Время опроса (сек)", PropType.Integer, 1);
            Add("Logged", "Сигнализация", PropType.Boolean, false);
            Add("Asked", "Квитирование", PropType.Boolean, false);
            //Add("Trend", "Тренд", PropType.Boolean, false);
            //Add("Table", "Архивирование", PropType.Enumeration, 0);
            Add("Channel", "Канал связи", PropType.Integer, 1);
            Add("Node", "Контроллер", PropType.Integer, 1);
            Add("Block", "Алгоблок", PropType.Integer, 1);
            Add("Place", "Выход алгоблока", PropType.Integer, 1);
            Add("Source", "Источник данных", PropType.Link, String.Empty);
            Add("Parent", "Привязка", PropType.Invisible, String.Empty);
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
            //Add("Table", "TableKinds");
            Add("FetchTime", new[] { 1, 3600 });
            Add("Channel", new[] { 1, 16 });
            Add("Node", new[] { 1, 32 });
            Add("Block", new[] { 1, 999 });
            Add("Place", new[] { 1, 127 });
        }
        #region Члены IPointPlugin
        public new int PluginType { get { return PointPlaginType.Field; } }
        public new string PluginCategory { get { return "KR500"; } }
        public new string PluginName()
        {
            return String.Format("{0} - {1}", PluginShortType, PluginDescriptor);
        }
        public new string PluginVersion { get { return "1.0"; } }
        public new string PluginShortType { get { return "DI"; } }
        public new string PluginDescriptor { get { return "Дискретный выход алгоблока"; } }
        public new Color GetIconColor { get { return Color.FromArgb(234, 242, 160); } }
        public new IDictionary<string, string> Fetch(string ptname, Entity ent)
        {
            return ent.FetchDigital();
        }
        public new Form Passport(Entity entity, ImportRealValues updater, ExportRealValues saver)
        {
            return new frmDI(entity, updater, saver);
        }
        #endregion
    }
}
