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
            int machine_id = 0;
            //Machine's tolerances
            GlucoseMachine glucose = new GlucoseMachine(90, 120);
            HeartRateMachine heartRate = new HeartRateMachine(60, 100);
            BloodPressureMachine bloodPressure = new BloodPressureMachine(80, 120);
            TemperatureMachine temperature = new TemperatureMachine(36.8f, 37.9f);

            //seting the id
            Console.WriteLine("Enter Machine's id: ");
            machine_id = Convert.ToInt32(Console.ReadLine());
            sender.setID(machine_id);
            //set machines to the sender
            sender.glucose = glucose;
            sender.heartRate = heartRate;
            sender.bloodPressure = bloodPressure;
            sender.temperature = temperature;

            /*
             * In the string bellow there is 7 parts seperated by spaces
             * the scheduler execute the correspanding task each time 
             * the time is constructed according to this string
             * part 1: seconds
             * part 2: minutes
             * part 3: hours
             * part 4: days of the month 1-31
             * part 5: month 1-12
             * part 6: days of the week 0-6
             * part 7 is optional represents the year
             */
            Scheduler Scheduler = new Scheduler(
                "0 0 6 ? * * *",    //glucose level every day at 6am
                "0 0/5 * ? * * *",    //heart rate every  minutes
                "0 0 * ? * * *",    //blood pressure every hour
                "0 0,30 * ? * * *",  //mesure temperature every 30 min
                 "* 0/10 * ? * * *"   //check every 10 min
                );

            //testing
            Scheduler testScheduler = new Scheduler(
                "0 * * ? * *",    //every one minute
                "0/5 * * ? * * *",    //heart rate every  5 sec 
                "0/10 * * ? * * *",    //blood pressure every 10 sec
                "0/13 * * ? * * *",  //mesure temperature every 13 sec
                "0/10 * * ? * * *"  //check every 10 sec
                );
            //we use the test one
            sender.scheduler = testScheduler;
            await testScheduler.start();    //start the scheduling
            Console.WriteLine("start");

            Console.WriteLine(DateTime.Now);
            

            Console.ReadLine();
        }
    }
}
