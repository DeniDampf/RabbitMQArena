
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
            // var consumer = new EventingBasicConsumer(channel);
            // consumer.Received += (model, ea) =>
            // {
            //     var body = ea.Body;
            //     var message = Encoding.UTF8.GetString(body);
            //     Console.WriteLine(" [x] Received {0}", message);

            //     int dots = message.Split('.').Length - 1;
            //     Thread.Sleep(dots * 1000);

            //     Console.WriteLine(" [x] Done");
            // };
            // channel.BasicConsume(queue: qeueuName, autoAck: true, consumer: consumer);

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