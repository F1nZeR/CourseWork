using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODIS.AIM;
using ODIS.Controls;

namespace ODIS.AIM
{
    /// <summary>
    /// Величина, имеющая детерминированное значение
    /// </summary>
    public class DeterminateDistribution : DiscreteDistribution //ContinuousDistribution
    {
        public double Value = 0;

        public DeterminateDistribution(double value = 0)
        {
            this.Value = value;
        }

        public override double NextValue()
        {
            return Value;
        }

        public override double F(double x)
        {
            if (x < Value) return 0;
            else return 1;
        }

        public override double P(double x)
        {
            if (x == Value) return 1;// double.NaN;
            return 0;
        }

        public override string ToString()
        {
            return String.Format("Детерминированная величина, равная {0}", Value);
        }

        public override double GetParam(string ParamName)
        {
            if (ParamName.ToLower() == "value") return Value;
            else return base.GetParam(ParamName);
        }

    }

    public class DeterminateDistributionFactory : IDistributionFactory
    {
        DeterminateDistributionVisualFactory VisualFactory = new DeterminateDistributionVisualFactory();

        #region IDistributionFactory Members

        public override string ToString()
        {
            return "Детерминированное значение";
        }

        public IDistributionVisualFactory GetVisualFactory()
        {
            return VisualFactory;
        }

        public bool IsMyDistribution(RandomDistribution distribution)
        {
            return distribution is DeterminateDistribution;
        }

        public RandomDistribution CreateDistribution(params object[] args)
        {
            return new DeterminateDistribution((double)args[0]);
        }

        #endregion
    }

    public class DeterminateDistributionVisualFactory : IDistributionVisualFactory
    {
        #region IDistributionVisualFactory Members

        public IDistributionParamsPanel CreateControl()
        {
            return new panelDeterminateDistributionParams();
        }

        #endregion
    }
}
