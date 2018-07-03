namespace Kamen.TimeService
{
    using System;
    using System.Threading.Tasks;

    public interface ITimeService
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
        Task Delay(int ms);
    }
}
