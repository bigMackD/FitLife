using System;
using System.IO;
using FitLife.Shared.Infrastructure.Helpers;

namespace FitLife.Infrastructure.Helpers
{
    public sealed class FileHelper : IFileHelper
    {
        private readonly string _projectRootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\"));

        public string GetRootDirectoryFullPath(string directory)
        {
            return Path.Combine(_projectRootPath, directory);
        }
    }
}
