using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ODIS.AIM;
using ODIS.AIM.Queue;

namespace ODIS.WinApp
{
    public partial class PanelServerBlock : UserControl
    {
        public PanelServerBlock()
        {
            InitializeComponent();
        }

        private void checkInfCount_CheckedChanged(object sender, EventArgs e)
        {
            editCount.Enabled = !checkInfCount.Checked;
        }

        internal void InitControls()
        {
            comboBufferType.SelectedIndex = 0;
            panelDistribution.SetDistribution("ExponentialDistribution", 2d);
        }

        public bool ConfigureBlock(ServerBlock serverBlock)
        {
            serverBlock.Distribution = panelDistribution.GetDistribution();
            bool ok = serverBlock.Distribution != null;
            serverBlock.ServersCount = checkInfCount.Checked ? -1 : (int)editCount.Value;
            switch (comboBufferType.SelectedIndex)
            {
                case (int)BufferType.Queue:
                    // по умолчанию - буфер изначально очередь
                    (serverBlock.Buffer as PassiveBuffer).MaxLength = (panelBuffer.Controls[0] as panelQueueParams).GetQueueLength();
                    break;
                case (int)BufferType.Orbit:
                    AIM.Queue.ActiveBuffer orbit = new ActiveBuffer(serverBlock);
                    orbit.Distribution = (panelBuffer.Controls[0] as panelOrbit).GetDistribution();
                    ok = ok && (orbit.Distribution != null);
                    serverBlock.Buffer = orbit;
                    break;
            }
            return ok;
        }

        private void comboBufferType_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBuffer.Controls.Clear();
            UserControl panel = null;
            switch (comboBufferType.SelectedIndex)
            {
                case (int)BufferType.Queue:
                    panel = new panelQueueParams();
                    break;
                case (int)BufferType.Orbit:
                    panel = new panelOrbit();
                    break;
            }
            if (panel != null)
            {
                panelBuffer.Controls.Add(panel);
                panel.Dock = DockStyle.Fill;
            }
        }
    }

    public enum BufferType
    {
        None = 0,
        Queue = 1,
        Orbit = 2
    }
}
