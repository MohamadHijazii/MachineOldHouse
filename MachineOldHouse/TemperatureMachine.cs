using System;
using System.Collections.Generic;
using System.Text;

namespace MachineOldHouse
{
    public class TemperatureMachine : Machine
    {
        public static TemperatureMachine instance = null;

        public TemperatureMachine(float min, float max) : base(min, max) {

            last_value = 37.5f;
            instance = this;

        }

        public static TemperatureMachine getInstance()
        {
            return instance;
        }



        public override float mesure()
        {
            double inc = rand.NextDouble();
            inc -= 0.5;
            inc /= 2;
            last_value += (float)inc;
            Console.WriteLine($"Mesure Temperature at {DateTime.Now} : {last_value}");
            return last_value;
        }
    }
}
