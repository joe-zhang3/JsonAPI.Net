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
            LoadBuildInTemplates();
        }

        public static JObject GetTemplate(string templateName){
            return GetTemplate(templateName, true);
        }

		public static JObject GetTemplate(string templateName, bool useCopy)
		{
            JObject jb;

            if(!templates.TryGetValue(templateName, out jb)){
                throw new Exception($"Template {templateName} does not exist");
			}

            return useCopy ? (JObject)jb.DeepClone() : jb;
		}

        private static void LoadBuildInTemplates(){
            TryAdd(Constants.MASTER_TEMPLATE_NAME, Constants.DEFAULT_MASTER_TEMPLATE);
            TryAdd(Constants.ERROR_TEMPLATE_NAME, Constants.DEFAULT_ERROR_TEMPLATE);
            TryAdd(Constants.RELATIONSHIP_TEMPLATE_NAME, Constants.DEFAULT_RELATIONSHIP_TEMPLATE);
        }

        private static void TryAdd(string key, string value){
			if (!templates.ContainsKey(key))
			{
                JObject jo = JObject.Parse(value);
				templates.Add(key, jo);
			}
        }
    }
}
