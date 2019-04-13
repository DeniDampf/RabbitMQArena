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
            factory.Port = 8080;
        
            IConnection conn = factory.CreateConnection();


        }
    }
}
