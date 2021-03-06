﻿using System;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Humanizer;

namespace JsonAPI.Net
{
	/// <summary>
	/// self links name equals the type name
	/// </summary>

    public abstract class JaResource : JaResourceBase, IResource, ICacheable
    {
        public override JToken Serialize(JaBuilderContext context)
		{
            JObject template = GetTemplate();

            List<string> propertiesToRemove = new List<string>();

			foreach (var property in template.Properties()){
                if (property.Name.Equals(Constants.KEY_RELATIONSHIPS)){
                    JToken jt = BuildRelationships(property.Value, context);

					if (jt == null || jt.IsEmpty()){
						propertiesToRemove.Add(property.Name);
					} else {
						property.Value = jt;
					}
				} else {
					context.Populate(property, this);
				}
			}

            propertiesToRemove.ForEach(n => template.Remove(n));

			return template;
        }

        public virtual JToken BuildRelationships(JToken relationships, JaBuilderContext context){

            JObject jObject = relationships as JObject; //relationships must be a object

            List<string> propertiesToRemove = new List<string>();

			foreach(var property in jObject.Properties()){ //need to know what need to be built
                JToken jt = context.GetPropertyValue(property.Name.Pascalize(), this, true);

                if(jt == null || jt.IsEmpty()){
                    propertiesToRemove.Add(property.Name);
                }else{
                    property.Value = jt;
                }
            }

            propertiesToRemove.ForEach(n => jObject.Remove(n));

            return jObject;
        }
    }
}
