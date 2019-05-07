using System;
using RabbitMQ.Client;
using System.Text;
using RabbitBuilders;

namespace RabbitMQArena
{
    class Program
    {
        static void Main(string[] args)
        {
            MainBuilder mBuilder = new MainBuilder();
            mBuilder.doWork();
        }
    }
}
