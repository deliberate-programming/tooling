using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace dp.git.tests
{
    public class GitProvider_tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public GitProvider_tests(ITestOutputHelper testOutputHelper) {  _testOutputHelper = testOutputHelper; }

        
        [Fact]
        public void Find_repo_root() {
            using(var sut = new GitProvider(".")) {
                var result = sut.RepoPath;
                Assert.True(Directory.Exists(Path.Combine(result, ".git")));
            }
        }
    }
}