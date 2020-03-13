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

        public bool CompareTolerance(float value)
        {
            //it returns false if every think is okay
            return value <= tolerance[0] || value >= tolerance[1];
        }

        public abstract float mesure(); //each machine has its own way to measure

        public Alert createAlert()
        {
            //creation of the alert to send
            Alert alert = new Alert("Alert ", alertType.Danger, DateTime.Now);

            return alert;
        }

        public async void notify(Alert alert)
        {
            //notify the sender to send the alert
            bool b = await sender.sendAlert(alert);
            //In case the alert did not arrive to the server start an alarm
            Console.WriteLine( b ?  "ALERT ARRIVED" : "START ALARM IMMEDIATELLY, REQUEST DID NOT ARRIVED TO THE SERVER");
            
        }

        public void check()
        {
                float value = mesure();    //Machine measures the patient's <Glucose,...>
                if (CompareTolerance(value))//Compare the value with the tolerance
                {
                    //If there is a danger
                    Alert alert = createAlert();    //we create the alert
                    Console.WriteLine("Sending the alert ...");
                    notify(alert);  //Notify the observer (Sender)
                    sender.scheduler.update();  //pause the machine to avoid sending alerts
                }
        }

        public float getLastVal() { return last_value; }    //last value calculated by the machine

    }
}
