using RabbitMQ.Client;
using System.Text;
using Models;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System;

namespace RabbitMessaging
{

    public class RpcClient
    {
        private IBasicProperties props;
        private IConnection connection;
        private IModel _channel;
        private string _replyQueueName;
        private EventingBasicConsumer _consumer;
        private BlockingCollection<string> _respQueue = new BlockingCollection<string>();
        public void consumeReplyQueue(IModel channel,string replyQueueName)
        {
            _channel = channel;
            _replyQueueName = replyQueueName;
            _consumer = new EventingBasicConsumer(channel);

            props = _channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            _consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var response = Encoding.UTF8.GetString(body);
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    _respQueue.Add(response);
                }
            };
        }

        public string Call(string message,string rpcQueueName)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(
                exchange: "",
                routingKey: rpcQueueName,
                basicProperties: props,
                body: messageBytes);

            _channel.BasicConsume(
                consumer: _consumer,
                queue: _replyQueueName,
                autoAck: true);

            return _respQueue.Take(); ;
        }  
    }
}