using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODIS.AIM;
using ODIS.AMM;
using ODIS.Controls;

namespace ODIS.AIM
{
    /// <summary>
    /// Гамма-распределение
    /// </summary>
    public class GammaDistribution : ContinuousDistribution
    {
        public double Shape = 1; // форма
        public double Scale = 1; // масштаб

        public GammaDistribution(double shape, double scale, BaseGenerator externalBaseGenerator = null)
            : base(externalBaseGenerator)
        {
            this.Shape = shape;
            this.Scale = scale;
        }

        public override double NextValue()
        {
            int ParamInt = (int)Shape;
            double ParamFrac = Shape - ParamInt;
            double result = 0;
            for (int i = 1; i <= ParamInt; i++)
            {
                double a = baseGenerator.NextValue();
                while (a == 0) a = baseGenerator.NextValue();
                result -= Math.Log(a);
            }

            if (ParamFrac > 0)
            {
                BetaDistribution betaSource = new BetaDistribution(1 - ParamFrac, ParamFrac, baseGenerator);
                double a = baseGenerator.NextValue();
                while (a == 0) a = baseGenerator.NextValue();
                result += (betaSource.NextValue() - 1) * Math.Log(a);
            }
            return Scale * result;
        }

        public override double F(double x)
        // Вычисляется Gamma(x|a):
        //      вероятность того, что случайная величина,
        //      подчиняющаяся центральному гамма-распределению с параметром 'a',
        //      меньше или равна 'x'.
        {
            x /= Scale; // в исходнике почему-то было x*=ScaleRate (?)
            if (x <= 0) return 0;
            else if (x < (Shape + 1)) return AMMUtility.GammaSeries(Shape, x);
            else return 1 - AMMUtility.GammaFraction(Shape, x);
        }

        public override double p(double x)
        {
            if (x < 0) return 0;
            else return Math.Pow(x, Shape - 1) * Math.Exp(-x / Scale) / (AMMUtility.Gamma(Shape) * Math.Pow(Scale, Shape));
        }

        public override string ToString()
        {
            return String.Format("Гамма-распределение с параметром {0} и коэффициентом масштабирования {1}", Shape, Scale);
        }

        public override double GetParam(string ParamName)
        {
            if (ParamName.ToLower() == "shape") return Shape;
            else if (ParamName.ToLower() == "scale") return Scale;
            else return base.GetParam(ParamName);
        }

    }

    public class GammaDistributionFactory : IDistributionFactory
    {
        GammaDistributionVisualFactory VisualFactory = new GammaDistributionVisualFactory();

        #region IDistributionFactory Members

        public override string ToString()
        {
            return "Гамма-распределение";
        }

        public IDistributionVisualFactory GetVisualFactory()
        {
            return VisualFactory;
        }

        public bool IsMyDistribution(RandomDistribution distribution)
        {
            return distribution is GammaDistribution;
        }

        public RandomDistribution CreateDistribution(params object[] args)
        {
            return new GammaDistribution((double)args[0], (double)args[1]);
        }

        #endregion
    }

    public class GammaDistributionVisualFactory : IDistributionVisualFactory
    {
        #region IDistributionVisualFactory Members

        public IDistributionParamsPanel CreateControl()
        {
            return new panelGammaDistributionParams();
        }

        #endregion
    }

}
