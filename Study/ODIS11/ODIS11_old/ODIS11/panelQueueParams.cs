using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ODIS.WinApp
{
    public partial class panelQueueParams : UserControl
    {
        public panelQueueParams()
        {
            InitializeComponent();
        }

        private void checkInfQueueLength_CheckedChanged(object sender, EventArgs e)
        {
            editQueueLength.Enabled = !checkInfQueueLength.Checked;
        }

        public int GetQueueLength()
        {
            return checkInfQueueLength.Checked ? -1 : (int)editQueueLength.Value;
        }
    }
}
