using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    internal sealed class JaTemplates
    {
        private static IDictionary<string, JObject> templates = null;

        internal static void Intialize(string templatePath){
            templates = JaTemplateScanner.Scan(templatePath);
        }

        public static JObject GetTemplate(string templateName){
            return GetTemplate(templateName, true);
        }

		public static JObject GetTemplate(string templateName, bool useCopy)
		{
            JObject jb;

            if(!templates.TryGetValue(templateName, out jb)){

                if(templateName.Equals(Constants.DEFAULT_RELATIONSHIP_NAME)){
                    jb = JObject.Parse(Constants.DEFAULT_RELATIONSHIP);
                    templates.Add(Constants.DEFAULT_RELATIONSHIP_NAME, jb);
                }else{
                    throw new Exception($"Template {templateName} does not exist");
				}
            }

            return useCopy ? (JObject)jb.DeepClone() : jb;
		}
    }
}
