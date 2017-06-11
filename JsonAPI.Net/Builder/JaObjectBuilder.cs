using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using JsonAPI.Net.Extensions;

namespace JsonAPI.Net.Builder
{
    public class JaObjectBuilder{
        private readonly JObject schema;
        private readonly object resource;

        public JaObjectBuilder(JObject schema, object inputValue){
            this.schema = schema;
            this.resource = inputValue;
            if (this.resource == null) throw new Exception("Resource object cannot be null");
		}

        public JObject BuildObject(){

            JObject returnObject = new JObject(schema);
            TypeInfo ti = null;
            JaResource jaResource = null;
            IEnumerable<JaResource> jaResources = null;
            if(resource is IEnumerable){
                jaResources = (IEnumerable<JaResource>)resource;
                ti = jaResources.First()?.GetType().GetTypeInfo();
            }else{
                jaResource = (JaResource)resource;
                ti = jaResource.GetType().GetTypeInfo();
            }

            bool isCollection = jaResources != null;

            JArray jArray = isCollection ? new JArray() : null;

            foreach(var o in returnObject.Properties()){
                if(o.Name.Equals("data")){
                    if(o.Value.Type == JTokenType.Array){ //compondedResrouce
                        JToken jFirst = ((JArray)o.Value).First;
                        foreach(var r in jaResources){
                            JToken temp = jFirst.DeepClone();

                            Populate(temp, r, ti);

                            jArray.Add(temp);
                        }
                    }else{
                        Populate(o, jaResource, ti);
                    }
                }else{
                    
                }
            }

            if(isCollection){
                returnObject.Property("data").Value = jArray;
            }

            return returnObject;
        }

        private void Populate(JToken token, JaResource inputResource, TypeInfo ti)
		{
            if(token is JObject){
                JObject jo = (JObject)token;

                foreach(var o in jo.Properties()){
                    Populate(o, inputResource, ti);
                }
            }else if(token is JProperty){

                JProperty jp = (JProperty)token;

				if (jp.Value.Type == JTokenType.Object)
				{
					foreach (var obj in ((JObject)jp.Value).Properties())
					{
                        Populate(obj, inputResource, ti);
					}
                }else if(jp.Value.Type == JTokenType.String){
					string value = jp.Value.ToString();
                    if (value.EvaulationRequired())
					{
                        object obj = GetPropertyValue(ti, value.GetEvaulationKey(), inputResource);
						jp.Value = obj.GenerateJValue(jp.Name.IsId());
					}
                } 
            }
		}

        private object GetPropertyValue(TypeInfo ti, string key, object value){
            if(key.Contains(".")){
                int dot = key.IndexOf('.');
                string tempKey = key.Substring(0, dot);

                PropertyInfo temp = ti.GetProperty(tempKey);

                TypeInfo tempTi = temp.PropertyType.GetTypeInfo();

                object tempValue = temp.GetValue(value);

                return GetPropertyValue(tempTi, key.Substring(dot + 1), tempValue);

            }else{
                return ti.GetProperty(key).GetValue(value);
            }
        }
    }
}
