using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODIS.Controls;

namespace ODIS.AIM
{
    /// <summary>
    /// Простейший поток событий
    /// </summary>
    public class PoissonStream : RandomEventStream
    {
        public ExponentialDistribution baseDistribution = null;

        public double Lambda
        {
            get 
            {
                if (baseDistribution == null) return 0;
                return baseDistribution.Lambda; 
            }
        }

        public PoissonStream(double lambda, BaseGenerator baseGenerator = null)
            : base(baseGenerator)
        {
            baseDistribution = new ExponentialDistribution(lambda, baseGenerator);
        }

        public override double NextValue()
        {
            LastValue += baseDistribution.NextValue();
            return LastValue;
        }

        public override RandomDistribution GetEstimateDistribution()
        {
            return baseDistribution;
        }

        public override double GetParam(string ParamName)
        {
            return baseDistribution.GetParam(ParamName);
        }
    }

    public class PoissonStreamFactory : IRandomEventStreamFactory
    {
        PoissonStreamVisualFactory VisualFactory = new PoissonStreamVisualFactory();

        #region IRandomEventStreamFactory Members

        public override string ToString()
        {
            return "Простейший поток событий";
        }

        public bool IsMyStream(RandomEventStream stream)
        {
            return stream is PoissonStream;
        }

        public RandomEventStream CreateEventStream(params object[] args)
        {
            return new PoissonStream((double)args[0]);
        }

        public IRandomEventStreamVisualFactory GetVisualFactory()
        {
            return VisualFactory;
        }

        #endregion
    }

    public class PoissonStreamVisualFactory : IRandomEventStreamVisualFactory
    {
        #region IRandomEventStreamVisualFactory Members

        public IRandomEventStreamParamsPanel CreateControl()
        {
            return new panelPoissonStreamParams();
        }

        #endregion
    }

}
