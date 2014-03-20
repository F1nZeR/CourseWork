using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODIS.AMM;

namespace ODIS.AIM
{
    /// <summary>
    /// Абстрактный поток случайных событий
    /// </summary>
    public abstract class RandomEventStream
    {
        /// <summary>
        /// Момент последнего события
        /// </summary>
        public double LastValue { get; set; }
        protected BaseGenerator baseGenerator = null;

        public RandomEventStream(BaseGenerator baseGenerator = null)
        {
            if (baseGenerator == null) this.baseGenerator = AIMCore.GetBaseGenerator();
            else this.baseGenerator = baseGenerator;
            this.LastValue = 0;
        }

        public abstract double NextValue();

        public virtual RandomDistribution GetEstimateDistribution()
        {
            return null;
        }


        public virtual double GetParam(string ParamName)
        {
            return 0;
        }
    }

    public interface IRandomEventStreamFactory
    {
        string ToString();
        bool IsMyStream(RandomEventStream stream);
        RandomEventStream CreateEventStream(params object[] args);

        IRandomEventStreamVisualFactory GetVisualFactory();
    }

    public interface IRandomEventStreamVisualFactory
    {
        IRandomEventStreamParamsPanel CreateControl();
    }

    public interface IRandomEventStreamParamsPanel
    {
        RandomEventStream GetStream();
        void SetStream(RandomEventStream stream);
    }

}
