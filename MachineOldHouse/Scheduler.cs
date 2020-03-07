using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace MachineOldHouse
{
    public class Scheduler 
    {
        string glucose, bloodpressure, heartrate, temperature;
        public IScheduler sched;
        public Scheduler(string glucose, string bloodpressure, string heartrate, string temperature)
        {
            this.glucose = glucose;
            this.bloodpressure = bloodpressure;
            this.heartrate = heartrate;
            this.temperature = temperature;
        }

      



        public async Task start()
        {
            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);


            sched = await factory.GetScheduler();
            await sched.Start();
            Console.WriteLine("start Scheduling");

            IJobDetail Glucosejob = JobBuilder.Create<GlucoseJob>()
                .WithIdentity("glucose", "group1")
                .Build();


            ITrigger Glucosetrigger = TriggerBuilder.Create()
                .WithIdentity("glucoset", "group1")
                 .WithCronSchedule(glucose)
                .ForJob("glucose", "group1")
                .Build();

            IJobDetail HeartRatejob = JobBuilder.Create<HeartRateJob>()
               .WithIdentity("HeartRate", "group2")
               .Build();


            ITrigger HeartRatetrigger = TriggerBuilder.Create()
                .WithIdentity("HeartRatet", "group2")
                .WithCronSchedule(bloodpressure)
                .ForJob("HeartRate", "group2")
                .Build();

            IJobDetail BloodPressurejob = JobBuilder.Create<BloodPressureJob>()
               .WithIdentity("BloodPressure", "group3")
               .Build();


            ITrigger BloodPressuretrigger = TriggerBuilder.Create()
                .WithIdentity("BloodPressuret", "group3")
                .WithCronSchedule(heartrate)
                .ForJob("BloodPressure", "group3")
                .Build();


            IJobDetail Temperaturejob = JobBuilder.Create<TemperatureJob>()
               .WithIdentity("Temperature", "group4")
               .Build();


            ITrigger Temperaturetrigger = TriggerBuilder.Create()
                .WithIdentity("Temperaturet", "group4")
                .WithCronSchedule(temperature)
                .ForJob("Temperature", "group4")
                .Build();



            await sched.ScheduleJob(Glucosejob, Glucosetrigger);
            await sched.ScheduleJob(HeartRatejob, HeartRatetrigger);
            await sched.ScheduleJob(BloodPressurejob, BloodPressuretrigger);
            await sched.ScheduleJob(Temperaturejob, Temperaturetrigger);

            
            Console.WriteLine("End Scheduling");
        }

        public void update() => sched.PauseAll();
        

    }
}
