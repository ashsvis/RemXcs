using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Points.Plugins;

namespace Points.Modbus
{
    public partial class frmAM : Form, IPointConnect
    {
        bool blink = false;
        Color alarmcolor = Color.Black;
        Decimal lastcent = 0;
        Decimal percent, hhps, hips,lops,llps;
        bool hhactive, hiactive, loactive, llactive;
        Entity ent;
        ImportRealValues import;
        ExportRealValues export;
        public frmAM(Entity ent, ImportRealValues import, ExportRealValues export)
        {
            InitializeComponent();
            this.Width = 164;
            this.ent = ent;
            this.import = import;
            this.export = export;
        }
        public Entity Point { get { return this.ent; } set { this.ent = value; } }
        private void frmAI_Load(object sender, EventArgs e)
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
            lbEUDesc.Text = (string)ent.Values["EUDesc"];
            lbPtType.Text = ent.Values["PtType"] + " " + ent.Values["Plugin"];
            lbChannel.Text = ent.Values["Channel"].ToString();
            lbNode.Text = ent.Values["Node"].ToString();
            lbAddress.Text = ent.Values["Address"].ToString();
            lbFuncCode.Text = ent.Values["FuncCode"].ToString();
            lbDataType.Text = ent.Plugin.GetEnumValue("DataType", ent.Values["DataType"]);
            Decimal euhi = ent.FloatEx((string)ent.Values["PVEUHi"]);
            Decimal eulo = ent.FloatEx((string)ent.Values["PVEULo"]);
            Decimal range = euhi - eulo;
            if (Math.Abs(range) > 0.0001m)
            {
                hhps = ent.FloatEx((string)ent.Values["PVHHTP"]) / range;
                hips = ent.FloatEx((string)ent.Values["PVHITP"]) / range;
                lops = ent.FloatEx((string)ent.Values["PVLOTP"]) / range;
                llps = ent.FloatEx((string)ent.Values["PVLLTP"]) / range;
            }
            else
                hhps = hips = lops = llps = 0;
            hhactive = ent.EnumEx((string)ent.Values["PVHHTP"]) > 0;
            hiactive = ent.EnumEx((string)ent.Values["PVHITP"]) > 0;
            loactive = ent.EnumEx((string)ent.Values["PVLOTP"]) > 0;
            llactive = ent.EnumEx((string)ent.Values["PVLLTP"]) > 0;
            int frmt = (int)ent.Values["FormatPV"];
            lbPVHHTP.Text = ent.Plugin.GetFineValue("PVHHTP", ent.Values["PVHHTP"], frmt);
            lbPVHITP.Text = ent.Plugin.GetFineValue("PVHITP", ent.Values["PVHITP"], frmt);
            lbPVLOTP.Text = ent.Plugin.GetFineValue("PVLOTP", ent.Values["PVLOTP"], frmt);
            lbPVLLTP.Text = ent.Plugin.GetFineValue("PVLLTP", ent.Values["PVLLTP"], frmt);
            lbPVSUHI.Text = ent.Plugin.GetFineValue("PVSUHi", ent.Values["PVSUHi"], frmt);
            lbPVSULO.Text = ent.Plugin.GetFineValue("PVSULo", ent.Values["PVSULo"], frmt);
            lbPVEUHI.Text = ent.Plugin.GetFineValue("PVEUHi", ent.Values["PVEUHi"], frmt);
            lbPVEULO.Text = ent.Plugin.GetFineValue("PVEULo", ent.Values["PVEULo"], frmt);
            lbKoeff.Text = ent.Plugin.GetFineValue("Koeff", ent.Values["Koeff"], frmt);
            lbOffset.Text = ent.Plugin.GetFineValue("Offset", ent.Values["Offset"], frmt);
            lbActived.Text = ent.Plugin.GetFineValue("Actived", ent.Values["Actived"]);
            lbLogged.Text = ent.Plugin.GetFineValue("Logged", ent.Values["Logged"]);
            lbAsked.Text = ent.Plugin.GetFineValue("Asked", ent.Values["Asked"]);
            lbPVFormat.Text = ent.Plugin.GetFineValue("FormatPV", ent.Values["FormatPV"]);
            lbTrend.Text = ent.Plugin.GetFineValue("Trend", ent.Values["Trend"]);
            timerFetch.Enabled = true;
        }
        private void frmAI_KeyDown(object sender, KeyEventArgs e)
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

