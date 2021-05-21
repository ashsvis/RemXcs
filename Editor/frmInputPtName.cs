using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Points.Plugins;
using System.Text.RegularExpressions;

namespace DataEditor
{
    public partial class frmInputPtName : Form
    {
        private int selected = -1;
        private static IDictionary<int, Color> colorlist = new Dictionary<int, Color>();
        private static IDictionary<int, string> keylist = new Dictionary<int, string>();
        private static IDictionary<int, IPointPlugin> pluglist = new Dictionary<int, IPointPlugin>();

        public frmInputPtName()
        {
            InitializeComponent();
            int i = 0;
            colorlist.Clear(); keylist.Clear(); pluglist.Clear();
            IDictionary<string, IPointPlugin> plugins =
                PointPlugin.LoadPlugins(Application.StartupPath);
            foreach (KeyValuePair<string, IPointPlugin> kvp in plugins)
            {
                colorlist.Add(i, kvp.Value.GetIconColor);
                keylist.Add(i, kvp.Value.PluginShortType);
                pluglist.Add(i, kvp.Value);
                i++;
            }
        }

        public string EnteredValue
        { 
            get { return tbPtName.Text.Trim(); }
            set { tbPtName.Text = value; }
        }

        public IPointPlugin EnteredType
        {
            set
            {
                foreach (KeyValuePair<int, IPointPlugin> kvp in pluglist)
                {
                    if (kvp.Value.ToString().Equals(value.ToString()))
                    {
                        selected = kvp.Key;
                        break;
                    }
                }
            }
        }

        private void pbEntity_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            RectangleF rect = pbEntity.ClientRectangle;
            if (selected >= 0)
            {
                int i = selected;
                Color color = colorlist[i];
                using (SolidBrush brush = new SolidBrush(color))
                {
                    g.FillRectangle(brush, rect);
                    using (StringFormat format = new StringFormat())
                    {
                        format.Alignment = StringAlignment.Center;
                        format.LineAlignment = StringAlignment.Center;
                        g.DrawString(keylist[i], this.Font, SystemBrushes.MenuText, rect, format);
                    }
                }
            }
            else
                g.FillRectangle(SystemBrushes.ButtonFace, rect);
        }

        private bool checkPtName()
        {
            return (selected >= 0) && Regex.IsMatch(tbPtName.Text, @"^([A-Z]|_)([0-9A-Z]|_|^\s)*$");
        }

        private void tbPtName_TextChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = checkPtName();
        }
    }
}
