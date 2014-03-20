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
    public partial class panelStream : UserControl
    {
        public string Title
        {
            get { return groupBox1.Text; }
            set { groupBox1.Text = value; }
        }

        public panelStream()
        {
            InitializeComponent();
            InitControls();
        }

        public void InitControls()
        {
            try
            {
                comboStreamType.Items.AddRange(AIMCore.RandomEventStreamFactories.ToArray());
                if (comboStreamType.Items.Count > 0) comboStreamType.SelectedItem = AIMCore.RandomEventStreamFactories[0];
            }
            catch { }
        }

        private void comboStreamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParamsPanel.Controls.Clear();
            UserControl panel = (comboStreamType.SelectedItem as IRandomEventStreamFactory).GetVisualFactory().CreateControl() as UserControl;
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

        public RandomEventStream GetStream()
        {
            return (ParamsPanel.Controls[0] as IRandomEventStreamParamsPanel).GetStream();
        }

        public void SetStream(RandomEventStream stream)
        {
            foreach (IRandomEventStreamFactory sf in AIMCore.RandomEventStreamFactories)
                if (sf.IsMyStream(stream))
                {
                    comboStreamType.SelectedItem = sf;
                    (ParamsPanel.Controls[0] as IRandomEventStreamParamsPanel).SetStream(stream);
                    return;
                }
        }

        public void SetStream(string ClassName, params object[] args)
        {
            RandomEventStream stream = AIMCore.CreateEventStream(ClassName, args);
            SetStream(stream);
        }

    }

}
