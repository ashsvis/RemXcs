using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Points.Plugins;

namespace Points.Virtuals
{
    public class FL : ClassPoint, IPointPlugin
    {
        private void InitEnumerations()
        {
            enums.Add("Colors", DigitalTextColors);
        }
        public FL()
        {
            Add("PtName", "Шифр позиции", PropType.String, String.Empty);
            Add("Version", "Версия", PropType.Invisible, DateTime.MinValue);
            Add("Plugin", "Плагин", PropType.Invisible, "Virtual");
            Add("PtType", "Тип точки", PropType.Invisible, "FL");
            Add("PtDesc", "Дескриптор позиции", PropType.String, "Виртуальный флаг");
            Add("PtKind", "Тип данных", PropType.Invisible, PtKind.Digital);
            Add("Actived", "Опрос", PropType.Boolean, false);
            Add("FetchTime", "Время опроса (сек)", PropType.Integer, 1);
            Add("Logged", "Сигнализация", PropType.Boolean, false);
            Add("Asked", "Квитирование", PropType.Boolean, false);
            //Add("Trend", "Тренд", PropType.Boolean, false);
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
            Add("FetchTime", new[] { 1, 3600 });
        }
        #region Члены IPointPlugin
        public new string PluginCategory { get { return "Virtual"; } }
        public new int PluginType { get { return PointPlaginType.Virtual; } }
        public new string PluginName()
        {
            return String.Format("{0} - {1}", PluginShortType, PluginDescriptor);
        }
        public new string PluginVersion { get { return "1.0"; } }
        public new string PluginShortType { get { return "FL"; } }
        public new string PluginDescriptor { get { return "Флаг (дискретный)"; } }
        public new Color GetIconColor { get { return Color.FromArgb(204, 255, 255); } }

        public new IDictionary<string, string> Fetch(string ptname, Entity ent)
        {
            return ent.FetchDigital();
        }
        public new Form Passport(Entity entity, ImportRealValues updater, ExportRealValues saver)
        {
            return new frmFL(entity, updater, saver);
        }
        #endregion
    }
}
