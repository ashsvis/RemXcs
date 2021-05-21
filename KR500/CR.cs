using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Points.Plugins;

namespace Points.KR500
{
    public class CR : ClassPoint, IPointPlugin
    {
        private void InitEnumerations()
        {
            enums.Add("NodeTypes", new string[] { "РК-131/300", "КР-300", "КР-300И", "БУСО-1", "КР-500" });
            enums.Add("Format", new string[] { "D0", "D1", "D2", "D3" });
            enums.Add("Deadband", new string[]
                { "Отключено", "Точно", "± 0.5%", "± 1%", "± 2%", "± 3%", "± 4%", "± 5%" });
            enums.Add("TableKinds", new string[] { "Отключено", "Значение", "Накопление", "Усреднение" });
        }

        public CR()
        {
            Add("PtName", "Шифр позиции", PropType.String, String.Empty);
            Add("Version", "Версия", PropType.Invisible, DateTime.MinValue);
            Add("Plugin", "Плагин", PropType.Invisible, "KR500");
            Add("PtType", "Тип точки", PropType.Invisible, "CR");
            Add("PtDesc", "Дескриптор позиции", PropType.String, "Контур регулирования");
            Add("PtKind", "Тип данных", PropType.Invisible, PtKind.Kontur);
            Add("Actived", "Опрос", PropType.Boolean, false);
            Add("Logged", "Сигнализация", PropType.Boolean, false);
            Add("Asked", "Квитирование", PropType.Boolean, false);
            Add("Channel", "Канал связи", PropType.Integer, 1);
            Add("Node", "Контроллер", PropType.Integer, 1);
            Add("NodeType", "Тип контроллера", PropType.Enumeration, 0);
            Add("Kontur", "Контур", PropType.Integer, 1);
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
            Add("SPEUHi", "Верхняя граница задания", PropType.Float, 100m);
            Add("SPEULo", "Нижняя граница задания", PropType.Float, 0m);
            Add("PVDHTP", "Отклонение PV от SP вверх", PropType.Float, 0m);
            Add("PVDLTP", "Отклонение PV от SP вниз", PropType.Float, 0m);
            Add("OPEUHi", "Верхняя граница выхода", PropType.Float, 100m);
            Add("OPEULo", "Нижняя граница выхода", PropType.Float, 0m);
            Add("K", "Коэффициент усиления", PropType.Link, String.Empty);
            Add("T1", "Интегральный коэффициент", PropType.Link, String.Empty);
            Add("T2", "Дифференциальный коэффициент", PropType.Link, String.Empty);
            InitEnumerations();
            Add("Table", "TableKinds");
            Add("NodeType", "NodeTypes");
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
            Add("Kontur", new[] { 1, 32 });
        }

        #region Члены IPointPlugin

        public new int PluginType { get { return PointPlaginType.Field; } }

        public new string PluginCategory { get { return "KR500"; } }

        public new string PluginName()
        {
            return String.Format("{0} - {1}", PluginShortType, PluginDescriptor);
        }

        public new string PluginVersion { get { return "1.0"; } }

        public new string PluginShortType { get { return "CR"; } }

        public new string PluginDescriptor { get { return "Контур регулирования"; } }

        public new Color GetIconColor { get { return Color.FromArgb(234, 242, 160); } }

        public new string[] GetFilterChildTypes { get { return new string[] { "AP" }; } }

        public new Form Passport(Entity entity, ImportRealValues updater, ExportRealValues saver)
        {
            return new frmCR(entity, updater, saver);
        }
        public new IDictionary<string, string> Fetch(string ptname, Entity entity)
        {
            bool musttrend;
            if (entity.Reals.ContainsKey("TrendTime"))
            {
                double minutes = (DateTime.Now -
                    DateTime.Parse(entity.Reals["TrendTime"])).TotalMinutes;
                musttrend = (minutes >= 1);
            }
            else
                musttrend = true;
            // Дополнительная обаботка
            entity.Reals = entity.FetchAnalog();

            int frmt = (int)entity.Values["FormatPV"];
            decimal pv = FloatEx(entity.Reals["PV"]);
            decimal lastpv = FloatEx(entity.Reals["LastPV"]);
            bool toup = pv > lastpv;
            bool todown = pv < lastpv;
            bool trend = (bool)entity.Values["Trend"];
            bool dotrend = trend && (musttrend || !musttrend && (toup || todown));
            decimal op = (entity.Reals.ContainsKey("OPRaw")) ? FloatEx(entity.Reals["OPRaw"]) : 0m;
            entity.SetRealProp("OP", op.ToString());
            entity.SetRealProp("OPText", Float(op, frmt));

            decimal SPEUHi = FloatParse(entity.Values["SPEUHi"].ToString());
            decimal SPEULo = FloatParse(entity.Values["SPEULo"].ToString());
            decimal spraw = (entity.Reals.ContainsKey("SPRaw")) ? FloatEx(entity.Reals["SPRaw"]) : 0m;
            decimal sp = CalcScales(ref entity, spraw, SPEUHi, SPEULo);
            entity.SetRealProp("SP", sp.ToString());
            entity.SetRealProp("SPText", Float(sp, frmt));

            decimal hvraw = (entity.Reals.ContainsKey("HVRaw")) ? FloatEx(entity.Reals["HVRaw"]) : 0m;
            decimal hv = CalcScales(ref entity, hvraw, SPEUHi, SPEULo);
            entity.SetRealProp("HV", hv.ToString());
            entity.SetRealProp("HVText", Float(hv, frmt));

            decimal dvraw = (entity.Reals.ContainsKey("DVRaw")) ? FloatEx(entity.Reals["DVRaw"]) : 0m;
            decimal dv = CalcScales(ref entity, dvraw, SPEUHi, SPEULo);
            entity.SetRealProp("DV", dv.ToString());
            entity.SetRealProp("DVText", Float(dv, frmt));

            if (dotrend)
            {
                entity.LogUpdate.SendToTrend(entity, "SP", true);
                entity.LogUpdate.SendToTrend(entity, "OP", true);
                entity.SetRealProp("TrendTime", DateTime.Now.ToString());
            }
            // Проценты для нужд
            decimal percent;
            if (Math.Abs(SPEUHi - SPEULo) < 0.0001m)
                percent = 0;
            else
                percent = (sp - SPEULo) / (SPEUHi - SPEULo) * 100m;
            entity.SetRealProp("SPPercent", percent.ToString());
            decimal OPEUHi = FloatParse(entity.Values["OPEUHi"].ToString());
            decimal OPEULo = FloatParse(entity.Values["OPEULo"].ToString());
            if (Math.Abs(OPEUHi - OPEULo) < 0.0001m)
                percent = 0;
            else
                percent = (op - OPEULo) / (OPEUHi - OPEULo) * 100m;
            entity.SetRealProp("OPPercent", percent.ToString());
            return entity.Reals;
        }

        private static decimal CalcScales(ref Entity entity, decimal raw, decimal EUHi, decimal EULo)
        {
            decimal PVSUHi = 100m;
            decimal PVSULo = 0m;
            decimal D = Math.Abs(PVSUHi - PVSULo) * 0.25m;
            decimal sens;
            if (Math.Abs(PVSUHi - PVSULo) < 0.0001m)
                sens = entity.Plugin.EnsureRange(raw, PVSULo - D, PVSUHi + D);
            else
                sens = (entity.Plugin.EnsureRange(raw, PVSULo - D, PVSUHi + D) - PVSULo) * 100m /
                    (PVSUHi - PVSULo);
            return (EUHi - EULo) * (sens / 100m) + EULo;
        }
        #endregion
    }
}
