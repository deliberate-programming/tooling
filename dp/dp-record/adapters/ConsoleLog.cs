using System;

namespace dp_record.adapters
{
    internal interface IConsoleLog
    {
        void ShowModal(int intervalSeconds);
        void Add((int Modified, int Added, int Deleted, int Other) gitStatus, string commitSha, TimeSpan elapsedTimeSincePreviousCommit);
    }

    
    class ConsoleLog : IConsoleLog
    {
        private int _logEntryCount;
        
        public void ShowModal(int intervalSeconds) {
            Console.WriteLine($"Started recording at {DateTime.Now} with an interval of {intervalSeconds} seconds...");
            Console.WriteLine($"Press Ctrl-C to stop");
            Console.WriteLine();
            
            _logEntryCount = 0;
        }

        public void Add((int Modified, int Added, int Deleted, int Other) gitStatus, string commitSha, TimeSpan elapsedTimeSincePreviousCommit) {
            Console.WriteLine($"{++_logEntryCount} {DateTime.Now:g}, {elapsedTimeSincePreviousCommit.TotalMinutes}min: {commitSha.Substring(0,5)}(!{gitStatus.Modified},+{gitStatus.Added},-{gitStatus.Deleted},?{gitStatus.Other})");
        }
    }
}