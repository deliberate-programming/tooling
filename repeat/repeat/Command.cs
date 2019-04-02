using System;
using System.Diagnostics;

namespace repeat
{
    class Command
    {
        private readonly string _command;
        private readonly string _parameters;
        
        public Command(string command, string parameters) {
            _command = command;
            _parameters = parameters;
        }


        public void Execute() => Execute(_parameters);
        public void Execute(string parameters) {
            var pi = new ProcessStartInfo {
                FileName = _command, 
                Arguments = parameters
            };
            pi.CreateNoWindow = true;
            pi.UseShellExecute = false;
            pi.RedirectStandardOutput = true;

            var p = new Process {StartInfo = pi};
            p.Start();
            p.WaitForExit();

            var output = p.StandardOutput.ReadToEnd();
            Console.WriteLine($"{output}");
            p.Close();
        }
    }
}