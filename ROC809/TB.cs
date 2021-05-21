using System;
using System.Collections.Generic;
using System.Drawing;
using Points.Plugins;

namespace Points.ROC809
{
    public class TB : ClassPoint, IPointPlugin
    {
        private void InitEnumerations()
        {
            enums.Add("TableTypes", new string[] {"Минутные","Часовые","Суточные","Месячные"});
            enums.Add("ArchTypes", new string[] { "Значение", "Накопление", "Усреднение"});
        }

        public TB()
        {
            Add("PtName", "Шифр позиции", PropType.String, String.Empty);
            Add("Version", "Версия", PropType.Invisible, DateTime.MinValue);
            Add("Plugin", "Плагин", PropType.Invisible, "ROC809");
            Add("PtType", "Тип точки", PropType.Invisible, "TB");
            Add("PtDesc", "Дескриптор позиции", PropType.String, "Группа архива ROC809");
            Add("PtKind", "Тип данных", PropType.Invisible, PtKind.Table);
            Add("Actived", "Опрос", PropType.Boolean, false);
            Add("FetchTime", "Время опроса (сек)", PropType.Integer, 1);
            Add("Address", "Номер регистра", PropType.Integer, 0);
            Add("Channel", "Канал связи", PropType.Integer, 1);
            Add("Node", "Устройство", PropType.Integer, 20);
            Add("TableType", "Тип таблицы", PropType.Enumeration, 0);
            Add("RecordCount", "Количество записей", PropType.Integer, 0);
            for (int i = 1; i <= 32; i++)
                Add("Child" + i, i + ".", PropType.LinkEx, String.Empty);
            InitEnumerations();
            Add("TableType", "TableTypes");
            for (int i = 1; i <= 32; i++) Add("Child" + i, "ArchTypes");
            //--------------------------
            Add("FetchTime", new[] { 1, 3600 });
            Add("Channel", new[] { 1, 16 });
            Add("Node", new[] { 1, 32 });
            Add("Address", new[] { 0, 65535 });
            Add("RecordCount", new[] { 0, 65535 });
        }

        #region Члены IPointPlugin

        public new int PluginType { get { return PointPlaginType.Field; } }

        public new string PluginCategory { get { return "ROC809"; } }

        public new string PluginName()
        {
            return String.Format("{0} - {1}", PluginShortType, PluginDescriptor);
        }

        public new string PluginVersion { get { return "1.0"; } }

        public new  string PluginShortType { get { return "TB"; } }

        public new string PluginDescriptor { get { return "Группа архива"; } }

        public new Color GetIconColor { get { return Color.FromArgb(192, 192, 0); } }

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
                            {   // свойства ChildN
                                if (!String.IsNullOrWhiteSpace(prop.Value.ToString()))
                                {
                                    string[] vals = prop.Value.ToString().Split(new char[] { ';' });
                                    int enumVal = (vals.Length == 2) ? int.Parse(vals[1]) : 0;
                                    results.Add(new FineRow(key + " " +
                                        GetFineValue(prop.Key, vals[0]), //prop.Value
                                        String.Format("({0}) {1}", GetEnumValue(prop.Key,enumVal),
                                                    callback(vals[0])),
                                        prop.Key, prop.Value,
                                        GetPropType(prop.Key)));
                                }
                                else  // пустые значения свойств ChildN
                                    results.Add(new FineRow(key, String.Empty, prop.Key, prop.Value,
                                        GetPropType(prop.Key)));
                            }
                            else // другие свойства
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
