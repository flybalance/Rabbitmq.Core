using Newtonsoft.Json;
using RabbitMq.Core.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMq.Core.RabbitService
{
    /// <summary>
    /// 基类
    /// </summary>
    public class BaseService
    {
        /// <summary>
        /// 消费通道
        /// </summary>
        public static IModel model;

        /// <summary>
        /// 服务器配置
        /// </summary>
        public RabbitMqConfigModel RabbitConfig { get; set; }

        /// <summary>
        /// 连接对象
        /// </summary>
        private static IConnection _connection = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public BaseService(RabbitMqConfigModel config)
        {
            try
            {
                RabbitConfig = config;
                InitRabbit();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 初始化rabbit连接信息
        /// </summary>
        private void InitRabbit()
        {
            CreateConnection();
            CreateModel();
        }

        /// <summary>
        /// 创建连接
        /// </summary>
        private void CreateConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();
            // 服务器的端口
            connectionFactory.Port = RabbitConfig.Port;
            // 服务器ip
            connectionFactory.Endpoint = new AmqpTcpEndpoint(new Uri("amqp://" + RabbitConfig.IP + "/"));
            // 登录账户
            connectionFactory.UserName = RabbitConfig.UserName;
            connectionFactory.Password = RabbitConfig.Password;
            // 虚拟主机
            connectionFactory.VirtualHost = RabbitConfig.VirtualHost;
            connectionFactory.RequestedHeartbeat = 60;

            _connection = connectionFactory.CreateConnection();
        }

        /// <summary>
        /// 创建消费通道
        /// </summary>
        private void CreateModel()
        {
            // 创建连接成功后声明队列
            if (null == _connection && !_connection.IsOpen)
            {
                return;
            }

            model = _connection.CreateModel();
            if (!string.IsNullOrEmpty(RabbitConfig.ExchangeName) && !string.IsNullOrEmpty(RabbitConfig.RoutingKey))
            {
                // 使用自定义的路由
                model.ExchangeDeclare(RabbitConfig.ExchangeName, RabbitConfig.ExchangeType.ToString(), RabbitConfig.DurableMessage, false, null);
                model.QueueDeclare(RabbitConfig.QueueName, RabbitConfig.DurableQueue, false, false, null);
                // 队列和交换机绑定
                model.QueueBind(RabbitConfig.QueueName, RabbitConfig.ExchangeName, RabbitConfig.RoutingKey);
            }
            else
            {
                // 申明消息队列，且为可持久化的，如果队列的名称不存在，系统会自动创建，有的话不会覆盖
                model.QueueDeclare(RabbitConfig.QueueName, RabbitConfig.DurableQueue, false, false, null);
            }
        }

        /// <summary>
        /// 发送泛型消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Send<T>(T messageInfo)
        {
            if (null == messageInfo)
            {
                return false;
            }

            string value = JsonConvert.SerializeObject(messageInfo);

            return Send(value);
        }

        /// <summary>
        /// 发送消息，string类型
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool Send(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return false;
            }

            try
            {
                if (!_connection.IsOpen)
                {
                    InitRabbit();
                }

                byte[] bytes = Encoding.UTF8.GetBytes(message);

                IBasicProperties properties = model.CreateBasicProperties();
                // 支持可持久化数据
                properties.DeliveryMode = Convert.ToByte(RabbitConfig.DurableMessage ? 2 : 1);

                // 消息投递等待确认
                model.ConfirmSelect();

                if (!string.IsNullOrEmpty(RabbitConfig.ExchangeName) && !string.IsNullOrEmpty(RabbitConfig.RoutingKey))
                {
                    model.BasicPublish(RabbitConfig.ExchangeName, RabbitConfig.RoutingKey, properties, bytes);
                }
                else
                {
                    model.BasicPublish("", RabbitConfig.QueueName, properties, bytes);
                }

                return model.WaitForConfirms();
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 消费消息
        /// </summary>
        /// <param name="method"></param>
        public virtual void Received(Func<string, bool> method)
        {
            model.BasicQos(0, 1, false);

            EventingBasicConsumer consumer = new EventingBasicConsumer(model);

            // 设置为主动应答模式
            // 为false则服务端必须在收到客户端的回执（ack）后才能删除本条消息
            model.BasicConsume(RabbitConfig.QueueName, false, consumer);

            // 注册接收事件，一旦创建连接就去拉取消息
            consumer.Received += (m, ea) =>
            {
                // 塞函数，获取队列中的消息
                ProcessingResultsEnum processingResult = ProcessingResultsEnum.Retry;
                try
                {
                    var body = ea.Body;

                    string message = Encoding.UTF8.GetString(body);
                    method(message);
                    processingResult = ProcessingResultsEnum.Accept;
                }
                catch (Exception)
                {
                    // 系统无法处理的错误
                    processingResult = ProcessingResultsEnum.Reject;
                }
                finally
                {
                    switch (processingResult)
                    {
                        case ProcessingResultsEnum.Accept:
                            // 回复确认处理成功
                            // 处理单条信息
                            model.BasicAck(ea.DeliveryTag, false);
                            break;
                        case ProcessingResultsEnum.Retry:
                            // 发生错误了，但是还可以重新提交给队列重新分配
                            model.BasicNack(ea.DeliveryTag, false, true);
                            break;
                        case ProcessingResultsEnum.Reject:
                            //发生严重错误，无法继续进行，这种情况应该写日志或者是发送消息通知管理员
                            model.BasicNack(ea.DeliveryTag, false, false);
                            //写日志
                            break;
                        default:
                            break;
                    }
                }
            };

            // 重要
            // 否则断开连接后自动结束进程
            System.Threading.Thread.Sleep(-1);
        }
    }
}