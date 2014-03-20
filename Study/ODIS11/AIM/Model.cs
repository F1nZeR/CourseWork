using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ODIS.AIM
{
    /// <summary>
    /// Абстрактная модель имитационного моделирования
    /// </summary>
    public class SimulationModel
    {
        /// <summary>
        /// Один шаг моделирования
        /// </summary>
        public virtual void DoStep()
        {
        }

        /// <summary>
        /// Признак окончания процесса моделирования
        /// </summary>
        /// <returns>True, если процесс моделирования окончен</returns>
        public virtual bool IsDone()
        {            
            return true;
        }

        /// <summary>
        /// Основной цикл моделирования
        /// </summary>
        public void Run(ICycleMonitor CycleMonitor = null)
        {
            OnInitialization();
            while (!IsDone() && ((CycleMonitor == null)|| (!CycleMonitor.IsAborted()))) DoStep();
            OnFinalization();
        }

        public virtual void OnFinalization()
        {
        }

        public virtual void OnInitialization()
        {
        }

    }

    public interface ICycleMonitor
    {
        bool IsAborted();
    }
}
