using System;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public class JaResourceBuilder : IBuilder
    {
        public JaBuilderType BuilderType => JaBuilderType.Resource;

        public JToken Build(JaBuilderContext context){
            return ((IResource)context.Value).Serialize(context);
        }
    }
}
