
    using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMessaging
{   
    public class GetMessaging
    { 
        public void GetMessage(IModel channel,string qeueuName)
        {
            BasicGetResult result = channel.BasicGet(qeueuName,false);
           
            IBasicProperties props = result.BasicProperties;
            Console.WriteLine("Delivery Tag: " + result.DeliveryTag.ToString());
            



            Console.WriteLine("Start waiting");
            Task.Delay(10000).Wait();

            channel.BasicAck(result.DeliveryTag,false);
            Console.WriteLine("Waited enough");

        }

    }
}