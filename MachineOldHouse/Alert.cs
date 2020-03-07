using Newtonsoft.Json.Linq;
using System;

using System.Collections.Generic;
using System.Text;


namespace MachineOldHouse
{
    public class Alert
    {        
        public string text;
        alertType type;
        DateTime time;
        
        public AlertInfo info;

        public Alert(string text, alertType type, DateTime time)
        {
            this.text = text;
            this.type = type;
            this.time = time;
        }

        public string toJson()
        {
            JObject obj = new JObject();
            return JObject.FromObject(this).ToString();
        }

        public override string ToString()
        {
            return "Alert:  " + text + "\n" + info;
        } 

    }

    public class AlertInfo
    {
        public float glucose;
        public float heartRate;
        public float bloodPressure;
        public float temperature;

        public AlertInfo() { }

        public string toJson()
        {
            JObject obj = new JObject();
            return JObject.FromObject(this).ToString();
        }

        public AlertInfo(float glucose, float heartRate, float bloodPressure, float temperature)
        {
            this.glucose = glucose;
            this.heartRate = heartRate;
            this.bloodPressure = bloodPressure;
            this.temperature = temperature;
        }

        public override string ToString()
        {
            return "Glucose: " + glucose + "\nHeart Rate: " + heartRate + "\nBlood Pressure: " + bloodPressure + "\nTemperature: " + temperature + "\n";
        }
    }

    public enum alertType
    {
        Danger,
        Battery,
        Check
    }
}
