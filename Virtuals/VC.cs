using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Points.Plugins;

namespace Points.Virtuals
{
    public class VC : ClassPoint, IPointPlugin
    {
        private void InitEnumerations()
        {
        }
        public VC()
        {
            Add("PtName", "Шифр позиции", PropType.String, String.Empty);
            Add("Version", "Версия", PropType.Invisible, DateTime.MinValue);
            Add("Plugin", "Плагин", PropType.Invisible, "Virtual");
            Add("PtType", "Тип точки", PropType.Invisible, "VC");
            Add("PtDesc", "Дескриптор позиции", PropType.String, "Управление задвижкой");
            Add("PtKind", "Тип данных", PropType.Invisible, PtKind.Valve);
            Add("Actived", "Опрос", PropType.Boolean, false);
            Add("FetchTime", "Время опроса (сек)", PropType.Integer, 1);
            Add("Logged", "Сигнализация", PropType.Boolean, false);
            Add("Asked", "Квитирование", PropType.Boolean, false);
            Add("Source", "Источник данных", PropType.Link, String.Empty);
            Add("Parent", "Привязка", PropType.Invisible, String.Empty);
            Add("StatALM", "Состояние АВАРИЯ", PropType.Link, String.Empty);
            Add("StatON", "Состояние ОТКРЫТО", PropType.Link, String.Empty);
            Add("StatOFF", "Состояние ЗАКРЫТО", PropType.Link, String.Empty);
            Add("CommON", "Команда ОТКРЫТЬ", PropType.Link, String.Empty);
            Add("CommOFF", "Команда ЗАКРЫТЬ", PropType.Link, String.Empty);
            InitEnumerations();
            //--------------------------
            Add("FetchTime", new[] {1, 3600});
        }
        #region Члены IPointPlugin
        public new string PluginCategory { get { return "Virtual"; } }
        public new int PluginType { get { return PointPlaginType.Virtual; } }
        public new string PluginName()
        {
            return String.Format("{0} - {1}", PluginShortType, PluginDescriptor);
        }
        public new string PluginVersion { get { return "1.0"; } }
        public new string PluginShortType { get { return "VC"; } }
        public new string PluginDescriptor { get { return "Управление задвижкой"; } }
        public new Color GetIconColor { get { return Color.FromArgb(204, 255, 255); } }

        public new string[] GetFilterChildTypes { get { return new string[] { "DI", "DP", "FL" }; } }

        public new Form Passport(Entity entity, ImportRealValues updater, ExportRealValues saver)
        {
            return new frmVC(entity, updater, saver);
        }
        public new IDictionary<string, string> Fetch(string ptname, Entity ent)
        {
            #region расчёт алармов
            bool logged = (bool)ent.Values["Logged"];
            bool asked = (bool)ent.Values["Asked"];
            ushort states;
            if (ent.Reals.ContainsKey("Raw") && ushort.TryParse(ent.Reals["Raw"], out states))
            {
                bool wa = false;
                bool em = false;
                if (logged)
                {
                    wa = ent.GetBit(states, 0) && ent.GetBit(states, 1);
                    if (!wa && ent.inAlarms("WA")) ent.removeAlarm("WA");
                    if (wa && !ent.inAlarms("WA")) ent.addAlarm("WA");
                    em = ent.GetBit(states, 2);
                    if (!em && ent.inAlarms("EM")) ent.removeAlarm("EM");
                    if (em && !ent.inAlarms("EM")) ent.addAlarm("EM");

                    if (ent.isAlarms()) ent.updateAlarmValue();
                }
                else
                {
                    ent.SetRealProp("Alarms", String.Empty);
                    ent.SetRealProp("LostAlarms", String.Empty);
                }
                bool quit;
                if (ent.Reals.ContainsKey("QuitAlarms"))
                    quit = Boolean.Parse(ent.Reals["QuitAlarms"]);
                else
                {
                    quit = true;
                    ent.SetRealProp("QuitAlarms", quit.ToString());
                }
                quit = (asked) ? ent.isQuitAlarms() : true;
                //-----------------------------------
                Color backcolor, forecolor;
                if (ent.inAlarms("EM"))
                {
                    backcolor = Color.Red;
                    forecolor = Color.White;
                }
                else if (ent.inAlarms("WA"))
                {
                    backcolor = Color.Yellow;
                    forecolor = Color.Black;
                }
                else if (ent.isLostAlarms() && asked)
                {
                    backcolor = Color.LightGray;
                    forecolor = Color.Black;
                }
                else
                {
                    backcolor = Color.Black;
                    forecolor = Color.White;
                }
                ent.SetRealProp("BackColor", backcolor.ToArgb().ToString());
                ent.SetRealProp("ForeColor", forecolor.ToArgb().ToString());
                ent.SetRealProp("QuitAlarms", quit.ToString());
                bool isactive = em || wa;
                ent.SetRealProp("HasAlarms", (isactive) ? ent.isAlarms().ToString() : false.ToString());
                ent.SetRealProp("HasLostAlarms", (isactive) ? ent.isLostAlarms().ToString() : false.ToString());
            }
            #endregion
            return ent.Reals;
        }
        #endregion
    }
}
