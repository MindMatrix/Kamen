namespace Kamen.ProcessService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Kamen.FileSystemService;
    using Kamen.TimeService;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class ProcessWatchdog
    {
        protected ILogger<ProcessWatchdog> _logger;
        protected ITimeService _time;
        protected IProcessService _process;
        protected IFileSystemService _filesystem;
        protected ProcessWatchdogOptions _options;
        protected bool _isRunning = false;

        protected int _failCounter = 0;

        public bool Failed => _failCounter > 0;
        public bool IsRunning => _isRunning;

        public ILogger<ProcessWatchdog> Logger => _logger;
        public ITimeService TimeService => _time;
        public IProcessService ProcessService => _process;
        public IFileSystemService FileSystemService => _filesystem;
        public ProcessWatchdogOptions Options => _options;
        public ProcessWatchdogStates State { get; protected set; }

        public ProcessWatchdog(
            ILogger<ProcessWatchdog> logger,
            ITimeService time,
            IProcessService process,
            IFileSystemService filesystem,
            IOptions<ProcessWatchdogOptions> options)
        {
            _logger = logger;
            _time = time;
            _process = process;
            _filesystem = filesystem;
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            State = ProcessWatchdogStates.Initing;
        }

        public async Task Step()
        {
            var path = _options.ProcessInfo.FullPath;
            //var processWasRunning = false;

            if (State == ProcessWatchdogStates.Initing && !_filesystem.FileExists(path))
            {
                _logger.LogCritical($"Could not find file '{path}'");
                State = ProcessWatchdogStates.FileNotFound;
            }
            else if (State == ProcessWatchdogStates.FileNotFound)
            {
                _failCounter++;
                await _time.Delay(_options.RestartInterval);
                if (_filesystem.FileExists(path))
                    State = ProcessWatchdogStates.Initing;
            }
            else
            {
                //check if its already running
                var processes = GetProcesses();

                if (processes.Count == 0)
                {
                    if (State == ProcessWatchdogStates.Running)
                    {
                        _failCounter++;
                        _logger.LogError($"Process unexpectedly quit '{path}'.");
                        //await _time.Delay(_options.RestartInterval);
                        State = ProcessWatchdogStates.Initing;
                    }
                    else
                    {
                        if (State == ProcessWatchdogStates.Starting)
                        {
                            _failCounter++;
                            _logger.LogError($"Failed to start '{path}', trying again.");
                            //await _time.Delay(_options.RestartInterval);
                        }

                        State = ProcessWatchdogStates.Starting;

                        //needs started
                        _logger.LogInformation($"Starting process '{path}'");
                        _process.Run(_options.ProcessInfo);
                        await _time.Delay(_options.WarmupInterval);
                    }
                }
                else if (processes.Count == 1)
                {
                    //already running, monitor
                    if (State != ProcessWatchdogStates.Running)
                        _logger.LogInformation($"Found instance of '{path}'.");

                    _failCounter = 0;
                    State = ProcessWatchdogStates.Running;
                    await _time.Delay(_options.CheckInterval);
                }
                else
                {
                    if (State != ProcessWatchdogStates.MultipleProcesses)
                    {
                        //more than 1 running ?!?!
                        _logger.LogCritical($"More than one instance of '{path}', waiting for exit.");
                        //if (!await StopAll())
                        _failCounter++;
                    }

                    State = ProcessWatchdogStates.MultipleProcesses;
                    await _time.Delay(_options.RestartInterval);
                }
            }
        }

        public async Task StartMonitoring()
        {
            if (_isRunning)
                return;

            _isRunning = true;

            while (_isRunning)
            {
                await Step();
                await Task.Yield();
            }
        }

        private List<IProcess> GetProcesses()
        {
            return _process.GetByName(_options.ProcessInfo.FileName)
                            .Where(x => string.Compare(_options.ProcessInfo.FullPath, x.Path, true) == 0)
                            .ToList();
        }

        public void StopMonitoring(bool stopProcess)
        {
            _isRunning = false;
        }
    }
}
