using System;
using System.Text;
using System.Net;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public class JaError : IResource
    {
        private JObject template;

        public JaError(HttpError error):this(){

            Title = "An unexpected error occurs";

            StringBuilder sb = new StringBuilder(error.Count); 

            foreach(var kv in error){
                sb.AppendLine($"{kv.Key} - {kv.Value.ToString()}");
            }
            Detail = sb.ToString();

            StatusCode = HttpStatusCode.InternalServerError;
        }

        public JaError(){
            template = JaTemplates.GetTemplate(Constants.ERROR_TEMPLATE_NAME);
        }

        public HttpStatusCode StatusCode { get; set; }
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
