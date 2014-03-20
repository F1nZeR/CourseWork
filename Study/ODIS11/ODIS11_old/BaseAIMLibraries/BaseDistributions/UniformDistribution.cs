using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODIS.AIM;
using ODIS.Controls;

namespace ODIS.AIM
{
    /// <summary>
    /// Случайные величины, равномерно распределенные в интервале [min, max]
    /// </summary>
    public class UniformDistribution : ContinuousDistribution
    {
        public double Min = 0;
        public double Max = 1;

        public UniformDistribution(double min = 0, double max = 1, BaseGenerator externalBaseGenerator = null)
            : base(externalBaseGenerator)
        {
            this.Min = min;
            this.Max = max;
        }

        public override double NextValue()
        {
            return Min + baseGenerator.NextValue() * (Max - Min);
        }

        public override double F(double x)
        {
            if (x <= Min) return 0;
            else if (x >= Max) return 1;
            else return (x - Min) / (Max - Min);
        }

        public override double p(double x)
        {
            if ((x < Min) || (x > Max)) return 0;
            else return 1 / (Max - Min);
        }

        public override string ToString()
        {
            return String.Format("Равномерное распределение в интервале [{0}, {1}]", Min, Max);
        }

        public override double GetParam(string ParamName)
        {
            if (ParamName.ToLower() == "min") return Min;
            else if (ParamName.ToLower() == "max") return Max;
            else return base.GetParam(ParamName);
        }
    }

    public class UniformDistributionFactory : IDistributionFactory
    {
        UniformDistributionVisualFactory VisualFactory = new UniformDistributionVisualFactory();

        #region IDistributionFactory Members

        public override string ToString()
        {
            return "Равномерное распределение";
        }

        public IDistributionVisualFactory GetVisualFactory()
        {
            return VisualFactory;
        }

        public bool IsMyDistribution(RandomDistribution distribution)
        {
            return distribution is UniformDistribution;
        }

        public RandomDistribution CreateDistribution(params object[] args)
        {
            return new UniformDistribution((double)args[0], (double)args[1]);
        }

        #endregion
    }

    public class UniformDistributionVisualFactory : IDistributionVisualFactory
    {
        #region IDistributionVisualFactory Members

        public IDistributionParamsPanel CreateControl()
        {
            return new panelUniformDistributionParams();
        }

        #endregion
    }

}
