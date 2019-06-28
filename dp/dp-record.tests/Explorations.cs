using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Xunit;
using Xunit.Abstractions;

namespace dp_record.tests
{
    public class Explorations
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Explorations(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Taking_screenshots_on_mac()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // https://ss64.com/osx/screencapture.html
                var filepath = "/Users/ralfw/Downloads/xxx.jpg";
                Process.Start("screencapture", $"-C -m -x -t jpg \"{filepath}\"")
                    .WaitForExit();
            }
            else
            {
                throw new NotSupportedException("screencapture only supported on Mac!");
            }
        }

        [Fact]
        public void Taking_screenshots_on_win()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // https://www.nirsoft.net/utils/nircmd.html
                // https://nircmd.nirsoft.net/savescreenshot.html
                var filepath = "xxx.jpg";
                Process.Start("nircmdc.exe", $"savescreenshot \"{filepath}\"")
                    .WaitForExit();
            }
            else
            {
                throw new NotSupportedException("nircmd only supported on Windows");
            }
        }

        [Fact]
        public void Check_for_OS()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                _testOutputHelper.WriteLine("Running on Mac!");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                _testOutputHelper.WriteLine("Running on Linux!");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                _testOutputHelper.WriteLine("Running on Windows!");
        }
    }
}