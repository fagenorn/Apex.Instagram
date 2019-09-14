using System;
using System.Threading.Tasks;

using Apex.Shared.Model;

using Xunit;

namespace Apex.Instagram.API.Tests
{
    public class UtilsTest
    {
        [Fact]
        public void Epoch_Test()
        {
            const double epochTime        = 1532344691d;
            const double epochTime2       = 91d;
            var          currentEpochTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            var          epoch            = new Epoch();
            Assert.True(epoch.Value - currentEpochTime <= 0.1);
            Assert.True(epoch.Value - currentEpochTime <= 15);

            epoch = new Epoch(epochTime);
            var epoch2 = new Epoch(epochTime2);

            Assert.Equal(1532344600d, (epoch - epoch2).Value);
        }

        [Fact]
        public async Task Last_Action_Test()
        {
            var wait   = TimeSpan.FromMilliseconds(10);
            var action = new LastAction(wait);

            Assert.True(action.Passed);
            action.Update();
            Assert.False(action.Passed);
            await Task.Delay(wait + TimeSpan.FromMilliseconds(5));
            Assert.True(action.Passed);


            action = new LastAction(wait, new Epoch());

            Assert.False(action.Passed);
            await Task.Delay(wait + TimeSpan.FromMilliseconds(5));
            Assert.True(action.Passed);
        }
    }
}