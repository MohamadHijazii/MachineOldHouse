using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MachineOldHouse
{
    class HeartRateJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            HeartRateMachine heartRate = HeartRateMachine.getInstance();
            heartRate.check();
            await Console.Out.WriteAsync("");
        }
    }
}
