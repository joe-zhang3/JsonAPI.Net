using System;
using Newtonsoft.Json.Linq;
namespace JsonAPI.Net
{
    public class JaSimpleLink : JaLink
    {
        public override JToken Build(JaBuilder builder, string temnplate = null)
        {
            return new JProperty(Name, Href.AbsolutePath);
        }
    }
}
