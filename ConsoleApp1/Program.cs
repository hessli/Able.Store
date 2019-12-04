using Able.Store.Infrastructure.Queue.Rabbit;
using Able.Store.Infrastructure.Queue.Rabbit.Consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {


            //var factory = new RabbitConnectionFactory(new RabbitConnectOptions
            //{
            //    Account = "username",
            //    PassWord = "password",
            //    Host = "192.168.229.131",
            //    Port = 5672,

            //});

            //RabbitSimpleConsume rabbitMQClient = new RabbitSimpleConsume(factory);

            //rabbitMQClient.OnConsumeHandler += RabbitMQClient_OnConsumeHandler;

            //rabbitMQClient.Consume(new RabbitClientOption
            //{
            //    AutoDelete = false,
            //    Exclusive = false,
            //    QueueName = "Hello",
            //    Durable = true,
            //    RoutingKey = "Hello",
            //    ChangeType = RabbitExchangeType.Direct,
            //    ExchangeName = "Hello",
            //    ConsumerTag = "Hello"

            //});
            Console.ReadLine();
        }
        //private static void RabbitMQClient_OnConsumeHandler(ConsumeArgs data)
        //{
        //    var value = data.GetParamter<string>();
        //    Console.Write(value);
        //}
    }
}
