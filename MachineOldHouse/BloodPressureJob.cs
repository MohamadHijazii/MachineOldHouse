using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MachineOldHouse
{
    public class BloodPressureJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            BloodPressureMachine bloodPressure = BloodPressureMachine.getInstance();
            bloodPressure.check();
            await Console.Out.WriteAsync("");
        }
    }
}
