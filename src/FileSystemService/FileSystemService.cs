namespace Kamen.FileSystemService
{
    using System.IO;

    public class FileSystemService : IFileSystemService
    {
        public bool FileExists(string path) => File.Exists(path);
    }
}
