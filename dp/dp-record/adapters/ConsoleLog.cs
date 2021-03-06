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

            Console.ReadLine();
        }

        public void Add((int Modified, int Added, int Deleted, int Other) gitStatus, string commitSha, TimeSpan elapsedTimeSincePreviousCommit) {
            Console.WriteLine($"{++_logEntryCount:00}. {DateTime.Now:g}, {elapsedTimeSincePreviousCommit.TotalMinutes:F1}min: {commitSha.Substring(0,5)}(!{gitStatus.Modified},+{gitStatus.Added},-{gitStatus.Deleted},?{gitStatus.Other})");
        }
    }
}