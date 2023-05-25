using FitLife.Consumer.Shared.Infrastructure.Services.File;

namespace FitLife.Consumer.Services.File
{
    public sealed class FileHelper : IFileHelper
    {
        private readonly string _projectRootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\"));

        public void CreateAtRootDirectory(string directory)
        {
            var downloadDirectory = Path.Combine(_projectRootPath, directory);
            Directory.CreateDirectory(downloadDirectory);
        }

        public string GetRootDirectoryFullPath(string directory)
        {
            return Path.Combine(_projectRootPath, directory);
        }
    }
}
