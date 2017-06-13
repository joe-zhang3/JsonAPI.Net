using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    internal class JaTemplateScanner
    {
        public static IDictionary<string, JObject> Scan(string path){

            IDictionary<string, JObject> objects = new Dictionary<string, JObject>();

            foreach(var json in Directory.GetFiles(path, "*.json")){

                string content = File.ReadAllText(json, Encoding.UTF8);

                if (string.IsNullOrWhiteSpace(content)) continue;

                JObject j = JObject.Parse(content);
                objects.Add(json.GetFileName(), j);
            }

            HandleRef(objects);

            return objects;
        }

        private static void HandleRef(IDictionary<string, JObject> objects){

            foreach(var kv in objects){
                foreach (var property in kv.Value.Properties())
				{
                    if(property.Value.ToString().HasRef()){
                        string templateName = property.Value.ToString().GetRefKey();

                        JObject t = objects[templateName];

                        if(property.Value.Type == JTokenType.Array){
                            property.Value = new JArray(t);
                        }else{
                            property.Value = t;    
                        }
                    }
				}
            }
        }
    }
}
