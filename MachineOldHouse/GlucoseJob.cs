using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MachineOldHouse
{
    public class GlucoseJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            GlucoseMachine glucose = GlucoseMachine.getInstance();
            glucose.check();
            await Console.Out.WriteAsync("");
        }
    }
}
