using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public class JaError : IResource
    {
        private JObject template;

        public JaError(){
            template = JaTemplates.GetTemplate(Constants.ERROR_TEMPLATE_NAME);
        }

        public HttpStatusCode Status { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }

        public virtual JToken Serialize(JaBuilderContext context){

            foreach(var property in template.Properties()){
                context.Populate(property, this);
            }
            return template;
        }

        public JContainer GetContainer() { return new JArray(); }
    }
}
