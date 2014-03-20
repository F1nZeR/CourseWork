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
    public partial class panelPoissonDistributionParams : panelOneParameter, IDistributionParamsPanel
    {
        public panelPoissonDistributionParams() : base()
        {
            InitializeComponent();
            SetMaximum(700); // при больших значениях не работает генератор из-за e^(-p), которое становится = 0
        }

        #region IDistributionParamsPanel Members

        public RandomDistribution GetDistribution()
        {
            return new PoissonDistribution(GetParam());
        }

        public void SetDistribution(RandomDistribution distribution)
        {
            SetParam((distribution as PoissonDistribution).Parameter);
        }

        #endregion
    }
}
