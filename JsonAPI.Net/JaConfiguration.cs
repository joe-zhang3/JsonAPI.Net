using System;
using System.Net;
using System.Net.Http;

namespace JsonAPI.Net
{
    public class JaConfiguration
    {
        public JaConfiguration(HttpRequestMessage message)
        {
            if(message.Properties.ContainsKey(Constants.MASTER_TEMPLATE_NAME)){
                TemplateName = message.Properties[Constants.MASTER_TEMPLATE_NAME].ToString();
            }

            if(message.Properties.ContainsKey(Constants.JA_BUILDER)){
                Builder = (JaBuilder)message.Properties[Constants.JA_BUILDER];
            }
        }

        public string TemplateName { get; set; }
        public JaBuilder Builder { get; set; }
    }
}
