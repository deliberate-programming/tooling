using System;
using System.IO;
using System.Linq;
using LibGit2Sharp;

namespace dp.git
{
    public class GitRepoProvider: IDisposable
    {
        private readonly string _repoPath;
        private readonly Repository _repo;
        
        public GitRepoProvider(string workingDirectory) {
            _repoPath = Find_repo_root(workingDirectory);
            _repo = new Repository(_repoPath);
        }


        public string RepoPath => _repoPath;


        public void StageAllChanges() {
            Commands.Stage(_repo, "*");
        }


        public string Commit(string message, string authorName, string authorEmail) {
            var authorSig = new Signature(authorName, authorEmail, DateTime.Now);

            var commit = _repo.Commit(message, authorSig, authorSig);
            
            return commit.Sha;
        }


        public (int Changed, int Added, int Deleted, int Other) Status
        {
            get {
                (int Modified, int Added, int Deleted, int Other) status = (0, 0, 0, 0);
                
                foreach (var item in _repo.RetrieveStatus(new StatusOptions()).Where(i => i.State != FileStatus.Ignored)) {
                    /*
                     * Items can be in multiple states at the same time, eg NewInIndex & ModifiedInWorkdir.
                     * To get the count right in each bin the order of state checks has to be:
                     * 1. check for deletion, 2. check for addition, 3. check for modification!
                     * Only that way deleted in workdir overrides new in index, and new in index overrides modified in workdir.
                     */
                    if (item.State.HasFlag(FileStatus.DeletedFromIndex) ||
                        item.State.HasFlag(FileStatus.DeletedFromWorkdir))
                        status.Deleted += 1;
                    else if (item.State.HasFlag(FileStatus.NewInIndex) || 
                             item.State.HasFlag(FileStatus.NewInWorkdir))
                        status.Added += 1;
                    else if (item.State.HasFlag(FileStatus.ModifiedInIndex) ||
                             item.State.HasFlag(FileStatus.ModifiedInWorkdir) ||
                             item.State.HasFlag(FileStatus.RenamedInIndex) ||
                             item.State.HasFlag(FileStatus.RenamedInWorkdir))
                        status.Modified += 1;
                    else
                        status.Other += 1;
                }
                
                return status;
            }
        }
        

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