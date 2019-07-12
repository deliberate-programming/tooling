using System;
using System.Linq;

namespace repeat
{
    //TODO: Rewrite Config in F# for easier parsing with a clearly defined syntax
    class Config
    {
        public Config(string[] args) {
            if (args.Length < 3) {
                Console.WriteLine("Usage: repeat.exe <delay seconds> <command to execute> <parameters>");
                Environment.Exit(1);
            }
            
            Delay = new TimeSpan(0,0,0,int.Parse(args[0]));
            Command = args[1];
            Parameters = string.Join(" ", args.Skip(2));
        }
        
        public TimeSpan Delay { get; }
        public string Command { get; }
        public string Parameters { get; }
    }
}