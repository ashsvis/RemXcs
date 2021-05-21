using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Points.Plugins;

namespace Points.OPC
{
    public class AP : ClassPoint, IPointPlugin
    {
        private void InitEnumerations()
        {
            enums.Add("Format", new string[] { "D0", "D1", "D2", "D3"});
            enums.Add("Deadband", new string[]
                { "Отключено", "Точно", "± 0.5%", "± 1%", "± 2%", "± 3%", "± 4%", "± 5%" });
        }

        public AP()
        {
            Add("PtName", "Шифр позиции", PropType.String, String.Empty);
            Add("Version", "Версия", PropType.Invisible, DateTime.MinValue);
            Add("Plugin", "Плагин", PropType.Invisible, PluginCategory);
            Add("PtType", "Тип точки", PropType.Invisible, PluginShortType);
            Add("PtDesc", "Дескриптор позиции", PropType.String, PluginDescriptor);
            Add("PtKind", "Тип данных", PropType.Invisible, PtKind.Analog);
            Add("Actived", "Опрос", PropType.Boolean, false);
            Add("FetchTime", "Время опроса (сек)", PropType.Integer, 1);
            Add("Logged", "Сигнализация", PropType.Boolean, false);
            Add("Asked", "Квитирование", PropType.Boolean, false);
            Add("Server", "Сервер", PropType.LinkOPC, String.Empty);
            Add("Group", "Группа", PropType.LinkOPC, String.Empty);
            Add("Item", "Параметр", PropType.LinkOPC, String.Empty);
            Add("CDT", "Канонический тип данных", PropType.TypeOPC, 0);
            Add("Trend", "Тренд", PropType.Boolean, false);
            Add("EUDesc", "Размерность", PropType.String, "%");
            Add("FormatPV", "Формат PV", PropType.Enumeration, 1);
            Add("PVSUHi", "Верхняя граница шкалы источника", PropType.Float, 100m);
            Add("PVSULo", "Нижняя граница шкалы источника", PropType.Float, 0m);
            Add("PVEUHi", "Верхняя граница шкалы", PropType.FloatEx, "100;0");
            Add("PVHHTP", "Верхняя предаварийная граница", PropType.FloatEx, "0;0");
            Add("PVHITP", "Верхняя предупредительная граница", PropType.FloatEx, "0;0");
            Add("PVLOTP", "Нижняя предупредительная граница", PropType.FloatEx, "0;0");
            Add("PVLLTP", "Нижняя предаварийная граница", PropType.FloatEx, "0;0");
            Add("PVEULo", "Нижняя граница шкалы", PropType.FloatEx, "0;0");
            Add("Offset", "Смещение", PropType.Float, 0m);
            Add("Koeff", "Множитель", PropType.Float, 1m);
            InitEnumerations();
            Add("FormatPV", "Format");
            Add("PVEUHi", "Deadband");
            Add("PVHHTP", "Deadband");
            Add("PVHITP", "Deadband");
            Add("PVLOTP", "Deadband");
            Add("PVLLTP", "Deadband");
            Add("PVEULo", "Deadband");
            //--------------------------
            Add("FetchTime", new[] { 1, 3600 });
        }

        #region Члены IPointPlugin

        public new string PluginCategory { get { return "OPC"; } }

        public new int PluginType { get { return PointPlaginType.OPC; } }

        public new string PluginName()
        {
            return String.Format("{0} - {1}", PluginShortType, PluginDescriptor);
        }

        public new string PluginVersion { get { return "1.0"; } }

        public new string PluginShortType { get { return "AP"; } }

        public new string PluginDescriptor { get { return "Аналоговое значение"; } }

        public new Color GetIconColor { get { return Color.FromArgb(234, 242, 160); } }

        public new Form Passport(Entity entity, ImportRealValues updater, ExportRealValues saver)
        {
            return new frmAP(entity, updater, saver);
        }
        public new IDictionary<string, string> Fetch(string ptname, Entity entity)
        {
            return entity.FetchAnalog();
        }

        #endregion
    }
}
