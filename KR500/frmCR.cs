using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Points.Plugins;

namespace Points.KR500
{
    public partial class frmCR : Form, IPointConnect
    {
        bool blink = false;
        Color alarmcolor = Color.Black;
        Decimal percent, hhps, hips,lops,llps, sppercent, oppercent;
        bool hhactive, hiactive, loactive, llactive;

        int KonturMode = 0;

        Entity ent;
        ImportRealValues import;
        ExportRealValues export;
        public frmCR(Entity ent, ImportRealValues import, ExportRealValues export)
        {
            InitializeComponent();
            this.Width = 164;
            this.ent = ent;
            this.import = import;
            this.export = export;
        }
        public Entity Point { get { return this.ent; } set { this.ent = value; } }
        private void frmCR_Load(object sender, EventArgs e)
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
            lbKontur.Text = ent.Values["Kontur"].ToString();
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
            lbPVEUHI.Text = ent.Plugin.GetFineValue("PVEUHi", ent.Values["PVEUHi"], frmt);
            lbPVEULO.Text = ent.Plugin.GetFineValue("PVEULo", ent.Values["PVEULo"], frmt);
            lbSPEUHI.Text = ent.Plugin.GetFineValue("SPEUHi", ent.Values["SPEUHi"], frmt);
            lbSPEULO.Text = ent.Plugin.GetFineValue("SPEULo", ent.Values["SPEULo"], frmt);
            lbOPEUHI.Text = ent.Plugin.GetFineValue("OPEUHi", ent.Values["OPEUHi"], frmt);
            lbOPEULO.Text = ent.Plugin.GetFineValue("OPEULo", ent.Values["OPEULo"], frmt);
            lbActived.Text = ent.Plugin.GetFineValue("Actived", ent.Values["Actived"]);
            lbLogged.Text = ent.Plugin.GetFineValue("Logged", ent.Values["Logged"]);
            lbAsked.Text = ent.Plugin.GetFineValue("Asked", ent.Values["Asked"]);
            lbPVFormat.Text = ent.Plugin.GetFineValue("FormatPV", ent.Values["FormatPV"]);
            lbTrend.Text = ent.Plugin.GetFineValue("Trend", ent.Values["Trend"]);
            lbSourceK.Text = ent.Plugin.GetFineValue("K", ent.Values["K"]);
            lbSourceT1.Text = ent.Plugin.GetFineValue("T1", ent.Values["T1"]);
            lbSourceT2.Text = ent.Plugin.GetFineValue("T2", ent.Values["T2"]);
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
            string Kname = (string)ent.Values["K"];
            string T1name = (string)ent.Values["T1"];
            string T2name = (string)ent.Values["T2"];
            if (mustReload)
            { 
                FirstLoad();
                mustReload = false;
            }
            if (!backgroundFetch.IsBusy)
                backgroundFetch.RunWorkerAsync(
                    new Tuple<string, string, string, string>(
                        ptname, Kname, T1name, T2name));
        }

