using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Points.Plugins;

namespace Points.KR500
{
    public class ND : ClassPoint, IPointPlugin
    {
        private void InitEnumerations()
        {
            enums.Add("NodeTypes", new string[] { "РК-131/300", "КР-300", "КР-300И", "БУСО-1", "КР-500" });
        }

        public ND()
        {
            Add("PtName", "Шифр позиции", PropType.String, String.Empty);
            Add("Version", "Версия", PropType.Invisible, DateTime.MinValue);
            Add("Plugin", "Плагин", PropType.Invisible, "KR500");
            Add("PtType", "Тип точки", PropType.Invisible, "ND");
            Add("PtDesc", "Дескриптор позиции", PropType.String, "Информация контроллера");
            Add("PtKind", "Тип данных", PropType.Invisible, PtKind.Nothing);
            Add("Actived", "Опрос", PropType.Boolean, false);
            Add("FetchTime", "Время опроса (сек)", PropType.Integer, 1);
            Add("Logged", "Сигнализация", PropType.Boolean, false);
            Add("Asked", "Квитирование", PropType.Boolean, false);
            Add("Channel", "Канал связи", PropType.Integer, 1);
            Add("Node", "Контроллер", PropType.Integer, 1);
            Add("Parent", "Привязка", PropType.Invisible, String.Empty);
            Add("NodeType", "Тип контроллера", PropType.Enumeration, 0);
            InitEnumerations();
            Add("NodeType", "NodeTypes");
            //--------------------------
            Add("FetchTime", new[] { 1, 3600 });
            Add("Channel", new[] { 1, 16 });
            Add("Node", new[] { 1, 32 });
        }
        #region Члены IPointPlugin
        public new int PluginType { get { return PointPlaginType.Field; } }
        public new string PluginCategory { get { return "KR500"; } }
        public new string PluginName()
        {
            return String.Format("{0} - {1}", PluginShortType, PluginDescriptor);
        }
        public new string PluginVersion { get { return "1.0"; } }
        public new string PluginShortType { get { return "ND"; } }
        public new string PluginDescriptor { get { return "Информация контроллера"; } }
        public new Color GetIconColor { get { return Color.FromArgb(214, 222, 140); } }
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
