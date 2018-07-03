namespace Kamen.ProcessService
{
    public class ProcessWatchdogOptions
    {
        public ProcessInfo ProcessInfo;

        public int RestartInterval = 30000;
        public int WarmupInterval = 10000;
        public int CheckInterval = 10000;
        public int StopInterval = 10000;
    }
}
