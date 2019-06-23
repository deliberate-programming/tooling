using System;
using System.Threading;
using dp_record.adapters;
using Xunit;

namespace dp_record.tests
{
    public class Repeater_tests
    {
        [Fact]
        public void Start()
        {
            using (var sut = new Repeater(1))
            {
                var are = new AutoResetEvent(false);
                var sw = new Stopwatch();
                var n = 0;
                sw.Start();
                sut.StartInBackground(() => {
                    n++;
                    if (n == 3) {
                        sut.Stop();
                        are.Set();
                    }
                });
                are.WaitOne(5000);
                Assert.Equal(3,n);
                Assert.True(sw.RunningTime < new TimeSpan(0,0,0,4));
            }
        }
    }
}