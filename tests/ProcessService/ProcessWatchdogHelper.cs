namespace Kamen.Tests.ProcessService
{
    using Moq;
    using Kamen.TimeService;
    using Kamen.ProcessService;
    using Microsoft.Extensions.Logging;
    using Kamen.FileSystemService;
    using Microsoft.Extensions.Options;

    public class ProcessWatchdogHelper
    {
        public Mock<ILogger<ProcessWatchdog>> logger = new Mock<ILogger<ProcessWatchdog>>();
        public FakeTimeService time = new FakeTimeService();
        public Mock<IProcessService> process = new Mock<IProcessService>();
        public Mock<IFileSystemService> filesystem = new Mock<IFileSystemService>();
        public Mock<IOptions<ProcessWatchdogOptions>> options = new Mock<IOptions<ProcessWatchdogOptions>>();

        public ProcessWatchdog CreateInstance()
        {
            return new ProcessWatchdog(logger.Object, time, process.Object, filesystem.Object, options.Object);
        }
    }

}
