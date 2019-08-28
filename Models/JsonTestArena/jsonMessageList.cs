using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Models
{     
    public class jsonMessageList
    {
        public void iterateList()
        {
            Console.WriteLine("iterating");

            BatchMessage batchMessage = new BatchMessage();
            batchMessage.batchname = "hi";
            List<ErfassungsMessage> messages = new List<ErfassungsMessage>();

            for(int i = 0 ; i< 3 ; i++)
            {
                ErfassungsMessage tmp = new ErfassungsMessage();
                tmp.Id = i;
                tmp.x = i +100;

                Console.WriteLine(tmp.ToJson());
                messages.Add(tmp);
            }
            batchMessage.Erfassungen = messages;
            Console.WriteLine(batchMessage.ToJson());

            LoadJson();
        }

        public void LoadJson()
        {
            ErfassungsMessage items;
            using (StreamReader r = new StreamReader(@"Models\JsonTestArena\erfassungsmessage.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<ErfassungsMessage>(json);
            }

            Console.WriteLine("i read: "+ items.ToJson());


            //and now the batch message list:
            BatchMessage batchMessage;
            using (StreamReader r = new StreamReader(@"Models\JsonTestArena\batchmessage.json"))
            {
                string json = r.ReadToEnd();
                batchMessage = JsonConvert.DeserializeObject<BatchMessage>(json);
            }

            Console.WriteLine("i read a batch: "+ batchMessage.ToJson());
        }
    }
}