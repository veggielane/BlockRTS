using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockRTS.Core.Reactive;

namespace BlockRTS.Core.Timing
{
    public class Timer : Observable<TickTime>, ITimer
    {
        public TimeSpan TickDelta { get; set; }
        public TimerState State { get; private set; }
        public TickTime LastTickTime { get; private set; }

        public Timer()
        {
            TickDelta = TimeSpan.FromMilliseconds(1);
            State = TimerState.Stopped;
            LastTickTime = new TickTime();
        }

        public virtual void Start()
        {
            if (State == TimerState.Running)
            {
                return;
            }

            if (State == TimerState.Stopping)
            {
                return;
            }

            State = TimerState.Running;


            while (State == TimerState.Running)
            {
                OnNext(LastTickTime);

                var currentElapsed = LastTickTime.CurrentElapsed();

                if (currentElapsed < TickDelta)
                {
                    Thread.Sleep(TickDelta - currentElapsed);
                }
                LastTickTime.Update(TickDelta);
            }

            State = TimerState.Stopped;
        }

        public virtual void Stop()
        {
            if (State == TimerState.Running)
            {
                State = TimerState.Stopping;
            }
        }
    }
}
