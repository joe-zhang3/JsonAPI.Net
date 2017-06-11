using System;
using System.Web.Http;
using JsonAPI.Net;

namespace JsonAPI.Net.Extensions
{
    public static class HttpConfigurationExtension
    {
        public static void ConfigureJsonAPI(this HttpConfiguration configuration){

            configuration.Formatters.Clear();
            configuration.Formatters.Insert(0, new JaMediaTypeFormatter());

            var a = JaTemplates.Templates;
        }
    }
}
