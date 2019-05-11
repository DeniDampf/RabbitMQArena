using System;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMessaging
{

    public class PutMessaging
    {
        public void putMessage(IModel channel,string qeueuName)
        {
            var message = createMessage(qeueuName);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: "",
                                routingKey: qeueuName,
                                basicProperties: properties,
                                body: body);
        }

        private string createMessage(string messagePartFront)
        {
            return messagePartFront + "__" + System.DateTime.Now.ToString();
        }


    }
}