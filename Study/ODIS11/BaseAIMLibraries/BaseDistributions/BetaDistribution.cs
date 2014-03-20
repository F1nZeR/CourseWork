using System;
using ODIS.AMM;
using ODIS.Controls;

namespace ODIS.AIM
{
    public class BetaDistribution : ContinuousDistribution
    {
        public double Alpha = 2;
        public double Beta = 2;

        public BetaDistribution(double alpha, double beta, BaseGenerator externalBaseGenerator = null)
            : base(externalBaseGenerator)
        {
            this.Alpha = alpha;
            this.Beta = beta;
        }

        public override double NextValue()
        {
            double a = Math.Pow(baseGenerator.NextValue(), 1 / Alpha);
            double b = Math.Pow(baseGenerator.NextValue(), 1 / Beta);
            while (a + b > 1)
            {
                a = Math.Pow(baseGenerator.NextValue(), 1 / Alpha);
                b = Math.Pow(baseGenerator.NextValue(), 1 / Beta);
            }
            return a / (a + b);
        }

        public override double F(double x)
        {
            //
            // Вычисляет Beta(x|a,b):
            //      вероятность того, что случайная величина,
            //      подчиняющаяся бета-распределению с параметрами 'a' и 'b',
            //      меньше или равна 'x'.
            //
            if (x <= 0) return 0;
            else if (x >= 1) return 1;
            if (x < (Alpha + 1) / (Alpha + Beta + 2)) return AMMUtility.BetaFraction(Alpha, Beta, x);
            else return 1 - AMMUtility.BetaFraction(Beta, Alpha, 1 - x);
        }

        public override double p(double x)
        {
            if ((x < 0) || (x > 1)) return 0;
            else return Math.Pow(x, Alpha - 1) * Math.Pow(1 - x, Beta - 1) / AMMUtility.Beta(Alpha, Beta);
        }

        public override string ToString()
        {
            return String.Format("Бета-распределение с параметрами {0} и {1}", Alpha, Beta);
        }

        public override double GetParam(string ParamName)
        {
            if (ParamName.ToLower() == "alpha") return Alpha;
            else if (ParamName.ToLower() == "beta") return Beta;
            else return base.GetParam(ParamName);
        }
    }

    public class BetaDistributionFactory : IDistributionFactory
    {
        BetaDistributionVisualFactory VisualFactory = new BetaDistributionVisualFactory();

        #region IDistributionFactory Members

        public override string ToString()
        {
            return "Бета-распределение";
        }

        public IDistributionVisualFactory GetVisualFactory()
        {
            return VisualFactory;
        }

        public bool IsMyDistribution(RandomDistribution distribution)
        {
            return distribution is BetaDistribution;
        }

        public RandomDistribution CreateDistribution(params object[] args)
        {
            return new BetaDistribution((double)args[0], (double)args[1]);
        }

        #endregion
    }

    public class BetaDistributionVisualFactory : IDistributionVisualFactory
    {
        #region IDistributionVisualFactory Members

        public IDistributionParamsPanel CreateControl()
        {
            return new panelBetaDistributionParams();
        }

        #endregion
    }
        
}
