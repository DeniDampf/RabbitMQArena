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
                channel.ExchangeDeclare("logs7even" + i.ToString(),"fanout"); 
                Console.WriteLine("Created Exchange");
            }
        }
    }
}