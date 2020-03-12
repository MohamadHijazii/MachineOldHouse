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

        public async Task<bool> sendAlert(Alert alert){ //send the alert and wait for something
            setInfo();
            alert.info = info;
            alert.text = $"Patient {id} has a medical danger!";
            string json = alert.toJson();
            Console.WriteLine(json);
            string res = await send(json);
            //Console.WriteLine($"From machine {alert.GetHashCode()}");
            //Console.WriteLine($"From Server {res}");
            Console.WriteLine($"Equal Hash ? {alert.GetHashCode().ToString().CompareTo(res) == 0}");


            return alert.GetHashCode().ToString().CompareTo(res) == 0;
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


       async Task<string> send(string json)
        {
            string url = "https://localhost:44321/HandleAlert";
            Uri uri = new Uri(url);
           using(HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(json,Encoding.UTF8,"application/json");
                HttpResponseMessage res = await client.PostAsync(url, content);
                //Console.WriteLine($"Status code : {res.StatusCode}");
                //Console.WriteLine($"Returned string : {res.Content.ReadAsStringAsync().Result}");
                return  res.Content.ReadAsStringAsync().Result;
            }
        }

        public async Task<string> sendCheck()
        {
            string url = $"https://localhost:44321/HandleAlert/check/{id}";
            Uri uri = new Uri(url);
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = await client.GetAsync(url);
                string message = res.Content.ReadAsStringAsync().Result;
                return message;
            }
        }

        public async Task sendHash(string hash)
        {
            string url = $"https://localhost:44321/HandleAlert/check/{id}/{hash}";
            Uri uri = new Uri(url);
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = await client.GetAsync(url);
                string message = res.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Status: {res.StatusCode}, Message :{message}");
            }
        }

    }
}
