using System;

namespace dp_record.adapters
{
    class CLI
    {
        private const int DEFAULT_INTERVAL_SECONDS = 60;
        
        public CLI(string[] args)
        {
            if (args.Length < 1) args = new[] {DEFAULT_INTERVAL_SECONDS.ToString()};
            
            if (int.TryParse(args[0], out var intervalSeconds) is false) {
                Console.Error.WriteLine($"Missing an integer parameter for interval seconds! Found instead: '{args[0]}'");
                Environment.Exit(1);
            }
            this.IntervalSeconds = intervalSeconds;
        }

        public int IntervalSeconds { get; }
    }
}