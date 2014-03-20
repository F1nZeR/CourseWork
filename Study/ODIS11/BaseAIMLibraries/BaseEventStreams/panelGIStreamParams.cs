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
    public partial class panelGIStreamParams : panelDistribution, IRandomEventStreamParamsPanel
    {
        public panelGIStreamParams()
        {
        }
 
        public bool ParamsIsCorrect()
        {
            // проверить, чтобы значения были только положительными
            return true;
        }

        #region IRandomEventStreamParamsPanel Members

        public RandomEventStream GetStream()
        {
            return new GIStream(GetDistribution() as RandomDistribution);
        }

        public void SetStream(RandomEventStream stream)
        {
            SetDistribution((stream as GIStream).baseDistribution);
        }

        #endregion
    }
}

