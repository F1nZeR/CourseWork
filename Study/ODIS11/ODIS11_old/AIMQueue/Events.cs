using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ODIS.AIM.Queue
{
    public class Event
    {
        public double Time = 0;
        public Call Call = null;
        public Element Invoker = null;
        public Event(double time, Call call, Element invoker)
        {
            this.Time = time;
            this.Call = call;
            this.Invoker = invoker;
        }

        public void Process()
        {
            Invoker.ProcessEvent(this);
        }
    }

}
