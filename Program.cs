using System;
using RabbitMQ.Client;
using System.Text;
using RabbitBuilders;
using RabbitMessaging;

namespace RabbitMQArena
{
    class Program
    {
        static MainBuilder _mBuilder;
        static void Main(string[] args)
        {
            _mBuilder = new MainBuilder();
            _mBuilder.doWork();

            for(int i = 0; i < 10; i++)
            {
                putMessages("WorkingQueue_" + i.ToString().PadLeft(2,'0'));                
            } 

            GetMessaging getMessages = new GetMessaging();
            getMessages.GetMessage(_mBuilder.Channel,"WorkingQueue_03");
            
            Console.WriteLine("i am really finished");
           
        }
        public static void putMessages(string queueName)
        {
            PutMessaging messageService = new PutMessaging();
            for(int i = 0; i < 10; i++)
            {
                messageService.putMessage(_mBuilder.Channel,queueName);
            }             
        }
    }
}
