namespace Kamen.Tests.TimeService
{
    using System;
    using Kamen.TimeService;
    using Xunit;

    public class TimeServiceTests
    {
        [Fact]
        public void FakeTimeServiceAdvancesTime()
        {
            var baseTime = new DateTime(0, DateTimeKind.Utc);
            var fakeTimeService = new FakeTimeService(baseTime);

            fakeTimeService.AdvanceDateTime(1000);

            Assert.Equal(baseTime + TimeSpan.FromMilliseconds(1000), fakeTimeService.UtcNow);
        }

        [Fact]
        public async void FakeTimeServiceAdvancesOnDelay()
        {
            var baseTime = new DateTime(0, DateTimeKind.Utc);
            var fakeTimeService = new FakeTimeService(baseTime);

            await fakeTimeService.Delay(1000);

            Assert.Equal(baseTime + TimeSpan.FromMilliseconds(1000), fakeTimeService.UtcNow);
        }
    }
}
