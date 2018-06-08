using System;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public class JaDefaultBuilder : IBuilder
    {
        public JaBuilderType BuilderType => JaBuilderType.Default;

        public JToken Build(JaBuilderContext context){
            return new JValue(context.Value);
        }
    }
}
