using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SchemeEditor
{
    public partial class EditText : Form
    {
        public EditText(string caption)
        {
            InitializeComponent();
            tbText.Text = caption;
        }

        public string Caption { get { return tbText.Text; } }
    }
}
