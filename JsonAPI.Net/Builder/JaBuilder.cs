﻿using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace JsonAPI.Net
{
    public class JaBuilder
    {
        private static JaBuilder builder = new JaBuilder();
        public static JaBuilder GetInstance(){
            return builder;
        }

        public virtual void Populate(JToken token, object inputResource)
		{
			if (token is JObject)
			{
				JObject jo = (JObject)token;

				foreach (var o in jo.Properties())
				{
					Populate(o, inputResource);
				}
            } else if (token is JProperty) {
				JProperty jp = (JProperty)token;

                if (jp.Value.Type == JTokenType.Object) {
                    foreach (var obj in ((JObject)jp.Value).Properties()) {
						Populate(obj, inputResource);
					}
                }else {
					string key = jp.EvaulationKey();

					if (key != null){
                        jp.Value = GetPropertyValue(key, inputResource);
					}
				}
			}
		}

        /// <summary>
		/// Gets the property value.
		/// Example:
		/// {{property}}
		/// {{OtherType.Property}}
		/// </summary>
		/// <returns>The property value.</returns>
		/// <param name="ti">Ti.</param>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>

        public virtual JToken GetPropertyValue(string key, object value){
            return GetPropertyValue(key, value, null);
        }
        public virtual JToken GetPropertyValue(string key, object value, string templateName)
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
            } else {
				PropertyInfo pi = value.GetType().GetProperty(key, BindingFlags.Public | BindingFlags.Instance);

				if (pi == null) return null;

				object returnValue = pi.GetValue(value);

                if (returnValue == null) return null;

                bool isPreHandled = false;

                //a hook where derived class can override and handle the value.
                JToken preHandledValue = PreHandleValue(returnValue, out isPreHandled);

                if (isPreHandled && preHandledValue != null) return preHandledValue;

				if (pi.PropertyType == typeof(Uri)){
					return new JValue(((Uri)returnValue).AbsolutePath);
                } else if (pi.PropertyType.IsGenericType){
                    if(typeof(IDictionary<,>).IsAssignableFrom(pi.PropertyType.GetGenericTypeDefinition())){
                        return ParseFromDic(returnValue);
                    }else if(typeof(IEnumerable<IResource>).IsAssignableFrom(pi.PropertyType)){
                        return ParseFromEnumberable(returnValue, templateName);	
                    }
                } else if(typeof(IResource).IsAssignableFrom(pi.PropertyType)){
                    return ((IResource)returnValue).Build(this, templateName);
                }

				return new JValue(returnValue);
			}
		}

        public virtual JToken PreHandleValue(object value, out bool preHandled){
            preHandled = false;
            return null;
        }

        public JToken ParseFromDic(object obj){
			JObject j = new JObject();

            IDictionary<string, object> dic = (IDictionary<string, object>)obj;

			if (dic.Count == 0) return new JObject();

			foreach (KeyValuePair<string, object> kv in dic)
			{
				j.Add(new JProperty(kv.Key, new JValue(kv.Value)));
			}

			return j;
        }
        public JToken ParseFromEnumberable(object obj)
        {
            return ParseFromEnumberable(obj, null);
        }
        public JToken ParseFromEnumberable(object obj, string templateName){

            IEnumerable<IResource> elements = (IEnumerable<IResource>)obj;

            JContainer ja = elements.FirstOrDefault()?.GetContainer() ?? new JArray(); 

			foreach (var resource in elements){
                ja.Add(resource.Build(this, templateName));
			}

			return ja;
        }
    }
}
