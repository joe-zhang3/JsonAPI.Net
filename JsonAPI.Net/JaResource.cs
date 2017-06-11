using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonAPI.Net.Cache;

namespace JsonAPI.Net
{
    public abstract class JaResource : ICacheAble
    {
        protected JToken document;
        protected readonly IDictionary<string, JValue> metas;
        protected JaResource()
        {
            document = new JObject();
            metas = new Dictionary<string, JValue>();
        }

        [JsonProperty("ignored")]
        public IDictionary<string, JValue> Metas { get { return metas; } }


        public void Meta(string key, JValue value){
            metas.Add(key, value);
        }

        public void Includes(string key, JObject obj){
            
        }
    }
}
