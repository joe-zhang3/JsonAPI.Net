using System;
using System.Collections.Generic;
using Humanizer;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public class JaResourceBase 
    {
        protected readonly string[] validKeys = { "jsonapi", "meta"};

        public virtual string Id { get; }

		private IDictionary<string, object> meta = new Dictionary<string, object>();
		private ICollection<ILink> links = new List<ILink>();

		public IDictionary<string, object> Meta { get { return meta; } }
        public ICollection<ILink> Links { get { return links; } }

        protected string tempName;
        protected string type ;
        protected string url;
		/// <summary>
		/// Template name must be pascal, like Accounts
		/// </summary>
		/// <returns>The template.</returns>
		/// <param name="templateName">Template name.</param>
		public JaResourceBase OfTemplate(string templateName){
			this.tempName = templateName;
			return this;
		}

		public JaResourceBase OfType(string type)
		{
			this.type = type;
			return this;
		}
		public JaResourceBase OfURL(string url)
		{
			this.url = url;
			return this;
		}
		/// <summary>
		/// By default, the type is the Pluralize of the class name
		/// </summary>
		/// <value>The type.</value>
		public string Type
		{
			get
			{
				if (type != null) return type;

				return GetType().Name.ToLower().Pluralize();
			}
		}


        public virtual Uri Self
		{
			get
			{
				if (url != null) return new Uri($"{url}/{Id ?? ""}");

				return new Uri($"/{Type}/{Id}");
			}
		}


		public JToken Build(JaBuilder builder){
			return Build(builder, tempName);
		}

        public virtual JToken Build(JaBuilder builder, string templateName){
            throw new NotImplementedException();
        }

        public JContainer GetContainer(){
            return new JArray();
        }
    }
}
