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
    public partial class panelGammaDistributionParams : panelTwoParameters, IDistributionParamsPanel
    {
        public panelGammaDistributionParams() : base("Форма", "Масштаб")
        {
            InitializeComponent();
        }

        #region IDistributionParamsPanel Members

        public RandomDistribution GetDistribution()
        {
            return new GammaDistribution(GetParam1(), GetParam2());
        }

        public void SetDistribution(RandomDistribution distribution)
        {
            GammaDistribution D = distribution as GammaDistribution;
            SetParam1(D.Shape);
            SetParam2(D.Scale);
        }

        #endregion
    }
}
