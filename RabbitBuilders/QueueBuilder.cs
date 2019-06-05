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
                createQueue(channel, "WorkingQueue_" + i.ToString().PadLeft(2,'0'));                                                              

            }
        }

        public void BindToExchange(IModel channel, string exchangeName,string QueueName)
        {
            channel.QueueBind(queue: QueueName  ,
                                 exchange: exchangeName,
                                 routingKey: "");         
        }

        public void createQueue(IModel channel, string queueName)
        {
                                 channel.QueueDeclare(queue: queueName ,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }
    }
}