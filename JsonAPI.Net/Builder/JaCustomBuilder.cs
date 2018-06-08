using System;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public abstract class JaCustomBuilder : ICustomBuilder
    {
        public Type CustomType { get; }

        public JaBuilderType BuilderType => JaBuilderType.Custom;

        public abstract JToken Build(JaBuilderContext context);

        public JaCustomBuilder(Type type){
            this.CustomType = type;
        }
    }
}
