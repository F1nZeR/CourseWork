using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ODIS.AIM
{
    /// <summary>
    /// Абстрактный базовый датчик (случайные числа, равномерно распределенные в [0,1])
    /// </summary>
    public abstract class BaseGenerator
    {
        public abstract double NextValue();
    }

    public interface IBaseGeneratorFactory
    {
        BaseGenerator CreateGenerator();
    }

    /// <summary>
    /// Простейший базовый датчик (Random)
    /// </summary>
    public class SimpleBaseGenerator : BaseGenerator
    {
        private Random generator;
        public SimpleBaseGenerator()
        {
            Thread.Sleep(100); // не забываем про это - иначе можем получить две одинаковые последовательности
            this.generator = new Random();
        }

        public override double NextValue()
        {
            return generator.NextDouble();
        }
    }

    public class SimpleBaseGeneratorFactory : IBaseGeneratorFactory
    {
        #region IBaseGeneratorFactory Members

        public override string ToString()
        {
            return "Встроенный (Microsoft)";
        }

        public BaseGenerator CreateGenerator()
        {
            return new SimpleBaseGenerator();
        }

        #endregion
    }

}
