using System;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public class JaResourceBuilder : IBuilder
    {
        public JaBuilderType BuilderType { get { return JaBuilderType.Resource; } }
        public JToken Build(JaBuilderContext context){
            context.AddIncludedResources((IResource)context.Value);
            return ((IResource)context.Value).Serialize(context);
        }
    }
}
