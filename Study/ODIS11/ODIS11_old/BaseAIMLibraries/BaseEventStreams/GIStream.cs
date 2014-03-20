using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODIS.Controls;

namespace ODIS.AIM
{

    /// <summary>
    /// Рекуррентный поток событий
    /// </summary>
    public class GIStream : RandomEventStream
    {
        public RandomDistribution baseDistribution = null;

        public GIStream(RandomDistribution distribution = null)
        {
            SetDistribution(distribution);
        }

        public void SetDistribution(RandomDistribution distribution)
        {
            baseDistribution = distribution;
        }

        const double nearZero = 1E-12;
        public override double NextValue()
        {
            double t = baseDistribution.NextValue();
            if (t < nearZero) t = nearZero; //! сделать такое же в других потоках!
            LastValue += t;
            return LastValue;
        }
    }

    public class GIStreamFactory : IRandomEventStreamFactory
    {
        GIStreamVisualFactory VisualFactory = new GIStreamVisualFactory();

        #region IRandomEventStreamFactory Members

        public override string ToString()
        {
            return "Рекуррентный поток событий";
        }

        public bool IsMyStream(RandomEventStream stream)
        {
            return stream is GIStream;
        }

        public RandomEventStream CreateEventStream(params object[] args)
        {
            object[] args2 = new object[args.Length - 1];
            for (int i = 1; i < args.Length; i++) args2[i - 1] = args[i];
            return new GIStream(AIMCore.CreateDistribution(args[0] as string, args2));
        }

        public IRandomEventStreamVisualFactory GetVisualFactory()
        {
            return VisualFactory;
        }

        #endregion
    }

    public class GIStreamVisualFactory : IRandomEventStreamVisualFactory
    {
        #region IRandomEventStreamVisualFactory Members

        public IRandomEventStreamParamsPanel CreateControl()
        {
            return new panelGIStreamParams();
        }

        #endregion
    }
}
