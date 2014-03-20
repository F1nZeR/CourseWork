using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ODIS.AIM;

namespace ODIS.Controls
{
    public partial class panelUniformDistributionParams : UserControl, IDistributionParamsPanel
    {
        public panelUniformDistributionParams(double min = 0, double max = 1)
        {
            InitializeComponent();
            editMin.Value = (decimal)min;
            editMax.Value = (decimal)max;
        }

        public void SetMinMinimum(double value)
        {
            editMin.Minimum = (decimal)value;
        }

        private void editMin_ValueChanged(object sender, EventArgs e)
        {
            editMax.Minimum = editMin.Value;
        }

        private void editMax_ValueChanged(object sender, EventArgs e)
        {
            editMin.Maximum = editMax.Value;
        }

        public double GetMin()
        {
            return (double)editMin.Value;
        }

        public double GetMax()
        {
            return (double)editMax.Value;
        }

        public void SetMax(double value)
        {
            editMax.Value = (decimal)value;
        }

        public void SetMin(double value)
        {
            editMin.Value = (decimal)value;
        }

        #region IDistributionParamsPanel Members

        public RandomDistribution GetDistribution()
        {
            return new UniformDistribution((double)editMin.Value, (double)editMax.Value);
        }

        public void SetDistribution(RandomDistribution distribution)
        {
            editMin.Value = (decimal)(distribution as UniformDistribution).Min;
            editMax.Value = (decimal)(distribution as UniformDistribution).Max;
        }

        #endregion
    }
}
