﻿using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public class JaRelationship : IResource
    {
        private readonly object value;
        private readonly JObject template;

        public JaRelationship(object value){
            this.value = value;
            template = JaTemplates.GetTemplate(Constants.DEFAULT_RELATIONSHIP_NAME);
        }

        public JToken Serialize(JaBuilderContext context){

            foreach(var t in template.Properties()){
                if(t.Name.Equals("data")){
                    t.Value = BuildData();
                }else{
                    string key = t.EvaulationKey();

					if (key == null) continue;

                    JToken jt = context.GetPropertyValue(key, GetPrimaryObject());
                    t.Value = jt ?? t.Value;
                }
            }

            return template;
		}

        private IResource GetPrimaryObject(){
            var resouce = value as IEnumerable<IResource>;

            return resouce?.First() ?? (IResource)value;
        }


        private JToken BuildData(){

            var resouce = value as IEnumerable<IResource>;

			if (resouce == null)
			{
				return SerializeSingleResource((IResource)value);
			}
			else
			{
				JArray ja = new JArray();
				foreach (var r in resouce){
					ja.Add(SerializeSingleResource(r));
				}

                return ja;
			}
		}

        private JToken SerializeSingleResource(IResource resource){
			JObject jo = new JObject();
			jo.Add("type", ((JaResourceBase)resource).Type);
			jo.Add("id", ((JaResourceBase)resource).Id);
            return jo;
        }

        public JContainer GetContainer(){
            return new JObject();
        }
    }
}
