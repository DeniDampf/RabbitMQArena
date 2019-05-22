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
            for(int i =0; i< 1; i++)
            {
                channel.ExchangeDeclare("logs7even","fanout"); 
                Console.WriteLine("Created Exchange");
            }
        }
    }
}