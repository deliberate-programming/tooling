using System;
using System.IO;
using dp.git;
using dp_record.adapters;

namespace dp_record
{
    class App
    {
        private readonly IConsoleLog _log;
        private readonly GitRepoProvider _git;
        private readonly Func<int, IRepeater> _repeaterFactory;
        private readonly Stopwatch _stopwatch;
        private readonly IScreenshotProvider _screenshots;

        public App(IConsoleLog log, GitRepoProvider git, IScreenshotProvider screenshots) 
            : this(log, git, screenshots, intervalSeconds => new Repeater(intervalSeconds)) {}
        internal App(IConsoleLog log, GitRepoProvider git, IScreenshotProvider screenshots, Func<int,IRepeater> repeaterFactory) {
            _log = log;
            _git = git;
            _repeaterFactory = repeaterFactory;
            _stopwatch = new Stopwatch();
            _screenshots = screenshots;
        }


        public void Run(CLI cli) {
            using (var rep = _repeaterFactory(cli.IntervalSeconds))  {
                _stopwatch.Start();
                rep.StartInBackground(
                    Commit_pending_changes
                );
                _log.ShowModal(cli.IntervalSeconds);
            }
        }
        
        
        void Commit_pending_changes() {
            _screenshots.Capture();
            
            var status = _git.Status;
            if (pending_changes_exist()) {
                _git.StageAllChanges();
                var sha = _git.Commit(message(), "dp-record", "dp-record@deliberate-programming.org");
                _log.Add(status, sha, _stopwatch.RunningTime);
                _stopwatch.Reset();
            }
            
            bool pending_changes_exist() => status.Modified + status.Added + status.Deleted + status.Other > 0;
            
            string message() => $"Auto-commit after {_stopwatch.RunningTime.TotalMinutes:F1}min";
        }
    }
}