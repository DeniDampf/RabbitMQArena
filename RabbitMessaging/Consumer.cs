
    using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMessaging
{   
    public class Consumer
    { 
        IConnection _conn;
        IModel _channel;

        string dummy = "";
        public void Subscribe(string exchangeName,string consumerIdentifier,string dum)
        {

            dummy = dum;
            ConnectionFactory factory = new ConnectionFactory();
            
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.VirtualHost = "/";
            factory.HostName = "localhost";
                    
            _conn = factory.CreateConnection();

            _channel = _conn.CreateModel();

            string queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(queue:queueName,exchange:exchangeName, routingKey:"");
            Console.WriteLine("Register listener on: " + queueName);


            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(consumerIdentifier + ": " + dum + ": " + message);
            };
            _channel.BasicConsume(queue: queueName,
                                    autoAck: true,
                                    consumer: consumer);

        }

    }
}