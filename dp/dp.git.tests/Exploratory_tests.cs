using System;
using LibGit2Sharp;
using Xunit;
using Xunit.Abstractions;

namespace dp.git.tests
{
    public class Exploratory_tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Exploratory_tests(ITestOutputHelper testOutputHelper) { _testOutputHelper = testOutputHelper; }

        
        
        [Fact]
        public void Status()
        {
            using (var repo = new Repository("/Users/ralfw/Projects/07 deliberate programming/tooling.github"))
            {
                foreach (var item in repo.RetrieveStatus(new LibGit2Sharp.StatusOptions()))
                {
                    _testOutputHelper.WriteLine($"{item.State} {item.FilePath}");
                }
            }
        }
    }
}