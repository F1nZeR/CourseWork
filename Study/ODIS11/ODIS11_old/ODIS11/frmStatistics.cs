using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ODIS.AIM;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.DataVisualization.Charting.Utilities;

namespace ODIS.WinApp
{
    public partial class frmStatistics : Form
    {

        public frmStatistics(Generation generation, RandomDistribution distribution = null, int intervalCount = 0)
        {
            InitializeComponent();
            panelStatistic.SetGeneration(generation, intervalCount);
            panelStatistic.SetDistribution(distribution);
        }
    }
}
