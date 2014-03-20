using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODIS.AMM;

namespace ODIS.AIM
{
    /// <summary>
    /// Абстрактный закон распределения
    /// </summary>
    public abstract class RandomDistribution
    {
        protected BaseGenerator baseGenerator = null;// = RandomBaseGenerator.GetGenerator();

        public RandomDistribution(BaseGenerator externalBaseGenerator = null)
        {
            if (externalBaseGenerator == null) baseGenerator = AIMCore.GetBaseGenerator();
            else baseGenerator = externalBaseGenerator;
        }

        /// <summary>
        /// Возвращает очередное значение случайной величины
        /// </summary>
        /// <returns></returns>
        public abstract double NextValue();
        //public abstract int NextInt();

        /// <summary>
        /// Вероятность попадания значения в интервал
        /// </summary>
        /// <param name="a">Левая граница интервала</param>
        /// <param name="b">Правая граница интервала</param>
        /// <returns></returns>
        public double GetIntervalProbability(double a, double b)
        {
            return F(b) - F(a);
        }

        /// <summary>
        /// Функция распределения
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public abstract double F(double x);


        public virtual double GetParam(string ParamName)
        {
            return 0;
        }
    }

    /// <summary>
    /// Непрерывное распределение
    /// </summary>
    public abstract class ContinuousDistribution : RandomDistribution
    {
        public ContinuousDistribution(BaseGenerator externalBaseGenerator = null)
            : base(externalBaseGenerator)
        { }

        /// <summary>
        /// Плотность распределения
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public abstract double p(double x);
    }

    /// <summary>
    /// Дискретное распределение
    /// </summary>
    public abstract class DiscreteDistribution : RandomDistribution
    {
        public DiscreteDistribution(BaseGenerator externalBaseGenerator = null)
            : base(externalBaseGenerator)
        {
        }

        /// <summary>
        /// Закон распределения для дискретной с.в.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public abstract double P(double x);
    }

    public interface IDistributionFactory
    {
        string ToString();
        bool IsMyDistribution(RandomDistribution distribution);
        RandomDistribution CreateDistribution(params object[] args);

        IDistributionVisualFactory GetVisualFactory();
    }

    public interface IDistributionVisualFactory
    {
        IDistributionParamsPanel CreateControl();
    }

    public interface IDistributionParamsPanel
    {
        RandomDistribution GetDistribution();
        void SetDistribution(RandomDistribution distribution);
    }


}
