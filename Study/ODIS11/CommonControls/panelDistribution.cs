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
    public partial class panelDistribution : UserControl
    {
        public panelDistribution()
        {
            InitializeComponent();
            InitControls();
        }

        private void comboDistributionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParamsPanel.Controls.Clear();
            UserControl panel = (comboDistributionType.SelectedItem as IDistributionFactory).GetVisualFactory().CreateControl() as UserControl;
            AddParamsPanel(panel);
        }

        private void AddParamsPanel(UserControl panel)
        {
            if (panel != null)
            {
                ParamsPanel.Controls.Add(panel);
                panel.Dock = DockStyle.Fill;
            }
        }

        public void InitControls()
        {
            try
            {
                comboDistributionType.Items.AddRange(AIMCore.DistributionFactries.ToArray());
                if (comboDistributionType.Items.Count > 0) comboDistributionType.SelectedItem = AIMCore.DistributionFactries[0];
            }
            catch { }
        }

        public RandomDistribution GetDistribution()
        {
            return (ParamsPanel.Controls[0] as IDistributionParamsPanel).GetDistribution();
        }

        public void SetDistribution(RandomDistribution distribution)
        {
            foreach (IDistributionFactory df in AIMCore.DistributionFactries)
                if (df.IsMyDistribution(distribution))
                {
                    comboDistributionType.SelectedItem = df;
                    (ParamsPanel.Controls[0] as IDistributionParamsPanel).SetDistribution(distribution);
                    return;
                }
        }

        public void SetDistribution(string ClassName, params object[] args)
        {
            RandomDistribution distribution = AIMCore.CreateDistribution(ClassName, args);
            SetDistribution(distribution);
        }
    }

}
