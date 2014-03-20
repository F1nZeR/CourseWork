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
    public partial class panelExponentialDistributionParams : panelOneParameter, IDistributionParamsPanel
    {
        public panelExponentialDistributionParams() : base()
        {
            InitializeComponent();
        }

        #region IDistributionParamsPanel Members

        public RandomDistribution GetDistribution()
        {
            return new ExponentialDistribution(GetParam());
        }

        public void SetDistribution(RandomDistribution distribution)
        {
            SetParam((distribution as ExponentialDistribution).Lambda);
        }

        #endregion
    }
}
