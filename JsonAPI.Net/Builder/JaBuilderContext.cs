using System;
using System.Reflection;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace JsonAPI.Net
{
    public class JaBuilderContext
    {
        public string MasterTemplate { get; set; }
        public string TemplateName { get; set; }

        private ICollection<IResource> includedResources;
		private bool buildingIncludedResouce = false;

        public void AddIncludedResources(IResource resource)
		{
            if (!(resource is JaResource) || buildingIncludedResouce) return;

			if (includedResources == null) includedResources = new List<IResource>();

			includedResources.Add(resource);
		}

        public object Value { get; set; }

		public virtual JToken BuildIncludedResources()
		{
            TemplateName = null; //included resource does not need any template. user their own.
            buildingIncludedResouce = true;

			JArray ja = new JArray();

			if (includedResources != null)
			{
				foreach (var r in includedResources)
				{
					ja.Add(r.Build(this));
				}
			}

			buildingIncludedResouce = false;
			includedResources?.Clear();

			return ja;
		}

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

        private bool BuildingRelationships(){
            return TemplateName != null && TemplateName.Equals(Constants.DEFAULT_RELATIONSHIP_NAME);
        }

		public JToken GetPropertyValue(string key, object value)
		{
			if (value == null || key == null) return null;

			if (key.Contains("."))
			{
				int dot = key.IndexOf('.');
				string tempKey = key.Substring(0, dot);

				PropertyInfo temp = value.GetType().GetProperty(tempKey);

				if (temp == null) return null;

				object tempValue = temp.GetValue(value);

				return GetPropertyValue(key.Substring(dot + 1), tempValue);
			}
			else
			{
				PropertyInfo pi = value.GetType().GetProperty(key, BindingFlags.Public | BindingFlags.Instance);

				if (pi == null) return null;

				this.Value = pi.GetValue(value);

                return Value != null ? pi.PropertyType.GetBuilder(BuildingRelationships()).Build(this) : null;
			}
		}
    }
}
