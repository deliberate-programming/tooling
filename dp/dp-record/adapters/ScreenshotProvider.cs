using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace dp_record.adapters
{
    /*
     * Taking screenshots:
     *     - OSX: http://osxdaily.com/2011/08/11/take-screen-shots-terminal-mac-os-x/
     *     - Win: https://nircmd.nirsoft.net/savescreenshot.html, nircmdc.exe is included with dp-record
     */
    public interface IScreenshotProvider {
        void Capture();
    }

    
    public class NoScreenshotsProvider : IScreenshotProvider {
        public void Capture() { }
    }
    
    
    public class ScreenshotProvider : IScreenshotProvider
    {
        private readonly string _path;

        private readonly Dictionary<OSPlatform, (string executable, string arguments)> _services =
            new Dictionary<OSPlatform, (string executable, string arguments)> {
                {OSPlatform.OSX, ("screencapture", "-C -m -x -t jpg \"{0}\"")},
                {OSPlatform.Windows, ("$dp-recorder/win/nircmdc.exe", "savescreenshot \"{0}\"")},
                {OSPlatform.Linux, ("", "")}
            };


        public ScreenshotProvider(string path) {
            _path = path;
            if (Directory.Exists(_path) is false)
                Directory.CreateDirectory(_path);
        }


        public void Capture() {
            var service = new[] {OSPlatform.OSX, OSPlatform.Linux, OSPlatform.Windows}
                            .Where(RuntimeInformation.IsOSPlatform)
                            .Select(platform => _services[platform])
                            .First();
            if (service.executable == "") return;

            var executable = service.executable.Replace("$dp-recorder", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            var filename = $"{DateTime.Now:yyyyMMdd-HHmmss}.jpg";
            var filepath = Path.Combine(_path, filename);
            var arguments = string.Format(service.arguments, filepath);
            
            Process.Start(executable, arguments)
                   .WaitForExit();
        }
    }
}