using System;
using RabbitMQ.Client;

namespace RabbitBuilders
{

    public class ExchangeBuilder
    {
        public ExchangeBuilder()
        {
            
        }

        public void doWork(IModel channel)
        {
            for(int i =0; i< 2; i++)
            {
                createExchange(channel, "logs7even" + i.ToString());
            }
        }

        public void createExchange(IModel channel,string exchangeName)
        {
            channel.ExchangeDeclare(exchangeName,"fanout"); 
            Console.WriteLine("Created Exchange");
        }
    }
}