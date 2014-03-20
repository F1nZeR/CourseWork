using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODIS.AMM;
using ODIS.Controls;
using ODIS.AIM;

namespace ODIS.AIM
{
    /// <summary>
    /// Дискретная цепь Маркова с непрерывным временем
    /// MDCCT (Markovian Discrete Chain with Continuous Time)
    /// </summary>
    public class MDCCT: RandomProcess
    {
        protected BaseGenerator baseGenerator = null; // перенести в базовый класс?

        public Matrix Q = null; // матрица инфинитезимальных коэффициентов
        public Matrix R = null; // вектор-строка финальных вероятностей

        public int K
        {
            get
            {
                if (Q == null) return 0;
                else return Q.Rows;
            }
        }

        public MDCCT(Matrix Q)
        {
            baseGenerator = AIMCore.GetBaseGenerator();
            this.Q = Q;
            CalculateFinalProbabilities(); 
        }

        private void CalculateFinalProbabilities()
        {
            // вычисляем вектор R = V M' обр(M M')
            
            // M = Q и еще единичный столбец
            Matrix M = Q.Copy();
            M.Resize(M.Rows, M.Cols + 1);
            for (int i = 1; i <= M.Rows; i++) M[i, M.Cols] = 1;

            Matrix MT = M.Transp();
            Matrix MMT = M.Multiply(MT);
            Matrix MMTInv = MMT.Inversion();
            
            // V=(0,0,...,0,1)
            Matrix V = Matrix.Get0Matrix(1,M.Cols);
            V[1, V.Cols] = 1;

            Matrix VMT = V.Multiply(MT);
            R = VMT.Multiply(MMTInv);
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

        public override double NextTimeInterval()
        {
            double a = baseGenerator.NextValue();
            int i = (int)State;
            double t = Math.Log(a) / Q[i, i];
            Time += t;
            return t;
        }

        public override double NextState()
        {
            int i = (int)State;
            Matrix probs = Matrix.Get0Matrix(1, K);
            for (int j = 1; j <= K; j++)
                if (j != State) probs[1, j] = -Q[i, j] / Q[i, i];
            GenericDiscreteDistribution d = new GenericDiscreteDistribution(probs, null, baseGenerator);
            State = d.NextValue();
            return State;
        }

        public override RandomDistribution GetEstimateDistribution()
        {
            return new GenericDiscreteDistribution(R);
        }
    }

    public class MDCCTFactory : IRandomProcessFactory
    {
        MDCCTVisualFactory VisualFactory = new MDCCTVisualFactory();

        #region IRandomProcessFactory Members

        public override string ToString()
        {
            return "Дискретная цепь Маркова с непрерывным временем";
        }

        public bool IsMyProcess(RandomProcess process)
        {
            return process is MDCCT;
        }

        public RandomProcess CreateProcess(params object[] args)
        {
            return new MDCCT((Matrix)args[0]);
        }

        public IRandomProcessVisualFactory GetVisualFactory()
        {
            return VisualFactory;
        }

        #endregion
    }

    public class MDCCTVisualFactory : IRandomProcessVisualFactory
    {
        #region IRandomProcessVisualFactory Members

        public IRandomProcessParamsPanel CreateControl()
        {
            return new panelMDCCTParams();
        }

        #endregion
    }

    
}
