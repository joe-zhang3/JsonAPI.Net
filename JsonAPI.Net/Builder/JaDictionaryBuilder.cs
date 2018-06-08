using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public class JaDictionaryBuilder : IBuilder
    {
        public JaBuilderType BuilderType => JaBuilderType.Dictionary;

	    public JToken Build(JaBuilderContext context){
			JObject j = new JObject();

            IDictionary<string, object> dic = (IDictionary<string, object>)context.Value;

			if (dic.Count == 0) return new JObject();

			foreach (KeyValuePair<string, object> kv in dic)
			{
				j.Add(new JProperty(kv.Key, new JValue(kv.Value)));
			}

			return j;
        }
    }
}
