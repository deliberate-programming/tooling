using System.IO;
using dp.git;
using dp_record.adapters;

namespace dp_record
{
    static class Program
    {
        static void Main(string[] args){
            var cli = new CLI(args);
            var git = new GitRepoProvider(".");
            var screenshots = cli.TakeScreenshots 
                                ? new ScreenshotProvider(Path.Combine(git.RepoPath, "Screenshots")) 
                                : (IScreenshotProvider)new NoScreenshotsProvider(); 
            
            var app = new App(
                new ConsoleLog(), 
                git,
                screenshots);
            
            app.Run(cli);
        }
    }
}