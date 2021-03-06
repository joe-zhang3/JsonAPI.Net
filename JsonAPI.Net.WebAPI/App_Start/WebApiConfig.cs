﻿using System.Web.Http;
using System.Net.Http.Formatting;
using JsonAPI.Net;

namespace JsonAPI.Net.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.ConfigureJsonAPI(c => {
                c.TemplateDirectory = "Templates";
                c.RegisterTypeBuilder(new ComplexBuilder());
            });
        }
    }
}
