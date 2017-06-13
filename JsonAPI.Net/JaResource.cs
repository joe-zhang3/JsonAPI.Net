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

    public abstract class JaResource : IResource, ICacheable
    {
        private string type = null;
        private string templateName = null;
        private string link = null;
        private IDictionary<string, string> meta;
        private TypeInfo ti = null;
        public virtual string Id { get; }

        public JaResource(){
            this.ti = this.GetType().GetTypeInfo();
        }

       	/// <summary>
		/// By default, the type is the Pluralize of the class name
		/// </summary>
		/// <value>The type.</value>
		public string Type { 
            get{
				if (type != null) return type;

				return GetType().Name.ToLower().Pluralize();
            }
        }
        /// <summary>
        /// Template name must be pascal, like Accounts
        /// </summary>
        /// <returns>The template.</returns>
        /// <param name="templateName">Template name.</param>
        public JaResource OfTemplate(string templateName){
            this.templateName = templateName;
            return this;
        }

        public JaResource OfType(string type){
            this.type = type;
            return this;
        }

        public JaResource OfLinks(string link){
            this.link = link;
            return this;
        }

        public Uri Link{
            get{
                if (link != null) return new Uri($"{link}/{Id ?? ""}");

                return new Uri($"/{Type}/{Id}");
            }
        }

 		public IDictionary<string, string> Meta
		{
			get
			{
                if (meta == null) meta = new Dictionary<string, string>();

				return meta;
			}
		}

        public JToken Build(JaBuilder builder){
            JObject template = JaTemplates.GetTemplate(templateName != null ? templateName : Type.Pascalize());

            foreach(var property in template.Properties()){
                builder.Populate(property, this);
            }

            return template;
		}
    }
}
