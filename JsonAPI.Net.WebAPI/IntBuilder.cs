using System;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net.WebAPI
{
    public class IntBuilder : JaCustomBuilder
    {
        public IntBuilder() : base(typeof(int)){
            
        }

        public override JToken Build(JaBuilderContext context)
        {
            return new JValue(context.Value.ToString());
        }
    }
}
