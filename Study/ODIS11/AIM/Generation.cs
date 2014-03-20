using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ODIS.AIM
{
    public abstract class Generation
    {
        public string Title = "";
        public bool IsDiscrete = false;
        public abstract double Min();
        public abstract double Max();
        public abstract int Count();
        public abstract void Clear();
        public abstract void SaveInText(string filename, SaveOptions options = SaveOptions.None);
        public abstract string GetDetailInfo();
        public virtual double F(double x)
        {
            return 0;
        }

        public override string ToString()
        {
            return Title;
        }
    }

    public enum SaveOptions
    {
        None = 0,
        SimpleValues = 1 // для "сложных" массивов исключает значения
    }

    /// <summary>
    /// Выборочные данные (массив вещественных чисел)
    /// </summary>
    public class SimpleGeneration : Generation
    {
        public List<double> Values = new List<double>();

        public void Add(double value)
        {
            Values.Add(value);
        }

        /// <summary>
        /// Вычисление выборочного среднего
        /// </summary>
        /// <returns></returns>
        public double GetMean()
        {
            if (Values.Count > 0) return Values.Average();
            else return 0;
        }

        /// <summary>
        /// Вычисление выборочной дисперсии
        /// </summary>
        /// <returns></returns>
        public double GetVariance()
               
        {
            double M = GetMean();
            return Values.Sum(x => (x - M) * (x - M)) / Values.Count;
        }

        /// <summary>
        /// Эмпирическая функция распределения
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        override public double F(double x)
        {
            return (double)(Values.Count(v => v <= x)) / Values.Count;
        }

        override public string GetDetailInfo()
        {
            return String.Format("Объем: {0};\r\nВыборочное среднее: {1" + AIMCore.DoubleFormat + "};\r\nВыборочная дисперсия: {2" + AIMCore.DoubleFormat + "}",
                                    Values.Count, GetMean(), GetVariance());
        }

        override public void SaveInText(string filename, SaveOptions options = SaveOptions.None)
        {
            StreamWriter sw = File.CreateText(filename);
            foreach (double x in Values) sw.WriteLine(x);
            sw.Close();
            sw.Dispose();
        }

        override public double Min()
        {
            if (Values.Count > 0) return Values.Min();
            else return 0;
        }

        override public double Max()
        {
            if (Values.Count > 0) return Values.Max();
            else return 0;
        }

        override public int Count()
        {
            return Values.Count;
        }

        public override void Clear()
        {
            Values.Clear();
        }

    }

}
