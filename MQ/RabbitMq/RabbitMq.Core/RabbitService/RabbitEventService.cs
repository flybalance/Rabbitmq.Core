using Newtonsoft.Json;
using RabbitMq.Core.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Core.RabbitService
{
    public class RabbitEventService : BaseService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public RabbitEventService(RabbitMqConfigModel config)
            : base(config)
        {
        }

        public override void Received(Func<string, bool> method)
        {
            base.Received(method);
        }

    }
}