        private void backgroundFetch_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Tuple<string, string, string, string> args =
                (Tuple<string, string, string, string>)e.Argument;
            string ptname = args.Item1;
            string Kname = args.Item2;
            string T1name = args.Item3;
            string T2name = args.Item4;
            IDictionary<string, string> Reals = this.import(ptname);
            IDictionary<string, string> RealsK = this.import(Kname);
            IDictionary<string, string> RealsT1 = this.import(T1name);
            IDictionary<string, string> RealsT2 = this.import(T2name);
            if (RealsK.ContainsKey("PV")) Reals.Add("ValueK", RealsK["PV"]);
            if (RealsT1.ContainsKey("PV")) Reals.Add("ValueT1", RealsT1["PV"]);
            if (RealsT2.ContainsKey("PV")) Reals.Add("ValueT2", RealsT2["PV"]);
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
            lbSP.Text = (Reals.ContainsKey("SPText")) ? Reals["SPText"] : "------";
            lbOP.Text = (Reals.ContainsKey("OPText")) ? Reals["OPText"] : "------";
            lbHV.Text = (Reals.ContainsKey("HVText")) ? Reals["HVText"] : "------";
            lbDV.Text = (Reals.ContainsKey("DVText")) ? Reals["DVText"] : "------";
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
            percent = 0;
            if (Decimal.TryParse((Reals.ContainsKey("PVPercent")) ?
                (string)Reals["PVPercent"] : "0", out percent)) percent = percent / 100m;
            lbPVPercent.Text = percent.ToString("0.000 %");
            sppercent = 0;
            if (Decimal.TryParse((Reals.ContainsKey("SPPercent")) ?
                (string)Reals["SPPercent"] : "0", out sppercent)) sppercent = sppercent / 100m;
            oppercent = 0;
            if (Decimal.TryParse((Reals.ContainsKey("OPPercent")) ?
                (string)Reals["OPPercent"] : "0", out oppercent)) oppercent = oppercent / 100m;
            if (Reals.ContainsKey("KonturMode"))
            {
                int konturmode;
                if (int.TryParse(Reals["KonturMode"], out konturmode))
                {
                    KonturMode = konturmode;
                    string[] modes = new string[3] { "Ручной", "Автомат", "Каскад" };
                    Color[] colors = new Color[3] { Color.Yellow, this.BackColor, Color.Aqua };
                    Color[] forecolors = new Color[3] { Color.Black, Color.White, Color.Black };
                    lbKonturMode.Text = modes[konturmode];
                    lbKonturMode.BackColor = colors[konturmode];
                    lbKonturMode.ForeColor = forecolors[konturmode];
                    btnChangeValue.Text = (konturmode > 0) ? "Задание..." : "Выход...";
                }
            }
            if (Reals.ContainsKey("KonturErrors"))
            {
                bool konturerrors;
                if (bool.TryParse(Reals["KonturErrors"], out konturerrors))
                {
                    lbKonturErrors.Text = (konturerrors) ? "Есть" : "Нет";
                    lbKonturErrors.ForeColor = (konturerrors) ? Color.Red : Color.White;
                }
            }
            if (Reals.ContainsKey("KonturEU"))
            {
                bool kontureu;
                if (bool.TryParse(Reals["KonturEU"], out kontureu))
                {
                    lbKonturEU.Text = (kontureu) ? "Технические единицы" : "Проценты";
                }
            }

