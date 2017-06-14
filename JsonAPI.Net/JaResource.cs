using System;
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
        public override JToken Build(JaBuilder builder, string templateName)
		{
            JObject template = JaTemplates.GetTemplate(templateName != null ? templateName : Type.Pascalize());

            List<string> propertiesToRemove = new List<string>();

			foreach (var property in template.Properties()){
				if (property.Name.Equals(Constants.DEFAULT_RELATIONSHIP_NAME)){
                    JToken jt = BuildRelationships(property.Value, builder);

					if (jt == null || jt.IsEmpty()){
						propertiesToRemove.Add(property.Name);
					} else {
						property.Value = jt;
					}
				} else {
					builder.Populate(property, this);
				}
			}

            propertiesToRemove.ForEach(n => template.Remove(n));

			return template;
        }

        public virtual JToken BuildRelationships(JToken relationships, JaBuilder builder){

            JObject jObject = relationships as JObject; //relationships must be a object

            List<string> propertiesToRemove = new List<string>(); 

			foreach(var property in jObject.Properties()){ //need to know what need to be built
                JToken jt = builder.GetPropertyValue(property.Name.Pascalize(), this, Constants.DEFAULT_RELATIONSHIP_NAME);

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
