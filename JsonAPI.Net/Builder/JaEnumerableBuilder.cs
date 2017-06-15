using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public class JaEnumerableBuilder : IBuilder
    {
        public JaBuilderType BuilderType { get { return JaBuilderType.Enumerable; } }
        public JToken Build(JaBuilderContext context){
			
            IEnumerable<IResource> elements = (IEnumerable<IResource>)context.Value;

			JContainer ja = elements.FirstOrDefault()?.GetContainer() ?? new JArray();

			foreach (var resource in elements)
			{
                ja.Add(resource.Serialize(context));
			}

			return ja;
        }
    }
}
