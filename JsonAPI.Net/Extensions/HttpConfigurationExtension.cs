using System;
using System.Web.Http;

namespace JsonAPI.Net
{
    public static class HttpConfigurationExtension
    {
        public static void ConfigureJsonAPI(this HttpConfiguration configuration, string templatePath){

            configuration.Formatters.Clear();
            configuration.Formatters.Insert(0, new JaMediaTypeFormatter());

            JaBuilderFactory.Initialize();

            JaTemplates.Intialize(templatePath);
        }
    }
}
