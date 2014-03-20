using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ODIS.AMM;

namespace ODIS.DataAnalyzer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        const string path = @"data\";
        const string ext = ".txt";

        bool Aborted = false;

        DataLine[] DataLines = null;
        List<NetworkDataRecord> States = new List<NetworkDataRecord>();
        private void btStart_Click(object sender, EventArgs e)
        {
            Aborted = false;
            panelParams.Enabled = false;
            panelProgress.Visible = true;
            DataLines = new DataLine[NetworkDataRecord.NodesCount];
            for (int i = 0; i < NetworkDataRecord.NodesCount; i++) DataLines[i] = new DataLine();

            labInfo.Text = "Чтение файлов";
            progressBar.Maximum = NetworkDataRecord.NodesCount;
            progressBar.Value = 0;
            Application.DoEvents();
            for (int i = 1; i <= NetworkDataRecord.NodesCount; i++)
            {
                string[] lines = File.ReadAllLines(path + i.ToString() + ext);
                for (int k = 0; k < lines.Length; k++) DataLines[i - 1].Add(new NodeDataRecord(lines[k]));
                progressBar.Value++;
                Application.DoEvents();
                if (Aborted) break;
            }

            labInfo.Text = "Обработка данных";
            int count = 0;
            for (int i = 0; i < NetworkDataRecord.NodesCount; i++) count += DataLines[i].Count;
            progressBar.Maximum = count;
            progressBar.Value = 0;
            Application.DoEvents();

            Matrix M = Matrix.Get0Matrix(1, NetworkDataRecord.NodesCount);
            Matrix Max = Matrix.Get0Matrix(1, NetworkDataRecord.NodesCount);
            Matrix Opt = Matrix.Get0Matrix(1, NetworkDataRecord.NodesCount);
            Matrix OptAbove = Matrix.Get0Matrix(1, NetworkDataRecord.NodesCount);
            Opt[1, 1] = 87; Opt[1, 2] = 122; Opt[1, 3] = 83; Opt[1, 4] = 56; Opt[1, 5] = 86;
            double T = 0;
            while (DataLinesIsNotEmpty() && (!Aborted)) // на самом деле, останавливаемся, как только окончились события в одном из узлов
            {
                // запоминаем состояние
                NetworkDataRecord newState = new NetworkDataRecord(DataLines);
                States.Add(newState);
                // находим наименьшее время среди верхних, фиксируем это событие и вычеркиваем из списка, в остальных - уменьшаем время на соотв. величину
                int additionalNu = -1;
                int Nu = GetMinimalTimeLine();
                double dt = DataLines[Nu][0].Time;
                newState.Time = dt;
                for (int j = 0; j < NetworkDataRecord.NodesCount; j++)
                {
                    if (j == Nu) DataLines[j].RemoveAt(0);
                    else if (DataLines[j].Count > 0)
                    {
                        DataLines[j][0].Time -= dt;
                        if (DataLines[j][0].Time == 0)
                        {
                            additionalNu = j;
                            DataLines[j].RemoveAt(0);
                        }
                    }
                }
                
                // делаем статистику: вычисл. м.о., макс., кол-во превышений опт. числа
                for (int k = 1; k <= NetworkDataRecord.NodesCount; k++)
                {
                    M[1, k] += newState.State[k - 1] * dt;
                    if (Max[1, k] < newState.State[k - 1]) Max[1, k] = newState.State[k - 1];
                    if (newState.State[k - 1] > Opt[1, k]) OptAbove[1, k] += dt;
                }

                T += dt;
                progressBar.Value++;
                Application.DoEvents();
            }

            if (!Aborted)
            {
                for (int k = 1; k <= NetworkDataRecord.NodesCount; k++)
                {
                    M[1, k] = M[1, k] / T;
                }

                labInfo.Text = "Вычисление ковариаций";
                progressBar.Maximum = States.Count;
                progressBar.Value = 0;
                Application.DoEvents();
                
                Matrix CovSum = Matrix.Get0Matrix(NetworkDataRecord.NodesCount, NetworkDataRecord.NodesCount);
                foreach (NetworkDataRecord state in States)
                {
                    for (int i = 1; i <= NetworkDataRecord.NodesCount; i++)
                        for (int j = 1; j <= NetworkDataRecord.NodesCount; j++)
                            CovSum[i, j] += (state.State[i - 1] - M[1, i]) * (state.State[j - 1] - M[1, j]) * state.Time;
                    progressBar.Value++;
                    Application.DoEvents();
                    if (Aborted) break;
                }
                Matrix Cov = CovSum.MultiplyOnDouble(1 / T);
                Matrix OptAboveUdel = OptAbove.MultiplyOnDouble(1 / T);


                // выводим результат

                textBox.Text = "Средние:\r\n" + M.ToString(ShowColNumbers: true) + "\r\n" +
                    "Ковариации:\r\n" + Cov.ToString(true, true) + "\r\n\r\n" +
                    "Максимумы:\r\n" + Max.ToString(ShowColNumbers: true) + "\r\n\r\n" +
                    "Оптимальное число приборов (аналитический расчет):\r\n" + Opt.ToString(ShowColNumbers: true) + "\r\n\r\n" +
                    "Время превышения оптимального числа приборов:\r\n" + OptAbove.ToString(ShowColNumbers: true) + "\r\n\r\n" +
                    "Удельное время превышения оптимального числа приборов:\r\n" + OptAboveUdel.ToString(ShowColNumbers: true) + "\r\n" +
                    "(общее время моделирования: " + T.ToString() + ")\r\n\r\n" +
                    "Всего событий: " + States.Count.ToString();

            }
            panelParams.Enabled = true;
            panelProgress.Visible = false;
        }

        private int GetMinimalTimeLine()
        {
            double maxTime = double.MaxValue;
            int maxNode = -1;
            for (int k = 0; k < NetworkDataRecord.NodesCount; k++)
            {
                if ((DataLines[k].Count > 0) && (DataLines[k][0].Time < maxTime))
                {
                    maxTime = DataLines[k][0].Time;
                    maxNode = k;
                }
            }
            return maxNode;
        }

        private bool DataLinesIsNotEmpty()
        {
            // доходим не до конца файлов
            foreach (DataLine line in DataLines)
                if (line.Count == 0) return false;
            return true;

            /* здесь - до конца
             * foreach (DataLine line in DataLines)
                if (line.Count > 0) return true;
            return false;*/
        }

        private void btAbort_Click(object sender, EventArgs e)
        {
            Aborted = true;
        }
    }

    public class NodeDataRecord
    {
        public int Count = 0;
        public double Time = 0;
        public NodeDataRecord(string s)
        {
            string[] data = s.Split(' ');
            Count = int.Parse(data[0]);
            Time = double.Parse(data[1]);
        }
    }

    public class DataLine : List<NodeDataRecord>
    {
    }

    public class NetworkDataRecord
    {
        public const int NodesCount = 5;
        public int[] State = new int[NodesCount];
        public double Time = 0;

        public NetworkDataRecord(DataLine[] lines)
        {
            for (int k = 0; k < NodesCount; k++)
                State[k] = lines[k][0].Count;
        }
    }
}
