using System.Linq;
using System.Reflection;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace JsonAPI.Net
{
    public class JaBuilderContext
    {
	    public JaBuilderContext(HttpRequestMessage message)
        {
            this.RequestMessage = message;

            object obj;

            if (message.Properties.TryGetValue(Constants.ACTION_TEMPLATE, out obj)) ActionTemplate = obj.ToString();
        }
        public HttpRequestMessage RequestMessage { get; }

	    public string ActionTemplate { get; }

	    private bool buildingIncludedResouce = false;

        internal ICollection<IResource> IncludedResources { get; private set; }

	    public void AddIncludedResources(IResource resource)
		{
            if (buildingIncludedResouce) return;

            if (IncludedResources == null) IncludedResources = new List<IResource>();

            if (IncludedResources.Any(n => n.Equals(resource))) return;

			IncludedResources.Add(resource);
		}

        public object Value { get; set; }

		public void Populate(JToken token, object inputResource)
		{
			if (token is JObject jo)
			{
				foreach (var o in jo.Properties())
				{
					Populate(o, inputResource);
				}
			}
			else if (token is JProperty jp)
			{
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
				var dot = key.IndexOf('.');
				var tempKey = key.Substring(0, dot);

				var temp = value.GetType().GetProperty(tempKey);

				if (temp == null) return null;

				var tempValue = temp.GetValue(value);

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
