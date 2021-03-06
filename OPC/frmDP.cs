using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Points.Plugins;

namespace Points.OPC
{
    public partial class frmDP : Form, IPointConnect
    {
        bool blink = false;
        Color alarmcolor = Color.Black;
        bool haactive, laactive;
        Entity ent;
        ImportRealValues import;
        ExportRealValues export;

        public frmDP(Entity ent, ImportRealValues import, ExportRealValues export)
        {
            InitializeComponent();
            this.ent = ent;
            this.import = import;
            this.export = export;
        }
        public Entity Point { get { return this.ent; } set { this.ent = value; } }

        private void frmDP_Load(object sender, EventArgs e)
        {
            FirstLoad();
            FetchPoint();
        }

        private void FirstLoad()
        {
            timerFetch.Enabled = false;

            lbPtName.Text = (string)ent.Values["PtName"];
            lbPtName1.Text = (string)ent.Values["PtName"];
            lbPtDesc.Text = (string)ent.Values["PtDesc"];
            lbPtType.Text = ent.Values["PtType"] + " " + ent.Values["Plugin"];
            lbServer.Text = ent.Values["Server"].ToString();
            lbGroup.Text = ent.Values["Group"].ToString();
            lbParameter.Text = ent.Values["Item"].ToString();
            lbCDT.Text = ent.Plugin.GetFineValue("CDT", ent.Values["CDT"]);
            lbOn.Text = (string)ent.Values["TextUp"];
            lbOnColor.BackColor = Color.LightGreen;
            lbOff.Text = (string)ent.Values["TextDown"];
            lbOffColor.BackColor = Color.Red;
            haactive = (bool)ent.Values["AlarmUp"];
            laactive = (bool)ent.Values["AlarmDown"];
            lbAlarmUp.Text = ent.Plugin.GetFineValue("AlarmUp", ent.Values["AlarmUp"]);
            lbAlarmDown.Text = ent.Plugin.GetFineValue("AlarmDown", ent.Values["AlarmDown"]);
            lbSwitchUp.Text = ent.Plugin.GetFineValue("SwitchUp", ent.Values["SwitchUp"]);
            lbSwitchDown.Text = ent.Plugin.GetFineValue("SwitchDown", ent.Values["SwitchDown"]);
            lbActived.Text = ent.Plugin.GetFineValue("Actived", ent.Values["Actived"]);
            lbLogged.Text = ent.Plugin.GetFineValue("Logged", ent.Values["Logged"]);
            lbAsked.Text = ent.Plugin.GetFineValue("Asked", ent.Values["Asked"]);
            lbFetchTime.Text = ent.Plugin.GetFineValue("FetchTime", ent.Values["FetchTime"]);
            lbInvert.Text = ent.Plugin.GetFineValue("Invert", ent.Values["Invert"]);
            timerFetch.Enabled = true;
        }

        private void frmDP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape) this.Close();
        }

        private void timerFetch_Tick(object sender, EventArgs e)
        {
            FetchPoint();
        }

        bool mustReload = false;
        private void FetchPoint()
        {
            string ptname = (string)ent.Values["PtName"];
            if (mustReload)
            {
                FirstLoad();
                mustReload = false;
            }
            if (!backgroundFetch.IsBusy) backgroundFetch.RunWorkerAsync(ptname);
        }

        private void backgroundFetch_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string ptname = (string)e.Argument;
            IDictionary<string, string> Reals = this.import(ptname);
            e.Result = Reals;
        }

        private void backgroundFetch_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            IDictionary<string, string> Reals = (IDictionary<string, string>)e.Result;
            if (!mustReload) mustReload = (Reals.ContainsKey("Version") &&
                                           Reals.ContainsKey("BaseVersion")) ?
                DateTime.Parse(Reals["Version"]) > DateTime.Parse(Reals["BaseVersion"]) : false;
            lbPV.Text = (Reals.ContainsKey("PVText")) ? Reals["PVText"] : "------";
            lbRaw.Text = (Reals.ContainsKey("Raw")) ? Reals["Raw"] : "------";
            if (Reals.ContainsKey("PV"))
            {
                bool pv = bool.Parse(Reals["PV"]);
                if (pv)
                {
                    lbPVCalc.Text = "Лог. \"1\"";
                    lbOn.ForeColor = Color.White;
                    lbOff.ForeColor = Color.Silver;
                    lbOnColor.BackColor = ent.BaseColor((int)ent.Values["ColorUp"]);
                    lbOffColor.BackColor = Color.Black;
                }
                else
                {
                    lbPVCalc.Text = "Лог. \"0\"";
                    lbOn.ForeColor = Color.Silver;
                    lbOff.ForeColor = Color.White;
                    lbOnColor.BackColor = Color.Black;
                    lbOffColor.BackColor = ent.BaseColor((int)ent.Values["ColorDown"]);
                }
            }
            else
                lbPVCalc.Text = "------";
            string alarms = (Reals.ContainsKey("Alarms")) ? Reals["Alarms"] : String.Empty;
            lbAlarmUp.BackColor = (alarms.IndexOf("HA=") < 0) ? this.BackColor : Color.Red;
            lbAlarmDown.BackColor = (alarms.IndexOf("LA=") < 0) ? this.BackColor : Color.Red;
            lbPV.BackColor = (Reals.ContainsKey("BackColor")) ?
                Color.FromArgb(int.Parse(Reals["BackColor"])) : Color.Black;
            lbPV.ForeColor = (Reals.ContainsKey("ForeColor")) ?
                Color.FromArgb(int.Parse(Reals["ForeColor"])) : Color.White;
            bool quit = (Reals.ContainsKey("QuitAlarms")) ?
                bool.Parse(Reals["QuitAlarms"]) : true;
            bool alarm = (Reals.ContainsKey("HasAlarms")) ?
                bool.Parse(Reals["HasAlarms"]) : false;
            bool lostalarm = (Reals.ContainsKey("HasLostAlarms")) ?
                bool.Parse(Reals["HasLostAlarms"]) : false;
            if (!quit && blink)
            {
                lbPV.BackColor = Color.Transparent;
                lbPV.ForeColor = Color.White;
                if (alarms.IndexOf("HA=True") < 0)
                    lbAlarmUp.BackColor = Color.Transparent;
                if (alarms.IndexOf("LA=True") < 0)
                    lbAlarmDown.BackColor = Color.Transparent;
            }
            if (Reals.ContainsKey("UserLevel"))
            {
                string slevel = Reals["UserLevel"];
                UserLevel[] levels = (UserLevel[])UserLevel.GetValues(typeof(UserLevel));
                UserLevel level = UserLevel.Диспетчер;
                foreach (UserLevel item in levels)
                    if (item.ToString().Equals(slevel)) { level = item; break; }
                if (level >= UserLevel.Оператор)
                    btnQuit.Enabled = !quit;
                else
                    btnQuit.Enabled = false;
            }
            else 
                btnQuit.Enabled = false;
            blink = !blink;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            string ptname = lbPtName.Text;
            if (!ent.Empty)
            {
                ent.Reals = import(ptname);
                string username = (ent.Reals.ContainsKey("UserName")) ? ent.Reals["UserName"] : "пользователь";
                ent.QuitAlarms(username);
                ent.SetRealProp("QuitAlarms", true.ToString());
                export("QuitAlarm", ptname, ent.Reals);
            }
        }
    }
}
