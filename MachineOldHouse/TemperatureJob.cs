using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MachineOldHouse
{
    public class TemperatureJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            TemperatureMachine temperature = TemperatureMachine.getInstance();
            temperature.check();
            await Console.Out.WriteAsync("");
        }
    }
}
