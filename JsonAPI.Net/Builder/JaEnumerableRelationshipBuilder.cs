using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    /// <summary>
    /// Enumerable relationship is kind of special as the structure looks like below:
    /// 
    /// "items" : {
    ///   "links" : {}
    ///   "data" : []
    /// }
    /// 
    /// which says the data is an array. 
    /// 
    /// </summary>

    public class JaEnumerableRelationshipBuilder : IBuilder
    {
        public JaBuilderType BuilderType { get { return JaBuilderType.EnumerableRelationships; } }
        public JToken Build(JaBuilderContext context)
        {
            IEnumerable<IResource> elements = (IEnumerable<IResource>)context.Value;

            JObject jObject = new JObject();
            JArray ja = new JArray();

            foreach (var resource in elements)
            {
                context.AddIncludedResources(resource);

                JObject jo = new JObject();
                jo.Add("type", ((JaResourceBase)resource).Type);
                jo.Add("id", ((JaResourceBase)resource).Id);
                ja.Add(jo);
            }

            jObject.Add("data", ja);

            return jObject;
        }
    }
}
