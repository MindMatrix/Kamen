namespace Kamen.TimeService
{
    using System;
    using System.Threading.Tasks;

    public class FakeTimeService : ITimeService
    {
        private DateTime _currentUtcDateTime;

        public DateTime Now => _currentUtcDateTime.ToLocalTime();
        public DateTime UtcNow => _currentUtcDateTime;

        public FakeTimeService() : this(DateTime.UtcNow) { }
        public FakeTimeService(DateTime startUtcDateTime) => _currentUtcDateTime = startUtcDateTime;

        public void AdvanceDateTime(int ms) => AdvanceDateTime(TimeSpan.FromMilliseconds(ms));
        public void AdvanceDateTime(TimeSpan span) => _currentUtcDateTime += span;

        public Task Delay(int ms) => Task.Run(() => AdvanceDateTime(ms));
    }
}
