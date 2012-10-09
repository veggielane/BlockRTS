using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockRTS.Core.Timing
{
    public class AsyncObservableTimer : ObservableTimer
    {
        private Task RunningTask { get; set; }
        public override void Start()
        {
            RunningTask = new Task(() => base.Start());
            RunningTask.Start();
        }

        public override void Stop()
        {
            base.Stop();

            if (RunningTask != null && RunningTask.IsCompleted)
                RunningTask.Wait();
        }
    }
}
