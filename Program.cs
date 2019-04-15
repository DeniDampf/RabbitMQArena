using System;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQArena
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            ConnectionFactory factory = new ConnectionFactory();
            
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.VirtualHost = "/";
            factory.HostName = "localhost";
            //  factory.Port = 5672;
        
            IConnection conn = factory.CreateConnection();

            var channel = conn.CreateModel();

                 channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

Console.WriteLine("didi it");
        }
    }
}
