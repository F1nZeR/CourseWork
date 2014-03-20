using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODIS.AIM;
using ODIS.Controls;

namespace ODIS.AIM
{
    public class PoissonDistribution : DiscreteDistribution
    {
        public double Parameter = 1;
        private NormalDistribution ApproximatingDistribution;
        private GammaDistribution HelperGammaDistribution; // для вычисления F(x)

        public PoissonDistribution(double parameter, BaseGenerator externalBaseGenerator = null)
            : base(externalBaseGenerator)
        {
            this.Parameter = parameter;
            ApproximatingDistribution = new NormalDistribution(parameter, parameter);
            HelperGammaDistribution = new GammaDistribution(Parameter, 1);
        }

        public override double NextValue()
        {
            double p = Math.Exp(-Parameter);
            double A = baseGenerator.NextValue() - p;
            int i = 0;
            while (A >= 0)
            {
                p *= Parameter / (i + 1);
                A -= p;
                i++;
            }
            return i;
        }

        public override double F(double x)
        {
            int n = (int)x; // или Round?
            double result = 0;
            for (int i = 0; i <= n; i++)
                result += P(i);
            return result;
            /* не работает
             * if (n < 0) return 0;
            else return HelperGammaDistribution.F(n + 1);*/
        }

        private int lastArg = 0;         // для ускорения расчета значений в циклах "по порядку"
        private double lastValue = 1;   // Arg - аргумент, Value - значение рекуррекнтного члена
        public override double P(double x)
        {
            int n = (int)x;
            if (n < 0) return 0;
            else if (n < 100)
            {
                // вычисляем по рекуррентной формуле, основываясь на предыдущем значении
                // но сначала определим, не начать ли заново
                if (n < lastArg)
                {
                    lastArg = 0;
                    lastValue = 1;
                }
                for (int i = lastArg + 1; i <= n; i++) lastValue *= (Parameter / i);
                lastArg = n;
                return lastValue * Math.Exp(-Parameter);
            }
            else //При Parameter>9 распределение Пуассона можно аппроксимировать 
            //нормальным распределением со средним и дисперсией, равными Parameter.
            {
                return ApproximatingDistribution.p(x);
            }
        }

        public override string ToString()
        {
            return String.Format("Распределение Пуассона с параметром {0}", Parameter);
        }

        public override double GetParam(string ParamName)
        {
            if ((ParamName.ToLower() == "parameter") || (ParamName.ToLower() == "lambda")) return Parameter;
            else return base.GetParam(ParamName);
        }
    }

    public class PoissonDistributionFactory : IDistributionFactory
    {
        PoissonDistributionVisualFactory VisualFactory = new PoissonDistributionVisualFactory();

        #region IDistributionFactory Members

        public override string ToString()
        {
            return "Распределение Пуассона";
        }

        public IDistributionVisualFactory GetVisualFactory()
        {
            return VisualFactory;
        }

        public bool IsMyDistribution(RandomDistribution distribution)
        {
            return distribution is PoissonDistribution;
        }

        public RandomDistribution CreateDistribution(params object[] args)
        {
            return new PoissonDistribution((double)args[0]);
        }

        #endregion
    }

    public class PoissonDistributionVisualFactory : IDistributionVisualFactory
    {
        #region IDistributionVisualFactory Members

        public IDistributionParamsPanel CreateControl()
        {
            return new panelPoissonDistributionParams();
        }

        #endregion
    }

}
