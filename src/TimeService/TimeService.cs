namespace Kamen.TimeService
{
    using System;
    using System.Threading.Tasks;

    public class TimeService : ITimeService
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;

        public Task Delay(int ms)
        {
            return Task.Delay(ms);
        }
    }
}
