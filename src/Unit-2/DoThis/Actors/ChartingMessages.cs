using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChartApp.Actors
{
    #region Reporting
    public class GatherMetrics { }

    public class Metric
    {
        public Metric(string series, float counterValue)
        {
            CounterValue = counterValue;
            Series = series;
        }

        public string Series { get; private set; }
        public float CounterValue { get; private set; }
    }
    #endregion

    #region Performance Counter Management
    public enum CounterType
    {
        Cpu,
        Memory,
        Disk
    }

    public class SubscribeCounter
    {
        public SubscribeCounter(CounterType counter, IActorRef subscriber)
        {
            Subscriber = subscriber;
            Counter = counter;
        }

        public CounterType Counter { get; private set; }
        public IActorRef Subscriber { get; private set; }
    }

    public class UnsubscribeCounter
    {
        public UnsubscribeCounter(CounterType counter, IActorRef subscriber)
        {
            Counter = counter;
            Subscriber = subscriber;
        }

        public CounterType Counter { get; private set; }
        public IActorRef Subscriber { get; private set; }

    }
    #endregion
}
