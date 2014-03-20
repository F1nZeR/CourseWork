using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODIS.AMM;

namespace ODIS.AIM
{
    /// <summary>
    /// Абстрактный случайный процесс
    /// </summary>
    public abstract class RandomProcess
    {

        public double Time = 0; // текущее время (последний момент, когда произошло событие)
        public double State = 0; // текущее состояние процесса во время моделирования

        /// <summary>
        /// Инициирует начальное состояние и возвращает его
        /// </summary>
        /// <returns></returns>
        public abstract double Initiate();

        /// <summary>
        /// Вычисляет интервал времени, через который произойдет смена состояния, переводит текущее время к этому моменту
        /// Внимание! НЕ вычисляет и НЕ переводит процесс в следующее состояние
        /// </summary>
        /// <returns></returns>
        public abstract double NextTimeInterval();

        /// <summary>
        /// Вычисляет и переводит процесс в следующее состояние. Считается, что время перехода уже вычислено и переведено с помощью NextTimeInterval()
        /// </summary>
        /// <returns></returns>
        public abstract double NextState();

        public virtual RandomDistribution GetEstimateDistribution()
        {
            return null;
        }
    }

    public interface IRandomProcessFactory
    {
        string ToString();
        bool IsMyProcess(RandomProcess process);
        RandomProcess CreateProcess(params object[] args);

        IRandomProcessVisualFactory GetVisualFactory();
    }

    public interface IRandomProcessVisualFactory
    {
        IRandomProcessParamsPanel CreateControl();
    }

    public interface IRandomProcessParamsPanel
    {
        RandomProcess GetProcess();
        void SetProcess(RandomProcess process);
        bool ParamsIsCorrect();

        void AddParamsChangedEventHandler(EventHandler eventHandler);
    }

}
