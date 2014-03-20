using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODIS.AMM;

namespace ODIS.AIM
{
    /// <summary>
    /// Полумарковский процесс
    /// SMP (Semi-Markovian Process)
    /// </summary>
    public class SMP: RandomProcess
    {
        protected BaseGenerator baseGenerator = null; // вроде бы должно хватить одного

        public DistributionsMatrix G = null; // матрица наступления моментов восстановления G(x)
        public Matrix P = null; // матрица вероятностей переходов? из выражения A(x)=G(x)*P

        public Matrix R = null; /* вектор-строка финальных вероятностей 
                                 (вычисляется на основе G(x) и P, но очень сложно! поэтому пусть пользователь сам ее посчитает и введет */

        public int K
        {
            get
            {
                if (P == null) return 0;
                else return P.Rows;
            }
        }

        public SMP(DistributionsMatrix G, Matrix P, Matrix R = null)
        {
            baseGenerator = AIMCore.GetBaseGenerator();
            this.G = G;
            this.P = P;
            this.R = R;
            CalculateFinalProbabilities(); 
        }

        private void CalculateFinalProbabilities()
        {
            // надо вычислить R, когда научимся :) а пока так (равномерное распределение):
            if (R == null)
            {
                double p = 1 / (double)K;
                R = new Matrix(1, K);
                R.Fill(p);
            }
        }

        private int GetFinalValue()
        {
            GenericDiscreteDistribution finalDistribution = new GenericDiscreteDistribution(R, null, baseGenerator);
            return (int)finalDistribution.NextValue();
        }

        /// <summary>
        /// Приводит процесс в начальное состояние и возвращает его
        /// </summary>
        /// <returns></returns>
        public override double Initiate()
        {
            State = GetFinalValue();
            return State;
        }

        public override void Next()
        {
            // сохраняем старое сотсояние
            int prevState = (int)State;

            // вычисляем новое состояние
            Matrix probs = Matrix.Get0Matrix(1, K);
            for (int j = 1; j <= K; j++)
                probs[1, j] = P[prevState, j];
            GenericDiscreteDistribution d = new GenericDiscreteDistribution(probs, null, baseGenerator);
            State = d.NextValue();

            // вычисляем момент восстановления
            RandomDistribution distr = G[prevState, (int)State];
            double t = distr.NextValue();
            Time += t;
        }

        public override RandomDistribution GetEstimateDistribution()
        {
            return new GenericDiscreteDistribution(R);
        }

    }
}
