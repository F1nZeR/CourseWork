using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODIS.AIM;
using ODIS.AMM;

namespace ODIS.AIM.Queue
{
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

        const double nearZero = 1E-11;
        public void AddEventAfterTime(double afterTime, Call call, Element invoker)
        {
            if (afterTime < nearZero) afterTime = nearZero; //! сделать это общим!
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
                  String.Format("Среднее число заявок в буфере: {0:G5}\r\n", queueAvgLength) +
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
    /// Модель имитационного моделирования СеМО
    /// (потом убрать Модель обычной СМО :) )
    /// </summary>
    public class QueueNetworkSimulationModel : QueueSimulationModel
    {
        // Элементы модели
        public Router Router;
        //public Source Source;
        public List<ServerBlock> Nodes=new List<ServerBlock>();

        public QueueNetworkSimulationModel(Matrix inputDivision, Matrix routing, List<RandomDistribution> servicing)
        {
            Router = new Router(this, inputDivision, routing);
            foreach (RandomDistribution s in servicing) Nodes.Add(new ServerBlock(this, s, Router));
            Source = new Source(this, Router);
        }

    }

    /// <summary>
    /// Маршрутизатор для СеМО
    /// </summary>
    public class Router : Element
    {
        private Matrix InputDivision;
        private Matrix Routing;

        GenericDiscreteDistribution InputDivisionDistribution;
        List<GenericDiscreteDistribution> RoutingDistributions = new List<GenericDiscreteDistribution>();

        public Router(QueueSimulationModel model, Matrix inputDivision, Matrix routing) : base(model)
        {
            this.InputDivision = inputDivision;
            this.Routing = routing;
            InputDivisionDistribution = new GenericDiscreteDistribution(inputDivision);
            for (int i = 1; i <= Routing.Rows; i++)
            {
                double s = 1;
                Matrix d = new Matrix(1, Routing.Cols + 1);
                for (int j = 1; j <= Routing.Cols; j++)
                {
                    d[1, j] = Routing[i, j];
                    s -= d[1, j];
                }
                d[1, Routing.Cols + 1] = s;
                RoutingDistributions.Add(new GenericDiscreteDistribution(d));
            }
        }

        public override void Accept(Call call, Element source = null)
        {
            if (source is Source)
            {
                int k = (int)InputDivisionDistribution.NextValue();
                (Model as QueueNetworkSimulationModel).Nodes[k - 1].Accept(call);
            }
            else if (source is ServerBlock)
            {
                int k = (Model as QueueNetworkSimulationModel).Nodes.IndexOf(source as ServerBlock);
                int nu = (int)RoutingDistributions[k].NextValue();
                if (nu <= Routing.Cols) (Model as QueueNetworkSimulationModel).Nodes[nu - 1].Accept(call);
            }
        }

        public override void ProcessEvent(Event e)
        {
            e.Call.ErrorElement(); // маршрутизатор сам не обрабатывает заявки!
        }
    }
}
