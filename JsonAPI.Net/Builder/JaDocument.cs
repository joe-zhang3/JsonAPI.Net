using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public class JaDocument : JaResourceBase, IResource{
        private List<IResource> resources = new List<IResource>();

        public bool CompoundDocuments { get; set; }

        public JaDocument(IEnumerable<IResource> resources){
            this.resources.AddRange(resources);
        }

        public JaDocument(IResource resource){
            this.resources.Add(resource);
		}

        public override JToken Build(JaBuilder builder, string templateName){

            JObject masterTemplate = GetMasterTemplate(templateName);

            if (masterTemplate == null) throw new Exception("Template of master cannot be empty");

            List<string> propertiesNeedToRemove = new List<string>(); 

            foreach(var property in masterTemplate.Properties()){
                if (property.Name.Equals("data", StringComparison.CurrentCultureIgnoreCase)){
                    property.Value = BuildData(builder);
                }else if(property.Name.Equals("included")){
                    property.Value = builder.BuildIncludedResources();                    
                } else{
                    JToken jt = builder.GetPropertyValue(property.EvaulationKey(), this);

                    if(jt == null || jt.IsEmpty()){
                        propertiesNeedToRemove.Add(property.Name);
                    }else{
                        property.Value = jt;
                    }
                }
            }



            propertiesNeedToRemove.ForEach(p => masterTemplate.Remove(p));

            return masterTemplate;
        }

        private JToken BuildData(JaBuilder builder){
            
            if(CompoundDocuments || resources.Count > 1){
                JArray array = new JArray();

                resources.ForEach(r => {
                    array.Add(r.Build(builder));
                });

                return array;
            }else{
                return resources.First().Build(builder);
            }
        }

        private JObject GetMasterTemplate(string tempName)
		{
            if (tempName != null )
			{
                return JaTemplates.GetTemplate(tempName);
			}
			else
			{
				return JObject.Parse(Constants.DEFAULT_MASTER);
			}
		}
    }
}
