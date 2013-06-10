using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Utilities.Helpers
{
    public class MatrixHelper
    {
        public static void CheckMatrix(Matrix matrix)
        {
            double sum = 0;
            for (int i = 1; i <= matrix.Rows; i++)
            {
                for (int j = 1; j <= matrix.Cols; j++)
                {
                    sum += matrix[i, j];
                }
                if (sum > 1.0f)
                {
                    throw new Exception("Сумма вероятностей > 100% (строка №" + i + ")");
                }
                sum = 0;
            }
        }

        public static void CheckVector(Matrix vector)
        {
            double sum = 0;
            for (int i = 1; i <= vector.Cols; i++)
            {
                sum += vector[1, i];
            }
            if (sum > 1.0f)
            {
                throw new Exception("Сумма вероятностей > 100% (вектор)");
            }
        }

        public static double CalculateRowChance(Matrix matrix, int row)
        {
            double sum = 0;
            for (int i = 1; i <= matrix.Cols; i++)
            {
                sum += matrix[row, i];
            }

            return sum;
        }
    }
}
