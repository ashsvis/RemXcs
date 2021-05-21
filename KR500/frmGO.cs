using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using Points.Plugins;

namespace Points.KR500
{
    public partial class frmGO : Form, IPointConnect
    {
        Color alarmcolor = Color.Black;
        Entity ent;
        ImportRealValues import;
        ExportRealValues export;
        public frmGO(Entity ent, ImportRealValues import, ExportRealValues export)
        {
            InitializeComponent();
            lvList.SetDoubleBuffered(true);
            this.ent = ent;
            this.import = import;
            this.export = export;
        }
        public Entity Point { get { return this.ent; } set { this.ent = value; } }
        private void frmFD_Load(object sender, EventArgs e)
        {
            FirstLoad();
            FetchPoint();
        }
        private void FirstLoad()
        {
            timerFetch.Enabled = false;
            lbPtName.Text = (string)ent.Values["PtName"];
            lbPtDesc.Text = (string)ent.Values["PtDesc"];
            lbPtType.Text = ent.Values["PtType"] + " " + ent.Values["Plugin"];
            lbChannel.Text = ent.Values["Channel"].ToString();
            lbNode.Text = ent.Values["Node"].ToString();
            lbActived.Text = ent.Plugin.GetFineValue("Actived", ent.Values["Actived"]);
            lbGroupFetchTime.Text = ent.Values["FetchTime"].ToString(); 
            for (int i = 1; i <= 14; i++)
            {
                ListViewItem item = new ListViewItem(i.ToString() + ".");
                if (ent.Values.ContainsKey("Child" + i))
                {
                    string childname = ent.Values["Child" + i].ToString().Trim();
                    item.SubItems.Add(childname);
                    IDictionary<string, string> reals = this.import(childname);
                    item.SubItems.Add(reals.ContainsKey("PtDesc") ? reals["PtDesc"] : "");
                    item.SubItems.Add(reals.ContainsKey("PVText") ? reals["PVText"] : "");
                    lvList.Items.Add(item);
                }
            }
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

        private void backgroundFetch_RunWorkerCompleted(object sender,
            System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            IDictionary<string, string> Reals = (IDictionary<string, string>)e.Result;
            if (!mustReload) mustReload = (Reals.ContainsKey("Version") &&
                                           Reals.ContainsKey("BaseVersion")) ?
                DateTime.Parse(Reals["Version"]) > DateTime.Parse(Reals["BaseVersion"]) : false;
            if (Reals.ContainsKey("GroupFetchTime"))
                lbGroupFetchTime.Text = Reals["GroupFetchTime"];
            lbGroupFactTime.Text = (Reals.ContainsKey("Seconds")) ?
                Reals["Seconds"] : "------";
            int ancnt;
            if (Reals.ContainsKey("FetchAnswerCount") &&
                int.TryParse(Reals["FetchAnswerCount"], out ancnt))
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ancnt; i++)
                {
                    string sname = "FetchAnswer" + i;
                    if (Reals.ContainsKey(sname)) sb.Append(Reals[sname]);
                }
                lbFetchAnswer.Text = sb.ToString();
            }
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
            foreach (ListViewItem item in lvList.Items)
            {
                item.UseItemStyleForSubItems = false;
                string childname = item.SubItems[1].Text.Trim();
                if (childname.Length > 0)
                {
                    IDictionary<string, string> reals = this.import(childname);
                    item.SubItems[3].Text = reals.ContainsKey("PVText") ? reals["PVText"] : "";
                    item.SubItems[3].ForeColor = Color.White;
                    item.SubItems[3].BackColor = timeout ? Color.Blue : this.BackColor;
                }
            }
        }

        private void lbFetchGroup_DoubleClick(object sender, EventArgs e)
        {
            Label lab = (Label)sender;
            string ptname = lab.Text;
            export("ShowPassport", ptname, null);
        }

        private void lvList_DoubleClick(object sender, EventArgs e)
        {
            if (lvList.FocusedItem != null)
            {
                string ptname = lvList.FocusedItem.SubItems[1].Text;
                export("ShowPassport", ptname, null);
            }
        }

    }
}
