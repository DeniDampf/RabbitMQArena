using System;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Models
{
    [Serializable] 
    public class ErfassungsMessage
    {
        public int Id { get; set; }  

        public string Operation { get; set; }

        public int x { get; set; }
        public int y { get; set; }

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


        public string ToXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(JsonMessage));
            string returnString;

            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, this);
                returnString = sw.ToString();
            }
            return returnString;
        }

        public static JsonMessage FromJsonString(string json)
        {
            JsonMessage message = JsonConvert.DeserializeObject<JsonMessage>(json);

            return message;  
        
        }
    }
}
