using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using dp.git;
using dp.git.tests.utilities;
using dp_record.adapters;
using Xunit;
using Xunit.Abstractions;

namespace dp_record.tests
{
    public class App_tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public App_tests(ITestOutputHelper testOutputHelper) { _testOutputHelper = testOutputHelper; }

        
        [Fact]
        public void Acceptance_test()
        {
            var repoPath = "acceptance";
            using (new TempRepo(repoPath))
            {
                MockRepeater rep = null;
                var mockLog = new MockConsoleLog();
                var sut = new App(mockLog, 
                                  new GitRepoProvider(repoPath),
                                  new NoScreenshotsProvider(),
                                  _ => {
                                      rep = new MockRepeater();
                                      return rep;
                                  });
                
                // mockLog is blocking to keep the Repeater alive. Hence the App needs to run in the background.
                ThreadPool.QueueUserWorkItem(_ => {
                    sut.Run(new CLI(new[]{"60"}));
                }, null);
                Thread.Sleep(1000); // wait for everything to be initialized
                
                File.WriteAllText(repoPath + "/a.txt", "a");
                rep.Tick();
                
                File.WriteAllText(repoPath + "/a.txt", "aa");
                File.WriteAllText(repoPath + "/b.txt", "b");
                rep.Tick();
                
                File.WriteAllText(repoPath + "/a.txt", "aaa");
                File.Delete(repoPath + "/b.txt");
                rep.Tick();
                
                Assert.Equal(60, mockLog.IntervalSeconds);
                Assert.Equal(3, mockLog.Entries.Count);
                Assert.Equal((0,1,0,0), mockLog.Entries[0].gitStatus);
                Assert.Equal((1,1,0,0), mockLog.Entries[1].gitStatus);
                Assert.Equal((1,0,1,0), mockLog.Entries[2].gitStatus);
                
                _testOutputHelper.WriteLine($"Interval seconds: {mockLog.IntervalSeconds}");
                foreach(var e in mockLog.Entries)
                    _testOutputHelper.WriteLine($"!{e.gitStatus.Modified},+{e.gitStatus.Added},-{e.gitStatus.Deleted},?{e.gitStatus.Other}; {e.commitSha}; {e.elapsed}");
                mockLog.Close();
            }
        }


        class MockRepeater : IRepeater
        {
            private Action _onTick;
            
            public void StartInBackground(Action onTick) {
                _onTick = onTick;
            }

            public void Stop()
            {}


            public void Tick() => _onTick();
            
            public void Dispose() {}
        }


        class MockConsoleLog : IConsoleLog
        {
            public int IntervalSeconds;
            public List<((int Modified, int Added, int Deleted, int Other) gitStatus, string commitSha, TimeSpan elapsed)> Entries;

            private AutoResetEvent _are;
            
            public void ShowModal(int intervalSeconds) {
                IntervalSeconds = intervalSeconds;
                Entries = new List<((int Modified, int Added, int Deleted, int Other), string commitSha, TimeSpan elapsed)>();
                _are = new AutoResetEvent(false);
                _are.WaitOne();
            }

            public void Add((int Modified, int Added, int Deleted, int Other) gitStatus, string commitSha, TimeSpan elapsedTimeSincePreviousCommit) {
                Entries.Add((gitStatus, commitSha, elapsedTimeSincePreviousCommit));
            }


            public void Close() {
                _are.Set();
            }
        }
    }
}