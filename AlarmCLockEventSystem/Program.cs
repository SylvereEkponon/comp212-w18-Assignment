using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmCLockEventSystem
{
    public delegate void AlarmEventHandler(object sender, AlarmEventArgs e);
    
    public class AlarmEventArgs : EventArgs
    {
        public string Text { get; }

        public AlarmEventArgs(string text)
        {
            Text = text;
        }
    }

    public class AlarmEventConsumer
    {
        private string name;

        public AlarmEventConsumer(string name)
        {
            this.name = name;
        }

        public void AlarmRang(object sender,AlarmEventArgs e)
        {
            Console.WriteLine(name + " was called with " + e.Text);
        }
    }

    public class AlarmEventProducer
    {
        public event AlarmEventHandler Alarm;

        protected virtual void OnAlarm(AlarmEventArgs e)
        {
            AlarmEventHandler handler = Alarm;

            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        public void RunAlarm()
        {
            int count = 1;
            while (count <= 12)
            {
                System.Threading.Thread.Sleep(1000);
                //this raises the event
                OnAlarm(new AlarmEventArgs(count++.ToString() + " o'clock"));

            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //the event source/producer
            AlarmEventProducer producer = new AlarmEventProducer();

            //event listener/consumer
            AlarmEventConsumer naren = new AlarmEventConsumer("Narendra");
            AlarmEventConsumer arben = new AlarmEventConsumer("Arben");
            AlarmEventConsumer ilia = new AlarmEventConsumer("Ilia");

            //wire up the listener
            producer.Alarm += new AlarmEventHandler(naren.AlarmRang);
            producer.Alarm += new AlarmEventHandler(arben.AlarmRang);
            producer.Alarm += new AlarmEventHandler(ilia.AlarmRang);
            //uses an anonymous Consumer object
            producer.Alarm += new AlarmEventHandler(new AlarmEventConsumer("Yin Li").AlarmRang);

            //uses a lambda expression
            producer.Alarm += new AlarmEventHandler((sender, e) => Console.WriteLine("Lamdba expression was called with " + e.Text));

            //start the clock
            producer.RunAlarm();


        }
    }
}
