using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Models
{
    [Serializable] 
    public class BatchMessage
    {
        public string batchname { get; set; }

        public List<ErfassungsMessage> Erfassungen { get; set; }

        public string ToJson()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();

            string returnString;
            using (StringWriter sw = new StringWriter())
            {
                jsonSerializer.Serialize(sw,this);
                returnString = sw.ToString();
            }
            return returnString;
        }

        public static BatchMessage FromJsonString(string json)
        {
            BatchMessage message = JsonConvert.DeserializeObject<BatchMessage>(json);
            return message;             
        }
    }
}