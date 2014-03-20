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
    public partial class panelDeterminateDistributionParams : panelOneParameter, IDistributionParamsPanel
    {
        public panelDeterminateDistributionParams() : base("Значение")
        {
            InitializeComponent();
        }

        #region IDistributionParamsPanel Members

        public RandomDistribution GetDistribution()
        {
            return new DeterminateDistribution(GetParam());
        }

        public void SetDistribution(RandomDistribution distribution)
        {
            SetParam((distribution as DeterminateDistribution).Value);
        }

        #endregion
    }
}
