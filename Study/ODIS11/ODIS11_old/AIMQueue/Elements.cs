using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ODIS.AIM.Queue
{
    /// <summary>
    /// Queue Simulation Model Element
    /// </summary>
    public abstract class Element
    {
        public QueueSimulationModel Model = null;
        public Element NextElement = null;
        public TimeStatistic BasicStatistic = new TimeStatistic(); 
        public abstract void Accept(Call call);
        public abstract void ProcessEvent(Event e);
        public Element(QueueSimulationModel model)
        {
            this.Model = model;
        }
    }

    /// <summary>
    /// Источник заявок (входящий поток)
    /// </summary>
    public class Source : Element
    {
        public RandomEventStream InputStream = null;

        public Source(QueueSimulationModel model, Element nextElement = null): base(model)
        {
            this.NextElement = nextElement;
        }
        
        public Event NextEvent()
        {
            double t = InputStream.NextValue();
            Call call = new Call(t);
            BasicStatistic.Add(t, 1);
            return new Event(t, call, this);
        }

        public override void Accept(Call call)
        {
            call.ErrorElement(); // сюда не могут поступать заявки
        }

        public override void ProcessEvent(Event e)
        {
            if (NextElement != null) NextElement.Accept(e.Call);
        }

    }

    /// <summary>
    /// Блок обслуживающих приборов
    /// </summary>
    public class ServerBlock : Element
    {
        public int ServersCount = 1; // -1 означает бесконечное число приборов
        public Buffer Buffer;
        public List<Call> HandlingCalls = new List<Call>();
        public RandomDistribution Distribution = null;
        public SimpleGeneration OutputStream = new SimpleGeneration();
        public TimeStatistic InSystemStatistic = new TimeStatistic();

        public ServerBlock(QueueSimulationModel model): base(model)
        {
            InSystemStatistic.Informer = Model;
            Buffer = new PassiveBuffer(this); // по умолчанию ("буфер отсутствует") создаем очередь с макс. длиной 0 - чтобы регистрировать "отвергнутые" заявки
        }

        /// <summary>
        /// Возвращает true, если в очереди нет заявок, либо буфер не является очередью
        /// </summary>
        private bool QueueIsEmpty
        {
            get 
            {
                if (Buffer is PassiveBuffer) return (Buffer as PassiveBuffer).IsEmpty;
                else return true;
            }
        }

        /// <summary>
        /// Признак возможности обработать заявку (имеется свободный прибор)
        /// </summary>
        public bool IsFree
        {
            get { // либо число приборов бесконечно || либо имеются незанятые приборы, а очередь пуста (либо это ИПВ)
                  return (ServersCount < 0) || ((HandlingCalls.Count < ServersCount) && QueueIsEmpty); }
        }

        /// <summary>
        /// Количество заявок в системе
        /// </summary>
        public int InSystemCount
        {
            get { return HandlingCalls.Count + Buffer.GetCallsCount(); }
        }

        public override void Accept(Call call)
        {
            InSystemStatistic.Add(Model.Time, InSystemCount);
            if (IsFree) // обслужить
                EnforceAccept(call);
            else // поставить в очередь или добавить в ИПВ
                Buffer.Accept(call);
        }

        private void EnforceAccept(Call call)
        {   // стопудово поставить на обслуживание
            // но сначала запишем, сколько было до этого
            BasicStatistic.Add(Model.Time, HandlingCalls.Count); //статистика - это число заявок на обслуживании
            // (потом, может быть, сделать +1 и -1 ?)
            HandlingCalls.Add(call);
            double t = Distribution.NextValue(); // время, через которое заявка обслужится
            Model.AddEventAfterTime(t, call, this);
        }

        private double LastProcessedTime = 0;
        public override void ProcessEvent(Event e)
        {
            //сначала запишем, сколько было до этого
            BasicStatistic.Add(Model.Time, HandlingCalls.Count);
            InSystemStatistic.Add(Model.Time, InSystemCount);
            //теперь - заявка обработана:
            HandlingCalls.Remove(e.Call);
            if (LastProcessedTime > 0) OutputStream.Add(Model.Time - LastProcessedTime);
            LastProcessedTime = Model.Time;
            //взять следующую из очереди, если есть
            if ((Buffer is PassiveBuffer) && (Buffer.GetCallsCount() > 0)) EnforceAccept((Buffer as PassiveBuffer).GetCall());
        }

    }

    /// <summary>
    /// Буфер в блоке приборов. Бывает двух видов: пассивный (например, очередь) и активный (например, ИПВ)
    /// </summary>
    public abstract class Buffer : Element
    {
        public TimeStatistic RejectionStatistic = new TimeStatistic(); // необслуженные заявки (отвергнутые, например, потому что длина очереди ограничена)
        protected ServerBlock ServerBlock = null;

        public Buffer(ServerBlock serverBlock)
            : base(serverBlock.Model)
        {
            this.ServerBlock = serverBlock;
        }

        public abstract int GetCallsCount();

        public virtual RandomDistribution GetEstimatedistribution()
        {
            return null;
        }
    }

    /// <summary>
    /// Пассивный буфер в блоке приборов (например, очередь) 
    /// </summary>
    public class PassiveBuffer : Buffer
    {
        public int MaxLength = 0; // -1 означает, что буфер неограничен
        public Queue<Call> Calls = new Queue<Call>(); // пока очередь, но потом можно переделать

        public bool IsEmpty
        {
            get { return Calls.Count == 0; }
        }

        public override int GetCallsCount()
        {
            return Calls.Count;
        }

        public PassiveBuffer(ServerBlock serverBlock)
            : base(serverBlock)
        {
        }

        public override void Accept(Call call)
        {
            if ((MaxLength < 0) || (Calls.Count < MaxLength))
            {
                //записываем, сколько до этого момента было
                BasicStatistic.Add(Model.Time, Calls.Count); // статистика - число заявок в очереди
                //принять заявку:
                Calls.Enqueue(call);
            }
            else
            {
                //отвергнуть заявку:
                RejectionStatistic.Add(Model.Time, 1); // отказ обслуживания
                call.Kill();
            }
        }

        public override void ProcessEvent(Event e)
        {
            //ошибка - здесь не может быть журнальных событий 
        }

        /// <summary>
        /// Взять заявку из очереди
        /// </summary>
        /// <returns></returns>
        public Call GetCall()
        {
            if (Calls.Count == 0) return null;
            else
            {
                BasicStatistic.Add(Model.Time, Calls.Count);
                Call call = Calls.Dequeue();
                return call;
            }
        }
    }

    /// <summary>
    /// Активный буфер в блоке приборов (например, источник повторных вызовов - ИПВ, орбита)
    /// </summary>
    public class ActiveBuffer : Buffer
    {
        public RandomDistribution Distribution = null;
        private List<Call> Calls = new List<Call>();

        public ActiveBuffer(ServerBlock serverBlock)
            : base(serverBlock)
        {
        }

        public override int GetCallsCount()
        {
            return Calls.Count;
        }

        public override void Accept(Call call)
        {
            //записываем, сколько до этого момента было
            BasicStatistic.Add(Model.Time, Calls.Count); // статистика - число заявок в буфере
            // добавляем заявку к списку
            Calls.Add(call);
            // создаем событие возврата заявки в Блок и записываем его в журнал событий Модели
            AddEvent(call);
        }

        private void AddEvent(Call call)
        {
            if (Distribution != null)
            {
                // узнаем, через сколько времени произойдет событие (выход заявки с орбиты)
                double t = Distribution.NextValue();
                // создаем событие возврата заявки в Блок и записываем его в журнал событий Модели
                Model.AddEventAfterTime(t, call, this);
            }
        }

        public override void ProcessEvent(Event e)
        {
            // записываем, сколько до этого момента было
            BasicStatistic.Add(Model.Time, Calls.Count); // статистика - число заявок в очереди
            // удаляем заявку из списка
            Calls.Remove(e.Call);
            // посылаем ее на прибор
            ServerBlock.Accept(e.Call);
        }
    }
}
