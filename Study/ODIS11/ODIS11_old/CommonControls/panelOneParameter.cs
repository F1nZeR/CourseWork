using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ODIS.Controls
{
    public partial class panelOneParameter : UserControl
    {
        public panelOneParameter(string text = "Параметр", double value = 1)
        {
            InitializeComponent();
            labText.Text = text;
            SetParam(value);
        }

        public double GetParam()
        {
            return (double)editParameter.Value;
        }

        public void SetParam(double value)
        {
            editParameter.Value = (decimal)value;
        }

        public void SetMaximum(int max)
        {
            editParameter.Maximum = max;
        }

    }
}
