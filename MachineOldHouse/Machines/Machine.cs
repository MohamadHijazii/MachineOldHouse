using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MachineOldHouse
{
    public abstract class Machine
    {
        public Random rand = new Random();
        public float []tolerance;
        public int battery;
        public float last_value;
        public Sender sender = Sender.GetSender();
        public Machine(float min,float max)
        {
            

            tolerance = new float[2];
            tolerance[0] = min;
            tolerance[1] = max;
            battery = 100;
        }

        public bool CompareTolerance(float value)  //it returns false if every think is okay
        {
            return value <= tolerance[0] || value >= tolerance[1];
        }

        public abstract float mesure();
        public Alert createAlert()
        {
            
            Alert alert = new Alert("Alert ", alertType.Danger, DateTime.Now);

            return alert;
        }

        public async void notify(Alert alert)
        {
            bool b = await sender.sendAlert(alert);
            Console.WriteLine( b ?  "ALERT ARRIVED" : "START ALARM IMMEDIATELLY, REQUEST DID NOT ARRIVED TO THE SERVER");
            
        }

        public void check()
        {
                float value = mesure();
                if (CompareTolerance(value))
                {
                    Console.WriteLine("-----------------");
                    Alert alert = createAlert();
                    Console.WriteLine("Alert should be sent");
                    notify(alert);
                    sender.scheduler.update();
                }
        }

        public float getLastVal() { return last_value; }

    }
}
