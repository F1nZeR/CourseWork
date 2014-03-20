using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ODIS.AIM;
using ODIS.AMM;

namespace ODIS.Controls
{
    public partial class panelProcess : UserControl
    {
        public string Title
        {
            get { return groupBox1.Text; }
            set { groupBox1.Text = value; }
        }

        public panelProcess()
        {
            InitializeComponent();
            InitControls();
        }

        private void comboProcessType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParamsPanel.Controls.Clear();
            UserControl panel = (comboProcessType.SelectedItem as IRandomProcessFactory).GetVisualFactory().CreateControl() as UserControl;
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

        public Control ProcessParamsPanel
        {
            get { return ParamsPanel.Controls[0]; }
        }

        public void AddParamsChangedEventHandler(EventHandler eventHandler)
        {
            (ProcessParamsPanel as IRandomProcessParamsPanel).AddParamsChangedEventHandler(eventHandler);
        }

        public void InitControls()
        {
            try
            {
                comboProcessType.Items.AddRange(AIMCore.RandomProcessFactories.ToArray());
                if (comboProcessType.Items.Count > 0) comboProcessType.SelectedItem = AIMCore.RandomProcessFactories[0];
            }
            catch { }
        }

        public bool ParamsIsCorrect()
        {
            return (ProcessParamsPanel as IRandomProcessParamsPanel).ParamsIsCorrect();
        }

        public RandomProcess GetProcess()
        {
            return (ProcessParamsPanel as IRandomProcessParamsPanel).GetProcess();
        }

        public void SetProcess(RandomProcess process)
        {
            foreach (IRandomProcessFactory pf in AIMCore.RandomProcessFactories)
                if (pf.IsMyProcess(process))
                {
                    comboProcessType.SelectedItem = pf;
                    (ProcessParamsPanel as IRandomProcessParamsPanel).SetProcess(process);
                    return;
                }
        }
    }

}
