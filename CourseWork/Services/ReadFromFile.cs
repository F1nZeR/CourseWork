using System.IO;
using System.Linq;
using System.Windows;
using CourseWork.Utilities;
using CourseWork.Utilities.Helpers;

namespace CourseWork.Services
{
    public static class ReadFromFile
    {
        public static Matrix ReadMatrix(string path)
        {
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
                    var row = s.Replace('.', ',').Split(' ').Select(double.Parse).ToArray();
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
            }

            MatrixHelper.CheckMatrix(resultMatrix);
            return resultMatrix;
        }

        public static MatrixRow ReadMatrixRow(string path)
        {
            var resultRow = new MatrixRow(1);
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
                var s = streamReader.ReadLine();
                if (s != null)
                {
                    var row = s.Replace('.', ',').Split(' ').Select(double.Parse).ToArray();
                    resultRow = new MatrixRow(row.Count());
                    for (int i = 0; i < row.Count(); i++)
                    {
                        resultRow[i + 1] = row[i];
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
            }

            MatrixHelper.CheckVector(resultRow);
            return resultRow;
        }
    }
}
