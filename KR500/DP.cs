using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Points.Plugins;

namespace Points.KR500
{
    public class DP : ClassPoint, IPointPlugin
    {
        private void InitEnumerations()
        {
            enums.Add("Colors", DigitalTextColors);
        }
        public DP()
        {
            Add("PtName", "Шифр позиции", PropType.String, String.Empty);
            Add("Version", "Версия", PropType.Invisible, DateTime.MinValue);
            Add("Plugin", "Плагин", PropType.Invisible, "KR500");
            Add("PtType", "Тип точки", PropType.Invisible, "DP");
            Add("PtDesc", "Дескриптор позиции", PropType.String, "Дискретный параметр алгоблока");
            Add("PtKind", "Тип данных", PropType.Invisible, PtKind.Digital);
            Add("Actived", "Опрос", PropType.Boolean, false);
            //Add("FetchTime", "Время опроса (сек)", PropType.Integer, 1);
            Add("Channel", "Канал связи", PropType.Integer, 1);
            Add("Node", "Контроллер", PropType.Integer, 1);
            Add("Block", "Алгоблок", PropType.Integer, 1);
            Add("Place", "Параметр алгоблока", PropType.Integer, 1);
            Add("Source", "Источник данных", PropType.Link, String.Empty);
            Add("Parent", "Привязка", PropType.Invisible, String.Empty);
            Add("Strob", "Время строба (мсек)", PropType.Integer, 1000);
            Add("Invert", "Инверсия данных", PropType.Boolean, false);
            InitEnumerations();
            //--------------------------
            Add("FetchTime", new[] { 1, 3600 });
            Add("Channel", new[] { 1, 16 });
            Add("Node", new[] { 1, 32 });
            Add("Block", new[] { 1, 999 });
            Add("Place", new[] { 1, 127 });
            Add("Strob", new[] { 500, 5000 });
        }
        #region Члены IPointPlugin
        public new int PluginType { get { return PointPlaginType.Field; } }
        public new string PluginCategory { get { return "KR500"; } }
        public new string PluginName()
        {
            return String.Format("{0} - {1}", PluginShortType, PluginDescriptor);
        }
        public new string PluginVersion { get { return "1.0"; } }
        public new string PluginShortType { get { return "DP"; } }
        public new string PluginDescriptor { get { return "Дискретный параметр алгоблока"; } }
        public new Color GetIconColor { get { return Color.FromArgb(234, 242, 160); } }
        public new IDictionary<string, string> Fetch(string ptname, Entity ent)
        {
            return ent.FetchDigitalParam();
        }
        public new Form Passport(Entity entity, ImportRealValues updater, ExportRealValues saver)
        {
            return new frmDP(entity, updater, saver);
        }
        #endregion
    }
}
