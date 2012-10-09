using System;

namespace BlockRTS.Core.Timing
{
    public interface IGameTime
    {
        TimeSpan GameTimeElapsed { get; }
        TimeSpan GameTimeDelta { get; }
        long TickCount { get; }
    }
}