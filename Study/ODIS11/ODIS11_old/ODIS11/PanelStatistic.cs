using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ODIS.AIM;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace ODIS.WinApp
{
    public partial class PanelStatistic : UserControl, ICycleMonitor
    {
        const int LineWidth = 2;
        private Generation generation = null;

        public PanelStatistic()
        {
            InitializeComponent();
        }

        public PanelStatistic(Generation generation = null, RandomDistribution distribution = null, int intervalCount = 0)
        {
            InitializeComponent();
            SetGeneration(generation, intervalCount);
            SetDistribution(distribution);
        }

        public void SetDistribution(RandomDistribution distribution)
        {
            if (distribution != null) panelDistribution.SetDistribution(distribution);
            ResetCrit();
        }

        private void ResetCrit()
        {
            btCalcCrit.Visible = true;
            labCrit.Visible = false;
        }

        public void SetGeneration(Generation generation, int intervalCount = 0, bool isDiscrete = false)
        {
            this.generation = generation;
            InitControls(intervalCount);
        }

        private void InitControls(int intervalCount = 0)
        {
            if (this.generation != null)
            {
                InitGraph(intervalCount);
                labStat.Text = generation.GetDetailInfo();
                labStat.Visible = labStat.Text.Length > 0;
                if (groupBox1.Width < labStat.Width + 10)
                    groupBox1.Width = labStat.Width + 10;
            }
        }

        public Histogram histogram = null;
        private void InitGraph(int intervalCount = 0)
        {
            chart.Series.Clear();
            Series S = chart.Series.Add(generation.ToString());
            histogram = new Histogram(generation, intervalCount);

            if (generation.IsDiscrete)
            {
                foreach (SimpleValueHistogramRow row in histogram)
                    if (row.Freq > 0)
                        S.Points.AddXY(row.Value, row.Freq);
            }
            else if (generation is SimpleGeneration)
            {
                foreach (IntervalHistogramRow row in histogram)
                    S.Points.AddXY(row.Mid, row.Freq);
            }
            SetSeriesType(S, generation.IsDiscrete);
        }

        private void SetSeriesType(Series S, bool isFastLine)
        {
            ChartArea Area = chart.ChartAreas[S.ChartArea];
            if (isFastLine)
            {
                S.ChartType = SeriesChartType.Line;
                S.BorderWidth = LineWidth;
                try
                {
                    Area.AxisX.Minimum = (int)Math.Round(S.Points.Min(x => x.XValue));
                    Area.AxisX.Interval = 0;
                }
                catch { }
                /* это надо было, когда состояния изображались столбцами
                Area.AxisX.Minimum = -0.5;
                Area.AxisX.IntervalOffset = 0.5;
                Area.AxisY.Maximum *= 0.1;*/
            }
            else
            {
                S.ChartType = SeriesChartType.Column;
                S.BackGradientStyle = GradientStyle.DiagonalLeft;
                S["PointWidth"] = "1";
                S.BorderColor = Color.Black;
                S.Color = Color.Blue;
                Area.AxisX.Minimum = generation.Min();
                Area.AxisX.Interval = histogram.Step;
                Area.AxisX.LabelStyle.Format = "f2";
            }
            S.LabelFormat = "G5";
            S.Font = new System.Drawing.Font(S.Font.FontFamily, 9);//, FontStyle.Bold);
            S.LabelBackColor = Color.LightYellow;
            S.LabelForeColor = Color.Black;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK) generation.SaveInText(saveFileDialog.FileName);
        }

        private void btCalcCrit_Click(object sender, EventArgs e)
        {
            btCalcCrit.Visible = false;
            frmWait.ShowInfo("Производится расчет критериев");
            if (histogram != null)
                labCrit.Text = histogram.GetCriteriasCheckingText(panelDistribution.GetDistribution(), this);
            labCrit.Visible = true;
            frmWait.HideInfo();
        }

        private void btShowDistribution_Click(object sender, EventArgs e)
        {
            RandomDistribution distribution = panelDistribution.GetDistribution();
            if (distribution != null)
            {
                frmWait.ShowInfo("Построение");
                Series S;
                if (chart.Series.Count > 1)
                {
                    S = chart.Series[1];
                    S.Points.Clear();
                }
                else
                {
                    S = chart.Series.Add(distribution.ToString());
                    S.ChartType = SeriesChartType.FastLine;
                    S.Color = Color.Red;
                    S.BorderWidth = LineWidth;
                }
                ChartArea Area = chart.ChartAreas[0];
                double lastF = distribution.F(Area.AxisX.Minimum);
                double interval = 1; //шаг точек
                double delta = 0; // сдвиг выводимой точки влево
                double offset = 0; // сдвиг начальной точки
                double max = generation.Max();
                if (!generation.IsDiscrete)
                {
                    interval = Area.AxisX.Interval;
                    delta = interval / 2;
                    offset = interval;
                    max += interval;
                }
                for (double x = Area.AxisX.Minimum + offset; x <= max; x += interval)
                {
                    if (distribution is DiscreteDistribution) S.Points.AddXY(x - delta, (distribution as DiscreteDistribution).P(x));
                    else
                    {
                        double newF = distribution.F(x);
                        S.Points.AddXY(x - delta, newF - lastF); // выводим не плотность а ее интеграл на отрезке
                        lastF = newF;
                    }
                }
                frmWait.HideInfo();
                ResetCrit();
            }
        }

        private void btnSaveStateProbs_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Series S = chart.Series[0];
                StreamWriter sw = File.CreateText(saveFileDialog.FileName);
                foreach (DataPoint point in S.Points)
                    sw.WriteLine("{0} {1}", point.XValue, point.YValues[0]);
                sw.Close();
                sw.Dispose();
            }
        }

        private void btnShowLabels_Click(object sender, EventArgs e)
        {
            chart.Series[0].IsValueShownAsLabel = btnShowLabels.Checked;
        }

        private void btnSaveStateImage_Click(object sender, EventArgs e)
        {
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
            {
                ChartImageFormat format = ChartImageFormat.Jpeg;
                string filename = saveImageDialog.FileName;
                string ext = filename.Substring(filename.Length - 3).ToUpper();
                if (ext == "BMP") format = ChartImageFormat.Bmp;
                else if (ext == "GIF") format = ChartImageFormat.Gif;
                else if (ext == "EMF") format = ChartImageFormat.Emf;
                chart.SaveImage(filename, format);
            }
        }


        public bool IsAborted()
        {
            return frmWait.Aborted;
        }
    }



}
