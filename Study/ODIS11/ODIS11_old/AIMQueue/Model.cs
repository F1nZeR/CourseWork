using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODIS.AIM;

namespace ODIS.AIM.Queue
{
    /// <summary>
    /// Модель имитационного моделирования СМО
    /// </summary>
    public class QueueSimulationModel: SimulationModel, IInformer
    {
        public int StopConditionType = 0; // 0 - остановка после определенного числа событий (1 - после определенного времени)
        public int StopConditionValue = 10000; // значение, на котором останавливается процесс моделирования
        public int FailCode = 0; // 0 - всё нормально
        public double ActualTime = -1; // последний актуальный момент моделирования, = времени генерации последней заявки, 
                                       // используется для вычисления средних величин, протяженных во времени: средняя длина очереди, ср. число заявок в системе и т.п.
        
        public override void DoStep()
        {
            Event e = NextEvent();
            if (e != null) e.Process();
        }

        public override void OnInitialization()
        {
            // типа всегда будет условие останова по количеству событий
            for (int i = 0; i < StopConditionValue; i++) AddEvent(Source.NextEvent()); // добавляем в журнал все события поступления заявок
            ActualTime = Events.Keys[Events.Count - 1];
        }

        private Event NextEvent()
        {
            Event result = null;
            int i = -1;
            if (Time > 0)
            {
                i = Events.IndexOfKey(Time);
                if (i < 0) FailCode = -1; // "Time/Event Error"
                else if (i == Events.Count - 1) FailCode = 1; // "Закончились события в журнале"
            }
            if (FailCode == 0)
            {
                Time = Events.Keys[i + 1];
                result = Events.Values[i + 1];
            }
            return result;
        }

        public override bool IsDone()
        {
            if (FailCode != 0) return true;
            else return Time >= ActualTime;
            /*switch (StopConditionType)
            {
                case 0: return FailCode == 1; // все события журнала обработаны
                default: return Time >= StopConditionValue;
            }*/
        }

        // Элементы модели
        public Source Source;
        public ServerBlock ServerBlock;

        public QueueSimulationModel()
        {
            ServerBlock = new ServerBlock(this);
            Source = new Source(this, ServerBlock);
        }

        public void AddEvent(Event e)
        {
            Events.Add(e.Time, e);
        }

        public double Time = 0; // текущее время
        public SortedList<double, Event> Events = new SortedList<double, Event>(); // Журнал событий

        public void AddEventAfterTime(double afterTime, Call call, Element invoker)
        {
            AddEvent(Time + afterTime, call, invoker);
        }

        private void AddEvent(double time, Call call, Element invoker)
        {
            AddEvent(new Event(time, call, invoker));
        }

        #region Informer Members

        public string GetDetailInfo()
        {
           double queueAvgLength = ServerBlock.Buffer.BasicStatistic.CalculateAverage();
           double serverAvgServicing = ServerBlock.BasicStatistic.CalculateAverage();
           //double TotalTime = ServerBlock.InSystemStatistic.Sum(x => x.Time);
           return String.Format("Время моделирования: {0:G5}\r\n", ActualTime) +
                  String.Format("Создано заявок: {0}\r\n", Source.BasicStatistic.Count()) +
                  String.Format("Обслужено: {0}\r\n", ServerBlock.OutputStream.Count()) +
                  String.Format("Средняя число заявок в буфере: {0:G5}\r\n", queueAvgLength) +
                  String.Format("Среднее число заявок в системе: {0:G5}\r\n", ServerBlock.InSystemStatistic.CalculateAverage());
                  
                  // Что еще могло бы попасть: 
                  //"Оценки финальных вероятностей состояния системы:\r\n";
                  //String.Format("Относительное время свободной системы: {0:G5}", P0);
                  //String.Format("Отвергнуто: {0}\r\n", ServerBlock.Queue.RejectionStatistic.Count)+
        }

        #endregion

        public RandomDistribution GetEstimateDistributionForQueue()
        {
            if ((Source.InputStream.GetType().Name == "PoissonStream") && (ServerBlock.Distribution.GetType().Name == "ExponentialDistribution"))
            {
                return AIMCore.CreateDistribution("PoissonDistribution", Source.InputStream.GetParam("Lambda") / ServerBlock.Distribution.GetParam("Lambda"));
            }
            else return null;
        }

        public RandomDistribution GetEstimateDistributionForOutput(params object[] args)
        {
            if (Source.InputStream.GetType().Name == "PoissonStream")
            {
                double p = 1;
                if (args.Length > 0) p = (double)args[0];
                return AIMCore.CreateDistribution("PoissonDistribution", p * Source.InputStream.GetParam("Lambda"));
            }
            else
            return null;
        }


        public RandomDistribution GetEstimateDistributionForInput()
        {
            return Source.InputStream.GetEstimateDistribution();
        }
    }

    /// <summary>
    /// Заявка
    /// </summary>
    public class Call
    {
        public double GenerationTime = 0; // 0 - время не является существенным значением

        public Call(double generationTime = 0)
        {
            this.GenerationTime = generationTime;
        }

        public void ErrorElement()
        {
            
        }

        public void Kill()
        {
            
        }
    }
}
