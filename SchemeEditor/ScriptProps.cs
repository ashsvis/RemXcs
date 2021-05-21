using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SchemeEditor
{
    public partial class ScriptProps : Form
    {
        Draws drw;
        public ScriptProps(Draws drw, string text, string[] errors)
        {
            InitializeComponent();
            this.drw = drw;
            tbScriptText.Text = text;
            lbScriptErrors.Items.Clear();
            if (errors != null) lbScriptErrors.Items.AddRange(errors);
        }
        public string GetEditedText()
        {
            return (tbScriptText.Text);
        }

        private void btnCheckCompile_Click(object sender, EventArgs e)
        {
            drw.ScriptOnDoubleClick = tbScriptText.Text;
            lbScriptErrors.Items.Clear();
            lbScriptErrors.Items.AddRange(drw.ScriptErrors());
        }
    }
}
