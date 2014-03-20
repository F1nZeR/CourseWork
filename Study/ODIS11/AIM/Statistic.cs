using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ODIS.AIM
{

    public interface IInformer
    {
        string GetDetailInfo();
    }

    public struct TimeStatisticRow
    {
        public double Time;
        public double Value;
        public TimeStatisticRow(double time = 0, double value = 0)
        {
            this.Time = time;
            this.Value = value;
        }
    }

    public class TimeStatistic: Generation
    {
        public List<TimeStatisticRow> Values = new List<TimeStatisticRow>();
        public IInformer Informer = null;
        private double prevTime = 0;

        public TimeStatistic(string title = "", IInformer informer = null)
        {
            this.Title = title;
            this.Informer = informer;
            this.IsDiscrete = true;
        }

        public TimeStatisticRow Add(double time = 0, double value = 0)
        {
            // по умолчанию фактически фиксируется время от предыдущего события
            TimeStatisticRow row = new TimeStatisticRow(time - prevTime, value);
            prevTime = time;
            Values.Add(row);
            return row;
        }

        public double CalculateAverage()
        {
            return Values.Sum(a => a.Time * a.Value) / Values.Sum(a => a.Time);
        }

        public override void SaveInText(string filename, SaveOptions options = SaveOptions.None)
        {
            StreamWriter sw = File.CreateText(filename);
            foreach (TimeStatisticRow row in Values)
                if ((options & SaveOptions.SimpleValues) == 0)  sw.WriteLine("{0} {1}", row.Value, row.Time);
                else sw.WriteLine("{0}", row.Time);
            sw.Close();
            sw.Dispose();
        }

        public override string GetDetailInfo()
        {
            if (Informer != null) return Informer.GetDetailInfo();
            else return "";
        }

        public override double Min()
        {
            return Values.Min(row => row.Value);
        }

        public override double Max()
        {
            return Values.Max(row => row.Value);
        }

        public override int Count()
        {
            return Values.Count;
        }

        public override void Clear()
        {
            Values.Clear();
        }

        public override double F(double x)
        {
            return Values.Where(v => v.Time <= x).Sum(v => v.Value) / Values.Sum(v => v.Value);
        }
    }
}
