using Newtonsoft.Json;
using RabbitMq.Core.Model;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMq.Core.RabbitService
{
    public class RabbitBasicService : BaseService
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public RabbitBasicService(RabbitMqConfigModel config)
            : base(config)
        { }
        #endregion

        #region 方法
        #region 接受消息，使用Action进行处理
        /// <summary>
        /// 接受消息，使用Action进行处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        public void Receive<T>(Action<T> method)
        {
            try
            {
                #region 绑定队列和交换机
                //申明队列
                model.QueueDeclare(RabbitConfig.QueueName, RabbitConfig.DurableQueue, false, false, null);
                //使用路由
                if (!string.IsNullOrEmpty(RabbitConfig.ExchangeName))
                {
                    //申明路由
                    model.ExchangeDeclare(RabbitConfig.ExchangeName, RabbitConfig.ExchangeType.ToString(), RabbitConfig.DurableQueue);
                    //队列和交换机绑定
                    model.QueueBind(RabbitConfig.QueueName, RabbitConfig.ExchangeName, RabbitConfig.RoutingKey);
                }
                #endregion

                #region 业务
                //输入1，那如果接收一个消息，但是没有应答，则客户端不会收到下一个消息
                model.BasicQos(0, 1, false);
                //在队列上定义一个消费者
                var customer = new QueueingBasicConsumer(model);
                //var customer = new EventingBasicConsumer (channel);

                //消费队列，并设置应答模式为程序主动应答
                model.BasicConsume(RabbitConfig.QueueName, false, customer);

                while (true)
                {
                    //阻塞函数，获取队列中的消息
                    ProcessingResultsEnum processingResult = ProcessingResultsEnum.Retry;
                    ulong deliveryTag = 0;
                    try
                    {
                        Thread.Sleep(10);

                        var ea = customer.Queue.Dequeue();
                        deliveryTag = ea.DeliveryTag;
                        byte[] bytes = ea.Body;
                        string body = Encoding.UTF8.GetString(bytes);
                        T info = JsonConvert.DeserializeObject<T>(body);
                        method(info);
                        processingResult = ProcessingResultsEnum.Accept;
                    }
                    catch (Exception ex)
                    {
                        processingResult = ProcessingResultsEnum.Reject; //系统无法处理的错误
                    }
                    finally
                    {
                        switch (processingResult)
                        {
                            case ProcessingResultsEnum.Accept:
                                //回复确认处理成功
                                model.BasicAck(deliveryTag,
                                    false);//处理单挑信息
                                break;
                            case ProcessingResultsEnum.Retry:
                                //发生错误了，但是还可以重新提交给队列重新分配
                                model.BasicNack(deliveryTag, false, true);
                                break;
                            case ProcessingResultsEnum.Reject:
                                //发生严重错误，无法继续进行，这种情况应该写日志或者是发送消息通知管理员
                                model.BasicNack(deliveryTag, false, false);
                                //写日志
                                break;
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
        #endregion
    }
}
