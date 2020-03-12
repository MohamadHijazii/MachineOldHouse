using System;
using System.Collections.Generic;
using System.Text;

namespace MachineOldHouse
{
    public class BloodPressureMachine : Machine
    {
        public static BloodPressureMachine instance = null;

        public BloodPressureMachine(float min, float max) : base(min, max) {
            last_value = 100;
            instance = this;
        }

        public static BloodPressureMachine getInstance()
        {
            return instance;
        }

        public override float mesure()
        {
            int inc = rand.Next(-4,4);
            
            last_value += inc;
            Console.WriteLine($"Mesure Blood Pressure at {DateTime.Now} : {last_value}");
            return last_value;
        }
    }
}
