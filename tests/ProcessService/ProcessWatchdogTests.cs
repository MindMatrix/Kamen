namespace Kamen.Tests.ProcessService
{
    using Moq;
    using Xunit;
    using Kamen.TimeService;
    using Kamen.ProcessService;
    using Microsoft.Extensions.Logging;
    using Kamen.FileSystemService;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;

    public class ProcessWatchdogTests
    {
        //[Fact]
        //public async void ProcessWatchdogFailsOnFileNotFound()
        //{
        //    var logger = new Mock<ILogger<ProcessWatchdog>>();
        //    var time = new FakeTimeService();
        //    var process = new Mock<IProcessService>();
        //    var filesystem = new Mock<IFileSystemService>();
        //    var options = new Mock<IOptions<ProcessWatchdogOptions>>();

        //    options.Setup(x => x.Value).Returns(new ProcessWatchdogOptions()
        //    {
        //        ProcessInfo = new ProcessInfo()
        //        {
        //            FileName = "text.exe",
        //            Path = @"C:\",
        //            WorkingPath = @"C:\"
        //        }
        //    });

        //    var watchdog = new ProcessWatchdog(logger.Object, time, process.Object, filesystem.Object, options.Object);

        //    var task = Task.Run(async () => {
        //        await Task.Yield();
        //        await watchdog.StartMonitoring();
        //    });

        //    Assert.True(task.Wait(1000));
        //}

        [Fact]
        public async void TransitionsFromInitingToFileNotFound()
        {
            var wdh = new ProcessWatchdogHelper();

            wdh.options.Setup(x => x.Value).Returns(new ProcessWatchdogOptions()
            {
                ProcessInfo = new ProcessInfo()
                {
                    FileName = "text.exe",
                    Path = @"C:\",
                    WorkingPath = @"C:\"
                }
            });

            var watchdog = wdh.CreateInstance();

            await watchdog.Step();

            Assert.Equal(ProcessWatchdogStates.FileNotFound, watchdog.State);
        }

        [Fact]
        public async void TransitionsFromInitingToStarting()
        {
            var wdh = new ProcessWatchdogHelper();
            wdh.options.Setup(x => x.Value).Returns(new ProcessWatchdogOptions()
            {
                ProcessInfo = new ProcessInfo()
                {
                    FileName = "test.exe",
                    Path = @"C:\",
                    WorkingPath = @"C:\"
                }
            });

            var path = @"C:\test.exe";

            wdh.filesystem.Setup(x => x.FileExists(path)).Returns(true);

            var watchdog = wdh.CreateInstance();

            await watchdog.Step();

            Assert.Equal(ProcessWatchdogStates.Starting, watchdog.State);
        }


        [Fact]
        public async void TransitionsFromStartingToRunning()
        {
            var wdh = new ProcessWatchdogHelper();
            wdh.options.Setup(x => x.Value).Returns(new ProcessWatchdogOptions()
            {
                ProcessInfo = new ProcessInfo()
                {
                    FileName = "test.exe",
                    Path = @"C:\",
                    WorkingPath = @"C:\"
                }
            });

            var path = @"C:\test.exe";
            var proc = new Mock<IProcess>();
            proc.Setup(x => x.Path).Returns(path);

            wdh.filesystem.Setup(x => x.FileExists(path)).Returns(true);
            wdh.process.Setup(x => x.GetByName("test.exe")).Returns(new IProcess[] { proc.Object });

            var watchdog = wdh.CreateInstance();
            await watchdog.Step();
            await watchdog.Step();

            Assert.Equal(ProcessWatchdogStates.Running, watchdog.State);
            Assert.False(watchdog.Failed);
        }

        [Fact]
        public async void RecoversFromCrashedProcess()
        {
            var wdh = new ProcessWatchdogHelper();
            wdh.options.Setup(x => x.Value).Returns(new ProcessWatchdogOptions()
            {
                ProcessInfo = new ProcessInfo()
                {
                    FileName = "test.exe",
                    Path = @"C:\",
                    WorkingPath = @"C:\"
                }
            });

            var path = @"C:\test.exe";
            var proc = new Mock<IProcess>();
            proc.Setup(x => x.Path).Returns(path);

            wdh.filesystem.Setup(x => x.FileExists(path)).Returns(true);
            wdh.process.Setup(x => x.GetByName("test.exe")).Returns(new IProcess[] { proc.Object });

            var watchdog = wdh.CreateInstance();
            await watchdog.Step();
            Assert.Equal(ProcessWatchdogStates.Running, watchdog.State);           

            wdh.process.Setup(x => x.GetByName("test.exe")).Returns(new IProcess[] {});
            await watchdog.Step();
            Assert.Equal(ProcessWatchdogStates.Initing, watchdog.State);

            wdh.process.Setup(x => x.GetByName("test.exe")).Returns(new IProcess[] { proc.Object });
            await watchdog.Step();
            Assert.Equal(ProcessWatchdogStates.Running, watchdog.State);
            Assert.False(watchdog.Failed);
        }

        [Fact]
        public async void RecoversFromMoreThanOneProcess()
        {
            var wdh = new ProcessWatchdogHelper();
            wdh.options.Setup(x => x.Value).Returns(new ProcessWatchdogOptions()
            {
                ProcessInfo = new ProcessInfo()
                {
                    FileName = "test.exe",
                    Path = @"C:\",
                    WorkingPath = @"C:\"
                }
            });

            var path = @"C:\test.exe";
            var proc = new Mock<IProcess>();
            proc.Setup(x => x.Path).Returns(path);

            wdh.filesystem.Setup(x => x.FileExists(path)).Returns(true);
            wdh.process.Setup(x => x.GetByName("test.exe")).Returns(new IProcess[] { proc.Object, proc.Object });

            var watchdog = wdh.CreateInstance();
            await watchdog.Step();
            Assert.Equal(ProcessWatchdogStates.MultipleProcesses, watchdog.State);

            wdh.process.Setup(x => x.GetByName("test.exe")).Returns(new IProcess[] { });
            await watchdog.Step();
            Assert.Equal(ProcessWatchdogStates.Starting, watchdog.State);

            wdh.process.Setup(x => x.GetByName("test.exe")).Returns(new IProcess[] { proc.Object });
            await watchdog.Step();
            Assert.Equal(ProcessWatchdogStates.Running, watchdog.State);
            Assert.False(watchdog.Failed);
        }

        [Fact]
        public async void RecoversFromMissingFile()
        {
            var wdh = new ProcessWatchdogHelper();
            wdh.options.Setup(x => x.Value).Returns(new ProcessWatchdogOptions()
            {
                ProcessInfo = new ProcessInfo()
                {
                    FileName = "test.exe",
                    Path = @"C:\",
                    WorkingPath = @"C:\"
                }
            });

            var path = @"C:\test.exe";
            var proc = new Mock<IProcess>();
            proc.Setup(x => x.Path).Returns(path);

            wdh.filesystem.Setup(x => x.FileExists(path)).Returns(false);
            wdh.process.Setup(x => x.GetByName("test.exe")).Returns(new IProcess[] { });

            var watchdog = wdh.CreateInstance();
            await watchdog.Step();
            Assert.Equal(ProcessWatchdogStates.FileNotFound, watchdog.State);

            wdh.filesystem.Setup(x => x.FileExists(path)).Returns(true);
            await watchdog.Step();
            Assert.Equal(ProcessWatchdogStates.Initing, watchdog.State);

            await watchdog.Step();
            Assert.Equal(ProcessWatchdogStates.Starting, watchdog.State);

            wdh.process.Setup(x => x.GetByName("test.exe")).Returns(new IProcess[] { proc.Object });
            await watchdog.Step();
            Assert.Equal(ProcessWatchdogStates.Running, watchdog.State);
            Assert.False(watchdog.Failed);
        }
    }
}
