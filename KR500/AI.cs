using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Points.Plugins;

namespace Points.KR500
{
    public class AI : ClassPoint, IPointPlugin
    {
        private void InitEnumerations()
        {
            enums.Add("Format", new string[] { "D0", "D1", "D2", "D3" });
            enums.Add("Deadband", new string[]
                { "Отключено", "Точно", "± 0.5%", "± 1%", "± 2%", "± 3%", "± 4%", "± 5%" });
            enums.Add("TableKinds", new string[] { "Отключено", "Значение", "Накопление", "Усреднение" } );
        }

        public AI()
        {
            Add("PtName", "Шифр позиции", PropType.String, String.Empty);
            Add("Version", "Версия", PropType.Invisible, DateTime.MinValue);
            Add("Plugin", "Плагин", PropType.Invisible, "KR500");
            Add("PtType", "Тип точки", PropType.Invisible, "AI");
            Add("PtDesc", "Дескриптор позиции", PropType.String, "Аналоговый выход алгоблока");
            Add("PtKind", "Тип данных", PropType.Invisible, PtKind.Analog);
            Add("Actived", "Опрос", PropType.Boolean, false);
            Add("Logged", "Сигнализация", PropType.Boolean, false);
            Add("Asked", "Квитирование", PropType.Boolean, false);
            Add("Channel", "Канал связи", PropType.Integer, 1);
            Add("Node", "Контроллер", PropType.Integer, 1);
            Add("Block", "Алгоблок", PropType.Integer, 1);
            Add("Place", "Выход алгоблока", PropType.Integer, 1);
            Add("Trend", "Тренд", PropType.Boolean, false);
            Add("Table", "Архивирование", PropType.Enumeration, 0);
            Add("Source", "Источник данных", PropType.Link, String.Empty);
            Add("Parent", "Привязка", PropType.Invisible, String.Empty);
            Add("EUDesc", "Размерность", PropType.String, "%");
            Add("FormatPV", "Формат PV", PropType.Enumeration, 1);
            Add("PVEUHi", "Верхняя граница шкалы", PropType.FloatEx, "100;0");
            Add("PVHHTP", "Верхняя предаварийная граница", PropType.FloatEx, "0;0");
            Add("PVHITP", "Верхняя предупредительная граница", PropType.FloatEx, "0;0");
            Add("PVLOTP", "Нижняя предупредительная граница", PropType.FloatEx, "0;0");
            Add("PVLLTP", "Нижняя предаварийная граница", PropType.FloatEx, "0;0");
            Add("PVEULo", "Нижняя граница шкалы", PropType.FloatEx, "0;0");
            Add("Offset", "Смещение", PropType.Float, 0m);
            Add("Koeff", "Множитель", PropType.Float, 1m);
            InitEnumerations();
            Add("Table", "TableKinds");
            Add("FormatPV", "Format");
            Add("PVEUHi", "Deadband");
            Add("PVHHTP", "Deadband");
            Add("PVHITP", "Deadband");
            Add("PVLOTP", "Deadband");
            Add("PVLLTP", "Deadband");
            Add("PVEULo", "Deadband");
            //--------------------------
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

        public new string PluginShortType { get { return "AI"; } }

        public new string PluginDescriptor { get { return "Аналоговый выход алгоблока"; } }

        public new Color GetIconColor { get { return Color.FromArgb(234, 242, 160); } }

        public new Form Passport(Entity entity, ImportRealValues updater, ExportRealValues saver)
        {
            return new frmAI(entity, updater, saver);
        }

        public new IDictionary<string, string> Fetch(string ptname, Entity entity)
        {
            return entity.FetchAnalog();
        }

        #endregion
    }
}
