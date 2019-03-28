using System;

namespace repeat
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var config = new Config(args);
            Console.WriteLine($"Running {config.Command} with '{config.Parameters}' every {config.Delay}...");

            var cmd = new Command(config.Command, config.Parameters);

            var scheduler = new Scheduler(config.Delay, cmd);
            scheduler.Start();
            
            Console.WriteLine("Press Ctrl-C to stop...");
            Console.ReadLine();
        }
    }
}