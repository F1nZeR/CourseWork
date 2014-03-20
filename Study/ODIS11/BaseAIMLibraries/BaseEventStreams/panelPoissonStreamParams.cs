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
    public partial class panelPoissonStreamParams : panelOneParameter, IRandomEventStreamParamsPanel
    {
        public panelPoissonStreamParams()
        {
            //InitializeComponent();
        }

        #region IRandomEventStreamParamsPanel Members

        public RandomEventStream GetStream()
        {
            return new PoissonStream(GetParam());
        }

        public void SetStream(RandomEventStream stream)
        {
            SetParam((stream as PoissonStream).Lambda);
        }

        #endregion
    }
}
