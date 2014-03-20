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
    public partial class panelBetaDistributionParams : panelTwoParameters, IDistributionParamsPanel
    {
        public panelBetaDistributionParams(): base("Параметр 1", "Параметр 2")
        {
            InitializeComponent();
        }

        #region IDistributionParamsPanel Members

        public RandomDistribution GetDistribution()
        {
            return new BetaDistribution(GetParam1(), GetParam2());
        }

        public void SetDistribution(RandomDistribution distribution)
        {
            BetaDistribution D = distribution as BetaDistribution;
            SetParam1(D.Alpha);
            SetParam2(D.Beta);
        }

        #endregion
    }
}
