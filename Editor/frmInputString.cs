using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataEditor
{
    public partial class frmInputString : Form
    {
        public frmInputString()
        {
            InitializeComponent();
        }

        public void DoUpper()
        {
            tbText.CharacterCasing = CharacterCasing.Upper;
        }

        public string Value
        {
            get { return tbText.Text; }
            set { tbText.Text = value; }
        }
    }
}
