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
            Sender sender = Sender.GetSender(); //geting the singleton sender 
            string s = await sender.sendCheck();    //send the first check step
            string hash = SecurityHelper.getHash(s);    //hash the message
            await sender.sendHash(hash);    //second check connection step
        }
    }
}
