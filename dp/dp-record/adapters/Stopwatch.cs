using System;

namespace dp_record.adapters
{
    class Stopwatch
    {
        private DateTime _startedAt;

        public Stopwatch() => Reset();
        

        public void Start() => Reset();
        public void Reset() => _startedAt = DateTime.Now;
        
        
        public TimeSpan RunningTime => DateTime.Now.Subtract(_startedAt);
    }
}