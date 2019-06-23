using System.IO;
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
        public void Explore()
        {
        }
    }
}