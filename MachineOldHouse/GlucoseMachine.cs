using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace MachineOldHouse
{
    public class GlucoseMachine : Machine
    {
        public static GlucoseMachine instance = null;

        public GlucoseMachine(float min,float max) : base(min,max)
        {
            last_value = 96;
            instance = this;
        }

        public static GlucoseMachine getInstance()
        {
            return instance;
        }
        

        public override float mesure()
        {
            double inc = rand.NextDouble();
            inc -= 0.5;
            last_value += (float)inc;
            Console.WriteLine($"Mesure Glucose at {DateTime.Now} : {last_value}");
            return last_value;
        }


    }
}
