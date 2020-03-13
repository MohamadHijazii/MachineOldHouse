using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MachineOldHouse
{
    public class Sender
    {
        private static Sender sender = null;
        private int id; //the id of the machine is in the sender since it is sigleton
        private string url;
        public AlertInfo info;
        public Scheduler scheduler { get; set; }

        //machines 
        public GlucoseMachine glucose;
        public HeartRateMachine heartRate;
        public BloodPressureMachine bloodPressure;
        public TemperatureMachine temperature;

        //private sigleton constructor
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

        public async Task<bool> sendAlert(Alert alert){ //send the alert and wait for something
            setInfo();
            alert.info = info;
            alert.text = $"Patient {id} has a medical danger!";
            string json = alert.toJson();   //json string from the alert
            Console.WriteLine(json);
            string res = await send(json);  //send the alert async
            Console.WriteLine($"Equal Hash ? {alert.GetHashCode().ToString().CompareTo(res) == 0}");
            //compare the hashes to insure that the alert has arrived correctly

            return alert.GetHashCode().ToString().CompareTo(res) == 0;
        }

        public void setInfo()
        {
            setGlucose(glucose.getLastVal());
            setHeartRate(heartRate.getLastVal());
            setbloodPressure(bloodPressure.getLastVal());
            setTemperature(temperature.getLastVal());
            //get the last calculated values to send
        }

        public void setGlucose(float f) { info.glucose = f; }
        public void setHeartRate(float f) { info.heartRate = f; }
        public void setbloodPressure(float f) { info.bloodPressure = f; }
        public void setTemperature(float f) { info.temperature = f; }


       async Task<string> send(string json)
        {
            string url = "https://localhost:44321/HandleAlert";
            Uri uri = new Uri(url);
           using(HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(json,Encoding.UTF8,"application/json");
                HttpResponseMessage res = await client.PostAsync(url, content);
                //the result of the responce is the hash of the alert
                return  res.Content.ReadAsStringAsync().Result;
            }
        }

        public async Task<string> sendCheck()
        {   //first step in the check, send the id
            string url = $"https://localhost:44321/HandleAlert/check/{id}";
            Uri uri = new Uri(url);
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = await client.GetAsync(url);
                string message = res.Content.ReadAsStringAsync().Result;
                //get the message from the server to hash
                return message;
            }
        }

        public async Task sendHash(string hash)
        {
            //after hashing the message we should the hash
            string url = $"https://localhost:44321/HandleAlert/check/{id}/{hash}";
            Uri uri = new Uri(url);
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = await client.GetAsync(url);
                string message = res.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Status: {res.StatusCode}, Message :{message}");
                //The returned message
            }
        }

    }
}
