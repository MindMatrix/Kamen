namespace Kamen.ProcessService
{
    using System.Collections.Generic;

    public interface IProcessService
    {
        //IEnumerable<IProcess> GetAll();
        IEnumerable<IProcess> GetByName(string processName);
        void Run(ProcessInfo process);
    }
}
