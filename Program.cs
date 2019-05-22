using System;
using RabbitMQ.Client;
using System.Text;
using RabbitBuilders;
using RabbitMessaging;

using System.Threading;
using RabbitMQ.Client.Events;

namespace RabbitMQArena
{
  class Program
  {
    static MainBuilder _mBuilder;
    static void Main(string[] args)
    {
      try
      {


        _mBuilder = new MainBuilder();
        _mBuilder.doWork();

        for (int i = 0; i < 10; i++)
        {
          putMessages("WorkingQueue_" + i.ToString().PadLeft(2, '0'));
        }


        Console.WriteLine("open a thread to listen to exchange");
        ThreadPool.QueueUserWorkItem(listenToExchange,_mBuilder.Channel);

        putExchangeMessages("logs7even");

        GetMessaging getMessages = new GetMessaging();
        getMessages.GetMessage(_mBuilder.Channel, "WorkingQueue_03");

        Console.WriteLine("i am really finished");

        putExchangeMessages("logs7even");

        Thread.Sleep(500);
        putExchangeMessages("logs7even");
        Thread.Sleep(500);
        putExchangeMessages("logs7even");
        Thread.Sleep(500);
        putExchangeMessages("logs7even");

      }
      catch (Exception ex)
      {
        Console.WriteLine("Fehler: " + ex.Message);
      }
      finally
      {
        Console.WriteLine("fertig");
        Console.ReadLine();
      }

    }

    public static void putMessages(string queueName)
    {
      PutMessaging messageService = new PutMessaging();
      for (int i = 0; i < 2; i++)
      {
        messageService.putMessage(_mBuilder.Channel, queueName);
      }
    }

    public static void putExchangeMessages(string exchangeName)
    {
      PutMessaging messageService = new PutMessaging();

      messageService.putExchangeMessage(_mBuilder.Channel, exchangeName); 
      Console.WriteLine("Published to exchange");     
    }

    static void listenToExchange(Object stateInfo)
    {
      IModel channel = (IModel)stateInfo;
      string queueName = channel.QueueDeclare().QueueName;

      channel.QueueBind(queue:queueName,exchange:"logs7even", routingKey:"");
      Console.WriteLine("Register listener on: " + queueName);


      var consumer = new EventingBasicConsumer(channel);
      consumer.Received += (model, ea) =>
      {
          var body = ea.Body;
          var message = Encoding.UTF8.GetString(body);
          Console.WriteLine(" [x] {0}", message);
      };
      channel.BasicConsume(queue: queueName,
                            autoAck: true,
                            consumer: consumer);




      while(true)
      {
        Console.WriteLine("huhuhuhu");
        Thread.Sleep(1000);
      }
    }
  }
}
