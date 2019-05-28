using System;
using RabbitMQ.Client;
using System.Text;
using Models;

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

        public void putExchangeMessage(IModel channel,string exchangeName)
        {
            var message = createMessage(exchangeName);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: exchangeName,
                                routingKey: "",
                                basicProperties: null,
                                body: body);
        }

        public void putExchangeMessage(IModel channel,string exchangeName,string message)
        {           
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: exchangeName,
                                routingKey: "",
                                basicProperties: null,
                                body: body);
        }

        private string createMessage(string messagePartFront)
        {
            JsonMessage message = new JsonMessage();
            message.x = 1;
            message.y = 2;

            message.Operation = "add";
            message.Id = System.DateTime.Now.Millisecond;

            return message.ToJson();
        }
    }
}