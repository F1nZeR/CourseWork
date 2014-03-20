using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ODIS.AMM;
using ODIS.AIM;
using System.IO;

namespace ODIS.Controls
{
    public partial class panelGenericDiscreteDistributionParams : UserControl, IDistributionParamsPanel
    {
        public Matrix XP = null; // 1-я строка - значения величины
                                 // 2-я - их вероятности
        public int N
        {
            get { return (int)editN.Value; }
            set { editN.Value = value; }
        }

        public panelGenericDiscreteDistributionParams()
        {
            InitializeComponent();
            XP = new Matrix(2, 2);
            FillMatrixDefault();
        }

        private void FillMatrixDefault(int fromCol = 1)
        {
            double p = 1 / (double)XP.Cols;
            for (int j = fromCol; j <= N; j++)
            {
                XP[1, j] = j;
                XP[2, j] = p;
            }
        }

        private void btnInputMatrix_Click(object sender, EventArgs e)
        {
            string[] ColTitles = new string[N];
            for (int i = 0; i < N; i++) ColTitles[i] = "";
            frmInputMatrix.InputMatrix(XP, RowTitles: new string[] { "Значение", "Вероятность" }, ColTitles: ColTitles);
        }

        private void editN_ValueChanged(object sender, EventArgs e)
        {
            ResizeMatrix();
        }

        private void ResizeMatrix()
        {
            int oldCols = XP.Cols;
            XP.Resize(2, N);
            if (N > XP.Cols) FillMatrixDefault(oldCols);
        }

        private bool ParamsIsCorrect()
        {
            // проверим, что сумма вероятностей = 1
            double s = 0;
            for (int j = 1; j <= XP.Cols; j++)
                s += XP[2, j];
            if (Math.Abs(s - 1) > 1E-6)// (s != 1)
            {
                MessageBox.Show("Сумма вероятностей не равна единице\r\n(" + s + ")", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
            // + проверим, что все значения идут в порядке возрастания
            for(int j=2;j<=XP.Cols;j++)
                if (XP[1, j] <= XP[1, j - 1])
                {
                    MessageBox.Show("Значения должны идти в порядке строгого возрастания и без повторений\r\n(" + XP[1, j - 1] + " -> " + XP[1, j] + ")", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            return true;
        }

        #region IDistributionParamsPanel Members

        public RandomDistribution GetDistribution()
        {
            if (ParamsIsCorrect())
            {
                Matrix Values = XP.GetRow(1);
                Matrix Probs = XP.GetRow(2);
                return new GenericDiscreteDistribution(Probs, Values);
            }
            else return null;
        }

        public void SetDistribution(RandomDistribution distribution)
        {
            GenericDiscreteDistribution D = (distribution as GenericDiscreteDistribution);
            N = D.Probs.Cols;
            ResizeMatrix();
            FillMatrixDefault();
            for (int j = 1; j <= N; j++)
            {
                XP[2, j] = D.Probs[1, j];
                if (D.Values != null) XP[1, j] = D.Values[1, j];
            }
        }

        #endregion

        private void btLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] text = File.ReadAllLines(openFileDialog.FileName);
                // наверно, сначала почистить от пустых строк?
                Matrix M = new Matrix(2, text.Length);
                double x, p;
                int n = 1;
                foreach (string row in text)
                {
                    string s = row.Trim();
                    if (s.Length > 0)
                    {
                        string[] v = s.Split(' ');
                        if (v.Length == 1)
                        {
                            if (n == 1) x = n - 1;
                            else x = M[1, n - 1] + 1;
                            double.TryParse(CorrectInputString(v[0]), out p);
                        }
                        else
                        {
                            double.TryParse(CorrectInputString(v[0]), out x);
                            double.TryParse(CorrectInputString(v[1]), out p);
                        }
                        M[1, n] = x;
                        M[2, n] = p;
                        n++;
                        if (n > editN.Maximum) break;
                    }
                }
                M.Resize(2, n-1);

                N = M.Cols;
                XP = M;
            }
        }

        private static string CorrectInputString(string text)
        {
            string separatorMustBe = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            string separatorToChange = ".";
            if (separatorMustBe == separatorToChange) separatorToChange = ",";
            return text.Replace(separatorToChange, separatorMustBe);
        }

    }
}
