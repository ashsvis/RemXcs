using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using Points.Plugins;

namespace Points.Virtuals
{
    public partial class frmVC : Form, IPointConnect
    {
        bool blink = false;
        Color alarmcolor = Color.Black;
        Entity ent;
        ImportRealValues import;
        ExportRealValues export;
        public frmVC(Entity ent, ImportRealValues import, ExportRealValues export)
        {
            InitializeComponent();
            this.Width = 160;
            this.ent = ent;
            this.import = import;
            this.export = export;
        }
        public Entity Point { get { return this.ent; } set { this.ent = value; } }
        private void frmFL_Load(object sender, EventArgs e)
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
            lbStateOffSource.Text = (string)ent.Values["StatOFF"];
            lbStateOnSource.Text = (string)ent.Values["StatON"];
            lbStateAlmSource.Text = (string)ent.Values["StatALM"];
            lbCommOffSource.Text = (string)ent.Values["CommOFF"];
            lbCommOnSource.Text = (string)ent.Values["CommON"];
            lbActived.Text = ent.Plugin.GetFineValue("Actived", ent.Values["Actived"]);
            lbLogged.Text = ent.Plugin.GetFineValue("Logged", ent.Values["Logged"]);
            lbAsked.Text = ent.Plugin.GetFineValue("Asked", ent.Values["Asked"]);
            lbFetchTime.Text = ent.Plugin.GetFineValue("FetchTime", ent.Values["FetchTime"]);
            timerFetch.Enabled = true;
        }

