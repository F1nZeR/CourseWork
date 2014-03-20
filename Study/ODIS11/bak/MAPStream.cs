using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODIS.AMM;
using ODIS.Controls;

namespace ODIS.AIM
{
    /// <summary>
    /// MAP-поток
    /// </summary>
    public class MAPStream : RandomEventStream
    {
        public MDCCT ControlProcess = null; // затем можно обобщить до RandomProcess
                                // Здесь же храним текущее состояние модулирующего процесса и время смены его состояний
        public Matrix Lambda = null;
        public Matrix D = null;
        public Matrix Q
        {
            get
            {
                if (ControlProcess != null) return ControlProcess.Q;
                else return null;
            }
        }

        private PoissonStream innerStream = null;
        public double MaxTime = 1000000000; // 1 млрд. сек.

        public MAPStream(Matrix Q, Matrix Lambda, Matrix D, BaseGenerator baseGenerator = null)
            : base(baseGenerator)
        {
            ControlProcess = new MDCCT(Q); // аккуратнее здесь надо. Может baseGenerator передать?
            this.Lambda = Lambda;
            this.D = D;
            ControlProcess.Initiate(); // получили начальное состояние модулирующей цепи
            double currentLambda = Lambda[(int)ControlProcess.State, (int)ControlProcess.State];
            innerStream = new PoissonStream(currentLambda, baseGenerator);
            ControlProcessNext(); // сохраняем старое состояние и т.п.
            ControlProcess.NextTimeInterval(); // получили время следующей смены состояния
        }

        private int oldControlState = 0; // предыдущее состяние управляющего процесса
        private bool IsStateChanging = false; // сигнал о том, что предыдущее событие принудительно сгенерировано в момент смены состояний
        public override double NextValue()
        {
            // если зависло, то прерываем
            if (ControlProcess.Time >= MaxTime) return MaxTime;
            
            // иначе
            // получаем возможное следующее значение
            double t = innerStream.NextValue();

            // если оно укладывается в текущий интервал модулирующего процесса и не было нереализованной смены состояний, то всё ОК - возвращаем это значение
            if ((t < ControlProcess.Time) && (!IsStateChanging)) return t;
            
            // иначе придется генерить новое состояние модулирующего процесса
            // причем, сначала убедимся, что на предыдущем шаге мы не делали этого в той же точке
            if (!IsStateChanging)
            {
                // узнаем новое состояние
                int oldState = (int)ControlProcess.State;
                int newState = (int)ControlProcess.NextState();

                // решаем, надо ли производить событие в момент смены состояний (матрица D)
                IsStateChanging = (baseGenerator.NextValue() < D[oldState, newState]);
                if (IsStateChanging)
                    // если да, то возвращаем время смены состояний, но запоминаем про это (IsStateChanging=true) и в следующий раз сразу переходим к блоку смены состояния
                    return ControlProcess.Time;
            }
            else IsStateChanging = false; // сбрасываем флаг необходимости смены состояния, т.к. ниже оно сменяется
            
            // блок смены состояния
            // генерим новый поток и переключаем его время
            double currentLambda = Lambda[(int)ControlProcess.State, (int)ControlProcess.State];
            innerStream = new PoissonStream(currentLambda, baseGenerator);
            innerStream.LastValue = ControlProcess.Time;
            // генерим следующее время смены состояния
            ControlProcess.NextTimeInterval();
            // и, наконец, получаем следующее событие в нашем потоке:
            return NextValue();
        }
    }

    public class MAPStreamFactory : IRandomEventStreamFactory
    {
        MAPStreamVisualFactory VisualFactory = new MAPStreamVisualFactory();

        #region IRandomEventStreamFactory Members

        public override string ToString()
        {
            return "MAP-поток";
        }

        public bool IsMyStream(RandomEventStream stream)
        {
            return stream is MAPStream;
        }

        public RandomEventStream CreateEventStream(params object[] args)
        {
            return new MAPStream((Matrix)args[0], (Matrix)args[1], (Matrix)args[2]);
        }

        public IRandomEventStreamVisualFactory GetVisualFactory()
        {
            return VisualFactory;
        }

        #endregion
    }

    public class MAPStreamVisualFactory : IRandomEventStreamVisualFactory
    {
        #region IRandomEventStreamVisualFactory Members

        public IRandomEventStreamParamsPanel CreateControl()
        {
            return new panelMAPStreamParams();
        }

        #endregion
    }

}
