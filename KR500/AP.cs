using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Points.Plugins;

namespace Points.KR500
{
    public class AP : ClassPoint, IPointPlugin
    {
        private void InitEnumerations()
        {
            enums.Add("Format", new string[] { "D0", "D1", "D2", "D3" });
        }

        public AP()
        {
            Add("PtName", "Шифр позиции", PropType.String, String.Empty);
            Add("Version", "Версия", PropType.Invisible, DateTime.MinValue);
            Add("Plugin", "Плагин", PropType.Invisible, "KR500");
            Add("PtType", "Тип точки", PropType.Invisible, "AP");
            Add("PtDesc", "Дескриптор позиции", PropType.String, "Аналоговый параметр алгоблока");
            Add("PtKind", "Тип данных", PropType.Invisible, PtKind.Analog);
            Add("Actived", "Опрос", PropType.Boolean, false);
            Add("Channel", "Канал связи", PropType.Integer, 1);
            Add("Node", "Контроллер", PropType.Integer, 1);
            Add("Block", "Алгоблок", PropType.Integer, 1);
            Add("Place", "Параметр алгоблока", PropType.Integer, 1);
            Add("Source", "Источник данных", PropType.Link, String.Empty);
            Add("Parent", "Привязка", PropType.Invisible, String.Empty);
            Add("EUDesc", "Размерность", PropType.String, "%");
            Add("FormatPV", "Формат OP", PropType.Enumeration, 1);
            Add("OPEUHi", "Верхняя граница шкалы", PropType.Float, 100m);
            Add("OPEULo", "Нижняя граница шкалы", PropType.Float, 0m);
            Add("Koeff", "Делитель", PropType.Float, 1m);
            Add("Offset", "Смещение", PropType.Float, 0m);
            InitEnumerations();
            Add("FormatPV", "Format");
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

        public new string PluginShortType { get { return "AP"; } }

        public new string PluginDescriptor { get { return "Аналоговый параметр алгоблока"; } }

        public new Color GetIconColor { get { return Color.FromArgb(234, 242, 160); } }

        public new Form Passport(Entity entity, ImportRealValues updater, ExportRealValues saver)
        {
            return new frmAP(entity, updater, saver);
        }
        public new IDictionary<string, string> Fetch(string ptname, Entity entity)
        {
            return entity.FetchAnalogParam();
        }
        #endregion
    }
}
