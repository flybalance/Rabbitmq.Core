using RabbitMq.Core.Model;
using RabbitMq.Core.RabbitService;
using System;

namespace RabbitMq.Consumer.Send
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //RabbitEventService mq = RabbitMq.Consumer.Config.ConfigHelper.CreateDefaultInstance();
            BaseService baseService = new BaseService(new Core.Model.RabbitMqConfigModel
            {
                IP = "192.168.20.133",
                UserName = "masong",
                Password = "123456",
                Port = 5672,
                VirtualHost = "/mysteel",
                QueueName = "rabbit5",
                ExchangeName = "exchange-rabbit5",
                ExchangeType = ExchangeTypeEnum.direct,
                RoutingKey = "routing-key-rabbit5"
            });

            string message = "Hello World!";//待发送的消息

            for (int i = 0; i < 10000000; i++)
            {
                string body = string.Format(" {0} Sent: {1}", i.ToString(), message);

                bool resutl = baseService.Send(body);

                Console.WriteLine(body);
            }

            Console.ReadLine();
        }
    }
}