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
    public partial class panelTwoParameters : UserControl
    {
        public panelTwoParameters(string param1Text, string param2Text)
        {
            InitializeComponent();
            labParam1Text.Text = param1Text;
            labParam2Text.Text = param2Text;
        }

        public double GetParam1()
        {
            return (double)editParam1.Value;
        }

        public double GetParam2()
        {
            return (double)editParam2.Value;
        }

        public void SetParam1(double value)
        {
            editParam1.Value = (decimal)value;
        }

        public void SetParam2(double value)
        {
            editParam2.Value = (decimal)value;
        }

        public void FreeParam1Minimum()
        {
            editParam1.Minimum = -editParam1.Maximum;
            editParam1.Value = 0;
        }
    }
}
