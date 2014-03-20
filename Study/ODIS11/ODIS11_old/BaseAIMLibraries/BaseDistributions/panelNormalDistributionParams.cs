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
    public partial class panelNormalDistributionParams : panelTwoParameters, IDistributionParamsPanel
    {
        public panelNormalDistributionParams() : base("Мат. ожидание", "Дисперсия")
        {
            InitializeComponent();
            FreeParam1Minimum();
        }

        #region IDistributionParamsPanel Members

        public RandomDistribution GetDistribution()
        {
            return new NormalDistribution(GetParam1(), GetParam2());
        }

        public void SetDistribution(RandomDistribution distribution)
        {
            NormalDistribution D = distribution as NormalDistribution;
            SetParam1(D.M);
            SetParam2(D.D);
        }

        #endregion
    }
}
