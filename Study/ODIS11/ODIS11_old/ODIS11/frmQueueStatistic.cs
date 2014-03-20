using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ODIS.AIM.Queue;
using System.Windows.Forms.DataVisualization.Charting;
using ODIS.AIM;

namespace ODIS.WinApp
{
    public partial class frmQueueStatistic : Form
    {
        QueueSimulationModel Model;

        public frmQueueStatistic(QueueSimulationModel model)
        {
            InitializeComponent();
            this.Model = model;
            panelStatistic.SetGeneration(Model.ServerBlock.InSystemStatistic);
            panelStatistic.SetDistribution(Model.GetEstimateDistributionForQueue());
            btnBuffer.Enabled = Model.ServerBlock.Buffer.BasicStatistic.Count() > 0;
        }

        public static void ShowStatistic(QueueSimulationModel model)
        {
            frmQueueStatistic frm = new frmQueueStatistic(model);
            frm.Show();
        }

        private void btSaveSource_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK) Model.Source.BasicStatistic.SaveInText(saveFileDialog.FileName, SaveOptions.SimpleValues);                
        }

        private void btSaveProcessed_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK) Model.ServerBlock.OutputStream.SaveInText(saveFileDialog.FileName);
        }

        private void btSaveRejected_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK) Model.ServerBlock.Buffer.RejectionStatistic.SaveInText(saveFileDialog.FileName, SaveOptions.SimpleValues);                
        }

        private void btSaveQueueLength_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK) Model.ServerBlock.Buffer.BasicStatistic.SaveInText(saveFileDialog.FileName);
        }

        private void btSaveOutput_Click(object sender, EventArgs e)
        {
            // Анализ выходящего потока
            frmOutputStatistic StatisticForm = new frmOutputStatistic(Model);
            StatisticForm.Show();
        }

        private void btnSaveInSystem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK) Model.ServerBlock.InSystemStatistic.SaveInText(saveFileDialog.FileName);
        }

        private void btnBuffer_Click(object sender, EventArgs e)
        {
            frmStatistics frm = new frmStatistics(Model.ServerBlock.Buffer.BasicStatistic, Model.ServerBlock.Buffer.GetEstimatedistribution());
            frm.Show();
        }

    }

}
