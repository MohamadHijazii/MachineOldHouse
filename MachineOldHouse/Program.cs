using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using NCrontab;
using Quartz;
using Quartz.Impl;

namespace MachineOldHouse
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            Sender sender = Sender.GetSender();
            int machine_id = 5;
            GlucoseMachine glucose = new GlucoseMachine(90, 120);
            HeartRateMachine heartRate = new HeartRateMachine(60, 100);
            BloodPressureMachine bloodPressure = new BloodPressureMachine(80, 120);
            TemperatureMachine temperature = new TemperatureMachine(36.8f, 37.9f);
            //Console.WriteLine("Enter Machine's id: ");
            //machine_id = Convert.ToInt32(Console.ReadLine());
            sender.setID(machine_id);
            sender.glucose = glucose;
            sender.heartRate = heartRate;
            sender.bloodPressure = bloodPressure;
            sender.temperature = temperature;
            //Scheduler scheduler = new Scheduler(
            //    "0 0 6 ? * * *",    //glucose level every day at 6am
            //    "0 0/5 * ? * * *",    //heart rate every  minutes
            //    "0 0 * ? * * *",    //blood pressure every hour
            //    "0 0,30 * ? * * *"  //mesure temperature every 30 min
            //    );

            //testing
            Scheduler scheduler = new Scheduler(
                "0/3 * * ? * * *",    //glucose level every day at 6am
                "0/5 * * ? * * *",    //heart rate every  minutes
                "0/11 * * ? * * *",    //blood pressure every hour
                "0/13 * * ? * * *"  //mesure temperature every 30 min
                );
            sender.scheduler = scheduler;
            await scheduler.start();
            Console.WriteLine("start");

            Console.WriteLine(DateTime.Now);
            

            Console.ReadLine();

            //await scheduler.sched.ResumeAll();

            //Console.ReadLine();
        }
    }
}
