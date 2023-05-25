namespace FitLife.Consumer.Shared.Infrastructure.Services.File
{
    public interface IFileHelper
    {
        void CreateAtRootDirectory(string directory);

        string GetRootDirectoryFullPath(string directory);
    }
}
