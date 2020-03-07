using System;
using System.Collections.Generic;
using System.Text;

namespace MachineOldHouse
{
    public class Sender
    {
        private static Sender sender = null;
        private int id;
        private string url;
        public AlertInfo info;
        public Scheduler scheduler { get; set; }

        public GlucoseMachine glucose;
        public HeartRateMachine heartRate;
        public BloodPressureMachine bloodPressure;
        public TemperatureMachine temperature;
        private Sender()
        {
            url = "";
            info = new AlertInfo();

        }

        public void setID(int id) => this.id = id;

        public static Sender GetSender()
        {
            if(sender == null)
            {
                sender = new Sender();
            }
            return sender;
        }

        public bool sendAlert(Alert alert){ //send the alert and wait for something
            setInfo();
            alert.info = info;
            alert.text = $"Patient {id} has a medical danger!";
            string json = alert.toJson();
            Console.WriteLine(json);

            //send the alert &  receive the response
            //compare the expected waiting
            //return true or false

            return false;
        }

        public void setInfo()
        {
            setGlucose(glucose.getLastVal());
            setHeartRate(heartRate.getLastVal());
            setbloodPressure(bloodPressure.getLastVal());
            setTemperature(temperature.getLastVal());
        }

        public void setGlucose(float f) { info.glucose = f; }
        public void setHeartRate(float f) { info.heartRate = f; }
        public void setbloodPressure(float f) { info.bloodPressure = f; }
        public void setTemperature(float f) { info.temperature = f; }


    }
}
