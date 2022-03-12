using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ai_scheduler.src;
using ai_scheduler.src.models;

namespace AiSchedulerTest
{
    public class GameSchedulerTest
    {
        [Fact]
        public void EntryPoint_Test()
        {
            GameScheduler schGen = new GameScheduler();
            schGen.CreateMyCountrySchedule("Carpania", TestConstants.ResourceFileName, TestConstants.InitialWorldStateFileName, TestConstants.OutputScheduleFileName, 5, depthBound:7, 100);
        }
    }
}
