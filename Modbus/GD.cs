using System;
using System.Collections.Generic;
using Points.Plugins;
using System.Drawing;

namespace Points.Modbus
{
    public class GD : ClassPoint, IPointPlugin
    {
        private void InitEnumerations()
        {
        }

        public GD()
        {
            Add("PtName", "Шифр позиции", PropType.String, String.Empty);
            Add("Version", "Версия", PropType.Invisible, DateTime.MinValue);
            Add("Plugin", "Плагин", PropType.Invisible, "MODBUS");
            Add("PtType", "Тип точки", PropType.Invisible, "GD");
            Add("PtDesc", "Дескриптор позиции", PropType.String, "Группа опроса Modbus");
            Add("PtKind", "Тип данных", PropType.Invisible, PtKind.Group);
            Add("Actived", "Опрос", PropType.Boolean, false);
            Add("FetchTime", "Время опроса (сек)", PropType.Integer, 1);
            Add("Channel", "Канал связи", PropType.Integer, 1);
            Add("Node", "Устройство", PropType.Integer, 1);
            Add("FuncCode", "Код функции", PropType.Integer, 0x02);
            Add("Address", "Номер регистра", PropType.Integer, 0);
            Add("DataCount", "Количество", PropType.Invisible, 1);
            for (int i = 1; i <= 32; i++)
                 Add("Child"+i, i+".", PropType.Link, String.Empty);
            InitEnumerations();
            //--------------------------
            Add("FetchTime", new[] { 1, 3600 });
            Add("Channel", new[] { 1, 16 });
            Add("Node", new[] { 1, 32 });
            Add("FuncCode", new[] { 0x01, 0x02 });
            Add("Address", new[] { 0, 0xffff });
            Add("DataCount", new[] { 1, 32 });
        }

        #region Члены IPointPlugin

        public new int PluginType { get { return PointPlaginType.Field; } }

        public new string PluginCategory { get { return "MODBUS"; } }

        public new string PluginName()
        {
            return String.Format("{0} - {1}", PluginShortType, PluginDescriptor);
        }

        public new string PluginVersion { get { return "1.0"; } }

        public new string PluginShortType { get { return "GD"; } }

        public new string PluginDescriptor { get { return "Группа опроса дискретных значений"; } }

        public new Color GetIconColor { get { return Color.FromArgb(192, 192, 0); } }

        public new string[] GetFilterChildTypes { get { return new string[] { "DM", "FL" }; } }

        public new Func<string, IDictionary<string, object>, ChildDesc, List<FineRow>>
            GetFinePropList
        {
            get
            {
                return (name, values, callback) =>
                {
                    List<FineRow> results = new List<FineRow>();
                    foreach (KeyValuePair<string, object> prop in values)
                    {
                        if (GetPropType(prop.Key) != PropType.Invisible)
                        {
                            string key = GetPropDesc(prop.Key);
                            if (prop.Key.StartsWith("Child") && key.EndsWith("."))
                            {
                                if (!String.IsNullOrWhiteSpace(prop.Value.ToString()))
                                    results.Add(new FineRow(key + " " +
                                        GetFineValue(prop.Key, prop.Value),
                                        callback(prop.Value.ToString()), prop.Key, prop.Value,
                                        GetPropType(prop.Key)));
                                else
                                    results.Add(new FineRow(key, String.Empty, prop.Key, prop.Value,
                                        GetPropType(prop.Key)));
                            }
                            else
                                results.Add(new FineRow(key, GetFineValue(prop.Key, prop.Value),
                                    prop.Key, prop.Value, GetPropType(prop.Key)));
                        }
                    }
                    return results;
                };
            }
        }

        public new IDictionary<string, string> Fetch(string ptname, Entity entity)
        {
           return entity.Reals; // заглушка
        }

        #endregion
    }
}

