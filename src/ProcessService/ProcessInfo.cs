namespace Kamen.ProcessService
{
    public class ProcessInfo
    {
        public string FileName;
        public string Path;
        public string WorkingPath;
        public string Args;
        public string FullPath => System.IO.Path.Combine(Path, FileName);
    }
}
