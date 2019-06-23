using System;
using System.IO;
using LibGit2Sharp;

namespace dp.git
{
    public class GitProvider: IDisposable
    {
        private readonly string _repoPath;
        private readonly Repository _repo;
        
        public GitProvider(string workingDirectory) {
            _repoPath = Find_repo_root(workingDirectory);
            _repo = new Repository(_repoPath);
        }


        public string RepoPath => _repoPath;
        
        
        

        public void Dispose() {
            _repo.Dispose();
        }


        private string Find_repo_root(string path) {
            path = Path.GetFullPath(path);
            if (Directory.Exists(Path.Combine(path, ".git")))
                return path;

            var parentDirectory = Directory.GetParent(path);
            return Find_repo_root(parentDirectory.FullName);
        }
    }


}