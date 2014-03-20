using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODIS.AIM;
using ODIS.AMM;
using ODIS.Controls;

namespace ODIS.AIM
{
    public class GenericDiscreteDistribution : DiscreteDistribution
    {
        public Matrix Probs = null;
        public Matrix Values = null;

        // Probs - вектор-строка вероятностей для значений из Values;
        // если Values==null, то берутся числа 1, 2, 3, ... Probs.Cols
        public GenericDiscreteDistribution(Matrix Probs, Matrix Values = null, BaseGenerator externalBaseGenerator = null)
            : base(externalBaseGenerator)
        {
            this.Probs = Probs;
            this.Values = Values;
            Normalize();
        }

        /// <summary>
        /// Нормализация: приведение к: сумма_вероятностей = 1
        /// </summary>
        private void Normalize()
        {
            double P = Probs.SumOfRowItems(1);
            int candidate = Probs.Cols;
            if (P < 1) Probs[1, candidate] += (1 - P);
            else
            {
                while (P > 1)
                {
                    if ((P - 1) > Probs[1, candidate])
                    {
                        P -= Probs[1, candidate];
                        Probs[1, candidate] = 0;
                        candidate--;
                    }
                    else
                    {
                        Probs[1, candidate] -= (P - 1);
                        P = 1;
                    }
                }
            }
        }

        public override double NextValue()
        {
            double A = baseGenerator.NextValue();
            int i = 0;
            while (A >= 0)
            {
                i++;
                A -= Probs[1, i];
            }
            if (Values == null) return i;
            else return Values[1, i];
        }

        public override double P(double x)
        {
            for (int i = 1; i <= Probs.Cols; i++)
            {
                if (x == Value(i)) return Probs[1, i];
            }
            return 0;
        }

        public double Value(int number)
        {
            if (Values != null) return Values[1, number];
            else return number;
        }

        public override double F(double x)
        {
            double result = 0;
            for (int i = 1; (i <= Probs.Cols) && (Value(i) <= x); i++)
                result += Probs[1, i];
            return result;
        }

        public override double GetParam(string ParamName) // переделать на object!
        {
            return base.GetParam(ParamName);
        }
    }

    public class GenericDiscreteDistributionFactory : IDistributionFactory
    {
        GenericDiscreteDistributionVisualFactory VisualFactory = new GenericDiscreteDistributionVisualFactory();

        #region IDistributionFactory Members

        public override string ToString()
        {
            return "Дискретная случайная величина";
        }

        public IDistributionVisualFactory GetVisualFactory()
        {
            return VisualFactory;
        }

        public bool IsMyDistribution(RandomDistribution distribution)
        {
            return distribution is GenericDiscreteDistribution;
        }

        public RandomDistribution CreateDistribution(params object[] args)
        {
            return new GenericDiscreteDistribution((Matrix)args[0], (Matrix)args[1]);
        }

        #endregion
    }

    public class GenericDiscreteDistributionVisualFactory : IDistributionVisualFactory
    {
        #region IDistributionVisualFactory Members

        public IDistributionParamsPanel CreateControl()
        {
            return new panelGenericDiscreteDistributionParams();
        }

        #endregion
    }

}
