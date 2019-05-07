using System;
using RabbitMQ.Client;
using System.Text;

namespace RabbitBuilders
{

    public class QueueBuilder
    {
        public QueueBuilder()
        {
            
        }

        public void doWork(IModel channel)
        {
                 channel.QueueDeclare(queue: "hello1",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                channel.QueueDeclare(queue: "hello2",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }
    }
}