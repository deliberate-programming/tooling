using System;
using System.IO;
using LibGit2Sharp;

namespace dp.git.tests.utilities
{
    class TempRepo : IDisposable
    {
        public TempRepo(string path) {
            Path = System.IO.Path.GetFullPath(path);
            Repository.Init(Path);
            Repo = new Repository(Path);
        }

        public string Path { get; }

        public Repository Repo { get; }

        public void Dispose() {
            Directory.Delete(Path, true);
            Repo.Dispose();
        }
    }
}