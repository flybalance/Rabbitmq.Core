﻿using RabbitMq.Core.Model;
using RabbitMq.Core.RabbitService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Receive.Bug
{
    class Program
    {
        static void Main(string[] args)
        {
            test();
        }

        #region MyRegion
        private static void test()
        {
            RabbitBasicService mq = new RabbitBasicService(new RabbitMqConfigModel()
            {
                IP = "127.0.0.1",
                UserName = "guest",
                Password = "guest",
                Port = 15672,
                VirtualHost = "kevinHost",
                DurableQueue = true,
                QueueName = "bug",
                ExchangeName = "ex1",
                ExchangeType = ExchangeTypeEnum.direct,
                DurableMessage = true,
                RoutingKey = "system.info"
            });

            //mq.GetExchange();
            //Console.Read();

            Program p = new Program();
            mq.Receive<string>(p.GetMessage);
        }
        public void GetMessage(string value)
        {
            try
            {
                Console.WriteLine("接受到数据：" + value);
            }
            catch (Exception)
            {

            }
        }
        #endregion
    }
}
