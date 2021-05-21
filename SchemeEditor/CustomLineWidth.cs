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
    public partial class CustomLineWidth : Form
    {
        public CustomLineWidth()
        {
            InitializeComponent();
        }
        public decimal LineWidth
        {
            get
            {
                return (lineWidth.Value);
            }
            set
            {
                lineWidth.Value = value;
            }
        }
    }
}
