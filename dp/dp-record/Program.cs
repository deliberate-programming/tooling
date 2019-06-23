using dp.git;
using dp_record.adapters;

namespace dp_record
{
    class Program
    {
        static void Main(string[] args) {
            var app = new App(
                new ConsoleLog(), 
                new GitRepoProvider("."));
            
            app.Run(args);
        }
    }
}