namespace Kamen.ProcessService
{
    using System.Collections.Generic;

    public class WindowsProcessService : IProcessService
    {
        public class WindowsProcess : IProcess
        {
            public int ID { get; protected set; }
            public string Path { get; protected set; }

            protected internal WindowsProcess(int id, string path)
            {
                ID = id;
                Path = path;
            }
        }

        public IEnumerable<IProcess> GetByName(string processName)
        {
            var name = System.IO.Path.GetFileNameWithoutExtension(processName);
            var processes = System.Diagnostics.Process.GetProcessesByName(name);
            foreach (var it in processes)
                yield return new WindowsProcess(it.Id, it.MainModule.FileName);
        }

        public void Run(ProcessInfo process)
        {
            var startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.Arguments = process.Args;
            startInfo.CreateNoWindow = !true;
            startInfo.UseShellExecute = !false;
            startInfo.WorkingDirectory = process.WorkingPath;
            startInfo.FileName = process.FullPath;

            var p = System.Diagnostics.Process.Start(startInfo);
        }
    }
}
