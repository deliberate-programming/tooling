using System;
using System.Threading;

namespace dp_record.adapters
{
    class Repeater : IDisposable
    {
        private readonly int _intervalSeconds;
        private Timer _timer;

        public Repeater(int intervalSeconds) {
            _intervalSeconds = intervalSeconds;
        }

        
        public void StartInBackground(Action onTick) {
            _timer = new Timer(_ => onTick(), null, _intervalSeconds * 1000, _intervalSeconds * 1000);
        }

        public void Stop() => _timer?.Change(Timeout.Infinite, Timeout.Infinite);

        
        public void Dispose() {
            Stop();
            _timer?.Dispose();
        }
    }
}