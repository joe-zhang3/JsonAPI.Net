using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public class JaDocument : IResource{

        private string templateName;
        private List<IResource> resources = new List<IResource>();

        private IDictionary<string, object> meta = new Dictionary<string, object>();
        private IEnumerable<ILink> links = new List<ILink>();

        public IDictionary<string, object> Meta{ get { return meta; }}
        public IEnumerable<ILink> Links { get { return links; }}

        public bool CompoundDocuments { get; set; }

        public JaDocument(IEnumerable<IResource> resources){
            this.resources.AddRange(resources);
        }

        public JaDocument(IResource resource){
            this.resources.Add(resource);
		}

        public JaDocument OfTemplate(string templateName){
            this.templateName = templateName;
            return this;
        }

        public JToken Build(JaBuilder builder){

            JObject template = GetMasterTemplate();

            if (template == null) throw new Exception("Template of master cannot be empty");

            List<string> propertiesNeedToRemove = new List<string>(); 

            foreach(var property in template.Properties()){

                if(property.Name.Equals("data", StringComparison.CurrentCultureIgnoreCase)){
                    property.Value = BuildData(builder);
                }else{
                    JToken jt = builder.GetPropertyValue(property.EvaulationKey(), this);

                    if(jt == null || jt.IsEmpty()){
                        propertiesNeedToRemove.Add(property.Name);
                    }else{
                        property.Value = jt;
                    }
                }
            }

            propertiesNeedToRemove.ForEach(p => template.Remove(p));

            return template;
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

		private JObject GetMasterTemplate()
		{
            if (templateName != null )
			{
                return JaTemplates.GetTemplate(templateName);
			}
			else
			{
				return JObject.Parse(Constants.DEFAULT_MASTER);
			}
		}
    }
}
