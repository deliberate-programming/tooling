using System;

namespace dp_record.adapters
{
    class CLI
    {
        public CLI(string[] args) {
            if (args.Length < 1) {
                Console.Error.WriteLine("Usage: dp-record <recording interval seconds>");
                Environment.Exit(1);
            }
            if (int.TryParse(args[0], out var intervalSeconds) is false) {
                Console.Error.WriteLine($"Missing an integer parameter for interval seconds! Found: '{args[0]}'");
                Environment.Exit(1);
            }
            this.IntervalSeconds = intervalSeconds;
        }

        public int IntervalSeconds { get; }
    }
}