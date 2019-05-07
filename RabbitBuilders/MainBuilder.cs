using System;
using RabbitMQ.Client;
using System.Text;

namespace RabbitBuilders
{

    public class MainBuilder
    {
        QueueBuilder _qBuilder;
        IConnection _conn;
        IModel _channel;

        public MainBuilder()
        {
            init();
        }

        public void init()
        {
            ConnectionFactory factory = new ConnectionFactory();
            
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.VirtualHost = "/";
            factory.HostName = "localhost";
                    
            _conn = factory.CreateConnection();

            _channel = _conn.CreateModel();
            _qBuilder = new QueueBuilder();
        }

        public void doWork()
        {
            _qBuilder.doWork(_channel);
        }
    }
}