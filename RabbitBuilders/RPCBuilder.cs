using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;

namespace RabbitBuilders
{

    public class RPCBuilder
    {
        public RPCBuilder()
        {
            
        }

        public void doWork(IModel channel)
        {
   
        }

        public void createRPC(IModel channel, string rpcQueueName)    
        {
            channel.QueueDeclare(queue: rpcQueueName ,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

            channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: rpcQueueName,
            autoAck: false, consumer: consumer);
            Console.WriteLine(" [x] Awaiting RPC requests");


            consumer.Received += (model, ea) =>
            {
                string response = null;

                var body = ea.Body;
                var props = ea.BasicProperties;
                var replyProps = channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;

                try
                {
                    var message = Encoding.UTF8.GetString(body);
                    int n = int.Parse(message);
                    // Console.WriteLine(" [.] fib({0})", message);
                    response = (n+1).ToString();
                }
                catch (Exception e)
                {
                    Console.WriteLine(" [.] " + e.Message);
                    response = "";
                }
                finally
                {
                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                      basicProperties: replyProps, body: responseBytes);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag,
                      multiple: false);
                }
            };
        }
    }
}