        private void backgroundFetch_RunWorkerCompleted(object sender,
            System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            IDictionary<string, string> Reals = (IDictionary<string, string>)e.Result;
            if (!mustReload) mustReload = (Reals.ContainsKey("Version") &&
                                           Reals.ContainsKey("BaseVersion")) ?
                DateTime.Parse(Reals["Version"]) > DateTime.Parse(Reals["BaseVersion"]) : false;
            lbFetchGroup.Text = (Reals.ContainsKey("FetchGroup")) ?
                Reals["FetchGroup"] : "------";
            lbGroupFetchTime.Text = (Reals.ContainsKey("GroupFetchTime")) ?
                Reals["GroupFetchTime"] : "------";
            lbGroupFactTime.Text = (Reals.ContainsKey("Seconds")) ?
                Reals["Seconds"] : "------";
            lbPV.Text = (Reals.ContainsKey("PVText")) ? Reals["PVText"] : "------";
            lbRaw.Text = (Reals.ContainsKey("Raw")) ? Reals["Raw"] : "------";
            lbPVCalc.Text = (Reals.ContainsKey("PV")) ? Reals["PV"] : "------";
            string alarms = (Reals.ContainsKey("Alarms")) ? Reals["Alarms"] : String.Empty;
            lbPVEUHI.BackColor = (alarms.IndexOf("HE=") < 0) ? this.BackColor : Color.Magenta;
            lbPVEULO.BackColor = (alarms.IndexOf("LE=") < 0) ? this.BackColor : Color.Magenta;
            lbPVHHTP.BackColor = (alarms.IndexOf("HH=") < 0) ? this.BackColor : Color.Red;
            lbPVHITP.BackColor = (alarms.IndexOf("HI=") < 0) ? this.BackColor : Color.Yellow;
            lbPVHITP.ForeColor = (alarms.IndexOf("HI=") < 0) ? Color.White : Color.Black;
            lbPVLOTP.BackColor = (alarms.IndexOf("LO=") < 0) ? this.BackColor : Color.Yellow;
            lbPVLOTP.ForeColor = (alarms.IndexOf("LO=") < 0) ? Color.White : Color.Black;
            lbPVLLTP.BackColor = (alarms.IndexOf("LL=") < 0) ? this.BackColor : Color.Red;
            lastcent = percent;
            percent = 0;
            if (Decimal.TryParse((Reals.ContainsKey("PVPercent")) ?
                (string)Reals["PVPercent"] : "0", out percent)) percent = percent / 100m;
            lbPVPercent.Text = percent.ToString("0.000 %");
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
                if (alarms.IndexOf("HE=True") < 0)
                    lbPVEUHI.BackColor = Color.Transparent;
                if (alarms.IndexOf("HH=True") < 0)
                    lbPVHHTP.BackColor = Color.Transparent;
                if (alarms.IndexOf("HI=True") < 0)
                {
                    lbPVHITP.BackColor = Color.Transparent;
                    lbPVHITP.ForeColor = Color.White;
                }
                if (alarms.IndexOf("LO=True") < 0)
                {
                    lbPVLOTP.BackColor = Color.Transparent;
                    lbPVLOTP.ForeColor = Color.White;
                }
                if (alarms.IndexOf("LL=True") < 0)
                    lbPVLLTP.BackColor = Color.Transparent;
                if (alarms.IndexOf("LE=True") < 0)
                    lbPVEULO.BackColor = Color.Transparent;
            }
            if (Reals.ContainsKey("UserLevel"))
            {
                string slevel = Reals["UserLevel"];
                if (UserInfo.GetCurrentLevel(slevel) >= UserLevel.Оператор)
                    btnQuit.Enabled = !quit;
                else
                    btnQuit.Enabled = false;
            }
            else
                btnQuit.Enabled = false;
            pbBar.Invalidate();
            blink = !blink;
        }

        private void pbBar_Paint(object sender, PaintEventArgs e)
        {
            ent.DrawAnalogBar(e.Graphics, pbBar.DisplayRectangle, lbPtName.Font.Size, percent);
        }
        private void btnQuit_Click(object sender, EventArgs e)
        {
            string ptname = lbPtName.Text;
            if (!ent.Empty)
            {
                ent.Reals = import(ptname);
                //UserLevel[] levels = (UserLevel[])UserLevel.GetValues(typeof(UserLevel));
                string username = (ent.Reals.ContainsKey("UserName")) ? ent.Reals["UserName"] : "пользователь";
                ent.QuitAlarms(username);
                ent.SetRealProp("QuitAlarms", true.ToString());
                export("QuitAlarm", ptname, ent.Reals);
            }
        }

        private void btnSize_Click(object sender, EventArgs e)
        {
            switch (this.Width)
            {
                case 164: this.Width = 610; break;
                case 610: this.Width = 874; btnSize.Text = "<"; break;
                case 874: this.Width = 164; btnSize.Text = ">"; break;
            }
        }

        private void lbFetchGroup_DoubleClick(object sender, EventArgs e)
        {
            Label lab = (Label)sender;
            string ptname = lab.Text;
            export("ShowPassport", ptname, null);
        }
    }
}
