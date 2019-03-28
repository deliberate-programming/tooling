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

            var p = new Process {StartInfo = pi};
            p.Start();
        }
    }
}