        private void frmFL_KeyDown(object sender, KeyEventArgs e)
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
            bool boolval;
            if (Reals.ContainsKey("StatOFFState") && bool.TryParse(Reals["StatOFFState"], out boolval))
                lbStateOffState.Text = (boolval) ? "Да" : "Нет";
            if (Reals.ContainsKey("StatONState") && bool.TryParse(Reals["StatONState"], out boolval))
                lbStateOnState.Text = (boolval) ? "Да" : "Нет";
            if (Reals.ContainsKey("StatALMState") && bool.TryParse(Reals["StatALMState"], out boolval))
                lbStateAlmState.Text = (boolval) ? "Да" : "Нет";
            if (Reals.ContainsKey("CommOFFState") && bool.TryParse(Reals["CommOFFState"], out boolval))
                lbCommOffState.Text = (boolval) ? "Да" : "Нет";
            if (Reals.ContainsKey("CommONState") && bool.TryParse(Reals["CommONState"], out boolval))
                lbCommOnState.Text = (boolval) ? "Да" : "Нет";
            lbGroupFactTime.Text = (Reals.ContainsKey("Seconds")) ?
                Reals["Seconds"] : "------";
            #region определение таймаута
            bool timeout = false;
            DateTime facttime;
            int displaytimeout;
            if (Reals.ContainsKey("Quality") && Reals.ContainsKey("FactTime") &&
                DateTime.TryParse(Reals["FactTime"], out facttime) &&
                Reals.ContainsKey("DisplayTimeout") &&
                int.TryParse(Reals["DisplayTimeout"], out displaytimeout))
            {
                if (DateTime.Now - facttime >
                    TimeSpan.FromSeconds(displaytimeout))
                {
                    timeout = true;
                    lbGroupFactTime.Text = "нет опроса";
                }
            }
            #endregion
            lbPV.Text = (Reals.ContainsKey("PVText")) ? Reals["PVText"] : "------";
            lbRaw.Text = (Reals.ContainsKey("Raw")) ? Reals["Raw"] : "------";
            ushort states;
            if (Reals.ContainsKey("PV") && ushort.TryParse(Reals["PV"], out states))
            {
                StringBuilder sb = new StringBuilder(Convert.ToString(states, 2));
                while (sb.Length < 7) sb.Insert(0, "0");
                lbPVCalc.Text = sb.ToString();
                if (ent.GetBit(states, 0))
                {
                    lbOff.ForeColor = Color.White;
                    lbOffColor.BackColor = Color.Red;
                }
                else
                {
                    lbOff.ForeColor = Color.Gray;
                    lbOffColor.BackColor = Color.Black;
                }
                if (ent.GetBit(states, 1))
                {
                    lbOn.ForeColor = Color.White;
                    lbOnColor.BackColor = Color.Lime;
                }
                else
                {
                    lbOn.ForeColor = Color.Gray;
                    lbOnColor.BackColor = Color.Black;
                }
            }
            else
                lbPVCalc.Text = "------";
            string alarms = (Reals.ContainsKey("Alarms")) ? Reals["Alarms"] : String.Empty;
            lbStateAlmState.BackColor = (alarms.IndexOf("EM=") < 0) ? this.BackColor : Color.Red;
            lbStateOnState.BackColor = (alarms.IndexOf("WA=") < 0) ? this.BackColor : Color.Yellow;
            lbStateOnState.ForeColor = (alarms.IndexOf("WA=") < 0) ? Color.White : Color.Black;
            lbStateOffState.BackColor = (alarms.IndexOf("WA=") < 0) ? this.BackColor : Color.Yellow;
            lbStateOffState.ForeColor = (alarms.IndexOf("WA=") < 0) ? Color.White : Color.Black;
            if (!timeout)
            {
                lbPV.BackColor = (Reals.ContainsKey("BackColor")) ?
                    Color.FromArgb(int.Parse(Reals["BackColor"])) : Color.Black;
                lbPV.ForeColor = (Reals.ContainsKey("ForeColor")) ?
                    Color.FromArgb(int.Parse(Reals["ForeColor"])) : Color.White;
            }
            else
            {
                lbPV.BackColor = Color.Blue;
                lbPV.ForeColor = Color.White;
            }
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
                if (alarms.IndexOf("EM=True") < 0)
                    lbStateAlmState.BackColor = Color.Transparent;
                if (alarms.IndexOf("WA=True") < 0)
                {
                    lbStateOnState.BackColor = Color.Transparent;
                    lbStateOnState.ForeColor = Color.White;
                    lbStateOffState.BackColor = Color.Transparent;
                    lbStateOffState.ForeColor = Color.White;
                }
            }
            if (Reals.ContainsKey("UserLevel"))
            {
                string slevel = Reals["UserLevel"];
                if (UserInfo.GetCurrentLevel(slevel) >= UserLevel.Оператор)
                {
                    btnQuit.Enabled = !quit;
                    btnClose.Enabled = btnOpen.Enabled = true; 
                }
                else
                    btnQuit.Enabled = btnClose.Enabled = btnOpen.Enabled = false;
            }
            else
                btnQuit.Enabled = btnClose.Enabled = btnOpen.Enabled = false;
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

        private void lbCommOnSource_DoubleClick(object sender, EventArgs e)
        {
            Label lab = (Label)sender;
            string ptname = lab.Text;
            export("ShowPassport", ptname, null);
        }

        private void btnSize_Click(object sender, EventArgs e)
        {
            switch (this.Width)
            {
                case 160: this.Width = 480; break;
                case 480: this.Width = 736; btnSize.Text = "<"; break;
                case 736: this.Width = 160; btnSize.Text = ">"; break;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string ptname = lbCommOnSource.Text;
            IDictionary<string, string> reals = import(ptname); 
            if (reals.Count > 0)
            {
                if (reals.ContainsKey("Command"))
                    reals["Command"] = "CLICK";
                else
                    reals.Add("Command", "CLICK");
                export("Command", ptname, reals);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string ptname = lbCommOffSource.Text;
            IDictionary<string, string> reals = import(ptname);
            if (reals.Count > 0)
            {
                if (reals.ContainsKey("Command"))
                    reals["Command"] = "CLICK";
                else
                    reals.Add("Command", "CLICK");
                export("Command", ptname, reals);
            }
        }
    }
}
