using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ai_scheduler.src;
using ai_scheduler.src.models;

namespace AiSchedulerTest
{
    public class ScheduleGeneratorTest
    {
        [Fact]
        public void EntryPoint_Test()
        {
            SchedulerGnerator schGen = new SchedulerGnerator();
            schGen.CreateMyCountrySchedule("Carpania", TestConstants.ResourceFileName, TestConstants.InitialWorldStateFileName, TestConstants.OutputScheduleFileName, 5, 5, 100);
        }
    }
}
