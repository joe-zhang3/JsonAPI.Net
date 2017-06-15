using System;
using System.Linq;
using System.Reflection;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace JsonAPI.Net
{
    public class JaBuilderContext
    {
        private readonly HttpRequestMessage message;
        private string actionTemplate;

        public JaBuilderContext(HttpRequestMessage message)
        {
            this.message = message;

            object obj;

            if (message.Properties.TryGetValue(Constants.ACTION_TEMPLATE, out obj)) actionTemplate = obj.ToString();
        }
        public HttpRequestMessage RequestMessage{ get { return message; }}

        public string ActionTemplate { get { return actionTemplate; } }

        private ICollection<IResource> includedResources;
		private bool buildingIncludedResouce = false;

        internal ICollection<IResource> IncludedResources{ get { return includedResources; }}

        public void AddIncludedResources(IResource resource)
		{
            if (buildingIncludedResouce) return;

            if (includedResources == null) includedResources = new List<IResource>();

            if (includedResources.Any(n => n.Equals(resource))) return;

			includedResources.Add(resource);
		}

        public object Value { get; set; }

		public void Populate(JToken token, object inputResource)
		{
			if (token is JObject)
			{
				JObject jo = (JObject)token;

				foreach (var o in jo.Properties())
				{
					Populate(o, inputResource);
				}
			}
			else if (token is JProperty)
			{
				JProperty jp = (JProperty)token;

				if (jp.Value.Type == JTokenType.Object)
				{
					foreach (var obj in ((JObject)jp.Value).Properties())
					{
						Populate(obj, inputResource);
					}
				}
				else
				{
					string key = jp.EvaulationKey();

					if (key != null)
					{
						jp.Value = GetPropertyValue(key, inputResource);
					}
				}
			}
		}

        public JToken GetPropertyValue(string key, object value){
            return GetPropertyValue(key, value, false);    
        }
		public JToken GetPropertyValue(string key, object value, bool buildingRelationship)
		{
			if (value == null || key == null) return null;

			if (key.Contains(".")){
				int dot = key.IndexOf('.');
				string tempKey = key.Substring(0, dot);

				PropertyInfo temp = value.GetType().GetProperty(tempKey);

				if (temp == null) return null;

				object tempValue = temp.GetValue(value);

				return GetPropertyValue(key.Substring(dot + 1), tempValue, buildingRelationship);
			}
			else
			{
				PropertyInfo pi = value.GetType().GetProperty(key, BindingFlags.Public | BindingFlags.Instance);

				if (pi == null) return null;

				this.Value = pi.GetValue(value);

                if (Value == null) return null;

                if(buildingRelationship){
                    Value = new JaRelationship(Value);
                    return Value.GetType().GetBuilder().Build(this);
                }

                return pi.PropertyType.GetBuilder().Build(this);
			}
		}
    }
}
