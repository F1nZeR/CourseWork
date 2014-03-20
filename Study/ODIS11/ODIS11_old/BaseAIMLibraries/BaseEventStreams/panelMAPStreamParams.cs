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

namespace ODIS.Controls
{
    public partial class panelMAPStreamParams : UserControl, IRandomEventStreamParamsPanel
    {
        public Matrix Lambda = null;
        public Matrix D = null;
        public int K
        {
            get { return (panelProcess.ProcessParamsPanel as panelMDCCTParams).K; }
        }

        public Matrix Q
        {
            get { return (panelProcess.ProcessParamsPanel as panelMDCCTParams).Q; }
        }

        public panelMAPStreamParams()
        {
            InitializeComponent();
            Lambda = new Matrix(2, 2);
            FillLambdaMatrixDefault();
            D = new Matrix(2, 2);
            panelProcess.AddParamsChangedEventHandler(editK_ValueChanged);
            panelProcess.Title = "Модулирующий процесс";
            ResizeMatrix();
        }

        private void editK_ValueChanged(object sender, EventArgs e)
        {
            ResizeMatrix();
        }

        private void ResizeMatrix()
        {
            if (K > 0)
            {
                int oldSize = Lambda.Rows;
                Lambda.Resize(K, K);
                FillLambdaMatrixDefault(oldSize + 1);
                D.Resize(K, K);
            }
        }

        private void FillLambdaMatrixDefault(int from = 1)
        {
            for (int i = from; i <= Lambda.Rows; i++)
                Lambda[i, i] = 1;
        }

        private void btnInputLambdaMatrix_Click(object sender, EventArgs e)
        {
            frmInputMatrix.InputMatrix(Lambda, MatrixInputDisableOptions.DisableNonDiagonal, MatrixInputConstraintOptions.NonNegative);
        }

        private void btnInputDMatrix_Click(object sender, EventArgs e)
        {
            frmInputMatrix.InputMatrix(D, MatrixInputDisableOptions.DisableDiagonal, MatrixInputConstraintOptions.Normalized);
        }
        
        public bool ParamsIsCorrect()
        {
            if (!panelProcess.ParamsIsCorrect()) return false;
            // проверим матрицу Лямбда (все диаг должны быть >0)
                /* уже не надо
            else
            {
                for (int i = 1; i <= Lambda.Rows; i++)
                    if (Lambda[i, i] == 0)
                    {
                        MessageBox.Show("Среди значений интенсивности присутствует 0", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
            }*/
            return true;
        }

        #region IRandomEventStreamParamsPanel Members

        public RandomEventStream GetStream()
        {
            if (ParamsIsCorrect()) return new MAPStream(Q, Lambda, D);
            else return null;
        }

        public void SetStream(RandomEventStream stream)
        {
            MAPStream s = stream as MAPStream;
            panelProcess.SetProcess(s.ControlProcess);
            Lambda = s.Lambda;
            D = s.D;
        }

        #endregion
    }
}
