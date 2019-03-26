using RabbitMq.Core.RabbitService;
using System;
using System.Threading;

namespace RabbitMq.Consumer.Receive
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitEventService mq = RabbitMq.Consumer.Config.ConfigHelper.CreateDefaultInstance();
            mq.Received(Write);
        }
        public static bool Write(string message)
        {
            Console.WriteLine("Received {0}", message);
            Thread.Sleep(2000);
            return new Random().Next(0, 1) == 0 ? false : true;
        }
    }
}
