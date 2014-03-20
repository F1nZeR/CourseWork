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
    public partial class panelMDCCTParams : UserControl, IRandomProcessParamsPanel
    {
        public Matrix Q = null;
        public int K
        {
            get { return (int)editK.Value; }
            set { editK.Value = value; }
        }

        public panelMDCCTParams()
        {
            InitializeComponent();
            Q = new Matrix(2, 2);
            FillQMatrixDefault();
            AutocalculateQDiagonal(Q);
        }

        public void AutocalculateQDiagonal(Matrix M, int rowNumber = 0, int colNumber = 0)
            // rowNumber = 0 означает пересчитать всю матрицу
        {
            if (rowNumber > 0)
            {
                double s = 0;
                for (int j = 1; j <= M.Cols; j++)
                    if (j != rowNumber) s += M[rowNumber, j];
                M[rowNumber, rowNumber] = -s;
            }
            else
            {
                for (int i = 1; i <= M.Rows; i++)
                    AutocalculateQDiagonal(M, i);
            }
        }

        private void btnInputQMatrix_Click(object sender, EventArgs e)
        {
            frmInputMatrix.InputMatrix(Q, MatrixInputDisableOptions.DisableDiagonal, MatrixInputConstraintOptions.NonNegative, AutocalculateQDiagonal, "Q");
        }

        private void editK_ValueChanged(object sender, EventArgs e)
        {
            ResizeMatrix();
            DoParamsChanged(sender, e);
        }

        public event EventHandler ParamsChanged = null;

        public void DoParamsChanged(object sender, EventArgs e)
        {
            if (ParamsChanged != null) ParamsChanged(sender, e);
        }

        private void FillQMatrixDefault(int from = 1)
        {
            for (int i = from; i <= Q.Rows; i++)
                for (int j = 1; j <= Q.Cols; j++)
                    if (i != j) Q[i, j] = 1;

            for (int i = 1; i < from; i++)
                for (int j = from; j <= Q.Cols; j++)
                    if (i != j) Q[i, j] = 1;
        }

        private void ResizeMatrix()
        {
            int oldSize = Q.Rows;
            Q.Resize(K, K);
            if (K > oldSize) FillQMatrixDefault(oldSize + 1);
            AutocalculateQDiagonal(Q);
        }

        #region IRandomProcessParamsPanel Members

        public RandomProcess GetProcess()
        {
            if (ParamsIsCorrect()) return new MDCCT(Q);
            else return null;
        }

        public void SetProcess(RandomProcess process)
        {
            MDCCT p = process as MDCCT;
            K = p.K;
            Q = p.Q;
        }

        public bool ParamsIsCorrect()
        {
            // проверим на наличие нулевых элементов по диагонали Q
            for (int i = 1; i <= Q.Rows; i++)
                if (Q[i, i] == 0)
                {
                    MessageBox.Show("Нулевая строка в матрице инфинитезимальных характеристик", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            return true;
        }

        public void AddParamsChangedEventHandler(EventHandler eventHandler)
        {
            ParamsChanged += eventHandler;
        }

        #endregion
    }
}
