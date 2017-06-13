using System;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public class JaSimpleLink : ILink
    {
        public string Name { get; set; }
        public Uri URL { get; set; }

        public JToken Build(JaBuilder builder){
            return new JProperty(Name, URL.AbsolutePath);
        }
    }
}
