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
    public partial class frmInputNumber : Form
    {
        public frmInputNumber()
        {
            InitializeComponent();
        }

        public void SetIntValue(int value, int low, int high)
        {
            nuValue.Minimum = low;
            nuValue.Maximum = high;
            if (value < low) nuValue.Value = low;
            else if (value > high) nuValue.Value = high;
            else nuValue.Value = value;
            nuValue.Select(0, nuValue.Value.ToString().Length);
        }

        public void SetNumberValue(decimal value, decimal low, decimal high, int dec)
        {
            nuValue.Minimum = low;
            nuValue.Maximum = high;
            nuValue.DecimalPlaces = dec;
            if (value < low) nuValue.Value = low;
            else if (value > high) nuValue.Value = high;
            else nuValue.Value = value;
            nuValue.Select(0, nuValue.Value.ToString().Length + 7);
        }

        public int IntValue
        {
            get { return (int)nuValue.Value; }
        }

        public decimal NumberValue
        {
            get { return (decimal)nuValue.Value; }
        }
    }

}
