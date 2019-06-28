using System;
using System.Linq;

namespace dp_record.adapters
{
    class CLI
    {
        private const int DEFAULT_INTERVAL_SECONDS = 60;
        
        public CLI(string[] args)
        {
            if (HasOption("help")) {
                Console.WriteLine($"Usage: dp-record [<interval seconds>] [-noscreenshots]");
                Environment.Exit(1);
            }

            IntervalSeconds = GetIntervalSeconds();
            TakeScreenshots = HasOption("noscreenshots") is false;
            

            int GetIntervalSeconds() {
                foreach(var a in args) {
                    if (int.TryParse(a, out var intervalSeconds))
                        return intervalSeconds;
                }
                return DEFAULT_INTERVAL_SECONDS;
            }

            bool HasOption(string option)
                => args.Any(a => a.Equals("-" + option, StringComparison.InvariantCultureIgnoreCase)) ||
                   args.Any(a => a.Equals("--" + option, StringComparison.InvariantCultureIgnoreCase)) ||
                   args.Any(a => a.Equals("/" + option, StringComparison.InvariantCultureIgnoreCase));
        }

        
        public int IntervalSeconds { get; }
        
        public bool TakeScreenshots { get; }


    }
}