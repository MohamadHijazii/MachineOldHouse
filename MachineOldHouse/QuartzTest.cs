using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace MachineOldHouse
{
    public class QuartzTest : IJob
    {
        public static QuartzTest quartz {get;set;}
        public  QuartzTest() { }
        public async Task Execute(IJobExecutionContext context)
        {
            GlucoseMachine glucose = GlucoseMachine.getInstance();
            glucose.mesure();
            await Console.Out.WriteLineAsync("");
        }
    }
}
