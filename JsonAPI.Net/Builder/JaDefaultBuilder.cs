using System;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public class JaDefaultBuilder : IBuilder
    {
        public JaBuilderType BuilderType { get { return JaBuilderType.Default; } }

        public JToken Build(JaBuilderContext context){
            return new JValue(context.Value);
        }
    }
}
