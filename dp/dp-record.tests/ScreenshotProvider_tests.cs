using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using dp_record.adapters;
using Xunit;

namespace dp_record.tests
{
    public class ScreenshotProvider_tests
    {
        [Fact]
        public void Capture_on_Mac()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) is false) return;

            var screenshotspath = "screenshots";
            if (Directory.Exists(screenshotspath)) Directory.Delete(screenshotspath, true);
            
            var sut = new ScreenshotProvider(screenshotspath);
            
            sut.Capture();
            Assert.Single(Directory.GetFiles(screenshotspath, "*.jpg"));
            Thread.Sleep(1500);
            sut.Capture();
            Assert.Equal(2, Directory.GetFiles(screenshotspath, "*.jpg").Length);
        }
    }
}