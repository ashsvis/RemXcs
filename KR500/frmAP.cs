using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Points.Plugins;

namespace Points.KR500
{
    public partial class frmAP : Form, IPointConnect
    {
        Color alarmcolor = Color.Black;
        Decimal lastcent = 0;
        Decimal percent;
        Entity ent;
        ImportRealValues import;
        ExportRealValues export;
        public frmAP(Entity ent, ImportRealValues import, ExportRealValues export)
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
            lbAlgoblock.Text = ent.Values["Block"].ToString();
            lbPlace.Text = ent.Values["Place"].ToString();
            int frmt = (int)ent.Values["FormatPV"];
            lbPVEUHI.Text = ent.Plugin.GetFineValue("OPEUHi", ent.Values["OPEUHi"], frmt);
            lbPVEULO.Text = ent.Plugin.GetFineValue("OPEULo", ent.Values["OPEULo"], frmt);
            lbKoeff.Text = ent.Plugin.GetFineValue("Koeff", ent.Values["Koeff"], frmt);
            lbOffset.Text = ent.Plugin.GetFineValue("Offset", ent.Values["Offset"], frmt);
            lbActived.Text = ent.Plugin.GetFineValue("Actived", ent.Values["Actived"]);
            lbPVFormat.Text = ent.Plugin.GetFineValue("FormatPV", ent.Values["FormatPV"]);
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
            int errcode = 0;
            if (Reals.ContainsKey("RawErrorCode") &&
                int.TryParse(Reals["RawErrorCode"], out errcode) && errcode > 0)
            {
                lbFetchStatus.Text = "Ошибка опроса: " + SupportKR500.RawDataErrors(errcode);
            }
            else
                if (Reals.ContainsKey("CommErrorCode") &&
                    int.TryParse(Reals["CommErrorCode"], out errcode) && errcode > 0)
                {
                    lbFetchStatus.Text = "Ошибка команды: " + SupportKR500.RawDataErrors(errcode);
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
            lbRaw.Text = (Reals.ContainsKey("Raw")) ? Reals["Raw"] : "------";
            int rawdatatype;
            if (Reals.ContainsKey("RawDataType") &&
                int.TryParse(Reals["RawDataType"], out rawdatatype))
                lbRawDataType.Text = SupportKR500.RawDataTypes(rawdatatype);
            else
                lbRawDataType.Text = "------";
            lbPVCalc.Text = (Reals.ContainsKey("PV")) ? Reals["PV"] : "------";
            string alarms = (Reals.ContainsKey("Alarms")) ? Reals["Alarms"] : String.Empty;
            lbPVEUHI.BackColor = (alarms.IndexOf("HE=") < 0) ? this.BackColor : Color.Magenta;
            lbPVEULO.BackColor = (alarms.IndexOf("LE=") < 0) ? this.BackColor : Color.Magenta;
            lastcent = percent;
            percent = 0;
            if (Decimal.TryParse((Reals.ContainsKey("PVPercent")) ?
                (string)Reals["PVPercent"] : "0", out percent)) percent = percent / 100m;
            lbPVPercent.Text = percent.ToString("0.000 %");
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
                {
                    btnChangeValue.Enabled = true;
                }
                else
                    btnChangeValue.Enabled = false;
            }
            else
                btnChangeValue.Enabled = false;
            pbBar.Invalidate();
        }

        private void pbBar_Paint(object sender, PaintEventArgs e)
        {
            ent.DrawAnalogBar(e.Graphics, pbBar.DisplayRectangle, lbPtName.Font.Size, percent);
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
                case 164: this.Width = 610; break;
                case 610: this.Width = 874; btnSize.Text = "<"; break;
                case 874: this.Width = 164; btnSize.Text = ">"; break;
            }
        }

        private void btnChangeValue_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string ptname = lbPtName.Text;
            if (!ent.Empty)
            {
                ent.Reals = import(ptname);
                using (frmChangeValue form = new frmChangeValue())
                {
                    form.Text = "Изменить значение";
                    form.Value = ent.Reals["PV"];
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        decimal hi = (decimal)ent.Values["OPEUHi"];
                        decimal lo = (decimal)ent.Values["OPEULo"];
                        decimal kf = (decimal)ent.Values["Koeff"];
                        decimal ofs = (decimal)ent.Values["Offset"];
                        decimal value;
                        if (Math.Abs(hi - lo) > decimal.MinValue &&
                            Math.Abs(kf) > decimal.MinValue)
                        {
                            if (decimal.TryParse(form.Value, out value))
                            {
                                if (value >= lo && value <= hi)
                                {
                                    value = (value - lo) / (hi - lo) * 100m;
                                    value = value / kf - ofs;
                                    ent.SetRealProp("Command", value.ToString("0.000"));
                                    export("Command", ptname, ent.Reals);
                                }
                                else
                                    MessageBox.Show(this, "Значение вне диапазона", "Изменение значения",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                                MessageBox.Show(this, "Ошибка при вводе числа", "Изменение значения",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                }
            }
        }

    }
}
