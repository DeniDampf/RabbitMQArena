using System;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMessaging
{

    public class Messaging
    {
        public void putMessage(IModel channel,string qeueuName)
        {
            var message = getMessage(qeueuName);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: "",
                                routingKey: qeueuName,
                                basicProperties: properties,
                                body: body);
        }

        private string getMessage(string messagePartFront)
        {
            return messagePartFront + "__" + System.DateTime.Now.ToString();
        }


    }
}