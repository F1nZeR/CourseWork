using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODIS.AIM;
using ODIS.Controls;

namespace ODIS.AIM
{
    public class NormalDistribution : ContinuousDistribution
    {
        public double M = 0;
        public double D = 0;
        public NormalDistribution(double M, double D, BaseGenerator externalBaseGenerator = null)
            : base(externalBaseGenerator)
        {
            this.M = M;
            this.D = D;
        }

        public override double NextValue()
        {
            double result = 0;
            for (int i = 1; i <= 12; i++) result += baseGenerator.NextValue();
            return (result - 6) * Math.Sqrt(D) + M;
            // с большей точностью:
            // z = (result - 6); 
            // return (z + (z * z * z - 3 * z) / 240) * Math.Sqrt(D) + M;

            // еще один метод:
            //double z = Math.Sqrt(-2 * baseGenerator.NextValue());
            //return (2.515517 + 0.802853 * z + 0.010328 * z * z) / (1 + 1.432788 * z + 0.189269 * z * z + 0.001308 * z * z * z) - z;
        }

        public override double F(double x)
        {
            x = (x - M) / Math.Sqrt(D);
            double eps = 0.00000001;
            double pi_const = 0.3989422804014;
            double t = x;
            double sum = t;
            double x2 = x * x;
            double s = 0;
            for (int n = 3; Math.Abs(s - sum) > eps; n += 2)
            {
                t *= x2 / n; s = sum; sum += t;
            }
            return 0.5 + sum * Math.Exp(-x2 / 2) * pi_const;
            /* 14+8*N операций */

            /* еще один метод (его надо доделать)
                    double dfg = 0;
            if (x == 0) return 0.5;
            dfg = gammaDF(0.5, x * x / 2) / 2;
            return 0.5 + (x > 0 ? dfg : -dfg);*/
        }

        public override double p(double x)
        {
            return Math.Exp(-(x - M) * (x - M) / 2 / D) / Math.Sqrt(2 * Math.PI * D);
        }

        public override string ToString()
        {
            return String.Format("Нормально распределение с мат. ожиданием {0} и дисперсией {1}", M, D);
        }

        public override double GetParam(string ParamName)
        {
            if (ParamName.ToLower() == "m") return M;
            else if (ParamName.ToLower() == "d") return D;
            else return base.GetParam(ParamName);
        }

    }

    public class NormalDistributionFactory : IDistributionFactory
    {
        NormalDistributionVisualFactory VisualFactory = new NormalDistributionVisualFactory();

        #region IDistributionFactory Members

        public override string ToString()
        {
            return "Нормальное распределение";
        }

        public IDistributionVisualFactory GetVisualFactory()
        {
            return VisualFactory;
        }

        public bool IsMyDistribution(RandomDistribution distribution)
        {
            return distribution is NormalDistribution;
        }

        public RandomDistribution CreateDistribution(params object[] args)
        {
            return new NormalDistribution((double)args[0], (double)args[1]);
        }

        #endregion
    }

    public class NormalDistributionVisualFactory : IDistributionVisualFactory
    {
        #region IDistributionVisualFactory Members

        public IDistributionParamsPanel CreateControl()
        {
            return new panelNormalDistributionParams();
        }

        #endregion
    }

}
