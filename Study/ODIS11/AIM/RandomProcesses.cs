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
        /// Переводит текущее время Time к моменту смены состояния и устанавливает новое состояние процесса State
        /// </summary>
        /// <returns></returns>
        public abstract void Next();

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
