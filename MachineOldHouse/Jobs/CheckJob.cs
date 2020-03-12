using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MachineOldHouse
{
    public class CheckJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Sender sender = Sender.GetSender();
            string s = await sender.sendCheck();
            string hash = SecurityHelper.getHash(s);
            await sender.sendHash(hash);
        }
    }
}
