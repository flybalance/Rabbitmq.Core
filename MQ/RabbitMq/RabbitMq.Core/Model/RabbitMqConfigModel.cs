namespace RabbitMq.Core.Model
{
    using Common.AttributeHelper.Validates.ModelValidate;
    using System.ComponentModel;

    /// <summary>
    /// 消息队列的配置信息
    /// </summary>
    public class RabbitMqConfigModel
    {
        #region host

        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public string IP { get; set; } = "127.0.0.1";

        /// <summary>
        /// 服务器端口，默认是 5672
        /// </summary>
        public int Port { get; set; } = 5672;

        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserName { get; set; } = "guest";

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; } = "guest";

        /// <summary>
        /// 虚拟主机名称
        /// </summary>
        public string VirtualHost { get; set; } = "";

        #endregion host

        #region Queue

        [Required]
        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// 是否持久化该队列
        /// </summary>
        public bool DurableQueue { get; set; } = true;

        #endregion Queue

        #region exchange

        [Required]
        /// <summary>
        /// 路由名称
        /// </summary>
        public string ExchangeName { get; set; }

        /// <summary>
        /// 路由的类型枚举
        /// </summary>
        public ExchangeTypeEnum ExchangeType { get; set; }

        [Required]
        /// <summary>
        /// 路由的关键字
        /// </summary>
        public string RoutingKey { get; set; }

        #endregion exchange

        #region message

        /// <summary>
        /// 是否持久化队列中的消息
        /// </summary>
        public bool DurableMessage { get; set; } = true;

        #endregion message
    }
}