using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ODIS.AIM.Queue;
using ODIS.AIM;
using ODIS.Controls;

namespace ODIS.WinApp
{
    public partial class frmOutputStatistic : Form
    {
        private QueueSimulationModel Model;

        public frmOutputStatistic(QueueSimulationModel Model)
        {
            InitializeComponent();
            this.Model = Model;
            SetDefaults();
        }

        private void SetDefaultGeneration()
        {
            panelStatistic.SetGeneration(GetDefaultStream());
        }

        private void SetDefaultDistribution()
        {
            panelStatistic.SetDistribution(Model.GetEstimateDistributionForInput()); // потом придумать, чтоб модель сама давала подходящий Distribution
        }

        private void SetDefaults()
        {
            SetDefaultGeneration();
            SetDefaultDistribution();
        }

        private void btRestore_Click(object sender, EventArgs e)
        {
            SetDefaults();
        }

        private void btBuild_Click(object sender, EventArgs e)
        {
            frmWait.ShowInfo("Обработка данных");
            double period = (double)editLength.Value;
            int IntervalCount = (int)editCount.Value;
            SimpleGeneration sourceStream = GetDefaultStream();

            // подготовить линию событий
            SimpleGeneration EventLine = new SimpleGeneration();
            double lastTime = 0;
            for (int k = 0; (k < sourceStream.Values.Count) && (!frmWait.Aborted); k++)
            {
                lastTime += sourceStream.Values[k];
                EventLine.Add(lastTime);
            }

            if (!frmWait.Aborted)
            {
                // готовим статистику и генератор для выбора интервала
                SimpleGeneration destGeneration = new SimpleGeneration();
                destGeneration.Title = String.Format("Ряд распределения числа событий в потоке за интервал времени {0}", period);
                destGeneration.IsDiscrete = true;
                RandomDistribution rnd = AIMCore.CreateDistribution("UniformDistribution", (double)0, Model.ActualTime - period);

                // генерим интервалы и вычисляем кол-во событий на них
                for (int i = 0; (i < IntervalCount) && (!frmWait.Aborted); i++)
                {
                    double x = rnd.NextValue();
                    double endT = x + period;
                    int c = EventLine.Values.Count(value => (value >= x) && (value < endT));
                    destGeneration.Add(c);
                }
                if (!frmWait.Aborted)
                {
                    panelStatistic.SetGeneration(destGeneration);
                    panelStatistic.SetDistribution(Model.GetEstimateDistributionForOutput(period));
                }
            }
            frmWait.HideInfo();
        }

        private SimpleGeneration GetDefaultStream()
        {
            return Model.ServerBlock.OutputStream;
        }
    }
}
