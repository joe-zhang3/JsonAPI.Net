using System;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public class JaComplexLink : JaLink
    {
        public override JToken Build(JaBuilder builder, string temnplate = null)
        {
            JObject jb = new JObject();

            foreach(var property in GetType().GetProperties()){

                if (property.Name.Equals("name", StringComparison.CurrentCultureIgnoreCase)) continue;

                jb.Add(property.Name.ToLower(), builder.GetPropertyValue(property.Name, this));
            }

            return new JProperty(Name, jb);
        }
    }
}
