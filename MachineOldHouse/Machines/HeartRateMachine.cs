using System;
using System.Collections.Generic;
using System.Text;

namespace MachineOldHouse
{
    public class HeartRateMachine : Machine
    {
        int n = 0;

        public static HeartRateMachine instance = null;

        public HeartRateMachine(float min,float max) : base(min, max)
        {
            last_value = 70;
            instance = this;
        }

        public static HeartRateMachine getInstance()
        {
            return instance;
        }



        public override float mesure()
        {
            double inc = rand.NextDouble();
            inc -= 0.5;
            last_value += (float)inc;
            n++;
            //if(n == 4)
            //    last_value = 120;
            Console.WriteLine($"Mesure Heart Rate at {DateTime.Now} : {last_value}");
            return last_value;
        }

    }
}
