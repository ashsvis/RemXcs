using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Points.Plugins;

namespace Points.KR500
{
    public partial class frmDP : Form, IPointConnect
    {
        Color alarmcolor = Color.Black;
        Entity ent;
        ImportRealValues import;
        ExportRealValues export;
        public frmDP(Entity ent, ImportRealValues import, ExportRealValues export)
        {
            InitializeComponent();
            this.Width = 160;
            this.ent = ent;
            this.import = import;
            this.export = export;
        }
        public Entity Point { get { return this.ent; } set { this.ent = value; } }
        private void frmDI_Load(object sender, EventArgs e)
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
            lbChannel.Text = ent.Values["Channel"].ToString();
            lbNode.Text = ent.Values["Node"].ToString();
            lbAlgoblock.Text = ent.Values["Block"].ToString();
            lbPlace.Text = ent.Values["Place"].ToString();
            lbOnColor.BackColor = Color.LightGreen;
            lbOffColor.BackColor = Color.Red;
            lbActived.Text = ent.Plugin.GetFineValue("Actived", ent.Values["Actived"]);
            lbInvert.Text = ent.Plugin.GetFineValue("Invert", ent.Values["Invert"]);
            lbStrob.Text = ent.Plugin.GetFineValue("Strob", ent.Values["Strob"]);
            timerFetch.Enabled = true;
        }
        private void frmDI_KeyDown(object sender, KeyEventArgs e)
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
            lbFetchGroup.Text = (Reals.ContainsKey("FetchGroup")) ?
                Reals["FetchGroup"] : "------";
            lbGroupFetchTime.Text = (Reals.ContainsKey("GroupFetchTime")) ?
                Reals["GroupFetchTime"] : "------";
            lbGroupFactTime.Text = (Reals.ContainsKey("Seconds")) ?
                Reals["Seconds"] : "------";
            int errcode = 0;
            if (Reals.ContainsKey("RawErrorCode") &&
                int.TryParse(Reals["RawErrorCode"], out errcode) && errcode > 0)
            {
                lbFetchStatus.Text = "Ошибка опроса: " + SupportKR500.RawDataErrors(errcode);
            }
            else
                lbFetchStatus.Text = "";
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
            int rawdatatype;
            if (Reals.ContainsKey("RawDataType") &&
                int.TryParse(Reals["RawDataType"], out rawdatatype))
                lbRawDataType.Text = SupportKR500.RawDataTypes(rawdatatype);
            else
                lbRawDataType.Text = "------";
            lbRaw.Text = (Reals.ContainsKey("Raw")) ? Reals["Raw"] : "------";
            if (Reals.ContainsKey("PV"))
            {
                bool pv = bool.Parse(Reals["PV"]);
                if (pv)
                {
                    lbPVCalc.Text = "Лог. \"1\"";
                    lbOn.ForeColor = Color.White;
                    lbOff.ForeColor = Color.Silver;
                    lbOnColor.BackColor = Color.LightGreen;
                    lbOffColor.BackColor = Color.Black;
                }
                else
                {
                    lbPVCalc.Text = "Лог. \"0\"";
                    lbOn.ForeColor = Color.Silver;
                    lbOff.ForeColor = Color.White;
                    lbOnColor.BackColor = Color.Black;
                    lbOffColor.BackColor = Color.Red;
                }
            }
            else
                lbPVCalc.Text = "------";
            string alarms = (Reals.ContainsKey("Alarms")) ? Reals["Alarms"] : String.Empty;
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
            if (Reals.ContainsKey("UserLevel"))
            {
                string slevel = Reals["UserLevel"];
                if (UserInfo.GetCurrentLevel(slevel) >= UserLevel.Оператор)
                    btnOn.Enabled = btnOff.Enabled = true;
                else
                    btnOn.Enabled = btnOff.Enabled = false;
            }
            else
                btnOn.Enabled = btnOff.Enabled = false;
        }

        private void lbFetchGroup_DoubleClick(object sender, EventArgs e)
        {
            Label lab = (Label)sender;
            string ptname = lab.Text;
            export("ShowPassport", ptname, null);
        }

        private void btnSize_Click(object sender, EventArgs e)
        {
            switch (this.Width)
            {
                case 160: this.Width = 492; break;
                case 492: this.Width = 736; btnSize.Text = "<"; break;
                case 736: this.Width = 160; btnSize.Text = ">"; break;
            }
        }

        private void btnOn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string ptname = lbPtName.Text;
            if (!ent.Empty)
            {
                ent.Reals = import(ptname);
                ent.SetRealProp("Command", btn.Tag.ToString());
                export("Command", ptname, ent.Reals);
            }
        }

    }
}
