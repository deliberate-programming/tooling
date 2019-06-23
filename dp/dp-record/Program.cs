using System;
using dp.git;

namespace dp_record
{
    class Program
    {
        static void Main(string[] args) {
            var app = new App(
                new ConsoleLog(), 
                new GitRepoProvider("."));
            
            app.Run(args);
        }
    }


    class App
    {
        private ConsoleLog _log;
        private readonly GitRepoProvider _git;
        private readonly Stopwatch _stopwatch;

        public App(ConsoleLog log, GitRepoProvider git) {
            _log = log;
            _git = git;
            _stopwatch = new Stopwatch();
        }


        public void Run(string[] args) {
            var cli = new CLI(args);
            var rep = new Repeater(cli.IntervalSeconds);

            _stopwatch.Start();
            rep.StartInBackground(
                Commit_if_needed
            );
            _log.ShowModal();
        }
        
        
        void Commit_if_needed() {
            var status = _git.Status;
            if (pending_changes_exist()) {
                var sha = _git.Commit(message(), "dp-record", "dp-record@deliberate-programming.org");
                _log.Add(status, sha, _stopwatch.RunningTime);
                _stopwatch.Reset();
            }
            
            bool pending_changes_exist() 
                => status.Modified + status.Added + status.Deleted + status.Other > 0;
            
            string message() => $"Auto-commit after {_stopwatch.RunningTime.TotalMinutes} minutes";
        }
    }

    class CLI
    {
        
    }


    class Repeater
    {
        
    }

    class ConsoleLog
    {
        
    }

    class Stopwatch
    {
        public TimeSpan RunningTime => new TimeSpan();
    }
}