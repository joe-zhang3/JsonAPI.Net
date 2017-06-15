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
        private bool hasError;

        public JaDocument(IEnumerable<IResource> resources){
            this.resources.AddRange(resources);
            CheckError();
        }

        public JaDocument(IResource resource){
            this.resources.Add(resource);
            CheckError();
		}

        /// <summary>
        /// Set template for each resource as their template might be differnt from their own one.
        /// 
        /// E.g. we may use a different template when searching items and getting item details even resource is the same.
        /// </summary>
        /// <param name="templateName">Template name.</param>

        public void PreSetTemplate(string templateName){

            if (templateName == null) return;

            foreach(var resource in resources){
                (resource as JaResourceBase)?.OfTemplate(templateName);
            }
        }

        private void CheckError(){
            hasError = resources.Any(r => r is JaError);
        }

        public override JToken Serialize(JaBuilderContext context){

            PreSetTemplate(context.ActionTemplate);

            JObject masterTemplate = GetTemplate(Constants.MASTER_TEMPLATE_NAME);

            List<string> propertiesNeedToRemove = new List<string>(); 

            foreach(var property in masterTemplate.Properties()){
                if (!hasError && property.Name.Equals("data"))
                {
                    property.Value = BuildResources(context);
                }
                else if (!hasError && property.Name.Equals("included"))
                {
                    property.Value = BuildResources(new JaBuilderContext(context.RequestMessage), context.IncludedResources, true);
                }
                else if (hasError && property.Name.Equals("errors"))
                {
                    property.Value = BuildResources(context);
                } else {
                    string key = property.EvaulationKey();

                    if (key == null) continue;

                    property.Value = context.GetPropertyValue(key, this);
                }

                if(property.Value == null || property.Value.IsEmpty()){
                    propertiesNeedToRemove.Add(property.Name);
                }
            }

            propertiesNeedToRemove.ForEach(p => masterTemplate.Remove(p));

            return masterTemplate;
        }

        private JToken BuildResources(JaBuilderContext context){
            return BuildResources(context, resources);
        }

        private JToken BuildResources(JaBuilderContext context, ICollection<IResource> items, bool forceToUseArray = false)
        {
            if (items == null) return null;

			if (items.Count > 1 || forceToUseArray){
				JArray array = new JArray();

                foreach(var item in items){
                    array.Add(item.Serialize(context));
                }

				return array;
			}
			else
			{
				return items.First().Serialize(context);
			}
        }

        public override IList<ILink> BuildDefaultLinks()
        {
            return null;
        }
    }
}
