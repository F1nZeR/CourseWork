using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODIS.AIM;
using ODIS.Controls;

namespace ODIS.AIM
{
    public class ExponentialDistribution : ContinuousDistribution
    {
        /// <summary>
        /// Параметр экспоненциального распределения
        /// </summary>
        public double Lambda { get; set; }

        public ExponentialDistribution(double lambda, BaseGenerator externalBaseGenerator = null)
            : base(externalBaseGenerator)
        {
            this.Lambda = lambda;
        }

        public override double NextValue()
        {
            if (Lambda <= 0) throw new Exception("Lambda must be greater than 0 in ExponentialDistribution");
            else
            {
                double a = baseGenerator.NextValue();
                if (a > 0) return -Math.Log(a) / Lambda;
                else return NextValue();
            }
        }

        public override double F(double x)
        {
            return 1 - Math.Exp(-Lambda * x);
        }

        public override double p(double x)
        {
            if (x < 0) return 0;
            else return Lambda * Math.Exp(-Lambda * x);
        }
  
        public override string ToString()
        {
            return String.Format("Экспоненциальное распределение с параметром {0}", Lambda);
        }

        public override double GetParam(string ParamName)
        {
            if (ParamName.ToLower() == "lambda") return Lambda;
            return base.GetParam(ParamName);
        }
    }

    public class ExponentialDistributionFactory : IDistributionFactory
    {
        ExponentialDistributionVisualFactory VisualFactory = new ExponentialDistributionVisualFactory();

        #region IDistributionFactory Members

        public override string ToString()
        {
            return "Экспоненциальное распределение";
        }

        public IDistributionVisualFactory GetVisualFactory()
        {
            return VisualFactory;
        }

        public bool IsMyDistribution(RandomDistribution distribution)
        {
            return distribution is ExponentialDistribution;
        }

        public RandomDistribution CreateDistribution(params object[] args)
        {
            return new ExponentialDistribution((double)args[0]);
        }

        #endregion
    }

    public class ExponentialDistributionVisualFactory : IDistributionVisualFactory
    {
        #region IDistributionVisualFactory Members

        public IDistributionParamsPanel CreateControl()
        {
            return new panelExponentialDistributionParams();
        }

        #endregion
    }
}
