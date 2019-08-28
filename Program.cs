using System;
using RabbitMQ.Client;
using System.Text;
using RabbitBuilders;
using RabbitMessaging;

using System.Threading;
using RabbitMQ.Client.Events;
using Models;

namespace RabbitMQArena
{
  class Program
  {
    static MainBuilder _mBuilder;

    static bool _stopThread = false;
    static void Main(string[] args)
    {
      try
      {
        jsonMessageList jsonList = new jsonMessageList();
        jsonList.iterateList();

        return;


        _mBuilder = new MainBuilder();
        _mBuilder.doWork();

        //Checking Binding to an Exchange - Surviving Channel
        ExchangeBuilder exBuilder = new ExchangeBuilder() ;
        QueueBuilder qBuilder = new QueueBuilder();
        RPCBuilder rPCBuilder = new RPCBuilder();

        exBuilder.createExchange(_mBuilder.Channel,"ChannelTimeoutChecker_EX");
        qBuilder.createQueue(_mBuilder.Channel,"ChannelTimeoutListener");
        qBuilder.createQueue(_mBuilder.Channel,"rpcReplyQueue");


        qBuilder.BindToExchange(_mBuilder.Channel,"ChannelTimeoutChecker_EX", "ChannelTimeoutListener");
        rPCBuilder.createRPC(_mBuilder.Channel,"rpcQueue");
        
        
        // for (int i = 0 ;i < 100;i++)
        // {
        //   ConnectionChannelChecking(i.ToString());
        //   if(i % 20 == 0)
        //   {
        //     ThreadPool.QueueUserWorkItem(SendToExchange,_mBuilder.Channel);
        //   }
        // }
        
        
        // ThreadPool.QueueUserWorkItem(SendToExchange,_mBuilder.Channel);
        // ThreadPool.QueueUserWorkItem(SendToExchange,_mBuilder.Channel);
        // ThreadPool.QueueUserWorkItem(SendToExchange,_mBuilder.Channel);
        // ThreadPool.QueueUserWorkItem(SendToExchange,_mBuilder.Channel);
        // ThreadPool.QueueUserWorkItem(SendToExchange,_mBuilder.Channel);

        // Thread.Sleep(60000);

        // Console.WriteLine("end thread");
        // _stopThread = true;
        // Thread.Sleep(2000);
        // Console.WriteLine("did it ended?");

        // RPC Call
        var rpcClient = new RpcClient();
        rpcClient.consumeReplyQueue(_mBuilder.Channel,"rpcReplyQueue");
        string result = rpcClient.Call("1","rpcQueue");
        Console.WriteLine("RPC Result: " + result);


        //Ende RPC



         for (int i = 0; i < 10; i++)
         {
           putMessages("WorkingQueue_" + i.ToString().PadLeft(2, '0'));
         }


        // Console.WriteLine("open a thread to listen to exchange");
                
        //ThreadPool.QueueUserWorkItem(listenToExchange,_mBuilder.Channel);

        // Consumer consumer1 = new Consumer();
        // consumer1.Subscribe("logs7even0","Consumer 1","..1..");

        //Consumer consumer2 = new Consumer();
        //consumer2.Subscribe("logs7even","Consumer 2","..2..");
        // consumer1.Subscribe("logs7even1","Consumer 2","..2..");


        // GetMessaging getMessages = new GetMessaging();
        // getMessages.GetMessage(_mBuilder.Channel, "WorkingQueue_03");

        // Console.WriteLine("i am really finished");

        // putExchangeMessages("logs7even0");

        

        // Thread.Sleep(500);
        // putExchangeMessages("logs7even0");
        // Thread.Sleep(500);
        // putExchangeMessages("logs7even0");
        // Thread.Sleep(500);
        // putExchangeMessages("logs7even0");

        // for(int i = 0; i < 50 ;i ++)
        // {
        //   putExchangeMessages("logs7even0");
        //   putExchangeMessages("logs7even1","Sending TOO SECOND");
        //   Thread.Sleep(500);
        // }

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

    public static void SendToExchange(Object stateInfo)
    {
      long counter = 0 ;
      while(!_stopThread)
      {
        counter++;
        putExchangeMessages("ChannelTimeoutChecker_EX",counter.ToString());
        Thread.Sleep(10);       

      }

      Console.WriteLine("SendToExchangeThread stopped");
    }

    public static void ConnectionChannelChecking(string idname)
    {
      Console.WriteLine("Lets check now");

      IModel channel = null;
      if(idname.Equals("0") || idname.Equals("1"))
      {
        channel =_mBuilder.Channel;   
      }
      else
      {
        channel =_mBuilder.createNewChannel(idname);   
      }


      var consumer = new EventingBasicConsumer(channel);
      consumer.Received += (model, ea) =>
      {
          var body = ea.Body;
          var message = Encoding.UTF8.GetString(body);
          Console.WriteLine($"{idname}: {message}");
      };
      channel.BasicConsume(queue: "ChannelTimeoutListener",
                            autoAck: true,
                            consumer: consumer);
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
      //Console.WriteLine("Published to exchange: " +exchangeName);     
    }

    public static void putExchangeMessages(string exchangeName,string message)
    {
      PutMessaging messageService = new PutMessaging();

      messageService.putExchangeMessage(_mBuilder.Channel, exchangeName,message); 
      Console.WriteLine("Published to exchange: " +exchangeName);     
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
          Console.WriteLine("You are cool [x] {0}", message);
      };
      channel.BasicConsume(queue: queueName,
                            autoAck: true,
                            consumer: consumer);




      while(true)
      {        
        Console.WriteLine("huhuhuhu");
        Thread.Sleep(500); 
      }
    }
  }
}
