using System;
using System.Net.Http;

namespace JsonAPI.Net
{
    public static class HttpRequestMessageExtensions
    {
        public static string GetTemplateName(this HttpRequestMessage message){

            object template = null;

            message.Properties.TryGetValue(Constants.MASTER_TEMPLATE_NAME, out template);

            return template?.ToString();
        }
    }
}
