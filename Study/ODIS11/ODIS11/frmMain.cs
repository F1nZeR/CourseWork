using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ODIS.AIM;
using ODIS.AIM.Queue;
using ODIS.AMM;

namespace ODIS.WinApp
{
    public partial class frmMain : Form, ICycleMonitor
    {
        public frmMain()
        {
            AIMCore.Load(Application.StartupPath);
            InitializeComponent();
            sourcePanel.SetStream("PoissonStream", 1d);
            serverBlockPanel.InitControls();
            labNodeCount.Text = "5"; //потом = 1
            v = Matrix.Get0Matrix(1, 5); v[1, 1] = 0.1; v[1, 2] = 0.5; v[1, 3] = 0.4;
            r = Matrix.Get0Matrix(5, 5); r[1, 2] = 0.3; r[1, 4] = 0.2;
            r[2, 1] = 0.2; r[2, 3] = 0.3; r[2, 5] = 0.3;
            r[3, 3] = 0.2; r[3, 5] = 0.5;
            r[4, 1] = 0.1; r[4, 3] = 0.6;
            r[5, 2] = 0.2; r[5, 3] = 0.1; r[5, 4] = 0.1; r[5, 5] = 0.3;
            meInputDivisionVector.SetMatrix(v);
            meRoutingMatrix.SetMatrix(r);
        }

        private void checkStopEventCount_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == checkStopEventCount) checkStopTime.Checked = !checkStopEventCount.Checked;
            else checkStopEventCount.Checked = !checkStopTime.Checked;
            editSMOCount.Enabled = labelStopEventCount.Enabled = checkStopEventCount.Checked;
            editStopTime.Enabled = labelStopTime.Enabled = checkStopTime.Checked;
        }

        Matrix v, r;
        private void btRunNetwork_Click(object sender, EventArgs e)
        {
            List<RandomDistribution> serv = new List<RandomDistribution>();
            serv.Add(AIMCore.CreateDistribution("GammaDistribution", 1d, 2d));
            serv.Add(AIMCore.CreateDistribution("GammaDistribution", 0.5d, 2d));
            serv.Add(AIMCore.CreateDistribution("GammaDistribution", 0.125d, 4d));
            serv.Add(AIMCore.CreateDistribution("GammaDistribution", 1d, 2d));
            serv.Add(AIMCore.CreateDistribution("GammaDistribution", 0.25d, 2d));

            QueueNetworkSimulationModel model = new QueueNetworkSimulationModel(v, r, serv);
            for (int i = 1; i <= model.Nodes.Count; i++) model.Nodes[i - 1].Text = "Узел " + i.ToString();
            model.Source.InputStream = AIMCore.CreateEventStream("GIStream", "GammaDistribution", 0.25d, 0.04d);

            if (checkStopEventCount.Checked)
            {
                model.StopConditionType = 0;
                model.StopConditionValue = (int)editSMOCount.Value;
            }
            else
            {
                model.StopConditionType = 1;
                model.StopConditionValue = (int)editStopTime.Value;
            } 
            
            frmWait.ShowInfo("Моделирование событий");
            model.Run(this);
            if (!IsAborted())
            {
                frmWait.ShowInfo("Статистическая обработка");
                foreach (ServerBlock server in model.Nodes)
                    frmQueueStatistic.ShowStatistic(model, server);
            }
            frmWait.HideInfo();
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            QueueSimulationModel model = new QueueSimulationModel();
            bool ok = sourcePanel.ConfigureSource(model.Source) && serverBlockPanel.ConfigureBlock(model.ServerBlock);

            if (ok)
            {
                if (checkStopEventCount.Checked)
                {
                    model.StopConditionType = 0;
                    model.StopConditionValue = (int)editSMOCount.Value;
                }
                else
                {
                    model.StopConditionType = 1;
                    model.StopConditionValue = (int)editStopTime.Value;
                }

                frmWait.ShowInfo("Моделирование событий");
                model.Run(this);
                if (!IsAborted())
                {
                    frmWait.ShowInfo("Статистическая обработка");
                    frmQueueStatistic.ShowStatistic(model, model.ServerBlock);
                }
                frmWait.HideInfo();
            }
        }

        private void btRandomGenerate_Click(object sender, EventArgs e)
        {
            SimpleGeneration result = new SimpleGeneration();
            int count = (int)(editGenerationCount.Value);
            RandomDistribution distribution = panelDistribution.GetDistribution();
            if (distribution != null)
            {
                frmWait.ShowInfo("Моделирование");
                for (int i = 0; (i < count) && (!IsAborted()); i++)
                {
                    result.Add(distribution.NextValue());
                }
                if (!IsAborted())
                {
                    result.Title = "Выборка " + DateTime.Now.ToString();
                    result.IsDiscrete = (distribution is DiscreteDistribution);
                    frmStatistics StatisticForm = new frmStatistics(result, distribution);
                    StatisticForm.Show();
                }
                frmWait.HideInfo();
            }
        }

        private void btPreferences_Click(object sender, EventArgs e)
        {
            frmPreferences.Execute();
        }

        private void btRunProcesses_Click(object sender, EventArgs e)
        {
            RandomProcess process = panelProcess.GetProcess();
            if (process != null)
            {
                frmWait.ShowInfo("Моделирование");
                process.Initiate();
                TimeStatistic statistic = new TimeStatistic();
                double endTime = (double)editProcessesTime.Value;
                while ((process.Time < endTime) && (!IsAborted()))
                {
                    double state = process.State;
                    process.Next();
                    statistic.Add(process.Time, state);
                }
                if (!IsAborted())
                {
                    frmStatistics StatisticForm = new frmStatistics(statistic, process.GetEstimateDistribution());
                    StatisticForm.Show();
                }
            }
            frmWait.HideInfo();
        }

        private void btRunStreams_Click(object sender, EventArgs e)
        {
            RandomEventStream stream = panelStream.GetStream();
            if (stream != null)
            {
                frmWait.ShowInfo("Моделирование");
                SimpleGeneration statistic = new SimpleGeneration(); // или TimeStatistic?
                double endTime = (double)editStreamsTime.Value;
                double last_time = 0;
                double time = 0;
                while ((time <= endTime) && (!IsAborted()))
                {
                    last_time = time;
                    time = stream.NextValue();
                    statistic.Add(time - last_time);
                }
                if (!IsAborted())
                {
                    frmStatistics StatisticForm = new frmStatistics(statistic, stream.GetEstimateDistribution()); // распределение неизвестно (точнее - почти невозможно записать аналитически)
                    StatisticForm.Show();
                }
            }
            frmWait.HideInfo();
        }

        #region ICycleMonitor Members

        public bool IsAborted()
        {
            return frmWait.Aborted;
        }

        #endregion

    }
}
