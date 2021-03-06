using System;
using RabbitMQ.Client;
using System.Text;

namespace RabbitBuilders
{

    public class MainBuilder
    {
        QueueBuilder _qBuilder;
        ExchangeBuilder _exBuilder;
        IConnection _conn;
        IModel _channel;

        public IModel Channel{
            get{
                return _channel;
            }
        }

        public MainBuilder()
        {
            init();
        }

        public IModel createNewChannel(string name)
        {
            ConnectionFactory factory = new ConnectionFactory();
            
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.VirtualHost = "/";
            factory.HostName = "localhost";
                    
            IConnection conn = factory.CreateConnection("MainBuilder: " + name);
            IModel channel = conn.CreateModel();

            return Channel;
        }


        public void init()
        {
            ConnectionFactory factory = new ConnectionFactory();
            
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.VirtualHost = "/";
            factory.HostName = "localhost";
                    
            _conn = factory.CreateConnection("MainBuilderConnection");

            _channel = _conn.CreateModel();
            _qBuilder = new QueueBuilder();
            _exBuilder = new ExchangeBuilder();
        }

        public void doWork()
        {
            _qBuilder.doWork(_channel);
            _exBuilder.doWork(_channel);
        }
    }
}