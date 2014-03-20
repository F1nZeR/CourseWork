using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ODIS.AMM
{
    class MatrixRow 
    {
        //private List<double> Items = null;
        private double[] items = null;

        private int size = 1;
        public int Size { get { return size; } }

        public MatrixRow(int size)
        {
            SetSize(size);
            items = new double[size];
        }

        private void SetSize(int size)
        {
            if (size > 0) this.size = size;
            else throw new Exception("Invalid size of matrix row");
        }

        public double this[int index]
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


        internal MatrixRow Sort()
        {
            MatrixRow result = new MatrixRow(this.Size);
            var z = this.items.OrderBy(x => x);
            int i = 0;
            foreach (double x in z)
            {
                result.items[i] = x;
                i++;
            }
            return result;
        }
    }

    public class Matrix 
    {
        //private List<MatrixRow> RowsList = new List<MatrixRow>();
        private MatrixRow[] rowsList = null;
        private int rows = 1;
        public int Rows { get { return rows; } }
        private int cols = 1;
        public int Cols { get { return cols; } }

        public Matrix(int rows, int cols)
        {
            SetSizes(rows, cols);
            rowsList = CreateRowsList(this.Rows, this.Cols);
        }

        private MatrixRow[] CreateRowsList(int rows, int cols)
        {
            MatrixRow[] result = new MatrixRow[rows];
            for (int i = 0; i < rows; i++)
                result[i] = new MatrixRow(cols);
            return result;
        }

        private void SetSizes(int rows, int cols)
        {
            if ((rows < 1) || (cols < 1)) throw new Exception("Invalid matrix size");
            else
            {
                this.rows = rows;
                this.cols = cols;
            }
        }

        public double this[int index1, int index2]
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
            MatrixRow[] newRowsList = CreateRowsList(rows, cols);
            int minRows = (rows > this.Rows) ? this.Rows : rows;
            int minCols = (cols > this.Cols) ? this.Cols : cols;
            for (int i = 1; i <= minRows; i++)
                for (int j = 1; j <= minCols; j++)
                    newRowsList[i - 1][j] = this[i, j];
            SetSizes(rows, cols);
            rowsList = newRowsList;
        }

        /// <summary>
        /// Обращение матрицы
        /// </summary>
        /// <returns>Возвращает обратную матрицу</returns>
        public Matrix Inversion() // здесь также автоматически вычисляется определитель - но это будем использовать позже: detA: Real
        {
            /* i,j,k,irev: Integer;
              m,c,s: Real;
              ind: Boolean;*/
            if (Rows != Cols) return null;
            int size = Rows;
            Matrix original = Copy();
            Matrix result = Get1Matrix(size);
            bool ind = true; // какой-то индикатор
            int i = 1;
            int irev = 0;
            int j, k; double c, detA;
            while (ind && (i < size))
            {
                k = i;
                double m = Math.Abs(original[i, i]);
                for (j = i + 1; j <= size; j++)
                    if (m < Math.Abs(original[j, i]))
                    {
                        m = Math.Abs(original[j, i]);
                        k = j;
                    }
                if (m < 1.0E-30) ind = false;
                if (ind)
                {
                    if (i != k)
                    {
                        irev++;
                        for (j = i; j <= size; j++)
                        {
                            c = original[i, j];
                            original[i, j] = original[k, j];
                            original[k, j] = c;
                        }
                        for (j = 1; j <= size; j++)
                        {
                            c = result[i, j];
                            result[i, j] = result[k, j];
                            result[k, j] = c;
                        }
                    }
                    for (j = i + 1; j <= size; j++)
                    {
                        for (k = i + 1; k <= size; k++)
                            original[j, k] = original[j, k] - original[i, k] * original[j, i] / original[i, i];
                        for (k = 1; k <= size; k++)
                            result[j, k] = result[j, k] - result[i, k] * original[j, i] / original[i, i];
                    }
                }
                i++;
            }
            if (Math.Abs(original[size, size]) < 1.0E-30) ind = false;
            if ((irev % 2) == 0) detA = 1;
            else detA = -1;
            if (ind)
                for (i = 1; i <= size; i++) detA = detA * original[i, i];
            else detA = 0;
            if (ind)
            {
                for (i = 1; i <= size; i++) result[size, i] = result[size, i] / original[size, size];
                for (i = size - 1; i >= 1; i--)
                    for (j = 1; j <= size; j++)
                    {
                        double s = 0;
                        for (k = i + 1; k <= size; k++) s = s - original[i, k] * result[k, j];
                        result[i, j] = (result[i, j] + s) / original[i, i];
                    }
            }
            //if (!ind) OutMessage("ОБРАЩЕНИЕ ВЫРОЖДЕННОЙ МАТРИЦЫ");
            return result;
        }
        
        /// <summary>
        /// Создает и возвращает копию матрицы
        /// </summary>
        /// <returns></returns>
        public Matrix Copy()
        {
 	        Matrix result = new Matrix(Rows, Cols);
            for (int i = 1; i <= Rows; i++)
                for (int j = 1; j <= Cols; j++)
                    result[i, j] = this[i, j];
            return result;
        } 

        /// <summary>
        /// Создает и возвращает единичную матрицу размера size
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Matrix Get1Matrix(int size)
        {
            Matrix result = new Matrix(size, size);
            for (int i = 1; i <= size; i++)
                for (int j = 1; j <= size; j++)
                    if (i == j) result[i, j] = 1;
                    else result[i, j] = 0;
            return result;
        }

        /// <summary>
        /// Создает и возвращает нулевую матрицу
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public static Matrix Get0Matrix(int rows, int cols)
        {
            Matrix result = new Matrix(rows, cols);
            for (int i = 1; i <= rows; i++)
                for (int j = 1; j <= cols; j++)
                    result[i, j] = 0;
            return result;
        }

        /// <summary>
        /// Создает и возвращает транспонированную матрицу
        /// </summary>
        /// <returns></returns>
        public Matrix Transp()
        {
            Matrix result = new Matrix(Cols, Rows);
            for (int i = 1; i <= Rows; i++)
                for (int j = 1; j <= Cols; j++)
                    result[j, i] = this[i, j];
            return result;
        }

        /// <summary>
        /// Создает и возвращает произведение матрицы на другую матрицу
        /// </summary>
        /// <param name="M"></param>
        /// <returns></returns>
        public Matrix Multiply(Matrix M)
        {
            Matrix result = Get0Matrix(Rows,M.Cols);
            for (int i = 1; i <= Rows; i++)
                for (int j = 1; j <= M.Cols; j++)
                    for (int k = 1; k <= Cols; k++)
                        result[i, j] += this[i, k] * M[k, j];
            return result;
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 1; i <= Rows; i++)
            {
                for (int j = 1; j <= Cols; j++) result += String.Format("{0,8:f} ", this[i, j]);
                if (i < Rows) result += "\r\n";
            }
            return result;
        }

        /// <summary>
        /// Создает и возвращает в виде матрицы (вектор-строки) указанную строку матрицы
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Matrix GetRow(int rowNumber)
        {
            Matrix result = new Matrix(1, Cols);
            for (int j = 1; j <= Cols; j++)
                result[1, j] = this[rowNumber, j];
            return result;
        }

        public Matrix SortInRows()
        {
            Matrix result = new Matrix(Rows, Cols);
            for (int i = 1; i <= Rows; i++)
                result.rowsList[i] = this.rowsList[i].Sort();
            return result;
        }

        public double SumOfRowItems(int rowNumber)
        {
            double result = 0;
            for (int j = 1; j <= Cols; j++)
                result += this[rowNumber, j];
            return result;
        }
    }
}