            lbValueK.Text = (Reals.ContainsKey("ValueK")) ? Reals["ValueK"] : "------";
            lbValueT1.Text = (Reals.ContainsKey("ValueT1")) ? Reals["ValueT1"] : "------";
            lbValueT2.Text = (Reals.ContainsKey("ValueT2")) ? Reals["ValueT2"] : "------";

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
                {
                    btnQuit.Enabled = !quit;
                    btnChangeValue.Enabled = btnChangeMode.Enabled = true;
                }
                else
                    btnQuit.Enabled = btnChangeValue.Enabled = btnChangeMode.Enabled = false;
            }
            else
                btnQuit.Enabled = btnChangeValue.Enabled = btnChangeMode.Enabled = false;
            pbBar.Invalidate();
            blink = !blink;
        }

        private void pbBar_Paint(object sender, PaintEventArgs e)
        {
            // отрисовка бара значения PV
            ent.DrawAnalogBar(e.Graphics, pbBar.DisplayRectangle,
                lbPtName.Font.Size, percent, sppercent, oppercent);
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            string ptname = lbPtName.Text;
            if (!ent.Empty)
            {
                ent.Reals = import(ptname);
                string username = (ent.Reals.ContainsKey("UserName")) ?
                    ent.Reals["UserName"] : "пользователь";
                ent.QuitAlarms(username);
                ent.SetRealProp("QuitAlarms", true.ToString());
                export("QuitAlarm", ptname, ent.Reals);
            }
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

        private void btnChangeMode_Click(object sender, EventArgs e)
        {
            using (frmChangeMode form = new frmChangeMode())
            {
                form.Mode = KonturMode;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Button btn = (Button)sender;
                    string ptname = lbPtName.Text;
                    if (!ent.Empty)
                    {
                        ent.Reals = import(ptname);
                        ent.SetRealProp("Command", "Mode=" + form.Mode);
                        export("Command", ptname, ent.Reals);
                    }

                }
            }
        }

        private string PercentToScale(Entity ent ,string value)
        {
            decimal val = ent.Plugin.FloatParse(value) / 100m;
            decimal hi = ent.Plugin.FloatEx((string)ent.Values["PVEUHi"]);
            decimal lo = ent.Plugin.FloatEx((string)ent.Values["PVEULo"]);
            if (Math.Abs(hi - lo) > decimal.MinValue)
            {
                val = (hi - lo) * val + lo;
                return val.ToString("0.000");
            }
            else
                return value;
        }

        private string ScaleToPercent(Entity ent, string value)
        {
            decimal val = ent.Plugin.FloatParse(value);
            decimal hi = ent.Plugin.FloatEx((string)ent.Values["PVEUHi"]);
            decimal lo = ent.Plugin.FloatEx((string)ent.Values["PVEULo"]);
            if (Math.Abs(hi - lo) > decimal.MinValue)
            {
                val = (val - lo)/(hi - lo) * 100m;
                return val.ToString();
            }
            else
                return value;
        }

        private void btnChangeValue_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string ptname = lbPtName.Text;
            if (!ent.Empty)
            {
                ent.Reals = import(ptname);
                if (ent.Reals.ContainsKey("HVRaw") &&
                    ent.Reals.ContainsKey("OPRaw"))
                {
                    using (frmChangeValue form = new frmChangeValue())
                    {
                        form.Text = "Изменить " + ((KonturMode > 0) ? "задание" : "выход");
                        form.Value = (KonturMode > 0) ?
                            PercentToScale(ent, ent.Reals["HVRaw"]) : ent.Reals["OPRaw"];
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            if (KonturMode > 0)
                            {
                                decimal value;
                                if (decimal.TryParse(form.Value, out value))
                                {
                                    decimal hi = (decimal)ent.Values["SPEUHi"];
                                    decimal lo = (decimal)ent.Values["SPEULo"];
                                    if (value >= lo && value <= hi)
                                    {
                                        ent.SetRealProp("Command", "SP=" +
                                            ScaleToPercent(ent, value.ToString("0.000")));
                                        export("Command", ptname, ent.Reals);
                                    }
                                    else
                                        MessageBox.Show(this, "Значение вне диапазона", "Изменение задания",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                    MessageBox.Show(this, "Ошибка при вводе числа", "Изменение задания",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                decimal value;
                                if (decimal.TryParse(form.Value, out value))
                                {
                                    decimal hi = (decimal)ent.Values["OPEUHi"];
                                    decimal lo = (decimal)ent.Values["OPEULo"];
                                    if (value >= lo && value <= hi)
                                    {
                                        ent.SetRealProp("Command", "OP=" + value.ToString("0.000"));
                                        export("Command", ptname, ent.Reals);
                                    }
                                    else
                                        MessageBox.Show(this, "Значение вне диапазона", "Изменение выхода",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                    MessageBox.Show(this, "Ошибка при вводе числа", "Изменение выхода",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void lbSourceK_DoubleClick(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            if (lb != null && lb.Tag != null)
            {
                string key = (string)lb.Tag;
                string ptname;
                switch (key)
                {
                    case "K":
                        ptname = lbSourceK.Text;
                        export("ShowPassport", ptname, null);
                        break;
                    case "T1":
                        ptname = lbSourceT1.Text;
                        export("ShowPassport", ptname, null);
                        break;
                    case "T2":
                        ptname = lbSourceT2.Text;
                        export("ShowPassport", ptname, null);
                        break;
                }
            }
        }
    }
}
