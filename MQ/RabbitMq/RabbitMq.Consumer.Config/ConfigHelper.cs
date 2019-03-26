using RabbitMq.Core.Model;
using RabbitMq.Core.RabbitService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Consumer.Config
{
    public class ConfigHelper
    {
        public static RabbitEventService CreateDefaultInstance()
        {
            return new RabbitEventService(new RabbitMqConfigModel()
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
        }
    }
}
