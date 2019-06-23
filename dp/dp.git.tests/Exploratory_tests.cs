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
        public void Init_and_act()
        {
            var repoPath = "myrepo";
            using (var trepo = new TempRepo(repoPath)) {
                File.WriteAllText(repoPath + "/a.txt", "a");
                PrintStatus(trepo.Repo);
                StageAllChanges(trepo.Repo);
                PrintStatus(trepo.Repo);
                File.WriteAllText(repoPath + "/a.txt", "ab");
                PrintStatus(trepo.Repo);
                StageAllChanges(trepo.Repo);
                PrintStatus(trepo.Repo);
                File.Delete(repoPath + "/a.txt");
                PrintStatus(trepo.Repo);
                StageAllChanges(trepo.Repo);
                PrintStatus(trepo.Repo);
            }


            void StageAllChanges(Repository repo)
            {
                Commands.Stage(repo, "*");
            }

            void PrintStatus(Repository repo)  {            
                _testOutputHelper.WriteLine("status of: {0}", repoPath);
                    foreach (var item in repo.RetrieveStatus(new LibGit2Sharp.StatusOptions()))
                    {
                        if (item.State != FileStatus.Ignored)
                            _testOutputHelper.WriteLine($"  {item.State} {item.FilePath}");
                    }
            }
        }
    }
}