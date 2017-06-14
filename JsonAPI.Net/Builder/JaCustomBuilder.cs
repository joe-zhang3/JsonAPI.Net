using System;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public abstract class JaCustomBuilder : ICustomBuilder
    {
        private Type type;
        public Type CustomType{ get { return this.type; }}

        public JaBuilderType BuilderType { get { return JaBuilderType.Custom; }}

        public abstract JToken Build(JaBuilderContext context);

        public JaCustomBuilder(Type type){
            this.type = type;
        }
    }
}
