﻿using System;
using System.Web.Http;

namespace JsonAPI.Net
{
    public static class HttpConfigurationExtension
    {
        public static void ConfigureJsonAPI(this HttpConfiguration configuration, Action<JaConfiguration> action)
		{
            configuration.MessageHandlers.Add(new JaDelegatingHandler());
            //configuration.Formatters.Clear();
			configuration.Formatters.Insert(0, new JaMediaTypeFormatter());

            JaConfiguration config = new JaConfiguration(); 
		    
		    action?.Invoke(config);

            JaBuilderFactory.Initialize(config);

            JaTemplates.Intialize(config.TemplateDirectory ?? Constants.DEFAULT_TEMPLATE_PATH);
		}
    }
}
