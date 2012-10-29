using System;

namespace BlockRTS.Core.Timing
{
    public interface ITimer : IObservable<TickTime>
    {
        TimeSpan TickDelta { get; set; }
        TimerState State { get; }
        TickTime LastTickTime { get; }
        void Start();
        void Stop();
    }
}