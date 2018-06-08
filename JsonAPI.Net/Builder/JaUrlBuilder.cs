using System;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public class JaUrlBuilder : IBuilder
    {
        public JaBuilderType BuilderType => JaBuilderType.Uri;

        public JToken Build(JaBuilderContext context){
            return new JValue(((Uri)context.Value).AbsolutePath);
        }
    }
}
