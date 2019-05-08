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
            for(int i =0; i< 10; i++)
            {
                                 channel.QueueDeclare(queue: "WorkingQueue_" + i.ToString().PadLeft(2,'0') ,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            }
        }
    }
}