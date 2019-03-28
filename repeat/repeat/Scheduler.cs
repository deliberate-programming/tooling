using System;
using System.Threading;

namespace repeat
{
    class Scheduler
    {
        private readonly TimeSpan _delay;
        private readonly Command _cmd;

        public Scheduler(TimeSpan delay, Command cmd) {
            _delay = delay;
            _cmd = cmd;
        }

        public void Start() {
            var startedAt = DateTime.Now;
            Console.WriteLine($"Started at {startedAt:f}");
            new Timer(new TimerCallback(_ => {
                Console.WriteLine($"--- {DateTime.Now:T} / {DateTime.Now.Subtract(startedAt):g} ---");
                _cmd.Execute();
            }), null, _delay, _delay);
        }
    }
}