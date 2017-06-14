using System;
using System.Net;
using System.Net.Http;

namespace JsonAPI.Net
{
    public class JaMessage
    {
        public JaMessage(HttpRequestMessage message)
        {
            if(message.Properties.ContainsKey(Constants.MASTER_TEMPLATE_NAME)){
                TemplateName = message.Properties[Constants.MASTER_TEMPLATE_NAME].ToString();
            }
        }

        public string TemplateName { get; set; }
    }
}
