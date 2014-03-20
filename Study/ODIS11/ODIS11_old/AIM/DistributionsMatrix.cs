using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODIS.AMM;

namespace ODIS.AIM
{
    class DistributionsMatrixRow
    {
        private RandomDistribution[] items = null;

        private int size = 1;
        public int Size { get { return size; } }

        public DistributionsMatrixRow(int size)
        {
            SetSize(size);
            items = new RandomDistribution[size];
        }

        private void SetSize(int size)
        {
            if ((size > 0) && (size <= Matrix.MaxSize)) this.size = size;
            else throw new Exception("Invalid size of matrix row");
        }

        public RandomDistribution this[int index]
        {
            get
            {
                if ((index < 1) || (index > size)) throw new Exception("Invalid index in matrix row");
                else return items[index - 1];
            }
            set
            {
                if ((index < 1) || (index > size)) throw new Exception("Invalid index in matrix row");
                else items[index - 1] = value;
            }
        }
    }

    public class DistributionsMatrix
    {
        private DistributionsMatrixRow[] rowsList = null;
        
        private int rows = 1;
        public int Rows { get { return rows; } }
        private int cols = 1;
        public int Cols { get { return cols; } }

        public DistributionsMatrix(int rows, int cols)
        {
            SetSizes(rows, cols);
            rowsList = CreateRowsList(this.Rows, this.Cols);
        }

        private DistributionsMatrixRow[] CreateRowsList(int rows, int cols)
        {
            DistributionsMatrixRow[] result = new DistributionsMatrixRow[rows];
            for (int i = 0; i < rows; i++)
                result[i] = new DistributionsMatrixRow(cols);
            return result;
        }

        private void SetSizes(int rows, int cols)
        {
            if ((rows < 1) || (cols < 1) || (rows > Matrix.MaxSize) || (cols > Matrix.MaxSize)) throw new Exception("Invalid matrix size");
            else
            {
                this.rows = rows;
                this.cols = cols;
            }
        }

        public RandomDistribution this[int index1, int index2]
        {
            get
            {
                if ((index1 < 1) || (index1 > Rows)) throw new Exception("Invalid index of matrix row");
                else return rowsList[index1 - 1][index2];
            }
            set
            {
                if ((index1 < 1) || (index1 > Rows)) throw new Exception("Invalid index of matrix row");
                else rowsList[index1 - 1][index2] = value;
            }
        }

        public void Resize(int rows, int cols)
        {
            DistributionsMatrixRow[] newRowsList = CreateRowsList(rows, cols);
            int minRows = (rows > this.Rows) ? this.Rows : rows;
            int minCols = (cols > this.Cols) ? this.Cols : cols;
            for (int i = 1; i <= minRows; i++)
                for (int j = 1; j <= minCols; j++)
                    newRowsList[i - 1][j] = this[i, j];
            SetSizes(rows, cols);
            rowsList = newRowsList;
        }

    }
}
