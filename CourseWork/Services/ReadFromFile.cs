using System.IO;
using System.Linq;
using System.Windows;
using CourseWork.Utilities;
using CourseWork.Utilities.Helpers;

namespace CourseWork.Services
{
    public static class ReadFromFile
    {
        private static Matrix ReadFile(string path)
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), path);
            var resultMatrix = new Matrix(1, 1);
            FileStream fileStream;

            try
            {
                fileStream = new FileStream(path, FileMode.Open);
            }
            catch (IOException exception)
            {
                MessageBox.Show(exception.Message);
                throw;
            }

            var streamReader = new StreamReader(fileStream);
            try
            {
                int rowsCount = 0;
                string s;
                while ((s = streamReader.ReadLine()) != null)
                {
                    var row = s.Trim().Replace('.', ',').Split(' ').Select(double.Parse).ToArray();
                    rowsCount++; resultMatrix.Resize(rowsCount, row.Count());
                    for (int i = 0; i < row.Count(); i++)
                    {
                        resultMatrix[rowsCount, i + 1] = row[i];
                    }
                }
            }
            catch (IOException exception)
            {
                MessageBox.Show(exception.Message);
                throw;
            }
            finally
            {
                streamReader.Close();
                streamReader.Dispose();

                fileStream.Close();
                fileStream.Dispose();
            }
            return resultMatrix;
        }

        public static Matrix ReadMatrix()
        {
            var matrix = ReadFile(Constants.MatrixPath);
            MatrixHelper.CheckMatrix(matrix);
            return matrix;
        }

        /// <summary>
        /// Считать позиции элементов в LatLng
        /// </summary>
        /// <returns></returns>
        public static Matrix ReadLatLngPositions()
        {
            return ReadFile(Constants.LatLngPath);
        }

        public static Matrix ReadMatrixRow()
        {
            var matrix = ReadFile(Constants.VectorPath);
            for (int i = 0; i < matrix.Rows; i++)
            {
                MatrixHelper.CheckVector(matrix.GetRow(i+1));
            }
            return matrix;
        }
    }
}
