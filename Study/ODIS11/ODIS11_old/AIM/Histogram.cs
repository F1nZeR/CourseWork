using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ODIS.AIM
{
    public abstract class HistogramRow
    {
        public double Freq;
        public abstract double GetValue();
    }

    public class SimpleValueHistogramRow : HistogramRow
    {
        public double Value;

        public SimpleValueHistogramRow(double value, double freq)
        {
            this.Value = value;
            this.Freq = freq;
        }

        public override double GetValue()
        {
            return Value;
        }
    }

    public class IntervalHistogramRow : HistogramRow
    {
        public double Min;
        public double Max;

        public double Mid
        { get { return (Min + Max) / 2; } }

        public IntervalHistogramRow(double min, double max, double freq)
        {
            this.Min = min;
            this.Max = max; 
            this.Freq = freq;
        }

        public override double GetValue()
        {
            return Max;
        }
    }

    public class Histogram : List<HistogramRow>
    {
        private Generation generation = null;

        public Histogram(Generation generation, int intervalCount = 0)
        {
            this.generation = generation;
            BuildList(intervalCount);
        }

        const double maxCorrectionRate = 1.001;
        public double Step;
        private void BuildList(int intervalCount = 0)
        {
            if (generation is SimpleGeneration)
            {
                SimpleGeneration g = generation as SimpleGeneration;
                double min = generation.Min();
                double max = generation.Max();
                int totalCount = g.Count();
                if (generation.IsDiscrete)
                {
                    for (int i = (int)min; i <= (int)max; i++)
                    {
                        int count = g.Values.Count(x => x == i);
                        double freq = (double)count / totalCount;
                        if (freq > 0)
                            Add(new SimpleValueHistogramRow(i, freq));
                    }
                }
                else
                {
                    max = max * maxCorrectionRate; // чтобы max тоже был посчитан
                    if (intervalCount == 0) intervalCount = (int)(3.3 * Math.Log10(totalCount) + 1);
                    Step = (max - min) / intervalCount;
                    for (int i = 0; i < intervalCount; i++)
                    {
                        int count = g.Values.Count(x => (x >= min) && (x < min + Step));
                        //если нужно, здесь можно сделать, как сделано для дискретного (if freq>0)
                        Add(new IntervalHistogramRow(min, min + Step, (double)count / totalCount));
                        min += Step;
                    }
                }
            }
            else if (generation is TimeStatistic)
            {
                TimeStatistic ts = generation as TimeStatistic; 
                double TotalTime = ts.Values.Sum(x => x.Time);
                int N = (int)ts.Values.Max(x => x.Value); // потом для непрерывных значений надо ввести интервалы разбиения
                for (int i = 0; i <= N; i++)
                {
                    double freq = ts.Values.Where(x => x.Value == i).Sum(x => x.Time) / TotalTime;
                    if (freq > 0)
                        Add(new SimpleValueHistogramRow(i, freq));
                }
            }
        }

        public double X2(RandomDistribution distribution, ICycleMonitor CycleMonitor = null)
        {
            double sum = 0;
            foreach (HistogramRow row in this)
            {
                double P = 0;
                if (row is IntervalHistogramRow)
                {
                    IntervalHistogramRow R = row as IntervalHistogramRow;
                    P = distribution.GetIntervalProbability(R.Min, R.Max);
                }
                else if (row is SimpleValueHistogramRow)
                {
                    if (distribution is DiscreteDistribution)
                        P = (distribution as DiscreteDistribution).P((row as SimpleValueHistogramRow).Value);
                }
                sum += P + row.Freq * row.Freq / P - 2 * row.Freq; // (P - row.Freq) * (P - row.Freq) / P;
                if ((CycleMonitor != null) && CycleMonitor.IsAborted()) return double.NaN;
            }
            return sum * generation.Count();
        }

        public double Kolmogorov(RandomDistribution distribution, ICycleMonitor CycleMonitor = null)
        {
            if (generation.IsDiscrete) return DiscreteKolmogorov(distribution, CycleMonitor);
            else return ContinuousKolmogorov(distribution, CycleMonitor);
            /*
            double step = 1;
            double min = generation.Min();
            double max = generation.Max();
            if (!generation.IsDiscrete)
            {
                max *= maxCorrectionRate;
                step = (max - min) / generation.Count(); // пока так, потом надо подумать, на сколько интервалов разбивать
            }

            // + оптимизировать цикл (вычисление эмп. ф-ции распр.)
            double sup = 0;
            for (double x = min; x <= max; x += step)
            {
                double v = 0;
                if (generation.IsDiscrete) v = Math.Abs(this.F(x) - distribution.F(x));
                else v = Math.Abs(generation.F(x) - distribution.F(x));
                if (v > sup) sup = v;
                if ((CycleMonitor != null) && CycleMonitor.IsAborted()) return double.NaN;
            }
            return sup*/ /* *Math.Sqrt(Generation.Count)*/ //;
        }

        private double DiscreteKolmogorov(RandomDistribution distribution, ICycleMonitor CycleMonitor)
        {
            double Fn = 0;
            double sup = 0;
            foreach (HistogramRow R in this)
            {
                Fn += R.Freq;
                double v = Math.Abs(Fn - distribution.F(R.GetValue()));
                if (v > sup) sup = v;
                if ((CycleMonitor != null) && CycleMonitor.IsAborted()) return double.NaN;
            }
            return sup;
        }

        private double ContinuousKolmogorov(RandomDistribution distribution, ICycleMonitor CycleMonitor)
        {
            int count = 0;
            double sup = 0;
            IEnumerable<double> values = (generation as SimpleGeneration).Values.OrderBy(x => x);
            foreach (double x in values) // только, если values сортирован!
            {
                count++;
                double v = Math.Abs((double)count / (double)generation.Count() - distribution.F(x));
                if (v > sup) sup = v;
                if ((CycleMonitor != null) && CycleMonitor.IsAborted()) return double.NaN;
            }
            return sup;
        }

        private double F(double x)
        {
            return this.Where(z => z.GetValue() <= x).Sum(z => z.Freq);
        }

        public string GetCriteriasCheckingText(RandomDistribution distribution, ICycleMonitor CycleMonitor = null)
        {
            return String.Format("Хи-кв.={0" + AIMCore.DoubleFormat + "} степ.св.={1}\r\n"+
                                 "Колмогоров={2" + AIMCore.DoubleFormat + "}", X2(distribution, CycleMonitor), Count - 1, Kolmogorov(distribution, CycleMonitor));
        }

    }
}
