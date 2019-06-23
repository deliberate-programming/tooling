using System;
using System.IO;
using dp.git.tests.utilities;
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
            using(var sut = new GitRepoProvider(".")) {
                var result = sut.RepoPath;
                Assert.True(Directory.Exists(Path.Combine(result, ".git")));
            }
        }


        [Fact]
        public void Staging_changes_and_checking_status()
        {
            var repoPath = "stagingchanges";
            using (new TempRepo(repoPath))
            {
                var sut = new GitRepoProvider(repoPath);

                var status = sut.Status;
                Assert.Equal((0,0,0), status);
                
                File.WriteAllText(repoPath + "/a.txt", "a");
                status = sut.Status;
                Assert.Equal((0,1,0), status);
                
                File.Delete(repoPath + "/a.txt");
                status = sut.Status;
                Assert.Equal((0,0,0), status);
                
                File.WriteAllText(repoPath + "/ab.txt", "ab");
                status = sut.Status;
                Assert.Equal((0,1,0), status);
                
                sut.StageAllChanges();
                status = sut.Status;
                Assert.Equal((0,1,0), status);
                
                File.Delete(repoPath + "/ab.txt");
                status = sut.Status;
                Assert.Equal((0,0,1), status);
            }
        }


        [Fact]
        public void Commit() {
            var repoPath = "commit";
            using (new TempRepo(repoPath))
            {
                var sut = new GitRepoProvider(repoPath);

                var status = sut.Status;
                Assert.Equal((0,0,0), status);
                
                File.WriteAllText(repoPath + "/a.txt", "a");
                status = sut.Status;
                Assert.Equal((0,1,0), status);
                
                sut.StageAllChanges();
                status = sut.Status;
                Assert.Equal((0,1,0), status);

                var sha = sut.Commit("committing a.txt");
                _testOutputHelper.WriteLine($"Commit SHA: {sha}");
                
                status = sut.Status;
                Assert.Equal((0,0,0), status);
            }
        }
    }
}